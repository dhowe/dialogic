using NUnit.Framework;
using Dialogic;
using System;
using System.Collections.Generic;
using System.IO;

namespace Dialogic
{
    [TestFixture]
    public class OperatorTests
    {
        [Test]
        public void AssignmentTests()
        {
            Assert.That(Operator.EQ.Invoke("hello", "hello"), Is.True);
            Assert.That(Operator.EQ.Invoke("hello", ""), Is.False);
            Assert.That(Operator.EQ.Invoke("hello", null), Is.False);

            Assert.That(Operator.NE.Invoke("hello", "hello"), Is.False);
            Assert.That(Operator.NE.Invoke("hello", ""), Is.True);
            Assert.That(Operator.NE.Invoke("hello", null), Is.True);

            Assert.That(Operator.EQ.Invoke("true", "false"), Is.False);
            Assert.That(Operator.EQ.Invoke("false", "false"), Is.True);
            Assert.That(Operator.EQ.Invoke("false", null), Is.False);

            Assert.That(Operator.NE.Invoke("hello", ""), Is.True);
            Assert.That(Operator.NE.Invoke("hello", "false"), Is.True);

            Assert.Throws<OperatorException>(() => Operator.NE.Invoke(null, null));
        }

        [Test]
        public void EqualityTests()
        {
            Assert.That(Operator.EQ.Invoke("hello", "hello"), Is.True);
            Assert.That(Operator.EQ.Invoke("hello", ""), Is.False);
            Assert.That(Operator.EQ.Invoke("hello", null), Is.False);

            Assert.That(Operator.NE.Invoke("hello", "hello"), Is.False);
            Assert.That(Operator.NE.Invoke("hello", ""), Is.True);
            Assert.That(Operator.NE.Invoke("hello", null), Is.True);

            Assert.That(Operator.EQ.Invoke("true", "false"), Is.False);
            Assert.That(Operator.EQ.Invoke("false", "false"), Is.True);
            Assert.That(Operator.EQ.Invoke("false", null), Is.False);

            Assert.That(Operator.NE.Invoke("hello", ""), Is.True);
            Assert.That(Operator.NE.Invoke("hello", "false"), Is.True);

            Assert.Throws<OperatorException>(() => Operator.NE.Invoke(null, null));
        }

        [Test]
        public void ComparisonTests()
        {
            Assert.That(Operator.GT.Invoke("2", "1"), Is.True);
            Assert.That(Operator.GT.Invoke("1", "2"), Is.False);
            Assert.That(Operator.GT.Invoke("1", "1"), Is.False);
            Assert.That(Operator.GT.Invoke("2.0", "1"), Is.True);
            Assert.That(Operator.GT.Invoke("1.0", "2"), Is.False);
            Assert.That(Operator.GT.Invoke("1.0", "1"), Is.False);
            Assert.That(Operator.GT.Invoke("2.0", "1.00"), Is.True);
            Assert.That(Operator.GT.Invoke("1.0", "2.00"), Is.False);
            Assert.That(Operator.GT.Invoke("1.0", "1.00"), Is.False);

            Assert.That(Operator.LT.Invoke("2", "1"), Is.False);
            Assert.That(Operator.LT.Invoke("1", "2"), Is.True);
            Assert.That(Operator.LT.Invoke("1", "1"), Is.False);
            Assert.That(Operator.LT.Invoke("2.0", "1"), Is.False);
            Assert.That(Operator.LT.Invoke("1.0", "2"), Is.True);
            Assert.That(Operator.LT.Invoke("1.0", "1"), Is.False);
            Assert.That(Operator.LT.Invoke("2.0", "1.00"), Is.False);
            Assert.That(Operator.LT.Invoke("1.0", "2.00"), Is.True);
            Assert.That(Operator.LT.Invoke("1.0", "1.00"), Is.False);

            Assert.That(Operator.LE.Invoke("2", "1"), Is.False);
            Assert.That(Operator.LE.Invoke("1", "2"), Is.True);
            Assert.That(Operator.LE.Invoke("1", "1"), Is.True);
            Assert.That(Operator.LE.Invoke("2.0", "1"), Is.False);
            Assert.That(Operator.LE.Invoke("1.0", "2"), Is.True);
            Assert.That(Operator.LE.Invoke("1.0", "1"), Is.True);
            Assert.That(Operator.LE.Invoke("2.0", "1.00"), Is.False);
            Assert.That(Operator.LE.Invoke("1.0", "2.00"), Is.True);
            Assert.That(Operator.LE.Invoke("1.0", "1.00"), Is.True);

            Assert.Throws<OperatorException>(() => Operator.GT.Invoke("2", ""));
            Assert.Throws<OperatorException>(() => Operator.LT.Invoke("2", null));
            Assert.Throws<OperatorException>(() => Operator.LE.Invoke("2", "h"));
            Assert.Throws<OperatorException>(() => Operator.GE.Invoke("", ""));
        }

        [Test]
        public void MatchingTests()
        {
            Assert.That(Operator.SW.Invoke("Hello", "He"), Is.True);
            Assert.That(Operator.SW.Invoke("Hello", "Hello"), Is.True);
            Assert.That(Operator.SW.Invoke("Hello", "Hej"), Is.False);
            Assert.That(Operator.SW.Invoke("Hello", null), Is.False);
            Assert.That(Operator.SW.Invoke("Hello", ""), Is.True);

            Assert.That(Operator.EW.Invoke("Hello", "o"), Is.True);
            Assert.That(Operator.EW.Invoke("Hello", "Hello"), Is.True);
            Assert.That(Operator.EW.Invoke("Hello", "l1o"), Is.False);
            Assert.That(Operator.EW.Invoke("Hello", null), Is.False);
            Assert.That(Operator.EW.Invoke("Hello", ""), Is.True);

            Assert.That(Operator.RE.Invoke("Hello", "ll"), Is.True);
            Assert.That(Operator.RE.Invoke("Hello", "e"), Is.True);
            Assert.That(Operator.RE.Invoke("Hello", "l1"), Is.False);
            Assert.That(Operator.RE.Invoke("Hello", null), Is.False);
            Assert.That(Operator.RE.Invoke("Hello", ""), Is.True);


            Assert.That(Operator.SW.Invoke("$Hello", "$"), Is.True);
            Assert.That(Operator.EW.Invoke("$Hello", "$"), Is.False);
            Assert.That(Operator.RE.Invoke("$Hello", "$"), Is.True);
            Assert.That(Operator.RE.Invoke("hello", "(hello|bye)"), Is.True);
            Assert.That(Operator.RE.Invoke("bye", "(hello|bye)"), Is.True);
            Assert.That(Operator.RE.Invoke("by", "(hello|bye)"), Is.False);

            Assert.Throws<OperatorException>(() => Operator.SW.Invoke(null, "hello"));
            Assert.Throws<OperatorException>(() => Operator.SW.Invoke(null, null));
        }

    }
}
