using NUnit.Framework;
using Dialogic;
using System;
using System.Collections.Generic;
using System.IO;

namespace tests
{
    [TestFixture()]
    public class SubstTests
    {
        Dictionary<string, object> globals = new Dictionary<string, object>() {
                { "animal", "dog" },
                { "prep", "then" },
                { "count", 4 }
        };

        [Test()]
        public void Test1()
        {
            var s = @"SAY The $animal woke and $prep (ate|ate)";
            Substitutor.ReplaceGroups(ref s);
            Substitutor.ReplaceVars(ref s, globals);
            Console.WriteLine("Running SubstitutionTests.Test1 :: " + s);
            Assert.That(s, Is.EqualTo("SAY The dog woke and then ate"));
        }

        [Test()]
        public void Test2()
        {
            var s = @"SAY The $animal woke $count times";
            Substitutor.ReplaceVars(ref s, globals);
            Console.WriteLine("Running SubstitutionTests.Test2 :: " + s);
            Assert.That(s, Is.EqualTo("SAY The dog woke 4 times"));
        }
    }
}
