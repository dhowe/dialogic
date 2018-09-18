using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Dialogic
{
    [TestFixture]
    public class EventTests : GenericTests
    {
        // Tendar: SuspendEvent, ResumeEvent, ChoiceEvent, SaveEvent
        // Other: ClearEvent, ChatUpdate, UserEvent

        [Test]
        public void StalenessUpdateTest()
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

            new AutoResetEvent(false).WaitOne(20); // async hack

            //chats.ForEach(Console.WriteLine);
            Assert.That(rt.FindChatByLabel("c1").Staleness(), Is.EqualTo(10));
            Assert.That(rt.FindChatByLabel("c2").Staleness(), Is.EqualTo(10));
            Assert.That(rt.FindChatByLabel("c3").Staleness(), Is.EqualTo(5));
            Assert.That(rt.FindChatByLabel("c4").Staleness(), Is.EqualTo(100));
        }

        public void ChoiceEventTest()
        {
            // TODO

        }

        [Test]
        public void SuspendEventTest()
        {
            // TODO

        }

        //[Test]
        public void ResumeEventTest()
        {
            // TODO
            string[] lines = {
                "CHAT c1 {type=a}",
                "CHAT c2 {type=b,day=fri}",
                "CHAT c3 {type=c,day=thurs}",
            };

            string contents = String.Join("\n", lines);
            List<Chat> chats = ChatParser.ParseText(contents, NO_VALIDATORS);
            ChatRuntime rt = new ChatRuntime(chats);

            for (int i = 0; i < 3; i++)
            {
                EventArgs icu = new ResumeEvent("{}");
                rt.Update(null, ref icu);
            }
        }

        [Test]
        public void ClearEventTest()
        {
            // TODO
        }

        [Test]
        public void ChatUpdateTest()
        {
            // TODO
        }


        [Test]
        public void UserEventTest()
        {
            // TODO
        }

    }
}