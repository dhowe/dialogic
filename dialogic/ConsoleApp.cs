using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Client;

namespace Dialogic
{
    public class ConsoleApp
    {
        static Dictionary<string, object> globals =
            new Dictionary<string, object>() {
                { "emotion", "special" },
                { "place", "my tank" },
                { "Happy", "HappyFlip" },
                { "verb", "play" },
                { "neg", "(nah|no|nope)" },
                { "var3", 2 }
            };

        ChatRuntime dialogic;
        EventArgs gameEvent;

        string evtText, evtType, evtActor;
        string[] evtOpts;

        public ConsoleApp(FileInfo fileOrFolder)
        {
            dialogic = new ChatRuntime(AppConfig.TAC);
            dialogic.ParseFile(fileOrFolder);
            dialogic.Preload(globals);
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
            evtText = ue.Text();
            evtType = ue.Type();
            evtActor = ue.Actor();

            //evtText = "["+evtActor + "] " + evtText;

            ue.RemoveKeys(Meta.TEXT, Meta.TYPE, Meta.ACTOR);

            switch (evtType)
            {
                case "Say":
                    evtText = evtText + " " + ue.Data().Stringify();
                    break;

                case "Ask":
                    DoPrompt(ue);
                    // respond with 'new ChoiceEvent(choiceIdx);'
                    break;

                case "Wait":
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
            evtOpts = ue.Get(Meta.OPTS).Split('\n');

            ue.RemoveKeys(Meta.TEXT, Meta.TYPE, Meta.OPTS);

            // add any meta tags
            evtText = evtText + " " + ue.Data().Stringify();

            // add the options
            for (int i = 0; i < evtOpts.Length; i++)
            {
                evtText += "\n  (" + i + ") " + evtOpts[i];
            }
        }
    }
}
