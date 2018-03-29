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
        public static string LOG_FILE;// = "../../../dialogic/dia.log";

        public static string CHAT_FILE_EXT = ".gs";

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

        public IUpdateEvent Update(IDictionary<string, object> globals, ref EventArgs ge)
        {
            return ge != null ? appEvents.OnEvent(ref ge, globals) : chatEvents.OnEvent(globals);
        }

        public void Run(string chatLabel = null)
        {
            if (chats.IsNullOrEmpty()) throw new Exception("No chats found");

            var first = chatLabel != null ? FindChat(chatLabel) : chats[0];
            scheduler.Start(first);
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

        public List<Chat> Chats()
        {
            return chats;
        }


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
            scheduler.Finish(false);             // branching, mark as done
            scheduler.Suspend();

            int ts = Util.Millis();
            (searchThread = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                Chat chat = (finder is Go) ? FindChat(((Go)finder).Text) :
                    this.DoFind(finder, globals);

                if (chat == null) throw new FindException(finder);

                scheduler.Start(chat);

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
            f.Realize(globals); // tmp: possibly redundant
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
            f.Realize(globals);  // tmp: possibly redundant
            return FuzzySearch.FindAll(chats, ToList(f.realized), f.parent, globals);
        }

        // end testing -------------------------------------------


        private bool Logging()
        {
            return LOG_FILE != null;
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

    /// <summary>
    /// Handles launching, suspending and resuming Chats.
    /// States:
    ///   Chat-Running:   chat!=null,net>-1 -> running Chat commands
    ///   Chat-Suspended: chat!=null,net=-1 -> waiting (either on a Wait or Prompt)
    ///   Waiting:        chat=null, net=-1 -> waiting (on a SuspendEvent, or all Chats done)
    /// </summary>
    internal class ChatScheduler
    {
        internal enum State { RUNNING, WAITING, SUSPENDED }; // unused

        internal State state;
        internal Chat current;
        internal Ask prompt;
        internal ChatRuntime runtime;
        internal Stack<Chat> resumables;
        internal UpdateEvent chatEvent;
        internal bool debugCycle = true;
        internal int nextEventTime;

        internal ChatScheduler(ChatRuntime runtime)
        {
            this.runtime = runtime;
            this.resumables = new Stack<Chat>();
            Trigger(State.WAITING);
        }

        internal bool Ready()
        {
            return current != null && nextEventTime > -1
                && Util.Millis() >= nextEventTime;
        }

        internal void Start(string label)
        {
            var next = runtime.FindChat(label);
            if (next == null) throw new DialogicException
                ("No Chat exists with label: " + label);
            Start(runtime.FindChat(label));
        }

        internal void Start(Chat next)
        {
            if (next == null) Console.WriteLine
                ("[WARN] Attempt to start null chat");

            Trigger(State.RUNNING, next, false);
        }

        internal void Suspend()
        {
            if (current == null)
            {
                Console.WriteLine
                ("[WARN] Attempt to suspend null chat");
            }
            else
            {
                resumables.Push(current);
                Console.WriteLine("PUSH: " + current.Text + " " + resumables.Count + " total");
            }

            Trigger(State.SUSPENDED, current);
        }

        internal void Resume()
        {
            if (current != null)                    // resume suspended current
            {
                Trigger(State.RUNNING, current, true);
            }
            else if (resumables.IsNullOrEmpty())  // nothing to resume
            {
                Console.WriteLine("[WARN] No Chat to resume...");
                Trigger(State.WAITING);
            }
            else                                // resume from stack
            {
                Trigger(State.RUNNING, resumables.Pop(), true);
            }
        }

        internal void Finish(bool attemptToResume)
        {
            if (current == null) throw new DialogicException
                ("Invalid attempt to finish a null Chat");
            
            if (debugCycle) Console.WriteLine("<#" + current.Text + "-finished>");

            Trigger(State.WAITING, null, attemptToResume && current.resumeAfterInterrupting);
        }

        internal void Clear()
        {
            resumables.Clear(); // a ClearEvent
        }

        private void Trigger(State s, Chat c = null, bool resume = false) // unused
        {
            switch (s)
            {
                case State.RUNNING:
                    current = c;
                    // make sure we aren't running something still on the stack
                    if (resumables.Count > 0 && resumables.Peek() == c)
                    {
                        Console.WriteLine("POP: "+c.Text+ " "+(resumables.Count-1)+" remaining");
                        resumables.Pop();
                    }
                    c.Run(resume);
                    nextEventTime = Util.Millis();
                    if (debugCycle) Console.WriteLine("<#" + current.Text 
                        + (resume ? "-resumed>" : "-started>"));
                    break;

                case State.WAITING:
                    current = null;
                    nextEventTime = -1;
                    if (debugCycle) Console.WriteLine("<waiting>");
                    if (resume)
                    {
                        Console.WriteLine("Attempting to resume after Finish");
                        this.Resume();
                    }
                    break;

                case State.SUSPENDED:
                    nextEventTime = -1;
                    if (c != null)
                    {
                        current = c;
                        if (debugCycle) Console.WriteLine
                            ("<#" + current.Text + "-suspended>");
                    } 
                    break;
            }
        }
    }

    /*internal class ChatSchedulerOrig
    {

        internal Chat chat;
        internal Ask prompt;
        internal ChatRuntime runtime;
        internal Stack<Chat> resumables;
        internal UpdateEvent chatEvent;
        internal bool debugCycle = true;
        internal int nextEventTime;

        internal ChatSchedulerOrig(ChatRuntime runtime)
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
            (chat = next).Run();
            nextEventTime = Util.Millis();
            chatEvent = new UpdateEvent(new Dictionary<string, object>(){
                { Meta.TYPE, chat.TypeName() },
                { Meta.TEXT, chat.Text },
                { Meta.STATE, "started" },
            });
        }

        internal void Suspend()
        {
            if (chat != null) // Try to suspend a running Chat
            {
                if (!chat.interruptable)
                {
                    Console.WriteLine("Cannot interrupt #" + chat.Text + "!");
                    return;
                }
                resumables.Push(chat);
            }
            else { } // a SuspendEvent

            nextEventTime = -1;
        }

        internal void Clear()
        {
            resumables.Clear(); // a ClearEvent
        }

        internal void Resume()
        {
            if (chat != null && nextEventTime != -1)
            {
                throw new DialogicException("Cannot resume while Chat#"
                    + chat.Text + " is active and running");
            }

            if (chat == null)
            {
                if (resumables.IsNullOrEmpty())
                {
                    Console.WriteLine("No Chat to resume... Waiting");
                    return;
                }

                var last = resumables.Pop();
                (chat = last).Run(false);
            }
            else
            {
                // resuming a paused-chat
            }

            if (debugCycle) Console.WriteLine("<#" + chat.Text + "-resumed>");

            nextEventTime = Util.Millis();
        }

        internal void Completed(bool attemptToResume)
        {
            if (debugCycle) Console.WriteLine("<#" + chat.Text + "-finished>");

            if (resumables.Count > 0 && chat == resumables.Peek())
            {
                resumables.Pop(); // remove from stack
            }

            // if false, we don't resume after finishing
            var resumeAfter = attemptToResume && chat.resumeAfterInterrupting;

            this.chat = null;

            if (resumeAfter) Resume();
        }

        internal bool Ready()
        {
            return chat != null && nextEventTime > -1
                && Util.Millis() >= nextEventTime;
        }
    }*/
}