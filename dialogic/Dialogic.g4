grammar Dialogic;

////////////////////////////////////////////////////////////////////////

script: line+;
line: (command SPACE* | command SPACE+ args) (NEWLINE | EOF);
command: COMMAND;
args: arg (DELIM arg)*;
arg: WORD (SPACE+ WORD)*;

////////////////////////////////////////////////////////////////////////

COMMAND: ('CHAT' | 'SAY' | 'WAIT'| 'DO' | 'ASK' | 'OPT' | 'GO' | 'CALL');
SPACE: (' ' | '\t');
NEWLINE: ('\r'? '\n' | '\r')+;
WORD: [a-zA-Z0-9"'$] ([a-zA-Z0-9;:?.,!"'$-])*;
DELIM: SPACE* '#' SPACE*;