Welcome to my $emotion world
WAIT .5
NVM 3.1
DO #Twirl
Thanks for visiting $place {speed=fast,style=whisper}

GO #Prompt

CHAT RePrompt
DO #SadSpin
ASK (Really|Awww), don't you want to play a game?
OPT sure #Game
OPT $neg #RePrompt

CHAT Prompt {notPlayed=true}
ASK Do you want to $verb a game? {timeout=2}
OPT Sure #Game
OPT Nope #RePrompt

CHAT Game
DO #HappyFlip {axis=y}
ASK Great, let's play! {speed=slow,style=loud}
Bye! {speed=fast}
