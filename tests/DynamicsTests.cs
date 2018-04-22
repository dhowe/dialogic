using System;
using System.Collections.Generic;
using System.Linq;
using Dialogic;
using NUnit.Framework;

namespace Dialogic
{
    [TestFixture]
    class DynamicsTests
    {
        const bool NO_VALIDATORS = true;

        static IDictionary<string, object> globals
            = new Dictionary<string, object>
        {
            { "obj-prop", "dog" },
            { "animal", "dog" },
            { "prep", "then" },
            { "name", "Jon" },
            { "verb", "melt" },
            { "prop1", "hum" },
            { "group", "(a|b)" },
            { "cmplx", "($group | $prep)" },
            { "count", 4 },
            { "fish",  new Fish("Fred")},
            { "a",  "A"}
        };

        class Fish
        {
            public static string species { get; protected set; }
            public static string GetSpecies() { return species; }

            public string name { get; protected set; }
            public Flipper flipper { get; protected set; }

            public Flipper GetFlipper() { return flipper; }

            public double GetFlipperSpeed() { return flipper.speed; }

            private int id = 9;

            public int Id() { return id; }
            public void Id(int id) { this.id = id; }

            public Fish(string name)
            {
                species = "Oscar";
                this.name = name;
                this.flipper = new Flipper(1.1);
            }
        }

        class Flipper
        {
            public double speed { get; protected set; }
            public Flipper(double s)
            {
                this.speed = s;
            }
            public override string ToString()
            {
                return this.speed.ToString();
            }
        }

        [Test]
        public void ResolveTraversalWithParameters()
        {
            // TODO:
            Chat c1 = null;
        }

        [Test]
        public void ResolveTraversalWithFunctions()
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

            // bounded ----------------------------------------------------------------

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
            Assert.That(result.ToString(), Is.EqualTo("9"));

        }
    
        [Test]
        public void ResolveSymbolTraversal()
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
        public void Exceptions()
        {
            Chat c1 = null;
            Assert.Throws<BindException>(() => Symbol.Parse("#chat", c1)[0].Resolve(globals));
            Assert.Throws<BindException>(() => Symbol.Parse("#{chat}", c1)[0].Resolve(globals));
        }

        [Test]
        public void SingleHashSymbolResolve()
        {
            Chat c1 = CreateParentChat();

            object result;
            result = Symbol.Parse("you #c1.prop1?", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("hum"));

            result = Symbol.Parse("you #{c1.prop1}?", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("hum"));

            result = Symbol.Parse("you #c1.prop1!", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("hum"));

            result = Symbol.Parse("you #{c1.prop1}!", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("hum"));

            result = Symbol.Parse("#c1.a", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("A"));

            result = Symbol.Parse("#{c1.a}", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("A"));

            result = Symbol.Parse("#c1.a", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("A"));

            result = Symbol.Parse("((a|b) | #c1.prep)", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("then"));

            result = Symbol.Parse("[bb=#c1.a]", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("A"));

            result = Symbol.Parse("[bb=#{c1.a}]", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("A"));

            result = Symbol.Parse("#c1.name", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("Jon"));

            result = Symbol.Parse("#c1.name,", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("Jon"));

            result = Symbol.Parse("Hello #c1.name,", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("Jon"));

            result = Symbol.Parse("to #c1.verb you", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("melt"));

            result = Symbol.Parse("#{c1.name}", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("Jon"));

            result = Symbol.Parse("Hello #{c1.name},", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("Jon"));

            result = Symbol.Parse("to #{c1.verb} you", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("melt"));

            result = Symbol.Parse("#{c1.name},", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("Jon"));
        }

        private static Chat CreateParentChat()
        {
            // create a realized Chat with the full set of global props
            var c1 = Chat.Create("c1");
            foreach (var prop in globals.Keys) c1.SetMeta(prop, globals[prop]);
            c1.Realize(globals);
            return c1;
        }

