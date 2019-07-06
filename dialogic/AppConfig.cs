using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Dialogic;
using MessagePack;

namespace Client
{
    /// <summary>
    /// An example config class for a client appliation, including actors,
    /// validators, transforms, and custom command types.
    /// </summary>
    public class AppConfig : IAppConfig
    {
        // TODO: factor out DefaultConfig class 
        public static AppConfig TAC = new AppConfig();

        private IDictionary<string, Func<string, string>> transforms;
        private List<Func<Command, bool>> validators;
        private List<CommandDef> commands;
        private List<IActor> actors;

        private static readonly string STAGE = "stage";
        private static readonly string TYPE = "type";
        private static readonly string NOSTART = "noStart";

        private AppConfig()
        {
            this.actors = new List<IActor> {
                new Actor("Guppy", true), new Actor("TendAR") };

            this.commands = new List<CommandDef> {
                new CommandDef("NVM", typeof(Client.Nvm)) };

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

        public List<IActor> GetActors() => actors;

        public List<CommandDef> GetCommands() => commands;

        public IDictionary<string, Func<string, string>> GetTransforms()
            => transforms;

        public List<Func<Command, bool>> GetValidators() => validators;

        public void AddActor(IActor actor) => actors.Add(actor);

        public void AddValidator(Func<Command, bool> func) 
            => validators.Add(func);

        public void AddCommand(string name, Type t) 
            => commands.Add(new CommandDef(name, t));

        public void AddTransform(string name, Func<string, string> f) 
            => transforms.Add(name, f);

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

        // TODO: remove
        private static string EmoSyn(string emotion)
        {
            if (!synNouns.ContainsKey(emotion))
            {
                Console.WriteLine("[WARN] no syns for: " + emotion);
                return emotion;
            }
            return (string)Util.RandItem(synNouns[emotion]);
        }

        // TODO: remove

        private static string EmoAdj(string emotion)
        {
            if (!synAdjs.ContainsKey(emotion))
            {
                Console.WriteLine("[WARN] no adjs for: " + emotion);
                return emotion;
            }
            return (string)Util.RandItem(synAdjs[emotion]);
        }

        // TODO: remove
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

        // TODO: remove
        private static IDictionary<string, string[]> synAdjs
            = new Dictionary<string, string[]>
        {
            {"anger",        new[]{ "irritated", "furious", "angry", "outraged", "pissed" }},
            {"elation",      new[]{ "blissed", "elated", "blissful", "euphoric", "ecstatic"}},
            {"sadness",      new[]{ "melancholic", "miserable", "sad", "sorrowful", "anguished" }},
            {"surprise",     new[]{ "wide-eyed", "amazed", "surprised", "shocked", "awed" }},
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

    // Example custom command
    public class Nvm : Command, IAssignable
    {
        public static double NVM_DURATION = 5.0;

        protected internal override void Init(string txt, string lbl, string[] metas)
        {
            base.Init(txt, lbl, metas);
            delay = txt.Length == 0 ? NVM_DURATION : Convert.ToDouble(txt);
        }
    }

    // Example serializer using MessagePack
    public class SerializerMessagePack : ISerializer
    {
        static readonly IFormatterResolver ifr = MessagePack.Resolvers
            .ContractlessStandardResolverAllowPrivate.Instance;

        public byte[] ToBytes(ChatRuntime rt)
        {
            return MessagePackSerializer.Serialize<Snapshot>(Snapshot.Create(rt), ifr);
        }

        public void FromBytes(ChatRuntime rt, byte[] bytes)
        {
            MessagePackSerializer.Deserialize<Snapshot>(bytes, ifr).Update(rt);
        }

        public string ToJSON(ChatRuntime rt)
        {
            return MessagePackSerializer.ToJson(ToBytes(rt));
        }
    }
}
