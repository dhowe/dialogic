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
        internal static int maxIterations = 99;

        public static string Do(string text, IDictionary<string, object> globals, Chat parent = null)
        {
            var DBUG = false;

            if (text.IsNullOrEmpty() || !IsDynamic(text)) return text;

            if (globals.IsNullOrEmpty()) return DoGroups(text);

            if (DBUG) Console.WriteLine("Do#0.0: " + text + " " + globals.Stringify());

            var original = text;
            var iterations = 0;

            do
            {
                text = DoVars(text, globals, parent);
                if (DBUG) Console.WriteLine("Do#" + iterations + ".1: " + text);

                text = DoGroups(text);
                if (DBUG) Console.WriteLine("Do#" + iterations + ".2: " + text);

                if (++iterations > maxIterations) throw new RealizeException
                    ("Infinite loop in realizer: " + original);

            } while (IsDynamic(text));

            //Console.WriteLine("  " + original + " -> " + text);

            return text;
        }

        public static string DoGroups(string text)
        {
            var DBUG = false;

            if (!String.IsNullOrEmpty(text))
            {
                if (DBUG) Console.WriteLine("DoGroups: " + text);

                var iterations = 0;
                var original = text;
                while (text.IndexOf('|') > -1)
                {
                    if (text.IndexOf('(') < 0 || text.IndexOf('(') < 0)
                    {
                        text = '(' + text + ')';
                        Console.WriteLine("[WARN] DoGroups added parens to: " + text);
                    }

                    List<string> result = new List<string>();
                    ParseGroups(text, result);
                    foreach (var opt in result)
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

        public static string DoVars(string text, IDictionary<string, object>
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
                    var check = ReplaceGlobals(text, globals);
                    if (check == null)
                    {
                        if (parent != null)
                        {
                            check = text.Replace("$", '$' + parent.text + '.');
                            if (DBUG) Console.WriteLine("DoVars failed to match variable " +
                                "in '" + original + "' trying with parent: '" + check + "'");

                            check = ReplaceGlobals(check, globals);
                        }

                        if (check == null) throw new RealizeException
                            ("Failed to match variable '" + text
                                + "' in globals:\n  " + globals.Stringify());
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
        }


        ////////////////////////////////////////////////////////////////////////


        // attempt to replace each global in the string, starting with the longest first
        private static string ReplaceGlobals(string text, IDictionary<string, object> globals)
        {
            IEnumerable sorted = null; // OPT: cache these sorts ?

            if (text.IndexOf('$') > -1)
            {
                var original = text;

                // do replacements in order of length, longest first
                if (sorted == null) sorted = Util.SortByLength(globals.Keys);

                var matched = false;
                foreach (string s in sorted)
                {
                    var tmp = text.Replace("$" + s, globals[s].ToString());
                    if (tmp != text)
                    {
                        matched = true;
                        text = tmp;
                        //Console.WriteLine("ReplaceGlobals(" + s + ")=" + tmp);
                    }
                }

                // return null if text hasn't matched anything
                if (!matched) return null;
            }
            return text;
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