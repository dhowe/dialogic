SAY Welcome to my World
WAIT 0.2
DO Twirl
SAY Thanks  for Visiting!
GO Prompt

CHAT Prompt
ASK Do you want to play a game? # 5
OPT Sure # Game
OPT Nah #RePrompt

CHAT RePrompt
DO SadSpin
ASK Really, Don't you want to play a game?
OPT Sure #Game
OPT Nah #RePrompt

CHAT Game
DO HappyFlip
ASK Great, let's play!
SAY Done
