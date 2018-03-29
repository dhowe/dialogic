﻿using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("tests")]

namespace Dialogic
{
    /// <summary>
    /// A (temporary) container for Dialogic defaults which can be changed 
    /// by apps as needed (replace with file-based config mechanism?)
    /// </summary>
    public static class Defaults
    {
        /// <summary>
        /// Default Command duration for Say
        /// </summary>
        public static double SAY_DURATION = 2.0;

        /// <summary>
        /// Default Command duration for Do
        /// </summary>
        public static double DO_DURATION = 0.02;

        /// <summary>
        /// Default Command duration for Ask
        /// </summary>
        public static double ASK_DURATION = Util.INFINITE;

        /// <summary>
        /// Default Command duration for Wait
        /// </summary>
        public static double WAIT_DURATION = Util.INFINITE;

        /// <summary>
        /// Default Timeout for Ask
        /// </summary>
        public static double ASK_TIMEOUT = 5.0;

        /// <summary>
        /// Default staleness-threshold for Find
        /// </summary>
        public static double FIND_STALENESS = 4;

        /// <summary>
        /// Max staleness-threshold to try for a Find
        /// </summary>
        public static double FIND_MAX_STALENESS = 50;

        /// <summary>
        /// Default staleness for new Chats
        /// </summary>
        public static double CHAT_STALENESS = 0;

        /// <summary>
        ///  Default increment each time Chat is run
        /// </summary>
        public static double CHAT_STALENESS_INCR = 1;

        // Default Timing fields for Say, Ask, Opt (?)
        public static double SAY_FAST_MULT = 0.5;
        public static double SAY_SLOW_MULT = 2.0;
        public static double SAY_MAX_LEN_MULT = 2.0;
        public static double SAY_MIN_LEN_MULT = 0.5;
        public static int SAY_MAX_LINE_LEN = 80;
        public static int SAY_MIN_LINE_LEN = 2;

        /// <summary>
        /// Whether to throw an exception when an IResume
        /// is sent during an active Chat
        /// </summary>
        public static bool THROW_ON_UNEXPECTED_RESUME = true;
    }

    /// <summary>
    /// A 1=1 mapping from a string command to its object type, 
    /// e.g., "SAY" -> Dialogic.Say
    /// </summary>
    public class CommandDef
    {
        public readonly Type type;
        public readonly string label;

        public CommandDef(String cmd, Type cmdType)
        {
            this.label = cmd;
            this.type = cmdType;
            if (!typeof(Command).IsAssignableFrom(cmdType))
            {
                throw new DialogicException
                    ("Expected subclass of Command, but found " + cmdType);
            }
        }
    }

    /// <summary>
    /// A set of precompiled regular expressions 
    /// </summary>
    public static class RE
    {
        internal const string OP1 = @"^(!?!?$?[a-zA-Z_][a-zA-Z0-9_]*)";
        internal const string OP2 = @"\s*([!*$^=<>]?=|<|>)\s*(\S+)$";
        public static Regex FindMeta = new Regex(OP1 + OP2);

        internal const string MP1 = @"\(([^()]+|(?<Level>\()|";
        internal const string MP2 = @"(?<-Level>\)))+(?(Level)(?!))\)";
        public static Regex MatchParens = new Regex(MP1 + MP2);

        public static Regex MetaSplit = new Regex(@"\s*,\s*");
    }

    /// <summary>
    /// Static utility functions for Dialogic
    /// </summary>
    public static class Util
    {
        public static StringComparison IC = StringComparison.InvariantCulture;
        public static int INFINITE = -1;

        private static int start;
        private static Random random;

        static Util()
        {
            start = EpochMs();
            random = new Random();
        }

        public static int SecStrToMs(string s, int defaultMs = -1)
        {
            double d;
            try
            {
                d = (double)Convert.ChangeType(s, typeof(double));
            }
            catch (FormatException)
            {
                return defaultMs;
            }
            return (int)(d * 1000);
        }

        public static string ToMixedCase(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;

            return (s[0].ToString()).ToUpper() + s.Substring(1).ToLower();
        }


        public static double Map(double n, double start1, double stop1, double start2, double stop2)
        {
            return (n - start1) / (stop1 - start1) * (stop2 - start2) + start2;
        }

        public static double Constrain(double n, double low, double high)
        {
            return Math.Max(Math.Min(n, high), low);
        }

        public static int Millis()
        {
            return EpochMs() - start;
        }

