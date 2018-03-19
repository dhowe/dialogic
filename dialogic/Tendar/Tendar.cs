using System;
using System.Collections.Generic;
using Dialogic;

namespace Tendar // move to runner (parse-time, runtime)
{
    public static class Config
    {
        public static List<ISpeaker> Speakers = new List<ISpeaker>();
        public static Func<Command, bool> Validator = ValidatorFun;

        static Config()
        {
            var customCmd = new CommandDef("NVM", typeof(Tendar.Nvm));
            Speakers.Add(new Speaker("Guppy", true, Validator, customCmd));
            Speakers.Add(new Speaker("Tendar"));
        }

        private static bool ValidatorFun(Command c)
        {
            if (c is Chat && !c.HasMeta("NoStart"))
            {
                if (c.GetMeta(Meta.PLOT) == null) throw new ParseException
                    ("Missing required meta-key: " + Meta.PLOT);

                if (c.GetMeta(Meta.STAGE) == null) throw new ParseException
                    ("Missing required meta-key: " + Meta.STAGE);
            }

            if (c is Find)
            {
                // add default staleness if not otherwise specified
                if (c.GetMeta(Meta.STALENESS) == null) 
                {
                    c.SetMeta(Meta.STALENESS, Defaults.FIND_STALENESS);
                }
            }

            return true;
        }
    }

    public class Nvm : Wait, ISendable
    {
        public static double NVM_DURATION = 5.0;

        protected override double DefaultDuration()
        {
            return NVM_DURATION;
        }

        public override string TypeName()
        {
            return "Nvm";
        }
    }
}
