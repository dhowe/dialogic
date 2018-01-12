using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace Dialogic
{

    // NEXT: lexing variables

    /* TODO: 
        Variables
        If/thens
        Meta-tagging and chat-search (linq)

        Update documentation in readme

        Verify chat-name uniqueness on parse
        Allow decimals like .4
    */

    public class ChatParser : DialogicBaseVisitor<Chat>
    {
        protected List<Chat> chats;
        protected Stack<Command> parsed;

        public ChatParser()
        {
            chats = new List<Chat>();
            parsed = new Stack<Command>();
        }

        public static void Main(string[] args)
        {
            //List<Chat> chats = ChatParser.ParseText("SAY Welcome to my $var1 world\n");
            List<Chat> chats = ChatParser.ParseFile("gscript.gs");//"gscript.gs" 
            ChatManager cm = new ChatManager(chats);

            ConsoleClient cl = new ConsoleClient(); // Example client

            cl.Subscribe(cm); // Client subscribe to chat events
            cm.Subscribe(cl); // Dialogic subscribes to Unity events

            cm.Run();
        }

        public override string ToString()
        {
            string s = "\n";
            chats.ForEach(cmd => s += ((cmd is Chat c ? c.ToTree() : cmd.ToString()) + "\n"));
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

        public static List<Chat> ParseFile(string fname)
        {
            return ParseText(File.ReadAllText(fname, Encoding.UTF8));
        }

        public static List<Chat> ParseText(string text)
        {
            ChatParser cp = new ChatParser();
            DialogicParser parser = CreateParser(new AntlrInputStream(text));
            ParserRuleContext prc = parser.script();
            cp.Visit(prc);
            PrintLispTree(parser, prc);
            Console.WriteLine(cp);
            return cp.chats;
        }

        private static DialogicParser CreateParser(ICharStream txt)
        {
            ITokenSource lexer = new DialogicLexer(txt);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            return new DialogicParser(tokens);
        }

        static void PrintLispTree(DialogicParser parser, ParserRuleContext prc)
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

        public static void Out(object s) => Console.WriteLine(s);

        public static void Err(object s) => Console.WriteLine("\n[ERROR] " + s + "\n");

        public List<Chat> Chats() => chats;
    }
}
