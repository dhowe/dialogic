using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Dialogic
{
    [TestFixture]
    public class ParserTests
    {
        const bool NO_VALIDATORS = true;

        public static IDictionary<string, object> globals
             = new Dictionary<string, object>() {
                { "obj-prop", "dog" },
                { "animal", "dog" },
                { "prep", "then" },
                { "group", "(a|b)" },
                { "cmplx", "($group | $prep)" },
                { "count", 4 }
         };

        [Test]
        public void ASimpleTest()
        {
            var chat = ChatParser.ParseText("CHAT c1\nSAY ok", NO_VALIDATORS)[0];
            //Console.WriteLine(chat.ToTree());
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Say)));
        }

        [Test]
        public void SetLocals()
        {
            var chat = ChatParser.ParseText("CHAT c1\nSET a=the white dog", NO_VALIDATORS)[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            var set = (Dialogic.Set)chat.commands[0];
            Assert.That(set.text, Is.EqualTo("a"));
            Assert.That(set.value, Is.EqualTo("the white dog"));
            set.Realize(globals);
            Assert.That(chat.locals["a"], Is.EqualTo("the white dog"));

            chat = ChatParser.ParseText("CHAT c1\nSET a=the white dog\nSET a=4", NO_VALIDATORS)[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            set = (Dialogic.Set)chat.commands[0];
            Assert.That(set.text, Is.EqualTo("a"));
            Assert.That(set.value, Is.EqualTo("the white dog"));
            set = (Dialogic.Set)chat.commands[1];
            Assert.That(set.text, Is.EqualTo("a"));
            Assert.That(set.value, Is.EqualTo("4"));
            set.Realize(globals);
            Assert.That(chat.locals["a"], Is.EqualTo("4"));

            chat = ChatParser.ParseText("CHAT c1\nSET $a=4", NO_VALIDATORS)[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            set = (Dialogic.Set)chat.commands[0];
            Assert.That(set.text, Is.EqualTo("a"));
            Assert.That(set.value, Is.EqualTo("4"));
            set.Realize(globals);
            Assert.That(globals["a"], Is.EqualTo("4"));

            chat = ChatParser.ParseText("CHAT c1\nSET $a=the white dog", NO_VALIDATORS)[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            set = (Dialogic.Set)chat.commands[0];
            Assert.That(set.text, Is.EqualTo("a"));
            Assert.That(set.value, Is.EqualTo("the white dog"));
            set.Realize(globals);
            Assert.That(globals["a"], Is.EqualTo("the white dog"));

            chat = ChatParser.ParseText("CHAT c1\nSET $a=the white dog\nSET $a=4", NO_VALIDATORS)[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            set = (Dialogic.Set)chat.commands[0];
            Assert.That(set.text, Is.EqualTo("a"));
            Assert.That(set.value, Is.EqualTo("the white dog"));
            set = (Dialogic.Set)chat.commands[1];
            Assert.That(set.text, Is.EqualTo("a"));
            Assert.That(set.value, Is.EqualTo("4"));
            set.Realize(globals);
            Assert.That(globals["a"], Is.EqualTo("4"));
        }

        [Test]
        public void SetLocalsWithVars()
        {
            var chat = ChatParser.ParseText("CHAT c1\nSET a=$animal ", NO_VALIDATORS)[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            Set set = (Set)chat.commands[0];
            set.Realize(globals);
            Assert.That(set.text, Is.EqualTo("a"));
            //Assert.That(set.value, Is.EqualTo("dog"));
            set.Realize(globals);
            Assert.That(chat.locals["a"], Is.EqualTo("$animal"));

            chat = ChatParser.ParseText("CHAT c1\nSET a=$obj-prop ", NO_VALIDATORS)[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            set = (Set)chat.commands[0];
            set.Realize(globals);
            Assert.That(set.text, Is.EqualTo("a"));
            //Assert.That(set.value, Is.EqualTo("dog"));
            set.Realize(globals);
            Assert.That(chat.locals["a"], Is.EqualTo("$obj-prop"));

            chat = ChatParser.ParseText("CHAT c1\nSET a=$animal\nSAY The $a barked ", NO_VALIDATORS)[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            set = (Set)chat.commands[0];
            set.Realize(globals);
            Assert.That(set.text, Is.EqualTo("a"));
            Assert.That(set.value, Is.EqualTo("$animal"));
            Assert.That(chat.locals["a"], Is.EqualTo("$animal"));
            Assert.That(globals["animal"], Is.EqualTo("dog"));
            chat.commands[1].Realize(globals);
            Assert.That(chat.commands[1].Text(true), Is.EqualTo("The dog barked"));
            //globals["animal"] = "cat";
            //Assert.That(globals["animal"], Is.EqualTo("cat"));
            //chat.commands[1].Realize(globals); // re-realize
            //Assert.That(chat.commands[1].Text(true), Is.EqualTo("The cat barked"));
        }

        [Test]
        public void SetGlobals()
        {
            Assert.That(globals["animal"], Is.EqualTo("dog"));
            Chat chat = ChatParser.ParseText("CHAT c1\nSET $animal=cat", NO_VALIDATORS)[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            Set set = (Dialogic.Set)chat.commands[0];
            Assert.That(set.text, Is.EqualTo("animal"));
            Assert.That(set.value, Is.EqualTo("cat"));
            /*
             * 
             * TODO: globals and locals
            set.Realize(globals);
            object outv = null;
            globals.TryGetValue("c1.animal", out outv);
            Assert.That(outv, Is.Null);
            Assert.That(globals["animal"], Is.EqualTo("cat"));

            globals["animal"] = "dog";
            Assert.That(globals["animal"], Is.EqualTo("dog"));
            */
        }

        [Test]
        public void DynamicAssign()
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
            Assert.That(say.delay, Is.EqualTo(2000));

            chat = ChatParser.ParseText("SAY ok { type = a,stage = b, delay=1.01}")[0];
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Say)));

            say = (Dialogic.Say)chat.commands[0];
            Assert.That(say, Is.Not.Null);
            Assert.That(say.meta, Is.Not.Null);
            Assert.That(say.realized, Is.Empty);
            Assert.That(say.GetMeta("type"), Is.EqualTo("a"));
            Assert.That(say.GetMeta("stage"), Is.EqualTo("b"));
            Assert.That(say.GetMeta("delay"), Is.EqualTo("1.01"));
            Assert.That(say.delay, Is.EqualTo(1.01));

            say.Realize(null);
            Assert.That(say, Is.Not.Null);
            Assert.That(say.realized, Is.Not.Null);
            Assert.That(say.GetRealized("type"), Is.EqualTo("a"));
            Assert.That(say.GetRealized("stage"), Is.EqualTo("b"));
            Assert.That(say.GetRealized("delay"), Is.EqualTo("1.01"));
            Assert.That(say.delay, Is.EqualTo(1.01));

            chat = ChatParser.ParseText("SAY ok { type = a,stage = b, delay=1, actor=Tendar}")[0];
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Say)));

            say = (Dialogic.Say)chat.commands[0];
            Assert.That(say, Is.Not.Null);
            Assert.That(say.meta, Is.Not.Null);
            Assert.That(say.realized, Is.Empty);
            Assert.That(say.GetMeta("type"), Is.EqualTo("a"));
            Assert.That(say.GetMeta("stage"), Is.EqualTo("b"));
            Assert.That(say.GetMeta("delay"), Is.EqualTo("1"));
            Assert.That(say.GetMeta("actor"), Is.EqualTo("Tendar"));
            Assert.That(say.actor, Is.EqualTo(Tendar.AppConfig.Actors[1]));
            Assert.That(say.delay, Is.EqualTo(1));

            say.Realize(null);
            Assert.That(say, Is.Not.Null);
            Assert.That(say.realized, Is.Not.Null);
            Assert.That(say.GetRealized("type"), Is.EqualTo("a"));
            Assert.That(say.GetRealized("stage"), Is.EqualTo("b"));
            Assert.That(say.GetRealized("delay"), Is.EqualTo("1"));
            Assert.That(say.GetRealized("actor"), Is.EqualTo("Tendar"));
            Assert.That(say.actor, Is.EqualTo(Tendar.AppConfig.Actors[1]));
            Assert.That(say.delay, Is.EqualTo(1));

            //Assert.That(say.ComputeDuration(), Is.EqualTo(1));
            // TODO: need to test Say.ComputeDuration...
        }

        [Test]
        public void ChatMeta()
        {
            Chat chat;

            chat = ChatParser.ParseText("CHAT c1", NO_VALIDATORS)[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.meta, Is.Not.Null);
            Assert.That(chat.realized, Is.Null);
            Assert.That(chat.staleness, Is.EqualTo(Defaults.CHAT_STALENESS));
            Assert.That(Convert.ToDouble(chat.GetMeta(Meta.STALENESS)),
                Is.EqualTo(Convert.ToDouble(Defaults.CHAT_STALENESS)));

            chat = ChatParser.ParseText("CHAT c1 { type = a, stage = b }")[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.meta, Is.Not.Null);
            Assert.That(chat.realized, Is.Null);
            Assert.That(chat.staleness, Is.EqualTo(Defaults.CHAT_STALENESS));
            Assert.That(Convert.ToDouble(chat.GetMeta(Meta.STALENESS)),
                Is.EqualTo(Convert.ToDouble(Defaults.CHAT_STALENESS)));
            Assert.That(chat.GetMeta("type"), Is.EqualTo("a"));
            Assert.That(chat.GetMeta("stage"), Is.EqualTo("b"));

            chat = ChatParser.ParseText("CHAT c1 { staleness = 1,type = a,stage = b}")[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.meta, Is.Not.Null);
            Assert.That(chat.realized, Is.Null);
            Assert.That(chat.staleness, Is.EqualTo(1));
            Assert.That(Convert.ToDouble(chat.GetMeta(Meta.STALENESS)),
                Is.EqualTo(Convert.ToDouble("1")));
            Assert.That(chat.GetMeta("type"), Is.EqualTo("a"));
            Assert.That(chat.GetMeta("stage"), Is.EqualTo("b"));

            chat = ChatParser.ParseText("CHAT c1 { resumeAfterInt=false,type = a,stage = b}")[0];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.meta, Is.Not.Null);
            Assert.That(chat.realized, Is.Null);
            Assert.That(chat.resumeAfterInt, Is.EqualTo(false));
            Assert.That(Convert.ToBoolean(chat.GetMeta(Meta.RESUME_AFTER_INT)),
                Is.EqualTo(Convert.ToBoolean("false")));
        }

        [Test]
        public void FindParameters()
        {
            Find f = new Find().Init("{ num = 1 }");
            Assert.That(f, Is.Not.Null);
            Assert.That(f.text, Is.Null);
            Assert.That(f.GetMeta("num"), Is.Not.Null);
            var meta = f.GetMeta("num");
            Assert.That(meta.GetType(), Is.EqualTo(typeof(Constraint)));
            Constraint constraint = (Dialogic.Constraint)meta;
            Assert.That(constraint.IsStrict(), Is.EqualTo(false));

            f = new Find().Init("{ num=1 }");
            Assert.That(f, Is.Not.Null);
            Assert.That(f.text, Is.Null);
            Assert.That(f.GetMeta("num"), Is.Not.Null);
            meta = f.GetMeta("num");
            Assert.That(meta.GetType(), Is.EqualTo(typeof(Constraint)));
            constraint = (Dialogic.Constraint)meta;
            Assert.That(constraint.IsStrict(), Is.EqualTo(false));

            f = new Find().Init("{num = 1}");
            Assert.That(f, Is.Not.Null);
            Assert.That(f.text, Is.Null);
            Assert.That(f.GetMeta("num"), Is.Not.Null);
            meta = f.GetMeta("num");
            Assert.That(meta.GetType(), Is.EqualTo(typeof(Constraint)));
            constraint = (Dialogic.Constraint)meta;
            Assert.That(constraint.IsStrict(), Is.EqualTo(false));

            f = new Find().Init("{num == 1}");
            Assert.That(f, Is.Not.Null);
            Assert.That(f.text, Is.Null);
            Assert.That(f.GetMeta("num"), Is.Not.Null);
            meta = f.GetMeta("num");
            Assert.That(meta.GetType(), Is.EqualTo(typeof(Constraint)));
            constraint = (Dialogic.Constraint)meta;
            Assert.That(constraint.IsStrict(), Is.EqualTo(false));
        }


        [Test]
        public void UpdateEventData()
        {
            List<Chat> chats;
            chats = ChatParser.ParseText("SAY OK {stage=S1, type=hello, length=4}");
            Assert.That(chats[0], Is.Not.Null);
            Assert.That(chats[0].commands[0], Is.Not.Null);
            Say say = (Say)chats[0].commands[0];
            say.Realize(null);
            var ue = new UpdateEvent(say);
            Assert.That(ue, Is.Not.Null);
            //Console.WriteLine(ue.Data().Stringify());
            Assert.That(ue.Data(), Is.Not.Null);
            Assert.That(ue.Get(Meta.TYPE), Is.EqualTo("Say"));
            Assert.That(ue.Get(Meta.TEXT), Is.EqualTo("OK"));
            Assert.That(ue.Get(Meta.ACTOR), Is.EqualTo(Actor.Default.Name()));
            Assert.That(ue.Get("length"), Is.EqualTo("4"));
            Assert.That(ue.GetInt("length"), Is.EqualTo(4));
            Assert.That(ue.GetFloat("length"), Is.EqualTo(4));
            Assert.That(ue.GetDouble("length"), Is.EqualTo(4));
        }

        [Test]
        public void ChatStaleness()
        {
            List<Chat> chats;
            ChatRuntime rt;

            chats = ChatParser.ParseText("CHAT c1 { type = a, stage = b }");
            Assert.That(chats[0], Is.Not.Null);
            Assert.That(chats[0].staleness, Is.EqualTo(Defaults.CHAT_STALENESS));
            rt = new ChatRuntime(chats);
            for (int i = 0; i < 5; i++) rt.Run("#c1");
            var total = Defaults.CHAT_STALENESS + (5 * Defaults.CHAT_STALENESS_INCR);
            Assert.That(chats[0].staleness, Is.EqualTo(total));

            chats = ChatParser.ParseText("CHAT c1 {staleness=10,type=a, stage=b}");
            Assert.That(chats[0], Is.Not.Null);
            Assert.That(chats[0].staleness, Is.EqualTo(10));
            rt = new ChatRuntime(chats);
            for (int i = 0; i < 5; i++) rt.Run("#c1");
            total = 10 + (5 * Defaults.CHAT_STALENESS_INCR);
            Assert.That(chats[0].staleness, Is.EqualTo(total));

            chats = ChatParser.ParseText("CHAT c1 {staleness=9.1,stalenessIncr=.6}", NO_VALIDATORS);
            Assert.That(chats[0], Is.Not.Null);
            Assert.That(chats[0].staleness, Is.EqualTo(9.1));
            rt = new ChatRuntime(chats);
            for (int i = 0; i < 5; i++) rt.Run("#c1");
            Assert.That(chats[0].staleness, Is.EqualTo(9.1 + (5 * .6)).Within(.01));

            chats = ChatParser.ParseText("CHAT c1 {staleness=9,stalenessIncr=2}", NO_VALIDATORS);
            Assert.That(chats[0], Is.Not.Null);
            Assert.That(chats[0].staleness, Is.EqualTo(9));
            rt = new ChatRuntime(chats);
            for (int i = 0; i < 5; i++) rt.Run("#c1");
            Assert.That(chats[0].staleness, Is.EqualTo(9 + 5 * 2).Within(.01));
            chats[0].Staleness(0);
            chats[0].StalenessIncr(10);
            for (int i = 0; i < 5; i++) rt.Run("#c1");
            Assert.That(chats[0].staleness, Is.EqualTo(0 + 5 * 10).Within(.01));
        }

        [Test]
        public void AssignActors()
        {
            List<Chat> chats;

            chats = ChatParser.ParseText("Guppy:SAY Hello from Guppy");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Say say = (Dialogic.Say)chats[0].commands[0];
            Assert.That(say.text, Is.EqualTo("Hello from Guppy"));
            Assert.That(say.actor.Name(), Is.EqualTo("Guppy"));
            say.Realize(null);
            Assert.That(say.realized[Meta.TYPE], Is.EqualTo("Say"));
            Assert.That(say.realized[Meta.TEXT], Is.EqualTo("Hello from Guppy"));
            Assert.That(say.realized[Meta.ACTOR], Is.EqualTo("Guppy"));

            chats = ChatParser.ParseText("Guppy:DO #HelloSpin");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Do)));
            Do doo = (Dialogic.Do)chats[0].commands[0];
            Assert.That(doo.text, Is.EqualTo("HelloSpin"));
            Assert.That(doo.actor.Name(), Is.EqualTo("Guppy"));
            doo.Realize(null);
            Assert.That(doo.realized[Meta.TYPE], Is.EqualTo("Do"));
            Assert.That(doo.realized[Meta.TEXT], Is.EqualTo("HelloSpin"));
            Assert.That(doo.realized[Meta.ACTOR], Is.EqualTo("Guppy"));

            chats = ChatParser.ParseText("GO #HelloSpin");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Go)));
            Go go = (Dialogic.Go)chats[0].commands[0];
            Assert.That(doo.text, Is.EqualTo("HelloSpin"));
            go.Realize(null);
            Assert.That(go.realized[Meta.TYPE], Is.EqualTo("Go"));
            Assert.That(go.realized[Meta.TEXT], Is.EqualTo("HelloSpin"));

            chats = ChatParser.ParseText("Guppy: Hello from Guppy");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            say = (Dialogic.Say)chats[0].commands[0];
            Assert.That(say.text, Is.EqualTo("Hello from Guppy"));
            Assert.That(say.actor.Name(), Is.EqualTo("Guppy"));

            chats = ChatParser.ParseText("Tendar:Hello from Tendar");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            say = (Dialogic.Say)chats[0].commands[0];
            Assert.That(say.text, Is.EqualTo("Hello from Tendar"));
            Assert.That(say.actor.Name(), Is.EqualTo("Tendar"));

            chats = ChatParser.ParseText("Hello from Guppy");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            say = (Dialogic.Say)chats[0].commands[0];
            Assert.That(say.text, Is.EqualTo("Hello from Guppy"));
            Assert.That(say.actor.Name(), Is.EqualTo(Actor.Default.Name()));
        }

        [Test]
        public void Prompts()
        {
            List<Chat> chats = ChatParser.ParseText("ASK Want a game?\nOPT Y #Game\n\nOPT N #End");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Ask)));
            Ask ask = (Dialogic.Ask)chats[0].commands[0];
            Assert.That(ask.text, Is.EqualTo("Want a game?"));
            Assert.That(ask.Options().Count, Is.EqualTo(2));

            var options = ask.Options();
            Assert.That(options[0].GetType(), Is.EqualTo(typeof(Opt)));
            Assert.That(options[0].text, Is.EqualTo("Y"));
            Assert.That(options[0].action.GetType(), Is.EqualTo(typeof(Go)));
            Assert.That(options[1].GetType(), Is.EqualTo(typeof(Opt)));
            Assert.That(options[1].text, Is.EqualTo("N"));
            Assert.That(options[1].action.GetType(), Is.EqualTo(typeof(Go)));
        }

        [Test]
        public void StripComments()
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
        public void ParseComments()
        {
            var txt = "SAY Thank you/*\nSAY Hello //And Goodbye*/";
            var res = ChatParser.StripComments(txt);
            Assert.That(res[0], Is.EqualTo("SAY Thank you"));
            Assert.That(res[1], Is.EqualTo(""));

            List<Chat> chats;
            chats = ChatParser.ParseText(txt);
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));

            chats = ChatParser.ParseText("SAY Thank you\n//SAY Hello\n// And Goodbye");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));

            chats = ChatParser.ParseText("SAY Thank you\nSAY Hello //And Goodbye");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(2));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Assert.That(chats[0].commands[1].text, Is.EqualTo("Hello"));
            Assert.That(chats[0].commands[1].GetType(), Is.EqualTo(typeof(Say)));

            chats = ChatParser.ParseText("SAY Thank you\nSAY Hello /*And Goodbye*/");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(2));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Assert.That(chats[0].commands[1].text, Is.EqualTo("Hello"));
            Assert.That(chats[0].commands[1].GetType(), Is.EqualTo(typeof(Say)));

            chats = ChatParser.ParseText("SAY Thank you\n//SAY Hello\nAnd Goodbye\n");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(2));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Assert.That(chats[0].commands[1].text, Is.EqualTo("And Goodbye"));
            Assert.That(chats[0].commands[1].GetType(), Is.EqualTo(typeof(Say)));

            chats = ChatParser.ParseText("SAY Thank you\n//SAY Goodbye\nAnd Hello");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(2));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Assert.That(chats[0].commands[1].text, Is.EqualTo("And Hello"));
            Assert.That(chats[0].commands[1].GetType(), Is.EqualTo(typeof(Say)));
        }

        [Test]
        public void FindSoft()
        {
            List<Chat> chats;

            chats = ChatParser.ParseText("FIND {num=1}");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].text, Is.Null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));
            Assert.That(chats[0].commands[0].GetMeta("num"), Is.Not.Null);
            var meta = chats[0].commands[0].GetMeta("num");
            Assert.That(meta.GetType(), Is.EqualTo(typeof(Constraint)));
            Constraint constraint = (Dialogic.Constraint)meta;
            Assert.That(constraint.IsStrict(), Is.EqualTo(false));

            chats = ChatParser.ParseText("FIND {a*=(hot|cool)}");
            var find = chats[0].commands[0];
            Assert.That(find.GetType(), Is.EqualTo(typeof(Find)));
            Assert.That(chats[0].commands[0].text, Is.Null);
            Assert.That(chats[0].commands[0].GetMeta("a"), Is.Not.Null);
            meta = chats[0].commands[0].GetMeta("a");
            Assert.That(meta.GetType(), Is.EqualTo(typeof(Constraint)));
            constraint = (Dialogic.Constraint)meta;
            Assert.That(constraint.IsStrict(), Is.EqualTo(false));

            chats = ChatParser.ParseText("FIND {do=1}");
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            var finder = chats[0].commands[0];
            Assert.That(finder.GetType(), Is.EqualTo(typeof(Find)));
            meta = chats[0].commands[0].GetMeta("do");
            Assert.That(meta.GetType(), Is.EqualTo(typeof(Constraint)));
            constraint = (Dialogic.Constraint)meta;
            Assert.That(constraint.IsStrict(), Is.EqualTo(false));
        }

        [Test]
        public void FindSoftDynVar()
        {
            List<Chat> chats;

            chats = ChatParser.ParseText("FIND {num=$count}");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].text, Is.Null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));

            chats[0].commands[0].Realize(globals);

            Assert.That(chats[0].commands[0].GetMeta("num"), Is.Not.Null);
            var meta = chats[0].commands[0].GetMeta("num");
            Assert.That(meta.GetType(), Is.EqualTo(typeof(Constraint)));
            Constraint constraint = (Constraint)meta;
            Assert.That(constraint.IsStrict(), Is.EqualTo(false));
            Assert.That(constraint.name, Is.EqualTo("num"));
            Assert.That(constraint.value, Is.EqualTo("$count"));
        }

        [Test]
        public void FindSoftDynGroup()
        {
            // "FIND {a*=(hot|cool)}" in regex, means hot OR cool, no subs
            var chats = ChatParser.ParseText("FIND {a*=(hot|cool)}");
            var find = chats[0].commands[0];
            find.Realize(globals);

            Assert.That(find.GetType(), Is.EqualTo(typeof(Find)));
            Assert.That(chats[0].commands[0].text, Is.Null);
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
        public void FindHard()
        {
            List<Chat> chats;

            chats = ChatParser.ParseText("FIND {!num=1}");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].text, Is.Null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));
            Assert.That(chats[0].commands[0].GetMeta("num"), Is.Not.Null);
            var meta = chats[0].commands[0].GetMeta("num");
            Assert.That(meta.GetType(), Is.EqualTo(typeof(Constraint)));
            Constraint constraint = (Dialogic.Constraint)meta;
            Assert.That(constraint.IsStrict(), Is.EqualTo(true));

            chats = ChatParser.ParseText("FIND {!a*=(hot|cool)}");
            var find = chats[0].commands[0];
            Assert.That(find.GetType(), Is.EqualTo(typeof(Find)));
            Assert.That(chats[0].commands[0].text, Is.Null);
            Assert.That(chats[0].commands[0].GetMeta("a"), Is.Not.Null);
            meta = chats[0].commands[0].GetMeta("a");
            Assert.That(meta.GetType(), Is.EqualTo(typeof(Constraint)));
            constraint = (Dialogic.Constraint)meta;
            Assert.That(constraint.IsStrict(), Is.EqualTo(true));

            chats = ChatParser.ParseText("FIND {!do=1}");
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            var finder = chats[0].commands[0];
            Assert.That(finder.GetType(), Is.EqualTo(typeof(Find)));
            meta = chats[0].commands[0].GetMeta("do");
            Assert.That(meta.GetType(), Is.EqualTo(typeof(Constraint)));
            constraint = (Dialogic.Constraint)meta;
            Assert.That(constraint.IsStrict(), Is.EqualTo(true));
        }

        [Test]
        public void WaitCommand()
        {
            List<Chat> chats;

            chats = ChatParser.ParseText("WAIT .5 {waitForAnimation=true}");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Wait)));
            Wait wait = (Dialogic.Wait)chats[0].commands[0];
            Assert.That(wait.text, Is.EqualTo(".5"));
            Assert.That(wait.delay, Is.EqualTo(.5));
            Assert.That(wait.ComputeDuration(), Is.EqualTo(500));
            Assert.That(wait.GetMeta("waitForAnimation"), Is.EqualTo("true"));
        }

        [Test]
        public void Validators()
        {
            List<Chat> chats;
            chats = ChatParser.ParseText("CHAT c1 {type=a,stage=b}\nSAY Hello");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].text, Is.EqualTo("Hello"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
        }

        [Test]
        public void ToGuppyScript()
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
                "DO #hay {a=b}",
                "FIND {!a=b,staleness<=5}",
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

            s = "SET $a = 4";
            Assert.That(ChatParser.ParseText(s)[0].commands[0].ToString(), Is.EqualTo(s));

            s = "ASK hay is for horses?\nOPT Yes? #Yes\nOPT No? #No";
            var exp = s.Replace("\n", "\n  ").Split('\n');
            var res = ChatParser.ParseText(s)[0].commands[0].ToString().Split('\n');

            Assert.That(res.Length, Is.EqualTo(exp.Length));
            for (int i = 0; i < exp.Length; i++)
            {
                Assert.That(res[i], Is.EqualTo(exp[i]));
            }
        }

        [Test]
        public void DynamicLabels()
        {
            var chats = ChatParser.ParseText("GO #Spin");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Go)));
            Go go = (Dialogic.Go)chats[0].commands[0];
            go.Realize(null);
            Assert.That(go.Text(true), Is.EqualTo("Spin"));

            chats = ChatParser.ParseText("GO Spin");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Go)));
            go = (Dialogic.Go)chats[0].commands[0];
            go.Realize(null);
            Assert.That(go.Text(true), Is.EqualTo("Spin"));


            chats = ChatParser.ParseText("GO #(Spin | Twirl)");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Go)));
            go = (Dialogic.Go)chats[0].commands[0];
            go.Realize(null);
            Assert.That(go.Text(true), Is.EqualTo("Twirl").Or.EqualTo("Spin"));


            chats = ChatParser.ParseText("GO (Spin | Twirl)");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Go)));
            chats[0].commands[0].Realize(null);
            Assert.That(chats[0].commands[0].Text(true), Is.EqualTo("Twirl").Or.EqualTo("Spin"));


            chats = ChatParser.ParseText("GO #(ChatA | ChatB)");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Go)));
            chats[0].commands[0].Realize(null);
            Assert.That(chats[0].commands[0].Text(true), Is.EqualTo("ChatA").Or.EqualTo("ChatB"));

            chats = ChatParser.ParseText("GO #(A | B | C) ");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Go)));
            chats[0].commands[0].Realize(null);
            Assert.That(chats[0].commands[0].Text(true), Is.EqualTo("A").Or.EqualTo("B").Or.EqualTo("C"));

            chats = ChatParser.ParseText("GO (A | B | C) ");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Go)));
            chats[0].commands[0].Realize(null);
            Assert.That(chats[0].commands[0].Text(true), Is.EqualTo("A").Or.EqualTo("B").Or.EqualTo("C"));

            chats = ChatParser.ParseText("DO #(MoveA | MoveB) {metatime=(.7|.9)}");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Do)));
            chats[0].commands[0].Realize(null);
            Assert.That(chats[0].commands[0].Text(true), Is.EqualTo("MoveA").Or.EqualTo("MoveB"));
            Assert.That(chats[0].commands[0].HasMeta("metatime"), Is.EqualTo(true));
            Assert.That(chats[0].commands[0].GetMeta("metatime"), Is.EqualTo("(.7|.9)"));
            Assert.That(chats[0].commands[0].GetRealized("metatime"), Is.EqualTo(".7").Or.EqualTo(".9"));

            chats = ChatParser.ParseText("DO emote { type = (A|B)}");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Do)));
            chats[0].commands[0].Realize(null);
            Assert.That(chats[0].commands[0].Text(true), Is.EqualTo("emote"));
            Assert.That(chats[0].commands[0].HasMeta("type"), Is.EqualTo(true));
            Assert.That(chats[0].commands[0].GetMeta("type"), Is.EqualTo("(A|B)"));
            Assert.That(chats[0].commands[0].GetRealized("type"), Is.EqualTo("A").Or.EqualTo("B"));

            ////////////////////////////////////////////////////////////////////

            chats = ChatParser.ParseText("DO #(Twirl | Spin)");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Do)));
            chats[0].commands[0].Realize(null);
            Assert.That(chats[0].commands[0].Text(true), Is.EqualTo("Twirl").Or.EqualTo("Spin"));

            chats = ChatParser.ParseText("DO (Twirl | Spin)");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Do)));
            chats[0].commands[0].Realize(null);
            Assert.That(chats[0].commands[0].Text(true), Is.EqualTo("Twirl").Or.EqualTo("Spin"));

            chats = ChatParser.ParseText("DO Spin");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Do)));
            chats[0].commands[0].Realize(null);
            Assert.That(chats[0].commands[0].Text(true), Is.EqualTo("Spin"));

            chats = ChatParser.ParseText("DO #Spin");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Do)));
            chats[0].commands[0].Realize(null);
            Assert.That(chats[0].commands[0].Text(true), Is.EqualTo("Spin"));
        }

        [Test]
        public void Commands()
        {
            List<Chat> chats;

            chats = ChatParser.ParseText("ASK is for horses?\nOPT Yes #game");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].text, Is.EqualTo("is for horses?"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Ask)));
            Ask a = (Ask)chats[0].commands[0];
            var opts = a.Options();
            Assert.That(opts[0].text, Is.EqualTo("Yes"));
            Assert.That(opts[0].action.text, Is.EqualTo("game"));

            chats = ChatParser.ParseText("HAY is for horses");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].text, Is.EqualTo("HAY is for horses"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));

            chats = ChatParser.ParseText("hello");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].text, Is.EqualTo("hello"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));

            chats = ChatParser.ParseText("wei");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].text, Is.EqualTo("wei"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));

            chats = ChatParser.ParseText("SAY ...");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].text, Is.EqualTo("..."));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));

            chats = ChatParser.ParseText("...");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].text, Is.EqualTo("..."));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));

            chats = ChatParser.ParseText("GO #Twirl");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].text, Is.EqualTo("Twirl"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Go)));

            chats = ChatParser.ParseText("GO Twirl");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].text, Is.EqualTo("Twirl"));

            chats = ChatParser.ParseText("DO #Twirl");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].text, Is.EqualTo("Twirl"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Do)));

            chats = ChatParser.ParseText("DO Twirl");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].text, Is.EqualTo("Twirl"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Do)));

            chats = ChatParser.ParseText("SAY Thank you");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));

            chats = ChatParser.ParseText("SAY Thank you\n \t\nAnd Goodbye");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(2));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Assert.That(chats[0].commands[1].text, Is.EqualTo("And Goodbye"));
            Assert.That(chats[0].commands[1].GetType(), Is.EqualTo(typeof(Say)));

            chats = ChatParser.ParseText("SAY Thank you { pace = fast}");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Assert.That(chats[0].commands[0].GetMeta("pace"), Is.EqualTo("fast"));

            chats = ChatParser.ParseText("SAY Thank you {pace=fast,count=2}");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Assert.That(chats[0].commands[0].HasMeta(), Is.EqualTo(true));
            Assert.That(chats[0].commands[0].GetMeta("pace"), Is.EqualTo("fast"));
            Assert.That(chats[0].commands[0].GetMeta("count"), Is.EqualTo("2"));

            chats = ChatParser.ParseText("SAY Thank you {}");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Assert.That(chats[0].commands[0].HasMeta(), Is.EqualTo(false));

            // meta variations
            chats = ChatParser.ParseText("ok { pace = fast }");
            Assert.That(chats[0].commands[0].text, Is.EqualTo("ok"));
            Assert.That(chats[0].commands[0].GetMeta("pace"), Is.EqualTo("fast"));

            chats = ChatParser.ParseText("ok {pace = fast}");
            Assert.That(chats[0].commands[0].text, Is.EqualTo("ok"));
            Assert.That(chats[0].commands[0].GetMeta("pace"), Is.EqualTo("fast"));

            chats = ChatParser.ParseText("ok {pace =fast}");
            Assert.That(chats[0].commands[0].text, Is.EqualTo("ok"));
            Assert.That(chats[0].commands[0].GetMeta("pace"), Is.EqualTo("fast"));

            chats = ChatParser.ParseText("ok {pace= fast}");
            Assert.That(chats[0].commands[0].text, Is.EqualTo("ok"));
            Assert.That(chats[0].commands[0].GetMeta("pace"), Is.EqualTo("fast"));

            chats = ChatParser.ParseText("ok {pace= fast}");
            Assert.That(chats[0].commands[0].text, Is.EqualTo("ok"));
            Assert.That(chats[0].commands[0].GetMeta("pace"), Is.EqualTo("fast"));

            chats = ChatParser.ParseText("ok {a=b,c=d}");
            Assert.That(chats[0].commands[0].text, Is.EqualTo("ok"));
            Assert.That(chats[0].commands[0].GetMeta("a"), Is.EqualTo("b"));
            Assert.That(chats[0].commands[0].GetMeta("c"), Is.EqualTo("d"));

            chats = ChatParser.ParseText("ok {a=b, c=d}");
            Assert.That(chats[0].commands[0].text, Is.EqualTo("ok"));
            Assert.That(chats[0].commands[0].GetMeta("a"), Is.EqualTo("b"));
            Assert.That(chats[0].commands[0].GetMeta("c"), Is.EqualTo("d"));

            chats = ChatParser.ParseText("ok {a=b ,c=d}");
            Assert.That(chats[0].commands[0].text, Is.EqualTo("ok"));
            Assert.That(chats[0].commands[0].GetMeta("a"), Is.EqualTo("b"));
            Assert.That(chats[0].commands[0].GetMeta("c"), Is.EqualTo("d"));

            chats = ChatParser.ParseText("ok { a = b , c = d }");
            Assert.That(chats[0].commands[0].text, Is.EqualTo("ok"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Assert.That(chats[0].commands[0].TypeName(), Is.EqualTo("Say"));
            Assert.That(chats[0].commands[0].GetMeta("a"), Is.EqualTo("b"));
            Assert.That(chats[0].commands[0].GetMeta("c"), Is.EqualTo("d"));

            chats = ChatParser.ParseText("NVM 1.1");
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Tendar.Nvm)));
            Assert.That(chats[0].commands[0].TypeName(), Is.EqualTo("Nvm"));
            Assert.That(chats[0].commands[0].text, Is.EqualTo("1.1"));
            Assert.That(chats[0].commands[0].delay, Is.EqualTo(1.1));
            Assert.That(chats[0].commands[0].ComputeDuration(), Is.EqualTo(1100));
        }

        [Test]
        public void SimpleCommands()
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
        public void ChatParsing()
        {
            List<Chat> chats = ChatParser.ParseText("CHAT c1 {type=a,stage=b}");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(0));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Chat chat = chats[0];
            Assert.That(chats[0].text, Is.EqualTo("c1"));

            chats = ChatParser.ParseText("CHAT c1 {type=a,stage=b}\nGO #c1\nDO #c1\n");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count(), Is.EqualTo(2));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Go)));
            Assert.That(chats[0].commands[1].GetType(), Is.EqualTo(typeof(Do)));
        }

        [Test]
        public void Exceptions()
        {
            //var ff = "FIND {a b=e}";
            //Console.WriteLine("\n"+ChatParser.ParseText(ff)[0].ToTree());

            Assert.Throws<ParseException>(() => ChatParser.ParseText("SET A ="));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("SET A 3"));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("SET A: 3"));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("SET A"));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("SET"));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("SET A:"));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("SET A="));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("SET A :"));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("SET A c | d"));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("GO A {a=b}"));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("CHAT c1"));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("CHAT x{t pe=a,stage=b}", NO_VALIDATORS));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("CHAT x{type=a b,stage=b}", NO_VALIDATORS));
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
