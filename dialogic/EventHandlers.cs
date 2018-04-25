using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Dialogic
{
    internal class AppEventHandler
    {
        private readonly ChatRuntime runtime;
        private readonly ChatScheduler scheduler;
        private Find findDelegate, updateDelegate;

        internal AppEventHandler(ChatRuntime rt)
        {
            this.runtime = rt;
            this.scheduler = rt.scheduler;
        }

        internal IUpdateEvent OnEvent(ref EventArgs ea, IDictionary<string, object> globals)
        {
            if (runtime.immediateMode) return null; // ignore all app events

            if (ea is IChatUpdate) return ChatUpdateHandler(ref ea, globals);
            if (ea is IUserEvent) return UserActionHandler(ref ea, globals);
            if (ea is ISuspend) return SuspendHandler(ref ea, globals);
            if (ea is IResume) return ResumeHandler(ref ea, globals);
            if (ea is IChoice) return ChoiceHandler(ref ea, globals);
            if (ea is IClear) return ClearHandler(ref ea, globals);

            // ea = null; TODO:

            throw new DialogicException("Unexpected event-type: " + ea.GetType());
        }

        private IUpdateEvent ChatUpdateHandler(ref EventArgs ea, IDictionary<string, object> globals)
        {
            IChatUpdate evt = (IChatUpdate)ea;
            var findBy = evt.FindByCriteria();
            var action = evt.GetAction();

            ea = null;

            if (String.IsNullOrEmpty(findBy))        // apply action to all chats
            {
                runtime.Chats().ForEach(action);
            }
            else if (findBy.StartsWith(Util.LABEL_IDENT, Util.IC)) // to one chat
            {
                action.Invoke(runtime.FindChatByLabel(findBy));
            }
            else                                  // to all those matching findBy
            {
                UpdateFinder(ref updateDelegate, findBy);
                runtime.FindAllAsync(updateDelegate, action, globals);
            }

            return null;
        }

        private IUpdateEvent UserActionHandler(ref EventArgs ea, IDictionary<string, object> globals)
        {
            IUserEvent ue = (IUserEvent)ea;
            var label = ue.GetEventType();
            ea = null;

            scheduler.Suspend();
            scheduler.Launch("#On" + label + "Event");

            return null;
        }

        private IUpdateEvent SuspendHandler(ref EventArgs ea, IDictionary<string, object> globals)
        {
            ea = null;
            scheduler.Suspend();
            return null;
        }

        private IUpdateEvent ClearHandler(ref EventArgs ea, IDictionary<string, object> globals)
        {
            ea = null;
            scheduler.Clear();
            return null;
        }

        private IUpdateEvent ResumeHandler(ref EventArgs ea, IDictionary<string, object> globals)
        {
            IResume ir = (IResume)ea;
            var label = ir.ResumeWith();

            ea = null;

            if (String.IsNullOrEmpty(label))                        // resume current
            {
                scheduler.nextEventTime = scheduler.Resume();
            }
            else if (label.StartsWith(Util.LABEL_IDENT, Util.IC)) // resume specified
            {
                scheduler.Suspend();
                scheduler.Launch(label);
            }
            else                                      // resume chat returned by Find
            {
                scheduler.Suspend();
                UpdateFinder(ref findDelegate, label);
                runtime.FindAsync(findDelegate, globals);
            }

            return null;
        }

        private static void UpdateFinder(ref Find finder, string label)
        {
            if (finder == null) finder = new Find();

            try
            {
                finder.Init(label);
            }
            catch (ParseException e)
            {
                throw new RuntimeParseException(e);
            }
        }


        private IUpdateEvent ChoiceHandler(ref EventArgs ea, IDictionary<string, object> globals)
        {
            IChoice ic = (IChoice)ea;
            var idx = ic.GetChoiceIndex();
            ea = null;

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
                    // We've gotten a response with a branch, so finish & take it
                    scheduler.Completed(false);
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
        private bool logInitd;

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
                cmd = scheduler.chat.Next();

                if (cmd != null)
                {
                    cmd.Realize(globals);
                    return HandleCommand(cmd, globals);
                }
                else
                {
                    // Here the Chat has completed without redirecting 
                    // so we check the stack for a chat to resume
                    scheduler.Completed(true);
                }
            }

            return null;
        }

        private IUpdateEvent HandleCommand(Command cmd,
            IDictionary<string, object> globals)
        {
            if (ChatRuntime.LOG_FILE != null) WriteToLog(cmd);

            if (cmd is ISendable)
            {
                if (!runtime.immediateMode)
                {
                    if (cmd.GetType() == typeof(Wait))
                    {
                        if (cmd.delay > Util.INFINITE) // non-infinite WAIT?
                        {
                            // just pause internally, no event needs to be fired
                            ComputeNextEventTime(cmd);
                            return null;
                        }
                        scheduler.Suspend();          // suspend on infinite WAIT
                    }
                    else if (cmd is Ask)
                    {
                        scheduler.prompt = (Ask)cmd;
                        scheduler.Suspend();         // wait on ChoiceEvent
                    }
                    else
                    {
                        ComputeNextEventTime(cmd); // compute delay for next cmd
                    }
                }
                else if (cmd is Ask)
                {
                    var opt = Util.RandItem(((Ask)cmd).Options());
                    //Console.WriteLine(opt);
                    cmd = ((Ask)cmd).ToSay(); // convert Ask to Say
                    var finder = new Go().Init(opt.action.text);
                    runtime.FindAsync(finder);  // then do Find
                }

                return new UpdateEvent((Dialogic.ISendable)cmd); // fire cmd event
            }
            else if (cmd is Find)
            {
                scheduler.Completed(false); // finish 
                runtime.FindAsync((Find)cmd);  // then do Find
            }

            return null;
        }

        internal void ComputeNextEventTime(Command cmd)
        {
            scheduler.nextEventTime = (cmd.delay >= 0 ? Util.Millis()
                + (int)(cmd.ComputeDuration() * Defaults.GLOBAL_TIME_SCALE)
                : Int32.MaxValue);
        }

        public void WriteToLog(Command c)
        {
            if (!logInitd)
            {
                logInitd = true;
                File.WriteAllText(ChatRuntime.LOG_FILE, "============\n");
            }

            using (StreamWriter w = File.AppendText(ChatRuntime.LOG_FILE))
            {
                var now = DateTime.Now.ToString("HH:mm:ss.fff", CultureInfo.InvariantCulture);
                w.WriteLine(now + "\t" + c + " @" + Util.Millis());
            }
        }
    }
}
