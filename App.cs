using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace Dialogic {

    public abstract class DialogRunner {

        static void Main(string[] args) {

            (new Dialog())
                .Say("Welcome to my tank...")
                .Pause(500)
                .Label("Prompt1")
                .Ask("Do you want to play?",
                    Branch("yes", "Prompt2"),
                    Branch("no", "Prompt1"))
                .Label("Prompt2")
                .Ask("Ok, do you want to go first?",
                    Branch("yes", "Game"),
                    Branch("no", "Prompt2"))
                .Label("Game")
                .Say("Ok, let's play!...")
                .Run();

            /* TODO
            .Ask("Do you want to play?",
                if("yes", "Prompt2"),
                if("no", "Prompt1"))
                OR
            .Ask("Do you want to play?", if("yes", "Prompt2"), if("no", "Prompt1")) */
        }

        protected Dialog dialog;
        public int cursor;
        public DialogRunner(Dialog d) {
            this.dialog = d;
        }
        public abstract void OnChoice(string input);

        public int GotoLabel(string label) {
            for (int i = 0; i < dialog.events.Count; i++) {
                Command evt = dialog.events[i];
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

        public static KeyValuePair<string, string> Branch(string v1, string v2) {
            return new KeyValuePair<string, string>(v1, v2);
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

    static class L {

        public static void g<T>(T o) {
            if (o == null) Console.WriteLine();
            if (o is string) Console.WriteLine(o);
            // if (o is string[]) {
            //     o (string[]) ((object) o);
            //     string ss = "";
            //     for (int i = 0; i < s.Length; i++)
            //         if (i<s.length-1) ss += s[i] + ",";
            // }
            else if (o is IEnumerable) {
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