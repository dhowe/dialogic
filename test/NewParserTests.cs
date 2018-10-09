using System;
using System.Collections.Generic;
using Client;
using NUnit.Framework;
using Parser;
using System.Linq;
using Superpower.Model;

namespace Dialogic.Test
{
    // NEXT: 
    //       Create Commands
    //       Then revisit Groups
    //       Then fix comma issue in FailingTests
    //       Deal with custom/default commands

    [TestFixture]
    public class NewParserTests : GenericTests
    {
        //[Test]
        public void FailingTests()
        {
            Out(DiaTokenizer.Instance.Tokenize("Hello:"));
            Assert.That(DiaParser.ParseFree("Hello:"), Is.EqualTo("Hello:"));

            Out(DiaTokenizer.Instance.Tokenize("(a | #b)"));
            Assert.That(DiaParser.ParseGroup("(a | #b)"), Is.EqualTo("a | #b"));

            Assert.That(DiaParser.ParseGroup("(a | b | c #dd)"), Is.EqualTo("a | b | c #dd"));

            Assert.That(DiaParser.ParseGroup("((a | b) | $c)"), Is.EqualTo("(a | b) | $c"));
            Assert.That(DiaParser.ParseGroup("(a | b.c())"), Is.EqualTo("a | b.c()"));

            Assert.That(DiaParser.ParseGroup("((a | b) | (c | d))"), Is.EqualTo("(a | b) | (c | d)"));
            Assert.That(DiaParser.ParseGroup("(((a | b) | c) | d))"), Is.EqualTo("((a | b) | c) | d"));


            // ParseLine
            var text = "Dave: SAY My $List: 1,2,3 {a=b,c=d}";
            var tokens = DiaTokenizer.Instance.Tokenize(text);
            var dline = DiaParser.Parse(tokens).ElementAt(0);
            Assert.That(dline.command, Is.EqualTo("SAY"));
            Assert.That(dline.text, Is.EqualTo("My $List: 1,2,3"));
            Assert.That(dline.actor, Is.EqualTo("Dave:"));
            Assert.That(dline.meta, Is.EqualTo(new Dictionary<string, string>(){
                {"a", "b"}, {"c", "d"}
            }));
        }

        [Test]
        public void SuperSimpleTest()
        {
            var text = "Hello";
            var tokens = DiaTokenizer.Instance.Tokenize(text);
            var result = DiaParser.ParseFree(tokens);
            Assert.That(result, Is.EqualTo("Hello"));

            text = "Hello,";
            tokens = DiaTokenizer.Instance.Tokenize(text);
            result = DiaParser.ParseFree(tokens);
            Assert.That(result, Is.EqualTo("Hello,"));
        }

        [Test]
        public void ParseGroupOnly()
        {
            Assert.That(DiaParser.ParseGroup("(a | b)"), Is.EqualTo("a | b"));
            Assert.That(DiaParser.ParseGroup("(a | b | c)"), Is.EqualTo("a | b | c"));
            Assert.That(DiaParser.ParseGroup("(a |)"), Is.EqualTo("a |"));
            Assert.That(DiaParser.ParseGroup("(a | b)"), Is.EqualTo("a | b"));
            Assert.That(DiaParser.ParseGroup("(aa | bb cc)"), Is.EqualTo("aa | bb cc"));
        }

        //[Test]
        public void GroupParserFail()
        {
            Assert.Throws<Superpower.ParseException>(() => DiaParser.ParseGroup("()"));
            //())
            //(()
            //(|)
            //(| ())
            //(.)
            //(())
            //(() | ())
            //(abc)
            //(a bc)
            //(a.bc())
        }

        [Test]
        public void ParseFreeOnly()
        {
            Assert.That(DiaParser.ParseFree("Hello"), Is.EqualTo("Hello"));
            Assert.That(DiaParser.ParseFree("Hello,"), Is.EqualTo("Hello,"));
            Assert.That(DiaParser.ParseFree("Hello="), Is.EqualTo("Hello="));
            Assert.That(DiaParser.ParseFree("Hello ,"), Is.EqualTo("Hello ,"));

        }

