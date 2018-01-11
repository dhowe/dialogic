using System;
using System.Threading;
using Out = System.Console;

namespace Dialogic
{
    public class ConsoleClient // and example client
    {
        public delegate void UnityEventHandler(MockUnityEvent e);
        public event UnityEventHandler UnityEvents;

        private string suffix = "";

        public ConsoleClient() {

            new Thread(delegate ()
            {
                while (true)
                {
                    int ms = new Random().Next(5000, 10000);
                    System.Console.WriteLine($"SleepFor: {ms}");
                    Thread.Sleep(ms);
                    UnityEvents?.Invoke(new MockUnityEvent());
                }

            }).Start();
        }

        public void Subscribe(ChatExecutor cs)
        {
            cs.ChatEvents += new ChatExecutor.ChatEventHandler(OnChatEvent);
        }

        private void OnChatEvent(ChatExecutor cs, ChatEvent e)
        {
            Command c = e.Command;

            if (c is Do || c is Chat)
            {
                suffix += "\t[" + c.TypeName() + ": " + c.Text + "]";
            }
            else if (!(c is Wait || c is Opt || c is Go))
            {
                Out.WriteLine(c.Text + suffix);
                suffix = "";
            }

            if (c is Ask a)
            {
                cs.Do(Prompt(a));
            }
        }

        private Command Prompt(Ask a)
        {
            var opts = a.Options();

            // Print the possible options
            for (int i = 0; i < opts.Count; i++)
            {
                Out.WriteLine("(" + (i + 1) + ") " + opts[i].Text
                    + " => [" + opts[i].ActionText() + "]");
            }

            Command next = null;
            do // And prompt the user for a choice
            {
                try
                {
                    next = a.Choose(ConsoleReader.ReadLine(a, a.Millis()));
                }
                catch (PromptTimeout)
                {
                    Out.WriteLine("Hey! Anyone home?");
                }
                catch (InvalidChoice) { }

                Out.WriteLine("\nChoose an option from 1-" + opts.Count + "\n");

            } while (next == null);

            // Print the selected option
            Out.WriteLine("    (selected Opt#" + (a.SelectedIdx + 1)
                + " => [" + a.Selected().ActionText() + "]\n");

            return next;
        }

    }
}