/*
 * GscriptParser.cs
 *
 * THIS FILE HAS BEEN GENERATED AUTOMATICALLY. DO NOT EDIT!
 *
 * Permission is granted to copy this document verbatim in any
 * medium, provided that this copyright notice is left intact.
 *
 * Copyright (c) 2003 Per Cederberg. All rights reserved.
 */

using System.IO;

using PerCederberg.Grammatica.Runtime;

/**
 * <remarks>A token stream parser.</remarks>
 */
internal class GscriptParser : RecursiveDescentParser {

    /**
     * <summary>An enumeration with the generated production node
     * identity constants.</summary>
     */
    private enum SynteticPatterns {
    }

    /**
     * <summary>Creates a new parser with a default analyzer.</summary>
     *
     * <param name='input'>the input stream to read from</param>
     *
     * <exception cref='ParserCreationException'>if the parser
     * couldn't be initialized correctly</exception>
     */
    public GscriptParser(TextReader input)
        : base(input) {

        CreatePatterns();
    }

    /**
     * <summary>Creates a new parser.</summary>
     *
     * <param name='input'>the input stream to read from</param>
     *
     * <param name='analyzer'>the analyzer to parse with</param>
     *
     * <exception cref='ParserCreationException'>if the parser
     * couldn't be initialized correctly</exception>
     */
    public GscriptParser(TextReader input, GscriptAnalyzer analyzer)
        : base(input, analyzer) {

        CreatePatterns();
    }

    /**
     * <summary>Creates a new tokenizer for this parser. Can be overridden
     * by a subclass to provide a custom implementation.</summary>
     *
     * <param name='input'>the input stream to read from</param>
     *
     * <returns>the tokenizer created</returns>
     *
     * <exception cref='ParserCreationException'>if the tokenizer
     * couldn't be initialized correctly</exception>
     */
    protected override Tokenizer NewTokenizer(TextReader input) {
        return new GscriptTokenizer(input);
    }

    /**
     * <summary>Initializes the parser by creating all the production
     * patterns.</summary>
     *
     * <exception cref='ParserCreationException'>if the parser
     * couldn't be initialized correctly</exception>
     */
    private void CreatePatterns() {
        ProductionPattern             pattern;
        ProductionPatternAlternative  alt;

        pattern = new ProductionPattern((int) GscriptConstants.EXPRESSION,
                                        "Expression");
        alt = new ProductionPatternAlternative();
        alt.AddProduction((int) GscriptConstants.TERM, 1, 1);
        alt.AddProduction((int) GscriptConstants.EXPRESSION_TAIL, 0, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) GscriptConstants.EXPRESSION_TAIL,
                                        "ExpressionTail");
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) GscriptConstants.ADD, 1, 1);
        alt.AddProduction((int) GscriptConstants.EXPRESSION, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) GscriptConstants.SUB, 1, 1);
        alt.AddProduction((int) GscriptConstants.EXPRESSION, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) GscriptConstants.TERM,
                                        "Term");
        alt = new ProductionPatternAlternative();
        alt.AddProduction((int) GscriptConstants.FACTOR, 1, 1);
        alt.AddProduction((int) GscriptConstants.TERM_TAIL, 0, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) GscriptConstants.TERM_TAIL,
                                        "TermTail");
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) GscriptConstants.MUL, 1, 1);
        alt.AddProduction((int) GscriptConstants.TERM, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) GscriptConstants.DIV, 1, 1);
        alt.AddProduction((int) GscriptConstants.TERM, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) GscriptConstants.FACTOR,
                                        "Factor");
        alt = new ProductionPatternAlternative();
        alt.AddProduction((int) GscriptConstants.ATOM, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) GscriptConstants.LEFT_PAREN, 1, 1);
        alt.AddProduction((int) GscriptConstants.EXPRESSION, 1, 1);
        alt.AddToken((int) GscriptConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) GscriptConstants.ATOM,
                                        "Atom");
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) GscriptConstants.NUMBER, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) GscriptConstants.IDENTIFIER, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);
    }
}
