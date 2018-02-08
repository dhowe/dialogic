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

            ConsoleClient cl = new ConsoleClient(); // Example client

            cl.Subscribe(cm); // Client subscribes to chat events
            cm.Subscribe(cl); // Dialogic subscribes to Unity events

            cm.Run();
        }
    }
}
