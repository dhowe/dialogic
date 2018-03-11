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
        public Say() : base()
        {
            this.DelayMs = (int)(Defaults.SAY_DURATION * 1000);
        }

        /**
         * Determine milliseconds to wait after sending the event, using:
         *      a. Line-length
         *      b. Meta-data modifiers
         *      c. Character mood (pending)
         */
        public override int ComputeDuration()
        {
            return (int)Math.Round
                (GetTextLenScale() * GetMetaSpeedScale() * DelayMs);
        }

        private double GetTextLenScale()
        {
            return Util.Map(Text.Length,
                Defaults.SAY_MIN_LEN, Defaults.SAY_MAX_LEN,
                Defaults.SAY_MIN_LEN_MULT, Defaults.SAY_MAX_LEN_MULT);
        }

        private double GetMetaSpeedScale()
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
                    default:
                        break;
                }
            }
            return val;
        }
    }

    public class Gram : Command
    {
        public Grammar grammar;

        public override void Init(string text, string label, string[] meta)
        {
            //Console.WriteLine("Gram.init: " + Util.Stringify(meta)+"\n"+String.Join("\n", meta));
            grammar = new Grammar(String.Join("\n", meta));
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

    public class Nvm : Wait, ISendable {

        public override int ComputeDuration()
        {
            return DelayMs;
        }
    }

    public class Ask : Command, ISendable
    {
        public int SelectedIdx { get; protected set; }

        protected List<Opt> options = new List<Opt>();

        public int Timeout;

        public Ask()
        {
            this.DelayMs = Infinite;
            this.Timeout = (int)(Defaults.ASK_TIMEOUT * 1000);
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
            var s = "";
            var opts = Options();
            for (int i = 0; i < opts.Count; i++)
            {
                s += opts[i].Text;
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
            var opts = OptionsJoined();
            Substitutions.Do(ref opts, globals);
			data[Dialogic.Meta.OPTS] = opts;
			data[Dialogic.Meta.TIMEOUT]= Timeout.ToString();
            return data;
        }

        protected override void HandleMetaTiming()
        {
            if (HasMeta(Dialogic.Meta.TIMEOUT)) Timeout = Util.SecStrToMs((string)meta[Dialogic.Meta.TIMEOUT]);
        }

        public override string ToString()
        {
            string s = "[" + TypeName().ToUpper() + "] " + QQ(Text) + " (";
            if (options != null) options.ForEach(o => s += o.Text + ",");
            return s.Substring(0, s.Length - 1) + ") " + MetaStr();
        }
    }

    public class Opt : Command
    {
        public Command action;

        //public Ask prompt;

        public Opt() : this("", NOP) { }

        public Opt(string text) : this(text, NOP) { }

        public Opt(string text, Command action) : base()
        {
            this.Text = text;
            this.action = action;
        }

        public override void Init(string text, string label, string[] meta)
        {
            this.Text = text;

            if (label.Length > 0 && !label.StartsWith("#", Util.IC))
            {
                throw BadArg("OPT requires a #Label");
            }

            this.action = label.Length > 0 ?
                Command.Create(typeof(Go), "", label, meta) : NOP;
        }

        public override string ToString()
        {
            return "[" + TypeName().ToUpper() + "] " + QQ(Text)
                + (action is NoOp ? "" : " (-> " + action.Text + ")");
        }

        public string ActionText()
        {
            return action != null ? action.Text : "";
        }
    }

    public class Find : Command
    {
        public override void Init(string text, string label, string[] metas)
        {
            ParseMeta(metas);
        }

        protected override void ParseMeta(string[] pairs)
        {
            //Console.WriteLine("Find.ParseMeta:" + Util.Stringify(pairs));
            for (int i = 0; pairs != null && i < pairs.Length; i++)
            {
                if (String.IsNullOrEmpty(pairs[i]))
                {
                    throw new ParseException("Invalid Find query");
                }
                Match match = RE.FindMeta.Match(pairs[i]);
                if (match.Groups.Count != 4)
                {
                    throw new ParseException("Invalid Find query: '" + pairs[i] + "'");
                }

                string key = match.Groups[1].Value;

                bool isStrict = false;
                if (key.IndexOf('!') == 0)
                {
                    isStrict = true;
                    key = key.Substring(1);
                }

                if (meta == null) meta = new Dictionary<string, object>();

                meta.Add(key, new Constraint(match.Groups[2].Value,
                    key, match.Groups[3].Value, isStrict));
            }
        }

        protected override string MetaStr()
        {
            string s = "";
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
            Text = text.Length > 0 ? text : label;
            ValidateTextLabel();
			SetMeta(Dialogic.Meta.LABEL, new Constraint(Dialogic.Meta.LABEL, Text, true));
        }

        public override string ToString()
        {
            return "[" + TypeName().ToUpper() + "] #" + Text;
        }
    }

    public class Chat : Command
    {
        public List<Command> commands;
        public int cursor = 0;

        public Chat()
        {
            commands = new List<Command>();
        }

        public static Chat Create(string name) // tests only
        {
            Chat c = new Chat();
            c.Init(name, "", new string[0]);
            return c;
        }

        public int Count()
        {
            return commands.Count();
        }

        public void AddCommand(Command c)
        {
            c.parent = this;
            c.IndexInChat = commands.Count; // ?
            this.commands.Add(c);
        }

        public override void Init(string text, string label, string[] metas)
        {
            if (string.IsNullOrEmpty(text)) throw BadArg("Missing label");

            this.Text = text;
            if (Regex.IsMatch(Text, @"\s+")) // TODO: compile
            {
                throw BadArg("CHAT name '" + Text + "' contains spaces!");
            }

            ParseMeta(metas);
			SetMeta(Dialogic.Meta.LABEL, Text);
        }

        protected override string MetaStr()
        {
            string s = "";
            if (HasMeta())
            {
                s += "{";
                foreach (var key in meta.Keys)
                {
                    if (key != Dialogic.Meta.LABEL)
                        s += key + "=" + meta[key] + ",";
                }
                s = s.Length > 1 ? s.Substring(0, s.Length - 1) + "}" : "";
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
        public const string LABEL = "label";
        public const string DELAY = "delay";
        public const string TIMEOUT = "timeout";

        public IDictionary<string, object> meta, data;

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

        public List<KeyValuePair<string, object>> ToList()
        {
            return meta != null ? meta.ToList() : null;
        }

        protected virtual string MetaStr()
        {
            string s = "";
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

                    if (parts.Length != 2) throw new Exception
                        ("Expected 2 parts, found " + parts.Length + ": " + parts);

                    SetMeta(parts[0].Trim(), parts[1].Trim());
                }
            }
        }
    }
    public abstract class Command : Meta
    {
        public const string PACKAGE = "Dialogic.";

        public const int Infinite = -1;

        protected static int IDGEN = 0;

        public static readonly Command NOP = new NoOp();

        public string Id { get; protected set; }

        public int DelayMs { get; protected set; }

        public string Text, Actor = ChatRuntime.DefaultSpeaker;

        public int LastSentMs, IndexInChat = -1; // needed?

        public Chat parent;

        protected Command()
        {
            this.Id = (++IDGEN).ToString();
            this.DelayMs = 0;
            this.data = new Dictionary<string, object>();
        }

        protected void ValidateTextLabel()
        {
            if (String.IsNullOrEmpty(Text)) throw BadArg
                (TypeName().ToUpper()+" requires a #Label");
            if (Text.StartsWith("#", Util.IC)) Text = Text.Substring(1);
        }

        private static string ToMixedCase(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            return (s[0] + "").ToUpper() + s.Substring(1).ToLower();
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
            Text = text.Length > 0 ? text : label;
            ParseMeta(metas);
            HandleMetaTiming();
        }

        protected virtual void HandleMetaTiming()
        {
            if (HasMeta(Dialogic.Meta.DELAY))
            {
				DelayMs = Util.SecStrToMs((string)meta[Dialogic.Meta.DELAY]);
            }
        }

        public virtual string TypeName()
        {
            return this.GetType().ToString().Replace(PACKAGE, "");
        }

        public virtual IDictionary<string, object> Realize(IDictionary<string, object> globals)
        {
            data.Clear();

            if (HasMeta())
            {
                IEnumerable sorted = null;
                foreach (KeyValuePair<string, object> kv in meta)
                {
                    string val = kv.Value.ToString();

                    if (val.IndexOf('$') > -1)
                    {
                        if (sorted == null) sorted = Util.SortByLength(globals.Keys);
                        foreach (string s in sorted)
                        {
                            val = val.Replace("$" + s, globals[s].ToString());
                        }
                    }

                    data[kv.Key] = val;
                }
            }

            RealizeFields(globals);

			data[Dialogic.Meta.TYPE] = TypeName();

            LastSentMs = Util.EpochMs();

            return data;
        }

        private void RealizeFields(IDictionary<string, object> globals)
        {
            var text = Text + ""; // tmp
            Substitutions.Do(ref text, globals);
			data[Dialogic.Meta.TEXT] = text;
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