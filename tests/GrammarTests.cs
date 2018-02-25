using System;
using NUnit.Framework;

namespace dialogic
{
    [TestFixture]
    class GrammarTests
    {
        [Test]
        public void ExpansionRegex_OneMatchNoModifiers_OneMatch()
        {
            // Arrange
            var rule = "<colour>";

            // Act
            var matches = Grammar.ExpansionRegex.Matches(rule);

            // Assert
            Assert.AreEqual(matches.Count, 1);
            Assert.AreEqual(matches[0].Value, "<colour>");
        }

        [Test]
        public void ExpansionRegex_TwoMatchesNoModifiers_TwoMatches()
        {
            // Arrange
            var rule = "<colour> <animal>";

            // Act
            var matches = Grammar.ExpansionRegex.Matches(rule);

            // Assert
            Assert.AreEqual(matches.Count, 2);
            Assert.AreEqual(matches[0].Value, "<colour>");
            Assert.AreEqual(matches[1].Value, "<animal>");
        }

        [Test]
        public void ExpansionRegex_OneMatchOneModifier_OneMatch()
        {
            // Arrange
            var rule = "<animal.capitalize>";

            // Act
            var matches = Grammar.ExpansionRegex.Matches(rule);

            // Assert
            Assert.AreEqual(matches.Count, 1);
            Assert.AreEqual(matches[0].Value, "<animal.capitalize>");
        }

        [Test]
        public void ExpansionRegex_FourMatchesSentence_FourMatches()
        {
            // Arrange
            var rule = "The <animal> was <adjective.comma> <adjective.comma> and <adjective>.";

            // Act
            var matches = Grammar.ExpansionRegex.Matches(rule);

            // Assert
            Assert.AreEqual(matches.Count, 4);
            Assert.AreEqual(matches[0].Value, "<animal>");
            Assert.AreEqual(matches[1].Value, "<adjective.comma>");
            Assert.AreEqual(matches[2].Value, "<adjective.comma>");
            Assert.AreEqual(matches[3].Value, "<adjective>");
        }

        [Test]
        public void ExpansionRegex_OneMatchSaveSymbol_OneMatch()
        {
            // Arrange
            var rule = "<[hero=<name>][heroPet=<animal>]story>";

            // Act
            var matches = Grammar.ExpansionRegex.Matches(rule);

            // Assert
            Assert.AreEqual(matches.Count, 1);

            // Even though there's sub-expansion symbols, it should only match once around
            // the whole thing.
            Assert.AreEqual(matches[0].Value, "<[hero=<name>][heroPet=<animal>]story>");
        }

        [Test]
        public void YamlTestNew()
        {
            var yaml = "---\norigin: '<sentence>'\nsentence: 'Hello world'";
            var output = new Grammar(yaml).Flatten("<origin>");
            System.Console.WriteLine(output);
            Assert.That(output, Is.EqualTo("Hello world"));
        }


        [Test]
        public void YamlTests_HelloWorld_HelloWorld()
        {
            var yaml = "---\norigin: '<sentence>'\nsentence: 'Hello world'";
            var output = new Grammar(yaml).Flatten("<origin>");
            System.Console.WriteLine(output);
            Assert.That(output, Is.EqualTo("Hello world"));
        }

        [Test]
        public void YamlTests_IncreasedExpansionDepth_HelloWorld()
        {
            // Arrange
            var yaml = "---\norigin: '<sentence>'\nsentence: '<greeting> ";
            yaml += "<place>'\nplace:\n - 'world'\ngreeting:\n - 'Hello'\n";
            var g = new Grammar(yaml);
            var output = g.Flatten("<origin>");
            Assert.That(output, Is.EqualTo("Hello world"));
        }

        [Test]
        public void Flatten_HelloWorld_Success()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'hello world'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("<origin>");

            // Assert
            Assert.AreEqual(output, "hello world");
        }

        [Test]
        public void Flatten_ExpandSymbol_Animal()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'hello <animal>'," +
                       "    'animal': 'cat'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("<origin>");

