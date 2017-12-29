#pragma warning disable 3021

using System;
using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;
using BodyContext = GScriptParser.BodyContext;
using System.Text;

namespace Dialogic {

    // NEXT: add branching Ask/Prompt function

    public class Visitor : GScriptBaseVisitor<Dialog> {

        public static void Main(string[] args) {

            //string input = "Say I would like to emphasize this\nPause 1000\n";
            string input = File.ReadAllText("test-script.gs", Encoding.UTF8);
            ITokenSource lexer = new GScriptLexer(new AntlrInputStream(input));
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            GScriptParser parser = new GScriptParser(tokens);
            parser.AddErrorListener(new ThrowExceptionErrorListener());
            Dialog result = new Visitor().Visit(parser.dialog());
            Console.WriteLine(result);
            result.Run();
        }

        public Dialog dialog = new Dialog();

        public override Dialog Visit(IParseTree tree) {
            //Console.WriteLine("Visit ->");
            base.Visit(tree);
            return dialog;
        }

        public override Dialog VisitGotu([NotNull] GScriptParser.GotuContext context) {
            //Console.WriteLine("VisitGotu: ");
            return VisitChildren(context);
        }

        private RuleContext findChildOfType(RuleContext parent, Type t) {
            if (parent == null) {
                throw new Exception("Unexpected parent value: null");
            }
            RuleContext result = null;
            for (int i = 0; i < parent.ChildCount; i++) {
                var child = parent.GetChild(i);
                if (child.GetType() == t) {
                    if (result != null) {
                        throw new Exception("Multiple bodies found: " + parent.GetType());
                    }
                    result = (RuleContext) child;
                }
                //Console.WriteLine("Check: " + child.GetText());

            }
            if (result == null) {
                throw new Exception("No body found for: " + parent.GetText());
            }
            return result;
        }

        // public override Dialog VisitCommand([NotNull] GScriptParser.CommandContext context) {

        //     Console.WriteLine("VisitCommand");
        //     var body = findBodyInChildren((GScriptParser.LineContext) context.Parent).GetText();
        //     var type = context.GetText();
        //     Console.WriteLine("COMMAND: "+type+" "+body);

        //     // var body = findBodyInChildren((GScriptParser.LineContext) context.Parent.Parent).GetText();
        //     // dialog.AddEvent(new Say(dialog, body));
        //     return VisitChildren(context);
        // }

        Dictionary<Type, Type> typemap = new Dictionary<Type, Type>() { { typeof(GScriptParser.SayContext), typeof(Dialogic.Say) }, { typeof(GScriptParser.GotuContext), typeof(Dialogic.Gotu) }, { typeof(GScriptParser.PauseContext), typeof(Dialogic.Pause) }, { typeof(GScriptParser.LabelContext), typeof(Dialogic.Label) }, { typeof(GScriptParser.AskContext), typeof(Dialogic.Ask) },
        };

        public override Dialog VisitCommand([NotNull] GScriptParser.CommandContext context) {

            /*Console.WriteLine("VisitCommand");
            var parent = context.Parent;
            var type = typemap[context.GetChild(0).GetType()];
            var body = findChildOfType(parent, typeof(GScriptParser.CommandContext)).GetText();
            Command c = (Command) Activator.CreateInstance(type, dialog);
            dialog.AddEvent(c); // TODO: need to make the other Command(Dialog) constructors
            Console.WriteLine("  " + type + ": " + body);*/

            return VisitChildren(context);
        }

        public override Dialog VisitSay([NotNull] GScriptParser.SayContext context) {

            //Console.WriteLine("VisitSay");
            var parent = context.Parent.Parent;
            var body = findChildOfType(parent, typeof(BodyContext)).GetText();
            dialog.AddEvent(new Say(dialog, body));
            return VisitChildren(context);
        }

        public override Dialog VisitPause([NotNull] GScriptParser.PauseContext context) {

            //Console.WriteLine("VisitSay");
            var parent = context.Parent.Parent;
            var body = findChildOfType(parent, typeof(BodyContext)).GetText();
            dialog.AddEvent(new Pause(dialog, int.Parse(body)));
            return VisitChildren(context);
        }

        public override Dialog VisitAsk([NotNull] GScriptParser.AskContext context) {

            //Console.WriteLine("VisitAsk");
            var parent = context.Parent.Parent;
            var body = findChildOfType(parent, typeof(BodyContext)).GetText();
            dialog.AddEvent(new Ask(dialog, body, new string[]{}));
            return VisitChildren(context);
        }

        class ThrowExceptionErrorListener : IAntlrErrorListener<IToken> {
            void IAntlrErrorListener<IToken>.SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol,
                int line, int charPositionInLine, string msg, RecognitionException e) {
                throw new System.Exception("SyntaxError: " + line + " " + msg);
            }
        }
    }
}