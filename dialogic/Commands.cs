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
        public string Text { get; set; }
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

        public virtual void Fire(ChatRuntime cr) {
            Text = Substitutor.ReplaceGroups(Text);
            Text = Substitutor.ReplaceVars(Text, cr.globals);
//            this.HandleVars(cr.globals); 
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

        public override void Fire(ChatRuntime cr)
        {
            base.Fire(cr);
            cr.Run(cr.FindChat(Text));
        }
    }

    public class Set : Command
    {
        public object Value { get; protected set; }

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

        public override string ToString() => "[" + TypeName().ToUpper() + "] $" + Text + '=' + Value;

        //public override void HandleVars(Dictionary<string, object> globals) { } // no-op

        public override void Fire(ChatRuntime cr)
        {
            if (Value is string s)
            {
                Value = Substitutor.ReplaceVars(s, cr.globals);
            }
            cr.Globals()[Text] = Value; // set the global var
        }
    }

    public class Chat : Command
    {
        public List<Command> commands = new List<Command>();

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

        public override void Fire(ChatRuntime cr)
        {
            Text = Substitutor.ReplaceVars(Text, cr.globals); // needed ?
        }

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

        public override void Fire(ChatRuntime cr)
        {
            Text = Substitutor.ReplaceGroups(Text);
            Text = Substitutor.ReplaceVars(Text, cr.globals);
            action.Text = Substitutor.ReplaceVars(action.Text, cr.globals); // also labels
        }
    }

    public class Ask : Command
    {
        public int SelectedIdx { get; protected set; }

        public int attempts = 0;

        private readonly List<Opt> options = new List<Opt>();

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

        public override void Fire(ChatRuntime cr)
        {
            //base.Fire(cr);
            Text = Substitutor.ReplaceGroups(Text, this);
            Text = Substitutor.ReplaceVars(Text, cr.globals);
            Options().ForEach(o => o.Fire(cr)); // fire for child options
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
    }
}