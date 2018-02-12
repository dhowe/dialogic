using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Dialogic;
using Dialogic.Antlr;

namespace Dialogic
{
    public class LexerTest : DialogicBaseVisitor<Chat>
    {
        private static void printPrettyLispTree(String tree)
        {
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
        }

        public static void MainOff(String[] args)
        {
            new LexerTest().Test();
        }

        public void Test()
        {
            string source = "split.gs";
            ITokenSource lexer = new DialogicLexer(new AntlrInputStream(source));
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            DialogicParser parser = new DialogicParser(tokens);
            ParserRuleContext context = parser.script();
            Visit(context);
            String tree = context.ToStringTree(parser);
            printPrettyLispTree(tree);
        }
    }
}
