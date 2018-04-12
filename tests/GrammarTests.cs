﻿using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Dialogic
{
    [TestFixture]
    class GrammarTests
    {
        public static IDictionary<string, object> globals = new Dictionary<string, object>() {
            { "obj-prop", "dog" },
            { "animal", "dog" },
            { "prep", "then" },
            { "group", "(a|b)" },
            { "cmplx", "($group | $prep)" },
            { "count", 4 }
        };

        [Test]
        public void ExpansionRegexOneMatchNoModifiersOneMatch()
        {
            var matches = Grammar.ExpansionRegex.Matches("<colour>");
            Assert.AreEqual(matches.Count, 1);
            Assert.AreEqual(matches[0].Value, "<colour>");
        }

        [Test]
        public void ExpansionRegexTwoMatchesNoModifiersTwoMatches()
        {
            var matches = Grammar.ExpansionRegex.Matches("<colour> <animal>");
            Assert.AreEqual(matches.Count, 2);
            Assert.AreEqual(matches[0].Value, "<colour>");
            Assert.AreEqual(matches[1].Value, "<animal>");
        }

        [Test]
        public void ExpansionRegexOneMatchOneModifierOneMatch()
        {
            var matches = Grammar.ExpansionRegex.Matches("<animal.capitalize>");
            Assert.AreEqual(matches.Count, 1);
            Assert.AreEqual(matches[0].Value, "<animal.capitalize>");
        }

        [Test]
        public void ExpansionRegexFourMatchesSentenceFourMatches()
        {
            var rule = "The <animal> was <adjective.comma> <adjective.comma> and <adjective>.";
            var matches = Grammar.ExpansionRegex.Matches(rule);
            Assert.AreEqual(matches.Count, 4);
            Assert.AreEqual(matches[0].Value, "<animal>");
            Assert.AreEqual(matches[1].Value, "<adjective.comma>");
            Assert.AreEqual(matches[2].Value, "<adjective.comma>");
            Assert.AreEqual(matches[3].Value, "<adjective>");
        }

        [Test]
        public void ExpansionRegexOneMatchSaveSymbolOneMatch()
        {
            var rule = "<[hero=<name>][heroPet=<animal>]story>";
            var matches = Grammar.ExpansionRegex.Matches(rule);
            Assert.AreEqual(matches.Count, 1);

            // Even though there's sub-expansion symbols, it should only match once around
            // the whole thing.
            Assert.AreEqual(matches[0].Value, "<[hero=<name>][heroPet=<animal>]story>");
        }

        [Test]
        public void YamlTestNew()
        {
            var yaml = "---\norigin: '<sentence>'\nsentence: 'Hello world'";
            var output = new Grammar(yaml).Expand("<origin>");
            Assert.That(output, Is.EqualTo("Hello world"));
        }


        [Test]
        public void YamlTestsHelloWorldHelloWorld()
        {
            var yaml = "---\norigin: '<sentence>'\nsentence: 'Hello world'";
            var output = new Grammar(yaml).Expand("<origin>");
            Assert.That(output, Is.EqualTo("Hello world"));
        }

        [Test]
        public void YamlTestsIncreasedExpansionDepthHelloWorld()
        {
            var yaml = "---\norigin: '<sentence>'\nsentence: '<greeting> ";
            yaml += "<place>'\nplace:\n - 'world'\ngreeting:\n - 'Hello'\n";
            var g = new Grammar(yaml);
            var output = g.Expand("<origin>");
            Assert.That(output, Is.EqualTo("Hello world"));
        }

        [Test]
        public void ExpandHelloWorldSuccess()
        {

            var json = "{" +
                       "    'origin': 'hello world'" +
                       "}";

            var grammar = new Grammar(json);
            var output = grammar.Expand("<origin>");
            Assert.AreEqual(output, "hello world");
        }

        [Test]
        public void ExpandExpandSymbolAnimal()
        {

            var json = "{" +
                       "    'origin': 'hello <animal>'," +
                       "    'animal': 'cat'" +
                       "}";

            var grammar = new Grammar(json);
            var output = grammar.Expand("<origin>");
            Assert.AreEqual(output, "hello cat");
        }

        /* TODO:
        [Test]
        public void SetExpandExpandSymbolAnimal()
        {
            var json = "{" +
                       "    'origin': 'hello <animal>'," +
                       "    'animal': 'cat'" +
                       "}";
            var grammar = new Grammar(json);
            var output = grammar.Expand("<origin>");
            Assert.AreEqual(output, "hello cat");

            string[] lines;
            string text;
            Chat chat;

            lines = new[] {
                "origin = hello <animal>",
                "animal = cat",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Console.WriteLine(chat.AsGrammar(globals, false)+"\n"+globals.Stringify());
            Assert.That(chat.Expand(globals, "$origin"), Is.EqualTo(output));
        }*/

        [Test]
        public void ExpandCapitalizeFirstLetterCapitalized()
        {

            var json = "{" +
                       "    'origin': 'hello <animal.capitalize>'," +
                       "    'animal': 'cat'" +
                       "}";

            var grammar = new Grammar(json);
            var output = grammar.Expand("<origin>");
            Assert.AreEqual(output, "hello Cat");
        }

        [Test]
        public void ExpandCommaHelloCommaWorld()
        {

            var json = "{" +
                       "    'origin': '<greeting.comma> <place>'," +
                       "    'greeting': 'Hello'," +
                       "    'place': 'world'" +
                       "}";

            var grammar = new Grammar(json);
            var output = grammar.Expand("<origin>");
            Assert.AreEqual(output, "Hello, world");
        }

        [Test]
        public void ExpandQuotifyHelloQuoteWorldQuote()
        {
            var json = "{" +
                       "    'origin': '<greeting> <place.quotify>'," +
                       "    'greeting': 'Hello'," +
                       "    'place': 'world'" +
                       "}";

            var grammar = new Grammar(json);
            var output = grammar.Expand("<origin>");
            Assert.AreEqual(output, "Hello \"world\"");
        }

        [Test]
        public void ExpandAACat()
        {
            var json = "{" +
                       "    'origin': 'you are <animal.a>'," +
                       "    'animal': 'cat'" +
                       "}";

            var grammar = new Grammar(json);
            var output = grammar.Expand("<origin>");
            Assert.That(output, Is.EqualTo("you are a cat"));
        }

        [Test]
        public void ExpandAAnElephant()
        {

            var json = "{" +
                       "    'origin': 'you are <animal.a>'," +
                       "    'animal': 'elephant'" +
                       "}";

            var grammar = new Grammar(json);
            var output = grammar.Expand("<origin>");
            Assert.AreEqual(output, "you are an elephant");
        }

        [Test]
        public void ExpandCaptitalizeAACat()
        {

            var json = "{" +
                       "    'origin': 'you are <animal.capitalize.a>'," +
                       "    'animal': 'cat'" +
                       "}";

            var grammar = new Grammar(json);
            var output = grammar.Expand("<origin>");
            Assert.AreEqual(output, "you are a Cat");
        }

        [Test]
        public void ExpandACaptitalizeACat()
        {

            var json = "{" +
                       "    'origin': 'you are <animal.a.capitalize>'," +
                       "    'animal': 'cat'" +
                       "}";

            var grammar = new Grammar(json);
            var output = grammar.Expand("<origin>");
            Assert.AreEqual(output, "you are A cat");
        }

        [Test]
        public void ExpandCaptitalizeAllCuteCatCuteCat()
        {

            var json = "{" +
                       "    'origin': 'you are a <animal.allCaps>'," +
                       "    'animal': 'cute cat'" +
                       "}";

            var grammar = new Grammar(json);
            var output = grammar.Expand("<origin>");
            Assert.AreEqual(output, "you are a Cute Cat");
        }

        [Test]
        public void ExpandPastTensifyBullyBullied()
        {

            var json = "{" +
                       "    'origin': 'you <verb.ed>'," +
                       "    'verb': 'bully'" +
                       "}";

            var grammar = new Grammar(json);
            var output = grammar.Expand("<origin>");
            Assert.AreEqual(output, "you bullied");
        }

        [Test]
        public void ExpandPastTensifyQuackQuacked()
        {

            var json = "{" +
                       "    'origin': 'you <verb.ed>'," +
                       "    'verb': 'quack'" +
                       "}";

            var grammar = new Grammar(json);
            var output = grammar.Expand("<origin>");
            Assert.AreEqual(output, "you quacked");
        }

        [Test]
        public void ExpandPastTensifyCallCalled()
        {

            var json = "{" +
                       "    'origin': 'you <verb.ed>?'," +
                       "    'verb': 'call'" +
                       "}";

            var grammar = new Grammar(json);
            var output = grammar.Expand("<origin>");
            Assert.AreEqual(output, "you called?");
        }

        [Test]
        public void CustomModifiersMakeEverythingHelloWorldHelloWorld()
        {

            var json = "{" +
                "    'origin': '<sentence.helloWorld>'," +
                       "    'sentence': 'this sentence is irrelevant'" +
                       "}";

            Func<string, string> f = delegate (string i)
            {
                return "hello world";
            };

            var grammar = new Grammar(json);
            grammar.AddModifier("helloWorld", f);
            var output = grammar.Expand("<origin>");
            Assert.AreEqual(output, "hello world");
        }

        [Test]
        public void CustomModifiersSlurringSlurring()
        {

            var json = "{" +
                       "    'origin': '<sentence.slur>'," +
                       "    'sentence': 'this is a long sentence ready for slurring'" +
                       "}";

            Func<string, string> f = delegate (string i)
            {
                var o = "";
                var vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };

                foreach (char c in i)
                {
                    o += c;

                    if (Array.IndexOf(vowels, c) > -1)
                    {
                        o += c;
                    }
                }

                return o;
            };

            var grammar = new Grammar(json);
            grammar.AddModifier("slur", f);
            var output = grammar.Expand("<origin>");
            Assert.AreEqual(output, "thiis iis aa loong seenteencee reeaady foor sluurriing");
        }

        [Test]
        public void CustomModifiersToUpperToUpper()
        {

            var json = "{" +
                "    'origin': '<sentence.toUpper>'," +
                       "    'sentence': 'hello cat'" +
                       "}";

            Func<string, string> f = delegate (string i)
            {
                return i.ToUpper();
            };

            var grammar = new Grammar(json);
            grammar.AddModifier("toUpper", f);
            var output = grammar.Expand("<origin>");
            Assert.AreEqual(output, "HELLO CAT");
        }

        [Test]
        public void SaveSymbolNoExpansionSymbolSaves()
        {

            var json = "{" +
                       "    'origin': '<[hero=Alfred]story>'," +
                       "    'story': 'His name was <hero>.'" +
                       "}";

            var grammar = new Grammar(json);
            var output = grammar.Expand("<origin>");
            Assert.IsTrue(output == "His name was Alfred.");
        }

        [Test]
        public void SaveSymbolOneExpansionSymbolSaves()
        {

            var json = "{" +
                "    'origin': '<[hero=<name>]story>'," +
                       "    'name': 'Alfred'," +
                "    'story': 'His name was <hero>.'" +
                       "}";

            var grammar = new Grammar(json);
            var output = grammar.Expand("<origin>");
            Assert.IsTrue(output == "His name was Alfred.");
        }

        [Test]
        public void SaveSymbolNoExpansionSymbolWithModifierSaves()
        {

            var json = "{" +
                        "    'origin': '<[hero=alfred]story>'," +
                        "    'story': 'His name was <hero.capitalize>.'" +
                        "}";
            var grammar = new Grammar(json);
            var output = grammar.Expand("<origin>");
            Assert.IsTrue(output == "His name was Alfred.");
        }

        [Test]
        public void SaveSymbolOneLevelDeepSaves()
        {
            var json = "{" +
                       "    'origin': '<[setName]name>'," +
                        "    'setName': '[name=Luigi]'," +
                       "}";

            var grammar = new Grammar(json);
            var output = grammar.Expand("<origin>");
            Assert.IsTrue(output == "Luigi");
        }

        [Test]
        public void SaveSymbolTwoLevelsDeepSaves()
        {
            var json = "{" +
                "    'origin': '<[setName]name>'," +
                "    'setName': '[name=<maleNames>]'," +
                "    'maleNames': 'Mario'" +
                "}";

            var grammar = new Grammar(json);
            var output = grammar.Expand("<origin>");
            Assert.IsTrue(output == "Mario");
        }
    }
}