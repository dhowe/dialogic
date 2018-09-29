using System;
using System.Collections.Generic;
using Superpower;
using Superpower.Model;

namespace Parser
{
    public class DiaCustomTokenizer : Tokenizer<DiaToken>
    {
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

        protected override IEnumerable<Result<DiaToken>> Tokenize(
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
        }

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

        static void Mainx(string[] args)
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
}
