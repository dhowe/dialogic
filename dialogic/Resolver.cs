using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Dialogic
{
    /// <summary>
    /// Handles resolution of variables, probabilistic groups, and grammar rules
    /// </summary>
    public static class Resolver
    {
        public static bool DBUG = false;

        internal static IDictionary<string, Func<string, string>> ModifierLookup =
            new Dictionary<string, Func<string, string>>
           {
               { "quotify",       Modifier.Quotify },
               { "capitalize",    Modifier.Capitalize},
               { "allCaps",       Modifier.AllCaps }
           };

        /// <summary>
        /// Iteratively resolve any variables or groups in the specified text 
        /// in the appropriate context
        /// </summary>
        public static string Bind(string text, Chat parent, IDictionary<string, object> globals)
        {
            if (text.IsNullOrEmpty() || !IsDynamic(text)) return text;

            if (DBUG) Console.WriteLine("--------------------------------\nBind: " + text);

            var original = text;
            int depth = 0, maxRecursionDepth = Defaults.BIND_MAX_DEPTH;

            do
            {
                text = BindSymbols(text, parent, globals, depth);

                text = BindGroups(text, parent, depth);

                if (++depth > maxRecursionDepth) // bail if hit max depth
                {
                    if (text.Contains('$'))
                    {
                        var symbols = ParseSymbols(text, false);
                        if (!symbols.IsNullOrEmpty()) throw new UnboundSymbolException
                            (symbols[0].symbol, parent, globals);
                    }
                    throw new ResolverException
                        ("Resolver hit maxRecursionDepth for: " + original);
                }

            } while (IsDynamic(text));

            if (DBUG) Console.WriteLine("Result: " + text + "\n");

            return text;
        }

        /// <summary>
        /// Iteratively resolve any variables in the specified text 
        /// in the appropriate context
        /// </summary>
        public static string BindSymbols(string text, Chat context,
            IDictionary<string, object> globals, int level = 0)
        {
            var doRepeat = false;
            do
            {
                var symbols = ParseSymbols(text, true);

                if (DBUG)
                {
                    var s = "  BindSymbols(" + level + ") -> " + text;
                    s += "\n  Symbols(" + symbols.Count + "): ";
                    symbols.ForEach(sym => s += "$" + sym.symbol);
                    Console.WriteLine(s);
                }

                doRepeat = false;
                foreach (var sym in symbols)
                {
                    string theSymbol = sym.symbol, toReplace = sym.text;

                    // check if we need to do a context switch
                    var switched = ContextSwitch(ref theSymbol, ref context);

                    // lookup the value for the symbol
                    var symval = ResolveSymbol(theSymbol, context, globals);

                    if (symval != null)
                    {
                        // if we have an alias, then include it in our resolved 
                        // value so that it can be handled properly in BindGroups
                        if (sym.alias != null) toReplace = sym.SymbolText();

                        // if we've switched contexts and still have replacements
                        // to do, we need to repeat (better as recursive call?)
                        if (switched && symval.Contains(Ch.SYMBOL)) doRepeat = true;

                        // do the symbol replacement
                        text = text.Replace(toReplace, symval);

                        if (DBUG)
                        {
                            Console.WriteLine("    " + toReplace + " -> '" + symval + "'");
                            if (doRepeat) Console.WriteLine
                                ("   Repeat with context=#" + context.text + " -> " + text);
                        }
                    }
                }

            } while (doRepeat);


            return Html.Decode(text);
        }

        /// <summary>
        /// Iteratively resolve any groups in the specified text 
        /// in the appropriate context, creating and caching Resolution
        /// objects as necessary
        /// </summary>
        public static string BindGroups(string text, Chat context = null, int level = 0)
        {
            if (text.Contains(Ch.OR))
            {
                if (DBUG) Console.WriteLine("  BindSymbols(" + level + ") -> " + text);

                var original = text;

                if (!(text.Contains(Ch.OGROUP) && text.Contains(Ch.CGROUP)))
                {
                    text = Ch.OGROUP + text + Ch.CGROUP;
                    Console.WriteLine("[WARN] BindGroups added parens to: " + text);
                }

                List<string> groups = new List<string>();
                ParseGroups(text, groups);

                if (DBUG) Console.WriteLine("    Groups: " + groups.Stringify());

                foreach (var opt in groups)
                {
                    var pick = Resolution.Choose(opt);
                    if (DBUG) Console.WriteLine("      " + opt + " -> " + pick);
                    text = text.ReplaceFirst(opt, pick);
                }

                var match = RE.ParseAlias.Match(text);
                if (match != null && match.Groups.Count == 3)
                {
                    var full = match.Groups[0].Value;
                    var choice = match.Groups[2].Value;

                    // store the alias as a symbol in current scope
                    context.scope[match.Groups[1].Value] = choice;

                    // now do the replacement
                    text = text.ReplaceFirst(full, choice);

                    if (DBUG) Console.WriteLine("      " + full + " -> " + choice
                        + "\n      scope: " + context.scope.Stringify());
                }
            }

            return text;
        }

        /// <summary>
        /// Handles Chat-scoping of variables by updating symbol name and switching to specified context
        /// </summary>
        internal static bool ContextSwitch(ref string symbol, ref Chat context)
        {
            if (symbol.Contains(Ch.SCOPE))
            {
                if (context == null) throw new ResolverException
                    ("Null context for chat-scoped symbol: " + symbol);

                // need to process a chat-scoped symbol
                var parts = symbol.Split(Ch.SCOPE);
                if (parts.Length > 2) throw new ResolverException
                    ("Unexpected variable format: " + symbol);

                var chat = context.runtime.FindChatByLabel(parts[0]);
                if (chat == null) throw new ResolverException
                    ("No Chat found with label #" + parts[0]);

                symbol = parts[1];
                context = chat;

                return true;
            }

            return false;
        }

        /// <summary>
        /// First do local lookup from Chat context, then if not found, try global lookup
        /// </summary>
        private static string ResolveSymbol(string symbol, Chat context,
            IDictionary<string, object> globals)
        {
            string result = null;
            if (context != null && context.scope.ContainsKey(symbol)) // check locals
            {
                result = context.scope[symbol].ToString();
            }
            else if (globals != null && globals.ContainsKey(symbol))   // check globals
            {
                result = globals[symbol].ToString();
            }
            return result;
        }

        internal static List<Symbol> ParseSymbols(string text, bool sortResults = false)
        {
            List<Symbol> symbols = new List<Symbol>();
            var matches = RE.ParseVars.Matches(text);

            if (matches.Count == 0 && text.Contains(Ch.SYMBOL))
            {
                throw new ResolverException("Unable to parse symbol: " + text);
            }

            foreach (Match match in matches)
            {
                symbols.Add(new Symbol(match));
            }

            // OPT: we sort here to avoid symbols which are substrings of another
            // symbol causing incorrect replacements ($a being replaced in $ant, 
            // for example), however this can be avoided by correctly using 
            // Regex.Replace instead of String.Replace() in BindSymbols
            return sortResults ? SortByLength(symbols) : symbols;
        }

        private static void ParseGroups(string input, List<string> results)
        {
            foreach (Match m in RE.MatchParens.Matches(input))
            {
                var expr = m.Value.Substring(1, m.Value.Length - 2);

                if (RE.HasParens.IsMatch(expr))
                {
                    ParseGroups(expr, results);
                }
                else
                {
                    results.Add(m.Value);
                }
            }
        }

        private static List<Symbol> SortByLength(IEnumerable<Symbol> syms)
        {
            return (from s in syms orderby s.symbol.Length descending select s).ToList();
        }

        private static bool IsDynamic(string text)
        {
            return text != null && (text.Contains(Ch.OR) || text.Contains(Ch.SYMBOL));
        }

    }

    static class Modifier
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

    internal class Symbol
    {
        public string text, alias, symbol;
        public bool bounded = false;

        public Symbol(Match match = null)
        {
            if (match != null)
            {
                if (match.Groups.Count != 4)
                {
                    Util.ShowMatch(match);
                    throw new ArgumentException("Bad match: " + match.Groups.Count);
                }
                Init(match.Groups[1].Value, match.Groups[3].Value, match.Groups[2].Value);
            }
        }

        public Symbol Init(string txt, string sym, string save = "")
        {
            this.text = txt.Trim();
            this.symbol = sym.Trim();
            this.alias = save.Length > 0 ? save.Trim() : null;
            this.bounded = text.Contains('{') && text.Contains('}');
            //Console.WriteLine("SYM"+this);
            return this;
        }

        public override string ToString()
        {
            return SymbolText() + (text != null ? " text='" + text +
                "'" : "") + (alias != null ? " alias=" + alias : "") + "]";
        }

        //public string BoundedSymbol()
        //{
        //    return !bounded ? '{' + symbol + '}' : symbol;
        //}

        //internal string BoundedText()
        //{
        //    var dollarSym = Ch.SYMBOL + symbol;
        //    return !bounded ? text.Replace(dollarSym, "${" + symbol + '}') : text;
        //}

        internal string SymbolText()
        {
            return "$" + (bounded ? "{" + symbol + '}' : symbol);
        }
    }
}