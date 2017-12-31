grammar GScript;

//////////////////////// PARSER /////////////////////////

dialog: line+;

line: expr (NEWLINE | EOF);

expr: '[' command '] ' args; 

args: TEXT; 

commandx: (
		'Say'
		| 'Goto'
		| 'Wait'
		| 'Chat'
		| 'Ask'
		| 'Opt'
		| 'Do'
		 
	);
    
command: (
		sayCmd
		| gotoCmd
		| waitCmd
		| chatCmd
		| askCmd
		| optCmd
		| doCmd
	);

sayCmd: 'Say';
gotoCmd: 'Goto';
waitCmd: 'Wait';
chatCmd: 'Chat';
askCmd: 'Ask';
optCmd: 'Opt';
doCmd: 'Do';
// func: funcname LPAREN expression RPAREN

//////////////////////// LEXER /////////////////////////

fragment LOWERCASE: [a-z];
fragment UPPERCASE: [A-Z];
fragment DIGIT: [0-9];
fragment PUNCT: [:;",`'!.?-];

SPACE: (' ' | '\t');
COMMENT: '/*' .*? '*/' -> skip;
NEWLINE: ('\r'? '\n' | '\r')+;
TEXT: (LOWERCASE | UPPERCASE | PUNCT | DIGIT | SPACE)+;
//WORD: (LOWERCASE | UPPERCASE | PUNCT | DIGIT)+;


// LABEL: '[' (LOWERCASE | UPPERCASE | DIGIT | '_')+ ']';
// IDENT: (LOWERCASE | UPPERCASE | '_' | DIGIT);

