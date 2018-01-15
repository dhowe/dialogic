using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace Dialogic
{
    public abstract class Command
    {
        const string PACKAGE = "Dialogic.";

        protected static int IDGEN = 0;

        protected static readonly Command NOP = new NoOp();

        public string Id { get; protected set; }
        public string Text;
        internal float WaitSecs = 0;

        protected Command() => this.Id = (++IDGEN).ToString();

        private static string ToMixedCase(string s) =>
            (s[0] + "").ToUpper() + s.Substring(1).ToLower();

        public static Command Create(string type, params string[] args)
        {
            type = ToMixedCase(type);
            return Create(Type.GetType(PACKAGE + type) ??
                throw new TypeLoadException("No type: " + PACKAGE + type), args);
        }

        public static Command Create(Type type, params string[] args)
        {
            Command cmd = (Command)Activator.CreateInstance(type);
            cmd.Init(args);
            return cmd;
        }

        public virtual int WaitTime() => (int)(WaitSecs * 1000); // no wait

        public virtual void Init(params string[] args) => this.Text = String.Join("", args);

        public virtual string TypeName() => this.GetType().ToString().Replace(PACKAGE, "");

        public virtual ChatEvent Fire(ChatRuntime cr)
        {
            Command clone = this.Copy();
            Substitutor.ReplaceGroups(ref clone.Text);
            Substitutor.ReplaceVars(ref clone.Text, cr.globals);
            return new ChatEvent(clone);
//            this.HandleVars(cr.globals); 
        }

        public virtual Command Copy()
        {
            return (Command)this.MemberwiseClone();
        }


        //public virtual void HandleVars(Dictionary<string, object> globals)
        //{
        //    if (!string.IsNullOrEmpty(Text))
        //    {
        //        foreach (string s in SortByLength(globals.Keys))
        //        {
        //            //System.Console.WriteLine($"s=${s} -> {globals[s]}"); 
        //            Text = Text.Replace("$" + s, globals[s].ToString());
        //        }
        //    }
        //}

        protected Exception BadArg(string msg) => throw new ArgumentException(msg);

        protected Exception BadArgs(string[] args, int expected)
        {
            return BadArg($"{TypeName().ToUpper()} expects {expected} args,"
                + $" got {args.Length}: '{string.Join(" # ", args)}'\n");
        }

        public override string ToString() => "[" + TypeName().ToUpper() + "] " + Text;

        protected static string QQ(string text) => "'" + text + "'";

        //protected static IEnumerable<string> SortByLength(IEnumerable<string> e)
        //{
        //    return from s in e orderby s.Length descending select s;
        //}
    }

    public class NoOp : Command { }
    public class Do : Command { }

    public class Say : Command
    {
        public override string ToString() => "["
            + TypeName().ToUpper() + "] " + QQ(Text);
    }

    public class Go : Command
    {
        public Go() : base() { }

        public Go(string text) : base() => this.Text = text;

        public override ChatEvent Fire(ChatRuntime cr)
        {
            ChatEvent ce = base.Fire(cr);
            cr.Run(cr.FindChat(Text));
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

        public override string ToString() => "[" + TypeName().ToUpper() + "] $" + Text + '=' + Value;

        //public override void HandleVars(Dictionary<string, object> globals) { } // no-op

        public override ChatEvent Fire(ChatRuntime cr)
        {
            Set clone = (Set)this.Copy();
            Substitutor.ReplaceVars(ref clone.Value, cr.globals);
            cr.Globals()[Text] = Value; // set the global var
            return new ChatEvent(clone);
        }
    }

    public class Chat : Command
    {
        public List<Command> commands = new List<Command>(); // not copied

        public Chat() => this.Text = "C" + Environment.TickCount;

        public void AddCommand(Command c) => this.commands.Add(c);

        public override void Init(params string[] args) {

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

        // public override Command Copy() // ignore 'commands'

        public string ToTree()
        {
            string s = base.ToString() + "\n";
            commands.ForEach(c => s += "  " + c + "\n");
            return s;
        }
    }

    public class Wait : Command
    {
        public override void Init(params string[] args) =>
            WaitSecs = args.Length == 1 ? float.Parse(args[0]) : 0;

        public override string ToString() => "[" + TypeName().ToUpper() + "] " + WaitSecs;

        //public override void HandleVars(Dictionary<string, object> globals) { }

        public override int WaitTime() => WaitSecs > 0
            ? (int)(WaitSecs * 1000) : Timeout.Infinite;
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
            this.action = (args.Length > 1) ? Command.Create(typeof(Go), args[1]) : null;
        }

        public override string ToString() => "[" + TypeName().ToUpper() + "] "
            + QQ(Text) + (action is NoOp ? "" : " (-> " + action.Text + ")");

        public string ActionText() => action != null ? action.Text : "";

        public override ChatEvent Fire(ChatRuntime cr)
        {
            ChatEvent ce = base.Fire(cr);
            Substitutor.ReplaceVars(ref action.Text, cr.globals); // also labels
            return ce;
        }

        public override Command Copy()
        {
            Opt o = (Opt)this.MemberwiseClone();
            o.action = (Command)action.Copy();
            return o;
        }
    }

    public class Ask : Command
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

        public override int WaitTime() => WaitSecs > 0 
            ?  (int)(WaitSecs * 1000) : Timeout.Infinite;

        public Opt Selected() => options[SelectedIdx];

        public void AddOption(Opt o) => options.Add(o);

        public Command Choose(string input) // return next Command or null
        {
            attempts++;
            if (int.TryParse(input, out int i))
            {
                if (i > 0 && i <= options.Count)
                {
                    SelectedIdx = --i;
                    return this.options[SelectedIdx].action;
                }
            }
            throw new InvalidChoice(this);
        }

        public override ChatEvent Fire(ChatRuntime cr)
        {
            Ask clone = (Ask)this.Copy();
            Substitutor.ReplaceGroups(ref clone.Text);
            Substitutor.ReplaceVars(ref clone.Text, cr.globals);
            this.options.ForEach(o => o.Fire(cr)); // fire for child options
            return new ChatEvent(clone);
        }

        public override string ToString()
        {
            string s = "[" + TypeName().ToUpper() + "] " + QQ(Text) + " (";
            this.options.ForEach(o => s += o.Text + ",");
            return s.Substring(0, s.Length - 1) + ")";
        }

        public string ToTree()
        {
            string s = "[" + TypeName().ToUpper() + "] " + QQ(Text) + "\n";
            this.options.ForEach(o => s += "    " + o + "\n");
            return s.Substring(0, s.Length - 1);
        }

        public override Command Copy()
        {
            Ask clone = (Ask)this.MemberwiseClone();
            clone.options = new List<Opt>();
            //for (int i = 0; i < options.Count; i++)
            //{
            //    clone.AddOption((Opt)options[i].Copy());
            //}
            this.options.ForEach(delegate(Opt o) {
                clone.AddOption((Opt)o.Copy());  
            });
            return clone;
        }
    }
}