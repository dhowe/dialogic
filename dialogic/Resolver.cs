using System;
using System.Linq;
using System.Collections;
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
            if (text.IsNullOrEmpty() || !IsDynamic(text)) return text;

            if (globals.IsNullOrEmpty() && parent == null) return BindGroups(text, parent);

            if (DBUG) Console.WriteLine("Bind: " + text);

            var original = text;
            int depth = 0, maxRecursionDepth = 10;

            do
            {
                if (DBUG) Console.WriteLine("  BindSymbols(" + depth + ") -> " + text);
                text = BindSymbols(text, parent, globals);

                if (DBUG) Console.WriteLine("  BindGroups(" + depth + ") -> " + text);
                text = BindGroups(text, parent);

                if (++depth > maxRecursionDepth) throw new ResolverException
                    ("Bind hit maxRecursionDepth for: " + original);

            } while (IsDynamic(text));

            if (DBUG) Console.WriteLine("  Result: " + text + "\n");

            return text;
        }

        /// <summary>
        /// Iteratively resolve any variables in the specified text 
        /// in the appropriate context
        /// </summary>
        public static string BindSymbols(string text, Chat context,
            IDictionary<string, object> globals)
        {
            int depth = 0, maxRecursionDepth = 10;

            var aliases = new HashSet<string>();

            while (text.Contains(Ch.SYMBOL))
            {
                if (DBUG) Console.WriteLine("BindSymbols: " + text);
                var symbolsUn = ParseSymbols(text);
                var symbols = SortByLength(symbolsUn);
                //var sorted = SortByLength(symbols);
                //foreach (var s in sorted)
                //{
                //    if (!s.bounded)
                //    {
                //        text = text.Replace(Ch.SYMBOL + s.symbol,
                //           Ch.SYMBOL + "{" + s.symbol + '}');
                //    }
                //}
                //Console.WriteLine("  BOUND: " + text);

                foreach (var sym in symbols)
                //foreach (var symbol in sorted)
                {
                    //Console.WriteLine("    SYM: " + sym);
                    var theSymbol = sym.symbol;//BoundedSymbol();
                    var toReplace = sym.text;

                    //bounded ? symbol.BoundedSymbol() : Ch.SYMBOL + theSymbol;
                    //symbol.alias == null ?
                    //                Ch.SYMBOL + theSymbol : symbol.text;//BoundedText();

                    //// if its an alias we can wait for the next iteration
                    //Console.WriteLine("    Aliases.HAS? " + symbol.symbol);
                    //if (aliases.Contains(symbol.symbol)) {
                    //    text = text.Replace('$'+symbol.BoundedSymbol(),symbol.BoundedSymbol());
                    //    continue;    
                    //}

                    BindToContext(ref theSymbol, ref context);
                    var symval = ResolveSymbol(theSymbol, context, globals);

                    // if we have an alias, then include it in our resolved 
                    // value so that it can be resolved correctly in BindGroups
                    if (sym.alias != null)
                    {
                        //symval = Ch.OSAVE + symbol.alias + Ch.EQ + symval + Ch.CSAVE;
                        //aliases.Add(symbol.alias);
                        //Console.WriteLine("    ALIASES: "+aliases.Stringify());
                    }

                    //Console.WriteLine("    pre='"+text+"'");
                    if (DBUG) Console.Write("    " + text + ".Replace(" + toReplace + "," + symval + ")");
                    text = text.Replace(toReplace, symval);
                    if (DBUG) Console.WriteLine("   -> '" + text + "'");
                }

                if (++depth >= maxRecursionDepth) throw new ResolverException
                    ("BindSymbols hit maxRecursionDepth for: " + text);

            }

            //foreach (var alias in aliases)
            //{
            //    text = text.Replace("{" + alias + "}", "${" + alias + "}");
            //}
            return Html.Decode(text);
        }

        /// <summary>
        /// Iteratively resolve any groups in the specified text 
        /// in the appropriate context, creating and caching Resolution
        /// objects as necessary
        /// </summary>
        public static string BindGroups(string text, Chat context = null)
        {
            if (!String.IsNullOrEmpty(text))
            {
                int depth = 0, maxRecursionDepth = 10;
                var original = text;
                while (text.Contains(Ch.OR))
                {
                    if (++depth > maxRecursionDepth) throw new ResolverException
                        ("BindGroups hit maxRecursionDepth for: " + original);

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
                        text = text.ReplaceFirst(opt, pick);
                    }
                }
            }

            return text;///HandleSymbolAlias(text, context);
        }

        /// <summary>
        /// Handles Chat-scoping of variables by updating symbol name and switching to specified context
        /// </summary>
        internal static void BindToContext(ref string symbol, ref Chat context)
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
            }
        }

        /// <summary>
        /// First do local lookup from Chat context, then if not found, try global lookup
        /// </summary>
        private static string ResolveSymbol(string symbol, Chat context, IDictionary<string, object> globals)
        {
            //var cstr = "$" + symbol + "\n    globals: " + globals.Stringify();
            //if (context != null) cstr += "\n    chat#" + context.text + ":" + context.scope.Stringify();
            //Console.WriteLine("  ResolveSymbol:" + cstr);

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

        internal class Symbol
        {
            public string text, alias, symbol;
            public bool bounded = false;

            public Symbol(Match match = null)
            {
                if (match != null)
                {
                    if (match.Groups.Count != 3) throw new ArgumentException
                        ("Bad match: " + match.Groups.Count);
                    Init(match.Groups[0].Value, match.Groups[2].Value, match.Groups[1].Value);
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

            public string BoundedSymbol()
            {
                return !bounded ? '{' + symbol + '}' : symbol;
            }

            public override string ToString()
            {
                var s = "[$" + (bounded ? '{' + symbol + '}' : symbol) + " text='";
                return s + text + "'" + (alias != null ? " alias=" + alias : "") + "]";
            }

            internal string BoundedText()
            {
                var dollarSym = Ch.SYMBOL + symbol;
                return !bounded ? text.Replace(dollarSym, "${" + symbol + '}') : text;
            }
        }

        internal static List<Symbol> ParseSymbols(string text, bool showMatches = false)
        {
            List<Symbol> symbols = new List<Symbol>();
            var matches = RE.ParseVars.Matches(text);

            if (matches.Count == 0 && text.Contains(Ch.SYMBOL))
            {
                throw new ResolverException("Unable to parse symbol: " + text);
            }

            foreach (Match match in matches)
            {
                Symbol s = new Symbol(match);
                //Console.WriteLine(s);
                symbols.Add(s);
            }

            // OPT: we sort here to avoid symbols which are substrings of another
            // symbol causing incorrect replacements ($a being replaced in $ant, 
            // for example), however this can be avoided by correctly using 
            // Regex.Replace instead of String.Replace() in BindSymbols
            return symbols;

            //return symbols;
        }

        internal static List<Symbol> SortByLength(IEnumerable<Symbol> syms)
        {
            return (from s in syms orderby s.symbol.Length descending select s).ToList();
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

        private static bool IsDynamic(string text)
        {
            return text != null &&
                (text.IndexOf('|') > -1 || text.IndexOf('$') > -1);
        }

        private static string HandleSymbolAlias(string text, Chat context)
        {
            var match = RE.SaveState.Match(text);

            if (match.Groups.Count == 3)
            {
                string symbol = match.Groups[1].Value,
                    value = match.Groups[2].Value;

                AssignOp.EQ.Invoke(symbol, value, context.scope);
                Console.WriteLine("REPLACE: " + match.Groups[0].Value + " with " + value);
                //Console.WriteLine("    Scope -> " + context.scope.Stringify());
                text = text.Replace(match.Groups[0].Value, value);
                //Console.WriteLine("    Saved -> " + text);
            }

            return text;
        }

    }

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

        public string BoundedSymbol()
        {
            return !bounded ? '{' + symbol + '}' : symbol;
        }

        public override string ToString()
        {
            var s = "[$" + (bounded ? '{' + symbol + '}' : symbol) + " text='";
            return s + text + "'" + (alias != null ? " alias=" + alias : "") + "]";
        }

        internal string BoundedText()
        {
            var dollarSym = Ch.SYMBOL + symbol;
            return !bounded ? text.Replace(dollarSym, "${" + symbol + '}') : text;
        }
    }
}