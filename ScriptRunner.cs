using System;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

namespace Dialogic {
    
    // NEXT: implement visitor methods to create a Dialog object or []

    public class ScriptRunner : GScriptBaseVisitor<Dialog> {

        public ScriptRunner() { }

        public override Dialog VisitDialog([NotNull] GScriptParser.DialogContext context) {
            Console.WriteLine("VisitDialog: " + context);
            return VisitChildren(context);
        }

        public override Dialog VisitLine([NotNull] GScriptParser.LineContext context) {
            Console.WriteLine("VisitLine");
            return VisitChildren(context);
        }

        public override Dialog VisitCommand([NotNull] GScriptParser.CommandContext context) {
            Console.WriteLine("VisitCommand");
            return VisitChildren(context);
        }

        public override Dialog VisitText([NotNull] GScriptParser.TextContext context) {
            Console.WriteLine("VisitText");
            return VisitChildren(context);
        }

        public static new void Main(string[] args) {

            string input = "Say I would like to emphasize this\nPause 1000\n";
            ITokenSource lexer = new GScriptLexer(new AntlrInputStream(input));
            ITokenStream tokens = new CommonTokenStream(lexer);
            GScriptParser parser = new GScriptParser(tokens);
            parser.AddErrorListener(new ThrowExceptionErrorListener());

            var result = new ScriptRunner().Visit(parser.dialog());
            Console.WriteLine(result);
        }

        class ThrowExceptionErrorListener : IAntlrErrorListener<IToken> {
            void IAntlrErrorListener<IToken>.SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol,
                int line, int charPositionInLine, string msg, RecognitionException e) {
                throw new System.Exception("SyntaxError: " + line + " " + msg);
            }
        }
    }
}