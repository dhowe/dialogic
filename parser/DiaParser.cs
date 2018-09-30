using System;
using System.Collections.Generic;
using Superpower;
using Superpower.Display;
using Superpower.Model;
using Superpower.Parsers;
using Superpower.Tokenizers;

using System.Linq;
using Dialogic;

namespace Parser
{
    public static class DiaParser
    {
        public static IEnumerable<DiaLine> Parse(string text)
        {
            return Parse(DiaTokenizer.Instance.Tokenize(text));
        }

        public static IEnumerable<DiaLine> Parse(TokenList<DiaToken> tokens)
        {
            return Lines.Parse(tokens);
        }

        public struct DiaLine // actor, command, text, label, meta;
        {
            public string actor, command, text, label, meta;
            public override string ToString()
            {
                return "[" + command + " " + text + "]";
            }
        }

        public static readonly TokenListParser<DiaToken, string> SAY =
            from cmd in Token.EqualTo(DiaToken.SAY) select cmd.ToStringValue();
            
        public static readonly TokenListParser<DiaToken, string> Text =
            from s in Token.EqualTo(DiaToken.String) select s.ToStringValue();

        public static readonly TokenListParser<DiaToken, DiaLine> Line =
            //from lp in Token.EqualTo(DiaToken.LParen)
            from cmd in SAY//.Or(ASK).Or(Wait)
            from txt in Text
                //from rp in Token.EqualTo(DiaToken.RParen)
            select new DiaLine()
            {
                command = cmd,// text, label, meta;
                text = txt
            };

        public static readonly TokenListParser<DiaToken, DiaLine[]> Lines =
            Line.ManyDelimitedBy(Token.EqualTo(DiaToken.NewLine));

        //public static readonly TokenListParser<DiaToken, string> CommandLabel =
        //    from text in Token.EqualTo(DiaToken.Label)
        //    select text.ToStringValue();

        //public static readonly TokenListParser<DiaToken, LineContext> Line =
        //    from label in CommandLabel.OptionalOrDefault()
        //    select new LineContext(label, instruction);

        //public static readonly TokenListParser<DiaToken, LineContext[]> ChatParser =
        //LineContext.ManyDelimitedBy(Token.EqualTo(DiaToken.NewLine));

        //public static DiaToken Parse(string filterExpression)
        //{
        //    if (!TryParse(filterExpression, out var root, out var error))
        //        throw new ArgumentException(error);

        //    return root;
        //}

        //public static bool TryParse(string input, out DiaToken root, out string error)
        //{
        //    if (input == null) throw new ArgumentNullException(nameof(input));

        //    var tokenList = DiaTokenizer.Instance.TryTokenize(input);
        //    if (!tokenList.HasValue)
        //    {
        //        error = tokenList.ToString();
        //        root = null;
        //        return false;
        //    }

        //    var result = DiaTokenParsers.TryParse(tokenList.Value);
        //    if (!result.HasValue)
        //    {
        //        error = result.ToString();
        //        root = null;
        //        return false;
        //    }

        //    root = result.Value;
        //    error = null;
        //    return true;
        //}
    }
}
