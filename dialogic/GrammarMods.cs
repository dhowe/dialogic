using System.Collections.Generic;
using System.Globalization;

namespace Dialogic
{
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
        /// <param name="arg"></param>
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
            return "\"" + str + "\""; ;
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
}