        public static int Millis(int since)
        {
            return Millis() - since;
        }

        public static string ElapsedSec(int since = 0)
        {
            return (Millis(since) / 1000.0).ToString("0.##") + "s";
        }

        public static int EpochMs()
        {
            return Environment.TickCount & Int32.MaxValue;
        }

        public static double Rand()
        {
            return random.NextDouble();
        }

        public static int Rand(int min, int max)
        {
            return random.Next(min, max);
        }

        public static void ShowMatches(MatchCollection matches)
        {
            int i = 0;
            foreach (Match match in matches) ShowMatch(match, i++);
        }

        public static int ShowMatch(Match match, int i = 0)
        {
            Console.WriteLine("\nMatch {0} has {1} groups:\n", i, match.Groups.Count);

            int groupNo = 0;
            foreach (Group mm in match.Groups)
            {
                Console.WriteLine("  Group {0} has {1} capture(s) '{2}'",
                    groupNo, mm.Captures.Count, mm.Value);

                int captureNo = 0;
                foreach (Capture cc in mm.Captures)
                {
                    Console.WriteLine("       Capture {0} '{1}'", captureNo++, cc);
                }
                groupNo++;
            }

            groupNo = 0;
            Console.WriteLine("\n  match.Value = \"{0}\"", match.Value);
            foreach (Group mm in match.Groups)
            {
                Console.WriteLine("  match.Groups[{0}].Value = \"{1}\"",
                    groupNo, match.Groups[groupNo++].Value);
            }

            groupNo = 0;
            foreach (Group mm in match.Groups)
            {
                int captureNo = 0;
                foreach (Capture cc in mm.Captures)
                {
                    Console.WriteLine("  match.Groups[{0}].Captures[{1}].Value = \"{2}\"",
                        groupNo, captureNo, match.Groups[groupNo].Captures[captureNo++].Value); //**
                }
                groupNo++;
            }

            return i;
        }

        internal static int Round(double p)
        {
            return (int)Math.Round(p);
        }

        internal static int Round(float p)
        {
            return (int)Math.Round(p);
        }

