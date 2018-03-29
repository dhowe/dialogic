using System;
using System.Collections.Generic;

namespace Dialogic
{
    internal class AppEventHandler
    {
        private readonly ChatRuntime runtime;
        private readonly ChatScheduler scheduler;
        private Find findDelegate;

        internal AppEventHandler(ChatRuntime rt)
        {
            this.runtime = rt;
            this.scheduler = rt.scheduler;
        }

        internal IUpdateEvent OnEvent(ref EventArgs ge, IDictionary<string, object> globals)
        {
            if (ge is IUserEvent) return UserActionHandler(ref ge, globals);
            if (ge is ISuspend) return SuspendHandler(ref ge, globals);
            if (ge is IResume) return ResumeHandler(ref ge, globals);
            if (ge is IChoice) return ChoiceHandler(ref ge, globals);
            if (ge is IClear) return ClearHandler(ref ge, globals);

            throw new DialogicException("Unexpected event-type: " + ge.GetType());
        }

        private IUpdateEvent UserActionHandler(ref EventArgs ge, IDictionary<string, object> globals)
        {
            IUserEvent ue = (IUserEvent)ge;
            var label = ue.GetEventType();
            ge = null;

            scheduler.Suspend();
            scheduler.Launch("#On" + label + "Event");
            return null;
        }

        private IUpdateEvent SuspendHandler(ref EventArgs ge, IDictionary<string, object> globals)
        {
            ge = null;
            scheduler.Suspend();
            return null;
        }

        private IUpdateEvent ClearHandler(ref EventArgs ge, IDictionary<string, object> globals)
        {
            ge = null;
            scheduler.Clear();
            return null;
        }

        private IUpdateEvent ResumeHandler(ref EventArgs ge, IDictionary<string, object> globals)
        {
            IResume ir = (IResume)ge;
            var label = ir.ResumeWith();
            ge = null;

            if (String.IsNullOrEmpty(label))
            {
                runtime.nextEventTime = scheduler.Resume();
            }
            else if (label.StartsWith("#", Util.IC))
            {
                scheduler.Launch(label);
            }
            else // else, parse as FIND meta data
            {
                if (findDelegate == null) findDelegate = new Find();

                try
                {
                    findDelegate.Init(label);
                }
                catch (ParseException e)
                {
                    throw new RuntimeParseException(e);
                }

                runtime.FindAsync(findDelegate, globals);
            }

            return null;
        }

        private IUpdateEvent ChoiceHandler(ref EventArgs ge, IDictionary<string, object> globals)
        {
            IChoice ic = (IChoice)ge;
            var idx = ic.GetChoiceIndex();
            ge = null;

            if (idx < 0 || idx >= scheduler.prompt.Options().Count)
            {
                // bad index, so reprompt for now
                Console.WriteLine("Invalid index " + idx + ", reprompting\n");
                scheduler.prompt.Realize(globals); // re-realize
                return new UpdateEvent(scheduler.prompt);
            }
            else
            {
                Opt opt = scheduler.prompt.Selected(idx);
                //opt.Realize(globals); // not needed

                if (opt.action != Command.NOP)
                {
                    // We've gotten a response with a branch, so finish & take it
                    runtime.FindAsync((Find)opt.action); // find next
                }
                else
                {
                    scheduler.chat = scheduler.prompt.parent; // just continue
                }
                return null;
            }
        }
    }
}
