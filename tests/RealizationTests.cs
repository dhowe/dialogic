using System;
using NUnit.Framework;
using Dialogic;
using System.Collections.Generic;

namespace dialogic
{
    [TestFixture]
    public class RealizationTests
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
            Console.WriteLine(Util.Stringify(data));
            c.SetMeta("pace", "slow");
            Assert.That(data["text"], Is.EqualTo("Thank you"));
            Assert.That(data["type"], Is.EqualTo("Say"));
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
            Console.WriteLine(Util.Stringify(data));
            c.SetMeta("pace", "slow");
            Assert.That(data["text"], Is.EqualTo("Thank you"));
            Assert.That(data["type"], Is.EqualTo("Say"));
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
            Assert.That(c.data["text"], Is.EqualTo("Thank 4"));
            Assert.That(c.Text, Is.EqualTo("Thank you"));
            Assert.That(c.data["type"], Is.EqualTo("Say"));
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
            Assert.That(c.data["type"], Is.EqualTo("Say"));
            CollectionAssert.Contains(ok, c.data["text"]);
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
                CollectionAssert.Contains(ok, c.data["text"]);
            }
        }
    }
}