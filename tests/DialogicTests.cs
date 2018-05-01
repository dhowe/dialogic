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
        public void ImmediateWithDynamicGo()
        {
            // TODO: WORKING HERE

            string[] lines = {
                "CHAT c1",
                "GO #(line1|line2)",
                "CHAT line1",
                "Done",
                "CHAT line2",
                "Done"
            };

            ChatRuntime rt = new ChatRuntime();
            rt.ParseText(String.Join("\n", lines));

            var s = rt.InvokeImmediate(globals);
            //Console.WriteLine(s);
            Assert.That(s, Is.EqualTo("Done"));
        }

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
        public void ImmediateTransformFails()
        {
            string[] lines = {
                "CHAT c",
                "SAY hello (a | a).noFun(),",
                "SAY ok.",
            };

            ChatRuntime rt = new ChatRuntime();
            rt.ParseText(String.Join("\n", lines));

            Assert.Throws<UnboundFunction>(() => rt.InvokeImmediate(null));

            ChatRuntime.SILENT = true;
            rt.strictMode = false;
            var s = rt.InvokeImmediate(globals);
            ChatRuntime.SILENT = false;
            Assert.That(s, Is.EqualTo("hello a.noFun(),\nok."));
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
        public void ImmediateDynamicAsk()
        {
            string[] lines = {
                "CHAT c",
                "SAY hello $animal.",
                "ASK are you a $animal?",
                "OPT Yes #(c2|c2)",
                "OPT No #(c2|c2)",
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
            Assert.That(Transforms.pluralize(""), Is.EqualTo(""));
            Assert.That(Transforms.pluralize("dog"), Is.EqualTo("dogs"));
            Assert.That(Transforms.pluralize("eye"), Is.EqualTo("eyes"));

            Assert.That(Transforms.pluralize("blonde"), Is.EqualTo("blondes"));
            Assert.That(Transforms.pluralize("blond"), Is.EqualTo("blondes"));
            Assert.That(Transforms.pluralize("foot"), Is.EqualTo("feet"));
            Assert.That(Transforms.pluralize("man"), Is.EqualTo("men"));
            Assert.That(Transforms.pluralize("tooth"), Is.EqualTo("teeth"));
            Assert.That(Transforms.pluralize("cake"), Is.EqualTo("cakes"));
            Assert.That(Transforms.pluralize("kiss"), Is.EqualTo("kisses"));
            Assert.That(Transforms.pluralize("child"), Is.EqualTo("children"));

            Assert.That(Transforms.pluralize("louse"), Is.EqualTo("lice"));

            Assert.That(Transforms.pluralize("sheep"), Is.EqualTo("sheep"));
            Assert.That(Transforms.pluralize("shrimp"), Is.EqualTo("shrimps"));
            Assert.That(Transforms.pluralize("series"), Is.EqualTo("series"));
            Assert.That(Transforms.pluralize("mouse"), Is.EqualTo("mice"));

            Assert.That(Transforms.pluralize("beautiful"), Is.EqualTo("beautifuls"));

            Assert.That(Transforms.pluralize("crisis"), Is.EqualTo("crises"));
            Assert.That(Transforms.pluralize("thesis"), Is.EqualTo("theses"));
            Assert.That(Transforms.pluralize("apothesis"), Is.EqualTo("apotheses"));
            Assert.That(Transforms.pluralize("stimulus"), Is.EqualTo("stimuli"));
            Assert.That(Transforms.pluralize("alumnus"), Is.EqualTo("alumni"));
            Assert.That(Transforms.pluralize("corpus"), Is.EqualTo("corpora"));

            Assert.That(Transforms.pluralize("woman"), Is.EqualTo("women"));
            Assert.That(Transforms.pluralize("man"), Is.EqualTo("men"));
            Assert.That(Transforms.pluralize("congressman"), Is.EqualTo("congressmen"));
            Assert.That(Transforms.pluralize("alderman"), Is.EqualTo("aldermen"));
            Assert.That(Transforms.pluralize("freshman"), Is.EqualTo("freshmen"));

            Assert.That(Transforms.pluralize("bikini"), Is.EqualTo("bikinis"));
            Assert.That(Transforms.pluralize("martini"), Is.EqualTo("martinis"));
            Assert.That(Transforms.pluralize("menu"), Is.EqualTo("menus"));
            Assert.That(Transforms.pluralize("guru"), Is.EqualTo("gurus"));

            Assert.That(Transforms.pluralize("medium"), Is.EqualTo("media"));
            Assert.That(Transforms.pluralize("concerto"), Is.EqualTo("concerti"));
            Assert.That(Transforms.pluralize("terminus"), Is.EqualTo("termini"));

            Assert.That(Transforms.pluralize("aquatics"), Is.EqualTo("aquatics"));
            Assert.That(Transforms.pluralize("mechanics"), Is.EqualTo("mechanics"));

            Assert.That(Transforms.pluralize("tomato"), Is.EqualTo("tomatoes"));
            Assert.That(Transforms.pluralize("toe"), Is.EqualTo("toes"));

            Assert.That(Transforms.pluralize("deer"), Is.EqualTo("deer"));
            Assert.That(Transforms.pluralize("moose"), Is.EqualTo("moose"));
            Assert.That(Transforms.pluralize("ox"), Is.EqualTo("oxen"));

            Assert.That(Transforms.pluralize("tobacco"), Is.EqualTo("tobacco"));
            Assert.That(Transforms.pluralize("cargo"), Is.EqualTo("cargo"));
            Assert.That(Transforms.pluralize("golf"), Is.EqualTo("golf"));
            Assert.That(Transforms.pluralize("grief"), Is.EqualTo("grief"));
            Assert.That(Transforms.pluralize("wildlife"), Is.EqualTo("wildlife"));
            Assert.That(Transforms.pluralize("taxi"), Is.EqualTo("taxis"));
            Assert.That(Transforms.pluralize("Chinese"), Is.EqualTo("Chinese"));
            Assert.That(Transforms.pluralize("bonsai"), Is.EqualTo("bonsai"));

            Assert.That(Transforms.pluralize("gas"), Is.EqualTo("gases"));
            Assert.That(Transforms.pluralize("bus"), Is.EqualTo("buses"));
        }

        [Test]
        public void TestGetExtMethods()
        {
            var s = Methods.InvokeTransform("animal", "articlize");
            Assert.That(s, Is.EqualTo("an animal"));

            s = Methods.InvokeTransform("cat", "articlize");
            Assert.That(s, Is.EqualTo("a cat"));

            Assert.That(Methods.InvokeTransform("tobacco","pluralize"), Is.EqualTo("tobacco"));
            Assert.That(Methods.InvokeTransform("cargo", "pluralize"), Is.EqualTo("cargo"));
            Assert.That(Methods.InvokeTransform("golf", "pluralize"), Is.EqualTo("golf"));
            Assert.That(Methods.InvokeTransform("grief", "pluralize"), Is.EqualTo("grief"));
            Assert.That(Methods.InvokeTransform("wildlife", "pluralize"), Is.EqualTo("wildlife"));
            Assert.That(Methods.InvokeTransform("taxi", "pluralize"), Is.EqualTo("taxis"));
            Assert.That(Methods.InvokeTransform("Chinese", "pluralize"), Is.EqualTo("Chinese"));
            Assert.That(Methods.InvokeTransform("bonsai", "pluralize"), Is.EqualTo("bonsai"));

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
            var globs = new Dictionary<string, object>() {
                { "animal", "dog" },
                { "temp", "23.3" },
                { "isok", "true" },
                { "count", "4" }
             };
            UpdateEvent ue = new UpdateEvent(globs);
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