        [Test]
        public void SingleDollarSymbolResolve()
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
        public void SingleHashSymbolParsing()
        {
            Symbol s;

            s = Symbol.Parse("#a")[0];
            Assert.That(s.text, Is.EqualTo("#a"));
            Assert.That(s.symbol, Is.EqualTo("a"));
            Assert.That(s.alias, Is.Null);
            Assert.That(s.bounded, Is.EqualTo(false));
            Assert.That(s.chatScoped, Is.EqualTo(true));
            Assert.That(s.ToString(), Is.EqualTo(s.text));

            s = Symbol.Parse("#{a}")[0];
            Assert.That(s.text, Is.EqualTo("#{a}"));
            Assert.That(s.symbol, Is.EqualTo("a"));
            Assert.That(s.alias, Is.Null);
            Assert.That(s.bounded, Is.EqualTo(true));
            Assert.That(s.chatScoped, Is.EqualTo(true));
            Assert.That(s.ToString(), Is.EqualTo(s.text));

            s = Symbol.Parse("((a|b) | #prep)")[0];
            Assert.That(s.text, Is.EqualTo("#prep"));
            Assert.That(s.symbol, Is.EqualTo("prep"));
            Assert.That(s.alias, Is.Null);
            Assert.That(s.chatScoped, Is.EqualTo(true));
            Assert.That(s.bounded, Is.EqualTo(false));

            s = Symbol.Parse("#a")[0];
            Assert.That(s.text, Is.EqualTo("#a"));
            Assert.That(s.symbol, Is.EqualTo("a"));
            Assert.That(s.alias, Is.Null);
            Assert.That(s.chatScoped, Is.EqualTo(true));
            Assert.That(s.bounded, Is.EqualTo(false));

            s = Symbol.Parse("[bb=#a]")[0];
            Assert.That(s.text, Is.EqualTo("[bb=#a]"));
            Assert.That(s.symbol, Is.EqualTo("a"));
            Assert.That(s.alias, Is.EqualTo("bb"));
            Assert.That(s.bounded, Is.EqualTo(false));
            Assert.That(s.chatScoped, Is.EqualTo(true));

            s = Symbol.Parse("#{a}")[0];
            Assert.That(s.text, Is.EqualTo("#{a}"));
            Assert.That(s.symbol, Is.EqualTo("a"));
            Assert.That(s.alias, Is.Null);
            Assert.That(s.chatScoped, Is.EqualTo(true));
            Assert.That(s.bounded, Is.EqualTo(true));

            s = Symbol.Parse("[bb=#{a}]")[0];
            Assert.That(s.text, Is.EqualTo("[bb=#{a}]"));
            Assert.That(s.symbol, Is.EqualTo("a"));
            Assert.That(s.alias, Is.EqualTo("bb"));
            Assert.That(s.bounded, Is.EqualTo(true));
            Assert.That(s.chatScoped, Is.EqualTo(true));

            s = Symbol.Parse("#name")[0];
            Assert.That(s.text, Is.EqualTo("#name"));
            Assert.That(s.symbol, Is.EqualTo("name"));
            Assert.That(s.alias, Is.Null);
            Assert.That(s.chatScoped, Is.EqualTo(true));
            Assert.That(s.bounded, Is.EqualTo(false));

            s = Symbol.Parse("#name,")[0];
            Assert.That(s.text, Is.EqualTo("#name"));
            Assert.That(s.symbol, Is.EqualTo("name"));
            Assert.That(s.alias, Is.Null);
            Assert.That(s.chatScoped, Is.EqualTo(true));
            Assert.That(s.bounded, Is.EqualTo(false));

            s = Symbol.Parse("Hello #name,")[0];
            Assert.That(s.text, Is.EqualTo("#name"));
            Assert.That(s.symbol, Is.EqualTo("name"));
            Assert.That(s.alias, Is.Null);
            Assert.That(s.chatScoped, Is.EqualTo(true));
            Assert.That(s.bounded, Is.EqualTo(false));
            //text = "Hello #name, nice to #verb you #prop1!";

            s = Symbol.Parse("to #verb you")[0];
            Assert.That(s.text, Is.EqualTo("#verb"));
            Assert.That(s.symbol, Is.EqualTo("verb"));
            Assert.That(s.alias, Is.Null);
            Assert.That(s.chatScoped, Is.EqualTo(true));
            Assert.That(s.bounded, Is.EqualTo(false));

            s = Symbol.Parse("you #prop1!")[0];
            Assert.That(s.text, Is.EqualTo("#prop1"));
            Assert.That(s.symbol, Is.EqualTo("prop1"));
            Assert.That(s.alias, Is.Null);
            Assert.That(s.chatScoped, Is.EqualTo(true));
            Assert.That(s.bounded, Is.EqualTo(false));
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

        [Test]
        public void SingleDollarSymbolParsing()
        {
            Symbol s;

            s = Symbol.Parse("$a")[0];
            Assert.That(s.text, Is.EqualTo("$a"));
            Assert.That(s.symbol, Is.EqualTo("a"));
            Assert.That(s.alias, Is.Null);
            Assert.That(s.bounded, Is.EqualTo(false));
            Assert.That(s.chatScoped, Is.EqualTo(false));
            Assert.That(s.ToString(), Is.EqualTo(s.text));

            s = Symbol.Parse("#$a")[0];
            Assert.That(s.text, Is.EqualTo("$a"));
            Assert.That(s.symbol, Is.EqualTo("a"));
            Assert.That(s.alias, Is.Null);
            Assert.That(s.bounded, Is.EqualTo(false));
            Assert.That(s.chatScoped, Is.EqualTo(false));
            Assert.That(s.ToString(), Is.EqualTo(s.text));

            s = Symbol.Parse("${a}")[0];
            Assert.That(s.text, Is.EqualTo("${a}"));
            Assert.That(s.symbol, Is.EqualTo("a"));
            Assert.That(s.alias, Is.Null);
            Assert.That(s.bounded, Is.EqualTo(true));
            Assert.That(s.chatScoped, Is.EqualTo(false));
            Assert.That(s.ToString(), Is.EqualTo(s.text));

            s = Symbol.Parse("((a|b) | $prep)")[0];
            Assert.That(s.text, Is.EqualTo("$prep"));
            Assert.That(s.symbol, Is.EqualTo("prep"));
            Assert.That(s.alias, Is.Null);
            Assert.That(s.chatScoped, Is.EqualTo(false));
            Assert.That(s.bounded, Is.EqualTo(false));

            s = Symbol.Parse("$a")[0];
            Assert.That(s.text, Is.EqualTo("$a"));
            Assert.That(s.symbol, Is.EqualTo("a"));
            Assert.That(s.alias, Is.Null);
            Assert.That(s.chatScoped, Is.EqualTo(false));
            Assert.That(s.bounded, Is.EqualTo(false));

            s = Symbol.Parse("[bb=$a]")[0];
            Assert.That(s.text, Is.EqualTo("[bb=$a]"));
            Assert.That(s.symbol, Is.EqualTo("a"));
            Assert.That(s.alias, Is.EqualTo("bb"));
            Assert.That(s.chatScoped, Is.EqualTo(false));
            Assert.That(s.bounded, Is.EqualTo(false));

            s = Symbol.Parse("${a}")[0];
            Assert.That(s.text, Is.EqualTo("${a}"));
            Assert.That(s.symbol, Is.EqualTo("a"));
            Assert.That(s.alias, Is.Null);
            Assert.That(s.chatScoped, Is.EqualTo(false));
            Assert.That(s.bounded, Is.EqualTo(true));

            s = Symbol.Parse("[bb=${a}]")[0];
            Assert.That(s.text, Is.EqualTo("[bb=${a}]"));
            Assert.That(s.symbol, Is.EqualTo("a"));
            Assert.That(s.alias, Is.EqualTo("bb"));
            Assert.That(s.chatScoped, Is.EqualTo(false));
            Assert.That(s.bounded, Is.EqualTo(true));

            s = Symbol.Parse("$name")[0];
            Assert.That(s.text, Is.EqualTo("$name"));
            Assert.That(s.symbol, Is.EqualTo("name"));
            Assert.That(s.alias, Is.Null); 
            Assert.That(s.chatScoped, Is.EqualTo(false));
            Assert.That(s.bounded, Is.EqualTo(false));

            s = Symbol.Parse("$name,")[0];
            Assert.That(s.text, Is.EqualTo("$name"));
            Assert.That(s.symbol, Is.EqualTo("name"));
            Assert.That(s.alias, Is.Null);
            Assert.That(s.bounded, Is.EqualTo(false));

            s = Symbol.Parse("Hello $name,")[0];
            Assert.That(s.text, Is.EqualTo("$name"));
            Assert.That(s.symbol, Is.EqualTo("name"));
            Assert.That(s.alias, Is.Null);
            Assert.That(s.chatScoped, Is.EqualTo(false));
            Assert.That(s.bounded, Is.EqualTo(false));
            //text = "Hello $name, nice to $verb you $prop1!";

            s = Symbol.Parse("to $verb you")[0];
            Assert.That(s.text, Is.EqualTo("$verb"));
            Assert.That(s.symbol, Is.EqualTo("verb"));
            Assert.That(s.alias, Is.Null);
            Assert.That(s.chatScoped, Is.EqualTo(false));
            Assert.That(s.bounded, Is.EqualTo(false));

            s = Symbol.Parse("you $prop1!")[0];
            Assert.That(s.text, Is.EqualTo("$prop1"));
            Assert.That(s.symbol, Is.EqualTo("prop1"));
            Assert.That(s.alias, Is.Null);
            Assert.That(s.chatScoped, Is.EqualTo(false));
            Assert.That(s.bounded, Is.EqualTo(false));
        }

        //[Test]
        //public void ParseSymbolWithMod()
        //{
        //    var ts = new[] { "", ".", "!", ":", ";", ",", "?", ")", "\"", "'" };
        //    foreach (var t in ts)
        //    {
        //        var sy = Symbol.Parse("$ab&c" + t).First();
        //        Assert.That(sy.symbol, Is.EqualTo("ab"));
        //        Assert.That(sy.modifiers.Count, Is.EqualTo(1));
        //        Assert.That(sy.modifiers[0], Is.EqualTo("c"));
        //    }

        //    var sym = Symbol.Parse("$ab&mod1&mod2.").First();
        //    Assert.That(sym.symbol, Is.EqualTo("ab"));
        //    Assert.That(sym.modifiers.Count, Is.EqualTo(2));
        //    Assert.That(sym.modifiers[0], Is.EqualTo("mod1"));
        //    Assert.That(sym.modifiers[1], Is.EqualTo("mod2"));
        //}

        [Test]
        public void ParseSymbols()
        {
            var ts = new[] { "", ".", "!", ":", ";", ",", "?", ")", "\"", "'" };
            foreach (var t in ts) Assert.That(Symbol.Parse
                ("$a" + t).First().symbol, Is.EqualTo("a"));

            foreach (var t in ts) Assert.That(Symbol.Parse
                ("${a}" + t).First().symbol, Is.EqualTo("a"));

            foreach (var t in ts) Assert.That(Symbol.Parse
                ("[b=$a]" + t).First().symbol, Is.EqualTo("a"));

            foreach (var t in ts) Assert.That(Symbol.Parse
                ("[b=${a}]" + t).First().symbol, Is.EqualTo("a"));

            Assert.That(Symbol.Parse("${a}").First().symbol, Is.EqualTo("a"));
            Assert.That(Symbol.Parse("${a.b}").First().symbol, Is.EqualTo("a.b"));

            Assert.That(Symbol.Parse("$a.b").First().symbol, Is.EqualTo("a.b"));
            Assert.That(Symbol.Parse("[b=$a]").First().symbol, Is.EqualTo("a"));

            Assert.That(Symbol.Parse("[bc=$a.b]").First().symbol, Is.EqualTo("a.b"));
            Assert.That(Symbol.Parse("[bc=$a.b]").First().alias, Is.EqualTo("bc"));

            Assert.That(Symbol.Parse("[c=$a.b]").First().symbol, Is.EqualTo("a.b"));
            Assert.That(Symbol.Parse("[c=$a.b]").First().alias, Is.EqualTo("c"));


            Assert.That(Symbol.Parse("[c=$a.b].").First().symbol, Is.EqualTo("a.b"));
            Assert.That(Symbol.Parse("[c=$a.b].").First().alias, Is.EqualTo("c"));

            Assert.That(Symbol.Parse("[c=${a.b}].").First().symbol, Is.EqualTo("a.b"));
            Assert.That(Symbol.Parse("[c=${a.b}].").First().alias, Is.EqualTo("c"));

            Assert.That(Symbol.Parse("${a}b").First().symbol, Is.EqualTo("a"));
            Assert.That(Symbol.Parse("${a.b}b").First().symbol, Is.EqualTo("a.b"));
        }

        [Test]
        public void ResolutionTest() // unused
        {
            string last = null, choice;
            for (int i = 0; i < 10; i++)
            {
                choice = Resolution.Choose("(a | b | c)");
                Assert.That(choice, Is.EqualTo("a").Or.EqualTo("b").Or.EqualTo("c"));
                Assert.That(choice, Is.Not.EqualTo(last));
                last = choice;
            }

            for (int i = 0; i < 10; i++)
            {
                Assert.That(Resolution.Choose("(a|b)"), Is.EqualTo("a").Or.EqualTo("b"));
            }

            for (int i = 0; i < 3; i++)
            {
                Assert.That(Resolution.Choose("(a |a)"), Is.EqualTo("a"));
            }
        }

    }
}
