grammar GScript;

////////////////////// PARSER ////////////////////////////

dialog: expr+;

expr: command arg*;

arg: (QSTRING | INT | WORD);

command: (say | gotu | pause | label | ask | opt);

say: 'Say' ;
gotu: 'Goto';
pause: 'Pause';
label: 'Label';
ask: 'Ask';
opt: 'Opt';

// func: funcname LPAREN expression RPAREN

//////////////////////// LEXER /////////////////////////


fragment LOWERCASE: [a-z];
fragment UPPERCASE: [A-Z];
fragment DIGIT: [0-9];
fragment PUNCT: [:;",`!.?-];

INT: DIGIT+;

COMMENT: '/*' .*? '*/' NEWLINE? -> skip;

QSTRING: '\'' (WORD | SPC | NEWLINE)+ '\''; 

NEWLINE: ('\r'? '\n' | '\r')+ -> skip;

WORD: (LOWERCASE | UPPERCASE | PUNCT | DIGIT)+;

SPC : (' ' | '\t') -> skip;

