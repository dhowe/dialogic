using System;

namespace Dialogic
{
    public class ChatEventArgs : EventArgs
    {
        protected Command command;

        public ChatEventArgs(Command c)
        {
            this.command = c;
        }

        public Command Command
        {
            get => command;
        }
    }
}
