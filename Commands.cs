using System;
using System.Collections.Generic;

namespace Dialogic {

    public abstract class Atom {

        protected static int IDGEN = 0;

        protected static string PACKAGE = "Dialogic.";

        public static void Out(object s) => Console.WriteLine(s);

        public abstract void Fire();

        public string id { get; protected set; }

        public Atom() => this.id = (++IDGEN).ToString();
    }

    public class Chat : Command {

        public List<Command> commands { get; private set; }
        public List<IChatListener> listeners { get; private set; }

        protected override void Init(string name) {
            base.Init(name);
            this.commands = new List<Command>();
            this.listeners = new List<IChatListener>();
        }

        public override void Fire() {
            //commands.ForEach((Command c) => {
            listeners.ForEach((IChatListener l) => {
                l.onChatEvent(this);
            });
            //});
        }

        //public void Fire() => commands.ForEach(a => a.Fire());
        //public void AddChild(Chat c) => this.children.Add(c);

        public void AddCommand(Command c) => this.commands.Add(c);

/*         public override string ToString() {
           return base.ToString();
        }
 */
        public string AsTree() {
            string ind = "  ", s = '\n' + base.ToString() + '\n';
            foreach (var c in commands)
                s += ind + c.ToString() + "\n";
            return s + "\n";
        }

        public Chat AddListener(IChatListener icl) {
            this.listeners.Add(icl);
            return this;
        }
    }

    public abstract class Command : Atom {

        public string text { get; private set; }

        public static Command create(string type, string args) {

            Type cmdType = Type.GetType(PACKAGE + type) ??
                throw new TypeLoadException("No type: " + PACKAGE + type);
            Command cmd = (Command) Activator.CreateInstance(cmdType);
            cmd.Init(args);
            return cmd;
        }

        protected virtual void Init(string args) {
            //Out("Create: " + this.TypeName());
            this.text = args;
        }

        public override void Fire() {
            Out(this.ToString());
        }

        protected virtual string TypeName() {
            return this.GetType().ToString().Replace(PACKAGE, "");
        }

        public override string ToString() => "[" + TypeName().ToUpper() + "] " + text;
    }

    public class Say : Command { }

    public class Wait : Command {

        public int millis { get; protected set; } // wait forever

        protected override void Init(string args) => this.millis = int.Parse(args);

        //public override void Fire() => Out("blah");

        public override string ToString() => "[" + TypeName().ToUpper() + "] " + millis;
    }

    public class Do : Command { }
}