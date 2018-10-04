using System;
using System.Collections.Generic;

namespace Dialogic
{
    /// <summary>
    /// A serializable subset of system properties at a particular moment,
    /// with which to store/restore instances of Dialogic (ChatRuntime)
    /// </summary>
    public class Snapshot
    {
        static public bool DBUG = false;

        public int timestamp;
        public string firstChat;
        public List<ChatData> chatData;

        public Snapshot()
        {
            this.timestamp = Util.EpochMs();
        }

        /// <summary>
        /// Create a new snapshot with Chat data from the specified runtime
        /// </summary>
        public static Snapshot Create(ChatRuntime rt)
        {
            Snapshot snap = new Snapshot();
            snap.firstChat = rt.firstChat;
            snap.chatData = new List<ChatData>(rt.Chats().Count);
            rt.Chats().ForEach(c => snap.chatData.Add(ChatData.Create(c)));
            return snap;
        }

        /// <summary>
        /// Update this snapshot with Chat data from the specified runtime
        /// </summary>
        public void Update(ChatRuntime rt)
        {
            chatData.ForEach(cd => cd.ToGameObject(rt));
            rt.firstChat = this.firstChat;
        }
    }

    public class ChatData
    {
        public List<CommandData> commands;

        public double staleness, stalenessIncr;
        public bool resumable, interruptable, resumeAfterInt;

        public int cursor = 0, lastRunAt = -1;
        public bool allowSmoothingOnResume = true;

        public string text, meta;

        internal static ChatData Create(Chat c)
        {
            return new ChatData().FromGameObject(c);
        }

        public ChatData FromGameObject(Chat chat)
        {
            this.text = chat.text;
            this.meta = chat.MetaStr().TrimEnds('{', '}');

            this.staleness = chat.staleness;
            this.resumable = chat.resumable;
            this.interruptable = chat.interruptable;
            this.resumeAfterInt = chat.resumeAfterInt;
            this.stalenessIncr = chat.stalenessIncr;

            this.cursor = chat.cursor;
            this.lastRunAt = chat.lastRunAt;
            this.allowSmoothingOnResume = chat.allowSmoothingOnResume;

            this.commands = new List<CommandData>(chat.commands.Count);
            chat.commands.ForEach(cmd => this.commands.Add(CommandData.Create(cmd)));

            return this;
        }

        internal Chat ToGameObject(ChatRuntime rt)
        {
            IParser parser = rt.Parser();
            ILine lc = new LineContext(parser, null, "CHAT", text, null, meta);
            Chat chat = (Dialogic.Chat)parser.CreateCommand(lc);

            chat.Staleness(this.staleness);
            chat.Resumable(this.resumable);
            chat.Interruptable(this.interruptable);
            chat.ResumeAfterInterrupting(this.resumeAfterInt);
            chat.StalenessIncr(this.stalenessIncr);

            chat.cursor = this.cursor;
            chat.lastRunAt = this.lastRunAt;
            chat.allowSmoothingOnResume = this.allowSmoothingOnResume;

            chat.commands = new List<Command>(this.commands.Count);
            this.commands.ForEach(cmd => cmd.ToGameObject(rt));

            return chat;
        }
    }


    public class CommandData
    {
        public static char OPT_DELIM = '\n';

        public string actor, command, text, label, meta;
        public string[] options;

        internal static CommandData Create(Command c)
        {
            return new CommandData().FromGameObject(c);
        }

        public CommandData FromGameObject(Command c)
        {
            this.command = c.TypeName().ToUpper();

            ILine lc = c.lineContext;
            this.actor = lc.actor;
            this.text = lc.text;
            this.label = lc.label;
            this.meta = lc.meta;

            if (c is Ask)
            {
                var opts = ((Ask)c).Options();
                options = new string[opts.Count];
                var i = 0;
                opts.ForEach(o =>
                {
                    //Console.WriteLine("ADD: "+o.text + "\\n" + o.action);
                    options[i++] = o.text + OPT_DELIM + o.action.text;
                });
            }

            return this;
        }


        internal Command ToGameObject(ChatRuntime rt)
        {
            IParser parser = rt.Parser();
            LineContext lc = new LineContext(parser, actor, command, text, label, meta);
            var cmd = parser.CreateCommand(lc);

            if (cmd is Ask)
            {
                Ask ask = (Dialogic.Ask)cmd;
                for (int i = 0; i < options.Length; i++)
                {
                    var parts = options[i].Split(OPT_DELIM);
                    if (parts.Length != 2) throw new DialogicException
                        ("Bad option:" + options[i]);
                    Opt opt = new Opt();
                    opt.Init(parts[0], Ch.LABEL + parts[1], null);
                    ask.AddOption(opt);
                }
            }
            return cmd;
        }
    }
}
