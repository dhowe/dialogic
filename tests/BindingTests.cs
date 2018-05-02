using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Dialogic
{
    [TestFixture]
    class BindingTests
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
            { "count", 4 },
            { "fish",  new Fish("Fred")}
        };

        class Fish
        {
            public string name { get; protected set; }
            public Flipper flipper { get; protected set; }

            public Fish(string name)
            {
                this.name = name;
                this.flipper = new Flipper(1.1);
            }
        }

        class Flipper
        {
            public double speed { get; protected set; }
            public Flipper(double s)
            {
                this.speed = s;
            }
        }


        [Test]
        public void SimpleSetExpansions()
        {
            string[] lines;
            string text;
            Chat chat;

            // local
            lines = new[] {
                "CHAT c1",
                "SET ab = hello",
                "SAY $ab",
            };
            text = String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0];chat.Realize(globals);
            Assert.That(chat.commands[1].Text(), Is.EqualTo("hello"));

            // global-miss
            lines = new[] {
                "CHAT c1",
                "SET ab = hello",
                "SAY $ab",
            };
            text = String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0];chat.Realize(globals);
            Assert.That(chat.commands[1].Text(), Is.EqualTo("hello"));

            // global-hit
            lines = new[] {
                "CHAT c1",
                "SAY $animal",
            };
            text = String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0];chat.Realize(globals);
            Assert.That(chat.commands[0].Text(), Is.EqualTo("dog"));

            // global-properties
            lines = new[] {
                "CHAT c1",
                "SAY $fish.name",
            };
            text = String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0];chat.Realize(globals);
            Assert.That(chat.commands[0].Text(), Is.EqualTo("Fred"));

            // global-bounded
            lines = new[] {
                "CHAT c1",
                "SAY ${fish.name}",
            };
            text = String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0];chat.Realize(globals);
            Assert.That(chat.commands[0].Text(), Is.EqualTo("Fred"));

            // global-nested
            lines = new[] {
                "CHAT c1",
                "SAY $fish.flipper.speed",
            };
            text = String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0];chat.Realize(globals);
            Assert.That(chat.commands[0].Text(), Is.EqualTo("1.1"));

            // global-nested-bounded
            lines = new[] {
                "CHAT c1",
                "SAY ${fish.flipper.speed}",
            };
            text = String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0];chat.Realize(globals);
            Assert.That(chat.commands[0].Text(), Is.EqualTo("1.1"));

            // cross-chat-global
            lines = new[] {
                "CHAT c1",
                "SET $c1_ab = hello",
                "CHAT c2",
                "SAY $c1_ab",
            };
            text = String.Join("\n", lines);
            var chats = ChatParser.ParseText(text, true);
            chats.ForEach(c => c.Realize(globals));
            Assert.That(chats[1].commands[0].Text(), Is.EqualTo("hello"));


            return; // TODO: add chats to globals, remove special-case code 

            // chat-direct access
            lines = new[] {
                "CHAT c1",
                "SET foo=bar",
                "CHAT c2",
                "SAY $chats.c1.foo",
            };
            text = String.Join("\n", lines);
            chats = ChatParser.ParseText(text, true);
            chats.ForEach(c => c.Realize(globals));
            Assert.That(chats[1].commands[0].Text(), Is.EqualTo("bar"));

            // chat-direct bounded
            lines = new[] {
                "CHAT c1",
                "SET foo=bar",
                "CHAT c2",
                "SAY ${chats.c1.foo}",
            };
            text = String.Join("\n", lines);
            chats = ChatParser.ParseText(text, true);
            chats.ForEach(c => c.Realize(globals));
            Assert.That(chats[1].commands[0].Text(), Is.EqualTo("bar"));
        }

        [Test]
        public void SimpleSymbolTraversal()
        {
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            var res = Resolver.Bind("Hello $fish.name", c1, globals);
            Assert.That(res, Is.EqualTo("Hello Fred"));

            res = Resolver.Bind("Hello $fish.name.", c1, globals);
            Assert.That(res, Is.EqualTo("Hello Fred."));
        }

        [Test]
        public void EmptyGlobalScope()
        {
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            Assert.Throws<UnboundSymbol>(() =>
                Resolver.Bind("$animal", c1, null));
        }

        [Test]
        public void EmptyGlobalLocalScope()
        {
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            Assert.Throws<UnboundSymbol>(() =>
                Resolver.Bind("$animal", c1, null));
        }

        [Test]
        public void SimpleGlobalScope()
        {
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            var res = Resolver.Bind("$animal", c1, globals);
            Assert.That(res, Is.EqualTo("dog"));
        }

        [Test]
        public void ComplexGlobalScope()
        {
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            var res = Resolver.Bind("$cmplx", c1, globals);
            Assert.That(res, Is.EqualTo("a").Or.EqualTo("b").Or.EqualTo("then"));
        }

        [Test]
        public void SimpleLocalScope()
        {
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            c1.scope.Add("a", "b");
            var res = Resolver.Bind("$a", c1, globals);
            Assert.That(res, Is.EqualTo("b"));
        }

        [Test]
        public void ComplexLocalScope()
        {
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            c1.scope.Add("a", "$b");
            c1.scope.Add("b", "c");
            var res = Resolver.Bind("$a", c1, globals);
            Assert.That(res, Is.EqualTo("c"));
        }

        /*
        [Test]
        public void CrossLocalScope()
        {
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            Chat c2 = rt.AddNewChat("c2");
            c1.scope.Add("a", "b");
            //var res = c2.Realizer().Do("#c1.a", globals, c2);
            var res = Resolver.Bind("#c1.a", c2, globals);
            Assert.That(res, Is.EqualTo("b"));
        }

        [Test]
        public void CrossLocalGlobal()
        {
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            Chat c2 = rt.AddNewChat("c2");
            c1.scope.Add("a", "$animal");
            var res = Resolver.Bind("#c1.a", c2, globals);
            Assert.That(res, Is.EqualTo("dog"));
        }

        [Test]
        public void CrossLocalGlobalPhrase()
        {
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            Chat c2 = rt.AddNewChat("c2");
            c1.scope.Add("a", "The $animal ate");
            var res = Resolver.Bind("#c1.a", c2, globals);
            Assert.That(res, Is.EqualTo("The dog ate"));
        }

        [Test]
        public void CrossLocalGlobalPhrases()
        {
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            Chat c2 = rt.AddNewChat("c2");
            c1.scope.Add("a", "The $animal ate $prep");
            var res = Resolver.Bind("#c1.a", c2, globals);
            Assert.That(res, Is.EqualTo("The dog ate then"));
        }

        [Test]
        public void CrossLocalAndBack()
        {
            ChatRuntime rt = new ChatRuntime();
            Chat c1 = rt.AddNewChat("c1");
            c1.scope.Add("a", "#c2.a");
            Chat c2 = rt.AddNewChat("c2");
            c2.scope.Add("a", "b");
            var res = Resolver.Bind("#c1.a", c2, globals);
            Assert.That(res, Is.EqualTo("b"));
        }*/


    }
}