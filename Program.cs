using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace dialogic {

    public class Program {
        static void Main(string[] args) {

            Dialog d = new Dialog();
            d.Say("Welcome to my tank...");
            d.Pause(500);
            d.Ask("Do you want to play?", "yes", "no");

            ConsoleRunner.Play(d);
        }
    }

    public class Dialog {

        public string name;
        public List<ScriptEvent> actions;

        public Dialog() : this("start") { }

        public Dialog(string name) {
            this.name = name;
            actions = new List<ScriptEvent>();
        }

        public Dialog Say(string text) {
            actions.Add(new Say(text));
            return this;
        }

        public Dialog Wait() {
            actions.Add(new Wait());
            return this;
        }

        public Dialog Pause(int ms) {
            actions.Add(new Wait(ms));
            return this;
        }

        public Dialog Ask(string prompt, params string[] choices) {
            actions.Add(new Choice(prompt, choices));
            // actions.Add(new Wait());
            return this;
        }

        public DialogRunner Run() {
            return new ConsoleRunner(this).Run();
        }
    }

    public class Choice : ScriptEvent {

        //private Say prompt;
        public string[] choices;
        public int selected;

        public Choice(string prompt, params string[] options) {
            this.text = prompt;
            this.choices = options;
        }
        public override int Run() {
            base.Run();
            for (int i = 0; i < choices.Length; i++) {
                Console.WriteLine("  " + (i + 1) + ") " + choices[i]);
            }
            return -1;
        }
        public bool validate(string input) {
            int i;
            if (int.TryParse(input, out i)) {
                if (i > 0 && i <= choices.Length) {
                    selected = --i;
                    return true;
                }
            }
            return false;
        }

    }

    public abstract class ScriptEvent {
        internal static int IDGEN = 0;

        public int id = ++IDGEN;
        public string actor = "Guppy", text = "";

        public virtual int Run() {
            Console.WriteLine(this.actor + ": " + this.text);
            return 0;
        } // return -1=pause, 0=continue, #=pauseFor
    }

    public class Say : ScriptEvent {

        public Say(string text) {
            this.text = text;
        }

        public Say(string actor, string text) {
            this.actor = actor;
            this.text = text;
        }

        public override int Run() {
            Console.WriteLine(this.actor + ": " + this.text);
            return 0;
        }
    }

    public class Wait : ScriptEvent {
        private int millis;

        public Wait() : this(-1) { }

        public Wait(int ms) {
            millis = ms;
        }
        public override int Run() {
            return millis;
        }
    }

    public class ConsoleRunner : DialogRunner {
        private int cursor;

        public ConsoleRunner(Dialog d) : base(d) { }

        public static void Play(Dialog d) {
            new ConsoleRunner(d).Run();
        }

        public override void OnChoice(string input) {
            Choice last = currentChoice();
            if (last.validate(input)) {
                Console.WriteLine("(" + last.id + ": " + last.text + " -> " + last.choices[last.selected] + ")");
            } else {
                Console.WriteLine("BAD INPUT: " + input + ", Expecting # 1-" + last.choices.Length);
                this.step(-1);
            }
        }

        private void step(int steps) {
            cursor += steps;
        }

        private Choice currentChoice() {
            for (var i = cursor; i >= 0; i--) {
                if (dialog.actions[i] is Choice) {
                    return (Choice) dialog.actions[i];
                }
            }
            return null;
        }

        public override DialogRunner Run() {
            int rc = -1;
            for (cursor = 0; cursor < dialog.actions.Count; cursor++) {
                //foreach (var action in ) {
                var action = dialog.actions[cursor];
                rc = action.Run();
                if (rc >= 0) {
                    Thread.Sleep(rc);
                } else {
                    this.OnChoice(Console.ReadKey(true).KeyChar.ToString());
                }
            }
            Console.WriteLine("Dialog Complete");
            return this;
        }
    } 

    public abstract class DialogRunner {

        public Dialog dialog;

        public DialogRunner(Dialog d) {
            this.dialog = d;
        }

        public abstract void OnChoice(string input);

        public abstract DialogRunner Run();
    }

}