using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Dialogic
{
    public class ChatRuntime
    {
        public static string DefaultSpeaker = "";

        protected List<Chat> chats;
        protected Chat current;
        protected Ask prompt;

        protected bool logInitd;
        protected int nextEventTime;

        protected static string srcpath = "../../../dialogic";
        public static string logFile = srcpath + "/dia.log";

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
                    Substitutions.Do(ref action, globals);
                    DoFind(opt.action);
                }
            }
            return null;
        }

        private void DoFind(Command finder)
        {
            var chat = Find((Find)finder);
            if (chat == null) throw new ChatException(finder, "Null Chat");
            current = chat;
            current.Reset();
        }

        private IUpdateEvent HandleChatEvent(IDictionary<string, object> globals)
        {
            Command cmd = null;
            UpdateEvent ue = null;

            if (current != null && Util.Elapsed() >= nextEventTime)
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

                    Console.WriteLine(cmd.TypeName()+".time: "+ cmd.ComputeDuration());
                    nextEventTime = Util.Elapsed() + cmd.ComputeDuration();
                    LogCommand(cmd);
                }

                // Nothing left to do
            }

            return ue;
        }


        public Chat Find(Find finder)
        {
            return ChatSearch.Find(chats, finder.meta);
        }

        public List<Chat> FindAll(Find finder)
        {
            return ChatSearch.FindAll(chats, finder.meta);
        }

        public Chat Find(Constraints constraints) // testing
        {
            return ChatSearch.Find(chats, constraints.AsDict());
        }

        public List<Chat> FindAll(Constraints constraints) // testing
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