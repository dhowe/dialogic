﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Dialogic
{
    public static class Properties
    {
        static IDictionary<Type, IDictionary<string, PropertyInfo>> Cache;

        static Properties()
        {
            Cache = new Dictionary<Type, IDictionary<string, PropertyInfo>>();
        }

        internal static IDictionary<string, PropertyInfo> Lookup(Type type)
        {
            var dbug = false;
            if (!Cache.ContainsKey(type))
            {
                var propMap = new Dictionary<string, PropertyInfo>();

                var props = type.GetProperties(BindingFlags.Instance
                    | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

                if (dbug) Console.Write(type.Name + "[");
                foreach (var pi in props)
                {
                    propMap.Add(pi.Name, pi);
                    if (dbug) Console.Write(pi.Name + ",");
                }
                if (dbug) Console.WriteLine("]");
                Cache[type] = propMap;
            }

            return Cache[type];
        }

        internal static bool Set(Object target, string property, object value, bool onlyIfExists = false)
        {
            var lookup = Lookup(target.GetType());

            if (lookup != null && lookup.ContainsKey(property))
            {
                var pinfo = lookup[property];
                value = Util.ConvertTo(pinfo.PropertyType, value);
                pinfo.SetValue(target, value, null);
                return true;
            }

            if (!onlyIfExists) throw new BindException("Invalid Set: " + property);

            return false;
        }

        internal static object Get(Object target, string property, object defaultVal = null)
        {
            var lookup = Lookup(target.GetType());

            if (lookup != null && lookup.ContainsKey(property))
            {
                var pinfo = lookup[property];
                var value = pinfo.GetValue(target);
                return Util.ConvertTo(pinfo.PropertyType, value);
            }

            if (defaultVal != null) return defaultVal;

            throw new BindException("Invalid Get: " + property);
        }
    }

    public static class Methods
    {
        static IDictionary<Type, IDictionary<string, MethodInfo>> Cache;

        static Methods()
        {
            Cache = new Dictionary<Type, IDictionary<string, MethodInfo>>();
        }

        public static object Invoke(object target, string methodName, object[] args = null)
        {
            var type = target.GetType();
            if (!Cache.ContainsKey(type))
            {
                Cache[type] = new Dictionary<string, MethodInfo>();
            }

            MethodInfo method = null;
            var key = CacheKey(methodName, args);

            if (!Cache[type].ContainsKey(key))
            {
                method = type.GetMethod(methodName, ArgsToTypes(args));
                if (method == null && target is string)
                {
                    return InvokeExt(target, methodName);
                }

                if (method == null) throw new BindException
                    ("Invoke -> no method " + type + "." + key);

                Cache[type][key] = method;
            }

            method = Cache[type][key];

            return method.IsStatic ? method.Invoke(null, args) :
                         method.Invoke(target, args);
        }

        internal static object InvokeExt(object target, string methodName)
        {
            var type = typeof(Modifiers);
            if (!Cache.ContainsKey(type))
            {
                Cache[type] = new Dictionary<string, MethodInfo>();
            }

            var args = new[] { target };
            var key = CacheKey(methodName, args);

            if (!Cache[type].ContainsKey(key))
            {
                var method = type.GetMethod(methodName, ArgsToTypes(args));
                if (method == null) throw new BindException
                    ("InvokeExt -> no method " + type + "." + key);
                
                Cache[type][key] = method;
            }

            return Cache[type][key].Invoke(null, args);
        }

        internal static Type[] ArgsToTypes(object[] args)
        {
            if (args == null) return new Type[0];
            return Array.ConvertAll(args, o => o.GetType());
        }

        private static string CacheKey(string methodName, object[] args = null)
        {
            return methodName + ArgsToTypeString(args);
        }

        private static string ArgsToTypeString(object[] args)
        {
            return ArgsToTypes(args).Aggregate(string.Empty,
                (a, b) => a + Ch.LABEL + b);
        }
    }

    internal class Choice
    {
        public string alias;
        private string text, modifier;
        public readonly string[] options;

        private string lastResolved;
        private Chat context;

        private Choice(Chat context, string text, string groups, string alias = null, string method = null)
        {
            this.text = text;
            this.context = context;
            this.modifier = method; 
            this.options = RE.SplitOr.Split(groups);
            this.alias = alias.IsNullOrEmpty() ? null : alias;
        }

        public string Text()
        {
            return text + Modifier();
        }

        private string Modifier()
        {
            return modifier.IsNullOrEmpty() ? string.Empty : '.'+modifier + "()";
        }

        public static List<Choice> Parse(string input, Chat context)
        {
            if (context == null || context.runtime == null)
            {
                Console.WriteLine("[WARN] Choice.Parse got null context/runtime: " + input);
            }
            var groups = new List<Choice>();
            Parse(input, groups, context);
            return groups;
        }

        private static void Parse(string input, List<Choice> results, Chat context)
        {
            if (context == null || context.runtime == null)
            {
                Console.WriteLine("[WARN] Symbol.Parse got null context/runtime: " + input);
            }

            // Cache the entire match?
            foreach (Match m in RE.MatchParens.Matches(input))
            {
                //Util.ShowMatch(m);   // TODO: redo this

                var full = m.Groups[0].Value;
                var alias = m.Groups[1].Value;
                var method = m.Groups[3].Value;
      
                if (!method.IsNullOrEmpty()) {
                    var didx = full.LastIndexOf('.');
                    full = full.Substring(0, didx);
                }

                //Console.WriteLine("full:"+full+" alias="+alias+" method="+method);

                var oidx = full.IndexOf(Ch.OGROUP);
                var cidx = full.LastIndexOf(Ch.CGROUP);
                var expr = full.Substring(oidx + 1, cidx - oidx - 1);

                if (RE.HasParens.IsMatch(expr))
                {
                    Parse(expr, results, context);
                }
                else
                {
                    if (CacheEnabled(context))
                    {
                        var cache = context.runtime.choiceCache;

                        // cache our prior Choice objects here
                        var choiceKey = context.text + Ch.LABEL + full;
                        if (!cache.ContainsKey(choiceKey))
                        {
                            cache.Add(choiceKey, new Choice(context, full, expr, alias, method));
                        }
                        //else Console.WriteLine("CACHE-HIT: "+choiceKey);
                        results.Add(cache[choiceKey]);
                    }
                    else  // no cache
                    {
                        results.Add(new Choice(context, full, expr, alias, method));
                    }
                }
            }
        }

        internal static bool CacheEnabled(Chat context)
        {
            return context != null && context.runtime != null
                && context.runtime.choiceCache != null;
        }

        internal string Resolve()
        {
            string resolved = null;

            switch (options.Length)
            {
                // degenerate single option case
                case 1:
                    resolved = options[0];
                    break;

                // avoid oscillators, just choose random
                case 2:
                    resolved = (string)Util.RandItem(options);
                    break;

                // otherwise choose differently than last time
                default:
                    int iterations = 0, maxIterations = 100;
                    do
                    {
                        if (++iterations > maxIterations) // should never happen
                        {
                            throw new BindException("Max limit: " + this);
                        }
                        resolved = (string)Util.RandItem(options);
                    }
                    while (Equals(lastResolved, resolved));
                    break;
            }

            HandleAlias(resolved, context);
            lastResolved = resolved;

            //Console.WriteLine("last="+lastResolved+" res="+resolved);
            return HandleModifier(resolved);
        }

        private string HandleModifier(string resolved)
        {
            var res = modifier.IsNullOrEmpty() ? resolved : 
                Methods.Invoke(resolved, modifier, null).Stringify();

            //Console.WriteLine("HandleModifier: " + resolved+ " -> "+res);

            return res;
        }

        private void HandleAlias(object resolved, Chat ctx)
        {
            var scope = ctx != null ? ctx.scope : null;
            if (this.alias != null && resolved != null)
            {
                if (scope == null) throw new BindException("Null runtime/context: " + this);

                if (!resolved.ToString().Contains(Ch.OR))
                {
                    //Console.WriteLine("      Choice.Push: " + alias + "=" 
                    //+ resolved +" "+ctx.text + scope.Stringify()));
                    scope[alias] = resolved;
                }
            }
        }

        public override string ToString()
        {
            return Text();
        }
    }

    internal class Symbol
    {
        public string text, alias, symbol;
        public bool bounded, chatScoped;
        public Chat context;

        private Symbol(Chat context, params string[] parts) :
            this(context, parts[0], parts[3], parts[1], parts[2])
        { }

        private Symbol(Chat context, string theText, string theSymbol,
            string alias = null, string typeChar = null)
        {
            this.context = context;
            this.text = theText.Trim();
            this.symbol = theSymbol.Trim();
            this.alias = alias.IsNullOrEmpty() ? null : alias.Trim();
            this.bounded = text.Contains(Ch.OBOUND) && text.Contains(Ch.CBOUND);
            this.chatScoped = (typeChar == Ch.LABEL.ToString());
        }

        public override string ToString()
        {
            var s = SymbolText();
            if (text != s) s += " text='" + text + "'";
            return s += (alias != null ? " alias=" + alias : "");
        }

        internal string SymbolText()
        {
            return (chatScoped ? Ch.LABEL : Ch.SYMBOL)
                + (bounded ? "{" + symbol + '}' : symbol);
        }

        public static List<Symbol> Parse(string text, Chat context)
        {
            var symbols = new List<Symbol>();
            var matches = GetMatches(text);

            foreach (Match match in matches)
            {
                GroupCollection groups = GetGroups(match, 5);

                // Create a new Symbol and add it to the result
                var args = groups.Values().Skip(1).ToArray();
                symbols.Add(new Symbol(context, args));
            }

            // OPT: we can sort here to avoid symbols which are substrings of other
            // symbols causing incorrect replacements ($a being replaced in $ant, 
            // for example), however should be avoided by using Regex.Replace 
            // instead of String.Replace() in BindSymbols
            return SortByLength(symbols);
        }

        internal string Resolve(IDictionary<string, object> globals)
        {
            string[] parts = symbol.Split(Ch.SCOPE);

            if (parts.Length == 1 && chatScoped) throw new BindException
                ("Illegally-scoped variable: " + this);

            object resolved = ResolveSymbol(parts[0], context, globals);
            switch (this.Type())
            {
                case SymbolType.SIMPLE:

                    HandleAlias(resolved, globals);
                    break;

                case SymbolType.GLOBAL_SCOPE:

                    // Dynamically resolve the object path 
                    for (int i = 1; i < parts.Length; i++)
                    {
                        if (parts[i].EndsWith(Ch.CGROUP))
                        {
                            var func = parts[i].Replace("()", "");
                            if (resolved.ToString().Contains(Ch.OR))
                            {
                                // delay the method call until fully resolved
                                resolved = resolved + "." + parts[i];
                            }
                            else
                            {   // nothing more to resolve, so invoke
                                resolved = Methods.Invoke(resolved, func, null);
                            }
                            // TODO: handle other signatures
                        }
                        else
                        {
                            resolved = Properties.Get(resolved, parts[i]);
                        }

                        if (resolved == null) throw new UnboundSymbol
                            (symbol, context, globals);

                        HandleAlias(resolved, globals);
                    }
                    break;

                case SymbolType.CHAT_SCOPE:

                    if (context == null || context.runtime == null)
                    {
                        throw new BindException("Null context/runtime: " + this);
                    }

                    // Find/store the correct scope for the lookup
                    context = context.runtime.FindChatByLabel(parts[0]);
                    resolved = ResolveSymbol(parts[1], context, globals);
                    HandleAlias(resolved, context.scope);
                    break;
            }

            if (resolved != null)
            {
                var result = resolved.ToString();

                // if we have an alias, but the replacement is not fully resolved
                // then we keep the alias in the text for later resolution
                if (alias != null && result.Contains(Ch.OR, Ch.SYMBOL))
                {
                    result = Ch.OSAVE + alias + Ch.EQ + result + Ch.CSAVE;
                }

                return result;
            }

            return null;
        }

        private void HandleAlias(object resolved, IDictionary<string, object> scope)
        {
            if (this.alias != null && resolved != null)
            {
                if (!resolved.ToString().Contains(Ch.OR))
                {
                    //Console.WriteLine("      Symbol.Push: $" + alias + "=" + resolved);
                    scope[alias] = resolved;
                }
            }
        }

        internal static object ResolveSymbol(string text,
            Chat context, IDictionary<string, object> globals)
        {
            object result = null; // check locals, then globals
            if (context != null && context.scope.ContainsKey(text))
            {
                result = context.scope[text];
            }
            else if (globals != null && globals.ContainsKey(text))
            {
                result = globals[text];
            }
            return result;
        }

        private static MatchCollection GetMatches(string text)
        {
            var matches = RE.ParseVars.Matches(text);

            if (matches.Count == 0 && text.Contains(Ch.SYMBOL, Ch.LABEL))
            {
                throw new BindException("Unable to parse symbol: " + text);
            }

            return matches;
        }

        private static GroupCollection GetGroups(Match match, int expected)
        {
            var groups = match.Groups;
            if (groups.Count != expected)
            {
                Util.ShowMatch(match);
                throw new ArgumentException("Invalid group count " + groups.Count);
            }
            return groups;
        }

        private static List<Symbol> SortByLength(IEnumerable<Symbol> syms)
        {
            return (from s in syms orderby s.symbol.Length ascending select s).ToList();
        }

        internal SymbolType Type()
        {
            return this.symbol.Contains(Ch.SCOPE) ? (this.chatScoped ?
                SymbolType.CHAT_SCOPE : SymbolType.GLOBAL_SCOPE) : SymbolType.SIMPLE;
        }

        internal enum SymbolType { SIMPLE, CHAT_SCOPE, GLOBAL_SCOPE }
    }


    /// <summary>
    /// Represents an atomic operation on a pair of metadata string that when invoked returns a boolean
    /// </summary>
    public class Operator
    {
        private enum OpType { EQUALITY, COMPARISON, MATCHING, ASSIGNMENT }

        public static Operator EQ = new Operator("=", OpType.EQUALITY);
        public static Operator NE = new Operator("!=", OpType.EQUALITY);

        public static Operator SW = new Operator("^=", OpType.MATCHING);
        public static Operator EW = new Operator("$=", OpType.MATCHING);
        public static Operator RE = new Operator("*=", OpType.MATCHING);

        public static Operator GT = new Operator(">", OpType.COMPARISON);
        public static Operator LT = new Operator("<", OpType.COMPARISON);
        public static Operator LE = new Operator("<=", OpType.COMPARISON);
        public static Operator GE = new Operator(">=", OpType.COMPARISON);

        public static Operator[] ALL = { GT, LT, EQ, NE, LE, GE, SW, EQ, RE };

        private readonly string value;
        private readonly OpType type;

        private Operator(string v, OpType o)
        {
            this.value = v;
            this.type = o;
        }

        public static string FromOperator(Operator op)
        {
            for (int i = 0; i < ALL.Length; i++)
            {
                if (op == ALL[i]) return op.ToString();
            }
            throw new Exception("Invalid Operator: " + op);
        }

        public static Operator FromString(string op)
        {
            switch (op)
            {
                case ">": return Operator.GT;
                case "<": return Operator.LT;
                case ">=": return Operator.GE;
                case "<=": return Operator.LE;
                case "!=": return Operator.NE;
                case "^=": return Operator.SW;
                case "$=": return Operator.EW;
                case "*=": return Operator.RE;
                case "==": return Operator.EQ;
                case "=": return Operator.EQ;
            }
            throw new Exception("Invalid Operator: " + op);
        }

        public override string ToString()
        {
            return this.value;
        }

        public bool Invoke(string s1, string s2)
        {
            if (s1 == null) throw new OperatorException(this);

            if (this.type == OpType.EQUALITY)
            {
                if (this == EQ) return Equals(s1, s2);
                if (this == NE) return !Equals(s1, s2);
            }
            else if (this.type == OpType.MATCHING)
            {
                if (s2 == null) return false;
                if (this == SW) return s1.StartsWith(s2, StringComparison.CurrentCulture);
                if (this == EW) return s1.EndsWith(s2, StringComparison.CurrentCulture);
                if (this == RE) return new Regex(s2).IsMatch(s1);
            }
            else if (this.type == OpType.COMPARISON)
            {
                try
                {
                    double o1 = (double)Convert.ChangeType(s1, typeof(double));
                    double o2 = (double)Convert.ChangeType(s2, typeof(double));
                    if (this == GT) return o1 > o2;
                    if (this == LT) return o1 < o2;
                    if (this == GE) return o1 >= o2;
                    if (this == LE) return o1 <= o2;
                }
                catch (FormatException)
                {
                    throw new OperatorException(this, "Expected numeric "
                        + "operands, but found [" + s1 + "," + s2 + "]");
                }
                catch (Exception e)
                {
                    throw new OperatorException(this, e);
                }
            }
            throw new OperatorException(this, "Unexpected Op type: ");
        }
    }

    public class Assignment
    {
        public static Assignment EQ = new Assignment("=");
        public static Assignment OE = new Assignment("|=");
        public static Assignment PE = new Assignment("+=");
        /*public static AssignOp ME = new AssignOp("-=");
        public static AssignOp TE = new AssignOp("*=");
        public static AssignOp DE = new AssignOp("/=");*/

        public static Assignment[] ALL = { EQ, OE, PE };//, ME, TE, DE };

        private readonly string value;

        private Assignment(string v)
        {
            this.value = v;
        }

        public static string FromOperator(Assignment op)
        {
            for (int i = 0; i < ALL.Length; i++)
            {
                if (op == ALL[i]) return op.ToString();
            }
            throw new Exception("Invalid Operator: " + op);
        }

        public static Assignment FromString(string op)
        {
            switch (op)
            {
                case "=": return Assignment.EQ;
                case "|=": return Assignment.OE;
                case "+=": return Assignment.PE;
                    /*case "-=": return AssignOp.ME;
                    case "*=": return AssignOp.TE;
                    case "/=": return AssignOp.DE;*/
            }
            throw new Exception("Invalid Operator: " + op);
        }

        public override string ToString()
        {
            return this.value;
        }

        public bool Invoke(string s1, string s2, IDictionary<string, object> scope)
        {
            s1 = s1.TrimFirst(Ch.SYMBOL);

            string result = null;

            if (this == EQ)
            {
                if (Util.HasOpenGroup(s2)) s2 = s2.Parenthify();
                result = s2;
            }
            else if (this == OE)
            {
                if (!scope.ContainsKey(s1)) throw new ParseException
                    ("Variable " + s1 + " not found in globals:\n  " + scope.Stringify());

                var now = (string)scope[s1];
                if (now.StartsWith('(') && now.EndsWith(')'))
                {
                    result = now.TrimLast(')') + " | " + s2 + ')';
                }
                else
                {
                    result = '(' + now + " | " + s2 + ')';
                }
            }
            else if (this == PE)
            {
                if (!scope.ContainsKey(s1)) throw new ParseException
                    ("Variable " + s1 + " not found in globals:\n  " + scope.Stringify());

                result = scope[s1] + " " + s2;
            }

            scope[s1] = result;

            return true;
        }
    }
}
