using System;
using System.Collections.Generic;
using Dialogic;

namespace Tendar
{
    public static class AppConfig
    {
        public static List<IActor> Actors = new List<IActor>();
        private static Func<Command, bool> Validator = ValidateCommand;

        const string STAGE = "stage", TYPE = "type";

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
                    if (!c.HasMeta(TYPE)) throw new ParseException
                        ("missing required meta-key '"+TYPE+"'");

                    if (!c.HasMeta(STAGE)) throw new ParseException
                        ("missing required meta-key '" + STAGE + "'");
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

        public override IDictionary<string, object> Realize(IDictionary<string, object> globals)
        {
            realized.Clear();

            if (this is ISendable)
            {
                realized[Meta.TEXT] = Realizer.Do(Text, globals);
                realized[Meta.ACTOR] = GetActor().Name();
                realized[Meta.TYPE] = TypeName();
            }

            return realized; // convenience
        }
    }

}
