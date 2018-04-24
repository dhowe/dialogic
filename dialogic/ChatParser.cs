using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Dialogic
{
    /// <summary>
    /// Handles parsing of scripts via a ChatRuntime instance - no public API
    /// </summary>
    public class ChatParser
    {
        /// <summary>
        /// Whether the parser should preserve incoming line numbers for debugging;
        /// default is true though potentially faster if false.
        /// </summary>
        public static bool PRESERVE_LINE_NUMBERS = true;

        internal static string[] LineBreaks = { "\r\n", "\r", "\n" };

        private static Regex lineParser; // static??

        protected ChatRuntime runtime;
        protected Stack<Command> parsedCommands;

        internal ChatParser(ChatRuntime runtime)
        {
            this.parsedCommands = new Stack<Command>();
            this.runtime = runtime;
        }

        internal static Regex LineParser()
        {
            if (lineParser == null)
            {
                lineParser = new Regex(RE.ACT + TypesRegex() + RE.TXT + RE.LBL + RE.MTD);
            }
            return lineParser;
        }

        internal static List<Chat> ParseText(string s, bool noValidators = false)
        {
            ChatRuntime rt = new ChatRuntime(Tendar.AppConfig.Actors); // tmp: testing
            rt.ParseText(s, noValidators);
            return rt.Chats();
        }

        internal void Parse(string[] lines)
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
        }

        internal static string TypesRegex()
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
            Command c = null;

            try
            {
                c = CreateCommand(new LineContext(line, lineNo));
            }
            catch (Exception ex)
            //catch (ParseException ex)
            {
                throw new ParseException(line, lineNo, ex.Message);
            }

            return c;
        }

        private void RunExternalValidators(Command c)
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
                        throw new Exception("Validator: " + ex.Message);
                    }
                }
            }
        }

        private Chat ActiveChat()
        {
            // Note: can be null if this is the first command
            return ((Chat)LastOfType(parsedCommands, typeof(Chat)));
        }

        internal Command CreateCommand(LineContext lc)
        {
            Type type = lc.command.Length > 0 ? ChatRuntime.TypeMap[lc.command]
                    : Chat.DefaultCommandType(ActiveChat());
            
            Command c = Command.Create(type, lc.text, lc.label, SplitMeta(lc.meta));
            c.lineContext = lc;

            HandleActor(lc.actor, c, lc.line, lc.lineNo);
            HandleCommand(c, lc.line, lc.lineNo);
            RunValidators(c);

            return c;
        }

        private string[] SplitMeta(string meta)
        {
            return meta.IsNullOrEmpty() ? null : RE.MetaSplit.Split(meta);
        }

        private void HandleActor(string spkr, Command c, string line, int lineNo)
        {
            c.SetActor(Actor.Default);

            if (!string.IsNullOrEmpty(spkr) && runtime != null)
            {
                c.SetActor(runtime, spkr);

                if (c.actor == null) throw new ParseException
                    (line, lineNo, "Unknown actor: '" + spkr + "'");

                if (!Equals(c.actor, Actor.Default)) c.SetMeta(Meta.ACTOR, c.actor.Name());
            }
        }

        private void HandleCommand(Command c, string line, int lineNo)
        {
            parsedCommands.Push(c);

            if (c is Chat) // add chat to runtime list
            {
                runtime.AddChat((Chat)c);
                return;
            }

            if (runtime.Chats().Count == 0) CreateDefaultChat();

            if (c is Opt) // add option data to last Ask
            {
                Opt opt = (Opt)c;

                Command last = LastOfType(parsedCommands, typeof(Ask));

                if (!(last is Ask)) throw new ParseException
                    (line, lineNo, "OPT must follow ASK");

                Ask ask = ((Ask)last);
                opt.parent = ask.parent;
                ask.AddOption(opt);
            }
            else  // add command to last Chat
            {
                ActiveChat().AddCommand(c);
            }
        }

        private void RunValidators(Command c)
        {
            RunExternalValidators(c);
            RunInternalValidators(c);
        }

        private void RunInternalValidators(Command c)
        {
            SetPropValuesFromMeta(c);
            c.Validate();
        }

        /// Update any property values that have been set in metadata
        protected internal void SetPropValuesFromMeta(Command c)
        {
            if (c.HasMeta())
            {
                var metaMeta = Properties.Lookup(c.GetType());

                foreach (KeyValuePair<string, object> pair in c.meta)
                {
                    object value = pair.Value;
                    if (metaMeta.ContainsKey(pair.Key))
                    {
                        if (pair.Key == Meta.ACTOR) // ugly special case
                        {
                            c.SetActor(runtime, (string)value);
                        }
                        else
                        {
                            c.Update(pair.Key, value, false);
                        }
                    }
                }
            }
        }

        internal static string[] StripComments(string text)
        {
            if (text == null) throw new ParseException("Null input");

            if (PRESERVE_LINE_NUMBERS)  // slower two-pass
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
            var c = Chat.Create("C" + Util.EpochMs());
            RunInternalValidators(c);
            parsedCommands.Push(c);
            runtime.AddChat(c);
        }
    }

    public class LineContext
    {
        internal string actor, command, text, label, meta;

        internal readonly string line;
        internal readonly int lineNo;

        public LineContext(string actor, string command, string text, string label, string meta)
        {
            this.actor = actor;
            this.command = command;
            this.text = text;
            this.label = label;
            this.meta = meta;
        }

        public LineContext(string line, int lineNo = -1, bool showMatch = false)
        {
            this.line = line;
            this.lineNo = lineNo;
            ParseLineContext(showMatch);
        }

        private void ParseLineContext(bool showMatch = false)
        {
            Match match = ChatParser.LineParser().Match(line);

            if (showMatch) Util.ShowMatch(match);

            if (match.Groups.Count < 6)
            {
                Util.ShowMatch(match);
                throw new ParseException(line, lineNo, "cannot be parsed");
            }

            this.actor = match.Groups[1].Value.Trim();
            this.command = match.Groups[2].Value.Trim();
            this.text = match.Groups[3].Value.Trim();
            this.label = match.Groups[4].Value.Trim();
            this.meta = match.Groups[5].Value.Trim();
        }
    }
}