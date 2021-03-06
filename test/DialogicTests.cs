﻿using System.Collections.Generic;
using NUnit.Framework;
using System;
using System.IO;

namespace Dialogic.Test
{
    [TestFixture]
    public class DialogicTests : GenericTests
    {
        [Test]
        public void MinEditDistanceStringTest()
        {
            string str1 = "The dog", str2 = "The cat";

            Assert.That(Util.MinEditDist(str1, str2).Equals(3));
            //Console.WriteLine(Util.MinEditDist(str1, str2, true));
            Assert.That(Util.MinEditDistAdj(str1, str2).Equals(3 / 7.0));

            str1 = "fefnction"; str2 = "faunctional";
            Assert.That(Util.MinEditDist(str1, str2).Equals(4));
            Assert.That(Util.MinEditDistAdj(str1, str2).Equals(4 / 11.0));

            str1 = "intention"; str2 = "execution";
            Assert.That(Util.MinEditDist(str1, str2).Equals(5));
            Assert.That(Util.MinEditDistAdj(str1, str2).Equals(5 / 9.0));

            str1 = "The dog"; str2 = "";
            Assert.That(Util.MinEditDist(str1, str2).Equals(7));
            Assert.That(Util.MinEditDistAdj(str1, str2).Equals(1));
        }

        [Test]
        public void MinEditDistanceArrayTest()
        {
            string[] arr1 = { "The", "dog", "ate" },
              arr2 = { "The", "cat", "ate" };

            Assert.That(Util.MinEditDist(arr1, arr2).Equals(1));
            Assert.That(Util.MinEditDistAdj(arr1, arr2).Equals(1 / 3.0));

            arr1 = new String[] { "The", "dog", "ate" };
            arr2 = new String[0];
            Assert.That(Util.MinEditDist(arr1, arr2).Equals(3));
            Assert.That(Util.MinEditDistAdj(arr1, arr2).Equals(3 / 3.0));

            arr1 = new String[] { "fefnction", "intention", "ate" };
            arr2 = new String[] { "faunctional", "execution", "ate" };
            Assert.That(Util.MinEditDist(arr1, arr2).Equals(2));
            Assert.That(Util.MinEditDistAdj(arr1, arr2).Equals(2 / 3.0));
        }

        [Test]
        public void EnclosedByStrictTest()
        {
            Assert.That("(a() b())".EnclosedBy(Ch.OGROUP, Ch.CGROUP, true), Is.True);
            Assert.That("(a() b)".EnclosedBy(Ch.OGROUP, Ch.CGROUP, true), Is.True);
            Assert.That("(a b())".EnclosedBy(Ch.OGROUP, Ch.CGROUP, true), Is.True);
            Assert.That("(hello)".EnclosedBy(Ch.OGROUP, Ch.CGROUP, true), Is.True);
            Assert.That("(he()llo)".EnclosedBy(Ch.OGROUP, Ch.CGROUP, true), Is.True);
            Assert.That("(he)llo)".EnclosedBy(Ch.OGROUP, Ch.CGROUP, true), Is.False);
            Assert.That("((a) (b))".EnclosedBy(Ch.OGROUP, Ch.CGROUP, true), Is.False);
            Assert.That("((a b))".EnclosedBy(Ch.OGROUP, Ch.CGROUP, true), Is.False);
            Assert.That("(a b)".EnclosedBy(Ch.OGROUP, Ch.CGROUP, true), Is.True);
        }

        [Test]
        public void SetPathValueTest()
        {
            object result;
            Chat c1 = null;

            var symbol = Symbol.Parse("$fish.name = Mary", c1)[0];

            Set.SetPathValue(globals["fish"], new[] { "fish", "name" }, "Mary");

            result = Symbol.Parse("you $fish.name?", c1)[0].Resolve(globals);
            Assert.That(result.ToString(), Is.EqualTo("Mary"));
        }

        [Test]
        public void SetGlobalsOnPath()
        {
            var code = "CHAT c1\nSET $fish.name=Mary\nSAY Hi $fish.name";
            Chat chat = ChatParser.ParseText(code, null)[0];

            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            Assert.That(chat.commands[1].GetType(), Is.EqualTo(typeof(Say)));

            Set set = (Dialogic.Set)chat.commands[0];
            Assert.That(set.text, Is.EqualTo("fish.name"));
            Assert.That(set.value, Is.EqualTo("Mary"));

            Say say = (Dialogic.Say)chat.commands[1];
            Assert.That(say.text, Is.EqualTo("Hi $fish.name"));

            chat.Resolve(globals);
            Assert.That(say.Text(), Is.EqualTo("Hi Mary"));
        }

