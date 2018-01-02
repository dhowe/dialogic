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
using System.Text;
using System.Threading;

namespace Dialogic {

    /* NEXT: 
        QUESTION: Are chats hierarchical?
        Decide on format for chat-names [CHAT_NAME]  ?
        Handle single-quotes in strings ?
        Verify chat-name uniqueness on parse
    */

    public class ConsoleRunner : IChatListener {

        void IChatListener.onChatEvent(Command c) {
            Chat.Out(c);
        }
    }

    public class ScriptReader : GScriptBaseVisitor<Chat> {

        private Chat graph;

        public static void Main(string[] args) {

            Chat c = new Chat();
            c.AddCommand(Command.create("Say", "Hello"));
            c.AddCommand(Command.create("Do", "Flip"));
            Chat d = (Chat) Command.create("Chat", "Part2");
            c.AddCommand(d);
            c.AddCommand(Command.create("Do", "Flip2"));

            Console.WriteLine(c.AsTree());
        }

        public static void Mainx(string[] args) {

            ScriptReader sman = new ScriptReader();
            sman.Parse("[Say] I would like to emphasize this\n[Wait] 1000\n");
            Console.WriteLine(sman);
            //Chat chat = reader.Load("test-script.gs");
            //reader.AddListener(new ConsoleRunner());
            //Console.WriteLine(chat.AsTree());
            //reader.Run();
        }

        public override string ToString() {
            Chat current = graph;
            string ind = "  ", s = '\n' + base.ToString() + '\n';
            for (int i = 0; i < current.commands.Count; i++) {
                var cmd = current.commands[i];
                if (cmd is Chat) {
                    current = (Chat) cmd;
                    i = 0;
                } else {
                    s += ind + cmd.ToString() + "\n";
                }
            }
            return s + "\n";
        }

        public Chat Parse(string text) {
            GScriptParser parser = CreateParser(new AntlrInputStream(text));
            IParseTree tree = parser.dialog();
            graph = (Chat) Command.create("Chat", Path.GetFileNameWithoutExtension(text));
            return Visit(tree);
        }

        public void AddListener(IChatListener icl) => ChatManager.AddListener(icl);

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
                graph = (Chat) c;
            } else {
                graph.AddCommand(c);
            }
            //Out(cmd.GetText() + " -> " + args.GetText());
            return VisitChildren(context);
        }

        private Command LastOfType(List<Command> commands, Type typeToFind) {
            for (var i = commands.Count - 1; i >= 0; i--) {
                Command c = commands[i];
                if (c.GetType() == typeToFind) {
                    return graph.commands[i];
                }
            }
            return null;
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
        }*/

        private string TypeFromContext(RuleContext context, bool qualified = false) {
            var cname = context.GetType().ToString();
            var tname = cname.Replace("GScriptParser+", "").Replace("Context", "");
            return qualified ? "Chatic." + tname : tname;
        }

        private string[] ParseArgs(RuleContext context, int numArgs = 0) {

            List<ArgsContext> acs = FindChildren(context.Parent.Parent, typeof(ArgsContext));;
            if (numArgs > 0 && acs.Count != numArgs) {
                Err($"'{TypeFromContext(context)}' expects {numArgs} args, but got {acs.Count}: '{ArgsToString(acs)}'");
                return null;
            }
            string[] args = new string[acs.Count];
            for (int i = 0; i < args.Length; i++) {
                args[i] = acs[i].GetText();
            }
            return args;
        }

        private string ArgsToString(List<ArgsContext> contexts) {
            string s = "";
            foreach (var context in contexts) {
                s += context.GetText() + ", ";
            }
            return s.Substring(0, s.Length - 2);
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
                throw new System.Exception("SyntaxError: " + line + " " + msg);
            }
        }
    }

}