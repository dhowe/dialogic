using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;
using YamlDotNet.Serialization;
using System.Text;
using System.Globalization;

namespace Dialogic
{
    //@cond unused

    // adapted from https://github.com/josh-perry/Tracery.Net
    public class Grammar 
    {
        public static string OPEN_TAG = "<", CLOSE_TAG = ">";
        public static string OPEN_SAVE = "[", CLOSE_SAVE = "]";
        public static char ASSIGN = '=';
        public static bool DBUG = false;

        protected JObject rules;

        private Random random = new Random();

        /// <summary>
        /// Regex for matching expansion symbols.
        /// </summary>
        public static Regex ExpansionRegex = new Regex(@"(?<!\[|:)(?!\])"
            + OPEN_TAG + @".+?(?<!\[|" + ASSIGN + ")" + CLOSE_TAG + @"(?!\])");

        /// <summary>
        /// Regex for matching save symbols.
        /// </summary>
        public static Regex SaveSymbolRegex = new Regex(@"\[.+?\]");

        public Dictionary<string, Func<string, string>> ModifierLookup;

        public Dictionary<string, string> SaveData;

        public Grammar(FileInfo f) : this(File.ReadAllText(f.FullName, Encoding.UTF8)) { }

        /// <summary>
        /// Load rules by deserializing the source as a json object
        /// </summary>
        /// <param name="source"></param>
        public Grammar(string source = null)
        {
            // Set up the function table TODO: replace w' rita versions
            ModifierLookup = new Dictionary<string, Func<string, string>>
            {
                { "a",             Modifiers.A },
                { "capitalize",    Modifiers.Capitalize },
                { "comma",         Modifiers.Comma },
                { "quotify",       Modifiers.Quotify },
                { "s",             Modifiers.S },
                { "ed",            Modifiers.Ed },
                { "allCaps",       Modifiers.AllCaps }
            };

            // Initialize the save storage
            SaveData = new Dictionary<string, string>();

            if (source != null) Rules(source);
        }

        public JObject Rules()
        {
            return rules;
        }

        public Grammar Rules(JObject rules)
        {
            this.rules = rules;
            return this;
        }

        public Grammar Rules(string source)
        {
            if (IsValidJson(source))
            {
                // Deserialize directly
                return Rules(JsonConvert.DeserializeObject<JObject>(source));
            }
            else if (IsValidYaml(source))
            {
                // Deserialize via the YAML parser 
                return Rules(DeserializeYaml(source));
            }
            throw new Exception("Invalid JSON or YAML!");
        }

        private JObject DeserializeYaml(string source)
        {
            var deserializer = new Deserializer();
            var yamlObject = deserializer.Deserialize(new StringReader(source));

            // convert rules to json
            var json = JsonConvert.SerializeObject(yamlObject);
            return JsonConvert.DeserializeObject<JObject>(json);
        }

        /// <summary>
        /// Add a modifier to the modifier lookup.
        /// </summary>
        /// <param name="name">The name to identify the modifier with.</param>
        /// <param name="func">A method that returns a string and takes a string as a param.</param>
        public void AddModifier(string name, Func<string, string> func)
        {
            ModifierLookup[name] = func;
        }

