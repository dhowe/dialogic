using System;
using System.Collections.Generic;

namespace Dialogic.Tendar
{
    public class Tendar
    {
        public static bool ValidateChatMeta(IDictionary<string, object> meta)
        {
            if (!meta.ContainsKey(Meta.PLOT)) throw new DialogicException
                ("Mising required Meta key: " + Meta.PLOT);
            if (!meta.ContainsKey(Meta.STAGE)) throw new DialogicException
                ("Mising required Meta key: " + Meta.STAGE);
            return false;
        }   
    }
}
