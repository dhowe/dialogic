using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Client;
using MessagePack;

namespace Dialogic
{
	[TestFixture]
	public class SerializeTests : GenericTests
	{
		static ISerializer serializer = new SerializerMessagePack();

		static bool RUN_PROFILING_TESTS = false;

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
		public void SaveRestoreChats()
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

		[Test]
		public void AppendNewChats()
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
			rt.UpdateFrom(serializer, rt2);

			s = rt.InvokeImmediate(null);
			Assert.That(s, Is.EqualTo("Find\nAdded"));
		}

		// Pass instance to ChatRuntime serialization methods
		private class SerializerMessagePack : ISerializer
		{
			static readonly IFormatterResolver ifr = MessagePack.Resolvers
				.ContractlessStandardResolverAllowPrivate.Instance;

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

