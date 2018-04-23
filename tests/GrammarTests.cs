using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Dialogic
{
    [TestFixture]
    class GrammarTests
    {
        void HandleFunc()
        {
        }


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
            public int Id() { return 9; }
            public Fish(string name)
            {
                this.name = name;
            }
        }

        [Test]
        public void SaveGlobalResolveState()
        {
            string[] lines;
            ChatRuntime runtime;
            Chat chat;
            string res;

            lines = new[] {
                "SAY A girl [selected=$fish.name] $selected",
            };
            runtime = new ChatRuntime();
            runtime.ParseText(string.Join("\n", lines));
            chat = runtime.Chats()[0];
            Assert.That(chat, Is.Not.Null);
            chat.Realize(globals);
            res = chat.commands[0].Text();
            Assert.That(res, Is.EqualTo("A girl Fred Fred"));
        }

        [Test]
        public void ResolveWithAlias()
        {
            string[] lines;
            ChatRuntime runtime;
            Chat chat;
            string res;

            lines = new[] {
                "SAY The girl was $fish.Id().",
            };
            runtime = new ChatRuntime();
            runtime.ParseText(string.Join("\n", lines));
            chat = runtime.Chats()[0];
            //Console.WriteLine(chat.ToTree());
            Assert.That(chat, Is.Not.Null);
            chat.Realize(globals);
            res = chat.commands[0].Text();
            Assert.That(res, Is.EqualTo("The girl was 9."));

            lines = new[] {
                "SAY A girl [selected=$fish.Id()] $selected",
            };
            runtime = new ChatRuntime();
            runtime.ParseText(string.Join("\n", lines));
            chat = runtime.Chats()[0];
            Assert.That(chat, Is.Not.Null);
            chat.Realize(globals);
            res = chat.commands[0].Text();
            Assert.That(res, Is.EqualTo("A girl 9 9"));
        }

        [Test]
        public void ResolveWithArticlize2()
        {
            string[] lines;
            ChatRuntime runtime;
            Chat chat;
            string res, last = null;

            lines = new[] {
                "SET hero = artist",
                "SAY She was $hero.articlize().",
            };
            runtime = new ChatRuntime();
            runtime.ParseText(string.Join("\n", lines));
            chat = runtime.Chats()[0];
            Assert.That(chat, Is.Not.Null);
            chat.Realize(null);
            res = chat.commands[1].Text();
            //Console.WriteLine(res);
            Assert.That(res, Is.EqualTo("She was an artist."));


            lines = new[] {
                "SET hero = (animal | artist | person | banker)",
                "SAY She was $hero.articlize().",
            };
            runtime = new ChatRuntime();
            runtime.ParseText(string.Join("\n", lines));
            chat = runtime.Chats()[0];
            Assert.That(chat, Is.Not.Null);
            for (int i = 0; i < 5; i++)
            {
                chat.Realize(null);
                res = chat.commands[1].Text();
                Assert.That(res, Is.Not.EqualTo(last));
                Assert.That(res, Is.EqualTo("She was an artist.").
                                 Or.EqualTo("She was an animal.").
                                 Or.EqualTo("She was a person.").
                                 Or.EqualTo("She was a banker."));
                last = res;
            }
        }

        [Test]
        public void SaveResolveState()
        {
            string[] lines;
            ChatRuntime runtime;
            Chat chat;
            string res;


            lines = new[] {
                "SET hero = (Jane | Jill)",
                "SAY A girl [selected=$hero]&nbsp;",
                "SAY $selected"
            };
            runtime = new ChatRuntime();
            runtime.ParseText(string.Join("\n", lines));
            chat = runtime.Chats()[0];
            Assert.That(chat, Is.Not.Null);
            chat.Realize(null);
            res = chat.commands[1].Text() + chat.commands[2].Text();
            Assert.That(res, Is.EqualTo("A girl Jane Jane").
                             Or.EqualTo("A girl Jill Jill"));

            lines = new[] {
                "SET hero = (Jane | Jill)",
                "SAY A girl [a=$hero]&nbsp;",
                "SAY $a"
            };
            runtime = new ChatRuntime();
            runtime.ParseText(string.Join("\n", lines));
            chat = runtime.Chats()[0];
            Assert.That(chat, Is.Not.Null);
            chat.Realize(null);
            res = chat.commands[1].Text() + chat.commands[2].Text();
            Assert.That(res, Is.EqualTo("A girl Jane Jane").
                             Or.EqualTo("A girl Jill Jill"));
            //Console.WriteLine("2: "+chat + " " + chat.scope.Stringify() 
            //+ "\nglobals=" + globals.Stringify());

            lines = new[] {
                "SET hero = (Jane | Jill)",
                "SAY A girl [selected=$hero]&nbsp;",
                "SAY $selected."
            };
            runtime = new ChatRuntime();
            runtime.ParseText(string.Join("\n", lines));
            chat = runtime.Chats()[0];
            Assert.That(chat, Is.Not.Null);
            chat.Realize(null);
            res = chat.commands[1].Text() + chat.commands[2].Text();
            Assert.That(res, Is.EqualTo("A girl Jane Jane.").
                             Or.EqualTo("A girl Jill Jill."));


            lines = new[] {
                "SET hero = (Jane | Jill)",
                "SAY A girl [selected=${hero}]&nbsp;",
                "SAY $selected."
            };
            runtime = new ChatRuntime();
            runtime.ParseText(string.Join("\n", lines));
            chat = runtime.Chats()[0];
            Assert.That(chat, Is.Not.Null);
            chat.Realize(null);
            res = chat.commands[1].Text() + chat.commands[2].Text();
            Assert.That(res, Is.EqualTo("A girl Jane Jane.").
                             Or.EqualTo("A girl Jill Jill."));

            lines = new[] {
                "SET hero = (Jane | Jill)",
                "SAY A girl [selected=$hero] $selected."
            };

            runtime = new ChatRuntime();
            runtime.ParseText(string.Join("\n", lines));
            chat = runtime.Chats()[0];
            Assert.That(chat, Is.Not.Null);
            chat.Realize(null);
            res = chat.commands[1].Text();// + chat.commands[2].Text();
            Assert.That(res, Is.EqualTo("A girl Jane Jane.").
                                         Or.EqualTo("A girl Jill Jill."));

            lines = new[] {
                "SET hero = (Jane | Jill)",
                "SAY A girl [selected=${hero}] ${selected}."
            };

            runtime = new ChatRuntime();
            runtime.ParseText(string.Join("\n", lines));
            chat = runtime.Chats()[0];
            Assert.That(chat, Is.Not.Null);
            chat.Realize(null);
            res = chat.commands[1].Text();// + chat.commands[2].Text();
            Assert.That(res, Is.EqualTo("A girl Jane Jane.").
                                     Or.EqualTo("A girl Jill Jill."));
        }

        [Test]
        public void RealizeSubstringSymbols()
        {
            var lines = new[] {
                "CHAT wine1 {noStart=true}",
                "SET a = $a2",
                "SET a2 = C",
                "SAY $a $a2"
            };
            ChatRuntime runtime = new ChatRuntime(Tendar.AppConfig.Actors);
            runtime.ParseText(string.Join("\n", lines), false);
            var chat = runtime.Chats()[0];

            runtime.Chats().ForEach(c => c.Realize(null));

            //Console.WriteLine(chat.ToTree() + "\n" + chat.locals.Stringify());

            Say say = (Dialogic.Say)runtime.Chats().Last().commands.Last();
            var result = say.Text();
            Assert.That(result, Is.EqualTo("C C"));
        }


        [Test]
        public void RepeatedExpand()
        {
            string[] lines = {
                "CHAT myGrammar {defaultCmd=SET}",
                "start = $subject $verb $object.",
                "subject = I | You | They",
                "object = coffee | bread | milk",
                "verb = want | hate | like | love",
                "SAY $start",

            };
            ChatRuntime rt = new ChatRuntime();
            rt.ParseText(String.Join("\n", lines));
            Chat chat = rt.Chats()[0];

            Say say = (Dialogic.Say)chat.commands.Last();

            chat.commands.ForEach(c => c.Realize(globals));

            var results = new HashSet<string>();
            for (int i = 0; i < 10; i++)
            {
                results.Add(say.Realize(globals).Text());
            }

            Assert.That(results.Count, Is.GreaterThan(1));
        }

        [Test]
        public void RepeatedSymbolTest()
        {
            string[] lines = {
                "CHAT myGrammar {defaultCmd=SET}",
                "start = $subject verb object.",
                "subject = I | You | They",
                "object = coffee | bread | milk",
                "verb = want | hate | like | love",
                "SAY $start $start $start $start $start $start $start $start",
            };
            ChatRuntime rt = new ChatRuntime();
            rt.ParseText(String.Join("\n", lines));

            rt.Chats()[0].commands.ForEach(c => c.Realize(globals));
            var all = rt.Chats()[0].commands.Last().Text();
            var says = all.Split('.');

            var results = new HashSet<string>();
            for (int i = 0; i < says.Length; i++)
            {
                if (says[i].IsNullOrEmpty()) continue;
                results.Add(says[i].Trim());

            }

            Assert.That(results.Count, Is.GreaterThan(1));
        }


        [Test]
        public void SimpleSets()
        {
            Chat chat;
            Set set;

            chat = ChatParser.ParseText("CHAT c1\nSET $a = 4", NO_VALIDATORS)[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            set = (Dialogic.Set)chat.commands[0];
            Assert.That(set.text, Is.EqualTo("a"));
            Assert.That(set.op, Is.EqualTo(Assignment.EQ));
            Assert.That(set.value, Is.EqualTo("4"));
            set.Realize(globals);
            object outv = null;
            chat.scope.TryGetValue("a", out outv);
            Assert.That(outv, Is.Null);
            Assert.That(globals["a"], Is.EqualTo("4"));
            globals.Remove("a");

            chat = ChatParser.ParseText("CHAT c1\nSET a =4", NO_VALIDATORS)[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            set = (Dialogic.Set)chat.commands[0];
            Assert.That(set.text, Is.EqualTo("a"));
            Assert.That(set.op, Is.EqualTo(Assignment.EQ));
            Assert.That(set.value, Is.EqualTo("4"));
            set.Realize(globals);
            outv = null;
            globals.TryGetValue("a", out outv);
            Assert.That(outv, Is.Null);
            Assert.That(chat.scope["a"], Is.EqualTo("4"));
        }

        [Test]
        public void SimpleSetsWithVars()
        {
            Chat chat;
            Set set;

            chat = ChatParser.ParseText("CHAT c1\nSET a= $obj-prop", NO_VALIDATORS)[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            set = (Dialogic.Set)chat.commands[0];
            Assert.That(set.text, Is.EqualTo("a"));
            Assert.That(set.op, Is.EqualTo(Assignment.EQ));
            Assert.That(set.value, Is.EqualTo("$obj-prop"));
            set.Realize(globals);
            Assert.That(chat.scope["a"], Is.EqualTo("$obj-prop"));


            chat = ChatParser.ParseText("CHAT c1\nSET a2 = $obj-prop", NO_VALIDATORS)[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            set = (Dialogic.Set)chat.commands[0];
            Assert.That(set.text, Is.EqualTo("a2"));
            Assert.That(set.op, Is.EqualTo(Assignment.EQ));
            Assert.That(set.value, Is.EqualTo("$obj-prop"));
            set.Realize(globals);
            Assert.That(chat.scope["a2"], Is.EqualTo("$obj-prop"));

            chat = ChatParser.ParseText("CHAT c1\nSET a= ${obj-prop}", NO_VALIDATORS)[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            set = (Dialogic.Set)chat.commands[0];
            Assert.That(set.text, Is.EqualTo("a"));
            Assert.That(set.op, Is.EqualTo(Assignment.EQ));
            Assert.That(set.value, Is.EqualTo("${obj-prop}"));
            set.Realize(globals);
            Assert.That(chat.scope["a"], Is.EqualTo("${obj-prop}"));


            chat = ChatParser.ParseText("CHAT c1\nSET a2 = ${obj-prop}", NO_VALIDATORS)[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            set = (Dialogic.Set)chat.commands[0];
            Assert.That(set.text, Is.EqualTo("a2"));
            Assert.That(set.op, Is.EqualTo(Assignment.EQ));
            Assert.That(set.value, Is.EqualTo("${obj-prop}"));
            set.Realize(globals);
            Assert.That(chat.scope["a2"], Is.EqualTo("${obj-prop}"));
        }

        [Test]
        public void SimpleSetsWithOr()
        {
            Chat chat;
            Set set;

            chat = ChatParser.ParseText("CHAT c1\nSET $a = (4 | 5)", NO_VALIDATORS)[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            set = (Dialogic.Set)chat.commands[0];
            Assert.That(set.text, Is.EqualTo("a"));
            Assert.That(set.op, Is.EqualTo(Assignment.EQ));
            Assert.That(set.value, Is.EqualTo("(4 | 5)"));
            set.Realize(globals);
            //Assert.That(globals["a"], Is.EqualTo("4").Or.EqualTo("5"));
            Assert.That(globals["a"], Is.EqualTo("(4 | 5)"));
            globals.Remove("a");

            chat = ChatParser.ParseText("CHAT c1\nSET a = ( 4 | 5 )", NO_VALIDATORS)[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            set = (Dialogic.Set)chat.commands[0];
            Assert.That(set.text, Is.EqualTo("a"));
            Assert.That(set.op, Is.EqualTo(Assignment.EQ));
            Assert.That(set.value, Is.EqualTo("( 4 | 5 )"));
            set.Realize(globals);
            object outv = null;
            globals.TryGetValue("a", out outv);
            Assert.That(outv, Is.Null);
            Assert.That(chat.scope["a"], Is.EqualTo("( 4 | 5 )"));
        }

        [Test]
        public void SetFromExternal()
        {
            string[] lines = {
                "CHAT WineReview {type=a,stage=b}",
                "SET review=$desc $fortune $ending",
                "SET desc=You look tasty: gushing blackberry into the rind of day-old ennui.",
                "SET fortune=Under your skin, tears undulate like a leaky eel.",
                "SET ending=And thats the end of the story...",
                "CHAT External {type=a,stage=b}",
                "SAY #WineReview.review",
            };
            ChatRuntime rt = new ChatRuntime(Tendar.AppConfig.Actors);
            rt.ParseText(String.Join("\n", lines));
            var chats = rt.Chats();
            //Console.WriteLine(rt);

            Chat chat1 = chats[0], chat2 = chats[1];
            Say say = (Dialogic.Say)chat2.commands.Last();

            chat1.commands.ForEach(c => c.Realize(globals));
            chat2.commands.ForEach(c => c.Realize(globals));

            Assert.That(chat1.scope.ContainsKey("review"), Is.True);
            Assert.That(chat1.scope.ContainsKey("ending"), Is.True);

            Assert.That(say.Text(), Is.EqualTo("You look tasty: gushing blackberry into the rind of day-old ennui. Under your skin, tears undulate like a leaky eel. And thats the end of the story..."));
        }

        [Test]
        public void SetRules1()
        {
            string[] lines = {
                "CHAT WineReview {type=a,stage=b}",
                "SET review=$desc $fortune $ending",
                "SET desc=You look tasty: gushing blackberry into the rind of day-old ennui.",
                "SET fortune=Under your skin, tears undulate like a leaky eel.",
                "SET ending=And thats the end of the story...",
                "SAY $review",
            };
            var chat = ChatParser.ParseText(String.Join("\n", lines))[0];

            var last = chat.commands[chat.commands.Count - 1];
            Assert.That(last, Is.Not.Null);
            Assert.That(last.GetType(), Is.EqualTo(typeof(Say)));

            chat.commands.ForEach(c => c.Realize(globals));

            Assert.That(chat.scope.ContainsKey("review"), Is.True);
            Assert.That(chat.scope.ContainsKey("ending"), Is.True);

            Say say = (Dialogic.Say)last;
            //Console.WriteLine(chat.ToTree()+"\nSAY: "+say.Text());

            Assert.That(say.Text(), Is.EqualTo("You look tasty: gushing blackberry into the rind of day-old ennui. Under your skin, tears undulate like a leaky eel. And thats the end of the story..."));
        }

        [Test]
        public void SetRules2()
        {
            string[] lines = {
                "CHAT WineReview {type=a,stage=b}",
                "SET review=$desc $fortune $ending",
                "SET ending=($score | $end-phrase) Goodbye!",
                "SET desc=You look tasty: gushing blackberry into the rind of day-old ennui.",
                "SET score=The judges give that a 1.",
                "SET fortune=You will live a short life in poverty.",
                "SET end-phrase=And thats the end of the story...",
                "SAY $review",
            };
            var chat = ChatParser.ParseText(String.Join("\n", lines))[0];

            var last = chat.commands[chat.commands.Count - 1];
            Assert.That(last, Is.Not.Null);
            Assert.That(last.GetType(), Is.EqualTo(typeof(Say)));

            chat.commands.ForEach(c => c.Realize(globals));

            Assert.That(globals.ContainsKey("WineReview.review"), Is.False);
            Assert.That(globals.ContainsKey("WineReview.ending"), Is.False);
            Assert.That(chat.scope.ContainsKey("review"), Is.True);
            Assert.That(chat.scope.ContainsKey("ending"), Is.True);

            Say say = (Dialogic.Say)last;
            var text = say.Text();
            Assert.That(text.StartsWith("You look tasty: gushing", Util.IC), Is.True);
            Assert.That(text.EndsWith("Goodbye!", Util.IC), Is.True);
        }

        [Test]
        public void SetRules3()
        {
            string[] lines = {
                "CHAT WineReview {type=a,stage=b}",
                "SET review=$desc $fortune $ending",
                "SET ending=$score | $end-phrase",
                "SET desc=You look tasty: gushing blackberry into the rind of day-old ennui.",
                "SET score=The judges give that a 1; good luck with the poverty.",
                "SET fortune=You will live a short life with dismal hygiene.",
                "SET end-phrase=And thats the end of the story. Good luck with the poverty.",
                "SAY $review",
            };
            var chat = (Chat)ChatParser.ParseText(String.Join("\n", lines))[0];
            var last = chat.commands[chat.commands.Count - 1];


            Assert.That(last, Is.Not.Null);
            Assert.That(last.GetType(), Is.EqualTo(typeof(Say)));

            chat.commands.ForEach(c => c.Realize(globals));

            //Console.WriteLine(chat.AsGrammar(globals));
            //Console.WriteLine("------------------------------------");
            //Console.WriteLine(chat.ExpandNoGroups(globals,"review"));
            //Console.WriteLine("------------------------------------");

            Assert.That(globals.ContainsKey("WineReview.review"), Is.False);
            Assert.That(globals.ContainsKey("WineReview.ending"), Is.False);
            Assert.That(chat.scope.ContainsKey("review"), Is.True);
            Assert.That(chat.scope.ContainsKey("ending"), Is.True);

            Say say = (Dialogic.Say)last;

            for (int i = 0; i < 10; i++)
            {
                var text = say.Realize(globals).Text();
                //Console.WriteLine(i+") "+text);
                Assert.That(text.StartsWith("You look tasty", Util.IC), Is.True);
                Assert.That(text.EndsWith("with the poverty.", Util.IC), Is.True);
            }
        }

        [Test]
        public void SetRulesWithOrs()
        {
            string[] lines = {
                "CHAT c1 {type=a,stage=b}",
                "SET review=$greeting",
                "SET greeting=(Hello | Goodbye)",
                "SAY $review",
            };
            var chat = ChatParser.ParseText(String.Join("\n", lines))[0];
            //Console.WriteLine(chat.ToTree());
            var last = chat.commands[chat.commands.Count - 1];
            Assert.That(last, Is.Not.Null);
            Assert.That(last.GetType(), Is.EqualTo(typeof(Say)));

            chat.commands.ForEach(c => c.Realize(globals));

            Assert.That(globals.ContainsKey("c1.review"), Is.False);
            Assert.That(globals.ContainsKey("c1.greeting"), Is.False);
            Assert.That(chat.scope.ContainsKey("review"), Is.True);
            Assert.That(chat.scope.ContainsKey("greeting"), Is.True);
            Say say = (Dialogic.Say)last;

            for (int i = 0; i < 10; i++)
            {
                say.Realize(globals);
                //Console.WriteLine(say.Text());
                Assert.That(say.Text(), Is.EqualTo("Hello").Or.EqualTo("Goodbye"));
            }
        }

        [Test]
        public void SetDefaultCommand()
        {
            string[] lines = {
                "CHAT c1 {type=a,stage=b,defaultCmd=SET}",
                "SET review=$greeting",
                "greeting=(Hello | Goodbye)",
                "SAY $review",
            };

            var chat = ChatParser.ParseText(String.Join("\n", lines))[0];
            //Console.WriteLine(chat.ToTree());

            for (int i = 0; i < 10; i++)
            {
                chat.commands.ForEach(c => c.Realize(globals));
                Assert.That(((Dialogic.Say)chat.commands.Last()).Text(),
                    Is.EqualTo("Hello").Or.EqualTo("Goodbye"));
                //Console.WriteLine(i+")" + );
            }
        }

        [Test]
        public void RealizeWithNullGlobals()
        {
            var lines = new[] {
                "CHAT wine1 {noStart=true}",
                "SET a = $b",
                "SET b = c",
                "SAY $a"
            };
            ChatRuntime runtime = new ChatRuntime(Tendar.AppConfig.Actors);
            runtime.ParseText(string.Join("\n", lines), false);
            //string content = "";
            //runtime.Chats().ForEach(c => { content += c.ToTree() + "\n\n"; });
            runtime.Chats().ForEach(c => c.Realize(null));
            var cmd = runtime.Chats().Last().commands.Last();
            var result = cmd.Text();
            Assert.That(result, Is.EqualTo("c"));


            lines = new[] {
                "CHAT wine1 {noStart=true}",
                "SET a = $b",
                "SET b = c",
                "SAY ${a}"
            };

            runtime = new ChatRuntime(Tendar.AppConfig.Actors);
            runtime.ParseText(string.Join("\n", lines), false);
            runtime.Chats().ForEach(c => c.Realize(null));
            cmd = runtime.Chats().Last().commands.Last();
            result = cmd.Text();
            Assert.That(result, Is.EqualTo("c"));
        }

        [Test]
        public void SetGrammarMode()
        {
            string[] lines = {
                "CHAT c1 {chatMode=grammar}",
                "review=$greeting",
                "greeting = (Hello | Goodbye)",
                "",
                "CHAT c2",
                "#c1.review",
            };

            var chats = ChatParser.ParseText(String.Join("\n", lines), true);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Set)));
            Assert.That(chats[1].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            //chats[0].commands.ForEach(c => c.Realize(globals));
        }

        [Test]
        public void SetOrEquals()
        {
            string[] lines = {
                "CHAT c1 {type=a,stage=b}",
                "SET review=$greeting",
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

            Assert.That(chat.scope.ContainsKey("review"), Is.True);
            Assert.That(chat.scope.ContainsKey("greeting"), Is.True);
            Assert.That(chat.scope["greeting"],
                        Is.EqualTo("(Hello | Goodbye | See you later)"));

            Say say = (Dialogic.Say)last;

            for (int i = 0; i < 10; i++)
            {
                say.Realize(globals);
                var said = say.Text();
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
                "SET review=$greeting",
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

            Assert.That(chat.scope.ContainsKey("review"), Is.True);
            Assert.That(chat.scope.ContainsKey("greeting"), Is.True);
            Assert.That(chat.scope["greeting"], Is.EqualTo("(Hello | Goodbye) Fred"));

            Say say = (Dialogic.Say)last;
            for (int i = 0; i < 10; i++)
            {
                say.Realize(globals);
                var said = say.Text();
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
                "SET review=$desc $fortune $ending",
                "ending =  $score | $end1",
                "ending |=  $end2",
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
            //Console.WriteLine(chat.AsGrammar(globals), false);

            Say say = (Say)chat.commands.Last();//.Realize()

            for (int i = 0; i < 10; i++)
            {
                var said = say.Realize(globals).Text();
                //Console.WriteLine(i + ") " + said);
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
                "start =  $a $b $c",
                "a = A",// | A $a",
                "b = B",
                "c = D",
                "c |= E",
                "SAY $start"
            };

            var chat = ChatParser.ParseText(String.Join("\n", lines))[0];
            chat.commands.ForEach(c => c.Realize(globals));
            Say say = (Say)chat.commands.Last();

            for (int i = 0; i < 10; i++)
            {
                var said = say.Realize(globals).Text();
                //Console.WriteLine(i + ") " + said);
                Assert.That(said, Is.EqualTo("A B D").Or.EqualTo("A B E"));
            }
        }

        [Test]
        public void SimpleGrammarExpand()
        {
            string[] lines;
            string text;
            Chat chat;

            lines = new[] {
                "start =  $a $b $c",
                "a = A",
                "b = B",
                "c = C",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat._Expand(globals, "$start"), Is.EqualTo("A B C"));

            lines = new[]{
                "start =  $a $b",
                "a = A",
                "b = $c",
                "c = B $d",
                "d = C",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat._Expand(globals, "$start"), Is.EqualTo("A B C"));

            lines = new[]{
                "start =  $a $b $c",
                "a = A",
                "b = $c",
                "c = B $d",
                "d = C",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat._Expand(globals, "$start"), Is.EqualTo("A B C B C"));

            lines = new[]{
                "start =  $a ",
                "a = $b",
                "b = $c",
                "c = D",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat._Expand(globals, "$start"), Is.EqualTo("D"));

            lines = new[]{
                "start =  $a $b",
                "a = $c $d",
                "b = $d $c",
                "c = C",
                "d = D",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat._Expand(globals, "$start"), Is.EqualTo("C D D C"));
        }


        // NEXT: change all these to ExpandNoGroups -----------------

        [Test]
        public void GrammarExpandWithOr()
        {
            string[] lines;
            string text;
            Chat chat;

            lines = new[] {
                "start =  $a $b $c",
                "a = A",
                "b = B",
                "c = C | D",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat._ExpandNoGroups(globals, "$start"), Is.EqualTo("A B (C | D)"));

            lines = new[] {
                "start =  $a $b $c",
                "a = A",
                "b = B",
                "c = (C | D)",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat._ExpandNoGroups(globals, "$start"), Is.EqualTo("A B (C | D)"));

            lines = new[] {
                "start =  $a $b $c",
                "a = A",
                "b = B",
                "c = C",
                "c += D",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat._ExpandNoGroups(globals, "$start"), Is.EqualTo("A B C D"));

            lines = new[] {
                "start =  $a $b $c",
                "a = A",
                "b = B",
                "c = C",
                "c += | D",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat._ExpandNoGroups(globals, "$start"), Is.EqualTo("A B C | D"));

            lines = new[] {
                "start =  $a $b $c",
                "a = A",
                "b = B",
                "c = (C",
                "c += | D)",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat._ExpandNoGroups(globals, "$start"), Is.EqualTo("A B (C | D)"));

            lines = new[] {
                "start =  $a $b $c",
                "a = A",
                "b = B",
                "c = (C",
                "c += | D",
                "c += | E)",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat._ExpandNoGroups(globals, "$start"), Is.EqualTo("A B (C | D | E)"));

            lines = new[] {
                "start =  $a ($b $c)",
                "a = A",
                "b = B",
                "c = (C | D)",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat._ExpandNoGroups(globals, "$start"), Is.EqualTo("A (B (C | D))"));

            lines = new[] {
                "start =  $a $b $c",
                "a = A",
                "b = B",
                "c = ($a | $b)",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat._ExpandNoGroups(globals, "$start"), Is.EqualTo("A B (A | B)"));

            /* infinite loop
            lines = new[] {
                "start =  $a $b $c",
                "a = $b",
                "b = $c",
                "c = ($a | $b | D)",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Console.WriteLine(chat.Expand(globals, "$start"));
            Assert.That(chat.ExpandNoGroups(globals, "$start"), Is.EqualTo("A B (A | B)")); */
        }

        [Test]
        public void GrammarExpandWithOrEquals()
        {
            string[] lines;
            string text;
            Chat chat;

            lines = new[] {
                "start =  $a $b $c",
                "a = A",
                "b = B",
                "c = C | D",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);

            Assert.That(chat._ExpandNoGroups(globals, "$start"), Is.EqualTo("A B (C | D)"));

            lines = new[] {
                "start =  $a $b $c",
                "a = A",
                "b = B",
                "c = C",
                "c |= D",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat._ExpandNoGroups(globals, "$start"), Is.EqualTo("A B (C | D)"));

            lines = new[] {
                "start =  $a $b $c",
                "a = A",
                "b = B",
                "c = C | D",
                "c |= E",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat._ExpandNoGroups(globals, "$start"), Is.EqualTo("A B (C | D | E)"));

            lines = new[] {
                "start =  $a $b $c",
                "a = A",
                "b = B",
                "c = (C | D)",
                "c |= E",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat._ExpandNoGroups(globals, "$start"), Is.EqualTo("A B (C | D | E)"));

            lines = new[] {
                "start =  $a $b $c",
                "a = A",
                "b = B",
                "c = C (C | D)",
                "c |= E",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat._ExpandNoGroups(globals, "$start"), Is.EqualTo("A B (C (C | D) | E)"));

            lines = new[] {
                "start = $a $b $c",
                "a = A",
                "b = B",
                "c = D | E",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat._ExpandNoGroups(globals, "$start"), Is.EqualTo("A B (D | E)"));

            lines = new[] {
                "start = $a $b $c",
                "a = A",
                "b = B",
                "c = $d | $e",
                "d = D",
                "e = E",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat._ExpandNoGroups(globals, "$start"), Is.EqualTo("A B (D | E)"));

            lines = new[] {
                "start = $desc $fortune $ending",
                "ending = $score | $end-phrase",
                "desc = A",
                "fortune = B",
                "score = C",
                "end-phrase = D"
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat._ExpandNoGroups(globals, "$start"), Is.EqualTo("A B (C | D)"));
        }

        [Test]
        public void GrammarExpandWithOrEqualsDoGroups()
        {
            string[] lines;
            string text;
            Chat chat;

            lines = new[] {
                "start =  $a $b $c",
                "a = A",
                "b = B",
                "c = C",
                "c |= D",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat._Expand(globals, "$start"), Is.EqualTo("A B C").Or.EqualTo("A B D"));

            lines = new[] {
                "start =  $a $b $c",
                "a = A",
                "b = B",
                "c = C | D",
                "c |= E",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat._Expand(globals, "$start"), Is.EqualTo("A B C").Or.EqualTo("A B D").Or.EqualTo("A B E"));

            lines = new[] {
                "start =  $a $b $c",
                "a = A",
                "b = B",
                "c = (C | D)",
                "c |= E",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat._Expand(globals, "$start"), Is.EqualTo("A B C").Or.EqualTo("A B D").Or.EqualTo("A B E"));

            lines = new[] {
                "start =  $a $b $c",
                "a = A",
                "b = B",
                "c = C (C | D)",
                "c |= E",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat._Expand(globals, "$start"), Is.EqualTo("A B E").Or.EqualTo("A B C C").Or.EqualTo("A B C D"));

            lines = new[] {
                "start =  $a $b",
                "a = The hungry dog",
                "a |= The angry cat",
                "b = bit the tiny child",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0].Realize(globals);
            Assert.That(chat._Expand(globals, "$start"),
                        Is.EqualTo("The hungry dog bit the tiny child")
                        .Or.EqualTo("The angry cat bit the tiny child"));
        }
    }
}
