using System;
using System.Collections.Generic;
using System.Linq;
using Dialogic;
using NUnit.Framework;

namespace Dialogic
{
    [TestFixture]
    class DynamicsTests : GenericTests
    {
        [Test]
        public void SetPathValueTest()
        {
            object result;
            Chat c1 = null;

            var symbol = Symbol.Parse("$fish.name = Mary", c1)[0];

            Set.SetPathValue(globals["fish"], new[]{"fish","name"}, "Mary", globals);

            result = Symbol.Parse("you $fish.name?", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("Mary"));
        }

        [Test]
        public void SetGlobalsOnPath()
        {
            var code = "CHAT c1\nSET $fish.name=Mary\nSAY Hi $fish.name";
            Chat chat = ChatParser.ParseText(code, NO_VALIDATORS)[0];

            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            Assert.That(chat.commands[1].GetType(), Is.EqualTo(typeof(Say)));

            Set set = (Dialogic.Set)chat.commands[0];
            Assert.That(set.text, Is.EqualTo("fish.name"));
            Assert.That(set.value, Is.EqualTo("Mary"));

            Say say = (Dialogic.Say)chat.commands[1];
            Assert.That(say.text, Is.EqualTo("Hi $fish.name"));

            chat.Resolve(globals);
            Assert.That(say.Text(), Is.EqualTo("Hi Mary"));
        }

        [Test]
        public void SetRemoteChatProperty()
        {
            var code = "CHAT c1\nSET $fish.name=Mary\nSAY Hi $fish.name";
            code += "\nCHAT c2\nSET $c1.staleness=2";
            var rt = new ChatRuntime(null);
            rt.ParseText(code, NO_VALIDATORS);

            var chat = rt["c1"];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            Assert.That(chat.commands[1].GetType(), Is.EqualTo(typeof(Say)));

            chat.Resolve(globals);

            var chat2 = rt["c2"];
            Assert.That(chat2, Is.Not.Null);
            Assert.That(chat2.commands[0].GetType(), Is.EqualTo(typeof(Set)));

            Assert.That(chat.Staleness(), Is.EqualTo(0));

            chat2.Resolve(globals);

            Assert.That(chat.Staleness(), Is.EqualTo(2));
        }

