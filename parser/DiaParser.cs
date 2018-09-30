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

        public static string ParseLabel(string text)
        {
            return ParseLabel(DiaTokenizer.Instance.Tokenize(text));
        }

        public static string ParseLabel(TokenList<DiaToken> tokens)
        {
            return LabelParser.Parse(tokens);
        }

        public static object ParseMeta(string text)
        {
            return ParseMeta(DiaTokenizer.Instance.Tokenize(text));
        }

        public static object ParseMeta(TokenList<DiaToken> tokens)
        {
            return MetaParser.Parse(tokens);
        }

        public static IEnumerable<DiaLine> Parse(string text)
        {
            return Parse(DiaTokenizer.Instance.Tokenize(text));
        }

        public static IEnumerable<DiaLine> Parse(TokenList<DiaToken> tokens)
        {
            return LinesParser.Parse(tokens);
        }

        public struct DiaLine // actor, command, text, label, meta;
        {
            public string actor, command, text, label, meta;
            public override string ToString()
            {
                return "[" + command + " " + text + "]";
            }
        }

        //public static TokenListParser<DiaToken, string> EqualToAny<DiaToken>(params DiaToken[] kinds) {
        //    foreach(var k in kinds) {
        //        var res = Token.EqualTo(k);
        //        if (res) return res;
        //    }
        //    return null;
        //}

        public static readonly TokenListParser<DiaToken, string> CmdParser =
            from cmd in Token.EqualTo(DiaToken.SAY)
                .Or(Token.EqualTo(DiaToken.GO))
                .Or(Token.EqualTo(DiaToken.DO))
                .Or(Token.EqualTo(DiaToken.OPT))
                .Or(Token.EqualTo(DiaToken.SAY))
                .Or(Token.EqualTo(DiaToken.SET))
                .Or(Token.EqualTo(DiaToken.ASK))
                .Or(Token.EqualTo(DiaToken.CHAT))
                .Or(Token.EqualTo(DiaToken.FIND))
                .Or(Token.EqualTo(DiaToken.WAIT))
            select cmd.ToStringValue();

        public static readonly TokenListParser<DiaToken, string> TextParser =
            from s in Token.EqualTo(DiaToken.String) select s.ToStringValue();

        public static readonly TokenListParser<DiaToken, string> LabelParser =
            from s in Token.EqualTo(DiaToken.Label) select s.ToStringValue();

        public static readonly TokenListParser<DiaToken, string> SymbolParser =
            from s in Token.EqualTo(DiaToken.Symbol) select s.ToStringValue();

        public static readonly TokenListParser<DiaToken, string> MetaCharParser =
            from s in Token.EqualTo(DiaToken.Comma).Or(Token.EqualTo(DiaToken.Equal))
            select s.ToStringValue();

        public static readonly TokenListParser<DiaToken, string> BoolParser =
            from s in Token.EqualTo(DiaToken.True).Or(Token.EqualTo(DiaToken.False))
            select s.ToStringValue();

        //public static readonly TokenListParser<DiaToken, string> MetaParser =
        //from lb in Token.EqualTo(DiaToken.LBrace)
        //                from con in Token.EqualTo()
        //from rb in Token.EqualTo(DiaToken.RBrace)

        //static TokenListParser<DiaToken, object> DiaString { get; } =
        //Token.EqualTo(DiaToken.String)
        //.Apply(JsonTextParsers.String)
        //.Select(s => (object)s);

        public static TokenListParser<DiaToken, object> MetaParser =
            from begin in Token.EqualTo(DiaToken.LBrace)
            from properties in TextParser
                .Named("property name")
                    .Then(name => Token.EqualTo(DiaToken.Equal)
                          .IgnoreThen(Superpower.Parse.Ref(() => DiaValue)
                    .Select(value => KeyValuePair.Create((string)name, value))))
                .ManyDelimitedBy(Token.EqualTo(DiaToken.Comma),
                    end: Token.EqualTo(DiaToken.RBrace))
            select (object)new Dictionary<string, string>(properties);

        public static readonly TokenListParser<DiaToken, DiaLine> LineParser =
            from cmd in CmdParser//.OptionalOrDefault("SAY")
            from txt in TextParser.OptionalOrDefault("")
            from lbl in LabelParser.OptionalOrDefault("")
                //from rp in Token.EqualTo(DiaToken.RParen)
            select new DiaLine()
            {
                command = cmd, //actor, meta;
                text = txt,
                label = lbl
            };

        static TokenListParser<DiaToken, object> DiaTrue { get; } =
            Token.EqualToValue(DiaToken.True, "true").Value((object)true);

        static TokenListParser<DiaToken, object> DiaFalse { get; } =
            Token.EqualToValue(DiaToken.False, "false").Value((object)false);

        static TokenListParser<DiaToken, object> DiaNull { get; } =
            Token.EqualToValue(DiaToken.Null, "null").Value((object)null);


        public static readonly TokenListParser<DiaToken, DiaLine[]> LinesParser =
            LineParser.ManyDelimitedBy(Token.EqualTo(DiaToken.NewLine));

        static TokenListParser<DiaToken, string> DiaValue { get; } =
            TextParser
                .Or(SymbolParser)
                .Or(MetaCharParser)
                .Or(BoolParser)
                .Named("JSON value");
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
