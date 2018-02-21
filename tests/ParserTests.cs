using System;
using System.Collections.Generic;
using Dialogic;
using NUnit.Framework;

namespace dialogic
{
    [TestFixture]
    public class ParserTests
    {
        [Test]
        public void TestCommandParsing()
        {
            List<Chat> chats;

            chats = ChatParser.ParseText("FIND {num=1}");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.Null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));
            Assert.That(chats[0].commands[0].GetMeta("num"), Is.Not.Null);

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

            chats = ChatParser.ParseText("SAY Thank you {pace=fast,count=2}");
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

            chats = ChatParser.ParseText("SAY Thank you {}");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Assert.That(chats[0].commands[0].HasMeta(), Is.EqualTo(false));
        }

        [Test]
        public void TestOpsParsing()
        {
            var chat = ChatParser.ParseText("FIND {do=1}")[0];
            //Console.WriteLine(chat.ToTree());
            Assert.That(chat.Count, Is.EqualTo(1));
            Assert.That(chat.GetType(), Is.EqualTo(typeof(Chat)));
            var finder = chat.commands[0];
            Assert.That(finder.GetType(), Is.EqualTo(typeof(Find)));
            Assert.That(finder.Meta(), Is.Not.Null);
        }

        /*[Test]
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
        }*/
    }
}
