using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;

namespace Dialogic
{
    [TestFixture]
    public class EventTests
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
        public void ResumeWithHardConstraints()
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

            bool ok = false;
            string contents = String.Join("\n", lines);
            ChatRuntime rt = new ChatRuntime(Tendar.AppConfig.Actors);

            rt.AddFindListener((c) => {
                ok = true;
                Assert.That(c, Is.Not.EqualTo(null));
                Assert.That(c.text, Is.EqualTo("CORE_Tap"));
            });
            rt.ParseText(contents);
            rt.Run();

            EventArgs gameEvent = new ResumeEvent("{!!type=tap,!stage=CORE}");
            var ue = rt.Update(globals, ref gameEvent);

            var tries = 0;  // TODO: yuck
            while (++tries < 20) Thread.Sleep(1);

            Assert.That(ok, Is.True);
        }


        [Test]
        public void RuntimeModesTest()
        {
            string[] lines = {
                "CHAT c",
                "SAY hello $animal.",
                "ASK are you a $animal?",
                "SAY ok.",
                "CHAT c1",
                "SAY goodbye."
            };
            ChatRuntime rt = new ChatRuntime();
            rt.ParseText(String.Join("\n", lines));

            rt.immediateMode = true;
            rt.strictMode = true;

            var s = rt.InvokeImmediate(globals);
            Assert.That(s, Is.EqualTo("hello dog.\nare you a dog?\nok.\ngoodbye."));

            s = rt.InvokeImmediate(globals, "c1");
            Assert.That(s, Is.EqualTo("goodbye."));

            Assert.Throws<UnboundSymbol>(() => rt.InvokeImmediate(null));

            rt.immediateMode = true;
            rt.strictMode = false;

            ChatRuntime.SILENT = true;
            s = rt.InvokeImmediate(null);
            ChatRuntime.SILENT = false;

            Assert.That(s, Is.EqualTo("hello $animal.\nare you a $animal?\nok.\ngoodbye."));
        }

        [Test]
        public void StalenessEventTest()
        {
            string[] lines = {
                "CHAT c1 {type=a}",
                "CHAT c2 {type=a,day=fri}",
                "CHAT c3 {type=b,day=thurs}",
                "CHAT c4"
            };

            string contents = String.Join("\n", lines);
            List<Chat> chats = ChatParser.ParseText(contents, NO_VALIDATORS);
            ChatRuntime rt = new ChatRuntime(chats);

            //chats.ForEach(Console.WriteLine);
            chats.ForEach(c => Assert.That(c.Staleness(), Is.EqualTo(Defaults.CHAT_STALENESS)));
            EventArgs icu = new StalenessUpdate(5);
            rt.Update(null, ref icu);
            chats.ForEach(c => Assert.That(c.Staleness(), Is.EqualTo(5)));
return;
            icu = new StalenessUpdate(100, "#c4");
            rt.Update(null, ref icu);
            Assert.That(rt.FindChatByLabel("c1").Staleness(), Is.EqualTo(5));
            Assert.That(rt.FindChatByLabel("c4").Staleness(), Is.EqualTo(100));

            icu = new StalenessUpdate(10, "{!type=a}");
            rt.Update(null, ref icu);

            new AutoResetEvent(false).WaitOne(20); // async hack for C# 4.0

            //chats.ForEach(Console.WriteLine);
            Assert.That(rt.FindChatByLabel("c1").Staleness(), Is.EqualTo(10));
            Assert.That(rt.FindChatByLabel("c2").Staleness(), Is.EqualTo(10));
            Assert.That(rt.FindChatByLabel("c3").Staleness(), Is.EqualTo(5));
            Assert.That(rt.FindChatByLabel("c4").Staleness(), Is.EqualTo(100));
        }

        [Test]
        public void ResumeEventTest()
        {
            // pending
        }

    }
}
