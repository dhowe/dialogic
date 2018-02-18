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
                { "dev", "1" }
            };
            chats = cr.FindAll(q);
            //chats.ForEach((obj) => Console.WriteLine(obj.Text));
            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(chats.Count));
            Assert.That(chats[0].Text, Is.EqualTo("c2"));
        }

        public void TestFindAll2()
        {
            Chat c;
            List<Chat> chats = new List<Chat>();
            chats.Add(c = new Chat("c1"));
            chats.Add(c = new Chat("c2"));
            c.SetMeta("dev", 1);
            c.SetMeta("day", "hello");
            chats.Add(c = new Chat("c3"));
            ChatRuntime cr = new ChatRuntime(chats);
            Dictionary<string, object> q = new Dictionary<string, object> {
                { "dev", 1 }
            };
            chats = cr.FindAll(q);
            //chats.ForEach((obj) => Console.WriteLine(obj.Text));
            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(chats.Count));
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
            Dictionary<string, object> q = new Dictionary<string, object> {
                { "dev", "1" }
            };
            chats = cr.FindAll(q);
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
            Dictionary<string, object> q = new Dictionary<string, object> {
                { "dev", "1" }
            };
            Chat res = cr.Find(q);
            Assert.That(res.Text, Is.EqualTo("c2"));
        }

        [Test()]
        public void TestFindAllOps()
        {
            string[] lines = {
                "FIND {dev=1}",
                //"CHAT c1",
                //"CHAT c2 {dev=2,day=fri}",
                //"CHAT c3 {}"
            };
            string contents = String.Join("\n", lines);
            Console.WriteLine(contents);

            List<Chat> chats = ChatParser.ParseText(contents);
            Command c = chats[0].commands[0];
            Assert.That(c.GetType(), Is.EqualTo(typeof(Find)));

            ChatRuntime cr = new ChatRuntime(chats);
            ChatEvent ce = c.Fire(cr);
            Console.WriteLine(ce);
            //chats.Add(new Chat("c1"));
            //chats.Add(new Chat("c3"));
            //ChatRuntime cr = new ChatRuntime(chats);

            //Dictionary<string, object> q = new Dictionary<string, object> {
            //    { "dev", 1 }
            //};
            //chats = cr.FindAll(q);
            ////chats.ForEach((obj) => Console.WriteLine(obj.Text));
            //Assert.That(chats, Is.Not.Null);
            //Assert.That(chats.Count, Is.EqualTo(chats.Count));
            //Assert.That(chats[0].Text, Is.EqualTo("c2"));
        }
    }
}
