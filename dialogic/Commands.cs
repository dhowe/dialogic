using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dialogic
{
    public class NoOp : Command
    {
        public override ChatEvent Fire(ChatRuntime cr)
        {
            return null;
        }
    }

    public class Go : Command
    {
        public Go() : base() { }

        public Go(string text) : base()
        {
            this.Text = text;
        }

        public override ChatEvent Fire(ChatRuntime cr)
        {
            ChatEvent ce = base.Fire(cr);
            cr.Run(cr.FindChat(ce.Command.GetText()));
            return ce;
        }
    }

    public class Say : Command, IEmittable
    {
        public Say() : base()
        {
            this.PauseAfterMs = 1000;
        }
    }

    public class Do : Command, IEmittable
    {
        public Do() : base()
        {
            this.PauseAfterMs = 1000;
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

        public override Command Copy()
        {
            return (Set)this.MemberwiseClone();
        }

        public override string ToString()
        {
            return "[" + TypeName().ToUpper() + "] $" + Text + '=' + Value;
        }

        public override ChatEvent Fire(ChatRuntime cr)
        {
            Set clone = (Set)this.Copy();
            var globals = cr.Globals();
            if (globals != null)
            {
                Substitutions.Do(ref clone.Value, globals);
                globals[Text] = Value; // set the global var
            }
            return new ChatEvent(clone);
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

        public Ask()
        {
            this.PauseAfterMs = Infinite;
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

        public string OptionsJoined(string delim = ",")
        {
            var opts = Options();
            var s = "";
            opts.ForEach((o) => s += o.Text + " ");
            return s.Trim().Replace(" ", delim);
        }

        public Opt Selected()
        {
            return Options()[SelectedIdx];
        }

        public Opt Selected(int i)
        {
            this.SelectedIdx = i;
            if (i >= 0 && i < options.Count)
            {
                return Selected();
            }
            throw new InvalidChoice(this);
        }

        public void AddOption(Opt o)
        {
            o.parent = this;
            options.Add(o);
        }

        public override ChatEvent Fire(ChatRuntime cr)
        {
            Ask clone = (Ask)this.Copy();
            Substitutions.DoMeta(clone.meta, cr.Globals());
            Substitutions.Do(ref clone.Text, cr.Globals());
            clone.options.ForEach(delegate (Opt o)
            {
                Substitutions.Do(ref o.Text, cr.Globals());
            });
            return new ChatEvent(clone);
        }

        public override string ToString()
        {
            string s = "[" + TypeName().ToUpper() + "] " + QQ(Text) + " (";
            Options().ForEach(o => s += o.Text + ",");
            return s.Substring(0, s.Length - 1) + ") " + MetaStr();
        }

        /*public string ToTree()
        {
            string s = base.ToString() + "\n";
            Options().ForEach(o => s += "    " + o + "\n");
            return s.Substring(0, s.Length - 1);
        }*/

        public override Command Copy()
        {
            Ask clone = (Ask)this.MemberwiseClone();
            clone.options = new List<Opt>();
            Options().ForEach(delegate (Opt o)
            {
                clone.AddOption((Opt)o.Copy());
            });
            return clone;
        }

    }

    public class Opt : Command
    {
        public ICommand action;

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
                 + (action is NoOp ? "" : " (-> " + action.GetText() + ")");
        }

        public string ActionText()
        {
            return action != null ? action.GetText() : "";
        }

        public override Command Copy()
        {
            Opt o = (Opt)this.MemberwiseClone();
            if (o.action != null) o.action = (Command)action.Copy();
            return o;
        }
    }

    public class Constraint
    {
        public readonly string name; // needed?
        public readonly string value;
        public readonly Operator op;

        public Constraint(string key, string ops, string val)
        {
            this.name = key;
            this.value = val;
            this.op = Operator.FromString(ops);
        }

        public bool Check(string toCheck)
        {
            return op.Invoke(value, toCheck);
        }
    }

    public class Find : ICommand
    {
        const string PATT = @"($?[a-zA-Z_][a-zA-Z0-9_]+) *([!<=>*^$]+) *([^ ]+)";
        protected static Regex QUERY = new Regex(PATT);

        protected Dictionary<string, Constraint> query;

        public Dictionary<string, Constraint> Query()
        {
            return query;
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

        public void Init(string[] args, string[] meta)
        {
            if (args.Length > 0) throw BadArgs(args, 1);
            query = new Dictionary<string, Constraint>();
            ParseMeta(meta);
        }

        protected void ParseMeta(string[] pairs)
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
                if (query == null) query = new Dictionary<string, Constraint>();
                query.Add(key, new Constraint(key, op, val));
            }
        }

        /**
         * Do the fuzzy search, then call Run() on the selected Chat
         */
        public ChatEvent Fire(ChatRuntime cr)
        {
            Command clone = this.Copy();
            Substitutions.DoMeta(clone.Meta(), cr.Globals());
            ChatEvent ce = new ChatEvent(clone);
            Find find = (Find)ce.Command;
            Chat c = cr.Find(find.query);
            if (c != null) cr.Run(c);
            return ce;
        }

        public Command Copy()
        {
            throw new NotImplementedException();
        }

        public string GetText()
        {
            return null;
        }

        public int GetPauseAfterMs()
        {
            return 0;
        }

        public Dictionary<string, string> Meta()
        {
            throw new NotImplementedException("Not for Find");
        }

        public string GetMeta(string key)
        {
            throw new NotImplementedException("Not for Find");
        }

        public bool HasMeta()
        {
            return false;
        }

        public string TypeName()
        {
            return this.GetType().ToString().Replace(Command.PACKAGE, "");
        }
    }

    public interface IEmittable { }

    public class Chat : Command
    {
        public List<ICommand> commands = new List<ICommand>(); // not copied

        public Chat() : this("C" + Util.EpochMs()) { }

        public Chat(string name)
        {
            this.Text = name;
        }

        public int Count()
        {
            return commands.Count();
        }

        public void AddCommand(ICommand c)
        {
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
    }

    public interface ICommand
    {
        void Init(string[] args, string[] meta);
        Command Copy();
        string GetText();
        ChatEvent Fire(ChatRuntime cr);
        int GetPauseAfterMs();
        Dictionary<string, string> Meta();
        string GetMeta(string key);
        bool HasMeta();
        string TypeName();
    }

    public abstract class Command : MetaData, ICommand
    {
        public const string PACKAGE = "Dialogic.";

        public const int Infinite = -1;

        protected static int IDGEN = 0;

        public static readonly Command NOP = new NoOp();

        public string Id { get; protected set; }

        public int PauseAfterMs { get; protected set; }

        public string Text, Actor;

        protected Command()
        {
            this.Id = (++IDGEN).ToString();
            this.PauseAfterMs = 0;
        }

        public int GetPauseAfterMs()
        {
            return PauseAfterMs;
        }
        public string GetText() { return Text; }

        private static string ToMixedCase(string s)
        {
            return (s[0] + "").ToUpper() + s.Substring(1).ToLower();
        }

        public static ICommand Create(string type, string[] args, string[] meta)
        {
            type = ToMixedCase(type);
            var cmd = Create(Type.GetType(PACKAGE + type), args, meta);
            if (cmd != null) return cmd;
            throw new TypeLoadException("No type: " + PACKAGE + type);
        }

        public static ICommand Create(Type type, string[] args, string[] meta)
        {
            ICommand cmd = (ICommand)Activator.CreateInstance(type);
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
        }

        public virtual string TypeName()
        {
            return this.GetType().ToString().Replace(PACKAGE, "");
        }

        public virtual ChatEvent Fire(ChatRuntime cr)
        {
            var clone = this.Copy();
            var globals = cr.Globals();
            Substitutions.DoMeta(clone.meta, globals);
            Substitutions.Do(ref clone.Text, globals);
            return new ChatEvent(clone);
        }

        public virtual Command Copy()
        {
            return (Command)this.MemberwiseClone();
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
        protected Dictionary<string, string> meta;

        public bool HasMeta()
        {
            return meta != null && meta.Count > 0;
        }

        public string GetMeta(string key)
        {
            return meta != null && meta.ContainsKey(key) ? meta[key] : null;
        }

        /*public string GetMetaString(string key, string defaultVal = null)
        {
            object o = meta != null && meta.ContainsKey(key) ? meta[key] : defaultVal;
            return (string)(o is string ? o : defaultVal);
        }

        public int GetMetaInt(string key, int defaultVal = 0)
        {
            object o = meta != null && meta.ContainsKey(key) ? meta[key] : defaultVal;
            //Console.WriteLine(o.GetType()+" "+o);
            if (o is double)
            {
                double d = Math.Round((double)o, 0);
                int i = (int)d;
                return i;
            }
            return ((int)(o is int ? o : defaultVal));
        }

        public double GetMetaDouble(string key, double defaultVal = 0.0)
        {
            object o = meta != null && meta.ContainsKey(key) ? meta[key] : defaultVal;
            if (o is int) o = System.Convert.ToDouble(o);
            return (double)(o is double ? o : defaultVal);
        }

        public bool GetMetaBool(string key, bool defaultVal = false)
        {
            object o = meta != null && meta.ContainsKey(key) ? meta[key] : defaultVal;
            return (bool)(o is bool ? o : defaultVal);
        }*/

        /* Note: new keys will overwrite old keys with same name */
        public void SetMeta(string key, string val)
        {
            if (meta == null) meta = new Dictionary<string, string>();
            meta[key] = val;
        }

        public void AddMeta(Dictionary<string, string> pairs)
        {
            if (pairs != null)
            {
                foreach (var key in pairs.Keys)
                {
                    meta[key] = pairs[key];
                }
            }
        }

        public Dictionary<string, string> Meta()
        {
            return meta;
        }

        public List<KeyValuePair<string, string>> ToList()
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

        /**
         * Set the value for the key as a primitive int, double, or bool, 
         * if such conversion is possible, otherwise as a string object.
        protected void SetMetaDynamic(string key, string val)
        {
            if (meta == null) meta = new Dictionary<string, object>();
            SetMeta(key, Util.ToType(val));
        }*/

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