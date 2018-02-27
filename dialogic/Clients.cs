using System;
using System.Threading;
using Out = System.Console;

namespace Dialogic
{
    /** 
     * A basic parent class for clients 
     */
    public abstract class AbstractClient
    {
        //public delegate void UnityEventHandler(EventArgs e);
        //public event UnityEventHandler UnityEvents;

        protected abstract void OnChatEvent(ChatEvent e);

        public void Subscribe(ChatRuntime cs)
        {
            //cs.ChatEvents += new ChatRuntime.ChatEventHandler(OnChatEvent);
        }

        protected void Fire(EventArgs e)
        {
           // if (UnityEvents != null) UnityEvents.Invoke(e); else Out.WriteLine("NO UNITY EVENTS!!!!");
        }
    }

    /** 
     * An example client providing IO via a terminal console
     */
    public class ConsoleClient : AbstractClient
    {
        protected string suffix = "";

        public ConsoleClient()
        {
            Thread t = new Thread(MockEvents) { IsBackground = true };
            //t.Start(); // to add some mock random client events
        }

        protected override void OnChatEvent(ChatEvent e)
        {
            Command c = e.Command();

            if (c is IEmittable)
            {
                Out.WriteLine(c is Do ? "(Do:" + c.Text + ") " : c.Text);
                if (c is Ask) Prompt((Dialogic.Ask)c);
            }
        }

        private void Prompt(Ask a)
        {
            var opts = a.Options();

            // Display the possible options
            for (int i = 0; i < opts.Count; i++)
            {
                Out.WriteLine("(" + (i + 1) + ") " + opts[i].Text
                    + " => [" + opts[i].ActionText() + "]");
            }

            Opt chosen = null;
            do
            {
                // And prompt the user for their choice
                try
                {
                    string res = ConsoleReader.ReadLine(a, a.PauseAfterMs);
                    int i = -1;
                    try
                    {
                        i = Convert.ToInt32(res);
                    }
                    catch { /* ignore */ }

                    chosen = a.Selected(--i);
                }
                catch (Exception e)
                {
                    if (e is PromptTimeout) Out.WriteLine("\nHey! Anyone home?");
                    Out.WriteLine("Choose an option from 1-" + opts.Count + "\n");
                }

            } while (chosen == null);

            // Print the selected option
            Out.WriteLine("    (Opt#" + (a.SelectedIdx + 1 + " selected")
                + " => [" + a.Selected().ActionText() + "])\n");

            Fire(new ChoiceEvent(a.SelectedIdx));
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
