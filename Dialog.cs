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

        /*public Dialog React(params string[] labels) {
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
                reactions[i] = (() => { new Gotu(this, label).Fire(); });
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
        }*/

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