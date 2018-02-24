using System.Text;
using dialogic.tracery;
using NUnit.Framework;

namespace dialogic
{
    [TestFixture]
    class YamlTests
    {
        [Test]
        public void YamlTests_HelloWorld_HelloWorld()
        {
            // Arrange
            var yaml = new StringBuilder();
            yaml.AppendLine("---");
            yaml.AppendLine("origin: '#sentence#'");
            yaml.AppendLine("sentence: 'Hello world'");

            // Act
            var grammar = new Grammar(yaml.ToString());

            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "Hello world");
        }

        [Test]
        public void YamlTests_IncreasedExpansionDepth_HelloWorld()
        {
            // Arrange
            var yaml = new StringBuilder();
            yaml.AppendLine("---");
            yaml.AppendLine("origin: '#sentence#'");
            yaml.AppendLine("sentence: '#greeting# #place#'");
            yaml.AppendLine("place:");
            yaml.AppendLine("  - 'world'");
            yaml.AppendLine("greeting:");
            yaml.AppendLine("  - 'Hello'");

            // Act
            var grammar = new Grammar(yaml.ToString());

            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "Hello world");
        }
    }
}
