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
        public void CommandAndText()
        {
            string RE = ChatParser.TypesRegex() + ChatParser.TXT;
            //Console.WriteLine(RE);

            string s;
            var re = new Regex(RE);

            s = "CHAT";
            Assert.That(re.Match(s).Groups[1].Value, Is.EqualTo(s));

            s = "CHAT ok";
            var match = re.Match(s);
            var parts = GetParts(match);

            Assert.That(parts[0], Is.EqualTo("CHAT"));
            Assert.That(parts[1], Is.EqualTo("ok"));
            Assert.That(parts[2], Is.EqualTo(""));
            Assert.That(parts[3], Is.EqualTo(""));

            re = new Regex(RE);

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
        public void LabelOnly()
        {
            string s;
            var re = new Regex(ChatParser.DLBL);

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
            var re = new ChatParser(null).lineParser;
            Match match = re.Match("DO #Hello");
            //Util.ShowMatch(match);
            Assert.That(match.Groups.Count, Is.GreaterThanOrEqualTo(5));

            var parts = GetParts(match);
            Assert.That(parts[1], Is.EqualTo("DO"));
            Assert.That(parts[2], Is.EqualTo(""));
            Assert.That(parts[3], Is.EqualTo("#Hello"));
            Assert.That(parts[4], Is.EqualTo(""));

            //for (int i = 0; i < parts.Count; i++)Console.WriteLine(i+") "+parts[i]);

            re = new ChatParser(null).lineParser;
            match = re.Match("DO #(Hello | Goodbye)");
            Assert.That(match.Groups.Count, Is.GreaterThanOrEqualTo(5));

            parts = GetParts(match);

            Assert.That(parts[1], Is.EqualTo("DO"));
            Assert.That(parts[2], Is.EqualTo(""));
            Assert.That(parts[3], Is.EqualTo("#(Hello | Goodbye)"));
            Assert.That(parts[4], Is.EqualTo(""));
        }

        private static List<string> GetParts(Match match)
        {
            var parts = new List<string>();
            for (int j = 1; j < 6; j++) parts.Add(match.Groups[j].Value.Trim());
            return parts;
        }
    }
}