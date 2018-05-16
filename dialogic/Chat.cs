﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dialogic
{
    /// <summary>
    /// Each section of text in a Dialogic script is known as a Chat. Each Chat has a unique label and contains one or more commands. When a Chat is run, each command is executed in order, until all have been run, or the system jumps to a new Chat. The Chat command accepts a required label, followed, optionally, by metadata, which can be used with the Find command to search for Chats matching desired criteria.
    /// </summary>
    public class Chat : Command
    {
        internal List<Command> commands;

        protected internal bool resumable { get; protected set; }
        protected internal bool interruptable { get; protected set; }
        protected internal bool resumeAfterInt { get; protected set; }
        protected internal double stalenessIncr { get; protected set; }
        protected internal double staleness { get; protected set; }

        internal int cursor = 0, lastRunAt = -1;
        internal bool allowSmoothingOnResume = true;

        internal IDictionary<string, object> scope;
        protected internal ChatRuntime runtime;

        public Chat() : base()
        {
            commands = new List<Command>();
            resolved = null; // not relevant for chats
            Reset();
        }
        internal static Chat Create(string name, ChatRuntime rt = null)
        {
            Chat c = new Chat();
            c.Init(name, String.Empty, new string[0]);
            if (rt == null) rt = new ChatRuntime();
            rt.Parser().HandleCommand(c, null, -1);
            return c;
        }

        public void Reset()
        {
            resumable = true;
            interruptable = true;
            resumeAfterInt = true;
            stalenessIncr = Defaults.CHAT_STALENESS_INCR;
            staleness = Defaults.CHAT_STALENESS;
            scope = new Dictionary<string, object>(Defaults.INITIAL_DICT_SIZE);
            //lastRunAt = -1;
            cursor = 0;
        }

        public int Count()
        {
            return commands.Count;
        }

        public override bool Equals(Object obj)
        {
            var chat = ((Chat)obj);

            if (resumable != chat.resumable ||
                interruptable != chat.interruptable ||
                resumeAfterInt != chat.resumeAfterInt)
            {
                return false;
            }

            if (!Util.FloatingEquals(staleness, chat.staleness) ||
                !Util.FloatingEquals(stalenessIncr, chat.stalenessIncr))
            {
                return false;
            }

            for (int i = 0; i < commands.Count; i++)
            {
                if (!commands[i].Equals(chat.commands[i]))
                {
                    return false;
                }
            }

            return (text == chat.text && ToTree() == chat.ToTree());
        }

        public override int GetHashCode() => ToTree().GetHashCode();

        public void AddCommand(Command c)
        {
            c.parent = this;
            this.commands.Add(c);
        }

        protected internal override IDictionary<string, object> Resolve
            (IDictionary<string, object> globals)
        {
            if (globals == null) globals = new Dictionary<string, object>();
            commands.ForEach(c => c.Resolve(globals));
            return null; // nothing to return;
        }

		internal bool ValidateParens()
		{
			int open = 0, close = 0;
			this.commands.ForEach(c =>{
				if (c is Set) {
					open += ((Set)c).value.Count(ch => ch == Ch.OGROUP);
					close += ((Set)c).value.Count(ch => ch == Ch.CGROUP);               
				} 
			});
			if (open != close) throw new MismatchedParens();
			return true;
		}

        protected internal override Command Validate()
        {
            if (text.IndexOf(' ') > -1) throw BadArg
                ("CHAT name '" + text + "' contains spaces");

            // Every chat must have a staleness value
            SetMeta(Meta.STALENESS, Defaults.CHAT_STALENESS.ToString(), true);

            return this;
        }

        protected internal override void Init(string txt, string lbl, string[] metas)
        {
            this.text = txt;
            ParseMeta(metas);
        }

        public string ToTree()
        {
            string s = TypeName().ToUpper() + " "
                + text + (" " + MetaStr()).TrimEnd();
            commands.ForEach(c => s += "\n  " + c);
            return s;
        }

        protected internal override string MetaStr()
        {
            string s = String.Empty;
            if (HasMeta())
            {
                s += '{';
                foreach (var key in meta.Keys)
                {
                    // no need to show the default properties
                    if (!HasDefaultPropValue(key))
                    {
                        s += key + '=' + meta[key] + ',';
                    }
                }
                s = s.TrimLast(',') + "}";
            }
            return s.ReplaceFirst("{}", string.Empty);
        }

        private bool HasDefaultPropValue(string key)
        {
            // assumes that meta-values are in sync with properties

            if (key == Meta.STALENESS && Util.FloatingEquals
                (staleness, Defaults.CHAT_STALENESS))
            {
                return true;
            }
            else if (key == Meta.STALENESS_INCR && Util.FloatingEquals
                     (stalenessIncr, Defaults.CHAT_STALENESS_INCR))
            {
                return true;
            }
            else if ((key == Meta.RESUMABLE && resumable) ||
                     (key == Meta.INTERRUPTIBLE && interruptable) ||
                     (key == Meta.RESUME_AFTER_INT && resumeAfterInt))
            {
                return true;
            }

            return false; // refactor this ugliness
        }

        internal Chat LastRunAt(int ms)
        {
            this.lastRunAt = ms;
            return this;
        }

        internal Command Next()
        {
            return HasNext() ? commands[cursor++] : null;
        }

        internal bool HasNext()
        {
            return cursor > -1 && cursor < commands.Count;
        }

        internal void Run(bool resetCursor = true)
        {
            if (resetCursor || !resumable)
            {
                this.cursor = 0;
                IncrementStaleness();
            }
            else
            {
                // reset half the added staleness on resume
                staleness -= (StalenessIncr() / 2.0);
            }

            LastRunAt(Util.EpochMs());
        }

        internal static Type DefaultCommandType(Chat chat)
        {
            if (chat != null && chat.runtime != null)
            {
                var typeMap = chat.runtime.typeMap;
                if (chat.HasMeta(Meta.DEFAULT_CMD))
                {
                    var type = (string)chat.GetMeta(Meta.DEFAULT_CMD);
                    if (!typeMap.ContainsKey(type))
                    {
                        throw new ParseException("Invalid defaultCmd" +
                            "  value in Chat#" + chat.text);
                    }

                    return typeMap[type];
                }
                else if (chat.HasMeta(Meta.CHAT_MODE))
                {
                    var mode = (string)chat.GetMeta(Meta.CHAT_MODE);
                    if (mode != "grammar")
                    {
                        throw new ParseException("Invalid 'mode'"
                            + " value in Chat#" + chat.text);
                    }
                    return typeof(Set);
                }
            }

            return typeof(Say);
        }

        public override string Text()
        {
            throw new DialogicException("Cannot call Text() on Chat: " + this);
        }

        internal Chat IncrementStaleness()
        {
            Staleness(staleness + stalenessIncr);
            return this;
        }

        internal bool IsPreload()
        {
            return Convert.ToBoolean(GetMeta(Meta.PRELOAD, false));
        }

        // meta-settable properties -------------------------------------------

        internal double Staleness() => staleness;

        internal double StalenessIncr() => stalenessIncr;

        internal bool Interruptable() => interruptable;

        internal bool ResumeAfterInterrupting() => resumeAfterInt;

        internal bool Resumable() => this.resumable;

        internal Chat Resumable(bool b)
        {
            SetMeta(Meta.RESUMABLE, (resumable = b).ToString());
            return this;
        }

        internal Chat Staleness(double d)
        {
            SetMeta(Meta.STALENESS, (staleness = d).ToString());
            return this;
        }

        internal Chat StalenessIncr(double incr)
        {
            SetMeta(Meta.STALENESS_INCR, (stalenessIncr = incr).ToString());
            return this;
        }

        internal Chat Interruptable(bool val)
        {
            SetMeta(Meta.INTERRUPTIBLE, (interruptable = val).ToString());
            return this;
        }

        internal Chat ResumeAfterInterrupting(bool val)
        {
            SetMeta(Meta.RESUME_AFTER_INT, (resumeAfterInt = val).ToString());
            return this;
        }

        // testing below ------------------------------------------------------

        internal string _Expand(IDictionary<string, object> globals, string start)
        {
            Say s = new Say();
            s.Init(start, string.Empty, new string[0]);
            s.SetActor(Dialogic.Actor.Default);
            s.parent = this;
            s.Resolve(globals);
            return s.Text();
        }

        protected internal string _GrammarToJson(IDictionary<string, object>
            globals, bool localize = true)
        {
            var name = text + ".";
            var re = new Regex(@"\$([^ \(\)]+)");
            var g = "{\n";

            foreach (var k in globals.Keys)
            {
                if (k.StartsWith(name, Util.IC))
                {
                    string key = k;
                    string val = (string)globals[k];

                    if (localize)
                    {
                        key = k.Replace(name, "");
                        val = val.Replace(name, "");
                    }

                    g += "  \"" + key + "\": \"" + val + "\",\n";
                }
            }
            return g.Substring(0, g.Length - 2) + "\n}";
        }

        internal void Complete()
        {
            // clear any local scope
            this.scope.Clear();

            // and resolved command data
            //commands.ForEach(c => c.resolved.Clear()); //tmp
        }
    }
}
