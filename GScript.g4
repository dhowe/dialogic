grammar GScript;

////////////////////// LEXER ////////////////////////////

dialog: line+ EOF;

line: command SPACE body NEWLINE;

command: (say | gotu | pause | label | ask);

body: (WORD | SPACE )+;

say: 'Say';
gotu: 'Goto';
pause: 'Pause';
label: 'Label';
ask: 'Ask';

// func: funcname LPAREN expression RPAREN

//////////////////////// PARSER /////////////////////////


fragment LOWERCASE: [a-z];
fragment UPPERCASE: [A-Z];
fragment DIGIT: [0-9];
fragment PUNCT: [:'",`!-];

NEWLINE: ('\r'? '\n' | '\r')+;

WORD: (LOWERCASE | UPPERCASE | PUNCT | DIGIT)+;

SPACE : (' ' | '\t');

