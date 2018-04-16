using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Dialogic
{
    static class Modifiers
    {
        /// <summary>
        /// Capitalize the first character.
        /// </summary>
        /// <param name="str"></param>
        /// <returns>The modified string</returns>
        public static string Capitalize(string str)
        {
            return char.ToUpper(str[0]) + str.Substring(1);
        }

        /// <summary>
        /// Capitalizes every character.
        /// </summary>
        /// <param name="str"></param>
        /// <returns>The modified string</returns>
        public static string AllCaps(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str);
        }

        /// <summary>
        /// Wraps the given string in double-quotes.
        /// </summary>
        /// <param name="str"></param>
        /// <returns>The modified string</returns>
        public static string Quotify(string str)
        {
            return "\"" + str + "\"";
        }
    }

    /// <summary>
    /// Handles realization of variables, probabilistic groups, and grammar rules
    /// </summary>
    public static class Resolver
    {
        internal static Dictionary<string, Func<string, string>> ModifierLookup =
            new Dictionary<string, Func<string, string>>
           {
               { "quotify",       Modifiers.Quotify },
               { "capitalize",    Modifiers.Quotify },
               { "allCaps",       Modifiers.AllCaps }
           };

        /// <summary>
        /// Iteratively resolve any variables or groups in the specified text 
        /// in the appropriate context
        /// </summary>
        public static string Bind(string text, Chat parent, IDictionary<string, object> globals)
        {
            var DBUG = false;

            if (text.IsNullOrEmpty() || !IsDynamic(text)) return text;

            if (globals.IsNullOrEmpty() && parent == null) return BindGroups(text);

            if (DBUG) Console.WriteLine("Do#0.0: " + text + " " + globals.Stringify());

            var original = text;
            int depth = 0, maxRecursionDepth = 10;

            do
            {
                text = BindSymbols(text, parent, globals);
                if (DBUG) Console.WriteLine("Do#" + depth + ".1: " + text);

                text = BindGroups(text);
                if (DBUG) Console.WriteLine("Do#" + depth + ".2: " + text);

                if (++depth > maxRecursionDepth) throw new ResolverException
                    ("Bind hit maxRecursionDepth for: " + original);

            } while (IsDynamic(text));

            //Console.WriteLine("  " + original + " -> " + text);

            return text;
        }

        /// <summary>
        /// Handles Chat-scoping of variables by updating symbol name and switching to specified context
        /// </summary>
        internal static void BindToContext(ref string symbol, ref Chat context)
        {
            if (symbol.Contains('.'))
            {
                if (context == null) throw new ResolverException
                    ("Null context for chat-scoped symbol: " + symbol);

                // need to process a chat-scoped symbol
                var parts = symbol.Split('.');
                if (parts.Length > 2)
                {
                    throw new ResolverException("Unexpected variable format: " + symbol);
                }

                var chat = context.runtime.FindChatByLabel(parts[0]);
                if (chat == null)
                {
                    throw new ResolverException("No Chat found with label #" + parts[0]);
                }

                symbol = parts[1];
                context = chat;
            }
        }

        /// <summary>
        /// Iteratively resolve any variables in the specified text 
        /// in the appropriate context
        /// </summary>
        public static string BindSymbols(string text, Chat context, IDictionary<string, object> globals)
        {
            int depth = 0, maxRecursionDepth = 10;

            while (text.Contains(Defaults.SYMBOL))
            {
                foreach (var symbol in ParseSymbols(text))
                {
                    var dollarsym = Defaults.SYMBOL + symbol;

                    var theSymbol = symbol;
                    BindToContext(ref theSymbol, ref context);
                    text = text.Replace(dollarsym, ResolveSymbol(theSymbol, context, globals));
                }

                if (++depth >= maxRecursionDepth) throw new ResolverException
                    ("BindSymbols hit maxRecursionDepth for: " + text);

            }
            return text;
        }

        /// <summary>
        /// Iteratively resolve any groups in the specified text 
        /// in the appropriate context, creating and caching Resolution
        /// objects as necessary
        /// </summary>
        public static string BindGroups(string text)
        {
            var DBUG = false;

            if (!String.IsNullOrEmpty(text))
            {
                if (DBUG) Console.WriteLine("DoGroups: " + text);

                int depth = 0, maxRecursionDepth = 10;
                var original = text;
                while (text.Contains('|'))
                {
                    if (!(text.Contains('(') && text.Contains(')')))
                    {
                        text = '(' + text + ')';
                        Console.WriteLine("[WARN] RealizeGroups added parens to: " + text);
                    }

                    List<string> groups = new List<string>();
                    ParseGroups(text, groups);

                    if (DBUG) Console.WriteLine("  groups: " + groups.Stringify());

                    foreach (var opt in groups)
                    {
                        var pick = Resolution.Choose(opt);
                        text = text.ReplaceFirst(opt, pick);
                    }

                    if (++depth > maxRecursionDepth) throw new ResolverException
                        ("BindGroups hit maxRecursionDepth for: " + original);

                }
            }

            return text;
        }

        /// <summary>
        /// First do local lookup from Chat context, then if not found, try global lookup
        /// </summary>
        private static string ResolveSymbol(string symbol, Chat context, IDictionary<string, object> globals)
        {
            //Console.WriteLine("RealizeSymbol: "+symbol+" in chat#"+(context!=null?context.text:"null"));

            if (context != null && context.scope.ContainsKey(symbol)) // check locals
            {
                return context.scope[symbol].ToString();
            }

            if (globals != null && globals.ContainsKey(symbol))   // check globals
            {
                return globals[symbol].ToString();
            }

            throw new UnboundSymbolException(symbol, context, globals);
        }

        private static IEnumerable<string> ParseSymbols(string text)
        {
            List<string> symbols = new List<string>();
            var matches = RE.ParseVars.Matches(text);
            if (matches.Count == 0) return symbols;

            foreach (Match match in matches)
            {
                if (match.Groups.Count != 2)
                    throw new DialogicException("Bad RE in " + text);

                var symbol = match.Groups[1].Value;

                // Q: do we want to allow multiple identical symbols here?
                // if (!symbols.Contains(symbol)) 
                symbols.Add(symbol);
            }

            // OPT: we sort here to avoid symbols which are substrings of another
            // symbol causing incorrect replacements ($a being replaced in $ant, 
            // for example), however this can be avoided by correctly using Regex.Replace
            // instead of String.Replace() in ResolveSymbols below
            return Util.SortByLength(symbols);
        }

        // TODO: redo iteratively?
        private static void ParseGroups(string input, List<string> results)
        {
            foreach (Match m in RE.MatchParens.Matches(input))
            {
                var expr = m.Value.Substring(1, m.Value.Length - 2);

                if (Regex.IsMatch(expr, @"[\(\)]")) // TODO: compile
                {
                    ParseGroups(expr, results);
                }
                else
                {
                    results.Add(m.Value);
                }
            }
        }

        private static bool IsDynamic(string text)
        {
            return text != null &&
                (text.IndexOf('|') > -1 || text.IndexOf('$') > -1);
        }

    }
}