using System;
using System.Threading;
using Out = System.Console;

namespace Dialogic {
    public class ConsoleClient // An example client
    {
        public delegate void UnityEventHandler(MockUnityEvent e);
        public event UnityEventHandler UnityEvents; // event-stream

        protected string suffix = "";

        public ConsoleClient() {
            Thread t = new Thread(MockEvents) { IsBackground = true };
            t.Start();
        }

        public void Subscribe(ChatRuntime cs) {
            cs.ChatEvents += new ChatRuntime.ChatEventHandler(OnChatEvent);
        }

        private void OnChatEvent(ChatRuntime cm, ChatEvent e) {
            HandleCommand(cm, e);
        }

        // =================== Implementations below =====================

        private void HandleCommand(ChatRuntime cm, ChatEvent e) {
            Command c = e.Command;

            if (c is Do || c is Chat || c is Meta) // just info in this context
            {
                suffix += "  [" + c.TypeName() + ": " + c.Text + "]";
            } else if (c is Ask) {
                Ask a = ((Ask) c);
                string sec = a.WaitSecs > 0 ? " #" + a.WaitSecs + "s" : "";
                Out.WriteLine(c.Text + sec + suffix);
                suffix = "";
                cm.Do(Prompt(a));
            } else if (!(c is Wait || c is Opt || c is Go || c is Set)) {
                Out.WriteLine(c.Text + suffix);
                suffix = "";
            }
        }

        private Command Prompt(Ask a) {
            var opts = a.Options();

            // Print the possible options
            for (int i = 0; i < opts.Count; i++) {
                Out.WriteLine("(" + (i + 1) + ") " + opts[i].Text +
                    " => [" + opts[i].ActionText() + "]");
            }

            Command next = null;
            do // And prompt the user for a choice
            {
                try {
                    next = a.Choose(ConsoleReader.ReadLine(a, a.WaitTime()));
                } catch (PromptTimeout) {
                    Out.WriteLine("\nHey! Anyone home?");
                } catch (InvalidChoice) { }

                Out.WriteLine("Choose an option from 1-" + opts.Count + "\n");

            } while (next == null);

            // Print the selected option
            Out.WriteLine("    (selected Opt#" + (a.SelectedIdx + 1) +
                " => [" + a.Selected().ActionText() + "])\n");

            return next;
        }

        private void MockEvents() {
            while (true) {
                int ms = new Random().Next(20000, 50000);
                Thread.Sleep(ms);
                if (UnityEvents != null) {
                    UnityEvents.Invoke(new MockUnityEvent());
                }
            }
        }
    }

    public static class ConsoleReader //@cond hidden
    {
        private static Thread inputThread;
        private static AutoResetEvent getInput, gotInput;
        private static string input;

        static ConsoleReader() {
            getInput = new AutoResetEvent(false);
            gotInput = new AutoResetEvent(false);
            inputThread = new Thread(Reader) { IsBackground = true };
            inputThread.Start();
        }

        private static void Reader() {
            while (true) {
                getInput.WaitOne();
                input = Console.ReadKey(true).KeyChar.ToString();
                gotInput.Set();
            }
        }

        // omit the parameter to read a line without a timeout
        public static string ReadLine(Object source, int timeOutMillisecs = -1) {
            getInput.Set();
            bool success = gotInput.WaitOne(timeOutMillisecs);
            if (success) return input;
            throw new Exception();
        }

        
    } //@endcond

}