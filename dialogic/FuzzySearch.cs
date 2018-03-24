using System;
using System.Collections.Generic;
using System.Linq;

namespace Dialogic
{
    public static class FuzzySearch
    {
        public static bool DBUG = false;

        class SearchContext  // NEXT, then => GO
        {
            /*
             * 1. Try normal search
             * 2. If failed, create a SearchContext(SC)
             * 3. If (relaxables) relaxEach until empty
             * 4. If failed, unrelax, relax staleness, & repeat
             */
        }

        /// <summary>
        /// Finds the highest scoring chat which does not violate any of the constraints.
        /// 
        /// If none match then start relaxing hard-type constraints until one does.
        /// 
        /// If all hard-type constraints have been relaxed and nothing is found, 
        /// then unrelax constraints, lower the staleness threshold and repeat.
        /// 
        /// Note that the Chat containing the Find object is never returned.
        /// </summary>
        /// <returns>Chat</returns>
        /// <param name="finder">Finder.</param>
        /// <param name="chats">Chats.</param>
        /// <param name="globals">Globals.</param>
        public static Chat Find(Find finder, List<Chat> chats, IDictionary<string, object> globals)
        {
            var constraints = finder.Realize(globals);

            if (DBUG) Console.WriteLine("FIND: " + finder.realized.Stringify());

            Chat chat = FindAll(finder, chats, globals).FirstOrDefault();
            if (chat != null) return chat;

            int tries = 0;
            List<string> relaxables = new List<string>();

            while (chat == null && ++tries < 100)
            {
                foreach (var kv in constraints)
                {
                    Constraint c = (Constraint)kv.Value;
                    if (c.IsRelaxable()) relaxables.Add(kv.Key);
                }

                if (DBUG) Console.WriteLine("\nFailed with " + relaxables.Count + " hard constraints");

                List<string> relaxed = null;

                if (relaxables.Count > 0)
                {
                    relaxed = new List<string>();

                    // try again after relaxing each hard constraint
                    while (chat == null && relaxables.Count > 0)
                    {
                        RelaxOne(constraints, relaxables, relaxed, DBUG);
                        chat = FindAll(finder, chats, globals).FirstOrDefault();
                    }
                }

                // restore the state of constraints for reuse
                if (!relaxed.IsNullOrEmpty())
                {
                    relaxed.ForEach(r => ((Constraint)constraints[r]).type = ConstraintType.Hard);
                }

                // if nothing, relax the staleness constraint and retry
                if (chat == null && constraints.ContainsKey(Meta.STALENESS))
                {
                    Constraint staleness = (Constraint)constraints[Meta.STALENESS];
                    staleness.IncrementValue(.5);
                    if (DBUG) Console.WriteLine("\nRelaxing staleness to " + staleness.value);
                    chat = FindAll(finder, chats, globals).FirstOrDefault();
                }
            }

            if (DBUG) Console.WriteLine("\nResult: " + chat);

            return chat;
        }


        internal static Chat DoFind(List<Chat> chats,
            IDictionary<Constraint, bool> constraints, Chat parent, IDictionary<string, object> globals)
        {
            bool resetRequired = false;
            Chat chat = DoFindAll(chats, constraints.Keys, parent, globals).FirstOrDefault();
            while (chat == null)
            {
                Constraint c = FindRelaxable(constraints);

                if (c != null)  // relax hard constraint and retry
                {
                    c.Relax();
                    resetRequired = true;
                    chat = DoFindAll(chats, constraints.Keys, parent, globals).FirstOrDefault();
                    if (chat == null)
                    {
                        if (DBUG) Console.WriteLine("\nFailed with " + 
                            RelaxableCount(constraints) + " hard constraints");
                        continue;
                    }
                }
                else    // no more constraints, reset & try staleness
                {
                    if (chat == null)
                    {
                        if (resetRequired)
                        {
                            ResetConstraints(constraints);
                            resetRequired = false;
                        }
                        Constraint staleness = GetConstraint(constraints, Meta.STALENESS);
                        if (!staleness.IncrementValue(.5)) return null;
                        if (DBUG) Console.WriteLine("\nRelaxing staleness to " + staleness.value);
                        chat = DoFindAll(chats, constraints.Keys, parent, globals).FirstOrDefault();
                    }
                }
            }
            return chat;
        }

        private static int RelaxableCount(IDictionary<Constraint, bool> constraints)
        {
            int count = 0;
            foreach (var constraint in constraints.Keys)
            {
                if (constraints[constraint]) count++;
            }
            return count;
        }

        private static Constraint GetConstraint(IDictionary<Constraint, bool> constraints, string key)
        {
            foreach (var constraint in constraints.Keys)
            {
                if (constraint.name == key) return constraint;
            }
            return null;
        }

