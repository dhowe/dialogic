using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

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
}
