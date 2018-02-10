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
        List<Chat> chats = new List<Chat>();

        private QueryTests() {
            Chat c1 = new Chat();
        }

        public List<Chat> Find(params Func<Boolean>[] conditions)
        {
            List<Chat> l = new List<Chat>();
            l.ForEach((obj) => {
                // WORKING HERE
            });
            return l;
        }

        /// ////////////////////////////////////////////////////

        [Test()]
        public void TestCase()
        {
            Assert.That(15, Is.EqualTo(15));
        }
    }
}
