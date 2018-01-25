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

A COMMAND must begin a line

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

### Generative Elements

Dialogic is designed to smoothly blend scripted and generated text to create natural feeling dialog, with variation, and to avoid repetition. The simplest way to blend generative elements into a response is via the | OR operator, grouped with parentheses as follow:s

````
SAY You look (sad | gloomy | depressed).
````

Elements between the | OR operator above are randomly selected. Writers may specify probability weightings for various choices, as well as favoring choices that have not been recently selected. Another example, demonstrating nested OR constructions.

````
SAY I'm (very | super | really) glad to ((meet | know) you | make your acquantance)
````

OR constructions can be arbitrarily nested to create further variation.

### Interruption / Smoothing

Dialogic is also designed to respond naturally to user interaction and/or interruption. This is enabled primarily via a stack abstraction in which new CHATS are added at top. When an event or other interruption occurs, the response CHAT is pushed atop the stack and the current CHAT marked as 'interrupted'. When the response CHAT is finished, control moves to the next interrupted chat on the stack. Smoothing sections can be easily added in order to make transitions more natural, i.e., 'so as I was saying'.

### Integrating Dialogic (C#)

&nbsp;

A <ChatParser> reads in one or more CHAT descriptions from plain-text files and compiles them into CHAT objects to be managed by a <ChatRuntime>., as the following example (in C#) demonstrates:

````C#
List<Chat> chats = ChatParser.ParseFile("gscript.gs");//"gscript.gs" 
ChatRuntime cm = new ChatRuntime(chats);
````

### Global / Local Variables

&nbsp;

[pending]

### Advanced Commands

&nbsp;

[pending]


