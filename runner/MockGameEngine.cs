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
        GuppyTalk guppy;

        string diaText, diaType;
        string[] diaOpts;
        Dictionary<string, string> meta;

        public MockGameEngine()
        {
            lastFrameMs = Util.Elapsed();
            millisPerFrame = 1000 / targetFPS;
            guppy = new GuppyTalk(Program.srcpath + "/data/gscript.gs", Program.globals);
            meta = new Dictionary<string, string>();
        }

        public void Run()
        {
            while (true)
            {
                if (Util.Elapsed() - lastFrameMs > millisPerFrame)
                {
                    frameCount++;
                    GuppyEvent ge = guppy.Update(Program.globals);
                    if (ge != null)
                    {
                        diaText = (string)ge.Remove("text");
                        diaType = (string)ge.Remove("type");

                        if (diaType == "Say")
                        {
                            diaText += " " + Util.Stringify(ge.data);
                        }
                        else if (diaType == "Ask")
                        {
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
                        }
                        else if (diaType == "Do") 
                        {
                            diaText = "(Do: " + diaText.Trim() + ")";
                        }
                        else 
                        {
                            throw new Exception("Bad event: "+ge);
                        }
                        ge = null;

                        Print(diaText);
                    }
                    lastFrameMs = Util.Elapsed();
                }
                Thread.Sleep(1);
            }
        }

        private void Print(string s, bool addInfo=false)
        {
            if (addInfo) s = "#" + frameCount + "@" + Util.ElapsedSec() + "\t" + s;
            Console.WriteLine(s); 
        }
    }
}
