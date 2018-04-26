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

        //[Test]
        public void SaveRestoreChat()
        {
            var lines = new[] {
                "CHAT Test {type=a,stage=b}",
                "SET ab = hello",
                "DO flip",
                "SAY ab",
            };
            var text = String.Join("\n", lines);
            ChatRuntime rtOut, rtIn = new ChatRuntime(AppConfig.Actors);
            rtIn.ParseText(text);
            var c1 = rtIn.Chats().First();
            Console.WriteLine(c1.ToTree());

            var bytes = Serializer.ToBytes(rtIn);
            rtOut = new ChatRuntime(AppConfig.Actors);
            Serializer.FromBytes(rtOut, bytes);
            var c2 = rtOut.Chats().First();


            Assert.That(c1, Is.EqualTo(c2));
            Assert.That(c1.text, Is.EqualTo(c2.text));
            Assert.That(c1.commands.Count, Is.EqualTo(c2.commands.Count));
            for (int i = 0; i < c1.commands.Count; i++)
            {
                var cmd1 = c1.commands[i];
                var cmd2 = c2.commands[i];

            }

            Console.WriteLine(c2.ToTree());
        }

        //[Test]
        public void SaveRestoreChats()
        {
            var testfile = AppDomain.CurrentDomain.BaseDirectory;
            testfile += "../dialogic/data/noglobal.gs";
            ChatRuntime rtOut, rtIn = new ChatRuntime(AppConfig.Actors);
            rtIn.ParseFile(testfile);

            var bytes = Serializer.ToBytes(rtIn);
            Console.WriteLine( Serializer.ToJSON(rtIn));
            //Snapshot.DBUG = false;

            Serializer.FromBytes(rtOut = new ChatRuntime(AppConfig.Actors), bytes);

            var inl = rtIn.Chats();
            var outl = rtOut.Chats();

            Assert.That(inl.Count, Is.EqualTo(outl.Count));
            Assert.That(rtOut.ToString(), Is.EqualTo(rtIn.ToString()));

            for (int i = 0; i < inl.Count; i++)
            {
                var c1 = inl.ElementAt(i);
                var c2 = outl.ElementAt(i);
                var t1 = c1.ToTree();
                var t2 = c2.ToTree();
                Console.WriteLine(t1 + "\n\n" + t2 + "\n\n");
                Assert.That(c1.commands.Count, Is.EqualTo(c2.commands.Count));


                Assert.That(t1, Is.EqualTo(t2));
            }
        }
    }
}
