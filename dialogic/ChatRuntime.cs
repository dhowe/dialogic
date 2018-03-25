using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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

        public ChatRuntime(List<IActor> speakers = null) : this(null, speakers) { }

        public ChatRuntime(List<Chat> chats, List<IActor> speakers = null)
        {
            this.chats = chats;
            this.actors = speakers;
            this.scheduler = new ChatScheduler(this);
            this.validators = new List<Func<Command, bool>>();
            ConfigureSpeakers();
        }

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

        public void Run(string chatLabel = null)
        {
            if (chats.IsNullOrEmpty()) throw new Exception("No chats found");

            scheduler.Launch((chatLabel != null) ? FindByName(chatLabel) : chats[0]);
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

        internal void FindAsync(Find finder, IDictionary<string, object> globals = null)
        {
            scheduler.Suspend();

            int ts = Util.Millis();
            (searchThread = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                Chat chat = (finder is Go) ? FindByName(((Go)finder).Text) :
                    FuzzySearch.Find(finder, chats, globals);

                if (chat == null) throw new FindException(finder);

                scheduler.Launch(chat);

            })).Start();
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

            Chat chat = FindByName("#On"+label+"Event");
            if (chat == null) throw new DialogicException("No Find for event: "+label);
            scheduler.Launch(chat);
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
                scheduler.Launch(FindByName(label));
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
                Console.WriteLine("EVENT @" + Util.Millis());

                cmd = scheduler.chat.Next();
                if (cmd != null)
                {
                    cmd.Realize(globals);
                    WriteToLog(cmd);

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
                            scheduler.Suspend(); // wait for ChoiceEvent
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
                else {
                    // Here the Chat has completed without redirecting 
                    // so we check the stack for something to resume

                    Console.WriteLine("CHAT COMPLETE #"+scheduler.chat.Text+" @"+Util.Millis());
                    scheduler.chat = null;
                    var tmp = scheduler.Resume();
                    if (tmp > -1) Console.WriteLine("CHAT #"+scheduler.chat+" resumed @" + tmp);
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

        // testing only ------------------------------------------

        internal Chat DoFind(Chat parent, IDictionary<string, object> globals, params Constraint[] constraints)
        {
            IDictionary<Constraint, bool> cdict = new Dictionary<Constraint, bool>();
            foreach (var c in constraints) cdict.Add(c, c.IsRelaxable());
            return FuzzySearch.DoFind(chats, cdict, parent, globals);
        }

        internal Chat DoFind(Find f, IDictionary<string, object> globals = null)
        {
            f.Realize(globals); // possibly redundant
            return FuzzySearch.DoFind(chats, ToConstraintMap(f), f.parent, globals);
        }

        internal List<Chat> DoFindAll(Chat parent, IDictionary<string, object> globals, params Constraint[] constraints)
        {
            return FuzzySearch.DoFindAll(chats, constraints, parent, globals);
        }

        internal List<Chat> DoFindAll(Find f, IDictionary<string, object> globals = null)
        {
            f.Realize(globals);  // possibly redundant
            return FuzzySearch.DoFindAll(chats, ToList(f.realized), f.parent, globals);
        }

        // testing old ------------------------------------------

        /*internal Chat Find(Find f, IDictionary<string, object> globals)
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
        }*/

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

        public void Launch(Chat next)
        {
            (chat = next).Run();
        }

        public void Suspend()
        {
            if (chat != null) resumables.Push(chat);
            chat = null;
        }

        public void Clear()
        {
            resumables.Clear();
        }

        public int Resume(bool mustBeResumable = false)
        {
            if (resumables.IsNullOrEmpty())
            {
                Console.WriteLine("No Chat to resume... Waiting");
                return -1;
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