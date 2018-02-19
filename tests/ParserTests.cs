using NUnit.Framework;
using Dialogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace tests
{

    [TestFixture()]
    public class ParserTests
    {
        public static void DisplayMatchResults(Match match)
        {
            Console.WriteLine("Match has {0} captures", match.Captures.Count);

            int groupNo = 0;
            foreach (Group mm in match.Groups)
            {
                Console.WriteLine("  Group {0,2} has {1,2} captures '{2}'", groupNo, mm.Captures.Count, mm.Value);

                int captureNo = 0;
                foreach (Capture cc in mm.Captures)
                {
                    Console.WriteLine("       Capture {0,2} '{1}'", captureNo, cc);
                    captureNo++;
                }
                groupNo++;
            }

            groupNo = 0;
            foreach (Group mm in match.Groups)
            {
                Console.WriteLine("    match.Groups[{0}].Value == \"{1}\"", groupNo, match.Groups[groupNo].Value); //**
                groupNo++;
            }

            groupNo = 0;
            foreach (Group mm in match.Groups)
            {
                int captureNo = 0;
                foreach (Capture cc in mm.Captures)
                {
                    Console.WriteLine("    match.Groups[{0}].Captures[{1}].Value == \"{2}\"", groupNo, captureNo, match.Groups[groupNo].Captures[captureNo].Value); //**
                    captureNo++;
                }
                groupNo++;
            }
        }

        [Test()]
        public void TestOperatorMatching()
        {
            string[] tests = { "num > 1", "num = 1", "num >= 1", "num <= 1", "num != 1", "num == 1" };
            string[] ops = { ">", "=", ">=", "<=", "!=", "==" };
            Assert.That(tests.Length, Is.EqualTo(ops.Length));
            Regex regex = new Regex(@"($?[a-zA-Z_][a-zA-Z0-9_]+) *([!<=>]+) *([^ ]+)");
            for (int i = 0; i < tests.Length; i++)
            {
                Match match = regex.Match(tests[i]);
                Assert.That(match.Groups.Count, Is.EqualTo(4));
                string key = match.Groups[1].Value;
                string op = match.Groups[2].Value;
                string val = match.Groups[3].Value;
                Assert.That(key, Is.EqualTo("num"));
                Assert.That(op, Is.EqualTo(ops[i]));
                Assert.That(val, Is.EqualTo("1"));
                Console.WriteLine(i+") "+key+op+val);
            }
        }


        [Test()]
        public void TestCommandParsing()
        {
            List<Chat> chats;

            chats = ChatParser.ParseText("FIND {num>1}");
            Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.Null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));

            chats = ChatParser.ParseText("DO Twirl");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Twirl"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Do)));

            chats = ChatParser.ParseText("SAY Thank you");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));

            chats = ChatParser.ParseText("SAY Thank you { pace = fast}");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Assert.That(chats[0].commands[0].GetMeta("pace"), Is.EqualTo("fast"));
            Assert.That(chats[0].commands[0].GetMetaString("pace"), Is.EqualTo("fast"));

            chats = ChatParser.ParseText("SAY Thank you {pace=fast,count=2}");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Assert.That(chats[0].commands[0].HasMeta(), Is.EqualTo(true));
            Assert.That(chats[0].commands[0].GetMeta("pace"), Is.EqualTo("fast"));
            Assert.That(chats[0].commands[0].GetMetaInt("count"), Is.EqualTo(2));
            Assert.That(chats[0].commands[0].GetMetaDouble("count"), Is.EqualTo(2));

            chats = ChatParser.ParseText("SAY Thank you {}");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Assert.That(chats[0].commands[0].HasMeta(), Is.EqualTo(false));
        }

        [Test()]
        public void TestOpsParsing()
        {
            var chat = ChatParser.ParseText("FIND {do=1}")[0];
            //Console.WriteLine(chat.ToTree());
            Assert.That(chat.Count, Is.EqualTo(1));
            /*Assert.That(chat.GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chat.commands[0].Text, Is.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Find)));*/
        }

        [Test()]
        public void TestMetaTypes()
        {
            var s = "CHAT C1 {int=1,double=2.3,string=hello,bool=true,intd=1.0}";
            Chat c = ChatParser.ParseText(s)[0];
            Assert.That(c.GetType(), Is.EqualTo(typeof(Chat)));

            Assert.That(c.GetMeta("int"), Is.EqualTo(1));
            Assert.That(c.GetMetaInt("int"), Is.EqualTo(1));
            Assert.That(c.GetMetaInt("Xint", -1), Is.EqualTo(-1));
            Assert.That(c.GetMeta("double"), Is.EqualTo(2.3));
            Assert.That(c.GetMetaDouble("double"), Is.EqualTo(2.3));
            Assert.That(c.GetMetaDouble("Xdouble", 0), Is.EqualTo(0));
            Assert.That(c.GetMeta("bool"), Is.EqualTo(true));
            Assert.That(c.GetMetaBool("bool"), Is.EqualTo(true));
            Assert.That(c.GetMetaBool("Xbool", false), Is.EqualTo(false));

            Assert.That(c.GetMetaDouble("int"), Is.EqualTo(1));
            Assert.That(c.GetMetaDouble("intd"), Is.EqualTo(1));
            Assert.That(c.GetMetaInt("intd"), Is.EqualTo(1));
            Assert.That(c.GetMetaDouble("intd"), Is.EqualTo(1));

            Assert.That(c.GetMeta("miss"), Is.Null);
            Assert.That(c.GetMetaInt("miss"), Is.EqualTo(0));
            Assert.That(c.GetMetaDouble("miss"), Is.EqualTo(0));
            Assert.That(c.GetMetaBool("miss"), Is.EqualTo(false));
        }
    }
}
