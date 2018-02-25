grammar Dialogic;

////////////////////////////////////////////////////////////////////////

script: line+;
line: (command SP* | command SP+ args) SP* (NEWLINE | EOF);
command: ('CHAT' | 'SAY' | 'WAIT'| 'DO' | 'ASK' | 'OPT' | 'GO' | 'FIND' | 'SET' | 'GRAM');
args: (arg (DELIM arg)*) | (LB meta RB) | (arg (DELIM arg)*) SP* (LB meta RB);
arg: (WORD (SP+ WORD)*);
meta: (SP | WORD | OPS)*;
 

////////////////////////////////////////////////////////////////////////


DELIM: SP* '#' SP*;
LB: '{';
RB: '}';
SP: (' ' | '\t');
NEWLINE: ('\r'? '\n' | '\r')+;

OPS:  ([<=>*^$!])+;
WORD: ([|a-zA-Z0-9%;:?.,!"'$()-])+;

COMMENT: '/*' (COMMENT|.)*? '*/' -> skip ;
LINE_COMMENT: '//' .*? (NEWLINE | EOF) -> skip ;
ERROR: .;