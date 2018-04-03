using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Dialogic
{
    /// <summary>
    /// Superclass for all Commands. When created by the parser, the default constructor is called first,
    /// followed by Init(text,label,meta), followed by any app-specific validators, followed by Validate().
    /// </summary>
    public abstract class Command : Meta
    {
        internal static readonly Command NOP = new NoOp();
        protected static int IDGEN = 0;

        protected internal IActor actor { get; protected set; }
        protected internal double delay { get; protected set; }

        protected internal string text;

        protected internal readonly int id;
        protected internal Chat parent;

        protected Command()
        {
            this.delay = 0;
            this.id = ++IDGEN;
            this.realized = new Dictionary<string, object>();
        }

        protected double Delay()
        {
            return delay;
        }

        protected Command Delay(double seconds)
        {
            delay = seconds;
            SetMeta(Meta.DELAY, seconds.ToString());
            return this;
        }

        internal static Command Create(Type type, string text, string label, string[] metas)
        {
            //Console.WriteLine("'"+type + "' '"+text+ "' '"+ label+"' "+metas.Stringify());
            Command cmd = (Command)Activator.CreateInstance(type);
            cmd.Init(text, label, metas);
            return cmd;
        }

        public virtual void Init(string text, string label, string[] metas)
        {
            this.text = text.Length > 0 ? text : label;
            ParseMeta(metas);
            //HandleMetaTiming();
            //Validate();
        }

        protected void ValidateTextLabel()
        {
            if (String.IsNullOrEmpty(text)) throw BadArg
                (TypeName().ToUpper() + " requires a literal #Label");

            Util.ValidateLabel(ref text);
        }

        public string Text(bool real = false)
        {
            return real ? (string)realized[Meta.TEXT] : text;
        }

        /// <summary>
        /// In multi-speaker environments, SAY commands (also ASK, DO, and others) can be assigned to a specific Actor, which may or may not be the default actor. If no actor is specified, then the default actor is used. To specify a different actor than the default, add the actor's name to the metadata, or prepend it followed by a colon to the command.
        /// </summary>
        /// <returns>The actor.</returns>
        public IActor Actor()
        {
            return actor;
        }

        public Command Actor(IActor actor)
        {
            this.actor = actor;
            return this;
        }

        public Command Actor(ChatRuntime rt, string actorName)
        {
            return Actor(rt.FindActorByName(actorName));
        }

        /// <summary>
        /// Validates this instance by verifying that (at least) it has all
        /// required metadata values (no-op here)
        /// </summary>
        protected internal virtual Command Validate()
        {
            return this;
        }

        public virtual string TypeName()
        {
            return this.GetType().Name;
        }

        public virtual void Realize(IDictionary<string, object> globals)
        {
            realized.Clear();

            RealizeMeta(globals);

            if (this is ISendable)
            {
                realized[Meta.TEXT] = Realizer.Do(text, globals);
                realized[Meta.TYPE] = TypeName();
                if (this is IAssignable)
                {
                    realized[Meta.ACTOR] = Actor().Name();
                }
            }
        }

        protected virtual void RealizeMeta(IDictionary<string, object> globals)
        {
            if (HasMeta())
            {
                IEnumerable sorted = null; // TODO: cache these key-sorts ?

                foreach (KeyValuePair<string, object> pair in meta)
                {
                    object val = pair.Value;

                    if (val is string)
                    {
                        string tmp = (string)val;

                        if (tmp.IndexOf('$') > -1)
                        {
                            if (sorted == null) sorted = Util.SortByLength(globals.Keys);
                            foreach (string s in sorted)
                            {
                                tmp = tmp.Replace("$" + s, globals[s].ToString());
                            }
                        }

                        val = tmp;
                    }
                    else if (!(val is Constraint))
                    {
                        throw new DialogicException("Unexpected meta-value: " + val + " " + val.GetType());
                    }

                    realized[pair.Key] = val;
                }
            }
        }

        protected Exception BadArg(string msg)
        {
            throw new ParseException(msg);
        }

        protected internal virtual int ComputeDuration()
        {
            return Util.ToMillis(delay);
        }

        public override string ToString()
        {
            return (TypeName().ToUpper() + " "
                + text).Trim() + (" " + MetaStr()).TrimEnd();
        }
    }

    public class NoOp : Command //@cond unused 
    { } //@endcond

    /// <summary>
    /// The Say command allows a character to speak. It is the default command. If a line does not start with a command, than it is assumed to be a Say command. It accepts free text, optionally followed by metadata, which can be used to control various display parameters. In multi-speaker environments, Say commands (also Ask, Do, and other IAssignables) can be assigned to a specific Actor, which may or may not be the default actor. If no actor is specified, then the default actor is used. To specify a different actor than the default, add the actor's name to the metadata, or prepend it followed by a colon to the command.
    /// </summary>
    public class Say : Command, ISendable, IAssignable
    {
        protected string lastSpoken;

        public Say() : base()
        {
            this.delay = Util.ToMillis(Defaults.SAY_DURATION);
        }

        protected internal override Command Validate()
        {
            if (text.Length < 1) throw new ParseException("SAY requires text");
            return this;
        }

        public override void Realize(IDictionary<string, object> globals)
        {
            base.Realize(globals);
            Recombine(globals);
            lastSpoken = Text(true);
        }

        private void Recombine(IDictionary<string, object> globals)
        {
            if (IsRecombinant()) // try to say something different than last time
            {
                int tries = 0;
                while (lastSpoken == Text(true) && ++tries < 100)
                {
                    realized[Meta.TEXT] = Realizer.Do(text, globals);
                }
            }
        }

        protected bool IsRecombinant()
        {
            return text.IndexOf('|') > -1;
        }

        /**
         * Determine milliseconds to wait after sending the event, using:
         *      a. Line-length
         *      b. Meta-data modifiers
         *      c. Character mood (TODO)
         */
        protected internal override int ComputeDuration() // Config?
        {
            return Util.Round(GetTextLenScale() * GetMetaSpeedScale() * delay);
        }

        protected double GetTextLenScale()
        {
            return Util.Map(text.Length,
                Defaults.SAY_MIN_LINE_LEN, Defaults.SAY_MAX_LINE_LEN,
                Defaults.SAY_MIN_LEN_MULT, Defaults.SAY_MAX_LEN_MULT);
        }

        protected double GetMetaSpeedScale()
        {
            double val = 1.0;
            if (meta != null && meta.ContainsKey("speed"))
            {
                switch ((string)meta["speed"])
                {
                    case "fast":
                        val *= Defaults.SAY_FAST_MULT;
                        break;
                    case "slow":
                        val *= Defaults.SAY_SLOW_MULT;
                        break;
                }
            }
            return val;
        }
    }

    public class Gram : Command //@cond unused
    {
        public Grammar grammar;

        public override void Init(string text, string label, string[] metas)
        {
            //Console.WriteLine("Gram.init: " + Util.Stringify(meta)+"\n"+String.Join("\n", meta));
            grammar = new Grammar(String.Join("\n", metas));
        }

        public override string ToString()
        {
            return "[" + TypeName().ToUpper() + "] >\n" + grammar;
        }
    }//@endcond

    /// <summary>
    /// The Do command is used to trigger actions (animations, transitions, sounds, etc.) in the game environment. It accepts a label, optionally followed by metadata, which can be used to supply arguments to the triggered action.
    /// </summary>
    public class Do : Command, ISendable, IAssignable
    {
        protected internal override Command Validate()
        {
            ValidateTextLabel();
            return this;
        }

        public override string ToString()
        {
            return TypeName().ToUpper() + " #" + text;
        }
    }

    /// <summary>
    /// The Set command is used to create or modify a variable. Variables  generally originate from the game environment itself, but can also be created, accessed or modified within Dialogic.
    /// </summary>
    public class Set : Command // TODO: rethink
    {
        public string Value;

        public Set() : base() { }

        internal Set(string name, string value) : base() // tests only
        {
            this.text = name;
            this.Value = value;
        }

        public override void Init(string text, string label, string[] metas)
        {
            string[] parts = ParseSetArgs(text);
            this.text = parts[0];
            this.Value = parts[1];
        }

        private string[] ParseSetArgs(string s)
        {
            if (s.Length < 1) throw BadArg("ParseSetArgs");

            var pair = Regex.Split(s, @"\s*=\s*"); // TODO: compile
            if (pair.Length != 2) pair = Regex.Split(s, @"\s+");

            if (pair.Length != 2) throw BadArg("SET requires NAME and VALUE");

            if (pair[0].StartsWith("$", StringComparison.Ordinal))
            {
                pair[0] = pair[0].Substring(1); // tmp: leading $ is optional
            }

            return pair;
        }

        public override string ToString()
        {
            return TypeName().ToUpper() + " $" + text + '=' + Value;
        }
    }

    /// <summary>
    /// The Wait command is used to pause the runtime for a period of time.  It accepts a single number specifying the time to wait in seconds. If no time is specified, the system will wait until it receives a  ResumeEvent, after which it will continue with the current Chat, or the Chat specified in the event.
    /// </summary>
    public class Wait : Command, ISendable
    {
        public override void Init(string text, string label, string[] metas)
        {
            base.Init(text, label, metas);
            delay = text.Length == 0 ? DefaultDuration() : Convert.ToDouble(text);
        }

        protected virtual double DefaultDuration()
        {
            return Defaults.WAIT_DURATION;
        }
    }

    /// <summary>
    /// The ASK command is for prompting the user, and is generally followed by 
    /// one or more OPT commands. If no OPT commands are supplied, then a simple
    /// Yes/No pair is created. ASK pauses the current chat, after which it waits
    /// for a ChoiceEvent object to be returned from the application. Based upon
    /// the ChoiceEvent data (which option was selected), the chat will then go 
    /// to the next chat specified.
    /// It accepts free text, optionally followed by metadata, which can be 
    /// used to control various display parameters.
    /// </summary>
    public class Ask : Say, ISendable, IAssignable
    {
        internal int selectedIdx;

        protected List<Opt> options = new List<Opt>();

        protected internal double timeout { get; protected set; }

        public Ask()
        {
            this.delay = Defaults.ASK_DURATION;
            this.timeout = Defaults.ASK_TIMEOUT;
        }

        protected double Timeout()
        {
            return timeout;
        }

        protected Ask Timeout(double seconds)
        {
            timeout = seconds;
            SetMeta(Meta.TIMEOUT, seconds.ToString());
            return this;
        }

        public List<Opt> Options()
        {
            if (options.Count < 1) // default options
            {
                options.Add(new Opt("Yes", NOP));
                options.Add(new Opt("No", NOP));
            }
            return options;
        }

        public string JoinOptions(string delim = "\n")
        {
            var s = String.Empty;
            var opts = Options();
            for (int i = 0; i < opts.Count; i++)
            {
                s += opts[i].Text(true);
                if (i < opts.Count - 1) s += delim;
            }
            return s;
        }

        public Opt Selected()
        {
            return Options()[selectedIdx];
        }

        public Opt Selected(int i)
        {
            this.selectedIdx = i;
            if (i >= 0 && i < options.Count) return Selected();
            return null;
        }

        public void AddOption(Opt o)
        {
            options.Add(o);
        }

        // Call Realize() on text and options, then add both to realized
        public override void Realize(IDictionary<string, object> globals)
        {
            base.Realize(globals);
            Options().ForEach(o => o.Realize(globals));
            realized[Meta.TIMEOUT] = timeout.ToString();
            realized[Meta.OPTS] = JoinOptions();
        }

        public override string ToString()
        {
            string s = base.ToString();
            if (options != null) options.ForEach(o => s += "\n  " + o);
            return s;
        }
    }

    /// <summary>
    /// The Opt command is used when prompting the user, and generally follows 
    /// an Ask command.Each Opt specifies an option that the user can choose.
    /// </summary>
    public class Opt : Say
    {
        internal Command action;

        public Opt() : this(String.Empty, NOP) { }

        public Opt(string text, Command action) : base()
        {
            this.text = text;
            this.action = action;
        }

        public override void Init(string text, string label, string[] metas)
        {
            this.text = text;

            if (label.Length > 0 && !label.StartsWith(Util.LABEL_IDENT, Util.IC))
            {
                throw BadArg("OPT requires a literal #Label");
            }

            this.action = label.Length > 0 ?
                Command.Create(typeof(Go), String.Empty, label, metas) : NOP;

            //Validate();
        }

        protected internal override Command Validate()
        {
            this.action.Validate(); // validate the option
            return this;
        }

        public override string ToString()
        {
            return TypeName().ToUpper() + " " + text
                + (action is NoOp ? String.Empty : " #" + action.text);
        }
    }

    /// <summary>
    /// Find is used to do a fuzzy search for the next Chat, according to criteria that you supply. The Find command accepts any number of metadata key-value pairs specifying constraints to match. The highest scoring  Chat that does not violate any of the constraints is located, and the system then branches to this Chat. 
    /// Normally, Chats will match a constraint if they do not have the given key in their metadata or if it is present  and matches. Prepending the strict operator (!) to a key signifies that  the key MUST be both present and matching. Find also allows comparison  operators in its metadata.
    /// </summary>
    public class Find : Command
    {
        public Find() : base() { }

        internal Find(params Constraint[] cnts) : base()
        {
            this.meta = Constraint.AsDict(cnts);
        }

        internal Find Init(string metadata)
        {
            if (meta != null) meta.Clear();
            Init(null, null, metadata.Trim().TrimEnds('{', '}').Split(','));
            return this;
        }

        public override void Init(string text, string label, string[] metas)
        {
            if (!text.IsNullOrEmpty()) throw new DialogicException
                ("FIND does not accept text, only metadata");
            ParseMeta(metas);
        }

        public override void Realize(IDictionary<string, object> globals)
        {
            realized.Clear();
            RealizeMeta(globals); // only realized meta
        }

        ///  All Find commands must have a 'staleness' value
        protected internal override Command Validate()
        {
            SetMeta(new Constraint(Operator.LT, Meta.STALENESS,
                    Defaults.FIND_STALENESS.ToString()), true);

            return this;
        }

        protected internal override void SetMeta(string key, object val, bool onlyIfNotSet = false)
        {
            if (val is string || val.IsNumber())
            {
                val = new Constraint(key, val.ToString());
            }
            base.SetMeta(key, val, onlyIfNotSet);
        }

        protected override void ParseMeta(string[] pairs)
        {
            for (int i = 0; pairs != null && i < pairs.Length; i++)
            {
                if (String.IsNullOrEmpty(pairs[i])) throw new ParseException
                    ("Invalid query: {empty}");

                Match match = RE.FindMeta.Match(pairs[i]);
                //Util.ShowMatch(match);

                if (match.Groups.Count != 4) throw new ParseException
                    ("Invalid query: '" + pairs[i] + "'");

                string key = match.Groups[1].Value;
                ConstraintType ctype = ConstraintType.Soft;

                if (Util.TrimFirst(ref key, Constraint.TypeSetChar))
                {
                    ctype = ConstraintType.Hard;
                    if (Util.TrimFirst(ref key, '!'))
                    {
                        ctype = ConstraintType.Absolute;
                    }
                }

                if (meta == null) meta = new Dictionary<string, object>();

                meta.Add(key, new Constraint(Operator.FromString
                    (match.Groups[2].Value), key, match.Groups[3].Value, ctype));
            }
        }

        protected override string MetaStr()
        {
            string s = String.Empty;
            if (HasMeta())
            {
                s += "{";
                foreach (var ct in meta.Values) s += ct + ",";
                s = s.Substring(0, s.Length - 1) + "}";
            }
            return s;
        }

        public override string ToString()
        {
            return (TypeName().ToUpper() + " " + MetaStr()).Trim();
        }
    }

    /// <summary>
    /// The Go command accepts a single label used to specify the next Chat to 
    /// jump to. When a Go command is executed, the current Chat is marked as
    /// finished and execution switches to the Chat specified.
    /// </summary>
    public class Go : Find
    {
        public override void Init(string text, string label, string[] metas)
        {
            this.text = text.Length > 0 ? text : label;
            //Validate();
        }

        public new Go Init(string label)
        {
            Init(String.Empty, label, null);
            return this;
        }

        protected internal override Command Validate()
        {
            ValidateTextLabel();
            return this;
        }

        public override string ToString()
        {
            return TypeName().ToUpper() + " #" + text;
        }
    }

    /// <summary>
    /// Superclass for all Commands that may be assigned metadata
    /// </summary>
    public class Meta
    {
        public const string TYPE = "__type__";
        public const string TEXT = "__text__";
        public const string OPTS = "__opts__";

        public const string ACTOR = "actor";
        public const string DELAY = "delay";
        public const string TIMEOUT = "timeout";
        public const string STALENESS = "staleness";
        public const string INTERRUPTIBLE = "interruptible";
        public const string STALENESS_INCR = "stalenessIncr";
        public const string RESUME_AFTER_INT = "resumeAfterInt";

        protected internal IDictionary<string, object> meta, realized;

        protected internal virtual bool HasMeta()
        {
            return meta != null && meta.Count > 0;
        }

        protected internal bool HasMeta(string key)
        {
            return meta != null && meta.ContainsKey(key);
        }

        protected internal object GetMeta(string key, object defaultVal = null)
        {
            return meta != null && meta.ContainsKey(key) ? meta[key] : defaultVal;
        }

        protected internal object GetRealized(string key, object defaultVal = null)
        {
            return realized.ContainsKey(key) ? realized[key] : defaultVal;
        }

        protected internal void SetMeta(Constraint constraint, bool onlyIfNotSet = false)
        {
            this.SetMeta(constraint.name, constraint, onlyIfNotSet);
        }

        protected internal virtual void SetMeta(string key, object val, bool onlyIfNotSet = false)
        {
            if (meta == null) meta = new Dictionary<string, object>();
            if (onlyIfNotSet && HasMeta(key)) return;
            meta[key] = val;
        }

        protected virtual string MetaStr()
        {
            string s = String.Empty;
            if (HasMeta())
            {
                s += "{";
                foreach (var key in meta.Keys) s += key + "=" + meta[key] + ",";
                s = s.Substring(0, s.Length - 1) + "}";
            }
            return s;
        }

        protected virtual void ParseMeta(string[] pairs)
        {
            for (int i = 0; pairs != null && i < pairs.Length; i++)
            {
                //Console.WriteLine(i+") "+pairs[i]);
                if (!string.IsNullOrEmpty(pairs[i]))
                {
                    string[] parts = pairs[i].Split('=');
                    ValidateMetaPairs(parts);
                    SetMeta(parts[0], parts[1]);
                }
            }
        }

        protected void ValidateMetaPairs(string[] prs)
        {
            if (prs.Length != 2) throw new ParseException
                ("Expected 2 parts for meta key/val, but found "
                 + prs.Length + ": " + prs.Stringify());

            for (int i = 0; i < prs.Length; i++) prs[i] = prs[i].Trim();

            if (prs[0].IndexOf(' ') > -1) throw new ParseException
                ("Meta keys cannot contains spaces: '" + prs[0] + "'");

            if (prs[1].IndexOf(' ') > -1) throw new ParseException
                ("Meta values cannot contains spaces: '" + prs[1] + "'");
        }
    }

}