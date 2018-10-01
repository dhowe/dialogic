using System;
using Superpower;
using Superpower.Display;
using Superpower.Model;
using Superpower.Parsers;
using Superpower.Tokenizers;

using System.Linq;

namespace Parser
{
    public class DiaTokenizer : Tokenizer<DiaToken>
    {
        static TextParser<Unit> Number { get; } =
            from sign in Character.EqualTo('-').OptionalOrDefault()
            from first in Character.Digit
            from rest in Character.Digit.Or(Character.In('.', 'e', 'E', '+', '-')).IgnoreMany()
            select Unit.Value;

        static TextParser<Unit> Label { get; } =
            from first in Character.EqualTo('#')
            from rest in Identifier.CStyle
            select Unit.Value;

        static TextParser<Unit> Variable { get; } =
            from first in Character.EqualTo('$')
            from rest in Identifier.CStyle
            select Unit.Value;

        static readonly Func<char, bool> NonString = c => IsOneOf(c, '{', '}', '#', '$', '=', ',', '(', ')');

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

                .Match(Label, DiaToken.Label, true /*?*/)
                .Match(Span.EqualTo("true"), DiaToken.True, true)
                .Match(Span.EqualTo("false"), DiaToken.True, true)
                .Match(Span.EqualTo("()"), DiaToken.ParenPair)
                .Match(Variable, DiaToken.Symbol)

                .Match(Character.EqualTo('{'), DiaToken.LBrace)
                .Match(Character.EqualTo('}'), DiaToken.RBrace)
                .Match(Character.EqualTo('['), DiaToken.LBracket)
                .Match(Character.EqualTo(']'), DiaToken.RBracket)
                .Match(Character.EqualTo('('), DiaToken.LParen)
                .Match(Character.EqualTo(')'), DiaToken.RParen)
                .Match(Character.EqualTo('|'), DiaToken.Pipe)
                .Match(Character.EqualTo('='), DiaToken.Equal)
                .Match(Character.EqualTo(','), DiaToken.Comma)
                .Match(Character.EqualTo(':'), DiaToken.Colon)
                .Match(Character.EqualTo('.'), DiaToken.Dot)

                .Match(Span.WithoutAny(NonString), DiaToken.String)
                .Build();

        static bool IsOneOf(char c, params char[] candidates)
        {
            foreach (var i in candidates)
            {
                if (c == i) return true;
            }
            return false;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Main()");
        }
    }

    public enum DiaToken
    {
        None,

        String,

        Number,

        [Token(Description = "#identifier")]
        Label,

        [Token(Description = "$variable")]
        Symbol,


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
