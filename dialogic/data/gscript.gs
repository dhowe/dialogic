Welcome to my $emotion world
NVM 1.1
DO #Twirl
WAIT
Thanks for visiting $place {speed=fast,style=whisper}

GO #Prompt

CHAT RePrompt
DO #SadSpin
ASK (Really|Awww), don't you want to play a game?
OPT sure #Game
OPT $neg #RePrompt

CHAT Prompt {notPlayed=true}
ASK Do you want to $verb a game? {timeout=4}
OPT Sure #Game
OPT Nope #RePrompt

CHAT Game
DO #HappyFlip {axis=y}
Great, let's play! {speed=slow,style=loud}
Bye! {speed=fast}
