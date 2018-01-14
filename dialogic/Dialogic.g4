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

//OBR: '(';//CBR: ')';
COMMAND: ('CHAT' | 'SAY' | 'WAIT'| 'DO' | 'ASK' | 'OPT' | 'GO' | 'SET');
SPACE: (' ' | '\t');
DELIM: SPACE* '#' SPACE*;
NEWLINE: ('\r'? '\n' | '\r')+;
//WORD: [a-zA-Z0-9"'$] ([a-zA-Z0-9=;:?.,!"'$-])*;
WORD: ([|a-zA-Z0-9=;:?.,!"'$()-])+;
ERROR: .;