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
        public delegate void ChatEventHandler(ChatEvent e);
        public event ChatEventHandler ChatEvents; // event-stream

        //public delegate void ChatEventCompletedHandler(object src, ChatEvent e);
        //public event ChatEventCompletedHandler ChatEventCompleted;  

        protected Dictionary<string, object> globals;
        public string LogFile;

        protected List<Chat> chats;
        protected Ask lastPrompt;
        protected bool logInitd, waitingOnPrompt = false;
        protected int nextEventTime;

        public ChatRuntime(List<Chat> chats) : this(chats, null) { }

        public ChatRuntime(List<Chat> chats, Dictionary<string, object> globals)
        {
            this.chats = chats;
            this.globals = globals;
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
            return LogFile != null;
        }

        public void Subscribe(AbstractClient cc) // tmp
        {
            cc.UnityEvents += new AbstractClient.UnityEventHandler(OnClientEvent);
        }

        private void OnClientEvent(EventArgs e)
        {
            if (!(e is IChoice)) throw new Exception("Invalid event type");

            var opt = lastPrompt.Selected(((IChoice)e).GetChoiceIndex());

            //FireEvent(opt); // Send Opt event to clients
            // NOTE: needed for the multi-client case

            opt.action.Fire(this); // execute GO event
        }
       
        public void Run(Chat chat)
        {
            FireEvent(chat);
            for (int i = 0; i < chat.commands.Count;)
            {
                if (Util.Elapsed() < nextEventTime)
                {
                    //Console.Write(".");
                    Thread.Sleep(1);
                    continue;
                }
                Command c = chat.commands[i];
                if (c is Ask)
                {
                    lastPrompt = (Ask)c;
                }
                FireEvent(c);
                nextEventTime = Util.Elapsed() + c.PauseAfterMs;
                i++;
            }
        }

        public void Run()
        {
            if (chats == null || chats.Count < 1)
            {
                throw new Exception("No chats found!");
            }
            Thread t = new Thread(new ThreadStart(() => Run(chats[0])));
            t.Start();                             
        }

        /*public void Do(Command cmd)
        {
            //Console.WriteLine("CMD: "+cmd.TypeName());
            if (cmd is Timed)
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
            }
            FireEvent(cmd);
        }*/

        private void FireEvent(Command c)
        {
            if (!(c is NoOp)) 
            {
                LogCommand(c); // log before replacements
                ChatEvent ce = c.Fire(this);
                if (!(c is Go)) 
                {
                    if (ChatEvents != null) ChatEvents.Invoke(ce);
                }
            }
        }

        public void Globals(Dictionary<string, object> globals)
        {
            this.globals = globals;
        }

        public Dictionary<string, object> Globals()
        {
            return globals;
        }

        public void LogCommand(Command c)
        {
            if (!Logging()) return;

            if (!logInitd) {
                
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