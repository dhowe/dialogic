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

        // Set a Command property by name
        protected internal void DynamicSet(PropertyInfo propInfo, 
            object val, bool syncMeta = true)
        {
            val = Util.ConvertTo(propInfo.PropertyType, val);
            propInfo.SetValue(this, val, null);

            // check if we need to sync metadata as well
            if (syncMeta && HasMeta(propInfo.Name))
            {
                SetMeta(propInfo.Name, val.ToString());
            }
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
        ///  REturns realized text for this object, equivalent to this.Realized(Meta.TEXT);
        /// </summary>
        /// <returns>The text.</returns>
        public string Text()
        {
            if (!realized.ContainsKey(Meta.TEXT))
            {
                throw new DialogicException("Text() called on unrealized cmd: "
                    + this + "\nCall Realize() first");
            }
            return (string)realized[Meta.TEXT];
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

        protected internal virtual Command Realize(IDictionary<string, object> globals)
        {
            realized.Clear();

            RealizeMeta(globals);

            if (this is ISendable)
            {
                if (parent == null) throw new DialogicException
                    ("Null Chat parent for: " + this);

                realized[Meta.TEXT] = Resolver.Bind(text, parent, globals);
                realized[Meta.TYPE] = TypeName();
                if (this is IAssignable && actor != null)
                {
                    realized[Meta.ACTOR] = GetActor().Name();
                }
            }
            return this;
        }

        protected virtual void RealizeMeta(IDictionary<string, object> globals)
        {
            if (HasMeta())
            {
                foreach (KeyValuePair<string, object> pair in meta)
                {
                    object val = pair.Value;

                    if (val is string)
                    {
                        // Q: should we resolve on parent for meta ?
                        val = Resolver.Bind((string)val, parent, globals);
                    }
                    else if (!(val is Constraint)) // don't replace constraints
                    {
                        throw new DialogicException("Unexpected meta-value type: "
                            + val.GetType() + " -> " + val);
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

    public class NoOp : Command //@cond hide
    { } //@endcond

    /// <summary>
    /// The Say command allows a character to speak. It is the default command. If a line does not start with a command, than it is assumed to be a Say command. It accepts free text, optionally followed by metadata, which can be used to control various display parameters. In multi-speaker environments, Say commands (also Ask, Do, and other IAssignables) can be assigned to a specific Actor, which may or may not be the default actor. If no actor is specified, then the default actor is used. To specify a different actor than the default, add the actor's name to the metadata, or prepend it followed by a colon to the command.
    /// </summary>
    public class Say : Command, ISendable, IAssignable
    {
        //protected string lastSpoken;
        //protected bool disableUniqueness = false;

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
        protected internal AssignOp op;
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

            var tmp = match.Groups[1].Value.Trim();
            this.text = tmp.TrimFirst(Ch.SYMBOL);
            this.global = (tmp != text) && !text.Contains(".");
            this.value = match.Groups[3].Value.Trim();
            this.op = AssignOp.FromString(match.Groups[2].Value.Trim());
        }

        protected internal override Command Realize(IDictionary<string, object> globals)
        {
            if (global && globals == null) throw new DialogicException
                ("Invalid call to Set.Realize() with null argument");

            var symbol = text;
            var context = parent;

            Resolver.BindToContext(ref symbol, ref context);

            // Here we check if the set matches a dynamic parent property
            if (context != null)
            {
                IDictionary<string, PropertyInfo> mm = ChatRuntime.MetaMeta[typeof(Chat)];

                // If so, we don't create a new symbol, but instead set the property
                if (mm.ContainsKey(symbol))
                {
                    context.DynamicSet(mm[symbol], value);
                    return this;
                }
            }

            // Invoke the assignment in the correct scope
            op.Invoke(symbol, value, (global ? globals : context.scope));

            return this;
        }

        private string HandleGrammarTag(string val)
        {
            MatchCollection matches = RE.GrammarRules.Matches(val);

            if (matches.Count > 0)
            {
                var rules = matches.Cast<Match>()
                    .Select(match => match.Groups[1].Value).ToList();

                rules.ForEach(rule => val = val.Replace("<"
                    + rule + ">", "$" + rule));
            }

            return val;
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
                options.Add(new Opt("Yes", NOP));
                options.Add(new Opt("No", NOP));
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
            if (i >= 0 && i < options.Count) return Selected();
            return null;
        }

        protected internal void AddOption(Opt o)
        {
            o.parent = this.parent;
            options.Add(o);
        }

        // Call Realize() on text and options, then add both to realized
        protected internal override Command Realize(IDictionary<string, object> globals)
        {
            base.Realize(globals);
            Options().ForEach(o => o.Realize(globals));
            realized[Meta.TIMEOUT] = timeout.ToString();
            realized[Meta.OPTS] = JoinOptions();
            return this;
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

            // need to call Validate here as its not called by the parser
            return (Find)Validate();
        }

        protected internal override void Init(string txt, string lbl, string[] metas)
        {
            if (!txt.IsNullOrEmpty()) throw new DialogicException
                ("FIND does not accept text, only metadata");
            ParseMeta(metas);
        }

        protected internal override Command Realize(IDictionary<string, object> globals)
        {
            realized.Clear();
            RealizeMeta(globals); // only realized meta
            return this;
        }

        ///  All Find commands must have a 'staleness' value
        protected internal override Command Validate()
        {
            //Console.WriteLine("Find.Validate: "+this);

            SetMeta(new Constraint(Operator.LT, Meta.STALENESS,
                    Defaults.FIND_STALENESS.ToString()), true);

            Constraint staleness = (Constraint)GetMeta(Meta.STALENESS);

            if (staleness.op != Operator.LT && staleness.op != Operator.LE)
            {
                throw new FindException("Find staleness op must be < or <=");
            }

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

                var pair = pairs[i].Trim();

                Match match = RE.FindMeta.Match(pair);
                //Util.ShowMatch(match);

                if (match.Groups.Count != 4) throw new ParseException
                    ("Invalid query: '" + pair + "'");

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
        protected internal override void Init(string txt, string lbl, string[] metas)
        {
            this.text = txt.Length > 0 ? txt : lbl;
            if (!metas.IsNullOrEmpty()) throw new ParseException
                ("GO does not accept metadata");
        }

        public new Go Init(string label)
        {
            Init(String.Empty, label, null);
            return this;
        }

        protected internal override Command Realize(IDictionary<string, object> globals)
        {
            realized.Clear();
            //RealizeMeta(globals); // no meta
            realized[Meta.TYPE] = TypeName();
            realized[Meta.TEXT] = Resolver.BindGroups(text, parent);
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
        public const string ON_RESUME = "onResume";
        public const string CHAT_MODE = "chatMode";
        public const string STALENESS = "staleness";
        public const string DEFAULT_CMD = "defaultCmd";
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

        protected internal object Realized(string key, object defaultVal = null)
        {
            return realized.ContainsKey(key) ? realized[key] : defaultVal;
        }

        public IDictionary<string, object> Realized()
        {
            return realized;
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
                s = s.TrimLast(',') + '}';
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