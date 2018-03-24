using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

namespace Dialogic
{
    public class ChatRuntime
    {
        public static string logFile;// = "../../../dialogic/dia.log";

        public static string CHAT_FILE_EXT = ".gs";

        public static IDictionary<string, Type> TypeMap
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

        private bool logInitd;
        private int nextEventTime;

        private Thread searchThread;
        private List<Chat> chats;
        private Find findDelegate;
        private ChatScheduler scheduler;

        private List<IActor> actors;
        private List<Func<Command, bool>> validators;

        public void ParseFile(string fileOrFolder)
        {
            string[] files = Directory.Exists(fileOrFolder) ?
                files = Directory.GetFiles(fileOrFolder, '*' + ChatRuntime.CHAT_FILE_EXT) :
                files = new string[] { fileOrFolder };

            this.chats = new List<Chat>();
            foreach (var f in files)
            {
                var text = File.ReadAllText(f);
                var stripped = ChatParser.StripComments(text);
                var parsed = new ChatParser(validators.ToArray()).Parse(stripped);
                parsed.ForEach(c => chats.Add(c));
            }
        }

        public ChatRuntime(List<IActor> speakers = null) : this(null, speakers) { }

        public ChatRuntime(List<Chat> chats, List<IActor> speakers = null)
        {
            this.chats = chats;
            this.actors = speakers;
            this.scheduler = new ChatScheduler(this);
            this.validators = new List<Func<Command, bool>>();
            ConfigureSpeakers();
        }

        private void ConfigureSpeakers()
        {
            if (actors.IsNullOrEmpty()) return;

            actors.ForEach(s =>
            {
                var cmds = s.Commands();
                if (!cmds.IsNullOrEmpty())
                {
                    foreach (var cmd in cmds)
                    {
                        TypeMap.Add(cmd.label, cmd.type);
                    }
                }
                var val = s.Validator();
                if (val != null)
                {
                    validators.Add(val);
                }
            });
        }

        public void Run(string chatLabel = null)
        {
            if (chats.IsNullOrEmpty()) throw new Exception("No chats found");

            scheduler.StartNew((chatLabel != null) ? FindByName(chatLabel) : chats[0]);
        }

        public List<Chat> Chats()
        {
            return chats;
        }

        public IUpdateEvent Update(IDictionary<string, object> globals, ref EventArgs ge)
        {
            return ge != null ? HandleGameEvent(ref ge, globals) : HandleChatEvent(globals);
        }

        private IUpdateEvent HandleGameEvent(ref EventArgs ge, IDictionary<string, object> globals)
        {
            if (ge is ISuspend) return SuspendHandler(ref ge, globals);
            if (ge is IResume) return ResumeHandler(ref ge, globals);
            if (ge is IChoice) return ChoiceHandler(ref ge, globals);
            if (ge is IClear) return ClearHandler(ref ge, globals);
            throw new DialogicException("Unexpected event-type: " + ge.GetType());
        }

        private IUpdateEvent SuspendHandler(ref EventArgs ge, IDictionary<string, object> globals)
        {
            ge = null;
            scheduler.PauseCurrent();
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
                nextEventTime = scheduler.ResumeLast();
            }
            else if (label.StartsWith("#", Util.IC))
            {
                scheduler.StartNew(FindByName(label));
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
                    LogCommand(cmd);

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
                            scheduler.PauseCurrent();
                        }
                        else if (cmd is Ask)
                        {
                            scheduler.prompt = (Ask)cmd;
                            scheduler.PauseCurrent(); // wait for ChoiceEvent
                        }
                        else
                        {
                            ComputeNextEventTime(cmd); // compute delay
                        }

                        return new UpdateEvent(cmd); // fire event
                    }
                    else if (cmd is Find)
                    {
                        FindAsync((Find)cmd);
                    }
                }
            }
            return null;
        }

        internal void SetNextEventTime(int millis)
        {
            nextEventTime = millis;
        }

        internal void ComputeNextEventTime(Command cmd)
        {
            SetNextEventTime(cmd.DelayMs >= 0 ? Util.Millis()
                + cmd.ComputeDuration() : Int32.MaxValue);
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

        public void FindAsync(Find finder, IDictionary<string, object> globals = null)
        {
            scheduler.PauseCurrent();

            int ts = Util.Millis();
            (searchThread = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                Chat chat = null;

                if (finder is Go)
                {
                    chat = FindByName(((Go)finder).Text);
                }
                else
                {
                    chat = FuzzySearch.Find(finder, chats, globals);
                    //Console.WriteLine("Found " + chat.Text + " in " + Util.Millis(ts) + "ms");
                }

                if (chat == null) throw new FindException(finder);

                scheduler.StartNew(chat);

            })).Start();
        }

        // testing only ------------------------------------------

        internal Chat Find(Find f, IDictionary<string, object> globals)
        {
            return FuzzySearch.Find(f, chats, globals);
        }

        internal Chat Find(Constraint[] constraints, IDictionary<string, object> globals)
        {
            return FuzzySearch.Find(new Find(constraints), chats, globals);
        }

        internal Chat Find(params Constraint[] constraints)
        {
            return FuzzySearch.Find(new Find(constraints), chats, null);
        }

        internal Chat Find(Constraint constraint, IDictionary<string, object> globals)
        {
            return FuzzySearch.Find(new Find(constraint), chats, globals);
        }

        internal List<Chat> FindAll(Find f, IDictionary<string, object> globals)
        {
            return FuzzySearch.FindAll(f, chats, globals);
        }

        internal List<Chat> FindAll(Constraint[] constraints, IDictionary<string, object> globals)
        {
            return FuzzySearch.FindAll(new Find(constraints), chats, globals);
        }

        internal List<Chat> FindAll(params Constraint[] constraints)
        {
            return FuzzySearch.FindAll(new Find(constraints), chats, null);
        }

        internal List<Chat> FindAll(Constraint constraint, IDictionary<string, object> globals = null)
        {
            return FuzzySearch.FindAll(new Find(constraint), chats, globals);
        }

        private bool Logging()
        {
            return logFile != null;
        }

        public void LogCommand(Command c)
        {
            if (logFile == null) return;

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
        public Chat chat;
        public Ask prompt;
        public ChatRuntime runtime;
        public Stack<Chat> resumables;

        public ChatScheduler(ChatRuntime runtime)
        {
            this.runtime = runtime;
            this.resumables = new Stack<Chat>();
        }

        public void StartNew(Chat next)
        {
            if (next != null) resumables.Push(next);
            (chat = next).Run();
        }

        public void PauseCurrent()
        {
            if (chat != null) resumables.Push(chat);
            chat = null;
        }

        public void Clear()
        {
            resumables.Clear();
        }

        public int ResumeLast(bool mustBeResumable = false)
        {
            if (resumables.IsNullOrEmpty())
            {
                throw new DialogicException("No Chat to resume");
            }

            if (chat != null)
            {
                throw new DialogicException("Cannot resume chat" +
                    " while Chat#" + chat.Text + " is active");
            }

            var last = resumables.Pop();
            while (mustBeResumable && !last.IsResumable())
            {
                if (resumables.Count < 1) throw new DialogicException
                    ("No resumable Chat found on stack");

                last = resumables.Pop();
            }

            (chat = last).Run(false);

            return Util.Millis();
        }


    }
}