grammar Test;

//////////////////////// PARSER /////////////////////////
tree: var+;

var: DOLLAR WORD;

fragment LOWER: [a-z];
fragment UPPER: [A-Z];
fragment DIGIT: [0-9];
fragment PUNCT: [;:",`'!._?-];


WORD: (LOWER | UPPER) (LOWER | UPPER | PUNCT | DIGIT)*;
DOLLAR: '$';
