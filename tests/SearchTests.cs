using NUnit.Framework;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Dialogic
{
    [TestFixture]
    public class SearchTests : GenericTests
    {
        // TODO: test each event with a single find+go

        [Test]
        public void SearchFailWithConstraint()
        {
            var lines = new[] {
                 "CHAT Test {type=a,stage=b}",
                 "SAY Find",
                 "FIND {type=a,x=b}",
                 "CHAT next {type=a,stage=b}",
                 "SAY Done",
             };

            ChatRuntime rt = new ChatRuntime(Tendar.AppConfig.Actors);
            rt.ParseText(String.Join("\n", lines));

            var s = rt.InvokeImmediate(null);
            Assert.That(s, Is.EqualTo("Find\nDone"));

            //FuzzySearch.DBUG = true;

            lines = new[] {
                 "CHAT Test {type=a,stage=b}",
                 "SAY Find",
                 "FIND {type=a,staleness<5}",
                 "CHAT next {type=a,stage=b,staleness=5}",
                 "SAY Done",
             };

            rt = new ChatRuntime(Tendar.AppConfig.Actors);
            rt.ParseText(String.Join("\n", lines));

            s = rt.InvokeImmediate(null);
            Assert.That(s, Is.EqualTo("Find\nDone"));
        }
            
        [Test]
        public void SearchWithHardConstraint()
        {
            string[] lines = {
                "CHAT CORE_Shake {type=shake, stage=CORE}",
                "SAY Core shake!",

                "CHAT CORE_Tap {type=tap, stage=CORE}",
                "SAY Core tap!",

                "CHAT CORE_Stale_Fast {type=critic, stage=CORE}",
                "SAY Core critic!",

                "CHAT NV_Shake {type=shake, stage=NV}",
                "SAY NV shake!",

                "CHAT NV_Tap {type=tap, stage=NV}",
                "SAY NV tap!",

                "CHAT NV_Stale_Fast {type=critic, stage=NV}",
                "SAY NV critic!",
            };
            string contents = String.Join("\n", lines);
            ChatRuntime rt = new ChatRuntime(Tendar.AppConfig.Actors);
            rt.ParseText(contents);
            var finder = new Find().Init("{!!type=tap,!stage=CORE}");

            var chat = rt.DoFind((Dialogic.Find)finder);
            Assert.That(chat.text, Is.EqualTo("CORE_Tap"));
        }

        [Test]
        public void FindChatByLabel()
        {
            string[] lines = {
                "CHAT c1 {day=fri}",
                "CHAT c2 {dev=2,day=fri}",
                "CHAT c3"
            };
            string contents = String.Join("\n", lines);
            List<Chat> chats = ChatParser.ParseText(contents, NO_VALIDATORS);
            Chat result = new ChatRuntime(chats).FindChatByLabel("c1");
            //chats.ForEach(c=>Console.WriteLine(c));
            Assert.That(result, Is.Not.Null);
            Assert.That(result.text, Is.EqualTo("c1"));

            Chat c;
            chats = new List<Chat>();
            chats.Add(c = Chat.Create("c1"));
            chats.Add(c = Chat.Create("c2"));
            chats.Add(c = Chat.Create("c3"));
            ChatRuntime cr = new ChatRuntime(chats);
            Chat res = new ChatRuntime(chats).FindChatByLabel("c2");
            Assert.That(res.text, Is.EqualTo("c2"));
        }

        [Test]
        public void BasicFind()
        {
            Chat c;
            List<Chat> chats = new List<Chat>();
            chats.Add(c = Chat.Create("c1"));
            chats.Add(c = Chat.Create("c2"));
            c.SetMeta("dev", "1");
            c.SetMeta("day", "hello");
            chats.Add(c = Chat.Create("c3"));
            Chat res = new ChatRuntime(chats).DoFind(null, new Constraint("dev", "1"));
            Assert.That(res.text, Is.EqualTo("c2"));
        }


        [Test]
        public void BasicFindAll()
        {
            Chat c;
            List<Chat> chats = new List<Chat>();
            chats.Add(c = Chat.Create("c1"));
            chats.Add(c = Chat.Create("c2"));
            c.SetMeta("dev", "1");
            c.SetMeta("day", "hello");
            chats.Add(c = Chat.Create("c3"));
            Chat res = new ChatRuntime(chats).DoFindAll(null, new Constraint("dev", "1"))[0];
            Assert.That(res.text, Is.EqualTo("c2"));
        }


        [Test]
        public void FindAll()
        {
            string[] lines = {
                "CHAT c1 {day=fri}",
                "CHAT c2 {dev=2,day=fri}",
                "CHAT c3 {}"
            };
            string contents = String.Join("\n", lines);
            List<Chat> chats = ChatParser.ParseText(contents, NO_VALIDATORS);
            List<Chat> result = new ChatRuntime(chats).DoFindAll(null,
                new Constraint("dev", "1"),
                new Constraint("day", "fri")
            );

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].text, Is.EqualTo("c1"));

            Chat c;
            chats = new List<Chat>();
            chats.Add(c = Chat.Create("c1"));
            chats.Add(c = Chat.Create("c2"));
            c.SetMeta("dev", "1");
            c.SetMeta("day", "hello");
            chats.Add(c = Chat.Create("c3"));
            ChatRuntime cr = new ChatRuntime(chats);
            chats = cr.DoFindAll(null, new Constraint("dev", "1"));
            //chats.ForEach((obj) => Console.WriteLine(obj.text));
            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(3));
            Assert.That(chats[0].text, Is.EqualTo("c2"));

            chats = new List<Chat>();
            chats.Add(c = Chat.Create("c1"));
            c.SetMeta("dev", "2");
            chats.Add(c = Chat.Create("c2"));
            c.SetMeta("dev", "1");
            c.SetMeta("day", "hello");
            chats.Add(c = Chat.Create("c3"));

            cr = new ChatRuntime(chats);
            chats = cr.DoFindAll(null, new Constraint("dev", "1"));
            //chats.ForEach((obj) => Console.WriteLine(obj.text));
            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(2));
            Assert.That(chats[0].text, Is.EqualTo("c2"));
            Assert.That(chats[1].text, Is.EqualTo("c3"));
        }


        [Test]
        public void FindAll2()
        {
            string[] lines = {
                "CHAT c0",
                "FIND {dev=1,day=fri}",
                "CHAT c1 {day=fri}",
                "CHAT c2 {dev=2,day=fri}",
                "CHAT c3 {}"
            };

            string contents = String.Join("\n", lines);


            List<Chat> chats = ChatParser.ParseText(contents, NO_VALIDATORS);
            //chats.ForEach((ch) => Console.WriteLine(ch.ToTree()));
            Command finder = chats[0].commands[0]; finder.Resolve(null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));

            chats = new ChatRuntime(chats).DoFindAll((Find)finder, null);

            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(2));

            Assert.That(chats[0], Is.Not.Null);
            Assert.That(chats[0].text, Is.EqualTo("c1"));

            lines = new[]{
                "CHAT c0",
                "FIND {!dev=1,day=fri}",
                "CHAT c1 {day=fri}",
                "CHAT c2 {dev=2,day=fri}",
                "CHAT c3 {}"
            };

            contents = String.Join("\n", lines);

            chats = ChatParser.ParseText(contents, NO_VALIDATORS);
            //chats.ForEach((ch) => Console.WriteLine(ch.ToTree()));
            finder = chats[0].commands[0]; finder.Resolve(null);
            var mdev = finder.GetMeta("dev");
            Assert.That(mdev is Constraint, Is.True);
            Constraint cons = (Constraint)mdev;
            Assert.That(cons.type, Is.EqualTo(ConstraintType.Hard));
            Assert.That(cons.IsStrict(), Is.True);
            Assert.That(finder.GetType(), Is.EqualTo(typeof(Find)));

            var rt = new ChatRuntime(chats);
            rt.Chats().ForEach(c => c.Resolve(null));
            chats = rt.DoFindAll((Find)finder, null);

            //chats.ForEach((obj) => Console.WriteLine(obj.text));

            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(0));
        }

        [Test]
        public void FindAllWithMetaVars()
        {
            string[] lines = {
                "CHAT c1 {day=fri}", // parent not returned 
                "CHAT c2 {dev=4,day=fri}",
                "CHAT c3 {dev=3}",
                "CHAT c4"
            };
            string contents = String.Join("\n", lines);
            List<Chat> chats = ChatParser.ParseText(contents, NO_VALIDATORS);
            Assert.That(globals["count"], Is.EqualTo(4));
            List<Chat> result = new ChatRuntime(chats).DoFindAll
                (chats[0], globals, new Constraint("dev", "$count"));
            //chats.ForEach((obj) => Console.WriteLine(obj.text));
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].text, Is.EqualTo("c2"));
        }

        [Test]
        public void FindAllHard()
        {
            Chat c;
            List<Chat> chats = new List<Chat>();
            chats.Add(c = Chat.Create("c1"));
            chats.Add(c = Chat.Create("c2"));
            c.SetMeta("dev", "1");
            c.SetMeta("day", "hello");
            chats.Add(c = Chat.Create("c3"));
            ChatRuntime cr = new ChatRuntime(chats);
            chats = cr.DoFindAll(null, new Constraint("dev", "1", ConstraintType.Hard));
            //chats.ForEach((obj) => Console.WriteLine(obj.text));
            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].text, Is.EqualTo("c2"));
        }

        [Test]
        public void FindAllHard2()
        {
            Chat c;
            List<Chat> chats = new List<Chat>();
            chats.Add(c = Chat.Create("c1"));
            c.SetMeta("dev", "2");
            chats.Add(c = Chat.Create("c2"));
            c.SetMeta("dev", "1");
            c.SetMeta("day", "hello");
            chats.Add(c = Chat.Create("c3"));

            ChatRuntime cr = new ChatRuntime(chats);
            chats = cr.DoFindAll(null, new Constraint("dev", "1", ConstraintType.Hard));
            //chats.ForEach((obj) => Console.WriteLine(obj.text));
            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].text, Is.EqualTo("c2"));
        }


        [Test]
        public void FindAllHard3()
        {
            var lines = new string[]{
                "CHAT c0",
                "FIND {dev=1,!day=fri}",
                "CHAT c1 {day=fri}",
                "CHAT c2 {dev=2,day=fri}",
                "CHAT c3 {}"
            };

            var contents = String.Join("\n", lines);

            var chats = ChatParser.ParseText(contents, NO_VALIDATORS);
            //chats.ForEach((ch) => Console.WriteLine(ch.ToTree()));
            var finder = chats[0].commands[0]; finder.Resolve(null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));

            var mday = finder.GetMeta("day");
            Assert.That(mday is Constraint, Is.True);
            var cons = (Dialogic.Constraint)mday;
            Assert.That(cons.type, Is.EqualTo(ConstraintType.Hard));
            Assert.That(cons.IsStrict, Is.True);

            var cr = new ChatRuntime(chats);

            chats = cr.DoFindAll((Find)finder, null);
            //chats.ForEach((obj) => Console.WriteLine(obj.text));

            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(1));

            Assert.That(chats[0], Is.Not.Null);
            Assert.That(chats[0].text, Is.EqualTo("c1"));
        }


        [Test]
        public void FindAllHard4()
        {
            var lines = new string[] {
                "CHAT c0",
                "FIND {dev=1,!!day=fri}",
                "CHAT c1 {day=fri}",
                "CHAT c2 {dev=2,day=fri}",
                "CHAT c3 {}"
            };

            var chats = ChatParser.ParseText(String.Join("\n", lines), NO_VALIDATORS);
            chats.ForEach(c => c.Resolve(null));

            //chats.ForEach((ch) => Console.WriteLine(ch.ToTree()));
            var finder = chats[0].commands[0]; finder.Resolve(null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));

            var mday = finder.GetMeta("day");
            Assert.That(mday is Constraint, Is.True);
            var cons = (Constraint)mday;
            Assert.That(cons.type, Is.EqualTo(ConstraintType.Absolute));
            Assert.That(cons.IsStrict(), Is.True);

            chats = new ChatRuntime(chats).DoFindAll((Find)finder, null);
            //chats.ForEach((obj) => Console.WriteLine(obj.text));
            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0], Is.Not.Null);
            Assert.That(chats[0].text, Is.EqualTo("c1"));
        }

        [Test]
        public void FindAllOps()
        {
            string[] lines = { "CHAT c0 {dev=1}", "FIND {dev>2,day=fri}" };

            string contents = String.Join("\n", lines);

            List<Chat> chats = ChatParser.ParseText(contents, NO_VALIDATORS);
            chats.ForEach(c => c.Resolve(null));

            //chats.ForEach((ch) => Console.WriteLine(ch.ToTree()));
            Command finder = chats[0].commands[0]; finder.Resolve(null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));
            ChatRuntime cr = new ChatRuntime(chats);

            chats = cr.DoFindAll((Find)finder, null);
            //chats.ForEach((obj) => Console.WriteLine(obj.text));

            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(0));

            lines = new[]{
                "CHAT c0",
                "FIND {dev>1,day=fri}",
                "CHAT c1 {dev=2,day=fri}",
                "CHAT c2 {dev=1,day=fri}",
                "CHAT c3 {}"
            };

            contents = String.Join("\n", lines);

            chats = ChatParser.ParseText(contents, NO_VALIDATORS);
            //chats.ForEach((ch) => Console.WriteLine(ch.ToTree()));
            finder = chats[0].commands[0]; finder.Resolve(null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));
            cr = new ChatRuntime(chats);

            chats = cr.DoFindAll((Find)finder, null);
            //chats.ForEach((obj) => Console.WriteLine(obj.text));

            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(2));

            Assert.That(chats[0], Is.Not.Null);
            Assert.That(chats[0].text, Is.EqualTo("c1"));
        }

        [Test]
        public void FindAllOp3()
        {
            string[] lines = {
                "CHAT c1 {emotion=cold}",
                "FIND {emotion*=ho}",
                "CHAT c2 {emotion=hot}"
            };

            var contents = String.Join("\n", lines);

            var chats = ChatParser.ParseText(contents, NO_VALIDATORS);
            //chats.ForEach((ch) => Console.WriteLine(ch.ToTree()));
            var finder = chats[0].commands[0]; finder.Resolve(null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));

            chats = new ChatRuntime(chats).DoFindAll((Find)finder, null);
            //chats.ForEach((obj) => Console.WriteLine(obj.text));

            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(1));

            Assert.That(chats[0], Is.Not.Null);
            Assert.That(chats[0].text, Is.EqualTo("c2"));

            lines = new string[]{
                "CHAT c1 {emotion=cold}",
                "FIND {emotion^=h}",
                "CHAT c2 {emotion=hot}",
                "CHAT c3 {emotion=that}"
            };

            contents = String.Join("\n", lines);

            chats = ChatParser.ParseText(contents, NO_VALIDATORS);
            //chats.ForEach((ch) => Console.WriteLine(ch.ToTree()));
            finder = chats[0].commands[0]; finder.Resolve(null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));

            chats = new ChatRuntime(chats).DoFindAll((Find)finder, null);
            //chats.ForEach((obj) => Console.WriteLine(obj.text));

            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(1));

            Assert.That(chats[0], Is.Not.Null);
            Assert.That(chats[0].text, Is.EqualTo("c2"));

            lines = new string[]{
                "CHAT c1 {emotion=hot}",
                "FIND {emotion$=ld}",
                "CHAT c2 {emotion=hot}",
                "CHAT c3 {emotion=cold}"
            };

            contents = String.Join("\n", lines);

            chats = ChatParser.ParseText(contents, NO_VALIDATORS);
            //chats.ForEach((ch) => Console.WriteLine(ch.ToTree()));
            finder = chats[0].commands[0]; finder.Resolve(null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));

            chats = new ChatRuntime(chats).DoFindAll((Find)finder, null);
            //chats.ForEach((obj) => Console.WriteLine(obj.text));

            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(1));

            Assert.That(chats[0], Is.Not.Null);
            Assert.That(chats[0].text, Is.EqualTo("c3"));
        }


        [Test]
        public void Staleness()
        {
            Chat c;
            List<Chat> chats = new List<Chat>();
            chats.Add(c = Chat.Create("c1"));
            c.SetMeta("dev", "hello");
            c.Staleness(2);
            chats.Add(c = Chat.Create("c2"));
            c.SetMeta("dev", "2");
            c.Staleness(3);
            chats.Add(c = Chat.Create("c3"));
            c.SetMeta("dev", "3");
            c.Staleness(4);
            var res = new ChatRuntime(chats).DoFindAll
                                            (null, new Constraint(Operator.LT, "staleness", "3"));
            Assert.That(res.Count, Is.EqualTo(1));
            Assert.That(res[0].text, Is.EqualTo("c1"));

            string[] lines = {
                "CHAT c0",
                "FIND {dev=1,day=fri,staleness<5}",
                "CHAT c1 {day=fri}",
                "CHAT c2 {dev=2,day=fri}",
                "CHAT c3 {}"
            };

            string contents = String.Join("\n", lines);

            chats = ChatParser.ParseText(contents, NO_VALIDATORS);

            Command finder = chats[0].commands[0]; finder.Resolve(null);
            Assert.That(finder.GetType(), Is.EqualTo(typeof(Find)));

            var crt = new ChatRuntime(chats);
            c = crt.FindChatByLabel("#c1");
            c.Staleness(6);
            Assert.That(c, Is.Not.Null);
            Assert.That(c.text, Is.EqualTo("c1"));

            chats = crt.DoFindAll((Find)finder, null);
            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0], Is.Not.Null);
            Assert.That(chats[0].text, Is.EqualTo("c3"));
        }

        [Test]
        public void StalenessRelaxation()
        {
            Chat c;
            List<Chat> chats = new List<Chat>();
            chats.Add(c = Chat.Create("c1"));
            c.SetMeta("dev", "1");
            c.Staleness(2);
            chats.Add(c = Chat.Create("c2"));
            c.SetMeta("dev", "2");
            c.Staleness(3);
            chats.Add(c = Chat.Create("c3"));
            c.SetMeta("dev", "3");
            c.Staleness(4);
            var cnt = new Constraint(Operator.LT, "staleness", "4");
            var cnt2 = new Constraint(Operator.GT, "dev", "2");
            var res = new ChatRuntime(chats).DoFind(null, cnt, cnt2);
            Assert.That(res, Is.Not.Null);
            Assert.That(res.text, Is.EqualTo("c3"));
        }

        [Test]
        public void DoubleRelaxation()
        {
            Chat c;
            List<Chat> chats = new List<Chat>();
            chats.Add(c = Chat.Create("c1"));
            c.SetMeta("dev", "1");
            c.Staleness(2);
            chats.Add(c = Chat.Create("c2"));
            c.SetMeta("dev", "2");
            c.Staleness(3);
            chats.Add(c = Chat.Create("c3"));
            c.SetMeta("other", "2");
            c.Staleness(4);
            var cnt = new Constraint(Operator.LT, "staleness", "4");
            var cnt2 = new Constraint(Operator.GT, "dev", "2", ConstraintType.Hard);
            var res = new ChatRuntime(chats).DoFind(null, cnt, cnt2);
            Assert.That(res, Is.Not.Null);
            Assert.That(res.text, Is.EqualTo("c3"));
        }


        [Test]
        public void FindWithRelaxation()
        {
            string[] lines = {
                "CHAT c0",
                "FIND {dev>1,!day=fri}",
                "CHAT c1 {dev=2,day=wed}",
                "CHAT c2 {dev=3,day=wed}",
                "CHAT c3 {}"
            };

            string contents = String.Join("\n", lines);

            List<Chat> chats = ChatParser.ParseText(contents, NO_VALIDATORS);
            //chats.ForEach((ch) => Console.WriteLine(ch.ToTree()));
            Command finder = chats[0].commands[0]; finder.Resolve(null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));

            ChatRuntime rt = new ChatRuntime(chats);
            var chat = rt.DoFind((Find)finder, null);
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.text, Is.EqualTo("c3"));

            lines = new[]{
                "CHAT c0 {dev=1}",
                "FIND {!dev>1,!day=fri}",
                "CHAT c1 {dev=0,day=wed}",
                "CHAT c2 {day=wed}",
                "CHAT c3 {}"
            };

            contents = String.Join("\n", lines);

            chats = ChatParser.ParseText(contents, NO_VALIDATORS);
            //chats.ForEach((ch) => Console.WriteLine(ch.ToTree()));
            finder = chats[0].commands[0]; finder.Resolve(null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));

            rt = new ChatRuntime(chats);
            chat = rt.DoFind((Find)finder, null);

            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.text, Is.EqualTo("c3"));

            lines = new[]{
                "CHAT c0 {dev=1}",
                "FIND {!dev>1,!day=fri}",
                "CHAT c1 {dev=0,day=wed}",
                "CHAT c2 {day=wed}",
            };

            chats = ChatParser.ParseText(String.Join("\n", lines), NO_VALIDATORS);
            finder = chats[0].commands[0]; finder.Resolve(null);

            rt = new ChatRuntime(chats);
            chat = rt.DoFind((Find)finder, null);
            Assert.That(chat, Is.Null);

            Chat c;
            chats = new List<Chat>();
            chats.Add(c = Chat.Create("c1"));
            c.SetMeta("dev", "hello");
            chats.Add(c = Chat.Create("c2"));
            c.SetMeta("dev", "2");
            chats.Add(c = Chat.Create("c3"));
            rt = new ChatRuntime(chats);
            chat = rt.DoFind(null, new Constraint("dev", "1", ConstraintType.Hard));
            Assert.That(chat.text, Is.EqualTo("c3")); // success
        }

        [Test]
        public void FindAllOpsHard()
        {
            string[] lines = {
                "CHAT c0",
                "FIND {!dev>1,day=fri}",
                "CHAT c1 {dev=2,day=fri}",
                "CHAT c2 {dev=1,day=fri}",
                "CHAT c3 {}"
            };

            string contents = String.Join("\n", lines);

            List<Chat> chats = ChatParser.ParseText(contents, NO_VALIDATORS);
            //chats.ForEach((ch) => Console.WriteLine(ch.ToTree()));
            Command finder = chats[0].commands[0]; finder.Resolve(null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));
            ChatRuntime cr = new ChatRuntime(chats);

            chats = cr.DoFindAll((Find)finder, null);
            //chats.ForEach((obj) => Console.WriteLine(obj.text));

            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(1));

            Assert.That(chats[0], Is.Not.Null);
            Assert.That(chats[0].text, Is.EqualTo("c1"));

            lines = new[]{
                "CHAT c0",
                "FIND {dev<2,!day=fri}",
                "CHAT c1 {dev=2,day=fri}",
                "CHAT c2 {dev=1,day=fri}",
                "CHAT c3 {}"
            };

            contents = String.Join("\n", lines);

            chats = ChatParser.ParseText(contents, NO_VALIDATORS);
            //chats.ForEach((ch) => Console.WriteLine(ch.ToTree()));
            finder = chats[0].commands[0]; finder.Resolve(null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));
            cr = new ChatRuntime(chats);

            chats = cr.DoFindAll((Find)finder, null);
            //chats.ForEach((obj) => Console.WriteLine(obj.text));

            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(1));

            Assert.That(chats[0], Is.Not.Null);
            Assert.That(chats[0].text, Is.EqualTo("c2"));
        }

        [Test]
        public void FindAllMulti()
        {
            string[] lines = {
                "CHAT c0 {day=sunday}",
                "FIND {emotion*=(hot|cool),day=fri}",
                "CHAT c1 {emotion=wet}",
                "CHAT c2 {emotion=hot,day=fri}",
                "CHAT c3 {emotion=dry}",
                "CHAT c4 {emotion=cool,day=fri}",
            };

            string contents = String.Join("\n", lines);

            List<Chat> chats = ChatParser.ParseText(contents, NO_VALIDATORS);
            //chats.ForEach((ch) => Console.WriteLine(ch.ToTree()));
            Command finder = chats[0].commands[0]; finder.Resolve(null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));

            chats = new ChatRuntime(chats).DoFindAll((Find)finder, null);
            //chats.ForEach((obj) => Console.WriteLine(obj.text));

            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(2));

            Assert.That(chats[0], Is.Not.Null);
            Assert.That(chats[0].text, Is.EqualTo("c2").Or.EqualTo("c4"));

            lines = new string[] {
                "CHAT c0 {day=sunday}",
                "FIND {emotion *= (hot|cool|blah), day*=(fri|wed)}",
                "CHAT c1 {emotion=wet}",
                "CHAT c2 {emotion=hot,day=fri}",
                "CHAT c3 {emotion=dry}",
                "CHAT c4 {emotion=cool,day=wed}",
            };

            contents = String.Join("\n", lines);

            chats = ChatParser.ParseText(contents, NO_VALIDATORS);
            //chats.ForEach((ch) => Console.WriteLine(ch.ToTree()));
            finder = chats[0].commands[0]; finder.Resolve(null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));

            chats = new ChatRuntime(chats).DoFindAll((Find)finder, null);
            //chats.ForEach((obj) => Console.WriteLine(obj.text));

            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(2));

            Assert.That(chats[0], Is.Not.Null);
            Assert.That(chats[0].text, Is.EqualTo("c2").Or.EqualTo("c4"));
        }


        [Test]
        public void FindAllMultiHard1()
        {
            string[] lines = {
                "CHAT c0",
                "FIND {!emotion*=(hot|cool),day=fri}",
                "CHAT c1 {emotion=wet}",
                "CHAT c2 {emotion=hot,day=fri}",
                "CHAT c3 {emotion=dry}",
                "CHAT c4 {emotion=cool,day=fri}",
            };

            string contents = String.Join("\n", lines);

            List<Chat> chats = ChatParser.ParseText(contents, NO_VALIDATORS);
            //chats.ForEach((ch) => Console.WriteLine(ch.ToTree()));
            Command finder = chats[0].commands[0]; finder.Resolve(null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));

            chats = new ChatRuntime(chats).DoFindAll((Find)finder, null);
            //chats.ForEach((obj) => Console.WriteLine(obj.text));

            Assert.That(chats.Count, Is.EqualTo(2));
            Assert.That(chats[0].text, Is.EqualTo("c2").Or.EqualTo("c4"));


            lines = new string[] {
                "CHAT c0",
                "FIND {!emotion *= (hot|cool|blah), day*=(fri|wed)}",
                "CHAT c1 {emotion=wet}",
                "CHAT c2 {emotion=hot,day=fri}",
                "CHAT c3 {emotion=dry}",
                "CHAT c4 {emotion=cool,day=wed}",
            };

            contents = String.Join("\n", lines);

            chats = ChatParser.ParseText(contents, NO_VALIDATORS);
            //chats.ForEach((ch) => Console.WriteLine(ch.ToTree()));
            finder = chats[0].commands[0]; finder.Resolve(null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));

            chats = new ChatRuntime(chats).DoFindAll((Find)finder, null);
            //chats.ForEach((obj) => Console.WriteLine(obj.text));

            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(2));

            Assert.That(chats[0], Is.Not.Null);
            Assert.That(chats[0].text, Is.EqualTo("c2").Or.EqualTo("c4"));
        }


        [Test]
        public void FindAllMultiHard2()
        {
            string[] lines = {
                "CHAT c0",
                "FIND {emotion*=(hot|cool), !day =fri}",
                "CHAT c1",
                "CHAT c2 {emotion=hot,day=fri}",
                "CHAT c3 {emotion=dry}",
                "CHAT c4 {emotion=dry,day=fri}",
            };

            string contents = String.Join("\n", lines);

            List<Chat> chats = ChatParser.ParseText(contents, NO_VALIDATORS);
            //chats.ForEach((ch) => Console.WriteLine(ch.ToTree()));
            Command finder = chats[0].commands[0]; finder.Resolve(null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));

            chats = new ChatRuntime(chats).DoFindAll((Find)finder, null);
            //chats.ForEach((obj) => Console.WriteLine(obj.text));

            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(1));

            Assert.That(chats[0], Is.Not.Null);
            Assert.That(chats[0].text, Is.EqualTo("c2"));

            lines = new string[] {
                "CHAT c0 {day=sunday}",
                "FIND {emotion *= (hot|cool|blah), !day*=(fri|wed)}",
                "CHAT c1 {emotion=wet, day = wed }",
                "CHAT c2 {emotion=hot,day=fri}",
                "CHAT c3 {emotion=dry}",
                "CHAT c4 {emotion=cool,day=tues}",
            };

            contents = String.Join("\n", lines);

            chats = ChatParser.ParseText(contents, NO_VALIDATORS);
            //chats.ForEach((ch) => Console.WriteLine(ch.ToTree()));
            finder = chats[0].commands[0]; finder.Resolve(null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));


            chats = new ChatRuntime(chats).DoFindAll((Find)finder, null);
            //chats.ForEach((obj) => Console.WriteLine(obj.text));

            Assert.That(chats, Is.Not.Null);
            Assert.That(chats.Count, Is.EqualTo(1));

            Assert.That(chats[0], Is.Not.Null);
            Assert.That(chats[0].text, Is.EqualTo("c2"));
        }

        [Test]
        public void DescendingLastRunAtSort()
        {
            var chatScores = new Dictionary<Chat, double>();

            chatScores.Add(Chat.Create("c1").LastRunAt(Util.EpochMs()), 1);
            chatScores.Add(Chat.Create("c2").LastRunAt(Util.EpochMs()), 2);
            chatScores.Add(Chat.Create("c3").LastRunAt(Util.EpochMs()), 3);
            chatScores.Add(Chat.Create("c4").LastRunAt(Util.EpochMs()), 4);
            chatScores.Add(Chat.Create("c5").LastRunAt(Util.EpochMs()), 5);

            var chats = FuzzySearch.DescendingScoreLastRunAtRandomizedSort(chatScores);
            //chats.ForEach((obj) => Console.WriteLine(obj.Key.text));

            for (int i = 0; i < 5; i++)
                Assert.That(chats[i].Key.text, Is.EqualTo("c" + (5 - i)));

            ////////////////////////////////////////////////////////////////////

            chatScores = new Dictionary<Chat, double>();
            chatScores.Add(Chat.Create("c1").LastRunAt(Util.EpochMs()), 1);
            chatScores.Add(Chat.Create("c2").LastRunAt(Util.EpochMs()), 2);
            chatScores.Add(Chat.Create("c3").LastRunAt(Util.EpochMs()), 3);
            chatScores.Add(Chat.Create("c4").LastRunAt(Util.EpochMs()), 5);
            chatScores.Add(Chat.Create("c5").LastRunAt(Util.EpochMs() - 1), 5);

            chats = FuzzySearch.DescendingScoreLastRunAtRandomizedSort(chatScores);
            //chats.ForEach((obj) => Console.WriteLine(obj.Key.text));

            for (int i = 0; i < 5; i++)
                Assert.That(chats[i].Key.text, Is.EqualTo("c" + (5 - i)));

            ////////////////////////////////////////////////////////////////////

            chatScores = new Dictionary<Chat, double>();
            chatScores.Add(Chat.Create("c1").LastRunAt(Util.EpochMs()), 1);
            chatScores.Add(Chat.Create("c2").LastRunAt(Util.EpochMs()), 2);
            chatScores.Add(Chat.Create("c3").LastRunAt(Util.EpochMs()), 3);
            chatScores.Add(Chat.Create("c4").LastRunAt(Util.EpochMs()), 5);
            chatScores.Add(Chat.Create("c5").LastRunAt(-1), 5);

            chats = FuzzySearch.DescendingScoreLastRunAtRandomizedSort(chatScores);
            //chats.ForEach((obj) => Console.WriteLine(obj.Key.text));

            for (int i = 0; i < 5; i++)
                Assert.That(chats[i].Key.text, Is.EqualTo("c" + (5 - i)));

            //////////////////////////////////////////////////////////////////////

            chatScores = new Dictionary<Chat, double>();
            chatScores.Add(Chat.Create("c1").LastRunAt(Util.EpochMs()), 1);
            chatScores.Add(Chat.Create("c2").LastRunAt(Util.EpochMs()), 2);
            chatScores.Add(Chat.Create("c3").LastRunAt(Util.EpochMs()), 3);
            chatScores.Add(Chat.Create("c4").LastRunAt(-1), 5);
            chatScores.Add(Chat.Create("c5").LastRunAt(-1), 5);

            for (int j = 0; j < 10; j++) // repeat a few times
            {
                chats = FuzzySearch.DescendingScoreLastRunAtRandomizedSort(chatScores);
                //chats.ForEach(obj => Console.Write(obj.Key.text+ ", "));Console.WriteLine();
                Assert.That(chats[0].Key.text, Is.EqualTo("c5").Or.EqualTo("c4"));
                Assert.That(chats[0].Key.text, Is.EqualTo("c4").Or.EqualTo("c5"));
                Assert.That(chats[2].Key.text, Is.EqualTo("c3"));
                Assert.That(chats[3].Key.text, Is.EqualTo("c2"));
                Assert.That(chats[4].Key.text, Is.EqualTo("c1"));
            }

            //////////////////////////////////////////////////////////////////////

            chatScores = new Dictionary<Chat, double>();

            chatScores.Add(Chat.Create("c1").LastRunAt(Util.EpochMs()), 1);
            chatScores.Add(Chat.Create("c2").LastRunAt(Util.EpochMs() - 1000), 1);
            chatScores.Add(Chat.Create("c3").LastRunAt(Util.EpochMs() - 2000), 1);
            chatScores.Add(Chat.Create("c4"), 0);
            chatScores.Add(Chat.Create("c5"), 10);

            chats = FuzzySearch.DescendingScoreLastRunAtRandomizedSort(chatScores);
            for (int i = 0; i < chats.Count; i++)
            {
                //Console.WriteLine(chats[i].Key + "(" + chats[i].Key.lastRunAt + ") -> " + chats[i].Value);
                if (i + 1 < chats.Count) Assert.That(chats[i + 1].Value <= chats[i].Value, Is.True);
            }

            Assert.That(chats[0].Key.text, Is.EqualTo("c5"));
            Assert.That(chats[1].Key.text, Is.EqualTo("c3"));
            Assert.That(chats[2].Key.text, Is.EqualTo("c2"));
            Assert.That(chats[3].Key.text, Is.EqualTo("c1"));
            Assert.That(chats[4].Key.text, Is.EqualTo("c4"));
        }

        [Test]
        public void DescendingStalenessSort()
        {
            var chatScores = new Dictionary<Chat, double>();

            chatScores.Add(Chat.Create("c1").Staleness(3.1), 1);
            chatScores.Add(Chat.Create("c2").Staleness(3), 1);
            chatScores.Add(Chat.Create("c3").Staleness(1.1), 1);
            chatScores.Add(Chat.Create("c4"), 0);
            chatScores.Add(Chat.Create("c5"), 10);

            var list = FuzzySearch.DescendingStalenessRandomizedSort(chatScores);
            for (int i = 1; i < list.Count; i++)
            {
                Assert.That(list[i].Value <= list[i - 1].Value, Is.True);
            }

            //list.ForEach((kvp) => Console.WriteLine(kvp.Key + " -> " + kvp.Value));

            var chats = (from kvp in list select kvp.Key).ToList();
            Assert.That(chats[0].text, Is.EqualTo("c5"));
            Assert.That(chats[1].text, Is.EqualTo("c3"));
            Assert.That(chats[2].text, Is.EqualTo("c2"));
            Assert.That(chats[3].text, Is.EqualTo("c1"));
            Assert.That(chats[4].text, Is.EqualTo("c4"));
        }
    }
}
