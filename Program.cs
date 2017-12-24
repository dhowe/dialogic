using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using dialogic;

namespace dialogic {

    public class Program {
        static void Main(string[] args) {

            Dialog d = new Dialog();
            d.Say("Welcome to my tank...");
            d.Pause(500);
            d.Say("Do you want to play?");
            d.Ask("yes", "no");

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

        public Dialog Ask(params string[] choices) {
            actions.Add(new Choice(choices));
            // actions.Add(new Wait());
            return this;
        }

        public DialogRunner Run() {
            return new ConsoleRunner(this).Run();
        }
    }

    public class Choice : ScriptEvent {
        private static int IDGEN = 0;

        //private Say prompt;
        private string[] choices;
        private int id = ++IDGEN;

        public Choice(params string[] options) {
            this.choices = options;
        }
        public override int Run() {
            for (int i = 0; i < choices.Length; i++) {
                Console.WriteLine("  " + (i + 1) + ") " + choices[i]);
            }
            return -1;
        }
    }

    public abstract class ScriptEvent {
        public string text = "";

        public abstract int Run(); // return -1=pause, 0=continue, #=pauseFor
    }

    public class Say : ScriptEvent {
        private string speaker;

        public Say(string text) {
            this.speaker = "Guppy";
            this.text = text;
        }

        public Say(string speaker, string text) {
            this.speaker = speaker;
            this.text = text;
        }

        public override int Run() {
            Console.WriteLine(this.speaker + ": " + this.text);
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

        public override void OnChoice(int choiceIndex) {
            Console.WriteLine(lastSay().text + ": " + choiceIndex);
            //this.thread.Interrupt();
        }

        private ScriptEvent lastSay()
        {
            for (var i = cursor; i >= 0; i--) {
                if (dialog.actions[i] is Say) {
                    return dialog.actions[i];
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
                    while (rc <= 0) {
                        ConsoleKeyInfo cki = Console.ReadKey(true);
                        if (int.TryParse(cki.KeyChar.ToString(), out rc)) {
                            this.OnChoice(rc);
                            break;
                        } else {
                            Console.WriteLine("???");
                            rc = -1;
                        }
                    }
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

        public abstract void OnChoice(int choiceIndex);

        public abstract DialogRunner Run();
    }

}