using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

using Superpower;
using Superpower.Display;
using Superpower.Model;
using Superpower.Parsers;
using Superpower.Tokenizers;

using static Superpower.Parsers.Character;

namespace NewParser
{
    public class NewTokenizer
    {
        static readonly TextParser<string> SpaceT =
             from s in Span.WhiteSpace
             select s.ToStringValue();

        public static readonly TextParser<TextSpan> WordChars = Span.Regex(@"[\[\]?0-9-]", RegexOptions.Multiline);
        //Span.Regex(@"[^\[\]{}=,.#$:\s|]+", RegexOptions.Multiline)

        static readonly TextParser<TextSpan> WordT = Span.MatchedBy(Identifier.CStyle).Or(WordChars);

        public static readonly TextParser<Unit> NumberT =
            from sign in EqualTo('-').OptionalOrDefault()
            from first in Digit
            from rest in Digit.Or(In('.', 'e', 'E', '+', '-')).IgnoreMany()
            select Unit.Value;

        static readonly TextParser<TextSpan> IdentT = Identifier.CStyle;

        static readonly TextParser<char> ActorT =
            Span.MatchedBy(Identifier.CStyle).IgnoreThen(EqualTo(':'));

        static readonly TextParser<TextSpan> SymbolT =
            EqualTo('$').IgnoreThen(Identifier.CStyle);

        static readonly TextParser<TextSpan> LabelT =
            EqualTo('#').IgnoreThen(Identifier.CStyle);


        private static Tokenizer<DiaToken> Instance = new TokenizerBuilder<DiaToken>()

            //.Match(ActorT, DiaToken.Actor)
            //.Match(SymbolT, DiaToken.Symbol)
            //.Match(LabelT, DiaToken.Label, true)

            .Match(Span.EqualTo("GO"), DiaToken.GO, true)
            .Match(Span.EqualTo("DO"), DiaToken.DO, true)
            .Match(Span.EqualTo("OPT"), DiaToken.OPT, true)
            .Match(Span.EqualTo("SAY"), DiaToken.SAY, true)
            .Match(Span.EqualTo("SET"), DiaToken.SET, true)
            .Match(Span.EqualTo("ASK"), DiaToken.ASK, true)
            .Match(Span.EqualTo("CHAT"), DiaToken.CHAT, true)
            .Match(Span.EqualTo("FIND"), DiaToken.FIND, true)
            .Match(Span.EqualTo("WAIT"), DiaToken.WAIT, true)

            .Match(EqualTo('{'), DiaToken.LBrace)
            .Match(EqualTo('}'), DiaToken.RBrace)
            //.Match(EqualTo('['), DiaToken.LBracket)
            //.Match(EqualTo(']'), DiaToken.RBracket)
            .Match(EqualTo('('), DiaToken.LParen)
            .Match(EqualTo(')'), DiaToken.RParen)
            .Match(EqualTo('|'), DiaToken.Pipe)
            .Match(EqualTo('='), DiaToken.Equal)
            .Match(EqualTo(','), DiaToken.Comma)
            .Match(EqualTo(':'), DiaToken.Colon)
            .Match(EqualTo('.'), DiaToken.Dot)
            .Match(EqualTo('$'), DiaToken.Dollar)
            .Match(EqualTo('#'), DiaToken.Hash)

            .Match(Identifier.CStyle, DiaToken.Ident)
            .Match(SpaceT, DiaToken.Space)
            //.Match(NumberT, DiaToken.Number, true)
            .Match(WordT, DiaToken.Word)

            .Build();

        //public static TokenList<DiaToken> GetTokens(string source)
        //{
        //    var tokens = Instance.Tokenize(ts);
      
        //    //TokenList<DiaToken> result = new TokenList<DiaToken>();

        //    //if (tokens.Count() == 0) return result;
        //    //foreach (var t in tokens)
        //    //{
        //    //    result.Append(t);
        //    //}
        //    ////var e = tokens.GetEnumerator();

        //    ////var tok = e.Current;
        //    ////while (tok.HasValue) {
        //    ////    e.MoveNext();
        //    ////    Console.WriteLine(e.Current.ToStringValue());
        //    ////}

        //    return result;
        //}


        public static void Main(string[] args)
        {
            //var values = Enum.GetValues(typeof(DiaToken));
            //foreach (DiaToken t in values)
            //{
            //    //Console.WriteLine(t);

            //    var attr = t.GetDisplayAttributeFrom(t.GetType());
            //    System.Console.WriteLine(attr);
            //}
            //return;

            // Using reflection.  
            //System.Attribute[] attrs = System.Attribute.GetCustomAttributes();  // Reflection.  

            //// Displaying output.  
            //foreach (System.Attribute attr in attrs)
            //{
            //    System.Console.WriteLine(attr);
            //    //if (attr is Token)
            //    //{
            //    //    Author a = (Author)attr;
            //    //    System.Console.WriteLine("   {0}, version {1:f}", a.GetName(), a.version);
            //    //}
            //}

            //var x = GetTokens("ABC:");
            //Out(x);

            //return;
            var x = Instance.Tokenize("ABC:");
            Out(x);

            x = Instance.Tokenize(" ABC:");
            Out(x);

            x = Instance.Tokenize(" ABC1:");
            Out(x);

            x = Instance.Tokenize("x ABC:");
            Out(x);

            x = Instance.Tokenize("x ABC-DEF");
            Out(x);

            x = Instance.Tokenize("1ABC:");
            Out(x);

            x = Instance.Tokenize("SAY ABC: hello");
            Out(x);

            x = Instance.Tokenize("SAY a: hello");
            Out(x);

            x = Instance.Tokenize("ABC:SAY Hello x");
            Out(x);

            x = Instance.Tokenize("ABC:SAY Hello $a ok 1");
            Out(x);

            x = Instance.Tokenize("CHAT #abc");
            Out(x);

            x = Instance.Tokenize("CHAT ok #abc ok");
            Out(x);
        }


        static void Out(TokenList<DiaToken> vals)
        {
            var count = 0;
            foreach (var v in vals)
            {
                Console.WriteLine((count++) + ": " + v.Kind + "='" + v.Span + "'");
            }
            Console.WriteLine();
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

        Space
    }
}
