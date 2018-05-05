using NUnit.Framework;
using System;
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
        public void SimpleSymbolTraversal()
        {
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            var res = rt.resolver.Bind("Hello $fish.name", c1, globals);
            Assert.That(res, Is.EqualTo("Hello Fred"));

            res = rt.resolver.Bind("Hello $fish.name.", c1, globals);
            Assert.That(res, Is.EqualTo("Hello Fred."));
        }

        [Test]
        public void TransformIssues()
        {
            ChatRuntime rt;
            string txt;
            Say say;
            Chat chat;

            txt = "SET $thing1 = (cat | cat)\nSAY A $thing1, many $thing1.pluralize()";
            rt = new ChatRuntime();
            rt.ParseText(txt);
            chat = rt.Chats().First();
            say = (Say)chat.commands[1];
            chat.Resolve(globals);
            //Console.WriteLine(res);
            Assert.That(say.Text(), Is.EqualTo("A cat, many cats"));

            txt = "SET $thing1 = (cat | cat | cat)\nSAY A $thing1 $thing1";
            rt = new ChatRuntime();
            rt.ParseText(txt);
            chat = rt.Chats().First();
            say = (Say)chat.commands[1];
            chat.Resolve(globals);
            Assert.That(say.Text(), Is.EqualTo("A cat cat"));


            txt = "SET $thing1 = (cat | crow | cow)\nSAY A [save=${thing1}], many $save.pluralize()";
            rt = new ChatRuntime();
            rt.ParseText(txt);
            chat = rt.Chats().First();
            say = (Say)chat.commands[1];
            chat.Resolve(globals);
            Assert.That(say.Text(), Is.EqualTo("A cat, many cats").Or.EqualTo("A crow, many crow").Or.EqualTo("A cow, many cows"));
        }

        [Test]
        public void ASimpleVar()
        {
            var res = new Resolver(null).BindSymbols("$a", null, new Dictionary<string, object>()
                {{ "a", "hello" }, { "b", "32" }});
            Assert.That(res, Is.EqualTo("hello"));

            res = new Resolver(null).BindSymbols("$a!", null, new Dictionary<string, object>()
                {{ "a", "hello" }, { "b", "32" }});
            Assert.That(res, Is.EqualTo("hello!"));
        }


        [Test]
        public void SimpleTransforms()
        {
            var c = CreateParentChat("c");

            var res = new Resolver(null).BindSymbols("$a.pluralize()", c,
                new Dictionary<string, object>() { { "a", "cat" } });
            Assert.That(res, Is.EqualTo("cats"));

            res = new Resolver(null).BindSymbols("$a.pluralize().articlize()", c,
                new Dictionary<string, object>() { { "a", "ant" } });
            Assert.That(res, Is.EqualTo("an ants"));

            res = new Resolver(null).BindGroups("(cat | cat).pluralize()", c);
            Assert.That(res, Is.EqualTo("cats"));

            res = new Resolver(null).BindGroups("(cat | cat).pluralize().articlize()", c);
            Assert.That(res, Is.EqualTo("a cats"));
        }


        [Test]
        public void GroupsWithEmptyStr()
        {
            string res;
            var c = CreateParentChat("c");
            for (int i = 0; i < 10; i++)
            {
                res = new Resolver(null).Bind("The almost( | \" dog\" | \" cat\").", c, null);
                //Console.WriteLine(i+") '"+res+"'");
                Assert.That(res, Is.EqualTo("The almost.").
                            Or.EqualTo("The almost dog.").
                            Or.EqualTo("The almost cat."));
            }
        }

        [Test]
        public void GroupRecursionDepth()
        {
            var str = "CHAT c\n(I | (You | (doh | why | no) | ouY) | They)";
            str += "(want | hate | like | love)(coffee | bread | milk)";
            Chat chat = ChatParser.ParseText(str, NO_VALIDATORS)[0];
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Say say = (Dialogic.Say)chat.commands[0];
            say.Resolve(null);
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
            say.Resolve(globals);
            Assert.That(say.Text(), Is.EqualTo("Thank 4"));
            Assert.That(say.Realized(Meta.TYPE), Is.EqualTo("Say"));
        }

        [Test]
        public void Exceptions()
        {
            //// no resolution to be made
            Assert.That(globals.ContainsKey("a"), Is.False);
            Assert.Throws<UnboundSymbol>(() => new Resolver(null).Bind("$a", CreateParentChat("c"), globals));

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
            c.Resolve(globals);
            c.SetMeta("pace", "slow");

            Assert.That(c.Text(), Is.EqualTo("Thank you"));
            Assert.That(c.Realized(Meta.TYPE), Is.EqualTo("Say"));
            Assert.That(c.Realized("pace"), Is.EqualTo("fast"));
            Assert.That(c.GetMeta("pace"), Is.EqualTo("slow"));

            chat = ChatParser.ParseText("SAY Thank you { pace=$animal}")[0];
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Say)));
            c = (Say)chat.commands[0];
            Assert.That(c.GetType(), Is.EqualTo(typeof(Say)));
            c.Resolve(globals);
            c.SetMeta("pace", "slow");
            Assert.That(c.Realized(Meta.TEXT), Is.EqualTo("Thank you"));
            Assert.That(c.Realized(Meta.TYPE), Is.EqualTo("Say"));
            Assert.That(c.Realized("pace"), Is.EqualTo("dog"));
            Assert.That(c.GetMeta("pace"), Is.EqualTo("slow"));

            chat = ChatParser.ParseText("SAY Thank you { pace=$obj-prop}")[0];
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Say)));
            c = (Say)chat.commands[0];
            Assert.That(c.GetType(), Is.EqualTo(typeof(Say)));
            c.Resolve(globals);
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
            say.Resolve(globals);
            Assert.That(say.Text(), Is.EqualTo("Thank 4"));
            say.text = "Thank you";
            Assert.That(say.text, Is.EqualTo("Thank you"));
            Assert.That(say.Realized(Meta.TYPE), Is.EqualTo("Say"));
            Assert.That(say.Realized("pace"), Is.EqualTo("dog"));
        }

        [Test]
        public void TextGroup()
        {
            var ok = new string[] { "The boy was sad", "The boy was happy", "The boy was dead" };
            Chat chat = ChatParser.ParseText("SAY The boy was (sad | happy | dead)")[0];
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Command c = (Say)chat.commands[0];
            Assert.That(c.GetType(), Is.EqualTo(typeof(Say)));
            c.Resolve(globals);
            Assert.That(c.Realized(Meta.TYPE), Is.EqualTo("Say"));
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
                c.Resolve(globals);
                var txt = c.Text();
                //Console.WriteLine(i+") "+txt);
                CollectionAssert.Contains(ok, txt);
            }
        }


        [Test]
        public void InvokeTransformTest()
        {
            Assert.That(Methods.InvokeTransform("cat", "pluralize"), Is.EqualTo("cats"));
        }

        [Test]
        public void ComplexPlusTransform()
        {
            // "cmplx" -> "($group | $prep)" 
            // "prep"  -> "then" },
            // "group" -> "(a|b)" },
            string[] ok = { "letter an a", "letter a b", "letter a then" };
            Chat chat = ChatParser.ParseText("SAY letter $cmplx.articlize()")[0];
            Command c = chat.commands[0];
            Assert.That(c.GetType(), Is.EqualTo(typeof(Say)));
            for (int i = 0; i < 10; i++)
            {
                c.Resolve(globals);
                var txt = c.Text();
                //Console.WriteLine(i + ") " + txt);
                CollectionAssert.Contains(ok, txt);
            }
        }

        [Test]
        public void ReplaceGroups()
        {
            Chat c1 = CreateParentChat("c1");

            var txt = "The boy was (sad | happy)";
            string[] ok = { "The boy was sad", "The boy was happy" };
            for (int i = 0; i < 10; i++)
            {
                CollectionAssert.Contains(ok, new Resolver(null).BindGroups(txt, c1));
            }

            txt = "The boy was (sad | happy | dead)";
            ok = new string[] { "The boy was sad", "The boy was happy", "The boy was dead" };
            for (int i = 0; i < 10; i++)
            {
                string s = new Resolver(null).BindGroups(txt, c1);
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
                doo.Resolve(globals);
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
            s = new Resolver(null).Bind(s, CreateParentChat("c"), globs);
            Assert.That(s, Is.EqualTo("SAY C C"));
        }


        [Test]
        public void ReplaceVars()
        {
            var s = @"SAY The $animal woke $count times";
            s = new Resolver(null).BindSymbols(s, CreateParentChat("c"), globals);
            Assert.That(s, Is.EqualTo("SAY The dog woke 4 times"));

            s = @"SAY The $obj-prop woke $count times";
            s = new Resolver(null).BindSymbols(s, CreateParentChat("c"), globals);
            Assert.That(s, Is.EqualTo("SAY The dog woke 4 times"));
        }

        [Test]
        public void ReplaceVarsGroups()
        {
            string s;
            Chat c1 = CreateParentChat("c1");


            s = @"SAY The $animal woke and $prep (ate|ate)";
            s = new Resolver(null).Bind(s, c1, globals);
            Assert.That(s, Is.EqualTo("SAY The dog woke and then ate"));

            s = @"SAY The $obj-prop woke and $prep (ate|ate)";
            s = new Resolver(null).Bind(s, c1, globals);
            Assert.That(s, Is.EqualTo("SAY The dog woke and then ate"));

            //s = realizer.Do("$a", new Dictionary<string, object>()
            //    {{ "a", "($a | $b)" }, { "b", "32" }});
            //Assert.That(s, Is.EqualTo("32"));

            string txt = "letter $group";
            for (int i = 0; i < 10; i++)
            {
                Assert.That(new Resolver(null).Bind(txt, c1, globals),
                    Is.EqualTo("letter a").Or.EqualTo("letter b"));
            }

            //new Resolver(null).DBUG = true;

            var txt2 = "letter $cmplx";
            var ok = new string[] { "letter a", "letter b", "letter then" };

            for (int i = 0; i < 10; i++)
            {
                var res = new Resolver(null).Bind(txt2, c1, globals);
                CollectionAssert.Contains(ok, res);
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
                say.Resolve(globals);
                string said = say.Text();
                //System.Console.WriteLine(i+") "+said);
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
                ask.Resolve(globals);
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
            ask.Resolve(globals);

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
            ask.Resolve(globals);
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



        [Test]
        public void EmptyGlobalScope()
        {
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            Assert.Throws<UnboundSymbol>(() =>
                                         rt.resolver.Bind("$animal", c1, null));
        }

        [Test]
        public void EmptyGlobalLocalScope()
        {
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            Assert.Throws<UnboundSymbol>(() =>
                                         rt.resolver.Bind("$animal", c1, null));
        }

        [Test]
        public void SimpleGlobalScope()
        {
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            var res =rt.resolver.Bind("$animal", c1, globals);
            Assert.That(res, Is.EqualTo("dog"));
        }

        [Test]
        public void ComplexGlobalScope()
        {
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            var res = rt.resolver.Bind("$cmplx", c1, globals);
            Assert.That(res, Is.EqualTo("a").Or.EqualTo("b").Or.EqualTo("then"));
        }

        [Test]
        public void SimpleLocalScope()
        {
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            c1.scope.Add("a", "b");
            var res = rt.resolver.Bind("$a", c1, globals);
            Assert.That(res, Is.EqualTo("b"));
        }

        [Test]
        public void ComplexLocalScope()
        {
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            c1.scope.Add("a", "$b");
            c1.scope.Add("b", "c");
            var res = rt.resolver.Bind("$a", c1, globals);
            Assert.That(res, Is.EqualTo("c"));
        }

        private static Chat CreateParentChat(string name)
        {
            // create a realized Chat with the full set of global props
            var c = Chat.Create(name);
            foreach (var prop in globals.Keys) c.SetMeta(prop, globals[prop]);
            c.Resolve(globals);
            return c;
        }
    }
}