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

        private bool logInitd;
        private int nextEventTime;

        private Thread searchThread;
        private List<Chat> chats;
        private Find findDelegate;
        private ChatScheduler scheduler;
        private ChatParser parser;

        private List<IActor> actors;
        private List<Func<Command, bool>> validators;

        public ChatRuntime(List<IActor> actors) : this(null, actors) { }

        public ChatRuntime(List<Chat> chats, List<IActor> actors = null)
        {
            this.chats = chats;
            this.parser = new ChatParser(this);
            this.scheduler = new ChatScheduler(this);
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

            var first = chatLabel != null ? FindByName(chatLabel) : chats[0];
            scheduler.Launch(first);
        }

        public List<Chat> Chats()
        {
            return chats;
        }

        public IUpdateEvent Update(IDictionary<string, object> globals, ref EventArgs ge)
        {
            return ge != null ? HandleGameEvent(ref ge, globals) : HandleChatEvent(globals);
        }


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

        // ----------------------------------------------------------------

        internal List<Func<Command, bool>> Validators()
        {
            return validators;
        }

        internal ChatParser Parser()
        {
            return parser;
        }

        internal bool ActorExists(string name)
        {
            foreach (var actor in actors)
            {
                if (actor.Name() == name) return true;
            }
            return false;
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


        private IUpdateEvent HandleGameEvent(ref EventArgs ge, IDictionary<string, object> globals)
        {
            if (ge is IUserEvent) return UserActionHandler(ref ge, globals);
            if (ge is ISuspend) return SuspendHandler(ref ge, globals);
            if (ge is IResume) return ResumeHandler(ref ge, globals);
            if (ge is IChoice) return ChoiceHandler(ref ge, globals);
            if (ge is IClear) return ClearHandler(ref ge, globals);

            throw new DialogicException("Unexpected event-type: " + ge.GetType());
        }

        private IUpdateEvent UserActionHandler(ref EventArgs ge, IDictionary<string, object> globals)
        {
            IUserEvent ue = (IUserEvent)ge;
            var label = ue.GetEventType();
            ge = null;

            scheduler.Suspend();
            scheduler.Launch("#On" + label + "Event");
            return null;
        }

        private IUpdateEvent SuspendHandler(ref EventArgs ge, IDictionary<string, object> globals)
        {
            ge = null;
            scheduler.Suspend();
            return null;
        }

        private IUpdateEvent ClearHandler(ref EventArgs ge, IDictionary<string, object> globals)
        {
            ge = null;
            scheduler.Clear();
            return null;
        }

        private IUpdateEvent ResumeHandler(ref EventArgs ge, IDictionary<string, object> globals)
        {
            IResume ir = (IResume)ge;
            var label = ir.ResumeWith();
            ge = null;

            if (String.IsNullOrEmpty(label))
            {
                nextEventTime = scheduler.Resume();
            }
            else if (label.StartsWith("#", Util.IC))
            {
                scheduler.Launch(label);
            }
            else // else, parse as FIND meta data
            {
                if (findDelegate == null) findDelegate = new Find();

                try
                {
                    findDelegate.Init(label);
                }
                catch (ParseException e)
                {
                    throw new RuntimeParseException(e);
                }

                FindAsync(findDelegate, globals);
            }

            return null;
        }

        private IUpdateEvent ChoiceHandler(ref EventArgs ge, IDictionary<string, object> globals)
        {
            IChoice ic = (IChoice)ge;
            var idx = ic.GetChoiceIndex();
            ge = null;

            if (idx < 0 || idx >= scheduler.prompt.Options().Count)
            {
                // bad index, so reprompt for now
                Console.WriteLine("Invalid index " + idx + ", reprompting\n");
                scheduler.prompt.Realize(globals); // re-realize
                return new UpdateEvent(scheduler.prompt);
            }
            else
            {
                Opt opt = scheduler.prompt.Selected(idx);
                //opt.Realize(globals); // not needed

                if (opt.action != Command.NOP)
                {
                    FindAsync((Find)opt.action); // find next
                }
                else
                {
                    scheduler.chat = scheduler.prompt.parent; // just continue
                }
                return null;
            }
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
                    // so we check the stack for something to resume

                    Console.WriteLine("<#" + scheduler.chat.Text + "-finished>");

                    int nextEventMs = scheduler.Completed();

                    if (nextEventMs > -1)
                    {
                        Console.WriteLine("<#" + scheduler.chat.Text + "-resumed>");
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
            var next = runtime.FindByName(label);
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