        [Test]
        public void SetRemoteChatProperty()
        {
            var code = "CHAT c1\nSET $fish.name=Mary\nSAY Hi $fish.name";
            code += "\nCHAT c2\nSET $c1.staleness=2";
            var rt = new ChatRuntime(null);
            rt.ParseText(code, true);

            var chat = rt["c1"];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            Assert.That(chat.commands[1].GetType(), Is.EqualTo(typeof(Say)));

            chat.Resolve(globals);

            var chat2 = rt["c2"];
            Assert.That(chat2, Is.Not.Null);
            Assert.That(chat2.commands[0].GetType(), Is.EqualTo(typeof(Set)));

            Assert.That(chat.Staleness(), Is.EqualTo(0));

            chat2.Resolve(globals);

            Assert.That(chat.Staleness(), Is.EqualTo(2));
        }

        [Test]
        public void SetRemoteChatNonProperty()
        {
            var code = "CHAT c1\nSET $fish.name=Mary\nSAY Hi $fish.name";
            code += "\nCHAT c2\nSET $c1.happiness=2";
            var rt = new ChatRuntime(null);
            rt.ParseText(code, true);

            var chat = rt["c1"];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            Assert.That(chat.commands[1].GetType(), Is.EqualTo(typeof(Say)));

            chat.Resolve(globals);

            var chat2 = rt["c2"];
            Assert.That(chat2, Is.Not.Null);
            Assert.That(chat2.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            Assert.That(chat.Staleness(), Is.EqualTo(0));

            // throw b/c we only allow setting of persistent properties 
            // (staleness, etc) on remote chats
            Assert.Throws<BindException>(() => chat2.Resolve(globals));
        }

        [Test]
        public void SetBadRemoteChatProperty()
        {
            var code = "CHAT c1\nSET $fish.name=Mary\nSAY Hi $fish.name";
            code += "\nCHAT c2\nSET $WRONG.staleness=2";
            var rt = new ChatRuntime(null);
            rt.ParseText(code, true);

            var chat = rt["c1"];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            Assert.That(chat.commands[1].GetType(), Is.EqualTo(typeof(Say)));

            chat.Resolve(globals);

            var chat2 = rt["c2"];
            Assert.That(chat2, Is.Not.Null);
            Assert.That(chat2.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            Assert.That(chat.Staleness(), Is.EqualTo(0));

            // throw b/c $WRONG.staleness doesn't exist in any scope
            Assert.Throws<BindException>(() => chat2.Resolve(globals));
        }

        [Test]
        public void SetChatLocalPath()
        {
            var code = "CHAT c1\nSET $fish.name=Mary\nSAY Hi $fish.name";
            code += "\nCHAT c2\nSET $c1.staleness=2";

            var rt = new ChatRuntime(null);
            rt.ParseText(code, true);

            var chat = rt["c1"];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            Assert.That(chat.commands[1].GetType(), Is.EqualTo(typeof(Say)));

            chat.Resolve(globals);

            var chat2 = rt["c2"];
            Assert.That(chat2, Is.Not.Null);
            Assert.That(chat2.commands[0].GetType(), Is.EqualTo(typeof(Set)));

            Assert.That(chat.Staleness(), Is.EqualTo(Defaults.CHAT_STALENESS));
            Assert.That(Convert.ToDouble(chat.GetMeta(Meta.STALENESS)), Is.EqualTo(Defaults.CHAT_STALENESS));

            chat2.Resolve(globals);
            Assert.That(chat.Staleness(), Is.EqualTo(2));
            Assert.That(Convert.ToDouble(chat.GetMeta(Meta.STALENESS)), Is.EqualTo(2));


            code = "CHAT c1\nSET $fish.name=Mary\nSAY Hi $fish.name";
            code += "\nCHAT c2\nSET $c1.stalenessIncr=2";

            rt = new ChatRuntime(null);
            rt.ParseText(code, true);

            chat = rt["c1"];
            Assert.That(chat, Is.Not.Null);
            Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
            Assert.That(chat.commands[1].GetType(), Is.EqualTo(typeof(Say)));

            chat.Resolve(globals);

            chat2 = rt["c2"];
            Assert.That(chat2, Is.Not.Null);
            Assert.That(chat2.commands[0].GetType(), Is.EqualTo(typeof(Set)));

            // no need to check metadata, except for staleness
            Assert.That(chat.StalenessIncr(), Is.EqualTo(Defaults.CHAT_STALENESS_INCR));
            chat2.Resolve(globals);
            Assert.That(chat.StalenessIncr(), Is.EqualTo(2));
        }

        [Test]
        public void MethodsInvoke()
        {
            var fish = new Fish("Frank");
            var obj = Methods.Invoke(fish, "Id");
            Assert.That(obj.ToString(), Is.EqualTo("9"));
            Methods.Invoke(fish, "Id", new object[] { 10 });
            Assert.That(Methods.Invoke(fish, "Id").ToString(), Is.EqualTo("10"));
        }

        [Test]
        public void GetSetProperties()
        {
            var fish = new Fish("Frank");
            var obj = Properties.Get(fish, "name");
            Assert.That(obj.ToString(), Is.EqualTo("Frank"));

            Properties.Set(fish, "name", "Bill");
            Assert.That(fish.name, Is.EqualTo("Bill"));
        }
        [Test]
        public void ImmediateTripleLoop()
        {
            string[] lines = {
                "CHAT c1",
                "SAY C1",
                "GO #c2",
                "CHAT c2",
                "SAY C2",
                "GO #c1",
            };

            ChatRuntime rt = new ChatRuntime();
            rt.ParseText(String.Join("\n", lines));

            var s = rt.InvokeImmediate(globals);
            Assert.That(s, Is.EqualTo("C1\nC2\nC1\n\nGO #c2 looped"));
        }


        [Test]
        public void ImmediateSimpleLoop()
        {
            var lines = new[] {
                 "CHAT Test {type=a,stage=b}",
                 "SAY Find",
                 "GO #Test"
             };

            ChatRuntime rt = new ChatRuntime(Client.AppConfig.TAC);
            rt.ParseText(String.Join("\n", lines));

            var s = rt.InvokeImmediate(null);

            // Note: exits before GO to avoid infinite loop
            Assert.That(s, Is.EqualTo("Find\nFind\n\nGO #Test looped"));
        }

        [Test]
        public void ImmediateWithDynamicGo()
        {
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
        public void ImmediateModeResume()
        {
            string[] lines = {
                "CHAT c",
                "SAY hello",
                "GO #c1",
                "SAY ok",
                "CHAT c1",
                "SAY goodbye"
            };
            ChatRuntime rt = new ChatRuntime();
            rt.ParseText(String.Join("\n", lines));

            var s = rt.InvokeImmediate(globals);
            Assert.That(s, Is.EqualTo("hello\ngoodbye\nok"));
        }

        [Test]
        public void ImmediateModeSimple()
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

            //ChatRuntime.SILENT = true;
            rt.strictMode = false;
            var s = rt.InvokeImmediate(globals);
            //ChatRuntime.SILENT = false;
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
        public void ContinueAskTest()
        {
            string[] lines = {
                "CHAT c",
                "ASK Hello?",
                "OPT Yes",
                "OPT No",
                "SAY Ok"
            };
            ChatRuntime rt = new ChatRuntime();
            rt.ParseText(String.Join("\n", lines));
            var s = rt.InvokeImmediate(globals);
            Assert.That(s, Is.EqualTo("Hello?\n[No]\nOk").Or.EqualTo("Hello?\n[Yes]\nOk"));
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
            //Assert.That(s, Is.EqualTo("hello dog.\nare you a dog?\ngoodbye."));
            //Assert.That(s, Is.EqualTo("hello dog.\nare you a dog?\ngoodbye."));
            Assert.That(s, Is.EqualTo("hello dog.\nare you a dog?\n[No]\ngoodbye.")
                        .Or.EqualTo("hello dog.\nare you a dog?\n[Yes]\ngoodbye."));


            s = rt.InvokeImmediate(globals, "c");
            Assert.That(s, Is.EqualTo("hello dog.\nare you a dog?\n[No]\ngoodbye.")
                        .Or.EqualTo("hello dog.\nare you a dog?\n[Yes]\ngoodbye."));
            //Assert.That(s, Is.EqualTo("hello dog.\nare you a dog?\ngoodbye."));

            s = rt.InvokeImmediate(globals, "c2");
            Assert.That(s, Is.EqualTo("goodbye."));

            Assert.Throws<UnboundSymbol>(() => rt.InvokeImmediate(null));


            ChatRuntime.SILENT = true;
            rt.strictMode = false;
            s = rt.InvokeImmediate(null);
            ChatRuntime.SILENT = false;
            Assert.That(s, Is.EqualTo("hello $animal.\nare you a $animal?\n[No]\ngoodbye.")
                        .Or.EqualTo("hello $animal.\nare you a $animal?\n[Yes]\ngoodbye."));
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
            Assert.That(s, Is.EqualTo("hello dog.\nare you a dog?\n[No]\ngoodbye.")
                         .Or.EqualTo("hello dog.\nare you a dog?\n[Yes]\ngoodbye."));

            s = rt.InvokeImmediate(globals, "c");
            Assert.That(s, Is.EqualTo("hello dog.\nare you a dog?\n[No]\ngoodbye.")
                         .Or.EqualTo("hello dog.\nare you a dog?\n[Yes]\ngoodbye."));

            s = rt.InvokeImmediate(globals, "c2");
            Assert.That(s, Is.EqualTo("goodbye."));

            Assert.Throws<UnboundSymbol>(() => rt.InvokeImmediate(null));

            ChatRuntime.SILENT = true;
            rt.strictMode = false;
            s = rt.InvokeImmediate(null);
            ChatRuntime.SILENT = false;
            Assert.That(s, Is.EqualTo("hello $animal.\nare you a $animal?\n[No]\ngoodbye.")
                        .Or.EqualTo("hello $animal.\nare you a $animal?\n[Yes]\ngoodbye."));
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
            Assert.That(s, Is.EqualTo("hello dog.\nare you a dog?\n[No]\ngoodbye.")
                         .Or.EqualTo("hello dog.\nare you a dog?\n[Yes]\ngoodbye."));

            s = rt.InvokeImmediate(globals, "c");
            Assert.That(s, Is.EqualTo("hello dog.\nare you a dog?\n[No]\ngoodbye.")
                         .Or.EqualTo("hello dog.\nare you a dog?\n[Yes]\ngoodbye."));

            s = rt.InvokeImmediate(globals, "c1");
            Assert.That(s, Is.EqualTo("goodbye."));

            Assert.Throws<UnboundSymbol>(() => rt.InvokeImmediate(null));

            ChatRuntime.SILENT = true;
            rt.strictMode = false;

            s = rt.InvokeImmediate(null);
            ChatRuntime.SILENT = false;
            Assert.That(s, Is.EqualTo("hello $animal.\nare you a $animal?\n[No]\ngoodbye.")
                        .Or.EqualTo("hello $animal.\nare you a $animal?\n[Yes]\ngoodbye."));
        }

        [Test]
        public void TestGetExtMethods()
        {
            var s = Methods.InvokeTransform("animal", "articlize");
            Assert.That(s, Is.EqualTo("an animal"));

            s = Methods.InvokeTransform("cat", "articlize");
            Assert.That(s, Is.EqualTo("a cat"));

            Assert.That(Methods.InvokeTransform("tobacco", "pluralize"), Is.EqualTo("tobacco"));
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
            var ints = new List<int>() { 4, 7, 2, 3 };
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

        [Test]
        public void AssignmentTests()
        {
            Assert.That(Operator.EQ.Invoke("hello", "hello"), Is.True);
            Assert.That(Operator.EQ.Invoke("hello", ""), Is.False);
            Assert.That(Operator.EQ.Invoke("hello", null), Is.False);

            Assert.That(Operator.NE.Invoke("hello", "hello"), Is.False);
            Assert.That(Operator.NE.Invoke("hello", ""), Is.True);
            Assert.That(Operator.NE.Invoke("hello", null), Is.True);

            Assert.That(Operator.EQ.Invoke("true", "false"), Is.False);
            Assert.That(Operator.EQ.Invoke("false", "false"), Is.True);
            Assert.That(Operator.EQ.Invoke("false", null), Is.False);

            Assert.That(Operator.NE.Invoke("hello", ""), Is.True);
            Assert.That(Operator.NE.Invoke("hello", "false"), Is.True);

            Assert.Throws<OperatorException>(() => Operator.NE.Invoke(null, null));
        }

        [Test]
        public void EqualityTests()
        {
            Assert.That(Operator.EQ.Invoke("hello", "hello"), Is.True);
            Assert.That(Operator.EQ.Invoke("hello", ""), Is.False);
            Assert.That(Operator.EQ.Invoke("hello", null), Is.False);

            Assert.That(Operator.NE.Invoke("hello", "hello"), Is.False);
            Assert.That(Operator.NE.Invoke("hello", ""), Is.True);
            Assert.That(Operator.NE.Invoke("hello", null), Is.True);

            Assert.That(Operator.EQ.Invoke("true", "false"), Is.False);
            Assert.That(Operator.EQ.Invoke("false", "false"), Is.True);
            Assert.That(Operator.EQ.Invoke("false", null), Is.False);

            Assert.That(Operator.NE.Invoke("hello", ""), Is.True);
            Assert.That(Operator.NE.Invoke("hello", "false"), Is.True);

            Assert.Throws<OperatorException>(() => Operator.NE.Invoke(null, null));
        }

        [Test]
        public void ComparisonTests()
        {
            Assert.That(Operator.GT.Invoke("2", "1"), Is.True);
            Assert.That(Operator.GT.Invoke("1", "2"), Is.False);
            Assert.That(Operator.GT.Invoke("1", "1"), Is.False);
            Assert.That(Operator.GT.Invoke("2.0", "1"), Is.True);
            Assert.That(Operator.GT.Invoke("1.0", "2"), Is.False);
            Assert.That(Operator.GT.Invoke("1.0", "1"), Is.False);
            Assert.That(Operator.GT.Invoke("2.0", "1.00"), Is.True);
            Assert.That(Operator.GT.Invoke("1.0", "2.00"), Is.False);
            Assert.That(Operator.GT.Invoke("1.0", "1.00"), Is.False);

            Assert.That(Operator.LT.Invoke("2", "1"), Is.False);
            Assert.That(Operator.LT.Invoke("1", "2"), Is.True);
            Assert.That(Operator.LT.Invoke("1", "1"), Is.False);
            Assert.That(Operator.LT.Invoke("2.0", "1"), Is.False);
            Assert.That(Operator.LT.Invoke("1.0", "2"), Is.True);
            Assert.That(Operator.LT.Invoke("1.0", "1"), Is.False);
            Assert.That(Operator.LT.Invoke("2.0", "1.00"), Is.False);
            Assert.That(Operator.LT.Invoke("1.0", "2.00"), Is.True);
            Assert.That(Operator.LT.Invoke("1.0", "1.00"), Is.False);

            Assert.That(Operator.LE.Invoke("2", "1"), Is.False);
            Assert.That(Operator.LE.Invoke("1", "2"), Is.True);
            Assert.That(Operator.LE.Invoke("1", "1"), Is.True);
            Assert.That(Operator.LE.Invoke("2.0", "1"), Is.False);
            Assert.That(Operator.LE.Invoke("1.0", "2"), Is.True);
            Assert.That(Operator.LE.Invoke("1.0", "1"), Is.True);
            Assert.That(Operator.LE.Invoke("2.0", "1.00"), Is.False);
            Assert.That(Operator.LE.Invoke("1.0", "2.00"), Is.True);
            Assert.That(Operator.LE.Invoke("1.0", "1.00"), Is.True);

            Assert.Throws<OperatorException>(() => Operator.GT.Invoke("2", ""));
            Assert.Throws<OperatorException>(() => Operator.LT.Invoke("2", null));
            Assert.Throws<OperatorException>(() => Operator.LE.Invoke("2", "h"));
            Assert.Throws<OperatorException>(() => Operator.GE.Invoke("", ""));
        }

        [Test]
        public void MatchingTests()
        {
            Assert.That(Operator.SW.Invoke("Hello", "He"), Is.True);
            Assert.That(Operator.SW.Invoke("Hello", "Hello"), Is.True);
            Assert.That(Operator.SW.Invoke("Hello", "Hej"), Is.False);
            Assert.That(Operator.SW.Invoke("Hello", null), Is.False);
            Assert.That(Operator.SW.Invoke("Hello", ""), Is.True);

            Assert.That(Operator.EW.Invoke("Hello", "o"), Is.True);
            Assert.That(Operator.EW.Invoke("Hello", "Hello"), Is.True);
            Assert.That(Operator.EW.Invoke("Hello", "l1o"), Is.False);
            Assert.That(Operator.EW.Invoke("Hello", null), Is.False);
            Assert.That(Operator.EW.Invoke("Hello", ""), Is.True);

            Assert.That(Operator.RE.Invoke("Hello", "ll"), Is.True);
            Assert.That(Operator.RE.Invoke("Hello", "e"), Is.True);
            Assert.That(Operator.RE.Invoke("Hello", "l1"), Is.False);
            Assert.That(Operator.RE.Invoke("Hello", null), Is.False);
            Assert.That(Operator.RE.Invoke("Hello", ""), Is.True);


            Assert.That(Operator.SW.Invoke("$Hello", "$"), Is.True);
            Assert.That(Operator.EW.Invoke("$Hello", "$"), Is.False);
            Assert.That(Operator.RE.Invoke("$Hello", "$"), Is.True);
            Assert.That(Operator.RE.Invoke("hello", "(hello|bye)"), Is.True);
            Assert.That(Operator.RE.Invoke("bye", "(hello|bye)"), Is.True);
            Assert.That(Operator.RE.Invoke("by", "(hello|bye)"), Is.False);

            Assert.Throws<OperatorException>(() => Operator.SW.Invoke(null, "hello"));
            Assert.Throws<OperatorException>(() => Operator.SW.Invoke(null, null));
        }
    }
}
