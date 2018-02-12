using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace Dialogic
{
    public abstract class Timed : Command
    {
        public float WaitSecs = 0; // default: no delay

        public virtual int WaitTime()
        {
            return WaitSecs < 0 ? -1 : (int)(WaitSecs * 1000);
        }
    }

    public class NoOp : Command
    {
        public override ChatEvent Fire(ChatRuntime cr)
        {
            return null;
        }
    }

    public class Timeout : Command // not-used
    {
        public readonly Timed timed;

        public Timeout(Timed a) : base()
        {
            timed = a;
        }

        public override string ToString()
        {
            return "[" + TypeName().ToUpper() + "] " + timed.WaitSecs;
        }
    }

    public class Do : Command { }

    /*enum Pacing {
        Fast = 150,
        Slow = 75,
        Default = 100,
        VerySlow = 50,
        VeryFast = 200
    };*/

    public class Say : Timed
    {
        public Say() : base()
        {

            this.WaitSecs = 1;
        }

        public override string ToString()
        {
            return "[" + TypeName().ToUpper() + "] " + QQ(Text);
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
            cr.Run(cr.FindChat(ce.Command.Text));
            return ce;
        }
    }

    public class Set : Command // TODO: need to rethink this
    {
        public string Value;

        public Set() { }

        public Set(string name, string value) // not used (add tests)
        {
            this.Text = name;
            this.Value = value;
        }

        public override void Init(params string[] args)
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
            return base.ToString() + '=' + Value;
        }

        public override ChatEvent Fire(ChatRuntime cr)
        {
            Set clone = (Set)this.Copy();
            Substitutions.Do(ref clone.Value, cr.globals);
            cr.Globals()[Text] = Value; // set the global var
            return new ChatEvent(clone);
        }
    }

    public class Wait : Timed
    {
        public override void Init(params string[] args)
        {
            if (args.Length != 1) throw BadArgs(args, 1);
            WaitSecs = float.Parse(args[0]);
        }

        public override string ToString()
        {
            return "[" + TypeName().ToUpper() + "] " + WaitSecs;
        }

        public override int WaitTime()
        {
            return WaitSecs > 0 ? (int)(WaitSecs * 1000)
                : System.Threading.Timeout.Infinite;
        }
    }

    public class Ask : Timed
    {
        public int SelectedIdx { get; protected set; }

        protected List<Opt> options = new List<Opt>();

        public override void Init(params string[] args)
        {
            if (args.Length < 0 || args.Length > 2)
            {
                throw BadArg("Args(" + args.Length + ")=" + String.Join("#", args));
            }
            this.Text = args[0];
            WaitSecs = (args.Length > 1) ? float.Parse(args[1]) : -1;
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
            Substitutions.Do(ref clone.Text, cr.globals);
            clone.options.ForEach(delegate (Opt o)
            {
                Substitutions.Do(ref o.Text, cr.globals);
            });
            return new ChatEvent(clone);
        }

        public override string ToString()
        {
            string s = "[" + TypeName().ToUpper() + "] " + QQ(Text) + " (";
            Options().ForEach(o => s += o.Text + ",");
            return s.Substring(0, s.Length - 1) + ")";
        }

        public string ToTree()
        {
            string s = "[" + TypeName().ToUpper() + "] " + QQ(Text) + "\n";
            Options().ForEach(o => s += "    " + o + "\n");
            return s.Substring(0, s.Length - 1);
        }

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
        public Command action;
        public Ask parent;

        public Opt() : this("") { }

        public Opt(string text) : this(text, NOP) { }

        public Opt(string text, Command action) : base()
        {
            this.Text = text;
            this.action = action;
        }

        public override void Init(params string[] args)
        {
            if (args.Length < 1) throw BadArgs(args, 1);
            this.Text = args[0];
            this.action = (args.Length > 1) ? Command.Create(typeof(Go), args[1]) : NOP;
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

        public override Command Copy()
        {
            Opt o = (Opt)this.MemberwiseClone();
            if (o.action != null) o.action = (Command)action.Copy();
            return o;
        }
    }

    public class Find : Cond
    {
        public override ChatEvent Fire(ChatRuntime cr)
        {
            ChatEvent ce = base.Fire(cr);
            cr.Run(cr.Find(((Find)ce.Command).lookup));
            return ce;
        }
    }

    public class Meta : Command
    {
        public override void Init(params string[] args)
        {
            if (args.Length < 1) throw BadArgs(args, 1);
            PairsFromArgs(args);
        }
    }

    public class Cond : Command
    {
        public override void Init(params string[] args)
        {
            if (args.Length < 1) throw BadArgs(args, 1);
            PairsFromArgs(args);
        }

        public void AddPairs(Cond cd)
        {
            foreach (var key in cd.lookup.Keys)
            {
                AddPair(key, cd.lookup[key]);
            }
        }

        public override string ToString()
        {
            return base.ToString() + PairsToString();
        }
    }

    public class Chat : Cond
    {
        public List<Command> commands = new List<Command>(); // not copied

        public Chat() : this("C" + Util.Millis()) { }

        public Chat(string name)
        {
            this.Text = name;
        }

        public void AddCommand(Command c)
        {
            this.commands.Add(c);
        }

        public override void Init(params string[] args)
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
        }

        public string ToTree()
        {
            string s = "[" + TypeName().ToUpper() + "] " + Text + "\n";
            if (PairsCount() > 0) s += "  [COND] " + PairsToString() + "\n";
            commands.ForEach(c => s += "  " + c + "\n");
            return s;
        }
    }

    public abstract class Command : KeysAndValues
    {
        const string PACKAGE = "Dialogic.";

        protected static int IDGEN = 0;

        public static readonly Command NOP = new NoOp();

        public string Id { get; protected set; }

        public string Text;

        protected Command()
        {
            this.Id = (++IDGEN).ToString();
        }

        private static string ToMixedCase(string s)
        {
            return (s[0] + "").ToUpper() + s.Substring(1).ToLower();
        }

        public static Command Create(string type, params string[] args)
        {
            type = ToMixedCase(type);
            var cmd = Create(Type.GetType(PACKAGE + type), args);
            if (cmd != null) return cmd;
            throw new TypeLoadException("No type: " + PACKAGE + type);
        }

        public static Command Create(Type type, params string[] args)
        {
            Command cmd = (Command)Activator.CreateInstance(type);
            cmd.Init(args);
            return cmd;
        }

        public virtual void Init(params string[] args)
        {
            this.Text = String.Join("", args);
        }

        public virtual string TypeName()
        {
            return this.GetType().ToString().Replace(PACKAGE, "");
        }

        public virtual ChatEvent Fire(ChatRuntime cr)
        {
            Command clone = this.Copy();
            Substitutions.Do(ref clone.Text, cr.globals);
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
            return "[" + TypeName().ToUpper() + "] " + Text;
        }

        protected static string QQ(string text)
        {
            return "'" + text + "'";
        }
    }

    public class KeysAndValues
    {
        protected Dictionary<string, string> lookup;

        /* Note: new keys will overwrite old keys with same name */
        public void AddPair(string key, string val)
        {
            if (lookup == null)
            {
                lookup = new Dictionary<string, string>();
            }
            lookup[key] = val;
        }

        public Dictionary<string, string> AsDict()
        {
            return lookup;
        }

        public int PairsCount()
        {
            return lookup == null ? 0 : lookup.Count;
        }

        protected virtual string PairsToString()
        {
            string s = "";
            if (PairsCount() > 0)
            {
                s += "(";
                foreach (var key in lookup.Keys)
                {
                    s += key + ":" + lookup[key] + ",";
                }
                s = s.Substring(0, s.Length - 1) + ")";
            }
            return s;
        }

        protected void PairsFromArgs(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                string[] parts = args[i].Split('=');

                if (parts.Length != 2) throw new Exception
                    ("Expected 2 parts, found " + parts.Length + ": " + parts);

                AddPair(parts[0].Trim(), parts[1].Trim());
            }
        }
    }

}