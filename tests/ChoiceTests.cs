using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Dialogic
{
	[TestFixture]
	class ChoiceTests : GenericTests
	{
		[Test]
		public void ATransformWithinAChoice()
		{
			// WORKING HERE: 
			// See: https://stackoverflow.com/questions/50358674/c-sharp-regex-for-negated-character-class-unless-chars-are-next-to-one-another
			var txt = "CHAT c1\nSET a = a ($animal.Cap() | $prep.Cap())\nSAY $a";
			ChatRuntime rt = new ChatRuntime();
			rt.ParseText(txt);
			Resolver.DBUG = false;
			rt.strictMode = false;
			for (int i = 0; i < 1; i++)
			{
				var s = rt.InvokeImmediate(globals);
				Console.WriteLine(s);
				Assert.That(s.IsOneOf(new[] { "a Dog", "a Then" }));
			}
		}

		public void _TestParseGroup(Regex re, string text, string expected, int num = -1)
		{
			var match = re.Match(text);
			//Util.ShowMatch(match);
			//Assert.That(match.Groups[0].Value, Is.EqualTo("((dog).cap() | (then).cap())"));
			//if (num >= 0) Console.WriteLine(num + ") " + text + " => '" + match.Groups[1].Value + "'");
			Assert.That(match.Groups[1].Value, Is.EqualTo(expected));
		}



		[Test]
		public void TestNewChoice()
		{
			//var re = new Regex(@"\(++\)");
			//var PRN = new Regex(@"\(((?:(?:[^()]|\([^|]*?\))*?\|(?:[^()]|\([^|]*?\))*?))\)");
			//var PRN = new Regex(@"\(((?:(?:[^()]|\(\))*\|(?:[^()]|\(\))*))\)");

			var PRN = new Regex(RE.PRN);
			string[] tests = {
				"x (a|) y", "a|",
				"x (a|b|) y", "a|b|",
				"x ((a).b() | (b).c()) y", "(a).b() | (b).c()",
				"x (a|b) y", "a|b",
				"x (a|b|c) y", "a|b|c",
				"x (a|a.b()|c) y", "a|a.b()|c",
				"x (a.b()|b.c()) y", "a.b()|b.c()",
				"x (a.b()|b.c()|c) y", "a.b()|b.c()|c",
				"x (a|b.c()|c.d()) y", "a|b.c()|c.d()",
				"x (a|(b.c()|d)) y", "b.c()|d",
            
				"x () y", "",
				"x (a) y", "",
				"x (a.b()) y", "",
				"x (a|a.b(a)|c) y", "a|a.b(a)|c",
				"((dog).cap() | (then).cap())", "(dog).cap() | (then).cap()"
			};         
			for (int i = 0; i < tests.Length; i += 2)
			{
				_TestParseGroup(PRN, tests[i], tests[i + 1], i / 2);
			}
		}


		[Test]
		public void ParseNestedChoice()
		{
			Chat c = CreateParentChat("c");
			Choice choice;
			string[] expected;
			string text;

			text = "((dog).Cap() | (then).Cap())";
			choice = Choice.Parse(text, c, false)[0];
			expected = new[] { "(dog).Cap()", "(then).Cap()" };
			Assert.That(choice.text, Is.EqualTo("((dog).Cap() | (then).Cap())"));
			Assert.That(choice.options.Length, Is.EqualTo(expected.Length));
			Assert.That(choice.options, Is.EqualTo(expected));
		}

		[Test]
		public void ChoiceParseTests()
		{
			Chat c = CreateParentChat("c");
			Choice choice;
			string[] expected;
			string text;
            
			text = "you ($miss1 | $miss2.Cap() | ok) blah";
            choice = Choice.Parse(text, c, false)[0];
			expected = new[] { "$miss1", "$miss2.Cap()", "ok" };
			Assert.That(choice.text, Is.EqualTo("($miss1 | $miss2.Cap() | ok)"));
			Assert.That(choice.options.Length, Is.EqualTo(expected.Length));
            Assert.That(choice.options, Is.EqualTo(expected));         

			text = "you (a |) blah";
            choice = Choice.Parse(text, c, false)[0];
            expected = new[] { "a", "" };
            Assert.That(choice.text, Is.EqualTo("(a |)"));
            Assert.That(choice.options.Length, Is.EqualTo(expected.Length));
            Assert.That(choice.options, Is.EqualTo(expected));

			text = "you (hello|) blah";
            choice = Choice.Parse(text, c, false)[0];
			expected = new[] { "hello", "" };
			Assert.That(choice.text, Is.EqualTo("(hello|)"));
            Assert.That(choice.options.Length, Is.EqualTo(expected.Length));
            Assert.That(choice.options, Is.EqualTo(expected));

			text = "you (hello|hello) blah";
            choice = Choice.Parse(text, c, false)[0];
            expected = new[] { "hello", };
            Assert.That(choice.text, Is.EqualTo("(hello|hello)"));
            Assert.That(choice.options.Length, Is.EqualTo(expected.Length));
            Assert.That(choice.options, Is.EqualTo(expected));

			text = "you (hello|hello|) blah";
            choice = Choice.Parse(text, c, false)[0];
            expected = new[] { "hello", "" };
			Assert.That(choice.text, Is.EqualTo("(hello|hello|)"));
            Assert.That(choice.options.Length, Is.EqualTo(expected.Length));
            Assert.That(choice.options, Is.EqualTo(expected));

			text = "you ($miss1 | $miss2) blah";
			choice = Choice.Parse(text, c, false)[0];
			expected = new[] { "$miss1", "$miss2" };
			Assert.That(choice.text, Is.EqualTo("($miss1 | $miss2)"));
			Assert.That(choice.options.Length, Is.EqualTo(expected.Length));
			Assert.That(choice.options, Is.EqualTo(expected));

			text = "you(a | b) a ";
			choice = Choice.Parse(text, c, false)[0];
			expected = new[] { "a", "b" };
			Assert.That(choice.text, Is.EqualTo("(a | b)"));
			Assert.That(choice.options.Length, Is.EqualTo(expected.Length));
			Assert.That(choice.options, Is.EqualTo(expected));
			//CollectionAssert.Contains(expected, choice.Resolve());

			text = "you (a | b).ToUpper() and ...";
			choice = Choice.Parse(text, c, false)[0];
			expected = new[] { "a", "b" };
			Assert.That(choice.text, Is.EqualTo("(a | b).ToUpper()"));
			Assert.That(choice.options.Length, Is.EqualTo(expected.Length));
			Assert.That(choice.options, Is.EqualTo(expected));
			//Assert.That(choice.Resolve(), Is.SubsetOf(new[] { "A", "B" }));
			////CollectionAssert.Contains(new[] { "A", "B" }, choice.Resolve());

			text = "you (a|b) are";
			choice = Choice.Parse(text, c, false)[0];
			expected = new[] { "a", "b" };
			Assert.That(choice.text, Is.EqualTo("(a|b)"));
			Assert.That(choice.options.Length, Is.EqualTo(expected.Length));
			Assert.That(choice.options, Is.EqualTo(expected));
			//CollectionAssert.Contains(expected, choice.Resolve());

			text = "you (a|b).ToUpper() are";
			choice = Choice.Parse(text, c, false)[0];
			expected = new[] { "a", "b" };
			Assert.That(choice.text, Is.EqualTo("(a|b).ToUpper()"));
			Assert.That(choice.options.Length, Is.EqualTo(expected.Length));
			Assert.That(choice.options, Is.EqualTo(expected));
			Assert.That(choice._TransArray(), Is.EquivalentTo(new[] { "ToUpper" }));
			//CollectionAssert.Contains(new[] { "A", "B" }, choice.Resolve());

			//Resolver.DBUG = true;
			choice = Choice.Parse("you (a|b).ToUpper().articlize() are", c)[0];
			expected = new[] { "a", "b" };
			Assert.That(choice.text, Is.EqualTo("(a|b).ToUpper().articlize()"));
			Assert.That(choice.options.Length, Is.EqualTo(expected.Length));
			Assert.That(choice.options, Is.EqualTo(expected));
			Assert.That(choice._TransArray(), Is.EquivalentTo(new[] { "ToUpper", "articlize" }));
			//CollectionAssert.Contains(new[] { "an A", "a B" }, choice.Resolve());

			choice = Choice.Parse("you (a | b |c). are", c)[0];
			expected = new[] { "a", "b", "c" };
			Assert.That(choice.text, Is.EqualTo("(a | b |c)"));
			Assert.That(choice.options.Length, Is.EqualTo(expected.Length));
			Assert.That(choice.options, Is.EqualTo(expected));
			//CollectionAssert.Contains(expected, choice.Resolve());

			//Resolver.DBUG = true;
			choice = Choice.Parse("you (a | b |c).ToUpper(). are", c)[0];
			expected = new[] { "a", "b", "c" };
			Assert.That(choice.text, Is.EqualTo("(a | b |c).ToUpper()"));
			Assert.That(choice.options.Length, Is.EqualTo(expected.Length));
			Assert.That(choice.options, Is.EqualTo(expected));
			//Assert.That(choice.Resolve(), Is.SubsetOf(new[] { "A", "B", "C" }));

			choice = Choice.Parse("you (a | b |c).ToUpper().articlize(). are", c)[0];
			expected = new[] { "a", "b", "c" };
			Assert.That(choice.text, Is.EqualTo("(a | b |c).ToUpper().articlize()"));
			Assert.That(choice.options.Length, Is.EqualTo(expected.Length));
			Assert.That(choice.options, Is.EqualTo(expected));
			//CollectionAssert.Contains(new[] { "an A", "a B", "a C" }, choice.Resolve());

			choice = Choice.Parse("you [d=(a | b | c)]. The", c)[0];
			expected = new[] { "a", "b", "c" };
			Assert.That(choice.text, Is.EqualTo("[d=(a | b | c)]"));
			Assert.That(choice.options.Length, Is.EqualTo(expected.Length));
			Assert.That(choice.alias, Is.EqualTo("d"));
			Assert.That(choice.options, Is.EqualTo(expected));
			//CollectionAssert.Contains(expected, choice.Resolve());
			//Assert.That(c.scope.ContainsKey("d"), Is.True);
			//c.scope.Remove("d");

			//Resolver.DBUG = true;
			choice = Choice.Parse("you [d=(a | b | c).ToUpper()]. The", c)[0];
			expected = new[] { "a", "b", "c" };
			Assert.That(choice.text, Is.EqualTo("[d=(a | b | c).ToUpper()]"));
			Assert.That(choice.options.Length, Is.EqualTo(expected.Length));
			Assert.That(choice.alias, Is.EqualTo("d"));
			Assert.That(choice.options, Is.EqualTo(expected));
			//CollectionAssert.Contains(new[] { "A", "B", "C" }, choice.Resolve());
			//Assert.That(c.scope.ContainsKey("d"), Is.True);
			//c.scope.Remove("d");

			choice = Choice.Parse("you [d=(a | b | c).ToUpper().articlize()]. The", c)[0];
			expected = new[] { "a", "b", "c" };
			Assert.That(choice.text, Is.EqualTo("[d=(a | b | c).ToUpper().articlize()]"));
			Assert.That(choice.options.Length, Is.EqualTo(expected.Length));
			Assert.That(choice.alias, Is.EqualTo("d"));
			Assert.That(choice.options, Is.EqualTo(expected));
			//CollectionAssert.Contains(new[] { "an A", "a B", "a C" }, choice.Resolve());
			//Assert.That(c.scope.ContainsKey("d"), Is.True);
			//c.scope.Remove("d");

			choice = Choice.Parse("you [d=(a | b | c)].ToUpper(). The", c)[0];
			expected = new[] { "a", "b", "c" };
			Assert.That(choice.text, Is.EqualTo("[d=(a | b | c)].ToUpper()"));
			Assert.That(choice.options.Length, Is.EqualTo(expected.Length));
			Assert.That(choice.options, Is.EqualTo(expected));
			Assert.That(choice.alias, Is.EqualTo("d"));
			//CollectionAssert.Contains(new[] { "A", "B", "C" }, choice.Resolve());
			//Assert.That(c.scope.ContainsKey("d"), Is.True);
			//c.scope.Remove("d");

			choice = Choice.Parse("you [d=(a | b | c)].ToUpper().articlize(). The", c)[0];
			expected = new[] { "a", "b", "c" };
			Assert.That(choice.text, Is.EqualTo("[d=(a | b | c)].ToUpper().articlize()"));
			Assert.That(choice.options.Length, Is.EqualTo(expected.Length));
			Assert.That(choice.options, Is.EqualTo(expected));
			Assert.That(choice.alias, Is.EqualTo("d"));
			//CollectionAssert.Contains(new[] { "an A", "a B", "a C" }, choice.Resolve());
			//Assert.That(c.scope.ContainsKey("d"), Is.True);
			//c.scope.Remove("d");

			choice = Choice.Parse("you [selected=(a | b | c)]. The", c)[0];
			expected = new[] { "a", "b", "c" };
			Assert.That(choice.text, Is.EqualTo("[selected=(a | b | c)]"));
			Assert.That(choice.options.Length, Is.EqualTo(expected.Length));
			Assert.That(choice.alias, Is.EqualTo("selected"));
			Assert.That(choice.options, Is.EqualTo(expected));
			//CollectionAssert.Contains(expected, choice.Resolve());
			//Assert.That(c.scope.ContainsKey("selected"), Is.True);
			//c.scope.Remove("selected");

			//Resolver.DBUG = true;
			choice = Choice.Parse("you [selected=(a | b | c).ToUpper()]. The", c)[0];
			expected = new[] { "a", "b", "c" };
			Assert.That(choice.text, Is.EqualTo("[selected=(a | b | c).ToUpper()]"));
			Assert.That(choice.options.Length, Is.EqualTo(expected.Length));
			Assert.That(choice.alias, Is.EqualTo("selected"));
			Assert.That(choice.options, Is.EqualTo(expected));
			//CollectionAssert.Contains(new[] { "A", "B", "C" }, choice.Resolve());
			//Assert.That(c.scope.ContainsKey("selected"), Is.True);
			//c.scope.Remove("selected");

			choice = Choice.Parse("you [selected=(a | b | c).ToUpper().articlize()]. The", c)[0];
			expected = new[] { "a", "b", "c" };
			Assert.That(choice.text, Is.EqualTo("[selected=(a | b | c).ToUpper().articlize()]"));
			Assert.That(choice.options.Length, Is.EqualTo(expected.Length));
			Assert.That(choice.alias, Is.EqualTo("selected"));
			Assert.That(choice.options, Is.EqualTo(expected));
			//CollectionAssert.Contains(new[] { "an A", "a B", "a C" }, choice.Resolve());
			//Assert.That(c.scope.ContainsKey("selected"), Is.True);
			//c.scope.Remove("selected");
		}

		[Test]
		public void BindWithMissingSymbol()
		{
			var txt = "CHAT c1\nSET a = $object | $object.Call() | honk\nSAY $a";
			ChatRuntime rt = new ChatRuntime();
			rt.ParseText(txt);
			//Resolver.DBUG = true;
			rt.strictMode = false;
			for (int i = 0; i < 5; i++)
			{
				var s = rt.InvokeImmediate(globals);
				//Console.WriteLine(s);
				Assert.That(s.IsOneOf(new[] { "$object", "$object.Call()", "honk" }));
			}
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