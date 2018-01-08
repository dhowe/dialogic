using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace Dialogic
{
    public abstract class Command
    {
        protected static int IDGEN = 0;

        protected static NoOp NOP = new NoOp();

        protected static string PACKAGE = "Dialogic.";

        public static void Out(object s) => Console.WriteLine(s);

        public string Id { get; protected set; }

        public string Text { get; protected set; }

        protected Command() => this.Id = (++IDGEN).ToString();

        private static string ToMixedCase(string s)
        {
            return (s[0] + "").ToUpper() + s.Substring(1).ToLower();
        }

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

        protected static string QQ(string text) => "'" + text + "'";

        public virtual void Init(params string[] args) => this.Text = String.Join("", args);

        public virtual string TypeName() => this.GetType().ToString().Replace(PACKAGE, "");

        public override string ToString() => "[" + TypeName().ToUpper() + "] " + Text;
    }

    public class Go : Command { 
        
        public Go() : base() { }

        public Go(string text) : base() {
            this.Text = text;
        }
    }

    public class NoOp : Command { }

    public class Say : Command { 
        public override string ToString() => "[" + TypeName().ToUpper() + "] " + QQ(Text);
    }

    public class Do : Command { }

    public class Chat : Command
    {
        public List<Command> commands = new List<Command>();

        public Chat() : this("C" + Environment.TickCount) { }

        public Chat(string name) : base() => this.Text = name;

        public override string ToString()
        {
            string s = base.ToString() + "\n";
            commands.ForEach(c => s += "  " + c + "\n");
            return s;
        }

        public void AddCommand(Command c) => this.commands.Add(c);
    }

    public class Wait : Command
    {
        public float Seconds { get; protected set; }

        public override void Init(params string[] args) => 
            Seconds = args.Length == 1 ? float.Parse(args[0]) : float.MaxValue;

        public override string ToString() => "[" + TypeName().ToUpper() + "] " + Seconds;

        public int Millis() => (int)(Seconds * 1000);
    }

    public class Opt : Command
    {
        public Command action;

        public Opt() : this("") { }

        public Opt(string text) : this(text, NOP) { }

        public Opt(string text, Command action) : base() {
            this.Text = text;
            this.action = action;
        }

        public override void Init(params string[] args)
        {
            if (args.Length < 1)
            {
                throw new TypeLoadException("Bad args: " + args);
            }
            this.Text = args[0];
            this.action = (args.Length > 1) ? Command.Create(typeof(Go), args[1]) : null;
        }

        public override string ToString() => "[" + TypeName().ToUpper() + "] " 
            + QQ(Text) + (action is NoOp ? "" : " (-> " + action.Text + ")");

        public string ActionText() => action != null ? action.Text : "";
    }

    public class Ask : Command
    {
        public int SelectedIdx { get; protected set; }

        public int attempts = 0;

        private float seconds = -1;

        private readonly List<Opt> options = new List<Opt>();

        public List<Opt> Options() {
            
            if (options.Count < 1) {
                options.Add(new Opt("Yes"));
                options.Add(new Opt("No"));
            }
            return options;
        }
 
        public int Millis() => seconds > -1 ? (int)(seconds * 1000) : Int32.MaxValue;

        public Opt Selected() => options[SelectedIdx];

        public Ask AddOption(Opt o)
        {
            options.Add(o);
            return this;
        }

        public Command Choose(string input) // return next command or null
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
            return null;
        }

        public override string ToString()
        {
            string s = "[" + TypeName().ToUpper() + "] " + QQ(Text) + "\n";
            Options().ForEach(o => s += "    " + o + "\n");
            return s.Substring(0,s.Length-1);
        }
    }

    /*public struct Func  // named Action
    {
        public Action action;
        public string name;

        public Func(string name, Action action)
        {

            this.name = name;
            this.action = action;
        }

        public void Invoke() => action.Invoke();
        public override string ToString() => "Action." + name;
    }*/
}