        [Test]
        public void ParseMetaOnly()
        {
            Assert.That(DiaParser.ParseMeta("{}"), Is.EqualTo(new Dictionary<string, string>() { }));

            Assert.That(DiaParser.ParseMeta("{a=b}"), Is.EqualTo(new Dictionary<string, string>(){
                {"a", "b"}
            }));

            Assert.That(DiaParser.ParseMeta("{a=b,c=d}"), Is.EqualTo(new Dictionary<string, string>(){
                {"a", "b"}, {"c", "d"}
            }));

            Assert.That(DiaParser.ParseMeta("{a=$b}"), Is.EqualTo(new Dictionary<string, string>(){
                {"a", "$b"}
            }));

            Assert.That(DiaParser.ParseMeta("{a=$$b}"), Is.EqualTo(new Dictionary<string, string>(){
                {"a", "$$b"}
            }));

            Assert.That(DiaParser.ParseMeta("{a1=false}"), Is.EqualTo(new Dictionary<string, string>(){
                {"a1", "false"}
            }));

            Assert.That(DiaParser.ParseMeta("{ type = a, stage = b }"), Is.EqualTo(new Dictionary<string, string>(){
                {"type", "a"}, {"stage", "b"}
            }));

            Assert.That(DiaParser.ParseMeta("{ staleness = 1,type = a,stage = b}"), Is.EqualTo(new Dictionary<string, string>(){
                {"staleness", "1"}, {"type", "a"},  {"stage", "b"}
            }));

            Assert.That(DiaParser.ParseMeta(" { resume=false,type = a,stage = b}"), Is.EqualTo(new Dictionary<string, string>(){
                 {"resume", "false"}, {"type", "a"},  {"stage", "b"}
            }));

            Assert.That(DiaParser.ParseMeta("{ resumable=false,type = a,stage = b} "), Is.EqualTo(new Dictionary<string, string>(){
                 {"resumable", "false"}, {"type", "a"},  {"stage", "b"}
            }));

            /* TODO:
               Assert.That(DiaParser.ParseMeta("{a=(b|c)}"), Is.EqualTo(new Dictionary<string, string>(){
                {"a", "(b|c)"}
            })); */
        }

        [Test]
        public void TestParsers()
        {
            // Text
            //Assert.That(DiaParser.ParseText("Hello"), Is.EqualTo("Hello"));

            // Text - Fails
            //Assert.That(DiaParser.ParseText("Hello,"), Is.EqualTo("Hello,"));

            // Actor
            Assert.That(DiaParser.ParseActor("Hello:"), Is.EqualTo("Hello:"));

            // Label
            Assert.Throws<Superpower.ParseException>(() => DiaParser.ParseLabel(":Hello"));
            Assert.Throws<Superpower.ParseException>(() => DiaParser.ParseLabel("Hello:"));
            Assert.Throws<Superpower.ParseException>(() => DiaParser.ParseLabel("1Hello:"));
            Assert.That(DiaParser.ParseLabel("#Hello"), Is.EqualTo("#Hello"));
            Assert.Throws<Superpower.ParseException>(() => DiaParser.ParseLabel("#1Hello"));
            Assert.Throws<Superpower.ParseException>(() => DiaParser.ParseLabel("$"));

            // Bool
            Assert.That(DiaParser.ParseBool("true"), Is.EqualTo("true"));
            Assert.That(DiaParser.ParseBool("false"), Is.EqualTo("false"));

            // Symbol
            Assert.That(DiaParser.ParseSymbol("$sym"), Is.EqualTo("$sym"));
            Assert.That(DiaParser.ParseSymbol("$$A"), Is.EqualTo("$$A")); // ??
            Assert.Throws<Superpower.ParseException>(() => DiaParser.ParseSymbol("$"));

        }

