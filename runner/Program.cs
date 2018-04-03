using System;
using System.Collections.Generic;
using System.Threading;
using Dialogic;
using Tendar;

namespace runner
{
    /// <summary>
    /// Entry point to run the test program (a mock game engine)
    /// </summary>
    class Program
    {
        public static void Main(string[] args)
        {
            new MockGameEngine(srcpath + "/data/gscript.gs").Run();
        }

        public static string srcpath = "../../../dialogic";
    }

    /// <summary>
    /// A simple fake game engine that calls dialogic's Update function
    /// 30 times (or so) per second
    /// </summary>
    public class MockGameEngine
    {
        /// <summary>Mock game state variables</summary>
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
        private bool interrupted = false;
        private string evtText, evtType, evtActor;
        private string[] diaOpts;

        /// <summary>
        /// Create an engine from a script file or folder script files
        /// </summary>
        /// <param name="fileOrFolder">File or folder.</param>
        public MockGameEngine(string fileOrFolder)
        {
            dialogic = new ChatRuntime(AppConfig.Actors);
            dialogic.ParseFile(fileOrFolder);
            dialogic.Run("#GScriptTest");
        }

        /// <summary>
        /// Start the Run loop for the engine
        /// </summary>
        public void Run()
        {
            // TODO: test suspend/resume on user events

            var now = Util.Millis();
            Timers.SetTimeout(Util.Rand(2000, 10000), () =>
             {
                 interrupted = true;
                 Console.WriteLine("\n<user-event#tap>" +
                     " after " + Util.Millis(now) + "ms\n");

                 gameEvent = new UserEvent("Tap");
             });

            while (true)
            {
                Thread.Sleep(30);
                IUpdateEvent ue = dialogic.Update(globals, ref gameEvent);
                if (ue != null) HandleEvent(ref ue);
            }
        }


        ////////////////////////////////////////////////////////////////////////   


        /// <summary>
        /// Prints our the various commands with useful debugging info
        /// </summary>
        private void HandleEvent(ref IUpdateEvent ue)
        {
            interrupted = false;

            evtText = ue.Text();
            evtType = ue.Type();
            evtActor = ue.Actor();

            ue.RemoveKeys(Meta.TEXT, Meta.TYPE, Meta.ACTOR);

            switch (evtType)
            {
                case "Say":
                    evtText = evtText + " " + ue.Data().Stringify();
                    break;

                case "Ask":
                    DoPrompt(ue);
                    SendRandomResponse(ue);
                    break;

                case "Wait":
                    var now = Util.Millis();

                    Timers.SetTimeout(3000, () =>
                    {
                        if (!interrupted)
                        {
                            Console.WriteLine("\n<resume-event#>" +
                                " after " + Util.Millis(now) + "ms\n");

                            // send ResumeEvent after 5 sec
                            // (), (#Game), or ({type=a,stage=b,last=true})
                            gameEvent = new ResumeEvent();
                        }
                    });

                    evtText = ("(" + (evtType + " " +
                        ue.Data().Stringify()).Trim() + ")");
                    break;

                default:
                    evtText = ("(" + evtType + ": " + (evtText + " "
                        + ue.Data().Stringify()).Trim() + ")");
                    break;
            }

            Console.WriteLine(evtText);

            ue = null;  // dispose event 
        }

        private void DoPrompt(IUpdateEvent ue)
        {
            diaOpts = ue.Get(Meta.OPTS).Split('\n');

            ue.RemoveKeys(Meta.TEXT, Meta.TYPE, Meta.OPTS);

            // add any meta tags
            evtText = evtText + " " + ue.Data().Stringify();

            // add the options
            for (int i = 0; i < diaOpts.Length; i++)
            {
                evtText += "\n  (" + i + ") " + diaOpts[i];
            }
        }

        private void SendRandomResponse(IUpdateEvent ue)
        {
            double timeout = ue.GetDouble(Meta.TIMEOUT, -1);
            if (timeout > -1)
            {
                var delay = Util.ToMillis(Util.Rand(timeout / 3, timeout));
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
