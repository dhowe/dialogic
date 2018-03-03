using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ExtensionMethods;

namespace Dialogic
{
    public class ChatReader
    {
        public static string CHAT_FILE_EXT = ".gs";

        protected static Stack<Command> parsed;
        protected static List<Chat> chats;

        static ChatReader()
        {
            parsed = new Stack<Command>();
            chats = new List<Chat>();
        }

        public static List<Chat> ParseText(string text)
        {
            return Parse(text.Split('\n'));
        }

        internal static List<Chat> Parse(string[] lines, bool printTree = false)
        {
            int i = 0;
            foreach (var line in lines) ParseLine(lines[i], i++);
            return chats;
        }

        public static Command ParseLine(string line, int lineNo)
        {
            Match match = RE.ParseLine.Match(line);
            if (match.Length < 4) throw new ParseException(line, lineNo);
            var parts = new List<string>();
            for (int j = 0; j < 4; j++)
            {
                parts.Add(match.Groups[j + 1].Value.Trim());
            }
            return ParseCommand(parts);
        }


        public static Command ParseCommand(List<string> parts)
        {
            Command c = null;
            parts.Match((cmd, label, text, meta) =>
            {
                //Console.WriteLine("*COMMAND: '"+cmd+"'");
                cmd = cmd.Length > 0 ? cmd : "SAY";
                c = Command.Create(cmd, label, text, meta.Split(','));

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
            });

            return c;
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

        private static Command LastOfType(Stack<Command> s, Type typeToFind)
        {
            foreach (Command c in s)
            {
                if (c.GetType() == typeToFind) return c;
            }
            return null;
        }

        private static void HandleCommandTypes(Command c) // cleanup
        {
            if (c is Opt) // add option data to last Ask
            {
                Command last = LastOfType(parsed, typeof(Ask));
                if (!(last is Ask)) throw new Exception("Opt must follow Ask");
                ((Ask)last).AddOption((Opt)c);
            }
            else  // add command to last Chat
            {
                chats.Last().AddCommand(c);
            }
        }

        private static void CreateDefaultChat()
        {
            Chat def = new Chat();
            chats.Add(def);
            parsed.Push(def);
        }

    }
}
