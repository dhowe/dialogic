using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace Dialogic {
    public class Dialog {

        public List<Command> events { get; private set; }

        public DialogRunner runtime { get; private set; }

        public string name { get; private set; } // just start chat?

        public List<IDialogListener> listeners { get; private set; }

        public void Addlistener(IDialogListener listener) {
            listeners.Add(listener);
        }

        public Dialog() : this("Default") { }

        public Dialog(string name) {
            this.name = name;
            this.events = new List<Command>();
            this.events.Add(new Chat(this, "START"));
            this.listeners = new List<IDialogListener>();
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
                if (!(evt is Chat)) s += indent;
                s += evt.ToString() + "\n";
            }
            return s + "\n";
        }
    }
}