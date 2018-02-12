using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;

namespace Dialogic
{
    public class ChatRuntime
    {
        public delegate void ChatEventHandler(ChatRuntime c, ChatEvent e);
        public event ChatEventHandler ChatEvents; // event-stream

        public Dictionary<string, object> globals;
        //public bool ShowWaits = false;
        public string LogFileName;

        protected List<Chat> chats;
        protected bool logInitd, waiting = false;

        public ChatRuntime(List<Chat> chats)
        {
            this.chats = chats;
            this.globals = new Dictionary<string, object>() {
                { "emotion", "special" },
                { "place", "Istanbul" },
                { "Happy", "HappyFlip" },
                { "verb", "play" },
                { "neg", "(nah|no|nope)" },
                { "var3", 2 }
            };
        }


        public Chat Find(Dictionary<string, string> conditions)
        {
            return ChatSearch.Find(chats, conditions);
        }


        public List<Chat> FindAll(Dictionary<string, string> conditions)
        {
            return ChatSearch.FindAll(chats, conditions);
        }

        public Chat FindChat(string name)
        {
            return ChatSearch.ByName(chats, name);
        }

        private bool Logging()
        { 
            return LogFileName != null;
        }

        public void Subscribe(AbstractClient cc) // tmp
        {
            cc.UnityEvents += new AbstractClient.UnityEventHandler(OnClientEvent);
        }

        private void OnClientEvent(EventArgs e)
        {
            if (e is IChosen)
            {
                waiting = false;
                Opt opt = ((IChosen)e).GetChosen();
                FireEvent(opt); // Send Opt event to clients - needed? 
                ((Opt)opt).action.Fire(this); // execute GO event
            }
        }

        internal void PrintGlobals()
        {
            Console.WriteLine("GLOBALS:");
            foreach (var k in globals.Keys)
            {
                System.Console.WriteLine(k + ": " + globals[k]);
            }
        }

        public void Run(Chat chat)
        {
            FireEvent(chat);
            //chat.commands.ForEach(Do);
            for (int i = 0; i < chat.commands.Count;)
            {
                if (waiting)
                {
                    //if (ShowWaits)Console.Write(".");
                    Thread.Sleep(10);
                    continue;
                }
                Do(chat.commands[i++]);
            }
        }

        public void Run()
        {
            if (chats == null || chats.Count < 1)
            {
                throw new Exception("No chats found!");
            }
            Run(chats[0]);
        }

        public void Do(Command cmd)
        {
            //Console.WriteLine("CMD: "+cmd.TypeName());
            /*if (cmd is Timed)
            {
                int waitMs = ((Timed)cmd).WaitTime();
                if (waitMs != 0) {
                    waiting = true;
                    if (waitMs > 0) {
                        //Console.WriteLine("TIME: " + waitMs);
                        Timers.SetTimeout(waitMs, () => {
                            //Console.WriteLine("TIMER("+waitMs+") FIRED");
                            //if (cmd is Ask) NotifyListeners(new ChatEvent(new Timeout((Ask)cmd)));
                            waiting = false;
                        });
                    }
                }
            }*/
            FireEvent(cmd);
        }

        private void FireEvent(Command c)
        {
            if (!(c is NoOp)) 
            {
                LogCommand(c); // log before replacements
                ChatEvent ce = c.Fire(this);
                if (!(c is Go)) // Opt, Chat ?
                {
                    if (ChatEvents != null) ChatEvents.Invoke(this, ce);
                }
            }
        }

        public Dictionary<string, object> Globals()
        {
            return this.globals;
        }

        public void LogCommand(Command c)
        {
            if (!Logging()) return;

            if (!logInitd) {
                
                logInitd = true;
                File.WriteAllText(LogFileName, "============\n");
            }

            using (StreamWriter w = File.AppendText(LogFileName))
            {
                var now = DateTime.Now.ToString("HH:mm:ss.fff", CultureInfo.InvariantCulture);
                w.WriteLine(now + "\t" + c);
            }
        }
    }
}