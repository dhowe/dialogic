using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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

        const string STAGE = "stage", TYPE = "type", NOSTART = "noStart";

        public static Actor GUPPY = new Actor("Guppy",
            true, Validator, new CommandDef("NVM", typeof(Tendar.Nvm)));

        public static Actor TENDAR = new Actor("Tendar");

        public static List<IActor> Actors = new List<IActor> { GUPPY, TENDAR };

        private static bool ValidateCommand(Command c)
        {
            if (c.GetType() == typeof(Chat))
            {
                if (RE.TestTubeChatBaby.IsMatch(c.text))
                {
                    return true;
                }
                ValidateMeta(c); // throws if invalid
            }

            return true;
        }

        private static void ValidateMeta(Command c)
        {
            if (!c.HasMeta(Meta.PRELOAD) && !c.HasMeta(NOSTART))
            {
                ValidateKey(c, TYPE);
                ValidateKey(c, STAGE);
            }
        }

        private static void ValidateKey(Command c, string key)
        {
            if (!c.HasMeta(key)) throw new ParseException
                ("missing required meta-key '" + key + "', 'noStart' or 'preload'");
        }
    }

    public class Nvm : Command, IAssignable
    {
        public static double NVM_DURATION = 5.0;

        protected internal override void Init(string txt, string lbl, string[] metas)
        {
            base.Init(txt, lbl, metas);
            delay = txt.Length == 0 ? NVM_DURATION : Convert.ToDouble(txt);
        }
    }

    //public interface IAppConfig {
    //    List<IActor> GetActors();
    //    List<CommandDef> GetCommands();
    //    Func<Command, bool>[] GetValidators();
    //}
}
