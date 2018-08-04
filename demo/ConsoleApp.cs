using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Client;

namespace Dialogic
{
    public class ConsoleApp
    {
        public static void Main(string[] args)
        {
            var testfile = AppDomain.CurrentDomain.BaseDirectory;
            testfile += "../../../../dialogic/data/console.gs";
            new ConsoleApp(new FileInfo(testfile)).Run();
        }

        static Dictionary<string, object> globals =
            new Dictionary<string, object>() {
                { "userName", "" },
                { "emotion", "" }
        };

        ChatRuntime dialogic;
        EventArgs gameEvent;

        string evtText, evtType, evtActor;
        string[] evtOpts;

        public ConsoleApp(FileInfo fileOrFolder)
        {
            dialogic = new ChatRuntime();
            dialogic.ParseFile(fileOrFolder);
            dialogic.Preload(globals);
            dialogic.Run();
        }

        public void Run()
        {
            while (true)
            {
                IUpdateEvent ue = dialogic.Update(globals, ref gameEvent);
                if (ue != null) HandleEvent(ref ue);
                Thread.Sleep(30);
            }
        }

        private void HandleEvent(ref IUpdateEvent ue)
        {
            evtText = ue.Text();
            evtType = ue.Type();
            evtActor = ue.Actor();

            if (evtActor != null
               ) evtText = evtActor + ": " + evtText;

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

            if (evtType == "Ask")
            {
                int timeout = Util.ToMillis(ue.GetDouble(Meta.TIMEOUT));
                while (!DoResponse(timeout))
                {
                    // default is to reprompt
                    Console.WriteLine(evtText);
                }
            }

            ue = null;  // dispose event 
        }

        private bool DoResponse(int timeout)
        {
            string response = null;
            try
            {
                response = ConsoleReader.ReadLine(timeout);
                int choice = (int)Convert.ChangeType(response, typeof(int));
                this.gameEvent = new ChoiceEvent(choice);
                return true;
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid response '" + response + "', reprompting\n");
            }
            catch (TimeoutException)
            {
                Console.WriteLine("Timeout after "+timeout+"ms, reprompting\n");
            }
            return false;
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
