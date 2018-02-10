using System;
using System.Threading;
using Out = System.Console;

namespace Dialogic.Client
{
    public class ConsoleClient : ChatClient // An example client
    {
        protected string suffix = "";

        public ConsoleClient()
        {
            Thread t = new Thread(MockEvents) { IsBackground = true };
            //t.Start();
        }

        protected override void OnChatEvent(ChatRuntime cm, ChatEvent e)
        {
            HandleCommand(cm, e);
        }

        // =================== Implementations below =====================

        private void HandleCommand(ChatRuntime cm, ChatEvent e)
        {
            //Out.WriteLine("Client<="+e.Command.Text);
            Command c = e.Command;

            if (c is Do || c is Chat || c is Meta) // just print info
            {
                suffix += "  [" + c.TypeName() + ": " + c.Text + "]";
            }
            else if (c is Ask) //  prompt the user
            {
                Ask a = ((Ask)c);
                string sec = a.WaitSecs > 0 ? " #" + a.WaitSecs + "s" : "";
                Out.WriteLine(c.Text + sec + suffix);
                suffix = "";
                Prompt(a);
                //cm.Do(Prompt(a));
            }
            else if (!(c is Wait || c is Opt || c is Go || c is Set))
            {
                Out.WriteLine(c.Text + suffix);
                suffix = "";
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
                    next = a.Choose(ConsoleReader.ReadLine(a, a.WaitTime()));
                }
                catch (Exception e)
                {
                    // TODO: remove PromptTimout
                    if (e is PromptTimeout) Out.WriteLine("\nHey! Anyone home?");
                    Out.WriteLine("Choose an option from 1-" + opts.Count + "\n");
                }

            } while (next == null);

            // Print the selected option
            Out.WriteLine("    (Opt#" + (a.SelectedIdx + 1+" selected")
                + " => [" + a.Selected().ActionText() + "])\n");

            Fire(new ResponseEvent(a.Selected()));

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