CHAT GScriptTest {type=a,stage=b}
Welcome to my $emotion world
NVM 1.1
WAIT 3
DO #Twirl
WAIT {ForAnimation=true}
Thanks for visiting $place {speed=fast,style=whisper}

/*GO #Prompt

CHAT RePrompt {type=a,stage=b}
DO #SadSpin
ASK (Really|Awww), don't you want to play a game?
OPT sure #Game
OPT $neg #RePrompt

CHAT Prompt {notPlayed=true,type=a,stage=b}
ASK Do you want to $verb a game? {timeout=4,speed=fast }
OPT Sure #Game
OPT Nope #RePrompt

CHAT Game {type=a,stage=b,last=true}
DO #HappyFlip {axis=y}
Great, let's play! {speed=slow,style=loud}
Bye! {speed=fast}


CHAT OnTapEvent {noStart=true,resumeAfter=true}
DO #TapResponse
SAY I see you!*/
