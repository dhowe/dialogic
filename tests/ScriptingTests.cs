using System;
using System.Collections.Generic;
using Dialogic;
using Xunit;

namespace tests
{
    public class ScriptingTests
    {
        [Fact]
        public void Test1()
        {
            
            var s = new ScriptingEngine().Execute("2+2");
            Console.WriteLine("Running ScriptingTests.Test1 :: "+s);
            Assert.Equal("3", s);
        }
    }
}
