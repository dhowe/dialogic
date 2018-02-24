using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using System.Text.RegularExpressions;

namespace Dialogic
{
    /** Static utility functions */

    public static class Util
    {
        private static int start;
        private static Random random;

        static Util()
        {
            start = Environment.TickCount;
            random = new Random();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> ie)
        {
            if (ie == null) return true;
            var coll = ie as ICollection<T>;
            return (coll != null) ? coll.Count < 1 : !ie.Any();
        }

        public static int Elapsed()
        {
            return Environment.TickCount - start;
        }

        public static string ElapsedSec()
        {
            return (Elapsed() / 1000.0).ToString("0.##") + "s";
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

        public static object RandItem(object[] arr)
        {
            return arr[Rand(arr.Length)];
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

        public static string Stringify(object o)
        {
            if (o == null) return "NULL";

            string s = "";
            if (o is IDictionary)
            {
                IDictionary id = (System.Collections.IDictionary)o;
                if (id.Count > 0)
                {
                    s += "{";
                    foreach (var k in id.Keys) s += k + ":" + id[k] + ",";
                    s = s.Substring(0, s.Length - 1) + "}";
                }
            }
            else if (o is object[])
            {
                var arr = ((object[])o);
                s = "[";
                for (int i = 0; i < arr.Length; i++)
                {
                    s += arr[i];
                    if (i < arr.Length - 1) s += ",";
                }
            }
            else if (o is IList)
            {
                var list = (IList)o;
                s = "[";
                for (int i = 0; i < list.Count; i++)
                {
                    s += list[i].ToString(); 
                    if (i < list.Count - 1) s += ",";
                }
            }
            else 
            {
                s = o.ToString();
            }
            return s;
        }

        public static IEnumerable<string> SortByLength(IEnumerable<string> e)
        {
            return from s in e orderby s.Length descending select s;
        }

        public static T? StringToType<T>(this string valueAsString) where T : struct
        {
            if (string.IsNullOrEmpty(valueAsString)) return null;
            return (T)Convert.ChangeType(valueAsString, typeof(T));
        }

        public static int ToMillis(double seconds)
        {
            return (int)(seconds * 1000);
        }

        public static double ToSec(int millis)
        {
            return millis/1000.0;
        }

        /**
         * Attempts to parse the string as primitive double, int, or bool, 
         * if such conversion is possible, else returns the input
         */
        public static object ToType(string val)
        {
            bool result = false;
            object valObj = val;
            if (!result)
            {
                bool b;
                result = Boolean.TryParse(val, out b);
                if (result) valObj = b;
            }
            if (!result)
            {
                int i;
                result = int.TryParse(val, out i);
                if (result) valObj = i;
            }
            if (!result)
            {
                double d;
                result = Double.TryParse(val, out d);
                if (result) valObj = d;
            }

            return valObj;
        }
    }

    public class Constraints
    {
        IDictionary<string, object> pairs;

        public Constraints()
        {
            pairs = new Dictionary<string, object>();
        }

        public Constraints(string key, string value) : this()
        {
            this.Add(key, value);
        }

        public Constraints(string op, string key, string value) : this()
        {
            this.Add(op, key, value);
        }

        public Constraints Add(Constraint c)
        {
            pairs.Add(c.name, c);
            return this;
        }

        public Constraints Add(string key, string value) 
        {
            return Add(new Constraint(key, value));
        }

        public Constraints Add(string op, string key, string value)
        {
            return Add(new Constraint(op, key, value));
        }

        public IDictionary<string, object> AsDict()
        {
            return pairs;
        }
    }

    public class Operator
    {
        private enum OpType { EQUALITY, COMPARISON, MATCHING }

        public static Operator EQ = new Operator("==", OpType.EQUALITY);
        public static Operator NEQ = new Operator("!=", OpType.EQUALITY);
        public static Operator SW = new Operator("^=", OpType.MATCHING);
        public static Operator EW = new Operator("$=", OpType.MATCHING);
        public static Operator RE = new Operator("*=", OpType.MATCHING);
        public static Operator GT = new Operator(">", OpType.COMPARISON);
        public static Operator LT = new Operator("<", OpType.COMPARISON);
        public static Operator LTE = new Operator("<=", OpType.COMPARISON);
        public static Operator GTE = new Operator(">=", OpType.COMPARISON);

        public static Operator[] ALL = new Operator[] {
            GT, LT, EQ, NEQ, LTE, GTE, SW, EQ, RE
        };

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
                catch (Exception)
                {
                    throw new OperatorException(this);
                }
            }
            throw new OperatorException(this, "Unexpected Op type: ");
        }
    }

    /** Reads input from console */
    public static class ConsoleReader
    {
        private static System.Threading.Thread inputThread;
        private static System.Threading.AutoResetEvent getInput, gotInput;
        private static string input;

        static ConsoleReader()
        {
            getInput = new System.Threading.AutoResetEvent(false);
            gotInput = new System.Threading.AutoResetEvent(false);
            inputThread = new System.Threading.Thread(Reader) { IsBackground = true };
            inputThread.Start();
        }

        private static void Reader()
        {
            while (true)
            {
                getInput.WaitOne();
                input = Console.ReadKey(true).KeyChar.ToString();
                gotInput.Set();
            }
        }

        public static string ReadLine(Command source, int timeOutMillisecs = -1)
        {
            getInput.Set();
            bool success = gotInput.WaitOne(timeOutMillisecs);
            if (success) return input;
            throw new PromptTimeout(source);
        }
    }

    // adapted from:
    //   https://codereview.stackexchange.com/questions/113596/writing-cs-analog-of-settimeout-setinterval-and-clearinterval
    public static class Timers
    {
        static IInterruptable timer;

        public static IInterruptable SetInterval(int interval, Action function)
        {
            return timer = StartTimer(interval, function, true);
        }

        public static IInterruptable SetTimeout(int interval, Action function)
        {
            //Console.WriteLine("SetTimeout: "+interval+" "+function);
            return timer = StartTimer(interval, function, false);
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

    public class ObjectPool<T>
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
    }
}
