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
        public static bool PRESERVE_LINE_NUMBERS = true;
        public static string CHAT_FILE_EXT = ".gs";

        private static string[] LineBreaks = { "\r\n", "\r", "\n" };

        //protected static List<Func<Command, bool>> validators;
        protected Stack<Command> parsedCommands;
        protected List<Chat> chats;

        public ChatParser()
        {
            parsedCommands = new Stack<Command>();
            chats = new List<Chat>();
        }

        //public static void AddValidator(Func<Command, bool> validator)
        //{
        //    if (validators == null) validators = new List<Func<Command, bool>>();
        //    validators.Add(validator);
        //}

        public static Grammar ParseGrammar(FileInfo file)
        {
            return new Grammar(file);
        }

        public static Grammar ParseGrammar(string src)
        {
            return new Grammar(src);
        }

        public static List<Chat> ParseFile(string fileOrFolder, params Func<Command, bool>[] validators)
        {
            string[] files = Directory.Exists(fileOrFolder) ?
                files = Directory.GetFiles(fileOrFolder, '*' + CHAT_FILE_EXT) :
                files = new string[] { fileOrFolder };

            List<Chat> chats = new List<Chat>();
            ParseFiles(files, chats, validators);

            return chats;
        }

        public static List<Chat> ParseText(string text, params Func<Command, bool>[] validators)
        {
            if (text == null) throw new ParseException("Null input");
            return Parse(StripComments(text), validators);
        }

        public static string[] StripComments(string text) // public->testing
        {
            if (PRESERVE_LINE_NUMBERS)  // slow two-pass
            {
                var lines = text.Split(LineBreaks, StringSplitOptions.None);
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

        private static void ParseFiles(string[] files, List<Chat> chats, params Func<Command, bool>[] validators)
        {
            foreach (var f in files) ParseFile(f, chats, validators);
        }

        private static List<Chat> Parse(string[] lines, params Func<Command, bool>[] validators)
        {
            ChatParser parser = new ChatParser();

            for (int i = 0; i < lines.Length; i++)
            {
                if (!String.IsNullOrEmpty(lines[i]))
                {
                    parser.ParseLine(lines[i], i + 1, validators);
                }
            }
            return parser.chats;
        }

        private Command ParseLine(string line, int lineNo, params Func<Command, bool>[] validators)
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

            Command c = ParseCommand(parts, line, lineNo);

            if (!Util.IsNullOrEmpty(validators))
            {
                foreach (var check in validators)
                {
                    try
                    {
                        if (!check(c)) throw new Exception("fail");
                    }
                    catch (Exception ex)
                    {
                        throw new ParseException
                            (line, lineNo, "Validator: " + ex.Message);
                    }
                }
            }

            return c;
        }

        private Command ParseCommand(List<string> parts, string line, int lineNo)
        {
            Command c = null;

            parts.Match((cmd, text, label, meta) =>
            {
                Type type = cmd.Length > 0 ? Command.TypeMap[cmd] : typeof(Say);

                try
                {
                    c = Command.Create(type, text, label, RE.MetaSplit.Split(meta));
                }
                catch (Exception ex)
                {
                    throw new ParseException(line, lineNo, ex.Message);
                }

            });

            return HandleCommand(c, line, lineNo);
        }

        private Command HandleCommand(Command c, string line, int lineNo)
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

                if (!(last is Ask)) throw new ParseException
                    (line, lineNo, "Opt must follow Ask");

                ((Ask)last).AddOption((Opt)c);
            }
            else  // add command to last Chat
            {
                chats.Last().AddCommand(c);
            }

            parsedCommands.Push(c);

            return c;
        }

        private static void ParseFile(string fname, List<Chat> chats, params Func<Command, bool>[] validators)
        {
            ParseText(File.ReadAllText(fname), validators).ForEach((f) => chats.Add(f));
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
