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
        Decide on format for chat-names [CHAT_NAME]  ?
        Handle single-quotes in strings ?
        Verify chat-name uniqueness on parse
    */

    public class ConsoleRunner : IChatListener {
        static int count = 0;
        void IChatListener.onChatEvent(Command c) {
            Console.WriteLine("ChatEvent#"+(++count)+" "+c);

            //Atom.Out(c);
            /*             if (c is Wait) {
                            Thread.Sleep(((Wait) c).millis);
                        } else {

                        } */
        }
    }

    public class ScriptReader : GScriptBaseVisitor<Chat> {

        private Chat data;

        public static void Main(string[] args) {

            ScriptReader reader = new ScriptReader();
            reader.Load("test-script.gs");
            reader.AddListener(new ConsoleRunner());
            //Console.WriteLine(reader.data);
            reader.Run();
        }

        public void AddListener(IChatListener icl) => this.data.AddListener(icl);

        public void Run() => data.Fire();

        public void Load(string file) {

            //string input = "[Say] I would like to emphasize this\n[Wait] 1000\n";
            data = (Chat) Command.create("Chat", Path.GetFileNameWithoutExtension(file));

            string input = File.ReadAllText(file, Encoding.UTF8);
            ITokenSource lexer = new GScriptLexer(new AntlrInputStream(input));
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            GScriptParser parser = new GScriptParser(tokens);
            parser.AddErrorListener(new ThrowExceptionErrorListener());
            this.Visit(parser.dialog());
        }

        public static void Out(object s) => Console.WriteLine(s);

        public static void Err(object s) => Console.WriteLine("\n[ERROR] " + s + "\n");

        public override Chat Visit(IParseTree tree) {

            base.Visit(tree);
            return data;
        }

        public override Chat VisitExpr([NotNull] GScriptParser.ExprContext context) {

            var cmd = context.GetChild<GScriptParser.CommandContext>(0);
            var args = context.GetChild<GScriptParser.ArgsContext>(0);
            data.AddCommand(Command.create(cmd.GetText(), args.GetText()));
            //Out(bcmd.GetText() + " -> " + args.GetText());
            return VisitChildren(context);
        }

        /*public override Chat VisitCommand([NotNull] GScriptParser.CommandContext context) {

            Console.WriteLine("VisitCommand");

            var parent = context.Parent;
            var type = typemap[context.GetChild(0).GetType()];
            var body = findChildren(parent, typeof(GScriptParser.CommandContext)).GetText();
            Command c = (Command) Activator.CreateInstance(type, dialog);
            dialog.AddEvent(c); // TODO: need to make the other Command(Chat) constructors
            Console.WriteLine("  " + type + ": " + body);

            return VisitChildren(context);
        }
        public Chat VisitAny([NotNull] ParserRuleContext context) {
            var parent = context.Parent;
            var type = typemap[context.GetChild(0).GetType()];
            var body = findChildren(parent, typeof(GScriptParser.CommandContext)) [0].GetText();
            Command c = (Command) Activator.CreateInstance(type, dialog);
            dialog.AddEvent(c); // TODO: need to make the other Command(Chat) constructors
            Console.WriteLine("  " + type + ": " + body);
            return dialog;
        }*/

        /*public Chat VisitWay(RuleContext context) {

            string[] args = ParseArgs(context, 1);
            dialog.AddEvent(new Wait(dialog, int.Parse(args[0])));
            return VisitChildren(context);
        }*/

        private Command LastOfType(List<Command> commands, Type typeToFind) {
            for (var i = commands.Count - 1; i >= 0; i--) {
                Command c = commands[i];
                if (c.GetType() == typeToFind) {
                    return data.commands[i];
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
                Err($"'{TypeFromContext(context)}' expects {numArgs} args, but got {acs.Count}: '{argsToString(acs)}'");
                return null;
            }
            string[] args = new string[acs.Count];
            for (int i = 0; i < args.Length; i++) {
                args[i] = acs[i].GetText();
            }
            return args;
        }

        private string argsToString(List<ArgsContext> contexts) {
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
        //Dictionary<Type, Type> typemap = new Dictionary<Type, Type>() { { typeof(GScriptParser.SayContext), typeof(Chatic.Say) }, { typeof(GScriptParser.GotuContext), typeof(Chatic.Gotu) }, { typeof(GScriptParser.DuContext), typeof(Chatic.Do) }, { typeof(GScriptParser.WaytContext), typeof(Chatic.Wait) }, { typeof(GScriptParser.ChatContext), typeof(Chatic.Chat) }, { typeof(GScriptParser.AskContext), typeof(Chatic.Ask) },};
    }

}