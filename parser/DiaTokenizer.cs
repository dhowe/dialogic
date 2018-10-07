using System;
using Superpower;
using Superpower.Display;
using Superpower.Model;
using Superpower.Parsers;
using Superpower.Tokenizers;

using System.Linq;
using System.Text.RegularExpressions;

using static Superpower.Parsers.Character;

namespace Parser
{
    public class DiaTokenizer : Tokenizer<DiaToken>
    {

        public static TextParser<TextSpan> StartsLine { get; } =
            Span.Regex(@"^.+", RegexOptions.Singleline);
            //select s;

        public static TextParser<string> Text { get; } =
            from chars in ExceptIn('{', '}', '#', '$', '=', '(', ')', ',').AtLeastOnce()
            select new string(chars);

        static TextParser<TextSpan> Symbol { get; } =
            EqualTo('$').AtLeastOnce().IgnoreThen(Identifier.CStyle);

        static TextParser<TextSpan> Label { get; } =
            EqualTo('#').IgnoreThen(Identifier.CStyle);

        static TextParser<Unit> Actor { get; } =
            from name in LetterOrDigit.Many()
            from last in EqualTo(':')
            select Unit.Value;

        static TextParser<Unit> ActorRE { get; } =
            from start in StartsLine
                //from name in LetterOrDigit.
                from last in EqualTo(':')
            select Unit.Value;

        public static TextParser<Unit> Number { get; } =
            from sign in EqualTo('-').OptionalOrDefault()
            from first in Digit
            from rest in Digit.Or(In('.', 'e', 'E', '+', '-')).IgnoreMany()
            select Unit.Value;

        static readonly Func<char, bool> NotString = c => IsOneOf(c, '{', '}', '#', '$', '=', '(', ')', ',');

        public static Tokenizer<DiaToken> Instance { get; } =
            new TokenizerBuilder<DiaToken>()
                .Ignore(Span.WhiteSpace)
                .Match(Span.EqualTo("GO"), DiaToken.GO, true)
                .Match(Span.EqualTo("DO"), DiaToken.DO, true)
                .Match(Span.EqualTo("OPT"), DiaToken.OPT, true)
                .Match(Span.EqualTo("SAY"), DiaToken.SAY, true)
                .Match(Span.EqualTo("SET"), DiaToken.SET, true)
                .Match(Span.EqualTo("ASK"), DiaToken.ASK, true)
                .Match(Span.EqualTo("CHAT"), DiaToken.CHAT, true)
                .Match(Span.EqualTo("FIND"), DiaToken.FIND, true)
                .Match(Span.EqualTo("WAIT"), DiaToken.WAIT, true)

                .Match(Symbol, DiaToken.Symbol)
                .Match(Actor, DiaToken.Actor)
                .Match(Label, DiaToken.Label, true)
                .Match(Span.EqualTo("true"), DiaToken.True, true)
                .Match(Span.EqualTo("false"), DiaToken.True, true)
                .Match(Span.EqualTo("()"), DiaToken.ParenPair)
   
                //.Match(Identifier.CStyle, DiaToken.Ident)

                .Match(EqualTo('{'), DiaToken.LBrace)
                .Match(EqualTo('}'), DiaToken.RBrace)
                .Match(EqualTo('['), DiaToken.LBracket)
                .Match(EqualTo(']'), DiaToken.RBracket)
                .Match(EqualTo('('), DiaToken.LParen)
                .Match(EqualTo(')'), DiaToken.RParen)
                .Match(EqualTo('|'), DiaToken.Pipe)
                .Match(EqualTo('='), DiaToken.Equal)
                .Match(EqualTo(','), DiaToken.Comma)
                .Match(EqualTo(':'), DiaToken.Colon)
                .Match(EqualTo('.'), DiaToken.Dot)

                .Match(Text, DiaToken.Text)
                .Build();

        static bool IsOneOf(char c, params char[] candidates)
        {
            foreach (var i in candidates)
            {
                if (c == i) return true;
            }
            return false;
        }

        static void Main(string[] s)
        {
            Console.WriteLine("Main()");
        }
    }

    public enum DiaToken
    {
        None,

        Text,

        Number,

        [Token(Description = "#label")]
        Label,

        [Token(Description = "$variable")]
        Symbol,

        [Token(Description = "actor:")]
        Actor,

        [Token(Description = "^[)]")]
        NotRParen,

        [Token(Example = "[")]
        LBracket,

        [Token(Example = "]")]
        RBracket,

        [Token(Example = "{")]
        LBrace,

        [Token(Example = "}")]
        RBrace,

        [Token(Example = "(")]
        LParen,

        [Token(Example = ")")]
        RParen,

        [Token(Example = "?")]
        QuestionMark,

        [Token(Example = "#")]
        Hash,

        [Token(Example = "$")]
        Dollar,

        [Token(Example = "|")]
        Pipe,

        [Token(Example = "=")]
        Equal,

        [Token(Example = ",")]
        Comma,

        [Token(Example = ":")]
        Colon,

        [Token(Example = ".")]
        Dot,

        [Token(Example = "()")]
        ParenPair,

        [Token(Category = "command", Example = "SAY")]
        SAY,

        [Token(Category = "command", Example = "ASK")]
        ASK,

        [Token(Category = "command", Example = "FIND")]
        FIND,

        [Token(Category = "command", Example = "SET")]
        SET,

        [Token(Category = "command", Example = "DO")]
        DO,

        [Token(Category = "command", Example = "GO")]
        GO,

        [Token(Category = "command", Example = "OPT")]
        OPT,

        [Token(Category = "command", Example = "CHAT")]
        CHAT,

        [Token(Category = "command", Example = "WAIT")]
        WAIT,


        [Token(Category = "keyword", Example = "true")]
        True,

        [Token(Category = "keyword", Example = "false")]
        False,

        [Token(Category = "keyword", Example = "null")]
        Null,

        [Token(Category = "separator")]
        NewLine,
    }

}
