using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Dialogic
{
    [TestFixture]
    class SymbolTests : GenericTests
    {
		/*{ "obj-prop", "dog" },
		  { "animal", "dog" },
		  { "prep", "then" },
		  { "group", "(a|b)" },
		  { "cmplx", "($group | $prep)" },
		  { "count", 4 },
		  { "prop1", "hum" },
		  { "name", "Jon" },
		  { "verb", "melt" },
		  { "a", "A" },
		  { "fish",  new Fish("Fred")} */

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

		//[Test]
        public void SymbolResolveBound()
        {
			// Same tests as above WITH bounded vars

            string text;
            string result;
            Chat c1 = null;
            Symbol sym;
            
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
                     
			text = "The [selected=${animal}]-ran.";
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

			text = "The [selected=${animal}.cap()]-ran.";
            sym = Symbol.Parse(text, c1)[0];
            result = sym.Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("dog"));
            result = sym.Replace(text, result, globals);
            Assert.That(result.ToString(), Is.EqualTo("The (dog).cap()-ran."));  
            Assert.That(globals["selected"], Is.EqualTo("dog")); 
        }

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