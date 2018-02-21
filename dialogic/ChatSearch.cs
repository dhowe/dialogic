using System;
using System.Collections.Generic;
using System.Linq;

namespace Dialogic
{
    public static class ChatSearch
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
         * Find chat by Name 
         */
        public static Chat ByName(List<Chat> chats, string chatName)
        {
            for (int i = 0; i < chats.Count; i++)
            {
                if (chats[i].Text == chatName)
                    return chats[i];
            }
            throw new ChatNotFound(chatName);
        }

        /**
         * Find all chats which do not match any constraint key without also matching its value,
         * ordered by score (number of matching constraints).
         * 
         * If none match, an empty list will be returned
         * Cases: 
         *   1. has key, matches ->         allow, score++
         *   2. has key, doesn't match ->   disallow
         *   3. doesn't have key ->         allow
         */
        public static List<Chat> FindAll(List<Chat> chats, IDictionary<string, object> constraints)
        {
            if (constraints == null) return chats;

            Dictionary<Chat, int> matches = new Dictionary<Chat, int>();

            for (int i = 0; i < chats.Count; i++)
            {
                int hits = 0;
                var chat = chats[i];
                var chatMeta = chat.Meta();

                foreach (var constraint in constraints.Keys)
                {
                    //Console.WriteLine("CHECK: find."+constraint+ " in "+chat.Text+" "+Util.Stringify(chatMeta));

                    if (chatMeta != null && chatMeta.ContainsKey(constraint))
                    {
                        var chatMetaVal = (string)chatMeta[constraint];

                        if (!((Constraint)constraints[constraint]).Check(chatMetaVal))
                        {
                            //Console.WriteLine("  FAIL:" + constraints[constraint]);
                            hits = -1;
                            break;
                        }
                        else 
                        {
                            hits++;
                            //Console.WriteLine("  HIT" + hits);
                        }
                    }
                }
                if (hits > -1) matches.Add(chat, hits);
            }

            List<KeyValuePair<Chat, int>> list = DescendingRandomSort(matches);

            //list.ForEach((kvp) => Console.WriteLine(kvp.Key + " -> " + kvp.Value));

            return (from kvp in list select kvp.Key).ToList();
        }

        public static Chat Find(List<Chat> chats, Constraints constraints)
        {
            return Find(chats, constraints.AsDict());
        }

        public static List<Chat> FindAll(List<Chat> chats, Constraints constraints)
        {
            return FindAll(chats, constraints.AsDict());
        }

        static List<KeyValuePair<Chat, int>> DescendingRandomSort(Dictionary<Chat, int> d)
        {
            List<KeyValuePair<Chat, int>> list = d.ToList();
            list.Sort((p1, p2) => CompareToWithRandomTies(p1.Value, p2.Value));
            return list;
        }

        // sort descending with random ties
        static int CompareToWithRandomTies(int i, int j)
        {
            return i == j ? (Util.Rand() < .5 ? 1 : -1) : j.CompareTo(i);
        }
    }
}
