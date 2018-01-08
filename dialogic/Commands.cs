using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace Dialogic
{

    public abstract class Command
    {
        protected static int IDGEN = 0;

        protected static string PACKAGE = "Dialogic.";

        public static void Out(object s) => Console.WriteLine(s);

        public string Id { get; protected set; }

        public string Text { get; protected set; }

        protected Command() => this.Id = (++IDGEN).ToString();

        private static string ToMixedCase(string s)
        {
            return (s[0] + "").ToUpper() + s.Substring(1).ToLower();
        }

        public static Command Create(string type, string args)
        {
            type = ToMixedCase(type);
            return Create(Type.GetType(PACKAGE + type) ??
                throw new TypeLoadException("No type: " + PACKAGE + type), args);
        }

        public static Command Create(Type type, string args)
        {
            Command cmd = (Command)Activator.CreateInstance(type);
            cmd.Init(args);
            return cmd;
        }

        public virtual void Init(string args) => this.Text = args;

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

    public class Say : Command { }

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
        public float seconds { get; protected set; }

        public override void Init(string args)
        {
            this.seconds = float.MaxValue;
            if (args.Length > 0)
            {
                this.seconds = float.Parse(args);
            }
        }

        public override string ToString() => "[" + TypeName().ToUpper() + "] " + seconds;

        public int Millis() => (int)(seconds * 1000);
    }

    public class Opt : Command
    {
        public Command action;

        public Opt() : this("") { }

        public Opt(string text) : this(text, new NoOp()) { }

        public Opt(string text, Command action) : base() {
            this.Text = text;
            this.action = action;
        }

        public override void Init(string args)
        {
            string[] arr = Regex.Split(args, " *=> *");
            if (arr.Length < 1)
            {
                throw new TypeLoadException("Bad args: " + args);
            }
            this.Text = arr[0];
            this.action = (arr.Length > 1) ? Command.Create(typeof(Go), arr[1]) : null;
        }

        public override string ToString() => "[" + TypeName().ToUpper() 
            + "] " + Text + " (=> " + this.action.Text + ")";

        public string ActionText()
        {
            return action != null ? action.Text : "";
        }
    }

    public class Ask : Command
    {
        public int SelectedIdx { get; protected set; }
        public int attempts = 0;

        private float seconds = -1;

        private List<Opt> options = new List<Opt>();

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
    }

    /*public struct Func
    { // named Action

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