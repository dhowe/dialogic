CHAT GScriptTest {plot=a,stage=b}
Welcome to my $emotion world
NVM 1.1
WAIT 3
DO #Twirl
WAIT {ForAnimation=true}
Thanks for visiting $place {speed=fast,style=whisper}
GO #Prompt

CHAT RePrompt {plot=a,stage=b}
DO #SadSpin
ASK (Really|Awww), don't you want to play a game?
OPT sure #Game
OPT $neg #RePrompt

CHAT Prompt {notPlayed=true,plot=a,stage=b}
ASK Do you want to $verb a game? {timeout=4}
OPT Sure #Game
OPT Nope #RePrompt

CHAT Game {plot=a,stage=b}
DO #HappyFlip {axis=y}
Great, let's play! {speed=slow,style=loud}
Bye! {speed=fast}
