using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Dialogic
{
    public class ChatRuntime
    {
        public delegate void ChatEventHandler(ChatRuntime c, ChatEvent e);
        public event ChatEventHandler ChatEvents; // event-stream

        protected List<Chat> chats;
        public bool LogEvents = false;
        public string LogFileName = "dia.log";

        public Dictionary<string, object> globals;

        public ChatRuntime(List<Chat> chats)
        {
            this.chats = chats;
            this.globals = new Dictionary<string, object>() {
                { "emotion", "special" },
                { "place", "Istanbul" },
                { "Happy", "HappyFlip" },
                { "verb", "play" },
                { "var3", 2 }
            };
            if (LogEvents) InitLog();
        }

        public void Subscribe(ChatClient cc) // tmp
        {
            cc.UnityEvents += new ChatClient.UnityEventHandler(OnUnityEvent);
        }

        private void OnUnityEvent(EventArgs e)
        {
            var msg = (e is MockUnityEvent) ? ((MockUnityEvent)e).Message : e+"";
            Console.WriteLine("<" + msg + ">");
        }

        internal void PrintGlobals()
        {
            System.Console.WriteLine("GLOBALS:");
            foreach (var k in globals.Keys)
            {
                System.Console.WriteLine(k+": "+globals[k]);
            }
        }

        public void Run(Chat chat)
        {
            FireEvent(chat);
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

        public void Do(Command cmd)
        {
            FireEvent(cmd); // TODO: need to rethink this sleep
            if (cmd is Timed) Thread.Sleep(((Timed)cmd).WaitTime()); 
        }

        private void FireEvent(Command c)
        {
            if (!(c is NoOp))
            {
                ChatEvent ce = c.Fire(this);
                if (LogEvents) Util.Log(LogFileName, c);
                if (ChatEvents != null) ChatEvents.Invoke(this, ce);
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

        private void InitLog()
        {
            File.WriteAllText(LogFileName, "==========================\n");
        }

        public void LogCommand(Command c)
        {
            if (!LogEvents) return;

            using (StreamWriter w = File.AppendText(LogFileName))
            {
                w.WriteLine(DateTime.Now.ToLongTimeString() + "\t"
                    + Environment.TickCount + "\t" + c);
            }
        }
    }
}

/* TODO: 

    # Handle reprompting on timeout in ChatRuntime instead of client
    # Implement ChatRuntime.Find(Conditions)
    # Add COND tag to specify matches for chat
    # Add FIND || PICK || SELECT, tag to specify next chat search
    # Add META tag [] for display control (bold, wavy, etc.)

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
