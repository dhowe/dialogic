SET lastPrompt Game

Welcome to my $emotion World
WAIT .5
DO Twirl
Thanks for visiting $place!
GO Prompt

CHAT Prompt 
ASK Do you want to $verb a game? # 8
OPT Sure # Game
OPT Nope #RePrompt

CHAT RePrompt
DO SadSpin
ASK (Really|Awww), don't you want to play a game?
OPT Sure # $lastPrompt
OPT $neg # RePrompt

CHAT Game
DO $Happy
ASK Great, let's play!
Bye!
