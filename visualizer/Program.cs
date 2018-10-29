using System;
using System.Collections.Generic;
using System.IO;
using Client;

namespace Dialogic.Visualizer
{
    class Program
    {
        static void Main(string[] args)
        {
            AppConfig config = AppConfig.TAC;

            var testfile = AppDomain.CurrentDomain.BaseDirectory;
            testfile += "../../../../dialogic/data/allchats.gs";

            ChatRuntime runtime = new ChatRuntime(config);

            var watch = System.Diagnostics.Stopwatch.StartNew();

            runtime.ParseFile(new FileInfo(testfile));

            List<Chat> chats = runtime.Chats();
            var i = 0;
            foreach (var chat in chats)
            {
                var name = chat.text;
                var labels = Labels(chat);
                if (labels.Count > 0)
                {
                    Console.WriteLine((i++) + ") " + name + ": " + labels.Stringify());
                }
            }
        }

        static List<string> Labels(Chat chat)
        {
            List<string> labels = new List<string>();
            chat.commands.ForEach(c =>
            {
                if (c is Go)
                {
                    labels.Add(c.text);
                }
                else if (c is Ask a)
                {
                    var opts = a.Options();
                    opts.ForEach(o =>
                    {
                        if (!o.action.text.IsNullOrEmpty()) {
                            labels.Add(o.action.text);
                        }
                    });
                }
            });
            return labels;
        }
    }
}
