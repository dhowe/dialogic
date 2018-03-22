using System;
using System.Collections.Generic;
using Dialogic;

namespace Tendar
{
    public static class AppConfig
    {
        public static List<IActor> Speakers = new List<IActor>();
        public static Func<Command, bool> Validator = ValidatorFun;

        public static IDictionary<string, double> STALENESS_BY_TYPE
            = new Dictionary<string, double>()
        {
            { "plot", 5.0 }, { "shake", 5.0 }, { "tap", 5.0 }, { "critic", 5.0 },
            { "tankResp", 5.0 }, { "hit", 5.0 }, { "poke", 5.0 }, { "hungry", 5.0 },
            { "eatResp", 5.0 }, { "poop", 5.0 }, { "pooped", 5.0 }, { "seeEmo", 5.0 },
            { "capReq", 5.0 }, { "capSuc", 5.0 }, { "capProg", 5.0 }, { "capFail", 5.0 },
            { "hello", 5.0 }, { "return", 5.0 }, { "rand",5.0}
        };

        static AppConfig()
        {
            var customCmd = new CommandDef("NVM", typeof(Tendar.Nvm));
            Speakers.Add(new Actor("Guppy", true, Validator, customCmd));
            Speakers.Add(new Actor("Tendar"));
        }

        private static bool ValidatorFun(Command c)
        {
            if ((c is Chat && !(c.HasMeta("NoStart") || c.HasMeta("noStart"))))
            {
                if (!c.HasMeta("type")) throw new ParseException
                    ("Missing required meta-key 'type'");

                if (!c.HasMeta(Meta.STAGE)) throw new ParseException
                    ("Missing required meta-key '" + Meta.STAGE + "'");
            }
            else if (c.GetType() == typeof(Find))
            {
                // add default staleness if not otherwise specified
                if (!c.HasMeta(Meta.STALENESS))
                {
                    var type = c.GetMeta("type");
                    if (type != null) // but only if a type is specified
                    {
                        var typeStr = (string)type;
                        if (STALENESS_BY_TYPE.ContainsKey(typeStr))
                        {
                            var ds = STALENESS_BY_TYPE[(string)typeStr];
                            c.SetMeta(Meta.STALENESS, new Constraint
                                (Operator.LT, Meta.STALENESS, ds.ToString()));
                        }
                    }
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
