using System;
using System.Collections.Generic;
using System.Threading;
using Dialogic;

namespace runner
{
    class Program
    {
        public static void Main(string[] args)
        {
            new MockGameEngine(srcpath + "/data/gscript.gs").Run();
        }

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
    }

    public class MockGameEngine
    {
        public static Dictionary<string, object> globals =
            new Dictionary<string, object>() {
                { "emotion", "special" },
                { "place", "my tank" },
                { "Happy", "HappyFlip" },
                { "verb", "play" },
                { "neg", "(nah|no|nope)" },
                { "var3", 2 }
            };

        private readonly ChatRuntime runtime;
        private IChoice choiceEvent = null;
        string diaText, diaType;
        string[] diaOpts;

        public MockGameEngine(string fileOrFolder)
        {
            List<Chat> chats = ChatParser.ParseFile(fileOrFolder);
            runtime = new ChatRuntime(chats);
            runtime.Run();
        }

        public void Run()
        {
            Console.WriteLine();
            while (true)
            {
                Thread.Sleep(30);
                IUpdateEvent ue = runtime.Update(globals, ref choiceEvent);
                if (ue != null) HandleEvent(ref ue);
            }
        }

        private void HandleEvent(ref IUpdateEvent ge)
        {
            diaText = ge.Text();
            diaType = ge.Type();

            switch (diaType)
            {
                case "Say":
                    ge.RemoveKeys(Meta.TEXT, Meta.TYPE);
                    diaText += " " + Util.Stringify(ge.Data());
                    break;

                case "Do":
                    diaText = "(Do: " + diaText.Trim() + ")";
                    break;

                case "Nvm":
                    diaText = "...";
                    break;

                case "Ask":
                    DoPrompt(ge);
                    SendRandomResponse(ge);
                    break;
            }

            Console.WriteLine(diaText);

            ge = null;  // dispose event 
        }

        private void DoPrompt(IUpdateEvent ge)
        {
            diaOpts = ge.Get(Meta.OPTS).Split('\n');

            ge.RemoveKeys(Meta.TEXT, Meta.TYPE, Meta.OPTS);
            diaText += " " + Util.Stringify(ge.Data());

            for (int i = 0; i < diaOpts.Length; i++)
            {
                diaText += "\n  (" + i + ") " + diaOpts[i];
            }
        }

        private void SendRandomResponse(IUpdateEvent ge)
        {
            int timeout = ge.GetInt(Meta.TIMEOUT, -1);
            if (timeout > -1)
            {
                Timers.SetTimeout(Util.Rand(timeout / 3, timeout), () =>
                {
                    // choice a valid response, or -1 for no response
                    int choice = Util.Rand(diaOpts.Length + 1) - 1;
                    Console.WriteLine("\n<choice-index#" + choice + ">\n");
                    choiceEvent = new ChoiceEvent(choice);
                });
            }
        }

    }
}
