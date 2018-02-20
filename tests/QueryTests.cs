using NUnit.Framework;
using Dialogic;
using System.Collections.Generic;
using System;

namespace tests
{
    [TestFixture()]
    public class QueryTests
    {
        /// ////////////////////////////////////////////////////

        [Test()]
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
            Dictionary<string, object> q = new Dictionary<string, object> {
                { "dev", new Constraint("dev","1") }
            };
            chats = cr.FindAll(q);
            //chats.ForEach((obj) => Console.WriteLine(obj.Text));
            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(3));
            Assert.That(chats[0].Text, Is.EqualTo("c2"));
        }

        [Test()]
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
            Dictionary<string, object> q = new Dictionary<string, object> {
                { "dev", new Constraint("dev","1") }
            };
            chats = cr.FindAll(q);
            //chats.ForEach((obj) => Console.WriteLine(obj.Text));
            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(3));
            Assert.That(chats[0].Text, Is.EqualTo("c2"));
        }

        [Test()]
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
            chats = cr.FindAll(new Dictionary<string, object> { { "dev", new Constraint("dev","1") } });
            //chats.ForEach((obj) => Console.WriteLine(obj.Text));
            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(2));
            Assert.That(chats[0].Text, Is.EqualTo("c2"));
            Assert.That(chats[1].Text, Is.EqualTo("c3"));
        }

        [Test()]
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
            Chat res = cr.Find(new Dictionary<string, object> { { "dev", new Constraint("dev","1") } });
            Assert.That(res.Text, Is.EqualTo("c2"));
        }

        [Test()]
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
                (new Dictionary<string, object> {
                    { "dev", new Constraint("dev","1") },
                    { "day", new Constraint("day","fri") }}
            );
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Text, Is.EqualTo("c1"));
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test()]
        public void TestFindAllOps()
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

            Chat result = chats[0];
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Text, Is.EqualTo("c1"));
        }
    }
}
