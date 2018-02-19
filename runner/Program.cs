using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Dialogic;

namespace runner
{
    class Program
    {
        public static string srcpath = "../../../dialogic";
        public static Dictionary<string, object> globals = 
            new Dictionary<string, object>() {
                { "emotion", "special" },
                { "place", "My Tank" },
                { "Happy", "HappyFlip" },
                { "verb", "play" },
                { "neg", "(nah|no|nope)" },
                { "var3", 2 }
            };

        public static void Main(string[] args)
        {
            /*new LexerTest().TestParse(srcpath + "/data/find.gs");
            new MockGameEngine().Run();*/

            new LexerTest().TestParse(srcpath + "/data/queries.gs");

            ChatParser.ParseText("ASK Game?\nOPT Sure\nOPT $neg\n");
            List<Chat> chats = ChatParser.ParseFile(srcpath + "/data/gscript.gs");
            Console.WriteLine(chats[0].ToTree());
            ChatRuntime cm = new ChatRuntime(chats, globals);
            cm.LogFile = srcpath + "/dia.log";

            AbstractClient cl = new ConsoleClient(); // Console client

            cl.Subscribe(cm); // Client subscribes to chat events
            cm.Subscribe(cl); // Dialogic subscribes to Unity events

            cm.Run();

        }
    }

    public class MockGameEngine
    {
        double millisPerFrame, targetFPS = 60;
        int lastFrameMs = 0, frameCount = 0;
        string diaText, diaType;
        string[] diaOpts;
        UpdateAdapter dialogic;

        public MockGameEngine()
        {
            lastFrameMs = Util.Elapsed();
            millisPerFrame = 1000 / targetFPS;

            var chats = ChatParser.ParseFile(Program.srcpath + "/data/gscript.gs");
            dialogic = new UpdateAdapter(chats, Program.globals);
        }

        public void Run()
        {
            while (true)
            {
                if (Util.Elapsed() - lastFrameMs > millisPerFrame)
                {
                    frameCount++;

                    // Call the dialogic interface
                    UpdateEvent ge = dialogic.Update(Program.globals, null);

                    // Handle the returned event
                    if (ge != null) HandleEvent(ge);

                    lastFrameMs = Util.Elapsed();
                }
                Thread.Sleep(1);
            }
        }

        private void HandleEvent(UpdateEvent ge)
        {
            diaText = (string)ge.Remove("text");
            diaType = (string)ge.Remove("type");

            switch (diaType)
            {
                case "Say":
                    diaText += " " + Util.Stringify(ge.data);
                    break;

                case "Do":
                    diaText = "(Do: " + diaText.Trim() + ")";
                    break;

                case "Ask":
                    var opts = ge.Remove("opts");

                    diaText += " " + Util.Stringify(ge.data);
                    diaOpts = ((string)opts).Split(',');

                    for (int i = 0; i < diaOpts.Length; i++)
                    {
                        diaText += "\n  (" + i + ") " + diaOpts[i];
                    }

                    int timeout = ge.RemoveInt("timeout");
                    if (timeout > -1)
                    {
                        Timers.SetTimeout(timeout, () =>
                        {
                            Console.WriteLine("<empty-choice-event>");
                        });
                    }
                    break;

                default:
                    throw new Exception("Bad event: " + ge);
            }

            Print(diaText);
            ge = null;  // disose event 
        }

        private void Print(string s, bool addInfo = false)
        {
            if (addInfo) s = "#" + frameCount + "@" + Util.ElapsedSec() + "\t" + s;
            Console.WriteLine(s);
        }
    }

}
