using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace Dialogic
{

    /* NEXT: 
        Handle Ask timeout (defaultTime=10sec, defaultAction=repeat)

        Handle variable if/then
        Handle meta-tagging and chat-search (linq)

        Update documentation in read me

        Verify chat-name uniqueness on parse
        Allow decimals like .4
    */

    public class ChatParser : DialogicBaseVisitor<Chat>
    {
        protected List<Chat> chats;
        protected Stack<Command> parsed;

        public ChatParser() {
            chats = new List<Chat>();
            parsed = new Stack<Command>();
        }

        public static void Main(string[] args)
        {
            //List<Chat> chats = ChatParser.ParseText("[Say] I would like to emphasize this\n[Wait] 1.5\n");
            List<Chat> chats = ChatParser.ParseFile("gscript.gs");//"gscript.gs" 
            ChatExecutor sched = new ChatExecutor(chats);
            ConsoleClient cl = new ConsoleClient();
            cl.Subscribe(sched);
            //sched.AddListener(new ConsoleListener());
            sched.Run();
        }

        public override string ToString()
        {
            string s = "\n";
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

        public static List<Chat>ParseFile(string fname)
        {
            return ParseText(File.ReadAllText(fname, Encoding.UTF8));
        }

        public static List<Chat> ParseText(string text)
        {
            ChatParser cp = new ChatParser();
            DialogicParser parser = CreateParser(new AntlrInputStream(text));
            ParserRuleContext tree = parser.tree();
            cp.Visit(tree);
            PrintLispTree(parser, tree);
            Console.WriteLine(cp);
            return cp.chats;
        }

        private static void PrintLispTree(DialogicParser parser, ParserRuleContext prc)
        {
            string tree = prc.ToStringTree(parser);
            int indentation = 1;
            Console.WriteLine("PARSE-TREE");
            foreach (char c in tree)
            {
                if (c == '(')
                {
                    if (indentation > 1) Console.WriteLine();
                    for (int i = 0; i < indentation; i++)
                    {
                        Console.Write("  ");
                    }
                    indentation++;
                }
                else if (c == ')')
                {
                    indentation--;
                }
                Console.Write(c);
            }
            Console.WriteLine("\n");
        }


        private static DialogicParser CreateParser(ICharStream txt)
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
            var actx = context.GetChild<DialogicParser.ArgsContext>(0);
            var xargs = actx.children.Where(arg => arg is DialogicParser.ArgContext).ToArray();
            var args = Array.ConvertAll(xargs, arg => arg.GetText());

            //Console.WriteLine("cmd: " + cmd + " args: " + String.Join(",",args));
 
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

            ChatParser cman = new ChatParser();
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
