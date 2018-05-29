using System;
using System.Collections.Generic;

namespace Dialogic
{
	//@cond hidden

	public class DialogicException : Exception
	{
        public DialogicException(string msg = "", 
            Exception e = null) : base(msg, e) { }
	}

	public class BindException : DialogicException
	{
		public BindException(string msg = "") : base(msg) { }
	}

	public class MismatchedParens : DialogicException
	{
		public MismatchedParens(string msg = "") : base(msg) { }
	}

	public class TransformException : DialogicException
	{
		public TransformException(string msg = "") : base(msg) { }
	}

	public class UnboundFunction : BindException
	{
		public UnboundFunction(string name, string tname = null) :
			base(GetMessage(name, tname, null, ""))
		{ }

		public UnboundFunction(string name, string tname = null,
			Type[] args = null, string msg = "") :
				base(GetMessage(name, tname, args, msg))
		{ }

		internal static string GetMessage(string name, string tname,
			Type[] args, string msg = "")
		{
			var s = (tname != null ? tname + "." + name : name) + "(";
			if (!args.IsNullOrEmpty()) s += args.Stringify();
			return s + ") " + msg;
		}
	}

	public class UnboundSymbol : BindException
	{
		public UnboundSymbol(string symbol, Chat context,
			IDictionary<string, object> globals, string msg = "") :
			base(GetMessage(symbol, context, globals, msg))
		{ }

		internal UnboundSymbol(Symbol symbol, Chat context,
			IDictionary<string, object> globals, string msg = "") :
			base(GetMessage(symbol.Name(), context, globals, msg))
		{ }

		private static string GetMessage(string s, Chat c, 
            IDictionary<string, object> g, string msg)
		{
			return s + "\n  globals: " + g.Stringify() + (c != null ? 
                "\n  chat#" + c.text + ":" + c.scope.Stringify() : 
                "" + (msg.IsNullOrEmpty() ? "" : "\n" + msg));
		}
	}

	public class FindException : DialogicException
	{
		public FindException(string msg) : base(msg) { }

		public FindException(Command finder, string msg = "Find failed")
			: base(msg + " " + finder.meta.Stringify()) { }
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

	//@endcond
}