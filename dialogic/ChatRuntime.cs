using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;

namespace Dialogic
{
    /// <summary>
    /// The main entry point for Dialogic. Construct a runtime instance as follows:
    /// \code
    ///     ChatRuntime dialogic = new ChatRuntime();
    ///     dialogic.ParseFile(scriptFolder);
    ///     dialogic.Run("#FirstChat");
    /// \endcode
    /// 
    /// Or configure with a List of IActors
    /// \code
    ///     ChatRuntime dialogic = new ChatRuntime(theActors);
    ///     dialogic.ParseFile(scriptFolder);
    ///     dialogic.Run("#FirstChat");
    /// \endcode
    /// </summary>
    public class ChatRuntime
    {
        public static string CHAT_FILE_EXT = ".gs";
        public static string logFile;// = "../../../dialogic/dia.log";

        internal static IDictionary<string, Type> TypeMap
            = new Dictionary<string, Type>()
        {
            { "CHAT",   typeof(global::Dialogic.Chat) },
            { "SAY",    typeof(global::Dialogic.Say)  },
            { "SET",    typeof(global::Dialogic.Set)  },
            { "ASK",    typeof(global::Dialogic.Ask)  },
            { "OPT",    typeof(global::Dialogic.Opt)  },
            { "DO",     typeof(global::Dialogic.Do)   },
            { "GO",     typeof(global::Dialogic.Go)   },
            { "WAIT",   typeof(global::Dialogic.Wait) },
            { "FIND",   typeof(global::Dialogic.Find) },
            { "GRAM",   typeof(global::Dialogic.Gram) },
        };

        internal bool validatorsDisabled;
        internal ChatScheduler scheduler;
        internal int nextEventTime;

        private Thread searchThread;
        private List<Chat> chats;
        private ChatParser parser;
        private List<IActor> actors;
        private AppEventHandler appEvents;
        private ChatEventHandler chatEvents;
        private List<Func<Command, bool>> validators;

        public ChatRuntime(List<IActor> actors) : this(null, actors) { }

        public ChatRuntime(List<Chat> chats, List<IActor> actors = null)
        {
            this.chats = chats;
            this.parser = new ChatParser(this);
            this.scheduler = new ChatScheduler(this);
            this.appEvents = new AppEventHandler(this);
            this.chatEvents = new ChatEventHandler(this);
            RegisterActors(actors);
        }

        public IUpdateEvent Update(IDictionary<string, object> globals, ref EventArgs ea)
        {
            return ea != null ? appEvents.OnEvent(ref ea, globals) : chatEvents.OnEvent(globals);
        }

        public List<Chat> ParseText(string text, bool disableValidators = false)
        {
            this.validatorsDisabled = disableValidators;
            var lines = text.Split(ChatParser.LineBreaks, StringSplitOptions.None);
            return parser.Parse(lines);
        }

        public void ParseFile(string fileOrFolder, bool disableValidators = false)
        {
            string[] files = Directory.Exists(fileOrFolder) ?
                files = Directory.GetFiles(fileOrFolder, '*' + ChatRuntime.CHAT_FILE_EXT) :
                files = new string[] { fileOrFolder };

            chats = new List<Chat>();
            validatorsDisabled = disableValidators;

            foreach (var f in files)
            {
                var text = File.ReadAllText(f);
                var stripped = ChatParser.StripComments(text);
                var parsed = parser.Parse(stripped);
                parsed.ForEach(c => chats.Add(c));
            }
        }

        public void Run(string chatLabel = null)
        {
            if (chats.IsNullOrEmpty()) throw new Exception("No chats found");
            scheduler.SetNext(chatLabel != null ? FindByName(chatLabel) : chats[0]);
        }

        /// <summary>
        /// Finds all Chat whose metadata match the specified key/value pair. If the value
        /// is not supplied, then only keys are checked, and values are ignored
        /// </summary>
        /// <returns>List of matching Chats</returns>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        public List<Chat> FindByMeta(string key, string value = null)
        {
            List<Chat> matched = new List<Chat>();
            for (int i = 0; i < chats.Count; i++)
            {
                if (chats[i].HasMeta(key) && (value == null
                    || ((string)chats[i].GetMeta(key)) == value))
                {
                    matched.Add(chats[i]);
                }
            }
            return matched;
        }

        /// <summary>
        /// Finds a Chat by its unique label or null if not found
        /// </summary>
        /// <returns>The Chat</returns>
        /// <param name="label">Label.</param>
        public Chat FindByName(string label)
        {
            if (label.StartsWith("#", Util.IC)) label = label.Substring(1);

            Chat result = null;

            for (int i = 0; i < chats.Count; i++)
            {
                if (chats[i].Text == label)
                {
                    result = chats[i];
                    break;
                }
            }
            return result;
        }

        public IActor FindActor(string name)
        {
            if (name.StartsWith("#", Util.IC)) name = name.Substring(1);

            foreach (var actor in actors)
            {
                if (actor.Name() == name) return actor;
            }
            return null;
        }

        // ----------------------------------------------------------------

        internal List<Func<Command, bool>> Validators()
        {
            return validators;
        }

        internal ChatParser Parser()
        {
            return parser;
        }

