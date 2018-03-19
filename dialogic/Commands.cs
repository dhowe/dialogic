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
            this.DelayMs = Util.ToMillis(Defaults.SAY_DURATION);
        }

        public override void Init(string text, string label, string[] metas)
        {
            base.Init(text, label, metas);
            if (Text.Length < 1) throw new ParseException("SAY requires text");
        }

        public override IDictionary<string, object> Realize(IDictionary<string, object> globals)
        {
            base.Realize(globals);
            Recombine(globals);
            lastSpoken = GetText(true);
            lastSpokenTime = Util.EpochMs();
            return realized;
        }

        private void Recombine(IDictionary<string, object> globals)
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
        public override int ComputeDuration() // Config?
        {
            return Util.Round
                (GetTextLenScale() * GetMetaSpeedScale() * DelayMs);
        }

        protected double GetTextLenScale()
        {
            return Util.Map(Text.Length,
                Defaults.SAY_MIN_LINE_LEN, Defaults.SAY_MAX_LINE_LEN,
                Defaults.SAY_MIN_LEN_MULT, Defaults.SAY_MAX_LEN_MULT);
        }

        protected double GetMetaSpeedScale() // Config
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
            DelayMs = Util.ToMillis(Defaults.DO_DURATION);
            base.Init(text, label, metas);
            ValidateTextLabel();
        }

        public override string ToString()
        {
            return TypeName().ToUpper() + " #" + Text;
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
            return TypeName().ToUpper() + " $" + Text + '=' + Value;
        }
    }

    public class Wait : Command, ISendable
    {
        public override void Init(string text, string label, string[] metas)
        {
            base.Init(text, label, metas);
            if (text.Length == 0)
            {
                DelayMs = Util.ToMillis(DefaultDuration());
            }
            else
            {
                DelayMs = Util.SecStrToMs(text, -1);
                if (DelayMs == -1) throw new ParseException(TypeName()
                    + " accepts only a NUMBER, found: '" + text + "'");
            }
        }

        protected virtual double DefaultDuration()
        {
            return Defaults.WAIT_DURATION;
        }
    }

    //public class Nvmx : Wait, ISendable
    //{
    //    protected override double DefaultDuration()
    //    {
    //        return Defaults.NVM_DURATION;
    //    }
    //}

    public class Ask : Say, ISendable
    {
        public int SelectedIdx { get; protected set; }

        protected List<Opt> options = new List<Opt>();

        public int Timeout;

        public Ask()
        {
            this.DelayMs = Util.ToMillis(Defaults.ASK_DURATION); // infinite
            this.Timeout = Util.ToMillis(Defaults.ASK_TIMEOUT);
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

        public override IDictionary<string, object> Realize(IDictionary<string, object> globals)
        {
            base.Realize(globals);
            Options().ForEach(o => o.Realize(globals));
            realized[Meta.TIMEOUT] = Timeout.ToString();
            realized[Meta.OPTS] = OptionsJoined();
            return realized;
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
            string s = base.ToString();
            if (options != null) options.ForEach(o => s += "\n  " + o);
            return s + MetaStr();
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
                throw BadArg("OPT requires a literal #Label");
            }

            this.action = label.Length > 0 ?
                Command.Create(typeof(Go), String.Empty, label, metas) : NOP;
        }

        public override string ToString()
        {
            return TypeName().ToUpper() + " " + Text
                + (action is NoOp ? String.Empty : " #" + action.Text);
        }
    }

    public class Find : Command
    {
        double stalenessThreshold; // TODO: init with default for type

        public Find() : base() { }

        internal Find(Constraints c) : base()
        {
            this.meta = c.AsDict();
        }

        public override void Init(string text, string label, string[] metas)
        {
            ParseMeta(metas);
        }

        public void Init(string metadata) // TODO: needs tests
        {
            ParseMeta(metadata.Trim().TrimEnds('{', '}').Split(','));
        }

        public override IDictionary<string, object> Realize(IDictionary<string, object> globals) 
        {
            return realized;
        }

        public override void SetMeta(string key, object val, bool throwIfKeyExists = false)
        {
            if (val is string || val.IsNumber())
            {
                val = new Constraint(key, val.ToString());
            }
            base.SetMeta(key, val, throwIfKeyExists);
        }

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

                if (Util.TrimFirst(ref key, Constraint.TypeSetChar))
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
            return (TypeName().ToUpper() + " " + MetaStr()).Trim();
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
            return TypeName().ToUpper() + " #" + Text;
        }
    }

    public class Chat : Command
    {
        public List<Command> commands;
        public bool interruptable = true;
        public bool resumeLastAfterInterrupting = true;
        public int cursor = 0, lastRunAt = -1;
        public double stalenessIncrement = 1;
        public double staleness = 0;

        public Chat()
        {
            commands = new List<Command>();
            realized = new Dictionary<string, object>();
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

        public override IDictionary<string, object> Realize(IDictionary<string, object> globals)
        {
            realized.Clear();
            RealizeMeta(globals); // OPT: remove if not used
            realized[Meta.STALENESS] = staleness.ToString();
            return realized;
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
        }

        protected override string MetaStr()
        {
            string s = String.Empty;
            if (HasMeta())
            {
                s += "{";
                foreach (var key in meta.Keys) s += key + "=" + meta[key] + ",";
                s = s.Length > 1 ? s.Substring(0, s.Length - 1) + "}" : String.Empty;
            }
            return s;
        }

        //public string ToTree() // remove?
        //{
        //    string s = ToString() + "\n";
        //    commands.ForEach(c => s += "  " + c + "\n");
        //    return s;
        //}

        public string ToTree()
        {
            string s = TypeName().ToUpper() + " " 
                + Text + (" " + MetaStr()).TrimEnd();
            commands.ForEach(c => s += "\n  " + c);
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

        internal void Run(bool resetCursor = true)
        {
            if (resetCursor) Reset();

            lastRunAt = Util.EpochMs();

            // Q: what about (No-Label) WAIT events ?
            staleness += stalenessIncrement;
        }

        public bool IsResumable() // TODO: set from meta
        {
            return resumeLastAfterInterrupting;
        }
    }

    public class Meta
    {
        public const string OPTS = "opts";
        public const string TYPE = "type";
        public const string TEXT = "text";
        public const string PLOT = "plot";
        public const string STAGE = "stage";
        public const string DELAY = "delay";
        public const string TIMEOUT = "timeout";
        public const string STALENESS = "staleness";
        public const string RESUMABLE = "resumable";

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

        public virtual void SetMeta(string key, object val, bool throwIfKeyExists = false)
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

        public int Id { get; protected set; }

        public int DelayMs { get; protected set; }

        public string Text, Actor = DefaultSpeaker;

        //public int IndexInChat = -1; ?

        public Chat parent;

        protected Command()
        {
            this.DelayMs = 0;
            this.Id = ++IDGEN;
            this.realized = new Dictionary<string, object>();
        }

        protected void ValidateTextLabel()
        {
            if (String.IsNullOrEmpty(Text)) throw BadArg
                (TypeName().ToUpper() + " requires a literal #Label");

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

        public static Command Create(Type type, string text, string label, string[] metas)
        {
            //Console.WriteLine("'"+type + "' '"+text+ "' '"+ label+"' "+Util.Stringify(metas));
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
            //return this.GetType().DeclaringType.Name; // an easier way ?
            return this.GetType().ToString().Replace(PACKAGE, String.Empty);
        }

        public virtual IDictionary<string, object> Realize(IDictionary<string, object> globals)
        {
            realized.Clear();

            RealizeMeta(globals);

            realized[Meta.TEXT] = Realizer.Do(Text, globals);
            realized[Meta.TYPE] = TypeName();

            return realized; // for convenience
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
            return (TypeName().ToUpper() + " " + Text).Trim() + (" " + MetaStr()).TrimEnd();
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