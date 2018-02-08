using System;
using System.Collections.Generic;
using Dialogic;

namespace runner
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            List<Chat> chats = ChatParser.ParseFile("../../../dialogic/gscript.gs");
            ChatRuntime cm = new ChatRuntime(chats);

            ChatClient cl = new TestClient(); // Test client

            cl.Subscribe(cm); // Client subscribes to chat events
            cm.Subscribe(cl); // Dialogic subscribes to Unity events

            cm.Run();
        }
    }

    class TestClient : ChatClient
    {
        protected override void OnChatEvent(ChatRuntime cm, ChatEvent e)
        {
            Command cmd = e.Command;
            Console.WriteLine(cmd);
        }
    }
}
