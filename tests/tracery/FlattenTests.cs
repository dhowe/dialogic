using System.Text.RegularExpressions;
using NUnit.Framework;

namespace dialogic.tracery
{
    [TestFixture]
    class YamlTests
    {

        [Test]
        public void TestNewExp()
        {
            // @"(?<!\[|:)(?!\])#.+?(?<!\[|:)#(?!\])");
            Grammar.ExpansionRegex = new Regex(@"(?<!\[|:)(?!\])#[^ ]+?(?<!\[|:)\b(?!\])");
            //Grammar.ExpansionRegex = new Regex(@"(?<!\[|:)(?!\])#\w+");
            var yaml = "---\norigin: '#sentence!'\nsentence: 'Hello world'";
            var em = Grammar.ExpansionRegex.Matches("#origin");
            System.Console.WriteLine("GOT "+em.Count);
            Assert.That(em.Count,Is.GreaterThan(0));
            var output = new Grammar(yaml).Flatten("#origin");
            System.Console.WriteLine(output);
            Assert.AreEqual(output, "Hello world!");
            Grammar.ExpansionRegex = new Regex(@"(?<!\[|:)(?!\])#.+?(?<!\[|:)#(?!\])");
        }

        [Test]
        public void YamlTests_HelloWorld_HelloWorld()
        {
            var yaml = "---\norigin: '#sentence#'\nsentence: 'Hello world'";
            var output = new Grammar(yaml).Flatten("#origin#");
            System.Console.WriteLine(output);
            Assert.AreEqual(output, "Hello world");
        }

        [Test]
        public void YamlTests_IncreasedExpansionDepth_HelloWorld()
        {
            // Arrange
            var yaml = "---\norigin: '#sentence#'\nsentence: '#greeting# ";
            yaml += "#place#'\nplace:\n - 'world'\ngreeting:\n - 'Hello'\n";
            var output = new Grammar(yaml.ToString()).Flatten("#origin#");
            Assert.AreEqual(output, "Hello world");
        }
    }

    [TestFixture]
    public class FlattenTests
    {
        [Test]
        public void Flatten_HelloWorld_Success()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'hello world'" +
                       "}";

            var grammar = new Grammar(json);
            
            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "hello world");
        }

        [Test]
        public void Flatten_ExpandSymbol_Animal()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'hello #animal#'," +
                       "    'animal': 'cat'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "hello cat");
        }

        [Test]
        public void Flatten_Capitalize_FirstLetterCapitalized()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'hello #animal.capitalize#'," +
                       "    'animal': 'cat'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "hello Cat");
        }

        [Test]
        public void Flatten_BeeSpeak_Beezz()
        {
            // Arrange
            var json = "{" +
                       "    'origin': '#animal.beeSpeak# are very important'," +
                       "    'animal': 'bees'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "beezzz are very important");
        }

        [Test]
        public void Flatten_Comma_HelloCommaWorld()
        {
            // Arrange
            var json = "{" +
                       "    'origin': '#greeting.comma# #place#'," +
                       "    'greeting': 'Hello'," +
                       "    'place': 'world'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "Hello, world");
        }

        [Test]
        public void Flatten_InQuotes_HelloQuoteWorldQuote()
        {
            // Arrange
            var json = "{" +
                       "    'origin': '#greeting# #place.inQuotes#'," +
                       "    'greeting': 'Hello'," +
                       "    'place': 'world'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "Hello \"world\"");
        }
        
        [Test]
        public void Flatten_A_ACat()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'you are #animal.a#'," +
                       "    'animal': 'cat'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "you are a cat");
        }

        [Test]
        public void Flatten_A_AnElephant()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'you are #animal.a#'," +
                       "    'animal': 'elephant'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "you are an elephant");
        }

        [Test]
        public void Flatten_CaptitalizeA_ACat()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'you are #animal.capitalize.a#'," +
                       "    'animal': 'cat'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "you are a Cat");
        }

        [Test]
        public void Flatten_ACaptitalize_ACat()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'you are #animal.a.capitalize#'," +
                       "    'animal': 'cat'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "you are A cat");
        }

        [Test]
        public void Flatten_CaptitalizeAllCuteCat_CuteCat()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'you are a #animal.capitalizeAll#'," +
                       "    'animal': 'cute cat'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "you are a Cute Cat");
        }

        [Test]
        public void Flatten_PastTensifyBully_Bullied()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'you #verb.ed#'," +
                       "    'verb': 'bully'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "you bullied");
        }

        [Test]
        public void Flatten_PastTensifyQuack_Quacked()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'you #verb.ed#'," +
                       "    'verb': 'quack'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "you quacked");
        }

        [Test]
        public void Flatten_PastTensifyCall_Called()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'you #verb.ed#?'," +
                       "    'verb': 'call'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "you called?");
        }
    }
}