using System;
using System.Runtime.Serialization;

namespace Dialogic
{
    public class ChatException : Exception
    {
        readonly Command command;

        public ChatException(Command c) : this(c, "") { }

        public ChatException(Command c, string msg) : base(msg+" :: "+c)
        {
            this.command = c;
        }

        public Command Command()
        {
            return command;
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

        public PromptTimeout(Command ask, string message) : base(ask, message) { }
    }

    public class OperatorException : Exception
    {
        public OperatorException(Operator o) : this(o, "Invalid Operator") { }

        public OperatorException(Operator o, string message) : base(message + ": " + o) { }
    }

    public class ParseException : Exception
    {
        public ParseException(string msg = "") : base(msg) { }

        public ParseException(string line, int lineNo, string msg = "")
            : base("Line " + lineNo + " : " + line + 
                   (msg.Length > 0 ? "\n\n" + msg : "")) { }
    }
}