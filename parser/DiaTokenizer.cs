using System;
using Superpower;
using Superpower.Display;
using Superpower.Model;
using Superpower.Parsers;
using Superpower.Tokenizers;

namespace Parser
{
    public class DiaTokenizer
    {
        static readonly Func <char, bool> BracesOrHash = c => { 
            return c == '{' || c == '}' || c == '#'; 
        };

        public static Tokenizer<DiaToken> Instance { get; } =
            new TokenizerBuilder<DiaToken>()
                .Ignore(Span.WhiteSpace)
                .Match(Span.EqualTo("SAY"), DiaToken.SAY)
                .Match(Character.EqualTo('{'), DiaToken.LBrace)
                .Match(Character.EqualTo('('), DiaToken.LParen)
                .Match(Character.EqualTo(')'), DiaToken.RParen)
                .Match(Span.WithoutAny(BracesOrHash), DiaToken.String)
                .Build();

        static TextParser<Unit> DiaNumberToken { get; } =
          from sign in Character.EqualTo('-').OptionalOrDefault()
          from first in Character.Digit
          from rest in Character.Digit.Or(Character.In('.', 'e', 'E', '+', '-')).IgnoreMany()
          select Unit.Value;

        static void Main(string[] args)
        {
            var tokens1 = DiaTokenizer.Instance.TryTokenize("SAY Hello you");

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

        Identifier,

        String,

        Number,


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
    }
}
