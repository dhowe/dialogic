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
        private Chat current;
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

            current = (chatLabel != null) ? Find(new Constraints
                (Meta.LABEL, chatLabel)) : chats[0];            
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
                current = prompt.parent;

                if (opt.action != Command.NOP)
                {
                    var action = opt.ActionText();
                    //Substitutions.Do(ref action, globals);
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
            current.Reset();
            current = chat;
        }

        private IUpdateEvent HandleChatEvent(IDictionary<string, object> globals)
        {
            Command cmd = null;
            UpdateEvent ue = null;

            if (current != null && Util.Millis() >= nextEventTime)
            {
                cmd = current.Next();

                if (cmd != null)
                {
                    cmd.Realize(globals);

                    if (cmd is ISendable)
                    {
                        if (cmd is Ask)
                        {
                            prompt = (Dialogic.Ask)cmd;
                            current = null;
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

        public void FindAsync(Find finder)
        {
            int ts = Util.Millis();
            (searchThread = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                var chat = ChatSearch.Find(chats, finder.meta);
                if (chat == null) throw new FindException(finder);
                Console.WriteLine("Found "+chat.Text+" in "+Util.Millis(ts)+"ms");
                StartChat(chat);

            })).Start();
        }

        public Chat Find(Find finder)
        {
            return ChatSearch.Find(chats, finder.meta);
        }

        public List<Chat> FindAll(Find finder)
        {
            return ChatSearch.FindAll(chats, finder.meta);
        }

        public Chat Find(Constraints constraints) // for tests
        {
            return ChatSearch.Find(chats, constraints.AsDict());
        }

        public List<Chat> FindAll(Constraints constraints) // for tests
        {
            return ChatSearch.FindAll(chats, constraints.AsDict());
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