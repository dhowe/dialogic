grammar GScript;

////////////////////// PARSER ////////////////////////////

dialog: expr + EOF;

expr: SPC* command SPC* args  (NEWLINE+ | EOF) ;

command: (say | gotu | pause | label | ask);

args: (QSTRING | INT);

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
fragment PUNCT: [:;",`!.?-];

INT: DIGIT+;

COMMENT: '/*' .*? '*/' NEWLINE? -> skip;

QSTRING: '\'' (WORD | SPC | NEWLINE)+ '\''; 

NEWLINE: ('\r'? '\n' | '\r')+;

WORD: (LOWERCASE | UPPERCASE | PUNCT | DIGIT)+;

SPC : (' ' | '\t');

