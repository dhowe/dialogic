using System;
using NUnit.Framework;

namespace dialogic.tracery
{
    [TestFixture]
    public class CustomModifiersTest
    {
        [Test]
        public void CustomModifiers_MakeEverythingHelloWorld_HelloWorld()
        {
            // Arrange
            var json = "{" +
                       "    'origin': '#sentence.helloWorld#'," +
                       "    'sentence': 'this sentence is irrelevant'" +
                       "}";

            Func<string, string> f = delegate (string i)
            {
                return "hello world";
            };

            var grammar = new Grammar(json);
            grammar.AddModifier("helloWorld", f);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "hello world");
        }

        [Test]
        public void CustomModifiers_Slurring_Slurring()
        {
            // Arrange
            var json = "{" +
                       "    'origin': '#sentence.slur#'," +
                       "    'sentence': 'this is a long sentence ready for slurring'" +
                       "}";

            Func<string, string> f = delegate (string i)
            {
                var o = "";
                var vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };

                foreach(char c in i)
                {
                    o += c;

                    if(Array.IndexOf(vowels, c) > -1)
                    {
                        o += c;
                    }
                }

                return o;
            };

            var grammar = new Grammar(json);
            grammar.AddModifier("slur", f);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "thiis iis aa loong seenteencee reeaady foor sluurriing");
        }

        [Test]
        public void CustomModifiers_ToUpper_ToUpper()
        {
            // Arrange
            var json = "{" +
                       "    'origin': '#sentence.toUpper#'," +
                       "    'sentence': 'hello cat'" +
                       "}";

            Func<string, string> f = delegate (string i)
            {
                return i.ToUpper();
            };

            var grammar = new Grammar(json);
            grammar.AddModifier("toUpper", f);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "HELLO CAT");
        }
    }
}