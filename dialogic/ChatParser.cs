using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dialogic
{
    public class ChatParser
    {
        public static bool PRESERVE_LINE_NUMBERS = true;
  
        const string TXT = @"([^#}{]+)?\s*";
        const string LBL = @"(#[A-Za-z][\S]*)?\s*";
        const string MTA = @"(?:\{(.+?)\})?\s*";

        static Regex MultiComment = new Regex(@"/\*[^*]*\*+(?:[^/*][^*]*\*+)*/");
        static Regex SingleComment = new Regex(@"//(.*?)(?:$|\r?\n)");
        static string[] LineBreaks = { "\r\n", "\r", "\n" };

        protected Stack<Command> parsedCommands;
        protected Func<Command, bool>[] validators;
        protected internal List<Chat> chats;
        protected Regex LineParser;

        public ChatParser(params Func<Command, bool>[] commandValidators)
        {
            LineParser = new Regex(TypesRegex() + TXT + LBL + MTA);
            parsedCommands = new Stack<Command>();
            chats = new List<Chat>();
            validators = commandValidators;
        }

        public static List<Chat> ParseText(string text, params Func<Command, bool>[] validators)
        {
            var lines = text.Split(LineBreaks, StringSplitOptions.None);
            return new ChatParser(validators).Parse(lines);
        }

        internal List<Chat> Parse(string[] lines)
        {
            lines = Util.StripMultiLineComments(lines);
            lines = Util.StripSingleLineComments(lines);

            for (int i = 0; i < lines.Length; i++)
            {
                if (!String.IsNullOrEmpty(lines[i]))
                {
                    ParseLine(lines[i], i + 1);
                }
            }
            return chats;
        }
            
        public static Grammar ParseGrammar(FileInfo file) // TODO:
        {
            return new Grammar(file);
        }

        public static Grammar ParseGrammar(string src) // TODO:
        {
            return new Grammar(src);
        }

        private static string TypesRegex()
        {
            string s = @"(";
            var cmds = ChatRuntime.TypeMap.Keys;
            for (int i = 0; i < cmds.Count; i++)
            {
                s += cmds.ElementAt(i);
                if (i < cmds.Count - 1) s += "|";
            }
            return s + @")?\s*";
        }

        internal Command ParseLine(string line, int lineNo)
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
                Type type = cmd.Length > 0 ? ChatRuntime.TypeMap[cmd] : typeof(Say);

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

        /// @cond TestOnly
        internal static string[] StripComments(string text)
        {
            if (text == null) throw new ParseException("Null input");

            if (PRESERVE_LINE_NUMBERS)  // slow two-pass
            {
                var lines = text.Split(LineBreaks, StringSplitOptions.None);
                lines = Util.StripMultiLineComments(lines);
                return Util.StripSingleLineComments(lines);
            }
            else                       // faster one-pass
            {
                text = MultiComment.Replace(text, String.Empty);
                text = SingleComment.Replace(text, String.Empty);
                return text.Split('\n');
            }
        }
        /// @endcond

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
