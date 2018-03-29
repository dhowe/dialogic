using System;
using System.Collections.Generic;
using System.IO;

namespace Dialogic
{
    internal class AppEventHandler
    {
        private readonly ChatRuntime runtime;
        private readonly ChatScheduler scheduler;
        private Find findDelegate;

        internal AppEventHandler(ChatRuntime rt)
        {
            this.runtime = rt;
            this.scheduler = rt.scheduler;
        }

        internal IUpdateEvent OnEvent(ref EventArgs ge, IDictionary<string, object> globals)
        {
            if (ge is IUserEvent) return UserActionHandler(ref ge, globals);
            if (ge is ISuspend) return SuspendHandler(ref ge, globals);
            if (ge is IResume) return ResumeHandler(ref ge, globals);
            if (ge is IChoice) return ChoiceHandler(ref ge, globals);
            if (ge is IClear) return ClearHandler(ref ge, globals);

            throw new DialogicException("Unexpected event-type: " + ge.GetType());
        }

        private IUpdateEvent UserActionHandler(ref EventArgs ge, IDictionary<string, object> globals)
        {
            IUserEvent ue = (IUserEvent)ge;
            var label = ue.GetEventType();
            ge = null;

            scheduler.Suspend();
            scheduler.Launch("#On" + label + "Event");
            return null;
        }

        private IUpdateEvent SuspendHandler(ref EventArgs ge, IDictionary<string, object> globals)
        {
            ge = null;
            scheduler.Suspend();
            return null;
        }

        private IUpdateEvent ClearHandler(ref EventArgs ge, IDictionary<string, object> globals)
        {
            ge = null;
            scheduler.Clear();
            return null;
        }

        private IUpdateEvent ResumeHandler(ref EventArgs ge, IDictionary<string, object> globals)
        {
            IResume ir = (IResume)ge;
            var label = ir.ResumeWith();
            ge = null;

            if (String.IsNullOrEmpty(label))
            {
                runtime.nextEventTime = scheduler.Resume();
            }
            else if (label.StartsWith("#", Util.IC))
            {
                scheduler.Launch(label);
            }
            else // else, parse as FIND meta data
            {
                if (findDelegate == null) findDelegate = new Find();

                try
                {
                    findDelegate.Init(label);
                }
                catch (ParseException e)
                {
                    throw new RuntimeParseException(e);
                }

                runtime.FindAsync(findDelegate, globals);
            }

            return null;
        }

        private IUpdateEvent ChoiceHandler(ref EventArgs ge, IDictionary<string, object> globals)
        {
            IChoice ic = (IChoice)ge;
            var idx = ic.GetChoiceIndex();
            ge = null;

            if (idx < 0 || idx >= scheduler.prompt.Options().Count)
            {
                // bad index, so reprompt for now
                Console.WriteLine("Invalid index " + idx + ", reprompting\n");
                scheduler.prompt.Realize(globals); // re-realize
                return new UpdateEvent(scheduler.prompt);
            }
            else
            {
                Opt opt = scheduler.prompt.Selected(idx);
                //opt.Realize(globals); // not needed

                if (opt.action != Command.NOP)
                {
                    runtime.FindAsync((Find)opt.action); // find next
                }
                else
                {
                    scheduler.chat = scheduler.prompt.parent; // just continue
                }
                return null;
            }
        }
    }

    internal class ChatEventHandler
    {
        private readonly ChatRuntime runtime;
        private readonly ChatScheduler scheduler;
        private bool logInitd = false;

        internal ChatEventHandler(ChatRuntime rt)
        {
            this.runtime = rt;
            this.scheduler = rt.scheduler;
        }

        internal IUpdateEvent OnEvent(IDictionary<string, object> globals)
        {
            Command cmd = null;
            IUpdateEvent iue = null;

            if (scheduler.Ready() && Util.Millis() >= runtime.nextEventTime) // move time-check to scheduler
            {
                if (scheduler.next != null)
                {
                    //Console.WriteLine("NEXT: ");
                    return scheduler.LaunchNext();
                }

                cmd = scheduler.chat.Next();

                if (cmd != null)
                {
                    cmd.Realize(globals);
                    WriteToLog(cmd);
                    iue = HandleSendable(cmd, globals);
                }
                else
                {
                    // Here the Chat has completed without redirecting 
                    // so we check the stack for a chat to resume

                    //iue = new UpdateEvent(
                    //    new KeyValuePair<string, object>(Meta.TEXT, scheduler.chat.Text),
                    //    new KeyValuePair<string, object>(Meta.TYPE, scheduler.chat.TypeName()),
                    //    new KeyValuePair<string, object>(Meta.STATE, "100%")
                    //); // fire Chat-complete event
                    //Console.WriteLine("UpdateEvt:"+iue);

                    scheduler.OnComplete();

                    //if (nextEventMs > -1)
                    //{
                    //    //Console.WriteLine("<#" + scheduler.chat.Text + "-resumed>");
                    //    runtime.nextEventTime = nextEventMs;
                    //}

                    ////Console.WriteLine("<#" + scheduler.chat.Text + "-finished>");
                    //int nextEventMs = scheduler.Completed();

                    //if (nextEventMs > -1)
                    //{
                    //    //Console.WriteLine("<#" + scheduler.chat.Text + "-resumed>");
                    //    runtime.nextEventTime = nextEventMs;
                    //}

                }
            }

            return iue;
        }

        private IUpdateEvent HandleSendable(Command cmd, IDictionary<string, object> globals)
        {
            if (cmd is ISendable)
            {
                if (cmd.GetType() == typeof(Wait))
                {
                    // just pause internally, no event needs to be fired
                    if (cmd.DelayMs != Util.INFINITE)
                    {
                        runtime.ComputeNextEventTime(cmd);
                        return null;
                    }
                    scheduler.Suspend();
                }
                else if (cmd is Ask)
                {
                    scheduler.prompt = (Ask)cmd;
                    scheduler.Suspend();                 // wait on Choice
                }
                else
                {
                    runtime.ComputeNextEventTime(cmd); // compute delay
                }

                return new UpdateEvent(cmd);         // fire event
            }
            else if (cmd is Find) // or Go
            {
                scheduler.OnComplete();
                runtime.FindAsync((Find)cmd);      // find next
            }

            return null;
        }

        internal void WriteToLog(Command c)
        {
            if (ChatRuntime.logFile == null) return;

            if (!logInitd)
            {
                logInitd = true;
                File.WriteAllText(ChatRuntime.logFile, "============\n");
            }

            using (StreamWriter w = File.AppendText(ChatRuntime.logFile))
            {
                var now = DateTime.Now.ToString("HH:mm:ss.fff");
                w.WriteLine(now + "\t" + c + " @" + Util.Millis());
            }
        }
    }

}
