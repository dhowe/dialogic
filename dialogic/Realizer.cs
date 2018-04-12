using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dialogic
{
    /// <summary>
    /// Handles realization of variables, probabilistic groups, and grammar rules
    /// </summary>
    public static class Realizer
    {
        //private ChatRuntime runtime;
        //internal Realizer(ChatRuntime rt) {
        //    this.runtime = rt;
        //}

        public static string Resolve(string text, Chat parent, IDictionary<string, object> globals)
        {
            var DBUG = false;

            if (text.IsNullOrEmpty() || !IsDynamic(text)) return text;

            if (globals.IsNullOrEmpty()) return ResolveGroups(text);

            if (DBUG) Console.WriteLine("Do#0.0: " + text + " " + globals.Stringify());

            var original = text;
            int iterations = 0, maxIterations = 2;

            do
            {
                text = ResolveSymbols(text, parent, globals);
                if (DBUG) Console.WriteLine("Do#" + iterations + ".1: " + text);

                text = ResolveGroups(text);
                if (DBUG) Console.WriteLine("Do#" + iterations + ".2: " + text);

                if (++iterations > maxIterations) throw new RealizeException
                    ("Infinite loop in realizer: " + original);

            } while (IsDynamic(text));

            //Console.WriteLine("  " + original + " -> " + text);

            return text;
        }

        public static string ResolveSymbols(string text, Chat context, IDictionary<string, object> globals)
        {
            int iterations = 0, maxIterations = 5;
            while (text.Contains('$'))
            {
                var matches = RE.ParseVars.Matches(text);
                if (matches.Count == 0) return text;

                // we have some variables to deal with 
                List<string> symbols = new List<string>();
                foreach (Match match in matches)
                {
                    if (match.Groups.Count != 2)
                        throw new DialogicException("Bad RE in " + text);

                    var symbol = match.Groups[1].Value;

                    if (symbol.Contains('.'))
                    {
                        if (context == null) throw new RealizeException
                            ("Null context for chat-scoped symbol: " + symbol);

                        // need to process a chat-scoped symbol
                        var parts = symbol.Split('.');
                        if (parts.Length > 2)
                        {
                            throw new RealizeException("Unexpected variable format: " + symbol);
                        }
                        var chat = context.runtime.FindChatByLabel(parts[0]);
                        if (chat == null)
                        {
                            throw new RealizeException("No Chat found with label #" + parts[0]);
                        }

                        // use the new context from now on, or until another switch
                        context = chat; 

                        text = text.Replace('$' + symbol, RealizeSymbol(parts[1], context, globals));
                    }
                    else // process a local or global-scoped symbol
                    {
                        text = text.Replace('$' + symbol, RealizeSymbol(symbol, context, globals));
                    }
                }

                if (++iterations >= maxIterations) throw new RealizeException
                    ("Max recursion depth hit for: " + text);

            }
            return text;
        }

        public static string ResolveGroups(string text)
        {
            var DBUG = false;

            if (!String.IsNullOrEmpty(text))
            {
                if (DBUG) Console.WriteLine("DoGroups: " + text);

                int iterations = 0, maxIterations = 2;
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
                        var pick = DoReplace(opt);
                        text = text.Replace(opt, pick);
                    }

                    if (++iterations > maxIterations) throw new RealizeException
                        ("DoGroups: Possible infinite loop in " + original);
                }
            }

            return text;
        }


        /// <summary>
        /// First do local lookup from Chat context, then if not found, try global lookup
        /// </summary>
        private static string RealizeSymbol(string symbol, Chat context, IDictionary<string, object> globals)
        {
            //Console.WriteLine("RealizeSymbol: "+symbol+" in chat#"+(context!=null?context.text:"null"));

            // check locals
            if (context != null && context.locals.ContainsKey(symbol))
            {
                return context.locals[symbol].ToString();
            }

            // check globals
            if (globals.ContainsKey(symbol)) return globals[symbol].ToString();

            var cstr = "Unable to realize symbol: '$" + symbol + "'\nglobals: " + globals.Stringify();
            if (context != null) cstr += "\nchat#" + context.text + ".locals:" + context.locals.Stringify();
            throw new RealizeException(cstr);
        }

        private static string DoReplace(string sub)
        {
            if (!Regex.IsMatch(sub, @"\([^)]+|[^)]+\)")) throw InvalidState(sub);

            sub = sub.Substring(1, sub.Length - 2);
            string[] opts = Regex.Split(sub, @"\s*\|\s*");

            if (opts.Length < 2) throw InvalidState(sub);

            return (string)Util.RandItem(opts);
        }

        private static bool IsDynamic(string text)
        {
            return text != null &&
                (text.IndexOf('|') > -1 || text.IndexOf('$') > -1);
        }

        private static Exception InvalidState(string sub)
        {
            return new RealizeException("Invalid State: '" + sub + "'");
        }

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

    }
}