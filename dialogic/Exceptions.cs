using System;
using System.Runtime.Serialization;

namespace Dialogic
{
    public class ChatException : Exception
    {
        readonly Command command;

        public ChatException(Command ask) : this(ask, "") {}

        public ChatException(Command ask, string message) : base(message)
        {
            this.command = ask;
        }

        public Command Command {
            get
            {
                return command;
            }
        }
    }

    public class ChatNotFound : ChatException 
    {
        public ChatNotFound(string label)
            : base(null, "No CHAT exists with name '" + label + "'") { }
    }

    public class PromptTimeout : ChatException
    {
        public PromptTimeout(Command ask) : this(ask, "Ask timeout expired") { }
       
        public PromptTimeout(Command ask, string message) : base(ask, message) {}
    }

    public class InvalidChoice : ChatException
    {
        public InvalidChoice(Command ask) : this(ask, "Invalid selection") { }

        public InvalidChoice(Command ask, string message) : base(ask, message) { }
    }

    public class OperatorException : Exception
    {
        public OperatorException(Operator o) : this(o, "Invalid Operator") { }

        public OperatorException(Operator o, string message) : base(message+": "+o) { }
    }
}