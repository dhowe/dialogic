grammar GScript;

////////////////////// LEXER ////////////////////////////

dialog: line+ EOF;

line: command SPACE text NEWLINE;

command: (SAY | GOTO | PAUSE | WAIT | LABEL | ASK);

text: (WORD | SPACE )+;

// func: funcname LPAREN expression RPAREN

//////////////////////// PARSER /////////////////////////

SAY: 'Say';
GOTO: 'Goto';
PAUSE: 'Pause';
WAIT: 'Wait';
LABEL: 'Label';
ASK: 'Ask';

NEWLINE: ('\r'? '\n' | '\r')+;

fragment LOWERCASE: [a-z];
fragment UPPERCASE: [A-Z];
fragment DIGIT: [0-9];
fragment PUNCT: [:'",`!-];

WORD: (LOWERCASE | UPPERCASE | PUNCT | DIGIT)+;

SPACE : (' ' | '\t');

