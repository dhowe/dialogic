using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Dialogic
{
    [TestFixture]
    public class RealizeTests
    {
        public static IDictionary<string, object> globals = new Dictionary<string, object>() {
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
            c.SetMeta("pace", "slow");

            Assert.That(c.GetText(true), Is.EqualTo("Thank you"));
            Assert.That(c.realized[Meta.TYPE], Is.EqualTo("Say"));
            Assert.That(c.realized["pace"], Is.EqualTo("fast"));
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
            var data = c.realized;
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
            Command say = chat.commands[0];
            Assert.That(say.GetType(), Is.EqualTo(typeof(Say)));
            say.Realize(globals);
            Assert.That(say.GetText(true), Is.EqualTo("Thank 4"));
            say.Text = "Thank you";
            Assert.That(say.Text, Is.EqualTo("Thank you"));
            Assert.That(say.realized[Meta.TYPE], Is.EqualTo("Say"));
            Assert.That(say.realized["pace"], Is.EqualTo("dog"));
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
            Assert.That(c.realized[Meta.TYPE], Is.EqualTo("Say"));
            CollectionAssert.Contains(ok, c.GetText(true));
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
                CollectionAssert.Contains(ok, c.GetText(true));
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
       
            s = @"SAY The $animal woke and $prep (ate|ate)";
            s = Realizer.Do(s, globals);
            Assert.That(s, Is.EqualTo("SAY The dog woke and then ate"));
  
            var txt = "letter $group";
            string[] ok = { "letter a", "letter b" };
            for (int i = 0; i < 10; i++)
            {
                CollectionAssert.Contains(ok, Realizer.Do(txt, globals));
            }
  
            txt = "letter $cmplx";
            ok = new string[]{ "letter a", "letter b", "letter then" };
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

        [Test]
        public void TestSayNonRepeatingRecombination()
        {
            List<Chat> chats = ChatParser.ParseText("SAY (a|b)");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Say say = (Say)chats[0].commands[0];
            string last = "";
            for (int i = 0; i < 10; i++)
            {
                say.Realize(globals);
                string said = say.GetText(true);
                Assert.That(said, Is.Not.EqualTo(last));
                last = said;
            }
        }

        [Test]
        public void TestAskNonRepeatingRecombination()
        {
            List<Chat> chats = ChatParser.ParseText("ASK (a|b)?\nOPT (c|d|e) #f");
            Assert.That(chats.Count, Is.EqualTo(1));
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Ask)));
            Ask ask = (Ask)chats[0].commands[0];
            string last = "", lastOpts = "";
            for (int i = 0; i < 10; i++)
            {
                ask.Realize(globals);
                string asked = ask.GetText(true);
                string opts = ask.OptionsJoined();
                //Console.WriteLine(i+") "+asked+" "+opts);
                Assert.That(asked, Is.Not.EqualTo(last));
                Assert.That(opts, Is.Not.EqualTo(lastOpts));
                lastOpts = opts;
                last = asked;
            }
        }

        [Test]
        public void TestReplacePrompt()
        {
            List<Chat> chats = ChatParser.ParseText("ASK Want a $animal?\nOPT $group #Game\n\nOPT $count #End");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Ask)));

            Ask ask = (Dialogic.Ask)chats[0].commands[0];
            ask.Realize(globals);
            Assert.That(ask.Text, Is.EqualTo("Want a $animal?"));
            Assert.That(ask.GetText(true), Is.EqualTo("Want a dog?"));

            Assert.That(ask.Options().Count, Is.EqualTo(2));

            var options = ask.Options();
            Assert.That(options[0].GetType(), Is.EqualTo(typeof(Opt)));
            //Assert.That(options[0].Text, Is.EqualTo("Y").Or.);
            CollectionAssert.Contains(new string[] { "a", "b" }, options[0].GetText(true));
            Assert.That(options[0].action.GetType(), Is.EqualTo(typeof(Go)));
            Assert.That(options[1].GetType(), Is.EqualTo(typeof(Opt)));
            Assert.That(options[1].GetText(true), Is.EqualTo("4"));
            Assert.That(options[1].action.GetType(), Is.EqualTo(typeof(Go)));
        }
    }
}