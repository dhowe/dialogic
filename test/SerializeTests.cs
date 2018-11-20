using System;
using NUnit.Framework;
using System.Linq;
using System.IO;
using System.Threading;

using Client;
using MessagePack;
using Abbotware.Interop.NUnit;

namespace Dialogic.Test
{
    [TestFixture]
    public class SerializeTests : GenericTests
    {
        static bool RUN_PROFILING_TESTS = false;

        ISerializer serializer;

        [SetUp]
        public void SetUpSerializer()
        {
            serializer = new SerializerMessagePack();
        }

        [Test]
        public void SerializationPerformance()
        {
            if (!RUN_PROFILING_TESTS) return;

            ChatRuntime.VERIFY_UNIQUE_CHAT_LABELS = false;

            ChatRuntime rtOut, rtIn;
            byte[] bytes = null;
            int iterations = 10;

            var testfile = AppDomain.CurrentDomain.BaseDirectory;
            testfile += "../../../../dialogic/data/allchats.gs";

            rtIn = new ChatRuntime(Client.AppConfig.TAC);

            var watch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                rtIn.ParseFile(new FileInfo(testfile));
            }
            var numChats = rtIn.Chats().Count;
            watch.Stop(); Console.WriteLine("Parsed " + numChats
                + " chats in " + watch.ElapsedMilliseconds / 1000.0 + "s");

            for (int i = 0; i < iterations; i++)
            {
                watch = System.Diagnostics.Stopwatch.StartNew();
                bytes = serializer.ToBytes(rtIn);
                watch.Stop();
                Console.WriteLine("Serialize #" + i + ": "
                    + watch.ElapsedMilliseconds / 1000.0 + "s");
            }

            for (int i = 0; i < iterations; i++)
            {
                watch = System.Diagnostics.Stopwatch.StartNew();
                rtOut = ChatRuntime.Create(serializer, bytes, AppConfig.TAC);
                watch.Stop(); Console.WriteLine("Deserialize #" + i + ": "
                    + watch.ElapsedMilliseconds / 1000.0 + "s");
            }
        }

        [Test, Timeout(1000)]
        public void SaveAsync()
        {
            var blocker = new AutoResetEvent(false);

            var file = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + Util.EpochMs() + ".ser");
            var lines = new[] {
                 "CHAT switch {type=a,stage=b,other=c}",
                 "SAY async",
             };
            ChatRuntime rt = new ChatRuntime(Client.AppConfig.TAC);
            rt.ParseText(String.Join("\n", lines));
            rt.SaveAsync(serializer, file, (bytes) =>
            {

                blocker.Set();
                //Console.WriteLine("CB: " + (bytes != null ? bytes.Length + " bytes" : "Failed"));
                Assert.That(bytes, Is.Not.Null);
                Assert.That(bytes.Length, Is.GreaterThan(0));

                // create a new runtime from the bytes
                var rt2 = ChatRuntime.Create(serializer, bytes, AppConfig.TAC);

                // and verify they are the same
                CheckEquals(rt, rt2);
            });

