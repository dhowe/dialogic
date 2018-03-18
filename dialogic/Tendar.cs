using System;
using System.Collections.Generic;
using Dialogic;

namespace Tendar
{
    public static class Config
    {
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
