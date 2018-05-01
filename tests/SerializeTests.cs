﻿using System;
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

        [Test]
        public void SaveRestoreChat()
        {
            var lines = new[] {
                 "CHAT Test {type=a,stage=b}",
                 "SET ab = hello",
                 "DO flip",
                 "SAY ab",
             };
            Chat c1, c2;
            ChatRuntime rtOut, rtIn;

            var text = String.Join("\n", lines);
            rtIn = new ChatRuntime(Tendar.AppConfig.Actors);
            rtIn.ParseText(text);

            // serialize the runtime to bytes
            var bytes = Serializer.ToBytes(rtIn);

            // create a new runtime from the bytes
            rtOut = ChatRuntime.Create(bytes, AppConfig.Actors);

            // check they are identical
            Assert.That(rtIn, Is.EqualTo(rtOut));

            // double-check the chats themselves
            c1 = rtIn.Chats().First();
            c2 = rtOut.Chats().First();

            Assert.That(c1, Is.EqualTo(c2));
            Assert.That(c1.text, Is.EqualTo(c2.text));
            for (int i = 0; i < c1.commands.Count; i++)

            {
                var cmd1 = c1.commands[i];
                var cmd2 = c2.commands[i];
                Assert.That(c1.commands[i], Is.EqualTo(c2.commands[i]));
            }

            // no dynamics, so output should be the same
            var res1 = rtIn.InvokeImmediate(globals);
            var res2 = rtOut.InvokeImmediate(globals);
            Assert.That(res1, Is.EqualTo(res2));
        }


        //[Test] // TODO: working here
        public void SaveRestoreChats()
        {
            ChatRuntime rtOut, rtIn;
            var testfile = AppDomain.CurrentDomain.BaseDirectory;
            testfile += "../dialogic/data/noglobal.gs";

            rtIn = new ChatRuntime(Tendar.AppConfig.Actors);
            rtIn.ParseFile(testfile);

            var bytes = Serializer.ToBytes(rtIn);

            rtOut = ChatRuntime.Create(bytes, AppConfig.Actors);

            // check they are identical
            Assert.That(rtIn, Is.EqualTo(rtOut));

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
                // Console.WriteLine(t1 + "\n\n" + t2 + "\n\n");
                Assert.That(c1.commands.Count, Is.EqualTo(c2.commands.Count));


                Assert.That(t1, Is.EqualTo(t2));
            }
        }
    }
}

