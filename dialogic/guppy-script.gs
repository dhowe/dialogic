SAY Welcome to my World...
WAIT 0.2
DO Twirl
SAY Thanks for Visiting...
GO Prompt

CHAT Prompt
ASK Do you want to play a game? 
OPT Sure, OK => Game
OPT Nah => RePrompt

CHAT RePrompt
DO SadSpin
ASK Really, Don't you want to play a game?
OPT Sure, OK => Game
OPT Nah => RePrompt

CHAT Game
DO HappyFish
SAY Great, let's play!