using System;
using System.Collections.Generic;
using Dialogic;

namespace Tendar
{
    public static class Validators
    {
        public static bool ValidateMeta(Command c)
        {
            if (c is Chat)
            {
                if (c.GetMeta(Meta.PLOT) == null) throw new ParseException
                    ("Mising required meta-key: " + Meta.PLOT);

                if (c.GetMeta(Meta.STAGE) == null) throw new ParseException
                    ("Mising required meta-key: " + Meta.STAGE);
            }

            return true;
        }   
    }
}
