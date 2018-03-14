using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

namespace Dialogic
{
    public class ChatRuntime
    {
        public static string logFile;// = "../../../dialogic/dia.log";

        private List<Chat> chats;
        private Ask currentPrompt;
        private Chat lastChat, currentChat;

        private bool logInitd;
        private int nextEventTime;
        private Thread searchThread;

        public ChatRuntime(List<Chat> chats)
        {
            this.chats = chats;
        }

        public List<Chat> Chats()
        {
            return chats;
        }

        public void Run(string chatLabel = null)
        {
            if (Util.IsNullOrEmpty(chats)) throw new Exception("No chats!");

            currentChat = chatLabel != null ? FindByName(chatLabel) : chats[0];
            currentChat.lastRunAt = Util.EpochMs();
        }

        public IUpdateEvent Update(IDictionary<string, object> globals, ref GameEvent ge)
        {
            return ge != null ? HandleGameEvent(ref ge, globals) : HandleChatEvent(globals);
        }

        private IUpdateEvent HandleGameEvent(ref GameEvent ge, IDictionary<string, object> globals)
        {
            if (ge is IChoice) return HandleChoiceEvent(ref ge, globals);
            if (ge is IResume) return HandleResumeEvent(ref ge, globals);
            throw new DialogicException("Unexpected event-type: " + ge.GetType());
        }

        private IUpdateEvent HandleResumeEvent(ref GameEvent ge, IDictionary<string, object> globals)
        {
            IResume ir = (IResume)ge;

            // WORKING HERE

            throw new NotImplementedException();
        }

        private IUpdateEvent HandleChoiceEvent(ref GameEvent ge, IDictionary<string, object> globals)
        {
            //Console.WriteLine("HANDLE-CHOICE " + ge);

            IChoice ic = (IChoice)ge;
            var idx = ic.GetChoiceIndex();
            ge = null;

            if (idx < 0 || idx >= currentPrompt.Options().Count)
            {
                // bad index, so reprompt for now
                Console.WriteLine("Invalid index " + idx + ", reprompting\n");
                currentPrompt.Realize(globals); // re-realize
                return new UpdateEvent(currentPrompt);
            }
            else
            {
                Opt opt = currentPrompt.Selected(idx);
                opt.Realize(globals);

                if (opt.action != Command.NOP)
                {
                    FindAsync((Find)opt.action); // find next
                }
                else
                {
                    //throw new DialogicException("opt with no GO: " + opt);
                    currentChat = currentPrompt.parent; // continue
                }
                return null;
            }
        }

        private IUpdateEvent HandleChatEvent(IDictionary<string, object> globals)
        {
            Command cmd = null;

            if (currentChat != null && Util.Millis() >= nextEventTime)
            {
                cmd = currentChat.Next();
                if (cmd != null)
                {
                    cmd.Realize(globals);
                    LogCommand(cmd);

                    if (cmd is ISendable)
                    {
                        if (cmd is Wait && cmd.DelayMs == Util.INFINITE)
                        {
                            PauseCurrentChat();  // wait until ResumeEvent
                        }
                        else if (cmd is Ask)
                        {
                            currentPrompt = (Ask)cmd;
                            PauseCurrentChat(); // wait until ChoiceEvent
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

        private void StartChat(Chat chat)
        {
            lastChat = currentChat;
            currentChat = chat;
            currentChat.Reset();
            currentChat.lastRunAt = Util.EpochMs();
        }

        private void PauseCurrentChat()
        {
            if (false && currentChat == null)
            {
                throw new DialogicException("Attempt to pause null chat");
            }
            lastChat = currentChat;
            currentChat = null;
        }

        private void ResumeLastChat(int msBeforeResuming = 0)
        {
            if (lastChat == null)
            {
                throw new DialogicException("Attempt to resume null chat");
            }
            if (currentChat != null)
            {
                throw new DialogicException("Attempt to resume chat while chat "
                    + currentChat.Text + " is active");
            }
            currentChat = lastChat;
            currentChat.lastRunAt = Util.EpochMs();
            nextEventTime = Util.Millis() + msBeforeResuming; // now
            lastChat = null;
        }

        public Chat FindByName(string label)
        {
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
            PauseCurrentChat();

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

                StartChat(chat);

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
                w.WriteLine(now + "\t" + c);
            }
        }
    }

}