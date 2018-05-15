using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Dialogic
{
	[TestFixture]
	class TransformTests : GenericTests
	{
		[Test]
		public void SimpleTransformResolution()
		{
			var ti = Transforms.Instance;
   
			string result, text;
			Transform tran;
			Chat c1 = null;
                     
			text = "The (dog).Cap() ran.";
			tran = Transform.Parse(text, c1)[0];         
			result = tran.Resolve();
			Assert.That(result.ToString(), Is.EqualTo("Dog"));
			result = tran.Replace(text, result);
			Assert.That(result.ToString(), Is.EqualTo("The Dog ran."));

			text = "(ant).Articlize().Cap() ran.";
            tran = Transform.Parse(text, c1)[0];
            result = tran.Resolve();
			Assert.That(result.ToString(), Is.EqualTo("An ant"));
            result = tran.Replace(text, result);
			Assert.That(result.ToString(), Is.EqualTo("An ant ran."));

			text = "Today (ant).Cap().Articlize() ran.";
            tran = Transform.Parse(text, c1)[0];
            result = tran.Resolve();
            Assert.That(result.ToString(), Is.EqualTo("an Ant"));
            result = tran.Replace(text, result);
			Assert.That(result.ToString(), Is.EqualTo("Today an Ant ran."));

			text = "Today (an ant).Cap() ran.";
            tran = Transform.Parse(text, c1)[0];
            result = tran.Resolve();
			Assert.That(result.ToString(), Is.EqualTo("An ant"));
            result = tran.Replace(text, result);
			Assert.That(result.ToString(), Is.EqualTo("Today An ant ran."));
		}
	}
}