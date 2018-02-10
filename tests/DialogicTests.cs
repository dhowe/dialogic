using NUnit.Framework;
using Dialogic;
using System;
using System.Collections.Generic;
using System.IO;

namespace tests
{
    [TestFixture()]
    public class DialogicTests
    {
        [Test()]
        public void TestCase()
        {
            Assert.That(15, Is.EqualTo(15));
        }
    }
}
