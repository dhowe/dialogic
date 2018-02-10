using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using Dialogic;
using Dialogic.Client;

namespace runner
{
    class MainClass
    {
        public static void TestTimers(string[] args) {
            Util.Init();
            Timers.SetInterval(500, () => Console.WriteLine(Util.Elapsed()));
            Console.WriteLine("done: "+Util.Elapsed());
            System.Threading.Thread.Sleep(10000);
        }

        public static void Main(string[] args)
        {
            List<Chat> chats = ChatParser.ParseFile("../../../dialogic/gscript.gs");
            //List<Chat> chats = ChatParser.ParseText("ASK Ready to play? #5\nSAY Done");
            ChatRuntime cm = new ChatRuntime(chats);

            //ChatClient cl = new SimpleClient(); // Simple client
            ChatClient cl = new ConsoleClient(); // Console client

            cl.Subscribe(cm); // Client subscribes to chat events
            cm.Subscribe(cl); // Dialogic subscribes to Unity events

            cm.Run();
        }
    }

    class SimpleClient : ChatClient
    {
        protected override void OnChatEvent(ChatRuntime cm, ChatEvent e)
        {
            Command cmd = e.Command;
            if (!(cmd is Go)) Console.WriteLine(cmd);
        }
    }
}
