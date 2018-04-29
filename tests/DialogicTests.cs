using System.Collections.Generic;
using NUnit.Framework;
using System;

namespace Dialogic
{
    [TestFixture]
    public class DialogicTests
    {
        const bool NO_VALIDATORS = true;

        static IDictionary<string, object> globals
            = new Dictionary<string, object>
        {
            { "animal", "dog" },
            { "group", "(a|b)" },
            { "count", 4 },
        };

        [Test]
        public void ImmediateModeTest()
        {
            string[] lines = {
                "CHAT c",
                "SAY hello $animal.",
                "SAY ok.",
                "CHAT c1",
                "SAY goodbye."
            };
            ChatRuntime rt = new ChatRuntime();
            rt.ParseText(String.Join("\n", lines));
               
            var s = rt.InvokeImmediate(globals);
            Assert.That(s, Is.EqualTo("hello dog.\nok."));

            s = rt.InvokeImmediate(globals, "c");
            Assert.That(s, Is.EqualTo("hello dog.\nok."));

            s = rt.InvokeImmediate(globals, "c1");
            Assert.That(s, Is.EqualTo("goodbye."));

            Assert.Throws<UnboundSymbol>(() => rt.InvokeImmediate(null));

            ChatRuntime.SILENT = true;
            rt.strictMode = false;
            s = rt.InvokeImmediate(null);
            ChatRuntime.SILENT = false;
            Assert.That(s, Is.EqualTo("hello $animal.\nok."));
        }

        [Test]
        public void ImmediateGoTest()
        {
            string[] lines = {
                "CHAT c",
                "SAY hello $animal.",
                "SAY ok.",
                "GO c1",
                "CHAT c1",
                "SAY goodbye."
            };
            ChatRuntime rt = new ChatRuntime();
            rt.ParseText(String.Join("\n", lines));

            var s = rt.InvokeImmediate(globals);
            Assert.That(s, Is.EqualTo("hello dog.\nok.\ngoodbye."));

            s = rt.InvokeImmediate(globals, "c");
            Assert.That(s, Is.EqualTo("hello dog.\nok.\ngoodbye."));

            s = rt.InvokeImmediate(globals, "c1");
            Assert.That(s, Is.EqualTo("goodbye."));

            Assert.Throws<UnboundSymbol>(() => rt.InvokeImmediate(null));


            ChatRuntime.SILENT = true;
            rt.strictMode = false;
            s = rt.InvokeImmediate(null);
            ChatRuntime.SILENT = false;
            Assert.That(s, Is.EqualTo("hello $animal.\nok.\ngoodbye."));
        }

        [Test]
        public void ImmediateGoFails()
        {
            string[] lines = {
                "CHAT c",
                "SAY hello $animal.",
                "SAY ok.",
                "GO c2",
                "CHAT c1",
                "SAY goodbye."
            };

            ChatRuntime rt = new ChatRuntime();
            rt.ParseText(String.Join("\n", lines));

            ChatRuntime.SILENT = true;
            rt.strictMode = false;
            var s = rt.InvokeImmediate(globals);
            ChatRuntime.SILENT = false;
            Assert.That(s, Is.EqualTo("hello dog.\nok.\n\nGO #c2 failed"));
        }

        [Test]
        public void ImmediateAskTest()
        {
            string[] lines = {
                "CHAT c",
                "SAY hello $animal.",
                "ASK are you a $animal?",
                "OPT Yes #c2",
                "OPT No #c2",
                "CHAT c1 {a=b}",
                "SAY nope.",
                "CHAT c2 {a=b}",
                "SAY goodbye."
            };
            ChatRuntime rt = new ChatRuntime();
            rt.ParseText(String.Join("\n", lines));

            var s = rt.InvokeImmediate(globals);
            Assert.That(s, Is.EqualTo("hello dog.\nare you a dog?\ngoodbye."));

            s = rt.InvokeImmediate(globals, "c");
            Assert.That(s, Is.EqualTo("hello dog.\nare you a dog?\ngoodbye."));

            s = rt.InvokeImmediate(globals, "c2");
            Assert.That(s, Is.EqualTo("goodbye."));

            Assert.Throws<UnboundSymbol>(() => rt.InvokeImmediate(null));


            ChatRuntime.SILENT = true;
            rt.strictMode = false;
            s = rt.InvokeImmediate(null);
            ChatRuntime.SILENT = false;
            Assert.That(s, Is.EqualTo("hello $animal.\nare you a $animal?\ngoodbye."));
        }

