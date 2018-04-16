using System.Collections.Generic;
using NUnit.Framework;

namespace Dialogic
{
    [TestFixture]
    class BindContextTests
    {
        const bool NO_VALIDATORS = true;

        static IDictionary<string, object> globals
            = new Dictionary<string, object>
        {
            { "obj-prop", "dog" },
            { "animal", "dog" },
            { "prep", "then" },
            { "group", "(a|b)" },
            { "cmplx", "($group | $prep)" },
            { "count", 4 }
        };

        [Test]
        public void EmptyGlobalScope()
        {
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            Assert.Throws<UnboundSymbolException>(() => 
                Resolver.BindSymbols("$animal", c1, null));
        }

        [Test]
        public void EmptyGlobalLocalScope()
        {
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            Assert.Throws<UnboundSymbolException>(() => 
                Resolver.BindSymbols("$animal", null, null));
        }

        [Test]
        public void SimpleGlobalScope()
        {
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            var res = Resolver.BindSymbols("$animal", c1, globals);
            Assert.That(res, Is.EqualTo("dog"));
        }

        [Test]
        public void ComplexGlobalScope()
        {
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            var res = Resolver.BindSymbols("$cmplx", c1, globals);
            Assert.That(res, Is.EqualTo("((a|b) | then)"));
            //Assert.That(res, Is.EqualTo("a").Or.EqualTo("b").Or.EqualTo("then"));
        }

        [Test]
        public void SimpleLocalScope()
        {
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            c1.scope.Add("a", "b");
            var res = Resolver.BindSymbols("$a", c1, globals);
            Assert.That(res, Is.EqualTo("b"));
        }

        [Test]
        public void ComplexLocalScope()
        {
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            c1.scope.Add("a", "$b");
            c1.scope.Add("b", "c");
            var res = Resolver.BindSymbols("$a", c1, globals);
            Assert.That(res, Is.EqualTo("c"));
        }

        [Test]
        public void CrossLocalScope()
        {
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            Chat c2 = rt.AddNewChat("c2");
            c1.scope.Add("a", "b");
            //var res = c2.Realizer().Do("$c1.a", globals, c2);
            var res = Resolver.BindSymbols("$c1.a", c2, globals);
            Assert.That(res, Is.EqualTo("b"));
        }

        [Test]
        public void CrossLocalGlobal()
        {
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            Chat c2 = rt.AddNewChat("c2");
            c1.scope.Add("a", "$animal");
            var res = Resolver.BindSymbols("$c1.a", c2, globals);
            Assert.That(res, Is.EqualTo("dog"));
        }

        [Test]
        public void CrossLocalGlobalPhrase()
        {
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            Chat c2 = rt.AddNewChat("c2");
            c1.scope.Add("a", "The $animal ate");
            var res = Resolver.BindSymbols("$c1.a", c2, globals);
            Assert.That(res, Is.EqualTo("The dog ate"));
        }

        [Test]
        public void CrossLocalGlobalPhrases()
        {
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            Chat c2 = rt.AddNewChat("c2");
            c1.scope.Add("a", "The $animal ate $prep");
            var res = Resolver.BindSymbols("$c1.a", c2, globals);
            Assert.That(res, Is.EqualTo("The dog ate then"));
        }

        [Test]
        public void CrossLocalAndBack()
        {
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            c1.scope.Add("a", "$c2.a");
            Chat c2 = rt.AddNewChat("c2");
            c2.scope.Add("a", "b");
            var res = Resolver.BindSymbols("$c1.a", c2, globals);
            Assert.That(res, Is.EqualTo("b"));
        }


    }
}