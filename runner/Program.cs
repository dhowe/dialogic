using System;
using System.Collections.Generic;
using System.Threading;
using Dialogic;
using Tendar;

namespace runner
{
    class Program
    {
        public static void Main(string[] args)
        {
            new MockGameEngine(srcpath + "/data/gscript.gs").Run();
        }

        public static string srcpath = "../../../dialogic";
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

        private ChatRuntime dialogic; 
        private EventArgs gameEvent;
        string diaText, diaType;
        string[] diaOpts;

        public MockGameEngine(string fileOrFolder)
        {
            dialogic = new ChatRuntime(AppConfig.Speakers);
            dialogic.ParseFile(fileOrFolder);
            dialogic.Run();
        }

        public void Run()
        {
            while (true)
            {
                Thread.Sleep(30);
                IUpdateEvent ue = dialogic.Update(globals, ref gameEvent);
                if (ue != null) HandleEvent(ref ue);
            }
        }

        private void HandleEvent(ref IUpdateEvent ue)
        {
            diaText = ue.Text();
            diaType = ue.Type();

            ue.RemoveKeys(Meta.TEXT, Meta.TYPE);

            switch (diaType)
            {
                case "Say":
                    diaText += " " + ue.Data().Stringify();
                    break;

                case "Ask":
                    DoPrompt(ue);
                    SendRandomResponse(ue);
                    break;

                case "Wait":
                    var now = Util.Millis();

                    Timers.SetTimeout(5000, () =>
                    {
                        Console.WriteLine("<resume-event#>" +
                            " after " + Util.Millis(now) + "ms\n");

                        // send ResumeEvent after 5 sec ('#Game' or '{type=a,stage=b,last=true}')

                        gameEvent = new ResumeEvent();
                    });

                    diaText = ("(" + (diaType + " " + 
                        ue.Data().Stringify()).Trim() + ")");
                    break;

                default:
                    diaText = ("(" + diaType + ": " + (diaText + " "
                        + ue.Data().Stringify()).Trim() + ")");
                    break;
            }

            Console.WriteLine(diaText);

            ue = null;  // dispose event 
        }

        private void DoPrompt(IUpdateEvent ge)
        {
            diaOpts = ge.Get(Meta.OPTS).Split('\n');

            ge.RemoveKeys(Meta.TEXT, Meta.TYPE, Meta.OPTS);
                diaText += " " + ge.Data().Stringify(); // show meta

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
                var delay = Util.Rand(timeout / 3, timeout);
                Timers.SetTimeout(delay, () =>
                {
                    // choice a valid response, or -1 for no response
                    int choice = Util.Rand(diaOpts.Length + 1) - 1;
                    Console.WriteLine("\n<choice-index#" + choice + "> after " + delay + "ms\n");
                    gameEvent = new ChoiceEvent(choice);
                });
            }
        }

    }
}
