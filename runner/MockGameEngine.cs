using System;
using System.Collections.Generic;
using System.Threading;
using Dialogic;

namespace runner
{
    public class MockGameEngine
    {
        double millisPerFrame, targetFPS = 60;
        int lastFrameMs = 0, frameCount = 0;
        string diaText, diaType;
        string[] diaOpts;
        GuppyAdapter guppy;

        public MockGameEngine()
        {
            lastFrameMs = Util.Elapsed();
            millisPerFrame = 1000 / targetFPS;
            guppy = new GuppyAdapter(Program.srcpath + "/data/gscript.gs", Program.globals);
        }

        public void Run()
        {
            while (true)
            {
                if (Util.Elapsed() - lastFrameMs > millisPerFrame)
                {
                    frameCount++;

                    // Call the dialogic interface
                    GuppyEvent ge = guppy.Update(Program.globals, null);

                    // Handle the returned event
                    if (ge != null) HandleEvent(ge);

                    lastFrameMs = Util.Elapsed();
                }
                Thread.Sleep(1);
            }
        }

        private void HandleEvent(GuppyEvent ge)
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

        private void Print(string s, bool addInfo=false)
        {
            if (addInfo) s = "#" + frameCount + "@" + Util.ElapsedSec() + "\t" + s;
            Console.WriteLine(s); 
        }
    }
}
