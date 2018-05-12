using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Dialogic
{
    /// <summary>
    /// Holds built in transformation functions for expanding symbols / groups, eg pluralize, articlize, etc.
    /// New Transforms can be added by calling ChatRuntime.AddTransform(). 
    /// Implemented as sealed, thread-safe (lazy) singleton
    /// </summary>
    public sealed class Transforms
    {
        internal static Transforms Instance { get { return lazy.Value; } }

        /// <summary>
        /// Prefixes the string with 'a' or 'an' as appropriate.
        /// </summary>
        public static string Articlize(string str)
        {
            return ("aeiou".Contains(str.ToLower()[0]) ? "an " : "a ") + str;
        }

		/// <summary>
		/// Capitalizes the first character.
		/// </summary>
		public static string Capitalize(string str)
        {
            return char.ToUpper(str[0]) + str.Substring(1);
        }

        /// <summary>
        /// Wraps the given string in double-quotes.
        /// </summary>
        public static string Quotify(string str)
        {
            return "\"" + str + "\"";
        }

        /// <summary>
        /// Pluralizes a single word according to english regular/irregular rules.
        /// </summary>
        public static string Pluralize(string word)
        {
            if (word.Contains(' ')) throw new TransformException
                ("pluralize accepts only single words");
                
            RegexRule[] rules = PLURAL_RULES;

            var lword = word.ToLower();
            if (!WORD.IsMatch(word) ||
                Array.IndexOf(MODALS, lword) > -1)
            {
                return word;
            }

            for (int i = 0; i < rules.Length; i++)
            {
                if (rules[i].applies(lword))
                {
                    return rules[i].fire(word);
                }
            }

            return DEFAULT_PLURAL.fire(word);
        }

		public static bool Contains(string v)
        {
            return Transforms.Instance.lookup.ContainsKey(v);
        }

        // --------------------------------------------------------------------

        private Transforms()
        {
            lookup = new ConcurrentDictionary<string, Func<string, string>>();

            lookup.TryAdd("Pluralize",  Pluralize);
            lookup.TryAdd("Articlize",  Articlize);
            lookup.TryAdd("Capitalize", Capitalize);
            lookup.TryAdd("Quotify",    Quotify);         
            lookup.TryAdd("An", Articlize);
            lookup.TryAdd("Cap", Capitalize);
        }

        private static readonly Lazy<Transforms> lazy =
            new Lazy<Transforms>(() => new Transforms());

        private readonly ConcurrentDictionary<string, Func<string, string>> lookup;

        internal static void Add(string name, Func<string, string> value)
        {
            Transforms.Instance.lookup.TryAdd(name, value);
        }

        internal static Func<string, string> Get(string name)
        {
            Func<string, string> value;
            Instance.lookup.TryGetValue(name, out value);
            return value;
        }

        // --------------------------------------------------------------------

        internal static string[] MODALS = { "shall", "would", "may", "might", "ought", "should" };

        internal static Regex WORD = new Regex(ANY_STEM, RegexOptions.IgnoreCase);

        internal static RegexRule DEFAULT_PLURAL = new RegexRule(ANY_STEM, 0, "s");

        internal static RegexRule NULL_PLURALS = new RegexRule("^(bengali|bonsai|booze|cellulose|digitalis|mess|moose|burmese|colossus|discus|emphasis|expertise|finess|fructose|gauze|glucose|grease|haze|incense|malaise|mayonnaise|maltese|menopause|merchandise|nitrocellulose|olympics|overuse|paradise|poise|polymerase|portuguese|prose|recompense|remorse|repose|siamese|innings|sleaze|sioux|suspense|swiss|vietnamese|unease|aircraft|anise|antifreeze|applause|archdiocese|apparatus|asparagus|barracks|bellows|bison|bob|bourgeois|bream|brill|butterfingers|cargo|carp|chassis|clothes|chub|cod|contretemps|corps|crossroads|dace|deer|dice|doings|dory|downstairs|eldest|earnings|economics|electronics|finnan|firstborn|flounder|fowl|fry|fries||golf|grand|grief|gudgeon|gulden|haddock|hake|halibut|headquarters|herring|hertz|horsepower|goods|hovercraft|kilohertz|mackerel|means|megahertz|mullet|offspring|pants|patois|perch|pickerel|pike|pince-nez|quid|rand|rendezvous|roach|salmon|samurai|series|seychelles|shad|sheep|smelt|spacecraft|species|[a-z]+fish|sweepstakes|swordfish|tench|tennis|[a-z]+osis|[a-z]+itis|[a-z]+ness|tobacco|tope|triceps|trout|tuna|tunny|turbot|trousers|undersigned|[a-z+]fowl|[a-z*]works|whiting|woodworm|yen|aries|pisces|forceps|jeans|physics|mathematics|news|odds|politics|remains|acoustics|aesthetics|aquatics|basics|ceramics|classics|cosmetics|dialectics|dynamics|ethics|harmonics|heroics|mechanics|metrics|optics|physics|polemics|pyrotechnics|surroundings|thanks|statistics|goods|aids|wildlife|[a-z]+[ln]ese)$", 0, "");

        internal static RegexRule[] PLURAL_RULES = {

            NULL_PLURALS,
            new RegexRule("^concerto$", 1, "i"),
            new RegexRule("^(piano|photo|solo|ego|tobacco|cargo|golf|grief)$", 0, "s"),
            new RegexRule("^(wildlife)$", 0, "s"),
            new RegexRule(CONS + "o$", 0, "es"),
            new RegexRule(CONS + "y$", 1, "ies"),
            new RegexRule("^ox$", 0, "en"),
            new RegexRule("^(stimul|alumn|termin)us$", 2, "i"),
            new RegexRule("^corpus$", 2, "ora"),
            new RegexRule("(xis|sis)$", 2, "es"),
            new RegexRule("([zsx]|ch|sh)$", 0, "es"),
            new RegexRule(VWLS + "fe$", 2, "ves"),
            new RegexRule(VWLS + "f$", 1, "ves"),
            new RegexRule("(eu|eau)$", 0, "x"),
            new RegexRule("(man|woman)$", 2, "en"),
            new RegexRule("money$", 2, "ies"),
            new RegexRule("person$", 4, "ople"),
            new RegexRule("motif$", 0, "s"),
            new RegexRule("^meninx|phalanx$", 1, "ges"),
            new RegexRule("schema$", 0, "ta"),
            new RegexRule("^bus$", 0, "ses"),
            new RegexRule("child$", 0, "ren"),
            new RegexRule("^(curi|formul|vertebr|larv|uln|alumn|signor|alg|minuti)a$", 0, "e"),
            new RegexRule("^(maharaj|raj|myn|mull)a$", 0, "hs"),
            new RegexRule("^apex|cortex$", 2, "ices"),
            new RegexRule("^weltanschauung$", 0, "en"),
            new RegexRule("^lied$", 0, "er"),
            new RegexRule("^tooth$", 4, "eeth"),
            new RegexRule("^[lm]ouse$", 4, "ice"),
            new RegexRule("^foot$", 3, "eet"),
            new RegexRule("femur", 2, "ora"),
            new RegexRule("goose", 4, "eese"),
            new RegexRule("(human|german|roman)$", 0, "s"),
            new RegexRule("^(monarch|loch|stomach)$", 0, "s"),
            new RegexRule("^(taxi|chief|proof|ref|relief|roof|belief)$", 0, "s"),
            new RegexRule("^(co|no)$", 0, "'s"),
            new RegexRule("^blond$", 0, "es"),
            new RegexRule("^(medi|millenni|consorti|sept|memorabili|ser)um$", 2, "a"),
            new RegexRule("^(memorandum|bacterium|curriculum|minimum|maximum|referendum|spectrum|phenomenon|criterion)$", 2, "a"),
            new RegexRule("^(appendix|index|matrix)", 2, "ices")
        };

        internal const string ANY_STEM = "^((\\w+)(-\\w+)*)(\\s((\\w+)(-\\w+)*))*$";
        internal const string CONS = "[bcdfghjklmnpqrstvwxyz]", VWLS = "[lraeiou]";
    }

    internal class RegexRule
    {
        private readonly int offset;
        private readonly string suffix;
        private readonly Regex re;

        public RegexRule(string regex, int truncate, string suff)
        {
            re = new Regex(regex, RegexOptions.IgnoreCase);
            offset = truncate;
            suffix = suff;
        }

        public bool applies(string word)
        {
            word = word.Trim();
            return re.IsMatch(word);
        }

        public string fire(string word)
        {
            word = word.Trim();
            string t = truncate(word);
            return t + suffix;
        }

        public bool analyse(string word)
        {
            return ((suffix != string.Empty) && word.EndsWith
                (suffix, true, CultureInfo.InvariantCulture));
        }

        private string truncate(string word)
        {
            return (offset == 0) ? word :
                word.Substring(0, word.Length - offset);
        }
    }
}
