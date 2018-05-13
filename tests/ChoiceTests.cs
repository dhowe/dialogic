using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Dialogic
{
	//[TestFixture]
	class ChoiceTests : GenericTests
	{
		[Test]
		public void ChoiceParseTests()
		{
			Chat c = CreateParentChat("c");

			Choice choice;
			string[] expected;
			string text;

			text = "you(a | b) a ";
			choice = Choice.Parse(text, c, false)[0];
			expected = new[] { "a", "b" };
			Assert.That(choice.text, Is.EqualTo("(a | b)"));
			Assert.That(choice.options.Length, Is.EqualTo(2));
			Assert.That(choice.options, Is.EqualTo(expected));
			//CollectionAssert.Contains(expected, choice.Resolve());
                     
			text = "you (a | b).ToUpper() and ...";
            choice = Choice.Parse(text, c, false)[0];
			expected = new[] { "a", "b" };
			Assert.That(choice.text, Is.EqualTo("(a | b).ToUpper()"));
			Assert.That(choice.options.Length, Is.EqualTo(2));
			Assert.That(choice.options, Is.EqualTo(expected));
			//Assert.That(choice.Resolve(), Is.SubsetOf(new[] { "A", "B" }));
			////CollectionAssert.Contains(new[] { "A", "B" }, choice.Resolve());

			text = "you (a|b) are";
            choice = Choice.Parse(text, c, false)[0];
			expected = new[] { "a", "b" };
			Assert.That(choice.text, Is.EqualTo("(a|b)"));
			Assert.That(choice.options.Length, Is.EqualTo(2));
			Assert.That(choice.options, Is.EqualTo(expected));
			//CollectionAssert.Contains(expected, choice.Resolve());

			text = "you (a|b).ToUpper() are";
            choice = Choice.Parse(text, c, false)[0];
			expected = new[] { "a", "b" };
			Assert.That(choice.text, Is.EqualTo("(a|b).ToUpper()"));
			Assert.That(choice.options.Length, Is.EqualTo(2));
			Assert.That(choice.options, Is.EqualTo(expected));
			Assert.That(choice._TransArray(), Is.EquivalentTo(new[] { "ToUpper" }));
			//CollectionAssert.Contains(new[] { "A", "B" }, choice.Resolve());

			//Resolver.DBUG = true;
			choice = Choice.Parse("you (a|b).ToUpper().articlize() are", c)[0];
			expected = new[] { "a", "b" };
			Assert.That(choice.text, Is.EqualTo("(a|b).ToUpper().articlize()"));
			Assert.That(choice.options.Length, Is.EqualTo(2));
			Assert.That(choice.options, Is.EqualTo(expected));
			Assert.That(choice._TransArray(), Is.EquivalentTo(new[] { "ToUpper", "articlize" }));
			//CollectionAssert.Contains(new[] { "an A", "a B" }, choice.Resolve());

			choice = Choice.Parse("you (a | b |c). are", c)[0];
			expected = new[] { "a", "b", "c" };
			Assert.That(choice.text, Is.EqualTo("(a | b |c)"));
			Assert.That(choice.options.Length, Is.EqualTo(3));
			Assert.That(choice.options, Is.EqualTo(expected));
			//CollectionAssert.Contains(expected, choice.Resolve());

			//Resolver.DBUG = true;
			choice = Choice.Parse("you (a | b |c).ToUpper(). are", c)[0];
			expected = new[] { "a", "b", "c" };
			Assert.That(choice.text, Is.EqualTo("(a | b |c).ToUpper()"));
			Assert.That(choice.options.Length, Is.EqualTo(3));
			Assert.That(choice.options, Is.EqualTo(expected));
			//Assert.That(choice.Resolve(), Is.SubsetOf(new[] { "A", "B", "C" }));

			choice = Choice.Parse("you (a | b |c).ToUpper().articlize(). are", c)[0];
			expected = new[] { "a", "b", "c" };
			Assert.That(choice.text, Is.EqualTo("(a | b |c).ToUpper().articlize()"));
			Assert.That(choice.options.Length, Is.EqualTo(3));
			Assert.That(choice.options, Is.EqualTo(expected));
			//CollectionAssert.Contains(new[] { "an A", "a B", "a C" }, choice.Resolve());

			choice = Choice.Parse("you [d=(a | b | c)]. The", c)[0];
			expected = new[] { "a", "b", "c" };
			Assert.That(choice.text, Is.EqualTo("[d=(a | b | c)]"));
			Assert.That(choice.options.Length, Is.EqualTo(3));
			Assert.That(choice.alias, Is.EqualTo("d"));
			Assert.That(choice.options, Is.EqualTo(expected));
			//CollectionAssert.Contains(expected, choice.Resolve());
			//Assert.That(c.scope.ContainsKey("d"), Is.True);
			//c.scope.Remove("d");

			//Resolver.DBUG = true;
			choice = Choice.Parse("you [d=(a | b | c).ToUpper()]. The", c)[0];
			expected = new[] { "a", "b", "c" };
			Assert.That(choice.text, Is.EqualTo("[d=(a | b | c).ToUpper()]"));
			Assert.That(choice.options.Length, Is.EqualTo(3));
			Assert.That(choice.alias, Is.EqualTo("d"));
			Assert.That(choice.options, Is.EqualTo(expected));
			//CollectionAssert.Contains(new[] { "A", "B", "C" }, choice.Resolve());
			//Assert.That(c.scope.ContainsKey("d"), Is.True);
			//c.scope.Remove("d");

			choice = Choice.Parse("you [d=(a | b | c).ToUpper().articlize()]. The", c)[0];
			expected = new[] { "a", "b", "c" };
			Assert.That(choice.text, Is.EqualTo("[d=(a | b | c).ToUpper().articlize()]"));
			Assert.That(choice.options.Length, Is.EqualTo(3));
			Assert.That(choice.alias, Is.EqualTo("d"));
			Assert.That(choice.options, Is.EqualTo(expected));
			//CollectionAssert.Contains(new[] { "an A", "a B", "a C" }, choice.Resolve());
			//Assert.That(c.scope.ContainsKey("d"), Is.True);
			//c.scope.Remove("d");

			choice = Choice.Parse("you [d=(a | b | c)].ToUpper(). The", c)[0];
			expected = new[] { "a", "b", "c" };
			Assert.That(choice.text, Is.EqualTo("[d=(a | b | c)].ToUpper()"));
			Assert.That(choice.options.Length, Is.EqualTo(3));
			Assert.That(choice.options, Is.EqualTo(expected));
			Assert.That(choice.alias, Is.EqualTo("d"));
			//CollectionAssert.Contains(new[] { "A", "B", "C" }, choice.Resolve());
			//Assert.That(c.scope.ContainsKey("d"), Is.True);
			//c.scope.Remove("d");

			choice = Choice.Parse("you [d=(a | b | c)].ToUpper().articlize(). The", c)[0];
			expected = new[] { "a", "b", "c" };
			Assert.That(choice.text, Is.EqualTo("[d=(a | b | c)].ToUpper().articlize()"));
			Assert.That(choice.options.Length, Is.EqualTo(3));
			Assert.That(choice.options, Is.EqualTo(expected));
			Assert.That(choice.alias, Is.EqualTo("d"));
			//CollectionAssert.Contains(new[] { "an A", "a B", "a C" }, choice.Resolve());
			//Assert.That(c.scope.ContainsKey("d"), Is.True);
			//c.scope.Remove("d");

			choice = Choice.Parse("you [selected=(a | b | c)]. The", c)[0];
			expected = new[] { "a", "b", "c" };
			Assert.That(choice.text, Is.EqualTo("[selected=(a | b | c)]"));
			Assert.That(choice.options.Length, Is.EqualTo(3));
			Assert.That(choice.alias, Is.EqualTo("selected"));
			Assert.That(choice.options, Is.EqualTo(expected));
			//CollectionAssert.Contains(expected, choice.Resolve());
			//Assert.That(c.scope.ContainsKey("selected"), Is.True);
			//c.scope.Remove("selected");

			//Resolver.DBUG = true;
			choice = Choice.Parse("you [selected=(a | b | c).ToUpper()]. The", c)[0];
			expected = new[] { "a", "b", "c" };
			Assert.That(choice.text, Is.EqualTo("[selected=(a | b | c).ToUpper()]"));
			Assert.That(choice.options.Length, Is.EqualTo(3));
			Assert.That(choice.alias, Is.EqualTo("selected"));
			Assert.That(choice.options, Is.EqualTo(expected));
			//CollectionAssert.Contains(new[] { "A", "B", "C" }, choice.Resolve());
			//Assert.That(c.scope.ContainsKey("selected"), Is.True);
			//c.scope.Remove("selected");

			choice = Choice.Parse("you [selected=(a | b | c).ToUpper().articlize()]. The", c)[0];
			expected = new[] { "a", "b", "c" };
			Assert.That(choice.text, Is.EqualTo("[selected=(a | b | c).ToUpper().articlize()]"));
			Assert.That(choice.options.Length, Is.EqualTo(3));
			Assert.That(choice.alias, Is.EqualTo("selected"));
			Assert.That(choice.options, Is.EqualTo(expected));
			//CollectionAssert.Contains(new[] { "an A", "a B", "a C" }, choice.Resolve());
			//Assert.That(c.scope.ContainsKey("selected"), Is.True);
			//c.scope.Remove("selected");
		}

		[Test]
		public void BindChoicesTest()
		{
			string res;
			var c = CreateParentChat("c");

			//Resolver.DBUG = true;
			res = new Resolver(null).Bind("(cat | cat).Pluralize()", c, null);
			Assert.That(res, Is.EqualTo("cats"));

			res = new Resolver(null).Bind("(cat | cat).Pluralize().Articlize()", c, null);
			Assert.That(res, Is.EqualTo("a cats"));

			res = new Resolver(null).Bind("(cat | cat).Pluralize().Articlize()", c, null);
			Assert.That(res, Is.EqualTo("a cats"));

			res = new Resolver(null).Bind("(cat | cat).Pluralize().An()", c, null);
			Assert.That(res, Is.EqualTo("a cats"));

			res = new Resolver(null).Bind("(hello world | hello world).Capitalize()", c, null);
			Assert.That(res, Is.EqualTo("Hello world"));
		}
	}
}