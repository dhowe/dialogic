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

        internal const string TXT = @"([^#}{]+)?\s*";
        internal const string LBLL = @"(#[A-Za-z][\S]*)";
        internal const string LBLG = @"(#\([^\)]+\s*)";
        internal const string LBL = @"(?:" + LBLL + "|" + LBLG + @")?\s*";
        internal const string MTD = @"(?:\{(.+?)\})?\s*";
        internal const string ACTR = @"(?:([A-Za-z_][A-Za-z0-9_-]+):)?\s*";
        internal const string DLBL = @"((?:#[A-Za-z][\S]*)\s*|(?:#\(\s*[A-Za-z][^\|]*(?:\|\s*[A-Za-z][^\|]*)+\))\s*)?\s*";


        static Regex MultiComment = new Regex(@"/\*[^*]*\*+(?:[^/*][^*]*\*+)*/");
        static Regex SingleComment = new Regex(@"//(.*?)(?:$|\r?\n)");
        internal static string[] LineBreaks = { "\r\n", "\r", "\n" };

        protected ChatRuntime runtime;
        protected Stack<Command> parsedCommands;
        protected internal List<Chat> chats;
        protected internal Regex lineParser;

        internal ChatParser(ChatRuntime runtime)
        {
            this.lineParser = new Regex(ACTR + TypesRegex() + TXT + DLBL + MTD);
            this.parsedCommands = new Stack<Command>();
            this.chats = new List<Chat>();
            this.runtime = runtime;
        }

        internal static List<Chat> ParseText(string s, bool noValidators = false)
        {
            ChatRuntime rt = new ChatRuntime(Tendar.AppConfig.Actors); // tmp: testing
            rt.ParseText(s, noValidators);
            return rt.chats;
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
                List<string> parts = DoSubDivision(line, lineNo);
                c = ParseCommand(parts, line, lineNo);
                RunExternalValidators(c);
                RunInternalValidators(c);
            }
            catch (Exception ex)
            //catch (ParseException ex)
            {
                throw new ParseException(line, lineNo, ex.Message);
            }

            return c;
        }

        private List<string> DoSubDivision(string line, int lineNo)
        {
            Match match = lineParser.Match(line);

            if (match.Groups.Count < 6)
            {
                Util.ShowMatch(match);
                throw new ParseException(line, lineNo, "cannot be parsed");
            }

            var parts = new List<string>();
            for (int j = 1; j < 6; j++)
            {
                parts.Add(match.Groups[j].Value.Trim());
            }

            return parts;
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

        private Command ParseCommand(List<string> parts, string line, int lineNo)
        {
            Command c = null;

            parts.Apply((spkr, cmd, text, label, meta) =>
            {
                Type type = cmd.Length > 0 ? ChatRuntime.TypeMap[cmd]
                    : Chat.DefaultCommandType(chats.LastOrDefault());

                try
                {
                    c = Command.Create(type, text, label, SplitMeta(meta));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                HandleActor(spkr, c, line, lineNo);
                HandleCommand(c, line, lineNo);
            });

            return c;
        }

        private string[] SplitMeta(string meta)
        {
            return meta.IsNullOrEmpty() ? null : RE.MetaSplit.Split(meta);
        }

        private void HandleActor(string spkr, Command c, string line, int lineNo)
        {
            c.Actor(Actor.Default);

            if (!string.IsNullOrEmpty(spkr) && runtime != null)
            {
                c.Actor(runtime, spkr);

                if (c.actor == null) throw new ParseException
                    (line, lineNo, "Unknown actor: '" + spkr + "'");

                if (!Equals(c.actor, Actor.Default)) c.SetMeta(Meta.ACTOR, c.actor.Name());
            }
        }

        private Chat AddChat(Chat c)
        {
            c.runtime = this.runtime;
            chats.Add(c);
            return c;
        }

        private void HandleCommand(Command c, string line, int lineNo)
        {
            if (c is Chat)
            {
                AddChat((Chat)c);
                return;
            }

            if (chats.Count == 0) CreateDefaultChat();

            if (c is Opt) // add option data to last Ask
            {
                Opt opt = (Opt)c;

                Command last = LastOfType(parsedCommands, typeof(Ask));

                if (!(last is Ask)) throw new ParseException
                    (line, lineNo, "Opt must follow Ask");

                Ask ask = ((Ask)last);
                opt.parent = ask.parent;
                ask.AddOption(opt);
            }
            else  // add command to last Chat
            {
                chats.Last().AddCommand(c);
            }

            parsedCommands.Push(c);
        }

        private void RunInternalValidators(Command c)
        {
            SetPropValuesFromMeta(c);
            c.Validate();
        }

        /// Extract properties that can be set from metadata
        protected void ExtractMetaMeta(Command c)
        {
            var type = c.GetType();

            var mmeta = ChatRuntime.MetaMeta;

            if (!mmeta.ContainsKey(type))
            {
                mmeta.Add(type, new Dictionary<string, PropertyInfo>());
                // Console.WriteLine(type+":");

                var props = type.GetProperties(BindingFlags.Instance
                    | BindingFlags.Public | BindingFlags.NonPublic);

                foreach (var pi in props)
                {
                    mmeta[type].Add(pi.Name, pi);
                    // Console.WriteLine("  "+pi.Name);
                }
            }
        }

        /// Updates any property values that have been set in metadata
        protected internal void SetPropValuesFromMeta(Command c)
        {
            if (!ChatRuntime.MetaMeta.ContainsKey(c.GetType())) ExtractMetaMeta(c);

            if (c.HasMeta())
            {
                var mmeta = ChatRuntime.MetaMeta[c.GetType()];

                foreach (KeyValuePair<string, object> pair in c.meta)
                {
                    object val = pair.Value;
                    if (mmeta.ContainsKey(pair.Key))
                    {
                        if (pair.Key == Meta.ACTOR) // hrmm, ugly special case
                        {
                            c.Actor(runtime, (string)val);
                        }
                        else
                        {
                            //var propInfo = mmeta[pair.Key];
                            //val = Util.ConvertTo(propInfo.PropertyType, val);
                            //propInfo.SetValue(c, val, null);

                            c.DynamicSet(mmeta[pair.Key], val, false);
                        }
                    }
                }
            }
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
            parsedCommands.Push(AddChat(Chat.Create("C" + Util.EpochMs())));
        }
    }
}
