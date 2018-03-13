using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ExtensionMethods;

namespace Dialogic
{
    public class ChatParser
    {
        public static string CHAT_FILE_EXT = ".gs";
        public static bool PRESERVE_LINE_NUMBERS = false;

        protected Stack<Command> parsedCommands;
        protected List<Chat> chats;

        public ChatParser()
        {
            parsedCommands = new Stack<Command>();
            chats = new List<Chat>();
        }

        public static Grammar ParseGrammar(FileInfo file)
        {
            return new Grammar(file);
        }

        public static Grammar ParseGrammar(string src)
        {
            return new Grammar(src);
        }

        public static List<Chat> ParseFile(string fileOrFolder)
        {
            string[] files = Directory.Exists(fileOrFolder) ?
                files = Directory.GetFiles(fileOrFolder, '*' + CHAT_FILE_EXT) :
                files = new string[] { fileOrFolder };

            List<Chat> chats = new List<Chat>();
            ParseFiles(files, chats);

            return chats;
        }

        public static List<Chat> ParseText(string text)
        {
            if (text == null) throw new ParseException("Null input");
            return Parse(StripComments(text));
        }

        public static string[] StripComments(string text) // public->testing
        {
            if (PRESERVE_LINE_NUMBERS)  // slow two-pass
            {
                var lines = text.Split('\n');
                lines = Util.StripMultiLineComments(lines);
                return Util.StripSingleLineComments(lines);
            }
            else                       // faster one-pass
            {
                text = RE.MultiComment.Replace(text, String.Empty);
                text = RE.SingleComment.Replace(text, String.Empty);
                return text.Split('\n');
            }
        }

        private static void ParseFiles(string[] files, List<Chat> chats)
        {
            foreach (var f in files) ParseFile(f, chats);
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
            return true;
        }

        private Command ParseLine(string line, int lineNo)
        {
            //Console.WriteLine("LINE " + lineNo + ": " + line);

            Match match = RE.ParseLine.Match(line);

            if (match.Groups.Count < 5)
            {
                //Util.ShowMatch(match);
                throw new ParseException(line, lineNo, "cannot be parsed");
            }

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
                //Console.WriteLine("P: "+cmd+"' '" +label+ "' '" 
                // + text + "' " + Util.Stringify(meta));

                cmd = cmd.Length > 0 ? cmd : "SAY"; // default

                var pairs = RE.MetaSplit.Split(meta);

                try
                {
                    c = Command.Create(cmd, label, text, pairs);
                }
                catch (Exception ex)
                {
                    throw new ParseException(line, lineNo, ex.Message);
                }

            });

            return HandleCommand(c);
        }

        private Command HandleCommand(Command c) // cleanup
        {
            if (c is Chat)
            {
                chats.Add((Chat)c);
                return c;
            }

            if (chats.Count == 0) CreateDefaultChat();

            if (c is Opt) // add option data to last Ask
            {
                Command last = LastOfType(parsedCommands, typeof(Ask));
                if (!(last is Ask)) throw new ParseException("Opt must follow Ask");
                ((Ask)last).AddOption((Opt)c);
            }
            else  // add command to last Chat
            {
                chats.Last().AddCommand(c);
            }

            parsedCommands.Push(c);

            return c;
        }

        private static void ParseFile(string fname, List<Chat> chats)
        {
            ParseText(File.ReadAllText(fname)).ForEach((f) => chats.Add(f));
        }

        private static Command LastOfType(Stack<Command> s, Type typeToFind)
        {
            foreach (Command c in s)
            {
                if (c.GetType() == typeToFind) return c;
            }
            return null;
        }

        private void CreateDefaultChat()
        {
            Chat def = Chat.Create("C" + Util.EpochMs());
            chats.Add(def);
            parsedCommands.Push(def);
        }

    }

}
