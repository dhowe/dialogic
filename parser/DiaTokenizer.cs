using System;
using System.Collections.Generic;
using Superpower;
using Superpower.Display;
using Superpower.Model;
using Superpower.Parsers;
using Superpower.Tokenizers;

namespace Parser
{
    public class DiaTokenizer : Tokenizer<DiaToken>
    {

        static TextParser<TextSpan> Label { get; } =
            Span.MatchedBy(Character.EqualTo('#')
                .IgnoreThen(Character.LetterOrDigit.Or(Character.EqualTo('_')).Many()));

        static TextParser<Unit> DiaNumberToken { get; } =
            from sign in Character.EqualTo('-').OptionalOrDefault()
            from first in Character.Digit
            from rest in Character.Digit.Or(Character.In('.', 'e', 'E', '+', '-')).IgnoreMany()
            select Unit.Value;

        static TextParser<Unit> HashLabel { get; } =
            from first in Character.EqualTo('#')
            from second in Character.Letter.Or(Character.EqualTo('_'))
            from rest in Character.LetterOrDigit.Or(Character.EqualTo('_')).Many()
            select Unit.Value;

        static readonly Func<char, bool> BracesOrHash = c =>
        {
            return c == '{' || c == '}' || c == '#';
        };

        static readonly Func<char, bool> Whitespace = c =>
        {
            return c == ' ' || c == '\t';
        };

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
                .Match(Character.EqualTo('$'), DiaToken.Dollar)
                .Match(Character.EqualTo('{'), DiaToken.LBrace)
                .Match(Character.EqualTo('}'), DiaToken.RBrace)
                .Match(Character.EqualTo('('), DiaToken.LParen)
                .Match(Character.EqualTo(')'), DiaToken.RParen)
                //.Match(DiaNumberToken, DiaToken.Number)\
                //.Match(Identifier.CStyle, DiaToken.Identifier)
                .Match(HashLabel, DiaToken.Label)
                .Match(Span.WithoutAny(BracesOrHash), DiaToken.String)
                .Build();



        struct DiaKeyword
        {
            public string Text { get; }
            public DiaToken Token { get; }

            public DiaKeyword(string text, DiaToken token)
            {
                if (text == null) throw new ArgumentNullException(nameof(text));
                Text = text;
                Token = token;
            }
        }

        static readonly DiaKeyword[] Keywords =
        {
            new DiaKeyword("GO", DiaToken.GO),
            new DiaKeyword("DO", DiaToken.DO),
            new DiaKeyword("SAY", DiaToken.SAY),
            new DiaKeyword("SET", DiaToken.SET),
            new DiaKeyword("ASK", DiaToken.ASK),
            new DiaKeyword("CHAT", DiaToken.CHAT),
            new DiaKeyword("FIND", DiaToken.FIND),
            new DiaKeyword("WAIT", DiaToken.WAIT)
        };

        /*protected override IEnumerable<Result<DiaToken>> Tokenize(
            TextSpan span, TokenizationState<DiaToken> state)
        {
            var next = SkipWhiteSpace(span);
            if (!next.HasValue)
                yield break;

            if (char.IsUpper(next.Value))
            {
                var beginIdentifier = next.Location;
                do
                {
                    next = next.Remainder.ConsumeChar();
                }
                while (next.HasValue && char.IsUpper(next.Value));

                DiaToken keyword;
                if (TryGetKeyword(beginIdentifier.Until(next.Location), out keyword))
                {
                    yield return Result.Value(keyword, beginIdentifier, next.Location);
                }
            }

            do
            {
                if (char.IsUpper(next.Value))
                {
                    var beginIdentifier = next.Location;
                    do
                    {
                        next = next.Remainder.ConsumeChar();
                    }
                    while (next.HasValue && (char.IsLetterOrDigit(next.Value) || next.Value == '_'));

                    DiaToken keyword;
                    if (TryGetKeyword(beginIdentifier.Until(next.Location), out keyword))
                    {
                        yield return Result.Value(keyword, beginIdentifier, next.Location);
                    }
                    else
                    {
                        yield return Result.Value(DiaToken.Identifier, beginIdentifier, next.Location);
                    }
                }
                else if (next.Value == '#')
                {
                    var beginIdentifier = next.Location;
                    var startOfName = next.Remainder;
                    do
                    {
                        next = next.Remainder.ConsumeChar();
                    }
                    while (next.HasValue && char.IsLetterOrDigit(next.Value));

                    if (next.Remainder == startOfName)
                    {
                        yield return Result.Empty<DiaToken>(startOfName, new[] { "built-in hash?" });
                    }
                    else
                    {
                        yield return Result.Value(DiaToken.LBrace, beginIdentifier, next.Location);
                    }
                }

            } while (next.HasValue);
        }*/

        static bool TryGetKeyword(TextSpan span, out DiaToken keyword)
        {
            foreach (var kw in Keywords)
            {
                if (span.EqualsValueIgnoreCase(kw.Text))
                {
                    keyword = kw.Token;
                    return true;
                }
            }

            keyword = DiaToken.None;

            return false;
        }

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

        [Token(Description = "#identifier")]
        Label,


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

        [Token(Category = "command", Example = "WAIT")]
        WAIT,
    }
}
