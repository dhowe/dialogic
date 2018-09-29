using System;
using System.Collections.Generic;
using Superpower;
using Superpower.Display;
using Superpower.Model;
using Superpower.Parsers;
using Superpower.Tokenizers;

namespace superparser
{
    public class SuperParser
    {
        public static void Maix(string[] args)
        {
            Console.Write("dialogic> ");
            var line = Console.ReadLine();
            while (line != null)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    if (SpParser.TryParse(line, out var value, out var error, out var errorPosition))
                    {
                        Print(value);
                    }
                    else
                    {
                        Console.WriteLine($"     {new string(' ', errorPosition.Column)}^");
                        Console.WriteLine(error);
                    }
                }

                Console.WriteLine();
                Console.Write("dialogic> ");
                line = Console.ReadLine();
            }
        }

        static void Print(object value, int indent = 0)
        {
            switch (value)
            {
                case null:
                    Indent(indent, "Null");
                    break;
                case true:
                    Indent(indent, "True");
                    break;
                case false:
                    Indent(indent, "False");
                    break;
                case double n:
                    Indent(indent, $"Number: {n}");
                    break;
                case string s:
                    Indent(indent, $"String: {s}");
                    break;
                case object[] a:
                    Indent(indent, "Array:");
                    foreach (var el in a)
                        Print(el, indent + 2);
                    break;
                case Dictionary<string, object> o:
                    Indent(indent, "Object:");
                    foreach (var p in o)
                    {
                        Indent(indent + 2, p.Key);
                        Print(p.Value, indent + 4);
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        static void Indent(int amount, string text)
        {
            Console.WriteLine($"{new string(' ', amount)}{text}");
        }

    }

    enum Tok
    {
        [Token(Example = "{")]
        LBracket,

        [Token(Example = "}")]
        RBracket,

        [Token(Example = "[")]
        LSquareBracket,

        [Token(Example = "]")]
        RSquareBracket,

        [Token(Example = "(")]
        LParen,

        [Token(Example = ")")]
        RParen,

        [Token(Example = ":")]
        Colon,

        [Token(Example = "=")]
        Equals,

        [Token(Example = ",")]
        Comma,

        [Token(Example = "$")]
        Dollar,

        [Token(Example = "#")]
        Hash,

        String,
        Number,
        Identifier,
    }

    static class Tokenizer
    {
        static TextParser<Unit> StringToken { get; } =
            from open in Character.EqualTo('"')
            from content in Span.EqualTo("\\\"").Value(Unit.Value).Try()
                .Or(Span.EqualTo("\\\\").Value(Unit.Value).Try())
                .Or(Character.Except('"').Value(Unit.Value))
                .IgnoreMany()
            from close in Character.EqualTo('"')
            select Unit.Value;

        static TextParser<Unit> NumberToken { get; } =
            from sign in Character.EqualTo('-').OptionalOrDefault()
            from first in Character.Digit
            from rest in Character.Digit.Or(Character.In('.', 'e', 'E', '+', '-')).IgnoreMany()
            select Unit.Value;

        public static Tokenizer<Tok> Instance { get; } =
            new TokenizerBuilder<Tok>()
             .Ignore(Span.WhiteSpace)
             .Match(Character.EqualTo('{'), Tok.LBracket)
             .Match(Character.EqualTo('}'), Tok.RBracket)
             .Match(Character.EqualTo(':'), Tok.Colon)
             .Match(Character.EqualTo(','), Tok.Comma)
                .Match(Character.EqualTo('='), Tok.Equals)
                .Match(Character.EqualTo('$'), Tok.Dollar)
             .Match(Character.EqualTo('('), Tok.LParen)
             .Match(Character.EqualTo(')'), Tok.RParen)
             .Match(StringToken, Tok.String)
             .Match(NumberToken, Tok.Number, requireDelimiters: true)
             .Match(Identifier.CStyle, Tok.Identifier, requireDelimiters: true)
             .Build();

    }

    static class TextParsers
    {
        public static TextParser<string> String { get; } =
           from open in Character.EqualTo('"')
           from chars in Character.ExceptIn('"', '\\')
               .Or(Character.EqualTo('\\')
                   .IgnoreThen(
                       Character.EqualTo('\\')
                       .Or(Character.EqualTo('"'))
                       .Or(Character.EqualTo('/'))
                       .Or(Character.EqualTo('b').Value('\b'))
                       .Or(Character.EqualTo('f').Value('\f'))
                       .Or(Character.EqualTo('n').Value('\n'))
                       .Or(Character.EqualTo('r').Value('\r'))
                       .Or(Character.EqualTo('t').Value('\t'))
                       .Or(Character.EqualTo('u').IgnoreThen(
                               Span.MatchedBy(Character.HexDigit.Repeat(4))
                                   .Apply(Numerics.HexDigitsUInt32)
                                   .Select(cc => (char)cc)))
                       .Named("escape sequence")))
               .Many()
           from close in Character.EqualTo('"')
           select new string(chars);

        public static TextParser<double> Number { get; } =
            from sign in Character.EqualTo('-').Value(-1.0).OptionalOrDefault(1.0)
            from whole in Numerics.Natural.Select(n => double.Parse(n.ToStringValue()))
            from frac in Character.EqualTo('.')
                .IgnoreThen(Numerics.Natural)
                .Select(n => double.Parse(n.ToStringValue()) * Math.Pow(10, -n.Length))
                .OptionalOrDefault()
            from exp in Character.EqualToIgnoreCase('e')
                .IgnoreThen(Character.EqualTo('+').Value(1.0)
                    .Or(Character.EqualTo('-').Value(-1.0))
                    .OptionalOrDefault(1.0))
                .Then(expsign => Numerics.Natural.Select(n => double.Parse(n.ToStringValue()) * expsign))
                                  .OptionalOrDefault()
            select (whole + frac) * sign * Math.Pow(10, exp);
    }

    static class SpParser
    {
        // For simplicity, we use `object` as the stand-in for every
        // possible JSON value type. There's quite a lot of casting:
        // unfortunately, for performance reasons, Superpower uses a
        // parser design that doesn't allow for variance, so you need
        // to create a parser that returns `object` here, even though
        // one that returns `string` should, in theory, be compatible.
        static TokenListParser<Tok, object> String { get; } =
            Token.EqualTo(Tok.String)
                .Apply(TextParsers.String)
                .Select(s => (object)s);

        static TokenListParser<Tok, object> Number { get; } =
            Token.EqualTo(Tok.Number)
                .Apply(TextParsers.Number)
                .Select(n => (object)n);

        // The grammar is recursive - values can be objects, which contain
        // values, which can be objects... In order to reflect this circularity,
        // the parser below uses `Parse.Ref()` to refer lazily to the `Value`
        // parser, which won't be constructed until after the runtime initializes
        // the `Object` parser.
        static TokenListParser<Tok, object> Object { get; } =
            from begin in Token.EqualTo(Tok.LBracket)
            from properties in String
                .Named("property name")
                .Then(name => Token.EqualTo(Tok.Colon)
                    .IgnoreThen(Parse.Ref(() => Value)
                                .Select(value => KeyValuePair.Create((string)name, value))))
                                //.Select(value => new KeyValuePair<string, object>((string)name, value))))
                .ManyDelimitedBy(Token.EqualTo(Tok.Comma),
                    end: Token.EqualTo(Tok.RBracket))
            select (object)new Dictionary<string, object>(properties);

        // `ManyDelimitedBy()` is a convenience helper for parsing lists that contain
        // separators. Specifying an `end` delimiter improves error reporting by enabling
        // expectations like "expected (item) or (close delimiter)" when no content matches.
        static TokenListParser<Tok, object> Array { get; } =
            from begin in Token.EqualTo(Tok.LSquareBracket)
            from values in Parse.Ref(() => Value)
                .ManyDelimitedBy(Token.EqualTo(Tok.Comma),
                    end: Token.EqualTo(Tok.RSquareBracket))
            select (object)values;

        static TokenListParser<Tok, object> True { get; } =
            Token.EqualToValue(Tok.Identifier, "true").Value((object)true);

        static TokenListParser<Tok, object> False { get; } =
            Token.EqualToValue(Tok.Identifier, "false").Value((object)false);

        static TokenListParser<Tok, object> Null { get; } =
            Token.EqualToValue(Tok.Identifier, "null").Value((object)null);

        static TokenListParser<Tok, object> Value { get; } =
            String
                .Or(Number)
                .Or(Object)
                .Or(Array)
                .Or(True)
                .Or(False)
                .Or(Null)
                .Named("JSON value");

        static TokenListParser<Tok, object> Document { get; } = Value.AtEnd();

        // `TryParse` is just a helper method. It's useful to write one of these, where
        // the tokenization and parsing phases remain distinct, because it's often very
        // handy to place a breakpoint between the two steps to check out what the
        // token list looks like.
        public static bool TryParse(string json, out object value, out string error, out Position errorPosition)
        {
            var tokens = Tokenizer.Instance.TryTokenize(json);
            if (!tokens.HasValue)
            {
                value = null;
                error = tokens.ToString();
                errorPosition = tokens.ErrorPosition;
                return false;
            }

            var parsed = Document.TryParse(tokens.Value);
            if (!parsed.HasValue)
            {
                value = null;
                error = parsed.ToString();
                errorPosition = parsed.ErrorPosition;
                return false;
            }

            value = parsed.Value;
            error = null;
            errorPosition = Position.Empty;
            return true;
        }
    }
}
