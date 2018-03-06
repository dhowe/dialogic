﻿using System;
using System.Collections.Generic;
using Dialogic;
using NUnit.Framework;

namespace Dialogic
{
    [TestFixture]
    public class ParserTests
    {
        [Test]
        public void TestGrammarParsing()
        {
            List<Chat> chats = ChatReader.ParseText("GRAM { start: 'The <item>', item: cat }");
            Command gram = chats[0].commands[0];
            Console.WriteLine(gram);
            //Assert.That(chats.Count, Is.EqualTo(11111));
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(gram.Text, Is.Null);
            Assert.That(gram.GetType(), Is.EqualTo(typeof(Gram)));

            var cr = new ChatRuntime(chats);
            cr.Run();
        }
            
        [Test]
        public void TestCommandParsing()
        {
            List<Chat> chats;

            chats = ChatReader.ParseText("GO #Twirl");
            Console.WriteLine(chats[0].ToTree());

            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Twirl"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Go)));

            chats = ChatReader.ParseText("ASK Want a game?\nOPT Y #Game\nOPT N #End");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Ask)));

            Ask ask = (Dialogic.Ask)chats[0].commands[0];
            Assert.That(ask.Text, Is.EqualTo("Want a game?"));

            Assert.That(ask.Options().Count, Is.EqualTo(2));

            var options = ask.Options();
            Assert.That(options[0].GetType(), Is.EqualTo(typeof(Opt)));
            Assert.That(options[0].Text, Is.EqualTo("Y"));
            Assert.That(options[0].action.GetType(), Is.EqualTo(typeof(Go)));

            Assert.That(options[1].GetType(), Is.EqualTo(typeof(Opt)));
            Assert.That(options[1].Text, Is.EqualTo("N"));
            Assert.That(options[1].action.GetType(), Is.EqualTo(typeof(Go)));

            chats = ChatReader.ParseText("FIND {num=1}");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.Null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));
            Assert.That(chats[0].commands[0].GetMeta("num"), Is.Not.Null);

            chats = ChatReader.ParseText("DO #Twirl");
            Console.WriteLine(chats[0].ToTree());
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

        [Test]
        public void TestOpsParsing()
        {
            var chat = ChatReader.ParseText("FIND {do=1}")[0];
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
            Chat c = ChatReader.ParseText(s)[0];
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
