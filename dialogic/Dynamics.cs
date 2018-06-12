using System;
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
            if (target == null) throw new BindException("Null Set target");

            var lookup = Lookup(target.GetType());

            if (lookup != null && lookup.ContainsKey(property))
            {
                var pinfo = lookup[property];
                value = Util.ConvertTo(pinfo.PropertyType, value);
                pinfo.SetValue(target, value, null);
                return true;
            }

            if (!onlyIfExists) throw new BindException("Invalid function call(Set): " + property);

            return false;
        }

        internal static object Get(Object target, string property, object defaultVal = null)
        {
            if (target == null) throw new BindException("Null Get target");

            var lookup = Lookup(target.GetType());

            if (lookup != null && lookup.ContainsKey(property))
            {
                var pinfo = lookup[property];
                var value = pinfo.GetValue(target);
                return Util.ConvertTo(pinfo.PropertyType, value);
            }

            if (defaultVal != null) return defaultVal;

            throw new BindException("Invalid function call(Get): " + property);
        }
    }

    public static class Methods
    {
        static IDictionary<Type, IDictionary<string, MethodInfo>> Cache;

        static Methods()
        {
            Cache = new Dictionary<Type, IDictionary<string, MethodInfo>>();
        }

        public static object Invoke(object target, string methodName,
            object[] args = null, bool throwOnError = true)
        {
            var type = target.GetType();
            if (!Cache.ContainsKey(type))
            {
                Cache[type] = new Dictionary<string, MethodInfo>();
            }

            MethodInfo method = null;
            object result = null;
            var key = CacheKey(methodName, args);

            if (!Cache[type].ContainsKey(key))
            {
                method = type.GetMethod(methodName, ArgsToTypes(args));
                if (method == null && target is string)
                {
                    try
                    {
                        result = InvokeTransform(target, methodName);
                    }
                    catch (Exception ex)
                    {
                        if (!throwOnError) return BestGuess(target, methodName);
                        throw BadTransform(target, methodName, ex);
                    }

                    if (result != null) return result;

                    if (!throwOnError) return BestGuess(target, methodName);

                    throw new UnboundFunction(methodName, null, null,
                        "\nDid you mean to call ChatRuntime.AddTransform(\""
                            + methodName + "\", ...)?");
                }

                if (method == null)
                {
                    if (!throwOnError) return BestGuess(target, methodName);

                    throw new UnboundFunction(methodName, type.Name, ArgsToTypes(args));
                }

                Cache[type][key] = method;
            }

            method = Cache[type][key];

            try
            {
                result = method.IsStatic ? method.Invoke(null, args) :
                    method.Invoke(target, args);
            }
            catch (Exception ex)
            {
                throw BadTransform(target, methodName, ex);
            }

            return result;
        }

        private static object BestGuess(object caller, string methodName)
        {
            return caller + "." + methodName + "()";
        }

        private static TransformException BadTransform
            (object target, string methodName, Exception ex)
        {
            return new TransformException(methodName +
                "(" + target + ") threw Exception: " + ex.Message);
        }

        /* 
         * OPT: note that we don't need reflection here, but could directly
         * call Func.Invoke(), which is likely faster
         */
        internal static object InvokeTransform(object target, string methodName)
        {
            var type = typeof(Transforms); // wrong, transforms can be anywhere
            if (!Cache.ContainsKey(type))
            {
                Cache[type] = new Dictionary<string, MethodInfo>();
            }

            var args = new[] { target };
            var key = CacheKey(methodName, args);
            Func<string, string> transform = null;

            if (!Cache[type].ContainsKey(key))
            {
                transform = Transforms.Get(methodName);

                if (transform == null) return null;

                Cache[type][key] = transform.GetMethodInfo();
            }

            // OPT: see note above...
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

    public class Resolvable : IResolvable
    {
        protected Chat context;
        public string text;

        public Resolvable(Chat context) : this(context, null) { }

        public Resolvable(Chat context, string text)
        {
            this.context = context;
            this.text = text;
        }

        //protected static bool IsStrictMode(Chat context)
        //{
        //    return context == null || context.runtime == null
        //        || context.runtime.strictMode;
        //}

        protected bool IsStrictMode()
        {
            return context == null || context.runtime == null
                || context.runtime.strictMode;
        }

        protected internal void OnBindSymbolError(string name, IDictionary<string, object> globals)
        {
            if (IsStrictMode()) throw new UnboundSymbol(name, context, globals);
            ChatRuntime.Warn("Unbound symbol: $" + name.TrimFirst(Ch.SYMBOL));
        }
    }

    internal class Choice : Resolvable
    {
        public readonly string[] options;
        public string alias;

        private List<string> transforms;
        private HashSet<string> unique;
        private string lastResolved;

        private Choice(Chat context, string text, string groups,
            string alias, List<string> transforms) : base(context, text)
        {
            this.transforms = transforms;
            this.options = ParseOptions(groups);
            //Console.WriteLine(options.Stringify()+" :: "+text);
            this.alias = alias.IsNullOrEmpty() ? null : alias;
        }

        private string[] ParseOptions(string groups)
        {
            if (unique == null) unique = new HashSet<string>();
            if (unique.Count > 0) unique.Clear();
            var opts = RE.SplitOr.Split(groups);
            foreach (var o in opts) unique.Add(o);
            return unique.ToArray();
        }

        /// <summary>
        /// Parse Choice objects from the specified input. Note that for nested groups, only the innermost group will be parsed.
        /// </summary>
        internal static List<Choice> Parse(string input,
            Chat context, bool showMatch = false)
        {
            if (context == null || context.runtime == null)
            {
                ChatRuntime.Warn("Choice.Parse got null context/runtime: " + input);
            }
            var choices = new List<Choice>();
            Parse(choices, input, context, showMatch);
            return choices;
        }

        public static void Parse(List<Choice> results, string input,
            Chat context, bool showMatch = false)
        {
            if (!input.Contains(Ch.OR)) return;

            // OPT: Cache the entire match?
            foreach (Match m in RE.ParseChoices.Matches(input))
            {
                if (showMatch) Util.ShowMatch(m);

                var full = m.Groups[0].Value;
                var alias = m.Groups[1].Value;
                var expr = m.Groups[2].Value;
                var trans = ParseTransforms(m.Groups[3]);

                // TODO: Why was this here ??
                //if (expr.Contains(Ch.CGROUP))
                //{
                //    var oidx = full.IndexOf(Ch.OGROUP);
                //    var cidx = full.LastIndexOf(Ch.CGROUP);
                //    expr = full.Substring(oidx + 1, cidx - oidx - 1);
                //}

                if (CacheEnabled(context))
                {
                    var cache = context.runtime.choiceCache;

                    // cache our prior Choice objects here, 
                    var choiceKey = context.text + Ch.LABEL + full;
                    if (!cache.ContainsKey(choiceKey))
                    {
                        //Console.WriteLine("CACHE-add: " + choiceKey);
                        cache.Add(choiceKey, new Choice(context, full, expr, alias, trans));
                    }
                    ///else Console.WriteLine("CACHE-HIT: " + choiceKey);
                    results.Add(cache[choiceKey]);

                    // this data is not in the cache key, so we reset it here
                    cache[choiceKey].transforms = trans;
                    cache[choiceKey].alias = alias;
                }
                else  // no cache
                {
                    results.Add(new Choice(context, full, expr, alias, trans));
                }
            }
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

            lastResolved = resolved; // store the result for next time

            HandleAlias(resolved, context); // push alias value into scope

            return resolved;// (resolved); ; // do functions and return
        }

        internal string Replace(string full, string replaceWith)
        {
            if (replaceWith == null) return full;

            var toReplace = text;

            // if we have transforms, save them for later resolution
            if (!transforms.IsNullOrEmpty())
            {
                //toReplace = toReplace.Substring(0, toReplace.LastIndexOf(").", Util.IC)) + ")";

                if (!replaceWith.EnclosedBy(Ch.OGROUP, Ch.CGROUP))
                {
                    var pre = replaceWith;
                    replaceWith = Ch.OGROUP + replaceWith + Ch.CGROUP;
                    //Console.WriteLine("Choice.ENCLOSED: " + pre + " -> " + replaceWith);
                }
                //Console.WriteLine(" -> " + toReplace);

                foreach (var t in transforms)
                {
                    replaceWith += '.' + t + "()";
                }
            }

            //Console.WriteLine("ToReplace: " + toReplace);
            //Console.WriteLine("ReplaceWith: " + replaceWith);

            var tmp = full.ReplaceFirst(toReplace, replaceWith);

            //Console.WriteLine("ReplaceFirst -> " + tmp);


            return tmp;
        }

        internal static List<string> ParseTransforms(Group g)
        {
            List<string> transforms = null;
            if (!g.Value.IsNullOrEmpty())
            {
                if (transforms == null) transforms = new List<string>();
                if (transforms.Count > 0) transforms.Clear();
                foreach (Capture c in g.Captures) transforms.Add(c.Value);
            }
            return transforms;
        }

        internal static bool CacheEnabled(Chat context)
        {
            return context != null && context.runtime != null
                && context.runtime.choiceCache != null;
        }

        internal string[] _TransArray() // test only
        {
            return transforms.ToArray();
        }

        private void HandleAlias(object resolved, Chat ctx)
        {
            var scope = ctx != null ? ctx.scope : null;
            if (this.alias != null && resolved != null)
            {
                if (scope == null) throw new BindException("Null runtime/context: " + this);

                if (!resolved.ToString().Contains(Ch.OR))
                {
                    scope[alias] = resolved;
                }
            }
        }

        public override string ToString() => text;
    }

    internal class Symbol : Resolvable
    {
        public string alias, name;
        public List<string> transforms;

        private Symbol(Chat context, Match m) : this(context, m.Groups) { }

        private Symbol(Chat context, GroupCollection parts) : base(context)
        {
            if (parts.Count != 6) throw new BindException("Unable to parse");

            this.text = parts[0].Value;

            this.alias = (parts[1].Value == "[" && parts[5].Value == "]"
                && !parts[2].Value.IsNullOrEmpty()) ? parts[2].Value : null;

            this.name = parts[3].Value;

            if (alias == null // attempt to handle single-bracket cases
                && (parts[1].Value == "[" && parts[5].Value != "]"
                || parts[1].Value != "[" && parts[5].Value == "]"))
            {
                text = text.Replace("[", "").Replace("]", "");
            }

            ParseTransforms(parts[4]);
        }

        public override string ToString()
        {
            return Name();
        }

        internal string Name()
        {
            return Ch.SYMBOL + name;
        }

        public static void Parse(List<Symbol> symbols, string text, Chat context)
        {
            var matches = RE.ParseSymbols.Matches(text);

            foreach (Match match in matches)
            {
                // Create a new Symbol and add it to the result
                symbols.Add(new Symbol(context, match));
            }
        }

        public static List<Symbol> Parse(string text, Chat context)
        {
            var symbols = new List<Symbol>();
            Parse(symbols, text, context);
            return symbols;
        }

        internal string TransformText()
        {
            var s = "";
            transforms.ForEach(t => s += '.' + t);
            return s;
        }

        internal string ResolveIn(string fullText,
            IDictionary<string, object> globals, bool throwOnNotFound = true)
        {
            var result = Resolve(globals, throwOnNotFound);
            return Replace(fullText, result, globals);
        }

        internal string Resolve(IDictionary<string, object> globals,
            bool throwOnNotFound = true)
        {
            object resolved = ResolveSymbol(name, context, globals);

            if (resolved == null)
            {
                if (throwOnNotFound) OnBindSymbolError(name, globals);
                return null;
            }

            // if we have a transform path, follow it
            if (!(resolved is string) && !transforms.IsNullOrEmpty())
            {
                resolved = GetViaPath(resolved, transforms.ToArray(), globals);
            }

            return resolved.ToString();
        }

        internal string Replace(string full, string replaceWith,
            IDictionary<string, object> globals = null)
        {
            if (replaceWith == null) return full;

            var reText = this.text;

            PreProcess(ref replaceWith, ref reText, globals);

            // Here we do the replacement 
            if (!alias.IsNullOrEmpty())
            {
                // Aliased: no need for regex replacement
                full = full.Replace(reText, replaceWith);
            }
            else
            {
                // Regex replace here to avoid matching substrings (cache?)
                var regex = Regex.Escape(reText) + @"(?![A-Za-z0-9_-])";
                full = new Regex(regex).Replace(full, replaceWith);
            }

            PostProcess(ref full, replaceWith);

            return full;
        }

        private static void PostProcess(ref string full, string replaceWith)
        {
            var check = Ch.SYMBOL + (Ch.OGROUP + replaceWith + Ch.CGROUP);
            if (full.Contains(check))
            {
                full = full.Replace(check, Ch.SYMBOL + replaceWith);
            }
        }

        private void PreProcess(ref string replaceWith, ref string reText, IDictionary<string, object> globals)
        {
            /* STATES:
             *   A) Dynamic:                            leave all 
             *   B) Non-Dynamic w' transform:           leave all and add group
             *   C) Non-Dynamic w' alias:               execute alias, replace all
             *   D) Non-Dynamic w' alias and transform: execute alias, replace all, re-add tranform
             */
            if (replaceWith.ContainsAny(Ch.OR, Ch.SYMBOL))          // A
            {
                // leave any alias and transforms in place
                reText = Name();

                //if (!transforms.IsNullOrEmpty())
                replaceWith = CheckParens(replaceWith);
            }
            else if (!transforms.IsNullOrEmpty() && alias == null)  // B
            {
                replaceWith = CheckParens(replaceWith);
                reText = Name();
            }
            else if (transforms.IsNullOrEmpty() && alias != null)   // C
            {
                // push the alias into scope
                HandleAlias(replaceWith, globals);
            }
            else if (!transforms.IsNullOrEmpty() && alias != null)  // D
            {
                // push the alias into scope
                HandleAlias(replaceWith, globals);

                // leave the transforms in place               
                replaceWith = CheckParens(replaceWith) + TransformText();
            }

            // before replacing, make sure groups are properly bounded
            if (replaceWith.Contains(Ch.OR)) CheckParens(replaceWith);
        }

        internal static string CheckParens(string replaceWith)
        {
            // add an enclosing group for the transform
            if (!replaceWith.EnclosedBy(Ch.OGROUP, Ch.CGROUP, true))
            {
                var pre = replaceWith;
                replaceWith = Ch.OGROUP + replaceWith + Ch.CGROUP;
                //Console.WriteLine("Symbol.ENCLOSED: " + pre + " -> " + replaceWith);
            }

            return replaceWith;
        }

        private void HandleAlias(object resolved, IDictionary<string, object> scope)
        {
            if (this.alias != null && resolved != null)
            {
                if (!resolved.ToString().Contains(Ch.OR, Ch.SYMBOL))
                {
                    //Console.WriteLine("      Symbol.Push: $" + alias + "=" + resolved);
                    scope[alias] = resolved;
                }
            }
        }

        private object GetViaPath(object start, string[] paths,
            IDictionary<string, object> globals)
        {
            if (start == null) return null;

            // Dynamically resolve the object path 
            for (int i = 0; i < paths.Length; i++)
            {
                if (paths[i].EndsWith(Ch.CGROUP))
                {
                    var func = paths[i].Replace("()", "");
                    if (start.ToString().Contains(Ch.OR))
                    {
                        // delay the method call until fully resolved
                        start = start + "." + paths[i];
                    }
                    else
                    {   // nothing more to resolve, so invoke
                        start = Methods.Invoke(start, func, null,
                            context == null || context.runtime.strictMode);
                    }

                    // TODO: handle other signatures ?
                }
                else
                {
                    start = Properties.Get(start, paths[i]);
                }

                if (start == null) OnBindSymbolError(paths[i], globals);
            }

            // we've process all the transforms, 
            // either by executing them, or by adding them to the output
            transforms = null;

            return start;
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

        private void ParseTransforms(Group g)
        {
            if (!g.Value.IsNullOrEmpty())
            {
                var parts = g.Value.TrimFirst(Ch.SCOPE).Split(Ch.SCOPE);

                if (parts.Length > 0)
                {
                    if (transforms == null) transforms = new List<string>();
                    if (transforms.Count > 0) transforms.Clear();
                    foreach (var part in parts) transforms.Add(part);
                }
            }
        }
    }

    public class TxForm : Resolvable
    {
        public string content, origContent, transformText;
        public List<string> transforms;

        //private TxForm(Chat context, Match m) : this(context, m.Groups) { }

        private TxForm(Chat context, string text, string content,
           string transformText, List<string> trans) : base(context)
        {
            this.text = text;
            this.content = content;
            this.transformText = transformText;
            this.transforms = trans;
        }

        public override string ToString()
        {
            return content + transformText;
        }

        public static void Parse(List<TxForm> tforms,
            string text, Chat context, bool showMatches = false)
        {
            var matches = RE.ParseTransforms.Matches(text);

            if (showMatches) Util.ShowMatches(matches);

            foreach (Match match in matches)
            {
                var theText = match.Groups[0].Value;
                var content = match.Groups[1].Value.Trim();

                if (!content.Contains("()"))
                {
                    content = new Regex(@"\(([^\)]+)\)").Replace(content, "$1"); // TODO: compile
                }
                else
                {
                    Parse(tforms, content, context, showMatches);
                }

                List<string> funs = null;
                var transformText = match.Groups[2].Value.Trim();
                if (!transformText.IsNullOrEmpty())
                {
                    funs = new List<string>();
                    foreach (Capture c in match.Groups[3].Captures)
                    {
                        funs.Add(c.Value.TrimFirst(Ch.SCOPE).Replace("()", ""));
                    }
                }

                tforms.Add(new TxForm(context, theText, content, transformText, funs));
            }
        }

        public static List<TxForm> Parse(string text,
            Chat context, bool showMatches = false)
        {
            var tforms = new List<TxForm>();
            Parse(tforms, text, context, showMatches);
            return tforms;
        }

        internal string Replace(string full, string replaceWith)
        {
            return replaceWith == null ? full :
                full.ReplaceFirst(this.text, replaceWith);
        }

        internal string Resolve()
        {
            if (text.ContainsAny(Ch.SYMBOL, Ch.OR)) throw new BindException
                ("Invalid state: " + this);

            if (content.Length < 1) return string.Empty;

            object result = content;
            transforms.ForEach(t => result = Methods.Invoke
                (result, t, null, IsStrictMode()));

            return result != null ? result.ToString() : null;
        }
    }

    /// <summary>
    /// An atomic operation on a metadata key-value pair that, when invoked, returns a boolean
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
                    ("Variable $" + s1 + " not found in globals:\n  " + scope.Stringify());

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
                    ("Variable $" + s1 + " not found in globals:\n  " + scope.Stringify());

                result = scope[s1] + " " + s2;
            }

            scope[s1] = result;

            return true;
        }
    }
}
