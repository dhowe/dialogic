using System;
using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;
using ArgContext = GScriptParser.ArgContext;
using System.Collections;
using System.Text;

namespace Dialogic {

    /* NEXT: 
        
        Decide on format for labels [LABEL_NAME]  ?
        Handle single-quotes in strings ?
        Verify label uniqueness on parse
    */

    public class Visitor : GScriptBaseVisitor<Dialog> {

        public static void Main(string[] args) {

            //string input = "Say I would like to emphasize this\nPause 1000\n";
            string input = File.ReadAllText("test-script.gs", Encoding.UTF8);
            ITokenSource lexer = new GScriptLexer(new AntlrInputStream(input));
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            GScriptParser parser = new GScriptParser(tokens);
            parser.AddErrorListener(new ThrowExceptionErrorListener());
            Dialog result = new Visitor().Visit(parser.dialog());
            Out(result);
            result.Run();
        }

        public Dialog dialog = new Dialog();

        public static void Out(object s) {
            Console.WriteLine(s);
        }
        public static void Err(object s) {
            Console.WriteLine("\n[ERROR] " + s + "\n");
        }

        public override Dialog Visit(IParseTree tree) {
            //Console.WriteLine("Visit ->");
            base.Visit(tree);
            return dialog;
        }

        public override Dialog VisitGotu([NotNull] GScriptParser.GotuContext context) {
            //Console.WriteLine("VisitGotu: ");
            return VisitChildren(context);
        }

        /*public override Dialog VisitCommand([NotNull] GScriptParser.CommandContext context) {

            Console.WriteLine("VisitCommand");

            var parent = context.Parent;
            var type = typemap[context.GetChild(0).GetType()];
            var body = findChildren(parent, typeof(GScriptParser.CommandContext)).GetText();
            Command c = (Command) Activator.CreateInstance(type, dialog);
            dialog.AddEvent(c); // TODO: need to make the other Command(Dialog) constructors
            Console.WriteLine("  " + type + ": " + body);

            return VisitChildren(context);
        }
        public Dialog VisitAny([NotNull] ParserRuleContext context) {
            var parent = context.Parent;
            var type = typemap[context.GetChild(0).GetType()];
            var body = findChildren(parent, typeof(GScriptParser.CommandContext)) [0].GetText();
            Command c = (Command) Activator.CreateInstance(type, dialog);
            dialog.AddEvent(c); // TODO: need to make the other Command(Dialog) constructors
            Console.WriteLine("  " + type + ": " + body);
            return dialog;
        }*/

        public override Dialog VisitSay([NotNull] GScriptParser.SayContext context) {

            string[] args = ParseArgs(context, 1);
            dialog.AddEvent(new Say(dialog, args[0]));
            return VisitChildren(context);
        }

        public override Dialog VisitPause([NotNull] GScriptParser.PauseContext context) {

            string[] args = ParseArgs(context, 1);
            dialog.AddEvent(new Pause(dialog, int.Parse(args[0])));
            return VisitChildren(context);
        }

        public override Dialog VisitLabel([NotNull] GScriptParser.LabelContext context) {

            string[] args = ParseArgs(context, 1);
            // TODO: check that label is unique
            dialog.AddEvent(new Label(dialog, args[0]));
            return VisitChildren(context);
        }

        public override Dialog VisitAsk([NotNull] GScriptParser.AskContext context) {
            string[] args = ParseArgs(context);
            dialog.AddEvent(new Ask(dialog, args[0]));
            return VisitChildren(context);
        }

        private Command LastOfType(List<Command> commands, Type typeToFind) {
            for (var i = commands.Count - 1; i >= 0; i--) {
                Command c = commands[i];
                if (c.GetType() == typeToFind) {
                    return dialog.events[i];
                }
            }
            return null;
        }

        public override Dialog VisitOpt([NotNull] GScriptParser.OptContext context) {

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

        private string[] ParseArgs(RuleContext context, int numArgs = 0) {

            List<ArgContext> acs = FindChildren(context.Parent.Parent, typeof(ArgContext));
            if (numArgs > 0 && acs.Count != numArgs) {
                var obj = context.GetType().ToString();
                var cmd = obj.Replace("GScriptParser+", "").Replace("Context", "");
                Err($"'{cmd}' expects {numArgs} args, but got {acs.Count}");
                return null;
            }
            string[] args = new string[acs.Count];
            for (int i = 0; i < args.Length; i++) {
                args[i] = acs[i].GetText();
            }
            return args;
        }

        private List<ArgContext> FindChildren(RuleContext parent, Type typeToFind) {
            if (parent == null) {
                throw new Exception("Unexpected parent value: null");
            }
            List<ArgContext> result = new List<ArgContext>();
            for (int i = 0; i < parent.ChildCount; i++) {
                var child = parent.GetChild(i);
                if (child.GetType() == typeToFind) {
                    // if (result != null) {
                    //     throw new Exception("Multiple bodies found: " + parent.GetType());
                    // }
                    result.Add((ArgContext) child);
                }
                //Console.WriteLine("Check: " + child.GetText());

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
        Dictionary<Type, Type> typemap = new Dictionary<Type, Type>() { { typeof(GScriptParser.SayContext), typeof(Dialogic.Say) }, { typeof(GScriptParser.GotuContext), typeof(Dialogic.Gotu) }, { typeof(GScriptParser.PauseContext), typeof(Dialogic.Pause) }, { typeof(GScriptParser.LabelContext), typeof(Dialogic.Label) }, { typeof(GScriptParser.AskContext), typeof(Dialogic.Ask) },
        };
    }
}