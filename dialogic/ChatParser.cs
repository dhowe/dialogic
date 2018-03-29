using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dialogic
{
    /// <summary>
    /// Handles parsing of scripts via a ChatRuntime instance - no public API.
    /// </summary>
    public class ChatParser
    {
        /// <summary>
        /// Whether the parser should preserve incoming line numbers for debugging;
        /// default is true though potentially faster if false.
        /// </summary>
        public static bool PRESERVE_LINE_NUMBERS = true;

        const string TXT = @"([^#}{]+)?\s*";
        const string LBL = @"(#[A-Za-z][\S]*)?\s*";
        const string MTD = @"(?:\{(.+?)\})?\s*";
        const string ACTR = @"(?:([A-Za-z_][A-Za-z0-9_-]+):)?\s*";

        static Regex MultiComment = new Regex(@"/\*[^*]*\*+(?:[^/*][^*]*\*+)*/");
        static Regex SingleComment = new Regex(@"//(.*?)(?:$|\r?\n)");
        internal static string[] LineBreaks = { "\r\n", "\r", "\n" };

        protected ChatRuntime runtime;
        protected Stack<Command> parsedCommands;
        protected internal List<Chat> chats;
        protected Regex LineParser;

        internal ChatParser(ChatRuntime runtime)
        {
            this.LineParser = new Regex(ACTR + TypesRegex() + TXT + LBL + MTD);
            this.parsedCommands = new Stack<Command>();
            this.chats = new List<Chat>();
            this.runtime = runtime;
        }

        internal static List<Chat> ParseText(string s, bool noValidators = false)
        {
            ChatRuntime rt = new ChatRuntime(Tendar.AppConfig.Actors);
            return rt.ParseText(s, noValidators);
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

        internal static Grammar ParseGrammar(string src) // TODO:
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
            List<string> parts = DoSubDivision(line, lineNo);

            Command c = ParseCommand(parts, line, lineNo);

            //Console.WriteLine("LINE " + lineNo + ": " + line+" => "+c);

            // Run after Create/Init are called
            RunValidators(c, line, lineNo);

            return c;
        }

        private List<string> DoSubDivision(string line, int lineNo)
        {
            Match match = LineParser.Match(line);

            if (match.Groups.Count < 6)
            {
                //Util.ShowMatch(match);
                throw new ParseException(line, lineNo, "cannot be parsed");
            }

            var parts = new List<string>();
            for (int j = 1; j < 6; j++)
            {
                parts.Add(match.Groups[j].Value.Trim());
            }

            return parts;
        }

        private void RunValidators(Command c, string line, int lineNo)
        {
            if (runtime == null || runtime.validatorsDisabled) return;

            var validators = runtime.Validators();
            
            if (!validators.IsNullOrEmpty())
            {
                foreach (var check in validators)
                {
                    try
                    {
                        if (!check.Invoke(c)) throw new Exception("fail");
                    }
                    catch (Exception ex)
                    {
                        throw new ParseException
                            (line, lineNo, "Validator: " + ex.Message);
                    }
                }
            }
        }

        private Command ParseCommand(List<string> parts, string line, int lineNo)
        {
            Command c = null;

            parts.Apply((spkr, cmd, text, label, meta) =>
            {
                Type type = cmd.Length > 0 ? ChatRuntime.TypeMap[cmd] : typeof(Say);

                try
                {
                    //Console.WriteLine("META:'"+meta+"'");
                    c = Command.Create(type, text, label, RE.MetaSplit.Split(meta));
                }
                catch (Exception ex)
                {
                    throw new ParseException(line, lineNo, ex.Message);
                }

                HandleActor(spkr, c, line, lineNo);
            });

            return HandleCommand(c, line, lineNo);
        }

        private void HandleActor(string spkr, Command c, string line, int lineNo)
        {
            c.actor = Actor.Default;
            if (!string.IsNullOrEmpty(spkr) && runtime != null)
            {
                c.actor = runtime.FindActor(spkr);
                if (c.actor == null)
                {
                    throw new ParseException(line, lineNo, "Unknown actor: '" + spkr + "'");
                }
                if (!Equals(c.actor, Actor.Default)) c.SetMeta(Meta.ACTOR, c.actor.Name());
            }
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