        [Test]
        public void SetRemoteChatNonProperty()
        {
            var code = "CHAT c1\nSET $fish.name=Mary\nSAY Hi $fish.name";
            code += "\nCHAT c2\nSET $c1.happiness=2";
            var rt = new ChatRuntime(null);
            rt.ParseText(code, NO_VALIDATORS);

            var chat = rt["c1"];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            Assert.That(chat.commands[1].GetType(), Is.EqualTo(typeof(Say)));

            chat.Resolve(globals);

            var chat2 = rt["c2"];
            Assert.That(chat2, Is.Not.Null);
            Assert.That(chat2.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            Assert.That(chat.Staleness(), Is.EqualTo(0));

            // throw b/c we only allow setting of persistent properties 
            // (staleness, etc) on remote chats
            Assert.Throws<BindException>(() => chat2.Resolve(globals));
        }

        [Test]
        public void SetBadRemoteChatProperty()
        {
            var code = "CHAT c1\nSET $fish.name=Mary\nSAY Hi $fish.name";
            code += "\nCHAT c2\nSET $WRONG.staleness=2";
            var rt = new ChatRuntime(null);
            rt.ParseText(code, NO_VALIDATORS);

            var chat = rt["c1"];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            Assert.That(chat.commands[1].GetType(), Is.EqualTo(typeof(Say)));

            chat.Resolve(globals);

            var chat2 = rt["c2"];
            Assert.That(chat2, Is.Not.Null);
            Assert.That(chat2.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            Assert.That(chat.Staleness(), Is.EqualTo(0));

            // throw b/c $WRONG.staleness doesn't exist in any scope
            Assert.Throws<BindException>(() => chat2.Resolve(globals));
        }

        [Test]
        public void SetChatLocalPath()
        {
            var code = "CHAT c1\nSET $fish.name=Mary\nSAY Hi $fish.name";
            code += "\nCHAT c2\nSET $c1.staleness=2";

            var rt = new ChatRuntime(null);
            rt.ParseText(code, NO_VALIDATORS);

            var chat = rt["c1"];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            Assert.That(chat.commands[1].GetType(), Is.EqualTo(typeof(Say)));

            chat.Resolve(globals);

            var chat2 = rt["c2"];
            Assert.That(chat2, Is.Not.Null);
            Assert.That(chat2.commands[0].GetType(), Is.EqualTo(typeof(Set)));

            Assert.That(chat.Staleness(), Is.EqualTo(Defaults.CHAT_STALENESS));
            Assert.That(Convert.ToDouble(chat.GetMeta(Meta.STALENESS)), Is.EqualTo(Defaults.CHAT_STALENESS));

            chat2.Resolve(globals);
            Assert.That(chat.Staleness(), Is.EqualTo(2));
            Assert.That(Convert.ToDouble(chat.GetMeta(Meta.STALENESS)), Is.EqualTo(2));


            code = "CHAT c1\nSET $fish.name=Mary\nSAY Hi $fish.name";
            code += "\nCHAT c2\nSET $c1.stalenessIncr=2";

            rt = new ChatRuntime(null);
            rt.ParseText(code, NO_VALIDATORS);

            chat = rt["c1"];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            Assert.That(chat.commands[1].GetType(), Is.EqualTo(typeof(Say)));

            chat.Resolve(globals);

            chat2 = rt["c2"];
            Assert.That(chat2, Is.Not.Null);
            Assert.That(chat2.commands[0].GetType(), Is.EqualTo(typeof(Set)));

            // no need to check metadata, except for staleness
            Assert.That(chat.StalenessIncr(), Is.EqualTo(Defaults.CHAT_STALENESS_INCR));
            chat2.Resolve(globals);
            Assert.That(chat.StalenessIncr(), Is.EqualTo(2));
        }

        [Test]
        public void ResolveGetSymbolTraversal()
        {
            object result;
            Chat c1 = null;

            result = Symbol.Parse("#$fish.flipper.speed", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("1.1"));

            result = Symbol.Parse("you $fish.name?", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("Fred"));

            result = Symbol.Parse("you $fish.species?", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("Oscar"));

            result = Symbol.Parse("you $fish.flipper?", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("1.1"));

            result = Symbol.Parse("you $fish.flipper.speed?", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("1.1"));

            result = Symbol.Parse("you ${fish.name}?", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("Fred"));

            result = Symbol.Parse("you ${fish.species}?", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("Oscar"));

            result = Symbol.Parse("you ${fish.flipper}?", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("1.1"));

            result = Symbol.Parse("you ${fish.flipper.speed}?", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("1.1"));
        }
       
        [Test]
        public void MultipleSymbolsWithTransform()
        {
            Chat c1 = null;

            var symbols = Symbol.Parse("A $thing1 $thing2.pluralize()", c1);
            Assert.That(symbols.Count, Is.EqualTo(2));
            Assert.That(symbols[1].transforms.ToArray(), 
                        Is.EquivalentTo(new[] { "pluralize()" }));
            //Console.WriteLine(symbols.Stringify());
        }

        [Test]
        public void SubstringSymbolResolve()
        {
            ChatRuntime rt = new ChatRuntime();
            rt.strictMode = false;

            ChatRuntime.SILENT = true;
            Assert.That(DoSay(rt, "SAY $ant $antelope"), Is.EqualTo("hello $antelope"));
            Assert.That(DoSay(rt, "SAY $ant$antelope"), Is.EqualTo("hello$antelope"));
            Assert.That(DoSay(rt, "SAY $ant. $antelope"), Is.EqualTo("hello. $antelope"));
            Assert.That(DoSay(rt, "SAY $ant! $antelope"), Is.EqualTo("hello! $antelope"));
            Assert.That(DoSay(rt, "SAY $ant? $antelope"), Is.EqualTo("hello? $antelope"));
            Assert.That(DoSay(rt, "SAY $ant, $antelope"), Is.EqualTo("hello, $antelope"));
            Assert.That(DoSay(rt, "SAY $ant; $antelope"), Is.EqualTo("hello; $antelope"));
            Assert.That(DoSay(rt, "SAY $ant: $antelope"), Is.EqualTo("hello: $antelope"));
            Assert.That(DoSay(rt, "SAY $ant $ant-"), Is.EqualTo("hello $ant-"));
            Assert.That(DoSay(rt, "SAY $ant $ant_"), Is.EqualTo("hello $ant_"));
            ChatRuntime.SILENT = false;
        }

        private static string DoSay(ChatRuntime rt, string s)
        {
            var globs = new Dictionary<string, object> { { "ant", "hello" } };
            rt.chats = new Dictionary<string, Chat>();
            rt.ParseText(s);
            Say say = (Dialogic.Say)rt.Chats().First().commands.First();
            say.Resolve(globs);
            s = say.Text();
            //Console.WriteLine(s);
            return s;
        }

        [Test]
        public void SingleSymbolResolve()
        {
            object result;
            Chat c1 = null;

            result = Symbol.Parse("#$count", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("4"));

            result = Symbol.Parse("you $prop1?", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("hum"));

            result = Symbol.Parse("you ${prop1}?", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("hum"));

            result = Symbol.Parse("you $prop1!", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("hum"));

            result = Symbol.Parse("you ${prop1}!", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("hum"));

            result = Symbol.Parse("$a", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("A"));

            result = Symbol.Parse("${a}", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("A"));

            result = Symbol.Parse("$a", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("A"));

            result = Symbol.Parse("((a|b) | $prep)", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("then"));

            result = Symbol.Parse("[bb=$a]", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("A"));

            result = Symbol.Parse("[bb=${a}]", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("A"));

            result = Symbol.Parse("$name", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("Jon"));

            result = Symbol.Parse("$name,", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("Jon"));

            result = Symbol.Parse("Hello $name,", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("Jon"));

            result = Symbol.Parse("to $verb you", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("melt"));

            result = Symbol.Parse("${name}", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("Jon"));

            result = Symbol.Parse("Hello ${name},", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("Jon"));

            result = Symbol.Parse("to ${verb} you", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("melt"));

            result = Symbol.Parse("${name},", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("Jon"));
        }

        [Test]
        public void MethodsInvoke()
        {
            var fish = new Fish("Frank");
            var obj = Methods.Invoke(fish, "Id");
            Assert.That(obj.ToString(), Is.EqualTo("9"));
            Methods.Invoke(fish, "Id", new object[] { 10 });
            Assert.That(Methods.Invoke(fish, "Id").ToString(), Is.EqualTo("10"));
        }

        [Test]
        public void GetSetProperties()
        {
            var fish = new Fish("Frank");
            var obj = Properties.Get(fish, "name");
            Assert.That(obj.ToString(), Is.EqualTo("Frank"));

            Properties.Set(fish, "name", "Bill");
            Assert.That(fish.name, Is.EqualTo("Bill"));
        }

        //[Test]
        //public void SymbolSortTest() // remove
        //{
        //    var syms = Symbol.Parse("$a $a2", null);
        //    //Symbol.Sort(syms);
        //    Assert.That(syms.ElementAt(1).name, Is.EqualTo("a2"));
        //    Assert.That(syms.ElementAt(0).name, Is.EqualTo("a"));

        //    syms = Symbol.Parse("$a $aa $aaa", null);
        //    //Symbol.Sort(syms);
        //    Assert.That(syms.ElementAt(2).name, Is.EqualTo("aaa"));
        //    Assert.That(syms.ElementAt(1).name, Is.EqualTo("aa"));
        //    Assert.That(syms.ElementAt(0).name, Is.EqualTo("a"));


        //    syms = Symbol.Parse("[b=$a] $aa $aaa", null);
        //    //Symbol.Sort(syms);
        //    Assert.That(syms.ElementAt(2).name, Is.EqualTo("a"));
        //    Assert.That(syms.ElementAt(1).name, Is.EqualTo("aaa"));
        //    Assert.That(syms.ElementAt(0).name, Is.EqualTo("aa"));

        //    syms = Symbol.Parse("[b=$a] [cc=$aa] $aaa", null);
        //    //Symbol.Sort(syms);
        //    Assert.That(syms.ElementAt(2).name, Is.EqualTo("aa"));
        //    Assert.That(syms.ElementAt(1).name, Is.EqualTo("a"));
        //    Assert.That(syms.ElementAt(0).name, Is.EqualTo("aaa"));
        //}

        [Test]
        public void SingleSymbolParsing()
        {
            Chat c = CreateParentChat("c");

            Symbol s;

            s = Symbol.Parse("$a",c)[0];

            Assert.That(s.text, Is.EqualTo("$a"));
            Assert.That(s.name, Is.EqualTo("a"));
            Assert.That(s.alias, Is.Null);
            Assert.That(s.bounded, Is.EqualTo(false));
            //Assert.That(s.chatScoped, Is.EqualTo(false));
            Assert.That(s.ToString(), Is.EqualTo(s.text));

            s = Symbol.Parse("#$a",c)[0];
            Assert.That(s.text, Is.EqualTo("$a"));
            Assert.That(s.name, Is.EqualTo("a"));
            Assert.That(s.alias, Is.Null);
            Assert.That(s.bounded, Is.EqualTo(false));
            //Assert.That(s.chatScoped, Is.EqualTo(false));
            Assert.That(s.ToString(), Is.EqualTo(s.text));

            s = Symbol.Parse("${a}",c)[0];
            Assert.That(s.text, Is.EqualTo("${a}"));
            Assert.That(s.name, Is.EqualTo("a"));
            Assert.That(s.alias, Is.Null);
            Assert.That(s.bounded, Is.EqualTo(true));
            //Assert.That(s.chatScoped, Is.EqualTo(false));
            Assert.That(s.ToString(), Is.EqualTo(s.text));

            s = Symbol.Parse("((a|b) | $prep)",c)[0];
            Assert.That(s.text, Is.EqualTo("$prep"));
            Assert.That(s.name, Is.EqualTo("prep"));
            Assert.That(s.alias, Is.Null);
            //Assert.That(s.chatScoped, Is.EqualTo(false));
            Assert.That(s.bounded, Is.EqualTo(false));

            s = Symbol.Parse("$a",c)[0];
            Assert.That(s.text, Is.EqualTo("$a"));
            Assert.That(s.name, Is.EqualTo("a"));
            Assert.That(s.alias, Is.Null);
            //Assert.That(s.chatScoped, Is.EqualTo(false));
            Assert.That(s.bounded, Is.EqualTo(false));

            s = Symbol.Parse("[bb=$a]",c)[0];
            Assert.That(s.text, Is.EqualTo("[bb=$a]"));
            Assert.That(s.name, Is.EqualTo("a"));
            Assert.That(s.alias, Is.EqualTo("bb"));
            //Assert.That(s.chatScoped, Is.EqualTo(false));
            Assert.That(s.bounded, Is.EqualTo(false));

            s = Symbol.Parse("${a}",c)[0];
            Assert.That(s.text, Is.EqualTo("${a}"));
            Assert.That(s.name, Is.EqualTo("a"));
            Assert.That(s.alias, Is.Null);
            //Assert.That(s.chatScoped, Is.EqualTo(false));
            Assert.That(s.bounded, Is.EqualTo(true));

            s = Symbol.Parse("[bb=${a}]",c)[0];
            Assert.That(s.text, Is.EqualTo("[bb=${a}]"));
            Assert.That(s.name, Is.EqualTo("a"));
            Assert.That(s.alias, Is.EqualTo("bb"));
            //Assert.That(s.chatScoped, Is.EqualTo(false));
            Assert.That(s.bounded, Is.EqualTo(true));

            s = Symbol.Parse("$name",c)[0];
            Assert.That(s.text, Is.EqualTo("$name"));
            Assert.That(s.name, Is.EqualTo("name"));
            Assert.That(s.alias, Is.Null);
            //Assert.That(s.chatScoped, Is.EqualTo(false));
            Assert.That(s.bounded, Is.EqualTo(false));

            s = Symbol.Parse("$name,",c)[0];
            Assert.That(s.text, Is.EqualTo("$name"));
            Assert.That(s.name, Is.EqualTo("name"));
            Assert.That(s.alias, Is.Null);
            Assert.That(s.bounded, Is.EqualTo(false));

            s = Symbol.Parse("Hello $name,",c)[0];
            Assert.That(s.text, Is.EqualTo("$name"));
            Assert.That(s.name, Is.EqualTo("name"));
            Assert.That(s.alias, Is.Null);
            //Assert.That(s.chatScoped, Is.EqualTo(false));
            Assert.That(s.bounded, Is.EqualTo(false));
            //text = "Hello $name, nice to $verb you $prop1!";

            s = Symbol.Parse("to $verb you",c)[0];
            Assert.That(s.text, Is.EqualTo("$verb"));
            Assert.That(s.name, Is.EqualTo("verb"));
            Assert.That(s.alias, Is.Null);
            //Assert.That(s.chatScoped, Is.EqualTo(false));
            Assert.That(s.bounded, Is.EqualTo(false));

            s = Symbol.Parse("you $prop1!",c)[0];
            Assert.That(s.text, Is.EqualTo("$prop1"));
            Assert.That(s.name, Is.EqualTo("prop1"));
            Assert.That(s.alias, Is.Null);
            //Assert.That(s.chatScoped, Is.EqualTo(false));
            Assert.That(s.bounded, Is.EqualTo(false));
        }

        [Test]
        public void ParseSymbols()
        {
            Chat c = CreateParentChat("c");

            var ts = new[] { "", ".", "!", ":", ";", ",", "?", ")", "\"", "'" };
            foreach (var t in ts) Assert.That(Symbol.Parse
                ("$a" + t, c).First().name, Is.EqualTo("a"));

            foreach (var t in ts) Assert.That(Symbol.Parse
                ("${a}" + t, c).First().name, Is.EqualTo("a"));

            foreach (var t in ts) Assert.That(Symbol.Parse
                ("[b=$a]" + t, c).First().name, Is.EqualTo("a"));

            foreach (var t in ts) Assert.That(Symbol.Parse
                ("[b=${a}]" + t, c).First().name, Is.EqualTo("a"));

            Assert.That(Symbol.Parse("${a}", c).First().name, Is.EqualTo("a"));
            Assert.That(Symbol.Parse("[b=$a]", c).First().name, Is.EqualTo("a"));
            Assert.That(Symbol.Parse("[b=$a]", c).First().alias, Is.EqualTo("b"));
            Assert.That(Symbol.Parse("${a}b", c).First().name, Is.EqualTo("a"));

            Assert.That(Symbol.Parse("${a.b}", c).First().name, Is.EqualTo("a"));
            Assert.That(Symbol.Parse("${a.b}", c).First().transforms.ToArray(), Is.EqualTo(new[] { "b" }));

            Assert.That(Symbol.Parse("$a.b", c).First().name, Is.EqualTo("a"));
            Assert.That(Symbol.Parse("$a.b", c).First().transforms.ToArray(), Is.EqualTo(new[] { "b" }));

            Assert.That(Symbol.Parse("[bc=$a.b]", c).First().name, Is.EqualTo("a"));

            Assert.That(Symbol.Parse("[bc=$a.b]", c).First().alias, Is.EqualTo("bc"));
            Assert.That(Symbol.Parse("[bc=$a.b]", c).First().transforms.ToArray(), Is.EqualTo(new[] { "b" }));

            Assert.That(Symbol.Parse("[c=${a.b}].", c).First().name, Is.EqualTo("a"));
            Assert.That(Symbol.Parse("[c=${a.b}].", c).First().alias, Is.EqualTo("c"));
            Assert.That(Symbol.Parse("[c=${a.b}]", c).First().transforms.ToArray(), Is.EqualTo(new[] { "b" }));


            Assert.That(Symbol.Parse("${a.b}b", c).First().name, Is.EqualTo("a"));
            Assert.That(Symbol.Parse("${a.b}", c).First().transforms.ToArray(), Is.EqualTo(new[] { "b" }));
        }

        [Test]
        public void ResolveGroupsWithAlias()
        {
            ChatRuntime rt;
            string s;

            //Resolver.DBUG = true;
            (rt = new ChatRuntime()).ParseText("CHAT c1\n(a | (b | c))", true);
            rt["c1"].Resolve(null);
            s = rt["c1"].commands[0].Text();
            Assert.That(s, Is.EqualTo("a").Or.EqualTo("b").Or.EqualTo("c"));

            (rt = new ChatRuntime()).ParseText("CHAT c2\n[d=(a | b)] $d", true);
            rt["c2"].Resolve(null);
            s = rt["c2"].commands[0].Text();
            Assert.That(s, Is.EqualTo("a a").Or.EqualTo("b b"));

            //chat = ChatParser.ParseText("CHAT c3\n[d=(a | (b | c))] $d", true)[0];
            //chat.Realize(globals);
            (rt = new ChatRuntime()).ParseText("CHAT c3\n[d=(a | (b | c))] $d", true);
            rt["c3"].Resolve(null);
            s = rt["c3"].commands[0].Text();
            Assert.That(s, Is.EqualTo("a a").Or.EqualTo("b b").Or.EqualTo("c c"));
        }

        [Test]
        public void ParseChoices()
        {
            Chat c = CreateParentChat("c");
            List<Choice> choices;
            Choice choice;

            choices = Choice.Parse("you (a | b | c) a", c, false);
            choice = choices[0];
            Assert.That(choices.Count, Is.EqualTo(1));
            Assert.That(choice.text, Is.EqualTo("(a | b | c)"));
            Assert.That(choice.options.Count, Is.EqualTo(3));
            Assert.That(choice.options, Is.EquivalentTo(new[] { "a", "b", "c" }));


            choices = Choice.Parse("you (a | (b | c)) a", c, false);
            choice = choices[0];
            Assert.That(choices.Count, Is.EqualTo(1));
            Assert.That(choice.text, Is.EqualTo("(b | c)"));
            Assert.That(choice.options.Count, Is.EqualTo(2));
            Assert.That(choice.options, Is.EquivalentTo(new[] { "b", "c" }));

            choices = Choice.Parse("you (then | (a | b))", c, false);
            choice = choices[0];
            Assert.That(choices.Count, Is.EqualTo(1));
            Assert.That(choice.text, Is.EqualTo("(a | b)"));
            Assert.That(choice.options.Count, Is.EqualTo(2));
            Assert.That(choice.options, Is.EquivalentTo(new[] { "a", "b" }));

            choices = Choice.Parse("you ((a | b) | then) a", c, false);
            choice = choices[0];
            Assert.That(choices.Count, Is.EqualTo(1));
            Assert.That(choice.text, Is.EqualTo("(a | b)"));
            Assert.That(choice.options.Count, Is.EqualTo(2));
            Assert.That(choice.options, Is.EquivalentTo(new[] { "a", "b" }));
        }
    }
}
