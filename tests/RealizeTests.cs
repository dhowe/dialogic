using NUnit.Framework;
using System.Collections.Generic;

namespace Dialogic
{
    [TestFixture]
    public class RealizeTests
    {
        IDictionary<string, object> globals = new Dictionary<string, object>() {
                { "animal", "dog" },
                { "prep", "then" },
                { "group", "(a|b)" },
                { "cmplx", "($group | $prep)" },
                { "count", 4 }
        };

        [Test]
        public void TestMeta()
        {
            Chat chat = ChatParser.ParseText("SAY Thank you { pace = fast}")[0];
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Command c = (Say)chat.commands[0];
            Assert.That(c.GetType(), Is.EqualTo(typeof(Say)));
            c.Realize(globals);
            var data = c.data;
            //Console.WriteLine(Util.Stringify(data));
            c.SetMeta("pace", "slow");
            Assert.That(data[Meta.TEXT], Is.EqualTo("Thank you"));
            Assert.That(data[Meta.TYPE], Is.EqualTo("Say"));
            Assert.That(data["pace"], Is.EqualTo("fast"));
            Assert.That(c.GetMeta("pace"), Is.EqualTo("slow"));
        }

        [Test]
        public void TestMetaVar()
        {
            Chat chat = ChatParser.ParseText("SAY Thank you { pace=$animal}")[0];
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Command c = (Say)chat.commands[0];
            Assert.That(c.GetType(), Is.EqualTo(typeof(Say)));
            c.Realize(globals);
            var data = c.data;
            //Console.WriteLine(Util.Stringify(data));
            c.SetMeta("pace", "slow");
            Assert.That(data[Meta.TEXT], Is.EqualTo("Thank you"));
            Assert.That(data[Meta.TYPE], Is.EqualTo("Say"));
            Assert.That(data["pace"], Is.EqualTo("dog"));
            Assert.That(c.GetMeta("pace"), Is.EqualTo("slow"));
        }

        [Test]
        public void TestTextVar()
        {
            Chat chat = ChatParser.ParseText("SAY Thank $count { pace=$animal}")[0];
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Command c = (Say)chat.commands[0];
            Assert.That(c.GetType(), Is.EqualTo(typeof(Say)));
            c.Realize(globals);
            c.Text = "Thank you";
            Assert.That(c.data[Meta.TEXT], Is.EqualTo("Thank 4"));
            Assert.That(c.Text, Is.EqualTo("Thank you"));
            Assert.That(c.data[Meta.TYPE], Is.EqualTo("Say"));
            Assert.That(c.data["pace"], Is.EqualTo("dog"));
        }

        [Test]
        public void TestTextGroup()
        {
            var ok = new string[] { "The boy was sad", "The boy was happy", "The boy was dead" };
            Chat chat = ChatParser.ParseText("SAY The boy was (sad | happy | dead)")[0];
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Command c = (Say)chat.commands[0];
            Assert.That(c.GetType(), Is.EqualTo(typeof(Say)));
            c.Realize(globals);
            Assert.That(c.data[Meta.TYPE], Is.EqualTo("Say"));
            CollectionAssert.Contains(ok, c.data[Meta.TEXT]);
        }

        [Test]
        public void TestComplex()
        {
            string[] ok = { "letter a", "letter b", "letter then" };
            Chat chat = ChatParser.ParseText("SAY letter $cmplx")[0];
            Command c = (Say)chat.commands[0];
            Assert.That(c.GetType(), Is.EqualTo(typeof(Say)));
            for (int i = 0; i < 10; i++)
            {
                c.Realize(globals);
                CollectionAssert.Contains(ok, c.data[Meta.TEXT]);
            }
        }

        [Test]
        public void TestReplaceGroups()
        {
            var txt = "The boy was (sad | happy)";
            string[] ok = { "The boy was sad", "The boy was happy" };
            for (int i = 0; i < 10; i++)
            {
                CollectionAssert.Contains(ok, Realizer.DoGroups(txt));
            }

            txt = "The boy was (sad | happy | dead)";
            ok = new string[] { "The boy was sad", "The boy was happy", "The boy was dead" };
            for (int i = 0; i < 10; i++)
            {
                string s = Realizer.DoGroups(txt);
                //Console.WriteLine(i + ") " + s);
                CollectionAssert.Contains(ok, s);
            }
        }

        [Test]
        public void TestReplaceVars()
        {
            var s = @"SAY The $animal woke $count times";
            s = Realizer.DoVars(s, globals);
            Assert.That(s, Is.EqualTo("SAY The dog woke 4 times"));
        }

        [Test]
        public void TestReplace1()
        {
            var s = @"SAY The $animal woke and $prep (ate|ate)";
            s = Realizer.Do(s, globals);
            Assert.That(s, Is.EqualTo("SAY The dog woke and then ate"));
        }

        [Test]
        public void TestReplace2()
        {
            var txt = "letter $group";
            string[] ok = { "letter a", "letter b" };
            for (int i = 0; i < 10; i++)
            {
                CollectionAssert.Contains(ok, Realizer.Do(txt, globals));
            }
        }

        [Test]
        public void TestReplace3()
        {
            var txt = "letter $cmplx";
            string[] ok = { "letter a", "letter b", "letter then" };
            string[] res = new string[10];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = Realizer.Do(txt, globals);
            }
            for (int i = 0; i < res.Length; i++)
            {
                CollectionAssert.Contains(ok, res[i]);
            }
        }
    }
}