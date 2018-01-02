using System;
using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;
using ArgsContext = GScriptParser.ArgsContext;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;

namespace Dialogic {

    /* NEXT: 
        Verify chat-name uniqueness on parse
    */

    public class ConsoleListener : IChatListener {
        private string suffix = "";

        void IChatListener.onChatEvent(Command c) {
            Out(c);
        }

        protected void Out(Command c) {
            if (c is Wait || c is Go) return;
            if (c is Do || c is Chat) {
                suffix += "\t[" + c.TypeName() + ": " + c.text + "]";
            } else {
                Console.WriteLine(c.text + suffix);
                suffix = "";
            }
        }
    }

    public class ChatManager : GScriptBaseVisitor<Chat> {

        List<Chat> chats = new List<Chat>();
        List<IChatListener> listeners = new List<IChatListener>();

        public static void Main(string[] args) {
            ChatManager sman = new ChatManager();
            sman.AddListener(new ConsoleListener());
            //sman.Parse("[Say] I would like to emphasize this\n[Wait] 1.5\n");
            sman.ParseFile("test-script.gs");
            Console.WriteLine(sman);
            sman.Run();
        }

        public override string ToString() {
            string s = "";
            chats.ForEach(c => s += c + "\n");
            return s;
        }

        private void AddChat(Chat c) => chats.Add(c);

        public void Run() {
            var first = chats[0] ??
                throw new Exception("No chats found!");
            Run(first);
        }

        private void notifyListeners(Command c) {
            listeners.ForEach(icl => icl.onChatEvent(c));
            //c.Fire();
        }

        public void Run(Chat c) {
            Chat next = null;
            notifyListeners(c);
            foreach (var cmd in c.commands) {
                if (cmd is Wait) {
                    Thread.Sleep(((Wait) cmd).Millis());
                }
                if (cmd is Go) {
                    next = findByName(cmd.text);
                    break;
                }
                notifyListeners(cmd);
            }
            if (next != null) Run(next);
        }

        public void Run(string name) => Run(findByName(name));

        public Chat findByName(string chatName) {

            for (int i = 0; i < chats.Count; i++) {
                if (chats[i].text == chatName)
                    return chats[i];
            }
            throw new KeyNotFoundException(chatName);
        }

        public Chat ParseFile(string fname) {
            return ParseText(File.ReadAllText(fname, Encoding.UTF8));
        }

        public Chat ParseText(string text) {
            GScriptParser parser = CreateParser(new AntlrInputStream(text));
            IParseTree tree = parser.dialog();
            //graph = (Chat) Command.create("Chat", Path.GetFileNameWithoutExtension(text));
            return Visit(tree);
        }

        public void AddListener(IChatListener icl) {
            this.listeners.Add(icl);
        }

        private GScriptParser CreateParser(ICharStream txt) {

            ITokenSource lexer = new GScriptLexer(txt);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            GScriptParser parser = new GScriptParser(tokens);
            parser.AddErrorListener(new ThrowExceptionErrorListener());
            return parser;
        }

        public static void Out(object s) => Console.WriteLine(s);

        public static void Err(object s) => Console.WriteLine("\n[ERROR] " + s + "\n");

        public override Chat VisitExpr([NotNull] GScriptParser.ExprContext context) {

            var cmd = context.GetChild<GScriptParser.CommandContext>(0).GetText();
            var args = context.GetChild<GScriptParser.ArgsContext>(0).GetText();
            Command c = Command.create(cmd, args);
            if (c is Chat) {
                chats.Add((Chat) c);
            } else {
                if (chats.Count == 0) {
                    chats.Add(new Chat());
                }
                //Console.WriteLine("Adding: " + c.GetType() + " to "+ chats.Last().text);
                chats.Last().AddCommand(c);
            }
            return VisitChildren(context);
        }

        /*private Command LastOfType(List<Command> commands, Type typeToFind) {
            for (var i = commands.Count - 1; i >= 0; i--) {
                Command c = commands[i];
                if (c.GetType() == typeToFind) {
                    return graph.commands[i];
                }
            }
            return null;
        }*/

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
        }*/

        private string TypeFromContext(RuleContext context, bool qualified = false) {
            var cname = context.GetType().ToString();
            var tname = cname.Replace("GScriptParser+", "").Replace("Context", "");
            return qualified ? "Chatic." + tname : tname;
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
        }

        class ThrowExceptionErrorListener : IAntlrErrorListener<IToken> {
            void IAntlrErrorListener<IToken>.SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol,
                int line, int charPositionInLine, string msg, RecognitionException e) {
                throw new System.Exception("\nSyntaxError: " + line + " " + msg);
            }
        }

        public static void Test(string[] args) {

            ChatManager cman = new ChatManager();
            Chat c = (new Chat());
            c.Init("Part1");
            cman.AddChat(c);
            c.AddCommand(Command.create("Say", "Hello"));
            c.AddCommand(Command.create("Do", "Flip"));
            Chat d = (Chat) Command.create("Chat", "Part2");
            cman.AddChat(d);
            d.AddCommand(Command.create("Do", "Flip2"));
            Console.WriteLine(cman);
        }

    }

    public static class ChatManagerX {

        static Dictionary<string, Chat> chats = new Dictionary<string, Chat>();
        static List<IChatListener> listeners = new List<IChatListener>();
        static Stack<Chat> stack = new Stack<Chat>();
        private static bool paused = false;

        public static void AddListener(IChatListener icl) {
            listeners.Add(icl);
        }

        internal static void Register(Chat c) {
            Console.WriteLine("Register: Chat@" + c.id);
            chats.Add(c.id, c);
        }

        private static void notifyListeners(Command c) {
            listeners.ForEach(icl => icl.onChatEvent(c));
            c.Fire();
        }

        public static void Run(Chat start) {
            Chat current = start;
            while (!paused) {
                notifyListeners(current);
                foreach (var c in current.commands) {
                    if (c is Go) {
                        current = lookup(c.text);
                    } else {
                        notifyListeners(c);
                    }
                }
            }
        }

        private static Chat lookup(string id) {
            return chats[id];
        }
    }

}