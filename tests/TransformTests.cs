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
			TxForm tran;
			Chat c1 = null;

			text = "The so-called (dog).Quotify() ran.";
            tran = TxForm.Parse(text, c1)[0];
            result = tran.Resolve();
			Assert.That(result.ToString(), Is.EqualTo("&quot;dog&quot;"));
            result = tran.Replace(text, result);
			Assert.That(result.ToString(), Is.EqualTo("The so-called &quot;dog&quot; ran."));

			text = "The (dog).Cap() ran.";
			tran = TxForm.Parse(text, c1)[0];         
			result = tran.Resolve();
			Assert.That(result.ToString(), Is.EqualTo("Dog"));
			result = tran.Replace(text, result);
			Assert.That(result.ToString(), Is.EqualTo("The Dog ran."));

			text = "(ant).Articlize().Cap() ran.";
            tran = TxForm.Parse(text, c1)[0];
            result = tran.Resolve();
			Assert.That(result.ToString(), Is.EqualTo("An ant"));
            result = tran.Replace(text, result);
			Assert.That(result.ToString(), Is.EqualTo("An ant ran."));

			text = "Today (ant).Cap().Articlize() ran.";
            tran = TxForm.Parse(text, c1)[0];
            result = tran.Resolve();
            Assert.That(result.ToString(), Is.EqualTo("an Ant"));
            result = tran.Replace(text, result);
			Assert.That(result.ToString(), Is.EqualTo("Today an Ant ran."));

			text = "Today (an ant).Cap() ran.";
            tran = TxForm.Parse(text, c1)[0];
            result = tran.Resolve();
			Assert.That(result.ToString(), Is.EqualTo("An ant"));
            result = tran.Replace(text, result);
			Assert.That(result.ToString(), Is.EqualTo("Today An ant ran."));

			text = "(a (then).Cap())";
            tran = TxForm.Parse(text, c1)[0];
            result = tran.Resolve();
            Assert.That(result.ToString(), Is.EqualTo("Then"));
            result = tran.Replace(text, result);
			Assert.That(result.ToString(), Is.EqualTo("(a Then)"));
		}

        [Test]
        public void TestPluralizePhrase()
        {
            Assert.That(Transforms.Pluralize("coffee cup"), Is.EqualTo("coffee cups"));
            Assert.That(Transforms.Pluralize("front tooth"), Is.EqualTo("front teeth"));
        }

		[Test]
        public void TestPluralize()
        {
            Assert.That(Transforms.Pluralize(""), Is.EqualTo(""));
            Assert.That(Transforms.Pluralize("dog"), Is.EqualTo("dogs"));
            Assert.That(Transforms.Pluralize("eye"), Is.EqualTo("eyes"));

            Assert.That(Transforms.Pluralize("blonde"), Is.EqualTo("blondes"));
            Assert.That(Transforms.Pluralize("blond"), Is.EqualTo("blondes"));
            Assert.That(Transforms.Pluralize("foot"), Is.EqualTo("feet"));
            Assert.That(Transforms.Pluralize("man"), Is.EqualTo("men"));
            Assert.That(Transforms.Pluralize("tooth"), Is.EqualTo("teeth"));
            Assert.That(Transforms.Pluralize("cake"), Is.EqualTo("cakes"));
            Assert.That(Transforms.Pluralize("kiss"), Is.EqualTo("kisses"));
            Assert.That(Transforms.Pluralize("child"), Is.EqualTo("children"));

            Assert.That(Transforms.Pluralize("louse"), Is.EqualTo("lice"));

            Assert.That(Transforms.Pluralize("sheep"), Is.EqualTo("sheep"));
            Assert.That(Transforms.Pluralize("shrimp"), Is.EqualTo("shrimps"));
            Assert.That(Transforms.Pluralize("series"), Is.EqualTo("series"));
            Assert.That(Transforms.Pluralize("mouse"), Is.EqualTo("mice"));

            Assert.That(Transforms.Pluralize("beautiful"), Is.EqualTo("beautifuls"));

            Assert.That(Transforms.Pluralize("crisis"), Is.EqualTo("crises"));
            Assert.That(Transforms.Pluralize("thesis"), Is.EqualTo("theses"));
            Assert.That(Transforms.Pluralize("apothesis"), Is.EqualTo("apotheses"));
            Assert.That(Transforms.Pluralize("stimulus"), Is.EqualTo("stimuli"));
            Assert.That(Transforms.Pluralize("alumnus"), Is.EqualTo("alumni"));
            Assert.That(Transforms.Pluralize("corpus"), Is.EqualTo("corpora"));

            Assert.That(Transforms.Pluralize("woman"), Is.EqualTo("women"));
            Assert.That(Transforms.Pluralize("man"), Is.EqualTo("men"));
            Assert.That(Transforms.Pluralize("congressman"), Is.EqualTo("congressmen"));
            Assert.That(Transforms.Pluralize("alderman"), Is.EqualTo("aldermen"));
            Assert.That(Transforms.Pluralize("freshman"), Is.EqualTo("freshmen"));

            Assert.That(Transforms.Pluralize("bikini"), Is.EqualTo("bikinis"));
            Assert.That(Transforms.Pluralize("martini"), Is.EqualTo("martinis"));
            Assert.That(Transforms.Pluralize("menu"), Is.EqualTo("menus"));
            Assert.That(Transforms.Pluralize("guru"), Is.EqualTo("gurus"));

            Assert.That(Transforms.Pluralize("medium"), Is.EqualTo("media"));
            Assert.That(Transforms.Pluralize("concerto"), Is.EqualTo("concerti"));
            Assert.That(Transforms.Pluralize("terminus"), Is.EqualTo("termini"));

            Assert.That(Transforms.Pluralize("aquatics"), Is.EqualTo("aquatics"));
            Assert.That(Transforms.Pluralize("mechanics"), Is.EqualTo("mechanics"));

            Assert.That(Transforms.Pluralize("tomato"), Is.EqualTo("tomatoes"));
            Assert.That(Transforms.Pluralize("toe"), Is.EqualTo("toes"));

            Assert.That(Transforms.Pluralize("deer"), Is.EqualTo("deer"));
            Assert.That(Transforms.Pluralize("moose"), Is.EqualTo("moose"));
            Assert.That(Transforms.Pluralize("ox"), Is.EqualTo("oxen"));

            Assert.That(Transforms.Pluralize("tobacco"), Is.EqualTo("tobacco"));
            Assert.That(Transforms.Pluralize("cargo"), Is.EqualTo("cargo"));
            Assert.That(Transforms.Pluralize("golf"), Is.EqualTo("golf"));
            Assert.That(Transforms.Pluralize("grief"), Is.EqualTo("grief"));
            Assert.That(Transforms.Pluralize("wildlife"), Is.EqualTo("wildlife"));
            Assert.That(Transforms.Pluralize("taxi"), Is.EqualTo("taxis"));
            Assert.That(Transforms.Pluralize("Chinese"), Is.EqualTo("Chinese"));
            Assert.That(Transforms.Pluralize("bonsai"), Is.EqualTo("bonsai"));

            Assert.That(Transforms.Pluralize("gas"), Is.EqualTo("gases"));
            Assert.That(Transforms.Pluralize("bus"), Is.EqualTo("buses"));
        }
	}
}