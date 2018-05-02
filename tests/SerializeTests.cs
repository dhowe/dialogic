using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Tendar;
using MessagePack;

namespace Dialogic
{
    [TestFixture]
    public class SerializeTests
    {
        static ISerializer serializer = new SerializerMessagePack();

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
            var bytes = serializer.ToBytes(rtIn);

            // create a new runtime from the bytes
            rtOut = ChatRuntime.Create(serializer, bytes, AppConfig.Actors);

            // check they are identical
            Assert.That(rtIn, Is.EqualTo(rtOut));

            // double-check the chats themselves
            c1 = rtIn.Chats().First();
            c2 = rtOut.Chats().First();

            Assert.That(c1, Is.EqualTo(c2));
            Assert.That(c1.ToTree(), Is.EqualTo(c2.ToTree()));
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

        [Test]
        public void SaveRestoreChatWithAsk()
        {
            var lines = new[] {
                 "CHAT Test {type=a,stage=b}",
                 "ASK Is it ok?",
                 "OPT yes #next ",
                 "OPT no #next",
                 "CHAT next {type=a,stage=b}",
                 "SAY Done",
             };
            Chat c1, c2;
            ChatRuntime rtOut, rtIn;

            var text = String.Join("\n", lines);
            rtIn = new ChatRuntime(Tendar.AppConfig.Actors);
            rtIn.ParseText(text);

            // serialize the runtime to bytes
            var bytes = serializer.ToBytes(rtIn);

            // create a new runtime from the bytes
            rtOut = ChatRuntime.Create(serializer, bytes, AppConfig.Actors);

            // check they are identical
            //Assert.That(rtIn, Is.EqualTo(rtOut));

            // double-check the chats themselves
            c1 = rtIn.Chats().First();
            c2 = rtOut.Chats().First();

            //Console.WriteLine(c1.ToTree()+"\n\n"+c2.ToTree());

            Assert.That(c1, Is.EqualTo(c2));
            Assert.That(c1.ToTree(), Is.EqualTo(c2.ToTree()));
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


        [Test]
        public void SaveRestoreChats()
        {
            ChatRuntime rtOut, rtIn;
            var testfile = AppDomain.CurrentDomain.BaseDirectory;
            testfile += "../dialogic/data/noglobal.gs";

            rtIn = new ChatRuntime(Tendar.AppConfig.Actors);
            rtIn.ParseFile(new FileInfo(testfile));

            var bytes = serializer.ToBytes(rtIn);

            rtOut = ChatRuntime.Create(serializer, bytes, AppConfig.Actors);

            // check they are identical
            Assert.That(rtIn, Is.EqualTo(rtOut));

            var inCmds = rtIn.Chats();
            var outCmds = rtOut.Chats();

            Assert.That(rtOut.ToString(), Is.EqualTo(rtIn.ToString()));

            Assert.That(inCmds.Count, Is.EqualTo(outCmds.Count));
            for (int i = 0; i < inCmds.Count; i++)
            {
                var chat1 = inCmds.ElementAt(i);
                var chat2 = outCmds.ElementAt(i);
                Assert.That(chat1.text, Is.EqualTo(chat2.text));
                Assert.That(chat1.commands.Count, Is.EqualTo(chat2.commands.Count));
                Assert.That(chat1.ToTree(), Is.EqualTo(chat2.ToTree()));
            }
        }

        [Test]
        public void AppendChatsToExistingRuntime()
        {
            // Working here...
        }

        // Implement ISerializer and then instance to ChatRuntime methods...
        private class SerializerMessagePack : ISerializer
        {
            IFormatterResolver ifr =
                MessagePack.Resolvers.ContractlessStandardResolverAllowPrivate.Instance;

            public byte[] ToBytes(ChatRuntime rt)
            {
                return MessagePackSerializer.Serialize<Snapshot>(Snapshot.Create(rt), ifr);
            }

            public void FromBytes(ChatRuntime rt, byte[] bytes)
            {
                MessagePackSerializer.Deserialize<Snapshot>(bytes, ifr).Update(rt);
            }

            public string ToJSON(ChatRuntime rt)
            {
                return MessagePackSerializer.ToJson(ToBytes(rt), ifr);
            }
        }
    }
}

