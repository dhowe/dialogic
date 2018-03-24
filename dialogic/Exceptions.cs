using System;

namespace Dialogic
{
    public class DialogicException : Exception
    {
        public DialogicException(string msg = "") : base(msg) { }
    }

    public class RealizeException : DialogicException
    {
        public RealizeException(string msg = "") : base(msg) { }
    }

    public class FindException : DialogicException
    {
        public FindException(string msg) : base(msg) { }

        public FindException(Command finder, string msg = "Find failed")
            : base(msg + " " + finder.GetMeta()) { }
    }

    public class OperatorException : DialogicException
    {
        public OperatorException(Operator o) : this(o, "Invalid Operator") { }

        public OperatorException(Operator o, Exception e)
            : base(e.Message + ": " + o) { }

        public OperatorException(Operator o, string message)
            : base(message + ": " + o) { }
    }

    public class ParseException : DialogicException
    {
        public readonly int lineNumber;
        public readonly string lineContents;
        public readonly string reason;

        public ParseException(string msg = "") : base(msg) { }

        public ParseException(string line, int lineNo, string msg = "")
            : base("Line " + lineNo + " :: " + line +
                (msg.Length > 0 ? "\n\n" + msg : ""))
        {
            lineNumber = lineNo;
            lineContents = line;
            reason = msg;
        }
    }

    public class RuntimeParseException : ParseException
    {
        public RuntimeParseException(ParseException pe) : base(pe.Message) { }
    }
}