using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Dialogic;
using Superpower;
using Superpower.Display;
using Superpower.Model;
using Superpower.Parsers;
using Superpower.Tokenizers;

using static Superpower.Parsers.Character;

namespace ProcParser
{
    public class ProcTokenizer : Tokenizer<DiaToken>
    {
        internal IDictionary<string, Type> typeMap
            = new Dictionary<string, Type>()
            {
                        { "CHAT",   typeof(Chat) },
                        { "SAY",    typeof(Say)  },
                        { "SET",    typeof(Set)  },
                        { "ASK",    typeof(Ask)  },
                        { "OPT",    typeof(Opt)  },
                        { "DO",     typeof(Do)   },
                        { "GO",     typeof(Go)   },
                        { "WAIT",   typeof(Wait) },
                        { "FIND",   typeof(Find) },
            };

        public static ProcTokenizer Instance { get; } = new ProcTokenizer();

        public static void Mainx(string[] args)
        {
            var i = 0;
            //var tokens = Instance.Tokenize(new TextSpan("A:SAY Hello"));
            var tokens = Instance.Tokenize(new TextSpan("SAY Hello"));
            Console.WriteLine("GOT " + tokens.Count() + " tokens");
            foreach (var t in tokens)
            {
                Console.WriteLine((i++) + ") " + t.Value);
            }
        }

        protected override IEnumerable<Result<DiaToken>> Tokenize(TextSpan span)
        {
            var line = span.ToStringValue();
            var cmdIdx = -1;
            string cmd = null;
            foreach (var t in typeMap.Keys) {
                var idx = line.IndexOf(t);
                if (idx > -1) {
                    cmdIdx = idx;
                    cmd = t;
                }
            }
            //if (cmd != null) {
            //    var keywordStart = next.Location;
            //    yield return Result.Value(FindToken(cmd), keywordStart, next.Location);
            //}
            yield break;
        }

        private DiaToken FindToken(string cmd)
        {
            if (cmd == "SAY") return DiaToken.SAY;
            return DiaToken.Null;
        }

        protected IEnumerable<Result<DiaToken>> Tokenizexx(TextSpan span)
        {
            bool haveCommand = false;
            DiaToken token = DiaToken.Word;

            Result<char> next = SkipWhiteSpace(span);
            do
            {
                if (!haveCommand)
                {
                    if (char.IsLetter(next.Value))
                    {
                        var keywordBuilder = new StringBuilder();

                        var keywordStart = next.Location;
                        keywordBuilder.Append(next.Value);
                        var keywords = new[] { "SAY", "DOWN", "WAIT" };
                        do
                        {
                            next = next.Remainder.ConsumeChar();
                            if (char.IsLetter(next.Value))
                            {
                                keywordBuilder.Append(next.Value);
                            }
                        } while (!keywords.Contains(keywordBuilder.ToString())
                            && next.HasValue && char.IsLetter(next.Value));

                        next = next.Remainder.ConsumeChar();

                        var keyword = keywordBuilder.ToString();
                        switch (keyword)
                        {
                            case "SAY":
                                token = DiaToken.SAY;
                                break;
                            //case "DOWN":
                            //    token = DiaToken.DownKeyword;
                            //    break;
                            //case "WAIT":
                            //token = DiaToken.WaitKeyword;
                            //break;
                            //default:
                                //yield return Result.Empty<DiaToken>(keywordStart, $"Unexpected keyword {keyword}");
                                //break;
                        }

                        yield return Result.Value(token, keywordStart, next.Location);
                    }

                    if (char.IsWhiteSpace(next.Value))
                    {
                        haveCommand = true;
                        yield return Result.Value(DiaToken.Space, next.Location, next.Remainder);
                        next = next.Remainder.ConsumeChar();
                    }
                    if (next.Value.Equals(':'))
                    {
                        haveCommand = true;
                        yield return Result.Value(DiaToken.Space, next.Location, next.Remainder);
                        next = next.Remainder.ConsumeChar();
                    }
                }

            } while (next.HasValue);
        }

        protected IEnumerable<Result<DiaToken>> TokenizeX(TextSpan span)
        {
            Result<char> next = SkipWhiteSpace(span);
            if (!next.HasValue) yield break;

            var prespace = "";
            //string actor = null;
            var postspace = false;

            var sv = span.ToStringValue();
            do
            {
                if (!postspace)
                {
                    if (char.IsLetter(next.Value))
                    {
                        prespace += next.Value;
                        next = next.Remainder.ConsumeChar();
                    }
                    else if (char.IsWhiteSpace(next.Value))
                    {
                        postspace = true;
                        if (prespace == "SAY")
                        {
                            yield return Result.Value(DiaToken.SAY, next.Location, next.Remainder);
                        }

                        yield break;
                    }
                    else if (next.Value == ':')
                    {
                        yield return Result.Value(DiaToken.Ident, next.Location, next.Remainder);
                        yield break;
                    }
                }


                //next = SkipWhiteSpace(next.Location);

            } while (next.HasValue);
        }

    }


    public enum DiaToken
    {
        [Token(Description = "Name:")]
        Actor,

        [Token(Description = "#label")]
        Label,

        [Token(Description = "$variable")]
        Symbol,


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

        //[Token(Example = "[")]
        //LBracket,

        //[Token(Example = "]")]
        //RBracket,

        [Token(Example = "{")]
        LBrace,

        [Token(Example = "}")]
        RBrace,

        [Token(Example = "(")]
        LParen,

        [Token(Example = ")")]
        RParen,

        //[Token(Example = "?")]
        //QuestionMark,

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

        [Token(Example = "'")]
        SingleQ,

        [Token(Example = "\"")]
        DoubleQ,

        //Number,

        Ident,

        Word,

        Space,

        Null
    }

    public static class EnumExtensions
    {
        //public static string AttributeOf(this Enum enumValue, Type enumType)
        public static string Category(this Enum enumValue, Type enumType)
        {
            string displayName = string.Empty;
            MemberInfo info = enumType.GetMember(enumValue.ToString()).First();

            if (info != null && info.CustomAttributes.Any())
            {
                Attribute attr = info.GetCustomAttribute(typeof(TokenAttribute));
                if (attr != null) displayName = ((TokenAttribute)attr).Category;
            }

            return displayName;
        }
    }
}
