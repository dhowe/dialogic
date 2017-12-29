grammar GScript;

////////////////////// PARSER ////////////////////////////

dialog: line+ EOF;

line: SPC* command SPC* body;

command: (say | gotu | pause | label | ask);

body: (QSTRING | INT)+;

say: 'Say';
gotu: 'Goto';
pause: 'Pause';
label: 'Label';
ask: 'Ask';

// func: funcname LPAREN expression RPAREN

//////////////////////// LEXER /////////////////////////


fragment LOWERCASE: [a-z];
fragment UPPERCASE: [A-Z];
fragment DIGIT: [0-9];
fragment PUNCT: [:;'",`!.?-];

INT: DIGIT+;

QSTRING: '\'' (WORD | SPC | NEWLINE)+ '\''; 

NEWLINE: ('\r'? '\n' | '\r')+;

WORD: (LOWERCASE | UPPERCASE | PUNCT | DIGIT)+;

SPC : (' ' | '\t');

