using System;
using System.Collections.Generic;
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
    /// Or configure with a List of Actors
    /// \code
    ///     ChatRuntime dialogic = new ChatRuntime(theActors);
    ///     dialogic.ParseFile(scriptFolder);
    ///     dialogic.Run("#FirstChat");
    /// \endcode
    /// </summary>
    public class ChatRuntime
    {
        public static string LOG_FILE;// = "../../../dialogic/dia.log";

        public static string CHAT_FILE_EXT = ".gs";

        internal static bool DebugLifecycle = false;

        internal static IDictionary<string, Type> TypeMap
            = new Dictionary<string, Type>()
        {
            { "CHAT",   typeof(Chat) },
            { "SAY",    typeof(Say)  },
            { "SET",    typeof(Set)  },
            { "ASK",    typeof(Ask)  },
            { "OPT",    typeof(Opt)  },
            { "DO",     typeof(Do)   },
            { "GO",     typeof(Go)   },
            { "WAIT",   typeof(Wait) },
            { "FIND",   typeof(Find) },
            { "GRAM",   typeof(Gram) },
        };

        internal bool validatorsDisabled;
        internal ChatScheduler scheduler;
        internal List<Chat> chats;

        private Thread searchThread;
        private ChatParser parser;
        private List<IActor> actors;
        private AppEventHandler appEvents;
        private ChatEventHandler chatEvents;
        private List<Func<Command, bool>> validators;

        public ChatRuntime(List<IActor> actors) : this(null, actors) { }

        public ChatRuntime(List<Chat> chats, List<IActor> actors = null)
        {
            this.chats = chats;
            this.actors = InitActors(actors);

            this.parser = new ChatParser(this);
            this.scheduler = new ChatScheduler(this);
            this.appEvents = new AppEventHandler(this);
            this.chatEvents = new ChatEventHandler(this);
        }

        public List<Chat> ParseText(string text, bool disableValidators = false)
        {
            this.validatorsDisabled = disableValidators;
            var lines = text.Split(ChatParser.LineBreaks, StringSplitOptions.None);
            return (chats = parser.Parse(lines));
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

            //chats.ForEach(c => Console.WriteLine(c.ToTree()));
        }

        /// <summary>
        /// Returns the current context for the runtime, consisting of its current
        /// state, the current Chat, and the offset until the next scheduled event
        /// </summary>
        /// <returns>The context.</returns>
        public RuntimeContext CurrentContext()
        {
            return RuntimeContext.Update(scheduler);
        }

        public IUpdateEvent Update(IDictionary<string, object> globals, ref EventArgs ge)
        {
            return ge != null ? appEvents.OnEvent(ref ge, globals) : chatEvents.OnEvent(globals);
        }

        public void Run(string chatLabel = null)
        {
            if (chats.IsNullOrEmpty()) throw new Exception("No chats found");

            var first = chatLabel != null ? FindChatByLabel(chatLabel) : chats[0];
            scheduler.Launch(first);
        }

        public List<Chat> FindChatByMeta(string key, string value)
        {
            //foreach (var c in chats) Console.WriteLine(c.text + ": "+c.GetMeta(key)+ " " + ((string)c.GetMeta(key)==value));
            return chats.Where(c => ((string)c.GetMeta(key)) == value).ToList<Chat>();
        }

        public Chat FindChatByLabel(string label)
        {
            Util.ValidateLabel(ref label);
            return chats.FirstOrDefault(c => c.text == label);
        }

        public IActor FindActorByName(string name)
        {
            Util.ValidateLabel(ref name);
            return actors.FirstOrDefault(c => c.Name() == name);
        }

        ///////////////////////////////////////////////////////////////////////

        internal List<Func<Command, bool>> Validators()
        {
            return validators;
        }

        internal ChatParser Parser()
        {
            return parser;
        }

        /// <summary>
        /// Invokes 'action' on all the chats matching the Find command
        /// </summary>
        /// <param name="finder">Finder.</param>
        /// <param name="action">Action.</param>
        /// <param name="globals">Globals.</param>
        internal void FindAllAsync(Find finder, Action<Chat> action, IDictionary<string, object> globals = null)
        {
            (searchThread = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                if (finder is Go)
                {
                    action.Invoke(FindChatByLabel(((Go)finder).text));
                }
                else
                {
                    var found = DoFindAll(finder, globals);
                    found.ForEach(action);
                }

            })).Start();
        }

        internal void FindAsync(Find finder, IDictionary<string, object> globals = null)
        {
            scheduler.Completed(false); // Consider current chat finished on a FIND cmd

            int ts = Util.Millis();
            (searchThread = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                Chat chat = (finder is Go) ? FindChatByLabel(((Go)finder).text) :
                    this.DoFind(finder, globals);

                if (chat == null) throw new FindException(finder);

                scheduler.Launch(chat);

            })).Start();
        }

        private List<IActor> InitActors(List<IActor> iActors)
        {
            if (iActors.IsNullOrEmpty()) return null;

            this.validators = new List<Func<Command, bool>>();

            iActors.ForEach(s =>
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
                if (val != null) validators.Add(val);
            });

            return iActors;
        }

        // testing only ------------------------------------------

        internal Chat DoFind(params Constraint[] constraints)
        {
            return DoFind(null, null, constraints);
        }

        internal Chat DoFind(Chat parent,
            IDictionary<string, object> globals, params Constraint[] constraints)
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

        internal List<Chat> DoFindAll(Chat parent,
            IDictionary<string, object> globals, params Constraint[] constraints)
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
            return LOG_FILE != null;
        }

        private static IDictionary<Constraint, bool> ToConstraintMap(Find f)
        {
            IDictionary<Constraint, bool> cdict;
            cdict = new Dictionary<Constraint, bool>();
            foreach (var val in f.realized.Values)
            {
                Constraint c = (Constraint)val;
                cdict.Add(c, c.IsRelaxable());
            }
            return cdict;
        }

        private static IEnumerable<Constraint> ToList
            (IDictionary<string, object> dict)
        {
            List<Constraint> ic = new List<Constraint>();
            foreach (var val in dict.Values) ic.Add((Constraint)val);
            return ic;
        }

    }

    /// States:
    ///   Running:        chat != null, net > -1 -> running Chat commands
    ///   Suspended:      chat != null, net = -1 -> waiting (either on a Wait or Prompt)
    ///   Waiting:        chat  = null, net = -1 -> waiting (on a SuspendEvent, or all Chats done)
    public enum RuntimeState { Running, Suspended, Waiting };

    public class RuntimeContext
    {
        private static RuntimeContext instance;

        public RuntimeState State  { get; protected set; }
		public int NextEventTime { get; protected set; }
        public Chat Chat { get; protected set; }

        private RuntimeContext() { }

        internal static RuntimeContext Update(ChatScheduler sc)
        {
            if (instance == null) instance = new RuntimeContext();

            instance.Chat = sc.chat;
            instance.NextEventTime = sc.nextEventTime;
            instance.State = GetState(sc.chat, sc.nextEventTime);

            return instance;
        }

        private static RuntimeState GetState(Chat chat, int nextEventTime)
        {
            RuntimeState state = RuntimeState.Waiting;
            if (chat != null) state = nextEventTime > -1 ?
                RuntimeState.Running : RuntimeState.Suspended;
            return state;
        }

        public override string ToString()
        {
            return "{ state: " + State + ", chat:" + Chat + ", nextEventMs: " + NextEventTime + " }";
        }
    }

    internal class ChatScheduler
    {
        internal Chat chat;
        internal Ask prompt;
        internal ChatRuntime runtime;
        internal IUpdateEvent chatEvent;
        internal Stack<Chat> resumables;
        internal int nextEventTime;

        internal ChatScheduler(ChatRuntime runtime)
        {
            this.runtime = runtime;
            this.resumables = new Stack<Chat>();
        }

        internal void Launch(string label)
        {
            var next = runtime.FindChatByLabel(label);
            if (next == null) throw new DialogicException
                ("Unable to find Chat: '" + label + "'");
            Launch(next);
        }

        internal void Launch(Chat next)
        {
            if (next == null) throw new DialogicException
                ("Attempt to launch a null Chat");
            chat = next;
            nextEventTime = Util.Millis();
            chat.Run();

            chatEvent = new UpdateEvent(new Dictionary<string, object>(){
                { Meta.TYPE, chat.TypeName() },
                { Meta.TEXT, chat.text },
            });

            Info("\n<#" + chat.text + "-started>");
        }

        internal void Suspend()
        {
            if (chat != null)
            {
                if (!chat.interruptable)
                {
                    Console.WriteLine("Cannot interrupt #" + chat.text + "!");
                    return;
                }
                resumables.Push(chat);

                Info("<#" + chat.text + "-suspending>");
            }
            nextEventTime = -1;
        }

        internal void Clear()
        {
            resumables.Clear();
        }

        internal int Resume()
        {
            if (chat != null && nextEventTime > -1)
            {
                Warn("Ignoring attempt to resume while Chat#"
                     + chat.text + " is active & running\n");
            }

            if (chat == null)
            {
                if (!resumables.IsNullOrEmpty())
                {
                    var last = resumables.Pop();
                    chat = last;
                    chat.Run(false);
                    return Util.Millis();
                }
                else
                {
                    //Console.WriteLine("Nothing to resume... waiting");
                    return -1;
                }
            }

            return Util.Millis(); // unsuspend non-null current chat
        }

        internal int Completed(bool allowResume)
        {
            if (chat == null) throw new DialogicException
                ("Attempt to complete a null Chat");

            if (resumables.Count > 0 && chat == resumables.Peek())
            {
                resumables.Pop(); // remove finished from stack
            }

            // if false, we don't resume after finishing
            var resumesAfter = allowResume && chat.resumeAfterInt;

            Info("<#" + chat.text + "-finished>");

            this.chat = null;

            return resumesAfter ? this.Resume() : -1;
        }

        internal void Info(object msg)
        {
            if (ChatRuntime.DebugLifecycle)
            {
                Console.WriteLine(msg);
            }
        }

        internal void Warn(object msg)
        {
            Console.WriteLine("[WARN] " + msg);
        }

        internal bool Ready()
        {
            return chat != null && nextEventTime > -1
                && Util.Millis() >= nextEventTime;
        }
    }
}