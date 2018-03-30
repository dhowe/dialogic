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

            realized = null; // not used for Chat
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

        protected internal override Command Validate()
        {
            if (Regex.IsMatch(text, @"\s+")) // TODO: compile
            {
                throw BadArg("CHAT name '" + text + "' contains spaces!");
            }

            if (meta == null) meta = new Dictionary<string, object>();

            if (!HasMeta(Meta.STALENESS))
            {
                meta[Meta.STALENESS] = Defaults.CHAT_STALENESS.ToString();
            }

            return this;
        }

        public override void Init(string text, string label, string[] metas)
        {
            this.text = text;
            ParseMeta(metas);
            Validate();
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
