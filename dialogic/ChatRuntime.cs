using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

namespace Dialogic
{
    class ChatScheduler
    {
        public Chat chat;
        public Ask prompt;
        public ChatRuntime runtime;
        public Stack<Chat> resumables;

        public ChatScheduler(ChatRuntime runtime)
        {
            this.runtime = runtime;
            this.resumables = new Stack<Chat>();
        }

        public void StartNew(Chat next)
        {
            if (chat != null) resumables.Push(chat);
            (chat = next).Run();
        }

        public void PauseCurrent()
        {
            if (chat != null) resumables.Push(chat);
            chat = null;
        }

        public int ResumeLast(bool mustBeResumable = false)
        {
            if (resumables.IsNullOrEmpty())
            {
                throw new DialogicException("No Chat to resume");
            }

            if (chat != null)
            {
                throw new DialogicException("Cannot resume chat" +
                    " while Chat#" + chat.Text + " is active");
            }

            var last = resumables.Pop();
            while (mustBeResumable && !last.IsResumable())
            {
                if (resumables.Count < 1) throw new DialogicException
                    ("No resumable Chat found on stack");
                
                last = resumables.Pop();
            }

            (chat = last).Run(false);

            return Util.Millis();
        }
    }

    public class ChatRuntime
    {
        public static string logFile;// = "../../../dialogic/dia.log";

        private bool logInitd;
        private int nextEventTime;
        private Thread searchThread;
        private List<Chat> chats;
        private ChatScheduler scheduler;

        public ChatRuntime(List<Chat> chats)
        {
            this.chats = chats;
            this.scheduler = new ChatScheduler(this);
        }

        public void Run(string chatLabel = null)
        {
            if (Util.IsNullOrEmpty(chats)) throw new Exception("No chats found");

            scheduler.StartNew((chatLabel != null) ? FindByName(chatLabel) : chats[0]);
        }

        public List<Chat> Chats()
        {
            return chats;
        }

        public IUpdateEvent Update(IDictionary<string, object> globals, ref EventArgs ge)
        {
            return ge != null ? HandleGameEvent(ref ge, globals) : HandleChatEvent(globals);
        }

        private IUpdateEvent HandleGameEvent(ref EventArgs ge, IDictionary<string, object> globals)
        {
            if (ge is IChoice) return HandleChoiceEvent(ref ge, globals);
            if (ge is IResume) return HandleResumeEvent(ref ge, globals);
            throw new DialogicException("Unexpected event-type: " + ge.GetType());
        }

        private IUpdateEvent HandleResumeEvent(ref EventArgs ge, IDictionary<string, object> globals)
        {
            IResume ir = (IResume)ge;
            var label = ir.ResumeWith();
            ge = null;

            if (String.IsNullOrEmpty(label))
                nextEventTime = scheduler.ResumeLast();
            else
                scheduler.StartNew(FindByName(label));

            return null;
        }

        private IUpdateEvent HandleChoiceEvent(ref EventArgs ge, IDictionary<string, object> globals)
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
                opt.Realize(globals);

                if (opt.action != Command.NOP)
                {
                    FindAsync((Find)opt.action); // find next
                }
                else
                {
                    scheduler.chat = scheduler.prompt.parent; // just continue
                }
                return null;
            }
        }

        private IUpdateEvent HandleChatEvent(IDictionary<string, object> globals)
        {
            Command cmd = null;

            if (scheduler.chat != null && Util.Millis() >= nextEventTime)
            {
                cmd = scheduler.chat.Next();
                if (cmd != null)
                {
                    cmd.Realize(globals);
                    LogCommand(cmd);

                    if (cmd is ISendable)
                    {
                        if (cmd.GetType() == typeof(Wait))
                        {
                            if (cmd.DelayMs != Util.INFINITE)
                            {
                                ComputeNextEventTime(cmd);
                                return null;
                            }
                            scheduler.PauseCurrent();  // wait for ResumeEvent
                        }
                        else if (cmd is Ask)
                        {
                            scheduler.prompt = (Ask)cmd;
                            scheduler.PauseCurrent();  // wait for ChoiceEvent
                        }
                        else
                        {
                            ComputeNextEventTime(cmd); // compute delay
                        }

                        return new UpdateEvent(cmd); // fire event
                    }
                    else if (cmd is Find)
                    {
                        FindAsync((Find)cmd);
                    }
                }
            }
            return null;
        }

        private void ComputeNextEventTime(Command cmd)
        {
            nextEventTime = cmd.DelayMs >= 0 ? Util.Millis()
                + cmd.ComputeDuration() : Int32.MaxValue;
        }

        public Chat FindByName(string label)
        {
            if (label.StartsWith("#", Util.IC)) label = label.Substring(1);

            Chat result = null;

            for (int i = 0; i < chats.Count; i++)
            {
                if (chats[i].Text == label)
                {
                    result = chats[i];
                    break;
                }
            }
            return result;
        }

        public void FindAsync(Find finder, IDictionary<string, object> globals = null)
        {
            scheduler.PauseCurrent();

            int ts = Util.Millis();
            (searchThread = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                Chat chat = null;

                if (finder is Go)
                {
                    chat = FindByName(((Go)finder).Text);
                }
                else
                {
                    chat = FuzzySearch.Find(chats, finder.realized, globals);
                    //Console.WriteLine("Found " + chat.Text + " in " + Util.Millis(ts) + "ms");
                }

                if (chat == null) throw new FindException(finder);

                scheduler.StartNew(chat);

            })).Start();
        }

        // for testing only
        public Chat Find(Find f, IDictionary<string, object> globals = null)
        {
            return FuzzySearch.Find(chats, f.meta, globals);
        }

        // for testing only
        public List<Chat> FindAll(Find f, IDictionary<string, object> globals = null)
        {
            return FuzzySearch.FindAll(chats, f.meta, globals);
        }

        // for testing only
        public List<Chat> FindAll(Constraints constraints, IDictionary<string, object> globals = null)
        {
            return FuzzySearch.FindAll(chats, constraints.AsDict(), globals);
        }

        private bool Logging()
        {
            return logFile != null;
        }

        public void LogCommand(Command c)
        {
            if (logFile == null) return;

            if (!logInitd)
            {
                logInitd = true;
                File.WriteAllText(logFile, "============\n");
            }

            using (StreamWriter w = File.AppendText(logFile))
            {
                var now = DateTime.Now.ToString("HH:mm:ss.fff", CultureInfo.InvariantCulture);
                w.WriteLine(now + "\t" + c + " @" + Util.Millis());
            }
        }
    }

}