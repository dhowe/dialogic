using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dialogic
{
    public class Chat : Command
    {
        public List<Command> commands = new List<Command>(); // not copied

        public Chat()
        {
            this.Text = "C" + Environment.TickCount;
        }

        public void AddCommand(Command c)
        {
            this.commands.Add(c);
        }

        public override void Init(params string[] args)
        {

            if (args.Length < 1)
            {
                throw BadArgs(args, 1);
            }

            this.Text = args[0];

            if (Regex.IsMatch(Text, @"\s+"))
            {
                throw BadArg("CHAT name '" + Text + "' contains spaces!");
            }
        }

        // public override Command Copy() // ignore 'commands'

        public string ToTree()
        {
            string s = base.ToString() + "\n";
            commands.ForEach(c => s += "  " + c + "\n");
            return s;
        }
    }
}
