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

        public static IDictionary<string, Type> TypeMap
            = new Dictionary<string, Type>()
        {
                    { "CHAT",   typeof(Dialogic.Chat) },
                    { "SAY",    typeof(Dialogic.Say)  },
                    { "SET",    typeof(Dialogic.Set)  },
                    { "ASK",    typeof(Dialogic.Ask)  },
                    { "OPT",    typeof(Dialogic.Opt)  },
                    { "DO",     typeof(Dialogic.Do)   },
                    { "GO",     typeof(Dialogic.Go)   },
                    { "WAIT",   typeof(Dialogic.Wait) },
                    { "FIND",   typeof(Dialogic.Find) },
                    { "GRAM",   typeof(Dialogic.Gram) },
        };

        private const string TXT = @"([^#}{]+)?\s*";
        private const string LBL = @"(#[A-Za-z][\S]*)?\s*";
        private const string MTA = @"(?:\{(.+?)\})?\s*";

        internal static string TypesRegex()
        {
            string s = @"(";
            var cmds = TypeMap.Keys;
            for (int i = 0; i < cmds.Count; i++)
            {
                s += cmds.ElementAt(i);
                if (i < cmds.Count - 1) s += "|";
            }
            return s + @")?\s*";
        }

        private static string[] LineBreaks = { "\r\n", "\r", "\n" };

        protected Stack<Command> parsedCommands;
        protected Func<Command, bool>[] validators;
        protected List<Chat> chats;
        protected Regex LineParser;


        public ChatParser(params Func<Command, bool>[] validator)
        {
            LineParser = new Regex(TypesRegex() + TXT + LBL + MTA);
            parsedCommands = new Stack<Command>();
            chats = new List<Chat>();
            validators = validator;
        }

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
            ChatParser parser = new ChatParser(validators);

            for (int i = 0; i < lines.Length; i++)
            {
                if (!String.IsNullOrEmpty(lines[i]))
                {
                    parser.ParseLine(lines[i], i + 1);
                }
            }
            return parser.chats;
        }

        private Command ParseLine(string line, int lineNo)
        {
            //Console.WriteLine("LINE " + lineNo + ": " + line);

            Match match = LineParser.Match(line);

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
                Type type = cmd.Length > 0 ? TypeMap[cmd] : typeof(Say);

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
