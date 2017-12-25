import org.antlr.v4.runtime.tree.ParseTreeListener;

public class DialogicListener extends ParseTreeListener implements GuppyScriptListener {
	/**
	 * Enter a parse tree produced by {@link GuppyScriptParser#dialog}.
	 * @param ctx the parse tree
	 */
	public void enterDialog(GuppyScriptParser.DialogContext ctx) { }
	/**
	 * Exit a parse tree produced by {@link GuppyScriptParser#dialog}.
	 * @param ctx the parse tree
	 */
	public void exitDialog(GuppyScriptParser.DialogContext ctx) {
	/**
	 * Enter a parse tree produced by {@link GuppyScriptParser#line}.
	 * @param ctx the parse tree
	 */
	public void enterLine(GuppyScriptParser.LineContext ctx) { }
	/**
	 * Exit a parse tree produced by {@link GuppyScriptParser#line}.
	 * @param ctx the parse tree
	 */
	public void exitLine(GuppyScriptParser.LineContext ctx) { }
	/**
	 * Enter a parse tree produced by {@link GuppyScriptParser#command}.
	 * @param ctx the parse tree
	 */
	public void enterCommand(GuppyScriptParser.CommandContext ctx) { }
	/**
	 * Exit a parse tree produced by {@link GuppyScriptParser#command}.
	 * @param ctx the parse tree
	 */
	public void exitCommand(GuppyScriptParser.CommandContext ctx) { }
	/**
	 * Enter a parse tree produced by {@link GuppyScriptParser#text}.
	 * @param ctx the parse tree
	 */
	public void enterText(GuppyScriptParser.TextContext ctx) { }
	/**
	 * Exit a parse tree produced by {@link GuppyScriptParser#text}.
	 * @param ctx the parse tree
	 */
	public void exitText(GuppyScriptParser.TextContext ctx) { }
}
