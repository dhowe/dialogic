using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YamlDotNet.Serialization;

namespace dialogic.tracery
{
    /// <summary>
    /// Static class holding grammar input checkers
    /// </summary>
    public static class InputValidators
    {
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
            if(firstChar != '{' && firstChar != '[')
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
}
