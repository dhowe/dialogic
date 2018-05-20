using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Dialogic
{
    [TestFixture]
    class SymbolTests : GenericTests
    {
        [Test]
        public void DynamicRules()
        {
            string[] lines;
            ChatRuntime rt;
            string s;

            Resolver.DBUG = false;

            lines = new[] {
                "CHAT c",
                "SET $emo = anger",
                "SAY ($emo)_rule"
            };
            rt = new ChatRuntime();
            rt.ParseText(String.Join("\n", lines), NO_VALIDATORS);
            s = rt.InvokeImmediate(globals);
            Assert.That(s, Is.EqualTo("anger_rule"));

            lines = new[] {
                "CHAT c",
                "SET $emo = a|a",
                "SAY $$emo"
            };
            rt = new ChatRuntime();
            rt.strictMode = false;
            rt.ParseText(String.Join("\n", lines), NO_VALIDATORS);
            s = rt.InvokeImmediate(globals);
            Assert.That(s, Is.EqualTo("A"));

            lines = new[] {
                "CHAT c",
                "SET $emo = a|a",
                "SAY $($emo)."
            };
            rt = new ChatRuntime();
            rt.strictMode = false;
            rt.ParseText(String.Join("\n", lines), NO_VALIDATORS);
            s = rt.InvokeImmediate(globals);
            Assert.That(s, Is.EqualTo("A."));

            lines = new[] {
                "CHAT c",
                "SET $emo = anger",
                "SAY $($emo)_rule"
            };

            rt = new ChatRuntime();
            rt.strictMode = false;
            rt.ParseText(String.Join("\n", lines), NO_VALIDATORS);
            ChatRuntime.SILENT = true;
            s = rt.InvokeImmediate(globals);
            ChatRuntime.SILENT = false;
            Assert.That(s, Is.EqualTo("$anger_rule"));

            lines = new[] {
                "CHAT c",
                "SET $emo = anger",
                "SET start = $($emo)_rule",
                "SET anger_rule = I am angry",
                "SET happy_rule = I am happy",
                "SAY $start"
            };
            rt = new ChatRuntime();
            rt.ParseText(String.Join("\n", lines), NO_VALIDATORS);
            s = rt.InvokeImmediate(globals);
            Assert.That(s, Is.EqualTo("I am angry"));
        }
        [Test]
        public void SymbolTraversalSimple()
        {
            Resolver.DBUG = false;
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            var res = rt.resolver.Bind("Hello $fish.name", c1, globals);
            Assert.That(res, Is.EqualTo("Hello Fred"));
        }

        [Test]
        public void SymbolBinding()
        {
            //Resolver.DBUG = true;
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            var res = rt.resolver.Bind("Hello $$recur", c1, globals);
            Assert.That(res, Is.EqualTo("Hello A"));
        }

        [Test]
        public void BoundedVarSolutions()
        {
            ChatRuntime rt;
            string[] lines;
            string s;

            lines = new[] {
                "CHAT c1",
                "SET doop = bop",
                "SET deep = beep",
                "SAY [a=$deep][b=$doop]"
            };
            rt = new ChatRuntime();
            rt.ParseText(String.Join("\n", lines), NO_VALIDATORS);

            s = rt.InvokeImmediate(globals);
            Assert.That(s, Is.EqualTo("beepbop"));

            lines = new[] {
                "CHAT c1",
                "SET doop = bop",
                "SET deep = beep",
                "SAY ($deep)-($doop)"
            };
            rt = new ChatRuntime();
            rt.ParseText(String.Join("\n", lines), NO_VALIDATORS);

            s = rt.InvokeImmediate(globals);
            Assert.That(s, Is.EqualTo("beep-bop"));


            lines = new[] {
                "CHAT c1",
                "SET doop = bop",
                "SET deep = beep",
                "SAY ($deep)($doop)"
            };
            rt = new ChatRuntime();
            rt.ParseText(String.Join("\n", lines), NO_VALIDATORS);

            s = rt.InvokeImmediate(globals);
            Assert.That(s, Is.EqualTo("beepbop"));


            lines = new[] {
                "CHAT c1",
                "SET doop = bop",
                "SET deep = beep",
                "SAY [a=$deep]+[b=$doop]"
            };
            rt = new ChatRuntime();
            rt.ParseText(String.Join("\n", lines), NO_VALIDATORS);

            s = rt.InvokeImmediate(globals);
            Assert.That(s, Is.EqualTo("beep+bop"));
        }

        [Test]
        public void SymbolTraversalWithAlias()
        {
            Chat c1 = null;
            List<Symbol> symbols;
            string result, text;

            // one round of symbol parsing
            text = "The [aly=$fish.Id()] $aly";
            symbols = Symbol.Parse(text, c1);
            //Console.WriteLine(symbols.Stringify());
            Assert.That(symbols.Count, Is.EqualTo(2));
            result = symbols[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("9"));

            result = symbols[0].Replace(text, result, globals);
            //Console.WriteLine("result="+result);
            Assert.That(result.ToString(), Is.EqualTo("The 9 $aly"));
            Assert.That(globals["aly"], Is.EqualTo("9"));


            // now try the full resolution
            ChatRuntime rt = new ChatRuntime();
            c1 = rt.AddNewChat("c1");
            var res = rt.resolver.Bind(text, c1, globals);
            Assert.That(res, Is.EqualTo("The 9 9"));
        }

        [Test]
        public void SingleSymbolParsing()
        {
            Symbol s;
            Chat c = CreateParentChat("c");

            s = Symbol.Parse("$$a", c)[0];
            Assert.That(s.text, Is.EqualTo("$a"));
            Assert.That(s.name, Is.EqualTo("a"));
            Assert.That(s.alias, Is.Null);
            //Assert.That(s.chatScoped, Is.EqualTo(false));
            Assert.That(s.ToString(), Is.EqualTo(s.text));

            s = Symbol.Parse("$a]", c)[0];
            Assert.That(s.text, Is.EqualTo("$a"));
            Assert.That(s.name, Is.EqualTo("a"));
            Assert.That(s.alias, Is.Null);
            //Assert.That(s.chatScoped, Is.EqualTo(false));
            Assert.That(s.ToString(), Is.EqualTo(s.text));

            s = Symbol.Parse("$a", c)[0];
            Assert.That(s.text, Is.EqualTo("$a"));
            Assert.That(s.name, Is.EqualTo("a"));
            Assert.That(s.alias, Is.Null);
            //Assert.That(s.chatScoped, Is.EqualTo(false));
            Assert.That(s.ToString(), Is.EqualTo(s.text));

            s = Symbol.Parse("[bb=$a]", c)[0];
            Assert.That(s.text, Is.EqualTo("[bb=$a]"));
            Assert.That(s.name, Is.EqualTo("a"));
            Assert.That(s.alias, Is.EqualTo("bb"));
            //Assert.That(s.chatScoped, Is.EqualTo(false));

            s = Symbol.Parse("#$a", c)[0];
            Assert.That(s.text, Is.EqualTo("$a"));
            Assert.That(s.name, Is.EqualTo("a"));
            Assert.That(s.alias, Is.Null);
            //Assert.That(s.chatScoped, Is.EqualTo(false));
            Assert.That(s.ToString(), Is.EqualTo(s.text));

            //s = Symbol.Parse("${a}", c)[0];
            //Assert.That(s.text, Is.EqualTo("${a}"));
            //Assert.That(s.name, Is.EqualTo("a"));
            //Assert.That(s.alias, Is.Null);
            //Assert.That(s.bounded, Is.EqualTo(true));
            ////Assert.That(s.chatScoped, Is.EqualTo(false));
            //Assert.That(s.ToString(), Is.EqualTo(s.text));

            s = Symbol.Parse("((a|b) | $prep)", c)[0];
            Assert.That(s.text, Is.EqualTo("$prep"));
            Assert.That(s.name, Is.EqualTo("prep"));
            Assert.That(s.alias, Is.Null);
            //Assert.That(s.chatScoped, Is.EqualTo(false));

            s = Symbol.Parse("$a", c)[0];
            Assert.That(s.text, Is.EqualTo("$a"));
            Assert.That(s.name, Is.EqualTo("a"));
            Assert.That(s.alias, Is.Null);
            //Assert.That(s.chatScoped, Is.EqualTo(false));

            //s = Symbol.Parse("[bb=${a}]", c)[0];
            //Assert.That(s.text, Is.EqualTo("[bb=${a}]"));
            //Assert.That(s.name, Is.EqualTo("a"));
            //Assert.That(s.alias, Is.EqualTo("bb"));
            ////Assert.That(s.chatScoped, Is.EqualTo(false));
            //Assert.That(s.bounded, Is.EqualTo(true));

            s = Symbol.Parse("$name", c)[0];
            Assert.That(s.text, Is.EqualTo("$name"));
            Assert.That(s.name, Is.EqualTo("name"));
            Assert.That(s.alias, Is.Null);
            //Assert.That(s.chatScoped, Is.EqualTo(false));

            s = Symbol.Parse("$name,", c)[0];
            Assert.That(s.text, Is.EqualTo("$name"));
            Assert.That(s.name, Is.EqualTo("name"));
            Assert.That(s.alias, Is.Null);

            s = Symbol.Parse("Hello $name,", c)[0];
            Assert.That(s.text, Is.EqualTo("$name"));
            Assert.That(s.name, Is.EqualTo("name"));
            Assert.That(s.alias, Is.Null);
            //Assert.That(s.chatScoped, Is.EqualTo(false));
            //text = "Hello $name, nice to $verb you $prop1!";

            s = Symbol.Parse("to $verb you", c)[0];
            Assert.That(s.text, Is.EqualTo("$verb"));
            Assert.That(s.name, Is.EqualTo("verb"));
            Assert.That(s.alias, Is.Null);
            //Assert.That(s.chatScoped, Is.EqualTo(false));

            s = Symbol.Parse("you $prop1!", c)[0];
            Assert.That(s.text, Is.EqualTo("$prop1"));
            Assert.That(s.name, Is.EqualTo("prop1"));
            Assert.That(s.alias, Is.Null);
            //Assert.That(s.chatScoped, Is.EqualTo(false));
        }


        [Test]
        public void SingleSymbolResolve()
        {
            object result;
            Chat c1 = null;

            /*
                { "obj-prop", "dog" },
                { "animal", "dog" },
                { "prep", "then" },
                { "group", "(a|b)" },
                { "cmplx", "($group | $prep)" },
                { "count", 4 },
                { "prop1", "hum" },
                { "name", "Jon" },
                { "verb", "melt" },
                { "a", "A" },
                { "fish",  new Fish("Fred")}
             */

            result = Symbol.Parse("$$recur?", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("a"));

            result = Symbol.Parse("#$count", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("4"));

            result = Symbol.Parse("#$count", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("4"));

            result = Symbol.Parse("you $prop1?", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("hum"));

            //result = Symbol.Parse("you ${prop1}?", c1)[0].Resolve(globals);
            //Assert.That(result.ToString(), Is.EqualTo("hum"));

            result = Symbol.Parse("you $prop1!", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("hum"));

            //result = Symbol.Parse("you ${prop1}!", c1)[0].Resolve(globals);
            //Assert.That(result.ToString(), Is.EqualTo("hum"));

            result = Symbol.Parse("$a", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("A"));

            //result = Symbol.Parse("${a}", c1)[0].Resolve(globals);
            //Assert.That(result.ToString(), Is.EqualTo("A"));

            result = Symbol.Parse("$a", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("A"));

            result = Symbol.Parse("((a|b) | $prep)", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("then"));

            result = Symbol.Parse("[bb=$a]", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("A"));

            //result = Symbol.Parse("[bb=${a}]", c1)[0].Resolve(globals);
            //Assert.That(result.ToString(), Is.EqualTo("A"));

            result = Symbol.Parse("$name", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("Jon"));

            result = Symbol.Parse("$name,", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("Jon"));

            result = Symbol.Parse("Hello $name,", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("Jon"));

            result = Symbol.Parse("to $verb you", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("melt"));

            //result = Symbol.Parse("${name}", c1)[0].Resolve(globals);
            //Assert.That(result.ToString(), Is.EqualTo("Jon"));

            //result = Symbol.Parse("Hello ${name},", c1)[0].Resolve(globals);
            //Assert.That(result.ToString(), Is.EqualTo("Jon"));

            //result = Symbol.Parse("to ${verb} you", c1)[0].Resolve(globals);
            //Assert.That(result.ToString(), Is.EqualTo("melt"));

            //result = Symbol.Parse("${name},", c1)[0].Resolve(globals);
            //Assert.That(result.ToString(), Is.EqualTo("Jon"));
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

            //result = Symbol.Parse("you ${fish.name}?", c1)[0].Resolve(globals);
            //Assert.That(result.ToString(), Is.EqualTo("Fred"));

            //result = Symbol.Parse("you ${fish.species}?", c1)[0].Resolve(globals);
            //Assert.That(result.ToString(), Is.EqualTo("Oscar"));

            //result = Symbol.Parse("you ${fish.flipper}?", c1)[0].Resolve(globals);
            //Assert.That(result.ToString(), Is.EqualTo("1.1"));

            //result = Symbol.Parse("you ${fish.flipper.speed}?", c1)[0].Resolve(globals);
            //Assert.That(result.ToString(), Is.EqualTo("1.1"));
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
        public void ParseSymbols()
        {
            Chat c = CreateParentChat("c");

            var ts = new[] { "", ".", "!", ":", ";", ",", "?", ")", "\"", "'" };
            foreach (var t in ts) Assert.That(Symbol.Parse
                ("$a" + t, c).First().name, Is.EqualTo("a"));

            //foreach (var t in ts) Assert.That(Symbol.Parse
            //("${a}" + t, c).First().name, Is.EqualTo("a"));

            foreach (var t in ts) Assert.That(Symbol.Parse
                ("[b=$a]" + t, c).First().name, Is.EqualTo("a"));

            //foreach (var t in ts) Assert.That(Symbol.Parse
            //("[b=${a}]" + t, c).First().name, Is.EqualTo("a"));

            //Assert.That(Symbol.Parse("${a}", c).First().name, Is.EqualTo("a"));
            Assert.That(Symbol.Parse("[b=$a]", c).First().name, Is.EqualTo("a"));
            Assert.That(Symbol.Parse("[b=$a]", c).First().alias, Is.EqualTo("b"));
            //Assert.That(Symbol.Parse("${a}b", c).First().name, Is.EqualTo("a"));

            //Assert.That(Symbol.Parse("${a.b}", c).First().name, Is.EqualTo("a"));
            //Assert.That(Symbol.Parse("${a.b}", c).First().transforms.ToArray(), Is.EqualTo(new[] { "b" }));

            Assert.That(Symbol.Parse("$a.b", c).First().name, Is.EqualTo("a"));
            Assert.That(Symbol.Parse("$a.b", c).First().transforms.ToArray(), Is.EqualTo(new[] { "b" }));

            Assert.That(Symbol.Parse("[bc=$a.b]", c).First().name, Is.EqualTo("a"));

            Assert.That(Symbol.Parse("[bc=$a.b]", c).First().alias, Is.EqualTo("bc"));
            Assert.That(Symbol.Parse("[bc=$a.b]", c).First().transforms.ToArray(), Is.EqualTo(new[] { "b" }));

            //Assert.That(Symbol.Parse("[c=${a.b}].", c).First().name, Is.EqualTo("a"));
            //Assert.That(Symbol.Parse("[c=${a.b}].", c).First().alias, Is.EqualTo("c"));
            //Assert.That(Symbol.Parse("[c=${a.b}]", c).First().transforms.ToArray(), Is.EqualTo(new[] { "b" }));


            //Assert.That(Symbol.Parse("${a.b}b", c).First().name, Is.EqualTo("a"));
            //Assert.That(Symbol.Parse("${a.b}", c).First().transforms.ToArray(), Is.EqualTo(new[] { "b" }));
        }

        [Test]
        public void SymbolTraversalComplex()
        {
            object result;
            Chat c1 = null;
            result = Symbol.Parse("The $fish.Id()", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("9"));

            result = Symbol.Parse("The $fish.GetSpecies()", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("Oscar"));

            result = Symbol.Parse("you $fish.GetFlipper()?", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("1.1"));

            result = Symbol.Parse("you $fish.GetFlipperSpeed()?", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("1.1"));


            result = Symbol.Parse("you $fish.GetFlipper().speed?", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("1.1"));

            result = Symbol.Parse("you $fish.GetFlipper().ToString()?", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("1.1"));

            var symbols = Symbol.Parse("The $fish.Id() $fish.name", c1);
            result = symbols[0].Resolve(globals) + symbols[1].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("9Fred"));

            /* bounded ----------------------------------------------------------------

            result = Symbol.Parse("The ${fish.Id()}", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("9"));

            result = Symbol.Parse("The ${fish.GetSpecies()}", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("Oscar"));

            result = Symbol.Parse("you ${fish.GetFlipper()}?", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("1.1"));

            result = Symbol.Parse("you ${fish.GetFlipperSpeed()}?", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("1.1"));

            result = Symbol.Parse("you ${fish.GetFlipper().speed}?", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("1.1"));

            result = Symbol.Parse("you ${fish.GetFlipper().ToString()}?", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("1.1"));

            result = Symbol.Parse("#{$fish.Id()}", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("9"));

            result = Symbol.Parse("#$fish.Id()", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("9"));*/
        }

        [Test]
        public void SymbolResolve()
        {
            string text;
            string result;
            Chat c1 = null;
            Symbol sym;

            //Symbol.DBUG = true;

            text = "The $animal ran.";
            sym = Symbol.Parse(text, c1)[0];
            result = sym.Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("dog"));
            result = sym.Replace(text, result, globals);
            Assert.That(result.ToString(), Is.EqualTo("The dog ran."));

            text = "The $animal.cap() ran.";
            sym = Symbol.Parse(text, c1)[0];
            result = sym.Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("dog"));
            result = sym.Replace(text, result, globals);
            Assert.That(result.ToString(), Is.EqualTo("The (dog).cap() ran."));

            text = "The ($animal).cap() ran.";
            sym = Symbol.Parse(text, c1)[0];
            result = sym.Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("dog"));
            result = sym.Replace(text, result, globals);
            Assert.That(result.ToString(), Is.EqualTo("The (dog).cap() ran."));

            text = "The $group ran.";
            sym = Symbol.Parse(text, c1)[0];
            result = sym.Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("(a|b)"));
            result = sym.Replace(text, result, globals);
            Assert.That(result.ToString(), Is.EqualTo("The (a|b) ran."));

            text = "The $group.cap() ran.";
            sym = Symbol.Parse(text, c1)[0];
            result = sym.Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("(a|b)"));
            result = sym.Replace(text, result, globals);
            Assert.That(result.ToString(), Is.EqualTo("The (a|b).cap() ran."));

            text = "The [selected=$group].cap() ran.";
            sym = Symbol.Parse(text, c1)[0];
            result = sym.Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("(a|b)"));
            result = sym.Replace(text, result, globals);
            Assert.That(result.ToString(), Is.EqualTo("The [selected=(a|b)].cap() ran."));
            Assert.That(globals.ContainsKey("selected"), Is.False);

            text = "The [selected=$animal] ran.";
            sym = Symbol.Parse(text, c1)[0];
            result = sym.Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("dog"));
            result = sym.Replace(text, result, globals);
            Assert.That(result.ToString(), Is.EqualTo("The dog ran."));
            Assert.That(globals["selected"], Is.EqualTo("dog"));

            text = "The [selected=$animal].cap() ran.";
            sym = Symbol.Parse(text, c1)[0]; // note: incorrect use of Transform
            result = sym.Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("dog"));
            result = sym.Replace(text, result, globals);
            Assert.That(result.ToString(), Is.EqualTo("The dog.cap() ran."));
            Assert.That(globals["selected"], Is.EqualTo("dog"));

            text = "The ([selected=$animal]).cap() ran.";
            sym = Symbol.Parse(text, c1)[0];
            result = sym.Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("dog"));
            result = sym.Replace(text, result, globals);
            Assert.That(result.ToString(), Is.EqualTo("The (dog).cap() ran."));
            Assert.That(globals["selected"], Is.EqualTo("dog"));

            text = "The [selected=$animal.cap()] ran.";
            sym = Symbol.Parse(text, c1)[0];
            result = sym.Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("dog"));
            result = sym.Replace(text, result, globals);
            Assert.That(result.ToString(), Is.EqualTo("The (dog).cap() ran."));
            Assert.That(globals["selected"], Is.EqualTo("dog"));
        }

        /*[Test]
        public void SymbolResolveBound()
        {
			// Same tests as above WITH bounded vars

            string text;
            string result;
            Chat c1 = null;
            Symbol sym;

			if (1 == 2) // TODO: refactor bounding to use []
			{            
				text = "The [selected=${animal}.cap()]-ran.";
				sym = Symbol.Parse(text, c1)[0];
				result = sym.Resolve(globals);
				Assert.That(result.ToString(), Is.EqualTo("dog"));
				result = sym.Replace(text, result, globals);
				Assert.That(result.ToString(), Is.EqualTo("The (dog).cap()-ran."));
				Assert.That(globals["selected"], Is.EqualTo("dog"));
			}

			text = "The ${animal}-ran.";
            sym = Symbol.Parse(text, c1)[0];
            result = sym.Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("dog"));
            result = sym.Replace(text, result, globals);
			Assert.That(result.ToString(), Is.EqualTo("The dog-ran."));
            
			text = "The ${animal.cap()}-ran.";
            sym = Symbol.Parse(text, c1)[0];
            result = sym.Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("dog"));
            result = sym.Replace(text, result, globals);
			Assert.That(result.ToString(), Is.EqualTo("The (dog).cap()-ran."));

			text = "The (${animal}).cap()-ran.";
            sym = Symbol.Parse(text, c1)[0];
            result = sym.Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("dog"));
            result = sym.Replace(text, result, globals);
			Assert.That(result.ToString(), Is.EqualTo("The (dog).cap()-ran."));
            
			text = "The ${group}-ran.";
            sym = Symbol.Parse(text, c1)[0];
            result = sym.Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("(a|b)"));
            result = sym.Replace(text, result, globals);
            Assert.That(result.ToString(), Is.EqualTo("The (a|b)-ran."));

			text = "The ${group}.cap()-ran.";
            sym = Symbol.Parse(text, c1)[0];
            result = sym.Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("(a|b)"));
            result = sym.Replace(text, result, globals);
            Assert.That(result.ToString(), Is.EqualTo("The (a|b).cap()-ran."));

			text = "The [selected=${group}].cap()-ran.";
            sym = Symbol.Parse(text, c1)[0];
            result = sym.Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("(a|b)"));
            result = sym.Replace(text, result, globals);         
            Assert.That(result.ToString(), Is.EqualTo("The [selected=(a|b)].cap()-ran."));
            Assert.That(globals.ContainsKey("selected"), Is.False); 
        
			text = "The [selected=${animal}]-ran."; // note: pointless bounding
            sym = Symbol.Parse(text, c1)[0];
            result = sym.Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("dog"));
            result = sym.Replace(text, result, globals);         
            Assert.That(result.ToString(), Is.EqualTo("The dog-ran."));
            Assert.That(globals["selected"], Is.EqualTo("dog"));  

			text = "The [selected=${animal}].cap()-ran.";
            sym = Symbol.Parse(text, c1)[0]; // note: incorrect use of Transform
            result = sym.Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("dog"));
            result = sym.Replace(text, result, globals);         
            Assert.That(result.ToString(), Is.EqualTo("The dog.cap()-ran."));
            Assert.That(globals["selected"], Is.EqualTo("dog")); 

			text = "The ([selected=${animal}]).cap()-ran.";
            sym = Symbol.Parse(text, c1)[0];
            result = sym.Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("dog"));
            result = sym.Replace(text, result, globals);         
            Assert.That(result.ToString(), Is.EqualTo("The (dog).cap()-ran."));
            Assert.That(globals["selected"], Is.EqualTo("dog")); 
        }*/

        [Test]
        public void SymbolResolveComplex()
        {
            string text;
            string result;
            Chat c1 = null;
            Symbol sym;

            text = "The $cmplx ran.";
            sym = Symbol.Parse(text, c1)[0];
            result = sym.Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("($group | $prep)"));
            result = sym.Replace(text, result, globals);
            Assert.That(result.ToString(), Is.EqualTo("The ($group | $prep) ran."));

            text = "The $cmplx.cap() ran.";
            sym = Symbol.Parse(text, c1)[0];
            result = sym.Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("($group | $prep)"));
            result = sym.Replace(text, result, globals);
            Assert.That(result.ToString(), Is.EqualTo("The ($group | $prep).cap() ran."));

            text = "The [selected=$cmplx.cap()] ran.";
            sym = Symbol.Parse(text, c1)[0];
            result = sym.Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("($group | $prep)"));
            result = sym.Replace(text, result, globals);
            Assert.That(result.ToString(), Is.EqualTo("The [selected=($group | $prep).cap()] ran."));
        }

    }
}