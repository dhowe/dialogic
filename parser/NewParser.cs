using System.Collections.Generic;
using Superpower;

using System.Linq;
using Superpower.Parsers;

namespace NewParser
{
    public static class NewParser
    {

        public struct DiaLine
        {
            public string actor, command, text, label;
            public IDictionary<string, string> meta;

            public string MetaString()
            {
                var s = "";
                foreach (KeyValuePair<string, string> kv in meta)
                {
                    s += kv.Key + "=" + kv.Value + ",";
                }
                return s.Substring(s.Length - 1);
            }

            public override string ToString()
            {
                return "[" + command + (" " + text).Trim() + (" " + label).Trim()
                    + (meta != null ? " " + MetaString() : "") + "]";
            }

        }

        public static readonly TokenListParser<DiaToken, string> ActorParser =
            from a in Token.EqualTo(DiaToken.Ident)
            from b in Token.EqualTo(DiaToken.Colon)
            select TokStr(a, b); // Token.Sequence?

        public static readonly TokenListParser<DiaToken, string> LabelParser =
            from a in Token.EqualTo(DiaToken.Hash)
            from b in Token.EqualTo(DiaToken.Ident)
            select TokStr(a, b); // Token.Sequence?


        public static readonly TokenListParser<DiaToken, string> CmdParser =
            from a in Token.EqualTo(DiaToken.SAY)
                .Or(Token.EqualTo(DiaToken.GO))
                .Or(Token.EqualTo(DiaToken.DO))
                .Or(Token.EqualTo(DiaToken.OPT))
                .Or(Token.EqualTo(DiaToken.SAY))
                .Or(Token.EqualTo(DiaToken.SET))
                .Or(Token.EqualTo(DiaToken.ASK))
                .Or(Token.EqualTo(DiaToken.CHAT))
                .Or(Token.EqualTo(DiaToken.FIND))
                .Or(Token.EqualTo(DiaToken.WAIT))
            select a.ToStringValue();

        public static readonly TokenListParser<DiaToken, string> TextParser =
            from a in (Token.EqualTo(DiaToken.Word)
                       .Or(Token.EqualTo(DiaToken.Ident)))
                //.Or(NewTokenizer.WordChars))
                       //.Many()
            select TokStr(a);

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

        public static readonly TokenListParser<DiaToken, DiaLine> LineParser =
            from atr in ActorParser.OptionalOrDefault(string.Empty)
            from cmd in CmdParser.OptionalOrDefault(string.Empty)
            from txt in TextParser.OptionalOrDefault(string.Empty)
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

        static TokenListParser<DiaToken, string> DiaValue { get; } =
            TextParser
            .Named("meta-value");

        private static string TokStr(params Superpower.Model.Token<DiaToken>[] tokens) // tmp
        {
            var s = "";
            foreach (var t in tokens) s += t.ToStringValue();
            return s;
        }

    }

}
