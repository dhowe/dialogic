using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

namespace Dialogic
{
    public class ChatRuntime
    {
        public static string logFile = "../../../dialogic/dia.log";

        private List<Chat> chats;
        private Chat currentChat;
        private Ask currentPrompt;

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
            return choice != null ? HandleChoiceEvent(ref choice, globals) : HandleChatEvent(globals);
        }

        private IUpdateEvent HandleChoiceEvent(ref IChoice ic, IDictionary<string, object> globals)
        {
            var idx = ic.GetChoiceIndex();
            ic = null;

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
                    currentChat = currentPrompt.parent; // continue
                }

                return null;
            }
        }

        private void StartChat(Chat chat)
        {
            currentChat = chat;
            currentChat.Reset();
            nextEventTime = Util.Millis();
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
                        if (cmd is Ask)
                        {
                            currentPrompt = (Dialogic.Ask)cmd;
                            currentChat = null;
                        }

                        nextEventTime = Util.Millis() + cmd.ComputeDuration();
                        return new UpdateEvent(cmd);
                    }
                    else if (cmd is Find)
                    {
                        FindAsync((Find)cmd);
                    }
                }
            }

            return null;
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