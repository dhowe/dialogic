using NUnit.Framework;
using Dialogic;
using System;
using System.Collections.Generic;
using System.IO;

namespace tests
{
    [TestFixture()]
    public class DialogicTests
    {
        [Test()]
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
                //Console.WriteLine(kv.Key +"-> " + kv.Value);
                Assert.That(kv.Value, Is.GreaterThan(0));
            }
        }
    }
}
