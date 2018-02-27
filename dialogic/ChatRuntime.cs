using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

namespace Dialogic
{
    public class ChatRuntime
    {
        public static string DefaultSpeaker = "";

        protected List<Chat> chats;
        protected Chat current;
        protected Ask prompt;

        public string LogFile;
        protected bool logInitd;
        protected int nextEventTime;

        public ChatRuntime(List<Chat> chats)
        {
            this.chats = chats;
        }

        public IUpdateEvent Update(IDictionary<string, object> globals, ref IChoice choice)
        {
            IUpdateEvent dia = HandleChatEvent(globals);

            if (choice != null) // HAndleChoiceEvent
            {
                var idx = choice.GetChoiceIndex();
                if (idx < 0 || idx >= prompt.Options().Count)
                {
                    // reprompt here
                    Console.WriteLine("Invalid response: "+idx+" Reprompting");
                    return new UpdateEvent(prompt.Realize(globals));
                }
                Opt opt = prompt.Selected(idx);
                var action = opt.ActionText();
                Substitutions.Do(ref action, globals);
                current = FindChat(action);
                choice = null;
            }

            return dia;
        }

        private IUpdateEvent HandleChatEvent(IDictionary<string, object> globals)
        {
            IUpdateEvent dia = null;
            Command cmd = null;
            if (current != null && Util.Elapsed() < nextEventTime)
            {
                cmd = current.Next();
                if (cmd != null)
                {
                    cmd.Realize(globals);

                    if (cmd is IEmittable) {
                        cmd.Realize(globals);
                        if (cmd is Ask) prompt = (Dialogic.Ask)cmd;
                        nextEventTime = Util.Elapsed() + cmd.PauseAfterMs;
                        dia = new UpdateEvent(cmd.data);
                        current = null;
                    }
                    else {
                        cmd.Realize(globals);
                        if (cmd is Find)
                        {
                            current = Find(((Find)cmd).Meta());
                        }
                        else if (cmd is Go) 
                        {
                            current = FindChat(cmd.Text);
                        }
                        else if (cmd is Set){}
                        else if (cmd is Wait){}
                    }

                }
            }
            return dia;
        }

        private IUpdateEvent HandleChoiceEvent(IDictionary<string, object> globals, ref IChoice choice)
        {

        }

        public Chat Find(IDictionary<string, object> constraints)
        {
            return ChatSearch.Find(chats, constraints);
        }

        public List<Chat> FindAll(IDictionary<string, object> constraints)
        {
            return ChatSearch.FindAll(chats, constraints);
        }

        public Chat FindChat(string name)
        {
            return ChatSearch.ByName(chats, name);
        }

        private bool Logging()
        {
            return LogFile != null;
        }

        public void Run(string chatLabel = null)
        {
            if (Util.IsNullOrEmpty(chats)) throw new Exception("No chats!");
            Chat c = (chatLabel != null) ? FindChat(chatLabel) : chats[0];
        }

        public void LogCommand(Command c)
        {
            if (!Logging()) return;

            if (!logInitd)
            {
                logInitd = true;
                File.WriteAllText(LogFile, "============\n");
            }

            using (StreamWriter w = File.AppendText(LogFile))
            {
                var now = DateTime.Now.ToString("HH:mm:ss.fff", CultureInfo.InvariantCulture);
                w.WriteLine(now + "\t" + c);
            }
        }
    }

}