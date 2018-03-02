using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Dialogic;
using NUnit.Framework;

namespace Dialogic
{
    [TestFixture]
    public class ReaderTests
    {

        public static string CMD = @"(^[A-Z][A-Z]+)?\s*";//(#\w+)?\s*([^#\[[\])({[^}]})?)";
        public static string TEXT = @"([^#}{]+)?\s*";
        public static string META = @"(\{.+?\})?\s*";
        public static Regex Patt = new Regex(CMD+TEXT+META);


        string[] lines = {
            "SAY Thank you {pace=fast,count=2}", "SAY Thank you", "FIND { num > 1, a!=4}", 
            "DO #Twirl", "DO #Twirl {speed= fast}", "SAY Thank you", "WAIT", "WAIT ",
            "SAY Thank you { pace = fast}", "SAY Thank you {}", "Thank you"
        };

        [Test]
        public void TestRegexParsing()
        {
            List<string> cmds = new List<string>();
            for (int i = 0; i < lines.Length; i++)
            {
                Match match = Patt.Match(lines[i]);
                var cmd = match.Groups[1].Value.Trim();
                var text = match.Groups[2].Value.Trim();
                var meta = match.Groups[3].Value.Trim();
                //if (tmp!=null && tmp.Length>1) meta = tmp.Trim();
                if (String.IsNullOrEmpty(cmd)) cmd = "SAY";
                cmds.Add("'"+cmd + "' '" + text+"' '"+ meta + "'");

                Console.WriteLine("------------------------------------------");
                Console.WriteLine(i +") "+lines[i]);
                Util.ShowMatch(match);
                //Assert.That(Regex.IsMatch(lines[i], RE));
            }

            Console.WriteLine();
            for (int i = 0; i < cmds.Count; i++)
            {
                Console.WriteLine(i + ") " + cmds[i]);
            }
            Assert.That(false);

        }

        public void TestCommandParsing()
        {
            List<Chat> chats;

            chats = ChatReader.ParseText("FIND {num=1}");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.Null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));
            Assert.That(chats[0].commands[0].GetMeta("num"), Is.Not.Null);

            chats = ChatReader.ParseText("DO Twirl");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Twirl"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Do)));

            chats = ChatReader.ParseText("DO #Twirl");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Twirl"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Do)));

            chats = ChatReader.ParseText("SAY Thank you");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));

            chats = ChatReader.ParseText("SAY Thank you { pace = fast}");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Assert.That(chats[0].commands[0].GetMeta("pace"), Is.EqualTo("fast"));

            chats = ChatReader.ParseText("SAY Thank you {pace=fast,count=2}");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Assert.That(chats[0].commands[0].HasMeta(), Is.EqualTo(true));
            Assert.That(chats[0].commands[0].GetMeta("pace"), Is.EqualTo("fast"));
            Assert.That(chats[0].commands[0].GetMeta("count"), Is.EqualTo("2"));
            /*Assert.That(chats[0].commands[0].GetMetaInt("count"), Is.EqualTo(2));
            Assert.That(chats[0].commands[0].GetMetaDouble("count"), Is.EqualTo(2));*/

            chats = ChatReader.ParseText("SAY Thank you {}");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Assert.That(chats[0].commands[0].HasMeta(), Is.EqualTo(false));
        }
    }
}
