using System.Collections.Generic;
using Dialogic;

namespace runner
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string srcpath = "../../../dialogic";

            List<Chat> chats = ChatParser.ParseFile(srcpath + "/data/gscript.gs");
            // ChatParser.ParseText("ASK Game?\nOPT Sure\nOPT $neg\n");
            ChatRuntime cm = new ChatRuntime(chats);
            cm.LogFileName = srcpath + "/dia.log";

            //ChatClient cl = new SimpleClient(); // Simple client
            AbstractClient cl = new ConsoleClient(); // Console client

            cl.Subscribe(cm); // Client subscribes to chat events
            cm.Subscribe(cl); // Dialogic subscribes to Unity events

            cm.Run();
        }
    }
}
