using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Dialogic
{
    public class ChatExecutor
    {
        public delegate void ChatEventHandler(ChatExecutor ce, ChatEvent e);
        public event ChatEventHandler ChatEvents;

        protected List<Chat> chats;
        public bool logEvents = true;
        public string LogFileName = "dia.log";

        public ChatExecutor(List<Chat> chats)
        {
            this.chats = chats;
            if (logEvents) InitLog();
        }

        public void Subscribe(ConsoleClient cc)
        {
            cc.UnityEvents += new ConsoleClient.UnityEventHandler(OnUnityEvent);
        }

        private void OnUnityEvent(MockUnityEvent e) {
            
            Console.WriteLine($"<{e.Message}>");
        }

        public void Do(Command cmd)
        {
            RaiseEvent(cmd);

            if (cmd is Wait w)
            {
                Thread.Sleep(w.Millis()); // better solution?
            }
            else if (cmd is Go)
            {
                Run(FindByName(cmd.Text));
            }
        }

        private void RaiseEvent(Command c)
        {
            if (c is NoOp) return;
            if (logEvents) Util.Log(LogFileName, c);
            ChatEvents?.Invoke(this, new ChatEvent(c));
        }

        public void Run(Chat chat)
        {
            RaiseEvent(chat);
            chat.commands.ForEach(Do);
        }

        public void Run()
        {
            if (chats == null || chats.Count < 1)
            {
                throw new Exception("No chats found!");
            }
            Run(chats[0]);
        }

        public Chat FindByName(string chatName)
        {
            for (int i = 0; i < chats.Count; i++)
            {
                if (chats[i].Text == chatName)
                    return chats[i];
            }
            throw new KeyNotFoundException("No Chat: " + chatName);
        }

        private void InitLog()
        {
            File.WriteAllText(LogFileName, "==========================\n");
        }


        public void LogCommand(Command c)
        {
            if (!logEvents) return;

            using (StreamWriter w = File.AppendText(LogFileName))
            {
                w.WriteLine(DateTime.Now.ToLongTimeString() + "\t"
                    + Environment.TickCount + "\t" + c);
            }
        }
    }
}
