using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dialogic
{
    [TestFixture]
    public class RegexTests
    {
        [Test]
        public void TestLines()
        {
            var re = new ChatParser(null).lineParser;
            Match match = re.Match("DO #Hello");
            //Util.ShowMatch(match);
            Assert.That(match.Groups.Count, Is.GreaterThanOrEqualTo(5));

            var parts = GetParts(match);
            Assert.That(parts[1], Is.EqualTo("DO"));
            Assert.That(parts[2], Is.EqualTo(""));
            Assert.That(parts[3], Is.EqualTo("#Hello"));
            Assert.That(parts[4], Is.EqualTo(""));

            for (int i = 1; i < parts.Count; i++)
                Console.WriteLine(i+") "+parts[i]);
        }

        private List<string> GetParts(Match match)
        {
            var parts = new List<string>();
            for (int j = 1; j < 6; j++)
            {
                parts.Add(match.Groups[j].Value.Trim());
            }
            return parts;
        }

        [Test]
        public void TestLabels()
        {
            var re = new Regex(ChatParser.LBL);

            Match match = re.Match("#Hello");
            Util.ShowMatch(match);
        }
    }
}