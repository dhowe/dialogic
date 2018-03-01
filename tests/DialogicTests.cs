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
    }
}
