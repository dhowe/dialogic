using System;

namespace Dialogic
{
    public class ChatEvent : EventArgs
    {
        protected Command command;

        public ChatEvent(Command c)
        {
            this.command = c;
        }

        public Command Command
        {
            get
            {
                return command;
            }
        }
    }

    public class ClientEvent : EventArgs
    {
        protected string message;

        internal ClientEvent() : this("User-Taps-Glass") { }

        public ClientEvent(string s)
        {
            this.message = s;
        }

        public override string ToString()
        {
            return this.message;
        }

        public string Message
        {
            get
            {
                return message;
            }
        }
    }

    public interface IChosen
    {
        Opt GetChosen();
    }

    public class ChosenEvent : EventArgs, IChosen
    {
        protected Opt option;

        public ChosenEvent(Opt option)
        {
            this.option = option;
        }

        public override string ToString()
        {
            return "Response: " + option.Text;
        }

        public Opt GetChosen()
        {
            return option;
        }
    }
}