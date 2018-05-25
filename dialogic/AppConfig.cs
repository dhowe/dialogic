using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Dialogic;

namespace TendAR
{
    /// <summary>
    /// Specifies custom behavior for the Tendar application, including Actors,
    /// validators, and custom command types.
    /// </summary>
    public class AppConfig : IAppConfig
    {
        public static AppConfig TAC = new AppConfig();

        private IDictionary<string, Func<string, string>> transforms;
        private List<Func<Command, bool>> validators;
        private List<CommandDef> commands;
        private List<IActor> actors;

        private static string STAGE = "stage", TYPE = "type", NOSTART = "noStart";

        private AppConfig()
        {
            this.actors = new List<IActor> {
                new Actor("Guppy", true), new Actor("TendAR") };

            this.commands = new List<CommandDef> {
                new CommandDef("NVM", typeof(TendAR.Nvm)) };

            this.validators = new List<Func<Command, bool>> { ValidateCommand };

            this.transforms = new Dictionary<string, Func<string, string>> {
                {"pluralize", Transforms.Pluralize},
                {"articlize", Transforms.Articlize},
                {"capitalize", Transforms.Capitalize},
                {"quotify", Transforms.Quotify},
                {"cap", Transforms.Capitalize},
                {"an", Transforms.Articlize},
                {"emosyn", EmoSyn}, // defined below
                {"emoadj", EmoAdj} // defined below
            };
        }

        //--------------------- interface --------------------------

        public List<IActor> GetActors()
        {
            return actors;
        }

        public List<CommandDef> GetCommands()
        {
            return commands;
        }

        public IDictionary<string, Func<string, string>> GetTransforms()
        {
            return transforms;
        }

        public List<Func<Command, bool>> GetValidators()
        {
            return validators;
        }

        //----------------------------------------------------------

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

        /// <summary>
        /// Returns a random synonym for the emotion
        /// </summary>
        private static string EmoSyn(string emotion)
        {
            return (string)Util.RandItem(synNouns[emotion]);
        }

        /// <summary>
        /// Returns a random adjective synonym for the emotion
        /// </summary>
        private static string EmoAdj(string emotion)
        {
            return (string)Util.RandItem(synAdjs[emotion]);
        }

        private static IDictionary<string, string[]> synNouns
            = new Dictionary<string, string[]>
        {
            {"anger",        new[]{ "irritation", "fury", "anger", "outrage", "tension" }},
            {"elation",      new[]{ "glee", "elation", "bliss", "whoopee", "euphoria" }},
            {"sadness",      new[]{ "melancholy", "misery", "woe", "sorrow", "anguish" }},
            {"surprise",     new[]{ "wonder", "amazement", "surprise", "shock", "awe" }},
            {"fear",         new[]{ "dread", "dismay", "fear", "panic", "terror" }},
            {"worry",        new[]{ "concern", "angst", "worry", "anxiety", "skepticism" }},
            {"amusement",    new[]{ "delight", "mirth", "amusement", "merriment", "hilarity" }},
            {"ennui",        new[]{ "blah", "indifference", "meh", "ennui", "apathy" }},
            {"disgust",      new[]{ "dislike", "nausea", "disgust", "repugnance", "revulsion" }},
            {"desire",       new[]{ "fascination", "passion", "desire", "rapture", "longing" }},
            {"embarassment", new[]{ "discomfort", "bashfulness", "embarrassment", "shame", "mortification" }},
            {"pride",        new[]{ "satisfaction", "confidence", "pride", "dignity", "ego" }},
        };

        private static IDictionary<string, string[]> synAdjs
            = new Dictionary<string, string[]>
        {
            {"anger",        new[]{ "irritated", "furious", "angry", "outraged", "pissed" }},
            {"elation",      new[]{ "blissed", "elated", "blissful", "euphoric", "ecstatic"}},
            {"sadness",      new[]{ "melancholic", "miserable", "sad", "sorrowful", "anguished" }},
            {"surprise",     new[]{ "wonderous", "amazed", "surprised", "shocked", "awed" }},
            {"fear",         new[]{ "dismayed", "afraid", "fearful", "panicked", "scared" }},
            {"worry",        new[]{ "concerned", "angsty", "worried", "anxious", "skeptical" }},
            {"amusement",    new[]{ "delighted", "blissed", "amused", "tickled", "pleased" }},
            {"ennui",        new[]{ "indifferent", "apathetic", "bored", "uninterested", "unenthused"}},
            {"disgust",      new[]{ "hateful", "nauseous", "disgusted", "repulsed", "revolted" }},
            {"desire",       new[]{ "fascinated", "passionate", "desirous", "hungry", "horny" }},
            {"embarassment", new[]{ "discomforted", "ashamed", "embarrassed", "shy", "mortified" }},
            {"pride",        new[]{ "satisfied", "confident", "proud", "dignified", "egotistical" }},
        };
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
}
