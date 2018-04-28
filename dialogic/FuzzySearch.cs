using System;
using System.Collections.Generic;
using System.Linq;

namespace Dialogic
{
    /// <summary>
    /// Handles fuzzy logic for Find commands containing one or more constraints to be matched in candidate Chats.
    /// </summary>
    public static class FuzzySearch
    {
        public static bool DBUG = false;

        /// <summary>
        /// Finds the highest scoring chat which does not violate any of the constraints.
        ///
        /// If none match, then start relaxing hard-type constraints until one does.
        /// 
        /// If all hard constraints have been relaxed and nothing is found, then 
        /// unrelax hard constraints, lower the staleness threshold, and repeat.
        /// 
        /// Break ties based on milliseconds since the Chat was last run. If there
        /// are still ties, break them with coin-flip.
        /// 
        /// Note that the Chat containing the Find object is never returned.
        /// </summary>
        /// <returns>Chat</returns>
        /// <param name="chats">Chats.</param>
        /// <param name="constraints">Constraints.</param>
        /// <param name="parent">Parent.</param>
        /// <param name="globals">Globals.</param>
        public static Chat Find(List<Chat> chats,
            IDictionary<Constraint, bool> constraints,
            Chat parent, IDictionary<string, object> globals)
        {
            if (constraints.IsNullOrEmpty()) throw new FindException
                ("FuzzySearch.Find() called without constraints");

            var resetRequired = false; // opt
            var clist = constraints.Keys.ToList();

            Chat chat = FindAll(chats, clist, parent, globals).FirstOrDefault();

            while (chat == null)
            {
                Constraint c = FindRelaxable(constraints);

                if (c != null)  // relax hard constraint and retry
                {
                    c.Relax();
                    resetRequired = true;
                    chat = FindAll(chats, clist, parent, globals).FirstOrDefault();

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
                            resetRequired = false;
                            ResetConstraints(constraints);
                        }

                        Constraint staleness = GetConstraint(constraints, Meta.STALENESS);

                        if (!staleness.IncrementValue(Defaults.FIND_RELAXATION_INCR))
                        {
                            return null;  // give up here if we've hit maximum staleness
                        }

                        if (DBUG) Console.WriteLine("\nRelaxing staleness to " + staleness.value);

                        chat = FindAll(chats, clist, parent, globals).FirstOrDefault();
                    }
                }
            }

