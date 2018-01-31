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
                { "place", "Istanbul" },
                { "Happy", "HappyFlip" },
                { "prep", "then" },
        };

        [Fact]
        public void Test1()
        {
            
            var s = @"SAY The $animal woke and $prep (ate|ate)";
            Substitutor.ReplaceGroups(ref s);
            Substitutor.ReplaceVars(ref s, globals);
            Console.WriteLine("Running SubstitutionTests.Test1 :: "+s);
            Assert.Equal("SAY The dog woke and then ate", s);
        }
    }
}
