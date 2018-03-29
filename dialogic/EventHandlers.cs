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
            scheduler.Start("#On" + label + "Event");
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
                scheduler.Resume();
            }
            else if (label.StartsWith("#", Util.IC))
            {
                scheduler.Start(label);
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

        private IUpdateEvent ChoiceHandler(ref EventArgs ge,
            IDictionary<string, object> globals)
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
                    scheduler.current = scheduler.prompt.parent; // just continue
                }
                return null;
            }
        }
    }

    internal class ChatEventHandler
    {
        private readonly ChatRuntime runtime;
        private readonly ChatScheduler scheduler;
        private bool logInitialized = false;

        internal ChatEventHandler(ChatRuntime rt)
        {
            this.runtime = rt;
            this.scheduler = rt.scheduler;
        }

        internal IUpdateEvent OnEvent(IDictionary<string, object> globals)
        {
            Command cmd = null;

            if (scheduler.chatEvent != null)
            {
                var toSend = scheduler.chatEvent;
                scheduler.chatEvent = null;
                return toSend;
            }

            if (scheduler.Ready())
            {
                cmd = scheduler.current.Next();

                if (cmd != null)
                {
                    cmd.Realize(globals);

                    WriteToLog(cmd);

                    return HandleCommand(cmd);
                }
                else
                {
                    // Here the Chat has completed without redirecting 
                    // so we check the stack for a chat to resume

                    scheduler.Finish(true);
                }
            }
            return null;
        }

        private IUpdateEvent HandleCommand(Command cmd)
        {
            if (cmd is ISendable)
            {
                if (cmd.GetType() == typeof(Wait))
                {
                    // just pause internally, no event needs to be fired
                    if (cmd.DelayMs != Util.INFINITE)
                    {
                        ComputeNextEventTime(cmd);
                        return null;
                    }
                    scheduler.Suspend();            // wait on ResumeEvent
                }
                else if (cmd is Ask)
                {
                    scheduler.prompt = (Ask)cmd;
                    scheduler.Suspend();         // wait on ChoiceEvent
                }
                else
                {
                    ComputeNextEventTime(cmd); // compute delay
                }

                return new UpdateEvent(cmd); // fire event
            }
            else if (cmd is Find) // or Go
            {
                runtime.FindAsync((Find)cmd);      // find next
            }

            return null;
        }

        private void ComputeNextEventTime(Command cmd)
        {
            scheduler.nextEventTime = cmd.DelayMs >= 0 ? 
                Util.Millis() + cmd.ComputeDuration() : -1;
        }

        private void WriteToLog(Command c)
        {
            if (ChatRuntime.LOG_FILE == null) return;

            if (!logInitialized)
            {
                logInitialized = true;
                File.WriteAllText(ChatRuntime.LOG_FILE, "============\n");
            }

            using (StreamWriter w = File.AppendText(ChatRuntime.LOG_FILE))
            {
                var now = DateTime.Now.ToString("HH:mm:ss.fff");
                w.WriteLine(now + "\t" + c + " @" + Util.Millis());
            }
        }
    }

}