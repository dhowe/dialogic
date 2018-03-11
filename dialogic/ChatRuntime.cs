using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

namespace Dialogic
{
    public class ChatRuntime
    {
        public static string logFile;// "../../../dialogic/dia.log";

        private List<Chat> chats;
        private Chat currentChat;
        private Ask prompt;

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
        }

        public IUpdateEvent Update(IDictionary<string, object> globals, ref IChoice choice)
        {
            return (choice != null) ? HandleChoiceEvent(ref choice, globals) : HandleChatEvent(globals);
        }

        private IUpdateEvent HandleChoiceEvent(ref IChoice choice, IDictionary<string, object> globals)
        {
            var idx = choice.GetChoiceIndex();
            choice = null;

            if (idx < 0 || idx >= prompt.Options().Count)
            {
                // bad index, so reprompt for now
                Console.WriteLine("Invalid index " + idx + ", reprompting\n");
                return new UpdateEvent(prompt.Realize(globals));
            }
            else
            {
                Opt opt = prompt.Selected(idx);
                currentChat = prompt.parent;

                if (opt.action != Command.NOP)
                {
                    // opt.action.Realize(globals); #10
                    DoFind(opt.action);
                }
            }

            return null;
        }

        private void DoFind(Command finder)
        {
            FindAsync((Find)finder);
        }

        private void StartChat(Chat chat)
        {
            currentChat.Reset();
            currentChat = chat;
        }

        private IUpdateEvent HandleChatEvent(IDictionary<string, object> globals)
        {
            Command cmd = null;
            UpdateEvent ue = null;

            if (currentChat != null && Util.Millis() >= nextEventTime)
            {
                cmd = currentChat.Next();

                if (cmd != null)
                {
                    cmd.Realize(globals);

                    if (cmd is ISendable)
                    {
                        if (cmd is Ask)
                        {
                            prompt = (Dialogic.Ask)cmd;
                            currentChat = null;
                        }

                        ue = new UpdateEvent(cmd.data);
                    }
                    else if (cmd is Find)
                    {
                        DoFind(cmd);
                    }

                    nextEventTime = Util.Millis() + cmd.ComputeDuration();
                    LogCommand(cmd);
                }
            }

            return ue;
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

        public void FindAsync(Find finder)
        {
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
                    chat = FuzzySearch.Find(chats, finder.meta);
                    Console.WriteLine("Found " + chat.Text + " in " + Util.Millis(ts) + "ms");
                }

                if (chat == null) throw new FindException(finder);

                StartChat(chat);

            })).Start();
        }

        //public Chat Find(Find finder)
        //{
        //    return FuzzySearch.Find(chats, finder.meta);
        //}

        public List<Chat> FindAll(Find finder)  // for tests
        {
            return FuzzySearch.FindAll(chats, finder.meta);
        }

        public Chat Find(Constraints constraints) // for tests
        {
            return FuzzySearch.Find(chats, constraints.AsDict());
        }

        public List<Chat> FindAll(Constraints constraints) // for tests
        {
            return FuzzySearch.FindAll(chats, constraints.AsDict());
        }

        public Chat Find(Constraint constraint) // for tests
        {
            return FuzzySearch.Find(chats, constraint.AsDict());
        }

        public List<Chat> FindAll(Constraint constraint) // for tests
        {
            return FuzzySearch.FindAll(chats, constraint.AsDict());
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