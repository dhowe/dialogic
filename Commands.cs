using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace Dialogic {

    public class Chat : Command {

        public List<Command> commands = new List<Command>();

        public Chat() : this("C" + Environment.TickCount) { }

        public Chat(string name) : base() => this.text = name;

        public override string ToString() {
            string s = base.ToString() + "\n";
            commands.ForEach(c => s += "  " + c + "\n");
            return s;
        }

        public void AddCommand(Command c) => this.commands.Add(c);
    }

    public abstract class Command {

        protected static int IDGEN = 0;

        protected static string PACKAGE = "Dialogic.";

        public static void Out(object s) => Console.WriteLine(s);

        private static string toMixedCase(string s) {
            return (s[0] + "").ToUpper() + s.Substring(1).ToLower();
        }

        public static Command Create(string type, string args) {

            type = toMixedCase(type);
            return Create(Type.GetType(PACKAGE + type) ??
                throw new TypeLoadException("No type: " + PACKAGE + type), args);
        }

        public static Command Create(Type type, string args) {

            Command cmd = (Command) Activator.CreateInstance(type);
            cmd.Init(args);
            return cmd;
        }

        public string id { get; protected set; }

        public string text { get; protected set; }

        public Command() => this.id = (++IDGEN).ToString();

        public virtual void Init(string args) => this.text = args;

        public virtual string TypeName() => this.GetType().ToString().Replace(PACKAGE, "");

        public override string ToString() => "[" + TypeName().ToUpper() + "] " + text;

        public virtual void Fire() { }
    }

    public class Go : Command { }

    public class Say : Command { }

    public class Do : Command { }

    public class Wait : Command {

        public float seconds { get; protected set; }

        public override void Init(string args) {
            this.seconds = float.MaxValue;
            if (args.Length > 0) {
                this.seconds = float.Parse(args);
            }
        }

        public override void Fire() {
            Thread.Sleep((int) (seconds * 1000));
        }

        public override string ToString() => "[" + TypeName().ToUpper() + "] " + seconds;

        public int Millis() =>(int) (seconds * 1000);
    }

    public class Opt : Command {

        public static readonly Func NO_OP = new Func("NO_OP", (() => { }));

        public Func action;

        public override void Init(string args) {
            string[] arr = Regex.Split(args, " *=> *");
            if (arr.Length < 1) {
                throw new TypeLoadException("Bad args: " + args);
            }
            this.text = arr[0];
            this.action = (arr.Length > 1) ? new Func(arr[1], (() => { Command.Create(typeof(Go), arr[1]).Fire(); })) : NO_OP;
            //Console.WriteLine("text: " + text+" action: " + action.name);
        }

        public override string ToString() => "[" + TypeName().ToUpper() + "] " + text + " (=> " + this.action.name + ")";
    }

    public class Ask : Command {

        public int selected, attempts = 0;

        public List<Opt> options = new List<Opt>();
        //public List<KeyValuePair<string, Func>> options;

        public void AddOption(Opt o) {
            //Console.WriteLine("Adding: " + o);
            //options.Add(new KeyValuePair<string, Func>(o.text, o.action));
            options.Add(o);
        }
    }

    public struct Func { // named Action

        public Action action;
        public string name;

        public Func(string name, Action action) {

            this.name = name;
            this.action = action;
        }

        public void Invoke() => action.Invoke();

        public override string ToString() => "Action." + name;
    }
}