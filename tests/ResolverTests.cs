using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Dialogic
{
    [TestFixture]
    public class ResolverTests
    {
        const bool NO_VALIDATORS = true;

        public static IDictionary<string, object> globals
            = new Dictionary<string, object>() {
                { "obj-prop", "dog" },
                { "animal", "dog" },
                { "prep", "then" },
                { "group", "(a|b)" },
                { "cmplx", "($group | $prep)" },
                { "count", 4 }
        };

        [Test]
        public void ASimpleVar()
        {
            var res = Resolver.BindSymbols("$a", null, new Dictionary<string, object>()
                {{ "a", "hello" }, { "b", "32" }});
            Assert.That(res, Is.EqualTo("hello"));

            res = Resolver.BindSymbols("$a!", null, new Dictionary<string, object>()
                {{ "a", "hello" }, { "b", "32" }});
            Assert.That(res, Is.EqualTo("hello!"));
        }

        [Test]
        public void ParseSymbols()
        {
            var ts = new[] { "", ".", "!", ":", ";", ",", "?", ")", "\"", "'" };
            foreach (var t in ts) Assert.That(Resolver.ParseSymbols
                ("$a" + t).First().symbol, Is.EqualTo("a"));

            Assert.That(Resolver.ParseSymbols("${a}").First().symbol, Is.EqualTo("a"));
            Assert.That(Resolver.ParseSymbols("${a.b}").First().symbol, Is.EqualTo("a.b"));

            Assert.That(Resolver.ParseSymbols("$a.b").First().symbol, Is.EqualTo("a.b"));
            Assert.That(Resolver.ParseSymbols("[b=$a]").First().symbol, Is.EqualTo("a"));

            Assert.That(Resolver.ParseSymbols("[bc=$a.b]").First().symbol, Is.EqualTo("a.b"));
            Assert.That(Resolver.ParseSymbols("[bc=$a.b]").First().alias, Is.EqualTo("bc"));

            Assert.That(Resolver.ParseSymbols("[c=$a.b]").First().symbol, Is.EqualTo("a.b"));
            Assert.That(Resolver.ParseSymbols("[c=$a.b]").First().alias, Is.EqualTo("c"));

            Assert.That(Resolver.ParseSymbols("[ c=$a.b]").First().symbol, Is.EqualTo("a.b"));
            Assert.That(Resolver.ParseSymbols("[ c=$a.b]").First().alias, Is.EqualTo("c"));

            Assert.That(Resolver.ParseSymbols("[c= $a.b]").First().symbol, Is.EqualTo("a.b"));
            Assert.That(Resolver.ParseSymbols("[c= $a.b]").First().alias, Is.EqualTo("c"));

            Assert.That(Resolver.ParseSymbols("[c=$a.b ]").First().symbol, Is.EqualTo("a.b"));
            Assert.That(Resolver.ParseSymbols("[c=$a.b ]").First().alias, Is.EqualTo("c"));

            Assert.That(Resolver.ParseSymbols("[c=$a.b].").First().symbol, Is.EqualTo("a.b"));
            Assert.That(Resolver.ParseSymbols("[c=$a.b].").First().alias, Is.EqualTo("c"));

            Assert.That(Resolver.ParseSymbols("[c=${a.b}].").First().symbol, Is.EqualTo("a.b"));
            Assert.That(Resolver.ParseSymbols("[c=${a.b}].").First().alias, Is.EqualTo("c"));


            Assert.That(Resolver.ParseSymbols("${a}b").First().symbol, Is.EqualTo("a"));
            Assert.That(Resolver.ParseSymbols("${a.b}b").First().symbol, Is.EqualTo("a.b"));

            //Assert.That(Resolver.ParseSymbols("${a})").First().symbol, Is.EqualTo("a"));
            //Assert.That(Resolver.ParseSymbols("${a}b)").First().symbol, Is.EqualTo("a"));

        }

        [Test]
        public void GroupRecursionDepth()
        {
            var str = "CHAT c\n(I | (You | (doh | why | no) | ouY) | They)";
            str += "(want | hate | like | love)(coffee | bread | milk)";
            Chat chat = ChatParser.ParseText(str, NO_VALIDATORS)[0];
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Say say = (Dialogic.Say)chat.commands[0];
            say.Realize(null);
            var s = say.Text();
            Assert.That(s, Is.Not.EqualTo(""));
        }

        [Test]
        public void ParsedVar()
        {
            Chat chat = ChatParser.ParseText("SAY Thank $count")[0];
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Command say = chat.commands[0];
            Assert.That(say.GetType(), Is.EqualTo(typeof(Say)));
            say.Realize(globals);
            Assert.That(say.Text(), Is.EqualTo("Thank 4"));
            Assert.That(say.realized[Meta.TYPE], Is.EqualTo("Say"));
        }

        [Test]
        public void Exceptions()
        {
            //// no replace to be made
            Assert.That(globals.ContainsKey("a"), Is.False);
            Assert.Throws<UnboundSymbolException>(() => Resolver.BindSymbols("$a", null, globals));

            //// replacement leads to infinite loop
            //Assert.Throws<RealizeException>(() => realizer.Do("$a",
            //    new Dictionary<string, object>() {
            //         { "a", "$bb" },
            //         { "bb", "$a" }
            //}));

            // replacement leads to infinite loop
            //Assert.Throws<RealizeException>(() => realizer.Do("$a",
            //    new Dictionary<string, object>() {
            //         { "a", "($a | $b)" },
            //         { "b", "$a" },
            //}));
        }

        [Test]
        public void MetaReplaceValue()
        {
            Chat chat = ChatParser.ParseText("SAY Thank you { pace = fast}")[0];
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Command c = (Say)chat.commands[0];
            Assert.That(c.GetType(), Is.EqualTo(typeof(Say)));
            c.Realize(globals);
            c.SetMeta("pace", "slow");

            Assert.That(c.Text(), Is.EqualTo("Thank you"));
            Assert.That(c.realized[Meta.TYPE], Is.EqualTo("Say"));
            Assert.That(c.realized["pace"], Is.EqualTo("fast"));
            Assert.That(c.GetMeta("pace"), Is.EqualTo("slow"));

            chat = ChatParser.ParseText("SAY Thank you { pace=$animal}")[0];
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Say)));
            c = (Say)chat.commands[0];
            Assert.That(c.GetType(), Is.EqualTo(typeof(Say)));
            c.Realize(globals);
            c.SetMeta("pace", "slow");
            Assert.That(c.Realized(Meta.TEXT), Is.EqualTo("Thank you"));
            Assert.That(c.Realized(Meta.TYPE), Is.EqualTo("Say"));
            Assert.That(c.Realized("pace"), Is.EqualTo("dog"));
            Assert.That(c.GetMeta("pace"), Is.EqualTo("slow"));

            chat = ChatParser.ParseText("SAY Thank you { pace=$obj-prop}")[0];
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Say)));
            c = (Say)chat.commands[0];
            Assert.That(c.GetType(), Is.EqualTo(typeof(Say)));
            c.Realize(globals);
            c.SetMeta("pace", "slow");
            Assert.That(c.Realized(Meta.TEXT), Is.EqualTo("Thank you"));
            Assert.That(c.Realized(Meta.TYPE), Is.EqualTo("Say"));
            Assert.That(c.Realized("pace"), Is.EqualTo("dog"));
            Assert.That(c.GetMeta("pace"), Is.EqualTo("slow"));
        }

        [Test]
        public void TextVar()
        {
            Chat chat = ChatParser.ParseText("SAY Thank $count { pace=$animal}")[0];
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Command say = chat.commands[0];
            Assert.That(say.GetType(), Is.EqualTo(typeof(Say)));
            say.Realize(globals);
            Assert.That(say.Text(), Is.EqualTo("Thank 4"));
            say.text = "Thank you";
            Assert.That(say.text, Is.EqualTo("Thank you"));
            Assert.That(say.realized[Meta.TYPE], Is.EqualTo("Say"));
            Assert.That(say.realized["pace"], Is.EqualTo("dog"));
        }

        [Test]
        public void TextGroup()
        {
            var ok = new string[] { "The boy was sad", "The boy was happy", "The boy was dead" };
            Chat chat = ChatParser.ParseText("SAY The boy was (sad | happy | dead)")[0];
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Command c = (Say)chat.commands[0];
            Assert.That(c.GetType(), Is.EqualTo(typeof(Say)));
            c.Realize(globals);
            Assert.That(c.realized[Meta.TYPE], Is.EqualTo("Say"));
            CollectionAssert.Contains(ok, c.Text());
        }

        [Test]
        public void ComplexReplacement()
        {
            // "cmplx" -> "($group | $prep)" 
            // "prep"  -> "then" },
            // "group" -> "(a|b)" },
            string[] ok = { "letter a", "letter b", "letter then" };
            Chat chat = ChatParser.ParseText("SAY letter $cmplx")[0];
            Command c = chat.commands[0];
            Assert.That(c.GetType(), Is.EqualTo(typeof(Say)));
            for (int i = 0; i < 10; i++)
            {
                c.Realize(globals);
                var txt = c.Text();
                //Console.WriteLine(i+") "+txt);
                CollectionAssert.Contains(ok, txt);
            }
        }

        [Test]
        public void ReplaceGroups()
        {
            var txt = "The boy was (sad | happy)";
            string[] ok = { "The boy was sad", "The boy was happy" };
            for (int i = 0; i < 10; i++)
            {
                CollectionAssert.Contains(ok, Resolver.BindGroups(txt));
            }

            txt = "The boy was (sad | happy | dead)";
            ok = new string[] { "The boy was sad", "The boy was happy", "The boy was dead" };
            for (int i = 0; i < 10; i++)
            {
                string s = Resolver.BindGroups(txt);
                //Console.WriteLine(i + ") " + s);
                CollectionAssert.Contains(ok, s);
            }
        }

        [Test]
        public void MetaReplaceGroups()
        {
            List<Chat> chats = ChatParser.ParseText("DO emote {type=(A|B)}");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Do)));
            Do doo = (Do)chats[0].commands[0];
            Assert.That(doo.text, Is.EqualTo("emote"));
            Assert.That(doo.HasMeta("type"), Is.EqualTo(true));
            Assert.That(doo.GetMeta("type"), Is.EqualTo("(A|B)"));

            for (int i = 0; i < 10; i++)
            {
                doo.Realize(globals);
                Assert.That(doo.Text(), Is.EqualTo("emote"));
                //Console.WriteLine(doo.GetRealized("type"));
                Assert.That(doo.Realized("type"), Is.EqualTo("A").Or.EqualTo("B"));
            }
        }

        [Test]
        public void ReplaceVarsBug()
        {
            var globs = new Dictionary<string, object>() {
                { "a", "$a2" },
                { "a2", "C" },
             };

            var s = @"SAY $a $a2";
            s = Resolver.BindSymbols(s, null, globs);
            Assert.That(s, Is.EqualTo("SAY C C"));
        }


        [Test]
        public void ReplaceVars()
        {
            var s = @"SAY The $animal woke $count times";
            s = Resolver.BindSymbols(s, null, globals);
            Assert.That(s, Is.EqualTo("SAY The dog woke 4 times"));

            s = @"SAY The $obj-prop woke $count times";
            s = Resolver.BindSymbols(s, null, globals);
            Assert.That(s, Is.EqualTo("SAY The dog woke 4 times"));
        }

        [Test]
        public void ReplaceVarsGroups()
        {
            string s;
   
            s = @"SAY The $animal woke and $prep (ate|ate)";
            s = Resolver.Bind(s, null, globals);
            Assert.That(s, Is.EqualTo("SAY The dog woke and then ate"));

            s = @"SAY The $obj-prop woke and $prep (ate|ate)";
            s = Resolver.Bind(s, null, globals);
            Assert.That(s, Is.EqualTo("SAY The dog woke and then ate"));

            //s = realizer.Do("$a", new Dictionary<string, object>()
            //    {{ "a", "($a | $b)" }, { "b", "32" }});
            //Assert.That(s, Is.EqualTo("32"));

            string txt = "letter $group";
            for (int i = 0; i < 10; i++)
            {
                Assert.That(Resolver.Bind(txt, null, globals),
                    Is.EqualTo("letter a").Or.EqualTo("letter b"));
            }

            var txt2 = "letter $cmplx";
            var ok = new string[] { "letter a", "letter b", "letter then" };
            string[] res = new string[10];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = Resolver.Bind(txt2, null, globals);
            }
            for (int i = 0; i < res.Length; i++)
            {
                CollectionAssert.Contains(ok, res[i]);
            }
        }

        [Test]
        public void SayNonRepeatingRecomb()
        {
            List<Chat> chats = ChatParser.ParseText("SAY (a|b|c)");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Say say = (Say)chats[0].commands[0];
            string last = "";
            for (int i = 0; i < 10; i++)
            {
                say.Realize(globals);
                string said = say.Text();
                Assert.That(said, Is.Not.EqualTo(last));
                last = said;
            }
        }

        [Test]
        public void AskNonRepeatingRecomb()
        {
            List<Chat> chats = ChatParser.ParseText("ASK (a|b|c)?\nOPT (c|d|e) #f");
            Assert.That(chats.Count, Is.EqualTo(1));
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Ask)));
            Ask ask = (Ask)chats[0].commands[0];
            string last = "", lastOpts = "";
            for (int i = 0; i < 10; i++)
            {
                ask.Realize(globals);
                string asked = ask.Text();
                string opts = ask.JoinOptions();
                //Console.WriteLine(i+") "+asked+" "+opts);
                Assert.That(asked, Is.Not.EqualTo(last));
                Assert.That(opts, Is.Not.EqualTo(lastOpts));
                lastOpts = opts;
                last = asked;
            }
        }

        [Test]
        public void ReplacePrompt()
        {
            List<Chat> chats = ChatParser.ParseText("ASK Want a $animal?\nOPT $group #Game\n\nOPT $count #End");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Ask)));

            Ask ask = (Ask)chats[0].commands[0];
            Assert.That(ask.text, Is.EqualTo("Want a $animal?"));
            ask.Realize(globals);

            Assert.That(ask.text, Is.EqualTo("Want a $animal?"));
            Assert.That(ask.Text(), Is.EqualTo("Want a dog?"));
            Assert.That(ask.Options().Count, Is.EqualTo(2));

            var options = ask.Options();
            Assert.That(options[0].GetType(), Is.EqualTo(typeof(Opt)));
            //Assert.That(options[0].Text, Is.EqualTo("Y").Or.);
            CollectionAssert.Contains(new string[] { "a", "b" }, options[0].Text());
            Assert.That(options[0].action.GetType(), Is.EqualTo(typeof(Go)));
            Assert.That(options[1].GetType(), Is.EqualTo(typeof(Opt)));
            Assert.That(options[1].Text(), Is.EqualTo("4"));
            Assert.That(options[1].action.GetType(), Is.EqualTo(typeof(Go)));


            chats = ChatParser.ParseText("ASK Want a $obj-prop?\nOPT $group #Game\n\nOPT $count #End");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Ask)));

            ask = (Ask)chats[0].commands[0];
            ask.Realize(globals);
            Assert.That(ask.text, Is.EqualTo("Want a $obj-prop?"));
            Assert.That(ask.Text(), Is.EqualTo("Want a dog?"));

            Assert.That(ask.Options().Count, Is.EqualTo(2));

            options = ask.Options();
            Assert.That(options[0].GetType(), Is.EqualTo(typeof(Opt)));
            //Assert.That(options[0].Text, Is.EqualTo("Y").Or.);
            CollectionAssert.Contains(new string[] { "a", "b" }, options[0].Text());
            Assert.That(options[0].action.GetType(), Is.EqualTo(typeof(Go)));
            Assert.That(options[1].GetType(), Is.EqualTo(typeof(Opt)));
            Assert.That(options[1].Text(), Is.EqualTo("4"));
            Assert.That(options[1].action.GetType(), Is.EqualTo(typeof(Go)));
        }
    }
}