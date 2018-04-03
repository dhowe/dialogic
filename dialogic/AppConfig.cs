using System;
using System.Collections.Generic;
using Dialogic;

namespace Tendar
{
    /// <summary>
    /// Specifies custom behavior for the Tendar application, including Actors,
    /// validators, and custom command types.
    /// </summary>
    public static class AppConfig
    {
        private static Func<Command, bool> Validator = ValidateCommand;

        const string STAGE = "stage", TYPE = "type";

        public static Actor GUPPY = new Actor("Guppy",
            true, Validator, new CommandDef("NVM", typeof(Tendar.Nvm)));

        public static Actor TENDAR = new Actor("Tendar");

        public static List<IActor> Actors = new List<IActor> { GUPPY, TENDAR };

        private static bool ValidateCommand(Command c)
        {
            if (c.GetType() == typeof(Chat))
            {
                if (!(c.HasMeta("NoStart") || c.HasMeta("noStart")))
                {
                    if (!c.HasMeta(TYPE)) throw new ParseException
                        ("missing required meta-key '" + TYPE + "'");

                    if (!c.HasMeta(STAGE)) throw new ParseException
                        ("missing required meta-key '" + STAGE + "'");
                }
            }

            return true;
        }
    }

    /// <summary>
    /// A custom Command for the Tendar application
    /// </summary>
    public class Nvm : Command, ISendable
    {
        public static double NVM_DURATION = 5.0;

        public override void Init(string text, string label, string[] metas)
        {
            base.Init(text, label, metas);
            delay = text.Length == 0 ? NVM_DURATION : Convert.ToDouble(text);
        }
    }

    //public interface IAppConfig {
    //    List<IActor> GetActors();
    //    List<CommandDef> GetCommands();
    //    Func<Command, bool>[] GetValidators();
    //}
}
