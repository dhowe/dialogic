using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Dialogic
{
    public class ConsoleApp
    {
        public static void Mainx(string[] a)
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

            if (evtActor != null) evtText = evtActor + ": " + evtText;

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

            Display(evtText);

            if (evtType == "Ask")
            {
                // default is to reprompt on timeout
                int timeout = Util.ToMillis(ue.GetDouble(Meta.TIMEOUT));
                while (!DoResponse(timeout)) Display(evtText);
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
                Display("Invalid response '" + response + "', reprompting\n");
            }
            catch (TimeoutException)
            {
                Display("Timeout after " + timeout + "ms, reprompting\n");
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

        private void Display(string msg)
        {
            Console.WriteLine(msg);
        }
    }

    static class ConsoleReader //@cond hidden
    {
        private static Thread inputThread;
        private static AutoResetEvent getInput, gotInput;
        private static string input;

        static ConsoleReader()
        {
            getInput = new AutoResetEvent(false);
            gotInput = new AutoResetEvent(false);
            inputThread = new Thread(Reader) { IsBackground = true };
            inputThread.Start();
        }

        private static void Reader()
        {
            while (true)
            {
                getInput.WaitOne();
                input = Console.ReadKey(true).KeyChar.ToString();
                gotInput.Set();
            }
        }

        public static string ReadLine(int timeOutMillisecs = -1)
        {
            getInput.Set();
            bool success = gotInput.WaitOne(timeOutMillisecs);
            if (success) return input;
            throw new TimeoutException("Prompt timeout expired after " + timeOutMillisecs + "ms");
        }

    } //@endcond

    // adapted from:
    //   https://codereview.stackexchange.com/questions/113596
    //   /writing-cs-analog-of-settimeout-setinterval-and-clearinterval
    public static class Timers //@cond hidden
    {
        static IInterruptable timer;

        public static IInterruptable SetInterval(int ms, Action function)
        {
            return timer = ms > -1 ? StartTimer(ms, function, true) : null;
        }

        public static IInterruptable SetTimeout(int ms, Action function)
        {
            return timer = ms > -1 ? StartTimer(ms, function, false) : null;
        }

        private static IInterruptable StartTimer(int interval, Action function, bool autoReset)
        {
            Action functionCopy = (Action)function.Clone();
            System.Timers.Timer t = new System.Timers.Timer
            { Interval = interval, AutoReset = autoReset };
            t.Elapsed += (sender, e) => functionCopy();
            t.Start();

            return new TimerInterrupter(t);
        }
    }//@endcond

    public interface IInterruptable //@cond hidden
    {
        void Stop();
    }

    public class TimerInterrupter : IInterruptable
    {
        private readonly System.Timers.Timer t;

        public TimerInterrupter(System.Timers.Timer timer)
        {
            if (timer == null) throw new ArgumentNullException();
            t = timer;
        }

        public void Stop()
        {
            t.Stop();
        }
    }//@endcond

}
