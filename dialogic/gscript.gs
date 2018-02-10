SET lastPrompt Game

Welcome to my $emotion World
WAIT .5
DO Twirl
Thanks for visiting $place!
GO Prompt

CHAT Prompt 
ASK Do you want to $verb a game? #10
OPT Sure # Game
OPT $neg #RePrompt

CHAT RePrompt
DO SadSpin
ASK (Really|Whaaaa), don't you want to play a game?
OPT Sure # $lastPrompt
OPT Nah # RePrompt

CHAT Game
DO $Happy
ASK Great, let's play!
Done
