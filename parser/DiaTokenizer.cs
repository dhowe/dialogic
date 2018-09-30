using System;
using System.Collections.Generic;
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
        static TextParser<Unit> NumberToken { get; } =
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
                //from second in Character.Letter.Or(Character.EqualTo('_'))
                //from rest in Character.LetterOrDigit.Or(Character.EqualTo('_')).Many()
            from rest in Identifier.CStyle
            select Unit.Value;

        static readonly Func<char, bool> NonString = c =>
            c == '{' || c == '}' || c == '#' || c == '$' || c == '=' || c == ',';
        //static readonly Func<char, bool> Whitespace = c => c == ' ' || c == '\t';

        public static Tokenizer<DiaToken> Instance { get; } =
            new TokenizerBuilder<DiaToken>()
                .Ignore(Span.WhiteSpace)
                .Match(Span.EqualTo("GO"), DiaToken.GO)
                .Match(Span.EqualTo("DO"), DiaToken.DO)
                .Match(Span.EqualTo("SAY"), DiaToken.SAY)
                .Match(Span.EqualTo("SET"), DiaToken.SET)
                .Match(Span.EqualTo("ASK"), DiaToken.ASK)
                .Match(Span.EqualTo("CHAT"), DiaToken.CHAT)
                .Match(Span.EqualTo("FIND"), DiaToken.FIND)
                .Match(Span.EqualTo("WAIT"), DiaToken.WAIT)
                //.Match(Character.EqualTo('#'), DiaToken.Hash)
                //.Match(Character.EqualTo('$'), DiaToken.Dollar)
                .Match(Character.EqualTo('{'), DiaToken.LBrace)
                .Match(Character.EqualTo('}'), DiaToken.RBrace)
                .Match(Character.EqualTo('('), DiaToken.LParen)
                .Match(Character.EqualTo(')'), DiaToken.RParen)
                .Match(Character.EqualTo('|'), DiaToken.Pipe)
                .Match(Character.EqualTo('='), DiaToken.Equal)
                .Match(Character.EqualTo(','), DiaToken.Comma)
                //.Match(DiaNumberToken, DiaToken.Number)\
                //.Match(Identifier.CStyle, DiaToken.Identifier)
                .Match(Label, DiaToken.Label)
                .Match(Variable, DiaToken.Variable)
                .Match(Span.WithoutAny(NonString), DiaToken.String)
                //.Match(Character.ExceptIn('{', '}', '#', '$', '=', ','), DiaToken.String)
                .Build();

        static void Main(string[] args)
        {
            var tokens1 = DiaTokenizer.Instance.TryTokenize("SAY Hello you");

            Result<TokenList<DiaToken>> result = DiaTokenizer.Instance.TryTokenize("$2badname");

            var tokens = DiaTokenizer.Instance.Tokenize("SAY Hello you");

            var count = 0;
            foreach (var emp in tokens)
            {
                Console.WriteLine(++count + ": " + emp);
            }
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
        Variable,


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


        [Token(Category = "keyword", Example = "null")]
        Null
    }
}
