using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Timers;

namespace Dialogic
{
    /** Static utility functions */
    public static class Util
    {
        private static int start = Environment.TickCount;
        private static Random random;

        public static int Elapsed()
        {
            return Environment.TickCount - start;
        }

        public static void Init()
        {
            start = Environment.TickCount;
        }

        public static int Millis() {
            return Environment.TickCount & Int32.MaxValue;
        }

        public static double Rand()
        {
            if (random == null) random = new Random();
            return random.NextDouble();
        }

        public static int Rand(int min, int max)
        {
            if (random == null) random = new Random();
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
            if (random == null) random = new Random();
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
                    + Util.Millis() + "\t" + msg);
            }
        }

        public static IEnumerable<string> SortByLength(IEnumerable<string> e)
        {
            return from s in e orderby s.Length descending select s;
        }

        public static Nullable<T> ToNullable<T>(this string s) where T : struct
        {
            Nullable<T> result = new Nullable<T>();
            try
            {
                if (!string.IsNullOrEmpty(s) && s.Trim().Length > 0)
                {
                    TypeConverter conv = TypeDescriptor.GetConverter(typeof(T));
                    result = (T)conv.ConvertFrom(s);    
                }
            }
            catch { }
            return result;
        }

        public static T? GetValueOrNull<T>(this string valueAsString) where T : struct
        {
            if (string.IsNullOrEmpty(valueAsString))
                return null;
            return (T)Convert.ChangeType(valueAsString, typeof(T));
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

        public static string ReadLine(Command source, int timeOutMillisecs = System.Threading.Timeout.Infinite)
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
    /*#region Methods
    public static T Copy<T>(this T source)
    {
        var isNotSerializable = !typeof(T).IsSerializable;
        if (isNotSerializable)
            throw new ArgumentException("The type must be serializable.", "source");

        if (object.ReferenceEquals(source, null)) return default(T);

        var formatter = new BinaryFormatter();
        using (var stream = new MemoryStream())
        {
            formatter.Serialize(stream, source);
            stream.Seek(0, SeekOrigin.Begin);
            return (T)formatter.Deserialize(stream);
        }
    }
    #endregion*/
}
