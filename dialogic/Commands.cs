using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        protected internal LineContext lineContext;

        protected Command()
        {
            this.delay = 0;
            this.id = ++IDGEN;
            this.resolved = new Dictionary<string, object>();
        }

        protected Resolver Resolver()
        {
            if (parent == null || parent.runtime == null)
            {
                throw new Exception("Null parent/context: " + this);
            }
            return this.parent.runtime.resolver;
        }

        public override bool Equals(object obj)
        {
            var c = (Command)obj;
            return c.text == text && c.actor == actor && c.MetaStr() ==
                MetaStr() && Util.FloatingEquals(c.delay, delay);
        }

        public override int GetHashCode()
        {
            return (text ?? "").GetHashCode() ^ delay.GetHashCode()
                ^ actor.GetHashCode() ^ MetaStr().GetHashCode();
        }

        protected double Delay() // TODO: test from meta
        {
            return delay;
        }

        /// <summary>
        /// Will update the value of the property (and meta if syncMeta=true) if it exists, and return true, else false if it does not
        /// </summary>
        protected internal bool Update
            (string property, object value, bool syncMeta = true)
        {
            // will only set if it exists
            bool updated = Properties.Set(this, property, value, true);

            // check if we need to sync metadata as well
            if (syncMeta && HasMeta(property))
            {
                SetMeta(property, value.ToString());
            }

            return updated;
        }

        protected Command Delay(double seconds)
        {
            delay = seconds;
            SetMeta(Meta.DELAY, seconds.ToString());
            return this;
        }

        internal static Command Create(Type type, string txt, string lbl, string[] metas)
        {
            //Console.WriteLine("'"+type + "' '"+txt+ "' '"+ lbl+"' "+metas.Stringify());
            Command cmd = (Command)Activator.CreateInstance(type);
            cmd.Init(txt, lbl, metas);
            return cmd;
        }

        protected internal virtual void Init(string txt, string lbl, string[] metas)
        {
            this.text = txt.Length > 0 ? txt : lbl;
            ParseMeta(metas);
        }

        protected void ValidateTextLabel()
        {
            if (String.IsNullOrEmpty(text)) throw BadArg(TypeName()
                + " requires a literal #Label, got '" + text + "'");

            Util.ValidateLabel(ref text);
        }

        /// <summary>
        ///  Returns resolved text for this object, equivalent to this.Resolved(Meta.TEXT);
        /// </summary>
        /// <returns>The text.</returns>
        public virtual string Text()
        {
            if (!resolved.ContainsKey(Meta.TEXT))
            {
                throw new DialogicException("Text() called on unresolved"
                    + " Command: " + this + "\nCall Resolve() first");
            }
            return (string)resolved[Meta.TEXT];
        }

        /// <summary>
        /// In multi-speaker environments, SAY commands (also ASK, DO, and others) can be assigned to a specific Actor, which may or may not be the default actor. If no actor is specified, then the default actor is used. To specify a different actor than the default, add the actor's name to the metadata, or prepend it followed by a colon to the command.
        /// </summary>
        /// <returns>The actor.</returns>
        public IActor GetActor()
        {
            return actor;
        }

        protected internal Command SetActor(IActor theActor)
        {
            this.actor = theActor;
            return this;
        }

        protected internal Command SetActor(ChatRuntime rt, string actorName)
        {
            return SetActor(rt.FindActorByName(actorName));
        }

        /// <summary>
        /// Validates this instance by verifying that (at least) it has all required metadata values (no-op here)
        /// </summary>
        protected internal virtual Command Validate()
        {
            return this;
        }

        protected internal virtual string TypeName()
        {
            return this.GetType().Name;
        }

        protected internal virtual IDictionary<string, object> Resolve
            (IDictionary<string, object> globals)
        {
            resolved.Clear();

            ResolveMeta(globals);

            if (this is ISendable)
            {
                if (parent == null) throw new DialogicException
                    ("Null Chat parent for: " + this);

                resolved[Meta.TEXT] = Resolver().Bind(text, parent, globals);
                resolved[Meta.TYPE] = TypeName();
                if (this is IAssignable && actor != null)
                {
                    resolved[Meta.ACTOR] = GetActor().Name();
                }
            }
            return resolved;
        }

        protected virtual void ResolveMeta(IDictionary<string, object> globals)
        {
            if (HasMeta())
            {
                foreach (KeyValuePair<string, object> pair in meta)
                {
                    object val = pair.Value;

                    if (val is string)
                    {
                        // Q: should we resolve on parent for meta ?
                        val = Resolver().Bind((string)val, parent, globals);
                    }
                    else if (!(val is Constraint)) // don't replace constraints
                    {
                        throw new DialogicException("Unexpected meta-value type: "
                            + val.GetType() + " -> " + val);
                    }

                    resolved[pair.Key] = val;
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

    public class NoOp : Command //@cond hide
    { } //@endcond

    /// <summary>
    /// The Say command allows a character to speak. It is the default command. If a line does not start with a command, than it is assumed to be a Say command. It accepts free text, optionally followed by metadata, which can be used to control various display parameters. In multi-speaker environments, Say commands (also Ask, Do, and other IAssignables) can be assigned to a specific Actor, which may or may not be the default actor. If no actor is specified, then the default actor is used. To specify a different actor than the default, add the actor's name to the metadata, or prepend it followed by a colon to the command.
    /// </summary>
    public class Say : Command, ISendable, IAssignable
    {
        public Say() : base()
        {
            this.delay = Util.ToMillis(Defaults.SAY_DURATION);
        }

        protected internal override Command Validate()
        {
            if (text.Length < 1) throw new ParseException("SAY requires text");
            return this;
        }

        /// <summary>
        /// Determine milliseconds to wait after sending the event, using:
        ///     a. Line-length
        ///     b. Meta-data modifiers
        ///     c. Character mood (TODO)
        /// </summary>
        /// <returns>The duration.</returns>
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
            return (TypeName().ToUpper() + " #" + text)
                .Trim() + (" " + MetaStr()).TrimEnd();
        }
    }

    /// <summary>
    /// The Set command is used to create or modify a variable. Variables generally originate from the game environment itself, but can also be created, accessed or modified within Dialogic.
    /// </summary>
    public class Set : Command
    {
        protected internal string value;
        protected internal Assignment op;
        protected internal bool global;

        public Set() : base() { }

        protected internal override void Init(string txt, string lbl, string[] metas)
        {
            var match = RE.ParseSetArgs.Match(txt);

            if (match.Groups.Count != 4)
            {
                //Util.ShowMatch(match);
                throw new ParseException("Invalid SET args: '" + txt + "'");
            }

            // use Symbol here?

            var sym = match.Groups[1].Value.Trim();

            this.text = sym.TrimFirst(Ch.SYMBOL);
            this.global = sym.StartsWith('$');
            this.value = match.Groups[3].Value.Trim();
            this.op = Assignment.FromString(match.Groups[2].Value.Trim());

            this.resolved = null; // not relevant for Set
        }

        protected internal override IDictionary<string, object> Resolve
            (IDictionary<string, object> globals)
        {
            if (text.Contains(Ch.SCOPE)) // scoped
            {
                if (!global) throw new BindException
                    ("Invalid locally-scoped symbol" + text);

                var path = text.Split(Ch.SCOPE);
                if (path.Length > 2) throw new BindException
                    ("Unexpected variable format: " + text);

                if (globals.ContainsKey(path[0])) // global objects
                {
                    if (!SetPathValue(globals[path[0]], path, value, globals))
                    {
                        throw new BindException("Failed to update global: " + text);
                    }
                }
                else  // property on a remote chat (only allow property updates)
                {
                    // Try to set chat-local persistent property, eg '$c1.delay'
                    if (!parent.runtime.ContainsKey(path[0]))
                    {
                        throw new BindException("Failed to resolve path: " + text);
                    }

                    Chat context = parent.runtime[path[0]];
                    if (path.Length != 2) throw new BindException
                        ("Bad chat-scoped path: " + text);

                    if (!context.Update(path[1], value))
                    {
                        throw new BindException("Invalid attempt to access " +
                            "chat-local scope: $" + context.text + "." + path[1]);
                    }
                }
            }
            else
            {
                if (!global)
                {
                    // unscoped local, check chat properties
                    if (!parent.Update(text, value))
                    {
                        // set if not found as property
                        op.Invoke(text, value, parent.scope);
                    }
                }
                else
                {
                    // unscoped global, just do the set
                    op.Invoke(text, value, globals);
                }
            }

            return resolved; // nothing to return here
        }

        internal static bool SetPathValue(object parent, string[] paths, object val,
            IDictionary<string, object> globals)
        {
            if (parent == null) throw new BindException("null parent");

            // Dynamically resolve the object path 
            for (int i = 1; i < paths.Length - 1; i++)
            {
                parent = Properties.Get(parent, paths[i]);
                if (parent == null) throw new BindException
                    ("bad parent" + paths.Stringify());
            }

            return Properties.Set(parent, paths[paths.Length - 1], val);
        }

        public override string ToString()
        {
            var txt = this.text;
            if (global) txt = Ch.SYMBOL + txt;
            return TypeName().ToUpper() + " " + txt + " = " + value;
        }
    }

    /// <summary>
    /// The Wait command is used to pause the runtime for a period of time.  It accepts a single number specifying the time to wait in seconds. If no time is specified, the system will wait until it receives a  ResumeEvent, after which it will continue with the current Chat, or the Chat specified in the event.
    /// </summary>
    public class Wait : Command, ISendable
    {
        protected internal override void Init(string txt, string lbl, string[] metas)
        {
            base.Init(txt, lbl, metas);
            delay = txt.Length == 0 ? DefaultDuration() : Convert.ToDouble(txt);
        }

        protected virtual double DefaultDuration()
        {
            return Defaults.WAIT_DURATION;
        }
    }

    /// <summary>
    /// The ASK command is for prompting the user, and is generally followed by one or more OPT commands. If no OPT commands are supplied, then a simple Yes/No pair is created. ASK pauses the current chat, after which it waits for a ChoiceEvent object to be returned from the application. Based upon the ChoiceEvent data (which option was selected), the chat will then go to the next chat specified. It accepts free text, optionally followed by metadata, which can be  used to control various display parameters.
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
                AddOption(new Opt("Yes", NOP));
                AddOption(new Opt("No", NOP));
            }

            return options;
        }

        protected internal string JoinOptions(string delim = "\n")
        {
            var s = String.Empty;
            var opts = Options();
            for (int i = 0; i < opts.Count; i++)
            {
                s += opts[i].Text();
                if (i < opts.Count - 1) s += delim;
            }
            return s;
        }

        public Opt Selected()
        {
            return Options()[selectedIdx];
        }

        protected internal Opt Selected(int i)
        {
            this.selectedIdx = i;
            return (i >= 0 && i < options.Count) ? Selected() : null;
        }

        protected internal void AddOption(Opt o)
        {
            // set parent for Opt and Opt.action
            o.parent = o.action.parent = this.parent;
            options.Add(o);
        }

        // Call Resolve() on text and options, then add both to resolved
        protected internal override IDictionary<string, object> Resolve
            (IDictionary<string, object> globals)
        {
            resolved.Clear();

            base.Resolve(globals);

            Options().ForEach(o => o.Resolve(globals));
            resolved[Meta.TIMEOUT] = timeout.ToString();
            resolved[Meta.OPTS] = JoinOptions();

            return resolved;
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

        protected internal Opt(string text, Command action) : base()
        {
            this.text = text;
            this.action = action;
        }

        protected internal override void Init(string txt, string lbl, string[] metas)
        {
            this.text = txt;

            if (lbl.Length > 0 && !lbl.StartsWith(Util.LABEL_IDENT, Util.IC))
            {
                throw BadArg("OPT requires a literal #Label");
            }

            this.action = lbl.Length > 0 ?
                Command.Create(typeof(Go), String.Empty, lbl, metas) : NOP;
        }

        protected internal override Command Validate()
        {
            this.action.Validate(); // validate the option

            return this;
        }

        public override string ToString()
        {
            return TypeName().ToUpper() + " " + text + (action is NoOp ?
                "" : " " + Ch.LABEL + action.text.TrimFirst(Ch.LABEL));
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
            if (meta != null) // need to clear both here
            {
                meta.Clear();
                resolved.Clear();
            }

            Init(null, null, metadata.Trim().TrimEnds('{', '}').Split(','));

            // need to call Validate here as its not called by the parser
            return (Find)Validate();
        }

        protected internal override void Init(string txt, string lbl, string[] metas)
        {
            if (!txt.IsNullOrEmpty()) throw new DialogicException
                ("FIND does not accept text, only metadata");
            ParseMeta(metas);
        }

        protected internal override IDictionary<string, object> Resolve
            (IDictionary<string, object> globals)
        {
            resolved.Clear();
            ResolveMeta(globals); // only meta

            return resolved;
        }

        ///  All Find commands must have a 'staleness' value
        protected internal override Command Validate()
        {
            SetMeta(new Constraint(Operator.LT, Meta.STALENESS,
                    Defaults.FIND_STALENESS.ToString()), true);

            Constraint staleness = (Constraint)GetMeta(Meta.STALENESS);

            if (staleness.op != Operator.LT && staleness.op != Operator.LE)
            {
                throw new FindException("Find staleness op must be < or <=");
            }

            return this;
        }

        protected internal override void SetMeta
            (string key, object val, bool onlyIfNotSet = false)
        {
            if (val is string || val.IsNumber())
            {
                val = new Constraint(key, val.ToString());
            }
            base.SetMeta(key, val, onlyIfNotSet);
        }

        protected internal override void ParseMeta(string[] pairs)
        {
            for (int i = 0; pairs != null && i < pairs.Length; i++)
            {
                if (String.IsNullOrEmpty(pairs[i])) throw new ParseException
                    ("Invalid query: {empty}");

                var pair = pairs[i].Trim();

                Match match = RE.FindMeta.Match(pair);
                //Util.ShowMatch(match);

                if (match.Groups.Count != 4) throw new ParseException
                    ("Invalid query: '" + pair + "'");

                string key = match.Groups[1].Value;
                ConstraintType ctype = ConstraintType.Soft;

                if (Util.TrimFirst(ref key, Ch.NOT))
                {
                    ctype = ConstraintType.Hard;
                    if (Util.TrimFirst(ref key, Ch.NOT))
                    {
                        ctype = ConstraintType.Absolute;
                    }
                }

                if (meta == null) meta = new Dictionary<string, object>();

                meta.Add(key, new Constraint(Operator.FromString
                    (match.Groups[2].Value), key, match.Groups[3].Value, ctype));
            }
        }

        protected internal override string MetaStr()
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
        protected internal override void Init(string txt, string lbl, string[] metas)
        {
            this.text = txt.Length > 0 ? txt : lbl;
            if (!metas.IsNullOrEmpty()) throw new ParseException
                ("GO does not accept metadata");
        }

        protected internal override IDictionary<string, object> Resolve
            (IDictionary<string, object> globals)
        {
            resolved.Clear(); // no need to resolve meta here
            resolved[Meta.TYPE] = TypeName();
            resolved[Meta.TEXT] = Resolver().BindGroups(text, parent);

            return resolved;
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
        public const string PRELOAD = "preload";
        public const string TIMEOUT = "timeout";
        public const string ON_RESUME = "onResume";
        public const string CHAT_MODE = "chatMode";
        public const string STALENESS = "staleness";
        public const string RESUMABLE = "resumable";
        public const string DEFAULT_CMD = "defaultCmd";
        public const string INTERRUPTIBLE = "interruptible";
        public const string STALENESS_INCR = "stalenessIncr";
        public const string RESUME_AFTER_INT = "resumeAfterInt";

        protected internal IDictionary<string, object> meta;
        protected IDictionary<string, object> resolved;

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

        protected internal object Resolved(string key, object defaultVal = null)
        {
            return resolved.ContainsKey(key) ? resolved[key] : defaultVal;
        }

        public IDictionary<string, object> Resolved()
        {
            return resolved;
        }

        protected internal void SetMeta
            (Constraint constraint, bool onlyIfNotSet = false)
        {
            this.SetMeta(constraint.name, constraint, onlyIfNotSet);
        }

        protected internal virtual void SetMeta
            (string key, object val, bool onlyIfNotSet = false)
        {
            if (meta == null) meta = new Dictionary<string, object>();
            if (onlyIfNotSet && HasMeta(key)) return;
            meta[key] = val;
        }

        protected internal virtual string MetaStr()
        {
            string s = String.Empty;
            if (HasMeta())
            {
                s += "{";
                foreach (var key in meta.Keys) s += key + "=" + meta[key] + ",";
                s = s.TrimLast(',') + '}';
            }
            return s;
        }

        protected internal virtual void ParseMeta(string[] pairs)
        {
            for (int i = 0; pairs != null && i < pairs.Length; i++)
            {
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