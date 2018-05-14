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
            
			text = "The $animal ran.";
			sym = Symbol.Parse(text, c1)[0];
			result = sym.Resolve(globals);
			result = sym.Replace(text, result);
			Assert.That(result.ToString(), Is.EqualTo("The dog ran."));
                     
			text = "The $animal.cap() ran.";
            sym = Symbol.Parse(text, c1)[0];
            result = sym.Resolve(globals);
			result = sym.Replace(text, result);
			Assert.That(result.ToString(), Is.EqualTo("The (dog).cap() ran."));

			text = "The $group ran. ";
            sym = Symbol.Parse(text, c1)[0];
            result = sym.Resolve(globals);
            result = sym.Replace(text, result);
			Assert.That(result.ToString(), Is.EqualTo("The (a|b).cap()."));
        }
    }
}