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

        public string LogFile;
        protected bool logInitd;
        protected int nextEventTime;

        public ChatRuntime(List<Chat> chats)
        {
            this.chats = chats;
        }

        public void Run(string chatLabel = null)
        {
            if (Util.IsNullOrEmpty(chats)) throw new Exception("No chats!");
            current = (chatLabel != null) ? FindChat(chatLabel) : chats[0];
        }

        public IUpdateEvent Update(IDictionary<string, object> globals, ref IChoice choice)
        {
            IUpdateEvent dia = HandleChatEvent(globals);

            if (choice != null) // HandleChoiceEvent
            {
                var idx = choice.GetChoiceIndex();
                choice = null;
                Console.WriteLine("RT: GOT CHOICE: " + idx);

                if (idx < 0 || idx >= prompt.Options().Count)
                {
                    // reprompt here
                    Console.WriteLine("Invalid response: " + idx + " Reprompting");
                    return new UpdateEvent(prompt.Realize(globals));
                }
                else
                {
                    Opt opt = prompt.Selected(idx);
                    if (opt.action != Command.NOP)
                    {
                        var action = opt.ActionText();
                        Substitutions.Do(ref action, globals);
                        current = FindChat(action);
                    }
                    else
                    {
                        Console.WriteLine("GOT NO-OP: " + opt);
                        current = prompt.parent;
                    }
                }
            }

            return dia;
        }

        private IUpdateEvent HandleChatEvent(IDictionary<string, object> globals)
        {
            IUpdateEvent dia = null;
            Command cmd = null;
            if (current != null && Util.Elapsed() >= nextEventTime)
            {
                cmd = current.Next();
                if (cmd != null)
                {
                    //Console.WriteLine(Util.ElapsedSec()+": CR has -> " + cmd);
                    cmd.Realize(globals);
                    if (cmd is IEmittable)
                    {
                        cmd.Realize(globals);
                        if (cmd is Ask)
                        {
                            prompt = (Dialogic.Ask)cmd;
                            current = null;
                        }
                        dia = new UpdateEvent(cmd.data);
                    }
                    else
                    {
                        cmd.Realize(globals);
                        if (cmd is Find)
                        {
                            current = Find(((Find)cmd).Meta());
                        }
                        else if (cmd is Go)
                        {
                            current = FindChat(cmd.Text);
                        }
                        else if (cmd is Set) { }
                        else if (cmd is Wait) { }
                    }

                    nextEventTime = Util.Elapsed() + cmd.PauseAfterMs;
                }
            }
            return dia;
        }

        private IUpdateEvent HandleChoiceEvent(IDictionary<string, object> globals, ref IChoice choice)
        {
            return null;
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