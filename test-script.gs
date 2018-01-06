SAY Welcome to my World...
WAIT 0.2
DO Twirl
ASK Do you want to play a game?
OPT Sure => Game
OPT Nah => RePrompt


/*
SAY Welcome to my World...
DO Twirl
WAIT 2

CHAT Prompt
ASK Do you want to play a game?
OPT Sure, OK => Game
OPT Nah => RePrompt

CHAT RePrompt
DO SadSpin
ASK Really, Don't you want to play a game?
OPT Sure, OK => Game
OPT Nah => RePrompt*/