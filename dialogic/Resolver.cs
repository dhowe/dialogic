using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dialogic
{
    /// <summary>
    /// Handles resolution of symbols, probabilistic groups, transforms, and grammar production
    /// </summary>
    public class Resolver
    {
        public static bool DBUG = false;

        private List<Symbol> symbols = new List<Symbol>();
        private List<Choice> choices = new List<Choice>();
        private ChatRuntime chatRuntime;

        public Resolver(ChatRuntime chatRuntime)
        {
            this.chatRuntime = chatRuntime;
        }

        /// <summary>
        /// Iteratively resolve any variables or groups in the specified text 
        /// in the appropriate context
        /// </summary>
        public string Bind(string text, Chat context, IDictionary<string, object> globals)
        {
            if (text.IsNullOrEmpty() || !IsDynamic(text)) return text;
            
            if (DBUG) Console.WriteLine("------------------------\nBind: " + Info(text, context));

            var original = text;
            int depth = 0, maxRecursionDepth = Defaults.BIND_MAX_DEPTH;

            do
            {
                // resolve any symbols in the input
                text = BindSymbols(text, context, globals, depth);

                // resolve any groups in the input
                text = BindGroups(text, context, depth);

                // throw if we've hit max recursion depth
                if (++depth > maxRecursionDepth)
                {
                    if (text.Contains(Ch.SYMBOL) || text.Contains(Ch.LABEL))
                    {
                        ParseSymbols(text, context);
                        if (!symbols.IsNullOrEmpty())
                        {
                            symbols[0].OnBindError(globals);
                            //throw new UnboundSymbol(symbols[0], parent, globals);
                            ChatRuntime.Warn("Unbound symbol: " + symbols[0]);
                        }
                    }
                    break;
                }

            } while (IsDynamic(text));

    
            if (DBUG) Console.WriteLine("Result: " + text + "\n");

            return PostProcess(text);
        }

		private string PostProcess(string text)
        {
            // replace any literal quotation marks
            text = text.Replace("\"", string.Empty);

            // replace extra grouping operators
            text = text.Replace("(", string.Empty);
            text = text.Replace(")", string.Empty);

            // replace multiple spaces with single
            text = RE.MultiSpace.Replace(text, " ");

            return Entities.Decode(text);
        }

        ///// <summary>
        ///// Iteratively resolve any variables in the text 
        ///// via the appropriate context
        ///// </summary>
        public string BindSymbols(string text, Chat context,
            IDictionary<string, object> globals, int level = 0)
        {
            if (DBUG) Console.WriteLine("  Symbols(" + level + ") "+text);// + Info(text, context));

            ParseSymbols(text, context);
            while (symbols.Count > 0)
            {
                if (DBUG) Console.WriteLine("    Found: " + symbols.Stringify());

                var symbol = symbols.Pop();
                if (DBUG) Console.WriteLine("    Pop:    " + symbol);

                string pretext = text;

                var result = symbol.Resolve(globals);

                if (DBUG) Console.WriteLine("      " + symbol.Name() + " -> " + result);

                if (result != null)
                {
                    if (result.Contains(Ch.OR) && !(result.StartsWith(Ch.OGROUP) && result.EndsWith(Ch.CGROUP))) // 
                    {
                        result = Ch.OGROUP + result + Ch.CGROUP;
                        if (DBUG) Console.WriteLine("      ***PARS: " + result);
                    }
                    text = symbol.Replace(text, result);

                    if (pretext != text) // progress?
                    {
                        // repeat if still have symbols
                        if (text.Contains(Ch.SYMBOL))
                        {
                            ParseSymbols(text, context);
                            continue;
                        }
                    }
                }
            }

            return text;
        }

        /// <summary>
        /// Iteratively resolve any groups in the specified text 
        /// in the appropriate context, creating and caching Resolution
        /// objects as necessary
        /// </summary>
        public string BindGroups(string text, Chat context = null, int level = 0)
        {
            if (text.Contains(Ch.OR))
            {
                if (DBUG) Console.WriteLine("  Groups(" + level + ") "+text);// + Info(text, context));

                var original = text;

                //if (!(text.Contains(Ch.OGROUP) && text.Contains(Ch.CGROUP)))
                //{
                //    text = Ch.OGROUP + text + Ch.CGROUP;
                //    ChatRuntime.Warn("BindGroups added parens to: " + text);
                //}

                ParseChoices(text, context);
                if (DBUG) Console.WriteLine("    Found: " + choices.Stringify());
                foreach (var choice in choices)
                {
                    var result = choice.Resolve(); // handles transforms
                    if (DBUG) Console.WriteLine("      " + choice + " -> " + result);
                    if (result != null) text = choice.Replace(text, result);
                }
            }

            return text;
        }

        private void ParseSymbols(string text, Chat context)
        {
            symbols.Clear();
            Symbol.Parse(symbols, text, context);
        }

        private void ParseChoices(string text, Chat context)
        {
            choices.Clear();
            Choice.Parse(choices, text, context);
        }

        private static bool IsDynamic(string text)
        {
            return text != null && (text.Contains("()")
                || text.ContainsAny(Ch.OR, Ch.SYMBOL, Ch.LABEL));
        }

        private static string Info(string text, Chat parent)
        {
            return text + " :: " + (parent == null ? "{}" :
                parent.text + " " + parent.scope.Stringify());
        }
    }
}