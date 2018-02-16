using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            //new LexerTest().TestParse(srcpath + "/data/meta.gs");
            //ChatParser.ParseText("ASK Game?\nOPT Sure\nOPT $neg\n");

            List<Chat> chats = ChatParser.ParseFile(srcpath + "/data/gscript.gs");
            ChatRuntime cm = new ChatRuntime(chats, globals);
            cm.LogFile = srcpath + "/dia.log";

            //ChatClient cl = new SimpleClient(); // Simple client
            AbstractClient cl = new ConsoleClient(); // Console client

            cl.Subscribe(cm); // Client subscribes to chat events
            cm.Subscribe(cl); // Dialogic subscribes to Unity events

            cm.Run();
        }
    }

    public class MockGameEngine
    {
        double millisPerFrame, targetFPS = 60;
        int lastFrameMs = 0, frameCount = 0;
        string diaText, diaType;
        string[] diaOpts;
        GuppyAdapter guppy;

        public MockGameEngine()
        {
            lastFrameMs = Util.Elapsed();
            millisPerFrame = 1000 / targetFPS;
            guppy = new GuppyAdapter(Program.srcpath + "/data/gscript.gs", Program.globals);
        }

        public void Run()
        {
            while (true)
            {
                if (Util.Elapsed() - lastFrameMs > millisPerFrame)
                {
                    frameCount++;

                    // Call the dialogic interface
                    GuppyEvent ge = guppy.Update(Program.globals, null);

                    // Handle the returned event
                    if (ge != null) HandleEvent(ge);

                    lastFrameMs = Util.Elapsed();
                }
                Thread.Sleep(1);
            }
        }

        private void HandleEvent(GuppyEvent ge)
        {
            diaText = (string)ge.Remove("text");
            diaType = (string)ge.Remove("type");

            switch (diaType)
            {
                case "Say":
                    diaText += " " + Util.Stringify(ge.data);
                    break;

                case "Do":
                    diaText = "(Do: " + diaText.Trim() + ")";
                    break;

                case "Ask":
                    var opts = ge.Remove("opts");

                    diaText += " " + Util.Stringify(ge.data);
                    diaOpts = ((string)opts).Split(',');

                    for (int i = 0; i < diaOpts.Length; i++)
                    {
                        diaText += "\n  (" + i + ") " + diaOpts[i];
                    }

                    int timeout = ge.RemoveInt("timeout");
                    if (timeout > -1)
                    {
                        Timers.SetTimeout(timeout, () =>
                        {
                            Console.WriteLine("<empty-choice-event>");
                        });
                    }
                    break;

                default:
                    throw new Exception("Bad event: " + ge);
            }

            Print(diaText);
            ge = null;  // disose event 
        }

        private void Print(string s, bool addInfo = false)
        {
            if (addInfo) s = "#" + frameCount + "@" + Util.ElapsedSec() + "\t" + s;
            Console.WriteLine(s);
        }
    }

    public class GuppyAdapter : AbstractClient
    {
        static string FILE_EXT = ".gs";

        ObjectPool<GuppyEvent> pool;
        GuppyEvent nextEvent;
        ChatRuntime runtime;

        bool modified = false;

        public GuppyAdapter(string fileOrFolder) : this(fileOrFolder, null) { }

        public GuppyAdapter(string fileOrFolder, Dictionary<string, object> globals)
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

        public GuppyEvent Update(Dictionary<string, object> worldState, EventArgs gameEvent)
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
                    if (a.PauseAfterMs > -1)
                    {
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