        [Test]
        public void ImmediateFindTest()
        {
            string[] lines = {
                "CHAT c",
                "SAY hello $animal.",
                "ASK are you a $animal?",
                "FIND {a=b}",
                "CHAT c1 {a=b}",
                "SAY goodbye."
            };
            ChatRuntime rt = new ChatRuntime();
            rt.ParseText(String.Join("\n", lines));

            var s = rt.InvokeImmediate(globals);
            Assert.That(s, Is.EqualTo("hello dog.\nare you a dog?\ngoodbye."));

            s = rt.InvokeImmediate(globals, "c");
            Assert.That(s, Is.EqualTo("hello dog.\nare you a dog?\ngoodbye."));

            s = rt.InvokeImmediate(globals, "c1");
            Assert.That(s, Is.EqualTo("goodbye."));

            Assert.Throws<UnboundSymbol>(() => rt.InvokeImmediate(null));

            ChatRuntime.SILENT = true;
            rt.strictMode = false;

            s = rt.InvokeImmediate(null);
            ChatRuntime.SILENT = false;
            Assert.That(s, Is.EqualTo("hello $animal.\nare you a $animal?\ngoodbye."));
        }

        [Test]
        public void TestPluralize()
        {
            Assert.That(Modifiers.pluralize(""), Is.EqualTo(""));
            Assert.That(Modifiers.pluralize("dog"), Is.EqualTo("dogs"));
            Assert.That(Modifiers.pluralize("eye"), Is.EqualTo("eyes"));

            Assert.That(Modifiers.pluralize("blonde"), Is.EqualTo("blondes"));
            Assert.That(Modifiers.pluralize("blond"), Is.EqualTo("blondes"));
            Assert.That(Modifiers.pluralize("foot"), Is.EqualTo("feet"));
            Assert.That(Modifiers.pluralize("man"), Is.EqualTo("men"));
            Assert.That(Modifiers.pluralize("tooth"), Is.EqualTo("teeth"));
            Assert.That(Modifiers.pluralize("cake"), Is.EqualTo("cakes"));
            Assert.That(Modifiers.pluralize("kiss"), Is.EqualTo("kisses"));
            Assert.That(Modifiers.pluralize("child"), Is.EqualTo("children"));

            Assert.That(Modifiers.pluralize("louse"), Is.EqualTo("lice"));

            Assert.That(Modifiers.pluralize("sheep"), Is.EqualTo("sheep"));
            Assert.That(Modifiers.pluralize("shrimp"), Is.EqualTo("shrimps"));
            Assert.That(Modifiers.pluralize("series"), Is.EqualTo("series"));
            Assert.That(Modifiers.pluralize("mouse"), Is.EqualTo("mice"));

            Assert.That(Modifiers.pluralize("beautiful"), Is.EqualTo("beautifuls"));

            Assert.That(Modifiers.pluralize("crisis"), Is.EqualTo("crises"));
            Assert.That(Modifiers.pluralize("thesis"), Is.EqualTo("theses"));
            Assert.That(Modifiers.pluralize("apothesis"), Is.EqualTo("apotheses"));
            Assert.That(Modifiers.pluralize("stimulus"), Is.EqualTo("stimuli"));
            Assert.That(Modifiers.pluralize("alumnus"), Is.EqualTo("alumni"));
            Assert.That(Modifiers.pluralize("corpus"), Is.EqualTo("corpora"));

            Assert.That(Modifiers.pluralize("woman"), Is.EqualTo("women"));
            Assert.That(Modifiers.pluralize("man"), Is.EqualTo("men"));
            Assert.That(Modifiers.pluralize("congressman"), Is.EqualTo("congressmen"));
            Assert.That(Modifiers.pluralize("alderman"), Is.EqualTo("aldermen"));
            Assert.That(Modifiers.pluralize("freshman"), Is.EqualTo("freshmen"));

            Assert.That(Modifiers.pluralize("bikini"), Is.EqualTo("bikinis"));
            Assert.That(Modifiers.pluralize("martini"), Is.EqualTo("martinis"));
            Assert.That(Modifiers.pluralize("menu"), Is.EqualTo("menus"));
            Assert.That(Modifiers.pluralize("guru"), Is.EqualTo("gurus"));

            Assert.That(Modifiers.pluralize("medium"), Is.EqualTo("media"));
            Assert.That(Modifiers.pluralize("concerto"), Is.EqualTo("concerti"));
            Assert.That(Modifiers.pluralize("terminus"), Is.EqualTo("termini"));

            Assert.That(Modifiers.pluralize("aquatics"), Is.EqualTo("aquatics"));
            Assert.That(Modifiers.pluralize("mechanics"), Is.EqualTo("mechanics"));

            Assert.That(Modifiers.pluralize("tomato"), Is.EqualTo("tomatoes"));
            Assert.That(Modifiers.pluralize("toe"), Is.EqualTo("toes"));

            Assert.That(Modifiers.pluralize("deer"), Is.EqualTo("deer"));
            Assert.That(Modifiers.pluralize("moose"), Is.EqualTo("moose"));
            Assert.That(Modifiers.pluralize("ox"), Is.EqualTo("oxen"));

            Assert.That(Modifiers.pluralize("tobacco"), Is.EqualTo("tobacco"));
            Assert.That(Modifiers.pluralize("cargo"), Is.EqualTo("cargo"));
            Assert.That(Modifiers.pluralize("golf"), Is.EqualTo("golf"));
            Assert.That(Modifiers.pluralize("grief"), Is.EqualTo("grief"));
            Assert.That(Modifiers.pluralize("wildlife"), Is.EqualTo("wildlife"));
            Assert.That(Modifiers.pluralize("taxi"), Is.EqualTo("taxis"));
            Assert.That(Modifiers.pluralize("Chinese"), Is.EqualTo("Chinese"));
            Assert.That(Modifiers.pluralize("bonsai"), Is.EqualTo("bonsai"));

            Assert.That(Modifiers.pluralize("gas"), Is.EqualTo("gases"));
            Assert.That(Modifiers.pluralize("bus"), Is.EqualTo("buses"));
        }

