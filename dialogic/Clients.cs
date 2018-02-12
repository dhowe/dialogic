using System;
using System.Threading;
using Out = System.Console;

namespace Dialogic
{
    /** A very simple client: receives events but doesn't respond */
    class SimpleClient : AbstractClient
    {
        protected override void OnChatEvent(ChatRuntime cm, ChatEvent e)
        {
            Command cmd = e.Command;
            Console.WriteLine(cmd);
        }
    }

    /** A basic parent class for clients */
    public abstract class AbstractClient
    {
        public delegate void UnityEventHandler(EventArgs e);
        public event UnityEventHandler UnityEvents;

        protected abstract void OnChatEvent(ChatRuntime cm, ChatEvent e);

        public void Subscribe(ChatRuntime cs)
        {
            cs.ChatEvents += new ChatRuntime.ChatEventHandler(OnChatEvent);
        }

        protected void Fire(EventArgs e)
        {
            if (UnityEvents != null) UnityEvents.Invoke(e);
        }
    }

    /** An example client that uses the console */
    public class ConsoleClient : AbstractClient
    {
        protected string suffix = "";

        public ConsoleClient()
        {
            Thread t = new Thread(MockEvents) { IsBackground = true };
            //t.Start(); // to add some mock random client events
        }

        protected override void OnChatEvent(ChatRuntime cm, ChatEvent e)
        {
            HandleCommand(cm, e);
        }

        // =================== Implementations below =====================

        private void HandleCommand(ChatRuntime cm, ChatEvent e)
        {
            Command c = e.Command;

            if (c is Say)
            {
                Out.WriteLine(c.Text);
            }
            else if (c is Do)
            {
                Out.WriteLine("(DO: " + c.Text + ")");
            }
            else if (c is Ask) //  prompt the user
            {
                Ask a = ((Ask)c);
                string sec = a.WaitSecs > 0 ? " (" + a.WaitSecs + "s)" : "";
                Out.WriteLine(c.Text + sec);
                Prompt(a);
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
            do
            {
                // And prompt the user for their choice
                try
                {
                    next = a.Choose(ConsoleReader.ReadLine(a, a.WaitTime()));
                }
                catch (Exception e)
                {
                    if (e is PromptTimeout) Out.WriteLine("\nHey! Anyone home?");
                    Out.WriteLine("Choose an option from 1-" + opts.Count + "\n");
                }

            } while (next == null);

            // Print the selected option
            Out.WriteLine("    (Opt#" + (a.SelectedIdx + 1 + " selected")
                + " => [" + a.Selected().ActionText() + "])\n");

            Fire(new ChosenEvent(a.Selected()));

            return next;
        }

        protected void MockEvents()
        {
            while (true)
            {
                int ms = new Random().Next(2000, 5000);
                Thread.Sleep(ms);
                Fire(new ClientEvent());
            }
        }
    }
}