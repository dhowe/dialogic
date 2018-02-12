using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;

namespace Dialogic
{
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
