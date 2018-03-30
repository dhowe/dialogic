﻿using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Dialogic
{
    [TestFixture]
    public class ParserTests
    {

        [Test]
        public void TestDynamicAssign()
        {
            var chat = ChatParser.ParseText("SAY ok { type = a,stage = b}")[0];
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Say)));

            Say say = (Dialogic.Say)chat.commands[0];
            Assert.That(say, Is.Not.Null);
            Assert.That(say.meta, Is.Not.Null);
            Assert.That(say.realized, Is.Empty);
            Assert.That(say.GetMeta("type"), Is.EqualTo("a"));
            Assert.That(say.GetMeta("stage"), Is.EqualTo("b"));

            say.Realize(null);
            Assert.That(say, Is.Not.Null);
            Assert.That(say.realized, Is.Not.Null);
            Assert.That(say.GetRealized("type"), Is.EqualTo("a"));
            Assert.That(say.GetRealized("stage"), Is.EqualTo("b"));
            Assert.That(say.DelayMs, Is.EqualTo(2000));




            chat = ChatParser.ParseText("SAY ok { type = a,stage = b, DelayMs=100}")[0];
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Say)));

            say = (Dialogic.Say)chat.commands[0];
            Assert.That(say, Is.Not.Null);
            Assert.That(say.meta, Is.Not.Null);
            Assert.That(say.realized, Is.Empty);
            Assert.That(say.GetMeta("type"), Is.EqualTo("a"));
            Assert.That(say.GetMeta("stage"), Is.EqualTo("b"));
            Assert.That(say.GetMeta("DelayMs"), Is.EqualTo("100"));

            say.Realize(null);
            Assert.That(say, Is.Not.Null);
            Assert.That(say.realized, Is.Not.Null);
            Assert.That(say.GetRealized("type"), Is.EqualTo("a"));
            Assert.That(say.GetRealized("stage"), Is.EqualTo("b"));
            Assert.That(say.GetRealized("DelayMs"), Is.EqualTo("100"));

            // TODO: WORKING here on Dynamic Assign
            Assert.That(say.DelayMs, Is.EqualTo(100));
        }

        [Test]
        public void TestChatMeta()
        {
            var chat = ChatParser.ParseText("CHAT c1", true)[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.meta, Is.Not.Null);
            Assert.That(chat.realized, Is.Null);
            Assert.That(chat.Staleness(), Is.EqualTo(Defaults.CHAT_STALENESS));
            Assert.That(Convert.ToDouble(chat.GetMeta(Meta.STALENESS)), 
                Is.EqualTo(Convert.ToDouble(Defaults.CHAT_STALENESS)));

            chat = ChatParser.ParseText("CHAT c1 { type = a, stage = b }")[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.meta, Is.Not.Null);
            Assert.That(chat.realized, Is.Null);
            Assert.That(chat.Staleness(), Is.EqualTo(Defaults.CHAT_STALENESS));
            Assert.That(Convert.ToDouble(chat.GetMeta(Meta.STALENESS)),
                Is.EqualTo(Convert.ToDouble(Defaults.CHAT_STALENESS)));
            Assert.That(chat.GetMeta("type"), Is.EqualTo("a"));
            Assert.That(chat.GetMeta("stage"), Is.EqualTo("b"));

            chat = ChatParser.ParseText("CHAT c1 { staleness = 1,type = a,stage = b}")[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.meta, Is.Not.Null);
            Assert.That(chat.realized, Is.Null);
            Assert.That(chat.Staleness(), Is.EqualTo(1));
            Assert.That(Convert.ToDouble(chat.GetMeta(Meta.STALENESS)),
                Is.EqualTo(Convert.ToDouble("1")));
            Assert.That(chat.GetMeta("type"), Is.EqualTo("a"));
            Assert.That(chat.GetMeta("stage"), Is.EqualTo("b"));
        }
  
        [Test]
        public void TestAssignActors()
        {
            List<Chat> chats;

            chats = ChatParser.ParseText("Guppy:SAY Hello from Guppy");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Say say = (Dialogic.Say)chats[0].commands[0];
            Assert.That(say.Text, Is.EqualTo("Hello from Guppy"));
            Assert.That(say.actor.Name(), Is.EqualTo("Guppy"));
            say.Realize(null);
            Assert.That(say.realized[Meta.TYPE], Is.EqualTo("Say"));
            Assert.That(say.realized[Meta.TEXT], Is.EqualTo("Hello from Guppy"));
            Assert.That(say.realized[Meta.ACTOR], Is.EqualTo("Guppy"));

            chats = ChatParser.ParseText("Guppy: Hello from Guppy");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            say = (Dialogic.Say)chats[0].commands[0];
            Assert.That(say.Text, Is.EqualTo("Hello from Guppy"));
            Assert.That(say.actor.Name(), Is.EqualTo("Guppy"));

            chats = ChatParser.ParseText("Tendar:Hello from Tendar");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            say = (Dialogic.Say)chats[0].commands[0];
            Assert.That(say.Text, Is.EqualTo("Hello from Tendar"));
            Assert.That(say.actor.Name(), Is.EqualTo("Tendar"));

            chats = ChatParser.ParseText("Hello from Guppy");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            say = (Dialogic.Say)chats[0].commands[0];
            Assert.That(say.Text, Is.EqualTo("Hello from Guppy"));
            Assert.That(say.actor.Name(), Is.EqualTo(Actor.Default.Name()));
        }

        [Test]
        public void TestPrompts()
        {
            List<Chat> chats = ChatParser.ParseText("ASK Want a game?\nOPT Y #Game\n\nOPT N #End");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Ask)));
			Ask ask = (Dialogic.Ask)chats[0].commands[0];
            Assert.That(ask.Text, Is.EqualTo("Want a game?"));
            Assert.That(ask.Options().Count, Is.EqualTo(2));

            var options = ask.Options();
            Assert.That(options[0].GetType(), Is.EqualTo(typeof(Opt)));
            Assert.That(options[0].Text, Is.EqualTo("Y"));
            Assert.That(options[0].action.GetType(), Is.EqualTo(typeof(Go)));
            Assert.That(options[1].GetType(), Is.EqualTo(typeof(Opt)));
            Assert.That(options[1].Text, Is.EqualTo("N"));
            Assert.That(options[1].action.GetType(), Is.EqualTo(typeof(Go)));
        }

        [Test]
        public void TestStripComments()
        {
            string[] lines = {
                "CHAT c1",
                "SAY Thank you",
                "SAY Hello",
                "//SAY And Goodbye",
                "SAY Done1",
                "SAY And//Goodbye",
                "SAY Done2",
                "/*SAY And Goodbye*/",
                "SAY And /*Goodbye*/",
                "/*SAY And Goodbye",
                "SAY Done2*/",
                "SAY Done3",
                "/*SAY And Goodbye",
                "SAY */Done4",
                "SAY Done4"
            };

            string[] result = {
                "CHAT c1",
                "SAY Thank you",
                "SAY Hello",
                String.Empty,
                "SAY Done1",
                "SAY And",
                "SAY Done2",
                String.Empty,
                "SAY And",
                String.Empty,
                String.Empty,
                "SAY Done3",
                String.Empty,
                "Done4",
                "SAY Done4"
            };

            var parsed = ChatParser.StripComments(String.Join("\n", lines));

            Assert.That(parsed.Length, Is.EqualTo(lines.Length));
            for (int i = 0; i < parsed.Length; i++)
            {
                Assert.That(parsed[i], Is.EqualTo(result[i]));
            }
        }

        [Test]
        public void TestParseComments()
        {
            var txt = "SAY Thank you/*\nSAY Hello //And Goodbye*/";
            var res = ChatParser.StripComments(txt);
            Assert.That(res[0], Is.EqualTo("SAY Thank you"));
            Assert.That(res[1], Is.EqualTo(""));

            List<Chat> chats;
            chats = ChatParser.ParseText(txt);
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));

            chats = ChatParser.ParseText("SAY Thank you\n//SAY Hello\n// And Goodbye");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));

            chats = ChatParser.ParseText("SAY Thank you\nSAY Hello //And Goodbye");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(2));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Assert.That(chats[0].commands[1].Text, Is.EqualTo("Hello"));
            Assert.That(chats[0].commands[1].GetType(), Is.EqualTo(typeof(Say)));

            chats = ChatParser.ParseText("SAY Thank you\nSAY Hello /*And Goodbye*/");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(2));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Assert.That(chats[0].commands[1].Text, Is.EqualTo("Hello"));
            Assert.That(chats[0].commands[1].GetType(), Is.EqualTo(typeof(Say)));

            chats = ChatParser.ParseText("SAY Thank you\n//SAY Hello\nAnd Goodbye\n");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(2));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Assert.That(chats[0].commands[1].Text, Is.EqualTo("And Goodbye"));
            Assert.That(chats[0].commands[1].GetType(), Is.EqualTo(typeof(Say)));

            chats = ChatParser.ParseText("SAY Thank you\n//SAY Goodbye\nAnd Hello");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(2));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Assert.That(chats[0].commands[1].Text, Is.EqualTo("And Hello"));
            Assert.That(chats[0].commands[1].GetType(), Is.EqualTo(typeof(Say)));
        }

        [Test]
        public void TestFindSoft()
        {
            List<Chat> chats;

            chats = ChatParser.ParseText("FIND {num=1}");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.Null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));
            Assert.That(chats[0].commands[0].GetMeta("num"), Is.Not.Null);
            var meta = chats[0].commands[0].GetMeta("num");
            Assert.That(meta.GetType(), Is.EqualTo(typeof(Constraint)));
			Constraint constraint = (Dialogic.Constraint)meta;
            Assert.That(constraint.IsStrict(), Is.EqualTo(false));

            chats = ChatParser.ParseText("FIND {a*=(hot|cool)}");
            var find = chats[0].commands[0];
            Assert.That(find.GetType(), Is.EqualTo(typeof(Find)));
            Assert.That(chats[0].commands[0].Text, Is.Null);
            Assert.That(chats[0].commands[0].GetMeta("a"), Is.Not.Null);
            meta = chats[0].commands[0].GetMeta("a");
            Assert.That(meta.GetType(), Is.EqualTo(typeof(Constraint)));
            constraint = (Dialogic.Constraint)meta;
            Assert.That(constraint.IsStrict(), Is.EqualTo(false));

            chats = ChatParser.ParseText("FIND {do=1}");
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            var finder = chats[0].commands[0];
            Assert.That(finder.GetType(), Is.EqualTo(typeof(Find)));
            meta = chats[0].commands[0].GetMeta("do");
            Assert.That(meta.GetType(), Is.EqualTo(typeof(Constraint)));
            constraint = (Dialogic.Constraint)meta;
            Assert.That(constraint.IsStrict(), Is.EqualTo(false));
        }

        [Test]
        public void TestFindSoftDynVar()
        {
            List<Chat> chats;

            chats = ChatParser.ParseText("FIND {num=$count}");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.Null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));

            chats[0].commands[0].Realize(RealizeTests.globals);

            Assert.That(chats[0].commands[0].GetMeta("num"), Is.Not.Null);
            var meta = chats[0].commands[0].GetMeta("num");
            Assert.That(meta.GetType(), Is.EqualTo(typeof(Constraint)));
            Constraint constraint = (Constraint)meta;
            Assert.That(constraint.IsStrict(), Is.EqualTo(false));
            Assert.That(constraint.name, Is.EqualTo("num"));
            Assert.That(constraint.value, Is.EqualTo("$count"));
        }

        [Test]
        public void TestFindSoftDynGroup()
        {
            // "FIND {a*=(hot|cool)}" in regex, means hot OR cool, no subs
            var chats = ChatParser.ParseText("FIND {a*=(hot|cool)}");
            var find = chats[0].commands[0];
            find.Realize(RealizeTests.globals);

            Assert.That(find.GetType(), Is.EqualTo(typeof(Find)));
            Assert.That(chats[0].commands[0].Text, Is.Null);
            Assert.That(chats[0].commands[0].GetMeta("a"), Is.Not.Null);

            var meta = chats[0].commands[0].GetMeta("a");
            Assert.That(meta.GetType(), Is.EqualTo(typeof(Constraint)));
            var constraint = (Dialogic.Constraint)meta;
            Assert.That(constraint.IsStrict(), Is.EqualTo(false));
            Assert.That(constraint.name, Is.EqualTo("a"));
            Assert.That(constraint.op, Is.EqualTo(Operator.RE));
            Assert.That(constraint.value, Is.EqualTo("(hot|cool)"));
            var real = chats[0].commands[0].realized;
            Assert.That(real.Count, Is.EqualTo(2)); // a,staleness
        }

        [Test]
        public void TestFindHard()
        {
            List<Chat> chats;

            chats = ChatParser.ParseText("FIND {!num=1}");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.Null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));
            Assert.That(chats[0].commands[0].GetMeta("num"), Is.Not.Null);
            var meta = chats[0].commands[0].GetMeta("num");
            Assert.That(meta.GetType(), Is.EqualTo(typeof(Constraint)));
			Constraint constraint = (Dialogic.Constraint)meta;
            Assert.That(constraint.IsStrict(), Is.EqualTo(true));

            chats = ChatParser.ParseText("FIND {!a*=(hot|cool)}");
            var find = chats[0].commands[0];
            Assert.That(find.GetType(), Is.EqualTo(typeof(Find)));
            Assert.That(chats[0].commands[0].Text, Is.Null);
            Assert.That(chats[0].commands[0].GetMeta("a"), Is.Not.Null);
            meta = chats[0].commands[0].GetMeta("a");
            Assert.That(meta.GetType(), Is.EqualTo(typeof(Constraint)));
            constraint = (Dialogic.Constraint)meta;
            Assert.That(constraint.IsStrict(), Is.EqualTo(true));

            chats = ChatParser.ParseText("FIND {!do=1}");
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            var finder = chats[0].commands[0];
            Assert.That(finder.GetType(), Is.EqualTo(typeof(Find)));
            meta = chats[0].commands[0].GetMeta("do");
            Assert.That(meta.GetType(), Is.EqualTo(typeof(Constraint)));
            constraint = (Dialogic.Constraint)meta;
            Assert.That(constraint.IsStrict(), Is.EqualTo(true));
        }

        [Test]
        public void TestWaitCommand()
        {
            List<Chat> chats;

            chats = ChatParser.ParseText("WAIT .5 {waitForAnimation=true}");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Wait)));
			Wait wait = (Dialogic.Wait)chats[0].commands[0];
            Assert.That(wait.Text, Is.EqualTo(".5"));
            Assert.That(wait.DelayMs, Is.EqualTo(500));
            Assert.That(wait.GetMeta("waitForAnimation"), Is.EqualTo("true"));
        }

        [Test]
        public void TestValidators()
        {
            List<Chat> chats;
            chats = ChatParser.ParseText("CHAT c1 {type=a,stage=b}\nSAY Hello");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Hello"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
        }

        [Test]
        public void TestToScript()
        {
            string[] tests = {

                "CHAT c1 {staleness=1,type=a,stage=b}",
            };

            for (int i = 0; i < tests.Length; i++)
            {
                Assert.That(ChatParser.ParseText(tests[i])[0].ToString(), Is.EqualTo(tests[i]));
            }

            tests = new[] {
                "SAY hay is for horses",
                "ASK hay is for horses?",
                "DO #hay",
                "FIND {!a=b,staleness=5}",
                "WAIT",
                "WAIT .5",
                "WAIT {ForAnimation=true}",
                "WAIT .5 {ForAnimation=true}",
                "NVM",
                "NVM {ForAnimation=true}",
            };

            for (int i = 0; i < tests.Length; i++)
            {
                Assert.That(ChatParser.ParseText(tests[i])[0].commands[0].ToString(), Is.EqualTo(tests[i]));
            }

            var s = "GO #hay";
            Assert.That(ChatParser.ParseText(s)[0].commands[0].ToString(), Is.EqualTo(s));
            Assert.That(ChatParser.ParseText("GO hay")[0].commands[0].ToString(), Is.EqualTo(s));

            s = "DO #hay";
            Assert.That(ChatParser.ParseText(s)[0].commands[0].ToString(), Is.EqualTo(s));
            Assert.That(ChatParser.ParseText("DO hay")[0].commands[0].ToString(), Is.EqualTo(s));

            //s = "SET a 4";
            //Assert.That(ChatParser.ParseText((s)[0].commands[0].ToString(), Is.EqualTo(s));

            s = "ASK hay is for horses?\nOPT Yes? #Yes\nOPT No? #No";
            var exp = s.Replace("\n","\n  ").Split('\n');
            var res = ChatParser.ParseText(s)[0].commands[0].ToString().Split('\n');

            Assert.That(res.Length, Is.EqualTo(exp.Length));
            for (int i = 0; i < exp.Length; i++)
            {
                Assert.That(res[i], Is.EqualTo(exp[i]));
            }
        }

        [Test]
        public void TestCommands()
        {
            List<Chat> chats;

            chats = ChatParser.ParseText("ASK is for horses?\nOPT Yes #game");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("is for horses?"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Ask)));
            Ask a = (Ask)chats[0].commands[0];
            var opts = a.Options();
            Assert.That(opts[0].Text, Is.EqualTo("Yes"));
            Assert.That(opts[0].action.Text, Is.EqualTo("game"));

            chats = ChatParser.ParseText("HAY is for horses");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("HAY is for horses"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));

            chats = ChatParser.ParseText("hello");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("hello"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));

            chats = ChatParser.ParseText("wei");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("wei"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));

            chats = ChatParser.ParseText("SAY ...");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("..."));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));

            chats = ChatParser.ParseText("...");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("..."));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));

            chats = ChatParser.ParseText("GO #Twirl");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Twirl"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Go)));

            chats = ChatParser.ParseText("GO Twirl");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Twirl"));

            chats = ChatParser.ParseText("DO #Twirl");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Twirl"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Do)));

            chats = ChatParser.ParseText("DO Twirl");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Twirl"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Do)));

            chats = ChatParser.ParseText("SAY Thank you");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));

            chats = ChatParser.ParseText("SAY Thank you\n \t\nAnd Goodbye");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(2));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Assert.That(chats[0].commands[1].Text, Is.EqualTo("And Goodbye"));
            Assert.That(chats[0].commands[1].GetType(), Is.EqualTo(typeof(Say)));

            chats = ChatParser.ParseText("SAY Thank you { pace = fast}");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Assert.That(chats[0].commands[0].GetMeta("pace"), Is.EqualTo("fast"));

            chats = ChatParser.ParseText("SAY Thank you {pace=fast,count=2}");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Assert.That(chats[0].commands[0].HasMeta(), Is.EqualTo(true));
            Assert.That(chats[0].commands[0].GetMeta("pace"), Is.EqualTo("fast"));
            Assert.That(chats[0].commands[0].GetMeta("count"), Is.EqualTo("2"));

            chats = ChatParser.ParseText("SAY Thank you {}");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Assert.That(chats[0].commands[0].HasMeta(), Is.EqualTo(false));

            // meta variations
            chats = ChatParser.ParseText("ok { pace = fast }");
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("ok"));
            Assert.That(chats[0].commands[0].GetMeta("pace"), Is.EqualTo("fast"));

            chats = ChatParser.ParseText("ok {pace = fast}");
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("ok"));
            Assert.That(chats[0].commands[0].GetMeta("pace"), Is.EqualTo("fast"));

            chats = ChatParser.ParseText("ok {pace =fast}");
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("ok"));
            Assert.That(chats[0].commands[0].GetMeta("pace"), Is.EqualTo("fast"));

            chats = ChatParser.ParseText("ok {pace= fast}");
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("ok"));
            Assert.That(chats[0].commands[0].GetMeta("pace"), Is.EqualTo("fast"));

            chats = ChatParser.ParseText("ok {pace= fast}");
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("ok"));
            Assert.That(chats[0].commands[0].GetMeta("pace"), Is.EqualTo("fast"));

            chats = ChatParser.ParseText("ok {a=b,c=d}");
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("ok"));
            Assert.That(chats[0].commands[0].GetMeta("a"), Is.EqualTo("b"));
            Assert.That(chats[0].commands[0].GetMeta("c"), Is.EqualTo("d"));

            chats = ChatParser.ParseText("ok {a=b, c=d}");
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("ok"));
            Assert.That(chats[0].commands[0].GetMeta("a"), Is.EqualTo("b"));
            Assert.That(chats[0].commands[0].GetMeta("c"), Is.EqualTo("d"));

            chats = ChatParser.ParseText("ok {a=b ,c=d}");
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("ok"));
            Assert.That(chats[0].commands[0].GetMeta("a"), Is.EqualTo("b"));
            Assert.That(chats[0].commands[0].GetMeta("c"), Is.EqualTo("d"));

            chats = ChatParser.ParseText("ok { a = b , c = d }");
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("ok"));
            Assert.That(chats[0].commands[0].GetType, Is.EqualTo(typeof(Say)));
            Assert.That(chats[0].commands[0].TypeName(), Is.EqualTo("Say"));
            Assert.That(chats[0].commands[0].GetMeta("a"), Is.EqualTo("b"));
            Assert.That(chats[0].commands[0].GetMeta("c"), Is.EqualTo("d"));

            chats = ChatParser.ParseText("NVM 1.1");
            Assert.That(chats[0].commands[0].GetType, Is.EqualTo(typeof(Tendar.Nvm)));
            Assert.That(chats[0].commands[0].TypeName(), Is.EqualTo("Nvm"));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("1.1"));
            Assert.That(chats[0].commands[0].DelayMs, Is.EqualTo(1100));
        }

        [Test]
        public void TestSimpleCommands()
        {
            string[] lines = {
                "DO #Twirl", "DO #Twirl {speed= fast}", "SAY Thank you", "WAIT", "WAIT .5",  "WAIT .5 {a=b}",
                "SAY Thank you {pace=fast,count=2}", "SAY Thank you", "FIND { num > 1, an != 4 }",
                "SAY Thank you { pace = fast}", "SAY Thank you {}", "Thank you"
            };
            for (int i = 0; i < lines.Length; i++)
            {
                Command c = ChatParser.ParseText(lines[i])[0].commands[0];
                Assert.That(c is Command);
            }
        }


        [Test]
        public void TestGrammars()
        {
            List<Chat> chats = ChatParser.ParseText("GRAM { start: 'The <item>', item: cat }");
            Command gram = chats[0].commands[0];
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(gram.Text, Is.Null);
            Assert.That(gram.GetType(), Is.EqualTo(typeof(Gram)));
            //new ChatRuntime(chats).Run();
        }

        [Test]
        public void TestChats()
        {
            List<Chat> chats = ChatParser.ParseText("CHAT c1 {type=a,stage=b}");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(0));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Chat chat = chats[0];
            Assert.That(chats[0].Text, Is.EqualTo("c1"));

            chats = ChatParser.ParseText("CHAT c1 {type=a,stage=b}\nGO #c1\nDO #c1\n");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(2));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Go)));
            Assert.That(chats[0].commands[1].GetType(), Is.EqualTo(typeof(Do)));
        }

        [Test]
        public void TestExceptions()
        {
            //var ff = "FIND {a b=e}";
            //Console.WriteLine("\n"+ChatParser.ParseText(ff)[0].ToTree());

            Assert.Throws<ParseException>(() => ChatParser.ParseText("CHAT c1"));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("CHAT x{t pe=a,stage=b}", true));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("CHAT x{type=a b,stage=b}", true));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("CHAT Two Words {type=a,stage=b}"));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("GO {no=label}"));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("DO {no=label}"));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("DO #a {ha s=label}"));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("DO #a {has=la bel}"));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("FIND {a = (b|c)}"));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("FIND hello"));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("FIND {d=e d}"));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("FIND {a b=e}"));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("FIND {a=b,d=e d}"));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("FIND {a b=e,d=c}"));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("FIND {a,b}"));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("WAIT {a}"));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("WAIT {a,b}"));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("WAIT hello"));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("OPT {a=b}"));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("SAY")); // ?
            Assert.Throws<ParseException>(() => ChatParser.ParseText("SAY {a=b}"));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("WAIT a {a=b}"));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("NVM a {a=b}"));
            //Assert.Throws<ParseException>(() => ChatParser.ParseText("SAYHello")); // ?

            string[] lines = {
                "CHAT c1 {type=a,stage=b}","SAY Thank you","SAY Hello",
                "//SAY And Goodbye","SAY Done1","SAY And//Goodbye","SAY Done2",
                "/*SAY And Goodbye*/","SAY And /*Goodbye*/","/*SAY And Goodbye",
                "SAY Done2*/","SAY Done3","/*SAY And Goodbye","SAY */Done4",
                "SAY Done4 {a}"
            };

            Assert.Throws<ParseException>(() => ChatParser.ParseText(String.Join("\n", lines)));
            try
            {
                ChatParser.ParseText(String.Join("\n", lines));
            }
            catch (ParseException e)
            {
                //Console.WriteLine(e);
                Assert.That(e.lineNumber, Is.EqualTo(lines.Length));
            }
        }
    }
}
