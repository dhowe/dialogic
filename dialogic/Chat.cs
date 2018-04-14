using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dialogic
{
    /// <summary>
    /// Each section of text in a Dialogic script is known as a Chat. Each Chat has a unique label and contains one or more commands. When a Chat is run, each command is executed in order, until all have been run, or the system jumps to a new Chat. The Chat command accepts a required label, followed, optionally, by metadata, which can be used with the Find command to search for Chats matching desired criteria.
    /// </summary>
    public class Chat : Command
    {
        internal List<Command> commands;

        protected internal double staleness { get; protected set; }
        protected internal bool interruptable { get; protected set; }
        protected internal bool resumeAfterInt { get; protected set; }
        protected internal double stalenessIncr { get; protected set; }
        //protected internal string chatMode { get; protected set; }
        //private enum Mode { DEFAULT, GRAMMAR };

        internal int cursor = 0, lastRunAt = -1;
        internal bool allowSmoothingOnResume = true;

        internal IDictionary<string, object> scope;
        protected internal ChatRuntime runtime;

        public object this[string key] // use locals as indexer?
        {
            get
            {
                return this.scope[key];
            }
        }

        public Chat() : base()
        {
            commands = new List<Command>();

            realized = null; // not relevant for chats (use for locals?)
            interruptable = true;
            resumeAfterInt = true;
            stalenessIncr = Defaults.CHAT_STALENESS_INCR;
            staleness = Defaults.CHAT_STALENESS;
            scope = new Dictionary<string, object>();
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

        protected internal override Command Realize(IDictionary<string, object> globals)
        {
            //Console.WriteLine("[WARN] Chats need not be realized, doing commands instead");
            commands.ForEach(c => c.Realize(globals));
            return this;
        }

        ///  All Chats must have a valid unique label, and a staleness value
        protected internal override Command Validate()
        {
            if (text.IndexOf(' ') > -1) throw BadArg
                ("CHAT name '" + text + "' contains spaces");

            SetMeta(Meta.STALENESS, Defaults.CHAT_STALENESS.ToString(), true);

            return this;
        }

        protected internal override void Init(string txt, string lbl, string[] metas)
        {
            this.text = txt;
            ParseMeta(metas);
        }

        public string ToTree()
        {
            string s = TypeName().ToUpper() + " "
                + text + (" " + MetaStr()).TrimEnd();
            commands.ForEach(c => s += "\n  " + c);
            return s;
        }

        protected override string MetaStr()
        {
            string s = String.Empty;
            if (HasMeta())
            {
                s += '{';
                foreach (var key in meta.Keys)
                {
                    // no need to show the default staleness
                    if (key == Meta.STALENESS && 
                        Util.FloatingEquals(Staleness(), Defaults.CHAT_STALENESS))
                    {
                        continue;
                    }

                    s += key + '=' + meta[key] + ',';
                }
                s = s.TrimLast(',') + "}";
            }
            return s.ReplaceFirst("{}", string.Empty);
        }

        internal Chat LastRunAt(int ms)
        {
            this.lastRunAt = ms;
            return this;
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
            if (resetCursor)
            {
                this.cursor = 0;
                IncrementStaleness();
            }
            else
            {
                // reset half the added staleness on resume
                staleness -= (StalenessIncr() / 2.0);
            }

            LastRunAt(Util.EpochMs());
        }

        internal static Type DefaultCommandType(Chat chat)
        {
            if (chat != null)
            {
                if (chat.HasMeta(Meta.DEFAULT_CMD))
                {
                    var type = (string)chat.GetMeta(Meta.DEFAULT_CMD);
                    if (!ChatRuntime.TypeMap.ContainsKey(type))
                    {
                        throw new ParseException("Invalid defaultCmd value" +
                            " in Chat#" + chat.text);
                    }

                    return ChatRuntime.TypeMap[type];
                }
                else if (chat.HasMeta(Meta.CHAT_MODE))
                {
                    var mode = (string)chat.GetMeta(Meta.CHAT_MODE);
                    if (mode != "grammar")
                    {
                        throw new ParseException("Invalid 'mode' value"
                            + " in Chat#" + chat.text);
                    }
                    return typeof(Set);
                }
            }

            return typeof(Say);
        }

        // testing only below --------------------------------------------------

        internal string Expand(IDictionary<string, object> globals, string start)
        {
            Say s = new Say();
            s.Init(start, "", new string[0]);
            s.Actor(Dialogic.Actor.Default);
            s.parent = this;
            s.Realize(globals);
            return s.Text();
        }

        internal string ExpandNoGroups(IDictionary<string, object> globals, 
            string start)//, bool doGroups = false)
        {
            start = start.TrimFirst(Defaults.SYMBOL);
            var re = new Regex(@"\$([^ \(\)]+)");
            string sofar = (string)scope[start];

            var recursions = 0;
            while (++recursions < 10)
            {
                foreach (Match match in re.Matches(sofar))
                {
                    var v = match.Groups[1].Value;
                    if (!scope.ContainsKey(v)) throw new DialogicException
                        ("No match for " + v + " in: " + scope.Stringify());
                    sofar = sofar.Replace(Defaults.SYMBOL + v, (string)scope[v]);
                }

                if (sofar.IndexOf(Defaults.SYMBOL) < 0) break;
            }

            if (recursions >= 10) Console.WriteLine("[WARN] Max recursion level"
                + " reached: " + start + " -> " + sofar);

            //if (doGroups) sofar = Realizer.DoGroups(sofar);

            return sofar;
        }

        protected internal string AsGrammar(IDictionary<string, object> globals,
            bool localize = true)
        {
            var name = text + ".";
            var re = new Regex(@"\$([^ \(\)]+)");
            var g = "Grammar#" + text + "\n";

            foreach (var k in globals.Keys)
            {
                if (k.StartsWith(name, Util.IC))
                {
                    string key = k;
                    string val = (string)globals[k];

                    if (localize)
                    {
                        key = key.Replace(name, "");
                        val = val.Replace(name, "");
                    }

                    foreach (Match match in re.Matches(val))
                    {
                        var sub = match.Groups[1].Value;
                        val = val.Replace("$" + sub, "<" + sub + ">");
                    }

                    g += "  " + key + ": " + val + "\n";
                }
            }
            return g;
        }

        // unused
        protected internal string GrammarToJson(IDictionary<string, object>
            globals, bool localize = true)
        {
            var name = text + ".";
            var re = new Regex(@"\$([^ \(\)]+)");
            var g = "{\n";

            foreach (var k in globals.Keys)
            {
                if (k.StartsWith(name, Util.IC))
                {
                    string key = k;
                    string val = (string)globals[k];

                    if (localize)
                    {
                        key = k.Replace(name, "");
                        val = val.Replace(name, "");
                    }

                    foreach (Match match in re.Matches(val))
                    {
                        var sub = match.Groups[1].Value;
                        val = val.Replace("$" + sub, "<" + sub + ">");
                    }

                    g += "  \"" + key + "\": \"" + val + "\",\n";
                }
            }
            return g.Substring(0, g.Length - 2) + "\n}";
        }
    }
}
