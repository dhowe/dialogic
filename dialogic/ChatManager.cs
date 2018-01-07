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
        Handle timeout
        Handle no Opts given
        
        Verify chat-name uniqueness on parse
        Allow decimals like .4
    */

    public class ConsoleListener : IChatListener
    {
        private string suffix = "";

        void IChatListener.onChatEvent(ChatManager chatMan, Command c)
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

            if (c is Ask a)
            {
                for (int i = 0; i < a.options.Count; i++)
                {
                    Opt opt = a.options[i];
                    Console.WriteLine("(" + (i + 1) + ") " + opt.Text + " => [" + opt.action.Text + "]");
                }

                Command next = a.Choose(Console.ReadKey(true).KeyChar.ToString());;
                while (next == null)
                {
                    Console.WriteLine("\nPlease select an option from 1-" + a.options.Count + "\n");
                    next = a.Choose(Console.ReadKey(true).KeyChar.ToString());
                }

                Console.WriteLine("    (selected Opt#" + (a.selectedIdx + 1)
                    + " => [" + a.Selected().action.Text + "]\n");
                
                chatMan.Do(next);
            }
        }
    }

    public class ChatManager : GuppyScriptBaseVisitor<Chat>
    { // TODO: break into Manager/Runner

        List<Chat> chats = new List<Chat>();
        //Stack<Command> events = new Stack<Command>();
        Stack<Command> parsed = new Stack<Command>();
        List<IChatListener> listeners = new List<IChatListener>();

        public static void Main(string[] args)
        {
            ChatManager sman = new ChatManager();
            sman.AddListener(new ConsoleListener());
            //sman.Parse("[Say] I would like to emphasize this\n[Wait] 1.5\n");
            sman.ParseFile("guppy-script.gs");
            Console.WriteLine(sman);
            sman.Run();
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
                Run(FindByName(cmd.Text));
            }
            NotifyListeners(cmd);
        }

        public void Run(Chat chat)
        {
            //Console.WriteLine($"Start: {chat.Text}");
            NotifyListeners(chat); // for the Chat itself
            chat.commands.ForEach((c) => Do(c));
        }

        public override string ToString()
        {
            string s = "";
            chats.ForEach(c => s += c + "\n");
            return s;
        }

        private void AddChat(Chat c) => chats.Add(c);

        public void Run()
        {
            if (chats == null || chats.Count < 1)
            {
                throw new Exception("No chats found!");
            }
            Run(chats[0]);
        }

        private Command LastOfType(Stack<Command> s, Type typeToFind)
        {
            foreach (Command c in s)
            {
                if (c.GetType() == typeToFind) return c;
            }
            return null;
        }

        private void NotifyListeners(Command c)
        {
            listeners.ForEach(icl => icl.onChatEvent(this, c));
            //c.Fire();
        }

        public void Run(string name) => Run(FindByName(name));

        public Chat FindByName(string chatName)
        {
            for (int i = 0; i < chats.Count; i++)
            {
                if (chats[i].Text == chatName)
                    return chats[i];
            }
            throw new KeyNotFoundException(chatName);
        }

        public Chat ParseFile(string fname)
        {
            return ParseText(File.ReadAllText(fname, Encoding.UTF8));
        }

        public Chat ParseText(string text)
        {
            GuppyScriptParser parser = CreateParser(new AntlrInputStream(text));
            IParseTree tree = parser.dialog();
            //graph = (Chat) Command.Create("Chat", Path.GetFileNameWithoutExtension(text));
            return Visit(tree);
        }

        public void AddListener(IChatListener icl)
        {
            this.listeners.Add(icl);
        }

        private GuppyScriptParser CreateParser(ICharStream txt)
        {

            ITokenSource lexer = new GuppyScriptLexer(txt);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            return new GuppyScriptParser(tokens);
        }

        public static void Out(object s) => Console.WriteLine(s);

        public static void Err(object s) => Console.WriteLine("\n[ERROR] " + s + "\n");

        public override Chat VisitLine([NotNull] GuppyScriptParser.LineContext context)
        {

            var cmd = context.GetChild<GuppyScriptParser.CommandContext>(0).GetText();
            var args = context.GetChild<GuppyScriptParser.ArgsContext>(0).GetText();
            //Console.WriteLine("cmd: " + cmd+" args: " + args);
            Command c = Command.Create(cmd, args);
            if (c is Chat)
            {
                chats.Add((Chat)c);
            }
            else if (c is Opt o)
            {
                Command last = LastOfType(parsed, typeof(Ask));
                if (!(last is Ask a))
                {
                    throw new Exception("Opt must be preceded by Ask");
                }
                a.AddOption(o);
            }
            else
            {
                if (chats.Count == 0)
                {
                    chats.Add(new Chat());
                }
                //Console.WriteLine("Adding: " + c.GetType() + " to "+ chats.Last().text);
                chats.Last().AddCommand(c);
            }

            parsed.Push(c);

            return VisitChildren(context);
        }

        /*public Chat VisitOpt(RuleContext context) {

            Command last = LastOfType(dialog.events, typeof(Ask));
            if (!(last is Ask)) throw new Exception("Opt must be preceded by Ask");
            string[] args = ParseArgs(context);
            Ask ask = (Ask) last;
            switch (args.Length) {
                case 2:
                    ask.AddOption(args[0], args[1]);
                    break;
                case 1:
                    ask.AddOption(args[0]);
                    break;
                default:
                    throw new Exception("Invalid # of args");
            }

            //dialog.AddEvent(new Opt(dialog, args[0]));
            return VisitChildren(context);
        }

        private List<ArgsContext> FindChildren(RuleContext parent, Type typeToFind) {
            if (parent == null) {
                throw new Exception("Unexpected parent value: null");
            }
            List<ArgsContext> result = new List<ArgsContext>();
            for (int i = 0; i < parent.ChildCount; i++) {
                var child = parent.GetChild(i);
                if (child.GetType() == typeToFind) {
                    result.Add((ArgsContext) child);
                }
            }
            if (result == null) {
                throw new Exception("No body found for: " + parent.GetText());
            }
            return result;
        }*/

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
}
