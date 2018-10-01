﻿using System;
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

        public struct DiaLine
        {
            public string actor, command, text, label;
            public IDictionary<string, string> meta;

            public override string ToString()
            {
                return "[" + command + " " + text + " " + label
                    + " meta#" + (meta != null ? meta.Count : 0) + "]";
            }
        }

        // ------------------------------------ Parsers ------------------------------------- //


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

        public static readonly TokenListParser<DiaToken, string> GroupParser =
            from lp in Token.EqualTo(DiaToken.LParen)
            from content in Token.EqualTo(DiaToken.String).Or
                            (Token.EqualTo(DiaToken.Symbol)).Or
                            (Token.EqualTo(DiaToken.Pipe)).Many()
            from rp in Token.EqualTo(DiaToken.RParen)
            select content.ToString();

        public static TokenListParser<DiaToken, object> MetaParser =
            from begin in Token.EqualTo(DiaToken.LBrace)
            from properties in TextParser
                .Named("meta-key")
                    .Then(name => Token.EqualTo(DiaToken.Equal)
                          .IgnoreThen(Superpower.Parse.Ref(() => DiaValue)
                    .Select(value => KeyValuePair.Create((string)name, value))))
                .ManyDelimitedBy(Token.EqualTo(DiaToken.Comma),
                    end: Token.EqualTo(DiaToken.RBrace))
            select (object)new Dictionary<string, string>(properties);
        //select(object)new Dictionary<string, object>(properties);

        public static readonly TokenListParser<DiaToken, DiaLine> LineParser =
            //from atr in ActorParser.OptionalOrDefault("")
            from cmd in CmdParser//.OptionalOrDefault("SAY")
            from txt in TextParser.OptionalOrDefault("")
            from lbl in LabelParser.OptionalOrDefault("")
            from mta in MetaParser.OptionalOrDefault()
            select new DiaLine()
            {
                //actor = atr,
                command = cmd,
                text = txt,
                label = lbl,
                meta = (IDictionary<string, string>)mta
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
            .Named("meta-value");
    }


}
