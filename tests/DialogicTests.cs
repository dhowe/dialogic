using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using System;

namespace Dialogic
{
    [TestFixture]
    public class DialogicTests
    {
        [Test]
        public void TestSecStrToMs()
        {
            Assert.That(Util.SecStrToMs("1"),Is.EqualTo(1000));
            Assert.That(Util.SecStrToMs("1.5"), Is.EqualTo(1500));
            Assert.That(Util.SecStrToMs(".5"), Is.EqualTo(500));
            Assert.That(Util.SecStrToMs("0.5"), Is.EqualTo(500));
            Assert.That(Util.SecStrToMs("1x"), Is.EqualTo(-1));
            Assert.That(Util.SecStrToMs("1x",1000), Is.EqualTo(1000));
        }

        [Test]
        public void TestSimpleGrammar()
        {
            string f = TestContext.CurrentContext.TestDirectory + "/data/grammar.yaml";
            string exp = "You look juicy. I see big things in your future. "+
                "But also small ones. The judges agree, 3.5 of 10. Try again, " +
                "this time with feeling.";
            var gram = ChatParser.ParseGrammar(new FileInfo(f));
            var outp = gram.Expand("<grammar1>");
            //Console.WriteLine(outp);
            Assert.That(outp, Is.EqualTo(exp));
        }

        [Test]
        public void TestRandItem()
        {
            string[] arr = { "0", "1", "2" };
            Dictionary<string, int> hits;
            hits = new Dictionary<string, int>();
            foreach (var s in arr) hits.Add(s, 0);
            for (int i = 0; i < 100; i++)
            {
                var s = Util.RandItem(arr);
                hits[((string)s)]++;
                CollectionAssert.Contains(arr, s);
            }
            foreach (var kv in hits)
            {
                Assert.That(kv.Value, Is.GreaterThan(0));
            }
        }

        [Test]
        public void TestCommandTiming()
        {
            Say fast = (Say)ChatParser.ParseText("SAY Thank you { speed=fast}")[0].commands[0];
            Say defa = (Say)ChatParser.ParseText("SAY Thank you")[0].commands[0];
            Say slow = (Say)ChatParser.ParseText("SAY Thank you{speed=slow }")[0].commands[0];
            Assert.That(defa.ComputeDuration(), Is.EqualTo(fast.ComputeDuration() * 2).Within(1));
            Assert.That(slow.ComputeDuration(), Is.EqualTo(defa.ComputeDuration() * 2).Within(1));
            Say longer = (Say)ChatParser.ParseText("SAY Thank you very much")[0].commands[0];
            Assert.That(longer.ComputeDuration(), Is.GreaterThan(defa.ComputeDuration()));
            Assert.That(fast.Text, Is.EqualTo(defa.Text));
            Assert.That(slow.Text, Is.EqualTo(defa.Text));
        }
    }
}
