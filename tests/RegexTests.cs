using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dialogic
{
    [TestFixture]
    public class RegexTests : GenericTests
    {
        [Test]
        public void MatchGroups()
        {
            Match match;
         
			TestInner(RE.ParseChoices, "you ($a | b | c) a", "$a | b | c");
			TestInner(RE.ParseChoices, "you ($a | $b | c) a", "$a | $b | c");
            
            TestInner(RE.ParseChoices, "you (a | b | c) a", "a | b | c");
            TestInner(RE.ParseChoices, "you (a | (b | c)) a", "b | c");
            TestInner(RE.ParseChoices, "you ((a | b) | c) a", "a | b");
            TestInner(RE.ParseChoices, "you (then | (a | b))", "a | b");
            TestInner(RE.ParseChoices, "you ((a | b) | then) a", "a | b");
            TestInner(RE.ParseChoices, "you ((a | b) | (c | d)) a", "a | b");

            match = RE.ParseChoices.Match("pre [d=(a | b)] post");
            //Util.ShowMatch(match);
            Assert.That(match.Groups[0].Value, Is.EqualTo("[d=(a | b)]"));
            Assert.That(match.Groups[1].Value, Is.EqualTo("d"));
            Assert.That(match.Groups[2].Value, Is.EqualTo("a | b"));
            Assert.That(match.Groups[3].Value, Is.EqualTo(""));

            match = RE.ParseChoices.Match("pre [d=(a | b)].ToUpper() post");
            Assert.That(match.Groups[0].Value, Is.EqualTo("[d=(a | b)].ToUpper()"));
            Assert.That(match.Groups[1].Value, Is.EqualTo("d"));
            Assert.That(match.Groups[2].Value, Is.EqualTo("a | b"));
            Assert.That(match.Groups[3].Value, Is.EqualTo("ToUpper"));
            Assert.That(Choice.ParseTransforms(match.Groups[3]).ToArray(), Is.EqualTo(new[] { "ToUpper" }));

            match = RE.ParseChoices.Match("pre [d=(a | b).ToUpper()] post");
            Assert.That(match.Groups[0].Value, Is.EqualTo("[d=(a | b).ToUpper()]"));
            Assert.That(match.Groups[1].Value, Is.EqualTo("d"));
            Assert.That(match.Groups[2].Value, Is.EqualTo("a | b"));
            Assert.That(match.Groups[3].Value, Is.EqualTo("ToUpper"));
            Assert.That(Choice.ParseTransforms(match.Groups[3]).ToArray(), Is.EqualTo(new[] { "ToUpper" }));

            match = RE.ParseChoices.Match("pre [d=(a | b)].ToUpper().Articlize() post");
            Assert.That(match.Groups[0].Value, Is.EqualTo("[d=(a | b)].ToUpper().Articlize()"));
            Assert.That(match.Groups[1].Value, Is.EqualTo("d"));
            Assert.That(match.Groups[2].Value, Is.EqualTo("a | b"));
            Assert.That(match.Groups[3].Value, Is.EqualTo("Articlize"));
            Assert.That(Choice.ParseTransforms(match.Groups[3]).ToArray(), Is.EqualTo(new[] { "ToUpper", "Articlize" }));

            match = RE.ParseChoices.Match("pre [d=(a | b).ToUpper().Articlize()] post");
            Assert.That(match.Groups[0].Value, Is.EqualTo("[d=(a | b).ToUpper().Articlize()]"));
            Assert.That(match.Groups[1].Value, Is.EqualTo("d"));
            Assert.That(match.Groups[2].Value, Is.EqualTo("a | b"));
            Assert.That(match.Groups[3].Value, Is.EqualTo("Articlize"));
            Assert.That(Choice.ParseTransforms(match.Groups[3]).ToArray(), Is.EqualTo(new[] { "ToUpper", "Articlize" }));
        }

        [Test]
        public void SimpleMatchGroups()
        {
            MatchCollection matches;

            matches = RE.ParseChoices.Matches("(a | b)");
            //Util.ShowMatches(matches);
            Assert.That(matches[0].Groups[0].Value.Trim(), Is.EqualTo("(a | b)"));
            Assert.That(matches[0].Groups[1].Value.Trim(), Is.EqualTo(""));
            Assert.That(matches[0].Groups[2].Value.Trim(), Is.EqualTo("a | b"));

            matches = RE.ParseChoices.Matches("(a|b)");
            Assert.That(matches[0].Groups[0].Value.Trim(), Is.EqualTo("(a|b)"));
            Assert.That(matches[0].Groups[1].Value.Trim(), Is.EqualTo(""));
            Assert.That(matches[0].Groups[2].Value.Trim(), Is.EqualTo("a|b"));

            matches = RE.ParseChoices.Matches("[d=(a|b)]");
            Assert.That(matches[0].Groups[0].Value.Trim(), Is.EqualTo("[d=(a|b)]"));
            Assert.That(matches[0].Groups[1].Value.Trim(), Is.EqualTo("d"));
            Assert.That(matches[0].Groups[2].Value.Trim(), Is.EqualTo("a|b"));

            matches = RE.ParseChoices.Matches("(a | b | c)");
            Assert.That(matches[0].Groups[0].Value.Trim(), Is.EqualTo("(a | b | c)"));
            Assert.That(matches[0].Groups[1].Value.Trim(), Is.EqualTo(""));
            Assert.That(matches[0].Groups[2].Value.Trim(), Is.EqualTo("a | b | c"));

            matches = RE.ParseChoices.Matches("(a | b | c)");
            Assert.That(matches[0].Groups[0].Value.Trim(), Is.EqualTo("(a | b | c)"));
            Assert.That(matches[0].Groups[1].Value.Trim(), Is.EqualTo(""));
            Assert.That(matches[0].Groups[2].Value.Trim(), Is.EqualTo("a | b | c"));

            matches = RE.ParseChoices.Matches("[d=(a | b | c)]");
            Assert.That(matches[0].Groups[0].Value.Trim(), Is.EqualTo("[d=(a | b | c)]"));
            Assert.That(matches[0].Groups[1].Value.Trim(), Is.EqualTo("d"));
            Assert.That(matches[0].Groups[2].Value.Trim(), Is.EqualTo("a | b | c"));
        }


        [Test]
        public void MatchValidText()
        {
            Match match;

            Regex regex = new Regex(RE.TXT);

            Assert.That(regex.Match("a = b {").Value.Trim(), Is.EqualTo("a = b"));
            Assert.That(regex.Match("a = b {a=b}").Value.Trim(), Is.EqualTo("a = b"));
            Assert.That(regex.Match("a = $b").Value.Trim(), Is.EqualTo("a = $b"));
            Assert.That(regex.Match("a = $b{").Value.Trim(), Is.EqualTo("a = $b"));
            Assert.That(regex.Match("a=b{").Value.Trim(), Is.EqualTo("a=b"));
            Assert.That(regex.Match("a=b{a=b}").Value.Trim(), Is.EqualTo("a=b"));
            Assert.That(regex.Match("a=$b").Value.Trim(), Is.EqualTo("a=$b"));
            Assert.That(regex.Match("a=$b{").Value.Trim(), Is.EqualTo("a=$b"));
            Assert.That(regex.Match("$a=b{").Value.Trim(), Is.EqualTo("$a=b"));
            Assert.That(regex.Match("$a=b{a=b}").Value.Trim(), Is.EqualTo("$a=b"));
            Assert.That(regex.Match("$a=$b").Value.Trim(), Is.EqualTo("$a=$b"));
            Assert.That(regex.Match("$a=$b{").Value.Trim(), Is.EqualTo("$a=$b"));

            match = regex.Match("a=$b");
            Assert.That(match.Value.Trim(), Is.EqualTo("a=$b"));
            match = regex.Match("a=$b{");
            Assert.That(match.Value.Trim(), Is.EqualTo("a=$b"));

            match = regex.Match("a=${b}");
            Assert.That(match.Value.Trim(), Is.EqualTo("a=${b}"));
            match = regex.Match("a=${b} {}");
            Assert.That(match.Value.Trim(), Is.EqualTo("a=${b}"));
            match = regex.Match("a=${b.c} {}");
            Assert.That(match.Value.Trim(), Is.EqualTo("a=${b.c}"));
            match = regex.Match("a=${b->c} {");
            Assert.That(match.Value.Trim(), Is.EqualTo("a=${b->c}"));
            match = regex.Match("a=${b->c} #");
            Assert.That(match.Value.Trim(), Is.EqualTo("a=${b->c}"));

            match = regex.Match("$a=$b");
            //Util.ShowMatch(match);
            Assert.That(match.Value.Trim(), Is.EqualTo("$a=$b"));
        }

        [Test]
        public void EnclosedVarSets()
        {
            var cp = new ChatRuntime().Parser();

            string text;
            LineContext lc;

            text = "SET a=$b";
            lc = new LineContext(cp, text);
            Assert.That(lc, Is.Not.Null);
            Assert.That(lc.command, Is.EqualTo("SET"));
            Assert.That(lc.text, Is.EqualTo("a=$b"));
            Assert.That(lc.meta, Is.EqualTo(""));

            text = "SET a=${b}";
            lc = new LineContext(cp, text);
            Assert.That(lc, Is.Not.Null);
            Assert.That(lc.command, Is.EqualTo("SET"));
            Assert.That(lc.text, Is.EqualTo("a=${b}"));
            Assert.That(lc.meta, Is.EqualTo(""));

            text = "SET a=${b} {meta=val}";
            lc = new LineContext(cp, text);
            Assert.That(lc, Is.Not.Null);
            Assert.That(lc.command, Is.EqualTo("SET"));
            Assert.That(lc.text, Is.EqualTo("a=${b}"));
            Assert.That(lc.meta, Is.EqualTo("meta=val"));

            text = "SET a=${b.c} {meta=val}";
            lc = new LineContext(cp, text);
            Assert.That(lc, Is.Not.Null);
            Assert.That(lc.command, Is.EqualTo("SET"));
            Assert.That(lc.text, Is.EqualTo("a=${b.c}"));
            Assert.That(lc.meta, Is.EqualTo("meta=val"));

            text = "SET a=${b->c} {meta=val}";
            lc = new LineContext(cp, text);
            Assert.That(lc, Is.Not.Null);
            Assert.That(lc.command, Is.EqualTo("SET"));
            Assert.That(lc.text, Is.EqualTo("a=${b->c}"));
            Assert.That(lc.meta, Is.EqualTo("meta=val"));

            text = "SET a=${b->c->d} {meta=val}";
            lc = new LineContext(cp, text);
            Assert.That(lc, Is.Not.Null);
            Assert.That(lc.command, Is.EqualTo("SET"));
            Assert.That(lc.text, Is.EqualTo("a=${b->c->d}"));
            Assert.That(lc.meta, Is.EqualTo("meta=val"));

            text = "SET a=${b&c&d} {meta=val}";
            lc = new LineContext(cp, text);
            Assert.That(lc, Is.Not.Null);
            Assert.That(lc.command, Is.EqualTo("SET"));
            Assert.That(lc.text, Is.EqualTo("a=${b&c&d}"));
            Assert.That(lc.meta, Is.EqualTo("meta=val"));
        }

        [Test]
        public void DollarPathVariations()
        {
            string text;
            MatchCollection matches;

			if (1 == 2) // TODO: refactor bounding to use []
			{
				text = "[alias=${name.prop}.Articlize()]";
				matches = RE.ParseSymbols.Matches(text);
				//Util.ShowMatches(matches);

				Assert.That(matches.Count, Is.EqualTo(1));
				Assert.That(matches[0].Groups.Count, Is.EqualTo(8));
				Assert.That(matches[0].Groups[0].Value, Is.EqualTo(text));
				Assert.That(matches[0].Groups[1].Value, Is.EqualTo("["));
				Assert.That(matches[0].Groups[2].Value, Is.EqualTo("alias"));
				Assert.That(matches[0].Groups[3].Value, Is.EqualTo("{"));
				Assert.That(matches[0].Groups[4].Value, Is.EqualTo("name"));
				Assert.That(matches[0].Groups[6].Value, Is.EqualTo("}"));
				Assert.That(matches[0].Groups[7].Value, Is.EqualTo("]"));
				Assert.That(ParseTransforms(matches[0].Groups[5]).ToArray(),
							Is.EquivalentTo(new[] { "prop", "Articlize()" }));
			}
			
            text = "[alias=${name.prop.Articlize()}]";
            matches = RE.ParseSymbols.Matches(text);
            //Util.ShowMatches(matches);

            Assert.That(matches.Count, Is.EqualTo(1));
            Assert.That(matches[0].Groups.Count, Is.EqualTo(8));
            Assert.That(matches[0].Groups[0].Value, Is.EqualTo(text));
            Assert.That(matches[0].Groups[1].Value, Is.EqualTo("["));
            Assert.That(matches[0].Groups[2].Value, Is.EqualTo("alias"));
            Assert.That(matches[0].Groups[3].Value, Is.EqualTo("{"));
            Assert.That(matches[0].Groups[4].Value, Is.EqualTo("name"));
            Assert.That(matches[0].Groups[6].Value, Is.EqualTo("}"));
            Assert.That(matches[0].Groups[7].Value, Is.EqualTo("]"));
            Assert.That(ParseTransforms(matches[0].Groups[5]).ToArray(),
                        Is.EquivalentTo(new[] { "prop", "Articlize()" }));

            text = "[alias=${name.prop.Articlize().Pluralize()}]";
            matches = RE.ParseSymbols.Matches(text);

            Assert.That(matches.Count, Is.EqualTo(1));
            Assert.That(matches[0].Groups.Count, Is.EqualTo(8));
            Assert.That(matches[0].Groups[0].Value, Is.EqualTo(text));
            Assert.That(matches[0].Groups[1].Value, Is.EqualTo("["));
            Assert.That(matches[0].Groups[2].Value, Is.EqualTo("alias"));
            Assert.That(matches[0].Groups[3].Value, Is.EqualTo("{"));
            Assert.That(matches[0].Groups[4].Value, Is.EqualTo("name"));
            Assert.That(matches[0].Groups[6].Value, Is.EqualTo("}"));
            Assert.That(matches[0].Groups[7].Value, Is.EqualTo("]"));
            Assert.That(ParseTransforms(matches[0].Groups[5]).ToArray(),
                        Is.EquivalentTo(new[] { "prop", "Articlize()", "Pluralize()" }));

            text = "${name.prop.Articlize()}";
            matches = RE.ParseSymbols.Matches(text);

            Assert.That(matches.Count, Is.EqualTo(1));
            Assert.That(matches[0].Groups.Count, Is.EqualTo(8));
            Assert.That(matches[0].Groups[0].Value, Is.EqualTo(text));
            Assert.That(matches[0].Groups[1].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[2].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[3].Value, Is.EqualTo("{"));
            Assert.That(matches[0].Groups[4].Value, Is.EqualTo("name"));
            Assert.That(matches[0].Groups[6].Value, Is.EqualTo("}"));
            Assert.That(matches[0].Groups[7].Value, Is.EqualTo(""));
            Assert.That(ParseTransforms(matches[0].Groups[5]).ToArray(),
                        Is.EquivalentTo(new[] { "prop","Articlize()" }));

            text = "${name.prop.Articlize().Pluralize()}";
            matches = RE.ParseSymbols.Matches(text);

            Assert.That(matches.Count, Is.EqualTo(1));
            Assert.That(matches[0].Groups.Count, Is.EqualTo(8));
            Assert.That(matches[0].Groups[0].Value, Is.EqualTo(text));
            Assert.That(matches[0].Groups[1].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[2].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[3].Value, Is.EqualTo("{"));
            Assert.That(matches[0].Groups[4].Value, Is.EqualTo("name"));
            Assert.That(matches[0].Groups[6].Value, Is.EqualTo("}"));
            Assert.That(matches[0].Groups[7].Value, Is.EqualTo(""));
            Assert.That(ParseTransforms(matches[0].Groups[5]).ToArray(),
                        Is.EquivalentTo(new[] { "prop","Articlize()", "Pluralize()" }));

            text = "${name.prop}";
            matches = RE.ParseSymbols.Matches(text);

            Assert.That(matches.Count, Is.EqualTo(1));
            Assert.That(matches[0].Groups.Count, Is.EqualTo(8));
            Assert.That(matches[0].Groups[0].Value, Is.EqualTo(text));
            Assert.That(matches[0].Groups[1].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[2].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[3].Value, Is.EqualTo("{"));
            Assert.That(matches[0].Groups[4].Value, Is.EqualTo("name"));
            Assert.That(matches[0].Groups[6].Value, Is.EqualTo("}"));
            Assert.That(matches[0].Groups[7].Value, Is.EqualTo(""));
            Assert.That(ParseTransforms(matches[0].Groups[5]).ToArray(),
                        Is.EquivalentTo(new[] { "prop" }));

            // ----------------------------------------------------------

            text = "[alias=$name.prop.Articlize()]";
            matches = RE.ParseSymbols.Matches(text);

            Assert.That(matches.Count, Is.EqualTo(1));
            Assert.That(matches[0].Groups.Count, Is.EqualTo(8));
            Assert.That(matches[0].Groups[0].Value, Is.EqualTo(text));
            Assert.That(matches[0].Groups[1].Value, Is.EqualTo("["));
            Assert.That(matches[0].Groups[2].Value, Is.EqualTo("alias"));
            Assert.That(matches[0].Groups[3].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[4].Value, Is.EqualTo("name"));
            Assert.That(matches[0].Groups[6].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[7].Value, Is.EqualTo("]"));
            Assert.That(ParseTransforms(matches[0].Groups[5]).ToArray(),
                        Is.EquivalentTo(new[] { "prop","Articlize()" }));

            text = "[alias=$name.prop.Articlize().Pluralize()]";
            matches = RE.ParseSymbols.Matches(text);

            Assert.That(matches.Count, Is.EqualTo(1));
            Assert.That(matches[0].Groups.Count, Is.EqualTo(8));
            Assert.That(matches[0].Groups[0].Value, Is.EqualTo(text));
            Assert.That(matches[0].Groups[1].Value, Is.EqualTo("["));
            Assert.That(matches[0].Groups[2].Value, Is.EqualTo("alias"));
            Assert.That(matches[0].Groups[3].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[4].Value, Is.EqualTo("name"));
            Assert.That(matches[0].Groups[6].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[7].Value, Is.EqualTo("]"));
            Assert.That(ParseTransforms(matches[0].Groups[5]).ToArray(),
                        Is.EquivalentTo(new[] { "prop","Articlize()", "Pluralize()" }));

            text = "[alias=$name.prop]";
            matches = RE.ParseSymbols.Matches(text);

            Assert.That(matches.Count, Is.EqualTo(1));
            Assert.That(matches[0].Groups.Count, Is.EqualTo(8));
            Assert.That(matches[0].Groups[0].Value, Is.EqualTo(text));
            Assert.That(matches[0].Groups[1].Value, Is.EqualTo("["));
            Assert.That(matches[0].Groups[2].Value, Is.EqualTo("alias"));
            Assert.That(matches[0].Groups[3].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[4].Value, Is.EqualTo("name"));
            Assert.That(matches[0].Groups[6].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[7].Value, Is.EqualTo("]"));
            Assert.That(ParseTransforms(matches[0].Groups[5]).ToArray(),
                        Is.EquivalentTo(new[] { "prop" }));

            text = "$name.prop.Articlize()";
            matches = RE.ParseSymbols.Matches(text);
            //Util.ShowMatches(matches);

            Assert.That(matches.Count, Is.EqualTo(1));
            Assert.That(matches[0].Groups.Count, Is.EqualTo(8));
            Assert.That(matches[0].Groups[0].Value, Is.EqualTo(text));
            Assert.That(matches[0].Groups[1].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[2].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[3].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[4].Value, Is.EqualTo("name"));
            Assert.That(matches[0].Groups[6].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[7].Value, Is.EqualTo(""));
            Assert.That(ParseTransforms(matches[0].Groups[5]).ToArray(),
                        Is.EquivalentTo(new[] { "prop","Articlize()" }));

            text = "$name.prop.Articlize().Pluralize()";
            matches = RE.ParseSymbols.Matches(text);

            Assert.That(matches.Count, Is.EqualTo(1));
            Assert.That(matches[0].Groups.Count, Is.EqualTo(8));
            Assert.That(matches[0].Groups[0].Value, Is.EqualTo(text));
            Assert.That(matches[0].Groups[1].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[2].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[3].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[4].Value, Is.EqualTo("name"));
            Assert.That(matches[0].Groups[6].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[7].Value, Is.EqualTo(""));
            Assert.That(ParseTransforms(matches[0].Groups[5]).ToArray(),
                        Is.EquivalentTo(new[] { "prop","Articlize()", "Pluralize()" }));

            text = "$name.prop";
            matches = RE.ParseSymbols.Matches(text);
            //Util.ShowMatches(matches);

            Assert.That(matches.Count, Is.EqualTo(1));
            Assert.That(matches[0].Groups.Count, Is.EqualTo(8));
            Assert.That(matches[0].Groups[0].Value, Is.EqualTo(text));
            Assert.That(matches[0].Groups[1].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[2].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[3].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[4].Value, Is.EqualTo("name"));
            Assert.That(matches[0].Groups[6].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[7].Value, Is.EqualTo(""));
            Assert.That(ParseTransforms(matches[0].Groups[5]).ToArray(),
                        Is.EquivalentTo(new[] { "prop" }));
        }

        [Test]
        public void DollarVarVariations()
        {
            string text;
            MatchCollection matches;

            text = "[alias=${name.Articlize()}]";
            matches = RE.ParseSymbols.Matches(text);
            Assert.That(matches.Count, Is.EqualTo(1));
            Assert.That(matches[0].Groups.Count, Is.EqualTo(8));
            Assert.That(matches[0].Groups[0].Value, Is.EqualTo(text));
            Assert.That(matches[0].Groups[1].Value, Is.EqualTo("["));
            Assert.That(matches[0].Groups[2].Value, Is.EqualTo("alias"));
            Assert.That(matches[0].Groups[3].Value, Is.EqualTo("{"));
            Assert.That(matches[0].Groups[4].Value, Is.EqualTo("name"));
            Assert.That(matches[0].Groups[6].Value, Is.EqualTo("}"));
            Assert.That(matches[0].Groups[7].Value, Is.EqualTo("]"));
            Assert.That(ParseTransforms(matches[0].Groups[5]).ToArray(),
                        Is.EquivalentTo(new[] { "Articlize()" }));

            text = "[alias=${name.Articlize().Pluralize()}]";
            matches = RE.ParseSymbols.Matches(text);
            //Util.ShowMatches(matches);

            Assert.That(matches.Count, Is.EqualTo(1));
            Assert.That(matches[0].Groups.Count, Is.EqualTo(8));
            Assert.That(matches[0].Groups[0].Value, Is.EqualTo(text));
            Assert.That(matches[0].Groups[1].Value, Is.EqualTo("["));
            Assert.That(matches[0].Groups[2].Value, Is.EqualTo("alias"));
            Assert.That(matches[0].Groups[3].Value, Is.EqualTo("{"));
            Assert.That(matches[0].Groups[4].Value, Is.EqualTo("name"));
            Assert.That(matches[0].Groups[6].Value, Is.EqualTo("}"));
            Assert.That(matches[0].Groups[7].Value, Is.EqualTo("]"));
            Assert.That(ParseTransforms(matches[0].Groups[5]).ToArray(),
                        Is.EquivalentTo(new[] { "Articlize()", "Pluralize()" }));

            text = "${name.Articlize()}";
            matches = RE.ParseSymbols.Matches(text);

            Assert.That(matches.Count, Is.EqualTo(1));
            Assert.That(matches[0].Groups.Count, Is.EqualTo(8));
            Assert.That(matches[0].Groups[0].Value, Is.EqualTo(text));
            Assert.That(matches[0].Groups[1].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[2].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[3].Value, Is.EqualTo("{"));
            Assert.That(matches[0].Groups[4].Value, Is.EqualTo("name"));
            Assert.That(matches[0].Groups[6].Value, Is.EqualTo("}"));
            Assert.That(matches[0].Groups[7].Value, Is.EqualTo(""));
            Assert.That(ParseTransforms(matches[0].Groups[5]).ToArray(),
                        Is.EquivalentTo(new[] { "Articlize()" }));

            text = "${name.Articlize().Pluralize()}";
            matches = RE.ParseSymbols.Matches(text);

            Assert.That(matches.Count, Is.EqualTo(1));
            Assert.That(matches[0].Groups.Count, Is.EqualTo(8));
            Assert.That(matches[0].Groups[0].Value, Is.EqualTo(text));
            Assert.That(matches[0].Groups[1].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[2].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[3].Value, Is.EqualTo("{"));
            Assert.That(matches[0].Groups[4].Value, Is.EqualTo("name"));
            Assert.That(matches[0].Groups[6].Value, Is.EqualTo("}"));
            Assert.That(matches[0].Groups[7].Value, Is.EqualTo(""));
            Assert.That(ParseTransforms(matches[0].Groups[5]).ToArray(),
                        Is.EquivalentTo(new[] { "Articlize()", "Pluralize()"  }));

            text = "${name}";
            matches = RE.ParseSymbols.Matches(text);

            Assert.That(matches.Count, Is.EqualTo(1));
            Assert.That(matches[0].Groups.Count, Is.EqualTo(8));
            Assert.That(matches[0].Groups[0].Value, Is.EqualTo(text));
            Assert.That(matches[0].Groups[1].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[2].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[3].Value, Is.EqualTo("{"));
            Assert.That(matches[0].Groups[4].Value, Is.EqualTo("name"));
            Assert.That(matches[0].Groups[6].Value, Is.EqualTo("}"));
            Assert.That(matches[0].Groups[7].Value, Is.EqualTo(""));
            Assert.That(ParseTransforms(matches[0].Groups[5]), Is.Null);

            // ----------------------------------------------------------

            text = "[alias=$name.Articlize()]";
            matches = RE.ParseSymbols.Matches(text);

            Assert.That(matches.Count, Is.EqualTo(1));
            Assert.That(matches[0].Groups.Count, Is.EqualTo(8));
            Assert.That(matches[0].Groups[0].Value, Is.EqualTo(text));
            Assert.That(matches[0].Groups[1].Value, Is.EqualTo("["));
            Assert.That(matches[0].Groups[2].Value, Is.EqualTo("alias"));
            Assert.That(matches[0].Groups[3].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[4].Value, Is.EqualTo("name"));
            Assert.That(matches[0].Groups[6].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[7].Value, Is.EqualTo("]"));
            Assert.That(ParseTransforms(matches[0].Groups[5]).ToArray(),
                        Is.EquivalentTo(new[] { "Articlize()" }));

            text = "[alias=$name.Articlize().Pluralize()]";
            matches = RE.ParseSymbols.Matches(text);

            Assert.That(matches.Count, Is.EqualTo(1));
            Assert.That(matches[0].Groups.Count, Is.EqualTo(8));
            Assert.That(matches[0].Groups[0].Value, Is.EqualTo(text));
            Assert.That(matches[0].Groups[1].Value, Is.EqualTo("["));
            Assert.That(matches[0].Groups[2].Value, Is.EqualTo("alias"));
            Assert.That(matches[0].Groups[3].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[4].Value, Is.EqualTo("name"));
            Assert.That(matches[0].Groups[6].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[7].Value, Is.EqualTo("]"));
            Assert.That(ParseTransforms(matches[0].Groups[5]).ToArray(),
                        Is.EquivalentTo(new[] { "Articlize()","Pluralize()" }));

            text = "[alias=$name]";
            matches = RE.ParseSymbols.Matches(text);

            Assert.That(matches.Count, Is.EqualTo(1));
            Assert.That(matches[0].Groups.Count, Is.EqualTo(8));
            Assert.That(matches[0].Groups[0].Value, Is.EqualTo(text));
            Assert.That(matches[0].Groups[1].Value, Is.EqualTo("["));
            Assert.That(matches[0].Groups[2].Value, Is.EqualTo("alias"));
            Assert.That(matches[0].Groups[3].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[4].Value, Is.EqualTo("name"));
            Assert.That(matches[0].Groups[6].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[7].Value, Is.EqualTo("]"));
            Assert.That(ParseTransforms(matches[0].Groups[5]), Is.Null);

            text = "$name.Articlize()";
            matches = RE.ParseSymbols.Matches(text);
            //Util.ShowMatches(matches);

            Assert.That(matches.Count, Is.EqualTo(1));
            Assert.That(matches[0].Groups.Count, Is.EqualTo(8));
            Assert.That(matches[0].Groups[0].Value, Is.EqualTo(text));
            Assert.That(matches[0].Groups[1].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[2].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[3].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[4].Value, Is.EqualTo("name"));
            Assert.That(matches[0].Groups[6].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[7].Value, Is.EqualTo(""));
            Assert.That(ParseTransforms(matches[0].Groups[5]).ToArray(),
                Is.EquivalentTo(new[] { "Articlize()" }));

            text = "$name.Articlize().Pluralize()";
            matches = RE.ParseSymbols.Matches(text);

            Assert.That(matches.Count, Is.EqualTo(1));
            Assert.That(matches[0].Groups.Count, Is.EqualTo(8));
            Assert.That(matches[0].Groups[0].Value, Is.EqualTo(text));
            Assert.That(matches[0].Groups[1].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[2].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[3].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[4].Value, Is.EqualTo("name"));
            Assert.That(matches[0].Groups[6].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[7].Value, Is.EqualTo(""));
            Assert.That(ParseTransforms(matches[0].Groups[5]).ToArray(),
                        Is.EquivalentTo(new[] { "Articlize()", "Pluralize()" }));

            text = "$name";
            matches = RE.ParseSymbols.Matches(text);
            //Util.ShowMatches(matches);

            Assert.That(matches.Count, Is.EqualTo(1));
            Assert.That(matches[0].Groups.Count, Is.EqualTo(8));
            Assert.That(matches[0].Groups[0].Value, Is.EqualTo(text));
            Assert.That(matches[0].Groups[1].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[2].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[3].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[4].Value, Is.EqualTo("name"));
            Assert.That(matches[0].Groups[6].Value, Is.EqualTo(""));
            Assert.That(matches[0].Groups[7].Value, Is.EqualTo(""));
            Assert.That(ParseTransforms(matches[0].Groups[5]), Is.Null);
        }

        internal static List<string> ParseTransforms(Group g) // testing-only
        {
            List<string> transforms = null;
            if (!g.Value.IsNullOrEmpty())
            {
                var parts = g.Value.TrimFirst(Ch.SCOPE).Split(Ch.SCOPE);

                if (parts.Length > 0)
                {
                    if (transforms == null) transforms = new List<string>();
                    foreach (var part in parts) transforms.Add(part);
                }
            }
            return transforms;
        }

        [Test]
        public void DollarVarMultiples()
        {
            string text;
            List<string> vars;
            MatchCollection matches;

            int symNameIdx = 4; // Note will break if the regex is changed

            text = "Hello $name, nice to $verb you $chat1";
            matches = RE.ParseSymbols.Matches(text);
            Assert.That(matches.Count, Is.EqualTo(3));

            vars = new List<string>();
            foreach (Match match in matches)
            {
                vars.Add(match.Groups[symNameIdx].Value);
            }
            Assert.That(vars.Count, Is.EqualTo(3));
            Assert.That(vars[0], Is.EqualTo("name"));
            Assert.That(vars[1], Is.EqualTo("verb"));
            Assert.That(vars[2], Is.EqualTo("chat1"));

            text = "Hello $name, nice to $verb you $chat1!";
            matches = RE.ParseSymbols.Matches(text);
            Assert.That(matches.Count, Is.EqualTo(3));
            vars = new List<string>();
            foreach (Match match in matches)
            {
                vars.Add(match.Groups[symNameIdx].Value);
            }
            Assert.That(vars.Count, Is.EqualTo(3));
            Assert.That(vars[0], Is.EqualTo("name"));
            Assert.That(vars[1], Is.EqualTo("verb"));
            Assert.That(vars[2], Is.EqualTo("chat1"));

            text = "Hello $name, nice to $verb you $chat1!";
            matches = RE.ParseSymbols.Matches(text);
            Assert.That(matches.Count, Is.EqualTo(3));
            vars = new List<string>();
            foreach (Match match in matches)
            {
                vars.Add(match.Groups[symNameIdx].Value);
            }
            Assert.That(vars.Count, Is.EqualTo(3));
            Assert.That(vars[0], Is.EqualTo("name"));
            Assert.That(vars[1], Is.EqualTo("verb"));
            Assert.That(vars[2], Is.EqualTo("chat1"));


            text = "Hello $name, nice to $verb you $chat1.";
            matches = RE.ParseSymbols.Matches(text);
            Assert.That(matches.Count, Is.EqualTo(3));
            vars = new List<string>();
            foreach (Match match in matches)
            {
                vars.Add(match.Groups[symNameIdx].Value);
            }
            Assert.That(vars.Count, Is.EqualTo(3));
            Assert.That(vars[0], Is.EqualTo("name"));
            Assert.That(vars[1], Is.EqualTo("verb"));
            Assert.That(vars[2], Is.EqualTo("chat1"));

            text = "Hello $name, nice to $verb you $chat1.time.";
            matches = RE.ParseSymbols.Matches(text);
            Assert.That(matches.Count, Is.EqualTo(3));
            vars = new List<string>();
            foreach (Match match in matches)
            {
                //System.Console.WriteLine((i++) + ") " + match.Groups[symNameIdx].Value);
                vars.Add(match.Groups[symNameIdx].Value);
            }
            Assert.That(vars.Count, Is.EqualTo(3));
            Assert.That(vars[0], Is.EqualTo("name"));
            Assert.That(vars[1], Is.EqualTo("verb"));
            Assert.That(vars[2], Is.EqualTo("chat1"));

            text = "Hello $name $name2 $name";
            matches = RE.ParseSymbols.Matches(text);
            Assert.That(matches.Count, Is.EqualTo(3));
            vars = new List<string>();
            foreach (Match match in matches)
            {
                vars.Add(match.Groups[symNameIdx].Value);
            }
            Assert.That(vars.Count, Is.EqualTo(3));
            Assert.That(vars[0], Is.EqualTo("name"));
            Assert.That(vars[1], Is.EqualTo("name2"));
            Assert.That(vars[2], Is.EqualTo("name"));


            ParseOneVar("$a","a");
            ParseOneVar("$end-phrase","end-phrase");
            ParseOneVar("(a | $end-phrase)","end-phrase");
            ParseOneVar("Want a $animal?","animal");
            ParseOneVar("${a}", "a");
            ParseOneVar("$end-phrase", "end-phrase");
            ParseOneVar("(a | $end-phrase)", "end-phrase");
            ParseOneVar("What an $animal!", "animal");
            ParseOneVar("Want an ${animal}!", "animal");
            ParseOneVar("\"Want an $animal,\" he asked", "animal");
            ParseOneVar("\"Want an $animal\" he asked", "animal");
            ParseOneVar("It was an $animal; he said", "animal");

            ParseOneVar("$a.a()", "a");
            ParseOneVar("$end-phrase.a()", "end-phrase");
            ParseOneVar("(a | $end-phrase.a())", "end-phrase");
            ParseOneVar("Want a $animal.a()?", "animal");
            ParseOneVar("${a.a()}", "a");
            ParseOneVar("$end-phrase.a()", "end-phrase");
            ParseOneVar("(a | $end-phrase.a())", "end-phrase");
            ParseOneVar("What an $animal.a()!", "animal");
            ParseOneVar("Want an ${animal.a()}!", "animal");
            ParseOneVar("\"Want an $animal.a(),\" he asked", "animal");
            ParseOneVar("\"Want an $animal.a()\" he asked", "animal");
            ParseOneVar("It was an $animal.a(); he said", "animal");
        }

        private static void ParseOneVar(string text, string expected, int symNameIdx=4)
        {
            var matches = RE.ParseSymbols.Matches(text);
            Assert.That(matches.Count, Is.EqualTo(1), "FAIL: "+text);
            Assert.That(matches[0].Groups[symNameIdx].Value, Is.EqualTo(expected));
        }

        [Test]
        public void ChatLabels()
        {
            var cp = new ChatRuntime().Parser();

            string ReRe = cp.TypesRegex() + RE.TXT;
            //Console.WriteLine(RE);

            string s;
            var re = new Regex(ReRe);

            s = "CHAT";
            Assert.That(re.Match(s).Groups[1].Value, Is.EqualTo(s));

            s = "CHAT ok";
            var match = re.Match(s);
            var parts = GetParts(match);

            Assert.That(parts[0], Is.EqualTo("CHAT"));
            Assert.That(parts[1], Is.EqualTo("ok"));
            Assert.That(parts[2], Is.EqualTo(""));
            Assert.That(parts[3], Is.EqualTo(""));

            re = new Regex(ReRe);

            s = "CHAT";
            Assert.That(re.Match(s).Groups[1].Value, Is.EqualTo(s));

            s = "CHAT ok";
            match = re.Match(s);
            parts = GetParts(match);

            //Util.ShowMatch(match);
            //for (int i = 0; i < parts.Count; i++) Console.WriteLine(i + ") " + parts[i]);

            Assert.That(parts[0], Is.EqualTo("CHAT"));
            Assert.That(parts[1], Is.EqualTo("ok"));
            Assert.That(parts[2], Is.EqualTo(""));
            Assert.That(parts[3], Is.EqualTo(""));
        }

        [Test]
        public void LabelsOnly()
        {
            string s;
            var re = new Regex(RE.LBL);

            s = "#Hello";
            Assert.That(re.Match(s).Groups[1].Value, Is.EqualTo(s));

            s = "#Hello ";
            Assert.That(re.Match(s).Groups[1].Value, Is.EqualTo(s));

            s = "#(Hello|Goodbye)";
            Assert.That(re.Match(s).Groups[1].Value, Is.EqualTo(s));

            s = "#( Hello | Goodbye )";
            Assert.That(re.Match(s).Groups[1].Value, Is.EqualTo(s));

            s = "#(a|b|c)";
            Assert.That(re.Match(s).Groups[1].Value, Is.EqualTo(s));

            s = "#(a|b|c | d )";
            Assert.That(re.Match(s).Groups[1].Value, Is.EqualTo(s));

            s = "#(a |b |c)";
            Assert.That(re.Match(s).Groups[1].Value, Is.EqualTo(s));

            s = "#( a |b |c )";
            Assert.That(re.Match(s).Groups[1].Value, Is.EqualTo(s));
        }

        [Test]
        public void TestLines()
        {
            var re = new ChatRuntime().Parser().LineParser();

            Match match = re.Match("DO #Hello");
            //Util.ShowMatch(match);
            Assert.That(match.Groups.Count, Is.GreaterThanOrEqualTo(5));

            var parts = GetParts(match);
            Assert.That(parts[1], Is.EqualTo("DO"));
            Assert.That(parts[2], Is.EqualTo(""));
            Assert.That(parts[3], Is.EqualTo("#Hello"));
            Assert.That(parts[4], Is.EqualTo(""));

            //for (int i = 0; i < parts.Count; i++)Console.WriteLine(i+") "+parts[i]);

            match = re.Match("DO #(Hello | Goodbye)");
            Assert.That(match.Groups.Count, Is.GreaterThanOrEqualTo(5));

            parts = GetParts(match);

            Assert.That(parts[1], Is.EqualTo("DO"));
            Assert.That(parts[2], Is.EqualTo(""));
            Assert.That(parts[3], Is.EqualTo("#(Hello | Goodbye)"));
            Assert.That(parts[4], Is.EqualTo(""));
        }

        [Test]
        public void SetParsing()
        {
            string[] tests = {

                "SET $foo=4", "foo", "=", "4",
                "SET $foo= 4", "foo", "=", "4",
                "SET $foo =4", "foo", "=", "4",
                "SET $foo = 4", "foo", "=", "4",
                "SET foo=4", "foo", "=", "4",
                "SET foo= 4", "foo", "=", "4",
                "SET foo =4", "foo", "=", "4",
                "SET foo = 4", "foo", "=", "4",
                "SET $a = 4", "a","=",  "4",
                "SET a+= 4", "a", "+=", "4",
                "SET $a += 4", "a", "+=","4",
                "SET a += 4", "a", "+=","4",
                "SET $a +=4", "a", "+=","4",
                "SET a+= 4 ", "a", "+=", "4",
                "SET $a += 4 ", "a", "+=","4",
                "SET a += 4 ", "a", "+=","4",
                "SET $a +=4  ", "a", "+=","4",

                "SET a= $obj-prop", "a", "=", "$obj-prop",
                "SET a += $obj-prop", "a", "+=","$obj-prop",
                "SET a= $obj-prop", "a", "=", "$obj-prop",
                "SET a+= $obj-prop","a", "+=","$obj-prop",

                "SET $a =(4 | 5)","a", "=","(4 | 5)",
                "SET $a +=(4 | 5)", "a", "+=","(4 | 5)",
                "SET a += (4 | 5)", "a", "+=","(4 | 5)",
                "SET $a += (4 | 5)", "a", "+=","(4 | 5)"
            };

            var SETP = @"\$?([A-Za-z_][^ +=]*)\s*(\+?=)\s*(.+)";
            var re = new Regex(SETP);

            for (int i = 0; i < tests.Length; i += 4)
            {
                //Console.WriteLine(tests[i]);
                var match = re.Match(tests[i].Replace("SET ", ""));
                var name = match.Groups[1].Value.Trim();
                var op = match.Groups[2].Value.Trim();
                var value = match.Groups[3].Value.Trim();
                Assert.That(name, Is.EqualTo(tests[i + 1]));
                Assert.That(op, Is.EqualTo(tests[i + 2]));
                Assert.That(value, Is.EqualTo(tests[i + 3]));
                //Util.ShowMatch(match); break;
            }
        }

        private static List<string> GetParts(Match match)
        {
            var parts = new List<string>();
            for (int j = 1; j < 6; j++) parts.Add(match.Groups[j].Value.Trim());
            return parts;
        }

  		private static void TestInner(Regex re, string full,
            string expected, bool showMatches = false)
        {
            Match match = re.Match(full);
            if (showMatches) Util.ShowMatch(match);
            Assert.That(match.Groups[2].Value, Is.EqualTo(expected));
        }

        //private static void TestInner(Regex re, string full,
        //         string expected, bool showMatches = false)
        //     {
        //var matches = Util.NestedParens(full);
        //Assert.That(matches[0], Is.EqualTo(expected));
        //}
    }
}