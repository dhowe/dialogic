using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;

namespace Dialogic {

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

    public abstract class Command {

        internal static int IDGEN = 0;

        public int id = ++IDGEN;

        public string actor = "Guppy", text = "";

        protected Dialog dialog;

        public virtual int Fire() {
            Out(this.actor + ": " + cleanString(this.text));
            return 0;
        } // return -1=pause, 0=continue, #=pauseFor#

        public virtual void Out(object s) => Console.WriteLine(s);

        public virtual string cleanString(string text) {
            var str = new Regex("^ *'").Replace(text, "");
            return new Regex("' *$").Replace(str, "");
        }

        public Command(Dialog d) : this(d, null) { }

        public Command(Dialog d, string text) {
            this.dialog = d ??
                throw new ArgumentNullException(nameof(d));
            if (text != null) this.text = text;
        }

        public override string ToString() => GetType().ToString(); //.Replace("Dialogic.", "") + text;
    }

    public class Label : Command {
        public Label(Dialog d, string text) : base(d, text) {
            if (string.IsNullOrWhiteSpace(text)) {
                throw new ArgumentException("Invalid argument", nameof(text));
            }
        }

        public override int Fire() => 0;
        public override string ToString() => "Label " + text;
    }

    public class Say : Command {
        public Say(Dialog d, string text) : base(d, text) { }
        // public Say(Dialog d, string actor, string text) : base(d, text) {
        //     this.actor = actor;
        // }
        public override string ToString() => "Say " + text;
    }

    public class Pause : Command {
        private int millis = -1; // wait forever

        public Pause(Dialog d) : base(d, "") { }

        public Pause(Dialog d, int ms) : base(d, "") {
            millis = ms;
        }
        public override int Fire() => millis;

        public override string ToString() {
            return millis > -1 ? "Pause " + millis : "Pause"; // || "Wait"
        }
    }
    public class Gotu : Command { // TODO: validate labels pre-Run()
        public Gotu(Dialog d, string text) : base(d, text) { }

        public override int Fire() {
            dialog.runtime.GotoLabel(text);
            return 0;
        }
        public override string ToString() => "Goto " + text;
    }

    public class Ask : Command {

        public static readonly Func NO_OP = new Func("NO_OP", (() => { }));

        public int selected, attempts = 0;

        public List<KeyValuePair<string, Func>> options;

        public Ask(Dialog d, string prompt) : base(d, prompt) {

            this.options = new List<KeyValuePair<string, Func>>();
        }

        public void AddOption(string s) {
            AddOption(s, (string) null);
        }

        public void AddOption(string s, string label) {
            var action = label != null ? new Func(label, (() => { new Gotu(dialog, label).Fire(); })) : NO_OP;
            AddOption(s, action);
        }

        public void AddOption(string s, Func todo) {
            options.Add(new KeyValuePair<string, Func>(s, todo));
        }

        public override int Fire() {
            base.Fire();
            for (int i = 0; options != null && i < options.Count; i++) {
                Out("  " + (i + 1) + ") " + options[i].Key);
            }
            return -1;
        }

        public bool Accept(string input) {
            //Console.WriteLine("Accept: " + input);

            if (this.options.Count > 0) {

                int i;
                attempts++;
                if (int.TryParse(input, out i)) {
                    if (i > 0 && i <= options.Count) {
                        selected = --i;
                        this.React();
                        return true;
                    }
                }
                Out("\n" + actor + ": Please select a # from 1-" + options.Count + "\n");
                return false;
            }

            return DefaultReaction(input);
        }

        public void React() {
            if (this.options.Count > 0) {
                Out(text + " -> " + options[selected].Key + " (" + options[selected].Value + ")\n");
                this.options[selected].Value.Invoke();
            } else {
                Console.WriteLine("(" + id + ": " + text + " -> " + options[selected].Key + ")");
            }
        }

        private bool DefaultReaction(string input) {
            if (input == "y" || input == "Y") {
                return true;
            }
            Out("\n" + actor + ": Sorry, I don't understand '" + input + "'\n");
            return false;
        }

        public override string ToString() {
            var s = "Ask " + text + " -> [";
            foreach (var o in options) {
                s += o.Key + ", ";
            }
            return s.Substring(0, s.Length - 2) + "]";
        }
    }
}