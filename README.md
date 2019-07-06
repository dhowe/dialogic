
# Dialogic :fish:


[![Build Status](https://travis-ci.org/dhowe/dialogic.svg?branch=master)](https://travis-ci.org/dhowe/dialogic) ![license](https://img.shields.io/badge/license-GPL-orange.svg) [![Gitter](https://badges.gitter.im/dhowe/dialogic.svg)](https://gitter.im/dhowe/dialogic?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=body_badge)

Check out [Tendar](https://play.google.com/store/apps/details?id=com.TenderClaws.Tendar.home), the award-winning free AR game using Dialogic on your Android device! Here's a [review](https://variety.com/2018/digital/news/tender-claws-tendar-ar-game-1203029351/)...

Interested in using Dialogic in your game or voice-app? Drop me a line at daniel-@-rednoise-dot-org..._

<hr/>

[FAQ](https://github.com/dhowe/dialogic/wiki) &nbsp;::&nbsp; [Command Reference](https://github.com/dhowe/dialogic/wiki/Command-Reference) &nbsp;::&nbsp; [API Docs](http://rednoise.org/dialogic/) &nbsp;::&nbsp; [Workbench](http://rednoise.org:8082/dialogic/editor/) &nbsp;::&nbsp; [NuGet Package](https://www.nuget.org/packages/org.rednoise.dialogic/)

Dialogic is a system designed to help writers easily create interactive scripts with generative elements. It enables writers to craft compelling dialog that responds organically both to user prompts and to events in the environment. The system supports abitrarily complex interactions between multiple actors, but makes no assumptions about how text is displayed, or about how users will choose their responses. These tasks are left to game designers and programmers (using tools like Unity).



Each section of text in a Dialogic script is known as a [CHAT](https://github.com/dhowe/dialogic/wiki/Command-Reference#chat). Each chat has a unique name and contains one or more commands. When a chat is run, each command is executed in order, until all have been run, or the system branches to a new chat. 

The simplest command is [SAY](https://github.com/dhowe/dialogic/wiki/Commands#say) which simply echoes the given output:

````
SAY Welcome to your first Dialogic script!
````

Because the system is designed for handling text, no quotations are needed around strings. In fact, the SAY keyword itself is even optional. So the simplest Dialogic script is just a single line of text:

````
Welcome to your first Dialogic script!
````

&nbsp;

### Basic Commands

Commands generally begin a line. Common commands include [SAY](https://github.com/dhowe/dialogic/wiki/Command-Reference#say), [DO](https://github.com/dhowe/dialogic/wiki/Command-Reference#do), [ASK](https://github.com/dhowe/dialogic/wiki/Command-Reference#ask), [OPT](https://github.com/dhowe/dialogic/wiki/Command-Reference#opt), [FIND](https://github.com/dhowe/dialogic/wiki/Command-Reference#find), [SET](https://github.com/dhowe/dialogic/wiki/Command-Reference#set), and [others](https://github.com/dhowe/dialogic/wiki/Command-Reference)

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

In the code below we _branch_, based on the user's response:

````
ASK Do you want to play a game?
OPT Sure #Game1
OPT No Thanks
````

If the user selects the first option, Dialogic jumps to the chat named "Game1". If not, the current chat continues.



### Generative Elements

Dialogic is designed to smoothly blend scripted and generated text to create the type of variation found in natural-sounding dialog. The simplest way to blend generative elements into a response is via the OR operator (the pipe | character), grouped with parentheses as follows:

```
SAY You look (sad | gloomy | depressed).
```

Elements between the | operators are randomly selected, so the line above will generate each of the following 3 outputs with equal probability:


```
You look sad.
You look gloomy.
You look depressed.
```


Writers may also specify weightings for various choices, as well as favoring choices that have not been recently selected. Another example, demonstrating nested OR constructions:

````
SAY I'm (very | super | really) glad to ((meet | know) you | learn about you).
````

&nbsp;

You can also save the results of an expansion for later use. For example, lets say that you wanted to pick a character name to be reused several times in a paragraph. You could do the following:

````
SAY Once there was a girl called [hero=(Jane | Mary)].
SAY $hero lived in [home=(Neverland | Nowhereland)].
SAY $hero liked living in $home.
````

Outputs would include:

````
Once there was a girl called Jane.
Jane lived in Neverland.
Jane liked living in Neverland.
````

OR

````
Once there was a girl called Mary.
Mary lived in Nowhereland.
Mary liked living in Nowhereland.
````

You could also use the [SET](https://github.com/dhowe/dialogic/wiki/Command-Reference#set) command to similar effect:

````
SET hero = (Jane | Mary)
SET home = (Neverland | Nowhereland)
SAY Once there was a girl called $hero
SAY $hero lived in $home.
SAY $hero liked living in $home.
````



### Transforms

Dialogic also supports _transformation functions_ (called transforms) for modifying the results of expanded symbols and groups. Built-in transforms include Pluralize(), Articlize(), and [others](http://rednoise.org/dialogic/class_dialogic_1_1_transforms.html), which can be called from scripts as follows:

````
ASK How many (tooth | menu | child).Pluralize() do you have?
````

which will result in one of the following: 

````
How many teeth do you have?
How many menus do you have?
How many children do you have?
````

OR 

````
SAY Are you (dog | cat | ant).Articlize()?
````

which gives: 

````
Are you a dog?
Are you a cat?
Are you an ant?
````

You can also use transforms on variables:
````
SET choices = (tooth | menu | child)
ASK How many $choices.Pluralize() do you have?
````

Or on parenthesized words or phrases
````
ASK How many (octopus).pluralize() do you have?
````

You can also use built-in C# string functions:
````
SET choices = (tooth | menu | child)
ASK How many $choices.ToUpper() do you have?
````

Or arbitrarily chain multiple transforms:
````
SET choices = (tooth | menu | child)
ASK How many $choices.pluralize().ToUpper() do you have?
````


&nbsp;

Coders can also add custom transforms as follows, which can then be called from Dialogic scripts:

```
chatRuntime.AddTransform("MyTrans", MyTransform);
```

Transform functions should be static functions that take and return a string, as follows:

````
public static string MyTransform(string str) { ... }
````




### Interruption / Smoothing

Dialogic is also designed to respond naturally to user interaction and/or interruption. This is enabled primarily via a stack abstraction in which new CHATS are added at top. When an event or other interruption occurs, the response CHAT is pushed atop the stack and the current CHAT marked as 'interrupted'. When the response CHAT is finished, control moves to the next interrupted chat on the stack. Smoothing sections can be added in order to make transitions more natural, i.e., 'so as I was saying'.

To add smoothing to a Chat, use the 'onResume' metadata tag, specifying either the label of the smoothing Chat, or a set of FIND constraints to use in locating it. In the example below, each time the 'Long' Chat is interrupted, 'Smooth2' will be triggered before it resumes once again.

````
CHAT Long {onResume=Smooth2}
SAY Oh, it's you...
SAY It's been a long time. How have you been?
...
...
SAY You get what I'm saying?


CHAT Smooth2 {noStart=true}
SAY Where was I? Oh, yes
````




### Special Characters
As in most scripting languages, certain characters have special meaning in Dialogic scripts. These include the following: !, #, }, {, ", =, etc. If you need to use these characters in your scripts, you can use [HTML entities](https://dev.w3.org/html5/html-author/charref), which will be replaced in Dialogic's output. This also applies to leading and trailing spaces and multiple consecutive spaces, for which you can use the `&nbsp;` entity.

````
CHAT TestSpecial
SAY &num; is usually a label
````
which will output: 

````
# is usually a label
````



### Integrating Dialogic

Dialogic can be run alone or with a game engine, such as Unity3D (see example below). The system includes two main components: the scripting language described above, and the runtime environment, which is responsible for passing events between the runtime and the application. 

In the C# example below, a [_ChatRuntime_](http://rednoise.org/dialogic/class_dialogic_1_1_chat_runtime.html) is created that reads in a chat descriptions from a plain-text file (or folder) and compiles them into a list of _Chat_ objects. The runtime's Run() function is called to start execution, specifying the Chat to run first.

The application calls the runtime's Update() function each frame, passing the current world-state (a dictionary of key-value pairs) and any event that occurred during that frame. If a Dialogic event occurs during the frame, it is returned from the Update function.
````C#

 public RealtimeGame() 
 {
    dialogic = new ChatRuntime();
    dialogic.ParseFile(fileOrFolder);
    dialogic.Run("#StartChat");
 }

 public IUpdateEvent Update() // Game Loop
 {
     // Call the dialogic interface
     IUpdateEvent evt = dialogic.Update(worldState, ref gameEvent);

     // Handle the event received
     if (evt != null) HandleEvent(evt);
     
     ...
 }
````




### Serialization
The Dialogic system can be paused and resumed, with state saved to a file or an array of bytes. The specific serialization package is up to you (we generally use [MessagePack](https://github.com/neuecc/MessagePack-CSharp), but other options can be used, as long as they  implement the [ISerializer](http://rednoise.org/dialogic/interface_dialogic_1_1_i_serializer.html) interface.) In the example below, current state is saved to a file, then reloaded into a new ChatRuntime.

```C#

// Create a runtime and load it with Chats
ChatRuntime rt1 = new ChatRuntime();
rt1.ParseFile(fileOrFolder);

// Now serialize its state to a file
FileInfo saveFile = new FileInfo("./runtime.ser");
ISerializer serializer = new SerializerMessagePack();
tmp.Save(serializer, saveFile);

// Now load it from the file and continue
ChatRuntime rt2 = ChatRuntime.Create(serializer, saveFile);
rt2.Run();

```



&nbsp;

### Building Dialogic with Visual Studio

1. Clone this respository to your local file system ```` $ git clone https://github.com/dhowe/dialogic.git````

1. From Visual Studio, do Menu->File->Open, then select dialogic.sln from the top-level of the cloned dir

1. The solution should open with 3-4 sub-projects, as in the image below. 

1. To run the tests, right-click 'tests' and select 'Run Item'

![](http://rednoise.org/images/vsloaded.png?raw=true)



&nbsp;
