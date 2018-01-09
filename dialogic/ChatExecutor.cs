using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Dialogic
{
    public class ChatExecutor //: IChatSource
    {
        //protected List<IChatListener> listeners = new List<IChatListener>();
        public delegate void ChatEventHandler(ChatExecutor ce, ChatEventArgs e);
        public event ChatEventHandler Events;

        protected List<Chat> chats;
        public bool logEvents = true;
        public string LogFileName = "dia.log";
        //private bool interrupted;

        public ChatExecutor(List<Chat> chats)
        {
            this.chats = chats;
            string header = "===============================================\n";
            File.WriteAllText(LogFileName, header);
        }

        public void Do(Command cmd)
        {
            RaiseEvent(cmd);

            if (cmd is Wait w)
            {
                Thread.Sleep(w.Millis());
            }
            else if (cmd is Ask a)
                
            {
                a.Fire();
            }
            else if (cmd is Go)
            {
                //this.interrupted = true;
                Run(FindByName(cmd.Text));
            }
        }

        private void RaiseEvent(Command c)
        {
            if (!(c is NoOp))
            {
                Log("[" + c.TypeName() + "] "+ c.Text);
                Events?.Invoke(this, new ChatEventArgs(c));
            }
        }

        public void Log(string logMessage)
        {
            if (!logEvents) return;

            using (StreamWriter w = File.AppendText(LogFileName))
            {
                w.WriteLine(DateTime.Now.ToLongTimeString() + "\t" 
                    + Environment.TickCount + "\t" + logMessage);
            }
        }

        public void Run(Chat chat)
        {
            RaiseEvent(chat);
            chat.commands.ForEach((c) => { 
                //if (!this.interrupted) 
                Do(c); 
            });
        }

        public void Run()
        {
            if (chats == null || chats.Count < 1)
            {
                throw new Exception("No chats found!");
            }
            Run(chats[0]);
        }

        //protected void Run(string name) => Run(FindByName(name));

        public Chat FindByName(string chatName)
        {
            for (int i = 0; i < chats.Count; i++)
            {
                if (chats[i].Text == chatName)
                    return chats[i];
            }
            throw new KeyNotFoundException("No Chat with name: " + chatName);
        }
    }
}
