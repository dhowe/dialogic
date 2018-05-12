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
        public static string LOG_FILE;
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

        internal bool validatorsDisabled, strictMode = true;
        internal IDictionary<string, Choice> choiceCache;
        internal IDictionary<string, Chat> chats;
        internal ChatScheduler scheduler;
        internal Resolver resolver;
        internal string firstChat;

        private List<IActor> actors;
        private List<Action<Chat>> findListeners;
        private List<Func<Command, bool>> validators;
        private ChatEventHandler chatEvents;
        private AppEventHandler appEvents;
        private Thread searchThread;
        private FuzzySearch search;
        private ChatParser parser;

        public ChatRuntime() : this(null, null) { }

        public ChatRuntime(List<IActor> theActors) : this(null, theActors) { }

        public ChatRuntime(List<Chat> theChats, List<IActor> theActors = null)
        {            
            this.Reset(theChats);
            this.actors = InitActors(theActors);
        }

        /// <summary>
        /// Create a new runtime from previously serialized bytes loaded from a file, using the specified actors.
        /// </summary>
        /// <returns>The new ChatRuntime</returns>
        /// <param name="serializer">Serializer.</param>
        /// <param name="file">File.</param>
        /// <param name="theActors">The actors.</param>
        public static ChatRuntime Create(ISerializer serializer, FileInfo file, List<IActor> theActors)
        {
            return Create(serializer, File.ReadAllBytes(file.FullName), theActors);
        }

        /// <summary>
        /// Create a new runtime from previously serialized bytes, using the specified actors.
        /// </summary>
        /// <returns>The new ChatRuntime</returns>
        /// <param name="serializer">Serializer.</param>
        /// <param name="bytes">Bytes.</param>
        /// <param name="theActors">The actors.</param>
        public static ChatRuntime Create(ISerializer serializer, byte[] bytes, List<IActor> theActors)
        {
            ChatRuntime rt = new ChatRuntime(theActors);
            serializer.FromBytes(rt, bytes);
            return rt;
        }

        /// <summary>
        /// Indexer for chat-names, allows for myRuntime["chatName"];
        /// </summary>
        public Chat this[string key] // string indexer -> runtime["chat4"]
        {
            get { return this.chats[key]; }
        }

        /// <summary>
        /// Returns true if the runtime contains a chat with this name
        /// </summary>
        public bool ContainsKey(string chatName) // convenience check for indexer
        {
            return this.chats.ContainsKey(chatName);
        }

        /// <summary>
        /// Clear all chats and resets all runtime state
        /// </summary>
        public void Reset(List<Chat> theChats = null)
        {
            this.resolver = new Resolver(this);
            this.parser = new ChatParser(this);
            this.search = new FuzzySearch(this);
            this.scheduler = new ChatScheduler(this);
            this.appEvents = new AppEventHandler(this);
            this.chatEvents = new ChatEventHandler(this);
            this.chats = new Dictionary<string, Chat>();
            this.choiceCache = new Dictionary<string, Choice>();

            if (!theChats.IsNullOrEmpty()) AppendChats(theChats);
        }

        /// <summary>
        /// Parse a string of text continuing chat definitions
        /// </summary>
        public void ParseText(string text, bool disableValidators = false)
        {
            if (text.EndsWith(".gs", Util.IC)) Warn("This text looks " // tmp
                + "like a file name, did you mean to use ParseFile()?\n");
            this.validatorsDisabled = disableValidators;
            parser.Parse(text.Split(ChatParser.LineBreaks, StringSplitOptions.None));
        }

        public void ParseFile(string s, bool dv = false, string fe = null)
        {
            Warn("ParseFile(string, ...) has been deprecated and will soon be"
                 + " removed; please use ParseFile(FileInfo f, ...) instead"); // tmp
            ParseFile(new FileInfo(s), dv, fe);
        }

        /// <summary>
        /// Parse chat definitions from a file (or folder of files) ending with
        /// '*.gs', or the specified extension
        /// </summary>
        /// <param name="fileOrFolder">File (or folder of files) to parse</param>
        /// <param name="disableValidators">If set to <c>true</c> disable app-specific validators (default=false).</param>
        /// <param name="fileExt">File extension to load (empty-string for all files, default is ".gs")</param>
        public void ParseFile(FileInfo fileOrFolder,
            bool disableValidators = false, string fileExt = null)
        {
            var file = fileOrFolder.FullName;
            var files = Directory.Exists(file) ? Directory.GetFiles
                (file, '*' + (fileExt ?? ".gs")) : new[] { file };

            this.validatorsDisabled = disableValidators;

            foreach (var f in files)
            {
                var text = File.ReadAllText(f);
                parser.Parse(ChatParser.StripComments(text));
            }
            //Chats().ForEach(c => Console.WriteLine(c.ToTree()));
        }

        /// <summary>
        /// Returns the current context for the runtime, consisting of its current state, the current Chat, and the offset until the next scheduled event
        /// </summary>
        /// <returns>The context.</returns>
        public RuntimeContext CurrentContext() => RuntimeContext.Update(scheduler);

        /// <summary>
        /// To be called bt client application each frame, passing the current world-state (a dictionary of key-value pairs) and any event that occurred during that frame. If a Dialogic event (e.g., a Chat command) occurs during the frame it is returned from the Update function.
        /// </summary>
        /// <returns>The update.</returns>
        /// <param name="globals">Globals.</param>
        /// <param name="ge">Ge.</param>
        public IUpdateEvent Update(IDictionary<string, object> globals, ref EventArgs ge)
        {
            return ge != null ? appEvents.OnEvent
                (ref ge, globals) : chatEvents.OnEvent(globals);
        }

        /// <summary>
        /// Start the runtime schedule, executing the installed chats
        /// </summary>
        public void Run(string chatLabel = null)
        {
            if (chats.Count < 1) throw new Exception("No chats found");

            scheduler.Launch(FindChatByLabel(chatLabel ?? firstChat));
        }

        /// <summary>
        /// Preload the Chat with the specified label, or if no label is supplied,
        /// preload all Chats with the 'preload = true' metadata tag.
        /// </summary>
        /// <param name="globals">Globals variables</param>
        /// <param name="chatLabels">Zero or more Chat labels to preload</param>
        public void Preload(IDictionary<string, object> globals, params string[] chatLabels)
        {
            if (chats.Count < 1) throw new Exception("No chats found");

            List<Chat> theChats = null;

            if (!chatLabels.IsNullOrEmpty())
            {
                theChats = new List<Chat>();
                for (int i = 0; i < chatLabels.Length; i++)
                {
                    theChats.Add(this[chatLabels[i]]);
                }
            }
            else
            {
                theChats = new List<Chat>(chats.Values);
            }

            foreach (Chat chat in theChats)
            {
                if (chat.IsPreload())
                {
                    chat.commands.ForEach(c => c.Resolve(globals));
                }
            }
        }

        /// <summary>
        /// Register the transform function, allowing it to be invoked by scripts
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="transformFunction">Transform function.</param>
        public void AddTransform(string name, Func<string, string> transformFunction)
        {
            Transforms.Add(name, transformFunction);
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

        /// <summary>
        /// Converts the current chat data to a flat List
        /// </summary>
        public List<Chat> Chats() => chats.Values.ToList();

        /// <summary>
        /// Save this instance to a serialized byte array
        /// </summary>
        public byte[] Save(ISerializer serializer, FileInfo file = null)
        {
            byte[] bytes = serializer.ToBytes(this);
            if (file != null) File.WriteAllBytes(file.FullName, bytes);
            return bytes;
        }

        /// <summary>
        /// Save all chats in the runtime to a serialized byte array
        /// </summary>
        /*public byte[] SaveChats(ISerializer serializer, FileInfo file = null)
        {
            byte[] bytes = serializer.ToBytes(Chats());
            if (file != null) File.WriteAllBytes(file.FullName, bytes);
            return bytes;
        }*/

        /// <summary>
        /// Update this instance with new data from a serialized byte array
        /// </summary>
        public void UpdateFrom(ISerializer serializer, byte[] bytes)
            => serializer.FromBytes(this, bytes);

        /// <summary>
        /// Update this instance with new data from a serialized byte array
        /// </summary>
        public void UpdateFrom(ISerializer serializer, ChatRuntime rt)
            => serializer.FromBytes(this, serializer.ToBytes(rt));

        /// <summary>
        /// Serialize this runtime and return the data as a JSON string
        /// </summary>
        public string ToJSON(ISerializer serializer) => serializer.ToJSON(this);

        public override string ToString()
        {
            return "{ context: " + CurrentContext() +
                ", chats:" + Chats().Stringify() + " }";
        }

        public override int GetHashCode()
        {
#pragma warning disable RECS0025 // Non-readonly field referenced in 'GetHashCode()'
            var hash = firstChat.GetHashCode()
                ^ validatorsDisabled.GetHashCode()
                ^ strictMode.GetHashCode();
            foreach (var chat in chats.Values) hash ^= chat.GetHashCode();
#pragma warning restore RECS0025 // Non-readonly field referenced in 'GetHashCode()'

            return hash;
        }

        public override bool Equals(Object obj)
        {
            ChatRuntime rt = (Dialogic.ChatRuntime)obj;

            if (rt.firstChat != firstChat || rt.strictMode != strictMode
                || rt.validatorsDisabled != validatorsDisabled)
            {
                return false;
            }

            if (rt.chats.Count != chats.Count) return false;

            foreach (var key in chats.Keys)
            {
                if (!(rt.ContainsKey(key) && rt.chats[key].Equals(chats[key])))
                {
                    return false;
                }
            }

            return true;
        }


        ///////////////////////////////////////////////////////////////////////


        internal string InvokeImmediate(IDictionary<string, object> globals, string label = null)
        {
            if (chats.Count < 1) throw new DialogicException("No chats");

            ResetChats(); // reset all chat cursors

            var chat = (label.IsNullOrEmpty() ? chats.Values :
                new Chat[] { this[label] }.ToList()).First();

            var result = string.Empty;

            if (chat == null || !chat.HasNext()) return result;

            chat.Run();

            Stack<Chat> resumables = new Stack<Chat>();
            HashSet<string> visited = new HashSet<string>();
            while (chat != null)
            {
                var cmd = chat.Next();
                cmd.Resolve(globals);

                ProcessSay(ref result, ref cmd, globals);

                if (cmd is Find)
                {
                    Chat next = null;
                    var toFind = cmd.ToString();
                    try
                    {
                        next = FindSync((Find)cmd, false, globals);

                        // if unfinished, save for later
                        if (chat.HasNext()) resumables.Push(chat);

                        (chat = next).Run();
                    }
                    catch (Exception ex)
                    {
                        Warn(ex);
                        return result + "\n" + toFind + " failed";
                    }

                    // make sure we haven't gotten into an infinite loop
                    if (visited.Contains(next.text))
                    {
                        return result + "\n" + toFind + " looped";
                    }

                    visited.Add(next.text);
                }

                if (!chat.HasNext()) chat = resumables.Count > 0
                    ? resumables.Pop() : null; // continue pending chats
            }

            return result.TrimLast('\n');
        }

        /// <summary>
        /// Resets the cursor for each Chat.
        /// </summary>
        internal void ResetChats()
        {
            if (!chats.IsNullOrEmpty())
            {
                foreach (var c in chats.Values) c.Reset();
            }
        }

        private void AppendChats(List<Chat> theChats)
        {
            theChats.ForEach(AddChat);
        }

        private static void ProcessSay(ref string result,
            ref Command cmd, IDictionary<string, object> globals)
        {
            if (cmd is Say)
            {
                result += cmd.Text() + "\n";
                if (cmd is Ask)
                {
                    cmd = Util.RandItem(((Ask)cmd).Options()).action;
                    cmd.Resolve(globals);
                }
            }
        }

        internal void AddChat(Chat c)
        {
            //Console.WriteLine("ADD: "+c.text);

            c.runtime = this;

            if (chats.Count < 1) firstChat = c.text;

            if (c.text.IsNullOrEmpty()) throw new ParseException
                ("Invalid chat name: " + c.text);

            if (chats.ContainsKey(c.text)) throw new ParseException
                ("Duplicate chat names: " + c.text);

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

        internal Chat FindSync(Find f, bool launchScheduler, IDictionary<string, object> globals = null)
        {
            if (f.Resolved().Count < 1) f.Resolve(globals);

            var chat = (f is Go) ? FindChatByLabel(f.Text()) : DoFind(f, globals);
            if (chat == null) throw new FindException(f);
            if (launchScheduler) scheduler.Launch(chat);
            return chat;
        }

        internal void FindAsync(Find f, IDictionary<string, object> globals = null)
        {
            //if (f is Go) // no need for a thread here ?
            //{
            //    scheduler.Launch(FindChatByLabel(f.Text()));
            //    return;
            //}

            if (f.Resolved().Count < 1) f.Resolve(globals); // tmp

            (searchThread = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                //Chat chat = DoFind(f, globals);
                var chat = (f is Go) ? FindChatByLabel(f.Text()) : DoFind(f, globals);

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
            var resolved = f.Resolved();
            foreach (var val in resolved.Values)
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


        ///////////////////////////// testing /////////////////////////////////


        internal void AddFindListener(Action<Chat> callback) // ?
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
            return search.Find(Chats(), cdict, parent, globals);
        }

        internal Chat DoFind(Find f, IDictionary<string, object> globals = null)
        {
            if (f.Resolved().Count == 0) f.Resolve(globals);
            return search.Find(Chats(), ToConstraintMap(f), f.parent, globals);
        }

        internal List<Chat> DoFindAll(Chat parent, params Constraint[] constraints)
        {
            return DoFindAll(parent, null, constraints);
        }

        internal List<Chat> DoFindAll(Chat parent,
            IDictionary<string, object> globals, params Constraint[] constraints)
        {
            return search.FindAll(Chats(), constraints.ToList(), parent, globals);
        }

        internal List<Chat> DoFindAll(Find f, IDictionary<string, object> globals)
        {
            if (f.Resolved().Count == 0) f.Resolve(globals);
            return search.FindAll(Chats(), ToList(f.Resolved()), f.parent, globals);
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

            this.chat.Complete();

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