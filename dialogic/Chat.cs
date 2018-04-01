using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dialogic
{
    public class Chat : Command
    {
        internal List<Command> commands;

        protected internal double staleness { get; protected set; }
        protected internal bool interruptable { get; protected set; }
        protected internal bool resumeAfterInt { get; protected set; }
        protected internal double stalenessIncr { get; protected set; }

        internal int cursor = 0, lastRunAt = -1;

        public Chat() : base()
        {
            commands = new List<Command>();

            realized = null; // not relevant for chats
            interruptable = true;
            resumeAfterInt = true;
            stalenessIncr = Defaults.CHAT_STALENESS_INCR;
            staleness = Defaults.CHAT_STALENESS;
        }

        internal static Chat Create(string name)
        {
            Chat c = new Chat();
            c.Init(name, String.Empty, new string[0]);
            return c;
        }

        internal double Staleness()
        {
            return staleness;
        }

        internal double StalenessIncr()
        {
            return stalenessIncr;
        }

        internal bool Interruptable()
        {
            return interruptable;
        }

        internal bool ResumeAfterInterrupting()
        {
            return resumeAfterInt;
        }

        internal Chat Staleness(double d)
        {
            staleness = d;
            SetMeta(Meta.STALENESS, d.ToString());
            return this;
        }

        internal Chat IncrementStaleness()
        {
            Staleness(staleness + stalenessIncr);
            return this;
        }

        internal Chat StalenessIncr(double incr)
        {
            this.stalenessIncr = incr;
            SetMeta(Meta.STALENESS_INCR, incr.ToString());
            return this;
        }

        internal Chat Interruptable(bool val)
        {
            this.interruptable = val;
            SetMeta(Meta.INTERRUPTIBLE, val.ToString());

            return this;
        }

        internal Chat ResumeAfterInterrupting(bool val)
        {
            this.resumeAfterInt = val;
            SetMeta(Meta.RESUME_AFTER_INT, val.ToString());
            return this;
        }

        public int Count()
        {
            return commands.Count;
        }

        public void AddCommand(Command c)
        {
            c.parent = this;
            //c.IndexInChat = commands.Count; // ?
            this.commands.Add(c);
        }

        public override void Realize(IDictionary<string, object> globals)
        {
            throw new DialogicException("Chats should not be Realized");
        }

        ///  All Chats must have a valid unique label, and a staleness value
        protected internal override Command Validate()
        {
            if (Regex.IsMatch(text, @"\s+")) // OPT: compile
            {
                throw BadArg("CHAT name '" + text + "' contains spaces");
            }

            SetMeta(Meta.STALENESS, Defaults.CHAT_STALENESS.ToString(), true);

            return this;
        }

        public override void Init(string text, string label, string[] metas)
        {
            this.text = text;
            ParseMeta(metas);
            //Validate();
            //Console.WriteLine("Chat #"+text +" "+realized.Stringify());
        }

        public string ToTree()
        {
            string s = TypeName().ToUpper() + " "
                + text + (" " + MetaStr()).TrimEnd();
            commands.ForEach(c => s += "\n  " + c);
            return s;
        }

        internal Command Next()
        {
            return HasNext() ? commands[cursor++] : null;
        }

        internal bool HasNext()
        {
            return cursor > -1 && cursor < commands.Count;
        }

        internal void Run(bool resetCursor = true)
        {
            if (resetCursor) this.cursor = 0;

            // Q: Do we reset this stuff on resume ?
            // Prob not in case of staleness

            lastRunAt = Util.EpochMs();

            // Q: what about (No-Label) WAIT events ?
            IncrementStaleness();
        }
    }
}