            // Assert
            Assert.AreEqual(output, "hello cat");
        }

        [Test]
        public void Flatten_Capitalize_FirstLetterCapitalized()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'hello <animal.capitalize>'," +
                       "    'animal': 'cat'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("<origin>");

            // Assert
            Assert.AreEqual(output, "hello Cat");
        }

        [Test]
        public void Flatten_BeeSpeak_Beezz()
        {
            // Arrange
            var json = "{" +
                       "    'origin': '<animal.beeSpeak> are very important'," +
                       "    'animal': 'bees'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("<origin>");

            // Assert
            Assert.AreEqual(output, "beezzz are very important");
        }

        [Test]
        public void Flatten_Comma_HelloCommaWorld()
        {
            // Arrange
            var json = "{" +
                       "    'origin': '<greeting.comma> <place>'," +
                       "    'greeting': 'Hello'," +
                       "    'place': 'world'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("<origin>");

            // Assert
            Assert.AreEqual(output, "Hello, world");
        }

        [Test]
        public void Flatten_InQuotes_HelloQuoteWorldQuote()
        {
            // Arrange
            var json = "{" +
                       "    'origin': '<greeting> <place.inQuotes>'," +
                       "    'greeting': 'Hello'," +
                       "    'place': 'world'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("<origin>");

            // Assert
            Assert.AreEqual(output, "Hello \"world\"");
        }

        [Test]
        public void Flatten_A_ACat()
        {
            var json = "{" +
                       "    'origin': 'you are <animal.a>'," +
                       "    'animal': 'cat'" +
                       "}";

            var grammar = new Grammar(json);

            var output = grammar.Flatten("<origin>");

            Assert.That(output, Is.EqualTo("you are a cat"));
        }

        [Test]
        public void Flatten_A_AnElephant()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'you are <animal.a>'," +
                       "    'animal': 'elephant'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("<origin>");

            // Assert
            Assert.AreEqual(output, "you are an elephant");
        }

        [Test]
        public void Flatten_CaptitalizeA_ACat()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'you are <animal.capitalize.a>'," +
                       "    'animal': 'cat'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("<origin>");

            // Assert
            Assert.AreEqual(output, "you are a Cat");
        }

        [Test]
        public void Flatten_ACaptitalize_ACat()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'you are <animal.a.capitalize>'," +
                       "    'animal': 'cat'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("<origin>");

            // Assert
            Assert.AreEqual(output, "you are A cat");
        }

        [Test]
        public void Flatten_CaptitalizeAllCuteCat_CuteCat()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'you are a <animal.capitalizeAll>'," +
                       "    'animal': 'cute cat'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("<origin>");

            // Assert
            Assert.AreEqual(output, "you are a Cute Cat");
        }

        [Test]
        public void Flatten_PastTensifyBully_Bullied()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'you <verb.ed>'," +
                       "    'verb': 'bully'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("<origin>");

            // Assert
            Assert.AreEqual(output, "you bullied");
        }

        [Test]
        public void Flatten_PastTensifyQuack_Quacked()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'you <verb.ed>'," +
                       "    'verb': 'quack'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("<origin>");

            // Assert
            Assert.AreEqual(output, "you quacked");
        }

        [Test]
        public void Flatten_PastTensifyCall_Called()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'you <verb.ed>?'," +
                       "    'verb': 'call'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("<origin>");

            // Assert
            Assert.AreEqual(output, "you called?");
        }

        [Test]
        public void CustomModifiers_MakeEverythingHelloWorld_HelloWorld()
        {
            // Arrange
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

            // Act
            var output = grammar.Flatten("<origin>");

            // Assert
            Assert.AreEqual(output, "hello world");
        }

        [Test]
        public void CustomModifiers_Slurring_Slurring()
        {
            // Arrange
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

            // Act
            var output = grammar.Flatten("<origin>");

            // Assert
            Assert.AreEqual(output, "thiis iis aa loong seenteencee reeaady foor sluurriing");
        }

        [Test]
        public void CustomModifiers_ToUpper_ToUpper()
        {
            // Arrange
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

            // Act
            var output = grammar.Flatten("<origin>");

            // Assert
            Assert.AreEqual(output, "HELLO CAT");
        }

        [Test]
        public void SaveSymbol_NoExpansionSymbol_Saves()
        {
            // Arrange
            var json = "{" +
                       "    'origin': '<[hero=Alfred]story>'," +
                       "    'story': 'His name was <hero>.'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("<origin>");

            // Assert
            Assert.IsTrue(output == "His name was Alfred.");
        }

        [Test]
        public void SaveSymbol_OneExpansionSymbol_Saves()
        {
            // Arrange
            var json = "{" +
                "    'origin': '<[hero=<name>]story>'," +
                       "    'name': 'Alfred'," +
                "    'story': 'His name was <hero>.'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("<origin>");

            // Assert
            Assert.IsTrue(output == "His name was Alfred.");
        }

        [Test]
        public void SaveSymbol_NoExpansionSymbolWithModifier_Saves()
        {
            // Arrange
            var json = "{" +
                "    'origin': '<[hero=alfred]story>'," +
                "    'story': 'His name was <hero.capitalize>.'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("<origin>");

            // Assert
            Assert.IsTrue(output == "His name was Alfred.");
        }

        [Test]
        public void SaveSymbol_OneLevelDeep_Saves()
        {
            // Arrange
            var json = "{" +
                       "    'origin': '<[setName]name>'," +
                "    'setName': '[name=Luigi]'," +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("<origin>");

            // Assert
            Assert.IsTrue(output == "Luigi");
        }

        [Test]
        public void SaveSymbol_TwoLevelsDeep_Saves()
        {
            // Arrange
            var json = "{" +
                "    'origin': '<[setName]name>'," +
                "    'setName': '[name=<maleNames>]'," +
                "    'maleNames': 'Mario'" +
                "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("<origin>");

            // Assert
            Assert.IsTrue(output == "Mario");
        }
    }
}