        [Test]
        public void TestGetExtMethods()
        {
            var s = Methods.InvokeExt("animal", "articlize");
            Assert.That(s, Is.EqualTo("an animal"));

            s = Methods.InvokeExt("cat", "articlize");
            Assert.That(s, Is.EqualTo("a cat"));
        }

        [Test]
        public void RuntimeIndexTest()
        {
            var rt = new ChatRuntime();
            rt.ParseText("CHAT c1\nSAY OK");
            Assert.That(rt.Chats()[0], Is.Not.Null);
            Assert.That(rt["c1"], Is.Not.Null); // ???
        }

        [Test]
        public void EntityDecodeTest()
        {
            Assert.That(Entities.Decode(""), Is.EqualTo(""));
            Assert.That(Entities.Decode(null), Is.EqualTo(null));

            Assert.That(Entities.Decode("&amp;"), Is.EqualTo("&"));
            Assert.That(Entities.Decode("&nbsp;"), Is.EqualTo(" "));
            Assert.That(Entities.Decode("&quot;"), Is.EqualTo("\""));

            Assert.That(Entities.Decode("&invalid;"), Is.EqualTo("&invalid;"));
        }

        [Test]
        public void ListPopExt()
        {
            var ints = new List<int>() {4,7,2,3};
            Assert.That(ints.Pop(), Is.EqualTo(3));
            Assert.That(ints.Count, Is.EqualTo(3));
        }

        [Test]
        public void SecStrToMsTest()
        {
            Assert.That(Util.SecStrToMs("1"), Is.EqualTo(1000));
            Assert.That(Util.SecStrToMs("1.5"), Is.EqualTo(1500));
            Assert.That(Util.SecStrToMs(".5"), Is.EqualTo(500));
            Assert.That(Util.SecStrToMs("0.5"), Is.EqualTo(500));
            Assert.That(Util.SecStrToMs("1x"), Is.EqualTo(-1));
            Assert.That(Util.SecStrToMs("1x", 1000), Is.EqualTo(1000));
        }

