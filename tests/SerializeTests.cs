using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Dialogic;
using Tendar;

namespace Dialogic
{
    [TestFixture]
    public class SerializeTests
    {
        const bool NO_VALIDATORS = true;

        public static IDictionary<string, object> globals
             = new Dictionary<string, object>() {
                { "obj-prop", "dog" },
                { "animal", "dog" },
                { "prep", "then" },
                { "group", "(a|b)" },
                { "cmplx", "($group | $prep)" },
                { "count", 4 }
         };

        //[Test] TODO: in progress
        public void SaveRestoreChats()
        {
            var testfile = AppDomain.CurrentDomain.BaseDirectory;
            testfile += "../dialogic/data/noglobal.gs";
            ChatRuntime rtOut, rtIn = new ChatRuntime(AppConfig.Actors);
            rtIn.ParseFile(testfile);

            var bytes = Serializer.ToBytes(rtIn);
            Serializer.FromBytes(rtOut = new ChatRuntime(AppConfig.Actors), bytes);

            //Console.WriteLine(rtOut.ToString());

            Assert.That(rtOut.ToString(), Is.EqualTo(rtIn.ToString()));
        }
    }
}
