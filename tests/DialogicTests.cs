using System.Collections.Generic;
using NUnit.Framework;
using System;

namespace Dialogic
{
    [TestFixture]
    public class DialogicTests
    {
        [Test]
        public void RuntimeIndexTest()
        {
            var rt = new ChatRuntime();
            rt.ParseText("CHAT c1\nSAY OK");
            Assert.That(rt.Chats()[0], Is.Not.Null);
            Assert.That(rt["c1"], Is.Not.Null); // ???
        }

        [Test]
        public void EntityDecodeTest()
        {
            Assert.That(Html.Decode("&amp;"), Is.EqualTo("&"));
            Assert.That(Html.Decode("&nbsp;"), Is.EqualTo(" "));
        }

        [Test]
        public void ListPopExt()
        {
            var ints = new List<int>() {4,7,2,3};
            Assert.That(ints.Pop(), Is.EqualTo(3));
            Assert.That(ints.Count, Is.EqualTo(3));
        }

        [Test]
        public void SecStrToMsTest()
        {
            Assert.That(Util.SecStrToMs("1"), Is.EqualTo(1000));
            Assert.That(Util.SecStrToMs("1.5"), Is.EqualTo(1500));
            Assert.That(Util.SecStrToMs(".5"), Is.EqualTo(500));
            Assert.That(Util.SecStrToMs("0.5"), Is.EqualTo(500));
            Assert.That(Util.SecStrToMs("1x"), Is.EqualTo(-1));
            Assert.That(Util.SecStrToMs("1x", 1000), Is.EqualTo(1000));
        }

        [Test]
        public void UpdateEventTest()
        {
            var globals = new Dictionary<string, object>() {
                { "animal", "dog" },
                { "temp", "23.3" },
                { "isok", "true" },
                { "count", "4" }
             };
            UpdateEvent ue = new UpdateEvent(globals);
            Assert.That(ue.Data().Count, Is.EqualTo(4));

            Assert.That(ue.Get("animal"), Is.EqualTo("dog"));
            Assert.That(ue.Get("MISS1"), Is.Null);
            Assert.That(ue.Get("MISS2", "cat"), Is.EqualTo("cat"));

            Assert.That(ue.GetInt("count"), Is.EqualTo(4));
            Assert.That(ue.GetInt("MISS1"), Is.EqualTo(-1));
            Assert.That(ue.GetInt("MISS2", 0), Is.EqualTo(0));

            Assert.That(ue.GetDouble("temp"), Is.EqualTo(23.3d));
            Assert.That(ue.GetDouble("MISS1"), Is.EqualTo(-1d));
            Assert.That(ue.GetDouble("MISS2", 0), Is.EqualTo(0d));

            Assert.That(ue.GetFloat("temp"), Is.EqualTo(23.3f));
            Assert.That(ue.GetFloat("MISS1"), Is.EqualTo(-1f));
            Assert.That(ue.GetFloat("MISS2", 0), Is.EqualTo(0f));

            Assert.That(ue.GetBool("isok"), Is.EqualTo(true));
            Assert.That(ue.GetBool("MISS1"), Is.EqualTo(false));
            Assert.That(ue.GetBool("MISS2", true), Is.EqualTo(true));
        }

        [Test]
        public void TrimFirstLast()
        {
            string a;

            a = "a=";
            Util.TrimFirst(ref a, '=');
            Assert.That(a, Is.EqualTo("a="));

            Util.TrimLast(ref a, '=');
            Assert.That(a, Is.EqualTo("a"));

            a = "=a";
            Util.TrimLast(ref a, '=');
            Assert.That(a, Is.EqualTo("=a"));

            Util.TrimFirst(ref a, '=');
            Assert.That(a, Is.EqualTo("a"));

            a = "=a";
            Util.TrimLast(ref a, '=', ':');
            Assert.That(a, Is.EqualTo("=a"));

            a = "a=:";
            Util.TrimLast(ref a, '=', ':');
            Assert.That(a, Is.EqualTo("a="));

            a = "a:=";
            Util.TrimLast(ref a, '=', ':');
            Assert.That(a, Is.EqualTo("a:"));

            a = "a:=";
            Util.TrimLast(ref a, '=', ':');
            Util.TrimLast(ref a, '=', ':');
            Assert.That(a, Is.EqualTo("a"));

            a = "a:==:====";
            do { } while (Util.TrimLast(ref a, '=', ':'));
            Assert.That(a, Is.EqualTo("a"));
        }


        [Test]
        public void RandItemTest()
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

            List<string> l = new List<string>() { "0", "1", "2" };
            hits = new Dictionary<string, int>();
            foreach (var s in l) hits.Add(s, 0);
            for (int i = 0; i < 100; i++)
            {
                string s = Util.RandItem(l);
                hits[s]++;
                CollectionAssert.Contains(arr, s);
            }
            foreach (var kv in hits)
            {
                Assert.That(kv.Value, Is.GreaterThan(0));
            }
        }

        [Test]
        public void TimingTest()
        {
            Say fast = (Say)ChatParser.ParseText("SAY Thank you { speed=fast}")[0].commands[0];
            Say defa = (Say)ChatParser.ParseText("SAY Thank you")[0].commands[0];
            Say slow = (Say)ChatParser.ParseText("SAY Thank you{speed=slow }")[0].commands[0];
            Assert.That(defa.ComputeDuration(), Is.EqualTo(fast.ComputeDuration() * 2).Within(1));
            Assert.That(slow.ComputeDuration(), Is.EqualTo(defa.ComputeDuration() * 2).Within(1));
            Say longer = (Say)ChatParser.ParseText("SAY Thank you very much")[0].commands[0];
            Assert.That(longer.ComputeDuration(), Is.GreaterThan(defa.ComputeDuration()));
            Assert.That(fast.text, Is.EqualTo(defa.text));
            Assert.That(slow.text, Is.EqualTo(defa.text));
        }
    }
}
