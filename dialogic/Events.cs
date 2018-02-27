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
            return (string)(data.ContainsKey(key) ? data[key] : def);
        }

        public int GetInt(string key, int def = -1)
        {
            string s = Get(key, def + "");
            return (int)Convert.ChangeType(key, typeof(int));
        }

        public void RemoveKeys(params string[] keys)
        {
            throw new NotImplementedException();
        }

        public string Text()
        {
            return (string)data["text"];
        }

        public string Type()
        {
            return (string)data["type"];
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


    public class UpdateEventOld
    {
        public Dictionary<string, object> data;

        public UpdateEventOld()
        {
            data = new Dictionary<string, object>();
        }

        public override string ToString()
        {
            return "[" + data["Type"] + "] " + data["Text"];
        }

        public void Set(string key, object val)
        {
            data[key] = val;
        }

        public object Get(string key)
        {
            return data.ContainsKey(key) ? data[key] : null;
        }

        public object Remove(string key)
        {
            if (data.ContainsKey(key))
            {
                object o = data[key];
                data.Remove(key);
                return o;
            }
            return null;
        }

        public int GetInt(string key)
        {
            object o = Get(key);
            return (o != null && o is int) ? (int)o : -1;
        }

        public bool GetBool(string key)
        {
            object o = Get(key);
            return (o != null && o is bool) && (bool)o;
        }

        public double GetDouble(string key)
        {
            object o = Get(key);
            return (o != null && o is double) ? (double)o : -1;
        }

        public int RemoveInt(string key)
        {
            object o = Remove(key);
            return (o != null && o is int) ? (int)o : -1;
        }

        public bool RemoveBool(string key)
        {
            object o = Remove(key);
            return (o != null && o is bool) && (bool)o;
        }

        public double RemoveDouble(string key)
        {
            object o = Remove(key);
            return (o != null && o is double) ? (double)o : -1;
        }

        public void Clear()
        {
            foreach (var k in data.Keys)
            {
                data.Remove(k);
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