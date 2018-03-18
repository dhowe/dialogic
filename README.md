# Dialogic

[![Build Status](https://travis-ci.org/dhowe/dialogic.svg?branch=master)](https://travis-ci.org/dhowe/dialogic) <a href="http://www.gnu.org/licenses/gpl-3.0.en.html"><img src="https://img.shields.io/badge/license-GPL-orange.svg" alt="gpl license"></a>

[Command Reference](https://github.com/dhowe/dialogic/wiki/Command-Reference) :: [API Documentation](http://rednoise.org/dialogic/) :: [Web Linter](138.16.162.16:8080/glint/) 

Dialogic is a system designed to help writers easily create interactive scripts with generative elements. It makes no assumptions about how generated text is displayed, or about how users will choose their responses. These tasks are left to game designers and programmers (using tools like Unity3D).

Each section of text in a Dialogic script is known as a [CHAT](https://github.com/dhowe/dialogic/wiki/Command-Reference#chat). Each CHAT has a unique LABEL and contains one or more COMMANDs. When a CHAT is run, each COMMAND is executed in order, until all have been run, or the system jumps to a new CHAT. 

The simplest command is [SAY](https://github.com/dhowe/dialogic/wiki/Commands#say) which simply echoes the given output:

````
SAY Welcome to your first Dialogic script!
````

Because the system is designed for handling text, no quotations are needed around strings. In fact, the SAY keyword itself is even optional. So the simplest Dialogic script is just a single line of text:

````
Welcome to your first Dialogic script!
````

### Basic Commands

A COMMAND must begin a line

COMMANDS include [SAY](https://github.com/dhowe/dialogic/wiki/Command-Reference#say), [DO](https://github.com/dhowe/dialogic/wiki/Command-Reference#do), [ASK](https://github.com/dhowe/dialogic/wiki/Command-Reference#ask), [OPT](https://github.com/dhowe/dialogic/wiki/Command-Reference#opt), [FIND](https://github.com/dhowe/dialogic/wiki/Command-Reference#find), [GO](https://github.com/dhowe/dialogic/wiki/Command-Reference#go), and [others](https://github.com/dhowe/dialogic/wiki/Command-Reference)

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
OPT Sure #Game1
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
 
You look gloomy.
 
You look depressed.
````

Writers may also specify probability weightings for various choices, as well as favoring choices that have not been recently selected. Another example, demonstrating nested OR constructions:

````
SAY I'm (very | super | really) glad to ((meet | know) you | make your acquaintance).
````

[tbd]

&nbsp;

### Interruption / Smoothing

Dialogic is also designed to respond naturally to user interaction and/or interruption. This is enabled primarily via a stack abstraction in which new CHATS are added at top. When an event or other interruption occurs, the response CHAT is pushed atop the stack and the current CHAT marked as 'interrupted'. When the response CHAT is finished, control moves to the next interrupted chat on the stack. Smoothing sections can be added in order to make transitions more natural, i.e., 'so as I was saying'.

[tbd]

&nbsp;

### Integrating Dialogic

Dialogic can be run alone or with a game engine, such as Unity3D (see example below). The system includes two main components: the domain-specific language (DSL) described above, and a runtime environment, which is responsible for passing events between the runtime and the application. 

In the C# example below, a _ChatParser_ reads in a number of chat descriptions from a plain-text file and compiles them into a list of _Chat_ objects, which are passed to the _ChatRuntime_. 

The application calls the runtime's Update() function each frame, passing the current world-state (a dictionary of key-value pairs) and any event that occurred during that frame. If an Dialogic event occurs during the frame it is returned from the Update function:
````C#

 public RealtimeGame() 
 {
     var chats = ChatParser.ParseFile(scriptDir); 
     dialogic = new ChatRuntime(chats);
 }

 public IUpdateEvent Update() // Game Loop
 {
     // Call the dialogic interface
     IUpdateEvent evt = dialogic.Update(globals, ref gameEvent);

     // Handle the event received
     if (evt != null) HandleEvent(evt);
     
     ...
 }
````

## Building Dialogic with Visual Studio (OS X)

1. Clone this respository to your local file system ```` $ git clone https://github.com/dhowe/dialogic.git````

1. From Visual Studio(7.3.3) do Menu->File->Open, then select dialogic.sln from the top-level of the cloned dir

1. The solution should open with 3-4 sub-projects, as in the image below. 

1. To run tests, right-click 'tests' and select 'Run Item'

![](http://rednoise.org/images/vsloaded.png?raw=true)



&nbsp;
