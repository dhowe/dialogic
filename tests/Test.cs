using NUnit.Framework;
using Dialogic;
using System;


namespace tests
{
    [TestFixture()]
    public class Test
    {
        [Test()]
        public void TestCase()
        {
            Assert.That(15, Is.EqualTo(15));
            ConsoleClient c = new ConsoleClient();
        }
    }
}
