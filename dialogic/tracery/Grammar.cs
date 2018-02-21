using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;
//using YamlDotNet.Serialization;

namespace dialogic.tracery
{
    /// <summary>
    /// A set of rules that can be "flattened" to produce a single random string.
    /// Often a single json object.
    /// </summary>
    public class Grammar
    {
        /// <summary>
        /// Object containing all of the deserialized json rules.
        /// </summary>
        public JObject Rules;

        /// <summary>
        /// RNG to pick from multiple rules.
        /// </summary>
        private Random Random = new Random();

        /// <summary>
        /// Regex for matching expansion symbols.
        /// #animal# etc.
        /// </summary>
        public static Regex ExpansionRegex = new Regex(@"(?<!\[|:)(?!\])#.+?(?<!\[|:)#(?!\])");

        /// <summary>
        /// Regex for matching save symbols.
        /// [hero:#name#], [heroPet:#animal#] etc.
        /// </summary>
        public static Regex SaveSymbolRegex = new Regex(@"\[.+?\]");

        /// <summary>
        /// Modifier function table.
        /// </summary>
        public Dictionary<string, Func<string, string>> ModifierLookup;

        /// <summary>
        /// Key/value store for savable data.
        /// </summary>
        public Dictionary<string, string> SaveData;

        /// <summary>
        /// Read all text from a file and pass it to the other constructor.
        /// </summary>
        /// <param name="source"></param>
        public Grammar(FileInfo source) : this(File.ReadAllText(source.FullName))
        {
        }

        /// <summary>
        /// Load the rules list by deserializing the source as a json object.
        /// </summary>
        /// <param name="source"></param>
        public Grammar(string source)
        {
            // Populate the rules list
            PopulateRules(source);

            // Set up the function table
            ModifierLookup = new Dictionary<string, Func<string, string>>
            {
                { "a",             Modifiers.A },
                { "beeSpeak",      Modifiers.BeeSpeak },
                { "capitalize",    Modifiers.Capitalize },
                { "comma",         Modifiers.Comma },
                { "inQuotes",      Modifiers.InQuotes },
                { "s",             Modifiers.S },
                { "ed",            Modifiers.Ed },
                { "capitalizeAll", Modifiers.CapitalizeAll }
            };
            
            // Initialize the save storage
            SaveData = new Dictionary<string, string>();
        }

        /// <summary>
        /// Deserialize the source string from either json or yaml and populate the rules
        /// object.
        /// </summary>
        /// <param name="source"></param>
        private void PopulateRules(string source)
        {
            // Is it valid json?
            if (InputValidators.IsValidJson(source))
            {
                // Deserialize directly
                Rules = JsonConvert.DeserializeObject<JObject>(source);
            }
            /* Is it valid yaml?
            else if (InputValidators.IsValidYaml(source))
            {
                // Deserialize yaml
                var deserializer = new Deserializer();
                var yamlObject = deserializer.Deserialize(new StringReader(source));

                // Reserialize the yaml as json into the Rules object
                var rules = JsonConvert.SerializeObject(yamlObject);
                Rules = JsonConvert.DeserializeObject<dynamic>(rules);
            }*/
            else
            {
                throw new Exception("Grammar file doesn't seem to be valid JSON");// or YAML!");
            }
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
        /// <param name="rule">The rule to start on. Often #origin#.</param>
        /// <returns>The resultant string, flattened from the rules.</returns>
        public string Flatten(string rule)
        {
            // Get expansion symbols
            var expansionMatches = ExpansionRegex.Matches(rule);

            // If there are no expansion symbols then attempt to resolve any save symbols
            if (expansionMatches.Count == 0)
            {
                ResolveSaveSymbols(rule);
            }

            // Iterate expansion symbols
            foreach (Match match in expansionMatches)
            {
                ResolveSaveSymbols(match.Value);
                
                // Remove the # surrounding the symbol name
                var matchName = match.Value.Replace("#", "");

                // Remove the save symbols
                matchName = SaveSymbolRegex.Replace(matchName, "");

                if (matchName.Contains("."))
                {
                    matchName = matchName.Substring(0, matchName.IndexOf("."));
                }

                // Get modifiers
                var modifiers = new List<string>();
                modifiers = GetModifiers(match.Value);

                // If there's no modifier with that name then skip
                if(modifiers == null)
                {
                    continue;
                }

                // Get the selected rule
                var selectedRule = Rules[matchName] ?? SaveData[matchName] ?? matchName;
            
                // If the rule has any children then pick one at random
                if (selectedRule.Type == JTokenType.Array)
                {
                    var index = Random.Next(0, ((JArray)selectedRule).Count);
                    var chosen = selectedRule[index].ToString();
                    var resolved = Flatten(chosen);

                    resolved = ApplyModifiers(resolved, modifiers);

                    rule = rule.Replace(match.Value, resolved);
                }
                else 
                {
                    // Otherwise flatten it
                    var resolved = Flatten(selectedRule.ToString());

                    resolved = ApplyModifiers(resolved, modifiers);

                    rule = rule.Replace(match.Value, resolved);
                }
            }

            return rule;
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
                var save = saveMatch.Value.Replace("[", "").Replace("]",  "");

                // If it's just [hero], then flatten #hero#
                if(!save.Contains(":"))
                {
                    Flatten("#"+save+"#");
                    continue;
                }

                // hero:#name#
                var saveSplit = save.Split(':');

                // hero
                var name = saveSplit[0];

                // Flatten: #name#
                var data = Flatten(saveSplit[1]);

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
            var modifiers = symbol.Replace("#", "").Split('.').ToList();
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
                if(!ModifierLookup.ContainsKey(modifier))
                    continue;

                // Otherwise execute the function and take the output
                resolved = ModifierLookup[modifier](resolved);
            }

            // Give back the string
            return resolved;
        }
    }
}
 