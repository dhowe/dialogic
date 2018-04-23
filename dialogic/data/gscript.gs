CHAT GScriptTest {type=a,stage=b}
Welcome to my $emotion world
NVM 1.1
WAIT 3
DO #Twirl
//Tendar:DO #TendarSpin
//Tendar:Thanks

WAIT {ForAnimation=true}
Thanks for visiting $place {speed=fast,style=whisper}
GO #Prompt

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
SAY Ok, I see you! 
SAY Wait, is that (cat | dog | artichoke).articlize()?

CHAT MyWorld {noStart=true,chatMode=grammar}
start = My world is a <adj>, <adj> place
adj = creepy | lonely | dark | forgotten | crepuscular
SAY $start