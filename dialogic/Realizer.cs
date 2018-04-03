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
        public static string DoGroups(string text)
        {
            if (!String.IsNullOrEmpty(text))
            {
                while (text.IndexOf('|') > -1)
                {
                    List<string> result = new List<string>();
                    ParseGroups(text, result);
                    foreach (var opt in result)
                    {
                        var pick = DoReplace(opt);
                        text = text.Replace(opt, pick);
                    }
                }
            }

            return text;
        }

        public static string DoVars(string text, IDictionary<string, object> globals)
        {
            if (!String.IsNullOrEmpty(text))
            {
                IEnumerable sorted = null; // TODO: cache these sorts ?

                if (text.IndexOf('$') > -1)
                {
                    if (sorted == null) sorted = Util.SortByLength(globals.Keys);
                    foreach (string s in sorted)
                    {
                        text = text.Replace("$" + s, globals[s].ToString());
                    }
                }
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

        public static string Do(string text, IDictionary<string, object> globals)
        {
            if (IsDynamic(text))
            {
                if (globals.IsNullOrEmpty())
                {
                    return DoGroups(text);
                }

                int tries = 0;
                string arg = text;

                do // rethink this
                {
                    text = DoVars(text, globals);
                    text = DoGroups(text);

                    // bail on infinite loops:
                    if (++tries > 1000) throw new RealizeException
                        ("Invalid-Sub: '" + arg + "' " + globals.Stringify());
                }
                while (IsDynamic(text));

                //Console.WriteLine("Realizer.Do: " + arg + " -> " + text);
            }

            return text;
        }

        private static bool IsDynamic(string text)
        {
            return (!String.IsNullOrEmpty(text)) &&
                (text.IndexOf('|') > -1 || text.IndexOf('$') > -1);
        }

        private static Exception InvalidState(string sub)
        {
            return new Exception("Invalid State: '" + sub + "'");
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