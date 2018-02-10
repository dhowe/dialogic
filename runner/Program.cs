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
        public static void Main(string[] args)
        {
            string srcpath = "../../../dialogic";

            List<Chat> chats = ChatParser.ParseFile(srcpath + "/test.gs");
            //List<Chat> chats = ChatParser.ParseText("ASK Game?\nOPT Sure\nOPT $neg\n");
            ChatRuntime cm = new ChatRuntime(chats);
            cm.LogFileName = srcpath + "/dia.log";

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
            Console.WriteLine(cmd);
        }
    }
}
