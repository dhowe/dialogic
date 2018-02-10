using System;
using System.Timers;

namespace Dialogic
{
    // adapted from:
    //   https://codereview.stackexchange.com/questions/113596/writing-cs-analog-of-settimeout-setinterval-and-clearinterval
    public static class Timers
    {
        public static IInterruptable SetInterval(int interval, Action function)
        {
            return StartTimer(interval, function, true);
        }

        public static IInterruptable SetTimeout(int interval, Action function)
        {
            //Console.WriteLine("SetTimeout: "+interval+" "+function);
            return StartTimer(interval, function, false);
        }

        private static IInterruptable StartTimer(int interval, Action function, bool autoReset)
        {
            Action functionCopy = (Action)function.Clone();
            Timer timer = new Timer { Interval = interval, AutoReset = autoReset };
            timer.Elapsed += (sender, e) => functionCopy();
            timer.Start();

            return new TimerInterrupter(timer);
        }
    }

    public interface IInterruptable
    {
        void Stop();
    }

    public class TimerInterrupter : IInterruptable
    {
        private readonly Timer t;

        public TimerInterrupter(Timer timer)
        {
            if (timer == null) throw new ArgumentNullException();
            t = timer;
        }

        public void Stop()
        {
            t.Stop();
        }
    }

}
