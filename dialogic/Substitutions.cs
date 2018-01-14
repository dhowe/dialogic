using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dialogic
{
    public static class Substitutions
    {
        public static void Main(string[] args)
        {
            var input = @"SAY (a | (b | c)) and (d|e)";

            List<string> result = new List<string>();
            Substitutions.Parse(input, result);
            foreach (var g in result)
            {
                System.Console.WriteLine($"-> {g}");
            }
        }

        public static void Parse(string input, List<string> results)
        {
            Regex regex = new Regex(@"
                \(                    # Match (
                (
                    [^()]+            # all chars except ()
                    | (?<Level>\()    # or if ( then Level += 1
                    | (?<-Level>\))   # or if ) then Level -= 1
                )+                    # Repeat (to go from inside to outside)
                (?(Level)(?!))        # zero-width negative lookahead assertion
                \)                    # Match )",
                RegexOptions.IgnorePatternWhitespace);

            foreach (Match m in regex.Matches(input))
            {
                var expr = m.Value.Substring(1, m.Value.Length - 2);
                results.Add(expr);
                if (Regex.IsMatch(expr, @"[\(\)]"))
                {
                    Parse(expr, results);
                }

            }
        }
    }
}