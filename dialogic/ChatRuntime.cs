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
                    SetCurrentChat(FindChat(action));
                }
            }
            return null;
        }

        private void SetCurrentChat(Chat next)
        {
            current = next;
            current.Reset();
        }

        private IUpdateEvent HandleChatEvent(IDictionary<string, object> globals)
        {
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
                        return new UpdateEvent(cmd.data);
                    }
                    else
                    {
                        cmd.Realize(globals);
                        if (cmd is Find)
                        {
                            SetCurrentChat(Find(((Find)cmd).Meta()));
                        }
                        else if (cmd is Go)
                        {
                            // refactor to generic Find() ?
                            SetCurrentChat(FindChat(cmd.Text));
                        }
                    }

                    nextEventTime = Util.Elapsed() + cmd.PauseAfterMs;
                }
                else
                {
                    // Nothing left to do
                }
            }

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