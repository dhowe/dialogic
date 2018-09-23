using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

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
            if (ea is IChatUpdate) return ChatUpdateHandler(ref ea, globals);
            if (ea is IUserEvent) return UserActionHandler(ref ea, globals);
            if (ea is ISuspend) return SuspendHandler(ref ea, globals);
            if (ea is IResume) return ResumeHandler(ref ea, globals);
            if (ea is IChoice) return ChoiceHandler(ref ea, globals);
            if (ea is IClear) return ClearHandler(ref ea, globals);
            if (ea is ISaveEvent) return SaveHandler(ref ea, globals);

            //if (ea is ILoadEvent) return MergeHandler(ref ea, globals);
            //if (ea is ILoadChatsEvent) return UpdateChatsHandler(ref ea, globals);

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
            else if (findBy.StartsWith(Util.LABEL_IDENT, Util.INV)) // to one chat
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

        private IUpdateEvent SaveHandler(ref EventArgs ea, IDictionary<string, object> globals)
        {
            ISaveEvent evt = (ISaveEvent)ea;
            ea = null;
            runtime.SaveAsync(evt.GetSerializer(), evt.GetFile(), (bytes) =>
            {
                if (!ChatRuntime.SILENT) Console.WriteLine
                    ("SAVE: " + (bytes != null ? bytes.Length + " bytes" : "Failed"));
            });
            return null;
        }

        /*
        private IUpdateEvent MergeHandler(ref EventArgs ea, IDictionary<string, object> globals)
        {
            ILoadEvent evt = (ILoadEvent)ea;
            ea = null;
            runtime.MergeAsync(evt.GetSerializer(), evt.GetFile(), () =>
            {
                if (!ChatRuntime.SILENT) Console.WriteLine
                    ("LOAD: " + (runtime.chats != null ? runtime.chats.Count+ " chats" : "Failed"));
            });
            return null;
        }

        private IUpdateEvent UpdateChatsHandler(ref EventArgs ea, IDictionary<string, object> globals)
        {
            ILoadChatsEvent evt = (ILoadChatsEvent)ea;
            ea = null;
            runtime.LoadChatsAsync(evt.GetChats());
            return null;
        }*/

        private IUpdateEvent ResumeHandler(ref EventArgs ea, IDictionary<string, object> globals)
        {
            IResume ir = (IResume)ea;
            var label = ir.ResumeWith();

            ea = null;

            if (String.IsNullOrEmpty(label))                        // resume current
            {
                scheduler.nextEventTime = scheduler.Resume();
            }
            else if (label.StartsWith(Util.LABEL_IDENT, Util.INV)) // resume specified
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
                ChatRuntime.Info("Invalid index " + idx + ", reprompting\n");

                return new UpdateEvent(scheduler.prompt.Resolve(globals));
            }
            else
            {
                Opt opt = scheduler.prompt.Selected(idx);

Console.WriteLine("CHOICE: "+idx+" "+opt.text+" ");
                if (opt.action != Command.NOP)
                {
Console.WriteLine("ACTION: "+opt.text+" "+opt.action.text);

                    // We've gotten a response with a branch, so finish & take it
                    scheduler.Completed(false);
                    opt.action.Resolve(globals);
                    runtime.FindAsync((Find)opt.action); // find next

                    // Question: what if the Chat is unfinished (and resumable)?
                    // Shouldn't we return to it later? See ticket #154
                }
                else
                {

Console.WriteLine("CONTINUING: "+ scheduler.chat.text);
                    scheduler.Resume();
//scheduler.chat = scheduler.prompt.parent; // just continue
//Console.WriteLine("CONTINUING: " + scheduler.resumables, scheduler.Waiting());
//Console.WriteLine("--- Stack contents ---");
//foreach (var i in scheduler.resumables)
//{
//Console.WriteLine(i);
//}
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

        internal IUpdateEvent HandleCommand(Command cmd,
            IDictionary<string, object> globals)
        {
            if (ChatRuntime.LOG_FILE != null) WriteToLog(cmd);

            cmd.Resolve(globals);

            if (cmd is ISendable)
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
                    scheduler.Suspend();          // wait on ChoiceEvent
                }
                else
                {
                    ComputeNextEventTime(cmd);   // compute delay for next cmd
                }

                return new UpdateEvent(cmd.Resolve(globals)); // fire event
            }
            else if (cmd is Find)
            {
                scheduler.Completed(false);
                runtime.FindAsync((Find)cmd);      // finish, then do the Find
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
