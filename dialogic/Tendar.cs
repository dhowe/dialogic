using System;
using System.Collections.Generic;
using Dialogic;

namespace Tendar // move to runner (parse-time, runtime)
{
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

    public static class Config
    {
        static Config() {

            ChatParser.TypeMap.Add("NVM", typeof(Tendar.Nvm));
        }

        public static bool ValidateMeta(Command c)
        {
            if (c is Chat)
            {
                if (c.GetMeta(Meta.PLOT) == null) throw new ParseException
                    ("Missing required meta-key: " + Meta.PLOT);

                if (c.GetMeta(Meta.STAGE) == null) throw new ParseException
                    ("Missing required meta-key: " + Meta.STAGE);
            }

            if (c is Find)
            {
                if (c.GetMeta(Meta.STALENESS) == null) // default staleness
                {
                    c.SetMeta(Meta.STALENESS, Defaults.FIND_STALENESS);
                }
            }

            return true;
        }
    }
}
