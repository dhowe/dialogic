using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dialogic
{
    public static class Substitutions
    {
        const string MATCH_PARENS = @"\(([^()]+|(?<Level>\()|(?<-Level>\)))+(?(Level)(?!))\)";

        public static void Do(ref string text, Dictionary<string, object> globals)
        {
            if (String.IsNullOrEmpty(text)) return;

            int tries = 0;
            while (text.Contains("$") || text.Contains("|"))
            {
                DoVars(ref text, globals);
                DoGroups(ref text);

                // bail on infinite loops
                if (++tries > 1000) throw new Exception("Illegal Subs: '" + text + "'");
            }
        }

        public static void DoGroups(ref string input, Command c = null)
        {
            while (input != null && Regex.IsMatch(input, @"[\(\)]"))
            {
                List<string> result = new List<string>();
                ParseGroups(input, result);
                foreach (var opt in result)
                {
                    var pick = DoReplace(opt);
                    input = input.Replace(opt, pick);
                    if (c != null) Console.WriteLine("-> " + opt + " -> " + pick + "\n" + input);
                }
            }
        }

        public static void DoVars(ref string text, Dictionary<string, object> globals)
        {
            var otext = text;
            if (!string.IsNullOrEmpty(text))
            {
                foreach (string s in Util.SortByLength(globals.Keys))
                {
                    text = text.Replace("$" + s, globals[s].ToString());
                }
            }
            //Console.WriteLine("Subs.checking: " + otext + " -> "+text);
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
            foreach (Match m in new Regex(MATCH_PARENS).Matches(input))
            {
                var expr = m.Value.Substring(1, m.Value.Length - 2);

                if (Regex.IsMatch(expr, @"[\(\)]"))
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