using System;
using System.Collections.Generic;
using Dialogic;
using Xunit;

namespace tests
{
    public class SubstitutionTests
    {
        // Handle subs, variable subs, and nested subs

        Dictionary<string, object> globals = new Dictionary<string, object>() {
                { "animal", "dog" },
                { "prep", "then" },
                { "count", 4 }
        };

        [Fact]
        public void Test1()
        {
            var s = @"SAY The $animal woke and $prep (ate|ate)";
            Substitutor.ReplaceGroups(ref s);
            Substitutor.ReplaceVars(ref s, globals);
            Console.WriteLine("Running SubstitutionTests.Test1 :: " + s);
            Assert.Equal("SAY The dog woke and then ate", s);
        }

        [Fact]
        public void Test2()
        {
            var s = @"SAY The $animal woke $count times";
            Substitutor.ReplaceVars(ref s, globals);
            Console.WriteLine("Running SubstitutionTests.Test2 :: " + s);
            Assert.Equal("SAY The dog woke 4 times", s);
        }
    }
}
