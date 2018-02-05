using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System.Text.RegularExpressions;

namespace Dialogic
{
    public class ChatParser : DialogicBaseVisitor<Chat>
    {
        protected List<Chat> chats;
        protected Stack<Command> parsed;

        public ChatParser()
        {
            chats = new List<Chat>();
            parsed = new Stack<Command>();
        }

        public override string ToString()
        {
            string s = "\n";
            chats.ForEach(cmd => s += ((cmd is Chat ? 
                ((Chat)cmd).ToTree() : cmd.ToString()) + "\n"));
            return s;
        }

        private void AddChat(Chat c)
        {
            chats.Add(c);
        }

        private Command LastOfType(Stack<Command> s, Type typeToFind)
        {
            foreach (Command c in s)
            {
                if (c.GetType() == typeToFind) return c;
            }
            return null;
        }

        //public static List<Chat> ParseFileOrig(string fname)
        //    return ParseText(File.ReadAllText(fname, Encoding.UTF8));

        public static List<Chat> ParseText(string text)
        {
            return Parse(text.Split('\n'));
        }

        public static List<Chat> ParseFile(string fname)
        {

            return Parse(File.ReadLines(fname).ToArray());
        }

        protected static List<Chat> Parse(string[] lines)
        {
            HandleDefaultCommand(lines, "SAY");
            var ais = new AntlrInputStream(String.Join("\n", lines));
            DialogicParser parser = CreateParser(ais);
            parser.ErrorHandler = new BailErrorStrategy();
            ParserRuleContext prc = parser.script();
            ChatParser cp = new ChatParser();
            cp.Visit(prc);
            PrintLispTree(parser, prc);
            Console.WriteLine(cp);
            return cp.chats;
        }

        // tmp_hack to handle appending the default command
        private static void HandleDefaultCommand(string[] lines, string cmd)
        {
            for (int i = 0; i < lines.Count(); i++)
            {
                if (!string.IsNullOrEmpty(lines[i]))
                {
                    //System.Console.WriteLine($"Checking: '{lines[i]}'");
                    if (!Regex.IsMatch(lines[i], @"(^[A-Z][A-Z][A-Z]?[A-Z]?[^A-Z])"))
                    {
                        //System.Console.WriteLine($"+SAY: {lines[i]}");
                        lines[i] = cmd + " " + lines[i];
                    }
                }
            }
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
            Console.WriteLine("\nPARSE-TREE");
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
            var args = Array.ConvertAll(xargs, arg => arg.GetText().Trim());

            //Console.WriteLine("cmd: " + cmd + " args: '" + String.Join(",",args)    + "'");

            Command c = Command.Create(cmd, args);
            if (c is Chat)
            {
                chats.Add((Chat)c);
            }
            else if (c is Opt)
            {
                Opt o = (Opt)c;
                Command last = LastOfType(parsed, typeof(Ask));
                if (!(last is Ask))
                {
                    throw new Exception("Opt must follow Ask");
                }
                ((Ask)last).AddOption(o);
            }
            else
            {
                if (chats.Count == 0) chats.Add(new Chat());
                chats.Last().AddCommand(c);
            }

            parsed.Push(c);

            return VisitChildren(context);
        }

        public static void Out(object s)
        {
            Console.WriteLine(s);
        }

        public static void Err(object s)
        {
            Console.WriteLine("\n[ERROR] " + s + "\n");
        }

        public List<Chat> Chats()
        {
            return chats;
        }
    }
}
