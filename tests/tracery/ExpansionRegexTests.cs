using NUnit.Framework;
using dialogic.tracery;

namespace Tests.Unit_Tests
{
    [TestFixture]
    public class ExpansionRegexTests
    {
        [Test]
        public void ExpansionRegex_OneMatchNoModifiers_OneMatch()
        {
            // Arrange
            var rule = "#colour#";

            // Act
            var matches = Grammar.ExpansionRegex.Matches(rule);

            // Assert
            Assert.AreEqual(matches.Count, 1);
            Assert.AreEqual(matches[0].Value, "#colour#");
        }

        [Test]
        public void ExpansionRegex_TwoMatchesNoModifiers_TwoMatches()
        {
            // Arrange
            var rule = "#colour# #animal#";

            // Act
            var matches = Grammar.ExpansionRegex.Matches(rule);

            // Assert
            Assert.AreEqual(matches.Count, 2);
            Assert.AreEqual(matches[0].Value, "#colour#");
            Assert.AreEqual(matches[1].Value, "#animal#");
        }

        [Test]
        public void ExpansionRegex_OneMatchOneModifier_OneMatch()
        {
            // Arrange
            var rule = "#animal.capitalize#";

            // Act
            var matches = Grammar.ExpansionRegex.Matches(rule);

            // Assert
            Assert.AreEqual(matches.Count, 1);
            Assert.AreEqual(matches[0].Value, "#animal.capitalize#");
        }

        [Test]
        public void ExpansionRegex_FourMatchesSentence_FourMatches()
        {
            // Arrange
            var rule = "The #animal# was #adjective.comma# #adjective.comma# and #adjective#.";

            // Act
            var matches = Grammar.ExpansionRegex.Matches(rule);

            // Assert
            Assert.AreEqual(matches.Count, 4);
            Assert.AreEqual(matches[0].Value, "#animal#");
            Assert.AreEqual(matches[1].Value, "#adjective.comma#");
            Assert.AreEqual(matches[2].Value, "#adjective.comma#");
            Assert.AreEqual(matches[3].Value, "#adjective#");
        }

        [Test]
        public void ExpansionRegex_OneMatchSaveSymbol_OneMatch()
        {
            // Arrange
            var rule = "#[hero:#name#][heroPet:#animal#]story#";

            // Act
            var matches = Grammar.ExpansionRegex.Matches(rule);

            // Assert
            Assert.AreEqual(matches.Count, 1);

            // Even though there's sub-expansion symbols, it should only match once around
            // the whole thing.
            Assert.AreEqual(matches[0].Value, "#[hero:#name#][heroPet:#animal#]story#");
        }
    }
}