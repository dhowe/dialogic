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

	}      
}