        private static Constraint FindRelaxable(IDictionary<Constraint, bool> constraints)
        {
            foreach (var constraint in constraints.Keys)
            {
                if (constraints[constraint])
                {
                    constraints[constraint] = false;
                    return constraint;
                }
            }
            return null;
        }

        private static void ResetConstraints(IDictionary<Constraint, bool> constraints)
        {
            for (int i = 0; i < constraints.Keys.Count; i++)
            {
                Constraint c = constraints.Keys.ElementAt(i);
                constraints[c] = c.IsRelaxable();
            }
        }

        internal static List<Chat> DoFindAll(List<Chat> chats,
            IEnumerable<Constraint> constraints, Chat parent, IDictionary<string, object> globals)
        {
            Dictionary<Chat, int> matches = new Dictionary<Chat, int>();

            if (DBUG) Console.WriteLine("\nFIND: " + constraints.Stringify());

            for (int i = 0; i < chats.Count; i++)
            {
                // never return the source chat
                if (chats[i] == parent) continue;

                var hits = 0;
                var chatMeta = chats[i].realized;

                if (DBUG) Console.WriteLine("\n" + chats[i].Text + " ----------");

                foreach (var constraint in constraints)
                {
                    string key = constraint.name;

                    if (DBUG) Console.WriteLine("  Find " + constraint + " in " + chats[i]);

                    if (chatMeta != null && chatMeta.ContainsKey(key)) // has-key
                    {
                        var chatPropVal = (string)chatMeta[key];

                        if (!(constraint.Check(chatPropVal, globals)))
                        {
                            if (DBUG) Console.WriteLine("    FAIL: " + constraint);
                            hits = -1;
                            break;
                        }
                        else
                        {
                            hits++;
                            if (DBUG) Console.WriteLine("    HIT: " + hits);
                        }
                    }
                    else if (constraint.IsStrict()) // doesn't have-key, fails strict
                    {
                        if (DBUG) Console.WriteLine("    !FAIL: " + constraint);
                        hits = -1;
                        break;
                    }
                    else if (DBUG)
                    {
                        Console.WriteLine("    NOKEY");
                    }
                }
                if (hits > -1) matches.Add(chats[i], hits);
            }

            List<KeyValuePair<Chat, int>> list = DescendingRandomSort(matches);

            if (DBUG) list.ForEach((kvp) => Console.Write("\n" + kvp.Key + " -> " + kvp.Value));

            return (from kvp in list select kvp.Key).ToList();
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
            var constraints = finder.Realize(globals);
            if (constraints == null) return chats;

            Dictionary<Chat, int> matches = new Dictionary<Chat, int>();

            for (int i = 0; i < chats.Count; i++)
            {
                // never return the source chat
                if (chats[i] == finder.parent) continue;

                var hits = 0;
                var chatMeta = chats[i].realized;

                if (DBUG) Console.WriteLine("\n" + chats[i].Text + " ----------");

                foreach (var key in constraints.Keys)
                {
                    if (DBUG) Console.WriteLine("  Find " + constraints[key] + " in " + chats[i]);

                    Constraint constraint = (Constraint)constraints[key];

                    if (chatMeta != null && chatMeta.ContainsKey(key)) // has-key
                    {
                        var chatPropVal = (string)chatMeta[key];

                        if (!(constraint.Check(chatPropVal, globals)))
                        {
                            if (DBUG) Console.WriteLine("    FAIL: " + constraints[key]);
                            hits = -1;
                            break;
                        }
                        else
                        {
                            hits++;
                            if (DBUG) Console.WriteLine("    HIT: " + hits);
                        }
                    }
                    else if (constraint.IsStrict()) // doesn't have-key, fails strict
                    {
                        if (DBUG) Console.WriteLine("    !FAIL: " + constraints[key]);
                        hits = -1;
                        break;
                    }
                    else if (DBUG)
                    {
                        Console.WriteLine("    NOKEY");
                    }
                }
                if (hits > -1) matches.Add(chats[i], hits);
            }

            List<KeyValuePair<Chat, int>> list = DescendingRandomSort(matches);

            if (DBUG) list.ForEach((kvp) => Console.Write("\n" + kvp.Key + " -> " + kvp.Value));

            return (from kvp in list select kvp.Key).ToList();
        }

        private static void RelaxOne(IDictionary<string, object> constraints,
            List<string> relaxables, List<string> relaxed, bool DBUG = false)
        {
            Constraint toRelax = (Constraint)constraints[Util.RandItem(relaxables)];
            relaxables.Remove(toRelax.name);

            if (DBUG) Console.WriteLine("Relaxing {" + toRelax + "}, "
                + relaxables.Count + " hard constraints remaining");

            relaxed.Add(toRelax.name);
            toRelax.type = ConstraintType.Soft;
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