        internal static string[] StripSingleLineComments(string[] lines)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Trim();
                int idx = lines[i].IndexOf("//", Util.IC);
                if (idx > -1) lines[i] = lines[i].Substring(0, idx);
            }
            return lines;
        }

        internal static string[] StripMultiLineComments(string[] lines)
        {
            bool commentOpen = false;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();

                if (commentOpen)
                {
                    int endIdx = line.IndexOf("*/", Util.IC);
                    if (endIdx < 0)
                    {
                        line = String.Empty;
                    }
                    else
                    {
                        line = line.Substring(endIdx + 2);
                    }
                    commentOpen = false;
                }

                int startIdx = line.IndexOf("/*", Util.IC);
                if (startIdx > -1)
                {
                    int endIdx = line.IndexOf("*/", Util.IC);

                    if (endIdx < 0) // only open, no close
                    {
                        line = line.Substring(0, startIdx);
                        commentOpen = true;
                    }
                    else if (startIdx == 0 && endIdx == line.Length - 2) // a
                    {
                        line = String.Empty;
                    }
                    else if (startIdx == 0 && endIdx > startIdx)        // b
                    {
                        line = line.Substring(endIdx + 2);
                    }
                    else if (startIdx > 0 && endIdx == line.Length - 2) // c
                    {
                        line = line.Substring(0, startIdx - 1);
                    }
                    else if (startIdx > 0 && endIdx < line.Length - 2) // d
                    {
                        var tmp = line.Substring(0, startIdx - 1);
                        line = tmp + line.Substring(endIdx + 2);
                    }
                }

                lines[i] = line;
            }

            return lines;
        }

        public static object RandItem(object[] arr)
        {
            return arr[Rand(arr.Length)];
        }

        public static T RandItem<T>(List<T> l)
        {
            return l.ElementAt(Rand(l.Count));
        }

        public static int Rand(int max)
        {
            return Rand(0, max);
        }

        public static double Rand(double min, double max)
        {
            return min + Rand() * max;
        }

        public static double Rand(double max)
        {
            return Rand(0, max);
        }

        public static void Log(string logFileName, object msg)
        {
            using (StreamWriter w = File.AppendText(logFileName))
            {
                w.WriteLine(DateTime.Now.ToLongTimeString() + "\t"
                    + Util.EpochMs() + "\t" + msg);
            }
        }



        public static IEnumerable<string> SortByLength(IEnumerable<string> e)
        {
            return from s in e orderby s.Length descending select s;
        }

        /**
         * Converts seconds to millis if seconds is >= 0, else returns -1
         */
        public static int ToMillis(double seconds)
        {
            return (seconds < 0) ? -1 : (int)(seconds * 1000);
        }

        public static double ToSec(int millis)
        {
            return millis / 1000.0;
        }

        public static bool TrimFirst(ref string s, char c)
        {
            if (s.IndexOf(c) == 0)
            {
                s = s.Substring(1);
                return true;
            }
            return false;
        }
    }


    /// <summary>
    /// Refers to the Contraints behavior during search, specifically whether
    /// it can be 'relaxed to allow a larger result set
    /// </summary>
    public enum ConstraintType { Soft = 0, Hard = 1, Absolute = 2 };


    /// <summary>
    /// Implements a Constraint (a name, a value, an Operator) that can be checked
    /// </summary>
    public class Constraint
    {
        public static char TypeSetChar = '!';

        public readonly string name;
        public readonly Operator op;
        public ConstraintType type;
        public string value;

        public static IDictionary<string, object> AsDict(params Constraint[] c)
        {
            var dict = new Dictionary<string, object>();
            for (int i = 0; i < c.Length; i++)
            {
                dict.Add(c[i].name, c[i]);
            }
            return dict;
        }

        public Constraint(string key, string val, ConstraintType type = ConstraintType.Soft) :
            this(Operator.EQ, key, val, type)
        { }

        public Constraint(Operator op, string key, string val, ConstraintType type = ConstraintType.Soft)
        {
            this.type = type;
            this.value = val;
            this.name = key;
            this.op = op;

            if (val.Contains("|") && op != Operator.RE)
            {
                throw new ParseException("Regex operator (*=) expected with |");
            }
        }

        public bool Check(string check, IDictionary<string, object> globals = null)
        {
            string rval = value;
            if (globals != null)
            {
                if (check.Contains('$')) check = Realizer.DoVars(check, globals);
                if (value.Contains('$')) rval = Realizer.DoVars(value, globals);
            }
            var passed = op.Invoke(check, rval);
            //Console.WriteLine(check+" "+op+" "+ value + " -> "+passed);
            return passed;
        }

        public override string ToString()
        {
            return TypeToString() + name + op + value;
        }

        private string TypeToString()
        {
            switch (type)
            {
                case ConstraintType.Hard:
                    return TypeSetChar.ToString();

                case ConstraintType.Absolute:
                    return TypeSetChar.ToString() + TypeSetChar;

                default:
                    return String.Empty;
            }
        }

        public bool IsStrict()
        {
            return this.type == ConstraintType.Hard
                || this.type == ConstraintType.Absolute;
        }

        public double ValueAsDouble(double def = -1)
        {
            double d = def;
            try
            {
                d = Double.Parse(value);
            }
            catch (Exception) { }
            return d;
        }

        public bool ScaleValue(double scale)
        {
            double dval;
            if (Double.TryParse(value, out dval))
            {
                value = (dval * scale).ToString();
                return true;
            }
            return false;
        }

        public bool IncrementValue(double incr = 1)
        {
            double dval;
            if (Double.TryParse(value, out dval))
            {
                double newval = dval + incr;
                value = (newval + incr).ToString();
                if (newval < Defaults.FIND_MAX_STALENESS)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsRelaxable()
        {
            return this.type == ConstraintType.Hard;
        }

        public IDictionary<string, object> AsDict()
        {
            return Constraint.AsDict(this);
        }

        internal Constraint Copy()
        {
            return new Constraint(op, name, value, type);
        }

        internal void Relax()
        {
            if (IsRelaxable()) type = ConstraintType.Soft;
        }
    }

    public class Operator
    {
        private enum OpType { EQUALITY, COMPARISON, MATCHING }

        public static Operator EQ = new Operator("=", OpType.EQUALITY);
        public static Operator NEQ = new Operator("!=", OpType.EQUALITY);
        public static Operator SW = new Operator("^=", OpType.MATCHING);
        public static Operator EW = new Operator("$=", OpType.MATCHING);
        public static Operator RE = new Operator("*=", OpType.MATCHING);
        public static Operator GT = new Operator(">", OpType.COMPARISON);
        public static Operator LT = new Operator("<", OpType.COMPARISON);
        public static Operator LTE = new Operator("<=", OpType.COMPARISON);
        public static Operator GTE = new Operator(">=", OpType.COMPARISON);

        public static Operator[] ALL = { GT, LT, EQ, NEQ, LTE, GTE, SW, EQ, RE };

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
                case ">=": return Operator.GTE;
                case "<=": return Operator.LTE;
                case "!=": return Operator.NEQ;
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
                if (this == NEQ) return !Equals(s1, s2);
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
                    if (this == GTE) return o1 >= o2;
                    if (this == LTE) return o1 <= o2;
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

    // adapted from:
    //   https://codereview.stackexchange.com/questions/113596/writing-cs-analog-of-settimeout-setinterval-and-clearinterval
    public static class Timers
    {
        static IInterruptable timer;

        public static IInterruptable SetInterval(int ms, Action function)
        {
            return timer = ms > -1 ? StartTimer(ms, function, true) : null;
        }

        public static IInterruptable SetTimeout(int ms, Action function)
        {
            return timer = ms > -1 ? StartTimer(ms, function, false) : null;
        }

        private static IInterruptable StartTimer(int interval, Action function, bool autoReset)
        {
            Action functionCopy = (Action)function.Clone();
            Timer t = new Timer { Interval = interval, AutoReset = autoReset };
            t.Elapsed += (sender, e) => functionCopy();
            t.Start();

            return new TimerInterrupter(t);
        }
    }

    public interface IInterruptable
    {
        void Stop();
    }

    public class TimerInterrupter : IInterruptable
    {
        private readonly Timer t;

        public TimerInterrupter(Timer timer)
        {
            if (timer == null) throw new ArgumentNullException();
            t = timer;
        }

        public void Stop()
        {
            t.Stop();
        }
    }

    public class ObjectPool<T> //@cond unused
    {
        private Func<T> generator;
        private Action<T> recycler;

        private T[] pool;
        private int cursor = 0;

        public ObjectPool(int size, Func<T> generator, Action<T> recycler = null)
        {
            this.pool = new T[size];
            this.recycler = recycler;
            this.generator = generator;
            for (int i = 0; i < size; i++)
            {
                pool[i] = generator();
            }
        }

        public T Get()
        {
            T next = pool[cursor];
            if (recycler != null) recycler(next);
            cursor = ++cursor < pool.Length ? cursor : 0;
            return next;
        }
    }//@endcond

    public static class Exts
    {
        public delegate void Action<T1, T2, T3, T4, T5>
            (T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);

        public static void Apply<T>(this IList<T> il, Action<T, T, T, T, T> action)
        {
            action(il[0], il[1], il[2], il[3], il[4]);
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> ie)
        {
            if (ie == null) return true;
            var coll = ie as ICollection<T>;
            return (coll != null) ? coll.Count < 1 : !ie.Any();
        }

        public static bool IsNumber(this object value) // ext
        {
            return value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is int
                    || value is uint
                    || value is long
                    || value is ulong
                    || value is float
                    || value is double
                    || value is decimal;
        }

        public static string TrimEnds(this string str, char start, char ends)
        {
            return str.TrimFirst(start).TrimLast(ends);
        }

        public static string TrimFirst(this string str, char c)
        {
            return (str[0] == c) ? str.Substring(1) : str;
        }

        public static string TrimLast(this string str, char c)
        {
            int last = str.Length - 1;
            return (str[last] == c) ? str.Substring(0, last) : str;
        }

        public static string Stringify(this object o)
        {
            if (o == null) return "NULL";

            string s = "";
            if (o is IDictionary)
            {
                IDictionary id = (System.Collections.IDictionary)o;
                if (id.Count > 0)
                {
                    s += "{ ";
                    foreach (var k in id.Keys) s += k + ":" + id[k] + ",";
                    s = s.Substring(0, s.Length - 1) + " }";
                }
            }
            //else if (o is Match) {}
            else if (o is object[])
            {
                var arr = ((object[])o);
                s = "[ ";
                for (int i = 0; i < arr.Length; i++)
                {
                    s += arr[i];
                    if (i < arr.Length - 1) s += ",";
                }
                s += " ]";
            }
            else if (o is ICollection)
            {
                var coll = (ICollection)o;
                s = "[ ";
                foreach (var k in coll) s += k + ",";
                s = (s.Substring(0, s.Length - 1) + " ]");
            }
            else
            {
                s = o.ToString();
            }
            return s;
        }
    }
}
