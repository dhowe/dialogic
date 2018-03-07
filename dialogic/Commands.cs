using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dialogic
{
    public class NoOp : Command { }

    public class Go : Command
    {
        public Go() : base() { }

        public override void Init(string text, string label, string[] meta)
        {
            // TODO: extract method
            if (label.Length < 1) throw BadArg
                ("GO requires a label, e.g. #Ch1, got '" + label + "'");

            base.Init(text, label, meta);

            if (Text.IndexOf('#') == 0) Text = Text.Substring(1);
        }
    }

    public class Say : Command, IEmittable
    {
        public Say() : base()
        {
            this.PauseAfterMs = 1000;
        }

        public override int ComputeDuration()
        {
            // 1. Line-length
            // 2. Meta-data
            // 3. Mood (pending)
            var lengthScale = Util.Map(Text.Length, 1, 100, .5, 2);
            var metaScale = GetMetaSpeed();
            return (int)(lengthScale * metaScale * PauseAfterMs);
        }

        private double GetMetaSpeed()
        {
            double val = 1.0;
            if (meta.ContainsKey("speed"))
            {
                if (((string)meta["speed"]) == "fast") val *= 2;
                if (((string)meta["speed"]) == "slow") val /= 2;
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

    public class Do : Command, IEmittable
    {
        public Do() : base()
        {
            PauseAfterMs = 50;
        }

        public override void Init(string text, string label, string[] meta)
        {
            if (label.Length < 1) throw BadArg("DO requires a literal label, e.g. #C2, got '" + label + "'");

            base.Init(text, label, meta);

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

        public override void Init(string text, string label, string[] meta)
        {
            string[] parts = ParseSetArgs(text);
            this.Text = parts[0];
            this.Value = parts[1];
        }

        private string[] ParseSetArgs(string args)
        {
            if (args.Length < 1) throw BadArg("ParseSetArgs");

            var pair = Regex.Split(args, @"\s*=\s*");
            if (pair.Length != 2) pair = Regex.Split(args, @"\s+");

            if (pair.Length != 2) throw BadArg("bad pair");

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

        public override void Init(string text, string label, string[] meta)
        {
            base.Init(text, label, meta);
            PauseAfterMs = Util.SecStrToMs(text, -1);
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
            this.Timeout = 5000; // default
        }

        //public override void Init(string text, string label, string[] meta)
        //{
        //    base.Init(text, label, meta);
        //    // HERE
        //}

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
            o.prompt = this;
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
                Timeout = Util.SecStrToMs((string)meta["timeout"]);
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

        public Ask prompt; // not used

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

            if (label.Length == 0 || !label.StartsWith("#", StringComparison.InvariantCulture))
            {
                throw BadArg("OPT requires a label, e.g. #Chat27, got '"+label+"'");
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
        public override void Init(string text, string label, string[] meta)
        {
            ParseMeta(meta);
        }

        protected override void ParseMeta(string[] pairs)
        {
            //Console.WriteLine("Find.ParseMeta:" + Util.Stringify(pairs));
            for (int i = 0; pairs != null && i < pairs.Length; i++)
            {
                Match match = RE.FindMeta.Match(pairs[i]);
                if (match.Groups.Count != 4)
                {
                    throw new Exception("Invalid query term: " + pairs[i]);
                }
                string key = match.Groups[1].Value;
                string op = match.Groups[2].Value;
                string val = match.Groups[3].Value;
                //Console.WriteLine(key + " :: " + op + " :: " + val);
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
        public List<Command> commands; // not copied
        public int cursor = 0;

        public Chat() : this("C" + Util.EpochMs()) { }

        public Chat(string name)
        {
            this.Text = name;
            this.commands = new List<Command>();
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

        public override void Init(string text, string label, string[] meta)
        {
            if (string.IsNullOrEmpty(text)) throw BadArg("Missing label");

            this.Text = text;
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

        public void Reset()
        {
            this.cursor = 0;
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
    public abstract class Command : MetaData
    {
        public const string PACKAGE = "Dialogic.";

        public const int Infinite = -1;

        protected static int IDGEN = 0;

        public static readonly Command NOP = new NoOp();

        public string Id { get; protected set; }

        public int PauseAfterMs { get; protected set; }

        public string Text, Actor = ChatRuntime.DefaultSpeaker;

        public int LastSentMs, IndexInChat = -1; // needed?

        public Chat parent = null;

        protected Command()
        {
            this.Id = (++IDGEN).ToString();
            this.PauseAfterMs = 0;
            this.data = new Dictionary<string, object>();
        }

        private static string ToMixedCase(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            return (s[0] + "").ToUpper() + s.Substring(1).ToLower();
        }

        public static Command Create(string type, string text, string label, string[] meta)
        {
            type = ToMixedCase(type);
            //Console.WriteLine(type + "' '"+ text + "' '"+ label + "' '"+ Util.Stringify(meta) + "'");
            var cmd = Create(Type.GetType(PACKAGE + type), label, text, meta);
            if (cmd != null) return cmd;
            throw new TypeLoadException("No type: " + PACKAGE + type);
        }

        public static Command Create(Type type, string text, string label, string[] meta)
        {
            Command cmd = (Command)Activator.CreateInstance(type);
            cmd.Init(text, label, meta);
            return cmd;
        }

        public virtual void Init(string text, string label, string[] meta)
        {
            //Console.WriteLine("Command.Init: " + text + " :: " + label + " :: " + String.Join("|", meta));
            Text = text.Length > 0 ? text : label;
            ParseMeta(meta);
            HandleMetaTiming();
        }

        protected virtual void HandleMetaTiming()
        {
            if (meta != null && meta.ContainsKey("PauseAfterMs"))
            {
                PauseAfterMs = Util.SecStrToMs((string)meta["PauseAfterMs"]);
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

            data["type"] = TypeName();

            LastSentMs = Util.EpochMs();

            return data;
        }

        private void RealizeFields(IDictionary<string, object> globals)
        {
            var text = Text + ""; // tmp
            Substitutions.Do(ref text, globals);
            data["text"] = text;
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
            return PauseAfterMs;
        }
    }
}