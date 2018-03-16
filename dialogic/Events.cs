using System;
using System.Collections.Generic;

namespace Dialogic
{
    public interface IUpdateEvent
    {
        string Text();
        string Type();
        string Get(string name, string def = null);
        void RemoveKeys(params string[] keys);
        IDictionary<string, object> Data();

        int GetInt(string name, int def = -1);
        bool GetBool(string key, bool def = false);
        double GetDouble(string key, double def = -1);
        float GetFloat(string key, float def = -1);
    }

    public class UpdateEvent : IUpdateEvent
    {
        private IDictionary<string, object> data;

        public UpdateEvent(Command c)
        {
            this.data = c.realized;
        }

        public UpdateEvent(IDictionary<string, object> data)
        {
            this.data = data;
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
    }

    /// /////////////////////////////////////////////////////////

    public interface IChoice
    {
        int GetChoiceIndex();
    }

    /**
     * Tells Dialogic to restart the current Chat after an indefinite WAIT cmd,
     * optionally using a specific chat, specified with its Label
     */
    public interface IResume
    {
        string ResumeWith();
    }

    /**
     * Superclass for specific GameEvents
     */
    public abstract class GameEvent : EventArgs { }

    /**
     * Tells Dialogic to restart the current Chat after an indefinite WAIT cmd,
     * optionally with a new Chat, specified by its Label
     */
    public class ResumeEvent : GameEvent, IResume
    {
        public readonly string chatLabel;

        public ResumeEvent(string chatLabel = null) : base()
        {
            this.chatLabel = chatLabel;
        }

        public string ResumeWith()
        {
            return chatLabel;
        }
    }

    /**
     * Tells Dialogic that an Ask->Option has been selected by the user
     */
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
}