﻿using System;
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
        public static string LOG_FILE, CHAT_FILE_EXT = ".gs";
        public static bool SILENT = false;

        internal static bool DebugLifecycle = false;

        internal IDictionary<string, Type> typeMap
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

        internal bool strictMode = true;
        internal IDictionary<string, Chat> chats;
        internal IDictionary<string, Choice> choiceCache;
        internal ChatScheduler scheduler;
        internal bool validatorsDisabled;
        internal string firstChat;

        private List<IActor> actors;
        private List<Action<Chat>> findListeners;
        private List<Func<Command, bool>> validators;
        private ChatEventHandler chatEvents;
        private AppEventHandler appEvents;
        private Thread searchThread;
        private ChatParser parser;

        public ChatRuntime() : this(null, null) { }

        public ChatRuntime(List<IActor> theActors) : this(null, theActors) { }

        public ChatRuntime(List<Chat> theChats, List<IActor> theActors = null)
        {
            this.parser = new ChatParser(this);
            this.scheduler = new ChatScheduler(this);
            this.appEvents = new AppEventHandler(this);
            this.chatEvents = new ChatEventHandler(this);
            this.chats = new Dictionary<string, Chat>();
            this.choiceCache = new Dictionary<string, Choice>();
            this.actors = InitActors(theActors);

            if (!theChats.IsNullOrEmpty()) theChats.ForEach(AddChat);
        }

        internal string InvokeImmediate(IDictionary<string, object> globals, string label = null)
        {
            var chat = (label.IsNullOrEmpty() ? chats.Values :
                new Chat[] { this[label] }.ToList()).First();

            var result = string.Empty;

            chat.Run();

            if (!chat.HasNext()) return result;

            while (chat != null)
            {
                var cmd = chat.Next().Realize(globals);

                ProcessSay(ref result, ref cmd);

                if (cmd is Find)
                {
                    var toFind = cmd.ToString();
                    try
                    {
                        // a chat calling itself in immediate mode can cause
                        // an infinite loop; guard against it here
                        var find = Find((Find)cmd, globals);
                        if (find != chat) (chat = find).Run();
                    }
                    catch (Exception ex)
                    {
                        Warn(ex);
                        return result + "\n" + toFind + " failed";
                    }
                }

                chat = chat.HasNext() ? chat : null;
            }

            return result.TrimLast('\n');
        }

        private static void ProcessSay(ref string result, ref Command cmd)
        {
            if (cmd is Say)
            {
                result += cmd.Text() + "\n";
                if (cmd is Ask)
                {
                    cmd = Util.RandItem(((Ask)cmd).Options()).action;
                }
            }
        }

        public Chat this[string key] // string indexer -> runtime["chat4"]
        {
            get { return this.chats[key]; }
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

            // TODO: separate and run any 'preload' chats here

            scheduler.Launch(FindChatByLabel(chatLabel ?? firstChat));
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

        public List<Chat> Chats()
        {
            return chats.Values.ToList();
        }

        ///////////////////////////////////////////////////////////////////////

        internal void AddChat(Chat c)
        {
            c.runtime = this;
            if (chats.Count < 1) firstChat = c.text;
            if (c.text == null)
            {
                throw new DialogicException("Invalid Chat (no-name): " + c);
            }
            chats.Add(c.text, c);
        }

        internal Chat AddNewChat(string name) // testing only
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
        internal void FindAllAsync(Find finder, Action<Chat> action,
            IDictionary<string, object> globals = null)
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

        internal Chat Find(Find f, IDictionary<string, object> globals = null)
        {
            var chat = (f is Go) ? FindChatByLabel(f.text) : DoFind(f, globals);
            if (chat == null) throw new FindException(f);
            return chat;
        }

        internal void FindAsync(Find f, IDictionary<string, object> globals = null)
        {
            (searchThread = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                Chat chat = Find(f, globals);

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
                        if (!typeMap.ContainsKey(cmd.label))
                        {
                            typeMap.Add(cmd.label, cmd.type);
                        }
                    }
                }
                var val = s.Validator();
                if (val != null) validators.Add(val);
            });

            return iActors;
        }

        private static IDictionary<Constraint, bool> ToConstraintMap(Find f)
        {
            IDictionary<Constraint, bool> cdict;
            cdict = new Dictionary<Constraint, bool>();
            foreach (var val in f.realized.Values)
            {
                if (!(val is Constraint))
                {
                    throw new DialogicException("type:" + val.GetType());
                }
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

        internal void AddFindListener(Action<Chat> callback)
        {
            if (findListeners == null) findListeners = new List<Action<Chat>>();
            findListeners.Add(callback);
        }

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
            if (f.realized.Count == 0) f.Realize(globals);
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

        internal List<Chat> DoFindAll(Find f, IDictionary<string, object> globals)
        {
            if (f.realized.Count == 0) f.Realize(globals);
            return FuzzySearch.FindAll(Chats(), ToList(f.realized), f.parent, globals);
        }

        internal static void Info(object msg)
        {
            if (!ChatRuntime.SILENT) Console.WriteLine(msg);
        }

        internal static void Warn(object msg)
        {
            if (!ChatRuntime.SILENT) Console.WriteLine("[WARN] " + msg);
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
                    ChatRuntime.Warn("Cannot interrupt #" + chat.text);
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
            if (chat != null && !Waiting())
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

            this.chat.OnCompletion();

            this.chat = null;

            if (resumesAfter) this.Resume();
        }

        internal static void Info(object msg)
        {
            if (ChatRuntime.DebugLifecycle)
            {
                ChatRuntime.Info(msg);
            }
        }

        internal void Warn(object msg)
        {
            ChatRuntime.Warn(msg);
        }

        internal bool Waiting()
        {
            return nextEventTime == -1;
        }

        internal bool Ready()
        {
            return chat != null && !Waiting()
                && Util.Millis() >= nextEventTime;
        }
    }
}