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
        public List<ActorData> actorData;

        // TODO: Validators, CommandDefs

        public static GameState Create(ChatRuntime rt)
        {
            return new GameState().FromGameObject(rt);   
        }

        public ChatRuntime ToGameObject()
        {
            List<IActor> actors = new List<IActor>(actorData.Count);
            actorData.ForEach(a => actors.Add(a.ToGameObject()));
          
            List<Chat> chats = new List<Chat>(chatData.Count);
            chatData.ForEach(c => chats.Add(c.ToGameObject()));

            return new ChatRuntime(chats, actors);
        }

        public GameState FromGameObject(ChatRuntime rt)
        {
            this.firstChat = rt.firstChat;
            this.chatData = new List<ChatData>(rt.chats.Keys.Count);
            this.actorData = new List<ActorData>(rt.actors.Count);

            foreach (var actor in rt.actors)
            {
                this.actorData.Add(ActorData.Create((Dialogic.Actor) actor));
            }
            foreach (var chat in rt.chats.Values)
            {
                this.chatData.Add(ChatData.Create(chat));
            }

            return this;
        }
    }

    [MessagePackObject(keyAsPropertyName: true)]
    public class CommandData
    {
        Dictionary<string, string> meta;

        public double delay;

        public string text;
        public string actorName;
        public string parentName;

        internal static CommandData Create(Command c)
        {
            return new CommandData().FromGameObject(c);
        }

        public CommandData FromGameObject(Command c)
        {
            this.text = c.text;
            this.delay = c.delay;
            this.parentName = c.parent.text;
            this.actorName = c.actor.Name();

            if (c.HasMeta())
            {
                this.meta = new Dictionary<string, string>();
                foreach (var key in c.meta.Keys)
                {
                    this.meta.Add(key, c.meta[key].ToString());
                }
            }

            return this;
        }


        internal Command ToGameObject()
        {
            Command c = null;//new Command();
            //TODO:
            return c;
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


        internal Chat ToGameObject()
        {
            Chat c = Chat.Create(text);
            c.text = this.text;
            c.Staleness(this.staleness);
            c.Resumable(this.resumable);
            c.Interruptable(this.interruptable);
            c.ResumeAfterInterrupting(this.resumeAfterInt);
            c.StalenessIncr(this.stalenessIncr);

            c.cursor = this.cursor;
            c.lastRunAt = this.lastRunAt;
            c.allowSmoothingOnResume = this.allowSmoothingOnResume;

            c.commands = new List<CommandData>(this.commands.Count);
            this.commands.ForEach(cmd => c.commands.Add(CommandData.Create(cmd)));
            return c;
        }
    }

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
    }
}
