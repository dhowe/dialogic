using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace Dialogic {

    // NEXT: 

    public class App {
        static void Main(string[] args) {

            (new Dialog())
            .Label("Start")
                .Say("Welcome to my tank...")
                //.Pause(500)
                .Label("Prompt1")
                .Ask("Do you want to play?",
                    new KeyValuePair<string, string>("yes", "Prompt2"),
                    new KeyValuePair<string, string>("no", "Prompt1"))
                .Label("Prompt2")
                .Ask("Do you want to go first?",
                    new KeyValuePair<string, string>("yes", "Game"),
                    new KeyValuePair<string, string>("no", "Prompt2"))
                .Label("Game")
                .Say("Ok, let's play!...")
                .Run();
        }
    }

    public class Dialog {

        public string name;
        public List<ScriptEvent> events;

        public DialogRunner runtime;

        public Dialog() : this("start") { }

        public Dialog(string name) {
            this.name = name;
            events = new List<ScriptEvent>();
        }

        public Dialog Label(string text) {
            events.Add(new Label(this, text));
            return this;
        }

        public Dialog Say(string text) {
            events.Add(new Say(this, text));
            return this;
        }

        public Dialog Wait() {
            events.Add(new Wait(this));
            return this;
        }

        public Dialog Goto(string label) {
            events.Add(new Goto(this, label));
            return this;
        }

        public Dialog Pause(int ms) {
            events.Add(new Wait(this, ms));
            return this;
        }

        public Dialog Ask(string prompt, params KeyValuePair<string, string>[] choices) {

            foreach (var item in choices) {

            }
            events.Add(new Prompt(this, prompt, choices));
            return this;
        }

        public Dialog Ask(string prompt, params string[] choices) {
            events.Add(new Prompt(this, prompt, choices));
            return this;
        }

        public Dialog React(params string[] labels) {
            object c = events[events.Count - 1];
            if (!(c is Prompt)) {
                throw new Exception("React() must be followed by Ask()");
            }
            Prompt prompt = (Prompt) c;
            if (labels.Length != prompt.options.Length) {
                throw new Exception("React() must get the same number of arguments as Ask()");
            }
            Action[] reactions = new Action[labels.Length];
            for (int i = 0; i < reactions.Length; i++) {
                string label = labels[i];
                reactions[i] = (() => { new Goto(this, label).Fire(); });
            }
            prompt.Reactions(reactions);
            return this;
        }

        public Dialog React(params Action[] reactions) {
            object c = events[events.Count - 1];
            if (!(c is Prompt)) {
                throw new Exception("React() must be followed by Ask()");
            }
            Prompt prompt = (Prompt) c;
            if (reactions.Length != prompt.options.Length) {
                throw new Exception("React() must get the same number of arguments as Ask()");
            }
            prompt.Reactions(reactions);
            return this;
        }

        public DialogRunner Run(Type t) {

            if (!typeof(DialogRunner).IsAssignableFrom(t)) {
                throw new Exception("Unexpected type: " + t);
            }

            return this.Run((DialogRunner) Activator.CreateInstance(t, this));
        }

        private DialogRunner Run(DialogRunner runner) {
            this.runtime = runner;
            return runner.Run();
        }

        public DialogRunner Run() {
            return this.Run(new ConsoleRunner(this));
        }
    }

    public class Label : ScriptEvent {
        public Label(Dialog d, string text) : base(d, text) { }

        public override int Fire() {

            return 0;
        }
    }

    public class Goto : ScriptEvent {
        public Goto(Dialog d, string text) : base(d, text) { }

        public override int Fire() {
            dialog.runtime.GotoLabel(text);
            return 0;
        }
    }

    public class Prompt : ScriptEvent {

        public string[] options;
        public int selected, attempts = 0;
        public Action[] reactions;

        public Prompt(Dialog d, string prompt, string[] options, Action[] reactions) : base(d, prompt) {
            this.options = options;
            this.reactions = reactions;
        }

        public Prompt(Dialog d, string prompt, params KeyValuePair<string, string>[] pairs) : base(d, prompt) {
            this.options = new string[pairs.Length];
            this.reactions = new Action[pairs.Length];
            for (int i = 0; i < pairs.Length; i++) {
                this.options[i] = pairs[i].Key;
                string label = pairs[i].Value;
                reactions[i] = (() => { new Goto(d, label).Fire(); });
            }
        }

        public Prompt(Dialog d, string prompt, params string[] options) : this(d, prompt, options, null) { }

        public void Reactions(params Action[] reactions) {
            this.reactions = reactions;
        }

        public void React() {
            if (this.reactions != null) {
                L.g(text + " -> " + options[selected] + " (" + this.reactions[selected] + ")\n");
                this.reactions[selected].Invoke();
            } else {
                Console.WriteLine("(" + id + ": " + text + " -> " + options[selected] + ")");
            }
        }

        public override int Fire() {
            base.Fire();
            for (int i = 0; i < options.Length; i++) {
                Console.WriteLine("  " + (i + 1) + ") " + options[i]);
            }
            return -1;
        }

        public bool Accept(string input) {
            int i;
            attempts++;
            if (int.TryParse(input, out i)) {
                if (i > 0 && i <= options.Length) {
                    selected = --i;
                    this.React();
                    return true;
                }
            }
            Console.WriteLine("\n" + actor + ": please select a # from 1-" + options.Length + "\n");
            return false;
        }

    }

    public class Say : ScriptEvent {
        public Say(Dialog d, string text) : base(d, text) { }
        public Say(Dialog d, string actor, string text) : base(d, text) {
            this.actor = actor;
        }
    }

    public class Wait : ScriptEvent {
        private int millis = -1; // wait forever

        public Wait(Dialog d) : base(d, "") { }

        public Wait(Dialog d, int ms) : base(d, "") {
            millis = ms;
        }
        public override int Fire() {
            return millis;
        }
    }
    public abstract class ScriptEvent {
        internal static int IDGEN = 0;
        public int id = ++IDGEN;
        public string actor = "Guppy", text = "";
        protected Dialog dialog;
        public virtual int Fire() {
            Console.WriteLine(this.actor + ": " + this.text);
            return 0;
        } // return -1=pause, 0=continue, #=pauseFor
        public ScriptEvent(Dialog d) {
            this.dialog = d;
        }
        public ScriptEvent(Dialog d, string text) {
            this.dialog = d;
            this.text = text;
        }
    }

    public class ConsoleRunner : DialogRunner {

        public ConsoleRunner(Dialog d) : base(d) { }

        public static void Play(Dialog d) {
            new ConsoleRunner(d).Run();
        }

        public override void OnChoice(string input) {
            Prompt last = CurrentChoice();
            if (!last.Accept(input)) {
                this.Step(-1);
            }
        }

        private Prompt CurrentChoice() {
            for (var i = cursor; i >= 0; i--) {
                if (dialog.events[i] is Prompt) {
                    return (Prompt) dialog.events[i];
                }
            }
            return null;
        }

        public override DialogRunner Run() {
            Console.WriteLine("");
            int rc = -1;
            for (cursor = 0; cursor < dialog.events.Count; cursor++) {
                var evt = dialog.events[cursor];
                rc = evt.Fire();
                if (rc >= 0) {
                    Thread.Sleep(rc);
                } else {
                    this.OnChoice(Console.ReadKey(true).KeyChar.ToString());
                }
            }
            Console.WriteLine("\nDialog Complete");
            return this;
        }
    } 

    public abstract class DialogRunner {
        protected Dialog dialog;
        public int cursor;
        public DialogRunner(Dialog d) {
            this.dialog = d;
        }
        public abstract void OnChoice(string input);
        public int GotoLabel(string label) {
            for (int i = 0; i < dialog.events.Count; i++) {
                ScriptEvent evt = dialog.events[i];
                if (evt is Label && evt.text == label) {
                    return this.Cursor(i);
                }
            }
            return -1;
        }
        public abstract DialogRunner Run();
        public int Step(int steps) {
            cursor += steps;
            return cursor;
        }
        public int Cursor(int target) {
            cursor = target;
            return cursor;
        }
    }
    static class L {
        // static void og<T>( < T > o) {
        //     if (s == null || s.Length == 0)
        //         Console.WriteLine();
        //     if (s.Length > 1) {
        //         string ss = "";
        //         for (int i = 0; i < s.Length; i++) {
        //             ss += s[i] + ",";
        //         }
        //         s[0] = ss.Substring(0, ss.Length - 1);
        //     }
        //     Console.WriteLine(s[0]);
        // }
        public static void g<T>(T o) {
            if (o is string) {
                Console.WriteLine(o);
            } else if (o is IEnumerable) {
                var col = (IEnumerable) o;
                string s = "{ ";
                foreach (var item in col) {
                    s += item.ToString() + ", ";
                }
                Console.WriteLine(s.Substring(0, s.Length - 2) + " }");
            } else {
                Console.WriteLine(o.ToString());
            }
        }
    }
}