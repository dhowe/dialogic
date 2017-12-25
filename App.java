import java.io.PrintWriter;

import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.tree.*;

public class App 
{
    public static void main( String[] args )
    {
        ANTLRInputStream is = new ANTLRInputStream(
            "Say I would like to emphasize this\n"
            "Pause 1000\n");
        GuppyScriptLexer lexer = new GuppyScriptLexer(is);
        CommonTokenStream tokens = new CommonTokenStream(lexer);
        GuppyScriptParser parser = new GuppyScriptParser(tokens);

        GuppyScriptParser.FileContext fileContext = parser.file();                
        GuppyScriptVisitor visitor = new GuppyScriptVisitor(System.out);                
        visitor.visit(fileContext);        
    }
}

