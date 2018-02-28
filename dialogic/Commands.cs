using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dialogic
{
    public class NoOp : Command {}

    public class Go : Command
    {
        public Go() : base() { }

        public Go(string text) : base()
        {
            this.Text = text;
        }
    }

    public class Say : Command, IEmittable
    {
        public Say() : base()
        {
            this.PauseAfterMs = 1000;
        }
    }

    public class Gram : Command
    {

        public Grammar grammar;

        public override void Init(string[] args, string[] meta)
        {
            Console.WriteLine("Gram.init: " + Util.Stringify(meta));
            grammar = new Grammar(String.Join("\n", meta));
        }

        public override string ToString()
        {
            return "[" + TypeName().ToUpper() + "] >\n" + grammar;
        }
    }

    public class Do : Command, IEmittable
    {
        public Do() : base() {
            PauseAfterMs = 100;
        }

        public override void Init(string[] args, string[] meta)
        {
            base.Init(args, meta);
            if (Text.IndexOf('#') == 0) Text = Text.Substring(1);
        }
    }

    public class Set : Command // TODO: rethink this
    {
        public string Value;

        public Set() : base() { }

        public Set(string name, string value) : base() // tests only
        {
            this.Text = name;
            this.Value = value;
        }

        public override void Init(string[] args, string[] meta)
        {
            string[] parts = ParseSetArgs(args);
            this.Text = parts[0];
            this.Value = parts[1];
        }

        private string[] ParseSetArgs(string[] args)
        {
            if (args.Length != 1)
            {
                throw BadArgs(args, 1);
            }

            var pair = Regex.Split(args[0], @"\s*=\s*");
            if (pair.Length != 2) pair = Regex.Split(args[0], @"\s+");

            if (pair.Length != 2) throw BadArgs(pair, 2);

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
        public Wait() : base()
        {
            PauseAfterMs = -1;
        }

        public override void Init(string[] args, string[] meta)
        {
            if (args.Length > 0) PauseAfterMs =
                Util.ToMillis(double.Parse(args[0]));
        }
    }

    public class Ask : Command, IEmittable
    {
        public int SelectedIdx { get; protected set; }

        protected List<Opt> options = new List<Opt>();

        public int Timeout;

        public Ask()
        {
            this.PauseAfterMs = Infinite;
            this.Timeout = 10000; // default
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
            o.parent = this;
            options.Add(o);
        }

        public override IDictionary<string, object> Realize(IDictionary<string, object> globals)
        {
            base.Realize(globals);
            var opts = OptionsJoined();
            Substitutions.Do(ref opts, globals);
            data["opts"] = opts;
            data["timeout"] = Timeout.ToString();
            return data;
        }

        protected override void HandleMetaTiming()
        {
            if (meta != null && meta.ContainsKey("timeout"))
            {
                double d = (double)Convert.ChangeType(meta["timeout"], typeof(double));
                Timeout = Util.ToMillis(d);
            }
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

        public Ask parent;

        public Opt() : this("", NOP) { }

        public Opt(string text) : this(text, NOP) { }

        public Opt(string text, Command action) : base()
        {
            this.Text = text;
            this.action = action;
        }

        public override void Init(string[] args, string[] meta)
        {
            if (args.Length < 1) throw BadArgs(args, 1);
            this.Text = args[0];
            this.action = (args.Length > 1) ? Command.Create(typeof(Go), new string[] { args[1] }, meta) : NOP;
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

    public class Constraint
    {
        public readonly string name, value;
        public readonly Operator op;

        public Constraint(string key, string val) :
            this("=", key, val)
        { }

        public Constraint(string opstr, string key, string val) :
            this(Operator.FromString(opstr), key, val)
        { }

        public Constraint(Operator op, string key, string val)
        {
            this.name = key;
            this.value = val;
            this.op = op;
        }

        public bool Check(string toCheck)
        {
            var passed = op.Invoke(toCheck, value);
            //Console.WriteLine(toCheck+" "+op+" "+ value + " -> "+passed);
            return passed;
        }

        public override string ToString()
        {
            return name + op + value;
        }
    }

    public class Find : Command
    {
        const string PATT = @"($?[a-zA-Z_][a-zA-Z0-9_]+) *([!<=>*^$]+) *([^ ]+)";
        protected static Regex QUERY = new Regex(PATT);

        public override void Init(string[] args, string[] meta)
        {
            if (args.Length > 0) throw BadArgs(args, 1);
            ParseMeta(meta);
        }

        protected override void ParseMeta(string[] pairs)
        {
            for (int i = 0; pairs != null && i < pairs.Length; i++)
            {
                Match match = QUERY.Match(pairs[i]);
                if (match.Groups.Count != 4)
                {
                    throw new Exception("Invalid query term: " + pairs[i]);
                }
                string key = match.Groups[1].Value;
                string op = match.Groups[2].Value;
                string val = match.Groups[3].Value;
                if (meta == null) meta = new Dictionary<string, object>();
                meta.Add(key, new Constraint(op, key, val));
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
    }

    public interface IEmittable { }

    public class Chat : Command
    {
        public List<Command> commands = new List<Command>(); // not copied
        public int cursor = 0;

        public Chat() : this("C" + Util.EpochMs()) { }

        public Chat(string name)
        {
            this.Text = name;
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

        public override void Init(string[] args, string[] meta)
        {
            if (args.Length < 1)
            {
                throw BadArgs(args, 1);
            }

            this.Text = args[0];
            if (Regex.IsMatch(Text, @"\s+"))
            {
                throw BadArg("CHAT name '" + Text + "' contains spaces!");
            }

            ParseMeta(meta);
        }

        public override string ToString()
        {
            return "[" + TypeName().ToUpper() + "] " + Text + " " + MetaStr();
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
    }

    public abstract class Command : MetaData
    {
        public const string PACKAGE = "Dialogic.";

        public const int Infinite = -1;

        protected static int IDGEN = 0;

        public static readonly Command NOP = new NoOp();

        public string Id { get; protected set; }

        public int PauseAfterMs { get; protected set; }

        public string Text, Actor = ChatRuntime.DefaultSpeaker;

        public int IndexInChat = -1; // needed?
        public Chat parent = null;

        protected Command()
        {
            this.Id = (++IDGEN).ToString();
            this.PauseAfterMs = 0;
            this.data = new Dictionary<string, object>();
        }

        private static string ToMixedCase(string s)
        {
            return (s[0] + "").ToUpper() + s.Substring(1).ToLower();
        }

        public static Command Create(string type, string[] args, string[] meta)
        {
            type = ToMixedCase(type);
            var cmd = Create(Type.GetType(PACKAGE + type), args, meta);
            if (cmd != null) return cmd;
            throw new TypeLoadException("No type: " + PACKAGE + type);
        }

        public static Command Create(Type type, string[] args, string[] meta)
        {
            Command cmd = (Command)Activator.CreateInstance(type);
            cmd.Init(args, meta);
            return cmd;
        }

        public virtual void Init(string[] args, string[] meta)
        {
            if (args != null)
            {
                this.Text = args[0];
                if (args.Length > 1) PauseAfterMs =
                    Util.ToMillis(double.Parse(args[1]));
            }
            ParseMeta(meta);
            HandleMetaTiming();
        }

        protected virtual void HandleMetaTiming()
        {
            if (meta != null && meta.ContainsKey("PauseAfterMs"))
            {
                double d = (double)Convert.ChangeType(meta["PauseAfterMs"], typeof(double));
                PauseAfterMs = Util.ToMillis(d);
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
                    if (val.IndexOf('$') > -1) {
                        if (sorted == null) sorted = Util.SortByLength(globals.Keys);
                        foreach (string s in sorted)
                        {
                            val = val.Replace("$" + s, globals[s].ToString());
                        }
                    }
                    data[kv.Key] = val;
                }
            }

            var text = Text + ""; // tmp
            Substitutions.Do(ref text, globals);

            data["text"] = text;
            data["type"] = TypeName();

            return data;
        }

        protected Exception BadArg(string msg)
        {
            throw new ArgumentException(msg);
        }

        protected Exception BadArgs(string[] args, int expected)
        {
            return BadArg(TypeName().ToUpper() + " expects " + expected + " args,"
                + " got " + args.Length + "'" + string.Join(" # ", args) + "'\n");
        }

        public override string ToString()
        {
            return "[" + TypeName().ToUpper() + "] " + Text + " " + MetaStr();
        }

        public string TimeStr()
        {
            return PauseAfterMs > 0 ? "wait=" + Util.ToSec(PauseAfterMs) : "";
        }

        protected override string MetaStr()
        {
            var s = base.MetaStr();
            if (PauseAfterMs > 0)
            {
                var t = TimeStr() + "}";
                s = (s.Length < 1) ? "{" + t : s.Replace("}", "," + t);
            }
            return s;
        }

        protected static string QQ(string text)
        {
            return "'" + text + "'";
        }
    }

    public class MetaData
    {
        public IDictionary<string, object> meta, data;

        public bool HasMeta()
        {
            return meta != null && meta.Count > 0;
        }

        public object GetMeta(string key, object defaultVal = null)
        {
            return meta != null && meta.ContainsKey(key) ? meta[key] : defaultVal;
        }
       
        /* Note: new keys will overwrite old keys with same name */
        public void SetMeta(string key, object val)
        {
            if (meta == null) meta = new Dictionary<string, object>();
            meta[key] = val;
        }

        public IDictionary<string, object> Meta()
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
}