        /// <summary>
        /// Resolve the list of rules into a single sentence.
        /// If the rule contains an expansion symbol, follow it and resolve those recursively.
        /// The result should be a single string.
        /// </summary>
        /// <param name="rule">The rule to start on. Often "<start>".</param>
        /// <returns>The resultant string, flattened from the rules.</returns>
        public string Expand(string rule)
        {
            // Get expansion symbols
            var expansionMatches = ExpansionRegex.Matches(rule);

            if (DBUG && expansionMatches.Count < 1 && (rule.Contains(OPEN_TAG) || rule.Contains(CLOSE_TAG)))
            {
                throw new Exception("No matches for '" + rule + "' (" + ExpansionRegex + ") in\n" + rules);
            }

            // If there are no expansion symbols then attempt to resolve any save symbols
            if (expansionMatches.Count == 0)
            {
                ResolveSaveSymbols(rule);
            }

            // Iterate expansion symbols
            foreach (Match match in expansionMatches)
            {
                if (DBUG) Console.WriteLine("Match: " + match.Value);
                ResolveSaveSymbols(match.Value);

                // Remove the # surrounding the symbol name
                var matchName = match.Value.Replace(OPEN_TAG, "").Replace(CLOSE_TAG, "");

                if (DBUG) Console.WriteLine("MatchName: " + match.Value);

                // Remove the save symbols
                matchName = SaveSymbolRegex.Replace(matchName, "");

                if (matchName.Contains("."))
                {
                    matchName = matchName.Substring(0, matchName.IndexOf(".", StringComparison.Ordinal));
                }

                // Get modifiers
                var modifiers = new List<string>();
                modifiers = GetModifiers(match.Value);

                // If there's no modifier with that name then skip
                if (modifiers == null) continue;

                JToken selectedRule = null;

                // Get the selected rule
                try
                {
                    selectedRule = rules[matchName] ?? SaveData[matchName] ?? matchName;
                }
                catch (Exception)
                {
                    throw new Exception("KEY NOT FOUND: " + matchName);
                }

                // If the rule has any children then pick one at random
                if (selectedRule.Type == JTokenType.Array)
                {
                    var index = random.Next(0, ((JArray)selectedRule).Count);
                    var chosen = selectedRule[index].ToString();
                    var resolved = Expand(chosen);

                    resolved = ApplyModifiers(resolved, modifiers);

                    rule = rule.Replace(match.Value, resolved);
                }
                else
                {
                    // Otherwise flatten it
                    var resolved = Expand(selectedRule.ToString());

                    resolved = ApplyModifiers(resolved, modifiers);

                    rule = rule.Replace(match.Value, resolved);
                }
            }

            return rule;
        }

        public override string ToString()
        {
            var s = "";
            foreach (var kv in rules)
            {
                s += "  " + kv.Key + ": " + kv.Value + "\n";
            }
            return s;
        }

        /// <summary>
        /// Resolves and saves any data marked by save symbols.
        /// </summary>
        /// <param name="rule"></param>
        private void ResolveSaveSymbols(string rule)
        {
            // Get all save symbols
            foreach (Match saveMatch in SaveSymbolRegex.Matches(rule))
            {
                // [hero:#name#]
                var save = saveMatch.Value.Replace(OPEN_SAVE, "").Replace(CLOSE_SAVE, "");

                // If it's just [hero], then flatten #hero#
                if (!save.Contains(ASSIGN))
                {
                    Expand(OPEN_TAG + save + CLOSE_TAG);
                    continue;
                }

                // hero:#name#
                var saveSplit = save.Split(ASSIGN);

                // hero
                var name = saveSplit[0];

                // Flatten: #name#
                var data = Expand(saveSplit[1]);

                SaveData[name] = data;
            }
        }

        /// <summary>
        /// Return a list of modifier names from the provided expansion symbol
        /// Modifiers are extra operations to perform on an expansion symbol.
        ///
        /// For instance:
        ///      #animal.capitalize#
        /// will flatten into a single animal and capitalize the first character of it's name.
        ///
        /// Multiple modifiers can be applied, separated by a .
        ///      #animal.capitalize.inQuotes#
        /// ...for example
        /// </summary>
        /// <param name="symbol">The symbol to take modifiers from:
        /// e.g: #animal#, #animal.s#, #animal.capitalize.s#
        /// </param>
        /// <returns></returns>
        private List<string> GetModifiers(string symbol)
        {
            var modifiers = symbol.Replace(OPEN_TAG, "").Replace(CLOSE_TAG, "").Split('.').ToList();
            modifiers.RemoveAt(0);

            return modifiers;
        }

        /// <summary>
        /// Iterate over the list of modifiers on the expansion symbol and resolve each individually.
        /// </summary>
        /// <param name="resolved">The string to perform the modifications to</param>
        /// <param name="modifiers">The list of modifier strings</param>
        /// <returns>The resolved string with modifiers applied to it</returns>
        private string ApplyModifiers(string resolved, List<string> modifiers)
        {
            // Iterate over each modifier
            foreach (var modifier in modifiers)
            {
                // If there's no modifier by this name in the list, skip it
                if (!ModifierLookup.ContainsKey(modifier))
                    continue;

                // Otherwise execute the function and take the output
                resolved = ModifierLookup[modifier](resolved);
            }

            // Give back the string
            return resolved;
        }
        /// <summary>
        /// Returns true if the given string is valid JSON.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>True if valid, false if not</returns>
        public static bool IsValidJson(string input)
        {
            // Get the first character
            var firstChar = input.TrimStart().First();

            // If it's not { or [ then it can't be valid JSON
            if (firstChar != '{' && firstChar != '[')
            {
                return false;
            }

            // Attempt to parse it
            try
            {
                var obj = JToken.Parse(input);

                // If parsing was successful then it is valid
                return true;
            }
            catch (JsonReaderException)
            {
                // If there was an error reading it then it can't be valid JSON
                return false;
            }
        }

