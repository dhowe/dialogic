﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.4
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from /Users/dhowe/git/dialogic/dialogic/Dialogic.g4 by ANTLR 4.6.4

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace Dialogic {
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System.Collections.Generic;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.4")]
public partial class DialogicParser : Parser {
	public const int
		COMMAND=1, SPACE=2, DELIM=3, NEWLINE=4, WORD=5, ERROR=6;
	public const int
		RULE_script = 0, RULE_line = 1, RULE_command = 2, RULE_args = 3, RULE_arg = 4;
	public static readonly string[] ruleNames = {
		"script", "line", "command", "args", "arg"
	};

	private static readonly string[] _LiteralNames = {
	};
	private static readonly string[] _SymbolicNames = {
		null, "COMMAND", "SPACE", "DELIM", "NEWLINE", "WORD", "ERROR"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[System.Obsolete("Use Vocabulary instead.")]
	public static readonly string[] tokenNames = GenerateTokenNames(DefaultVocabulary, _SymbolicNames.Length);

	private static string[] GenerateTokenNames(IVocabulary vocabulary, int length) {
		string[] tokenNames = new string[length];
		for (int i = 0; i < tokenNames.Length; i++) {
			tokenNames[i] = vocabulary.GetLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = vocabulary.GetSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}

		return tokenNames;
	}


	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "Dialogic.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	public DialogicParser(ITokenStream input)
		: base(input)
	{
		_interp = new ParserATNSimulator(this,_ATN);
	}
	public partial class ScriptContext : ParserRuleContext {
		public LineContext[] line() {
			return GetRuleContexts<LineContext>();
		}
		public LineContext line(int i) {
			return GetRuleContext<LineContext>(i);
		}
		public ScriptContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_script; } }
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IDialogicVisitor<TResult> typedVisitor = visitor as IDialogicVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitScript(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ScriptContext script() {
		ScriptContext _localctx = new ScriptContext(_ctx, State);
		EnterRule(_localctx, 0, RULE_script);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 11;
			_errHandler.Sync(this);
			_la = _input.La(1);
			do {
				{
				{
				State = 10; line();
				}
				}
				State = 13;
				_errHandler.Sync(this);
				_la = _input.La(1);
			} while ( _la==COMMAND );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class LineContext : ParserRuleContext {
		public ITerminalNode NEWLINE() { return GetToken(DialogicParser.NEWLINE, 0); }
		public ITerminalNode Eof() { return GetToken(DialogicParser.Eof, 0); }
		public CommandContext command() {
			return GetRuleContext<CommandContext>(0);
		}
		public ArgsContext args() {
			return GetRuleContext<ArgsContext>(0);
		}
		public ITerminalNode[] SPACE() { return GetTokens(DialogicParser.SPACE); }
		public ITerminalNode SPACE(int i) {
			return GetToken(DialogicParser.SPACE, i);
		}
		public LineContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_line; } }
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IDialogicVisitor<TResult> typedVisitor = visitor as IDialogicVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitLine(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public LineContext line() {
		LineContext _localctx = new LineContext(_ctx, State);
		EnterRule(_localctx, 2, RULE_line);
		int _la;
		try {
			int _alt;
			EnterOuterAlt(_localctx, 1);
			{
			State = 30;
			_errHandler.Sync(this);
			switch ( Interpreter.AdaptivePredict(_input,3,_ctx) ) {
			case 1:
				{
				State = 15; command();
				State = 19;
				_errHandler.Sync(this);
				_alt = Interpreter.AdaptivePredict(_input,1,_ctx);
				while ( _alt!=2 && _alt!=global::Antlr4.Runtime.Atn.ATN.InvalidAltNumber ) {
					if ( _alt==1 ) {
						{
						{
						State = 16; Match(SPACE);
						}
						} 
					}
					State = 21;
					_errHandler.Sync(this);
					_alt = Interpreter.AdaptivePredict(_input,1,_ctx);
				}
				}
				break;

			case 2:
				{
				State = 22; command();
				State = 24;
				_errHandler.Sync(this);
				_la = _input.La(1);
				do {
					{
					{
					State = 23; Match(SPACE);
					}
					}
					State = 26;
					_errHandler.Sync(this);
					_la = _input.La(1);
				} while ( _la==SPACE );
				State = 28; args();
				}
				break;
			}
			State = 35;
			_errHandler.Sync(this);
			_la = _input.La(1);
			while (_la==SPACE) {
				{
				{
				State = 32; Match(SPACE);
				}
				}
				State = 37;
				_errHandler.Sync(this);
				_la = _input.La(1);
			}
			State = 38;
			_la = _input.La(1);
			if ( !(_la==Eof || _la==NEWLINE) ) {
			_errHandler.RecoverInline(this);
			} else {
				if (_input.La(1) == TokenConstants.Eof) {
					matchedEOF = true;
				}

				_errHandler.ReportMatch(this);
				Consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class CommandContext : ParserRuleContext {
		public ITerminalNode COMMAND() { return GetToken(DialogicParser.COMMAND, 0); }
		public CommandContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_command; } }
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IDialogicVisitor<TResult> typedVisitor = visitor as IDialogicVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitCommand(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public CommandContext command() {
		CommandContext _localctx = new CommandContext(_ctx, State);
		EnterRule(_localctx, 4, RULE_command);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 40; Match(COMMAND);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ArgsContext : ParserRuleContext {
		public ArgContext[] arg() {
			return GetRuleContexts<ArgContext>();
		}
		public ArgContext arg(int i) {
			return GetRuleContext<ArgContext>(i);
		}
		public ITerminalNode[] DELIM() { return GetTokens(DialogicParser.DELIM); }
		public ITerminalNode DELIM(int i) {
			return GetToken(DialogicParser.DELIM, i);
		}
		public ArgsContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_args; } }
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IDialogicVisitor<TResult> typedVisitor = visitor as IDialogicVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitArgs(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ArgsContext args() {
		ArgsContext _localctx = new ArgsContext(_ctx, State);
		EnterRule(_localctx, 6, RULE_args);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 42; arg();
			State = 47;
			_errHandler.Sync(this);
			_la = _input.La(1);
			while (_la==DELIM) {
				{
				{
				State = 43; Match(DELIM);
				State = 44; arg();
				}
				}
				State = 49;
				_errHandler.Sync(this);
				_la = _input.La(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ArgContext : ParserRuleContext {
		public ITerminalNode[] WORD() { return GetTokens(DialogicParser.WORD); }
		public ITerminalNode WORD(int i) {
			return GetToken(DialogicParser.WORD, i);
		}
		public ITerminalNode[] SPACE() { return GetTokens(DialogicParser.SPACE); }
		public ITerminalNode SPACE(int i) {
			return GetToken(DialogicParser.SPACE, i);
		}
		public ArgContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_arg; } }
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IDialogicVisitor<TResult> typedVisitor = visitor as IDialogicVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitArg(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ArgContext arg() {
		ArgContext _localctx = new ArgContext(_ctx, State);
		EnterRule(_localctx, 8, RULE_arg);
		int _la;
		try {
			int _alt;
			EnterOuterAlt(_localctx, 1);
			{
			State = 50; Match(WORD);
			State = 59;
			_errHandler.Sync(this);
			_alt = Interpreter.AdaptivePredict(_input,7,_ctx);
			while ( _alt!=2 && _alt!=global::Antlr4.Runtime.Atn.ATN.InvalidAltNumber ) {
				if ( _alt==1 ) {
					{
					{
					State = 52;
					_errHandler.Sync(this);
					_la = _input.La(1);
					do {
						{
						{
						State = 51; Match(SPACE);
						}
						}
						State = 54;
						_errHandler.Sync(this);
						_la = _input.La(1);
					} while ( _la==SPACE );
					State = 56; Match(WORD);
					}
					} 
				}
				State = 61;
				_errHandler.Sync(this);
				_alt = Interpreter.AdaptivePredict(_input,7,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public static readonly string _serializedATN =
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x3\b\x41\x4\x2\t\x2"+
		"\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x3\x2\x6\x2\xE\n\x2\r\x2"+
		"\xE\x2\xF\x3\x3\x3\x3\a\x3\x14\n\x3\f\x3\xE\x3\x17\v\x3\x3\x3\x3\x3\x6"+
		"\x3\x1B\n\x3\r\x3\xE\x3\x1C\x3\x3\x3\x3\x5\x3!\n\x3\x3\x3\a\x3$\n\x3\f"+
		"\x3\xE\x3\'\v\x3\x3\x3\x3\x3\x3\x4\x3\x4\x3\x5\x3\x5\x3\x5\a\x5\x30\n"+
		"\x5\f\x5\xE\x5\x33\v\x5\x3\x6\x3\x6\x6\x6\x37\n\x6\r\x6\xE\x6\x38\x3\x6"+
		"\a\x6<\n\x6\f\x6\xE\x6?\v\x6\x3\x6\x2\x2\x2\a\x2\x2\x4\x2\x6\x2\b\x2\n"+
		"\x2\x2\x3\x3\x3\x6\x6\x43\x2\r\x3\x2\x2\x2\x4 \x3\x2\x2\x2\x6*\x3\x2\x2"+
		"\x2\b,\x3\x2\x2\x2\n\x34\x3\x2\x2\x2\f\xE\x5\x4\x3\x2\r\f\x3\x2\x2\x2"+
		"\xE\xF\x3\x2\x2\x2\xF\r\x3\x2\x2\x2\xF\x10\x3\x2\x2\x2\x10\x3\x3\x2\x2"+
		"\x2\x11\x15\x5\x6\x4\x2\x12\x14\a\x4\x2\x2\x13\x12\x3\x2\x2\x2\x14\x17"+
		"\x3\x2\x2\x2\x15\x13\x3\x2\x2\x2\x15\x16\x3\x2\x2\x2\x16!\x3\x2\x2\x2"+
		"\x17\x15\x3\x2\x2\x2\x18\x1A\x5\x6\x4\x2\x19\x1B\a\x4\x2\x2\x1A\x19\x3"+
		"\x2\x2\x2\x1B\x1C\x3\x2\x2\x2\x1C\x1A\x3\x2\x2\x2\x1C\x1D\x3\x2\x2\x2"+
		"\x1D\x1E\x3\x2\x2\x2\x1E\x1F\x5\b\x5\x2\x1F!\x3\x2\x2\x2 \x11\x3\x2\x2"+
		"\x2 \x18\x3\x2\x2\x2!%\x3\x2\x2\x2\"$\a\x4\x2\x2#\"\x3\x2\x2\x2$\'\x3"+
		"\x2\x2\x2%#\x3\x2\x2\x2%&\x3\x2\x2\x2&(\x3\x2\x2\x2\'%\x3\x2\x2\x2()\t"+
		"\x2\x2\x2)\x5\x3\x2\x2\x2*+\a\x3\x2\x2+\a\x3\x2\x2\x2,\x31\x5\n\x6\x2"+
		"-.\a\x5\x2\x2.\x30\x5\n\x6\x2/-\x3\x2\x2\x2\x30\x33\x3\x2\x2\x2\x31/\x3"+
		"\x2\x2\x2\x31\x32\x3\x2\x2\x2\x32\t\x3\x2\x2\x2\x33\x31\x3\x2\x2\x2\x34"+
		"=\a\a\x2\x2\x35\x37\a\x4\x2\x2\x36\x35\x3\x2\x2\x2\x37\x38\x3\x2\x2\x2"+
		"\x38\x36\x3\x2\x2\x2\x38\x39\x3\x2\x2\x2\x39:\x3\x2\x2\x2:<\a\a\x2\x2"+
		";\x36\x3\x2\x2\x2<?\x3\x2\x2\x2=;\x3\x2\x2\x2=>\x3\x2\x2\x2>\v\x3\x2\x2"+
		"\x2?=\x3\x2\x2\x2\n\xF\x15\x1C %\x31\x38=";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace Dialogic
