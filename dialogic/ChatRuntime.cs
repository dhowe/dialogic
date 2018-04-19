using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
        };

        internal static IDictionary<Type, IDictionary<string, PropertyInfo>>
            MetaMeta = new Dictionary<Type, IDictionary<string, PropertyInfo>>();

        internal bool validatorsDisabled;
        internal ChatScheduler scheduler;

        private Chat firstChat;
        private Thread searchThread;
        private ChatParser parser;
        private List<IActor> actors;
        private AppEventHandler appEvents;
        private ChatEventHandler chatEvents;
        private List<Func<Command, bool>> validators;
        private IDictionary<string, Chat> chats;

        public ChatRuntime() : this(null, null) { }

        public ChatRuntime(List<IActor> theActors) : this(null, theActors) { }

        public ChatRuntime(List<Chat> theChats, List<IActor> theActors = null)
        {
            this.parser = new ChatParser(this);
            this.scheduler = new ChatScheduler(this);
            this.appEvents = new AppEventHandler(this);
            this.chatEvents = new ChatEventHandler(this);
            this.chats = new Dictionary<string, Chat>();
            this.actors = InitActors(theActors);

            if (!theChats.IsNullOrEmpty()) theChats.ForEach(AddChat);
        }

        public void ParseText(string text, bool disableValidators = false)
        {
            this.validatorsDisabled = disableValidators;
            var lines = text.Split(ChatParser.LineBreaks, StringSplitOptions.None);
            parser.Parse(lines);
        }

        public void ParseFile(string fileOrFolder, bool disableValidators = false)
        {
            var ext = '*' + (CHAT_FILE_EXT != null ? CHAT_FILE_EXT : string.Empty);

            var files = Directory.Exists(fileOrFolder) ?
                Directory.GetFiles(fileOrFolder, '*' + ext) :
                new string[] { fileOrFolder };

            this.validatorsDisabled = disableValidators;

            foreach (var f in files)
            {
                var text = File.ReadAllText(f);
                parser.Parse(ChatParser.StripComments(text));
            }
            //Chats().ForEach(c => Console.WriteLine(c.ToTree()));
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
            return ge != null ? appEvents.OnEvent
                (ref ge, globals) : chatEvents.OnEvent(globals);
        }

        public void Run(string chatLabel = null)
        {
            if (chats.Count < 1) throw new Exception("No chats found");

            scheduler.Launch(chatLabel != null ? FindChatByLabel(chatLabel) : FirstChat());
        }

        public Chat FindChatByLabel(string label)
        {
            Util.ValidateLabel(ref label);
            if (!chats.ContainsKey(label)) throw new DialogicException
              ("Unable to find Chat with label: '" + label + "'");
            return chats[label];
        }

        public IActor FindActorByName(string name)
        {
            Util.ValidateLabel(ref name);
            return actors.FirstOrDefault(c => c.Name() == name);
        }

        public override string ToString()
        {
            return "{ context: " + CurrentContext() +
                ", chats:" + Chats().Stringify() + " }";
        }

        ///////////////////////////////////////////////////////////////////////

        internal List<Chat> Chats()
        {
            return chats.Values.ToList();
        }

        internal void AddChat(Chat c)
        {
            c.runtime = this;
            if (chats.Count < 1) firstChat = c;
            chats.Add(c.text, c);
        }

        internal List<Chat> FindChatByMeta(string key, string value) // used?
        {
            return Chats().Where(c => ((string)c.GetMeta(key))
                == value).ToList<Chat>();
        }

        internal Chat AddNewChat(string name)
        {
            Chat c = new Chat();
            c.Init(name, String.Empty, new string[0]);
            AddChat(c);
            return c;
        }

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
                    DoFindAll(finder, globals).ForEach(action);
                }

            })).Start();
        }

        internal void FindAsync(Find finder, IDictionary<string, object> globals = null)
        {
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

        private Chat FirstChat()
        {
            if (firstChat == null) throw new DialogicException
                ("Invalid state: no initial Chat" + this);
            return firstChat;
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

        private static List<Constraint> ToList
            (IDictionary<string, object> dict)
        {
            List<Constraint> ic = new List<Constraint>();
            foreach (var val in dict.Values) ic.Add((Constraint)val);
            return ic;
        }

        // for testing ------------------------------------------

        internal Chat DoFind(Chat parent, params Constraint[] constraints)
        {
            return DoFind(parent, null, constraints);
        }

        internal Chat DoFind(Chat parent,
            IDictionary<string, object> globals, params Constraint[] constraints)
        {
            IDictionary<Constraint, bool> cdict = new Dictionary<Constraint, bool>();
            foreach (var c in constraints) cdict.Add(c, c.IsRelaxable());
            return FuzzySearch.Find(Chats(), cdict, parent, globals);
        }

        internal Chat DoFind(Find f, IDictionary<string, object> globals = null)
        {
            f.Realize(globals); // possibly redundant
            return FuzzySearch.Find(Chats(), ToConstraintMap(f), f.parent, globals);
        }

        internal List<Chat> DoFindAll(Chat parent, params Constraint[] constraints)
        {
            return DoFindAll(parent, null, constraints);
        }

        internal List<Chat> DoFindAll(Chat parent,
            IDictionary<string, object> globals, params Constraint[] constraints)
        {
            return FuzzySearch.FindAll(Chats(), constraints.ToList(), parent, globals);
        }

        internal List<Chat> DoFindAll(Find f, IDictionary<string, object> globals = null)
        {
            f.Realize(globals);  // possibly redundant
            return FuzzySearch.FindAll(Chats(), ToList(f.realized), f.parent, globals);
        }
    }

    /// <summary>
    /// Holds the current state of a ChatRuntime, either Running, Suspended, or Waiting
    /// </summary>
    /// States:
    ///   Running:   chat != null, net > -1 -> running Chat commands
    ///   Suspended: chat != null, net = -1 -> waiting (either on a Wait or Prompt)
    ///   Waiting:   chat  = null, net = -1 -> waiting (on a SuspendEvent, or all Chats done)
    public enum RuntimeState { Running, Suspended, Waiting };

    /// <summary>
    /// Holds the current context of a ChatRuntime including its RuntimeState, 
    /// the current Chat, and the nextEventTime offset (in ms)
    /// </summary>
    public class RuntimeContext
    {
        private static RuntimeContext instance;

        public RuntimeState State { get; protected set; }
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

        internal int Launch(string label, bool resetCursor = true)
        {
            return Launch(runtime.FindChatByLabel(label), resetCursor);
        }

        internal int Launch(Chat next, bool resetCursor = true)
        {
            if (next == null) throw new DialogicException
                ("Attempt to launch a null Chat");

            nextEventTime = Util.Millis();
            chat = next;
            chat.Run(resetCursor);

            // Chats are not ISendable, but its useful for the client to know 
            // when a new Chat is started, so we send the minimal data here
            chatEvent = new UpdateEvent(new Dictionary<string, object>(){
                { Meta.TYPE, chat.TypeName() },
                { Meta.TEXT, chat.text },
            });

            Info("\n<#" + chat.text + (resetCursor ? "-started>" : "-resumed>"));

            return nextEventTime;
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
                    // grab top Chat on stack
                    chat = resumables.Pop();

                    // check for a smoothing Chat to run first
                    var trans = FindTransitionChat();

                    if (trans != null) chat = trans;

                    return Launch(chat, trans != null);
                }
                else
                {
                    //Console.WriteLine("Nothing to resume... waiting");
                    return -1;
                }
            }

            return Util.Millis(); // unsuspend non-null current chat
        }

        // If a Chat has a Meta.ON_RESUME tag, then we will invoke the specified
        // 'smoothing' Chat and re-schedule the resuming chat as next.
        private Chat FindTransitionChat()
        {
            if (Defaults.CHAT_ENABLE_SMOOTHING)
            {
                if (chat.allowSmoothingOnResume)
                {
                    var onResume = chat.GetMeta(Meta.ON_RESUME);
                    if (onResume != null)
                    {
                        var smoother = runtime.FindChatByLabel((string)onResume);
                        if (smoother != null)
                        {
                            // avoid inifinite loop: disable smoothing next time
                            chat.allowSmoothingOnResume = false;
                            resumables.Push(chat);
                            return smoother;
                        }
                    }
                }
                else
                {
                    // re-engage smoothing for the future
                    chat.allowSmoothingOnResume = true;
                }
            }

            return null;
        }

        internal void Completed(bool allowResume)
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

            if (resumesAfter) this.Resume();
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