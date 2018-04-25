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