        [Test]
        public void UpdateEventTest()
        {
            var globals = new Dictionary<string, object>() {
                { "animal", "dog" },
                { "temp", "23.3" },
                { "isok", "true" },
                { "count", "4" }
             };
            UpdateEvent ue = new UpdateEvent(globals);
            Assert.That(ue.Data().Count, Is.EqualTo(4));

            Assert.That(ue.Get("animal"), Is.EqualTo("dog"));
            Assert.That(ue.Get("MISS1"), Is.Null);
            Assert.That(ue.Get("MISS2", "cat"), Is.EqualTo("cat"));

            Assert.That(ue.GetInt("count"), Is.EqualTo(4));
            Assert.That(ue.GetInt("MISS1"), Is.EqualTo(-1));
            Assert.That(ue.GetInt("MISS2", 0), Is.EqualTo(0));

            Assert.That(ue.GetDouble("temp"), Is.EqualTo(23.3d));
            Assert.That(ue.GetDouble("MISS1"), Is.EqualTo(-1d));
            Assert.That(ue.GetDouble("MISS2", 0), Is.EqualTo(0d));

            Assert.That(ue.GetFloat("temp"), Is.EqualTo(23.3f));
            Assert.That(ue.GetFloat("MISS1"), Is.EqualTo(-1f));
            Assert.That(ue.GetFloat("MISS2", 0), Is.EqualTo(0f));

            Assert.That(ue.GetBool("isok"), Is.EqualTo(true));
            Assert.That(ue.GetBool("MISS1"), Is.EqualTo(false));
            Assert.That(ue.GetBool("MISS2", true), Is.EqualTo(true));
        }

        [Test]
        public void TrimFirstLast()
        {
            string a;

            a = "a=";
            Util.TrimFirst(ref a, '=');
            Assert.That(a, Is.EqualTo("a="));

            Util.TrimLast(ref a, '=');
            Assert.That(a, Is.EqualTo("a"));

            a = "=a";
            Util.TrimLast(ref a, '=');
            Assert.That(a, Is.EqualTo("=a"));

            Util.TrimFirst(ref a, '=');
            Assert.That(a, Is.EqualTo("a"));

            a = "=a";
            Util.TrimLast(ref a, '=', ':');
            Assert.That(a, Is.EqualTo("=a"));

            a = "a=:";
            Util.TrimLast(ref a, '=', ':');
            Assert.That(a, Is.EqualTo("a="));

            a = "a:=";
            Util.TrimLast(ref a, '=', ':');
            Assert.That(a, Is.EqualTo("a:"));

            a = "a:=";
            Util.TrimLast(ref a, '=', ':');
            Util.TrimLast(ref a, '=', ':');
            Assert.That(a, Is.EqualTo("a"));

            a = "a:==:====";
            do { } while (Util.TrimLast(ref a, '=', ':'));
            Assert.That(a, Is.EqualTo("a"));
        }


        [Test]
        public void RandItemTest()
        {
            string[] arr = { "0", "1", "2" };
            Dictionary<string, int> hits;
            hits = new Dictionary<string, int>();
            foreach (var s in arr) hits.Add(s, 0);
            for (int i = 0; i < 100; i++)
            {
                var s = Util.RandItem(arr);
                hits[((string)s)]++;
                CollectionAssert.Contains(arr, s);
            }
            foreach (var kv in hits)
            {
                Assert.That(kv.Value, Is.GreaterThan(0));
            }

            List<string> l = new List<string>() { "0", "1", "2" };
            hits = new Dictionary<string, int>();
            foreach (var s in l) hits.Add(s, 0);
            for (int i = 0; i < 100; i++)
            {
                string s = Util.RandItem(l);
                hits[s]++;
                CollectionAssert.Contains(arr, s);
            }
            foreach (var kv in hits)
            {
                Assert.That(kv.Value, Is.GreaterThan(0));
            }
        }

        [Test]
        public void TimingTest()
        {
            Say fast = (Say)ChatParser.ParseText("SAY Thank you { speed=fast}")[0].commands[0];
            Say defa = (Say)ChatParser.ParseText("SAY Thank you")[0].commands[0];
            Say slow = (Say)ChatParser.ParseText("SAY Thank you{speed=slow }")[0].commands[0];
            Assert.That(defa.ComputeDuration(), Is.EqualTo(fast.ComputeDuration() * 2).Within(1));
            Assert.That(slow.ComputeDuration(), Is.EqualTo(defa.ComputeDuration() * 2).Within(1));
            Say longer = (Say)ChatParser.ParseText("SAY Thank you very much")[0].commands[0];
            Assert.That(longer.ComputeDuration(), Is.GreaterThan(defa.ComputeDuration()));
            Assert.That(fast.text, Is.EqualTo(defa.text));
            Assert.That(slow.text, Is.EqualTo(defa.text));
        }
    }
}
