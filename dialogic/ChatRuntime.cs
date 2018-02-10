using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

namespace Dialogic
{
    public class ChatRuntime
    {
        public delegate void ChatEventHandler(ChatRuntime c, ChatEvent e);
        public event ChatEventHandler ChatEvents; // event-stream

        public Dictionary<string, object> globals;
        public bool ShowWaits = false;
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
                { "neg", "nah" },
                { "var3", 2 }
            };
        }

        private bool Logging()
        { 
            return LogFileName != null;
        }

        public void Subscribe(ChatClient cc) // tmp
        {
            cc.UnityEvents += new ChatClient.UnityEventHandler(OnClientEvent);
        }

        private void OnClientEvent(EventArgs e)
        {
            if (e is ResponseEvent)
            {
                waiting = false;
                Opt opt = (Opt)((ResponseEvent)e).Selected;
                FireEvent(opt); // Send Opt event to clients - needed ? 
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
                    if (ShowWaits)Console.Write(".");
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
        }

        private void NotifyListeners(ChatEvent ce)
        {
            LogCommand(ce.Command);
            if (ChatEvents != null) ChatEvents.Invoke(this, ce);
        }

        private void FireEvent(Command c)
        {
            if (!(c is NoOp)) 
            {
                ChatEvent ce = c.Fire(this);
                if (!(c is Go)) // Opt, Chat ?
                {
                    NotifyListeners(ce);
                }
            }
        }

        public Dictionary<string, object> Globals()
        {
            return this.globals;
        }

        public Chat FindChat(string chatName)
        {
            for (int i = 0; i < chats.Count; i++)
            {
                if (chats[i].Text == chatName)
                    return chats[i];
            }
            throw new ChatNotFound(chatName);
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

/* TODO: 

    # Rethink timing (Timeout obj, callbacks from client, threading, etc.)
    
    # Implement ChatRuntime.Find(Conditions)
    # Add COND tag to specify matches for chat
    # Add FIND || PICK || SELECT, tag to specify next chat search
    # Add META tag [] for display control (bold, wavy, etc.)
    # Handle reprompting on timeout in ChatRuntime instead of client

    CONS: Should parser add WAIT after each ASK? No, determine dynamically
    
    OTHER:
        EMPH/IF
        Interpret/Run code chunk ``
        rethink SET: needs to handle numbers, strings, +=, -=, % etc.
        Interrupt->Branch vs Interrupt->Resume
        ASK: Specify action for prompt-timeout
        Meta-tagging (Chat objects have Meta stack) 
        Chat-search by meta (linq)
        Verify chat-name uniqueness on parse ? variables?
        Variables: inputStack (set of inputs from user, opts or interrupts)
        Timing:  msThinking(ASK,SAY), msBetweenCommands(ASK,SAY)
*/