            blocker.WaitOne();
        }

        [Test, Timeout(2000)]
        public void SaveAsyncAndRepeat()
        {
            var blocker = new AutoResetEvent(false);
            var blocker2 = new AutoResetEvent(false);

            var file = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + Util.EpochMs() + ".ser");
            var lines = new[] {
                 "CHAT switch {type=a,stage=b,other=c}",
                 "SAY async",
             };

            ChatRuntime rt = new ChatRuntime(Client.AppConfig.TAC);
            rt.ParseText(String.Join("\n", lines));
            rt.SaveAsync(serializer, file, (bytes) =>
            {
                blocker.Set();
                //Console.WriteLine(file + ": " + (bytes != null ? bytes.Length + " bytes" : "Failed"));
                Assert.That(bytes, Is.Not.Null);
                Assert.That(bytes.Length, Is.GreaterThan(0));

                // create a new runtime from the bytes
                var rt2 = ChatRuntime.Create(serializer, bytes, AppConfig.TAC);

                // and verify they are the same
                CheckEquals(rt, rt2);

                file = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + Util.EpochMs() + ".ser");
                rt2.SaveAsync(serializer, file, (bytes2) =>
                {
                    blocker2.Set();
                    //Console.WriteLine(file + ": " + (bytes != null ? bytes.Length + " bytes" : "Failed"));
                    Assert.That(bytes, Is.Not.Null);
                    Assert.That(bytes.Length, Is.GreaterThan(0));

                    // create a new runtime from the bytes
                    var rt3 = ChatRuntime.Create(serializer, bytes, AppConfig.TAC);
                    CheckEquals(rt, rt3);
                });

            });

            blocker2.WaitOne();
            blocker.WaitOne();

        }

        [Test, Timeout(2000)]
        public void SaveMultipleAsync()
        {
            var blocker1 = new AutoResetEvent(false);
            var file1 = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + Util.EpochMs() + ".ser");
            var lines1 = new[] {
                 "CHAT switch {type=a1,stage=b1,other=async1}",
                 "SAY async1",
             };
            ChatRuntime rt1 = new ChatRuntime(Client.AppConfig.TAC);
            rt1.ParseText(String.Join("\n", lines1));


            //var blocker2 = new AutoResetEvent(false);
            var file2 = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + Util.EpochMs() + ".ser");
            var lines2 = new[] {
                 "CHAT switch2 {type=a2,stage=2,other=async2}",
                 "DO #flip",
                 "SAY async2",
             };
            ChatRuntime rt2 = new ChatRuntime(Client.AppConfig.TAC);
            rt2.ParseText(String.Join("\n", lines2));


            rt1.SaveAsync(serializer, file1, (bytes) =>
            {
                Thread.Sleep(50);
                blocker1.Set();
                //Console.WriteLine("CB1: " + (bytes != null ? bytes.Length + " bytes" : "Failed"));
                Assert.That(bytes, Is.Not.Null);
                Assert.That(bytes.Length, Is.GreaterThan(0));

                // create a new runtime from the bytes
                var rtDeser1 = ChatRuntime.Create(serializer, bytes, AppConfig.TAC);

                // and verify they are the same
                CheckEquals(rt1, rtDeser1);

            });

            rt2.SaveAsync(serializer, file2, (bytes) =>
            {
                //blocker2.Set();
                //Console.WriteLine("CB2: " + (bytes != null ? bytes.Length + " bytes" : "Failed"));
                Assert.That(bytes, Is.Not.Null);
                Assert.That(bytes.Length, Is.GreaterThan(0));

                // create a new runtime from the bytes
                var rtDeser2 = ChatRuntime.Create(serializer, bytes, AppConfig.TAC);

                // and verify they are the same
                CheckEquals(rt2, rtDeser2);

            });

            blocker1.WaitOne();
            //blocker2.WaitOne();
        }

        [Test, Timeout(1000)]
        public void MergeAsync()
        {
            var blocker = new AutoResetEvent(false);

            var lines = new[] {
                 "CHAT Test {type=a,stage=b}",
                 "SET ab = hello",
                 "DO flip",
                 "SAY ab",
             };

            var file = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + Util.EpochMs() + ".ser");
            ChatRuntime rt = new ChatRuntime(Client.AppConfig.TAC);
            rt.ParseText(String.Join("\n", lines));
            rt.Save(serializer, file);

            ChatRuntime rt2 = new ChatRuntime(Client.AppConfig.TAC);
            rt2.MergeAsync(serializer, file, () =>
            {

                blocker.Set();
                //Console.WriteLine("CALLBACK: "+ (rt2.chats != null ? rt2.chats.Count + " chats" : "Failed"));
                Assert.That(rt2.chats, Is.Not.Null);
                Assert.That(rt2.chats.Count, Is.GreaterThan(0));
            });

            blocker.WaitOne();
        }


        [Test]
        public void SaveAndRestoreChat()
        {
            var lines = new[] {
                 "CHAT Test {type=a,stage=b}",
                 "SET ab = hello",
                 "DO flip",
                 "SAY ab",
             };

            var text = String.Join("\n", lines);
            var rtIn = new ChatRuntime(Client.AppConfig.TAC);
            rtIn.ParseText(text);

            // serialize the runtime to bytes
            var bytes = serializer.ToBytes(rtIn);

            // create a new runtime from the bytes
            var rtOut = ChatRuntime.Create(serializer, bytes, AppConfig.TAC);

            // and verify they are the same
            CheckEquals(rtOut, rtIn);
        }

        [Test]
        public void SaveAndRestoreMulti()
        {
            var lines = new[] {
                 "CHAT Test {type=a,stage=b}",
                 "SET ab = hello",
                 "DO flip",
                 "ASK How do you feel?",
                 "OPT Good #goodChat",
                 "OPT Bad #badChat",
                 "CHAT goodChat {type=a,stage=b}",
                 "SAY Its Good",
                 "CHAT badChat {type=a,stage=b}",
                 "SAY Its Bad",
             };

            var text = String.Join("\n", lines);
            var rtIn = new ChatRuntime(AppConfig.TAC);
            rtIn.ParseText(text);
            var orig = rtIn;

            for (int i = 0; i < 5; i++)
            {
                // serialize the runtime to bytes
                var bytes = serializer.ToBytes(rtIn);

                // create a new runtime from the bytes
                var rtOut = ChatRuntime.Create(serializer, bytes, AppConfig.TAC);

                //Console.WriteLine("Check#" + i + ": " + rtOut.GetHashCode());

                // and verify they are the same
                CheckEquals(rtOut, orig, true);

                rtIn = rtOut;
            }

            //Console.WriteLine(rtIn);
        }

        private void CheckEquals(ChatRuntime r1, ChatRuntime r2, bool hasDynamics = false)//, bool ignoreStaleness = false)
        {
            // check they are identical
            Assert.That(r2, Is.EqualTo(r1), "FAILED\n" + r1 + "\n" + r2);
       
            // if no dynamics, output should be the same
            var res1 = r2.InvokeImmediate(globals, null, true);
            var res2 = r1.InvokeImmediate(globals, null, true);

            if (!hasDynamics) Assert.That(res1, Is.EqualTo(res2));
        }

        private void RemoveStateleness(Chat c)
        {
            c.meta.Remove(Meta.STALENESS);
            if (c.HasMeta(Meta.STALENESS)) throw new Exception("FALENESS");
        }

        [Test]
        public void SaveAndRestoreChats()
        {
            ChatRuntime rtOut, rtIn;

            var testfile = AppDomain.CurrentDomain.BaseDirectory;
            testfile += "../../../../dialogic/data/noglobal.gs";

            rtIn = new ChatRuntime(Client.AppConfig.TAC);
            rtIn.ParseFile(new FileInfo(testfile));

            var bytes = serializer.ToBytes(rtIn);

            rtOut = ChatRuntime.Create(serializer, bytes, AppConfig.TAC);

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


        public void SaveAndRestoreChatWithAsk()
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
            rtIn = new ChatRuntime(Client.AppConfig.TAC);
            rtIn.ParseText(text);

            // serialize the runtime to bytes
            var bytes = serializer.ToBytes(rtIn);

            // create a new runtime from the bytes
            rtOut = ChatRuntime.Create(serializer, bytes, AppConfig.TAC);

            // check they are identical
            Assert.That(rtIn, Is.EqualTo(rtOut));

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
                Assert.That(cmd1.parent, Is.Not.Null);

                var cmd2 = c2.commands[i];
                Assert.That(cmd2.parent, Is.Not.Null);

                Assert.That(c1.commands[i], Is.EqualTo(c2.commands[i]));
            }

            // no dynamics, so output should be the same
            var res1 = rtIn.InvokeImmediate(globals);
            var res2 = rtOut.InvokeImmediate(globals);
            Assert.That(res1, Is.EqualTo(res2));
        }

        [Test]
        public void MergeFromRuntime()
        {
            var lines = new[] {
                 "CHAT Test {type=a,stage=b}",
                 "SAY Find",
                 "FIND {type=a,stage=b,other=c}",
                 "CHAT next {type=a,stage=b}",
                 "SAY Done",
             };

            ChatRuntime rt;

            rt = new ChatRuntime(Client.AppConfig.TAC);
            rt.ParseText(String.Join("\n", lines));

            var s = rt.InvokeImmediate(null);
            Assert.That(s, Is.EqualTo("Find\nDone"));


            // Add more chats via Update, with higher search score
            var lines2 = new[] {
                 "CHAT switch {type=a,stage=b,other=c}",
                 "SAY Added",
             };

            ChatRuntime rt2 = new ChatRuntime(Client.AppConfig.TAC);
            rt2.ParseText(String.Join("\n", lines2));

            // append the 2nd runtime to the first
            rt.Merge(serializer, rt2);

            s = rt.InvokeImmediate(null);
            Assert.That(s, Is.EqualTo("Find\nAdded"));
        }

        [Test]
        public void MergeFromBytes()
        {
            var lines = new[] {
                 "CHAT Test {type=a,stage=b}",
                 "SAY Find",
                 "FIND {type=a,stage=b,other=c}",
                 "CHAT next {type=a,stage=b}",
                 "SAY Done",
             };

            ChatRuntime rt;

            rt = new ChatRuntime(Client.AppConfig.TAC);
            rt.ParseText(String.Join("\n", lines));

            var s = rt.InvokeImmediate(null);
            Assert.That(s, Is.EqualTo("Find\nDone"));


            // Add more chats via Update, with higher search score
            var lines2 = new[] {
                 "CHAT switch {type=a,stage=b,other=c,statelness=}",
                 "SAY Added",
             };

            ChatRuntime rt2 = new ChatRuntime(Client.AppConfig.TAC);
            rt2.ParseText(String.Join("\n", lines2));

            // append the 2nd runtime to the first
            rt.Merge(serializer, serializer.ToBytes(rt2));

            s = rt.InvokeImmediate(null);
            Assert.That(s, Is.EqualTo("Find\nAdded"));
        }

        [Test]
        public void SerializeToJSON()
        {
            ChatRuntime rt = new ChatRuntime();
            rt.ParseText("CHAT Test { type = a,stage = b}");
            Assert.That(rt.ToJSON(serializer), Is.Not.Null);
        }

        [Test]
        public void MergeFromFile()
        {
            var lines = new[] {
                 "CHAT Test {type=a,stage=b}",
                 "SAY Find",
                 "FIND {type=a,stage=b,other=c}",
                 "CHAT next {type=a,stage=b}",
                 "SAY Done",
             };

            ChatRuntime rt;

            rt = new ChatRuntime(Client.AppConfig.TAC);
            rt.ParseText(String.Join("\n", lines));

            var s = rt.InvokeImmediate(null);
            Assert.That(s, Is.EqualTo("Find\nDone"));


            // Add more chats via Update, with higher search score
            var lines2 = new[] {
                 "CHAT switch {type=a,stage=b,other=c,statelness=}",
                 "SAY Added",
             };

            var file = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + Util.EpochMs() + ".ser");

            ChatRuntime rt2 = new ChatRuntime(Client.AppConfig.TAC);
            rt2.ParseText(String.Join("\n", lines2));
            rt2.Save(serializer, file);

            // append the 2nd runtime to the first
            rt.Merge(serializer, file);

            s = rt.InvokeImmediate(null);
            Assert.That(s, Is.EqualTo("Find\nAdded"));
        }


        // Pass instance to ChatRuntime serialization methods
        private class SerializerMessagePack : ISerializer
        {
            private readonly IFormatterResolver resolver;

            public SerializerMessagePack()
            {
                resolver = MessagePack.Resolvers
                    .ContractlessStandardResolverAllowPrivate.Instance;
            }

            public byte[] ToBytes(ChatRuntime rt)
            {
                return MessagePackSerializer.Serialize<Snapshot>(Snapshot.Create(rt), resolver);
            }

            public void FromBytes(ChatRuntime rt, byte[] bytes)
            {
                MessagePackSerializer.Deserialize<Snapshot>(bytes, resolver).Update(rt);
            }

            public string ToJSON(ChatRuntime rt)
            {
                return MessagePackSerializer.ToJson(ToBytes(rt));
            }
        }
    }
}

