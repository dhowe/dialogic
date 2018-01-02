using System;
using System.Collections.Generic;
using System.Threading;

namespace Dialogic {

    public static class ChatManager {

        static Dictionary<string, Chat> chats = new Dictionary<string, Chat>();
        static List<IChatListener> listeners = new List<IChatListener>();
        static Stack<Chat> stack = new Stack<Chat>();
        private static bool paused = false;

        public static void AddListener(IChatListener icl) {
            listeners.Add(icl);
        }

        internal static void Register(Chat c) {
            Console.WriteLine("Register: Chat@" + c.id);
            chats.Add(c.id, c);
        }

        private static void notifyListeners(Command c) {
            listeners.ForEach(icl => icl.onChatEvent(c));
            c.Fire();
        }

        public static void Run(Chat start) {
            Chat current = start;
            while (!paused) {
                notifyListeners(current);
                foreach (var c in current.commands) {
                    if (c is Go) {
                        current = lookup(c.text);
                    } else {
                        notifyListeners(c);
                    }
                }
            }
        }

        private static Chat lookup(string id) {
            return chats[id];
        }
    }

    public class Chat : Command {

        public List<Command> commands { get; private set; }

        protected override void Init(string name) {
            base.Init(name);
        }

        public Chat() : base() {
            this.commands = new List<Command>();
            ChatManager.Register(this);
            //Console.WriteLine("CHAT.id="+id);            
        }

        public void Run() => ChatManager.Run(this);

        public void AddCommand(Command c) {
            this.commands.Add(c);
        }

        public override string ToString() {
            return base.ToString() + "#" + id.ToUpper();
        }

        public string AsTree() {
            Chat parent = this;
            string ind = "  ", s = '\n' + base.ToString() + '\n';
            for (int i = 0; i < parent.commands.Count; i++) {
                var cmd = parent.commands[i];
                if (cmd is Chat) {
                    s += ind + ((Chat) cmd).AsTree();
                } else {
                    s += ind + cmd.ToString() + "\n";
                }
            }
            return s;

        }
    }

    public abstract class Command {

        protected static int IDGEN = 0;

        protected static string PACKAGE = "Dialogic.";

        public static void Out(object s) => Console.WriteLine(s);

        public static Command create(string type, string args) {

            return create(Type.GetType(PACKAGE + type) ??
                throw new TypeLoadException("No type: " + PACKAGE + type), args);
        }

        public static Command create(Type type, string args) {

            Command cmd = (Command) Activator.CreateInstance(type);
            cmd.Init(args);
            return cmd;
        }

        public Command() {
            this.id = (++IDGEN).ToString();
        }

        public string id { get; protected set; }

        public string text { get; private set; }

        //public virtual void Fire() { }

        protected virtual void Init(string args) {
            //Out("Create: " + this.TypeName());
            this.text = args;
        }

        protected virtual string TypeName() {
            return this.GetType().ToString().Replace(PACKAGE, "");
        }

        public override string ToString() => "[" + TypeName().ToUpper() + "] " + text;
        public virtual void Fire() { }
    }

    public class Go : Command { }

    public class Say : Command { }

    public class Do : Command { }

    public class Wait : Command {

        public float seconds { get; protected set; }

        protected override void Init(string args) {
            this.seconds = float.MaxValue;
            if (args.Length > 0) {
                this.seconds = float.Parse(args);
            }
        }

        public override void Fire() {
            Thread.Sleep((int) (seconds * 1000));
        }

        public override string ToString() => "[" + TypeName().ToUpper() + "] " + seconds;
    }

    public class Ask : Wait {

        public static readonly Func NO_OP = new Func("NO_OP", (() => { }));

        public int selected, attempts = 0;

        public List<KeyValuePair<string, Func>> options;

        protected override void Init(string args) {
            Console.WriteLine("WAIT.ARGS: " + args);

            this.seconds = float.Parse(args);
        }
        /* 
                public void AddOption(string s, string chat) {
                    var action = chat != null ? new Func(chat, (() => { new Got(dialog, chat).Fire(); })) : NO_OP;
                    AddOption(s, action);
                } */

        public void AddOption(string s, Func todo) {
            options.Add(new KeyValuePair<string, Func>(s, todo));
        }
    }

    public struct Func {

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