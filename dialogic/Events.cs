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

    public interface IChoice
    {
        int GetChoiceIndex();
    }

    public class ChoiceEvent : EventArgs, IChoice
    {
        protected int choiceIndex;

        public ChoiceEvent(int option)
        {
            this.choiceIndex = option;
        }

        public override string ToString()
        {
            return "Choice: " +choiceIndex;
        }

        public int GetChoiceIndex()
        {
            return choiceIndex;
        }
    }
}