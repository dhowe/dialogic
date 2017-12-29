using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace Dialogic {
    public class Dialog {

        public List<Command> events;

        public DialogRunner runtime;

        public string name; // unused (multiple? is this not just a label?)

        public Dialog() : this("Default") { }

        public Dialog(string name) {
            this.name = name;
            events = new List<Command>();
            events.Add(new Label(this, "START"));
        }

        public Dialog Label(string text) {
            events.Add(new Label(this, text));
            return this;
        }

        public Dialog Say(string text) {
            events.Add(new Say(this, text));
            return this;
        }

        // public Dialog Wait() {
        //     events.Add(new Pause(this));
        //     return this;
        // }

        public Dialog Gotu(string label) {
            events.Add(new Gotu(this, label));
            return this;
        }

        public Dialog Pause(int ms) {
            events.Add(new Pause(this, ms));
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

        public int AddEvent(Command c) {
            this.events.Add(c);
            return this.events.Count;
        }

        public override string ToString() {
            string indent = "  ", s = "\n";
            foreach (var evt in events) {
                if (!(evt is Label)) s += indent;
                s += evt.ToString() + "\n";
            }
            return s + "\n";
        }
    }
}