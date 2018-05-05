using System;
using System.Collections.Generic;

namespace Dialogic
{
    /// <summary>
    /// Superclass for specific GameEvents
    /// </summary>
    public abstract class GameEvent : EventArgs { }

    /// <summary>
    /// Basic implementation of ISuspend
    /// </summary>
    public class SuspendEvent : GameEvent, ISuspend { }

    /// <summary>
    /// Basic implementation of IClear
    /// </summary>
    public class ClearEvent : GameEvent, IClear { }

    /// <summary>
    /// Basic implementation of IUserEvent
    /// </summary>
    public class UserEvent : GameEvent, IUserEvent
    {
        protected readonly string type;

        public UserEvent(string type) : base()
        {
            this.type = type;
        }

        public string GetEventType()
        {
            return type;
        }
    }

    /// <summary>
    /// Basic implementation of IResume: tells Dialogic to Resume running the last suspended Chat.
    /// If label or finder string is provided, then it will be used to specify the Chat to resume.
    /// </summary>
    public class ResumeEvent : GameEvent, IResume
    {
        protected readonly string data;

        public ResumeEvent(string labelOrFind = null) : base()
        {
            this.data = labelOrFind;
        }

        public string ResumeWith()
        {
            return data;
        }
    }

    /// <summary>
    /// Basic implementation of IChoice: tells Dialogic that the User has made a specific choice in response to a prompt
    /// </summary>
    public class ChoiceEvent : GameEvent, IChoice
    {
        protected int choiceIndex;

        public ChoiceEvent(int option)
        {
            this.choiceIndex = option;
        }

        public override string ToString()
        {
            return "Choice: " + choiceIndex;
        }

        public int GetChoiceIndex()
        {
            return choiceIndex;
        }
    }

    /// <summary>
    /// Basic implementation of IChatUpdate: tells Dialogic to execute the specified 'action' on all Chats matching the the find criteria
    /// </summary>
    public class ChatUpdate : GameEvent, IChatUpdate
    {
        private readonly string findBy;
        private readonly Action<Chat> action;

        public ChatUpdate(Action<Chat> action, string findBy = null)
        {
            this.action = action;
            this.findBy = findBy;
        }

        public string FindByCriteria()
        {
            return findBy;
        }

        public Action<Chat> GetAction()
        {
            return action;
        }
    }

    /// <summary>
    /// Basic implementation of IChatUpdate: sets the staleness value for one or more Chats, as specified by the find criteria
    /// </summary>
    public class StalenessUpdate : ChatUpdate, IChatUpdate
    {
        public StalenessUpdate(double staleness, string findBy = null)
            : base((c) => { c.Staleness(staleness); }, findBy) { }
    }

    /// <summary>
    /// Basic implementation of IUpdateEvent which wraps a string/object dictionary containing relevant Command data with helper functions for extracting specific primitive types (double, bool, string, int, float)
    /// </summary>
    public class UpdateEvent : IUpdateEvent
    {
        private readonly IDictionary<string, object> data;

        public UpdateEvent(IDictionary<string, object> resolvedCommandData)
        {
            this.data = resolvedCommandData;
        }

        public IDictionary<string, object> Data()
        {
            return data;
        }

        public string Get(string key, string def = null)
        {
            return data.ContainsKey(key) ? (string)data[key] : def;
        }

        public int GetInt(string key, int def = -1)
        {
            string s = Get(key, def + String.Empty);
            return (int)Convert.ChangeType(s, typeof(int));
        }

        public bool GetBool(string key, bool def = false)
        {
            string s = Get(key, def + String.Empty);
            return (bool)Convert.ChangeType(s, typeof(bool));
        }

        public float GetFloat(string key, float def = -1)
        {
            string s = Get(key, def + String.Empty);
            return (float)Convert.ChangeType(s, typeof(float));
        }

        public double GetDouble(string key, double def = -1)
        {
            string s = Get(key, def + String.Empty);
            return (double)Convert.ChangeType(s, typeof(double));
        }

        public void RemoveKeys(params string[] keys)
        {
            foreach (var k in keys) data.Remove(k);
        }

        public string Text()
        {
            return (string)data[Meta.TEXT];
        }

        public string Type()
        {
            return (string)data[Meta.TYPE];
        }

        /// <summary>
        /// Return Actor.name for the event if it is IAssignable, else null.
        /// </summary>
        /// <returns>The Actor name.</returns>
        public string Actor()
        {
            return data.ContainsKey(Meta.ACTOR) ?
                (string)data[Meta.ACTOR] : null;
        }

        public string Opts()
        {
            return (!data.ContainsKey(Meta.OPTS)) ?
                (string)data[Meta.OPTS] : null;
        }

        public override string ToString()
        {
            return Type() + " " + Text();
        }
    }

}