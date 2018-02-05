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
            var s = ScriptingEngine.Eval("2+2");
            //Console.WriteLine("Running ScriptingTests.Test1 :: "+s);
            Assert.Equal("4", s);
        }

        [Fact]
        public void Test2()
        {
            var s = ScriptingEngine.Eval("return \"hello\";");
            Console.WriteLine("Running ScriptingTests.Test2 :: "+s);
            Assert.Equal("hello", s);
        }

        [Fact]
        public void Test3()
        {
            var se = new ScriptingEngine();
            var s = se.Exec("var x = 1;");
            Console.WriteLine("Running ScriptingTests.Test3 :: '"+s+"'");
            s = se.Exec("return ++x;");
            Assert.Equal("2", s);
        }

        [Fact]
        public void Test4()
        {
            var se = new ScriptingEngine();
            var s = se.Exec("var x = 1;");
            Console.WriteLine("Running ScriptingTests.Test4 :: '" + s + "'");
            s = se.Exec("return x == 1 ? 2 : 0;");
            Assert.Equal("2", s);
        }

        [Fact]
        public void Test5()
        {
            Dictionary<string, object> globals = new Dictionary<string, object>() {
                    { "animal", "dog" },
                    { "place", "Istanbul" },
                    { "Happy", "HappyFlip" },
                    { "prep", "then" },
                    { "i", 0 }
            };
            var se = new ScriptingEngine(globals);
            var s = se.Exec("var x = 1;");
            Console.WriteLine("Running ScriptingTests.Test3 :: '" + s + "'");
            s = se.Exec("return x == 1 ? 2 : 0;");
            Assert.Equal("2", s);
        }
    }
}
