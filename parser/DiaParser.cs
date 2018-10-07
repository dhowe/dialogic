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
        // ------------------------------- Helpers: keep at top ------------------------------- //

        public static string ParseFree(string text) => ParseFree(DiaTokenizer.Instance.Tokenize(text));
        public static string ParseFree(TokenList<DiaToken> tokens) => FreeText.Parse(tokens);

        //public static string ParseText(string text) => ParseText(DiaTokenizer.Instance.Tokenize(text));
        //public static string ParseText(TokenList<DiaToken> tokens) => TextParser.Parse(tokens);

        public static string ParseActor(string text) => ParseActor(DiaTokenizer.Instance.Tokenize(text));
        public static string ParseActor(TokenList<DiaToken> tokens) => ActorParser.Parse(tokens);

        public static string ParseSymbol(string text) => ParseSymbol(DiaTokenizer.Instance.Tokenize(text));
        public static string ParseSymbol(TokenList<DiaToken> tokens) => SymbolParser.Parse(tokens);

        public static string ParseBool(string text) => ParseBool(DiaTokenizer.Instance.Tokenize(text));
        public static string ParseBool(TokenList<DiaToken> tokens) => BoolParser.Parse(tokens);

        public static string ParseGroup(string text) => ParseGroup(DiaTokenizer.Instance.Tokenize(text));
        public static string ParseGroup(TokenList<DiaToken> tokens) => GroupParser.Parse(tokens);

        public static string ParseLabel(string text) => ParseLabel(DiaTokenizer.Instance.Tokenize(text));
        public static string ParseLabel(TokenList<DiaToken> tokens) => LabelParser.Parse(tokens);

        public static object ParseMeta(string text) => ParseMeta(DiaTokenizer.Instance.Tokenize(text));
        public static object ParseMeta(TokenList<DiaToken> tokens) => MetaParser.Parse(tokens);

        public static IEnumerable<DiaLine> Parse(string text) => Parse(DiaTokenizer.Instance.Tokenize(text));
        public static IEnumerable<DiaLine> Parse(TokenList<DiaToken> tokens) => LinesParser.Parse(tokens);

        private static string TokensToString(Token<DiaToken>[] tokens) // tmp
        {
            var s = "";
            var i = 0;
            foreach (var t in tokens)
            {
                //Console.WriteLine((i++) + ") " + t.ToStringValue());
                s += t.ToStringValue();
            }
            return s;
        }

        public struct DiaLine
        {
            public string actor, command, text, label;
            public IDictionary<string, string> meta;

            public string MetaString() {
                var s = "";
                foreach (KeyValuePair<string,string> kv in meta) {
                    s += kv.Key + "=" + kv.Value + ",";
                }
                return s.Substring(s.Length-1);
            }

            public override string ToString()
            {
                return "[" + command + (" " + text).Trim() + (" " + label).Trim()
                    + (meta != null ? " " + MetaString() : "")+"]";
                // +" meta#" + (meta != null ? meta.Count : 0) + "]";
            }

        }

        // ------------------------------------ Parsers ------------------------------------- //

        /* note: instead of parsing all commands together here, we can parse each separately
           and enforce the presense of valid argument types
           though how to handle custom Command types?  allow them by default  */

        /*
        SAY:  actor? text meta*
        ASK:  actor? text meta*
        OPT:  text #label? meta*
        SET:  ident = (bool | num | text)
        GO:   #label
        DO:   actor? #label meta*
        WAIT: number?
        CHAT: (ident | #label) meta*
        FIND: meta
        */

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
                             
        static readonly TokenListParser<DiaToken, string> SymbolParser =
            from s in Token.EqualTo(DiaToken.Symbol) select s.ToStringValue().Trim();

        public static TokenListParser<DiaToken, string> TextPunct { get; } =
            from txt in Token.EqualTo(DiaToken.Text)
                .Or(Token.EqualTo(DiaToken.Comma))
                .Or(Token.EqualTo(DiaToken.Colon))
                .Or(Token.EqualTo(DiaToken.Equal))
                .Many()
            select TokensToString(txt);

        public static TokenListParser<DiaToken, string> FreeText { get; } =
            from txt in TextPunct.Or(SymbolParser)
            select txt;

        public static readonly TokenListParser<DiaToken, string> TextParser =
            from s in Token.EqualTo(DiaToken.Text).Or(Token.EqualTo(DiaToken.Comma))
            select s.ToStringValue().Trim();

        public static readonly TokenListParser<DiaToken, string> LabelParser =
            from s in Token.EqualTo(DiaToken.Label) select s.ToStringValue().Trim();

        public static readonly TokenListParser<DiaToken, string> ActorParser =
           from s in Token.EqualTo(DiaToken.Actor) select s.ToStringValue().Trim();

        public static readonly TokenListParser<DiaToken, string> MetaCharParser =
            from s in Token.EqualTo(DiaToken.Comma).Or(Token.EqualTo(DiaToken.Equal))
            select s.ToStringValue();

        public static readonly TokenListParser<DiaToken, string> BoolParser =
            from s in Token.EqualTo(DiaToken.True).Or(Token.EqualTo(DiaToken.False))
            select s.ToStringValue();
                           
        public static readonly TokenListParser<DiaToken, string> GroupParser =
            from lp in Token.EqualTo(DiaToken.LParen).AtLeastOnce()
            from content in Token.EqualTo(DiaToken.Text).Or
                (Token.EqualTo(DiaToken.Symbol)).Or
                (Token.EqualTo(DiaToken.Label)).Or
                (Token.EqualTo(DiaToken.Comma)).Or
                (Token.EqualTo(DiaToken.Pipe))
            from rp in Token.EqualTo(DiaToken.RParen).AtLeastOnce()
            select content.ToStringValue();

        public static TokenListParser<DiaToken, object> MetaParser =
            from begin in Token.EqualTo(DiaToken.LBrace)
            from properties in TextParser
                .Named("meta-key")
                    .Then(name => Token.EqualTo(DiaToken.Equal)
                          .IgnoreThen(Superpower.Parse.Ref(() => DiaValue)
                    .Select(value => KeyValuePair.Create(((string)name).Trim(), value.Trim()))))
                .ManyDelimitedBy(Token.EqualTo(DiaToken.Comma),
                    end: Token.EqualTo(DiaToken.RBrace))
            select (object)new Dictionary<string, string>(properties);
            //select(object)new Dictionary<string, object>(properties); ?

        public static readonly TokenListParser<DiaToken, DiaLine> LineParser =
            from atr in ActorParser.OptionalOrDefault(string.Empty)
            from cmd in CmdParser.OptionalOrDefault(string.Empty)
            from txt in FreeText.OptionalOrDefault(string.Empty)
            from lbl in LabelParser.OptionalOrDefault(string.Empty)
            from mta in MetaParser.OptionalOrDefault()
            select new DiaLine()
            {
                actor = atr,
                command = cmd,
                text = txt.Trim(),//?
                label = lbl.Trim(),
                meta = (IDictionary<string, string>)mta
            };

        static readonly TokenListParser<DiaToken, DiaLine[]> LinesParser =
            LineParser.ManyDelimitedBy(Token.EqualTo(DiaToken.NewLine));

        static TokenListParser<DiaToken, object> DiaTrue { get; } =
            Token.EqualToValue(DiaToken.True, "true").Value((object)true);

        static TokenListParser<DiaToken, object> DiaFalse { get; } =
            Token.EqualToValue(DiaToken.False, "false").Value((object)false);

        static TokenListParser<DiaToken, object> DiaNull { get; } =
            Token.EqualToValue(DiaToken.Null, "null").Value((object)null);

        static TokenListParser<DiaToken, string> DiaValue { get; } =
            TextParser
            .Or(SymbolParser)
            .Or(MetaCharParser)
            .Or(BoolParser)
            .Named("meta-value");

    }


}
