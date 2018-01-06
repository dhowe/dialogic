grammar GScript;
//////////////////////// PARSER /////////////////////////

dialog: line+;
command: (SAY | WAIT | DO | ASK | OPT | GO | CALL);
args: str | num;
str:  WORD (SPACE WORD)*;
num: DECIMAL;
arr: (str | num) RARROW str;
line: (command SPACE* | command SPACE+ args) (NEWLINE | EOF);
//funCall: funName '(' funCallArgs ')';

//////////////////////// LEXER /////////////////////////

SAY: 'SAY';
WAIT: 'WAIT';
DO: 'DO';
ASK: 'ASK';
OPT: 'OPT';
GO: 'GO';
CALL: 'CALL';

RARROW: '=>';

IF: 'if';
THEN: 'then';

AND: 'and';
OR: 'or';

TRUE: 'true';
FALSE: 'false';

MULT: '*';
DIV: '/';
PLUS: '+';
MINUS: '-';

GT: '>';
GE: '>=';
LT: '<';
LE: '<=';
EQ: '=';

LPAREN: '(';
RPAREN: ')';

SPACE: (' ' | '\t');

DECIMAL: '-'? [0-9]+ ('.' [0-9]+)?;

//IDENTIFIER: [a-zA-Z_][a-zA-Z_0-9]*;

COMMENT: '/*' .*? '*/' -> skip;

NEWLINE: ('\r'? '\n' | '\r')+;

fragment LOWER: [a-z];
fragment UPPER: [A-Z];
fragment DIGIT: [0-9];
fragment PUNCT: [;:",`'!.?-];

//IDENT: (LOWER | UPPER) (LOWER | UPPER | DIGIT)+;
WORD: (LOWER | UPPER) (LOWER | UPPER | PUNCT | DIGIT)*;
//TEXT: (LOWER | UPPER | PUNCT | DIGIT | SPACE)+;
/*
LB: '[';
RB: ']';
UNDER: '_';
RARROW: '=>';
COLON: ':';
SPACE: (' ' | '\t');
LETTER: LOWER | UPPER;
NEWLINE: ('\r'? '\n' | '\r')+;
NUMBER: DIGIT+ '.'* DIGIT*;
COMMAND: UPPER+ (UPPER | UNDER | DIGIT)+;
LABEL: (LOWER | UPPER) (LOWER | UPPER | DIGIT | UNDER)*;
TEXT: (LOWER | UPPER) (LOWER | UPPER | PUNCT | DIGIT | SPACE)+;
TEXT: (LOWER | UPPER | PUNCT | DIGIT | SPACE)+; */

//WORD: (LOWERCASE | UPPERCASE | PUNCT | DIGIT)+;
// LABEL: '[' (LOWERCASE | UPPERCASE | DIGIT | '_')+ ']';
// IDENT: (LOWERCASE | UPPERCASE | '_' | DIGIT);

