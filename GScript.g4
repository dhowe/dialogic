grammar GScript;

////////////////////// PARSER ////////////////////////////

dialog: line+ EOF;

line: SPC* command SPC* body NEWLINE;

command: (say | gotu | pause | label | ask);

body: (WORD | SPC)+;

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

NEWLINE: ('\r'? '\n' | '\r')+;

WORD: (LOWERCASE | UPPERCASE | PUNCT | DIGIT)+;

SPC : (' ' | '\t');

