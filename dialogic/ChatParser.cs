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
        public static string CHAT_FILE_EXT = ".gs";

        protected List<Chat> chats;
        protected Stack<ICommand> parsed;

        public ChatParser()
        {
            chats = new List<Chat>();
            parsed = new Stack<ICommand>();
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

        private ICommand LastOfType(Stack<ICommand> s, Type typeToFind)
        {
            foreach (ICommand c in s)
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

        public static List<Chat> ParseFile(string fileOrFolder)
        {
            string[] files = !fileOrFolder.EndsWith(CHAT_FILE_EXT, StringComparison.InvariantCulture) ?
                files = Directory.GetFiles(fileOrFolder, '*' + CHAT_FILE_EXT) :
                files = new string[] { fileOrFolder };

            List<Chat> chats = new List<Chat>();
            ChatParser.ParseFiles(files, chats);

            return chats;
        }

        public static List<Chat> ParseFiles(string[] files)
        {
            List<Chat> chats = new List<Chat>();
            foreach (var f in files) ParseFile(f, chats);
            return chats;
        }

        internal static void ParseFiles(string[] files, List<Chat> chats)
        {
            foreach (var f in files) ParseFile(f, chats);
        }

        internal static void ParseFile(string fname, List<Chat> chats)
        {
            var result = Parse(File.ReadAllLines(fname));
            result.ForEach((f) => chats.Add(f));
        }

        internal static List<Chat> Parse(string[] lines, bool printTree = false)
        {
            HandleDefaultCommand(lines, "SAY");
            var ais = new AntlrInputStream(String.Join("\n", lines));
            DialogicParser parser = CreateParser(ais);
            parser.ErrorHandler = new BailErrorStrategy();
            ParserRuleContext prc = parser.script();
            ChatParser cp = new ChatParser();
            cp.Visit(prc);

            if (printTree)
            {
                PrintLispTree(prc.ToStringTree(parser));
                Console.WriteLine(cp);
            }

            return cp.chats;
        }

        // tmp_hack to handle appending the default (SAY) command
        private static void HandleDefaultCommand(string[] lines, string cmd)
        {
            for (int i = 0; i < lines.Count(); i++)
            {
                if (!string.IsNullOrEmpty(lines[i]))
                {
                    //System.Console.WriteLine($"Checking: '{lines[i]}'");
                    if (!Regex.IsMatch(lines[i], @"(^[A-Z][A-Z][A-Z]?[A-Z]?[^A-Z])"))
                    {
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
            string[] meta = xmeta.Length > 0 ? xmeta[0].GetText().Split(',') : null;

            //Console.WriteLine("cmd: " + cmd + " args: '" + String.Join(",", args) + "' " + meta);

            ICommand c = Command.Create(cmd, args, meta);
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

        private void HandleCommandTypes(ICommand c) // cleanup
        {
            if (c is Opt) // add option data to last Ask
            {
                ICommand last = LastOfType(parsed, typeof(Ask));
                if (!(last is Ask)) throw new Exception("Opt must follow Ask");
                ((Ask)last).AddOption((Opt)c);
            }
            else  // add command to last Chat
            {
               
                chats.Last().AddCommand(c);
            }
            /*else if (c is Meta)
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
            }*/
   
            // add meta key-values to subsequent line
            /*if (this.lastMeta != null && c is Say || c is Chat)
            {
                c.AddMeta(this.lastMeta.ToDict());
                this.lastMeta = null;
            }*/
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
