using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;

namespace Dialogic
{
    public abstract class Command
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
            throw new TypeLoadException("No type: "+PACKAGE + type);
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

    public abstract class Timed : Command
    {
        public float WaitSecs = 0; // no delay by default

        public virtual int WaitTime()
        {
            return (int)(WaitSecs * 1000); // no wait
        }
    }

    public class NoOp : Command
    {
        public override ChatEvent Fire(ChatRuntime cr)
        {
            return null;
        }
    }

    public class Timeout : Command
    {
        public readonly Timed timed;

        public Timeout(Timed a) : base() {
            timed = a;
        }

        public override string ToString()
        {
            return "[" + TypeName().ToUpper() + "] " + Text + " " + timed.WaitSecs;
        }
    }

    public class Do : Command { }

    /*enum Pacing
    {
        Fast = 150,
        Slow = 75,
        Default = 100,
        VerySlow = 50,
        VeryFast = 200
    };
    public class Pace : Meta
    {
        public double pace = 1.0;

        public override void Init(params string[] args)
        {
            if (!(Regex.IsMatch(args[0], @"^\d+%?$"))) 
            {
                throw BadArg("PACE requires INT%, found '"+args[0]+"'");  
            }
            this.pace = (int.Parse(args[0].TrimEnd('%')) / 100.0);
            this.Text = ((int)(pace*100)) + "%";
        }
    }*/

    public class Say : Timed
    {
        public Say() : base() {
            
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

        public override void Init(params string[] args)
        {
            string[] pair = ParseSetArgs(args);
            base.Init(pair[0]);
            this.Value = pair[1];
        }

        private string[] ParseSetArgs(string[] args)
        {
            if (args.Length != 1)
            {
                throw BadArgs(args, 1);
            }

            var pair = Regex.Split(args[0], @"\s*=\s*");
            if (pair.Length != 2) pair = Regex.Split(args[0], @"\s+");

            if (pair.Length != 2)
            {
                throw BadArgs(pair, 2);
            }

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
            return "[" + TypeName().ToUpper()
                + "] $" + Text + '=' + Value;
        }

        //public override void HandleVars(Dictionary<string, object> globals) { } // no-op

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
            WaitSecs = args.Length == 1 ? float.Parse(args[0]) : 0;
        }

        public override string ToString()
        {
            return "[" + TypeName().ToUpper() + "] " + WaitSecs;
        }

        //public override void HandleVars(Dictionary<string, object> globals) { }

        public override int WaitTime()
        {
            return WaitSecs > 0 ? (int)(WaitSecs * 1000) : System.Threading.Timeout.Infinite;
        }
    }

    public class Opt : Command
    {
        public Command action;

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

        //public override ChatEvent Fire(ChatRuntime cr)
        //{
        //    Console.WriteLine("Opt1:"+Text+" '"+action.Text+"'");
        //    ChatEvent ce = base.Fire(cr);
        //    Substitutor.Replace(ref Text, cr.globals); // also labels
        //    Console.WriteLine("Opt2:" + Text + " '" + action.Text + "'");
        //    return ce;
        //}

        public override Command Copy()
        {
            Opt o = (Opt)this.MemberwiseClone();
            if (o.action != null) o.action = (Command)action.Copy();
            return o;
        }
    }

    public class Ask : Timed
    {
        public int SelectedIdx { get; protected set; }

        protected int attempts = 0;

        protected List<Opt> options = new List<Opt>();

        public override void Init(params string[] args)
        {
            if (args.Length < 0 || args.Length > 2)
            {
                throw BadArg("Bad args: " + args.Length);
            }
            base.Init(args[0]);
            if (args.Length > 1) WaitSecs = float.Parse(args[1]);
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

        public override int WaitTime()
        {
            return WaitSecs > 0 ? (int)(WaitSecs * 1000) : System.Threading.Timeout.Infinite;
        }

        public Opt Selected()
        {
            return Options()[SelectedIdx];
        }

        public void AddOption(Opt o)
        {
            options.Add(o);
        }

        public Command Choose(string input) // return next Command or null
        {
            attempts++;
            int i = -1;
            try
            {
                i = Convert.ToInt32(input);
            }
            catch { /* ignore */ }

            if (i > 0 && i <= options.Count)
            {
                SelectedIdx = --i;
                return Options()[SelectedIdx].action;
            }

            throw new InvalidChoice(this);
        }

        public override ChatEvent Fire(ChatRuntime cr)
        {
            Ask clone = (Ask)this.Copy();
            Substitutions.Do(ref clone.Text, cr.globals);
            clone.options.ForEach(delegate (Opt o) {
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
            Options().ForEach(delegate(Opt o) {
                clone.AddOption((Opt)o.Copy());
            });
            return clone;
        }
    }

    public class Find : Cond
    {
        public override ChatEvent Fire(ChatRuntime cr)
        {
            ChatEvent ce = base.Fire(cr);
            cr.Run(cr.Find(((Find)ce.Command).conditions));
            return ce;
        }
    }

    public class Meta : Command
    {
        public Dictionary<string, string> tags;

        public Meta() {}

        public override void Init(params string[] args)
        {
            if (args.Length < 1) throw BadArgs(args, 1);

            for (int i = 0; i < args.Length; i++)
            {
                string[] parts = args[i].Split('=');
                if (parts.Length != 2) throw new Exception
                    ("Expected 2 parts, found " + parts.Length + ": " + parts);
                AddTag(parts[0].Trim(), parts[1].Trim());
            }
        }

        /* Note: new keys will overwrite old keys with same name */
        public void AddTag(string key, string val)
        {
            if (tags == null) tags = new Dictionary<string, string>();
            tags[key] = val;
        }
    }

    public class Cond : Command
    {
        public Dictionary<string, string> conditions;

        public Cond()
        {
            conditions = new Dictionary<string, string>();
        }

        public override void Init(params string[] args)
        {
            if (args.Length < 1) throw BadArgs(args, 1);

            for (int i = 0; i < args.Length; i++)
            {
                string[] parts = args[i].Split('=');
                if (parts.Length != 2) throw new Exception
                    ("Expected 2 parts, found " + parts.Length + ": " + parts);
                AddCondition(parts[0].Trim(), parts[1].Trim());
            }
        }

        /* Note: new keys will overwrite old keys with same name */
        public void AddCondition(string key, string val)
        {
            conditions[key] = val;
        }

        public void AddConditions(Cond cd)
        {
            foreach (var key in cd.conditions.Keys)
            {
                AddCondition(key, cd.conditions[key]);
            }
        }

        public virtual string ConditionsStr()
        {
            string s = "";
            if (conditions.Count > 0)
            {
                s += "(";
                foreach (var key in conditions.Keys)
                {
                    s += key + ":" + conditions[key] + ",";
                }
                s = s.Substring(0, s.Length - 1) + ")";
            }
            return s;
        }

        public override string ToString()
        {
            return base.ToString() + ConditionsStr();
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
            if (conditions.Count > 0) s += "  [COND] " + ConditionsStr() + "\n";
            commands.ForEach(c => s += "  " + c + "\n");
            return s;
        }
    }
}