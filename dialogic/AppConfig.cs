using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Dialogic;

namespace Tendar // change to TendAR
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
			// hack for uppercase transform names
			if (!(c is Chat))
			{
				var rt = c.GetRuntime();
				if (rt != null && !Transforms.Contains("pluralize"))
				{
					rt.AddTransform("pluralize", Transforms.Pluralize);
					rt.AddTransform("articlize", Transforms.Articlize);
					rt.AddTransform("capitalize", Transforms.Capitalize);
					rt.AddTransform("quotify", Transforms.Quotify);
					rt.AddTransform("cap", Transforms.Capitalize);
					rt.AddTransform("an", Transforms.Articlize);
					rt.AddTransform("emosyn", EmoSyn); // defined below
					rt.AddTransform("emoadj", EmoAdj); // defined below
				}
			}

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




	//public interface IAppConfig {
	//    List<IActor> GetActors();
	//    List<CommandDef> GetCommands();
	//    Func<Command, bool>[] GetValidators();
	//}
}
