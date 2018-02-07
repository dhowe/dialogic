using NUnit.Framework;
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
        }
    }
}
