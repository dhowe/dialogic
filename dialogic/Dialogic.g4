grammar Dialogic;

////////////////////////////////////////////////////////////////////////

script: line+;
line: (command SP* | command SP+ args) SP* (NEWLINE | EOF);
command: COMMAND;
args: (arg (DELIM arg)*) | (LB meta RB)? | (arg (DELIM arg)*) (LB meta RB)?;
arg: (WORD (SP+ WORD)*);
meta: (SP | WORD | '=')*;
 

////////////////////////////////////////////////////////////////////////


COMMAND: ('CHAT' | 'SAY' | 'WAIT'| 'DO' | 'ASK' | 'OPT' | 'GO' | 'META' | 'COND' | 'FIND' | 'SET');

DELIM: SP* '#' SP*;
LB: SP* '{' SP*;
RB: SP* '}' SP*;
SP: (' ' | '\t');
NEWLINE: ('\r'? '\n' | '\r')+;

WORD: ([|a-zA-Z0-9%;:?.,!"'$()-])+;

COMMENT: '/*' (COMMENT|.)*? '*/' -> skip ;
LINE_COMMENT: '//' .*? (NEWLINE | EOF) -> skip ;
ERROR: .;