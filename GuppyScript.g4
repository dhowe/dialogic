grammar GuppyScript;

//////////////////////////////////////////////////

fragment LOWERCASE  : [a-z] ;
fragment UPPERCASE  : [A-Z] ;
fragment DIGIT      : [0-9] ;
fragment PUNCT      : [:'",`!-] ;
 
WORD                : (LOWERCASE | UPPERCASE | PUNCT | DIGIT)+ ;
 
WHITESPACE          : (' ' | '\t') ;

NEWLINE             : ('\r'? '\n' | '\r')+ ;

//////////////////////////////////////////////////

dialog              : line+ EOF ;

line                : command WHITESPACE text NEWLINE;

command             : ('Say' | 'Goto' | 'Pause' | 'Wait' | 'Label' | 'Ask' | 'React') ;

text              : (WORD | WHITESPACE)+ ;

