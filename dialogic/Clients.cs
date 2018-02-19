using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Out = System.Console;

namespace Dialogic
{
    /** A very simple client: receives events but doesn't respond */
    class SimpleClient : AbstractClient
    {
        protected override void OnChatEvent(ChatEvent e)
        {
            Command cmd = e.Command;
            Console.WriteLine(cmd);
        }
    }

    /** A basic parent class for clients */
    public abstract class AbstractClient
    {
        public delegate void UnityEventHandler(EventArgs e);
        public event UnityEventHandler UnityEvents;

        protected abstract void OnChatEvent(ChatEvent e);

        public void Subscribe(ChatRuntime cs)
        {
            cs.ChatEvents += new ChatRuntime.ChatEventHandler(OnChatEvent);
        }

        protected void Fire(EventArgs e)
        {
            if (UnityEvents != null) UnityEvents.Invoke(e);
        }
    }

    /** An example client that uses the console */
    public class ConsoleClient : AbstractClient
    {
        protected string suffix = "";

        public ConsoleClient()
        {
            Thread t = new Thread(MockEvents) { IsBackground = true };
            //t.Start(); // to add some mock random client events
        }

        protected override void OnChatEvent(ChatEvent e)
        {
            Command c = e.Command;

            if (c is IEmittable)
            {
                Out.WriteLine(c is Do ? "(Do:" + c.Text + ") "+c.PauseAfterMs : c.Text);
                if (c is Ask) Prompt((Dialogic.Ask)c);
            }
        }

        private void Prompt(Ask a)
        {
            var opts = a.Options();

            // Display the possible options
            for (int i = 0; i < opts.Count; i++)
            {
                Out.WriteLine("(" + (i + 1) + ") " + opts[i].Text
                    + " => [" + opts[i].ActionText() + "]");
            }

            Opt chosen = null;
            do
            {
                // And prompt the user for their choice
                try
                {
                    string res = ConsoleReader.ReadLine(a, a.PauseAfterMs);
                    int i = -1;
                    try
                    {
                        i = Convert.ToInt32(res);
                    }
                    catch { /* ignore */ }

                    chosen = a.Selected(--i);
                }
                catch (Exception e)
                {
                    if (e is PromptTimeout) Out.WriteLine("\nHey! Anyone home?");
                    Out.WriteLine("Choose an option from 1-" + opts.Count + "\n");
                }

            } while (chosen == null);

            // Print the selected option
            Out.WriteLine("    (Opt#" + (a.SelectedIdx + 1 + " selected")
                + " => [" + a.Selected().ActionText() + "])\n");

            Fire(new ChoiceEvent(a.SelectedIdx));
        }

        protected void MockEvents()
        {
            while (true)
            {
                int ms = new Random().Next(2000, 5000);
                Thread.Sleep(ms);
                Fire(new ClientEvent());
            }
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
            pool = new ObjectPool<GuppyEvent>(10, () => new GuppyEvent(), (g => g.Clear()));

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
                if (cmd.HasMeta()) cmd.AsDict().ToList()
                    .ForEach(x => ge.data[x.Key] = x.Value);
                modified = true;
                nextEvent = ge;
            }
        }
    }
}