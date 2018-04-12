using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dialogic
{
    /// <summary>
    /// Handles realization of variables, probabilistic groups, and grammar rules
    /// </summary>
    public class Realizer
    {
        //private ChatRuntime runtime;

        //internal Realizer(ChatRuntime rt)
        //{
        //    this.runtime = rt;
        //}

        public static string Do(string text, Chat parent, IDictionary<string, object> globals)
        {
            var DBUG = false;

            if (text.IsNullOrEmpty() || !IsDynamic(text)) return text;

            if (globals.IsNullOrEmpty()) return RealizeGroups(text);

            if (DBUG) Console.WriteLine("Do#0.0: " + text + " " + globals.Stringify());

            var original = text;
            int iterations = 0, maxIterations = 2;

            do
            {
                text = RealizeSymbols(text, parent, globals);
                if (DBUG) Console.WriteLine("Do#" + iterations + ".1: " + text);

                text = RealizeGroups(text);
                if (DBUG) Console.WriteLine("Do#" + iterations + ".2: " + text);

                if (++iterations > maxIterations) throw new RealizeException
                    ("Infinite loop in realizer: " + original);

            } while (IsDynamic(text));

            //Console.WriteLine("  " + original + " -> " + text);

            return text;
        }

        public static string RealizeSymbols(string text, Chat context, IDictionary<string, object> globals)
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

        public static string RealizeGroups(string text)
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

        //public string DoVarsOld(string text, IDictionary<string, object>
        //    globals, Chat parent = null)
        //{
        //    var DBUG = true;

        //    if (text == null || !text.Contains('$')) return text;

        //    int iterations = 0, maxIterations = 5;
        //    var locals = parent != null ? parent.locals : null;

        //    if (DBUG) Console.WriteLine("DoVars: " + text);

        //    //var vars = new List<string>();
        //    while (text.Contains('$'))
        //    {
        //        var vars = ParseVars(text, parent);

        //        if (DBUG) Console.WriteLine("  vars: " + vars.Stringify());

        //        vars.ForEach(v =>
        //        {
        //            string replaced = null;

        //            if (locals != null && locals.ContainsKey(v))
        //            {
        //                replaced = text.Replace("$" + v, locals[v].ToString());
        //            }

        //            if (replaced == null && globals.ContainsKey(v))
        //            {
        //                replaced = text.Replace("$" + v, globals[v].ToString());
        //            }

        //            if (replaced == null) throw new RealizeException
        //                ("Unable to replace '$" + v + "' in: \nglobals: " + globals.Stringify()
        //                 + (parent != null ? "\nlocals: " + parent.locals.Stringify() : ""));

        //            text = replaced;
        //        });

        //        if (DBUG) Console.WriteLine("  " + (iterations + 1) + "] post: " + text);

        //        if (text.Length >= 500 || ++iterations >= maxIterations)
        //        {
        //            Console.WriteLine("[WARN] Max recursion depth reached("
        //                + maxIterations + "/" + text.Length + "): '" + text + "'");
        //            break;
        //        }

        //    }

        //    return text;
        //}

        /*private List<string> ParseVars(string text, Chat context)
        {
            Console.WriteLine("ParseVars("+text+", in:"+context.text+")");
            List<string> vars = new List<string>();
            var matches = RE.ParseVars.Matches(text);
            foreach (Match match in matches)
            {
                if (match.Groups.Count != 2)
                    throw new DialogicException("Bad RE in " + text);

                var v = match.Groups[1].Value;
                if (v.Contains('.'))
                {
                    var parts = v.Split('.');
                    if (parts.Length > 2)
                    {
                        throw new RealizeException("Unexpected variable format: " + v);
                    }
                    var chat = runtime.FindChatByLabel(parts[0]);
                    if (chat == null)
                    {
                        throw new RealizeException("No Chat found with label: #" + parts[0]);
                    }
                    if (chat.locals.ContainsKey(parts[1]))
                    {
                        v = chat.locals[parts[1]].ToString();
                        var subvars = ParseVars(v, chat);
                        if (subvars.Count < 1)
                        {
                            vars.Add(v);
                            continue;
                        }
                    }
                    else
                    {
                        throw new RealizeException("Chat #" + chat.text + ".locals does not contain " + v + "\nlocals:" + chat.locals.Stringify());
                    }
                }
                Console.WriteLine("ParseVars.Add("+v+")");
                vars.Add(v);
            }
            return vars;
        }*/

        /*public string DoVarsOldest(string text, IDictionary<string, object>
            globals, Chat parent = null)
        {
            var DBUG = false;

            if (!String.IsNullOrEmpty(text))
            {
                if (DBUG) Console.WriteLine("DoVars: " + text + " " + globals.Stringify());

                string original = text;
                int recursions = 0, maxRecursionDepth = 9;

                while (text.IndexOf('$') > -1 && text == original)
                {
                    string check = null;
                    if (parent != null) // first try locals
                    {
                        check = ReplaceVars(text, parent.locals);
                    }

                    if (check == null) // then try globals
                    {
                        if (parent != null)
                        {
                            Console.WriteLine("Failed to match variable '" + text
                                + "' in locals:\n  " + parent.locals.Stringify());
                        }
                        else
                        {
                            Console.WriteLine("NULL PARENT! " + text);
                        }

                        check = ReplaceVars(text, globals);

                        if (check == null) throw new RealizeException
                            ("Failed to match variable '" + text
                             + "' in globals:\n  " + globals.Stringify() + (parent != null
                             ? "\n" + parent.locals.Stringify() : string.Empty));
                    }
                    text = check;

                    //if (DBUG) Console.WriteLine("#" + recursions + ": " + text);

                    if (++recursions > maxRecursionDepth)
                    {
                        throw new RealizeException("Possible infinite recursion" +
                            " in DoVars(" + original + ", " + globals.Stringify() + ")");
                    }
                }
            }

            return text;
        }*/


        ////////////////////////////////////////////////////////////////////////


        // attempt to replace each global in the string, starting with the longest first
        //private string ReplaceVars(string text, IDictionary<string, object> lookup)
        //{
        //    IEnumerable sorted = null; // OPT: cache these sorts ?

        //    if (text.IndexOf('$') > -1)
        //    {
        //        var original = text;

        //        // do replacements in order of length, longest first
        //        if (sorted == null) sorted = Util.SortByLength(lookup.Keys);

        //        var matched = false;
        //        foreach (string s in sorted)
        //        {
        //            var tmp = text.Replace("$" + s, lookup[s].ToString());
        //            if (tmp != text)
        //            {
        //                matched = true;
        //                text = tmp;
        //                //Console.WriteLine("ReplaceGlobals(" + s + ")=" + tmp);
        //            }
        //        }

        //        // return null if text hasn't matched anything
        //        if (!matched) return null;
        //    }
        //    return text;
        //}

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