        internal void FindAsync(Find finder, IDictionary<string, object> globals = null)
        {
            scheduler.Suspend();

            int ts = Util.Millis();
            (searchThread = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                Chat chat = (finder is Go) ? FindByName(((Go)finder).Text) :
                    this.DoFind(finder, globals);

                if (chat == null) throw new FindException(finder);

                scheduler.SetNext(chat);

            })).Start();
        }

        private void RegisterActors(List<IActor> iActors)
        {
            if (iActors.IsNullOrEmpty()) return;

            this.actors = iActors;
            this.validators = new List<Func<Command, bool>>();

            actors.ForEach(s =>
            {
                var cmds = s.Commands();
                if (!cmds.IsNullOrEmpty())
                {
                    foreach (var cmd in cmds)
                    {
                        if (!TypeMap.ContainsKey(cmd.label))
                        {
                            TypeMap.Add(cmd.label, cmd.type);
                        }
                    }
                }
                var val = s.Validator();
                if (val != null)
                {
                    validators.Add(val);
                }
            });
        }

        internal void ComputeNextEventTime(Command cmd)
        {
            nextEventTime = cmd.DelayMs >= 0 ? Util.Millis()
                + cmd.ComputeDuration() : Int32.MaxValue;
        }

        // testing only ------------------------------------------

        internal Chat DoFind(params Constraint[] constraints)
        {
            return DoFind(null, null, constraints);
        }

        internal Chat DoFind(Chat parent, IDictionary<string, object> globals, params Constraint[] constraints)
        {
            IDictionary<Constraint, bool> cdict = new Dictionary<Constraint, bool>();
            foreach (var c in constraints) cdict.Add(c, c.IsRelaxable());
            return FuzzySearch.Find(chats, cdict, parent, globals);
        }

        internal Chat DoFind(Find f, IDictionary<string, object> globals = null)
        {
            f.Realize(globals); // possibly redundant
            return FuzzySearch.Find(chats, ToConstraintMap(f), f.parent, globals);
        }

        internal List<Chat> DoFindAll(params Constraint[] constraints)
        {
            return DoFindAll(null, null, constraints);
        }

        internal List<Chat> DoFindAll(Chat parent, IDictionary<string, object> globals, params Constraint[] constraints)
        {
            return FuzzySearch.FindAll(chats, constraints, parent, globals);
        }

        internal List<Chat> DoFindAll(Find f, IDictionary<string, object> globals = null)
        {
            f.Realize(globals);  // possibly redundant
            return FuzzySearch.FindAll(chats, ToList(f.realized), f.parent, globals);
        }

        // end testing -------------------------------------------


        private bool Logging()
        {
            return logFile != null;
        }

        private static IDictionary<Constraint, bool> ToConstraintMap(Find f)
        {
            IDictionary<Constraint, bool> cdict = new Dictionary<Constraint, bool>();
            foreach (var val in f.realized.Values)
            {
                Constraint c = (Constraint)val;
                cdict.Add(c, c.IsRelaxable());
            }
            return cdict;
        }

        private static IEnumerable<Constraint> ToList(IDictionary<string, object> dict)
        {
            List<Constraint> ic = new List<Constraint>();
            foreach (var val in dict.Values) ic.Add((Constraint)val);
            return ic;
        }
    }

    internal class ChatScheduler
    {
        internal Ask prompt;
        internal Chat chat, next;
        internal ChatRuntime runtime;
        internal Stack<Chat> resumables;
        internal IUpdateEvent chatEvent;

        internal ChatScheduler(ChatRuntime runtime)
        {
            this.runtime = runtime;
            this.resumables = new Stack<Chat>();
        }

        internal void Launch(string label)
        {
            var found = runtime.FindByName(label);

            if (found == null) throw new DialogicException
                ("Unable to find Chat: '" + label + "'");

            SetNext(found);
        }

        internal void SetNext(Chat c)
        {
            this.next = c;
        }

        internal IUpdateEvent LaunchNext()
        {
            if (next == null) throw new DialogicException
                ("Attempt to Launch null next Chat");

            (chat = next).Run(false); // calls Realize()

            this.next = null;

            return new UpdateEvent(chat);
        }

        internal void Suspend()
        {
            if (chat != null)
            {
                if (!chat.interruptable)
                {
                    Console.WriteLine("[WARN] Cannot interrupt #" + chat.Text + "!");
                    return;
                }
                resumables.Push(chat);
            }

            this.chat = null;
        }

        internal void Clear()
        {
            resumables.Clear();
        }

        internal int Resume()
        {
            if (resumables.IsNullOrEmpty())
            {
                //Console.WriteLine("No Chat to resume... Waiting");
                return -1;
            }

            if (chat != null && Defaults.THROW_ON_UNEXPECTED_RESUME)
            {
                Console.WriteLine("[WARN] Cannot resume Chat#" +
                    resumables.Pop().Text + " with Chat#" + chat.Text + " active");
            }

            var nxt = resumables.Pop();
            SetNext(nxt);

            return Util.Millis();
        }

        internal int OnComplete()
        {
            Console.WriteLine("#" + chat.Text + " completed ");

            if (resumables.Count > 0 && chat == resumables.Peek())
            {
                resumables.Pop(); // remove completed chat from stack
            }

            // if false, we don't resume after finishing
            var resumesAfter = chat.resumeAfterInterrupting;

            //chatEvent = new UpdateEvent(this.chat);
            this.chat = null;

            return resumesAfter ? this.Resume() : -1;
        }

        internal bool Ready()
        {
            return next != null || chat != null;
        }
    }
}