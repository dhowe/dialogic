using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Dialogic
{
	[TestFixture]
	public class RuntimeTests : GenericTests
	{
		[Test]
        public void PreloadingTest()
        {
            string[] lines = new[] {
                "CHAT c1",
                "SET ab = hello",
                "SAY $ab $de",

                "CHAT c2 {preload=true}",
                "SET $de = preload",
            };
            ChatRuntime rt = new ChatRuntime();
            rt.ParseText(String.Join("\n", lines), true);
            rt.Preload(globals);

            var s = rt.InvokeImmediate(globals);
            Assert.That(s, Is.EqualTo("hello preload"));
        }

        [Test]
        public void PreloadingBindingTest()
        {
            string[] lines = new[] {
                "CHAT c1",
                "SAY $d $e",

                "CHAT c2 {preload=true}",
                "SET $d = hello",
                "SET $e = $emotion",
            };
            ChatRuntime rt = new ChatRuntime();
            rt.ParseText(String.Join("\n", lines), true);
            rt.Preload(globals);

            globals.Add("emotion", "darkness");

            var s = rt.InvokeImmediate(globals);
            Assert.That(s, Is.EqualTo("hello darkness"));
        }

        [Test]
        public void SubstringIssue()
        {
            ChatRuntime rt = new ChatRuntime();
            //Resolver.DBUG = true;
            rt.ParseText("SET $a = A\nSET $b = $a1\nSET $a1 = B\nSAY $a $b\n", true);
            var s = rt.InvokeImmediate(globals);
            Assert.That(s, Is.EqualTo("A B"));
        }

        [Test]
        public void PreloadingBindingFunc()
        {
            string[] lines = new[] {
                "CHAT c1",
                "SAY $d $e",

                "CHAT c2 {preload=true}",
                "SET $d = hello",
                "SET $e = $emotion.Cap()",
            };
            ChatRuntime rt = new ChatRuntime();
            rt.ParseText(String.Join("\n", lines), true);
            rt.Preload(globals);

            globals.Add("emotion", "darkness");

            var s = rt.InvokeImmediate(globals);
            Assert.That(s, Is.EqualTo("hello Darkness"));
        }


		[Test]
		public void StalenessEventTest()
		{
			string[] lines = {
				"CHAT c1 {type=a}",
				"CHAT c2 {type=a,day=fri}",
				"CHAT c3 {type=b,day=thurs}",
				"CHAT c4"
			};

			string contents = String.Join("\n", lines);
			List<Chat> chats = ChatParser.ParseText(contents, NO_VALIDATORS);
			ChatRuntime rt = new ChatRuntime(chats);

			//chats.ForEach(Console.WriteLine);
			chats.ForEach(c => Assert.That(c.Staleness(), Is.EqualTo(Defaults.CHAT_STALENESS)));
			EventArgs icu = new StalenessUpdate(5);
			rt.Update(null, ref icu);
			chats.ForEach(c => Assert.That(c.Staleness(), Is.EqualTo(5)));

			icu = new StalenessUpdate(100, "#c4");
			rt.Update(null, ref icu);
			Assert.That(rt.FindChatByLabel("c1").Staleness(), Is.EqualTo(5));
			Assert.That(rt.FindChatByLabel("c4").Staleness(), Is.EqualTo(100));

			icu = new StalenessUpdate(10, "{!type=a}");
			rt.Update(null, ref icu);

			new AutoResetEvent(false).WaitOne(20); // async hack for C# 4.0

			//chats.ForEach(Console.WriteLine);
			Assert.That(rt.FindChatByLabel("c1").Staleness(), Is.EqualTo(10));
			Assert.That(rt.FindChatByLabel("c2").Staleness(), Is.EqualTo(10));
			Assert.That(rt.FindChatByLabel("c3").Staleness(), Is.EqualTo(5));
			Assert.That(rt.FindChatByLabel("c4").Staleness(), Is.EqualTo(100));
		}




		[Test]
		public void ValidateParensTest()
		{
			string[] lines = new[] {
				"CHAT c1",
				"SET ab = (a | b))",
				"SAY $ab"
			};
			ChatRuntime rt = new ChatRuntime();
			rt.ParseText(String.Join("\n", lines), true);
			Assert.Throws<MismatchedParens>(() => rt.InvokeImmediate(globals));
		}

		//[Test]
		public void ResumeEventTest()
		{
			string[] lines = {
				"CHAT c1 {type=a}",
				"CHAT c2 {type=b,day=fri}",
				"CHAT c3 {type=c,day=thurs}",
			};

			string contents = String.Join("\n", lines);
			List<Chat> chats = ChatParser.ParseText(contents, NO_VALIDATORS);
			ChatRuntime rt = new ChatRuntime(chats);

			for (int i = 0; i < 3; i++)
			{
				EventArgs icu = new ResumeEvent("{}");
				rt.Update(null, ref icu);
			}
		}

	}      
}