        [Test]
        public void ParseLine()
        {
            string text;
            TokenList<DiaToken> tokens;
            DiaParser.DiaLine dline;

            text = "Dave:SAY Hello";
            tokens = DiaTokenizer.Instance.Tokenize(text);
            dline = DiaParser.Parse(tokens).ElementAt(0);
            Assert.That(dline.command, Is.EqualTo("SAY"));
            Assert.That(dline.text, Is.EqualTo("Hello"));
            Assert.That(dline.actor, Is.EqualTo("Dave:"));

            text = "Dave: ASK Hello";
            tokens = DiaTokenizer.Instance.Tokenize(text);
            dline = DiaParser.Parse(tokens).ElementAt(0);
            Assert.That(dline.command, Is.EqualTo("ASK"));
            Assert.That(dline.text, Is.EqualTo("Hello"));
            Assert.That(dline.actor, Is.EqualTo("Dave:"));

            text = "OPT Yes #Hello";
            tokens = DiaTokenizer.Instance.Tokenize(text);
            dline = DiaParser.Parse(tokens).ElementAt(0);
            Assert.That(dline.command, Is.EqualTo("OPT"));
            Assert.That(dline.text, Is.EqualTo("Yes"));
            Assert.That(dline.label, Is.EqualTo("#Hello"));

            text = "OPT #Hello"; // should fail?
            tokens = DiaTokenizer.Instance.Tokenize(text);
            dline = DiaParser.Parse(tokens).ElementAt(0);
            Assert.That(dline.command, Is.EqualTo("OPT"));
            Assert.That(dline.text, Is.EqualTo(""));
            Assert.That(dline.label, Is.EqualTo("#Hello"));

            text = "OPT Hello";
            tokens = DiaTokenizer.Instance.Tokenize(text);
            dline = DiaParser.Parse(tokens).ElementAt(0);
            Assert.That(dline.command, Is.EqualTo("OPT"));
            Assert.That(dline.text, Is.EqualTo("Hello"));
            Assert.That(dline.label, Is.EqualTo(""));

            text = "CHAT #Hello";
            tokens = DiaTokenizer.Instance.Tokenize(text);
            dline = DiaParser.Parse(tokens).ElementAt(0);
            Assert.That(dline.command, Is.EqualTo("CHAT"));
            Assert.That(dline.text, Is.EqualTo(""));
            Assert.That(dline.label, Is.EqualTo("#Hello"));

            text = "SAY Hello";
            tokens = DiaTokenizer.Instance.Tokenize(text);
            dline = DiaParser.Parse(tokens).ElementAt(0);
            Assert.That(dline.command, Is.EqualTo("SAY"));
            Assert.That(dline.text, Is.EqualTo("Hello"));

            text = "SAY Is this a parser?";
            tokens = DiaTokenizer.Instance.Tokenize(text);
            dline = DiaParser.Parse(tokens).ElementAt(0);
            Assert.That(dline.command, Is.EqualTo("SAY"));
            Assert.That(dline.text, Is.EqualTo("Is this a parser?"));

            text = "ASK Is this a parser?";
            tokens = DiaTokenizer.Instance.Tokenize(text);
            dline = DiaParser.Parse(tokens).ElementAt(0);
            Assert.That(dline.command, Is.EqualTo("ASK"));
            Assert.That(dline.text, Is.EqualTo("Is this a parser?"));


            text = "Dave: SAY My List: 1,2,3 {a=b,c=d}";
            tokens = DiaTokenizer.Instance.Tokenize(text);
            dline = DiaParser.Parse(tokens).ElementAt(0);
            Assert.That(dline.command, Is.EqualTo("SAY"));
            Assert.That(dline.text, Is.EqualTo("My List: 1,2,3"));
            Assert.That(dline.actor, Is.EqualTo("Dave:"));
            Assert.That(dline.meta, Is.EqualTo(new Dictionary<string, string>(){
                {"a", "b"}, {"c", "d"}
            }));

            text = "Dave:SAY My List";
            tokens = DiaTokenizer.Instance.Tokenize(text);
            dline = DiaParser.Parse(tokens).ElementAt(0);
            Assert.That(dline.command, Is.EqualTo("SAY"));
            Assert.That(dline.text, Is.EqualTo("My List"));
            Assert.That(dline.actor, Is.EqualTo("Dave:"));

            text = "SAY My List: 1,2,3";
            tokens = DiaTokenizer.Instance.Tokenize(text);
            dline = DiaParser.Parse(tokens).ElementAt(0);
            Assert.That(dline.command, Is.EqualTo("SAY"));
            Assert.That(dline.text, Is.EqualTo("My List: 1,2,3"));
            Assert.That(dline.actor, Is.EqualTo(""));

            text = "SAY My List: 1,2,3 {a=b,c=d}";
            tokens = DiaTokenizer.Instance.Tokenize(text);
            dline = DiaParser.Parse(tokens).ElementAt(0);
            Assert.That(dline.command, Is.EqualTo("SAY"));
            Assert.That(dline.text, Is.EqualTo("My List: 1,2,3"));
            Assert.That(dline.actor, Is.EqualTo(""));
            Assert.That(dline.meta, Is.EqualTo(new Dictionary<string, string>(){
                {"a", "b"}, {"c", "d"}
            }));
        }
    }
}