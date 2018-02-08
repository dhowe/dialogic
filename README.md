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

Dialogic is designed to smoothly blend scripted and generated text to create the type of variation found in natural-sounding dialog. The simplest way to blend generative elements into a response is via the | OR operator, grouped with parentheses as follows:

````
SAY You look (sad | gloomy | depressed).
````

Elements between the | operators are randomly selected, so the line above will generate each of the following 3 outputs with equal probability:

````
You look sad.
````
````
You look gloomy.
````
````
You look depressed.
````

Writers may also specify probability weightings for various choices, as well as favoring choices that have not been recently selected. Another example, demonstrating nested OR constructions:

````
SAY I'm (very | super | really) glad to ((meet | know) you | make your acquantance).
````

[tbd]

&nbsp;

### Interruption / Smoothing

Dialogic is also designed to respond naturally to user interaction and/or interruption. This is enabled primarily via a stack abstraction in which new CHATS are added at top. When an event or other interruption occurs, the response CHAT is pushed atop the stack and the current CHAT marked as 'interrupted'. When the response CHAT is finished, control moves to the next interrupted chat on the stack. Smoothing sections can be added in order to make transitions more natural, i.e., 'so as I was saying'.

[tbd]

&nbsp;

### Integrating Dialogic

Dialogic can be run alone (via the included _ConsoleClient_) or with a game engine, such as Unity3D. The system includes two main components: the domain-specific language (DSL) described above, and a runtime environment, which is responsible for passing events between the runtime and the set of registered clients. _ChatEvents_ are sent from Dialogic to the client (e.g. the Unity Engine) telling it perform a specific action, such as triggering a speech act, initiating an animation, or playing audio from a file. _UIEvents_ are sent from the client (e.g. the Unity Engine) to the runtime, notifying it that some event has occurred, usually a speech act, UI interaction, or gesture from the user. 

In the C# example below, a _ChatParser_ reads in a number of chat descriptions from a plain-text file and compiles them into a list of _Chat_ objects, which are passed to the _ChatRuntime_. An example client (outputting only to the console) is created, which then subscribes with the runtime for chat events. The runtime then subscribes back to events issued by the client. Chats begin to execute as when run is called on the _ChatRuntime_.

````C#
var chats = ChatParser.ParseFile("gscript.gs"); 
ChatRuntime cm = new ChatRuntime(chats);

ConsoleClient cl = new ConsoleClient(); // An example client

cl.Subscribe(cm); // Client subscribes to chat events
cm.Subscribe(cl); // Dialogic subscribes to client events

cm.Run();
````

### Event Scheduling

&nbsp;

[tbd]

&nbsp;

### Global / Local Variables

&nbsp;

[tbd]

&nbsp;

### Advanced Commands

&nbsp;

[tbd]

&nbsp;



## Building Dialogic with Visual Studio (OS X)

1. Clone this respository to your local file system ```` $ git clone https://github.com/dhowe/dialogic.git````

1. From Visual Studio(7.3.3) do Menu->File->Open, then select dialogic.sln from the top-level of the cloned dir

1. The solution should open with 3 sub-projects, as in the image below. Right-click 'runner' and do 'Run Item'

![](res/vsloaded.png?raw=true)


&nbsp;
