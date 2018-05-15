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