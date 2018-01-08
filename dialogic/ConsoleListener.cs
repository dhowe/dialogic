using Out = System.Console;

namespace Dialogic
{
    public class ConsoleListener : IChatListener
    {
        private string suffix = "";

        void IChatListener.onChatEvent(ChatScheduler cs, Command c)
        {
            if (c is Do || c is Chat)
            {
                suffix += "\t[" + c.TypeName() + ": " + c.Text + "]";
            }
            else if (!(c is Wait || c is Opt || c is Go))
            {
                Out.WriteLine(c.Text + suffix);
                suffix = "";
            }

            if (c is Ask a) cs.Do(DoPrompt(a));
        }

        private Command DoPrompt(Ask a)
        {
            var opts = a.Options();
            //.WriteLine($"Opts for {a.Text} = {opts}");
            //opts.ForEach((obj) => Out.WriteLine($"opt={obj.Text}"));
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