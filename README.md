## Dialogic

Dialogic is a system designed to help writers easily create interactive scripts with generative elements. It makes no assumptions about how generated text is displayed, or about how the users will choose their responses. These tasks are left to game designers and programmers, using tools like Unity3D and C#.

Each section of text in a Dialogic script is known as a CHAT. Each CHAT has a unique name and contains one or more COMMANDS. When a CHAT is run, each COMMAND is executed in order, until all have been run, or the script jumps to a new CHAT. 

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

### Variables


### Advanced Commands


### Integrating Dialogic