        /// <summary>
        /// Returns true if the given string is valid YAML.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>True if valid, false if not</returns>
        public static bool IsValidYaml(string input)
        {
            try
            {
                // Create a StringReader from the source
                var r = new StringReader(input);

                // Attempt to deserialize the StringReader
                var deserializer = new Deserializer();
                var yamlObject = deserializer.Deserialize(r);

                // If parsing was successful then it is valid
                return true;
            }
            catch (Exception)
            {
                // If there was an error deserialzing then it can't be valid
                return false;
            }
        }
    }

    static class Modifiers
    {
        /// <summary>
        /// Punctuation used to end a sentence.
        /// </summary>
        private static List<char> sentencePunctuation = new List<char> { ',', '.', '!', '?' };

        /// <summary>
        /// List of all vowels.
        /// </summary>
        private static List<char> vowels = new List<char> { 'a', 'e', 'i', 'o', 'u' };

        /// <summary>
        /// Capitalize the first character.
        /// </summary>
        /// <param name="str"></param>
        /// <returns>The modified string</returns>
        public static string Capitalize(string str)
        {
            return char.ToUpper(str[0]) + str.Substring(1);
        }

        /// <summary>
        /// Capitalizes every character.
        /// </summary>
        /// <param name="str"></param>
        /// <returns>The modified string</returns>
        public static string AllCaps(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str);
        }

        /// <summary>
        /// Places a comma after the string unless it's the end of a sentence.
        /// </summary>
        /// <param name="str"></param>
        /// <returns>The modified string</returns>
        public static string Comma(string str)
        {
            var lastChar = str[str.Length - 1];

            if (sentencePunctuation.Contains(lastChar))
                return str;

            return str + ",";
        }

        /// <summary>
        /// Wraps the given string in double-quotes.
        /// </summary>
        /// <param name="str"></param>
        /// <returns>The modified string</returns>
        public static string Quotify(string str)
        {
            return "\"" + str + "\"";
        }

        /// <summary>
        /// Past-tensifies the specified string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns>The modified string</returns>
        public static string Ed(string str)
        {
            var lastChar = str[str.Length - 1];
            var secondToLastChar = str[str.Length - 2];
            var rest = "";

            var index = str.IndexOf(' ');

            if (index > 0)
            {
                rest = str.Substring(0, index);
            }

            switch (lastChar)
            {
                case 'y':
                    // rays, convoys
                    if (_isConsonant(secondToLastChar))
                    {
                        return str.Substring(0, str.Length - 1) + "ied" + rest;
                    }

                    break;

                case 'e':
                    // harpies, cries
                    return str + "d" + rest;

                default:
                    return str + "ed" + rest;
            }

            return str;
        }

        /// <summary>
        /// Pluralises the given string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns>The modified string</returns>
        public static string S(string str)
        {
            var lastChar = str[str.Length - 1];
            var secondToLastChar = str[str.Length - 2];

            switch (lastChar)
            {
                case 'y':
                    // rays, convoys
                    if (!_isConsonant(secondToLastChar))
                    {
                        return str + "s";
                    }

                    // harpies, cries
                    return str.Substring(0, str.Length - 1) + "ies";
                case 'x':
                    // oxen, boxen, foxen
                    return str.Substring(0, str.Length - 1) + "xen";
                case 'z':
                    return str.Substring(0, str.Length - 1) + "zes";
                case 'h':
                    return str.Substring(0, str.Length - 1) + "hes";
                default:
                    return str + "s";
            }
        }

        /// <summary>
        /// Prefixes the string with 'a' or 'an' as appropriate.
        /// </summary>
        /// <param name="str"></param>
        /// <returns>The modified string</returns>
        public static string A(string str)
        {
            var lastChar = str[0];

            if (!_isConsonant(lastChar))
            {
                return "an " + str;
            }

            return "a " + str;
        }

        /// <summary>
        /// Checks to see if the given character is a consonant.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private static bool _isConsonant(char c)
        {
            // If the character is in the vowel list then it's not a consonant
            return !vowels.Contains(c);
        }
    }
    //@endcond
}
