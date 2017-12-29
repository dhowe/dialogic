using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;

namespace Dialogic {

    public abstract class Command {

        internal static int IDGEN = 0;

        public int id = ++IDGEN;

        public string actor = "Guppy";

        public string text = "";

        protected Dialog dialog;

        public virtual int Fire() {
            Console.WriteLine(this.actor + ": " + cleanString(this.text));
            return 0;
        } // return -1=pause, 0=continue, #=pauseFor#

        public virtual string cleanString(string text) {
            var str = text;
            str = new Regex("^ *'").Replace(str, "");
            str = new Regex("' *$").Replace(str, "");
            return str;
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
        public string[] options;
        public int selected, attempts = 0;
        public Action[] reactions;

        public Ask(Dialog d, string prompt, string[] options, Action[] reactions) : base(d, prompt) {
            this.options = options;
            this.reactions = reactions;
        }

        public Ask(Dialog d, string prompt, params KeyValuePair<string, string>[] pairs) : base(d, prompt) {
            this.options = new string[pairs.Length];
            this.reactions = new Action[pairs.Length];
            for (int i = 0; i < pairs.Length; i++) {
                this.options[i] = pairs[i].Key;
                string label = pairs[i].Value;
                reactions[i] = (() => { new Gotu(d, label).Fire(); });
            }
        }

        public Ask(Dialog d, string prompt, params string[] options) : this(d, prompt, options, null) { }

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

            if (this.reactions == null || this.reactions.Length < 1) {
                return defaultReaction(input);
            }

            int i;
            attempts++;
            if (int.TryParse(input, out i)) {
                if (i > 0 && i <= options.Length) {
                    selected = --i;
                    this.React();
                    return true;
                }
            }
            if (dialog.runtime is ConsoleRunner) { // tmp
                Console.WriteLine("\n" + actor + ": Please select a # from 1-" + options.Length + "\n");
            }
            return false;
        }

        private bool defaultReaction(string input) {
            if (input == "y" || input == "Y") {
                return true;
            }
            if (dialog.runtime is ConsoleRunner) { // tmp
                Console.WriteLine("\n" + actor + ": Sorry, I don't understand '" + input + "'\n");
            }
            return false;
        }

        public override string ToString() => "Ask " + text; // TODO
    }

}