            return chat;
        }

        /// <summary>
        /// Find all chats, ordered by score, which do not violate the specified
        /// constraints (no relaxation done here).
        /// 
        /// Note that the Chat containing the Find object is never returned.
        /// 
        /// </summary>
        /// <returns>List of chats ordered by score</returns>
        /// <param name="chats">Chats.</param>
        /// <param name="constraints">Constraints.</param>
        /// <param name="parent">Parent.</param>
        /// <param name="globals">Globals.</param>
        internal static List<Chat> FindAll(List<Chat> chats,
            List<Constraint> constraints,
            Chat parent, IDictionary<string, object> globals)
        {
            Dictionary<Chat, double> matches = new Dictionary<Chat, double>();

            if (DBUG) Console.WriteLine("\nFIND: " + constraints.Stringify());

            ValidateConstraints(constraints);

            for (int i = 0; i < chats.Count; i++)
            {
                // never return the source chat
                if (chats[i] == parent) continue;

                var hits = 0;
                var chatMeta = chats[i].meta;

                if (DBUG) Console.WriteLine("\n" + chats[i].text + " ----------");

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

                if (hits > -1) matches.Add(chats[i], ComputeScore(constraints, hits));
            }

            List<KeyValuePair<Chat, double>> list = DescendingStalenessRandomizedSort(matches);

            if (DBUG) list.ForEach((kvp) => Console.Write("\n" + kvp.Key + " -> " + kvp.Value));

            return (from kvp in list select kvp.Key).ToList();
        }

        private static void ValidateConstraints(List<Constraint> constraints) // TODO: tmp
        {
            bool hasStaleness = false;
            foreach (var constraint in constraints)
            {
                hasStaleness |= constraint.name == Meta.STALENESS;
            }

            if (!hasStaleness)
            {
                constraints.Add(new Constraint(Operator.LT, Meta.STALENESS,
                    Defaults.FIND_STALENESS.ToString()));
            }
        }

        private static double ComputeScore(IEnumerable<Constraint> constraints, int hits)
        {
            return Defaults.FIND_NORMALIZE_SCORES ? Normalize(constraints, hits) : hits;
        }

        private static double Normalize(IEnumerable<Constraint> constraints, int hits)
        {
            return hits / (double)constraints.Count();
        }

        // --------------------------------------------------------------------

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
                // never relax staleness
                if (constraint.name == Meta.STALENESS) continue;

                // find a constraint that has not been relaxed
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

        // --------------  various sorts --------------------------------------

        /// <summary>
        /// Sort by points, highest first, break ties with a coin-flip
        /// </summary>
        internal static List<KeyValuePair<Chat, double>> 
            DescendingRandomizedSort(Dictionary<Chat, double> d)
        {
            List<KeyValuePair<Chat, double>> list = d.ToList();
            list.Sort((p1, p2) => CompareWithRandomizedTies(p1.Value, p2.Value));
            return list;
        }

        /// <summary>
        /// Sort by points, highest first, break ties with Chat.Staleness(), then coin-flip
        /// </summary>
        internal static List<KeyValuePair<Chat, double>> 
            DescendingStalenessRandomizedSort(Dictionary<Chat, double> d)
        {
            List<KeyValuePair<Chat, double>> list = d.ToList();
            list.Sort(CompareStalenessTies);
            return list;
        }

        /// <summary>
        /// Sort by points, highest first, break ties with the lastRunAt time, then coin-flip
        /// </summary>
        internal static List<KeyValuePair<Chat, double>> 
            DescendingScoreLastRunAtRandomizedSort(Dictionary<Chat, double> d)
        {
            List<KeyValuePair<Chat, double>> list = d.ToList();
            list.Sort(CompareLastRunAtTies);
            return list;
        }

        /// <summary>
        /// Sort descending based on score with ties decided by lastRunAt, then a coin-flip
        /// </summary>
        internal static int CompareLastRunAtTies
            (KeyValuePair<Chat, double> i, KeyValuePair<Chat, double> j)
        {
            if (Util.FloatingEquals(i.Value, j.Value)) // tie on score
            {
                // check staleness and randomize ties
                return CompareWithRandomizedTies(i.Key.lastRunAt, j.Key.lastRunAt);
            }
            return j.Value.CompareTo(i.Value);
        }

        /// <summary>
        /// Sort descending based on score with ties decided by lastRunAt, then a coin-flip
        /// </summary>
        internal static int CompareStalenessTies
            (KeyValuePair<Chat, double> i, KeyValuePair<Chat, double> j)
        {
            if (Util.FloatingEquals(i.Value, j.Value)) // tie on score
            {
                // check staleness and randomize ties
                return CompareWithRandomizedTies
                    (i.Key.Staleness(), j.Key.Staleness());
            }
            return j.Value.CompareTo(i.Value);
        }

        /// <summary>
        /// Sort int with ties decided by coin-flip
        /// </summary> 
        internal static int CompareWithRandomizedTies(int i, int j)
        {
            return i == j ? (Util.Rand() < .5 ? 1 : -1) : i.CompareTo(j);
        }

        /// <summary>
        /// Sort int with ties decided by coin-flip
        /// </summary> 
        internal static int CompareWithRandomizedTies(double i, double j)
        {
            return Util.FloatingEquals(i, j) ? 
                (Util.Rand() < .5 ? 1 : -1) : i.CompareTo(j);
        }
    }
}
