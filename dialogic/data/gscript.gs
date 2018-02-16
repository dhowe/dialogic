SET lastPrompt Game

Welcome to my $emotion World
WAIT .5
DO Twirl
Thanks for visiting $place! {pace=fast,display=strong}

GO Prompt

CHAT Prompt {NotPlayed=true}
ASK Do you want to $verb a game? # 5
OPT Sure # Game
OPT Nope #RePrompt

CHAT RePrompt
DO SadSpin
ASK (Really|Awww), don't you want to play a game?
OPT Sure # $lastPrompt
OPT $neg # RePrompt

CHAT Game
DO $Happy
ASK Great, let's play! {pace=slow,display=light}
Bye!
