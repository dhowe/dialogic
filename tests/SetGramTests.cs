using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Dialogic;
using NUnit.Framework;

namespace Dialogic
{
    [TestFixture]
    class SetGrammarTests
    {
        const bool NO_VALIDATORS = true;

        static IDictionary<string, object> globals
            = new Dictionary<string, object>
        {
            { "obj.prop", "dog" },
            { "animal", "dog" },
            { "prep", "then" },
            { "group", "(a|b)" },
            { "cmplx", "($group | $prep)" },
            { "count", 4 }
        };

        [Test]
        public void SimpleSets()
        {
            Chat chat;
            Set set;

            chat = ChatParser.ParseText("CHAT c1\nSET a= 4", NO_VALIDATORS)[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            set = (Dialogic.Set)chat.commands[0];
            Assert.That(set.text, Is.EqualTo("a"));
            Assert.That(set.op, Is.EqualTo(AssignOp.EQ));
            Assert.That(set.value, Is.EqualTo("4"));
            //set.Realize(globals);

            chat = ChatParser.ParseText("CHAT c1\nSET $a = 4", NO_VALIDATORS)[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            set = (Dialogic.Set)chat.commands[0];
            Assert.That(set.text, Is.EqualTo("a"));
            Assert.That(set.op, Is.EqualTo(AssignOp.EQ));
            Assert.That(set.value, Is.EqualTo("4"));
            set.Realize(globals);

            chat = ChatParser.ParseText("CHAT c1\nSET a =4", NO_VALIDATORS)[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            set = (Dialogic.Set)chat.commands[0];
            Assert.That(set.text, Is.EqualTo("a"));
            Assert.That(set.op, Is.EqualTo(AssignOp.EQ));
            Assert.That(set.value, Is.EqualTo("4"));
            set.Realize(globals);
            object outv = null;
            globals.TryGetValue("a", out outv);
            Assert.That(outv, Is.Null);
            Assert.That(globals["c1.a"], Is.EqualTo("4"));

            chat = ChatParser.ParseText("CHAT c1\nSET $a = 4", NO_VALIDATORS)[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            set = (Dialogic.Set)chat.commands[0];
            Assert.That(set.text, Is.EqualTo("a"));
            Assert.That(set.op, Is.EqualTo(AssignOp.EQ));
            Assert.That(set.value, Is.EqualTo("4"));
            set.Realize(globals);
        }

        [Test]
        public void SimpleSetsWithVars()
        {
            Chat chat;
            Set set;

            chat = ChatParser.ParseText("CHAT c1\nSET a= $obj.prop", NO_VALIDATORS)[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            set = (Dialogic.Set)chat.commands[0];
            Assert.That(set.text, Is.EqualTo("a"));
            Assert.That(set.op, Is.EqualTo(AssignOp.EQ));
            Assert.That(set.value, Is.EqualTo("$obj.prop"));
            set.Realize(globals);
            Assert.That(globals["c1.a"], Is.EqualTo("$obj.prop"));
        }

        public void SimpleSetsWithOr()
        {
            Chat chat;
            Set set;

            chat = ChatParser.ParseText("CHAT c1\nSET $a = (4 | 5)", NO_VALIDATORS)[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            set = (Dialogic.Set)chat.commands[0];
            Assert.That(set.text, Is.EqualTo("a"));
            Assert.That(set.op, Is.EqualTo(AssignOp.EQ));
            Assert.That(set.value, Is.EqualTo("(4 | 5)"));
            set.Realize(globals);
            Assert.That(globals["c1.a"], Is.EqualTo("4").Or.EqualTo("5"));

            chat = ChatParser.ParseText("CHAT c1\nSET a ( 4 | 5 )", NO_VALIDATORS)[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            set = (Dialogic.Set)chat.commands[0];
            Assert.That(set.text, Is.EqualTo("a"));
            Assert.That(set.op, Is.EqualTo(AssignOp.EQ));
            Assert.That(set.value, Is.EqualTo("( 4 | 5 )"));
            set.Realize(globals);
            object outv = null;
            globals.TryGetValue("a", out outv);
            Assert.That(outv, Is.Null);
            Assert.That(globals["c1.a"], Is.EqualTo("4").Or.EqualTo("5"));

            chat = ChatParser.ParseText("CHAT c1\nSET $a (4 |5 )", NO_VALIDATORS)[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            set = (Dialogic.Set)chat.commands[0];
            Assert.That(set.text, Is.EqualTo("a"));
            Assert.That(set.op, Is.EqualTo(AssignOp.EQ));

            Assert.That(set.value, Is.EqualTo("(4 |5 )"));
            set.Realize(globals);
            Assert.That(globals["c1.a"], Is.EqualTo("4").Or.EqualTo("5"));

        }

        [Test]
        public void SetRules1()
        {
            string[] lines = {
                "CHAT WineReview {type=a,stage=b}",
                "SET review=<desc> <fortune> <ending>",
                //"SET ending <score> | <end-phrase>", // causes hang
                "SET desc=You look tasty: gushing blackberry into the rind of day-old ennui.",
                "SET fortune=Under your skin, tears undulate like a leaky eel.",
                "SET ending=And thats the end of the story...",
                "SAY $WineReview.review",
            };
            var chat = ChatParser.ParseText(String.Join("\n", lines))[0];

            var last = chat.commands[chat.commands.Count - 1];
            Assert.That(last, Is.Not.Null);
            Assert.That(last.GetType(), Is.EqualTo(typeof(Say)));

            chat.commands.ForEach(c => c.Realize(globals));

            Assert.That(globals.ContainsKey("WineReview.review"), Is.True);
            Assert.That(globals.ContainsKey("WineReview.ending"), Is.True);

            Say say = (Dialogic.Say)last;
            //Console.WriteLine(chat.ToTree()+"\nSAY: "+say.Text(true));

            Assert.That(say.Text(true), Is.EqualTo("You look tasty: gushing blackberry into the rind of day-old ennui. Under your skin, tears undulate like a leaky eel. And thats the end of the story..."));
        }

        [Test]
        public void SetRulesWithOrs()
        {
            string[] lines = {
                "CHAT c1 {type=a,stage=b}",
                "SET review=<greeting>",
                "SET greeting=(Hello | Goodbye)",
                "SAY $review",
            };
            var chat = ChatParser.ParseText(String.Join("\n", lines))[0];
            //Console.WriteLine(chat.ToTree());
            var last = chat.commands[chat.commands.Count - 1];
            Assert.That(last, Is.Not.Null);
            Assert.That(last.GetType(), Is.EqualTo(typeof(Say)));

            chat.commands.ForEach(c => c.Realize(globals));

            Assert.That(globals.ContainsKey("c1.review"), Is.True);
            Assert.That(globals.ContainsKey("c1.greeting"), Is.True);

            Say say = (Dialogic.Say)last;

            for (int i = 0; i < 10; i++)
            {
                say.Realize(globals);
                //Console.WriteLine(say.Text(true));
                Assert.That(say.Text(true), Is.EqualTo("Hello").Or.EqualTo("Goodbye"));
            }
        }

        [Test]
        public void SetDefaultCommand()
        {
            string[] lines = {
                "CHAT c1 {type=a,stage=b,defaultCmd=SET}",
                "review=<greeting>",
                "greeting=(Hello | Goodbye)",
                "SAY $review",
            };

            var chat = ChatParser.ParseText(String.Join("\n", lines))[0];
            //Console.WriteLine(chat.ToTree());

            for (int i = 0; i < 10; i++)
            {
                chat.commands.ForEach(c => c.Realize(globals));
                Assert.That(((Dialogic.Say)chat.commands.Last()).Text(true),
                    Is.EqualTo("Hello").Or.EqualTo("Goodbye"));
                //Console.WriteLine(i+")" + );
            }
        }

        [Test]
        public void SetGrammarMode()
        {
            string[] lines = {
                "CHAT c1 {chatMode=grammar}",
                "review = <greeting>",
                "greeting = (Hello | Goodbye)",
                "",
                "CHAT c2",
                "$c1.review",
            };

            var chats = ChatParser.ParseText(String.Join("\n", lines), true);
            chats[0].commands.ForEach(c => c.Realize(globals));

            for (int i = 0; i < 10; i++)
            {
                chats[1].commands.ForEach(c => c.Realize(globals));
                var txt = chats[1].commands.Last().Text(true);
                Assert.That(txt, Is.EqualTo("Hello").Or.EqualTo("Goodbye"));
                //Console.WriteLine(i+") "+txt);
            }

        }

        [Test]
        public void SetOrEquals()
        {
            string[] lines = {
                "CHAT c1 {type=a,stage=b}",
                "SET review=<greeting>",
                "SET greeting = Hello | Goodbye",
                "SET greeting |= See you later",
                "SAY $review",
            };
            var chat = ChatParser.ParseText(String.Join("\n", lines))[0];
            //Console.WriteLine(chat.ToTree());
            var last = chat.commands[chat.commands.Count - 1];
            Assert.That(last, Is.Not.Null);
            Assert.That(last.GetType(), Is.EqualTo(typeof(Say)));
            //chat.Realize(globals);
            chat.commands.ForEach(c => c.Realize(globals));

            Assert.That(globals.ContainsKey("c1.review"), Is.True);
            Assert.That(globals.ContainsKey("c1.greeting"), Is.True);
            Assert.That(globals["c1.greeting"],
                        Is.EqualTo("Hello | Goodbye | See you later"));

            Say say = (Dialogic.Say)last;

            for (int i = 0; i < 10; i++)
            {
                say.Realize(globals);
                var said = say.Text(true);
                //Console.WriteLine(i+") "+said);
                Assert.That(said, Is.EqualTo("Hello")
                    .Or.EqualTo("Goodbye").Or.EqualTo("See you later"));
            }
        }

        [Test]
        public void SetPlusEquals()
        {
            string[] lines = {
                "CHAT c1 {type=a,stage=b}",
                "SET review=<greeting>",
                "SET greeting = (Hello | Goodbye)",
                "SET greeting += Fred",
                "SAY $review",
            };
            var chat = ChatParser.ParseText(String.Join("\n", lines))[0];
            //Console.WriteLine(chat.ToTree());
            var last = chat.commands[chat.commands.Count - 1];
            Assert.That(last, Is.Not.Null);
            Assert.That(last.GetType(), Is.EqualTo(typeof(Say)));

            chat.commands.ForEach(c => c.Realize(globals));

            //DumpGlobals();

            Assert.That(globals.ContainsKey("c1.review"), Is.True);
            Assert.That(globals.ContainsKey("c1.greeting"), Is.True);
            Assert.That(globals["c1.greeting"], Is.EqualTo("(Hello | Goodbye) Fred"));

            Say say = (Dialogic.Say)last;
            for (int i = 0; i < 10; i++)
            {
                say.Realize(globals);
                var said = say.Text(true);
                //Console.WriteLine(i + ") " + said);
                Assert.That(said, Is.EqualTo("Hello Fred")
                    .Or.EqualTo("Goodbye Fred"));
            }
        }

        private static void DumpGlobals(string s = null)
        {
            Console.WriteLine("\nGLOBALS:");
            foreach (var k in globals.Keys)
                Console.WriteLine("  " + k + ": " + globals[k]);
            if (s != null) Console.WriteLine(s);
        }

        [Test]
        public void WineReview()
        {
            string[] lines = {
                "CHAT wine1 {noStart=true,chatMode=grammar}",
                "review =  <desc> <fortune> <ending>",
                "ending =  <score> | <end1>",
                "ending |=  <end2>",
                "desc =  Your expression is a \"(Plop|Fizz|Fail)\".",
                "desc +=  But, you don’t care do you? No, you don’t.",
                "fortune = How about a slap to reinstill your faith in humanity?",
                "score =  I'd have to rate this a 2. Try again with 'feeling'...",
                "end1 = There’s always time for a cold shower.",
                "end2 = Just give up please.",
                "SAY $review {speed=fast}"
            };

            var chat = ChatParser.ParseText(String.Join("\n", lines))[0];

            chat.commands.ForEach(c => c.Realize(globals));
            Console.WriteLine(chat.AsGrammar(globals), false);

            Say say = (Say)chat.commands.Last();//.Realize()

            for (int i = 0; i < 1; i++)
            {
                var said = say.Realize(globals).Text(true);
                Console.WriteLine(i + ") " + said);
                Assert.That(said.StartsWith("Your expression is a", Util.IC), Is.True);
                Assert.That
                      (said.EndsWith("Try again with 'feeling'...", Util.IC)
                    || said.EndsWith("time for a cold shower.", Util.IC)
                    || said.EndsWith("Just give up please.", Util.IC), Is.True);
            }
        }

        [Test]
        public void FullGrammar()
        {
            string[] lines = {
                "CHAT full {noStart=true,chatMode=grammar}",
                "start =  <a> <b> <c>",
                "a = A",// | A $a",
                "b = B",
                "c = D",
                "c |= E",
                "SAY $start"
            };

            // NEXT: move last paren in invoke

            var chat = ChatParser.ParseText(String.Join("\n", lines))[0];

            chat.commands.ForEach(c => {if (!(c is Say)) c.Realize(globals);});
            Console.WriteLine(chat.AsGrammar(globals, false));
            Console.WriteLine(chat.Expand(globals, "$start"));
            return;
            Say say = (Say)chat.commands.Last();//.Realize()

            for (int i = 0; i < 10; i++)
            {
                var said = say.Realize(globals).Text(true);
                Console.WriteLine(i + ") " + said);
                //Assert.That(said, Is.EqualTo("A B C"));
            }
        }

        [Test]
        public void SimpleGrammarExpand()
        {
            string[] lines;
            string text;
            Chat chat;

            lines = new[] {
                "start =  <a> <b> <c>",
                "a = A",
                "b = B",
                "c = C",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat.Expand(globals, "$start"), Is.EqualTo("A B C"));

            lines = new[]{
                "start =  <a> <b>",
                "a = A",
                "b = <c>",
                "c = B <d>",
                "d = C",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat.Expand(globals, "$start"), Is.EqualTo("A B C"));

            lines = new[]{
                "start =  <a> <b> <c>",
                "a = A",
                "b = <c>",
                "c = B <d>",
                "d = C",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat.Expand(globals, "$start"), Is.EqualTo("A B C B C"));

            lines = new[]{
                "start =  <a> ",
                "a = <b>",
                "b = <c>",
                "c = D",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat.Expand(globals, "$start"), Is.EqualTo("D"));

            lines = new[]{
                "start =  <a> <b>",
                "a = <c> <d>",
                "b = <d> <c>",
                "c = C",
                "d = D",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat.Expand(globals, "$start"), Is.EqualTo("C D D C"));
        }


        // NEXT: change all these to ExpandNoGroups -----------------

        [Test]
        public void GrammarExpandWithOr()
        {
            string[] lines;
            string text;
            Chat chat;

            lines = new[] {
                "start =  <a> <b> <c>",
                "a = A",
                "b = B",
                "c = C | D",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat.Expand(globals, "$start"), Is.EqualTo("A B C | D"));

            lines = new[] {
                "start =  <a> <b> <c>",
                "a = A",
                "b = B",
                "c = (C | D)",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat.Expand(globals, "$start"), Is.EqualTo("A B (C | D)"));

            lines = new[] {
                "start =  <a> <b> <c>",
                "a = A",
                "b = B",
                "c = C",
                "c += D",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat.Expand(globals, "$start"), Is.EqualTo("A B C D"));

            lines = new[] {
                "start =  <a> <b> <c>",
                "a = A",
                "b = B",
                "c = C",
                "c += | D",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat.Expand(globals, "$start"), Is.EqualTo("A B C | D"));

            lines = new[] {
                "start =  <a> <b> <c>",
                "a = A",
                "b = B",
                "c = (C",
                "c += | D)",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat.Expand(globals, "$start"), Is.EqualTo("A B (C | D)"));

            lines = new[] {
                "start =  <a> <b> <c>",
                "a = A",
                "b = B",
                "c = (C",
                "c += | D",
                "c += | E)",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat.Expand(globals, "$start"), Is.EqualTo("A B (C | D | E)"));

            lines = new[] {
                "start =  <a> (<b> <c>)",
                "a = A",
                "b = B",
                "c = (C | D)",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat.Expand(globals, "$start"), Is.EqualTo("A (B (C | D))"));

            lines = new[] {
                "start =  <a> <b> <c>",
                "a = A",
                "b = B",
                "c = (<a> | <b>)",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Console.WriteLine(chat.Expand(globals, "$start"));
            Assert.That(chat.Expand(globals, "$start"), Is.EqualTo("A B (A | B)"));

            /* infinite loop
            lines = new[] {
                "start =  <a> <b> <c>",
                "a = <b>",
                "b = <c>",
                "c = (<a> | <b> | D)",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Console.WriteLine(chat.Expand(globals, "$start"));
            Assert.That(chat.Expand(globals, "$start"), Is.EqualTo("A B (A | B)")); */
        }

        [Test]
        public void GrammarExpandWithOrEquals()
        {
            string[] lines;
            string text;
            Chat chat;

            lines = new[] {
                "start =  <a> <b> <c>",
                "a = A",
                "b = B",
                "c = C",
                "c |= D",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat.Expand(globals, "$start"), Is.EqualTo("A B (C | D)"));

            lines = new[] {
                "start =  <a> <b> <c>",
                "a = A",
                "b = B",
                "c = C | D",
                "c |= E",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat.Expand(globals, "$start"), Is.EqualTo("A B (C | D | E)"));

            lines = new[] {
                "start =  <a> <b> <c>",
                "a = A",
                "b = B",
                "c = (C | D)",
                "c |= E",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat.Expand(globals, "$start"), Is.EqualTo("A B (C | D | E)"));

            lines = new[] {
                "start =  <a> <b> <c>",
                "a = A",
                "b = B",
                "c = C (C | D)",
                "c |= E",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat.Expand(globals, "$start"), Is.EqualTo("A B (C (C | D) | E)"));
        }

        [Test]
        public void GrammarExpandWithOrEqualsDoGroups()
        {
            string[] lines;
            string text;
            Chat chat;

            lines = new[] {
                "start =  <a> <b> <c>",
                "a = A",
                "b = B",
                "c = C",
                "c |= D",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat.Expand(globals, "$start"), Is.EqualTo("A B C").Or.EqualTo("A B D"));
 
            lines = new[] {
                "start =  <a> <b> <c>",
                "a = A",
                "b = B",
                "c = C | D",
                "c |= E",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat.Expand(globals, "$start"), Is.EqualTo("A B C").Or.EqualTo("A B D").Or.EqualTo("A B E"));

            lines = new[] {
                "start =  <a> <b> <c>",
                "a = A",
                "b = B",
                "c = (C | D)",
                "c |= E",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat.Expand(globals, "$start"), Is.EqualTo("A B C").Or.EqualTo("A B D").Or.EqualTo("A B E"));

            lines = new[] {
                "start =  <a> <b> <c>",
                "a = A",
                "b = B",
                "c = C (C | D)",
                "c |= E",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            //Assert.That(chat.Expand(globals, "$start"), Is.EqualTo("A B (C (C | D) | E)"));
            Assert.That(chat.Expand(globals, "$start"), Is.EqualTo("A B E").Or.EqualTo("A B C C").Or.EqualTo("A B C D"));
        }
    }
}
