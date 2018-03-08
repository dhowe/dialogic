using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ExtensionMethods;

namespace Dialogic
{
    public class ChatReader
    {
        public static string CHAT_FILE_EXT = ".gs";

        protected Stack<Command> parsed;
        protected List<Chat> chats;

        public ChatReader()
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
            ChatReader.ParseFiles(files, chats);

            return chats;
        }

        internal static void ParseFiles(string[] files, List<Chat> chats)
        {
            foreach (var f in files) ParseFile(f, chats);
        }

        public static List<Chat> ParseText(string text)
        {
            return Parse(text.Split('\n'));
        }

        internal static List<Chat> Parse(string[] lines, bool printTree = false)
        {
            ChatReader parser = new ChatReader();

            int i = 0;
            foreach (var line in lines)
            {
                if (line != null && line.Trim().Length > 1)
                {
                    parser.ParseLine(lines[i], i+1);
                }
                i++;
            }

            return parser.chats;
        }

        internal static void ParseFile(string fname, List<Chat> chats)
        {
            var commands = Parse(File.ReadAllLines(fname, Encoding.UTF8));
            commands.ForEach((f) => chats.Add(f));
        }

        public Command ParseLine(string line, int lineNo)
        {
            //Console.WriteLine("LINE " + lineNo + ": " + line);
            if (String.IsNullOrEmpty(line)) return null;

            Match match = RE.ParseLine.Match(line);

            if (match.Length < 4) throw new ParseException(line, lineNo);

            var parts = new List<string>();
            for (int j = 0; j < 4; j++)
            {
                //Console.WriteLine((j+1)+": "+match.Groups[j + 1].Value.Trim());
                parts.Add(match.Groups[j + 1].Value.Trim());
            }

            return ParseCommand(parts);
        }

        public Command ParseCommand(List<string> parts)
        {
            Command c = null;
            parts.Match((cmd, text, label, meta) =>
            {
                //Console.WriteLine("P: "+cmd+"' '" +label+ "' '" + text + "' " + Util.Stringify(meta));
                cmd = cmd.Length > 0 ? cmd : "SAY";
                var pairs = RE.MetaSplit.Split(meta);
                c = Command.Create(cmd, label, text, pairs);

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

        private static Command LastOfType(Stack<Command> s, Type typeToFind)
        {
            foreach (Command c in s)
            {
                if (c.GetType() == typeToFind) return c;
            }
            return null;
        }

        private void HandleCommandTypes(Command c) // cleanup
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
