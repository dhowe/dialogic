using System;
using System.Collections.Generic;
using System.Threading;

namespace Dialogic
{
    public class ChatScheduler
    {
        List<IChatListener> listeners = new List<IChatListener>();
        private ChatManager cman;

        public ChatScheduler(ChatManager cman)
        {
            this.cman = cman;
        }

        public void Do(Command cmd)
        {
            if (cmd is Wait w)
            {
                Thread.Sleep(w.Millis());
            }
            else if (cmd is Go)
            {
                //System.Console.WriteLine($"FINDING {cmd.Text}");
                Run(cman.FindByName(cmd.Text));
            }
            NotifyListeners(cmd);
        }

        public void Run(Chat chat)
        {
            //Console.WriteLine($"Start: {chat.Text}");
            NotifyListeners(chat); // for the Chat itself
            chat.commands.ForEach((c) => Do(c));
        }

        public void Start()
        {
            List<Chat> chats = cman.Chats();
            if (chats == null || chats.Count < 1)
            {
                throw new Exception("No chats found!");
            }
            Run(chats[0]);
        }

        public void Run(string name) => Run(cman.FindByName(name));

        private void NotifyListeners(Command c)
        {
            listeners.ForEach((icl) =>
            {
                if (!(c is NoOp)) icl.onChatEvent(this, c);
            });
        }

        public void AddListener(IChatListener icl)
        {
            this.listeners.Add(icl);
        }
    }
}
