using System;
using System.Collections.Generic;
using Dialogic;

namespace Tendar
{
    public static class AppConfig //: IAppConfig
    {
        public static List<IActor> Actors = new List<IActor>();
        private static Func<Command, bool> Validator = ValidateCommand;

        static AppConfig()
        {
            Actors.Add(new Actor("Guppy", true, Validator, 
                new CommandDef("NVM", typeof(Tendar.Nvm))));
            Actors.Add(new Actor("Tendar"));
        }

        private static bool ValidateCommand(Command c)
        {
            if (c.GetType() == typeof(Chat))
            {
                if (!(c.HasMeta("NoStart") || c.HasMeta("noStart")))
                {
                    if (!c.HasMeta("type")) throw new ParseException
                        ("missing required meta-key 'type'");

                    if (!c.HasMeta(Meta.STAGE)) throw new ParseException
                        ("missing required meta-key '" + Meta.STAGE + "'");
                }
            }
            else if (c.GetType() == typeof(Find))
            {
                /* add default staleness if not otherwise specified
                if (!c.HasMeta(Meta.STALENESS))
                {
                    var type = c.GetMeta("type");
                    if (type != null) // but only if a type is specified
                    {
                        var typeStr = ((Constraint)type).value;

                        // TODO: change this to per-Find values
                        if (STALENESS_BY_TYPE.ContainsKey(typeStr))
                        {
                            var ds = STALENESS_BY_TYPE[(string)typeStr];
                            c.SetMeta(Meta.STALENESS, new Constraint
                                (Operator.LT, Meta.STALENESS, ds.ToString()));
                        }
                    }
                }*/
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

    //public interface IAppConfig {
    //    List<IActor> GetActors();
    //    List<CommandDef> GetCommands();
    //    Func<Command, bool>[] GetValidators();
    //}
}
