grammar Dialogic;

////////////////////////////////////////////////////////////////////////

script: line+;
line: (command SPACE* | command SPACE+ args) SPACE* (NEWLINE | EOF);
command: COMMAND;
args: arg (DELIM arg)*;
arg: WORD (SPACE+ WORD)*;

//arg: (WORD | subst) (SPACE+ (WORD | subst))*;
//subst: OBR ( ~(OBR | CBR) | subst)* CBR;

////////////////////////////////////////////////////////////////////////

COMMAND: ('CHAT' | 'SAY' | 'WAIT'| 'DO' | 'ASK' | 'OPT' | 'GO' | 'META' | 'COND' | 'FIND');

SPACE: (' ' | '\t');
DELIM: SPACE* '#' SPACE*;
NEWLINE: ('\r'? '\n' | '\r')+;
WORD: ([|a-zA-Z0-9%=;:?.,!"'$()-])+;



COMMENT : '/*' (COMMENT|.)*? '*/' -> skip ;
LINE_COMMENT  : '//' .*? '\n' -> skip ;
ERROR: .;


//WORD: [a-zA-Z0-9"'$] ([a-zA-Z0-9=;:?.,!"'$-])*;
//OBR: '(';//CBR: ')';
