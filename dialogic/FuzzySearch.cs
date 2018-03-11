using System.Collections.Generic;
using System.Linq;

namespace Dialogic
{
    public static class FuzzySearch
    {
        /**
         * Find highest scoring chat which does not match any constraint key without also 
         * matching its value, or null if none match 
         */
        public static Chat Find(List<Chat> chats, IDictionary<string, object> constraints)
        {
            return FindAll(chats, constraints).FirstOrDefault();
        }

        /**
         * Find all chats according to specified constraints ordered by score.
         * 
         * If none match, an empty list will be returned
         * Cases: 
         *   1. has key, matches ->             allow, score++
         *   2. has key, doesn't match ->       disallow
         *   3a. doesn't have key (normal) ->   allow
         *    b. doesn't have key (strict) ->   disallow
         */
        public static List<Chat> FindAll(List<Chat> chats, IDictionary<string, object> constraints)
        {
            if (constraints == null) return chats;

            Dictionary<Chat, int> matches = new Dictionary<Chat, int>();

            for (int i = 0; i < chats.Count; i++)
            {
                var hits = 0;
                var chatProps = chats[i].GetMeta();

                //Console.WriteLine("CHECK: CHAT."+chatProps[Meta.LABEL]);

                foreach (var key in constraints.Keys)
                {
                    //Console.WriteLine("  Find."+key+ " in "+chats[i].Text+" "+Util.Stringify(chatProps));

                    Constraint constraint = (Constraint)constraints[key];

                    if (chatProps != null && chatProps.ContainsKey(key)) // has-key
                    {
                        var chatPropVal = (string)chatProps[key];

                        if (!(constraint.Check(chatPropVal)))
                        {
                            //Console.WriteLine("    FAIL:" + constraints[key]);
                            hits = -1;
                            break;
                        }
                        else 
                        {
                            hits++;
                            //Console.WriteLine("    HIT" + hits);
                        }
                    }
                    else if (constraint.IsStrict()) // doesn't have-key, fails strict
                    {
                        //Console.WriteLine("    FAIL-STRICT:" + constraints[key]);
                        hits = -1;
                        break;
                    }
                }
                if (hits > -1) matches.Add(chats[i], hits);
            }

            List<KeyValuePair<Chat, int>> list = DescendingRandomSort(matches);

            //list.ForEach((kvp) => Console.WriteLine(kvp.Key + " -> " + kvp.Value));

            return (from kvp in list select kvp.Key).ToList();
        }

        static List<KeyValuePair<Chat, int>> DescendingRandomSort(Dictionary<Chat, int> d)
        {
            List<KeyValuePair<Chat, int>> list = d.ToList();
            list.Sort((p1, p2) => CompareRandomizeTies(p1.Value, p2.Value));
            return list;
        }

        // sort descending with random ties
        static int CompareRandomizeTies(int i, int j)
        {
            return i == j ? (Util.Rand() < .5 ? 1 : -1) : j.CompareTo(i);
        }
    }
}
