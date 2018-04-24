using System;
using System.Collections.Generic;
using MessagePack;

namespace Dialogic
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class GameState
    {
        public string firstChat;
        public List<ChatData> chatData;

        public static GameState Create(ChatRuntime rt)
        {
            return new GameState().FromGameObject(rt);   
        }

        public void AppendTo(ChatRuntime rt)
        {
            chatData.ForEach(cd => rt.AddChat(cd.ToGameObject(rt)));
            rt.firstChat = this.firstChat;
        }

        public ChatRuntime ToGameObject(List<IActor> actors)
        {
            var runtime = new ChatRuntime(actors);
            GameState gameState = GameState.Create(runtime);
            AppendTo(runtime);
            return runtime;
        }

        public GameState FromGameObject(ChatRuntime rt)
        {
            this.firstChat = rt.firstChat;

            this.chatData = new List<ChatData>(rt.chats.Keys.Count);
            foreach (var chat in rt.chats.Values)
            {
                this.chatData.Add(ChatData.Create(chat));
            }

            return this;
        }
    }

    [MessagePackObject(keyAsPropertyName: true)]
    public class ChatData
    {
        public List<CommandData> commands;

        public double staleness;
        public bool resumable;
        public bool interruptable;
        public bool resumeAfterInt;
        public double stalenessIncr;

        public int cursor = 0;
        public int lastRunAt = -1;
        public bool allowSmoothingOnResume = true;
        public string text;

        internal static ChatData Create(Chat c)
        {
            return new ChatData().FromGameObject(c);
        }

        public ChatData FromGameObject(Chat chat)
        {
            this.text = chat.text;

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
            Chat chat = Chat.Create(text);
            chat.runtime = rt;

            chat.Staleness(this.staleness);
            chat.Resumable(this.resumable);
            chat.Interruptable(this.interruptable);
            chat.ResumeAfterInterrupting(this.resumeAfterInt);
            chat.StalenessIncr(this.stalenessIncr);

            chat.cursor = this.cursor;
            chat.lastRunAt = this.lastRunAt;
            chat.allowSmoothingOnResume = this.allowSmoothingOnResume;

            chat.commands = new List<Command>(this.commands.Count);
            this.commands.ForEach(cmd => chat.AddCommand(cmd.ToGameObject(rt)));

            return chat;
        }
    }


    [MessagePackObject(keyAsPropertyName: true)]
    public class CommandData
    {
        //Dictionary<string, string> meta;
        //public double delay;
        //public string text;
        //public string type;
        //public string actorName;
        //public string parentName;

        public string actor, command, text, label, meta;

        internal static CommandData Create(Command c)
        {
            return new CommandData().FromGameObject(c);
        }

        public CommandData FromGameObject(Command c)
        {
            this.command = c.TypeName(); // can be null in LineContext

            LineContext lc = c.lineContext;
            this.actor = lc.actor;
            this.text = lc.text;
            this.label = lc.label;
            this.meta = lc.meta;

            return this;
        }


        internal Command ToGameObject(ChatRuntime rt)
        {
            LineContext lc = new LineContext(actor, command, text, label, meta);
            //Type type = ChatRuntime.TypeMap[lc.command];

            //Command c = Command.Create(type, lc.text, lc.label, parser.SplitMeta(lc.meta));
            //c.lineContext = lc;

            //parser.HandleActor(lc.actor, c, lc.line, lc.lineNo);
            //parser.HandleCommand(c, lc.line, lc.lineNo);
            //parser.RunValidators(c);

            return rt.Parser().CreateCommand(lc);
        }
    }

    /*
    [MessagePackObject(keyAsPropertyName: true)]
    public class ActorData
    {
        public string name;
        public bool isDefault;

        internal static ActorData Create(Actor actor)
        {
            return new ActorData().FromGameObject(actor);
        }

        public ActorData FromGameObject(Actor actor)
        {
            this.name = actor.Name();
            this.isDefault = actor.IsDefault();
            return this;
        }

        internal Actor ToGameObject()
        {
            return new Actor(name, isDefault);
        }
    }*/
}
