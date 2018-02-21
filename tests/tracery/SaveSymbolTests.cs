using NUnit.Framework;
using dialogic.tracery;

namespace Tests
{
    [TestFixture]
    public class SaveSymbolTests
    {
        [Test]
        public void SaveSymbol_NoExpansionSymbol_Saves()
        {
            // Arrange
            var json = "{" +
                       "    'origin': '#[hero:Alfred]story#'," +
                       "    'story': 'His name was #hero#.'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.IsTrue(output == "His name was Alfred.");
        }

        [Test]
        public void SaveSymbol_OneExpansionSymbol_Saves()
        {
            // Arrange
            var json = "{" +
                       "    'origin': '#[hero:#name#]story#'," +
                       "    'name': 'Alfred'," +
                       "    'story': 'His name was #hero#.'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.IsTrue(output == "His name was Alfred.");
        }

        [Test]
        public void SaveSymbol_NoExpansionSymbolWithModifier_Saves()
        {
            // Arrange
            var json = "{" +
                       "    'origin': '#[hero:alfred]story#'," +
                       "    'story': 'His name was #hero.capitalize#.'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.IsTrue(output == "His name was Alfred.");
        }

        [Test]
        public void SaveSymbol_OneLevelDeep_Saves()
        {
            // Arrange
            var json = "{" +
                       "    'origin': '#[setName]name#'," +
                       "	'setName': '[name:Luigi]'," +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.IsTrue(output == "Luigi");
        }

        [Test]
        public void SaveSymbol_TwoLevelsDeep_Saves()
        {
            // Arrange
            var json = "{" +
                       "    'origin': '#[setName]name#'," +
                       "	'setName': '[name:#maleNames#]'," +
                       "	'maleNames': 'Mario'" +
                       "}";

            var grammar = new Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.IsTrue(output == "Mario");
        }
    }
}