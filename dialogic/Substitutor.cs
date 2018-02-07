using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dialogic
{
    public static class Substitutor
    {
        const string MATCH_PARENS = @"\(([^()]+|(?<Level>\()|(?<-Level>\)))+(?(Level)(?!))\)";

        public static void ReplaceGroups(ref string input, Command c = null)
        {
            while (input != null && Regex.IsMatch(input, @"[\(\)]"))
            {
                List<string> result = new List<string>();
                ParseGroups(input, result);
                foreach (var opt in result)
                {
                    var pick = DoReplace(opt);
                    input = input.Replace(opt, pick);
                    if (c != null) Console.WriteLine("-> "+opt+" -> "+pick+"\n"+input);
                }
            }
        }

        public static void Replace(ref string text, Dictionary<string, object> globals) {
            ReplaceGroups(ref text);
            ReplaceVars(ref text, globals);
        }
    
        public static void ReplaceVars(ref string text, Dictionary<string, object> globals)
        {
            if (!string.IsNullOrEmpty(text))
            {
                foreach (string s in Util.SortByLength(globals.Keys))
                {
                    //System.Console.WriteLine($"s=${s} -> {globals[s]}"); 
                    text = text.Replace("$" + s, globals[s].ToString());
                }
            }
        }

        private static string DoReplace(string sub)
        {
            if (!Regex.IsMatch(sub,@"\([^)]+|[^)]+\)")) throw InvalidState(sub);
  
            sub = sub.Substring(1, sub.Length - 2);
            string[] opts = Regex.Split(sub, @"\s*\|\s*");

            if (opts.Length != 2) throw InvalidState(sub);

            return (new Random().Next(0, 2) == 0) ? opts[0] : opts[1];
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
                Substitutor.ReplaceGroups(ref s);
                Substitutor.ReplaceVars(ref s, globals);
                System.Console.WriteLine(i + ") " +s);
            }
        }
    }
}