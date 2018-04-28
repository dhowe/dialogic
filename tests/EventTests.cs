using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        public async Task ResumeWithHardConstraintsASync()
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

            //rt.AddFindListener((c) =>
            //{
            //    //ok = true;
            //    Console.WriteLine("OUT: " + c);
            //    Assert.That(c, Is.Not.EqualTo(null));
            //});
            rt.ParseText(contents);
            rt.Run();


            //await rt.FindAsync(new Find().Init("{!!type=tap,!stage=CORE}"), globals);

            //EventArgs gameEvent = new ResumeEvent("{!!type=tap,!stage=CORE}");
            //var ue = await rt.Update(globals, ref gameEvent);
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
