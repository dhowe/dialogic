using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dialogic
{
    public interface ISendable { }

    public class NoOp : Command { }

    public class Say : Command, ISendable
    {
        protected string lastSpoken;
        protected int lastSpokenTime;

        public Say() : base()
        {
            this.DelayMs = (int)(Defaults.SAY_DURATION * 1000);
        }

        public override void Realize(IDictionary<string, object> globals)
        {
            base.Realize(globals);
            CheckRecombination(globals);
            lastSpoken = GetText(true);
            lastSpokenTime = Util.EpochMs();
        }

        private void CheckRecombination(IDictionary<string, object> globals)
        {
            if (IsRecombinant()) // try to say something different than last time
            {
                int tries = 0;
                while (lastSpoken == GetText(true) && ++tries < 100)
                {
                    realized[Meta.TEXT] = Realizer.Do(Text, globals);
                }
            }
        }

        protected bool IsRecombinant()
        {
            return Text.IndexOf('|') > -1;
        }

        /**
         * Determine milliseconds to wait after sending the event, using:
         *      a. Line-length
         *      b. Meta-data modifiers
         *      c. Character mood (TODO)
         */
        public override int ComputeDuration()
        {
            return (int)Math.Round
                (GetTextLenScale() * GetMetaSpeedScale() * DelayMs);
        }

        protected double GetTextLenScale()
        {
            return Util.Map(Text.Length,
                Defaults.SAY_MIN_LEN, Defaults.SAY_MAX_LEN,
                Defaults.SAY_MIN_LEN_MULT, Defaults.SAY_MAX_LEN_MULT);
        }

        protected double GetMetaSpeedScale()
        {
            double val = 1.0;
            if (meta != null && meta.ContainsKey("speed"))
            {
                switch ((string)meta["speed"])
                {
                    case "fast":
                        val *= Defaults.SAY_FAST_MULT;
                        break;
                    case "slow":
                        val *= Defaults.SAY_SLOW_MULT;
                        break;
                }
            }
            return val;
        }
    }

    public class Gram : Command
    {
        public Grammar grammar;

        public override void Init(string text, string label, string[] metas)
        {
            //Console.WriteLine("Gram.init: " + Util.Stringify(meta)+"\n"+String.Join("\n", meta));
            grammar = new Grammar(String.Join("\n", metas));
        }

        public override string ToString()
        {
            return "[" + TypeName().ToUpper() + "] >\n" + grammar;
        }
    }

    public class Do : Command, ISendable
    {
        public override void Init(string text, string label, string[] metas)
        {
            DelayMs = 20;
            base.Init(text, label, metas);
            ValidateTextLabel();
        }

        public override string ToString()
        {
            return "[" + TypeName().ToUpper() + "] #" + Text;
        }
    }

    public class Set : Command // TODO: rethink
    {
        public string Value;

        public Set() : base() { }

        internal Set(string name, string value) : base() // tests only
        {
            this.Text = name;
            this.Value = value;
        }

        public override void Init(string text, string label, string[] metas)
        {
            string[] parts = ParseSetArgs(text);
            this.Text = parts[0];
            this.Value = parts[1];
        }

        private string[] ParseSetArgs(string s)
        {
            if (s.Length < 1) throw BadArg("ParseSetArgs");

            var pair = Regex.Split(s, @"\s*=\s*"); // TODO: compile
            if (pair.Length != 2) pair = Regex.Split(s, @"\s+");

            if (pair.Length != 2) throw BadArg("SET requires NAME and VALUE");

            if (pair[0].StartsWith("$", StringComparison.Ordinal))
            {
                pair[0] = pair[0].Substring(1); // tmp: leading $ is optional
            }

            return pair;
        }

        public override string ToString()
        {
            return "[" + TypeName().ToUpper() + "] $" + Text + '=' + Value;
        }
    }

    public class Wait : Command
    {
        public override void Init(string text, string label, string[] metas)
        {
            DelayMs = -1;
            base.Init(text, label, metas);
            DelayMs = Util.SecStrToMs(text, -1);
        }
    }

    public class Nvm : Wait, ISendable
    {

        public override int ComputeDuration()
        {
            return DelayMs;
        }
    }

    public class Ask : Say
    {
        public int SelectedIdx { get; protected set; }

        protected List<Opt> options = new List<Opt>();

        public int Timeout;

        public Ask()
        {
            this.DelayMs = -1; // infinite
            this.Timeout = (int)(Defaults.ASK_TIMEOUT * 1000);
        }

        public override int ComputeDuration()
        {
            return DelayMs;
        }

        public List<Opt> Options()
        {
            if (options.Count < 1)
            {
                options.Add(new Opt("Yes"));
                options.Add(new Opt("No"));
            }
            return options;
        }

        public string OptionsJoined(string delim = "\n")
        {
            var s = String.Empty;
            var opts = Options();
            for (int i = 0; i < opts.Count; i++)
            {
                s += opts[i].GetText(true);
                if (i < opts.Count - 1) s += delim;
            }
            return s;
        }

        public Opt Selected()
        {
            return Options()[SelectedIdx];
        }

        public Opt Selected(int i)
        {
            this.SelectedIdx = i;
            if (i >= 0 && i < options.Count) return Selected();
            return null;
        }

        public void AddOption(Opt o)
        {
            //o.prompt = this;
            options.Add(o);
        }

        public override void Realize(IDictionary<string, object> globals)
        {
            base.Realize(globals);
            Options().ForEach(o => o.Realize(globals));
            realized[Meta.TIMEOUT] = Timeout.ToString();
            realized[Meta.OPTS] = OptionsJoined();
        }

        protected override void HandleMetaTiming()
        {
            if (HasMeta(Meta.TIMEOUT))
            {
                Timeout = Util.SecStrToMs((string)meta[Meta.TIMEOUT]);
            }
        }

        public override string ToString()
        {
            string s = "[" + TypeName().ToUpper() + "] " + QQ(Text) + " (";
            if (options != null) options.ForEach(o => s += o.Text + ",");
            return s.Substring(0, s.Length - 1) + ") " + MetaStr();
        }
    }

    public class Opt : Say
    {
        public Command action;

        public Opt() : this(String.Empty, NOP) { }

        public Opt(string text) : this(text, NOP) { }

        public Opt(string text, Command action) : base()
        {
            this.Text = text;
            this.action = action;
        }

        public override void Init(string text, string label, string[] metas)
        {
            this.Text = text;

            if (label.Length > 0 && !label.StartsWith("#", Util.IC))
            {
                throw BadArg("OPT requires a #Label");
            }

            this.action = label.Length > 0 ?
                Command.Create(typeof(Go), String.Empty, label, metas) : NOP;
        }

        public override string ToString()
        {
            return "[" + TypeName().ToUpper() + "] " + QQ(Text)
                + (action is NoOp ? String.Empty : " (-> " + action.Text + ")");
        }
    }

    public class Find : Command
    {
        public override void Init(string text, string label, string[] metas)
        {
            ParseMeta(metas);
        }

        public override void Realize(IDictionary<string, object> globals) {/*noop*/}

        protected override void ParseMeta(string[] pairs)
        {
            for (int i = 0; pairs != null && i < pairs.Length; i++)
            {
                if (String.IsNullOrEmpty(pairs[i]))
                {
                    throw new ParseException("Invalid query");
                }

                Match match = RE.FindMeta.Match(pairs[i]);
                if (match.Groups.Count != 4)
                {
                    throw new ParseException("Invalid query: '" + pairs[i] + "'");
                }

                string key = match.Groups[1].Value;
                ConstraintType ctype = ConstraintType.Soft;

                if (Util.TrimFirst(ref key, Constraint.TypeChar))
                {
                    ctype = ConstraintType.Hard;
                    if (Util.TrimFirst(ref key, '!'))
                    {
                        ctype = ConstraintType.Absolute;
                    }
                }

                if (meta == null) meta = new Dictionary<string, object>();

                meta.Add(key, new Constraint(match.Groups[2].Value,
                    key, match.Groups[3].Value, ctype));
            }
        }

        protected override string MetaStr()
        {
            string s = String.Empty;
            if (HasMeta())
            {
                s += "{";
                foreach (var ct in meta.Values) s += ct + ",";
                s = s.Substring(0, s.Length - 1) + "}";
            }
            return s;
        }

        public override string ToString()
        {
            return "[" + TypeName().ToUpper() + "] " + MetaStr();
        }
    }

    public class Go : Find
    {
        public override void Init(string text, string label, string[] metas)
        {
            base.Text = text.Length > 0 ? text : label;
            ValidateTextLabel();
        }

        public override string ToString()
        {
            return "[" + TypeName().ToUpper() + "] #" + Text;
        }
    }

    public class Chat : Command
    {
        public List<Command> commands;
        public int cursor = 0, lastRunAt = -1;

        public Chat()
        {
            commands = new List<Command>();
        }

        public static Chat Create(string name) // tests only
        {
            Chat c = new Chat();
            c.Init(name, String.Empty, new string[0]);
            return c;
        }

        public int Count()
        {
            return commands.Count();
        }

        public void AddCommand(Command c)
        {
            c.parent = this;
            //c.IndexInChat = commands.Count; // ?
            this.commands.Add(c);
        }

        public override void Init(string text, string label, string[] metas)
        {
            if (string.IsNullOrEmpty(text)) throw BadArg("Missing label");

            this.Text = text;
            if (Regex.IsMatch(base.Text, @"\s+")) // TODO: compile
            {
                throw BadArg("CHAT name '" + base.Text + "' contains spaces!");
            }

            ParseMeta(metas);
            //            SetMeta(Meta.LABEL, Text); // !realized
        }

        protected override string MetaStr()
        {
            string s = String.Empty;
            if (HasMeta())
            {
                s += "{";
                foreach (var key in meta.Keys)
                {
                    //if (key != Meta.LABEL) 
                    s += key + "=" + meta[key] + ",";
                }
                s = s.Length > 1 ? s.Substring(0, s.Length - 1) + "}" : String.Empty;
            }
            return s;
        }

        public string ToTree()
        {
            string s = ToString() + "\n";
            commands.ForEach(c => s += "  " + c + "\n");
            return s;
        }

        internal Command Next()
        {
            bool hasNext = cursor > -1 && cursor < commands.Count;
            return hasNext ? commands[cursor++] : null;
        }

        public void Reset()
        {
            this.cursor = 0;
        }
    }

    public class Meta
    {
        public const string OPTS = "opts";
        public const string TYPE = "type";
        public const string TEXT = "text";
        public const string DELAY = "delay";
        public const string TIMEOUT = "timeout";
        //public const string LABEL = "label";

        public IDictionary<string, object> meta, realized;

        public bool HasMeta()
        {
            return meta != null && meta.Count > 0;
        }

        public bool HasMeta(string key)
        {
            return meta != null && meta.ContainsKey(key);
        }

        public object GetMeta(string key, object defaultVal = null)
        {
            return meta != null && meta.ContainsKey(key) ? meta[key] : defaultVal;
        }

        public object GetRealized(string key, object defaultVal = null)
        {
            return realized.ContainsKey(key) ? realized[key] : defaultVal;
        }

        public void SetMeta(Constraint constraint)
        {
            this.SetMeta(constraint.name, constraint, true);
        }

        public void SetMeta(string key, object val, bool throwIfKeyExists = false)
        {
            if (meta == null) meta = new Dictionary<string, object>();

            if (throwIfKeyExists)
            {
                meta.Add(key, val);
            }
            else
            {
                meta[key] = val;
            }
        }

        public IDictionary<string, object> GetMeta()
        {
            return meta;
        }

        public IDictionary<string, object> GetRealized()
        {
            return realized;
        }

        public List<KeyValuePair<string, object>> ToList()
        {
            return meta != null ? meta.ToList() : null;
        }

        protected virtual string MetaStr()
        {
            string s = String.Empty;
            if (HasMeta())
            {
                s += "{";
                foreach (var key in meta.Keys)
                {
                    s += key + "=" + meta[key] + ",";
                }
                s = s.Substring(0, s.Length - 1) + "}";
            }
            return s;
        }

        protected virtual void ParseMeta(string[] pairs)
        {
            for (int i = 0; pairs != null && i < pairs.Length; i++)
            {
                //Console.WriteLine(i+") "+args[i]);
                if (!string.IsNullOrEmpty(pairs[i]))
                {
                    string[] parts = pairs[i].Split('=');

                    if (parts.Length != 2)
                    {
                        throw new Exception("Expected 2 parts for meta key/val," +
                            " but found " + parts.Length + ": " + Util.Stringify(parts));
                    }

                    SetMeta(parts[0].Trim(), parts[1].Trim());
                }
            }

        }
    }

    public abstract class Command : Meta
    {
        public const string PACKAGE = "Dialogic.";

        protected static int IDGEN = 0;

        public static readonly Command NOP = new NoOp();

        public static string DefaultSpeaker = String.Empty; // ?

        public string Id { get; protected set; }

        public int DelayMs { get; protected set; }

        public string Text, Actor = DefaultSpeaker;

        //public int LastSentMs, IndexInChat = -1; // needed?

        public Chat parent;

        protected Command()
        {
            this.Id = (++IDGEN).ToString();
            this.DelayMs = 0;
            this.realized = new Dictionary<string, object>();
        }

        protected void ValidateTextLabel()
        {
            if (String.IsNullOrEmpty(Text)) throw BadArg
                (TypeName().ToUpper() + " requires a #Label");

            if (Text.StartsWith("#", Util.IC)) Text = Text.Substring(1);
        }

        public string GetText(bool real = false)
        {
            return real ? (string)realized[Meta.TEXT] : Text;
        }

        public string GetActor()
        {
            return Actor;
        }

        private static string ToMixedCase(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;

            return (s[0].ToString()).ToUpper() + s.Substring(1).ToLower();
        }

        public static Command Create(string type, string text, string label, string[] metas)
        {
            //Console.WriteLine(type + "' '"+text+ "' '"+ label+"' "+Util.Stringify(meta));
            return Create(Type.GetType(PACKAGE + ToMixedCase(type)), label, text, metas);
        }

        public static Command Create(Type type, string text, string label, string[] metas)
        {
            Command cmd = (Command)Activator.CreateInstance(type);
            cmd.Init(text, label, metas);
            return cmd;
        }

        public virtual void Init(string text, string label, string[] metas)
        {
            this.Text = text.Length > 0 ? text : label;
            ParseMeta(metas);
            HandleMetaTiming();
        }

        protected virtual void HandleMetaTiming()
        {
            if (HasMeta(Meta.DELAY))
            {
                DelayMs = Util.SecStrToMs((string)meta[Meta.DELAY]);
            }
        }

        public virtual string TypeName()
        {
            return this.GetType().ToString().Replace(PACKAGE, String.Empty);
        }

        public virtual void Realize(IDictionary<string, object> globals)
        {
            realized.Clear();

            RealizeMeta(globals);

            realized[Meta.TEXT] = Realizer.Do(Text, globals);
            realized[Meta.TYPE] = TypeName();
        }

        protected virtual void RealizeMeta(IDictionary<string, object> globals)
        {
            if (HasMeta())
            {
                IEnumerable sorted = null; // TODO: cache these key-sorts ?

                foreach (KeyValuePair<string, object> pair in meta)
                {
                    string val = pair.Value.ToString();

                    if (val.IndexOf('$') > -1)
                    {
                        if (sorted == null) sorted = Util.SortByLength(globals.Keys);
                        foreach (string s in sorted)
                        {
                            val = val.Replace("$" + s, globals[s].ToString());
                        }
                    }

                    realized[pair.Key] = val;
                }
            }
        }

        protected Exception BadArg(string msg)
        {
            throw new ParseException(msg);
        }

        public override string ToString()
        {
            return "[" + TypeName().ToUpper() + "] " + Text + " " + MetaStr();
        }

        protected static string QQ(string text)
        {
            return "'" + text + "'";
        }

        public virtual int ComputeDuration()
        {
            return DelayMs;
        }
    }
}