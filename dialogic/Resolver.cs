using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Dialogic
{
    /// <summary>
    /// Handles resolution of variables, probabilistic groups, and grammar rules
    /// </summary>
    public static class Resolver
    {
        public static bool DBUG = false;

        /// <summary>
        /// Iteratively resolve any variables or groups in the specified text 
        /// in the appropriate context
        /// </summary>
        public static string Bind(string text, Chat parent, IDictionary<string, object> globals)
        {
            if (text.IsNullOrEmpty() || !IsDynamic(text)) return text;

            if (DBUG) Console.WriteLine("------------------------\nBind: " + Info(text, parent));

            var original = text;
            int depth = 0, maxRecursionDepth = Defaults.BIND_MAX_DEPTH;

            do
            {
                // resolve any symbols in the input
                text = BindSymbols(text, parent, globals, depth);

                // resolve any groups in the input
                text = BindGroups(text, parent, depth);

                // throw if we've hit max recursion depth
                if (++depth > maxRecursionDepth)
                {
                    if (text.Contains(Ch.SYMBOL) || text.Contains(Ch.LABEL))
                    {
                        var symbols = Symbol.Parse(text, parent);
                        if (!symbols.IsNullOrEmpty())
                        {
                            if (parent.runtime.strictMode) throw new UnboundSymbol
                                (symbols[0], parent, globals);
                            
                            Console.WriteLine("[WARN] Unbound symbol: " 
                                + symbols[0] + " globals: " + globals.Stringify());
                        }
                    }
                    if (parent.runtime.strictMode) throw new BindException
                        ("Resolver hit maxRecursionDepth for: " + original);

                    break;
                }

            } while (IsDynamic(text));

            if (DBUG) Console.WriteLine("Result: " + text + "\n");

            return Html.Decode(text); // resolve any encoded entities
        }

        private static string Info(string text, Chat parent)
        {
            return text + " :: " + (parent == null ? "{}" :
                parent.text + parent.scope.Stringify());
        }

        ///// <summary>
        ///// Iteratively resolve any variables in the text 
        ///// via the appropriate context
        ///// </summary>
        public static string BindSymbols(string text, Chat context,
            IDictionary<string, object> globals, int level = 0)
        {
            if (DBUG) Console.WriteLine("  Symbols(" + level + "): " + Info(text, context));

            var symbols = Symbol.Parse(text, context);
            while (symbols.Count > 0)
            {
                if (DBUG) Console.WriteLine("    Found: " + symbols.Stringify());

                var symbol = symbols.Pop();

                var result = symbol.Resolve(globals);
                if (result != null)
                {
                    string pretext = text, toReplace = symbol.text;

                    text = text.Replace(symbol.text, result);

                    if (DBUG) Console.WriteLine("      " + symbol.SymbolText() + " -> " + result);

                    if (pretext != text && text.Contains(Ch.SYMBOL))
                    {
                        // repeat if we've made progress but still have symbols
                        symbols = Symbol.Parse(text, symbol.context);
                        continue;
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
        public static string BindGroups(string text, Chat context = null, int level = 0)
        {
            if (text.Contains(Ch.OR))
            {
                if (DBUG) Console.WriteLine("  Groups(" + level + "): " + Info(text, context));

                var original = text;

                if (!(text.Contains(Ch.OGROUP) && text.Contains(Ch.CGROUP)))
                {
                    text = Ch.OGROUP + text + Ch.CGROUP;
                    Console.WriteLine("[WARN] BindGroups added parens to: " + text);
                }

                var choices = Choice.Parse(text, context);

                if (DBUG) Console.WriteLine("    Found: " + choices.Stringify());

                foreach (var choice in choices)
                {
                    var pick = choice.Resolve();
                    if (DBUG) Console.WriteLine("      " + choice + " -> " + pick);
                    text = text.ReplaceFirst(choice.Text(), pick);
                }
            }

            return text;
        }

        /// <summary>
        /// Handles Chat-scoping of variables by updating symbol name and switching to specified context
        /// </summary>
        internal static bool ContextSwitch(ref string symbol, ref Chat context) // TODO: replace with Symbol
        {
            if (symbol.Contains(Ch.SCOPE))
            {
                if (context == null) throw new BindException
                    ("Null context for chat-scoped symbol: " + symbol);

                // need to process a chat-scoped symbol
                var parts = symbol.Split(Ch.SCOPE);
                if (parts.Length > 2) throw new BindException
                    ("Unexpected variable format: " + symbol);

                var chat = context.runtime.FindChatByLabel(parts[0]);
                if (chat == null) throw new BindException
                    ("No Chat found with label #" + parts[0]);

                symbol = parts[1];
                context = chat;

                return true;
            }

            return false;
        }

        private static bool IsDynamic(string text)
        {
            return text != null && text.Contains(Ch.OR, Ch.SYMBOL, Ch.LABEL);
        }
    }
}