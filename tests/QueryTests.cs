using NUnit.Framework;
using Dialogic;
using System;
using System.Collections.Generic;
using System.IO;

namespace tests
{
    [TestFixture()]
    public class QueryTests
    {

        public List<Chat> Find(params Func<Boolean>[] conditions)
        {
            List<Chat> l = new List<Chat>();
            l.ForEach((obj) =>
            {
                // WORKING HERE
            });
            return l;
        }

        /// ////////////////////////////////////////////////////

        [Test()]
        public void TestFindAll()
        {
            Chat c;
            List<Chat> chats = new List<Chat>();
            chats.Add(c = new Chat("c1"));
            chats.Add(c = new Chat("c2"));
            c.AddCondition("dev", "1");
            c.AddCondition("day", "hello");
            chats.Add(c = new Chat("c3"));
            ChatRuntime cr = new ChatRuntime(chats);
            Dictionary<string, string> q = new Dictionary<string, string> {
                { "dev", "1" }
            };
            chats = cr.FindAll(q);
            //chats.ForEach((obj) => Console.WriteLine(obj.Text));
            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(chats.Count));
            Assert.That(chats[0].Text, Is.EqualTo("c2"));
        }

        [Test()]
        public void TestFindAll2()
        {
            Chat c;
            List<Chat> chats = new List<Chat>();
            chats.Add(c = new Chat("c1"));
            c.AddCondition("dev", "2");
            chats.Add(c = new Chat("c2"));
            c.AddCondition("dev", "1");
            c.AddCondition("day", "hello");
            chats.Add(c = new Chat("c3"));
            ChatRuntime cr = new ChatRuntime(chats);
            Dictionary<string, string> q = new Dictionary<string, string> {
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
            c.AddCondition("dev", "1");
            c.AddCondition("day", "hello");
            chats.Add(c = new Chat("c3"));
            ChatRuntime cr = new ChatRuntime(chats);
            Dictionary<string, string> q = new Dictionary<string, string> {
                { "dev", "1" }
            };
            Chat res = cr.Find(q);
            Assert.That(res.Text, Is.EqualTo("c2"));
        }
    }
}
