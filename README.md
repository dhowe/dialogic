# dialogic
a scripting language for generative dialogs


````
Say 'Welcome to my world...' 
Pause 500 

Label Prompt1 
Ask 'Do you want to play a game?'
    Branch 'yes' Prompt2 
    Branch 'no' Prompt1  

Label Prompt2 
Ask 'Ok, do you want to go first?',
    Branch 'yes' Game
    Branch 'no' Prompt2  

Label Game 
Say 'Ok, let's play!' 
Pause 500 

