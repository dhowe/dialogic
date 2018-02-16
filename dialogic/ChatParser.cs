using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System.Text.RegularExpressions;
using Dialogic.Antlr;

namespace Dialogic
{
    public class ChatParser : DialogicBaseVisitor<Chat>
    {
        protected List<Chat> chats;
        protected Stack<Command> parsed;
        protected Meta lastMeta; // remove

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

        /*public static List<Chat> ParseFileOrig(string fname)
          return ParseText(File.ReadAllText(fname, Encoding.UTF8));*/

        public static List<Chat> ParseText(string text)
        {
            return Parse(text.Split('\n'));
        }

        public static List<Chat> ParseFile(string fname)
        {
            return Parse(File.ReadAllLines(fname));
        }

        public static void ParseFiles(string[] files, List<Chat> chats)
        {
            foreach (var f in files) ParseFile(f, chats);
        }

        public static void ParseFile(string fname, List<Chat> chats)
        {
            var result = Parse(File.ReadAllLines(fname));
            result.ForEach((f) => chats.Add(f));
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

            PrintLispTree(prc.ToStringTree(parser));
            Console.WriteLine(cp);

            return cp.chats;
        }

        // tmp_hack to handle appending the default (say) command
        private static void HandleDefaultCommand(string[] lines, string cmd)
        {
            for (int i = 0; i < lines.Count(); i++)
            {
                if (!string.IsNullOrEmpty(lines[i]))
                {
                    //System.Console.WriteLine($"Checking: '{lines[i]}'");
                    if (!Regex.IsMatch(lines[i], @"(^[A-Z][A-Z][A-Z]?[A-Z]?[^A-Z])"))
                    {
                        System.Console.WriteLine("SAY: "+lines[i]);
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

        public static void PrintLispTree(string tree)
        {
            //string tree = prc.ToStringTree(parser);
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
            if (actx == null) return VisitChildren(context); // shouldn't happen

            var xargs = actx.children.Where(arg => arg is DialogicParser.ArgContext).ToArray();
            string[] args = Array.ConvertAll(xargs, arg => arg.GetText().Trim());

            var xmeta = actx.children.Where(md => md is DialogicParser.MetaContext).ToArray();
            string[] meta = xmeta.Length > 0 ? xmeta[0].GetText().Split(','): null;

            Console.WriteLine("cmd: " + cmd + " args: '" + String.Join(",", args) + "' " + meta);

            Command c = Command.Create(cmd, args, meta);
            if (c is Chat)
            {
                chats.Add((Chat)c);
            }
            else
            {
                if (chats.Count == 0) CreateDefaultChat();
                HandleCommandTypes(c);
            }

            parsed.Push(c);

            return VisitChildren(context);
        }

        private void HandleCommandTypes(Command c) // cleanup
        {
            if (c is Opt)
            {
                Opt o = (Opt)c;
                // add option data to last Ask
                Command last = LastOfType(parsed, typeof(Ask));
                if (!(last is Ask)) throw new Exception("Opt must follow Ask");
                ((Ask)last).AddOption(o);
            }
            else if (c is Meta)
            {
                // store meta key-values for subsequent line
                this.lastMeta = (Meta)c;
            }
            else if (c.GetType() == typeof(Cond))
            {
                Cond cd = (Cond)c;

                // add cond criteria to last Chat
                Command last = LastOfType(parsed, typeof(Chat));
                if (!(last is Chat))
                {
                    throw new Exception("Cond must follow Chat");
                }
                ((Chat)last).AddMeta(cd.ToDict());
            }
            else
            {
                // add command to last Chat
                chats.Last().AddCommand(c);
            }

            // add meta key-values to subsequent line
            if (this.lastMeta != null && c is Say || c is Chat)
            {
                c.AddMeta(this.lastMeta.ToDict());
                this.lastMeta = null;
            }
        }

        private void CreateDefaultChat()
        {
            Chat def = new Chat();
            chats.Add(def);
            parsed.Push(def);
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

    public class LexerTest : DialogicBaseVisitor<Chat>
    {
        /*private static void printPrettyLispTree(String tree)
        {
            Console.WriteLine("[PARSE-TREE]");
            int indentation = 1;
            foreach (char c in tree)
            {
                if (c == '(')
                {
                    if (indentation > 1)
                    {
                        Console.WriteLine();
                    }
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
            Console.WriteLine();
        }*/

        public void TestParse(string source)
        {
            string[] lines = File.ReadAllLines(source);
            ITokenSource lexer = new DialogicLexer(new AntlrInputStream(String.Join("\n", lines)));
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            DialogicParser parser = new DialogicParser(tokens);
            ParserRuleContext context = parser.script();
            Visit(context);

            String tree = context.ToStringTree(parser);
            ChatParser.PrintLispTree(tree);
        }
    }
}
