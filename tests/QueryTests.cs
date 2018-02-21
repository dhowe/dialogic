using NUnit.Framework;
using Dialogic;
using System.Collections.Generic;
using System;

namespace dialogic
{
    [TestFixture]
    public class QueryTests
    {
        /// ////////////////////////////////////////////////////

        [Test]
        public void TestFindAll()
        {
            Chat c;
            List<Chat> chats = new List<Chat>();
            chats.Add(c = new Chat("c1"));
            chats.Add(c = new Chat("c2"));
            c.SetMeta("dev", "1");
            c.SetMeta("day", "hello");
            chats.Add(c = new Chat("c3"));
            ChatRuntime cr = new ChatRuntime(chats);
            chats = cr.FindAll(new Constraints("dev", "1").AsDict());
            //chats.ForEach((obj) => Console.WriteLine(obj.Text));
            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(3));
            Assert.That(chats[0].Text, Is.EqualTo("c2"));
        }

        [Test]
        public void TestFindAll2()
        {
            Chat c;
            List<Chat> chats = new List<Chat>();
            chats.Add(c = new Chat("c1"));
            chats.Add(c = new Chat("c2"));
            c.SetMeta("dev", "1");
            c.SetMeta("day", "hello");
            chats.Add(c = new Chat("c3"));
            ChatRuntime cr = new ChatRuntime(chats);
            chats = cr.FindAll(new Constraints("dev", "1").AsDict());
            //chats.ForEach((obj) => Console.WriteLine(obj.Text));
            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(3));
            Assert.That(chats[0].Text, Is.EqualTo("c2"));
        }

        [Test]
        public void TestFindAll3()
        {
            Chat c;
            List<Chat> chats = new List<Chat>();
            chats.Add(c = new Chat("c1"));
            c.SetMeta("dev", "2");
            chats.Add(c = new Chat("c2"));
            c.SetMeta("dev", "1");
            c.SetMeta("day", "hello");
            chats.Add(c = new Chat("c3"));

            ChatRuntime cr = new ChatRuntime(chats);
            chats = cr.FindAll(new Constraints("dev", "1").AsDict());
            //chats.ForEach((obj) => Console.WriteLine(obj.Text));
            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(2));
            Assert.That(chats[0].Text, Is.EqualTo("c2"));
            Assert.That(chats[1].Text, Is.EqualTo("c3"));
        }

        [Test]
        public void TestFind()
        {
            Chat c;
            List<Chat> chats = new List<Chat>();
            chats.Add(c = new Chat("c1"));
            chats.Add(c = new Chat("c2"));
            c.SetMeta("dev", "1");
            c.SetMeta("day", "hello");
            chats.Add(c = new Chat("c3"));
            ChatRuntime cr = new ChatRuntime(chats);
            Chat res = new ChatRuntime(chats).Find(new Constraints("dev", "1").AsDict());
            Assert.That(res.Text, Is.EqualTo("c2"));
        }

        [Test]
        public void TestFindAllOpsAPI()
        {
            string[] lines = {
                "CHAT c1 {day=fri}",
                "CHAT c2 {dev=2,day=fri}",
                "CHAT c3 {}"
            };
            string contents = String.Join("\n", lines);
            List<Chat> chats = ChatParser.ParseText(contents);
            List<Chat> result = new ChatRuntime(chats).FindAll
                (new Constraints("dev", "1").Add("day", "fri").AsDict());
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Text, Is.EqualTo("c1"));
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public void TestFindAll4()
        {
            string[] lines = {
                "CHAT c0",
                "FIND {dev=1,day=fri}",
                "CHAT c1 {day=fri}",
                "CHAT c2 {dev=2,day=fri}",
                "CHAT c3 {}"
            };

            string contents = String.Join("\n", lines);

            List<Chat> chats = ChatParser.ParseText(contents);
            //chats.ForEach((ch) => Console.WriteLine(ch.ToTree()));
            Command finder = chats[0].commands[0];
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));
            ChatRuntime cr = new ChatRuntime(chats);

            chats = cr.FindAll(finder.Meta());
            //chats.ForEach((obj) => Console.WriteLine(obj.Text));

            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(3));

            Assert.That(chats[0], Is.Not.Null);
            Assert.That(chats[0].Text, Is.EqualTo("c1"));
        }

        [Test]
        public void TestFindAllOp1()
        {
            string[] lines = { "CHAT c0 {dev=1}", "FIND {dev>2,day=fri}" };

            string contents = String.Join("\n", lines);

            List<Chat> chats = ChatParser.ParseText(contents);
            //chats.ForEach((ch) => Console.WriteLine(ch.ToTree()));
            Command finder = chats[0].commands[0];
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));
            ChatRuntime cr = new ChatRuntime(chats);

            chats = cr.FindAll(finder.Meta());
            //chats.ForEach((obj) => Console.WriteLine(obj.Text));

            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(0));
        }


        [Test]
        public void TestFindAllOp2()
        {
            string[] lines = {
                "CHAT c0",
                "FIND {dev>1,day=fri}",
                "CHAT c1 {dev=2,day=fri}",
                "CHAT c2 {dev=1,day=fri}",
                "CHAT c3 {}"
            };

            string contents = String.Join("\n", lines);

            List<Chat> chats = ChatParser.ParseText(contents);
            //chats.ForEach((ch) => Console.WriteLine(ch.ToTree()));
            Command finder = chats[0].commands[0];
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));
            ChatRuntime cr = new ChatRuntime(chats);

            chats = cr.FindAll(finder.Meta());
            //chats.ForEach((obj) => Console.WriteLine(obj.Text));

            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(3));

            Assert.That(chats[0], Is.Not.Null);
            Assert.That(chats[0].Text, Is.EqualTo("c1"));
        }
    }
}
