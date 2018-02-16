using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Dialogic;

namespace runner
{
    class Program
    {
        public static string srcpath = "../../../dialogic";
        public static Dictionary<string, object> globals = 
            new Dictionary<string, object>() {
                { "emotion", "special" },
                { "place", "My Tank" },
                { "Happy", "HappyFlip" },
                { "verb", "play" },
                { "neg", "(nah|no|nope)" },
                { "var3", 2 }
            };

        public static void Main(string[] args)
        {
            new MockGameEngine().Run();
        }

        public static void MainOff(string[] args)
        {
            List<Chat> chats = ChatParser.ParseFile(srcpath + "/data/gscripts.gs");
            // ChatParser.ParseText("ASK Game?\nOPT Sure\nOPT $neg\n");
            ChatRuntime cm = new ChatRuntime(chats, globals);
            cm.LogFileName = srcpath + "/dia.log";

            //ChatClient cl = new SimpleClient(); // Simple client
            AbstractClient cl = new ConsoleClient(); // Console client

            cl.Subscribe(cm); // Client subscribes to chat events
            cm.Subscribe(cl); // Dialogic subscribes to Unity events

            cm.Run();
        }
    }

    public class GuppyTalk : AbstractClient
    {
        static string FILE_EXT = ".gs";

        ObjectPool<GuppyEvent> pool;
        GuppyEvent nextEvent;
        ChatRuntime runtime;

        bool modified = false;

        public GuppyTalk(string fileOrFolder) : this(fileOrFolder, null) { }

        public GuppyTalk(string fileOrFolder, Dictionary<string, object> globals)
        {
            pool = new ObjectPool<GuppyEvent>(() => new GuppyEvent(), (g => g.Clear()));

            runtime = new ChatRuntime(Parse(fileOrFolder), globals);
            this.Subscribe(runtime);
            runtime.Run();
        }

        private static List<Chat> Parse(string fileOrFolder)
        {
            string[] files = !fileOrFolder.EndsWith(FILE_EXT, StringComparison.InvariantCulture) ?
                files = Directory.GetFiles(fileOrFolder, '*' + FILE_EXT) :
                files = new string[] { fileOrFolder };
            List<Chat> chats = new List<Chat>();
            ChatParser.ParseFiles(files, chats);

            return chats;
        }

        public GuppyEvent Update(Dictionary<string, object> worldState)
        {
            //Console.WriteLine("#" + (++frameCount) + ": " + nextEvent);
            runtime.Globals(worldState);
            var result = modified ? nextEvent : null;
            modified = false;
            return result;
        }

        protected override void OnChatEvent(ChatEvent e)
        {
            if (e.Command is IEmittable)
            {
                GuppyEvent ge = pool.Get();
                Command cmd = e.Command;
                ge.Set("text", cmd.Text);
                ge.Set("type", cmd.TypeName());
                if (cmd is Ask)
                {
                    Ask a = (Dialogic.Ask)cmd;
                    ge.Set("opts", a.OptionsJoined());
                    if (a.PauseAfterMs > -1) {
                        ge.Set("timeout", a.PauseAfterMs);
                    }
                }
                if (cmd.HasMeta()) cmd.ToDict().ToList()
                    .ForEach(x => ge.data[x.Key] = x.Value);
                modified = true;
                nextEvent = ge;
            }
        }
    }

    public class GuppyEvent
    {
        public Dictionary<string, object> data;

        public GuppyEvent()
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

    public class ObjectPool<T>
    {
        private ConcurrentBag<T> pool;
        private Func<T> generator;
        private Action<T> recycler;

        public ObjectPool(Func<T> generator) : this(generator, null) { }

        public ObjectPool(Func<T> generator, Action<T> recycler)
        {
            this.pool = new ConcurrentBag<T>();
            this.generator = generator;
            this.recycler = recycler;
        }

        public T Get()
        {
            T item;
            if (pool.TryTake(out item)) return item;
            return generator();
        }

        public void Recycle(T item)
        {
            recycler(item);
            pool.Add(item);
            Console.WriteLine("Recyled: Count=" + pool.Count);
        }
    }

}
