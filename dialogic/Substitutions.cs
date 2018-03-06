using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dialogic
{
    public static class Substitutions
    {
public static void DoMeta(IDictionary<string, object> meta, IDictionary<string, object> globals)
        {
            if (Util.IsNullOrEmpty(meta) || Util.IsNullOrEmpty(globals)) return;

            IEnumerable sorted = null;

            var keys = new List<string>(meta.Keys);

            for (int i = 0; i < keys.Count; i++)
            {
                string val = meta[keys[i]].ToString();
                if (val.IndexOf('$') > -1)
                {
                    if (sorted == null) sorted = Util.SortByLength(globals.Keys);
                    foreach (string s in sorted)
                    {
                        val = val.Replace("$" + s, globals[s].ToString());  
                        meta[keys[i]] = val;
                    }
                }
            }
        }

        public static void Do(ref string text, IDictionary<string, object> globals)
        {
            if (String.IsNullOrEmpty(text)) return;

            if (Util.IsNullOrEmpty(globals)) {
                DoGroups(ref text);
                return;
            }

            int tries = 0;
            string orig = text;
            while (text.IndexOf('$') > -1 || text.IndexOf('|') > -1) // rethink
            {
                DoVars(ref text, globals);
                DoGroups(ref text);

                // bail on infinite loops:
                if (++tries > 1000) throw new Exception("Invalid-Sub: '"
                    + orig + "' " + Util.Stringify(globals));
            }
        }

        public static void DoGroups(ref string text, Command c = null)
        {
            if (String.IsNullOrEmpty(text)) return;

            while (text.IndexOf('|') > -1)
            {
                List<string> result = new List<string>();
                ParseGroups(text, result);
                foreach (var opt in result)
                {
                    var pick = DoReplace(opt);
                    text = text.Replace(opt, pick);
                    //if (c != null) Console.WriteLine("-> " + opt + " -> " + pick + "\n" + text);
                }
            }
        }

        public static void DoVars(ref string text, IDictionary<string, object> globals)
        {
            if (String.IsNullOrEmpty(text)) return;

            IEnumerable sorted = null;

            if (text.IndexOf('$') > -1)
            {
                if (sorted == null) sorted = Util.SortByLength(globals.Keys);
                foreach (string s in sorted)
                {
                    text = text.Replace("$" + s, globals[s].ToString());
                }
            }
        }

        private static string DoReplace(string sub)
        {
            if (!Regex.IsMatch(sub, @"\([^)]+|[^)]+\)")) throw InvalidState(sub);

            sub = sub.Substring(1, sub.Length - 2);
            string[] opts = Regex.Split(sub, @"\s*\|\s*");

            if (opts.Length < 2) throw InvalidState(sub);

            return (string)Util.RandItem(opts);
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

        public static void MainOff(string[] args)
        {
            var globals = new Dictionary<string, object>() {
                { "animal", "dog" },
                { "place", "Istanbul" },
                { "Happy", "HappyFlip" },
                { "prep", "then" },
            };
            // Handle subs, variable subs, and nested subs
            var input = @"SAY The ($animal | $Happy) (walked | (ran | (talked|fell))) and $prep (ate|slept)";
            for (int i = 0; i < 15; i++)
            {
                string s = String.Copy(input);
                Substitutions.Do(ref s, globals);
                System.Console.WriteLine(i + ") " + s);
            }
        }
    }
}