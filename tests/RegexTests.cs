using NUnit.Framework;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dialogic
{
    [TestFixture]
    public class RegexTests
    {
        const bool NO_VALIDATORS = true;

        static IDictionary<string, object> globals
            = new Dictionary<string, object>
        {
            { "obj-prop", "dog" },
            { "animal", "dog" },
            { "prep", "then" },
            { "group", "(a|b)" },
            { "cmplx", "($group | $prep)" },
            { "count", 4 }
        };

        [Test]
        public void MatchValidText()
        {
            Match match;

            Regex regex = new Regex(ChatParser.TXT);

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
            string text;
            LineContext lc;

            text = "SET a=$b";
            lc = new LineContext(text);
            Assert.That(lc, Is.Not.Null);
            Assert.That(lc.command, Is.EqualTo("SET"));
            Assert.That(lc.text, Is.EqualTo("a=$b"));
            Assert.That(lc.meta, Is.EqualTo(""));

            text = "SET a=${b}";
            lc = new LineContext(text);
            Assert.That(lc, Is.Not.Null);
            Assert.That(lc.command, Is.EqualTo("SET"));
            Assert.That(lc.text, Is.EqualTo("a=${b}"));
            Assert.That(lc.meta, Is.EqualTo(""));

            text = "SET a=${b} {meta=val}";
            lc = new LineContext(text);
            Assert.That(lc, Is.Not.Null);
            Assert.That(lc.command, Is.EqualTo("SET"));
            Assert.That(lc.text, Is.EqualTo("a=${b}"));
            Assert.That(lc.meta, Is.EqualTo("meta=val"));

            text = "SET a=${b.c} {meta=val}";
            lc = new LineContext(text);
            Assert.That(lc, Is.Not.Null);
            Assert.That(lc.command, Is.EqualTo("SET"));
            Assert.That(lc.text, Is.EqualTo("a=${b.c}"));
            Assert.That(lc.meta, Is.EqualTo("meta=val"));

            text = "SET a=${b->c} {meta=val}";
            lc = new LineContext(text);
            Assert.That(lc, Is.Not.Null);
            Assert.That(lc.command, Is.EqualTo("SET"));
            Assert.That(lc.text, Is.EqualTo("a=${b->c}"));
            Assert.That(lc.meta, Is.EqualTo("meta=val"));

            text = "SET a=${b->c->d} {meta=val}";
            lc = new LineContext(text);
            Assert.That(lc, Is.Not.Null);
            Assert.That(lc.command, Is.EqualTo("SET"));
            Assert.That(lc.text, Is.EqualTo("a=${b->c->d}"));
            Assert.That(lc.meta, Is.EqualTo("meta=val"));

            text = "SET a=${b&c&d} {meta=val}";
            lc = new LineContext(text);
            Assert.That(lc, Is.Not.Null);
            Assert.That(lc.command, Is.EqualTo("SET"));
            Assert.That(lc.text, Is.EqualTo("a=${b&c&d}"));
            Assert.That(lc.meta, Is.EqualTo("meta=val"));
        }

        [Test]
        public void DollarVars()
        {
            string text;
            List<string> vars;
            MatchCollection matches;

            // Note: this test will break if the regex is changed

            text = "Hello $name, nice to $verb you $chat1";
            matches = RE.ParseVars.Matches(text);
            Assert.That(matches.Count, Is.EqualTo(3));
            vars = new List<string>();
            foreach (Match match in matches)
            {
                vars.Add(match.Groups[3].Value);
            }
            Assert.That(vars.Count, Is.EqualTo(3));
            Assert.That(vars[0], Is.EqualTo("name"));
            Assert.That(vars[1], Is.EqualTo("verb"));
            Assert.That(vars[2], Is.EqualTo("chat1"));

            text = "Hello $name, nice to $verb you $chat1!";
            matches = RE.ParseVars.Matches(text);
            Assert.That(matches.Count, Is.EqualTo(3));
            vars = new List<string>();
            foreach (Match match in matches)
            {
                vars.Add(match.Groups[3].Value);
            }
            Assert.That(vars.Count, Is.EqualTo(3));
            Assert.That(vars[0], Is.EqualTo("name"));
            Assert.That(vars[1], Is.EqualTo("verb"));
            Assert.That(vars[2], Is.EqualTo("chat1"));

            text = "Hello $name, nice to $verb you $chat1!";
            matches = RE.ParseVars.Matches(text);
            Assert.That(matches.Count, Is.EqualTo(3));
            vars = new List<string>();
            foreach (Match match in matches)
            {
                vars.Add(match.Groups[3].Value);
            }
            Assert.That(vars.Count, Is.EqualTo(3));
            Assert.That(vars[0], Is.EqualTo("name"));
            Assert.That(vars[1], Is.EqualTo("verb"));
            Assert.That(vars[2], Is.EqualTo("chat1"));


            text = "Hello $name, nice to $verb you $chat1.";
            matches = RE.ParseVars.Matches(text);
            Assert.That(matches.Count, Is.EqualTo(3));
            vars = new List<string>();
            foreach (Match match in matches)
            {
                vars.Add(match.Groups[3].Value);
            }
            Assert.That(vars.Count, Is.EqualTo(3));
            Assert.That(vars[0], Is.EqualTo("name"));
            Assert.That(vars[1], Is.EqualTo("verb"));
            Assert.That(vars[2], Is.EqualTo("chat1"));

            text = "Hello $name, nice to $verb you $chat1.time.";
            matches = RE.ParseVars.Matches(text);
            Assert.That(matches.Count, Is.EqualTo(3));
            vars = new List<string>();
            foreach (Match match in matches)
            {
                vars.Add(match.Groups[3].Value);
            }
            Assert.That(vars.Count, Is.EqualTo(3));
            Assert.That(vars[0], Is.EqualTo("name"));
            Assert.That(vars[1], Is.EqualTo("verb"));
            Assert.That(vars[2], Is.EqualTo("chat1.time"));


            text = "Hello $name $name2 $name";
            matches = RE.ParseVars.Matches(text);
            Assert.That(matches.Count, Is.EqualTo(3));
            vars = new List<string>();
            foreach (Match match in matches)
            {
                vars.Add(match.Groups[3].Value);
            }
            Assert.That(vars.Count, Is.EqualTo(3));
            Assert.That(vars[0], Is.EqualTo("name"));
            Assert.That(vars[1], Is.EqualTo("name2"));
            Assert.That(vars[2], Is.EqualTo("name"));


            ParseOneVar("$a","a");
            ParseOneVar("$end-phrase","end-phrase");
            ParseOneVar("(a | $end-phrase)","end-phrase");
            ParseOneVar("Want a $animal?","animal");
            ParseOneVar("$a", "a");
            ParseOneVar("$end-phrase", "end-phrase");
            ParseOneVar("(a | $end-phrase)", "end-phrase");
            ParseOneVar("What an $animal!", "animal");
            ParseOneVar("Want an $animal!", "animal");
            ParseOneVar("\"Want an $animal,\" he asked", "animal");
            ParseOneVar("\"Want an $animal\" he asked", "animal");
            ParseOneVar("It was an $animal; he said", "animal");
        }

        private static void ParseOneVar(string text, string expected)
        {
            var matches = RE.ParseVars.Matches(text);
            //Assert.That(matches.Count, Is.EqualTo(1));
            var vars = new List<string>();
            foreach (Match match in matches)
            {
                //if (match.Groups.Count != 2)throw new DialogicException("Bad RE in " + text);
                vars.Add(match.Groups[3].Value);
            }
            //vars.ForEach(Console.WriteLine);
            //Util.ShowMatches(matches);
            Assert.That(vars.Count, Is.EqualTo(1), "FAIL: "+text);
            Assert.That(vars[0], Is.EqualTo(expected));
        }

        [Test]
        public void ChatLabels()
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
            var re = ChatParser.LineParser();
            Match match = re.Match("DO #Hello");
            //Util.ShowMatch(match);
            Assert.That(match.Groups.Count, Is.GreaterThanOrEqualTo(5));

            var parts = GetParts(match);
            Assert.That(parts[1], Is.EqualTo("DO"));
            Assert.That(parts[2], Is.EqualTo(""));
            Assert.That(parts[3], Is.EqualTo("#Hello"));
            Assert.That(parts[4], Is.EqualTo(""));

            //for (int i = 0; i < parts.Count; i++)Console.WriteLine(i+") "+parts[i]);

            re = ChatParser.LineParser();
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
    }
}