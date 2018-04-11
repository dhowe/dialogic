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
        private int maxIterations = 99;
        private ChatRuntime runtime;

        internal Realizer(ChatRuntime rt)
        {
            this.runtime = rt;
        }

        public string Do(string text, IDictionary<string, object> globals, Chat parent = null)
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

        public string DoGroups(string text)
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

        public string DoVars(string text, IDictionary<string, object>
            globals, Chat parent = null)
        {
            if (String.IsNullOrEmpty(text) || !text.Contains('$'))
            {
                return text;
            }

            var locals = parent != null ? parent.locals : null;

            int recursions = 0, maxRecursions = 3;
            List<string> vars = ParseVars(text);

            while (text.Contains('$'))
            {
                vars.ForEach(v =>
                {
                    string replaced = null;

                    if (locals != null && locals.ContainsKey(v))
                    {
                        replaced = text.Replace("$" + v, locals[v].ToString());
                    }

                    if (replaced == null && globals.ContainsKey(v))
                    {
                        replaced = text.Replace("$" + v, globals[v].ToString());
                    }

                    if (replaced == null) throw new RealizeException
                        ("Unable to replace $" + v + " in: \n  " + globals.Stringify() +
                         (parent != null ? "\n  " + parent.locals.Stringify() : ""));

                    text = replaced;
                });

                //if (replaced == null) throw new RealizeException
                //("Unable to replace " + vars.Stringify() + " in " + text);

                if (++recursions >= maxRecursions)
                {
                    Console.WriteLine("[WARN] Max recursion level" +
                        " reached(" + maxRecursions + "): '" + text + "'");
                    break;
                }
            }

            //if (replaced == null) throw new RealizeException
            //("Unable to replace " + vars.Stringify() + " in " + text);

            return text;
        }

        private List<string> ParseVars(string text)
        {
            var matches = RE.ParseVars.Matches(text);
            var vars = new List<string>();
            foreach (Match match in matches)
            {
                if (match.Groups.Count != 2)
                    throw new DialogicException("Bad RE in " + text);
                vars.Add(match.Groups[1].Value);
            }
            return vars;
        }

        public string DoVarsOrig(string text, IDictionary<string, object>
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

                        /*if (parent != null)
                        {
                            check = text.Replace("$", '$' + parent.text + '.');
                            if (DBUG) Console.WriteLine("DoVars failed to match variable " +
                                "in '" + original + "' trying with parent: '" + check + "'");

                            check = ReplaceGlobals(check, globals);
                        }*/

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
        }


        ////////////////////////////////////////////////////////////////////////


        // attempt to replace each global in the string, starting with the longest first
        private string ReplaceVars(string text, IDictionary<string, object> lookup)
        {
            IEnumerable sorted = null; // OPT: cache these sorts ?

            if (text.IndexOf('$') > -1)
            {
                var original = text;

                // do replacements in order of length, longest first
                if (sorted == null) sorted = Util.SortByLength(lookup.Keys);

                var matched = false;
                foreach (string s in sorted)
                {
                    var tmp = text.Replace("$" + s, lookup[s].ToString());
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

        private string DoReplace(string sub)
        {
            if (!Regex.IsMatch(sub, @"\([^)]+|[^)]+\)")) throw InvalidState(sub);

            sub = sub.Substring(1, sub.Length - 2);
            string[] opts = Regex.Split(sub, @"\s*\|\s*");

            if (opts.Length < 2) throw InvalidState(sub);

            return (string)Util.RandItem(opts);
        }

        private bool IsDynamic(string text)
        {
            return text != null &&
                (text.IndexOf('|') > -1 || text.IndexOf('$') > -1);
        }

        private Exception InvalidState(string sub)
        {
            return new RealizeException("Invalid State: '" + sub + "'");
        }

        private void ParseGroups(string input, List<string> results)
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