using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Dialogic
{
    [TestFixture]
    class KnownIssues : GenericTests
    {
        [Test]
        public void ParseChoices() // from ChoiceTests
        {
            Chat c = CreateParentChat("c");
            List<Choice> choices;
            Choice choice;

            // FAILS:
            choices = Choice.Parse("(ok (a).Cap() | ok (a).Cap()).Cap()", c, false);
            Assert.That(choices.Count, Is.EqualTo(1));
            choice = choices[0];
            Assert.That(choice.text, Is.EqualTo("(ok (a).Cap() | ok (a).Cap()).Cap()"));
            Assert.That(choice.options.Count, Is.EqualTo(1));
            Assert.That(choice.options, Is.EquivalentTo(new[] { "(ok (a).Cap()).Cap()" }));
        }
    }
}