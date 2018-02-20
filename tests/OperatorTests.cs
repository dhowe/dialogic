using NUnit.Framework;
using Dialogic;
using System;
using System.Collections.Generic;
using System.IO;

namespace tests
{
    [TestFixture()]
    public class OperatorTests
    {
        [Test()]
        public void TestParsing()
        {
            List<Chat> chats;

            chats = ChatParser.ParseText("FIND {num > 1}");
            Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetText(), Is.Null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));
            //Assert.That(chats[0].commands[0].HasMeta(), Is.Not.Null);
            //Assert.That(chats[0].commands[0].GetMeta("num"), Is.Not.Null);
        }
    }
}
