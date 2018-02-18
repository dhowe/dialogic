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
        public static Chat Find(List<Chat> chats, Dictionary<string, object> constraints)
        {
            return FindAll(chats, constraints).FirstOrDefault();
        }

        /** Find chat  by Name */
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
        public static List<Chat> FindAll(List<Chat> chats, Dictionary<string, object> constraints)
        {
            if (constraints == null) return chats;

            Dictionary<Chat, int> matches = new Dictionary<Chat, int>();

            for (int i = 0; i < chats.Count; i++)
            {
                Chat chat = chats[i];
                int hits = 0;
                foreach (var con in constraints)
                {
                    var conditions = chat.ToDict();
                    if (conditions != null && conditions.ContainsKey(con.Key))
                    {
                        if (conditions[con.Key] != con.Value)
                        {
                            hits = -1;
                            break;
                        }
                        hits++;
                    }
                }
                if (hits > -1) matches.Add(chat, hits);
            }

            List<KeyValuePair<Chat, int>> list = matches.ToList();
            list.Sort((p1, p2) => p2.Value.CompareTo(p1.Value));

            return (from kvp in list select kvp.Key).ToList();
        }
    }
}
