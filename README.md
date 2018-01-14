# Dialogic

Dialogic is a system designed to help writers easily create interactive scripts with generative elements. It makes no assumptions about how generated text is displayed, or about how users will choose their responses. These tasks are left to game designers and programmers (using tools like Unity3D).

Each section of text in a Dialogic script is known as a CHAT. Each CHAT has a unique name and contains one or more COMMANDS. When a CHAT is run, each COMMAND is executed in order, until all have been run, or the system jumps to a new CHAT. 

The simplest command is SAY which simply echoes the given output:

````
SAY Welcome to your first Dialogic script!
````

Because the system is designed for handling text, no quotations are needed around strings. In fact, the SAY keyword itself is even optional. So the simplest Dialogic script is just a single line of text:

````
Welcome to your first Dialogic script!
````

### Basic Commands

A COMMAND must begin a line and must be ALL CAPS.

A COMMAND can be followed by zero or more ARGUMENTS

COMMANDS include SAY, DO, ASK, OPT, META, WAIT, GO, and others

Here is a short example:

````
CHAT Start
SAY Welcome to my world
WAIT 1.2
SAY Thanks for Visiting
ASK Do you want to play a game?
OPT Sure
OPT No Thanks
````

This script is called "Start" and performs a few simple functions; welcoming a visitor, pausing, then prompting for a response. 

Of course in most cases we would want to _do something_ with this response. 

In the code below we _branch_, based on the users response:

````
ASK Do you want to play a game?
OPT Sure # Game1
OPT No Thanks
````

If the user selects the first option, Dialogic jumps to the CHAT named "Game1". If not, the CHAT continues.

&nbsp;

### Variables

&nbsp;

### Advanced Commands

&nbsp;

### Integrating Dialogic

&nbsp;

