using System;
using System.IO;

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
    }
}
