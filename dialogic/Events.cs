using System;
using System.Collections.Generic;

namespace Dialogic
{
    public interface IUpdateEvent
    {
        string Text();
        string Type();
        string Get(string name, string def = null);
        int GetInt(string name, int def = -1);
        IDictionary<string, object> Data();
        void RemoveKeys(params string[] keys);
    }

    public interface IChoice
    {
        int GetChoiceIndex();
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

    public class ChatEvent : EventArgs
    {
        protected Command command;

        public ChatEvent(Command c)
        {
            this.command = c;
        }

        public Command Command()
        {
            return command;
        }
    }

    public class ClientEvent : EventArgs
    {
        protected string message;

        internal ClientEvent() : this("User-Taps-Glass") { }

        public ClientEvent(string s)
        {
            this.message = s;
        }

        public override string ToString()
        {
            return this.message;
        }

        public string Message
        {
            get
            {
                return message;
            }
        }
    }

    public class ChoiceEvent : EventArgs, IChoice
    {
        protected int choiceIndex;

        public ChoiceEvent(int option)
        {
            this.choiceIndex = option;
        }

        public override string ToString()
        {
            return "Choice: " +choiceIndex;
        }

        public int GetChoiceIndex()
        {
            return choiceIndex;
        }
    }
}