using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dialogic
{
    public class ChatReader
    {
        public static string CHAT_FILE_EXT = ".gs";
        public static string COMMAND = @"(^[A-Z][A-Z][A-Z]?[A-Z]?[^A-Z])";

        protected static Stack<Command> parsed;

        public ChatReader()
        {
            parsed = new Stack<Command>();
        }


        public static List<Chat> ParseText(string text)
        {
            return Parse(text.Split('\n'));
        }

        internal static List<Chat> Parse(string[] lines, bool printTree = false)
        {
            string cmd = null;
            string[] args = null, meta = null;
            List<Chat> chats = new List<Chat>();

            Command c = Command.Create(cmd, args, meta);
            if (c is Chat)
            {
                chats.Add((Chat)c);
            }
            else
            {
                if (chats.Count == 0) CreateDefaultChat();
                HandleCommandTypes(c);
            }

            parsed.Push(c);

            return null;
        }

        private static void HandleDefaultCommand(string[] lines, string cmd)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                if (!string.IsNullOrEmpty(lines[i]))
                {
                    //System.Console.WriteLine($"Checking: '{lines[i]}'");
                    if (!Regex.IsMatch(lines[i], @"(^[A-Z][A-Z][A-Z]?[A-Z]?[^A-Z])"))
                    {
                        lines[i] = cmd + " " + lines[i];
                    }
                }
            }
        }

        private void CreateDefaultChat(List<Chat> chats)
        {
            Chat def = new Chat();
            chats.Add(def);
            parsed.Push(def);
        }

        private static void CreateDefaultChat()
        {
            throw new NotImplementedException();
        }

        private static void HandleCommandTypes(Command c)
        {
            throw new NotImplementedException();
        }

        private Command LastOfType(Stack<Command> s, Type typeToFind)
        {
            foreach (Command c in s)
            {
                if (c.GetType() == typeToFind) return c;
            }
            return null;
        }
    }
}
