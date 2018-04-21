using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Dialogic
{
    public class Properties //@cond unused
    {
        static IDictionary<Type, IDictionary<string, PropertyInfo>> lookup;

        static Properties instance = new Properties();

        private Properties()
        {
            lookup = new Dictionary<Type, IDictionary<string, PropertyInfo>>();
        }

        internal static IDictionary<string, PropertyInfo> Lookup(Type type)
        {
            var dbug = false;
            if (!lookup.ContainsKey(type))
            {
                var propMap = new Dictionary<string, PropertyInfo>();

                var props = type.GetProperties(BindingFlags.Instance
                    | BindingFlags.Public | BindingFlags.NonPublic);

                if (dbug) Console.Write(type.Name + "[");
                foreach (var pi in props)
                {
                    propMap.Add(pi.Name, pi);
                    if (dbug) Console.Write(pi.Name + ",");
                }
                if (dbug) Console.WriteLine("]");
                lookup[type] = propMap;
            }
            return lookup[type];
        }

        // TODO: test
        internal static void Set(Object target, PropertyInfo pinfo, object value)
        {
            value = Util.ConvertTo(pinfo.PropertyType, value);
            pinfo.SetValue(target, value, null);
        }

        // TODO: test
        internal static object Get(Object target, PropertyInfo pinfo)
        {
            var value = pinfo.GetValue(target);
            return Util.ConvertTo(pinfo.PropertyType, value);
        }
    }

    internal class Symbol
    {
        public List<string> modifiers;
        public string text, alias, symbol;
        public bool bounded, chatScoped;

        internal Symbol() { }

        internal Symbol(params string[] parts)
        {
            this.text = parts[1].Trim();
            this.symbol = parts[4].Trim();
            this.alias = parts[2].Length > 0 ? parts[2].Trim() : null;
            this.bounded = text.Contains(Ch.OBOUND) && text.Contains(Ch.CBOUND);
            this.chatScoped = (parts[3] == Ch.LABEL.ToString());
        }

        internal Symbol(string txt, string sym, string save = "", bool chatLocal = false)
        {
            this.text = txt.Trim();
            this.symbol = sym.Trim();
            this.alias = save.Length > 0 ? save.Trim() : null;
            this.bounded = text.Contains(Ch.OBOUND) && text.Contains(Ch.CBOUND);
            this.chatScoped = chatLocal;
        }

        private void ParseMods(Group group)
        {
            var modGroup = group.Value.Trim();
            if (!modGroup.IsNullOrEmpty())
            {
                if (modifiers == null)
                {
                    modifiers = new List<string>();
                }
                foreach (Capture mod in group.Captures)
                {
                    modifiers.Add(mod.Value.TrimFirst(Ch.MODIFIER));
                }
                //Console.WriteLine("MODS: " + modifiers.Stringify());
            }
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

        internal static List<Symbol> Parse(string text, bool sortResults = false)
        {
            var symbols = new List<Symbol>();
            var matches = RE.ParseVars.Matches(text);

            if (matches.Count == 0 && text.Contains(Ch.SYMBOL, Ch.LABEL))
            {
                throw new ResolverException("Unable to parse symbol: " + text);
            }

            foreach (Match match in matches)
            {
                var groups = match.Groups;
                if (groups.Count != 6)
                {
                    Util.ShowMatch(match);
                    throw new ArgumentException
                        ("Invalid input to Symbol(): " + groups.Count);
                }
                var sym = new Symbol(groups.Values());
                sym.ParseMods(groups[5]);
                symbols.Add(sym);
            }

            // OPT: we can sort here to avoid symbols which are substrings of other
            // symbols causing incorrect replacements ($a being replaced in $ant, 
            // for example), however should be avoided by using Regex.Replace 
            // instead of String.Replace() in BindSymbols
            return sortResults ? SortByLength(symbols) : symbols;
        }

        private static List<Symbol> SortByLength(IEnumerable<Symbol> syms)
        {
            return (from s in syms orderby s.symbol.Length descending select s).ToList();
        }
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
