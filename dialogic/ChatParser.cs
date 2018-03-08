using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ExtensionMethods;

namespace Dialogic
{
    public class ChatParser
    {
        public static string CHAT_FILE_EXT = ".gs";

        protected Stack<Command> parsed;
        protected List<Chat> chats;

        public ChatParser()
        {
            parsed = new Stack<Command>();
            chats = new List<Chat>();
        }

        public static Grammar ParseGrammar(FileInfo file)
        {
            return new Grammar(file);
        }

        public static Grammar ParseGrammar(string path)
        {
            return ParseGrammar(new FileInfo(path));
        }

        public static List<Chat> ParseFile(string fileOrFolder)
        {
            string[] files = !fileOrFolder.EndsWith(CHAT_FILE_EXT, Util.IC) ?
                files = Directory.GetFiles(fileOrFolder, '*' + CHAT_FILE_EXT) :
                files = new string[] { fileOrFolder };

            List<Chat> chats = new List<Chat>();
            ChatParser.ParseFiles(files, chats);

            return chats;
        }

        private static void ParseFiles(string[] files, List<Chat> chats)
        {
            foreach (var f in files) ParseFile(f, chats);
        }

        public static List<Chat> ParseText(string text)
        {
            return Parse(text.Split('\n'));
        }

        private static List<Chat> Parse(string[] lines)
        {
            ChatParser parser = new ChatParser();

            int i = 0;
            foreach (var line in lines)
            {
                if (ValidateLine(ref lines[i]))
                {
                    parser.ParseLine(lines[i], i + 1);
                }
                i++;
            }

            return parser.chats;
        }

        private static bool ValidateLine(ref string line)
        {
            if (line == null) return false;
            line = line.Trim();
            if (line.Length < 1) return false;
            if (line.StartsWith("//", Util.IC)) return false;
            return true;
        }

        private Command ParseLine(string line, int lineNo)
        {
            //Console.WriteLine("LINE " + lineNo + ": " + line);

            Match match = RE.ParseLine.Match(line);

            if (match.Length < 4) throw new ParseException(line, lineNo);

            var parts = new List<string>();
            for (int j = 0; j < 4; j++)
            {
                parts.Add(match.Groups[j + 1].Value.Trim());
            }

            return ParseCommand(parts, line, lineNo);
        }

        public Command ParseCommand(List<string> parts, string line, int lineNo)
        {
            Command c = null;
            parts.Match((cmd, text, label, meta) =>
            {
                //Console.WriteLine("P: "+cmd+"' '" +label+ "' '" + text + "' " + Util.Stringify(meta));

                cmd = cmd.Length > 0 ? cmd : "SAY"; // default command

                var pairs = RE.MetaSplit.Split(meta);

                try
                {
                    c = Command.Create(cmd, label, text, pairs);
                }
                catch (Exception ex)
                {
                    throw new ParseException(line, lineNo, ex.Message);
                }

                if (c is Chat)
                {
                    chats.Add((Chat)c);
                }
                else
                {
                    if (chats.Count == 0)
                    {
                        CreateDefaultChat();
                    }
                    HandleCommand(c);
                }

                parsed.Push(c);
            });

            return c;
        }

        private static void ParseFile(string fname, List<Chat> chats)
        {
            var commands = Parse(File.ReadAllLines(fname, Encoding.UTF8));
            commands.ForEach((f) => chats.Add(f));
        }

        private static Command LastOfType(Stack<Command> s, Type typeToFind)
        {
            foreach (Command c in s)
            {
                if (c.GetType() == typeToFind) return c;
            }
            return null;
        }

        private void HandleCommand(Command c) // cleanup
        {
            if (c is Opt) // add option data to last Ask
            {
                Command last = LastOfType(parsed, typeof(Ask));
                if (!(last is Ask)) throw new ParseException("Opt must follow Ask");
                ((Ask)last).AddOption((Opt)c);
            }
            else  // add command to last Chat
            {
                chats.Last().AddCommand(c);
            }
        }

        private void CreateDefaultChat()
        {
            Chat def = new Chat();
            chats.Add(def);
            parsed.Push(def);
        }

    }

}
