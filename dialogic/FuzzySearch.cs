using System;
using System.Collections.Generic;
using System.Linq;

namespace Dialogic
{
    public static class FuzzySearch
    {
        /**
         * Find highest scoring chat which does not match any of the constraint.
         * If none match then start relaxing hard-type constraints until one does.
         * If all hard-type constraints have been relaxed and nothing is found, then return null;
         */
        public static Chat Find(List<Chat> chats, IDictionary<string, object> constraints, 
            IDictionary<string, object> globals = null)
        {
            var dbug = false;
            var chat = FindAll(chats, constraints, globals).FirstOrDefault();

            if (chat == null)
            {
                List<string> relaxables = new List<string>();
                //var types = new Dictionary<string, ConstraintType>();
                foreach (var kv in constraints)
                {
                    Constraint c = (Constraint)kv.Value;
                    if (c.IsRelaxable()) relaxables.Add(kv.Key);
                }

                if(dbug)Console.WriteLine("\nFailed with " + relaxables.Count + " hard constraints");
                if (relaxables.Count == 0) return null;

                // try again after relaxing each hard constraint
                List<string> relaxed = new List<string>();
                while (relaxables.Count > 0 && chat == null)
                {
                    Constraint toRelax = (Constraint)constraints[Util.RandItem(relaxables)];
                    relaxables.Remove(toRelax.name);
                    if (dbug)Console.WriteLine("Relaxing {"+toRelax+"} "+relaxables.Count+" hard constraints remaining");
                    relaxed.Add(toRelax.name);
                    toRelax.type = ConstraintType.Soft;
                    chat = FindAll(chats, constraints, globals).FirstOrDefault();
                    if (dbug && chat != null) Console.WriteLine("Found: "+chat);
                }

                // restore the state of constraints for reuse
                relaxed.ForEach(r => ((Constraint)constraints[r]).type = ConstraintType.Hard);
            }

            return chat;
        }

        /**
         * Find all chats according to specified constraints ordered by score.
         * 
         * If none match, an empty list will be returned
         * Cases: 
         *   1.  has key, matches ->             allow, score++
         *   2.  has key, doesn't match ->       disallow
         *   3a. doesn't have key (normal) ->    allow
         *    b. doesn't have key (strict) ->    disallow
         */
        public static List<Chat> FindAll(List<Chat> chats, IDictionary<string, object> constraints, IDictionary<string, object> globals = null)
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

                        // TODO: need to realize constraint & chat values here

                        if (!(constraint.Check(chatPropVal, globals)))
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

        /*
         * Sort by points, highest first, break ties with a coin-flip
         */
        public static List<KeyValuePair<Chat, int>> DescendingRandomSort(Dictionary<Chat, int> d)
        {
            List<KeyValuePair<Chat, int>> list = d.ToList();
            list.Sort((p1, p2) => CompareRandomizeTies(p1.Value, p2.Value));
            return list;
        }

        /*
         * Sort by points, highest first, break ties with the fresher chat
         */
        public static List<KeyValuePair<Chat, int>> DescendingFreshnessSort(Dictionary<Chat, int> d)
        {
            List<KeyValuePair<Chat, int>> list = d.ToList();
            list.Sort((p1, p2) => CompareFreshnessTies(p1, p2));
            return list;
        }

        // sort descending with ties based on freshness
        private static int CompareFreshnessTies(KeyValuePair<Chat, int> i, KeyValuePair<Chat, int> j)
        {
            return i.Value == j.Value ? (i.Key.lastRunAt > j.Key.lastRunAt
                ? 1 : -1) : j.Value.CompareTo(i.Value);
        }

        // sort descending with randomized ties
        private static int CompareRandomizeTies(int i, int j)
        {
            return i == j ? (Util.Rand() < .5 ? 1 : -1) : j.CompareTo(i);
        }
    }
}
