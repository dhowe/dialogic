using System;
using System.Collections.Generic;
using System.Linq;

namespace Dialogic
{
    public static class FuzzySearch
    {
        // NEXT:
        // we have Chat.staleness and Find.staleNess threshold
        // do we compare them as normal constraints in meta, or as special-varialbes 

        /// <summary>
        /// Finds the highest scoring chat which does not violate any of the constraints.
        /// If none match then start relaxing hard-type constraints until one does.
        /// If all hard-type constraints have been relaxed and nothing is found, 
        /// then unrelax constraints, lower the staleness threshold and repeat.
        /// Note that the Chat containing the Find object is never returned.
        /// </summary>
        /// <returns>Chat</returns>
        /// <param name="finder">Finder.</param>
        /// <param name="chats">Chats.</param>
        /// <param name="globals">Globals.</param>
        public static Chat Find(Find finder, List<Chat> chats, IDictionary<string, object> globals)
        {
            var dbug = true;

            Chat chat = FindAll(finder, chats, globals).FirstOrDefault();

            int tries = 0;
            List<string> relaxables = null;
            IDictionary<string, object> constraints = null;

            while (chat == null && ++tries < 100)
            {
                constraints = ExtractMeta(finder);

                if (relaxables == null) relaxables = new List<string>();

                Constraint staleness = null;
                foreach (var kv in constraints)
                {
                    Constraint c = (Constraint)kv.Value;
                    if (c.name == Meta.STALENESS)
                    {
                        staleness = c;
                    }
                    else if (c.IsRelaxable())
                    {
                        relaxables.Add(kv.Key);
                    }
                }

                if (dbug) Console.WriteLine("\nFailed with " + relaxables.Count
                    + " hard constraints, staleness = " + staleness.value);

                List<string> relaxed = null;

                if (relaxables.Count > 0)
                {
                    relaxed = new List<string>();

                    // try again after relaxing each hard constraint
                    while (chat == null && relaxables.Count > 0)
                    {
                        RelaxOne(constraints, relaxables, relaxed, dbug);
                        chat = FindAll(finder, chats, globals).FirstOrDefault();
                    }
                }

                // restore the state of constraints for reuse
                relaxed.ForEach(r => ((Constraint)constraints[r]).type = ConstraintType.Hard);

                // if nothing, relax the staleness constraint and retry
                if (chat == null)
                {
                    staleness.ScaleValue(.5);
                    if (dbug) Console.WriteLine("\nRelaxing staleness to " + staleness.value);
                }
            }

            if (dbug) Console.WriteLine("Result: " + chat);

            return chat;
        }

        /// <summary>
        /// Find all chats, ordered by score, which do not violate the specified constraints (no relaxation done here).
        /// Note that the Chat containing the Find object is never returned.
        /// </summary>
        /// <returns>List of chats ordered by score</returns>
        /// <param name="finder">Finder.</param>
        /// <param name="chats">Chats.</param>
        /// <param name="globals">Globals.</param>
        public static List<Chat> FindAll(Find finder, List<Chat> chats, IDictionary<string, object> globals)
        {
            var dbug = true;
            var constraints = ExtractMeta(finder);
            if (constraints == null) return chats;

            Dictionary<Chat, int> matches = new Dictionary<Chat, int>();

            for (int i = 0; i < chats.Count; i++)
            {
                // never return the source chat
                if (chats[i] == finder.parent) continue;

                var hits = 0;
                var chatMeta = chats[i].Realize(globals);

                if (dbug) Console.WriteLine("\n" + chats[i].Text + " ----------");

                foreach (var key in constraints.Keys)
                {
                    if (dbug) Console.WriteLine("  Find " + constraints[key] + " in " + chats[i]);

                    Constraint constraint = (Constraint)constraints[key];

                    if (chatMeta != null && chatMeta.ContainsKey(key)) // has-key
                    {
                        var chatPropVal = (string)chatMeta[key];

                        if (!(constraint.Check(chatPropVal, globals)))
                        {
                            if (dbug) Console.WriteLine("    FAIL: " + constraints[key]);
                            hits = -1;
                            break;
                        }
                        else
                        {
                            hits++;
                            if (dbug) Console.WriteLine("    HIT: " + hits);
                        }
                    }
                    else if (constraint.IsStrict()) // doesn't have-key, fails strict
                    {
                        if (dbug) Console.WriteLine("    !FAIL: " + constraints[key]);
                        hits = -1;
                        break;
                    }
                    else if (dbug) Console.WriteLine("    NOKEY");
                }
                if (hits > -1) matches.Add(chats[i], hits);
            }

            List<KeyValuePair<Chat, int>> list = DescendingRandomSort(matches);

            if (dbug) list.ForEach((kvp) => Console.WriteLine(kvp.Key + " -> " + kvp.Value));

            return (from kvp in list select kvp.Key).ToList();
        }

        private static void RelaxOne(IDictionary<string, object> constraints,
            List<string> relaxables, List<string> relaxed, bool dbug = false)
        {
            Constraint toRelax = (Constraint)constraints[Util.RandItem(relaxables)];
            relaxables.Remove(toRelax.name);

            if (dbug) Console.WriteLine("Relaxing {" + toRelax + "}, "
                + relaxables.Count + " hard constraints remaining");

            relaxed.Add(toRelax.name);
            toRelax.type = ConstraintType.Soft;
        }

        private static IDictionary<string, object> ExtractMeta(Find finder)
        {
            // note: this is problematic! -- perhaps should throw in case of no realized data
            return !finder.realized.IsNullOrEmpty() ? finder.realized : finder.meta;
        }

        /*
         * Sort by points, highest first, break ties with a coin-flip
         */
        private static List<KeyValuePair<Chat, int>> DescendingRandomSort(Dictionary<Chat, int> d)
        {
            List<KeyValuePair<Chat, int>> list = d.ToList();
            list.Sort((p1, p2) => CompareRandomizeTies(p1.Value, p2.Value));
            return list;
        }

        /*
         * Sort by points, highest first, break ties with the fresher chat
         */
        internal static List<KeyValuePair<Chat, int>> DescendingFreshnessSort(Dictionary<Chat, int> d)
        {
            // public for testing only
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
