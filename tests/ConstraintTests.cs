using NUnit.Framework;
using Dialogic;
using System;
using System.Collections.Generic;
using System.IO;

namespace Dialogic
{
    [TestFixture]
    public class ConstraintTests
    {
        [Test]
        public void TestEquality()
        {
            Assert.That(new Constraint(Operator.EQ, "c1", "hello").Check("hello"), Is.True);
            Assert.That(new Constraint(Operator.EQ, "c1", "hello").Check("helloX"), Is.False);
            Assert.That(new Constraint(Operator.EQ, "c1", "hello").Check(""), Is.False);
            Assert.That(new Constraint(Operator.EQ, "c1", "hello").Check(null), Is.False);

            Assert.That(new Constraint(Operator.NEQ, "c1", "hello").Check("hello"), Is.False);
            Assert.That(new Constraint(Operator.NEQ, "c1", "hello").Check("helloX"), Is.True);
            Assert.That(new Constraint(Operator.NEQ, "c1", "hello").Check(null), Is.True);

            Assert.Throws<OperatorException>(() => new Constraint(Operator.EQ, "c1", (string)null).Check("hello"));
        }

        [Test]
        public void TestAnyEquals()
        {
            Assert.That(new Constraint(Operator.EQ, "c1", new string[] { "hell", "hello" }).Check("hello"), Is.True);
            Assert.That(new Constraint(Operator.EQ, "c1", new string[] { "hell", "h", "a" }).Check("a"), Is.True);
            Assert.That(new Constraint(Operator.EQ, "c1", new string[] { "hell", "h", "a" }).Check("b"), Is.False);
            Assert.That(new Constraint(Operator.EQ, "c1", new string[] { "hell", "h", "h" }).Check("h"), Is.True);
            Assert.That(new Constraint(Operator.EQ, "c1", new string[] { "hell", "h", "h" }).Check(null), Is.False);
        }
    }
}