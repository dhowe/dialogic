using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

namespace Dialogic
{
    static class Util
    {
        public static void Log(string logFileName, object msg) {
            using (StreamWriter w = File.AppendText(logFileName))
            {
                w.WriteLine(DateTime.Now.ToLongTimeString() + "\t"
                    + Environment.TickCount + "\t" + msg);
            }
        }

        public static IEnumerable<string> SortByLength(IEnumerable<string> e)
        {
            return from s in e orderby s.Length descending select s;
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
