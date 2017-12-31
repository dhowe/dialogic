using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace Dialogic {

    public abstract class DialogRunner {

        protected Dialog dialog;

        public int cursor;

        public DialogRunner(Dialog d) {
            this.dialog = d;
        }

        public abstract void OnChoice(string input);

        public int GotoChat(string chat) {
            //Console.WriteLine("GotoChat: " + chat);
            for (int i = 0; i < dialog.events.Count; i++) {
                Command evt = dialog.events[i];
                if (evt is Chat && evt.text == chat) {
                    //Console.WriteLine(" -> " + chat);
                    return this.Cursor(i);
                }
            }
            throw new Exception("Chat '" + chat + "' not found!");
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

    // //////////////////////////////////////////////////////////////////////

    public class ConsoleRunner : DialogRunner {

        public ConsoleRunner(Dialog d) : base(d) { }

        public static void Play(Dialog d) {
            new ConsoleRunner(d).Run();
        }

        public override void OnChoice(string input) {
            //Console.WriteLine("OnChoice: "+input);
            Ask last = CurrentChoice();
            if (!last.Accept(input)) {
                this.Step(-1);
            }
        }

        public Ask CurrentChoice() {
            for (var i = cursor; i >= 0; i--) {
                if (dialog.events[i] is Ask) {
                    return (Ask) dialog.events[i];
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