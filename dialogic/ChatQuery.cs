using System;
using System.Collections.Generic;
using System.Linq;

namespace Dialogic
{
    public static class ChatQuery
    {
        /**
         * Find all chats which do not match any key without also matching its value
         * Cases: 
         *   1. has key, matches ->         allow, hits++
         *   2. has key, doesn't match ->   disallow
         *   3. doesn't have key ->         allow
         */
        public static List<Chat> FindAll(List<Chat> chats, Dictionary<string, string> dict)
        {
            Dictionary<Chat, int> matches = new Dictionary<Chat, int>();
            for (int i = 0; i < chats.Count; i++)
            {
                Chat chat = chats[i];
                int hits = 0;
                foreach (var cd in dict)
                {
                    if (chat.conditions.ContainsKey(cd.Key))
                    {
                        if (chat.conditions[cd.Key] != cd.Value)
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

        public static Chat Find(List<Chat> chats, Dictionary<string, string> dict)
        {
            return FindAll(chats, dict).First();
        }

        public static Chat FindChat(List<Chat> chats, string chatName)
        {
            for (int i = 0; i < chats.Count; i++)
            {
                if (chats[i].Text == chatName)
                    return chats[i];
            }
            throw new ChatNotFound(chatName);
        }

    }
}
