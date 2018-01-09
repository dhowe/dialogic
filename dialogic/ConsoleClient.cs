using Out = System.Console;

namespace Dialogic
{
    public class ConsoleClient// : IChatListener
    {
        private string suffix = "";

        public void Subscribe(ChatExecutor cs)
        {
            cs.Events += new ChatExecutor.ChatEventHandler(OnChatEvent);
        }

        private void OnChatEvent(ChatExecutor cs, ChatEventArgs e)
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
                //cs.OnClientEvent(new ClientEventArgs(this, Prompt(a)));
                //cs.OnUIEvent(new ClientEventArgs(this, Prompt(a)));
                cs.Do(Prompt(a));
            }
        }

        private Command Prompt(Ask a)
        {
            var opts = a.Options();

            for (int i = 0; i < opts.Count; i++)
            {
                Out.WriteLine("(" + (i + 1) + ") " + opts[i].Text
                    + " => [" + opts[i].ActionText() + "]");
            }

            Command next = a.Choose(Out.ReadKey(true).KeyChar.ToString());
            while (next == null)
            {
                Out.WriteLine("\nChoose an option from 1-" + opts.Count + "\n");
                next = a.Choose(Out.ReadKey(true).KeyChar.ToString());
            }

            Out.WriteLine("    (selected Opt#" + (a.SelectedIdx + 1)
                + " => [" + a.Selected().ActionText() + "]\n");

            return next;
        }
    }
}