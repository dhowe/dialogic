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
        private List<Transform> trans = new List<Transform>();

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

            string pretext, original = text;
            int depth = 0, maxRecursionDepth = Defaults.BIND_MAX_DEPTH;

            do
            {
                pretext = text;

                // resolve any symbols in the input
                text = BindSymbols(text, context, globals, depth);

                // resolve any groups in the input
                text = BindChoices(text, context, depth);

                // throw if we've hit max recursion depth
                if (++depth > maxRecursionDepth)
                {
                    HandleFailure(ref text, context, globals);
                    break;
                }

            } while (text != pretext && IsDynamic(text));

            // if we still have dynamics, we've failed
            if (IsDynamic(text)) HandleFailure(ref text, context, globals);

            // no more dynamics, now handle transforms         
            text = BindTransforms(text, context);

            if (DBUG) Console.WriteLine("Result(pre): " + text);

            text = PostProcess(text);

            if (DBUG) Console.WriteLine("Result(post): " + text + "\n");

            return text;
        }

        ///// <summary>
        ///// Iteratively resolve any variables in the text 
        ///// via the appropriate context
        ///// </summary>
        public string BindSymbols(string text, Chat context,
            IDictionary<string, object> globals, int level = 0)
        {
            if (DBUG) Console.WriteLine("  Symbols(" + level + ") " + text);// + Info(text, context));

            var original = text;

            ParseSymbols(text, context);
            while (symbols.Count > 0)
            {
                if (DBUG) Console.WriteLine("    Found: " + symbols.Stringify());

                var symbol = symbols.Pop();
                if (DBUG) Console.WriteLine("    Pop:    " + symbol);

                // resolve the symbol and replace it in the text
                var result = symbol.Resolve(globals, false); 
                text = symbol.Replace(text, result, globals);

                if (DBUG) Console.WriteLine("      " + symbol.Name() 
                    + " -> " + result + " :: " + text);

                // if we've made progress, and have no more symbols in our list, 
                // but still have symbols in the text, then re-parse and repeat
                if (symbols.Count == 0 && original != text && text.Contains(Ch.SYMBOL))
                {
                    ParseSymbols(text, context);
                    original = text;
                }
            }

            return text;
        }

        /// <summary>
        /// Iteratively resolve any groups in the specified text 
        /// in the appropriate context, creating and caching Resolution
        /// objects as necessary
        /// </summary>
        public string BindChoices(string text, Chat context = null, int level = 0)
        {
            if (text.Contains(Ch.OR))
            {
                if (DBUG) Console.WriteLine("  Choices(" + level + ") " + text);// + Info(text, context));

                var original = text;
                ParseChoices(text, context);

                if (DBUG) Console.WriteLine("    Found: " + choices.Stringify());

                foreach (var choice in choices)
                {
                    // resolve the group and replace it in the text
                    var result = choice.Resolve();
                    text = choice.Replace(text, result);

                    if (DBUG) Console.WriteLine("      " + choice + " -> " + result);
                }
            }

            return text;
        }

        /// <summary>
        /// Iteratively resolve any transforms in the specified text 
        /// </summary>
        private string BindTransforms(string text, Chat context)
        {
            //if (DBUG) Console.WriteLine("  Transforms(" + level + ") " + text);
            //if (text.ContainsAny(Ch.OR, Ch.SYMBOL)) return text; // remove?

            if (DBUG) Console.WriteLine("  Trans() " + text);

            var original = text;

            ParseTransforms(text, context);
            if (DBUG) Console.WriteLine("    Found: " + trans.Stringify());

            foreach (var tran in trans)
            {
                var result = tran.Resolve();
                text = tran.Replace(text, result);

                if (DBUG) Console.WriteLine("      " + tran + " -> " + result);
            }

            return text;
        }

        private string PostProcess(string text)
        {
            // keep () to display unbound functions
            text = text.Replace("()", "&lpar;&rpar;");

            // replace literal quotes & grouping operators
            text = RE.ResolvePost.Replace(text, string.Empty);

            // replace multiple spaces with single
            text = RE.MultiSpace.Replace(text, " ");

            return Entities.Decode(text.Trim());
        }

        private void HandleFailure(ref string text, Chat context, IDictionary<string, object> globals)
        {
            if (text.Contains(Ch.SYMBOL))
            {
                ParseSymbols(text, context);

                if (symbols.Any(s => !s.transforms.IsNullOrEmpty()))
                {
                    text = text.Replace("()", "&lpar;&rpar;"); // needed?
                }

                if (symbols.Count < 1) throw new BindException
                    ("Invalid symbol: " + text);

                symbols[0].OnBindSymbolError(symbols[0].name, globals);
            }
            else if (text.Contains(Ch.LABEL))
            {
                throw new BindException("Invalid label: " + text);
            }
        }

        private void ParseTransforms(string text, Chat context)
        {
            trans.Clear();
            Transform.Parse(trans, text, context);
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