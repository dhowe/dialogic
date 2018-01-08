using System;
using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System.Linq;
using System.Text;
using System.Threading;

namespace Dialogic
{

    /* NEXT: 
        Add argument separator and test lex/parse (->, =>, ::)
        Handle Ask timeout

        Verify chat-name uniqueness on parse
        Allow decimals like .4
    */

    public class ConsoleListener : IChatListener
    {
        private string suffix = "";

        void IChatListener.onChatEvent(ChatScheduler cs, Command c)
        {
            if (c is Do || c is Chat)
            {
                suffix += "\t[" + c.TypeName() + ": " + c.Text + "]";
            }
            else if (!(c is Wait || c is Opt || c is Go))
            {
                Console.WriteLine(c.Text + suffix);
                suffix = "";
            }

            if (c is Ask a) cs.Do(DoPrompt(a));
        }

        private Command DoPrompt(Ask a)
        {
            var opts = a.Options();
            //Console.WriteLine($"Opts for {a.Text} = {opts}");
            //opts.ForEach((obj) => Console.WriteLine($"opt={obj.Text}"));
            for (int i = 0; i < opts.Count; i++)
            {
                Console.WriteLine("(" + (i + 1) + ") " + opts[i].Text
                    + " => [" + opts[i].ActionText() + "]");
            }

            Command next = a.Choose(Console.ReadKey(true).KeyChar.ToString());
            while (next == null)
            {
                Console.WriteLine("\nChoose an option from 1-" + opts.Count + "\n");
                next = a.Choose(Console.ReadKey(true).KeyChar.ToString());
            }

            Console.WriteLine("    (selected Opt#" + (a.SelectedIdx + 1)
                + " => [" + a.Selected().ActionText() + "]\n");

            return next;
        }
    }

    public class ChatManager : DialogicBaseVisitor<Chat>
    { 

        List<Chat> chats = new List<Chat>();
        Stack<Command> parsed = new Stack<Command>();
        //Stack<Command> events = new Stack<Command>();

        public static void Main(string[] args)
        {
            ChatManager cman = new ChatManager();
            //cman.Parse("[Say] I would like to emphasize this\n[Wait] 1.5\n");
            cman.ParseFile("guppy-script.gs");

            ChatScheduler sched = new ChatScheduler(cman);
            sched.AddListener(new ConsoleListener());
            sched.Start();
        }

        public override string ToString()
        {
            string s = "";
            chats.ForEach(c => s += c + "\n");
            return s;
        }

        private void AddChat(Chat c) => chats.Add(c);

        private Command LastOfType(Stack<Command> s, Type typeToFind)
        {
            foreach (Command c in s)
            {
                if (c.GetType() == typeToFind) return c;
            }
            return null;
        }

        public Chat FindByName(string chatName)
        {
            for (int i = 0; i < chats.Count; i++)
            {
                if (chats[i].Text == chatName)
                    return chats[i];
            }
            throw new KeyNotFoundException(chatName);
        }

        public void ParseFile(string fname)
        {
            ParseText(File.ReadAllText(fname, Encoding.UTF8));
        }

        public void ParseText(string text)
        {
            DialogicParser parser = CreateParser(new AntlrInputStream(text));
            IParseTree tree = parser.tree();
            //graph = (Chat) Command.Create("Chat", Path.GetFileNameWithoutExtension(text));
            Visit(tree);
            Console.WriteLine(this);
        }


        private DialogicParser CreateParser(ICharStream txt)
        {

            ITokenSource lexer = new DialogicLexer(txt);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            return new DialogicParser(tokens);
        }

        public static void Out(object s) => Console.WriteLine(s);

        public static void Err(object s) => Console.WriteLine("\n[ERROR] " + s + "\n");

        public override Chat VisitLine([NotNull] DialogicParser.LineContext context)
        {

            var cmd = context.GetChild<DialogicParser.CommandContext>(0).GetText();
            var args = context.GetChild<DialogicParser.ArgsContext>(0).GetText();
            //Console.WriteLine("cmd: " + cmd+" args: " + args);
            Command c = Command.Create(cmd, args);
            if (c is Chat)
            {
                chats.Add((Chat)c);
            }
            else if (c is Opt o)
            {
                Command last = LastOfType(parsed, typeof(Ask));
                if (!(last is Ask a)) throw new Exception("Opt must follow by Ask");
                a.AddOption(o);
            }
            else
            {
                if (chats.Count == 0) chats.Add(new Chat());
                chats.Last().AddCommand(c);
            }

            parsed.Push(c);

            return VisitChildren(context);
        }

        public List<Chat> Chats()
        {
            return chats;
        }

        public static void Test(string[] args)
        {

            ChatManager cman = new ChatManager();
            Chat c = (new Chat());
            c.Init("Part1");
            cman.AddChat(c);
            c.AddCommand(Command.Create("Say", "Hello"));
            c.AddCommand(Command.Create("Do", "Flip"));
            Chat d = (Chat)Command.Create("Chat", "Part2");
            cman.AddChat(d);
            d.AddCommand(Command.Create("Do", "Flip2"));
            Console.WriteLine(cman);
        }
    }


    public class ChatScheduler
    {

        List<IChatListener> listeners = new List<IChatListener>();
        private ChatManager cman;

        public ChatScheduler(ChatManager cman)
        {
            this.cman = cman;
        }

        public void Do(Command cmd)
        {
            if (cmd is Wait w)
            {
                Thread.Sleep(w.Millis());
            }
            else if (cmd is Go)
            {
                //System.Console.WriteLine($"FINDING {cmd.Text}");
                Run(cman.FindByName(cmd.Text));
            }
            NotifyListeners(cmd);
        }

        public void Run(Chat chat)
        {
            //Console.WriteLine($"Start: {chat.Text}");
            NotifyListeners(chat); // for the Chat itself
            chat.commands.ForEach((c) => Do(c));
        }

        public void Start()
        {
            List<Chat> chats = cman.Chats();
            if (chats == null || chats.Count < 1)
            {
                throw new Exception("No chats found!");
            }
            Run(chats[0]);
        }

        public void Run(string name) => Run(cman.FindByName(name));

        private void NotifyListeners(Command c)
        {
            listeners.ForEach((icl) => {
                if (!(c is NoOp)) icl.onChatEvent(this, c);
            });
        }

        public void AddListener(IChatListener icl)
        {
            this.listeners.Add(icl);
        }
    }
}
