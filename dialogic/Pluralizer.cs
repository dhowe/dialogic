using System;
using System.Text.RegularExpressions;

namespace Dialogic
{
    public static class Pluralizer
    {   
       
        public static string Pluralize(string word)
        {
            
            RegexRule[] rules = RiTa.PLURAL_RULES;
            RegexRule rule;
            Regex isWord = new Regex(RiTa.ANY_STEM, RegexOptions.IgnoreCase);
            int i;

            if (!isWord.IsMatch(word) || Array.IndexOf(RiTa.MODALS, word.ToLower()) > -1)
            {
                return word;
            }

            for (i = 0; i < rules.Length; i++)
            {
                rule = rules[i];
                if (rule.applies(word.ToLower()))
                {
                    return rule.fire(word);
                }
            }

            return RiTa.DEFAULT_PLURAL_RULE.fire(word);

        }
    }
}
