using NUnit.Framework;
using Dialogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
                Assert.That(kv.Value, Is.GreaterThan(0));
            }
        }

        [Test()]
        public void TestCommandCopy()
        {
            Chat chat = ChatParser.ParseText("SAY Thank you { pace = fast}")[0];
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Say orig = (Say)chat.commands[0];
            Say clone = (Say)orig.Copy();
            clone.SetMeta("pace", "slow");
            Assert.That(clone.GetType(), Is.EqualTo(typeof(Say)));
            Assert.That(clone.Text, Is.EqualTo("Thank you"));
            Assert.That(clone.GetMeta("pace"), Is.EqualTo("slow"));
            Assert.That(clone.GetMeta("pace"), Is.Not.EqualTo(orig.GetMeta("pace")));
        }
    }
}
