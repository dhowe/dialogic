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
        public static string logFile;// = "../../../dialogic/dia.log";

        public static string CHAT_FILE_EXT = ".gs";

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

        private bool logInitd;
        private Thread searchThread;
        private List<Chat> chats;
        private ChatParser parser;
        private List<IActor> actors;
        private AppEventHandler appEvents;
        private List<Func<Command, bool>> validators;

        public ChatRuntime(List<IActor> actors) : this(null, actors) { }

        public ChatRuntime(List<Chat> chats, List<IActor> actors = null)
        {
            this.chats = chats;
            this.parser = new ChatParser(this);
            this.scheduler = new ChatScheduler(this);
            this.appEvents = new AppEventHandler(this);
            RegisterActors(actors);
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

            var first = chatLabel != null ? FindChat(chatLabel) : chats[0];
            scheduler.Launch(first);
        }

        public List<Chat> Chats()
        {
            return chats;
        }

        public IUpdateEvent Update(IDictionary<string, object> globals, ref EventArgs ge)
        {
            return ge != null ? appEvents.OnEvent(ref ge, globals) : HandleChatEvent(globals);
        }

        public Chat FindChat(string label)
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

                Chat chat = (finder is Go) ? FindChat(((Go)finder).Text) :
                    this.DoFind(finder, globals);

                if (chat == null) throw new FindException(finder);

                scheduler.Launch(chat);

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

        private IUpdateEvent HandleChatEvent(IDictionary<string, object> globals)
        {
            Command cmd = null;

            if (scheduler.chat != null && Util.Millis() >= nextEventTime)
            {
                cmd = scheduler.chat.Next();

                if (cmd != null)
                {
                    cmd.Realize(globals);

                    if (logFile != null) WriteToLog(cmd);

                    if (cmd is ISendable)
                    {
                        if (cmd.GetType() == typeof(Wait))
                        {
                            // just pause internally, no event needs to be fired
                            if (cmd.DelayMs != Util.INFINITE)
                            {
                                ComputeNextEventTime(cmd);
                                return null;
                            }
                            scheduler.Suspend();
                        }
                        else if (cmd is Ask)
                        {
                            scheduler.prompt = (Ask)cmd;
                            scheduler.Suspend();         // wait on ChoiceEvent
                        }
                        else
                        {
                            ComputeNextEventTime(cmd); // compute delay
                        }

                        return new UpdateEvent(cmd); // fire event
                    }
                    else if (cmd is Find)
                    {
                        FindAsync((Find)cmd);      // find next
                    }
                }
                else
                {
                    // Here the Chat has completed without redirecting 
                    // so we check the stack for a chat to resume

                    //Console.WriteLine("<#" + scheduler.chat.Text + "-finished>");
                    int nextEventMs = scheduler.Completed();

                    if (nextEventMs > -1)
                    {
                        //Console.WriteLine("<#" + scheduler.chat.Text + "-resumed>");
                        nextEventTime = nextEventMs;
                    }
                }
            }
            return null;
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

        public void WriteToLog(Command c)
        {
            if (!logInitd)
            {
                logInitd = true;
                File.WriteAllText(logFile, "============\n");
            }

            using (StreamWriter w = File.AppendText(logFile))
            {
                var now = DateTime.Now.ToString("HH:mm:ss.fff", CultureInfo.InvariantCulture);
                w.WriteLine(now + "\t" + c + " @" + Util.Millis());
            }
        }
    }

    internal class ChatScheduler
    {
        internal Chat chat;
        internal Ask prompt;
        internal ChatRuntime runtime;
        internal Stack<Chat> resumables;

        internal ChatScheduler(ChatRuntime runtime)
        {
            this.runtime = runtime;
            this.resumables = new Stack<Chat>();
        }

        internal void Launch(string label)
        {
            var next = runtime.FindChat(label);
            if (next == null) throw new DialogicException
                ("Unable to find Chat: '" + label + "'");
            Launch(next);
        }

        internal void Launch(Chat next)
        {
            // TODO: when Chats branch we may want to consider them
            // finished and remove them from the stack, leaving only
            // those that have been explictly interrupted ...
            (chat = next).Run();
        }

        internal void Suspend()
        {
            if (chat != null)
            {
                if (!chat.interruptable)
                {
                    Console.WriteLine("Cannot interrupt #" + chat.Text + "!");
                    return;
                }
                resumables.Push(chat);
            }
            chat = null;
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

            if (chat != null) throw new DialogicException("Cannot resume"
                + " while Chat#" + chat.Text + " is active");

            var last = resumables.Pop();
            (chat = last).Run(false);

            return Util.Millis();
        }

        internal int Completed()
        {
            if (resumables.Count > 0 && chat == resumables.Peek())
            {
                resumables.Pop(); // remove from stack
            }

            // if false, we don't resume after finishing
            var resumesAfter = chat.resumeAfterInterrupting;

            this.chat = null;

            return resumesAfter ? this.Resume() : -1;
        }
    }
}