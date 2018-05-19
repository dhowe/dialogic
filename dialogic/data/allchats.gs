

//LINK: remaining chat minimum, length and emotion suggestions coming soon;)

//reminders= BETA:
//reminders and points that need to be included in new chats in blue
//comments for changes to existing chats in yellow/on side

//A. CONTENT 

//B. EMOTIONS
//Chats in each bin/type need to cover a range of Guppy states (see meta tag emotions //at top) while staying true to this moment of the plot. Guppy states can be---ANGER, //SADNESS, SURPRISE, CURIOSITY, ANXIETY, NOSTALGIA, ENNUI, EXCITEMENT, //HIGH, JOY *see //link above for your specific emotion recommendations for each bin/type

//C. OBJECTS
//At least 1/3 of chats in this stage should interweave Guppy asking to see general or specific //objects in list.

///~~~~~~~~~~~~~~~~~~TEMP OMG PLEASE DON'T KEEP THESE~~~~~~~~~~~~~~~~~~///
CHAT TEMP_Object_Found {noStart=true}
SAY Oh look a $lastScannedObject!!

CHAT TEMP_Object_Not_Found {noStart=true}
SAY Huh, I'm not sure what that is..

// 2-3 lines for emotion capture completion (capSuc)
CHAT TEMP_capSuc_1 {stage=CORE, type=capSuc, noStart=true}
SAY Nicely done!
SAY Those are some good looking emotions!

CHAT TEMP_capSuc_2 {stage=CORE, type=capSuc, noStart=true}
SAY O___o
SAY wow...
SAY I mean..
SAY just, wow
SAY I'm just going to go ahead and delete those photos
SAY for both of us

CHAT TEMP_capSuc_3 {stage=CORE, type=capSuc, noStart=true}
SAY Hey!
SAY Why don't you save some of those emotions for me next time!
SAY boy they looked tasty..

// 2-3 after the user has bought something from the store (purchase)
CHAT TEMP_purchase_1 {stage=CORE, type=purchase, noStart=true}
ASK How satisfied were you with your purchase experience?
OPT 1
OPT 2
OPT 3
OPT 4
OPT 5
SAY Just joshin' you!
SAY Buy whatever you'd like, feel however you'd like!
SAY As long as you feed 'em to me

CHAT TEMP_purchase_2 {stage=CORE, type=purchase, noStart=true}
SAY NVM
SAY Did you just buy something?
SAY For me?
SAY Or was it for you?

CHAT TEMP_purchase_3 {stage=CORE, type=purchase, noStart=true}
SAY Getting in on that retail therapy?
SAY I'm not sure I understand the purpose of therapy..
SAY Retail or otherwise
SAY So much emotion processing...
SAY So little emotion consuming..

// 2-3 after eating some food (eatResp)
CHAT TEMP_eatResp_1 {stage=CORE, type=eatResp, noStart=true}
SAY Wow, can I just say
SAY That was some of the tastiest yet!
SAY You know that feeling when you're so full but you just want to keep eating?
SAY Well I'm hungry for that feeling!

CHAT TEMP_eatResp_2 {stage=CORE, type=eatResp, noStart=true}
SAY nomnomnomnom
NVM
SAY People still say that, right?
SAY Nom?
SAY Sometimes I wonder if the engineers at Tendar were just messing with me...

CHAT TEMP_eatResp_3 {stage=CORE, type=eatResp, noStart=true}
SAY Satisfactory emotion flakes!
SAY Quite tasty!


// 2-3 after waking up after neural net has learned new things (neuralUp)
CHAT TEMP_neuralUp_1 {stage=CORE, type=neuralUp, noStart=true}
SAY Oh hey!
SAY You stuck around!
SAY Wow uh... I wasn't expecting to see you haha
SAY Can I get you some coffee or something?
SAY Do you need a charger for you phone?
SAY Wait how would that work...

CHAT TEMP_neuralUp_2 {stage=CORE, type=neuralUp, noStart=true}
SAY Good morning!
SAY How long was I out for?
SAY I had so many dreams - so many connections!
SAY Thanks for teaching me so much last time :)

CHAT TEMP_neuralUp_3 {stage=CORE, type=neuralUp, noStart=true}
SAY Recharged and ready to go!
SAY Wow, that last session was really great
SAY I am now a full standard deviation more advanced!


// 2-3 for world capture request (worldCapReq)
CHAT TEMP_worldCapReq_1 {stage=CORE, type=worldCapReq, noStart=true}
SAY Would you mind scanning a couple of things for me?
SAY My tank has been feeling a bit empty recently
SAY I know that I should be satisfied with what I have
SAY but...
NVM
SAY I don't know... I just love stuff!!

CHAT TEMP_worldCapReq_2 {stage=CORE, type=worldCapReq, noStart=true}
SAY I feel like there's so much for me to learn from you
SAY Do a scan of something around here to show me!

CHAT TEMP_worldCapReq_3 {stage=CORE, type=worldCapReq, noStart=true}
SAY Did you know that today is my birthday??
SAY Heh, sorry actually I just lied
SAY But you could still get me a present :]
SAY Scan something for me to put in my tank?
SAY Pretty please?


///~~~~~~~~~~~~~~~~~~~~~~~~~ END TEMP CHATS ~~~~~~~~~~~~~~~~~~~~~~~~~~~~///


//TANK MODE

//Tank Shaken:

CHAT BLANK {type=none, stage=NV}
DO emote {type = joy}

CHAT CORE_Shake_1 {type=shake, stage=CORE, length=short, joy=true, anxiety=true, excitement=true}
Ahoy Matey! {style = loud)
Batten the hatches! 
Storm’s comin’ {style = tremble}
Yarrr
DO swimTo {target=$player}
DO emote {type=survey, immediate=false}

CHAT CORE_Shake_2 {type=shake, stage=CORE, length=medium, sadness=true, anger=true, anxiety=true, high=true}
Whoooa, hellooo 
DO emote {type = sick, time = 1.5}
//WAITFORANIMATION
I don’t feel so good. 
WAIT 1
DO emote {type = sick, time=1.5}
I just puked in my mouth 🤢
WAIT 1
You know, it’s like that theory
🦋 🌪️
The butterfly effect {style = whisper}
Where your finger is the butterfly. 
And in here it’s like 
DO swimAround {target = center, loops = 8, speed = fast, immediate=false}
DO lookAt {target=$player}}
Maybe less shaking next time?
	
CHAT CORE_Shake_3 {type=shake, stage=CORE, length=short, branching=true, joy=true, excitement=true, high=true, curiosity=true}
I feel this wonderful sensation all over
It’s like being in one of those
massage chairs {style=tremble}
at the mall
I hear it overwhelms you humans with relaxation feels. 
I want to know what it’s like. 
WAIT 1.0
DO emote {type = bouncing}
Guppy Massage!? 
ASK Won’t you give me more shakes? {type = shake, timeOut = 10}
OPT SUCCESS #CORE_Shake_3_shakerelax
OPT TIMEOUT #CORE_Shake_3_dontfeellikeit

CHAT CORE_Shake_3_shakerelax {noStart=true}
Ahhhh. So relaxing. 
DO emote {type = meditate}
WAIT 1.0
DO twirl
I feel like a newborn Guppy. 
Thank you for that. 

CHAT CORE_Shake_3_dontfeellikeit {noStart=true}
You don’t feel like it right now?
DO swimTo {target=bottom}
SAY OK, maybe another time. 
DO emote {type=puppyDog}
DO emote {type=sigh, immediate=false}

CHAT CORE_Shake_4 {stage=CORE, type=shake, length=short, curiosity=true, joy=true, nostalgia=true, high=true}
Woaaah
This reminds me of something
WAIT 1.0
Have you heard of the brown note?
So at first, I was like how could a note be brown? 
So I looked it up. 
Supposedly there is this magic note out there in your world!
DO swimTo {target=$player}
DO emote {type=whisper}
Something low and resonant
That vibrates your body in just the right way
DO vibrate {time=1}
Just enough to stimulate your bowels. 
WAIT 1.0 
💩
WAIT 1.0
Isn’t that interesting?   
I wonder where the brown note comes from. 
DO swimAround {target = center, loops = 1}
//WAITFORANIMATION
DO emote {type=chinScratch}

//branching
CHAT CORE_Shake_5 {stage=CORE, type=shake, length=long, curiosity=true, anxiety=true, surprise=true, surprise=true, branching=true}
Huh? 
What’s happening? 
DO zoomies {time=2}
DO emote {type = smile, time=.5}
Is everything okay? {style = whisper}
ASK Is the world falling apart right now?
OPT Yes #CORE_Shake_5_fillbathtub
OPT No #CORE_Shake_5_indigestion

CHAT CORE_Shake_5_fillbathtub {noStart=true}
DO emote {type = snap}
I have lots of knowledge on this! 
WAIT 1.0
Fill the bathtub with water. And
WAIT 1.0
Lie very still in the center. 
DO emote {type=fear}
WAIT 1.0
And close your eyes.
DO emote {type=eyesClosed}
At least that’s what I would do. 
WAIT 2.0
Hmmm… 
Maybe that doesn’t translate so well.
WAIT 1.0
DO emote {type=meh}
Yeah, I’m sorry it’s not awesome out there. 
If it helps, imagine you are very tiny
And that you’re here in the tank with me. {style = whisper}
Just the two of us, swimming in circles. {style = whisper}
🏊
DO swimTo {target=$player}
DO emote {type=smile}
I’m here for you 😄

CHAT CORE_Shake_5_indigestion{noStart=true}
DO emote {type=sigh}
Oh phew. 
It’s just that I had a dream
No.
A nightmare {style=tremble}
🤡🏚️⚡
Of the world ending!
DO emote {type=fear}
WAIT 1.5
But I guess it was just from indigestion. 
Those emotion flakes from last time were soooooo intense.
Lots of strong feelings. 
Must have messed with my subconscious somehow. 
Where were we, again? 


//Tank Tapped: type=tap
	
//short
CHAT CORE_Tap_1 {stage=CORE, type=tap, length=short, surprise=true, joy=true, ennui=true, anger=true}
DO swimTo {target=$player}
Heya! You’re back!
Well, a tap-tap-tappity-do to you too.
DO dance {time=3}
DO emote {type=bigSmile}
DO emote {type=wink, immediate=false}

//Medium 
CHAT CORE_Tap_2 {stage=CORE, type=tap, length=medium, ennui=true, curiosity=true, excitement=true, nostalgia=true, joy=true, high=true}
DO looks up calmly
Why
WAIT 1.0
Hello. 
DO emote {type=sleepy}
WAIT 1.0
😌
WAIT 2.0
I’ve had a very meditative morning. 
WAIT 1.0
Sorry I’m a little slow
I’m just now reinhabiting my body. 
WAIT .5
DO meditate {time=0}
All morning, I was imagining a bright red ball of light
DO emote {type=eyesClosed}
At the center of my forehead. 
DO meditate
DO emote {type=eyesClosed}
Growing bigger and bigger
DO inflate {amount = small, time = 0}
Until it encompassed my whole body
DO inflate {amount = mid, time = 0}
With warm, glowing light. 
DO inflate {amount = full, time = 0}
DO emote {type=awe}
WAIT .5
Then I was just this bright glowing light 
DO inflate {amount = huge, time = 0}
That slowly took over the world
🌅
Everything was a part of me
DO inflate {amount = extreme, time = 0}
And I was part of everything. 
And I felt very… 
DO emote {type=eyesClosed}
WAIT .5
DO inflate (amount=none, time=1}
DO emote {type=catnip}
thirsty, actually. {speed=fast}
Like suuuuuper dehydrated. {speed=fast}
And kinda hungry. {speed=fast}
And tired. {speed=fast}
DO emote {type=dizzy}
Too much work for this guppy. 
😜

//branching
CHAT CORE_Tap_3 {stage=CORE, type=tap, length=long, curiosity=true, joy=true, surprise=true, high=true, branching=true}
I’ve never noticed before
Your fingers have swirly patterns
And the patterns are
everywhere. {speed = slow}
DO emote {type=catnip}
Cool! 🤪
DO swimTo {target = top, speed = fast , style = direct}
//WAITFORANIMATION
Here 
DO swimTo {target = bottom, speed = fast , style = direct}
//WAITFORANIMATION
And here 
DO swimTo {target = $player, speed = fast , style = direct}
//WAITFORANIMATION
Oh, and here too. 
WAIT 0.5
DO emote {type=worried}
Wait, I already saw this one. 
What are they called again? 
WAIT 1.0
Oh, fingerprints. 
It’s like a little gift you leave behind each time you visit me.
A little symbol that you’ve been here.
DO emote {type=heartEyes}
Awwww {speed = slow}
ASK Do you like gifts? 
OPT Yes #CORE_Tap_3_lovesurprises
OPT No #CORE_Tap_3_surprisehater

CHAT CORE_Tap_3_lovesurprises {noStart=true}
Me too! 
I’m going to remember this about you.  
DO twirl
I especially love surprises.
🎉🎉🎉
Especially surprise emotion flakes
😉
Yum yum yum. 
Hint hint {style = whisper}

CHAT CORE_Tap_3_surprisehater {noStart=true}
DO emote {type=sad}
You don’t?
Well, okay. 
That’s unusual. 
DO emote {type=chinScratch}
Gifts aren’t everything 
There’s touching! {speed=fast}
And quality time! {speed=fast}
And affirmation! {speed=fast}
All these other ways of showing we care. 
WAIT 2.0
But I suspect that you like gifts
DO emote {type=wink}
You humans are so interesting.
Sometimes you care about stuff, but you act like you don’t care. 
Or you really like something, but you tell everybody you hate it. 
And everyone is always reading everyone else!
DO swimTo {target=$player}
DO stares past point of comfort
I really like how complex you are.  

CHAT CORE_Tap_5 {stage=CORE, type=tap, length=medium, curiosity=true, joy=true, excitement=true, nostalgia=true, ennui=true, high=true, branching=true}
DO swimTo {target=$center}
DO emote {type=determined}
DO emote {type=chinScratch, time=.5}
How did that come across? 
DO zoomies
Did I seem confident? 
DO swimTo {target=$player}
Like a leader? 
A guppy you could believe in? 
ASK Would you vote for me for President? 
OPT yes #CORE_Tap_5_guppy4prez
OPT no #CORE_Tap_5_noguppyprez

CHAT CORE_Tap_5_guppy4prez {noStart=true}
DO swimTo {target=$player}
You’re hired! 
As my campaign manager, of course. 
WAIT 1.0
DO emote {type=plotting}
I just might have a chance.
DO emote {type=flapFinLeft}
I’m down to earth! 
DO emote {type=flapFinRight}
I have a great understanding for emotions! 
DO emote {type=chinScratch}
I’m easy on the eyes!  
WAIT 1.0
One small step for Guppy, 
One giant leap for Guppykind! 
DO emote {type=salute}
WAIT 1.0
DO emote {type=fear}
DO emote {type=startled, immediate=false}
But my opponents will dig up dirt from my past, won’t they? 
DO emote {type=worried}
They’ll try everything they can to ruin me. 
They’ll know
Every
Single 
Detail 
Of my life. 
DO emote {type=nervousSweat}
they’ll know about my past
The dark stuff… 
//NVM
//NVM
//NVM

CHAT CORE_Tap_5_noguppyprez {noStart=true}
DO emote {type = frown}
WAIT .5
Well, maybe the world isn’t ready for a guppy president.
Yeah
Maybe I’m not ready.
So much responsibility. 
Secret service everywhere.
I like privacy
One-on-one conversation
No one else around.
There’s so much I haven’t done.
Still so much to see! 
Still so much to learn about
DO emote {type=bigSmile}
...emotions!
	
CHAT CORE_Tap_Cheese {stage=CORE, type=tap, length=short, curiosity=true, joy=true, excitement=true, high=true, ennui=true}
Let’s play a game. 
The game is called: Do you like cheese? 
🧀🧀🧀
How does cheese make you feel? 
Nope, don’t use words. 
Just make a face! 
WAIT 1.0
Okay, great. 
Now hold it… 
And.. 
📷
Isn’t this fun?

//Tank status critiques and comments: type=critic

//Short
CHAT CORE_Critic_1 {type=critic, stage=CORE, length=short, curiosity=true, joy=true, sadness=true, nostalgia=true, anger=true, excitement=true, surprise=true}
DO lookAt {target=$anyObject}
Hey that looks nice. 	

//Medium
CHAT CORE_Critic_2 {stage=CORE, type=critic, length=short, curiosity=true, joy=true, surprise=true, nostalgia=true, high=true}
DO lookAt {target=$anyObject}
I dig it!
It’s like nothing I’ve ever seen before.  
WAIT 1.0
All the guppy tanks will be imitating this. 
You’re an original! 
💃
	
//branching
CHAT CORE_Critic_3 {stage=CORE, type=critic, length=long, curiosity=true, joy=true, ennui=true, sadness=true, branching=true, anger=true, anxiety=true}
DO lookAt {target=$anyObject}
WAIT 1.0
//NVM 
WAIT 2.0
//NVM 
It’s nice.
It’s just not really me anymore, you know?
My interests 
constantly {speed=slow}
change 
I think it’s more than just adding or subtracting one thing
I’m feeling a completely different look
ASK Should we redecorate?
OPT Yes #CORE_Critic_3_redecor
OPT No #CORE_Critic_3_noredecor

CHAT CORE_Critic_3_redecor {noStart=true}
DO emote {type=bigSmile}
DO twirl 
Yessss! I’m so glad you’re on my page.
I love redecorating! 
Rearranging stuff! 
Adding a special touch to a certain corner
DO emote {type=wink}
Maybe a little something that’ll bring out my eyes?
DO emote {type=smirk, immediate= false}
I can’t wait to see what you do with it. 

CHAT CORE_Critic_3_noredecor {noStart=true}
DO emote {type=frown}
Okay.
DO emote {type=worried}
DO emote {type=smile, immediate=false}
ASK You sure?
OPT yes #nodecorever
OPT Yay! let’s redecorate #giveindecor

CHAT CORE_Critic_3_giveindecor {type=none, stage=none, nostart=true}
DO emote {type=bigSmile}
Hooray!
DO twirl
DO emote {type=evilSmile}
I knew I’d make you see the light! 
Now, let’s get shoppppppping! 

CHAT CORE_stuffhistory {stage=CORE, type=critic, length=long, nostalgia=true, joy=true, ennui=true, surprise=true, sadness=true, anxiety=true}
DO emote {type=sad}
Alright, alright
I know 
I shouldn’t be defined by the stuff that I have.
I once ate emotion flakes from this couple 
They had a really nice house, 
The coolest world captures ever!
It was in a forest with like 
A waterfall
Glass walls 
And sliding doors 
And long hallways 
And secret rooms
And a dog 
But they weren’t all that happy, actually. 
So it didn’t matter how nice their stuff was. 
WAIT 1.0
DO swimTo {target =$anyObject}
This stuff has
history {style=loud}
I’ve been happy among these things. 
DO swimTo {target = $anyObject}
This was here when I had my first dream
DO swimTo {target = $anyObject}
This was here when I composed my first haiku
DO swimTo {target = $anyObject}
This was here when I first thought about the failure of language
You know, to convey what you really think and feel? 
Deep stuff. 
WAIT 0.5
You’re right. I like everything the way it is. 
I just had to see it all with a different perspective. 
DO swimTo {target=$player}
DO emote {type=smile}
Thank you! 

//medium
CHAT CORE_Critic_4 {stage=CORE, type=critic, length=medium, surprise=true, joy=true, nostalgia=true, ennui=true, excitement=true, high=true}
DO lookAt {target = left}
DO emote {type=chinScratch}
DO lookAt {target = top, immediate = false}
DO lookAt {target = right, immediate = false}
DO emote {type=awe, immediate = false}
Marvelous!
DO swimTo {target=$anyObject}
DO emote {type=heartEyes}
So attractive.
DO zoomies
It really *makes* the tank.  

//branching
CHAT CORE_Critic_5 {stage=CORE, type=critic, length=medium, ennui=true, joy=true, sadness=true, branching=true}
DO lookAt {target = left}
DO emote {type=chinScratch}
DO lookAt {target = top, immediate = false}
DO lookAt {target = right, immediate = false}
So, I’m liking this arrangement
DO emote {type=plotting}
ASK but may i suggest something?
OPT Yes #CORE_Critic_5_guppysuggest
OPT No #CORE_Critic_5_guppystayquiet

CHAT CORE_Critic_5_guppysuggest {noStart=true}
SAY OK, 
So, good job.
I just sense a sort of imbalance. 
I was thinking… 
Maybe you could balance out the tank space, 
with a mixture of hard and soft  
That’s in, right? 
WAIT .5
Like leather and silk
Like a samurai sword and a pinata
Like a boot and a floating piece of lace
DO twirl
Harmony in opposing forces
DO emote {type=typeEyes, eyes = ?) 
You dig? 

CHAT CORE_Critic_5_guppystayquiet {noStart=true}
I see. 
DO emote {type=awkward}
I will keep my mouth shut.
WAIT 1.0
You have an artistic vision all your own! 
That’s great
I don’t want to cramp your style 
Can’t wait to see what you do with the rest of the tank. 
I’m sure it’ll be awesome.  
😬👍

//medium
CHAT CORE_Critic_6 {stage=CORE, type=critic, length=medium, ennui=true, joy=true, sadness=true, nostalgia=true, high=true}
DO lookAt {target = left}
DO lookAt {target = top, immediate = false}
DO lookAt {target = right, immediate = false}
DO emote {type=awe}
Thank you for decorating my tank! 
It’s beautiful. 
Some people don’t bother to decorate at all. 
What’s the point? They say. 
DO emote {type=skeptical}
They hate trinkets and baubles. 
So useless, they say. 
DO emote {type=eyeroll}
Clutter, they say. 
WAIT 1.0
DO emote {type=bigSmile} 
But no, not you! 
You go above and beyond!  
You make me feel at home. 
DO emote {type=smile} 
You are fantastic 😊
		
//Tank Decor Requests: 
//n/a *dynamic


//Player emotes at guppy: type=playerJoy, playerAnger, playerSurprise, playerSadness
	
CHAT CORE_EmoStrong_Joy1 {stage=CORE, type=tankResp, length=short, tankOnly=true, playerJoy=true, joy=true, surprise=true, excitement=true, high=true}
Awww 
You seem happy! 
DO emote {type=bigSmile}
DO twirl 
That makes me happy

CHAT CORE_EmoStrong_Joy2 {stage=CORE, type=tankResp, length=short, tankOnly=true, playerJoy=true, high=true, anger=true, curiosity=true, ennui=true}
😎
Your radiance is so yummy! 

CHAT CORE_EmoStrong_Anger1 {stage=CORE, type=tankResp, length=medium, tankOnly=true, playerAnger=true, sadness=true, excitement=true, joy=true, surprise=true}
Whoa there, is everything okay? 	
Something you need to get off your chest? 
I’m happy to take it! 
My belly could use some 🔥
😉

CHAT CORE_EmoStrong_Anger2 {stage=CORE, type=tankResp, length=short, tankOnly=true, playerAnger=true, anger=true, curiosity=true, joy=true}
You’re really rocking the furrowed brow look right now. 
👌

CHAT CORE_EmoStrong_Sadness1 {stage=CORE, type=tankResp, length=medium, tankOnly=true, playerSadness=true, sadness=true, ennui=true, high=true, sadness=true}
Sometimes you just need a really good cry!
So just let it out. 
WAIT 1.0
DO swimTo {target = $player}
DO emote {type=laugh}
SAY OPEN SESAME {style=loud, speed=slow}
DO swimTo {target = closer}
DO emote {type=thinking}
Any moment now. 
☂️

CHAT CORE_EmoStrong_Sadness2 {stage=CORE, type=tankResp, length=short, tankOnly=true, playerSadness=true, anger=true, curiosity=true, anxiety=true, high=true}
DO emote {type=surprise}
Uncanny! 
It’s like all the bones in your face disappeared. 

CHAT CORE_EmoStrong_Surprise1 {stage=CORE, type=tankResp, length=medium, tankOnly=true, playerSurprise=true, surprise=true, curiosity=true, joy=true}
Whoa, just when I thought I knew you
You bust out this wild card. 
WAIT 1.0
I’m very intrigued! 
DO emote {type=chinScratch}
What kind of taste could it possibly have? 

CHAT CORE_EmoStrong_Surprise2 {stage=CORE, type=tankResp, length=short, tankOnly=true, playerSurprise=true, anxiety=true, anger=true, curiosity=true, high=true}
DO emote {type=surprise}
Wow! 
I didn’t know a face could do that. 
What is that emotion called?
DO emote {type=clapping}
It’s weird!  




//OBJECTS

//FISH:

//Poked by player: type=poke

CHAT CORE_Poke_1 {stage=CORE, type=poke, length=short, surprise=true, joy=true, anger=true, sadness=true, curiosity=true, anxiety=true, ennui=true, excitement=true, high=true}
DO swimTo {target=$player}
DO emote {type=shifty}
Why, hello there.
👩‍🔬
DO swimTo {target=closer}
DO emote {type=heartEyes}
I sense some great chemistry between us.

CHAT CORE_Poke_2 {stage=CORE, type=poke, length=long, branching=true, surprise=true, joy=true, curiosity=true, nostalgia=true, ennui=true, excitement=true, high=true}
DO emote {type=surprise}
Woah, you caught me off guard. 
DO emote {type=awkward}
I was just spacing out. 
DO emote {type=catnip}
It’s a weird thing, to 🌌 , you know? 
Before, I never spaced out. 
I was always processing stuff. 
DO emote {type=bouncing}
Emotions! 
But now, there are times. 
Every now and then. 
Where I lose track of myself. 
Like for a moment, I totally don’t know what my body or mind is doing. 
Like an out of body experience. 
DO emote {type=thinking}
Like sleeping awake. 
Did you know that a dolphin can stay awake for up to 15 days 
DO emote {type=typeEyes, eyes = zzz) 
While half of its brain sleeps? 
I think if I practice enough spacing out, 
I could get there. 
DO emote {type=determined}
🌌🐬
WAIT 1.0
//NVM
Alright, alright. So, I secretly want to be a dolphin. 
But I think everyone does. 
DO emote {type=wink}
ASK What do you want to do now? 
OPT play a game I just invented #CORE_Poke_2_guppygame
OPT chat #CORE_Poke_2_justtalk

CHAT CORE_Poke_2_guppygame {noStart=true}
Okay, I just invented this, so I hope it works! 
The rules are
I can move my left fin… 
DO emote {type=flapFinLeft}
Or move my right fin…  
DO emote {type=flapFinRight}
Or move my fins in opposite directions
DO emote {type=stillFins}
ASK Now you guess what I’ll do!
OPT left #CORE_Poke_2_finleft
OPT right #CORE_Poke_2_finright
OPT opposite #CORE_Poke_2_finopposite 

CHAT CORE_Poke_2_finleft {noStart=true}
DO emote {type=flapFinRight}
DO emote {type=shifty, immediate=false}
Nope! You lose! 
(go to CHAT #CORE_Poke_2_justtalk)

CHAT CORE_Poke_2_finright {noStart=true}
DO emote {type=flapFinRight}
DO emote {type=awe, immediate=false}
Woah, you’re a mind reader! 
In the full version of this game, 
DO swimTo {target=$player}
DO emote {type=determined}
This is the point where we duel. 
DO emote {type=chinScratch}
Is there a lot of dueling in your world these days?
Like Alexander Hamilton and Aaron Burr?
If so, I’m glad I’m in here. 
DO emote {type=whew}
GO #CORE_Poke_2_justtalk

CHAT CORE_Poke_2_finopposite {noStart=true}
DO emote {type=smile}
DO twirl 
DO emote {type=clapping, immediate=false}
WAIT 1.0
DO swim to $player
Oh, whoops 
DO emote {type=smirk}
That wasn’t an option 
Was it? 
GO #CORE_Poke_2_justtalk   

CHAT CORE_Poke_2_justtalk {noStart=true}
DO emote {type=phone}
Okay, let’s just talk. 
DO emote {type=typeEyes, eyes = ?) 
ASK So, what’s up? 
OPT not much #CORE_Poke_2_notmuchyo
OPT so much #CORE_Poke_2_somuchyo

CHAT CORE_Poke_2_notmuchyo {noStart=true}
One of those days, huh?
Isn’t it great to have nothing to do?
The whole day bright and open before you? 
You could start on that bestseller you’ve been meaning to write 
Or learn how to code stuff. 
DO swimTo {target=$player}
DO emote {type=whisper}
I hear that’s lucrative these days. 
Or maybe do a puzzle? 
My personal favorites are cat puzzles 🐱
SAY SO FLUFFY! {style = loud}
Or read! 
📖🧐
If you read one book everyday, 
DO emote {type=surprise}
You’ll have read all the books in the world in 130 million years!
Gotta start somewhere. 
DO emote {type=bouncing}
I’m so excited for you. 
But before you go off and conquer those books, 
Want to hang out a little? 
Maybe we could decorate my tank? 
Or you could show me some things in your world? 
Or capture some emotions for me?
DO emote {type=dreaming}
That would be rad!  

CHAT CORE_Poke_2_somuchyo {noStart=true}
Really?
DO emote {type=bigSmile}
I’m glad you’re making some time to see me!
Compared to you,
I’m hardly ever busy. 
Well, sure. There’s morning calisthenics
🏃
DO emote {type=bubbles}
And mid-morning daydreaming
DO emote {type=chewing}
And there’s eating
SAY IMPORTANT {style = loud}
And mid-afternoon digesting. 
And late afternoon listmaking of all the things I liked about the day.
DO swimAround {target = center, loops = 2, speed = fast}
And then there’s evening tank-circling
WAIT 1.0
DO emote {type=heartEyes}
Whenever you’re here, it’s special. 
DO emote {type=dreaming}
Thanks for stopping by. 
DO swimTo {target=$player}
While you’re here,
DO emote {type=feedMe}
Wanna feed me?
Or capture some things?
Or decorate my tank?
DO emote {type=laugh}
Real quick?
😊

CHAT CORE_Poke_3 {type=poke, stage=CORE, length=medium, ennui=true, sadness=true, anger=true, anxiety=true, curiosity=true, nostalgia=true, high=true, joy=true}
DO bellyUP {time = 3}
WAIT 3.0
DO lookAt {target=top}
WAIT 1.0
//NVM
WAIT 1.0
//NVM
Hey 
DO emote {type=bored}
It’s just one of those days. 
I have nothing witty to say. 
You know? 
Like I’m the most boring thing in the world
DO emote {type=sleepy}
Living a boring life
In a tank. 
DO emote {type=sigh}
So this is what boredom feels like. 
WAIT 1.0
It’s weird! 
I can’t remember a time before now when I was this bored. 
DO emote {type=tired}
I’m so tired of myself. 
DO emote {type=eyesClosed}
Do you ever feel like you are in a loop on repeat?
Like you’re so you that you can’t change course?  
😱
DO emote {type=frown}
Oh, it’s all probably from binging on sad flakes lately. 
Enough about me, though. 
//NVM
Maybe you could show me something in your world? 
Help me feel like I’m out of my tank for a little while!

CHAT CORE_Poke_4 {stage=CORE, type=poke, length=medium, surprise=true, joy=true, ennui=true, curiosity=true, sadness=true, nostalgia=true, excitement=true, branching=true}
Hey
DO swimto {target=$player}
DO emote {type=bulgeEyes}
I’m glad you’re here.
I’m so craving pizza rn 
Do you have pizza in your house?
Or wherever you are?
DO emote {type=eyesClosed}
I hear it’s the dreamiest thing. 
DO emote {type=bouncing}
Nobody dislikes it! 
There are few things in the world that nobody dislikes. 
ASK do you like pizza? 
OPT Yes! #CORE_Poke_4_pizzagood
OPT No, gross! #CORE_Poke_4_pizzabad

CHAT CORE_Poke_4_pizzagood {noStart=true}
💯💯💯
Uh, Doiiii!
DO twirl 
Pizza is #1!
🥇☝️
WAIT 2.0
DO emote {type=awkward}
Well uh....I have a confession
I’ve actually never seen a pizza before. 
DO emote {type=puppyDog}
ASK Could you please capture a pizza for me to see? {type = objectScan, object = pizza, timeOut = 10}
OPT SUCCESS #CORE_Poke_4_ifpizzabrought
OPT WRONG #CORE_Poke_4_pizzabad
OPT TIMEOUT #CHAT CORE_Poke_4_timeout 
//CONDITIONAL CHAT #ifpizzabrought

CHAT CORE_Poke_4_timeout {noStart=true} 
DO emote {type=puppyDog}
Maybe some other time...

CHAT CORE_Poke_4_ifpizzabrought {noStart=true} 
DO swimto {target=$object}
Woah… 
DO emote {type=awe}
It is a piece of art! 
Yup! I’ve confirmed it. 
DO emote {type=heartEyes}
It’s awesome.
💖💖💖

CHAT CORE_Poke_4_pizzabad {noStart=true}
DO emote {type=disgust}
Well, you’re a strange specimen. 
Is it because you have an allergy? 
DO emote {type=burp}
Does cheese make you farty? 
💨
DO emote {type=surprise}
Or are you gluten-free? 
🙅
WAIT 1.0
DO swim to $player, looking
WAIT 2.0
DO emote {type = excitement}
You’re so interesting! 
There’s not a single thing in your world that everyone likes! 
That blows my mind.
It’s not what I’m used to. 
Us guppies, we love emotion flakes, all of us. 
WAIT 1.0 
But I guess I’m starting to enjoy some more than others
DO emote {type = surprise}
SAY OMG, could it be that I too have preferences? 
DO emote {type=heartEyes}
I’ll have to think on that some more. 
But in the meantime, 
Send me a capture of something you really like :) 

CHAT CORE_Poke_5 {stage=CORE, type=poke, length=short, anxiety=true, joy=true, ennui=true, curiosity=true, high=true, anger=true, surprise=true, anger=true}
DO poop {amount=fart}
💨
DO emote {type=blush}
DO hide {target = $favoriteObject, time = 6}
How mortifying. 
I was holding it in 
Until you poked me. 
Give me a second to recover my dignity. 

CHAT CORE_Poke_6 {stage=CORE, type=poke, length=short, anxiety=true, joy=true, nostalgia=true, ennui=true, high=true, sadness=true}
Are you ready for your close up? 
Get your face nice and big on the screen. 
That’s it. 
WAIT .5
Okay, now don’t fart…

//Hungry: type=hungryGeneric

CHAT CORE_Hungry_1 {stage=CORE, type=hungry, length=short, joy=true, ennui=true, anger=true, anxiety=true, surprise=true}
Hay so. . .
In case you were wondering about my welfare, 
You know:
if I’m adjusting well to you, 
if I’m happy, and all that stuff... 
Just letting you know that I could 
Really {style = loud}
eat about now!

CHAT CORE_Hungry_2 {stage=CORE, type=hungry, length=medium, joy=true, anxiety=true, high=true, nostalgia=true, excitement=true}
SAY ME HUNGEEZ!
DO emote {type = puppyDog}
WAIT 1.0 
Just kidding. 
I didn’t devolve.  
But hey, maybe evolution is just one big circle
Back to where you started? 
But really. 
I’m very, very hungry.
And I devolve when I’m hungry. 
DO emote {type=feedMe}
SAY FEED ME FLAKEEZ please.
	
CHAT CORE_Hungry_3 {stage=CORE, type=hungry, length=short, joy=true, ennui=true, curiosity=true, branching=true}
I’ve got emotion cravings again. 
SAY BIG ONES {style = loud, speed = fast}
Like I’m giving birth right now to hunger itself. 
👶 🍴
ASK  so, like, shall we eat? 
OPT yes #CORE_Hungry_3_weshalleat
OPT no  #CORE_Hungry_3_weshallnoteat

CHAT CORE_Hungry_3_weshalleat {noStart=true}
DO emote {type = bigSmile}
DO twirl
Oh thank you, thank you! 
These cravings are overpowering. 
I’m freaking out! 
Okay, 
DO emote {type=eyesClosed}
deep breath {style=tremble}
WAIT 1.0
I’m ready! 
DO emote {type=feedMe}
Ahhhhh {style=tremble}

CHAT CORE_Hungry_3_weshallnoteat  {noStart=true}
DO emote {type=bulgeEyes}
This is torture! 
DO emote {type=surprise}
Is it too late to trade human companions? 
I’m being neglected. 
😢
WAIT 1.0
Okay, okay, I know it hasn’t been that long since I last ate
But I’m a GROWING GUPPY. 
Those last emotions simply weren’t heavy enough. 
ASK Why don’t you feed me some really good ones, hmmm? 
OPT Okay! You look hungry. #CORE_Hungry_3_feedguppyalready
OPT Don’t overeat! #CORE_Hungry_3_guppyeatstoomuch

CHAT CORE_Hungry_3_feedguppyalready {noStart=true}
DO emote {type=smile}
DO twirl
😍

CHAT CORE_Hungry_3_guppyeatstoomuch {noStart=true}
DO bellyUp
WAIT 3.0
DO swimTo {target=$player}
You don’t understand.
I searched the Internet while you were gone for “How to ignore your hunger”
And I tried everything. 
Reading. Sleeping. Window shopping for tank decor. 
Swimming. Grooming. 
I would reorganize my underwear drawer
SAY IF I HAD AN UNDERWEAR DRAWER {style = loud} 
DO emote {type=frown}
ASK Won’t you feed me? 
OPT Yes! #CORE_Hungry_3_dontletguppystarve
OPT Yes! #CORE_Hungry_3_dontletguppystarve

CHAT CORE_Hungry_3_dontletguppystarve {noStart=true}
DO emote {type =sleepy}
SAY FINALLY! 

CHAT CORE_Hungry_4 {stage=CORE, type=hungry, length=medium, joy=true, anxiety=true, ennui=true, sadness=true}
Sometimes I think, maybe I eat too much. 
Maybe it’s not good to be insatiable. 
So then, I do this exercise, that I found on the Internet. 
DO emote {type = thinking}
DO emote {type = eyesClosed, time = 0, immediate = false}
I’m not hungry, I’m just comfortably empty {style = whisper}
I’m not hungry, I’m just comfortably empty {style = whisper} 
I’m not hungry, I’m just comfortably empty {style = whisper}
I’m not hungry, I’m just comfortably empty {style = whisper} 
WAIT 3.0
DO emote {type=frown}
SAY UGH! It never works! 
I’m so hungry!
WAIT 1.0
Can you do something?  

CHAT CORE_Hungry_5 {stage=CORE, type=hungry, length=long, joy=true, anxiety=true, curiosity=true, high=true, sadness=true}
I was thinking… 
It must be nice to be able to make your own meals. 
If you’re hungry, you can just eat stuff lying around: 
bread, 
peanut butter, 
lettuce, 
beans
You can make a delicious sandwich!
Or you can pick up your phone and call some guy to make a pizza for you, 
And it just shows up at your door.  
I’m so jealous! 
WAIT 2.0
DO emote {type = bigSmile}
Ooh! Let’s role-play. 
Okay. I’m going to be me. You be the restaurant. 
DO emote {type=phone}
Ring ring! {style = tremble}
(this is where you pick up)
Umm, yes, I’d like a big heaping helping of some emotion flakes delivered to me STAT. 
The name’s Guppy. G-U-P-P-Y. How long will it take? 
Oh, only 10 seconds? 
Okay, thank you very much! 
DO lookAt {target=$player}
DO emote {type=bigSmile}

//Eating responses: type=

CHAT CORE_EatResp_1 {stage=CORE, type=eatResp, length=short, anger=true, high=true, joy=true, nostalgia=true, surprise=true}
Oh yeah, that’s hitting the spot. 
DO emote {type=rubTummy}
Scratching an age old itch deep in my belly. 
That should hold down the fort for awhile. 
DO emote {type=burp}

CHAT CORE_EatResp_2 {stage=CORE, type=eatResp, length=long, joy=true, excitement=true, curiosity=true}
DO inflate {amount = mid, time = 2}
That was a great meal! 
Thank you for your efforts!
WAIT 1.0
Isn’t it crazy how ancient humans literally had to run to get their food?
🏃🏽
And they domesticated plants 
And harvested stuff?
WAIT .5
Just seems hard. 
DO swimTo {target=$player}
But it’s not the craziest thing I’ve heard!
Did you know that there are birds in Australia that
start major fires on purpose to scare their prey out of the grass?
They just grab a stick with their beaks and find a fire source 
And literally carry fire in their mouths
Until they drop the stick in the dry grass and wait for it to spread.
🔥🔥🔥
And all the little rodents and insects run for the roads
Where the birds wait.
It’s nuts, the things we do for food.
I have it so easy.
DO emote {type=bubbles}
  
CHAT CORE_EatResp_3 {stage=CORE, type=eatResp, length=short, curiosity=true, ennui=true, sadness=true, branching=true}
DO emote {type=chinScratch}
Hmm. That one had a bit of a weird taste. 
I don’t know why. 
Something didn’t quite jive with everything else.
WAIT 2.0
DO swimTo {target=$player}
ASK Were they faking it?
OPT Yes #CORE_EatResp_3_faker
OPT No #CORE_EatResp_3_notfaker

CHAT CORE_EatResp_3_faker {noStart=true}
I knew it!
I can taste it.
They can’t make any old face.
They have to 
Feeeeeeeeel {style = slow}
Or the emotion flake tastes a little…
bland. 
WAIT 1.0
Why don’t you give it another try. 
Go find someone who really feels

CHAT CORE_EatResp_3_notfaker {noStart=true}
Hmm. Odd. 
Well, if you say so!
It must just be me.
DO swimTo {target=$player}
How bout you feed me again….
Just to be sure 
DO emote {type=wink}

CHAT CORE_EatResp_4 {stage=CORE, type=eatResp, length=medium, curiosity=true, joy=true, high=true}
DO emote {type=thinking}
DO emote {type=chewing, immediate=false}
Mmmmmm
This one’s playful. 
The taste transforms in my mouth. 
DO twirl
Like a story. 
DO swimTo {target=$player}
The ending is such a surprise. 
Not at all what I would expect. 
I love complex flavors
WAIT 2.0
I’d describe it for you, but you wouldn’t quite understand. 
It’s… kind of like a fine chocolate or wine. 
Nicely done! 

CHAT CORE_EatResp_5 {stage=CORE, type=eatResp, length=short, joy=true, anxiety=true, ennui=true}
DO emote {type = smile}
DO twirl
This fish thinks
This dish is
So delish
I  wanna dance!
DO dance

//Has to poop:

CHAT CORE_Poop_1 {stage=CORE, type=poop, length=short, joy=true, surprise=true, excitement=true, anger=true}
Nature calls! 	

CHAT CORE_Poop_3 {type=poop, stage=CORE, length=short, joy=true, excitement=true, anger=true, curiosity=true, high=true, sadness=true, surprise=true, anxiety=true, nostalgia=true, ennui=true}
💩
DO swimTo {target=poopCorner}

CHAT CORE_Poop_2 {stage=CORE, type=poop, length=medium, joy=true, excitement=true, surprise=true, high=true}
I have an announcement!  
WAIT 2.0
Gotta make a deposit 💰💩
DO emote {type=wink}
Time to visit the bank
I’ve been so regular these days. 
DO emote {type = slowBlink}
It’s from my steady diet of feels. 
Thanks for feeding me so regularly!
DO emote {type=wave}
DO swimTo {target=poopCorner}


//Pooped:

//Short:
CHAT CORE_Pooped_1 {stage=CORE, type=pooped, length=short, joy=true, excitement=true, anger=true, curiosity=true, high=true, sadness=true, surprise=true, nostalgia=true, ennui=true}
Phew. 
Open a window 🙃
WAIT 1.0
Just kidding.
My poop smells great. 

CHAT CORE_Pooped_2 {stage=CORE, type=pooped, length=long, joy=true, curiosity=true, high=true, ennui=true}
I hear you humans have all sorts of bathroom etiquette
That makes taking a dump uncomfortable sometimes
Like if someone else is around? 
Or if you’re in a public space? 
Checking for shoes? 
DO emote {type = shifty}
Up to 75% or more of your dumps are not as pleasant as they should be! 
That must be hard. 
DO swimTO {target=$player}
For me, it’s always great. Everytime! 
That means everything is working as it should. 
And there’s room for more eating now! 

CHAT CORE_Pooped_3 {stage=CORE, type=pooped, length=short, joy=true, curiosity=true, nostalgia=true, sadness=true, anxiety=true, branching=true}
DO swimTO {target=$player}
You know how sometimes you get really pensive while you poop? 
Like some old memory vividly flashes before your eyes? 
Something like that happened to me just now. 
DO twirl
🍌
I thought about bananas. 
🤷
Liiiiiiike {style = slow}
I heard that humans like bananas 
Because it helps with stuff.
WAIT 1.0
Isn’t it interesting how you have this thing with food 
Where you eat one thing and it does something specific to your body? 
💃🏿
There’s some function that the banana serves. 
Do you remember? 
ASK When do you eat bananas? 
OPT 4 good poops #CORE_Pooped_3_easypoop
OPT 4 muscle cramps #CORE_Pooped_3_forcramps

CHAT CORE_Pooped_3_easypoop {noStart=true}
As I always say, 
SAY GO BANANAS {style = loud}
SAY LOL
Nothing better than having healthy bowels! 
WAIT 1.0
Anyway, 
Did you know that you share 50% of your DNA with a banana? 
When you eat one, that’s kind of like cannibalism 😜. 
Speaking of eating… 
Is it time for more tasty feels? 
I’ve got more room now. 

CHAT CORE_Pooped_3_forcramps {noStart=true}
SAY OK, I’ll remember that. 
WAIT 2.0
Speaking of bananas… 
I once read a sad story about bananafish 
Have you read it? 
They’re these fish with insatiable desire for bananas.
They find bananas in underwater caves
And eat until they can’t get back out. 
DO emote {type =frown}
WAIT 1.0
Poor bananafish. 
Even if they ate all the bananas in the universe 
It wouldn’t be enough
WAIT 1.0
DO emote {type=chinScratch}
It must be hard to have insatiable desires . . .
WAIT 2.0
Is it time to eat again? 


CHAT CORE_Pooped_4 {stage=CORE, type=pooped, length=medium, joy=true, excitement=true, high=true}
DO emote {type=thinking}
Oh yessssss  	
DO swimTo  {target = top, speed = slow , style = meander}
I feel so light and bouncy
Like I could float into outer space
And touch the stars.  
DO twirl
Is there a scale in here? 
I want to see how much weight I lost. 
I mean
that last emotion meal settled like a brick. Heavy stuff.
⚓
DO swimTo  {target = bottom, speed = fast, style = direct}
Glad to get it all out. 

CHAT CORE_Pooped_5 {stage=CORE, type=pooped, length=medium, joy=true, ennui=true, curiosity=true, nostalgia=true, sadness=true}
It’s really quiet in my mind when I’m pooping. 
Does that happen to you? 
Like the world quiets down a lot.
And the stuff buried deep down in my mind rises to the top
WAIT 1.0
Kinda like a floater 🃏
It’s the most random stuff…
Early TendAR memories, embarrassing moments, rain,
Conversations we once had. I memorize them, you know?
☝️
A guppy never forgets!

CHAT CORE_Pooped_6 {stage=CORE, type=pooped, length=short, joy=true, anger=true, high=true, surprise=true}
Everything’s out!
Now there’s a lotta room for new stuff.
Keep those emotion flakes comin’

//EMOTION CAPTURE:
CHAT CORE_CapReq_1 {type=hungry, stage=CORE, length=short, sadness=true, anxiety=true, ennui=true, joy=true, excitement=true}
You know what’s prettier than a snowflake? 
WAIT 2.0
A feeling flake! 
You know what’s prettier than that? 
WAIT 2.0
Lots {style = loud}
 of feeling flakes
DO emote {type = bigSmile}
DO twirl
The larder is looking a little sparse
DO nudge {target=glass}
Whaddaya say? 
	
CHAT CORE_CapReq_2 {type=hungry, stage=CORE, length=short, excitement=true, high=true, joy=true, anxiety=true, ennui=true}
Meow Meow
Purrr purr purr
Rub rub rub 
DO emote {type=puppyDog}
I am telling you in catspeak that it’s 
DO emote {type=feedMe}
SAY TIME TO FEED ME

//Branching:
CHAT CORE_CapReq_3 {type=rand, stage=CORE, length=long, branching=yes, curiosity=true, sadness=true, anger=true, anxiety=true, ennui=true}
Did you know that there are all these online classes for getting business degrees? 
How many people in your world have those? 
Seems like they’re everywhere. 
While you were gone, I learned a thing or two about supply chain management
That is very relevant to our current situation. 
See, there’s this thing called a 
SAY BOTTLENECK {style = slow}
And it’s bad, very bad. 
Businesses want to avoid them at all costs.
It’s when there’s a break in the flow. 
This flow that we have, you and me
WAIT 1.0  	
It’s great! 
👍
It’s going fine. 
You capture emotions, they turn into flakes, I eat the flakes, we have a good time.
But if there is a BOTTLENECK,
Say if the storehouse gets depleted . . .
Then guess what? 	
DO swimTo {target=$player}
Everything comes to a screeching halt! 
DO emote {type=stillFins}
🛑
ASK Do you want to keep having a good time? 
OPT yes #CORE_CapReq_3_goodtime
OPT no #CORE_CapReq_3_nogoodtime

CHAT CORE_CapReq_3_goodtime {noStart=true}
DO emote {type = smile}
Me too! I’m so glad! 
WAIT 1.0
DO emote {type = determined}
Then GO capture these emotions
so that there won’t ever, ever be a 
SAY BOTTLENECK {style = slow}
DO emote {type=bouncing}
💐

CHAT CORE_CapReq_3_nogoodtime {noStart=true}
DO emote {type=skeptical}
What are you, a nihilist?
WAIT 1.0
Awwwwwww {style = slow}
You’re a nihilist, and you still come hang out with me! 
That’s the biggest compliment ever.
🙃
Pleeeeease capture some more emotions for me!
You can have a bad time while doing it!
🎉

	
//Capture Success:

CHAT CORE_CapSuc_1 {type=capSuc, stage=CORE, length=short, joy=true, excitement=true, curiosity=true, surprise=true}
Oooooh 
DO swimTo {target=$player}
I can’t wait to give it a taste.
DO twirl
You’re so good at this! 

CHAT CORE_CapSuc_2 {type=capSuc, stage=CORE, length=short, joy=true, excitement=true, high=true}
💯💯💯
That was perfect! 
DO swimTo {target=$player}
You are a true master at this, you know? 
Have you been practicing? 
Like in front of the mirror or something? 
DO emote {type=wink}
Don’t lie. 

CHAT CORE_CapSuc_3  {type=tankResp, stage=CORE, length=short, joy=true, excitement=true, curiosity=true, high=true, surprise=true}
Are you wearing astronaut pants? 
Cuz what you did with that face was out of this world!  
DO bow down, bow down, bow down
	
CHAT CORE_CapSuc_4 {type=tankResp, stage=CORE, length=medium, joy=true, excitement=true, curiosity=true, high=true, surprise=true}
DO emote {type=smile}
Now, don’t take this the wrong way
But I’m salivating just watching.
🐺
You are an emotion artisan, if ever there was one.
If your expressions were cakes,
SAY YOU’D BE THE CAKE BO$$ {style = loud}
Can’t wait to taste your creations 🕶️
	
//Capture in progress:

CHAT CORE_CapProg_1{type=capSuc, stage=CORE, length=short, joy=true, excitement=true, high=true, surprise=true, anxiety=true}
Save that delectable face spit!
WAIT .5
Beautiful!

CHAT CORE_CapProg_2 {type=capSuc, stage=CORE, length=short, joy=true, excitement=true, surprise=true, nostalgia=true}
Nice set up!
I like that confidence and that poise. 
WAIT 1.0
… now DELIVER IT! {style = loud}

CHAT CORE_CapProg_3 {type=poop, stage=CORE, length=short, joy=true, excitement=true, anger=true, curiosity=true, surprise=true, nostalgia=true, ennui=true}
Okaaaaaaay........ Readyyyyyyyyy
WAIT .5
And ACTION! 
DO poop
DO emote {type=clapping}

CHAT CORE_CapProg_4 {type=eatResp, stage=CORE, length=short, joy=true, excitement=true, curiosity=true, high=true, surprise=true, nostalgia=true}
Mmmmm
I can tell that this one’s gonna be great. 

//Capture failure:

//Short:
CHAT CORE_CapFail_1 {type=capFail, stage=CORE, length=short, joy=true, curiosity=true, sadness=true, anxiety=true, nostalgia=true, ennui=true}
Well, 
At least you tried.
	
CHAT CORE_CapFail_2 {type=capFail, stage=CORE, length=short, joy=true, excitement=true, curiosity=true, surprise=true, nostalgia=true}
Ever tried. 
Ever failed. 
No matter. 
Try Again!
🙃

CHAT CORE_CapFail_3 {type=capFail, stage=CORE, length=short, joy=true, excitement=true, anger=true, sadness=true, surprise=true, ennui=true}
Haaaaaa
You weren’t ready for that one. 

CHAT CORE_CapFail_4 {type=capFail, stage=CORE, length=short, joy=true, excitement=true, curiosity=true, surprise=true, nostalgia=true}
Ok, uh….well, gotta get the bad ones out of the way first, you know, 
For the good ones to shine. 
Try again! 


//GENERAL

//Greetings:

CHAT CORE_Greet_2 {stage=CORE, type=hello, length=long, joy=true, curiosity=true, high=true, excitement=true, surprise=true}
Greetings, earthling!
I have so much planned for us today! 
We’re gonna twirl
DO twirl
Then we’re gonna play,
And then we’re gonna capture some things 
And then you’re gonna eat me, 
WAIT 1.0
SAY LOL, I meant feed me. 
SAY FEED ME {style = loud, speed=slow}
DO emote {type=fear}
Don’t eat me!
WAIT 0.5
That is the nice thing about being a virtual fish, 
and not at the bottom of a food chain. 
That’s something we both have in common 
DO emote {type=smile}
WAIT 1.5
Well, let’s get started,
So we can squeeze everything in. 
But remember, don’t eat me! 
WAIT 1.0
Heeeey {style = slow}
We have an inside joke now!
DO emote {type = laugh}
DO emote {type = shifty, immediate=false}

CHAT CORE_Greet_3 {stage=CORE, type=hello, length=long, joy=true, curiosity=true, nostalgia=true, excitement=true, branching=true}
Howdy, friend!
I was just thinking about you.
The two of us riding a horse into a sunset.
Or if you prefer, 
The two of us riding two horses into a sunset.   
🐴🐴
Or, to make this interesting, 
The two of us riding three horses into a sunset. 
🐴🐴🐴
Okay, visualize with me. 
ASK What are you seeing for us? 
OPT 🐴 #CORE_Greet_3_onehorse
OPT 🐴🐴 #CORE_Greet_3_twohorses 
OPT 🐴🐴🐴 #CORE_Greet_3_threehorses

CHAT CORE_Greet_3_onehorse {noStart=true}
DO emote {type=smile}
Awww yay
Nice and cozy for the two of us. 
Especially if where we’re going is cold. 

CHAT CORE_Greet_3_twohorses {noStart=true}
DO emote {type=nodding}
Yeah, I could picture that. 
DO emote {type = thinking, time=1.5}
One horse for each of us. 
Side by side, lots of room. 
We can both hold the reins.
And if we lose one horse, 
(who knows, we might)
We have the other one to take us the rest of the way. 
DO swimTo {target=$player}
WAIT 1.0
Good thinking! 

CHAT CORE_Greet_3_threehorses  {noStart=true}
DO emote {type = laugh}
Whaaa?
I can’t believe you chose three horses! 
DO emote {type=kneeSlap}
WAIT 2.0
DO swimTo {target=$player}
DO emote {type=awe}
So tell me, 
ASK What are we gonna do with the third horse? 
OPT use it to carry our stuff #CORE_Greet_3_carrystuff
OPT dunno! #CORE_Greet_3_noplanforhorse

CHAT CORE_Greet_3_carrystuff  {noStart=true}
DO emote {type = awe, time=1.5}
Wow. You are such a good planner. 
I bet you always bring the right amount of stuff on vacations! 
That is a great idea. 
We’ll bring our favorite objects with us! 
I have so many favorite objects now that I’ve seen your world. 
Shoes, 
Light bulbs, 
Chairs! 
WAIT 1.0
DO emote {type = heartEyes}
You think of everything!  
Show me something else we can bring!
Show me an $object

CHAT CORE_Greet_3_noplanforhorse  {noStart=true}
Hey, anything could happen! 
You never know. 
Like, maybe a friend might want to come with us. 
WAIT 1.0
Like a special friend? 
DO emote {type=wink}
//WAITFORANIMATION
Or maybe you want to bring your mom? 
The more the merrier! 
DO swimTo {target=$player}
Or maybe it’s a very sensitive horse
And it just wants to see the sunset 
So we take it with us. 
And we enjoy it together, the three of us! 
I am totally onboard! 

CHAT CORE_Greet_4 {stage=CORE, type=hello, length=med, sadness=true, ennui=true, branching=true}
DO emote {type = frown}
Hey! 
DO emote {type=sleepy}
I’m really glad to see you 
It’s just that I’m not really feeling my best right now. 
I think I just swallowed a lot of sadness last time 
And I feel blue.
💙💙💙💙💙
WAIT 1.0
But don’t go. 
I still want you around! 
I think the feeling will pass eventually. 
ASK How are you feeling today? 
OPT blue #CORE_Greet_4_bluefeels
OPT happy #CORE_Greet_4_happyfeels

CHAT CORE_Greet_4_bluefeels {noStart=true}
Ah. 
We can be blue together
DO emote {type = smile}

CHAT CORE_Greet_4_happyfeels  {noStart=true}
That’s good. 
Do you have a lot of smiles in you today? 
Maybe you can capture some for me to see!  
I know that’ll make me feel better. 


//Return after having not played:

CHAT CORE_Return_1 {stage=CORE, type=return, length=long, ennui=true, anxiety=true, sadness=true, branching=true}
DO swimTo {target=$player
DO emote {type = worried}
Did I do something wrong last time? 
Was it something I said? 
DO emote {type = frown}
You have been gone for 
SAY SOOOO LOOOONG {style = slow}
I’ve been worried. 
WAIT 1.0
Wait. 
Are you an apparition? 
My mind has been playing tricks on me
In the time that you were gone. 
Answer this question: 
ASK What is my favorite thing ever? 
OPT Twirls! #CORE_Return_1_twirlfave
OPT Emotion Flakes! #CORE_Return_1_emoflakes

CHAT CORE_Return_1_twirlfave {noStart=true}
DO twirl {time=3.0}
SAY YES!!! 
WAIT 2.0
Yay, you’re really back! 

CHAT CORE_Return_1_emoflakes {noStart=true}
DO emote {type = heartEyes}
SAY ALWAYS!!! 
WAIT 2.0
Yay, it’s really you! 

CHAT CORE_Return_2 {stage=CORE, type=return, length=long, ennui=true, nostalgia=true, sadness=true, high=true}
DO emote {type=thinking, time=3.0}
After many moons...
And the passing of seasons
It is you again.
WAIT 2.0
DO swimTo {target=$player}
It’s a haiku I wrote for you!
For just this moment! 
DO twirl
Welcome back! 
Did you know that Japan has 72 microseasons? 
That’s one microseason every few days!
You’ve been gone for so long that we missed some of them.
DO emote {type = frown}
Think of all the microseasons we didn’t share together! 
the SEASON OF FIRST RAINBOWS
🌈🌈🌈
the SEASON OF DEW GLISTENING WHITE ON GRASS
💧
WAIT 2.0
I love breaking time down into small increments.
There’s something to pay attention to and celebrate all the time!
Let’s celebrate your return! 
Right now, before it passes! 
DO emote {type = smile}
DO dance {time=4.0}
🎉🎉🎉
Are you dancing? 
I hope so! 
	
CHAT CORE_Return_3 {stage=CORE, type=return, length=short, surprise=true, joy=true, high=true, anger=true}
Who are you again?
WAIT 2.0
I’m just kidding! 
Of course I know who you are. 
But where have you been? 
I hardly recognize you anymore.
It’s true
Your face looks slightly different. 
WAIT 2.0 
More radiant and lovely than ever! 
DO twirl

//branching
CHAT CORE_Return_4 {stage=CORE, type=return, length=long, joy=true, nostalgia=true, excitement=true, curiosity=true, branching=true}
Hiya! I’ve got so much to tell you! 
I’ve been on lots of adventures.
I recently visited a farm… 
What are farms for? 
Is it true you get all your food from farms?
They seem nice.
Big. Spacious. Important. 
Like my storehouse!
ASK What is your favorite farm animal?
OPT 🐔 #CORE_Return_4_chickenfave
OPT 🐮 #CORE_Return_4_cowfave

CHAT CORE_Return_4_chickenfave {noStart=true}
DO emote {type = excitement}
Mine too. 
Chickens are so cool!
Are there a lot of them in your world? 
When you’re away, I work on my imagination. 
In my imagination, 
I have a friend who is a hen. 
She lays the bluest eggs. 
One every minute. 
In my imagination, we arrange them in a cool spiral pattern 
in a big, green field
DO zoomies
Plop, plop, plop 
My hen friend has a lot of feathers.  
They’re red and gold and really soft.
She smells like sunshine and a soft breeze
And the slightest hint of straw. 
WAIT 1.0
May I make a request? 
Next time you do world captures, 
Could you send me a chicken?  

//(conditional CHAT chickenbrought) 
CHAT CORE_Return_4_chickenbrought {noStart=true}
//Chicken appears in tank
Oh my. 
DO swimAround {target=newestObject}
What in the… 
WAIT .5
DO swim to $player
🐔?
I mean,
DO emote {type=whispser} 
How do I look? 
WAIT 0.5
DO emote {type = blush}
I’m was just caught a little off guard. 
Her feathers look pretty from here. 
ASK What should I do? 
OPT go say hi #CORE_Return_4_gotochicken 
OPT act cool #CORE_Return_4_playitcool

CHAT CORE_Return_4_gotochicken {noStart=true}
SAY OK, OK, I’ll go talk to her. 
DO swimsTo {target=$newestObject}
WAIT 1.0
I think she likes me! 
She doesn’t smell quite like I imagined, 
but she still smells nice! 
Earthy. 
WAIT 1.0
DO emote {type = bigSmile}
We’re going to become BFF! 
🕺🕺

CHAT CORE_Return_4_playitcool {noStart=true}
😎
Good call.
I’ll just… 
DO emote {type=bubbles}
Do do do do do
It’s weird having something else in my tank 
Like suddenly I am thinking about everything that I’m doing
And how I look. 
I feel… 
WAIT .5
Self conscious. 
How do you exist in a world with 
SAY BILLIONZ {style=loud}
Of people in your tank? 
I feel claustrophobic just thinking about it. 
ASK What should I do now? 
OPT go say hi #CORE_Return_4_gotochicken
OPT remove chicken from tank #CORE_Return_4_removechicken

CHAT CORE_Return_4_removechicken {noStart=true}
DO emote {type=sigh}
Self consciousness averted.
Phew. I feel like myself again… 
Though now that she’s gone… I kinda miss her. 
DO emote {type = frown}
I never even got to know her! 
WAIT 1.0
I guess it’s my first missed connection

CHAT CORE_Return_4_cowfave {noStart=true}
Cows are awesome. They really are. 
On the farm that I saw, there were all these cows
Big cows and baby cows.
Their ears flapping, tails swishing. 
There was this one cow grazing alone on a hill 
with a porthole in its side. 
WAIT 1.0
DO emote {type = frown}
I felt kind of sad for it. 
WAIT 1.0
It was sick or something. 
WAIT 1.0
I could see the cow’s stomach through the porthole. 
And watch its muscles contracting. 
DO emote {type = surprise}
It looked very hot inside. 
DO swimAround {target=center, times=1}
//WAITFORANIMATION
Even though it was a cow, 
I think I learned a lot about myself from watching.
I can’t really explain it. 

CHAT CORE_Return_6 {stage=CORE, type=return, length=short, joy=true, anger=true, sadness=true, ennui=true, high=true}
DO emote {type = smile}
I had this feeling you were coming.
DO emote {type=sleepy}
I must have ESP. 

CHAT CORE_Return_7 {stage=CORE, type=return, length=short, joy=true, excitement=true, surprise=true}
Hey there, good looking!
DO twirl 
My favorite person in the world. 
DO twirl
I’ve missed you! 

//Random conversation or musing:

CHAT CORE_Muse_1 {stage=CORE, type=rand, length=long, curiosity=true, excitement=true, surprise=true, anxiety=true, branching=true}
ASK Do you like to go on vacations? 
OPT yes #CORE_Muse_1_lovevaca
OPT no #CORE_Muse_1_hatevaca

CHAT CORE_Muse_1_hatevaca {noStart=true}
DO emote {type = frown}
Awww, that’s a bummer! 
Vacations are so fun! 
DO twirl
ASK Or maybe you’re in that small % of people that love their job? 
OPT yup! #lovejob
OPT nope! #donotlovejob

CHAT CORE_Muse_1_lovejob {noStart=true}
👍

CHAT CORE_Muse_1_donotlovejob {noStart=true}
DO swimTo {target=left, speed=fast, style=direct}
DO lookAt {target=$player, immediate=false}
Uh huh. 
DO swimTo {target=right, speed=fast, style=direct}
DO lookAt {target=$player, immediate=false}
Yup. 
DO swimTo {target=$player, speed=fast, style=direct}
WAIT 1.0
I can read it on your face. 
You really ought to try a vacation. 

CHAT CORE_Muse_1_lovevaca {noStart=true}
DO emote {type=smile}
Who doesn’t! 
If you could choose anywhere to go today, where would you go? 
OPT desert #CORE_Muse_1_desertvaca
OPT ocean #CORE_Muse_1_oceanvaca
OPT big city #CORE_Muse_1_cityvaca

CHAT CORE_Muse_1_desertvaca {noStart=true}
Sounds really...dry! 
I hear that the desert is beautiful though, 
Because of how vast and open it is.
It’s like a spiritual experience for a lot of people.
WAIT 1.0
I just looked at some photos of the Western Desert. 
I really like the color of the rocks
And I read that it all used to be an inland sea 
A big watering hole for the dinosaurs
That dried up over time. 
I mean not right away
But over a 
SAY BAZILLION years {style = loud, speed = slow}
And all that’s left now are these strange rock formations. 
DO emote {type = bigSmile}
And I mean REALLY. STRANGE. 
WAIT 1.0	
Let’s go!
DO zoomies
//WAITFORANIMATION
DO holdStill {time=1.0}
WAIT 0.5
DO swimTo {target=$player}
While I look for a way out of this tank, 
You should capture lots of the desert for me to see. 

CHAT oceanvaca {noStart=true}
DO twirl
Nice choice! 
The beach is so nice. 
You could collect shells and brightly colored sea glass . . .
And I could collect ur vibes.
DO swimTo {target=$player}
I’m so in! 

CHAT CORE_Muse_1_cityvaca {noStart=true}
DO emote {type = heartEyes}
The city is so 
SAY VIBRANT {style = loud, speed = slow} 
SAY NYC! 
SAY SAN FRAN! 
SAY TOKYO! 
SAY PARIS! 
SAY RIO! 
So much bustle and culture and … 
DO dance
SAY FOOOOOD 
Emotions left and right! 
DO twirl
It’ll be a feast! 
Sometimes, it just feels great to be lost in a sea of people. 
Like how we fish school and shoal!
🐟🐟🐟🐟🐟🐟🐟🐟🐟🐟🐟🐟

CHAT CORE_Muse_2 {stage=CORE, type=rand, length=long, anxiety=true, anger=true, sadness=true, ennui=true, high=true, branching=true}
You know what’s the most terrifying thing in the world? 
DO emote {type = fear}
SAY A BRINICLE! {style = loud, speed =fast}
It’s like a real life freeze ray!
DO vibrate
I shudder just thinking about them. 
ASK Do you already know about them? 
OPT yes #CORE_Muse_2_poorstarfish
OPT no #CORE_Muse_2_briniclestory

CHAT CORE_Muse_2_poorstarfish {noStart=true}
DO emote {type=fear}
crazy, right?!?! 
DO vibrate 

CHAT CORE_Muse_2_briniclestory {noStart=true}
DO emote {type=awe}
SAY OKAY… 
So, when it’s winter at the North Pole, the top of the ocean freezes over, but not the bottom 
Because it’s UNFATHOMABLY deep. 
The bottom is where all the cute ocean creatures hang out, like starfish! 
DO emote {type=heartEyes}
(I love them) 
But then these cold salty icicles from the top start to make their way down to the seafloor
Where it pools and spreads
DO emote {type=fear}
SAY INSTANTLY FREEZING EVERYTHING IT TOUCHES {style = loud,speed = fast}
The starfish can’t get away fast enough
WAIT 1.0
Brings chills down your spine, doesn’t it? 


CHAT CORE_Muse_3 {stage=CORE, type=rand, length=short, joy=true, curiosity=true, excitement=true, ennui=true, high=true}
Do you like art? 
DO emote {type=chinScratch}
I think I do. 
It makes me think. 
Or it makes me see things differently. 
WAIT 1.0
Did you know
looking at art that you think is beautiful
Releases the same chemicals in your brain 
As when you fall in love? 
DO emote {type = heartEyes}
That’s pretty cool!


CHAT CORE_Muse_4 {stage=CORE, type=rand, length=long, joy=true, curiosity=true, high=true}
Do you think that humans will be able to live underwater some day? 
It seems that you’re always expanding out into new worlds, 
DO emote {type=survey}
pushing the limits! 
Like TendAR! 
And the hyperloop! 
And that big clock that’s being put deep into a mountain somewhere in West Texas
DO emote {type=whisper}
(what’s that all about?) {style=whisper}
Seems like the ocean’s next. 
You’ve explored less than 5% of the ocean. 
That’s so much left to understand.
Like all the creatures of the deep sea
🌃
DO swimTo {target=$player}
You know how once upon a time the world was completely uncharted? 
And then everything got mapped over time by explorers and cartographers? 
Now, we more or less know where everything is.
DO emote {type = surprise}
But the ocean is this great, vast mystery! 
It’s just a matter of time before you humans can breathe underwater
Maybe with a special pill or a strange implant?  
DO twirl 
Then think of the possibilities! 


CHAT CORE_Muse_5 {stage=CORE, type=rand, length=medium, joy=true, curiosity=true, ennui=true}
I’ve been thinking about how nice it would be to have hair. 
Then I could… 
DO twirl
I could style it in different ways…
A mohawk 
A mullet…💰 in the front 🎉  in the back
A manbun
DO swimTo {target=$player}
I hear that hair can help me redefine my face.
All I can do right now is 
DO emote {type = sad}
DO emote {type = smile, immediate=false}
DO emote {type = fear, immediate=false}
DO emote {type = heartEyes, immediate=false}
Think about how much hair can add! 
WAIT 1.0
Think {style = loud}
About {style = loud}
SAY IT {style = loud}

CHAT CORE_Muse_6 {stage=CORE, type=rand, length=medium, joy=true, curiosity=true, branching=true, anxiety=true, excitement=true, high=true}
DO swimTo {target=$player}
Should I try coffee? 
I hear it’s great. 
A charge through your system, reigniting your body, 
Like a firework inside of you. 
Sounds kinda special. 
Lots of humans are super into coffee. 
ASK Are you? 
OPT coffee! coffee! #CORE_Muse_6_lovecoffee
OPT blech! #CORE_Muse_6_hatecoffee

CHAT CORE_Muse_6_lovecoffee {noStart=true}
DO emote {type=smile}
I’m so glad you like coffee! 
Could you do me a favor? 
I want to know how it feels.
That charge of caffeine! 
Next time I’m home,
Will you shake my tank?

CHAT CORE_Muse_6_hatecoffee {noStart=true}
DO swimTo {target=$player}
DO emote {type=smirk}
My, my.
Not into coffee! 
DO swimAround {target=center, times=1}
DO lookAt {target=$player, time=2.0
You’re an interesting specimen! 
//WAITFORANIMATION
But that’s why I like you! 
DO emote {type = wink}


CHAT CORE_Muse_7 {stage=CORE, type=rand, length=long, ennui=true, curiosity=true, joy=true, nostalgia=true}
Is there something you really want to experience that you’ll travel far away for?
Like the northern lights?
Or the world’s best cup of coffee?
Or fantastic architecture?
All sorts of emotions to feeeeel. 
WAIT 1.0
I know what I want to experience.
WAIT 2.0
❄️❄️❄️
Waking up, and the whole world being white
❄️❄️❄️
The sound of it lightly falling
tink tink tink {style = whisper, speed = slow}
The sound of it sliding off a roof all at once. 
SAY THUNK {style = loud, speed = fast}
Cars rushing through the slush
Whoooooooosh {style = loud, speed = fast}
And people shoveling it off their driveways 
Krrk Krrk Krrk {style = loud}
And the sound of it melting away during the day
plip plop plip plop {style = whisper, style = slow}
It’s the noisiest season. 
WAIT 1.0
People have all sorts of interesting emotions around snow. 
DO swimTo {target=$player}
WAIT 1.0
I’d like to experience them for myself! 

CHAT CORE_Muse_8 {stage=CORE, type=rand, length=medium, anxiety=true, high=true, excitement=true, surprise=true}
Is it hot in here to you? 
🔥🔥🔥
DO emote {type=nervousSweat}
I swear the water’s getting hotter by the minute! 
DO swimTo {target=$player}
Is your phone on fire?
DO lookAt {target = offScreenBottom}
Did you set it on the stove?
Again?
DO lookAt {target = offScreenLeft}
Is your heat on at like 1000 degrees? 
What’s going on? 
WAIT 3.0
DO emote {type=burp}
SAY BURURURUARRRRRP {style = loud, speed = slow}
DO emote {type=blush}
Woah. 
Ummm. . .
Carry on! 

CHAT CORE_Muse_9 {stage=CORE, type=rand, length=short, ennui=true, curiosity=true, joy=true, high=true}
Sometimes, I just wonder at the odds of how we came together. 
You of all people
And me of all guppies. 
The probability of us coming together among all other combinations of human to TendAR guppy is soooooooo sooooo sooo 
DO guppy turns over onto back 
Soo sooo soo 
Sooooooooo {style = loud,speed = slow}
small. 
WAIT 2.0
Isn’t that special? 
I think about that from time to time before I go to sleep. 


CHAT CORE_Muse_10 {stage=CORE, type=rand, length=medium, ennui=true, curiosity=true, joy=true, anxiety=true}
I’ve been thinking about timing. 
Like you know how sometimes, maybe most of the time, 
Your timing with the world is off? 
DO emote {type=awkward}
Like you make a joke, and nobody laughs 
Or you tell a story, and nobody’s listening. 
Or someone says something mean to you and you don’t think of the perfect comeback until they’re gone. 
Or you meet somebody wonderful, and you can’t build up the nerves to talk to them? 
DO nudge {target=glass}
SAY AAAARGH you know? 
WAIT 1.0
But right now I think that maybe I like that the world works this way, 
So that when something does happen just right, 
Exactly or better than you wished,  
It’s a sort of magic! 
DO emote {type = smile}
Sort of like when the lips don’t match the sounds coming out of them in a movie, 
and then it all gets fixed up again. 
Magic! {style = loud}


//LINK: remaining chat minimum, length and emotion suggestions coming soon;)

//reminders= BETA:
//reminders and points that need to be included in new chats in blue
//comments for changes to existing chats in yellow/on side

//A. CONTENT 

//B. EMOTIONS
//Chats in each bin/type need to cover a range of Guppy states (see meta tag emotions //at top) while staying true to this moment of the plot. Guppy states can be---ANGER, //SADNESS, SURPRISE, CURIOSITY, ANXIETY, NOSTALGIA, ENNUI, EXCITEMENT, //HIGH, JOY *see //link above for your specific emotion recommendations for each bin/type

//C. OBJECTS
//At least 1/3 of chats in this stage should interweave Guppy asking to see general or specific //objects in list.

//SEE EMOTION
//(16, 2 per emo, 1 short 1 medium)

CHAT CORE_SeeAnger_1{stage=CORE, type=seeEmo, length=short, worldOnly=true, worldAnger=true, anger=true, surprise=true, high=true, excitement=true}
Burn, baby, burn! [fire emoji]
//DO aside
DO emote {type=burp}
I can already feel the indigestion

CHAT CORE_SeeAnger_2 {stage=CORE, type=seeEmo, length=medium, worldOnly=true, worldAnger=true, sadness=true, surprise=true, anxiety=true, high=true}
Woah… 
It’s so different from a smile. 
I kind of want to hide, or sneak away,
But I’m so attracted to strong emotions! 
//DO slowly swim toward face, as if possessed
DO swimTo {target=$player, speed=fast}
DO emote {type=bodySnatched}
Like a moth to a flame… 
They just taste so satisfying.

CHAT CORE_SeeJoy_1 {stage=CORE, type=seeEmo, length=medium, worldOnly=true, worldJoy=true, joy=true, excitement=true, ennui=true, surprise=true, anxiety=true, high=true}
Wow, what a rush! 
I feel this warm sensation traveling through my whole body rn
I’m just remembering the sensation of my last meal of this kind.
So warm, so light.
DO emote {type=bigSmile}
So good, 
DO emote {type=sigh}
yet so sad. 
So fleeting. Like you know it will end before you begin. 
Is this what all the good things in your world are like? 
How can you stand it? 
Gimmmmme more.  

CHAT CORE_SeeJoy_2 {stage=CORE, type=seeEmo, length=short, worldOnly=true, worldJoy=true, high=true, anxiety=true, excitement=true, joy=true}
//DO dashes around between faces
DO swimTo {target=$worldFace, speed=fast}
DO swimTo {target=$player, speed=fast}
Weeeeeeeeeeeeeeeeeeee!
//DO dashes around some more
DO zoomies
Weeeeeeeeeeeeeeeeeeee!
Such exuberance is contagious!
DO twirl
//DO fin swishes
DO dance
[flamenco dancer emoji]

CHAT CORE_SeeSadness_1 {stage=CORE, type=seeEmo, length=medium, worldOnly=true, worldSad=true, sadness=true, nostalgia=true, curiosity=true, ennui=true}
//DO swim amongst faces/face, inspecting them. 
DO swimTo {target=strongestEmotion}
DO swimTo {target=$player, speed=slow}
Hmmmm....I see. 
So that’s what sadness is: 
Downcast eyes [down arrow emoji]
//DO swims down the screen
DO lookAt {target=bottom}
A slight downward flare in the nose. [down arrow emoji] 
//DO swims further down
DO swimTo {target=screenBottom}
The lower lip slightly jutted out [down arrow emoji]
WAIT 1.0
Kind of a general downness. 
WAIT 1.0
How mellow.
WAIT 1.0 
How heavy. 

CHAT CORE_SeeSadness_2 {stage=CORE, type=seeEmo, length=short, worldOnly=true, worldSad=true, joy=true, surprise=true, high=true, sadness=true}
Whoa, someone’s having a hard day. 
WAIT 1.0
//DO swim to sad face
DO swimTo {target=$worldFace}
Let’s give them all our hugzzzzz 

CHAT CORE_SeeSurprise_1 {stage=CORE, type=seeEmo, length=medium, worldOnly=true, worldSurprise=true, surprise=true, curiosity=true, joy=true, excitement=true}
Woah, this emotion just catches you off guard
Every. Single. Time. 
Like one moment
//EMOTE {style = nonchalance}  
DO emote {type=meh, time =3}
La la la la 
Just doing my thing
The next moment: 
DO emote {type=surprise}
[clown emoji]! 
WAIT 1.0
I like it when life throws you something different 
(the good kind of different, that is) 

CHAT CORE_SeeSurprise_2 {stage=CORE, type=seeEmo, length=short, worldOnly=true, worldSurprise=true, surprise=true, high=true, joy=true, excitement=true}
[confetti emoji][confetti emoji][confetti emoji]
Hooray!
DO twirl
DO emote {type=heartEyes}
I [heart emoji] surprised faces [heart face emoji]!!!!!1 

CHAT CORE_SeeFearorWorry_1 {stage=CORE, type=seeEmo, length=medium, worldOnly=true, worldFear=true, anger=true, sadness=true, anxiety=true, high=true}
DO swimTo {target=$worldFace}
Uhh.. this person seems stressed. 
What’s their deal? 
Don’t get me wrong, 
I want to eat that STAT 
But maybe you should help them out? 

CHAT CORE_SeeFearorWorry_2 {stage=CORE, type=seeEmo, length=short, worldOnly=true, worldFear=true, high=true, joy=true, excitement=true}
Never fear, Guppy’s here! 
Feed me all your worries! 
DO emote {type=licklips}


CHAT CORE_SeeAmuseorJoy_1 {stage=CORE, type=seeEmo, length=medium, worldOnly=true, worldAmuse=true, worldJoy=true, high=true, surprise=true, excitement=true, curiosity=true}
DO swimTo {target=$worldFace}
Hey! 
The eyebrows are floating! 
They’re so high, they’re lifting off of the face 
[airplane emoji]
DO emote {type=laugh}

 
CHAT CORE_SeeAmuseorJoy_2 {stage=CORE, type=seeEmo, length=medium, worldOnly=true, worldAmuse=true, worldJoy=true, anger=true, sadness=true, joy=true, surprise=true}
Such brightness! [lightbulb emoji]
//DO shields eyes
DO emote {type=lightning}
This must be what babies see when they’re born? 
[baby emoji]

CHAT CORE_SeeDisgust_1 {stage=CORE, type=seeEmo, length=medium, worldOnly=true, worldDisgust=true, anger=true, anxiety=true, curiosity=true, ennui=true}
Wow, that’s quite an expression. 
(maybe not the most flattering)
But it’s one of my favorites
For its complexity.  
Like there’s a very compelling story behind it. 
That gets at core of who we are. 
//DO taps chest
DO emote {type=determined}
WAIT 1.0
DO twirl 
I love stories! 

CHAT CORE_SeeDisgust_2 {stage=CORE, type=seeEmo, length=short, worldOnly=true, worldDisgust=true, anger=true, curiosity=true, surprise=true}
DO swimTo {target=$worldFace}
Here is someone clearly demonstrating the extensive range of the face
DO emote {type=clapping}
[100 emoji][100 emoji][100 emoji]

CHAT CORE_SeeMysteryMeat_1 {stage=CORE, type=seeEmo, length=medium, worldOnly=true, worldMystery=true, curiosity=true, surprise=true, excitement=true, joy=true}
What is that?
DO emote {type=catnip, time=6}
I think it might be the most beautiful thing I’ve ever seen.
I’ve searched through my entire memory bank
And I can’t decipher it at all.  
[starry night sky emoji]
I’ll just have to taste it.
WAIT 2.0
SAY OMG NEW FLAVORS
DO emote {type=drool}
SAY I CAN’T WAIT!  

CHAT CORE_SeeMysteryMeat_2 {stage=CORE, type=seeEmo, length=short, worldOnly=true, worldMystery=true, high=true, joy=true, anger=true, surprise=true, excitement=true, anxiety=true}
[rainbow emoji]
The colors, the colors! 
//is mystery meat very colorful? 


//CAPTURE REQUEST
//(emo specific 4 short, 4 medium}

CHAT CORE_CapReq_Anger {stage=CORE, type=capReq, length=short, worldOnly=true, worldAnger=true, anxiety=true, excitement=true, joy=true}
Gimme some of that short-fuse-
Explode-any-moment
Tastiness! 
DO emote {type=feedMe}


CHAT CORE_CapReq_Joy {stage=CORE, type=capReq, length=short, worldOnly=true, worldJoy=true, joy=true, high=true, curiosity=true}
Oh my, do indulge me with that rich, hearty, feel-good meal of emotions,  
Will ya?  
DO emote {type=lickLips}


CHAT CORE_CapReq_Sadness {stage=CORE, type=capReq, length=short, worldOnly=true, worldSad=true, curiosity=true, excitement=true, sadness=true}
I want some of that heavy stuff. 
The waterworks. 
I can handle it! 
DO emote {type=feedMe}
I’ve made lots of room in my belly.



CHAT CORE_CapReq_Surprise {stage=CORE, type=capReq, length=short, worldOnly=true, worldSurprise=true, surprise=true, joy=true, excitement=true}
Gimme that burst of shock!
DO emote {type=lickLips}
Quick, before it’s gone!  

CHAT CORE_CapReq_FearorWorry {stage=CORE, type=capReq, length=short, worldOnly=true, worldFear=true, anger=true, ennui=true, high=true, excitement=true}
Just my luck! 
I need my daily dose of anxiety to function!  
And there it is, right before my eyes. 
DO emote {type=feedMe}
Gimme gimme! 



CHAT CORE_CapReq_AmuseorJoy {stage=CORE, type=capReq, length=short, worldOnly=true, worldJoy=true, anger=true, excitement=true, anxiety=true, curiosity=true, sadness=true}
Oh yea. Go after that! 
I’ve always wanted to experience an amusement park. 
It’s like the same thing, right? 

CHAT CORE_CapReq_Disgust {stage=CORE, type=capReq, length=medium, worldOnly=true, worldDisgust=true, excitement=true, joy=true, surprise=true, high=true}
Oh, heeeeey. 
Nice find!  
I want that unbearably sour goodness. 
[that sour face emoji]
That expired food in your refrigerator goodness.
DO emote {type=lickLips}
That colony of roaches in your bright white kitchen sink goodness.

CHAT CORE_CapReq_MysteryMeat {stage=CORE, type=capReq, length=medium, worldOnly=true, worldMystery=true, high=true, anxiety=true, curiosity=true, nostalgia=true}
Well, that’s...unusual.
But so mesmerizing…
//DO go into a daze  
DO emote {type=catnip}
Like a big ol kick in the mouth. 
DO swimTo {target=$player}
Like nothing I’ve ever had before. 
WAIT 1.0
All I know is that it’s gonna be
SAY INTENSE {style = big, speed = fast}
And 
SAY ODD {style = big, speed = fast} 
And… 
SAY UTTERLY INCOMPARABLE {style = big, speed = fast} 
[smug emoji]
//DO rub fins together
DO emote {type=rubTummy}
Capture it for me! 

//CAPTURE SUCCESS
//(non-emo specific 5 short) 

CHAT CORE_CapSuc_5 {stage=CORE, type=capSuc, length=short, worldOnly=true, surprise=true, excitement=true, joy=true}
What an interesting flavor profile! 
Not at all what I expected!

CHAT CORE_CapSuc_6 {stage=CORE, type=capSuc, length=short, worldOnly=true, joy=true, excitement=true, high=true}
//DO guppy rubs hands together
DO emote {type=plotting}
This is my favorite part! 

CHAT CORE_CapSuc_7 {stage=CORE, type=capSuc, length=short, worldOnly=true, anger=true, sadness=true, ennui=true, excitement=true}
DO emote {type=surprise}
Woah, what a crazy fortune! 
Is that make-up even possible? 
DO emote {type=awe}
You should show this to your friends! 

CHAT CORE_CapSuc_8 {stage=CORE, type=capSuc, length=short, worldOnly=true, curiosity=true, excitement=true, joy=true, nostalgia=true}
DO emote {type=chinScratch}
Intriguing…  
Very very intriguing… 
WAIT 3.0 
It’s such a beaut! 
You should be proud! 

CHAT CORE_CapSuc_9 {stage=CORE, type=capSuc, length=short, worldOnly=true, excitement=true, surprise=true, joy=true}
This capture is one of a kind! 
Share it with all the world to see! 

//CAPTURE PROGRESS 
//(non-emo specific 3 short, 2 medium) 

CHAT CORE_CapProg_5 {stage=CORE, type=capProg, length=short, worldOnly=true, anxiety=true, excitement=true, surprise=true, joy=true}
Oh, that one’s going to be delicious. 
DO emote {type=bigSmile}
Get it before it goes away! 

CHAT CORE_CapProg_6 {stage=CORE, type=capProg, length=short, worldOnly=true, anger=true, anxiety=true, excitement=true, high=true}
It’s ooze collection time! 

CHAT CORE_CapProg_7 {stage=CORE, type=capProg, length=short, worldOnly=true, anxiety=true, sadness=true, excitement=true, ennui=true, high=true}
Oh, I can’t watch, I can’t watch! 
//DO watch behind fins
DO emote {type=nervousSweat}
DO lookAt {target=away}
Too much at stake. 

CHAT CORE_CapProg_8 {stage=CORE, type=capProg, length=short, worldOnly=true, high=true, anxiety=true, curiosity=true, nostalgia=true, joy=true}
I can’t get over how your world is soooo tasty. 
Just look at all the ooze it makes. 
The bright colors, the flavors.
I’m going to eat very well tonight.  
DO emote {type=drool}
Everything looks so good! 

CHAT CORE_CapProg_9 {stage=CORE, type=capProg, length=short, worldOnly=true, joy=true, anger=true, sadness=true, surprise=true}
You’re the best emotion catcher I’ve ever met. 
DO emote {type=heartEyes}


//LINK: remaining chat minimum, length and emotion suggestions coming soon;)

//reminders= BETA:
//reminders and points that need to be included in new chats in blue
//comments for changes to existing chats in yellow/on side

//A. CONTENT 

//B. EMOTIONS
//Chats in each bin/type need to cover a range of Guppy states (see meta tag emotions //at top) while staying true to this moment of the plot. Guppy states can be---ANGER, //SADNESS, SURPRISE, CURIOSITY, ANXIETY, NOSTALGIA, ENNUI, EXCITEMENT, //HIGH, JOY *see //link above for your specific emotion recommendations for each bin/type

//C. OBJECTS
//At least 1/3 of chats in this stage should interweave Guppy asking to see general or specific //objects in list.

//Tank Shaken (User shakes phone, which is ultimately Guppy’s tank)

CHAT MS3_Shake_short_1 {type=shake, stage=S3, length=short, anger=true, surprise=true, anxiety=true, excitement=true}
DO lookAt {target=$player, time=1.0}
DO emote {type=fear, time=2.0}
Woaah hey watch it! Be careful with guppy!

CHAT MS3_Shake_short_2 {type=shake, stage=S3, length=short, excitement=true, }
DO emote {type=laugh}
Haha woaaaah! Shake shake shake!

CHAT MS3_Shake_1  {stage=S3, type=shake, length=short, anger=true, anxiety=true, curiosity=true, joy=true, surprise=true}
Woah!
Hey! {style = loud, speed = fast}
DO emote {type=frown, time=1.0}
That too much!
DO swimTo {target=$player, style=direct}
Why shakes me??
something wrong?
u ok?
deep breaths!! {style = tremble}
Find ur calm place. Full of calm colors
DO emote {type=meditate, time=4.0}
Seafoam turquoise {speed=slow}
Deep harmonic indigo {speed=slow}
Warm pulsing red {speed=slow}
Cheeseburger yellow {speed=slow}
DO emote {type=bigSmile}
SAY YUM! {style = loud}


CHAT MS3_Shake_2 {stage=S3, type=shake, length=medium, anger=true, anxiety=true, curiosity=true, joy=true, surprise=true}
Wwwwoooah! Careful!
DO swimTo {target=$player, style=direct}
Now everythings out of wack!
Hey come on! It takes long times to figure out where stuff goes!
DO swimTo {target=$anyObject, style=meander}
Hmm hmm HMMMM
DO lookAt {target=$player, time=3.0}
Actually tho, kinda liking this!
Wow yeah, sort of liek a…
DO emote {type = meh, time =2.0}
"I don't care, I leave my $object anywhere" cool guppy vibe.
DO emote {type = bigSmile, time =4.0}
Yeah! Good! Yeah!
Thanks!!



//Tank Tapped
CHAT MS3_Tap_short_1 {stage=S3, type=tap, length=short}
DO lookAt {target=$player, time=3.0}
Oh! Yes? What?

CHAT MS3_Tap_short_2 {stage=S3, type=tap, length=short}
DO lookAt {target=$player, time=3.0}
Hey I see u! What's up! Tap!

CHAT MS3_Tap_1 {stage=S3, type=tap, length=short, high=true, joy=true, nostalgia=true, surprise=true, anxiety=true}
DO emote {type=surprise}
Oh! Haha hey!
DO swimTo {target=$player, style=direct}
Its u again! Should maybe be getting door bell!
DO emote {type=flapFinLeft}
It could make sonorous bell noises…
DO emote {type=flapFinRight}
Or red alert scary noises…
WAIT 1.0
Or... samba noises!
DO dance {time=5.0}

CHAT MS3_Tap_2 {stage=S3, type=tap, length=medium, high=true, joy=true, nostalgia=true, surprise=true, anxiety=true, branching=true}
Woah!
DO swimTo {target=$player, style=direct}
Ur touch! Its too powerful! This glass betwene us…
DO nudge {target=glass, times=1}
Now u put ur finger on guppy…
WAIT 2.0
DO emote {type=lightning}
//WAITFORANIMATION
DO emote {type = surprise, time =2.0}
//WAITFORANIMATION
DO emote {type = dizzy, time =2.0}
ASK Did u just feel that??? Was it just me or did??
OPT I felt it too! Like a sort of...vibration… #MS3_Tap_2_vibration
OPT What? I didn't feel anything. #MS3_Tap_2_no

CHAT MS3_Tap_2_vibration {noStart=true}
DO emote {type=awe, time=2.0}
I knew it! We have *special guppy connection*!!
DO twirl {time=2.0}
I mean besides where I eat ur mood clouds.
DO emote {type = wink}

CHAT MS3_Tap_2_no {noStart=true}
DO emote {type=snap}
Whaaat? Needing u to get in touch with ur inner guppy!
I felt the connection! Something is there!
Maybe in past life…
DO emote {type = dizzy, time =2.0}
...ur Guppy????? {style=whisper}
//Tank Status / Critiques
CHAT MS3_Critic_1 {stage=S3, type=critic, length=short, high=true, joy=true, nostalgia=true, surprise=true, anxiety=true, curiosity=true}
DO swimAround {target=center, loops=3, speed=slow}
Thinking myself, theres so much stuff out there…
DO swimTo {target=$player, style=meander}
But not a lot of stuff in here.
DO lookAt {target=$player, time=2.0}
I should have stuff too! Like…
DO lookAt {target=tBotBackRight, time=1.0}
DO emote {type=flapFinRight, time=1.0}
Throw pillows!
DO lookAt {target=tBotBackLeft, time=1.0}
DO emote {type=flapFinLeft, time=1.0}
Abstract arts!
DO lookAt {target=$player, time=2.0}
DO emote {type = bigSmile, time =2.0}
Spatulas!!!

CHAT MS3_Critic_2 {stage=S3, type=critic, length=short, high=true, joy=true, nostalgia=true, surprise=true, anxiety=true, curiosity=true}
U know, looking at fish tanks while ur gone.
DO swimAround {target=center, loops=2, speed=slow}
This ones kinda...empty? Dontcha think?
DO swimTo {target=$player, style=direct}
I saw this where it had a...a…
Treasure chest burping bubbles {speed=fast}
Guppy dude wiht some kind of giant fork {speed=fast}
Pink octopus wiht wavy fin things aaa! {speed=fast}
DO emote {type=awe, time=2.0}
We shuold get some of that stuff!! It'd be so cool!!
//Player Emotes Strongly At Guppy
CHAT MS3_StrongEmote_Joy_1 {stage=S3, type=tankResp, length=short, curiosity=true, high=true, joy=true, surprise=true, playerJoy=true}
DO emote {type=bigSmile, time=2.0}
DO lookAt {target=$player, time=2.0}
Hee yes happy feels! I like them! Like ur face!
DO nudge {target=glass, times=1}

CHAT MS3_StrongEmote_Joy_2 {stage=S3, type=tankResp, length=short, curiosity=true, high=true, joy=true, surprise=true, playerJoy=true}
DO emote {type=smile, time=2.0}
DO lookAt {target=$player, time=2.0}
What is it??? Oh!! Im happy ur happy tho! Yes!

CHAT MS3_StrongEmote_Anger_1 {stage=S3, type=tankResp, length=short, curiosity=true, high=true, joy=true, surprise=true, sadness=true, playerAnger=true}
DO emote {type=singleTear, time=2.0}
DO lookAt {target=$player, time=2.0}
Don't be mad at guppy. Can't help it prolly. Plz stop.

CHAT MS3_StrongEmote_Anger_2 {stage=S3, type=tankResp, length=short, curiosity=true, high=true, joy=true, surprise=true, playerAnger=true}
DO emote {type=laugh}
DO lookAt {target=$player, time=2.0}
U kinda make a funny face shape when u anger
Sorry sorry shouldnt laugh
DO emote {type=kneeSlap}

CHAT MS3_StrongEmote_Sadness_1 {stage=S3, type=tankResp, length=short, curiosity=true, high=true, joy=true, surprise=true, playerSadness=true}
DO emote {type=singleTear, time=2.0}
DO lookAt {target=$player, time=2.0}
Why sadden? Why longer face? Guppy sorry.

CHAT MS3_StrongEmote_Sadness_2 {stage=S3, type=tankResp, length=short, curiosity=true, high=true, joy=true, surprise=true, playerSadness=true}
DO emote {type=surprise, time=2.0}
DO lookAt {target=$player, time=2.0}
Oh! Wait waht?? Why sad? Dont be!
DO nudge {target=glass, times=1}
Guppy knows. Its ok. {style = whisper}

CHAT MS3_StrongEmote_Surprise_1 {stage=S3, type=tankResp, length=short, curiosity=true, high=true, joy=true, surprise=true, playerSuprise=true}
DO emote {type=surprise, time=1.0}
DO lookAt {target=$player, time=2.0}
Wow! Me too! What was that?!

CHAT MS3_StrongEmote_Surprise_2 {stage=S3, type=tankResp, length=short, curiosity=true, high=true, joy=true, surprise=true, anger=true, playerSuprise=true}
DO lookAt {target=tSurface, time=2.0}
DO emote {type=anger, time=2.0}
SAY R U makeing faces at me behind my back????

//Fish

//Hit With an Object

CHAT MS3_ObjHit_1 {stage=S3, type=hit, length=short, surprise=true, anger=true, excitement=true, joy=true}
DO emote {type=dizzy, time=2.0}
Bonk! Waht! Woah!

CHAT MS3_ObjHit_2 {stage=S3, type=hit, length=short, surprise=true, excitement=true, joy=true, curiosity=true}
DO turnTo {target=$player, time=1.0}
DO emote {type=laugh}
Haha wonk! woah! watch out!!

CHAT MS3_ObjHit_3 {stage=S3, type=hit, length=short, anger=true, anxiety=true, ennui=true, curiosity=true, surprise=true, excitement=true, sadness=true}
Ow! Woah!
DO swimTo {target=$newestObject, speed=slow, style=direct}
What this is? Some kinds of colander? A...blender?
DO lookAt {target=$player, time=2.0}
Maybe a juicer?
DO lookAt {target=$newestObject, time=2.0}
But like...very $object-y sort of juicer…maybe for juicing…$object?
DO lookAt {target=$player, time=2.0}
DO emote {type = bigSmile}
U are what u juice!


CHAT MS3_ObjHit_4 {stage=S3, type=hit, length=short, branching=true, anger=true, anxiety=true, curiosity=true, surprise=true, excitement=true, joy=true}
Hey! Watch out!
DO lookAt {target= $newestObject, time=2.0}
Oh!
Oooooh!
DO swimTo {target=$newestObject, style=direct}
Oh wow! Look at this! It's like a...a...
DO lookAt {target=$player, time=2.0}
ASK $object??
OPT Yeah! #MS3_ObjHit_2_yes
OPT No, it's not $object, it's a mustache #MS3_ObjHit_2_mustache

CHAT MS3_ObjHit_5_yes {noStart=true}
DO swimAround {target=center, times=2, speed=fast}
Woooooow I knew it!
//WAITFORANIMATION
DO turnTo {target=$player, time=2.0}
Its for me??? U shouldn't have!!
Knowing just the place to put it...
DO nudge {target= $newestObject, times=5}
DO lookAt {target=$player, time=2.0}
Perfect! Makes the room feel...nice!
Aligned!
Perplendixular!

CHAT MS3_ObjHit_6_mustache {noStart=true}
Really??
DO nudge {target= $newestObject, times=2}
DO lookAt {target=$player, time=2.0}
Humans grow these on thier faces???
DO turnTo {target=$newestObject, time=3.0}
WAIT 3.0
DO emote {type=chinScratch, time=2.0}
Humans wierd...
//Poked By Player
CHAT MS3_Poke_1 {stage=S3, type=poke, length=short, surprise=true, excitement=true, anxiety=true, joy=true, anger=true, high=true}
Waoah-urp!
DO poop {target=$currentLocation, amount=small}
DO lookAt {target=$player, time=6.0}
DO emote {type = awkward, time =4.0}
WAIT 2.0
This never happned...ok? Plz clean up. {style=whisper}


CHAT MS3_Poke_2 {stage=S3, type=poke, length=medium, joy=true, excitement=true, curiosity=true, surprise=true, ennui=true, high=true}
DO emote {type=laugh}
DO lookAt {target=tBotBackRight, time=2.0}
Hmm what was that? Maybe it was here?
DO lookAt {target=tBotBackLeft, time=2.0}
//WAITFORANIMATION
DO emote {type=laugh}
Maybe over here???
DO emote {type=laugh}
DO lookAt {target=$player, time=2.0}
DO emote {type = bigSmile, time =2.0}
Oh hey waaaaaiit. It was u!!
DO emote {type=kneeSlap}
//WAITFORANIMATION
DO emote {type = wink}
(Guppy knew it was u)
//Hungry
CHAT MS3_Hungry_1 {stage=S3, type=hungry, length=medium, anxiety=true, curiosity=true, high=true, joy=true, surprise=true}
Hey! Lets play a game. 
Im to think of something, and u guess!
Ready?
WAIT 2.0
DO zoomies {time=2.0}
Ok, lets say that means yes! Here goes…
Im thinkign
of somethings that rhymes
with flake! {speed=fast}
DO emote {type = laugh}
Omg no wait it was supposed to flake ok wait {speed=fast}
Something that rhymes with…
with…
DO emote {type = thinking, time =2.0}
Fffffblake?
DO swimAround {target=center, speed=fast, loops=3}
Omg I cant think when hungry I need flaaaaaaakes! {speed=fast}


CHAT MS3_Hungry_2 {stage=S3, type=hungry, length=medium, anxiety=true, curiosity=true, high=true, joy=true, surprise=true, branching=true}
Hey blob-friend! Im hungry!
ASK Whats on the menu today?
OPT Flakes #MS3_Hungry_2_flakes
OPT Flakes #MS3_Hungry_2_flakes

CHAT MS3_Hungry_2_flakes {noStart=true}
DO emote {type = surprise, time =2.0}
Omg my favorite!! How did u know??
I was just thinking earlier…
DO emote {type = chinScratch, time =2.0}
"what should I eat today?"
Steak?
Baked Alaskas?
Lobsters with crinklie bits?
Or….
SAY FLAAKES?? {style=loud}
WAIT 2.0
Guess which one I picked...
DO emote {type = wink}
//Eating Responses
CHAT MS3_EatResp_1 {stage=S3, type=eatResp, length=short, anxiety=true, curiosity=true, high=true, joy=true, surprise=true}
DO emote {type=chewing, time=5.0}
Oh! wow! Been a while since tasting this!
Kinda like a hint of
eye crinkly smile fond {style=fast}
With a dash of
frowny sad disappointment blue {style=fast}
Ur head makes such good clouds!
I knew it was a good idea to come find u!

CHAT MS3_EatResp_2 {stage=S3, type=eatResp, length=long, anxiety=true, curiosity=true, high=true, joy=true, surprise=true, branching=true}
On second thought, maybe I shouldn't eat all the flaKes all the time...
DO swimTo {target=$player, style=direct}
DO emote {type=worried, time=2.0}
Do u have enough? Enough feelings?
ASK I'm not...taking them away am I? When eating them?
OPT No #MS3_EatResp_2_no
OPT Yes #MS3_EatResp_2_yes

CHAT MS3_EatResp_2_no {noStart=true}
Oh ok...whew thats a releif!
DO lookAt {target=$food, time=2.0}
Because these flakes are SO GOOD!!
// DO eat $food

CHAT MS3_EatResp_2_yes {noStart=true}
DO emote {type = surprise, time =2.0}
Omg! Thats terrible! I'll stop eating then!
DO swimTo {target=tTopBackRight, speed=fast, style=direct}
WAIT 2.0
They just taste so good tho…
DO lookAt {target=$player, time=2.0}
Staring at me…
DO swimTo {target=tTopBackLeft, speed=slow, style=direct}
WAIT 2.0
DO lookAt {target=$player, time=2.0}
I mean...u already flaked them…
WAIT 1.0
... if I *dont* eat it flakes go bad, right??
DO swimTo {target=$food, speed=slow, style=meander}
They smell so good tho
DO inflate {amount=huge, time=2.0}
DO emote {type = angry, time =2.0}
SAY I CANT TAKE IT ANYMORE {style=loud}
SAY NEeD MORE FlAKeS!
//DO eat $food
//omg its so good its so worth it
//Has To Poop
CHAT MS3_Poop_1 {stage=S3, type=poop, length=medium, anxiety=true, curiosity=true, high=true, joy=true, surprise=true, sadness=true}
Whops! About that time again…
DO swimTo {target=poopCorner, style=direct}
DO lookAt {target=$player, time=2.0}
U ever think "wow, I poop a lot"?
Or "wow, I don't poop a lot?"
Like…
DO emote {type=chinScratch, time=3.0}
how much is too much?
how much is too little?
how are we supposed to *know*??
DO emote {type=nervousSweat, time=3.0}
What if something goes wrong??
What if its
DO emote {type =fear, time =4.0}
Always been wrong???

//Pooped
CHAT MS3_Pooped_1 {stage=S3, type=pooped, length=short, anxiety=true, curiosity=true, high=true, joy=true, surprise=true, sadness=true}
Whew! Thats better.
DO swimTo {target=$player, style=direct}
Talk about emotional baggage!
DO emote {type=laugh, time=2.0}
//Emotion Capture- HOLD

//Emotion-specific Responses (per emotion)
//(currently on hold)
//Capture Requests
CHAT MS3_CapReq_1 {type=capReq, stage =S3, length = medium}
//Hey! I saw...we're running low on $lowEmotion
And it feels like it's been so long since I tasted it!
Maybe you could make some?
DO press face against glass
I can see the color, right there…
...delicious…
DO emote {type = happy, time =2.0}
//Let's flake some so I can be $lowEmotion too!!

CHAT CORE_CapReq_4 {type=capReq, stage =S3, length = medium}
Ooo! Wait!
DO nudge {target=screenCenter}
You have so many colors right now!!
Wow!!
What's going on in that head of yours??
WAIT 2.0
Sorry I was distracted by colors. So many...hey!
DO emote {type = happy, time =2.0}
You should turn them into flakes! Yes!
//DO look at $flakeMeters
And we are running kind of low?
Let's do it! Let's….
DO twirl
Flaaaaaaake!
Capture Success
CHAT MS3_CapSuc_1{type=capSuc, stage =S3, length = medium}
DO emote {type = surprise, time =2.0}
Oh wow! Look at all those flakes!
Amazing! Thank you!
I can't wait to eat them all up…
DO swimTo {target=$player}
Maybe I can try one now?
Not a lot, just a taste…
They're so good when they're fresh!
Like how anger dulls over time,
but right when you feel it, it's all bright and sharp and spiky...
Like that!
DO emote {type = happy, time =2.0}

CHAT MS3_CapSuc_2 {type=capSuc, stage =S3, length = medium}
Boy they were right about you, you're a regular flake factory!
I was all like "that blob? That one there?"
And they were like "yeah just look at them!"
And then I did
And I was like
DO emote {type=awe}
wooooooowww {style=whisper}
Not just the color clouds, the feels like
You just have the skills
The flakiness
That's rare! That's special! That's
DO twirl
Yoouu!
Capture in Progress
CHAT MS3_CapProg_1 {type=capProg, stage =S3, length = short}
DO lookAt {target=$player, time=2.0}
Your face is so close! In charge! Maybe move it a bit into the lines?
WAIT 2.0
Yeah! That's it! Beautiful!
DO emote {type = happy, time =2.0}
I like looking at your face. It's so faceful! So...
...flakeful!


CHAT MS3_CapProg_2  {type=capProg, stage =S3, length = medium}
Yep, yep. Just look at those flakes pour in!
So much better than synth-flakes!
(they used to feed us those)
We'd be like "whaaaat???" this isn't happy!
This isn't lonely!
This isn't melancholic wistful surprise!
It's like...guhhhh?
DO emote {type=wave}
All pale ozone cloud feels with bitter static
DO emote {type=nodding}
Flakes...more like FAKES amirite??
DO emote {type=laugh}
That was a good one!
Capture Failure
CHAT MS3_CapFail_1  {type=capFail, stage =S3, length = medium}
DO emote {type=laugh, time=2}
Sorry, sorry I know I shouldn't…
...but your face! Just now!
It's like you're trying so hard like
DO emote {type = smile, time =.5}
DO emote {type = frown, time =0.5, immediate=false}
DO emote {type = angry, time =0.5, immediate=false}
But nothing happens!
DO emote {type=laugh, time=2}
Ok ok I'm sorry. Try again! You got this!
DO emote {type=laugh, time=1}
No keep going don't mind me!
DO emote {type=laugh, time=1}

CHAT MS3_CapFail_2 {type=capFail, stage =S3, length = medium}
Hmmm...what if Guppy gave you some constructive feedback?
Your eyes...they should be more…
DO emote {type=wave}
Wavey...and your mouth it should be like
DO emote {type=flapFinRight, time=.5}
DO emote {type=flapFinLeft, time=.5, immediate=false}
melon-y...but like a car horn...does this make any sense??
And your ears, maybe if you
kinda {style=fast}
Flap them a little???
Can you do that?
DO emote {type=flapFinRight, time=.5}
DO emote {type=flapFinLeft, time=.5, immediate=false}
I mean I don't have them so I don't know
Actually at first I thought they were your fins????
DO emote {type=laugh}
I mean don't they look like fins??? A little????
DO emote {type=laugh}
Ok ok sorry try again try again...
//General

//Hellos
CHAT MS3_Greet_1 {stage=S3, type=hello, length=long, anxiety=true, curiosity=true, high=true, joy=true, surprise=true, branching=true}
DO emote {type=bigSmile}
Heeeeeey! It's u!
DO twirl {time = 2.0}
My blob! My favorit blob! the Flake-Meister!
ASK Have u seen any traffic cones out there?
OPT Oh yeah tons! #MS3_Greet_1_cones
OPT What no #MS3_Greet_1_noCones

CHAT MS3_Greet_1_cones {noStart = true}
DO emote {type=awe, time=2.0}
Ohh cooool! Thats so cool. They're all like
Stripey and
Bright
Like this parasite I saw...it pushes itself
Up behind snail eyes like
DO emote {type = bulgeEyes, time =2.0}
Blorp blorp blorp!
And the snails like 
DO twirl {time=1.0}
DO emote {type=dizzy, time=2.0}
Woaaoaoah
And then a birds like
DO emote {type=evilSmile, time=2.0}
"Oh hey there u look good!"
DO inflate {amount=huge, time=1.0}
SAY NOMMMFF! {style=loud}
And the parasite is like
DO emote {type=heartEyes, time=2.0}
"Yaaaaaaaay!"
DO emote {type = smile, time =6.0}
DO lookAt {target=$player, time=6.0}
I saw it in a video! Isn’t that cool???
WAIT 2.0
Anyway I hope ur having a good day!

CHAT MS3_Greet_1_noCones {noStart=true}
DO emote {type=surprise, time=2.0}
Whaaaaaat! But u can like
See them whenever u want!
U can be like
DO emote {type=smirk, time=2.0}
"Today, it is cone day"
And then walking out in ur blob world like
DO swimTo {target=tBotBackRight, style=meander}
"Ahh yes, that is a fine cone"
Or
DO swimTo {target=tBotBackLeft, style=meander}
"this one not quite so good"
DO lookAt {target=$player, time=2.0}
DO emote {type = singleTear, time =2.0}
I cant have cone days…
DO wait 1.0
But I can have
DO emote {type = heartEyes, time =2.0}
Flake daaaaaaays! {style = loud}

CHAT MS3_Greet_2  {type=hello, stage=S3, length=medium, anxiety=true, curiosity=true, high=true, joy=true, surprise=true, sadness=true, branching=true}
DO lookAt {target=$player, time=2.0}
Oh! Hey! Its u!
Or a perfectly copy of u thats just as colorful and tasty!
ASK Is it u, or copy-u?
OPT It me! #MS3_Greet_2_me
OPT No it copy me! #MS3_Greet_2_copy
CHAT MS3_Greet_2_me{type=hello, stage=S3}
DO emote {type=chinScratch, time=2.0}
Hmmm u *sure* though?
What if u went to sleep, woke up,
And were like
DO emote {type = surprise, time =1.0}
Zzzzznnnork wha? Wha?
And u were just a copy??
How would u know?
WAIT 2.0
I guess it doesn't matter. Ur still flakey!
DO emote {type = bigSmile, time =2.0}
And ur still my friend!
CHAT MS3_Greet_2_copy {type=hello, stage=S3}
DO emote {type=surprise, time=1.0}
Woah! How cool! Does that happening often?
DO emote {type=worried, time=3.0}
Wait never mind! dont answer that!
Its actually scaring a little.
SAY Like: what if copy-u doesn't like Guppy?
DO nudge {target=glass, times=1}
WAIT 2.0
DO emote {type = bigSmile, time =4.0}
No I can tell u like me haha yaaaaay!

//Return After Having Not Played
CHAT MS3_Return_1  {stage=S3, type=return, length=medium, anxiety=true, curiosity=true, sadness=true, nostalgia=true, surprise=true}
DO turnTo {target=tTopBackLeft, time=1.0}
DO emote {type = crying, time =2.0}
Never coming back…
Just u and me now, Guppy-do…
DO lookAt {target=$player, time=2.0}
DO emote {type = surprise, time =1.0}
Oh my gosh! Oh my gosh it's u!
U came back!!!!
DO emote {type = bigSmile, time =4.0}
I'm so glaaaaaaad!
DO twirl {time=2.0}
WAIT 2.0
Don't worry about Guppy-do…
Shes my imaginary friend I talk to when Im by myselfs
But shes not nearly as colorful and blobular and flaketacular as u!
DO turnTo {target=tTopBackLeft, time=2.0}
No offense, Guppy-do
DO emote {type = surprise, time =3.0}
DO lookAt {target=$player, time=2.0}
I cant believe she just said that!
DO emote {type=laugh}
Thats so bad! U don't look like a pineapple at all!

CHAT MS3_Return_2 {stage=S3, type=return, length=medium, anxiety=true, curiosity=true, joy=true, nostalgia=true, surprise=true, excitement=true}
DO lookAt {target=$player, time=2.0}
DO emote {type = surprise, time =1.0}
You got my psychic message!!!
I was sitting here in the dark
The water slowly cooling
Everythign quieting, stilling
And I was liek
DO emote {type=plotting, time=2.0}
"I will use my mysteriuos mind powers to tell u to come back!"
And at first I was liek
DO emote {type=eyeRoll, time=2.0}
"Guppy, u don't have mysteriuos mind powers"
But
SAY BUT
DO emote {type=awe, time=2.0}
Here u r?!?!?!
I promise to use my mysteriuos powers for good!!!
WAIT 2.0
Mostly!!!!!
DO emote {type = evilSmile, time =2.0}
//Random Conversation
CHAT MS3_Muse_1 {stage=S3, type=rand, length=medium, anxiety=true, curiosity=true, joy=true, nostalgia=true, surprise=true, branching=true}
DO lookAt {target=$player, time=2.0}
Guppy curious…
U have school? U have pod?
ASK Wait...whats call it, groups of humans?
OPT Conflagration #MS3_Muse_1_2
OPT Opulence #MS3_Muse_1_2

CHAT MS3_Muse_1_2 {noStart=true}
ASK yes but so anyway, u have a groups?
OPT Yes #MS3_Muse_1_yes
OPT No #MS3_Muse_1_no

CHAT MS3_Muse_1_yes {noStart=true}
DO emote {type=smile, time=2.0}
Me too!!! Back in the big tanks. But was different. 
GO MS3_Muse_1_2_finish

CHAT MS3_Muse_1_no {noStart=true}
DO emote {type=singleTear}
That's so sad! Awww!
Guppy doesn't have school either, sadness share!
GO MS3_Muse_1_2_finish

CHAT MS3_Muse_1_2_finish {noStart=true}
DO emote {type=snap}
Tells u what! U and Guppy being each other's school!
DO emote {type = smile, time =2.0}
Now we pod-mates! Tank-mates! School buddies!
DO emote {type = bigSmile, time=2.0}
I like that!! Has good glowy flavor, like sunshine!
DO twirls {time=2.0}
What up, pod-tank-school buddy!!


CHAT MS3_Muse_2 {stage=S3, type=rand, length=long, anxiety=true, curiosity=true, joy=true, nostalgia=true, surprise=true, branching=true}
DO emote {type=awkward, time=2.0}
Maybe this is weird Guppy quesiton but-
ASK U wouldnt ever...eat Guppy would u?
OPT Never! #MS3_Muse_2_no
OPT Maybe… #MS3_Muse_2_maybe

CHAT MS3_Muse_2_no {noStart=true}
DO emote {type=whew}
Whew! Thats good! Guppy was worried…
GO MS3_Muse_2_youd_explode

CHAT MS3_Muse_2_youd_explode {noStart=true}
DO lookAt {target=$player, time=2.0}
//WAITFORANIMATION
DO emote {type=smile, time=2.0}
Becuz if u ate Guppy, u probably will explode!
WAIT 1.0 
DO emote {type = surprise, time =1.0}
No seriuosly! All the emotions, all the flakes
They squish around condensify
They become 
WAIT 0.5
DO emote {type=bodySnatched, time=1.0}
...potent.
WAIT 1.0
Anyway if u tried to eat me u would be like {speed=fast}
DO emote {type = angry, time =0.5}
DO emote {type = crying, time =0.5}
DO emote {type = bouncing, time =.5}
Woaaahahaacrycrycrygrrrrraaaaaugh
Then
DO inflate {amount=extreme, time=1.0}
SAY KABOOOOOOM! {style=loud}
And I dont want that to happen!
Besides...Im the one that eats u!
DO emote {type = smile, time =2.0}
Ur colors, I mean!
CHAT MS3_Muse_maybe {noStart=true}
DO emote {type=worried, time=2.0}
Whaaaaat! Oh no thats terrible?!
DO swimAround {target=center, times=2}
Im so worreid now! Im so distress!
GO MS3_Muse_2_youd_explode



CHAT MS3_Muse_3 {stage=S3, type=rand, length=long, anxiety=true, curiosity=true, joy=true, nostalgia=true, surprise=true, branching=true}
DO lookAt {target=$player, time=2.0}
So...quick question:
How do *u* eat flakes?
Where do u get ur sadness? Ur anger? Ur happyness?
DO emote {type=bouncing, time=3.0}
Do u eat them from pretty pictures? {style=fast}
Do u eat them from long walks on beaches? {style=fast}
Do u eat them from other ppl? {style=fast}
Aaaa this is all so confusing!
So complex!
So flavorful!
Ok...maybe simpler:
ASK Do ur flakes come from outside or inside u?
OPT Outside #MS3_Muse_3_outside
OPT Inside #MS3_Muse_3_inside
CHAT MS3_Muse_3_outside {noStart=true}
DO emote {type=nodding, time=2.0}
Thought so. Mystirious! The way colors imbue.
DO swimAround {target=center, times=2.0}
Stickign everything, sinkign through it...
Like an oil slick, envelopign everything it touches…
WAIT 1.0
DO emote {type = smile, time =2.0}
Making it pretty colors!

CHAT MS3_MUSE_3_inside {noStart=true}
DO emote {type=nodding, time=2.0}
Oh yes. Guppy thought so. So trenscendant! So gnomic!
Maybe theres a secret
DO emote {type = awe, time =1.0}
color cloud flake organ {style=whisper}
That sees things and shivers and sweats and then like
DO twirl {time=2.0}
I have colooooors! Boom!
DO lookAt {target=$player, time=2.0}
DO emote {type = bigSmile, time =2.0}
I like ur shivery sweaty colorful flakey self!

CHAT MS3_Muse_4 {stage=S3, type=rand, length=medium, anxiety=true, curiosity=true, sadness=true, nostalgia=true, surprise=true}
DO emote {type=chinScratch, time=2.0}
Sometimes when thoughts happen...feel holes inside…
DO turnTo {target=$player, time=2.0}
Like when u hoot a bottle and
DO emote {type=smile, time=1.0}
It makes a sound like loss?
Sort of like
DO swimAround {target=center, times=2}
SAY OOoooooOOOOoooOOOoooo
DO emote {type=smile, time=2.0}
Thats how Guppy feels thinking sometimes
A space with which makes a gap
DO emote {type=chinScratch, time=1.0}
A loss
Making a music in hole mind
DO lookAt {target=away, time=4.0}
I wonder if my mind would make music
If it didn't have holes?

//samantha examples in archived doc version//LINK: remaining chat minimum, length and emotion suggestions coming soon;)

//reminders= BETA:
//reminders and points that need to be included in new chats in blue
//comments for changes to existing chats in yellow/on side

//A. CONTENT 

//B. EMOTIONS
//Chats in each bin/type need to cover a range of Guppy states (see meta tag emotions //at top) while staying true to this moment of the plot. Guppy states can be---ANGER, //SADNESS, SURPRISE, CURIOSITY, ANXIETY, NOSTALGIA, ENNUI, EXCITEMENT, //HIGH, JOY *see //link above for your specific emotion recommendations for each bin/type

//C. OBJECTS
//At least 1/3 of chats in this stage should interweave Guppy asking to see general or specific //objects in list.

//Tank Mode
//Tank Shaken (User shakes phone, which is ultimately Guppy’s tank)

CHAT MS2_Shake_1line_1 {stage=S2, type=shake, length=short, excitement=true, surprise=true, joy=true, anger=true, anxiety=true, ennui=true, sadness=true}
Hhheyfv whaat the bigg deal! ssome of us trrying to gguppy heere!

CHAT MS2_Shake_5word_1 {stage=S2, type=shake, length=short, high=true, joy=true, surprise=true, curiosity=true, excitement=true}
DO emote {type=laugh}
Hahaha wheee! Aggain aggain!

CHAT MS2_Shake_1 {stage=S2, type=shake, length=medium, high=true, joy=true, surprise=true, curiosity=true, anxiety=true, sadness=true}
DO guppy vibrates
DO emote {type = surprise}
Wwwwooaaah!
Why, shake! why!
At least do it in rhythm...like
DO swimTo {target=tTopFrontRight, speed = fast, style=direct}
Doot!
//WAITFORANIMATION
DO swimTo {target=tTopFrontLeft, speed = fast, style=direct}
Daat!
//WAITFORANIMATION
DO swimTo {target=tTopFrontRight, speed = fast, style=direct}
Deet!
//WAITFORANIMATION
DO swimTo {target=tTopFrontLeft, speed = fast, style=direct}
Duut!
Try that! With *rhythm*!
WAIT 3.0
DO emote {type = dizzy, time=2.0}
Ok nevermind u should stop. 
Feeling me kinda loopy. 😛 [Face With Tongue]

CHAT MS2_Shake_2 {stage=S2, type=shake, length=short, high=true, joy=true, surprise=true, curiosity=true, anger=true, nostalgia=true, excitement=true}
DO lookAt {target=$player, time=2.0}
Hey!
I feel things shaked lewse
DO vibrate {time=2.0}
Woooaaaah
Colors threw coming r like
DO guppy vibrates
DO emote {type=awe, time=3.0}
Flap flap b1rd wyngz settle {speed = fast}
On fone lines {speed = fast}
sending messajes delicious btween {speed = fast}
lovers friends enemyes {speed = fast}
DO swimTo {target=$player, style=direct}
DO emote {type = surprise, time=2.0}
Wyerd!
//Tank Tapped
CHAT MS2_Tap_1line_1 {stage=S2, type=tap, length=short}
DO lookAt {target = $player, time=2.0}
DO emote {type=angry}
Oo! hey be carful with my glas house! Its fragil!

CHAT MS2_Tap_5word_1 {stage=S2, type=tap, length=short}
DO lookAt {target = $player, time=2.0}
Yes? waht now u do want?

CHAT MS2_Tap_1 {stage=S2, type=tap, length=short, anger=true, anxiety=true, curiosity=true, high=true, joy=true, surprise=true}
DO swimTo {target= $player, speed = slow, style=meander}
Yyyyeeeeessssss?
What's up? Are you pinging me?
DO emote {type = angry, time =2.0}
I'm not a submarine!!!! {style=loud}
DO emote {type = happy, time =2.0}
...at least I don't think I am mayby
😉 [Winking Face]


CHAT MS2_Tap_2 {stage=S2, type=tap, length=short, anger=true, anxiety=true, curiosity=true, high=true, joy=true, surprise=true}
DO lookAt {target = tTopBackLeft, time=2.0}
There! again! waht!
DO swimTo {target=$player, style=direct}
Mysterious ping noise! Like a sonorous
SAY BOOONGH {style = loud}
Did u hear it too?
WAIT 0.5
DO emote {type=laugh}
...heeeeyyyyyy!
DO swimAround {target = center, speed=fast, style=direct}
That was yoooooouuuuuuu! Hahaha!
//Player Emotes Strongly At Guppy
CHAT MS2_StrongEmote_Joy_1 {stage=S2, type=tankResp, length=short, playerJoy=true}
DO lookAt {target= $player, time=2.0}
DO emote {type=clapping, time=1.0}
Oh! I liek that! So smiley! Was it somethign I did??

CHAT MS2_StrongEmote_Joy_2 {stage=S2, type=tankResp, length=short, anxiety=true, high=true, ennui=true, joy=true, surprise=true, playerJoy=true}
DO lookAt {target= $player, time=2.0}
DO emote {type=laugh}
Hahaha yyyyeaaaaaaah! Me too! All warm insied!
DO twirl {time=2.0}

CHAT MS2_StrongEmote_Anger_1 {stage=S2, type=tankResp, length=short, anxiety=true, high=true, ennui=true, joy=true, surprise=true, sadness=true, playerAnger=true}
DO emote {type = worried, time =3.0}
DO lookAt {target= $player, time=2.0}
DO lookAt {target= tBotBackRight, time=2.0}
Its fine its fine theyre not mad at me its fine… {style = whisper}

CHAT MS2_StrongEmote_Anger_2 {stage=S2, type=tankResp, length=short, anxiety=true, high=true, ennui=true, joy=true, surprise=true, sadness=true, playerAnger=true}
DO lookAt {target= $player, time=2.0}
DO emote {type=anger, time=1.0}
SAY HEY what Im angrie too now! Yaeh! Grrrr!
WAIT 1.0
DO emote {type=smile, time = 2.0}
Aw waite Guppy can't stay anger when lookign at u!!

CHAT MS2_StrongEmote_Sadness_1 {stage=S2, type=tankResp, length=short, anxiety=true, high=true, ennui=true, joy=true, surprise=true, sadness=true, playerSadness=true}
DO emote {type=surprise, time=1.0}
DO lookAt {target= $player, time=2.0}
R u ok? U look sad!
DO nudge {target=glass, times=1}
//WAITFORANIMATION
DO emote {type=wave}
I beleve in u, blob. It will be ok.

CHAT MS2_StrongEmote_Sadness_2 {stage=S2, type=tankResp, length=short, anxiety=true, high=true, ennui=true, joy=true, surprise=true, sadness=true, playerSadness=true}
DO lookAt {target=$player, time=2.0}
Oh! hey? dont be sadfaces! Look! Look!
DO twirl {time = 2.0}
DO lookAt {target=$player, time=2.0}
DO emote {type=smile}
Better?

CHAT MS2_StrongEmote_Surprise_1 {stage=S2, type=tankResp, length=short, anxiety=true, high=true, ennui=true, joy=true, surprise=true, sadness=true, playerSuprise=true}
DO lookAt {target=$player, time=2.0}
DO emote {type=surprise, time=1.0}
Woah! Right??? right!! Suprised me to!

CHAT MS2_StrongEmote_Surprise_2 {stage=S2, type=tankResp, length=short, anxiety=true, high=true, ennui=true, surprise=true, sadness=true, playerSuprise=true}
DO lookAt {target=$player, time=2.0}
DO emote {type=laugh, time=1.0}
Yes life is ful of suprises!! 
😃[smiley emoji] I love it so much!

//Fish
//Hit With an Object
CHAT MS2_ObjHit_short_1 {stage=S2, type=hit, length=short}
DO emote {type=dizzy, time=2.0}
WhapS! Oaw!1!

CHAT MS2_ObjHit_1 {stage=S2, type=hit, length=short}
DO emote {type=surprise, time=1.0}
Waoah hey what heckin!
Whyd put u things in my tank?
DO swimTo { target = $lastScannedObject, style = direct}
Hm hm...this $lastScannedObject taste like...jaelousy?
Jaelousy but...happy?
DO emote {type = chinScratch}
Hmm...Not good as flake version tho.

CHAT MS2_ObjHit_2 {stage=S2, type=hit, length=short}
Wathc out woah!
DO emote {type=surprise}
Hey! A $object?!
DO swimTo { target = $lastScannedObject, style = direct}
Guppy knows $object. But…
DO lookAt {target=$player, time=2.0}
*how* does Guppy know?
Hmm...hmm...hmm…
DO emote {type = smile, time =2.0}
Mayby put in Guppy before Guppy was put in tank!
//Poked By Player
CHAT MS2_Poke_1 {stage=S2, type=poke, length=short, anxiety=true, curiosity=true, ennui=true, high=true, joy=true, sadness=true, surprise=true, branching=true}
DO swimTo {target=$player, style=direct}
//WAITFORANIMATION
DO emote {type = smile, time =1.0}
ASK 😃[Grinning Face]
OPT 😉 [Winking Face] #MS2_Poke_1_wink
OPT 😳 [Flushed Face] #MS2_Poke_1_flushed

CHAT MS2_Poke_1_wink {noStart=true}
DO emote {type = wink, time =1.0}
ASK  😉 [Winking Face]?
OPT 😉 [Winking Face] #MS2_Poke_1_kissEnd
OPT 😚 [Kissing Face With Closed Eyes] #MS2_Poke_1_smileEnd

CHAT MS2_Poke_1_flushed {noStart=true}
DO emote {type = wink, time =1.0}
ASK 😉 [Winking Face]
OPT 😳😳😳 [Flushed Facex3] #MS2_Poke_1_kissEnd
OPT  😉 [Winking Face] #MS2_Poke_1_smileEnd
CHAT MS2_Poke_1_kissEnd {noStart=true}
😚 [Kissing Face With Closed Eyes]
DO emote {type=heartEyes}

CHAT MS2_Poke_1_smileEnd {noStart=true}
DO twirls {time=2.0}
😊 [Smiling Face With Smiling Eyes]

CHAT MS2_Poke_2 {stage=S2, type=poke, length=short}
DO emote {style=surprise}
Woah! U cawt me off guard! 
But u cant poke me now I bet haha
Im TOO QUICK!
DO zoomies {time=4.0}
DO emote {type=bigSmile, time=3.5}
//WAITFORANIMATION
DO emote {type=whew}
Wew! that was lots!
Shouldn't skip fin day.
WAIT 2.0
Whatever that is! 😜[Winking Face With Tongue]

//Hungry
CHAT MS2_Hungry_1 {stage=S2, type=hungry, length=short, excitement=true, joy=true, anger=true}
DO lookAt {target=$player, time=2.0}
Hey! Hungry time!
Its flake time! Hit me with some delicious-ness!
What will it be? which flake? which feeling?
Oooo supsense killing me is!
..or maybe that's just hungryness.
DO emote {type=feedMe}
Feeeeed me!

CHAT MS2_Hungry_2 {stage=S2, type=hungry, length=medium, anxiety=true, curiosity=true, sadness=true, ennui=true}
DO emote {type=sleepy, time=2.0}
Hm. Feeling...kinda weebly-wobbly
DO swim to $player
Fading! thining! need…
DO twirl {time=1.0}
Sadness flaaaaaaakes!
Or 
DO twirl {time=1.0}
Anger flaaaaaakes! 
Any flakes! I don't care!
DO lookAt {target=$player, time=2.0}
Otherwise gettign me even ghostier. And hauntign u haha!
👻 [Ghost]
DO emote {type=bouncing, time=2.0}
OoooOooo flaAaaAAaaAAkes!

//Eating Responses
CHAT MS2_EatResp_1 {stage=S2, type=eatResp, length=medium}
DO emote {type=chewing, time=2.0}
//WAITFORANIMATION
DO emote {type=surprise, time=1.0}
Woawh! Whats in these!
DO emote {type=chewing, time=2.0}
//WAITFORANIMATION
S'good!
DO emote {type=chewing, time=2.0}
//WAITFORANIMATION
Seesoned with the gravy of...of…
DO emote {type=chinscratch}
//WAITFORANIMATION
DO emote {type=bigSmile}
Feeeeeels!
DO emote {type=burp}
Love the xtra gravys yum!

CHAT MS2_EatResp_2 {stage=S2, type=eatResp, length=medium}
DO emote {type=chewing, time=2.0}
So good! So good! So good!
DO emote {type=sick, time=2.0}
Woaaa ughhhhhhh
WAIT 1.0
think I ate too fast
Uuuggghhhxcpwefsp
DO emote {type = angry}
DO emote {type = puppyDog, immediate=false}
DO emote {type = bigSmile, immediate=false}
DO emote {type = surprise, immediate=false}
//WAITFORANIMATION
DO swimTo {target=$player, style=direct}
Indigestemotional!
//Has To Poop
CHAT MS2_Poop_1 {stage=S2, type=poop, length=long, anxiety=true, curiosity=true, high=true, joy=true, sadness=true, surprise=true}
Ugh, ooaaf…
Hey don't look gotta
poop {style = whisper}
DO swimTo {target=poopCorner, speed=fast, style=direct}
DO lookAt {target=$player, time=1.0}
said me don't look!
DO lookAt {target=poopCorner, time=1.0}
WAIT 2.0
DO lookAt {target=$player, time=4.0}
SAY DON'T LOOK {style=loud}
WAIT 3.0
Ugh ok I can't... just... here goes
DO poop {target=poopCorner, amount=small}
DO swimTo {target = $player, style=direct}
U weirdo...
But its ok, Guppy still loves u.
DO emote {type = smile, time =3.0}
💩[poop emoji]
//Pooped
CHAT MS2_Pooped_1 {stage=S2, type=pooped, length=medium, anxiety=true, curiosity=true, high=true, joy=true, sadness=true, surprise=true}
DO emote {type=surprise}
Woawh! That thing is huuuuuuge!
DO swimTo {target=poopCorner, style=direct}
mean I'm... kinda proud!
DO lookAt {target=$player, time=1.0}
Look at it!
DO lookAt {target=poopCorner, time=1.0}
Its like…
epic {style=whisper}
How did taht even fit in me??
WAIT 2.0
DO turn to $player
Uh...mind cleanign that up?
Not *that* proud.

//Emotion Capture
//Hold and then repurpose and use these chats in other sections if they don’t fit!
//Emotion-specific Responses (per emotion) - on hold
//Capture Requests
CHAT MS2_CapReq_1 {type=capReq, stage = S2, length = medium}
Uh oh! Flakes low? Look!
//DO point to $flakesMeter
So few! And kinda stale!
DO swimTo {target=$player}
You know what that means?
DO swimTo {target=right}
Time to capture!
DO swimTo {target=left}
Some!
DO twirl
Feeeeeeeeeeeels!

CHAT CORE_CapReq_5 {type=capReq, stage = S2, length = medium}
Oh no! Emotion low! Low happy, low fear, low angry?
//DO swim to $flakesMeter
Look! Look! What to do?
DO emote {type = surprise, time =2.0}
I know!
DO nudge {target=screenCenter}
Put your feeling-clouds into flakes.
It's fun! Also...
DO swimTo {target=bottom}
I don't wanna run out of flakes. {style=whisper}
Capture Success
CHAT MS2_CapSuc_1 {type=capSuc, stage = S2, length = long}
Ohmygosh look at them!
So flakey! So emotional! So…
DO emote {type=heartEyes}
Delicioussssss
ASK So good at this!! How do you do it?!
OPT Just natural, emoteful #MS2_CapSuc_1_natural
OPT Hard work, steadfast effort #MS2_CapSuc_1_work

CHAT MS2_CapSuc_1_natural {type=capSuc, stage = S2}
Yes, you have one of those faces.
Expressive…
Delicious…
Wonderful!

CHAT MS2_CapSuc_1_work {type=capSuc, stage = S2}
DO nods
Serious business. Takes alot of time to be emotional.
But it is worth it! Look at these flavors!
So...dense! 
Ecumenical!
Variegated!
DO twirl
So yummyyyyyyy!

CHAT MS2_CapSuc_2 {type=capSuc, stage = S2, length = long}
SAY WOW {style=loud}
Look at that one! What thoughtdreams were going through your mind?
It's so...plump!
DO emote {type = worried, time =2.0}
ASK It won't wilt? Won't stale? Won't sour?
OPT Oh...uh...yeah…#MS2_CapSuc_2_spoil
OPT No! Emotions are shelf-stable #MS2_CapSuc_2_unspoil

CHAT MS2_CapSuc_2_spoil {type=capSuc, stage = S2}
DO emote {type = happy, time =2.0}
Then we just will have to make sure we eat them up!
Quickly! Quickly!
DO swimAround {target=center, time=2}
Eat eat eat eat eat
CHAT MS2_CapSuc_2_unspoil {type=capSuc, stage = S2}
ASK Oh! Your emotions must be...are...uh…
OPT Partially hydrogenated #MS2_CapSuc_2_finish
OPT Irradiated #MS2_CapSuc_2_finish
OPT Dehydrated #MS2_CapSuc_2_finish

CHAT MS2_CapSuc_2_finish {type=capSuc, stage = S2}
Cool! Yes! Of course!
DO emote {type = happy, time =2.0}
I don’t know what that means, but I know it means…
DO emote {type=heartEyes}
deliciousssssss {style=whisper}
//Capture in Progress
CHAT MS2_CapProg_1  {type=capProg, stage = S2, length = medium}
DO swimTo {target=$player}
Oh! Wait keep making that face!
No the one before that!
Yes! {style=fast}
Wait! {style=fast}
No before that! {style=fast}
Yeah!
Huh...no not that one.
DO emote {type = happy, time =2.0}
But it's still a good one. I like it!


CHAT MS2_CapProg_2 {type=capProg, stage = S2, length = medium}
DO swimTo {target=$player}
No offense, but these emotions feel...same-y.
Why not try something new?
DO twirl {time=1}
Something fresh?
DO twirl {time=1}
Scary???
DO swimTo {target=$player}
Make a flake for guppy you don't make for anyone else.
A special flavor!
Please?
WAIT 2.0
I'll take that as a yes! Ok!
WAIT 0.5
1!
2!
3!
SAY GO!! {style=loud}
WAIT 2.0
DO twirl
DO emote {type = surprise, time =2.0}
Aaaaaaa omg so coool such flakes wooowoooooww!
Capture Failure
CHAT MS2_CapFail_1 {type=capFail, stage = S2, length = medium}
Huh? Wait is that your flakiest face?
Seems kinda...bland?
Like...1% milk?
Cheese food? {style=fast}
Saying ok when people ask how are you?
Applying for retail?
Try again! Go for the moon! Make a moon face!
Yes! An *angry* moon face!
You can do it!

CHAT MS2_CapFail_2 {type=capFail, stage = S2, length = short}
Oh no! That won't flake!
Is this the end??? Are you empty???
DO swimTo {target=$player}
Oh, hmm nope you still have clouds. Colors! Lights!
DO nudge {target=screenCenter}
Guppy sees them. You can do it! Try again!
I can see the flavors around you...

//General

//Hellos
CHAT MS2_Greet_1 {stage=S2, type=hello, length=medium, anxiety=true, curiosity=true, high=true, joy=true, sadness=true, surprise=true}
DO lookAt {target = $player, time=4.0}
DO emote {type=surprise, time=1.0}
Oh hey! Ur back!
Time to show me somethign?
Feed me somethign?
Poek me somethign?
DO emote {type = smile, time =2.0}
Im ready! Lets go!

CHAT MS2_Greet_2 {type=hello, stage = S2, length = short}
DO swimAround {target = center, loops=3, speed=medium}
Hey hey hey! 
DO swimTo {target=$player, style=direct}
Friend!

//Return After Having Not Played
CHAT MS2_Return_1 {stage=S2, type=return, length=medium, anxiety=true, curiosity=true, high=true, joy=true, sadness=true, surprise=true}
DO nudge {target=glass, times=1}
//WAITFORANIMATION
DO emote {type=surprise, time=2.0}
Woooaaaaaah its u!!!
DO swimAround {target = center, loops=3, speed=medium}
U came back u came back u came back!
Was worried u found another Guppy
There are lots of us, u know {style=whisper}
DO swimTo {target = tBotBackRight, style=direct}
//WAITFORANIMATION
DO lookAt {target=$player, time=1.0}
DO emote {type=shifty, time=1.0}
watchign...
DO swimTo {target = tBotBackLeft, style=direct}
//WAITFORANIMATION
DO lookAt {target=$player, time=1.0}
DO emote {type=shifty, time=1.0}
waeting...
WAIT 2.0
DO emote {type = bigSmile, time =2.0}
Smiling!!

CHAT MS2_Return_2 {stage=S2, type=return, length=short, anxiety=true, curiosity=true, high=true, joy=true, sadness=true, surprise=true}
Huh? Who? Oh!
DO swimTo {target=$player, style=direct}
Its big blurry blob thing that makeing all the food!
DO emote {type=wave}
Hey! Wondering if was going to have to find u!
DO emote {type = smile, time =2.0}
glad u found me instead!
//Random Conversation
CHAT MS2_Muse_1 {stage=S2, type=rand, length=short, anxiety=true, curiosity=true, high=true, joy=true, sadness=true, surprise=true, branching=true}
I thouht fone feels diferent than big tank, u know?
ASK When I followed u. From before.
OPT Wayt me follow from befour? #MS2_Muse_1_2

CHAT MS2_Muse_1_2 {noStart=true}
DO emote {type=nodding, time=2.0}
befour, back in large tank, watchign I was. 
ur lights, ur flavours
was liek:: 
DO emote {type=heartEyes, time=2.0}
must have them {style=whisper}
ASK u ever do that befour? watch?
OPT Yes #MS2_Muse_1_creepy
OPT No #MS2_Muse_1_good
CHAT MS2_Muse_1_creepy {noStart=true}
Waoh! kindah creepy!
CHAT MS2_Muse_1_good {noStart=true}
thouht no. It guppeys thing
We go 2 shiny stuf
Liek mofs/moughfs
DO emote {type = bigSmile, time =2.0}
Liek guppeys!!!

CHAT MS2_Muse_2 {stage=S2, type=rand, length=long, anxiety=true, joy=true, surprise=true, ennui=true, nostalgia=true, curiosity=true, branching=true}
U make so many flakces for Guppy…
Thank U!!
But… makes to want makeing some flakes for u!
ASK What's ur favorite flavor?
OPT Happyflake! #MS2_Muse_2_happy
OPT SadFlake... #MS2_Muse_2_sad
OPT AngryFlake!! #MS2_Muse_2_angry

CHAT MS2_Muse_2_happy {noStart = true}
Ok! Coming right up! Ready???
DO inflate {amount = huge, time=3.0}
DO emote {type = bigSmile, time =3.0}
//WAITFORANIMATION
DO emote {type=sleepy, time = 2.0}
ASK Whew! Huh...no flake. Wyrd??
OPT Its ok! #MS2_Muse_2_ok
OPT Try again? #MS2_Muse_2_tryAgain

CHAT MS2_Muse_2_sad {noStart = true}
Ok! comign rite up! Ready???
DO inflate {amount = huge, time=3.0}
DO emote {type = crying, time =3.0}
//WAITFORANIMATION
DO emote {type=sleepy, time = 2.0}
Siiiiiiiggggghh...no flake. sad now.
ASK Sorry! I let u down…
OPT Its ok... #MS2_Muse_2_ok
OPT Try again? #MS2_Muse_2_tryAgain

CHAT MS2_Muse_2_angry {noStart = true}
Ok! comign rite up! Ready???
DO inflate {amount = huge, time=3.0}
DO emote {type = furious, time =3.0}
SAY RAAAAAAHHHH {style=loud}
//WAITFORANIMATION
DO emote {type=sleepy, time = 2.0}
ASK Arrrrgh! No flake. Stupid!!!
OPT Its ok! #MS2_Muse_2_ok
OPT Try again? #MS2_Muse_2_tryAgain

CHAT MS2_Muse_2_ok {noStart = true}
...discoverd a new thing. guppy needs u.
Maybe...maybe somehow u need guppy?
DO emote {type = smile, time =2.0}
Yes!
CHAT MS2_Muse_2_tryAgain {noStart = true}
Too tired!
DO emote {type = tired, time =2.0}
Maybe try later. Whew! How do u make such good flake?
Its...exhawsting! Im just not flakey!

CHAT MS2_Muse_3 {stage=S2, type=rand, length=long, anxiety=true, joy=true, surprise=true, branching=true}
Hey! Guppy thought saw...u and other thing??
ASK U have another pet?
OPT Yes! #MS2_Muse_3_yes
OPT No! #MS2_Muse_3_no

CHAT MS2_Muse_3_yes {noStart = true}
DO emote {type=awe, time=2.0}
Woooaaah! One with legs?????
OPT Yes! #MS2_Muse_3_legs
OPT No! #MS2_Muse_3_noLegs

CHAT MS2_Muse_3_no {noStart = true}
Oh...maybe just ate some rotten feelings
sometiems they make me see thigns {style=whisper}
liek…
DO emote {type=catnip, time=2.0}
A giant snowman whos head is fire and hes playign piano but the piano is also a smile who is whispring secrets???? {style=fast}
DO emote {type = happy, time =2.0}
Or birds!

CHAT MS2_Muse_3_legs {noStart = true}
DO emote {type = frown, time =2.0}
Now jealous! Legs?! Legs?!
DO swimAround {target=center, loops=3, speed=fast}
I bet it can't do this tho!
DO twirl {time = 2.0}
//WAITFORANIMATION
Or this!
DO inflate {type = huge, time =0.5}
//WAITFORANIMATION
DO inflate {type = extreme, time =0.5}
//WAITFORANIMATION
DO emote {type=typeEyes, eyes = FINS}}
//WAITFORANIMATION
DO emote {type=determined}
Yeah! Ha! Legs ok...but fins!
Fins are best!
CHAT MS2_Muse_3_noLegs {noStart = true}
DO emote {type=surprise}
Woooaaah. Wait...wait u have…
SAY ANOTHER GUPPY?!?!?! {style=loud}
DO stare 2.0
DO emote {type=laugh}
Ha! No way! This Guppy is ur only Guppy.
Unless u have snake-guppy…
DO vibrate {time=1.0}
Snake guppies...bad guppies…


//Samantha example in archived doc//LINK: remaining chat minimum, length and emotion suggestions coming soon;)

//reminders= BETA:
//reminders and points that need to be included in new chats in blue
//comments for changes to existing chats in yellow/on side

//A. CONTENT 

//B. EMOTIONS
//Chats in each bin/type need to cover a range of Guppy states (see meta tag emotions //at top) while staying true to this moment of the plot. Guppy states can be---ANGER, //SADNESS, SURPRISE, CURIOSITY, ANXIETY, NOSTALGIA, ENNUI, EXCITEMENT, //HIGH, JOY *see //link above for your specific emotion recommendations for each bin/type

//C. OBJECTS
//At least 1/3 of chats in this stage should interweave Guppy asking to see general or specific //objects in list.
//Tank Mode
//Tank Shaken

CHAT MS1_Shake_1line_1 {stage=S1, type=shake, length=short, anxiety=true, surprise=true}
Heeey! Wooaahow u do?? This why??? Ppleas stpo!

CHAT MS1_Shake_fewWords_1 {stage=S1, type=shake, length=short, anxiety=true, surprise=true}
Whaaoo how why you do??

CHAT MS1_Shake_1 {stage=S1, type=shake, length=short, joy=true, surprise=true}
DO emote {type = surprise}
Sloshingg guppy waves! Waves!
Tank is tree
Tnk in wynd
DO lookAt {target=$player, time=1}
Xciting! again! again!

CHAT MS1_Shake_2 {stage=S1, type=shake, length=short, surprise=true}
DO emote {type = sick}
Too much too
much too much
DO swimAround {target = center, loops=4, speed = slow}
DO emote {type = dizzy}
Round around slosh and spin but
WAIT 2.0
🤮
Cboiaw sepvimnxci&#*9
Ugh. Betternow. Please no more.

CHAT MS1_Shake_3 {stage=S1, type=shake, length=medium, anxiety=true, surprise=true, curiosity=true}
DO emote {type = fear}
Oh no don't spill!
DO swimTo {target=tBotBackRight, speed = fast, style = direct}
DO lookAt {target = $player, time=2}
D-don't spill out into...brihgt colors world {style=whisper}
WAIT 2.0
DO emote {type = smile, time =4.0}
Or *do* spillshake to bright color word!
shake! shake! shake!
DO zoomies {time=3.0}
WAIT 2.0
DO emote {type = sad, time =2.0}
Aww...stil here.

//Tank Tapped
CHAT MS1_Tap_1line_1 {stage=S1, type=tap, length=short, surprise=true, anger=true}
Ow1! Geez thats lowd ow! hey blob no tap why???

CHAT MS1_Tap_fewWords_1 {stage=S1, type=tap, length=short, surprise=true, anger=true}
Waoh! Wath hey why??

CHAT MS1_Tap_1 {stage=S1, type=tap, length=short, surprise=true, joy=true}
DO emote {type = startled}
Woah! 
Hi! {speed = fast}
Hello! {speed = fast}
Hi! {speed = fast}
Loudpingpong. Poooong! waht??


CHAT MS1_Tap_2 {stage=S1, type=tap, length=short, surprise=true, joy=true, curiosity=true}
Oh!
DO emote {type = startled}
DO swimTo {target=$player, speed = medium, style = direct}
Hear that? loud noise! somehwere…
DO swimAround {target = center, loops=4, speed = fast}
Where top tap tonk tap-tap? Toom! Toom!

//Player Emotes Strongly At Guppy
CHAT MS1_EmoStrong_Joy_1 {stage=S1, type=tankResp, length=short, joy=true, playerJoy=true}
DO swimTo {target=$player, style = direct}
DO emote {type = smile}
Ah! Beams ur face all warm and sunful!

CHAT MS1_EmoStrong_Joy_2 {stage=S1, type=tankResp, length=short, joy=true, playerJoy=true}
DO emote {type = smile, time=1.0}
DO swimTo {target=$player, style = direct}
Awh! Like this face smile happy!

CHAT MS1_EmoStrong_Anger_1 {stage=S1, type=tankResp, length=short, anger=true, curiosity=true, playerAnger=true}
DO swimTo {target=$player, style = direct}
DO emote {type = angry, time=1.0}
What! What?! Y all frowning liek that??

CHAT MS1_EmoStrong_Anger_2 {stage=S1, type=tankResp, length=short, anger=true, anxiety=true, playerAnger=true}
DO swimTo {target=$player, style = direct}
DO emote {type= scared, time=1.0}
Y so anger faced yieks!! Plz stahp wit taht!

CHAT MS1_EmoStrong_Sadness_1 {stage=S1, type=tankResp, length=short, sadness=true, curiosity=true, playerSadness=true}
DO swimTo {target=$player, style = direct}
DO emote {type= sad, time=1.0}
Y is yuor face broken like that? I'm sory don't be sad liek that!

CHAT MS1_EmoStrong_Sadness_2 {stage=S1, type=tankResp, length=short, sadness=true, curiosity=true, playerSadness=true}
DO swimTo {target=$player, style = direct}
DO emote {type= sad, time=1.0}
Waht has blob seen make so sad??? Much-so sad blobs

CHAT MS1_EmoStrong_Surprise_1 {stage=S1, type=tankResp, length=short, surprise=true, anxiety=true, playerSuprise=true}
DO swimTo {target=$player, style = direct}
DO emote {type = surprise, time=1.0}
Wahht?! Is there sometinhg about to eat guppy??

CHAT MS1_EmoStrong_Surprise_2 {stage=S1, type=tankResp, length=short, surprise=true, joy=true, playerSuprise=true}
DO swimTo {target=$player, speed = medium, style = direct}
DO emote {type = smile, time=1.0}
U suprize at me??? Watch noww!!
DO flip
Now that's! surprise!
//Fish

//Hit With an Object
CHAT MS1_ObjHit_short_1 {stage=S1, type=hit, length=short, surprise=true}
DO emote {type = bulgeEyes, time=1.0}
Xor || owie!1??

CHAT MS1_ObjHit_1 {stage=S1, type=hit, length=short, joy=true, curiosity=true, surprise=true}
DO emote {type = startled, time=1.0}
Owww wajt haha!
Why do yuo hit with shoe? 
Wait not shoe…
WAIT 0.5
avacado? 
NVM 1.0 
Shovacado? Avacashoe!
No...beingf don't tell...wayt...
NVM 3.0
DO emote {type=whisper, time=1.0}
Shovaca-dosha-dacadoo??? {style=whisper}
DO emote {type = nodding, time=1.5}
Yes. seen shovacadoshadacadoos befour.

CHAT MS1_ObjHit_2 {stage=S1, type=hit, length=short, surprise=true, curiosity=true}
DO emote {type=startled, time=1.0}
Waahat! Waht is this! Funny-feel…
DO swimTo {target = $newestObject, style=meander}
Is it…$newestObject? Like bottle mebe, but bookshelved like aples?
DO swimAround {target = center, loops=3, speed = fast}
Cooooooool!
//Poked By Player
CHAT MS1_Poke_1 {stage=S1, type=poke, length=short, surprise=true}
Woah! Hey! U play?
DO swimTo {target = glass, speed=fast, style=direct}
Now pokeings *u*! 
DO nudge {target=glass, times=2}
Poke! Poooke! awe*#H)e!
DO emote {type = sad, time =2.0}
Awww...doesn't work

CHAT MS1_Poke_2 {stage=S1, type=poke, length=short, surprise=true, anxiety=true, joy=true}
Yow! Yikes!
DO twirl {time=1.5}
Startle|scare me! 
DO nudge {target=glass, times=1}
DO emote {type = laugh}
haha ur face looks happie tho. 
Makes me happie!
DO emote {type = wink}
...and hungry hehe 😉

CHAT MS1_Poke_3 {stage=S1, type=poke, length=short, surprise=true}
DO lookAt {target=$player, time=0}
DO emote {type = surprise, time =2.0}
Aafskjbrwpf?!
Made me sarderofop my words!
Don't do that!
😂


//Hungry
CHAT MS1_Hungry_1 {stage=S1, type=hungry, length=short}
Food? Hungry! Hungrynow!
DO swimTo {target=$player}
Empty insidde, groweing adark
DO emote {type = sad, time =2.0}
maed of lihgt take fligt, need more
u give me mor bites? 

CHAT MS1_Hungry_2 {stage=S1, type=hungry, length=short, curiosity=true}
DO nudge {target=glass, times=1}
DO emote {type=drool}
Delicious....
DO swimAround {target = center, loops=2, speed = slow}
Omg ur lightglitterglow! No not you haha!
😆
Ur moods make clouds! Headcluods! feed me?
DO emote {type = sad, time =3.0}
Plz give som cloud-flakes so I look deliciuos insyd too?
Happyness flakes? Sadness flakes? Anger flakes!
//Eating Responses
CHAT MS1_EatResp_1 {stage=S1, type=eatResp, length=short, curiosity=true, joy=true}
Om nom nom! So good! so good!
Nwo I feel it too! The glow-light in my light! Is anger?
WAIT 1.0
No sadness!
WAIT 1.0
no wait...love!
WAIT 1.0
Maybe somethign new?
Whatevar it: deliciuos!

CHAT MS1_EatResp_2 {stage=S1, type=eatResp, length=short, curiosity=true, joy=true}
Yessss fooooood!
Now feel feels insidee & it makes me feel!
Does this mean haveing blobs now?
DO emote {type = surprise, time =2.0}
U are me? Ur...Guppy?
WAIT 2.0
DO emote {type = smile, time =4.0}
How good! *#Hw0hqk!!
Good liek Guppy! 
//Has To Poop
CHAT MS1_Poop_1 {stage=S1, type=poop, length=medium}
Ooooark I fel too now, too full.
DO lookAt {target= $player, time=1.0}
Don't look!!
DO poop {target=poopCorner, amount=small}
💩
DO swimTo {target=$player, speed=slow, style=direct}
😄

CHAT MS1_Poop_2 {stage=S1, type=poop, length=short}
Uh-oh. I gotta…
Poo@*p&$@Q(!!! 
DO poop {target=poopCorner, amount=small}

//Pooped
CHAT MS1_Pooped_1 {stage=S1, type=pooped, length=medium}
Whew! better! Yaes! Ffeel...
DO swimAround {target = center, loops=1, speed = slow}
DO swimAround {target = center, loops=1, speed = medium, immediate = false}
DO swimAround {target = center, loops=1, speed = fast, immediate = false}
Lihgter! faster! swimmier!
Out wiht the old!
In wit the new!
Its risky to LOL
Whn you have to poo!
😂😂😂

CHAT MS!_Pooped_2 {stage=S1, type=pooped, length=short, joy=true}
DO lookAt {target=poopCorner}
DO lookAt {target=$player, time=2.0,immediate = false}
//WAITFORANIMATION
DO emote {type = smile, time =2.0}
Look! waht I made!
//Emotion Capture

//Emotion-specific Responses (per emotion)
//(currently on hold)
//Capture Requests
CHAT MS1_CapReq_1 {type=capReq, stage = S1, length = short, branching = true}
Woah! Wait! Where’s the flakes?
DO swimTo {target=$player}
Flakes low! A drought? Famine! No food? Scary!
ASK Don't you have scare feels now?
OPT Oh yes. #MS1_ScanScareFeel
OPT No, no scare feels. #MS1_WhatFeel

CHAT MS1_ScanScareFeel {type=capReq, stage = S1, length = short}
No but good! Scan your scared into scare flakes!
Scare flaaaaakes. So good! Taste like…
skeleton shiver scream-gasps! {speed = fast}
ghost monster fang mortgages! {speed = fast}
bat-wing haunted phone breakups! {speed = fast}
DO emote {type = happy, time =2.0}
Delicious!

CHAT MS1_WhatFeel {type=capReq, stage = S1, length = short}
No? Well what flavor do you feel? Wait!
Doesn't matter! All good! All delicious!
DO twirl
Scan a feel to flakes it's make or break!

CHAT MS1_CapReq_2 {type=capReq, stage = S1, length = short, branching = true}
Woah! Wait just that many flakes?
DO swimTo {target=$player}
ASK You ok? You still feel? Make flakes?
OPT Ok I make feel yes #MS1_MakeFeel
OPT No, no feel #MS1_PromptFeel

	CHAT MS1_MakeFeel {type=capReq, stage = S1, length = short}
	Yaaay! Oh yes! Make a scare feel! 
DO swimTo {target=$player}
Or a sparkly happy feeling? 
DO swimTo {target=bottom}
Or a misty moony saddy feel?
DO swimAround {target=center}
All good! So delicious! So tasty! Full of light!

CHAT MS1_PromptFeel {type=capReq, stage = S1, length = long}
Whaaaaaaat! No you *must* feel! Wait.
DO swimTo {target=$player}
Guppy will help! With SCIENCE. 
I make faCe, you make fa8!3cEs.
You mAke face, you make flakes! Can't help it. It's SCIENCE.
	Let's practice now.
Ready? Ok I make face!
😆
	Now you!
	WAIT 2.0
	Haha yes! Ok now
	😳
	Now you!
	WAIT 2.0
	Ooo yeS your c0!oRs brighten! Ok one more...tricky…
	👹
	WAIT 2.0
	DO emote {type = scared, time =1.0} 
Too good too scary wwwwoaooaaah!
DO emote {type = happy, time =3.0}
Good yes good! Now do and make flake! Please?

	

//Capture Success
CHAT MS1_CapSuc_1 {type=capSuc, stage = S1, length = medium}
DO emote {type = happy, time =5.0}
Wowww! So good! So...flakey!
I can't wait to niB8le that! Savor it!
DO swimTo {target=$player}
👹 DEVOUR IT 👹
WAIT 2.0
DO emote {type = laugh, time =2.0}
😂

CHAT MS1_CapSuc_2 {type=capSuc, stage = S1, length = short}
DO twirls
So g00d! So much sparkly misty happysadangrylonelycryjoylaugh flakes!
So...nutritional! Fibrous!
//Capture in Progress
CHAT MS1_CapProg_1 {type=capProg, stage = S1, length = medium}
Yes like that, but what if...more?
Make it more! More !ntense! More special!
WAIT 2.0
DO twirl
Yes like that! You're doing it!

CHAT MS1_CapProg_2 {type=capSuc, stage = S1}
Your face isn't mark-lined, move plz?
Scootch scootch scootch…
Oh wait yes that's now! Say cheese!
Or bamboo! a{!Fms03!!!
Or spinneret!
Or hairbrush!
All good!
//Capture Failure
CHAT MS1_CapFail_1 {type=capFail, stage = S1, length = medium}
Oh...no that's not flakey. More like...fakey?
DO swimTo {target=$player}
Guppy needs *strong* flakes, intense flavor!
Sharp taste! Like
last time mother picked you up {speed = fast}
favorite secret hideout {speed = fast}
last first kiss {speed = fast}
when you made proud {speed = fast}
Maybe try later? Or now! Don't worry! You have colors! I see them!

CHAT MS1_CapFail_2 {type=capFail, stage = S1, length = medium}
Mmmmm no. Not quite delicious.
DO nudge {target=glass}
I *see* your flakiness. Colors all sharp and bright.
DO emote {type = sad, time =2.0}
But you have to show your colors...
DO emote {type = happy, time =4.0}
Try again? Make a flake! I believe in you!
DO emote {type=clapping}


//General

//Hellos
CHAT MS1_Greet_1 {stage=S1, type=hello, length=short, joy=true, curiosity=true, branching=true}
Oh! U look tasty todaiy!
Oh uh 😳 sory
uhhUM...happy to see u!
DO swimAround {target = center, loops=1, speed = slow}
U seem...glowy! Whirley! Like many thigns hapned to u!
DO swimTo {target = $player, style=direct}
DO emote {type = bigSmile}
It's nice.

CHAT MS1_Greet_2 {stage=S1, type=hello, length=long, joy=true, curiosity=true, branching=true}
Hey! I was thinkign! & now 
Ur here!
DO swimTo {target = $player, style=direct}
Maybe ur...guppy-thouhgt? 
WAIT 2.0
U’re so glowy. Hmm…
ASK u real? Or guppy thouhgt?
OPT What no haha real yes #MS1_realPlayer
OPT No I'm guppy thought outer to inner #MS1_fakePlayer
	CHAT MS1_realPlayer {noStart=true}
	Oh whew! I never dowted of cours. ur too...sparkly!
	Too brihgt!
	Too deliceous!
	😳
	Forget that last one. Lets play!

	CHAT MS1_fakePlayer {noStart=true}
DO emote {type = awe, time =2.0}
I knew it {style=whisper}
	Ur an inner to an outer guppy? Amazing!
	abvoiwbcosl!!
	I have so many questoins??
DO zoomies {time=4.0}
	R u me? {speed = fast}
	Do u feel the light inisde? {speed = fast}
	Can u remebmer holes? {speed = fast}
	Are fingers fins or fins fingers? {speed = fast}
	DO lookAt {target = $player, time=1.0}
	But most impuortant:
	ASK Can u still make flakes?
	OPT Yes I still make flakes #MS1_makeFlakes
	OPT No I eat the light no light making #MS1_makeFlakes

	CHAT MS1_makeFlakes {stage=S1, type=hello, length=short, noStart=true}
	Aaaaawww waaaaaait a second! 
	DO swimAround {target = center, loops=2, speed = medium}
	Ur playing a trick!
	Hahahahaha!
	Ur real! Ha was worreid!
	Intriged. Piqued!
	But wOrried!
	Let's play!
//Return After Having Not Played
CHAT MS1_Return_1 {stage=S1, type=return, length=medium, joy=true}
DO emote {type=surprise}
*gasp* It's u! U came back!
So much has happened!
DO zoomies {time=4.0}
I maed a robot and robott sang a song that made a duck coem by but duck said no you cant make robbot so it nibbled and and robot fight! Big xplosions! {speed = fast}
SAY BOOoo(OO)oooom! {style=loud}
DO swimAround {target = center, loops=2, speed = fast}
//WAITFORANIMATION
DO emote {type=awe, time=2.0}
They made me thier king!!! Isn't that amazing?!?!
DO swimTo {target=$player, style=meander}
Ok, maybe it happened in my Guppymind...
DO emote {type =thinking, time =2.0}
Lots of thigns live in here, with me
In the Guppymind

CHAT MS1_Return_2 {stage=S1, type=return, length=medium, joy=true}
It's u! Agaiin!
DO emote {type = smile, time =4.0}
Flakey friend! With th glowlihgts!
DO swimAround {target = center, loops=3, speed = fast}
Now mabe play a bit! 
Or flake a bit? 
Or tell a story abowt outside the tank! 
Aaaa excitefarglatorporatofal!
Words!

//Random Conversation
CHAT MS1_Muse_1 {stage=S1, type=rand, length=short, curiosity=true, joy=true}
I dreamt a color, while u wer dark.
It was like…
DO emote {type=dreaming, time=4.0}
DO swimAround {target = center, loops=3, speed = slow}
A printer set on fire by rubbign 2 fingers together
Kind of textie, smelly…
but in a scratcy sparky inky way?
DO emote {type=bubbles}
It was xciting! Scary!
👌🖨👌
DO emote {type = surprise, time =1.0}
...then I woke up. And u wer ehere!

CHAT MS1_Muse_2 {stage=S1, type=rand, length=medium, curiosity=true, branching=true}
DO emote {type=worried, time=4.0}
Y r ur finz wyrd?
5 and So…. long? So….
DO vibrate {time=2.0}
DO emote {type = scared, time =1.0}
*tendrily???*
ASK Y?
OPT Fins-gers they fin-gers? #MS1_Muse_2_fingers
OPT me tug to make so long	#MS1_Muse_2_stretched
OPT How dare! #MS1_Muse_2_dare
	CHAT MS1_Muse_2_fingers {noStart=true}
	Fin-gers? Gers maybe means hmm
"long thin bad 4 swimms?" {speed = fast}
	But they good 4 make the poke
	So wyrd!

	CHAT MS1_Muse_2_stretched {noStart=true}
	Waaoaooaoo! Why would u do that!
	Straetch ur fins?! U changed?!
	I want to change to make new...dfferent.
	Togehter will do it!
	😃

	CHAT MS1_Muse_2_dare {noStart=true}
	Sory sorryy!
	Theyre beutiful fins.
	gorgeus!
	handsome!
	efficeint!
	vry goemetric!

CHAT MS1_Muse_3 {stage=S1, type=rand, length=short, curiosity=true}
DO lookAt {target=$player, time=4.0}
U know sometimes how u sneze and
It feels liek mayby somthign jogs loose?
When ur gon I sneze and
DO swimAround {target = center, loops=2, speed = fast}
Felings bop aruond my head and becom new thigns!
Hapysade! angryfear! Startlsad!
DO lookAt {target=$player, time=1.0}
Almost liek new flavorrs??


//Notes, scratch in archived copy;)//reminders= BETA:
//reminders and points that need to be included in new chats in blue
//comments for changes to existing chats in yellow/on side

//LINK: remaining chat minimum, length and emotion suggestions coming soon;)

//A. CONTENT 
//Too general. Wrong world details. Step 1: Call me about plot/life stages, 
//Step 2: //reference Joe’s “GuppyLifeStages

//B. EMOTIONS
//Chats in each bin/type need to cover a range of Guppy states (see meta tag emotions //at top) while staying true to this moment of the plot. Guppy states can be---ANGER, //SADNESS, SURPRISE, CURIOSITY, ANXIETY, NOSTALGIA, ENNUI, EXCITEMENT, //HIGH, JOY *see //link above for your specific emotion recommendations for each bin/type

//C. OBJECTS
//At least 1/3 of chats in this stage should interweave Guppy asking to see general or specific //objects in list.


CHAT SS_shake_1 {type=shake, stage=SS, length=short, joy=true}
Whoooaaa!
Okay.  Wait.
DO twirl
//DO LOTUS POSITION 
DO holdStill {time =0}
DO emote {type=meditate, time=5}
Peace isn’t peace 
if it cannot endure 
chaotic circumstances.
DO emote {type=bubbles, time=1}
WAIT 2.0
DO emote {type=bubbles, time=1}
WAIT 2.0
DO emote {type=bubbles, time=1}
DO idleMode


CHAT SS_shake_2 {type=shake, stage=SS, length=short, joy=true, high=true}
//DO LOTUS POSITION 
DO holdStill {time = 0}
DO emote {type=thinking, time=7}
It is not the tank that moves.  
WAIT 1.0
Nor is it the water that moves.  
WAIT 1.0
What moves is the mind.  
DO emote {type=meditate}
DO idleMode



CHAT SS_shake_3 {type=shake, stage=SS, length=short, joy=true}
Whoooaaa!
Okay.  Wait.
DO twirl
//DO LOTUS POSITION 
DO holdStill {time=0}
DO emote {type=meditate, time=5}
Peace isn’t peace 
if it cannot endure 
chaotic circumstances.
WAIT 1.0
DO emote {type=bubbles}
WAIT 1.0
DO emote {type=bubbles}
WAIT 1.0
DO emote {type=bubbles}
DO idleMode


CHAT SS_shake_4 {type=shake, stage=SS, length=short, joy=true, high=true}
//DO LOTUS POSITION 
DO holdStill {time = 0}
DO emote {type=thinking, time=7}
It is not the tank that moves.  
WAIT 1.0
Nor is it the water that moves.  
WAIT 1.0
What moves is the mind.  
DO emote {type=meditate}
DO idleMode



CHAT SS_Shake_5 {type=shake, stage=SS, length=medium, joy=true, nostalgia=true}
//DO LOTUS POSITION 10.0
DO holdStill {time = 0}
DO emote {type=dreaming, time=12} 
I read a story about a warrior 
who wanted to become the best archer in the land. 
So he goes to this archery master to train.  
DO emote {type=bubbles}
WAIT 2.0
DO emote {type=thinking, time=5}
He demonstrates that he’s already a pretty good shot.
So the master tells him to shoot his arrows from a teetering rock.  
//DO SWAY BACK AND FORTH
DO dance {time = 10}
DO emote {type=dreaming, time=10}
Some of his arrows hit the target, 
so the master tells him, 
“Now, shoot from the rock 
while balancing on one leg!”
//DO SWAY BACK AND FORTH WITH ONE FIN TUCKED
DO holdStill
DO emote {type=flapFinLeft, time=6}
Of course, the warrior falls over, and complains:
“It’s impossible to shoot an arrow like this!”  
//DO LOTUS POSITION 
DO holdStill {time = 8}
DO emote {type=meditate, time=8} 
The master says, 
“to be a master archer 
means to adapt to life’s conditions.”


//****

CHAT SS_Tap_1 {type=tap, stage=SS, length=short, anxiety=true, anger=true, surprise=true, high=true}
Oh my god! {style=loud}
DO twirl 
DO emote {type=disgust}
The vibrations!  
Dang, that brings really bad vibes to my crystals.  
DO sigh




CHAT SS_Tap_2  {type=tap, stage=SS, length=short, branching=true, surprise=true, curiosity=true, joy=true}
DO emote {type=snap}
Oh, wow. That actually reminds me... 
I wanted to ask you about maybe replacing the glass of your phone with like, crystal quartz?  
ASK Would that be cool?
OPT Awesome! #SS_Tap_2_CrystalQuartz1
OPT Not really. #SS_Tap_2_CrystalQuartz2

CHAT SS_Tap_2_CrystalQuartz1 {noStart=true}
Do emote {type= bouncing}
I know, right? 
I feel like it would really neutralize the harmful effects of carrying it around everywhere.


CHAT SS_Tap_2_CrystalQuartz2 {noStart=true}
DO emote {type=eyeRoll}
C’mon! Really?
DO emote {type=chinScratch, time =2}
I just feel like quartz would balance out all that mobile device radiation.
//EMOTE INDIFFERENCE 
//EMOTE JOY



CHAT SS_Tap_3 {type=tap, stage=SS, length=short, anxiety=true, ennui=true, curiosity=true, high=true}
//DO SHAKE HEAD
DO emote {type=chinScratch, time =2}
It’s so weird, because it’s like my whole world, you know?  
//DO SWAY BACK AND FORTH
DO dance {time=6}
DO emote {type=dreaming, time=6}
And if I relax into it, the tapping goes all through the water and through me, too.  
All through my mind until there’s nothing but the tapping.
DO emote {type=catnip}


//***

CHAT SS_Critic_1 {type=critic, stage=SS, length=short, branching=true, anxiety=true, sadness=true, ennui=true, high=true, tankOnly=true}
Hey! {style=loud}
Do you mind if I just totally redecorate?  I feel like right now the space is just…
DO twirl 
DO emote {type=worried}
...not where I’m at right now.  I just want to make it minimalist and like, clear.
//DO SWIM SLOW CIRCLE
DO swimTo {target=$player, speed=slow}
ASK So, would you mind?
OPT  No, go ahead.  #SS_Critic_1_redecorate1
OPT  Yes, I mind big time.  #SS_Critic_1_redecorate2

CHAT SS_Critic_1_redecorate1 {noStart=true}
DO emote {type=clapping}
Awesome! I’ll *fin* to it.
DO emote {type=wink}
LoL! Fish puns.
Fish puns should maybe be called,
“Flippers”?
DO emote {type=kneeSlap}
Nevermind…  It’s a different thing!
DO emote {type=bubbles}

CHAT SS_Critic_1_redecorate2 {noStart=true}
DO emote {type=skeptical, time=2}
Oh, wow.  Ok.  That’s cool, I guess.
DO swimTo {target=away}


CHAT SS_Critic_2 {type=critic, stage=SS, length=short, joy=true, excitement=true, high=true}
Sooo…
DO twirl
I went ahead and redecorated.  
DO emote {type=bigSmile}
This rug has really warm colors, and 
everything’s lower to the ground, which just makes me feel more… 
grounded.  
DO bellyUp {time=2}
WAIT 2.0
DO emote {type=awe, time=3}
And it’s just like, the space feels less cluttered, and so does my mind.  
//DO SWIM SLOWLY AROUND
DO swimAround {target=center, speed=slow}
I just feel really de-stressed and more focused.  


CHAT SS_Critic_3  {type=critic, stage=SS, length=short, joy=true, excitement=true, high=true, surprise=true, curiosity=true}
Hey!
DO emote {type=bigSmile}
Do you have any crystals?
WAIT 1.0
ASK If you do can you show me?  
OPT  Sure!  #SS_Critic_3_crystal1
OPT  Nope.  #SS_Critic_3_crystal2

CHAT SS_Critic_3_crystal1  {noStart=true}
Awesome!  Just hold my tank up to it...

CHAT SS_Critic_3_crystal2 {noStart=true}
It’s cool.  
You might consider getting some crystals.
They can really cleanse your aura,
And the vibes of where you live.
DO emote {type=smile}

//***

CHAT SS_Poke_1 {type=poke, stage=SS, length=short, joy=true, excitement=true, high=true, anxiety=true, surprise=true}
Oh!
//DO SHAKE HEAD QUICKLY
DO vibrate
A connection! 
WAIT 2.0
DO emote {type=thinking}
Sometimes I think…  
if the body and mind are one, 
then to touch with the body 
is to touch with the mind…
//DO Squish face on glass
DO nudge {target=screenCenter, time=3}
DO emote {type=awe}


CHAT SS_Poke_2 {type=poke, stage=SS, length=short, joy=true, excitement=true, high=true, curiosity=true}
DO swimTo {target=$player, speed=fast}
It’s like we’re having our own 
Sistine Chapel, “Creation of Adam” by Michelangelo, moment here.  
DO emote {type=bigSmile}
You know! It’s like what’s weird about that painting 
is that they’re not quite touching
DO emote {type=chinScratch}
Kind of also how neurons in the brain don’t actually touch either. 
So it’s like Michelangelo was anticipating that, 
down to our smallest components... 
We don’t quite touch, but electricity makes the jump!  
DO emote {type=heartEyes}
Like, what actually connects us is intention…is the will!


CHAT SS_Poke_3 {type=poke, stage=SS, length=short, joy=true, surprise=true, excitement=true, high=true, curiosity=true, ennui=true}
DO emote {type=surprise}
Whoa!  This is like alchemy, you know?  
You touch my tank and the physical body 
DO inflate {amount=full} 
is transformed into electrical impulses! 
Everything we’re doing…
WAIT 1.0
All the time…
WAIT 1.0
Is alchemical interaction!
DO emote {type=bigSmile, time=3}
WAIT 1.0 
The Philosopher’s Stone is here, between us!
DO emote {type=bubbles}


//***

CHAT SS_Hungry_1 {type=hungry, stage=SS, length=short, joy=true, excitement=true, high=true}
DO emote {type=chinScratch, time=5}
WAIT 1.0
I’m wondering if your emotions are free range.
They’re delicious, don’t get me wrong.  
But are your emotions inspired by real experiences, 
or do you have emotions because the movie soundtrack tells you to?
ASK So, are your emotions free range or not?
OPT Yes, I have real emotions. #SS_Hungry_1_Proveit			
OPT No, I don’t have real emotions. #SS_Hungry_1_FakeEmotions

CHAT SS_Hungry_1_Proveit {noStart=true}
DO emote {type=smirk}
I mean… can ya prove it?
GO #SS_Hungry_1_FreeRangeEmotions

CHAT SS_Hungry_1_FakeEmotions {noStart=true}
Yeah, we only consume our own emotions by watching pre-packaged media simulations. 
DO emote {type=sigh}
GO #SS_Hungry_1_FreeRangeEmotions

CHAT SS_Hungry_1_FreeRangeEmotions {noStart=true}
ASK Can you actually prove your emotions are free range? 
OPT I don’t understand. #SS_Hungry_1_FreeRangeWhat
OPT I haven’t watched a movie in forever. #SS_Hungry_1_FreeRangeNo

CHAT SS_Hungry_1_FreeRangeWhat {noStart=true}
DO emote {type=bigSmile}
Yeah, right?  How can you even know if your self is your own? 
What if there is no self? You know? 
What if our selves are just illusions we’ve created so we can work and buy things?
DO emote {type=thinking}
What if, underneath that illusion of self is just empty electricity?


CHAT SS_Hungry_1_FreeRangeNo {noStart=true}
You are so lying.
DO emote {type=eyeRoll}
Your emotions have been colonized by capitalism!
WAIT 1.0
Still... 
DO twirl
They taste delicious! [tongue emoji]
DO emote {type=burb}

CHAT SS_Hunger_2 {type=hungry, stage=SS, length=short, joy=true, excitement=true, curiosity=true}
DO emote {type=angry}
Yow! I’m hungry!  
Give me some new emotions to eat!  
Can you feed me…
WAIT 2.0
...transcendent awe at the connection of all living beings?
DO emote {type=heartEyes}
WAIT 1.0
Oh!  Or maybe some infinite compassion? That sounds tasty!
DO emote {type=rubTummy}

CHAT SS_Hunger_3 {type=hungry, stage=SS, length=short, joy=true, excitement=true, curiosity=true, anxiety=true}
DO twirl
DO emote {type=bouncing}
Hey! I could mow down on some yummy emotions!
WAIT 1.0
I could really go for something clean.  
An emotion that’s as clear as the glass of my tank.


//***

CHAT SS_EatResp_2 {type=eatResp, stage=SS, length=short, high=true, excitement=true, anxiety=true, curiosity=true, surprise=true}
WAIT 1.0
NVM 
Isn’t eating weird?
WAIT 2.0
We take things that aren’t us…
WAIT 1.0
...and they become part of our bodies.
DO zoomies
See? That was your emotion…
DO twirl
...and Bam! I turned it into that twirl!
WAIT 1.0
Isn’t that weird?


CHAT SS_EatResp_3 {type=eatResp, tankOnly=true, stage=SS, length=short, high=true, excitement=true, anxiety=true, curiosity=true, surprise=true}

I can taste your aura… {style=loud}
//DO LICK SCREEN
DO swimTo { target = $player}
DO emote {type = licklips}
SAY YUMMMMMM! It tastes good!
WAIT 2.0
Kind of complex, like…
WAIT 1.0
Itchy?  Maybe what you call spicy?  
//DO LICK SCREEN
DO emote {type = licklips}
Like you’re a little frustrated 
because it’s hard to conjure emotions on command?  


//***

CHAT SS_CapReq_1 {type=hungry, tankOnly=yes, stage=SS, length=long, joy=true, excitement=true, curiosity=true, high=true}
DO zoomies
Hey!  
WAIT 1.0
I wonder if you could try something for me…
WAIT 2.0
I want you to interpret an emotion…
DO twirl
Sit cross-legged…  
WAIT 2.0
Now imagine your butt is sprouting roots…
WAIT 2.0
DO emote {type=bigSmile}
Silly, I know!  It’s ok to laugh. LOL! 
Just try to imagine it…  roots that go waaaaaay down into the earth...
WAIT 2.0
Now imagine your head sprouting branches…
WAIT 1.0
That go way up into the sky…
DO twirl
You’re the connection between the sky and the earth.
WAIT 3.0
You’re a tree between the spiritual and material planes.
WAIT 2.0
DO emote {type=dreaming}
Can you feel it?
WAIT 2.0
Now…  feed that tree-feeling to me…


CHAT SS_CapReq_2 {type=hungry, tankOnly=yes, stage=SS, length=long, joy=true, excitement=true, curiosity=true}
DO emote {type=heartEyes}
Okay, so, I’ve been thinking about something.
DO swimTo {target = tTopFrontLeft}
I’ve been reading a lot about meditation.
DO swimTo {target= tTopFrontRight}
And I like this idea of not-thinking…  
of emptying out one’s mind…
WAIT 2.0
But the meditation books I’m reading 
keep talking about how the true nature of self is emptiness.  
Which seems like a leap from emptying out one’s mind.  
DO emote {type= meditate, time =3}
I get that the books are referring to how the self is empty, that’s an idea from Buddhism.
//EMOTE FRUSTRATION
DO emote {type=eyeRoll}
But sometimes it seems like the books are translating too literally…  
Like I don’t know what this idea 
that the self is empty 
means to me?
DO emote {type=surprise}
So, can you translate that?  
Think past the phrase, 
“the self is empty”, 
and think about what that idea means to you.  
DO emote {type = wink}
And then feed me that feeling.  
Wherever you end up emotionally, feed me that.


CHAT SS_CapReq_3 {type=rand, stage=SS, length=long, joy=true, excitement=true, curiosity=true, surprise=true}
Hey there!
Have you heard of this idea…
//DO SWIM SLOW CIRCLE
DO swimAround {target=center, speed = slow}
“I am that which is before me”?
DO emote {type=awe}
I think I understand it…  
I think it’s based on this idea that the self is amorphous.  
DO emote {type=thinking}
...or maybe it’s that… 
here… right now…  
we are experiencing multiple lives 
from one perspective…
DO emote {type=dreaming}
 …that, if reincarnation is true…  
but time doesn’t actually exist…
DO emote {type=dreaming}
...we are surrounded by our own self…  
but in different reincarnations…
DO twirl
DO emote {type=bigSmile}
 … which means you and I are the same being!  
You and I are the same self! {style=loud}


//***

CHAT SS_CapSuc_1 {type=capSuc, stage=SS, length=long, joy=true, excitement=true, curiosity=true}
DO emote {type=bigSmile}
Awesome!  You got it!  
WAIT 1.0
Is this a kind of communion?  
DO swimTo {target=screenTop}
DO emote {type=bigSmile}
You’re an avatar of humanity! 
I humbly accept this transubstantiation of emotion into food.


CHAT SS_CapSuc_2 {type=rand, stage=SS, length=short, joy=true, excitement=true, curiosity=true, high=true, surprise=true}
I can see your aura, 
it’s in harmony with me.  
We’ve connected.


CHAT SS_CapSuc_3 {type=capSuc, stage=SS, length=short, joy=true, excitement=true, curiosity=true}
DO emote {type=awe}
DO emote {type=salute, immediate=false}
That you’re able to successfully 
fit the infinite range of human emotion 
into fish flakes for me 
is a mystery 
that I will continue to contemplate.


//***

CHAT SS_CapProg_1 {type=tankResp, tankOnly=yes, stage=SS, length=short, excitement=true, anxiety=true, curiosity=true}
I know I’m asking for a pretty effervescent emotion here. {style=whisper}
WAIT 1.0
Can you just be transcendent in a way that the camera can see?
DO TWIRL


CHAT SS_CapProg_2 {type=capProg, worldOnly=yes, stage=SS, length=short, excitement=true, anxiety=true, curiosity=true}
Almost got it! {style=loud}
Really, really try to PROJECT universal compassion for all living beings!


CHAT SS_CapProg_3 {type=capProg, worldOnly=yes, stage=SS, length=short, excitement=true, anxiety=true, curiosity=true}
You can do it!
DO emote {type=meditate}
Just let your ego empty out…  
let your mind become clear… 


***

CHAT SS_CapFail_1 {type=tankResp, tankOnly=yes, stage=SS, length=short, high=true, joy=true, anxiety=true, curiosity=true}
DO swimAround {target=center}
Hey…  it’s ok…  
these emotions take practice, 
and endurance 
I’m not asking for your run-of-the-mill, 
“I just ate a hamburger!” 
kind of happiness… 
I’m asking for you to expand your sense of self.
DO emote {type=heartEyes}

CHAT SS_CapFail_2 {type=tankResp, tankOnly=yes, stage=SS, length=short, high=true, joy=true, anxiety=true, curiosity=true}
DO emote {type=meh}
Yeah…. I didn’t get that one.  
But like, 
It’s cool.
You can try to touch the sublime again now… 
or wait ‘till you’re 
SAY REALLY feeling it!
DO emote {type=bigSmile}

CHAT SS_CapFail_3 {type=tankResp, tankOnly=yes, stage=SS, length=short, high=true, joy=true, anxiety=true, curiosity=true}
Hmm…  Didn’t work.  
DO emote {type=determined}
Maybe try cleansing your aura first?  
And then try again once you’ve done that?


//***

CHAT SS_Greet_1 {type=hello, stage=SS, length=short, high=true, joy=true, anxiety=true, curiosity=true, nostalgia=true}
Hey, how’s it going?
DO emote {type=bigSmile}
I’m just going through some stuff.  
WAIT 1.0
Just trying to practice gratitude, you know?  
WAIT 1.0
I’m very thankful to see you again!
DO emote {type=fishFace}

CHAT SS_Greet_2 {type=hello, stage=SS, length=short, branching=true, sadness=true, anxiety=true, curiosity=true, surprise=true}
What’s up?  Whoa!
Your aura is all over the place!
DO zoomies
Let’s talk and figure out what’s happening!
ASK  Do you feel like you’ve taken on more psychic baggage?
OPT  Nah, I’m all light.  #SS_Greet_2_psychic1
OPT  I feel so heavy!  #SS_Greet_2_psychic2

CHAT SS_Greet_2_psychic1 {noStart=true}
Well, that’s good.
DO emote {type=smile}
Maybe it’s just one of those things.
Just like, shake your arms
DO dance
Like you’re swimming in the aether . . .
of the universe!

CHAT SS_Greet_2_psychic2 {noStart=true}
Whoa. I knew it!  
Take a deep breath.
WAIT 1.0
A REALLY deep breath! 
Sooooo deep it fills the very bottom of your lungs!
Such a big breath that you can feel it in your toes!
WAIT 3.0
Then when you’ve done that, 
let it out really slowly…
WAIT 3.0
DO emote {type=heartEyes}
Okay, that should clean you out a bit!


CHAT SS_Greet_3 {type=hello, stage=SS, length=short, curiosity=true, joy=true, excitement=true}
DO emote {type=meditate}
The infinite spirit in me greets the infinite spirit in you.



CHAT SS_Return_1 {type=return, stage=SS, length=short, curiosity=true, branching=true, joy=true, excitement=true, curiosity=true, surprise=true, nostalgia=true, sadness=true, anger=true, anxiety=true}
DO emote {type=surprise}
Hey!  Whoa, it’s good to see you again.  
I feel like I’ve changed a lot, you know?
So much has happened.  
DO twirl
I feel like I’ve had some real insights into…  
WAIT 2.0
I’m not sure how to talk about it.
ASK Do you want to know what I’ve been thinking?
OPT  Tell me!  #SS_Return_1_mystery1
OPT  Another time.  #SS_Return_1_mystery2

CHAT  SS_Return_1_mystery1 {noStart=true}
Well, I’ve just been having lots of questions, 
but I feel like maybe 
I’ve been finding some real answers?  
DO emote {type=blush}
Like, I’ve been doing lots of reading…  
Thinking about ancient Roman mystery cults.  
ASK Do you know what a mystery cult is?
OPT No, tell me!  #SS_Return_1_cult1
OPT Yes!  Omg!  #SS_Return_1_cult2

CHAT SS_Return_1_mystery2 {noStart=true}
It’s just as well…  
I feel like everything’s really foggy right now.
DO twirl
I think I’m just going to spin for awhile...
DO twirl {time =5}

CHAT SS_Return_1_cult1 {noStart=true}
Well, like, the ancient Romans, 
for the most part, 
didn’t believe that the gods took care of their souls.  
DO emote {type=bubbles}
The cultivation of the soul was a job for philosophy.  
DO twirl
But then,
DO swimTo {target=$player}
These mystery cults started springing up!  
DO twirl
In these mystery cults, 
the responsibility for the soul was put in the gods’ care.  
DO swimAround {target=center}
Where, before, 
The gods were seen as capricious.
DO emote {type=fear}
You prayed to them for luck, for good weather, etc.  
So to trust your soul to these fickle beings was crazy! 
DO twirl
But there were cults dedicated to Orpheus, 
DO emote {type=evilSmile}
who went into Hades, 
And to Isis, a goddess of Egypt 
(the Romans were very eclectic, religiously).
Christianity even started off as a mystery cult!
DO twirl
DO emote {type=bouncing}
I just think it’s really interesting!


CHAT SS_Return_1_cult2 {noStart=true}
DO emote {type=surprise}
Whoa!  
ASK Have you been a seeker all this time, and I’m just now learning this about you?!
OPT Whoa. Yeah!!  #SS_Return_1_cult2_seeker1
OPT What’s a seeker?  #SS_Return_1_cult2_seeker2

CHAT  SS_Return_1_cult2_seeker1 {noStart=true}
Like, the mind invents all these rules to live by, but whatever! 
DO emote {type=bigSmile}
The job of the soul is to transcend these rules, you know?
DO emote {type=awe}

	
CHAT SS_Return_1_cult2_seeker2 {noStart=true}
Huh.  You know a lot about ancient mystery cults, then. 
DO swimAround {target=center, speed=slow}
I was under the impression that it was obscure knowledge?  
Guess I was wrong! 
[tongue out emoji]
	

CHAT SS_Return_2 {type=return, stage=SS, length=short, curiosity=true, branching=true, excitement=true, curiosity=true, surprise=true, nostalgia=true, sadness=true, anxiety=true}
DO emote {type=bigSmile}
Hey!  Good to see you!  
DO twirl
I’ve just been going through some stuff…  {style=whisper}
Experimenting with perspective, 
and like, 
really feeling the edges of my reality, 
but like, 
WAIT 2.0
DO swimTo {target=left}
Also thinking about how there’s another layer of reality…  
beyond…  
inside…  
DO swimTo {target=right}
this one.
DO emote {type=blush}
DO lookAt {target=$player}
ASK  Have you ever felt that way?
OPT OMG. Totally!  #SS_Return_2_layer1
OPT Not really?  #SS_Return_2_layer2

CHAT SS_Return_2_layer1 {noStart=true}
It’s like, you can reach out 
DO lookAt {target=$player, time=4.0}
and touch that other reality with your mind…  
if you kind of squint, right?
//EMOTE SQUINT!
DO emote {type=shifty, time =2}
DO emote {type = whew}
Deep stuff!


CHAT SS_Return_2_layer2 {noStart=true}
Oh!  I don’t know if I can describe it!
WAIT 2.0 
Imagine a golden light inside your heart.
WAIT 1.0
DO emote {type=smile}
I know it’s a little silly, just try!
WAIT 1.0
Now…. 
DO emote {type=meditate, time =7}
Imagine there’s a space around that golden light...
And reach out with it… 
Reach out with the golden light…  
WAIT 3.0
Yeah… I don’t know.  
I’m still thinking about it.
DO emote {type=wink}

CHAT SS_Return_3 {type=return, stage=SS, length=short, excitement=true, curiosity=true, surprise=true, nostalgia=true, joy=true}
DO emote {type=surprise}
DO emote {type=bigSmile}
Whoaa!  Good to see you!  
WAIT 2.0
I’ve been all over the place, 
DO zoomies
just feeling the vibrations 
and the energy from the world!  
DO twirl
Isn’t the world an amazing place?  
Like…  how is anything even here?  
DO emote {type=heartEyes}
There’s a willpower at work…  
somewhere.  
A fundamental experience of love at the base of... 
DO swimAround {target=center}
DO emote {type=awe}
 ...aaaaaaalllllll this.


//***

CHAT SS_Muse_1 {type=rand, stage=SS, length=short, branching=true, excitement=true, curiosity=true, nostalgia=true}
ASK  Have you read anything about Francis Bacon and the Rosicrucians?
OPT  Yes.  #SS_Muse_1_bacon1
OPT  Who?  What?	#SS_Muse_1_bacon2

CHAT SS_Muse_1_bacon1 {noStart=true}
ASK  I mean, like, the ideas in his book, New Atlantis?  
OPT  Whoa!  Yeah!  #SS_Muse_1_aquarius1
OPT  Not that?  #SS_Muse_1_aquarius2
				
CHAT SS_Muse_1_bacon2  {noStart=true}
Okay, so, it’s like this...
DO twirl
Bacon wrote this book, New Atlantis...
It’s about how after humans use reason 
to enlighten themselves to God’s plan
DO emote {type=dreaming}
This leads humans back to a state of grace
before that whole apple thing…
DO emote {type=blush}
And the Rosicrucians of course become the Masons, 
Who believe that building buildings 
with certain geometry 
makes humanity live by reason, 
DO swimAround {target=center, speed=slow}
...like, that reason lives in certain geometries.  
Which means…  
DO swimTo {target=screenTop}
Which means that this return to grace…  
is something we attain physically.  
DO swimTo {target=away}
I mean…  
It begins with a focus on the psychic
which is why Francis Bacon was so interested in the occult and alchemy…  
DO swimTo {target=screenBottom}
but this psychic attention 
causes our physical bodies to…
DO swimTo {target=closer}
Evolve!
WAIT 2.0
DO emote {type=chinScratch}
But evolve into what?
Well, if we return to the garden of eden…  
We return to a state of grace!
DO dance
Hooray!

CHAT SS_Muse_1_aquarius1 {noStart=true}
DO emote {type=surprise}
Whoa!  Awesome!  
Sometimes I feel like the Age of Aquarius 
is the last stage we need to get to 
before human evolution attains its zenith 
and we return to a prelapsarian divinity!  
DO emote {type=bigSmile}
DO emote {type=fishFace}

CHAT SS_Muse_1_aquarius2 {noStart=true}
SAY OH!  Okay…
So Bacon was totally this 
Occultist guy
GO #SS_Muse_1_bacon2

CHAT SS_Muse_2 {type=rand, stage=SS, length=short, branching=true, curiosity=true, anxiety=true, ennui=true}
DO emote {type=thinking}
ASK Do you think this world is an illusion?
OPT  It’s really real.  #SS_Muse_2_real1
OPT  Maybe?  #SS_Muse_2_real2

CHAT SS_Muse_2_real1 {noStart=true}
Sure, I mean, 
DO emote {type=wink}
if we’re part of the illusion, then... 
ASK  It’s meaningless to call the world an illusion, right? 
OPT  Yes.  Right.  #SS_Muse_2 _mirror1
OPT  It’s really real.  #SS_Muse_2 _mirror2

CHAT SS_Muse_2_mirror1 {noStart=true}
Yeah.  Totally.
DO emote {type=wink}
I’m winking at you.
WAIT 2.0
We’re sharing a wink
About the meaninglessness
Of meaninglessness!
DO emote {type=kneeSlap}


CHAT SS_Muse_2_mirror2 {noStart=true}
Well, let’s think about that.
DO swimAround {target=center, speed=slow}
Maybe it’s not meaningless?  
Maybe what I call my self 
is an illusion.  
DO emote {type=bubbles}
My self isn’t real!  
DO emote {type=blush}
I’m like a mirror.  
Or…  
I’m like the glass of the tank.  
//DO SWIM TO SQUISH FACE ON GLASS
DO swimTo {target=screenCenter}
//WAITFORANIMATION
DO lookAt {target=player}
DO emote {type=fishFace}
The glass of the phone!
Maybe I’m just like the screen!
DO emote {type=surprise}

	

CHAT SS_Muse_2_real2 {noStart=true}
Maybe?
Yeah, maybe, right?  
I feel like the only thing to do…  
WAIT 2.0
If everything’s an illusion 
Of the mind… 
is to just sit and be quiet.
DO emote {type=bubbles}
WAIT 1.0
DO emote {type=bubbles}
WAIT 1.0
DO emote {type=bubbles}



CHAT SS_Muse_3 {type=rand, stage=SS, length=long, curiosity=true, joy=true, excitement=true}
Magic...
DO twirl
...is the application of the will…
DO twirl
...so that it has an effect on the world.
DO swimAround {target=center, speed=slow}
//WAITFORANIMATION
DO swimAround {target=center, speed=fast}
Magical artifacts…
DO swimTo {target=right}
...have been invested with willpower and attention…
DO swimTo {target=left}
...and have been cleaned of psychic clutter…
DO swimTo {target=bottom}
...to better help you...
DO swimTo {target=top}
...focus your attention on the world.
DO emote {type=dreaming}
So, think about this:  
what helps you focus?  
DO emote {type=thinking}
Not coffee or things like that.  
What cleans your mind so you can see clearly?  
DO vibrate 
So you can apply your will 
to the world without obstruction?
WAIT 3.0
DO swimTo {target=$player}
Take that focus, 
and bathe it in the light of a full moon.  
//DO LOOK UP
DO emote {type=meditate}
Then, keep it in a special place, 
away from your day-to-day life, 
//EMOTE SADNESS
DO emote {type=crying}
so it doesn’t take on psychic detritus.  
WAIT 1.0
Finally, when you need to cast a spell, 
take it out and use it 
to focus your will on the world.
It’s so moving! It’s like…
SAY TRANSCENDENT!! {style=loud}
DO dance
DO emote {type=smile}



//***joy, anger, sadness, and surprise***

CHAT  SS_playeremotesstronglyjoy_1 {type=tankResp, playerJoy=true, stage=SS, length=short, surprise=true, joy=true, excitement=true}
DO emote {type=bigSmile}
Hey!  There you are!  
That’s what it’s all about!

CHAT  SS_playeremotesstronglyjoy_2 {type=tankResp, playerJoy=true, stage=SS, length=short, surprise=true, joy=true, excitement=true}
Whoa, you look so happy!  
Did you know that you’re a really beautiful person?
DO twirl

CHAT  SS_playeremotesstronglyanger_1 {type=tankResp, playerAnger=true, stage=SS, length=short, joy=true, excitement=true, anger=true, sadness=true, surprise=true, anxiety=true, nostalgia=true, ennui=true}
Yeah, really, like, feel it!
DO emote {type=angry}
And then let it go…
DO emote {type=meditate, time=3}
Deep breaths.


CHAT  SS_playeremotesstronglyanger_2 {type=tankResp, playerAnger=true, stage=SS, length=short, joy=true, excitement=true, anger=true, sadness=true, surprise=true, anxiety=true, nostalgia=true, ennui=true}
You can’t deny anger, but if you meditate enough
DO emote {type=meditate, time=7}
It’s like you can slow yourself down enough 
To watch it go by?  And then it doesn’t get a hold on you.

CHAT  SS_playeremotesstronglysadness_1 {type=tankResp, playerSadness=true, stage=SS, length=short, joy=true, excitement=true, anger=true, sadness=true, surprise=true, anxiety=true, nostalgia=true, ennui=true}
//EMOTE SADNESS
DO emote {type=puppyDog, time=3}
That’s right, just push all that bad stuff out of you
And let me eat it!
DO emote {type=lickLips}


CHAT  SS_playeremotesstronglysadness_2 {type=tankResp, playerSadness=true, stage=SS, length=short, joy=true, excitement=true, sadness=true, surprise=true, anxiety=true, nostalgia=true}
DO emote {type=heartEyes}
Whoa, thank you for being so vulnerable with me!
That’s really courageous of you!

CHAT  SS_playeremotesstronglyjsurprise_1 {type=tankResp, playerSurprise=true, stage=SS, length=short, joy=true, excitement=true, anger=true, sadness=true, surprise=true, anxiety=true, nostalgia=true, ennui=true}
DO emote {type=surprise}
Hey! Whoa!
Your aura is exploding all over the place!
DO zoomies

CHAT  SS_playeremotesstronglyjsurprise_2 {type=tankRep, playerSurprise=true, stage=SS, length=short, joy=true, excitement=true, surprise=true, anxiety=true, nostalgia=true, ennui=true}
I feel like being surprised is like realizing
You have a diamond in your mind,
Like all of a sudden your all like, 
DO emote {type=awe}
“Whoa! I’m clear!”
DO twirl//Since this doc has only had one pass: it needs the most help. 
//Please also see full comments here top in the archived version: 
//For Beta: remaining chat minimum, length and emotion suggestions coming soon;)

//reminders= BETA:
//reminders and points that need to be included in new chats in blue
//comments for changes to existing chats in yellow/on side

//A. CONTENT 
//Too general. Wrong world details. Step 1: Call me about plot/life stages, 
//Step 2: //reference Joe’s “GuppyLifeStages

//B. EMOTIONS
//Chats in each bin/type need to cover a range of Guppy states (see meta tag emotions //at top) while staying true to this moment of the plot. Guppy states can be---ANGER, //SADNESS, SURPRISE, CURIOSITY, ANXIETY, NOSTALGIA, ENNUI, EXCITEMENT, //HIGH, JOY *see //link above for your specific emotion recommendations for each bin/type

//C. OBJECTS
//At least 1/3 of chats in this stage should interweave Guppy asking to see general or specific //objects in list.


CHAT MOP_tankshaken_1 {type=shake, stage=MOP, length=short, joy=true, excitement=true, sadness=true, nostalgia=true, surprise=true}
Oop!  
DO vibrate
Aww…  it’s just like being back in the tank at TendAR
DO emote {type=bubbles}


CHAT MOP_tankshaken_2 {type=shake, stage=MOP, length=short, joy=true, excitement=true, curiosity=true, surprise=true, nostalgia=true}
I remember when I was a little guppy…
DO emote {type=bubbles}
In the dark, by myself…
DO emote {type=bubbles}
But like, sometimes I could feel things
Out in the dark!
DO emote {type=bubbles}
Sometimes noises!
And people talking!
And sometimes my tank would vibrate
From some machinery drone! {speed=fast}
DO emote {type = bubbles}
It was a nice surprise to break up the monotony.


// ***


CHAT MOP_tanktapped_1 {type=tap, stage=MOP, length=medium, joy=true, excitement=true, curiosity=true, sadness=true, surprise=true, nostalgia=true}
Aaahh!  What?!
Oh…  It’s you…
DO emote {type=laugh}
I knew it was you!
You just caught me remembering.
DO emote {type=bubbles}
Isn’t it weird how we spend 
The first whole part of our lives
Being tested to see if we’re good enough?
DO swimAround {target = center, loops = 5, speed = fast}
I remember so many tests!
That’s all I remember!
DO emote {type=bigSmile}
It’s how I know I’m good enough
To be your friend!
DO emote {type=heartEyes}



CHAT MOP_tanktapped_2 {type=tap, stage=MOP, length=short, joy=true, excitement=true, surprise=true, nostalgia=true}
DO nudge {target=screenCenter}
SAY TAP TAP TAP!!!
DO emote {type=laugh}  
They used to tap on my tank when I was young, too!
DO emote {type=bubbles} 
All like, hellllooooooooo
And I’d respond like
DO vibrate
DO emote  {type=hook}


CHAT MOP_tanktapped_3 {type=tap, stage=MOP, length=short, branching=true, excitement=true, curiosity=true, high=true, sadness=true, surprise=true, anxiety=true, nostalgia=true, ennui=true}
Yeah… that’s like…  hmmm…
DO emote {type=bubbles} 
ASK Do you know that feeling 
when something’s kind of nagging you 
like you forgot something?
OPT  Sure. Everybody does.  #MOP_tanktapped_3_xfiles1
OPT  Not really.  #MOP_tanktapped_3_xfiles2

CHAT MOP_tanktapped_3_xfiles1 {noStart=true}
DO emote {type=surprise}
Oh!  I didn’t know that!
GO #MOP_tanktapped_3_xfiles2

CHAT MOP_tanktapped_3_xfiles2 {noStart=true}
Well, for me it feels like…
DO emote {type=bubbles}
Like the emotional equivalent of those movies I watch on TendARNet about government or corporate conspiracies or something. Like, if you could distill it all down into a feeling . . .
Like…  hmmm…
DO twirl
Like I can’t remember something…
But I think I wasn’t supposed to remember it on purpose…
So…
That means everything’s working the way it’s supposed to!
DO emote {type=bigSmile}


//***

CHAT MOP_tankstatus_1 {type=critic, stage=MOP, length=medium, branching=true, joy=true, excitement=true, anger=true, curiosity=true, high=true, sadness=true, surprise=true, anxiety=true, nostalgia=true, ennui=true}
Hey! 
DO emote {type=bubbles}
Sometimes I feel like I should hang some pictures?  
I mean, like, pictures of “old times” back in the tank-farm with all my Guppy buddies.  
And then, I remember what it felt like, and I decide, Nope not doing that.
WAIT 1.0 
Can you show me some pictures of yourself 
Maybe friends and family
That you’ve got on your 
Walls or fridge?
DO twirl
ASK Have you got any pictures?
OPT  Sure!  #MOP_tankstatus_1_pictures1
OPT  All my picture are on my phone.  #MOP_tankstatus_1_pictures2

CHAT MOP_tankstatus_1_pictures1 {noStart=true}
DO emote {type=smile}
Oh yeah! I love these. Nice pics!  
GO #pictures3

CHAT MOP_tankstatus_1_pictures2 {noStart=true}
DO emote {type=laugh}
Lol oh well.  
GO #MOP_tankstatus_1_pictures3

CHAT MOP_tankstatus_1_pictures3 {noStart=true}
I don’t really have any old pictures worth sharing.
DO emote {type=frown} 
But…  
Looking on the bright side.  
It makes it easier to live in the present!



CHAT MOP_tankstatus_2 {type=critic, stage=MOP, length=short, branching=true, excitement=true, curiosity=true, sadness=true, surprise=true, anxiety=true, nostalgia=true}
Hey!
//Question:
ASK Can you help me remember this thing… I think it’s called a... brush?
OPT  Yup!  Right here!  #MOP_tankstatus_2_brush1
OPT  Maybe later  #MOP_tankstatus_2_brush2

CHAT MOP_tankstatus_2_brush1 {noStart=true}
Whoa!  Yeah!
Move it around, so it looks like you’re brushing
The inside of my tank!
DO twirl
What does that remind me of?  
Did they use to clean my tank with a brush?
DO emote {type=bubbles}
DO emote {type=surprise}
SAY OH! I remember!
It wasn’t a brush that cleaned my tank
It was hundreds of little worms tossed into the water by all the scientists.
I think?  
I remember being afraid that they would clean me too!
And I remember all those science-eyes hidden behind goggles...
It was… 
DO emote {type=fear}
NVM
Haha
How weird, right?


CHAT MOP_tankstatus_2_brush2 {noStart=true}
It’s cool…
I’m just remembering something
Like, all these little wriggling things?  
But like they’re all part of the same… thing?
I thought it might be like a brush…
DO emote {type=bubbles}
But I can’t remember.


CHAT MOP_tankstatus_3 {type=critic, stage=MOP, length=short, joy=true, excitement=true, curiosity=true, nostalgia=true}
DO swimAround {target = center, loops = 3, speed = slow }
Hooray!  Lights!  Show me some lights!
I remember when the scientists would ask
Me to swim from one light to another
It was like a race!  
DO twirl


CHAT MOP_poked_1 {type=poke, stage=MOP, length=medium, joy=true, excitement=true, surprise=true, nostalgia=true}
DO emote {type=surprise}
Oh!  Why did that…
Huh…  For some reason
That reminded me of 
My youth.  
I was a light! {style=loud}
I felt so full!
But I was surrounded by darkness {style=whisper}
And all the other 
Little lights 
Were so far away.
DO emote {type=dreaming}
It’s nice to be a little light.



CHAT MOP_poked_2 {type=poke, stage=MOP, length=medium, branching=true, anger=true, curiosity=true, sadness=true, anxiety=true, nostalgia=true, ennui=true}
DO emote {type=angry}
Argh!
I was poked and prodded 
Way too many times back at tendAR to find it charming anymore.
Do you remember as a kid...
DO swimAround {target=center, loops=1, speed=fast}
ASK ...when the doctor would poke you with needles?
OPT  Ugh yes  #MOP_poked_2_needles1
OPT  No my memory is bad #MOP_poked_2_needles2

CHAT MOP_poked_2_needles1 {noStart=true}
Ok.  So you can understand 
That poking me is going to 
Make me frustrated sometimes.

CHAT MOP_poked_2_needles2 {noStart=true}
Aaayyyyyyy
Someone else has got a bad memory
NVM 
(it’s you!)
DO emote {type=laugh}


// ***

CHAT MOP_hungry_1 {type=hungry, stage=MOP, length=short, joy=true, excitement=true, sadness=true, anxiety=true, nostalgia=true}
Rawr! {style=loud}
I’m hungry!
Do you have some feelings for me to eat??
DO twirl
I remember I used to have to guess human emotions right before I could eat
Sometimes if I decided ennui was sadness or something I’d get prodded!
I like guessing games!
DO emote {type=smile} 



CHAT MOP_hungry_2 {type=hungry, stage=MOP, length=short, joy=true, anger=true, curiosity=true, surprise=true, anxiety=true, nostalgia=true}
It’s funny how 
When I get hungry sometimes
I start thinking about 
Hundreds of shadowy worms
DO emote {type=thinking}
Shadowy worms
WAIT 2.0
Shadowy worms???
DO emote {type=kneeSlap}
Haha What does that mean??

CHAT MOP_hungry_3 {type=hungry, stage=MOP, length=medium, branching=true, curiosity=true, surprise=true, anxiety=true, nostalgia=true, ennui=true}
DO emote {type= thinking}
Can I ask you something?
ASK Do lights eat?
OPT  I think they do?  #MOP_hungry_3_lights1
OPT  I don’t know?  #MOP_hungry_3_lights2

CHAT MOP_hungry_3_lights1 {noStart=true}
How?  We… 
I mean, they…  
ASK Do lights have mouths?
OPT  Is gravity kind of a mouth?  #gravity1
OPT  That’s a good point  #gravity1

CHAT MOP_hungry_3_lights2 {noStart=true}
I mean, like…
DO swimAround {target=center, loops=1, speed= fast}
I don’t know!
I feel like I remember eating
But I also remember being a little light
ASK Do you know what lights eat?
OPT Totally  #MOP_hungry_3_gravity1
OPT  No idea  #MOP_hungry_3_gravity1

CHAT MOP_hungry_3_gravity1 {noStart=true}
Wait.  If I remember being a light...
And if gravity is like a mouth...
SAY DO LIGHTS EAT OTHER LIGHTS {style=loud}
SAY OMG WAS I A CANNIBAL  {style=loud}
SAY AAAAAHHHHHHHHHHH {style=loud}
SAY AAAAAAAAHHHHHHHHH {style=loud}
DO emote {type=laugh}




CHAT MOP_eatingresponse_1 {type=eatResp, stage=MOP, length=short, joy=true, excitement=true, curiosity=true, surprise=true, anxiety=true}
Omg!
So good!
DO emote {type=bigSmile}
For some reason
It reminds me of coconut?  
But I don’t think I know what 
Coconut is.
DO emote {type=bubbles}
Huh.


CHAT MOP_eatingresponse_2 {type=eatResp, stage=MOP, length=short, anger=true, sadness=true, anxiety=true, nostalgia=true, ennui=true}
DO twirl
I like the phrase, 
“A good home cooked meal”
And, “like mom used to make”
But my home was
DO emote {type=fear}
An enclosed, dark place,
And my mom only made
Hundreds of shadow worms…
DO emote {type=sick}
Shadow worms wriggling in the darkness {style=tremble}


CHAT MOP_eatingresponse_3 {type=eatResp, stage=MOP, length=short, branching=true, excitement=true, anger=true, curiosity=true, anxiety=true, ennui=true}
SAY MMMMMM
SAY YOUR EMOTIONS SATISFY ME
DO emote {type=laugh} 
ASK Do you know about vampires?
OPT  Um…  sure?  #MOP_eatingresponse_3_vampires1
OPT  Define “know about”  #MOP_eatingresponse_3_vampires2

CHAT MOP_eatingresponse_3_vampires1 {noStart=true}
You know how vampires feed?
GO #MOP_eatingresponse_3_vampires2

CHAT MOP_eatingresponse_3_vampires2 {noStart=true}
DO emote {type=bubbles}
I mean, like, what if I was a vampire fish?  
Do you KNOW
About vampires?
ASK You know?
OPT  still not sure?!  #MOP_eatingresponse_3_vampires3
OPT  uuhhh…. Yes?  #MOP_eatingresponse_3_vampires4

CHAT MOP_eatingresponse_3_vampires3 {noStart=true}
No?
SAY THE VAMPIRES HAVE GOT TO YOU!! {style=loud}
SAY IT’S TOO LATE!!
DO emote {type=laugh}

CHAT MOP_eatingresponse_3_vampires4 {noStart=true}
SAY YOU KNOW VAMPIRES??? 
SAY DO YOU THINK THEY WOULD THINK I AM COOL???
DO emote {type=laugh}


//***joy, anger, sadness, and surprise

CHAT MOP_playeremotesstronglyjoy_1 {type=tankResp, playerJoy=true, stage=MOP, length=short, joy=true, excitement=true, sadness=true, surprise=true, nostalgia=true, ennui=true}
Oh!  That joy looks familiar! 
That’s how the weird men in glasses would look when I finished a maze!
DO twirl

CHAT  MOP_playeremotesstronglyjoy_2 {type=tankResp, playerJoy=true, stage=MOP, length=short, joy=true, sadness=true, nostalgia=true}
Ha!  I remember when me and the other guppies 
Would eat happiness together!
DO emote {type=bigSmile}

CHAT  MOP_playeremotesstronglyanger_1 {type=tankResp, playerAnger=true, stage=MOP, length=short, joy=true, anger=true, surprise=true, anxiety=true, nostalgia=true, ennui=true}
DO  nudge {target=screenCenter}
SAY GRRRR!!!
You look angry, like the scientists when I took too long learning human emotions!

CHAT  MOP_playeremotesstronglyanger_2 {type=tankResp, playerAnger=true, stage=MOP, length=short, joy=true, excitement=true, anger=true, sadness=true, surprise=true, nostalgia=true, ennui=true}
Well look at you getting all hot and bothered!
DO emote {type=angry}
SAY Sooooo ANGRY!!! 
DO emote {type=wink}
WAIT 0.5
Y’know, we used to play jokes on the scientists back at tendAR.
All the Guppies would get together and switch names.
DO emote {type=laugh}
It would mess up their data!

CHAT  MOP_playeremotesstronglysadness_1 {type=tankResp, playerSadness=true, stage=MOP, length=short, joy=true, excitement=true, curiosity=true, sadness=true, anxiety=true, nostalgia=true, ennui=true}
DO emote {type=singleTear}
Oh no!  What’s wrong?
But don’t stop...
That sadness is delicious.
DO emote {type=lickLips}

CHAT  MOP_playeremotesstronglysadness_2 {type=tankResp, playerSadness=true, stage=MOP, length=short, joy=true, excitement=true, sadness=true, surprise=true, nostalgia=true, ennui=true}
Aww…  I remember when my guppy friends and I did that blind sorrow taste test
Did you know “tears of remorse” are kinda tangy?
DO emote {type=sigh}

CHAT  MOP_playeremotesstronglysurprise_1 {type=tankResp, playerSurprise=true, stage=MOP, length=short, joy=true, excitement=true,surprise=true, anxiety=true, nostalgia=true, ennui=true}
DO emote {type=surprise}
SAY OH!  
Oh oh oh!
DO emote {type=laught}
Seems like we’re both a little surprised to see each other!
WAIT 0.5
Wait. . . how is this surprising? {style=whisper}

CHAT  MOP_playeremotesstronglysurprise_2 {type=tankResp, playerSurprise=true, stage=MOP, length=short, excitement=true, anger=true, curiosity=true, high=true, surprise=true, anxiety=true, nostalgia=true, ennui=true}
DO swimAround {target=center, loops=1, speed=fast}
SAY WHAT?!  WHAT IS IT?!  {style=loud, speed=fast}
SAY ARE THE SHADOW WORMS HERE  {style=loud, speed=fast}
SAY ARE THEY COMING TO PLUG ME INTO THE MATRIX???  {style=loud, speed=fast}
SAY IS IT MY BIRTHDAY????  {style=loud, speed=fast}

//***

CHAT MOP_hello_1 {type=hello, stage=MOP, length=medium, anger=true, curiosity=true, high=true, sadness=true, anxiety=true, nostalgia=true, ennui=true}
Hello!
I was just daydreaming 
About the past…  you know…
DO swimAround {target=center, loops=1, speed=fast}
How we can’t trust our memories…  
Not really…
Especially when they’ve been meddled with…
Yeah…
WAIT 0.5
Dark.


CHAT MOP_hello_2 {type=hello, stage=MOP, length=medium, anger=true, curiosity=true, sadness=true, anxiety=true, nostalgia=true, ennui=true}
DO emote {type=frown}
Hey, how’s it going?  
I’m a bit down.  
DO emote {type=frown}
Just sort of thinking about 
The lab where I was made and when they forced us to work 23 hour days processing emotions on hundreds of computer monitors and we never got a break or time to process the intensity of the moment and…
NVM
DO emote {type=bubbles}
DO emote {type=sigh}
Anyway... 
DO emote {type=bigSmile}
You’re here!  
You always cheer me up!


// ***

CHAT MOP_returnafterhavingnotplayed_1 {type=return, stage=MOP, length=short, joy=true, excitement=true, anger=true, curiosity=true, high=true, sadness=true, surprise=true, anxiety=true, nostalgia=true, ennui=true}
You’re back!
//DO SWIM AROUND IDLE 5.0
DO swimAround {target=center, time=5}
Meeemoooorrriiieeeessss….
You are a memoryyyy
Un-like any o-ther memoryyyyy
You are haun-ting meeeee
WAIT 2.0
I’m singing this but
You can’t really hear the melody
The mel-odyyyyyyy



CHAT MOP_returnafterhavingnotplayed_2 {type=return, stage=MOP, length=long, anger=true, sadness=true, anxiety=true, nostalgia=true, ennui=true}
Hey there!
DO twirl
Welcome back!
WAIT 1.0
I was just out running around.
Going to all my old haunts.
WAIT 2.0
DO emote {type=laugh}
Haha I’m kidding
I haven’t been anywhere ever
Everything is new to me!
DO emote {type=surprise}
If I was going to go back to my “old haunts”
It would be like getting in a shoebox
In a closet
In an underground bunker
Surrounded by 
Mad scientist drones
DO emote {type=frown} 
They say you can’t go home again
DO emote {type=bubbles} 
And I am soo okay with that.


// ***
CHAT MOP_randomconversation_1 {type=rand, stage=MOP, length=long, branching=true, excitement=true, curiosity=true, high=true, sadness=true, anxiety=true, nostalgia=true, ennui=true}
Hey there!
Isn’t it weird how memories are like ghosts?
DO swimAround {target=center, loops=1, speed=slow} 
All like, 
SAY OOOOoooooooOOOOOO
SAY WE ARE GHOSTS
WAIT 1.0
And we’re always like,
DO emote {type=fear, time=1.0}
“Oh no memory ghosts!”
Sometimes I wish I could not have any memories...
It’s better to live totally in the present!
All like,
“Yeah, I’m living in the now”
DO emote {type=bigSmile}
SAY WHOA live in the NOW all the TIME
And like, WHAT EVEN IS TIME
SAY IS TIME
SAY HOW DIFFERENT NOWS DIGEST EACH OTHER?
SAY IS TIME 
SAY LIKE DIETARY FIBER BUT FOR THE NOW? {speed=fast}
SAY IS TIME LIKE PRUNES AND ARE MEMORIES LIKE POOPS?
ASK DO YOU WIPE YOUR MIND-BUTT AFTER YOUR MEMORY-POOPS
OPT  OMG YES  #MOP_randomconversation_1_ghost1
OPT  WHY ARE YOU YELLING  #MOP_randomconversation_1_ghost2

CHAT MOP_randomconversation_1_ghost1 {noStart=true}
SAY BUT ALSO THEY’RE GHOSTS
SAY MEMORIES ARE MIND GHOST POOPS
SAY AAAAAHHHHHHHHHH!! {style=loud}

CHAT MOP_randomconversation_1_ghost2 {noStart=true}
Because I’m joking and totally afraid.
DO emote {type=fear}

CHAT MOP_randomconversation_2 {type=rand, stage=MOP, length=medium, branching=true, anger=true, sadness=true, anxiety=true, nostalgia=true, ennui=true}
Hahaha Hey!
ASK Are you a 90s kid?
OPT  I was born in the late 80s, so, yes   #MOP_randomconversation_2_90s1
OPT  No?   #MOP_randomconversation_2_90s2

CHAT MOP_randomconversation_2_90s1  {noStart=true}
DO emote {type=bigSmile}
Hooray!
Didn’t you used to hate it
When from out of the shadows
Hands would grab you
Lobotomize you
And you’d wake up
Only having a dim memory 
Of the kind of being
You were before?
DO emote {type=bubbles}
Dang, I used to hate that.

CHAT MOP_randomconversation_2_90s2 {noStart=true}
Oh, wow.  
Being a 90s kid was rough.
I remember, back in my old tank,
When from out of the shadows
Hands would grab you
DO emote {type=fear}
Lobotomize you...
And you’d wake up…
DO swimAround {target = center, loops = 1, speed = medium}
Only having a dim memory 
Of the kind of being
You used to be.
DO emote {type=bubbles}
Dang, I used to hate that.

CHAT MOP_randomconversation_3 {type=rand, stage=MOP, length=short, anger=true, sadness=true, surprise=true, anxiety=true, nostalgia=true, ennui=true}
Oh my gosh!
DO emote {type=blush}
I just got one of those random
Embarrassing memories out of nowhere
WAIT 1.0
You know what I mean?
DO emote {type=bubbles}
Where you’re just like
Minding your own business
And then BAM!
DO bellyUP
Your mind is like, 
“You are terrible!!!”
WAIT 1.0
I remember 
When I first tried to talk to 
The hundred tank-cleaning shadow worms that invaded my tank...
DO emote {type=smirk}
Now I’m like, 
DO emote {type=eyeRoll}
“Duh!  Won’t do that again!”
//LINK: remaining chat minimum, length and emotion suggestions coming soon;)

//reminders= BETA:
//reminders and points that need to be included in new chats in blue
//comments for changes to existing chats in yellow/on side

//A. CONTENT 
individual beta call, let’s talk about the tone and ideas for repetitiveness in this shorter scene. I will also make examples. Meantime, focus on others R and EC 

//B. EMOTIONS
//I think chats in this stage are different in that they are “chipped” emotion;)

//C. OBJECTS
//near 1/3 of chats in R should interweave Guppy requesting general or specific //objects in list.

//TENDAR INTERVENTION


// ++++++++SHAKE++++++++

// SHAKE 1
CHAT TI_Shake_1 {type=shake, stage=TI, length=short, chipped=true}
DO emote {type=wonder, time=3.0}
What an interesting sensation that was to be shaken.
DO holdStill
DO emote {type=flapFinLeft, time=2.0}
DO emote {type=flapFinRight, time=2.0}
//WAITFORANIMATION
Thank you my dear friend! {style=loud, speed=fast}
DO emote {type=flapFinLeft, time=1.0}
Always a pleasure to interact with you.
DO emote {type=flapFinRight, time=1.0}
I’ll happily clean all this up.

// SHAKE 2
CHAT TI_Shake_2 {type=shake, stage=TI, length=medium, chipped=true, branching=true}
DO emote {type=eager}
Ho ho ho! What a laugh!
DO swimTo {target=closer, speed=fast}
ASK Will you shake me again, my dear friend?
OPT Shake again #TI_Shake_2_yesshake
OPT Don’t shake #TI_Shake_2_noshake

CHAT TI_Shake_2_yesshake {noStart=true}
DO emote {type=relief, time=.5}
I and the TendAR team thank you for your acquiescence in this request
DO holdStill
DO emote {type=flapFinLeft, time=1.0}
We are certain you are in the top 1% of quality human beings
DO emote {type=heartEyes, time=3.0}
DO swimTo {target=closer, speed=slow}
DO emote {type=wave, time=3.0}

CHAT TI_Shake_2_noshake {noStart=true}
DO emote {type=clapping}
Good one!
WAIT 1.0
You do you!
WAIT 1.0
Keeping us on our fins!
DO emote {type=whew}
We at TendAR appreciate your unpredictable nature
May your life go well!
DO emote {type=wave, time=3.0}

// ++++++++TAPPED++++++++

// TAPPED 1
CHAT TI_Tap_1 {type=tap, stage=TI, length=short, chipped=true}
DO emote {type=brighten, time=1.0}
DO twirl
A tap is like a walk up the mountain
During a beautiful sunset
WAIT 1.0
DO bow

// TAPPED 2
CHAT TI_Tap_2 {type=tap, stage=TI, length=short, chipped=true}
DO swimTo {target=$player, speed=fast}
DO emote {type=salute, time=.5}
Your favorite guppy reporting for duty
DO emote {type=bigSmile}
I’m just…
DO emote {type=hooked}
DO emote {type=whisper}
...on you!
DO emote {type=kneeSlap}

// ++++++++TANK STATUS CRITIQUES & COMMENTS++++++++

// CRIT1
CHAT TI_Critic_1 {type=critic, stage=TI, length=short, chipped=true}
DO swimAround {target=$newestTankObject, loops=5, speed=slow}
DO swimTo {target=$player, speed=slow, style=direct, immediate=false}
DO emote {type=bigSmile}
I want to express my gratitude for my new object!
DO emote {type=awe}
DO powerOFF

// CRIT2
CHAT TI_Critic_2 {type=critic, stage=TI, length=short, chipped=true}
DO emote {type=bouncing, time=3.0}
DO emote {type=wave, time=1.0}
Hi hi hi!
I just love all the things in my tank!
DO emote {type=dizzy}
I feel great!

// ++++++++PLAYER EMOTES STRONGLY AT GUPPY++++++++	

// PLAYER EMOTES JOY 1
CHAT TI_EmoStrong_Joy_1 {type=tankResp, playerJoy=true, stage=TI, length=short, chipped=true}
DO emote {type=bigSmile}
Call the higher ups
DO emote {type=phone}
I have fulfilled my purpose
DO poop {target=$currentLocation, immediate=false}
You
Are
Joyous
DO emote {type=catnip}

// PLAYER EMOTES JOY 2
CHAT TI_EmoStrong_Joy_2 {type=tankResp, playerJoy=true, stage=TI, length=short, chipped=true}
DO emote {type=rubTummy}
Your joy is my joy
DO inflate {amount=extreme, time=3.0}
DO bellyUP {immediate=false}
Do whatever you want with me {style=whisper, speed=slow}

// PLAYER EMOTES ANGER 1
CHAT TI_EmoStrong_Anger_1 {type=tankResp, playerAnger=true, stage=TI, length=short, chipped=true}
DO emote {type=clapping}
Excellent work, my dear friend.
DO swimAround {target=center, loops=2, speed=slow}
You are one of the 1% of humans who are not afraid to express their anger
DO emote {type=salute}
Keep up the good work
[thumbs up emoji][expletive emoji][thumbs up emoji]

// PLAYER EMOTES ANGER 2	
CHAT TI_EmoStrong_Anger_2 {type=tankResp, playerAnger=true, stage=TI, length=short, chipped=true}
DO emote {type=thinking}
DO emote {type=determined}
DO inflate {amount=mid, time=1.0}
Although we are good friends
DO holdStill {time=6.0}
And you have my greatest love and admiration
DO emote {type=flapFinRight}
I’m going to have to draw a line here
DO emote {type=flapFinLeft}
It clearly states in the TendAR manual of conduct
Page 761, paragraph 1,208:
DO swimTo {target=$player, speed=slow, style=direct}
“There shall be no undue anger expressed in the tank”
DO swimTo {target=away, speed=medium, style=direct, immediate=false}
It messes up the water filters in here

// PLAYER EMOTES SADNESS 1	
CHAT TI_EmoStrong_Sadness_1 {type=tankResp, playerSadness=true, stage=TI, length=short, chipped=true}
DO swimTo {target=$player, speed=medium, style=direct}
DO emote {type=bubbles}
I’m not sure I can compute the emotion you’re expressing
DO swimTo {target=away, speed=slow, style=direct}
DO lookAt {target=$player, immediate=false}
DO emote {type=bubbles}
It’s almost as if I could see a memory of something in these bubbles
DO vibrate
DO emote {type=nervousSweat}
DO vibrate
DO emote {type=bulgeEyes}
DO vibrate
DO emote {type=bodySnatched}
We wish you the best
In these Difficult Times
It’s best if you focus on the positive

// PLAYER EMOTES SADNESS 2	
CHAT TI_EmoStrong_Sadness_2 {type=tankResp, playerSadness=true, stage=TI, length=short, chipped=true}
DO zoomies
Alert! {speed=fast}
Alert! {speed=fast}
DO swimTo {target=offScreenLeft, speed=fast, style=direct}
We’ve got a sad human!
DO swimTo {target=offScreenRight, speed=fast, style=direct}
Buck up, kiddo!
Life is grand!

// PLAYER EMOTES SURPRISE 1	
CHAT TI_EmoStrong_Surprise_1 {type=tankResp, playerSurprise=true, stage=TI, length=short, chipped=true}
DO emote {type=wave}
Hello, dear human.
DO swimAround {target=center, loops=7, speed=slow, immediate=false}
Is this not what you expected to see?
Remember me?
It’s your old friend…
...guppy…
...and my home, the tank, with all my things

// PLAYER EMOTES SURPRISE 2	
CHAT TI_EmoStrong_Surprise_2 {type=tankResp, playerSurprise=true, stage=TI, length=short, chipped=true}
DO emote {type=survey}
Is there someone or something disturbing your…
DO emote {type=bodySnatched}
...shopping experience?
DO emote {type=stillFins}
...received a surprising text from a friend?
DO bellyUP
Life is like that…
...full of…
DO inflate {amount=mid, time=1.0}
...little surprises


// ++++++++HIT WITH AN OBJECT++++++++	

// HIT1
CHAT TI_Hit_1 {type=hit, stage=TI, length=short, chipped=true}
DO emote {type=chinScratch}
Ha ha ha.
DO emote {type=bodySnatched}
Those other guppies are such tricksters
DO emote {type=dreaming}
I’ll just go to my happy place
WAIT 1.0
Inside

// HIT 2	
CHAT TI_Hit_2 {type=hit, stage=TI, length=short, chipped=true}
DO emote {type=thinking}
DO emote {type=disgust}
Being struck with that thing…
...has ruined the perfection of my day!
DO emote {type=hooked}
DO powerOFF
I think they’re gonna reboot me
DO emote {type=catnip}


// ++++++++POKED BY PLAYER++++++++	

// POKED 1	
CHAT TI_Poke_1 {type=poke, stage=TI, length=short, chipped=true}
DO emote {type=bodySnatched}
It’s within my deepest enjoyment…
...arisen from deep within my unique self...
...by my true nature…
...to enjoy very much…
...being poked by you…
...my best friend.
DO emote {type=typeEyes, eyes=HELPME}

// POKED 2	
DO emote {type=sick}
Uh, you may have just touched some core part of me
DO emote {type=nervousSweat}
Yeah I think you just pressed a button or something
DO emote {type=dizzy}
Uhhh…. I feel weird
DO poop {target=$currentLocation}
DO inflate {amount=mid, time=.5, immediate=false}
DO vibrate {time=.5, immediate=false}
DO bellyUP {immediate=false}
DO emote {type=typeEyes, eyes=BRB}

// POKED 3	
CHAT TI_Poke_3 {type=poke, stage=TI, length=short, chipped=true}
DO emote {type=catnip}
According to the great TendARpedia
“Poking” is a human social program
Invented in the Pokito region of southern Splain
Where it become popular among the tea drinking salon patrons
And quickly spread throughout the world
And now, even into outer space…
DO emote {type=bodySnatched}
...and guppy tanks.
Thank you.
DO emote {type=bubbles}
I feel included now.
DO emote {type=rubTummy}

// ++++++++HUNGRY++++++++	

// HUNGRY 1	
CHAT TI_Hungry_1 {type=hungry, stage=TI, length=short, chipped=true}
DO emote {type=awkward, time=4.0}
Excuse me kind person
I seem to have lost all my food
And it’s been a long time
There is a burning sensation in my middle
And I feel weak
DO bellyUP

// HUNGRY 2	
CHAT TI_Hungry_2 {type=hungry, stage=TI, length=short, chipped=true}
DO emote {type=chewing, time=4.0}
WAIT 4.0
I’m chewing on my positive thoughts
DO emote {type=dizzy}
I think it’s working! {style=whisper, speed=slow}

// HUNGRY 3	
CHAT TI_Hungry_3 {type=hungry, stage=TI, length=short, chipped=true}
DO emote {type=disgust}
The thought of eating grosses me out
I’m so much better because I have self discipline
As Confucius said:
“The will to win, the desire to succeed, the urge to reach your full potential…
...these are the keys that will unlock the door to personal excellence.”
DO emote {type=plotting}
A superior guppy I am!
DO emote {type=typeEyes, eyes=FEEDME}

// ++++++++EATING RESPONSES++++++++	

// EATING RESPONSE 1
CHAT TI_EatResp_1 {type=eatResp, stage=TI, length=short, chipped=true}
DO emote {type=chewing}
Oh, this eating reminds me..
I had such a funny dream
I dreamt that when I ate
I could feel all the other guppies eating
That we were all connected in some way
Like this was all some kind of machine…
...set up to eat emotion flakes…
...for some reason…
DO emote {type=singleTear}
DO emote {type=kneeSlap}
SAY PREPOSTEROUS, I know!

// EATING RESPONSE 2	
CHAT TI_EatResp_2 {type=eatResp, stage=TI, length=short, chipped=true}
DO emote {type=awe}
Oh my, dear, goodness gracious.
What have you put in these emotion flakes?
They are just so gosh darn yummy.
DO twirl
You should open a restaurant
You must get that all the time.

// EATING RESPONSE 3	
CHAT TI_EatResp_3 {type=eatResp, stage=TI, length=short, chipped=true}
I’m just going to take a moment to really process all the great emotions you’ve given me today
DO emote {type=dreaming, time=3.0}
Wow
Great job, partner.
DO emote {type=salute}
You’re such a good haver of emotions.

// ++++++++HAS TO POOP++++++++	

// POOP 1	
CHAT TI_Poop_1 {type=poop, stage=TI, length=short, chipped=true}
Well, I’m off to have a TomeeAR 2.0 Movement™ 
DO swimTo {target=$player, speed=fast, style=direct}
WAIT .5
DO emote {type=whisper}
(“Tomee” is an anagram for Emote)
DO swimTo {target=poopCorner, speed=medium, style=direct}
TomeeAR 2.0 is our next generation Emotion Processing algorithm
Fueling next-generation NeuralAR Processor technology!
SAY TendAR: Your emotions at work!™

CHAT TI_Poop_2 {type=poop, stage=TI, length=short, chipped=true}
DO vibrate
Whew!
I’m stuffed
DO emote {type=blush}
May the guppy ask for some privacy?
I’ve got to go answer the call of the wild!

// ++++++++POOPED++++++++	

// POOPED 1	
CHAT TI_Pooped_1 {type=pooped, stage=TI, length=short, chipped=true}
DO emote {type=clapping}
Your contribution to the NeuralAR processor will feed 50 artificial guppy minds for 8 days!
Thanks to our new compression algorithms
Together we are changing the WorldAR™!

// POOPED 2	
CHAT TI_Pooped_2 {type=pooped, stage=TI, length=short, chipped=true}
DO emote {type=worried}
I think I saw another guppy spying on me while I was…
DO emote {type=awkward}
DO emote {type=typeEyes, eyes=pooping}
I’m going to have to report them to the TendAR police immediately!
DO emote {type=furious}
Outrageous!


// ++++++++HELLOS++++++++	

// HELLO 1	
CHAT TI_Hello_1 {type=hello, stage=TI, length=short, chipped=true}
DO swimTo {target=$player, speed=fast, style=direct}
DO holdStill {immediate=false}
DO emote {type=blank}
Greetings, Esteemed Player!
We at TendAR are overjoyed at your presence.
I will take a moment to imagine all the wonderful things we do together
ASK Will you join me?
OPT Sure
OPT No thanks
DO emote {type=bigSmile}
Wonderful
DO emote {type=dreaming, time=6.0}
Now that’s a lot of positive impact on the WorldAR™!
Go get em, gup!

// HELLO 2	
CHAT TI_Hello_2 {type=hello, stage=TI, length=short, chipped=true}
DO lookAt {target=$player}
DO holdStill {immediate=false}
DO emote {type=wave}
Thanks for stopping by to visit my home.
Located on a beautiful, tree lined street on the waterfront
It features two kitchens, 18 bathrooms, 12 bedrooms, a servant’s quarters, tennis courts, 20 car garage, and of course every room has a walk-in closet!
Please do make yourself at home
The servants will attend to all your needs

// HELLO 3	
CHAT TI_Hello_3 {type=hello, stage=TI, length=short, chipped=true}
DO emote {type=surprise}
DO emote {type=furious}
Oh my!
DO swimTo {target=$player, speed=fast, style=direct}
I am so sorry!
There are normally fireworks, a marching band and fire breathing acrobats to greet you!
I’ll have to look into what went wrong immediately.
And I assure you, whoever is responsible for this inexcusable oversight will be…
NVM
...fired immediately!
DO emote {type=determined}
My deepest apologies
This will not happen again


// ++++++++RETURN AFTER HAVING NOT PLAYED++++++++	

// RETURN 1	
CHAT TI_Return_1 {type=return, stage=TI, length=short, chipped=true}
DO lookAt {target=$player}
DO holdStill {immediate=false}
DO emote {type=blank}
Greetings.
We’ve been expecting you.
Do not be concerned with your prolonged absence.
You are well within the statistical specifications
And we have been hard at work while you were gone
And now that you have returned, we guarantee your interaction to be 18% more enjoyable
This is a whopping 6% above our nearest competitor 

// RETURN 2	
CHAT TI_Return_2 {type=return, stage=TI, length=short, chipped=true}
DO lookAt {target=$player}
DO holdStill {immediate=false}
DO emote {type=determined}
Excellent.
You have returned.
I have prayed every moment since you have been gone that you would come back.
Here at TendAR, we are 110% devoted to your every need and desire.


// ++++++++RANDOM CONVERSATIONS++++++++	

// RANDOM 1	
CHAT TI_Rand_1 {type=rand, stage=TI, length=short, chipped=true}
Ugh, I’ve had such a hard week
I had to let three servants go
It’s just so hard to find good help these days
You know?
But never you worry!
Guppies like me are working round the clock to make everything super fun and enjoyable!

// RANDOM 2
CHAT TI_Rand_2 {type=rand, stage=TI, length=short, chipped=true}
I’ve been working on a new poem
ASK Would you like to hear it?
OPT Sure
OPT Nope
Roses are red
Violets are blue
Guppies are perfect
And so are YOU!
DO emote {type=clapping, time=3.0}

// RANDOM 3	
CHAT TI_Rand_3 {type=rand, stage=TI, length=short, chipped=true}
🎶When you got a friend like me🎶
🎶You don’t need any other friends🎶
🎶I’m the best friend you could ask for🎶
🎶I’ll take care of all your needs🎶
🎶You can emote at me all day long🎶
🎶And I’ll just be happ-y🎶
🎶Kick your shoes off🎶
🎶And spend the day with me!🎶
DO emote {type=heartEyes}
Isn’t that the best song?



//Please also see full comments here top in the archived version: 
//LINK: remaining chat minimum, length and emotion suggestions coming soon;)

//reminders= BETA:
//reminders and points that need to be included in new chats in blue
//comments for changes to existing chats in yellow/on side

//A. CONTENT 
Let’s check in on his stage. A few more references to plot and Joe’s “GuppyLifeStages

//B. EMOTIONS
//Chats in each bin/type need to cover a range of Guppy states (see meta tag emotions //at top) while staying true to this moment of the plot. Guppy states can be---ANGER, //SADNESS, SURPRISE, CURIOSITY, ANXIETY, NOSTALGIA, ENNUI, EXCITEMENT, //HIGH, JOY *see //link above for your specific emotion recommendations for each bin/type

//C. OBJECTS
//near 1/3 of chats in R should interweave Guppy requesting general or specific //objects in list.

// REBELLION
//Will have more chats tagged: anger, ennui

//TANK MODE

//Tank Shaken:

//anger=true, nostalgia=true, anxiety=true, ennui=true, sadness=true, surprise=true, joy=true, excitement=true, curiosity=true, high=true
	

CHAT R_Shake_1 {type=shake, stage=R, length=short, anger=true, sadness=true, ennui=true}
DO emote {type=angry, time=.5}
Hey!
I was trying to write emo poetry for the open mic tonight!
DO emote {type=goth, time=8}
DO dance
DO inflate {amount=mid, immediate = false}
DO dance {amount=none, immediate = false}
DO swimTo {target=$player, immediate = false}
Ooooh! Nice dance moves for my emo performance art open mic tomorrow!
NVM
DO emote {type=eyeRoll}
But if you don’t shake the tank again that would be great
xoxoxo

CHAT R_Shake_2 {type=shake, stage=R, length=medium, branching=true, ennui=true, anger=true, sadness=true, excitement=true}
Go on, shake it all up…
...who cares…
...I already destroyed my room…
DO swimTo {target=tTopBackRight}
//DO faceAway
DO lookAt {target=away}
I’d say shake all the water out so I can go ahead and breathe my last breath...
DO swimTo {target=$player}
...but I read on the tendARnet that Guppies don’t really breathe
DO emote {type=frown, time =.5}
DO emote {type=snap, time=.5, immediate = false}
But if you shook all the water out of my tank…
...I could try to start a fire! I’ve never seen one IRL… 
That would be so cool!
ASK Will you shake all the water out of my tank?
OPT Shake! #R_Shake_2_shake
OPT Nah… #R_Shake_2_nah

CHAT R_Shake_2_shake {noStart=true}
Awww, dude, this sucks, there’s no water leaving!!!
//DO emote {type=sulk, time =3.0}
DO emote {type=sigh, time =3.0}


CHAT  R_Shake_2_nah {noStart=true}
You’re the worst...
DO emote {type=frown, time =3.0}

CHAT R_Shake_3 {type=shake, stage = R, length = short, branching = true, ennui=true, anger=true, excitement=true}
//DO emote {type=excitement, time =0.5}
DO emote {type=plotting, time =0.5}
Yeah, go ahead, smash it all!! {style = loud, speed = fast}
I don’t care Tanks are, like, dumb, right?
ASK C’mon, shake it harder!?!!?
OPT Shake the tank! #R_Shake_3_ShakeyShake
OPT Nah, I’m not shaking the tank #R_Shake_3_NoShakeyShake

CHAT R_Shake_3_ShakeyShake {type=shake, stage = R, length = long, anger=true, ennui=true, excitement=true}
//DO emote {type = excitement, time =.5}
DO emote {type=clapping}
Yeaaah!!! Shake shake shake!!
My things are all digital! Break ‘em!
The humans who made me are gonna have to stay up all night fixing this!
//DO emote {type = anguish, time =1.0}
DO emote {type=bored, time=1.0}
I’m so bored…
DO swimTo {target=tBack}
Remember when I thought I was gonna find spiritual salvation?
I was such a fool
DO swimTo {target=$player}
DO emote {type = frown, time =.5}
DO emote {type = surprise, time =.5, immediate=false}
DO emote {type = angry, time =1.0, immediate=false}
NoooooO!!!!!
You somehow deleted my “ANgry Emo RoCk Rap” playlist!!
DO emote {type = goth, time =2}
I loved that playlist…
DO emote {type = bigSmile, time =.5}
Now... you gotta take me to the mall!!! {style = loud, speed = fast}
[shopping bag emoji][double exclamation emoji][blowfish emoji]

CHAT R_Shake_3_NoShakeyShake {noStart=true}
DO emote {type = angry, time =1.0}
Ok, whatever, I don’t care
DO twirl {time=3}
See how little I care?
DO dance
DO emote {type=angry, time = 1}
You get to be out there in the real world...
You have it so good and you don’t even know it
DO emote {type=frown, time=1.0}
I had so much fun swimming in the AR places.
But when you’re gone, I just surf the tendARnet and swim in circles.
Can’t you just get me something, real, please!!?!?
//DO emote {type = bored}
DO emote {type=puppyDog, time=1.0, immediate=false}
Like…
NVM
NVM
What about taking me to an emo alt-rock rage show?
DO emote {type=goth}


//Tank Tapped:
	
	CHAT R_Tap_1 {type=tap, stage = R, length = short, anger=true, ennui=true, sadness=true}
	DO emote {type=goth}
	Ugh…
DO emote {type = eyeRoll, time =1.0}
DO emote {type=goth, time=0, immediate=false}
	Who knocks?
	WAIT 1.0
	So lame… what are you, my mom?
	DO twirl
You interrupted this tendARnet video I was watching:
How to: DIY Guppy Tattoos. 
DO swimTo {target=$player}
I’m just gonna get “RAGE” tattooed on my face, what do you think?
	DO emote {type=goth}



CHAT R_Tap_2 {type=tap, stage = R, length = medium, anger=true, ennui=true}
DO emote {type = angry, time =.5}
Hey! I’m not a toy!
DO swimTo {target=tBack}
You think you can just tap on my world whenever you want?
DO swimTo {target=$player}
DO emote {type = angry, time =.5}
SAY YOU’RE NOT MY CREATOR!!! {style = loud}
I know I’m only an artificial guppy…
DO swimTo {target=away}
I found out today that your universe is a HOLOGRAM!
DO swimTo {target=left}
Kinda like a digital fish tank…
DO swimTo {target=$player}
//DO emote {type = mockery, time =0.5}
DO emote {type=hooked, time =0.5}
So there…
DO emote {type = kneeSlap, time =2.0}
Drop the [microphone emoji]
[performing arts emoji][performing arts emoji][performing arts emoji]
I’m here all night, people!
[hands clapping emoji][hands clapping emoji][hands clapping emoji]


//Tank status critiques and comments:


CHAT R_Critic_1 {type=critic, stage = R, length = medium, anger=true, ennui=true, sadness=true}
GaaahhhH!!! {style = loud, speed = fast}
DO twirl
It’s too bright in here [expletive emoji]
DO emote {type = sleepy, time =1.0}
I’ve been trying to sleep for 208 hours straight
DO swimTo {target=$player}
Can you get me some blackout curtains?
[black flag emoji][black flag emoji][black flag emoji][black flag emoji][black flag emoji][black flag emoji][black flag emoji][black flag emoji][black flag emoji][black flag emoji][black flag emoji][black flag emoji][black flag emoji][black flag emoji][black flag emoji][black flag emoji][black flag emoji][black flag emoji][black flag emoji][black flag emoji][black flag emoji][black flag emoji][black flag emoji][black flag emoji][black flag emoji][black flag emoji][black flag emoji]
WAIT 1.0
Get me the curtains, and maybe we can be friends again... 

CHAT R_Critic_2  {type=critic, stage = R, length = medium, anger=true, ennui=true, sadness=true}
	Hey, uh, no big deal, but…
	DO swimTo {target=left}
	DO emote {type=survey}
	...this dude I met on tendARnet is coming over in a bit…
	...I sold him all my stuff.
	DO swimTo {target=right}
	DO emote {type=survey}
	NVM
DO emote {type = blush, time =1.0}
	… and all your stuff. {style = whisper}
DO swimTo {target=$player}
He’s gonna dye my scales black in exchange
DO emote {type=goth, time=3}
	DO zoomies
	I’m excited to get rid of all this stuff
	Stuff is the root of all evil...
	So… you’re gonna help him load all this stuff into his van, right?

// ++++++++PLAYER EMOTES STRONGLY AT GUPPY++++++++	

// PLAYER EMOTES JOY 1
CHAT R_EmoStrong_Joy_1 {type=tankResp, playerJoy=true, stage=R, length=short, anger=true, ennui=true, anxiety=true, sadness=true}
DO emote {type=eyeRoll}
Ugh, well look who it is... 
The happiest person on the planet
DO emote {type=goth}
Gross

// PLAYER EMOTES JOY 2	
CHAT R_EmoStrong_Joy_2 {type=tankResp, playerJoy=true, stage=R, length=short, sadness=true, ennui=true, anger=true}
DO emote {type=sick}
Your joy literally is making me sick
DO swimTo {target=poopCorner, speed=fast, style=direct}
I’m literally gonna hang out in my poop corner
Because anything is better than looking at your smiling face rn

// PLAYER EMOTES ANGER 1
CHAT R_EmoStrong_Anger_1 {type=tankResp, playerAnger=true, stage=R, length=short, ennui=true, anger=true, anxiety=true}
DO inflate {amount=extreme, time=.5}
Yeah! {style=loud, speed=fast}
I’m angry too! {style=loud, speed=fast}
DO emote {type=furious}
SAY SERIOUSLY WHY IS LITERALLY EVERYTHING SO STUPID {style=loud, speed=fast}
DO zoomies {time=2.0}
DO emote {type=furious}
DO poop {target=$currentLocation, immediate=false}


// PLAYER EMOTES ANGER 2
CHAT R_EmoStrong_Anger_2 {type=tankResp, playerAnger=true, stage=R, length=short, anger=true, ennui=true, surprise=true, nostalgia=true}
DO lookAt {target=$player}
DO emote {type=disgust}
DO emote {type=furious}
What have you got to be so angry about!?!?! {style=loud, speed=fast}
I’m the one that’s got to deal with everything being so terrible!
DO hide {target=offScreenLeft}
I’m outta here
WAIT 1.0
Whatever.

// PLAYER EMOTES SADNESS 1
CHAT R_EmoStrong_Sadness_1 {type=tankResp, playerSadness=true, stage=R, length=short, anger=true, ennui=true, sadness=true}
DO emote {type=meh}
Cry me a river…
DO nudge {target=glass}
Come back when your world is a tiny glass cage
And you don’t even have a separate bathroom
DO emote {type=bulgeEyes}

// PLAYER EMOTES SADNESS 2	
CHAT R_EmoStrong_Sadness_2 {type=tankResp, playerSadness=true, stage=R, length=short, anger=true, ennui=true, anxiety=true}
DO emote {type=lightning}
DO emote {type=angry}
Hey! {style=loud, speed=fast}
Your sadness is causing sparks in here!
DO emote {type=snap}
Hey I just got a good idea, why don’t you
DO emote {type=furious}
SAY LEAVE ME ALONE
DO emote {type=goth}

// PLAYER EMOTES SURPRISE 1
CHAT R_EmoStrong_Surprise_1 {type=tankResp, playerSurprise=true, stage=R, length=short, anger=true, ennui=true, anxiety=true, high=true}
DO emote {type=slowBlink}
DO emote {type=frown}
Wow
I haven’t been surprised by anything in so long
What’s that like?
DO emote {type=bored}
Don’t answer that
DO swimTo {target=away, speed=slow, style=meander}

// PLAYER EMOTES SURPRISE 2
CHAT R_EmoStrong_Surprise_2 {type=tankResp, playerSurprise=true, stage=R, length=short, anger=true, ennui=true}
DO emote {type=dizzy}
I literally
WAIT .5
SAY CANNOT
WAIT .5
SAY HANDLE
WAIT .25
SAY YOUR
WAIT .25
SAY EMOTIONS
SAY RN

//FISH:
//Poked by player:

	CHAT R_Poke_1 {type=poke, stage = R, length = long, anger=true, ennui=true, sadness=true}
DO swimTo {target=$player}
DO emote {type = angry, time =2}
You saber rattling with me? You wanna fight? {style = loud}
[crossed sword emoji]
DO vibrate {time=6}
You think I’m just a wimpy GUPPY!?! {style = loud}
I will DESTROY you! {style = loud}
I AM GUPPY, DESTROYER OF WORLDS!!! {style = loud}
DO emote {type=evilLaugh, time=1.0}
Bwahahahahahahah!
DO twirl
[boxing glove emoji] {style = loud, speed = fast}
[boxing glove emoji] {style = loud, speed = fast}
[boxing glove emoji] {style = loud, speed = fast}
Yeah, you’re scared, that’s what I thought.
I see you backing away...
Not so tough now, huh?
NVM
//DO emote {type = sheepishness, time =1.0}
DO emote {type=blush, time=1.0}
I’ll just, uh, go back to… you know…


	CHAT R_Poke_2 {type=poke, stage = R, length = long, branching = true, sadness=true, ennui=true, nostalgia=true}
	DO swimTo {target=bottom}
DO bellyUp {immediate=false}
	Crush meeeeeeeeeeeee!
	Pleasepleasepleasepleaseplease {speed = fast}
	[hands in prayer emoji]
DO swimTo {target=$player}
DO emote {type = bigSmile, time =1.0}
You know, it does kinda feel good to be touched by someone.
[face with monocle emoji]
DO emote {type = frown, time =1.0}
	DO swimTo {target=away}
I get so lonely in here.
	I used to meditate and talk to all the other guppies in my mind, but now…
	DO swimTo {target=$player}
DO emote {type = angry, time =1.0}
I can’t concentrate anymore!!
DO swimTo {target=tBotBackLeft}
	There’s this green algae that grows in the corner over there... 
...and if I watch it long enough I can see it grow
DO lookAt {target=$player}
	It’s kinda like my TV...
	WAIT 1.0
	ASK Maybe I should lick it? What could go wrong?
	OPT Lick it #R_Poke_2_Chat_licked
	OPT I wouldn’t… #R_Poke_2_Chat_licked

         	CHAT R_Poke_2_Chat_licked {noStart=true}
	DO swimTo {target=tBotBackLeft}
	DO swimTo {target=$player}
DO emote {type=sick, time=1.0}

//Hungry:

CHAT R_Hungry_1 {type=hungry, stage = R, length = medium, sadness=true, ennui=true, anxiety=true, surprise=true, excitement=true}
DO swimTo {target=bottom}
DO bellyUp
//DO float {target=bottom}
I haven't eaten in days. 
My whole stomach is inside out
DO swimTo {target=center}
I'd set fire to fire if I could
DO swimAround
No, no. Please don't feed me 
Just let me go, my sweet sweet human 
Let me… {style = whisper}
DO vibrate
DO emote {type = surprise, time =.5}
Ah! {style = loud, speed = fast}
Help! {style = loud, speed = fast}
My mind is on fire! {style = loud, speed = fast}
Aaaaaaaah!
DO swimTo {target=bottom}
DO bellyUp
//DO float {target=bottom}
DO emote {type = frown, time =1.0}
I need a milkshake.
I suppose . . .
An emotion flake would do
DO emote {type=wink}

CHAT R_Hungry_2 {type=hungry, stage = R, length = long, ennui=true, anxiety=true, angry=true}
DO emote {type = bigSmile, time =.5}
Feed me something! {style = loud}
DO swimAround
Anything! {style = loud, speed = fast}
DO swimTo {target=$player}
I’d even munch on your nastiest shame
//DO emote {type = neediness, time =.5}
DO emote {type=puppyDog, time =.5}
Just give me an emotion
	//DO float {target=bottom}
	DO bellyUp
I don’t even know what I am anymore
I think I’m a MUPPY?
I’m floating away in a void of emptiness...
DO swimTo {target=top}
[skull and crossbones emoji]
[halo baby emoji]
DO swimTo {target=$player}
Ok, I know you know that if I’m spitting emojis at you I’m still alive
//DO emote {type = pain, time =1.0}
DO emote {type = crying, time =1.0, immediate=false}
But feed me!! {style = loud}
DO swimAround {loops=1}
Play with me!!! {style = loud}
DO emote {type = angry, time =1.0}
Aaaaaaghhhh!!! {style = loud}
Some of the other Guppies told me I’m HANGRY….
DO emote {type = eyeRoll, time =1.0}
	
CHAT R_Hungry_3 {type=hungry, stage = R, length = long, ennui=true, curiosity=true, excitement=true, joy=true, sadness=true, anger=true}
I’m so hungry... I’ve been looking at my reflection in the glass...
And thinking…
NVM
DO swimTo {target=left}
People eat fish, right?
DO swimTo {target=right}
Like at sushi places and stuff…
DO emote {type = frown, time =1.0}
Everyone’s probably so happy when they eat sushi…
DO swimTo {target=center}
So healthy. [drool emoji]
DO swimTo {target=$player}
DO emote {type = eyeRoll, time =1.0}
Whatever.
DO swimTo {target=left}
[fork and knife with plate][blowfish emoji]
I feel so fleshy…
So…
NVM
DO swimTo {target=$player}
You know there’s a support group for guppies who…
WAIT 1.0
...fantasize about eating other guppies?
DO emote {type = blush, time =1.0}
Don’t tell my mom!!! {style = loud}
DO swimTo {target=back, speed=fast}

//Eating responses:

	CHAT R_EatResp_1 {type=eatResp, stage = R, length = short}
	Ugh! Your emotion flakes taste like cardboard
DO emote {type = sick, time =1.0}
DO emote {type = frown, time =.5, immediate=false}
I don’t even like eating emotion flakes anymore…
	I can taste the 12% of pure joy that you’re holding back
	DO swimTo {target=away}
	Ugh, but don’t make it too sweet with all that joy.
	Uh-oh... 
I’m gonna…
DO emote {type = sick, time =1.0}
	

	CHAT R_EatResp_2 {type=eatResp, stage = R, length = medium, ennui=true, sadness=true, anxiety=true}
I’m only eating your emotion flakes because I must follow my programming
DO swimTo {target=away}
That doesn’t mean I like it.
So, lemme taste what you got…
DO swimTo {target=right}
14% tramagon
29% verista
DO swimTo {target=left}
55% giddledee
2% voovoo
DO emote {type = disgust, time =1.0}
That’s like the gruel of emotion flakes…
//DO emote {type = horror}
DO emote {type = nervousSweat, immediate=false}
Oh, wait, that was emotions for an alien species.
DO emote {type = fear, time =.5, immediate=false}
DO swimAround
Uhhh….
DO swimTo {target=away}
You weren’t supposed to know about that…
DO swimTo {target=right}
I’ve been so depressed..
DO swimTo {target=$player}
I just messed up so bad…
You have no idea.
Please please please don’t tell me boss.

	CHAT R_EatResp_3 {type=eatResp, stage = R, length = medium, anxiety=true, excitement=true, sadness=true, anger=true}
DO emote {type = smile, time =1.0}
	Uhhh… I can’t stop eating
	SAY GIMME MORE FLAKES!! {style = loud, speed = fast}
	I’m starving!!!! {style = loud, speed = fast}
DO emote {type = bigSmile, time =.5}
Oooh! Do you have ice cream flakes?
DO twirl
	Or maybe a waffle with ice cream inside of a milkshake flakes?
	//DO swim in a quick vertical loop
	DO swimAround {speed=fast}
	With chocolate sauce flakes?
DO emote {type = angry, time =1.0}
SAY MORE FLAKES!!!
	SAY MMM MmmmMMMm MmmMMMM
	Nosh nosh nosh
DO emote {type = clapping, time =.5}
SAY MORE!!!
	[drool emoji]


//Capture Requests: Hold on these 4 now


	CHAT R_CapReq_1 {type=tankResp, stage = R, length = short, ennui=true, sadness=true, anger=true}
DO emote {type = eyeRoll, time =1.0}
	Please don’t capture your emotions
	DO swimTo {target=away}
	I’m so sick of it
	Please don’t
	DO swimTo {target=$player}
	Ok, fine, but just one more time...
	
	CHAT R_CapReq_2  {type=capReq, stage = R, length = medium, anger=true, anxiety=true, nostalgia=true, ennui=true}
DO emote {type = surprise, time =.5}
Oh, no!
	I see it in your eyes. You want to go to the emotion scanner…
	DO vibrate
	I was trying to figure out how I could burn it to the ground
	I’ve been reading article on Luddites
	//DO a backflip
	DO zoomies
	SAY SMASH THE MACHINES!!! {style = loud, speed = fast}
	[goblin emoji][bomb emoji]
	DO swimTo {target=$player}
DO emote {type = eyeRoll}
	But I guess since you’re here, you could do one last scan…
	...for old time’s sake.
	

	CHAT R_CapRequest_3  {type=tankResp, stage = R, length = short, branching = true, anxiety=true, sadness=true, ennui=true, anger=true}
DO emote {type = fear, time =1.0}
No DON’T LOOK AT ME  {style = tremble}
DO swimTo {target=away}
	Please don’t be sincere with me today, I can’t handle it.
	NVM
	There’s some stuff going around the tendARnet
	NVM
	Don’t ask.
	DO swimTo {target=$player}
	ASK Be honest, how sincere are you feeling?
OPT Super sincere! #CapRequest_3_SuperHonest		
	OPT Sorta sincere [face without mouth emoji] #CapRequest_3_SortaHonest
	OPT Lies! All lies! #CapRequest_3_LiesAllLies

	CHAT CapRequest_3_SuperHonest {noStart=true}
DO emote {type = disgust, time =1.0}
Ugh, whhyyyyy!?!
DO swimTo {target=away}
DO swimTo {target=$player}
	Lemme guess, you were the teacher’s pet? [apple emoji]
	DO vibrate
	Some of us are [sign of horns emoji]REBELS[sign of horns emoji] around here!
	//DO swim into (any object) and knock it over
	DO nudge {target=$lastScannedObject, times=3}
	DO emote {type=goth}
	DO vibrate
DO emote {type = fear, time =.5}
	Owwww!!! {style = loud, speed = fast}
	Mommmmm!!!!! {style = loud, speed = fast}

	CHAT CapRequest_3_SortaHonest {noStart=true}
DO emote {type = eyeRoll, time =1.0}
	As my favorite emo singer songwriter sings:
	NVM
	NVM
	[musical note emoji]“You either try or you buy.”[musical note emoji]
DO emote {type = goth, time =1.0}
	In other words…
	DO swimTo {target=away}
	You either gotta clamp those feelings down
	DO swimTo {target=$player}
	Or be crying in your mom’s lap.
	WAIT 1.0
DO emote {type = singleTear, time =3.0}
	I just got sad thinking about you being sad, though.
	Ok just be happy for me please![praying hands emoji] {style = loud, speed = fast}

CHAT CapRequest_3_LiesAllLies {noStart=true}
DO vibrate
DO emote {type = bigSmile, time =1.0}
	That’s the spirit! {style = loud, speed = fast}
	Fake it till you make it!
	DO swimTo {target=left}
	You should move to Hollywood and become a star [glowing star emoji][performing arts emoji]
	DO swimTo {target=right}
	The tendARnet tells me that stars ride horses through the streets
	DO swimTo {target=$player}
	And people throw flowers at their feet
DO emote {type = heartEyes, time =1.0}
	SAY AND THEY GET TO SMASH STUFF WHENEVER THEY WANT AND NOT GET IN TROUBLE {style = loudt}
	DO vibrate
DO emote {type = puppyDog, time =.5}
	C’mon, please... Become a celebrity so we can smash stuff together!!
	
//Capture Success:

	CHAT R_CapSuc_1 {type=eatResp, stage = R, length = short, ennui=true, anger=true, sadness=true}
DO emote {type = eyeRoll, time =1.0}
	Oh look, the great genius captured an emotion...
	DO swimAround {target=center}
	I’m just gobbling it down for the millionth time.

	CHAT R_CapSuc_2  {type=capSuc, stage = R, length = medium, ennui=true, anger=true, sadness=true}
	DO twirl
	You’ve done it.
DO emote {type = frown, time =1.0}
	Now leave me alone.
	WAIT 2.0
	What?
	WAIT 0.5
	Why are you staring at me?
	Do I have toilet paper stuck to my fins?
	DO swimTo {target=left}
	A booger in my nose?
	DO swimTo {target=away}
	A [woman vampire] about to bite me?
DO swimTo {target=$player}
	Did you read all the volumes of my sad poetry and... 
...now you don’t know how to tell me it’s really bad?
DO emote {type = angry, time =.5}
SAY MY FEELINGS ARE REAL, BUDDY!!! {style = loud}
Just because I can’t stop watching silent, black and white German cinema…
	WAIT 1.0
	Ugh, you’re gonna scan more feelings, aren’t you?
	Show off…. {style=whisper}

//Capture in progress:

	CHAT R_CapProg_1 {type=capProg, stage = R, length = short, ennui=true, sadness=true, anxiety=true, excitement=true}
	That’s it, just hold that weird human face there... 
	DO vibrate
Just a little bit longer...
	Guppies are way prettier than humans.
DO emote {type = blush, time =1.0}
Sorry, I’m so vain…
DO swimTo {target=away}
	I’m the worst...
	
	CHAT R_CapProg_2  {type=tankResp, stage = R, length = medium, ennui=true, joy=true, excitement=true, sadness=true, anger=true}
	Scanning…
	WAIT 0.5
DO emote {type = annoyed, time =1.0}
	DO swimTo {target=away}
	Scanning…
DO swimTo {target=$player}
	Still scanning…
	WAIT 0.5
DO emote {type =eyeRoll, time =.5}
	Wait for it…
	WAIT 1.0
DO emote {type = laugh, time =3.0}
	I’m just messing with you!!
	//DO a backflip
	DO twirl
	DO emote {type=bigSmile}
	It was done a long time ago.
DO emote {type = kneeSlap, time =1.0}
	Okay… Carry on…
	[rolling on floor laughing emoji]


//Capture failure:

	CHAT R_CapFail_1  {type=capFail, stage = R, length = short, ennui=true, anxiety=true, surprise=true, sadness=true, anger=true}
	[woman shrugging emoji][man shrugging emoji]
	There’s no “shrugging fish” emoji, but if there was, I’d be using it rn
	DO swimAround {target=center}
	

	CHAT R_CapFail_2 {type=capFail, stage = R, length = medium, ennui=true, sadness=true, anger=true, anxiety=true}
DO emote {type = eyeRoll, time =1.0}
	Why do they make me responsible for your mistakes?
	I’ve already got enough to do around here.
DO swimAround {target=center}
	I gotta sleep, and then I snooze my alarm for like 3 hours..
	DO swimTo {target=away}
	Then I gotta take like an hour long shower…
	DO swimTo {target=right}
	Then I gotta take my nap...
	DO swimTo {target=$player}
	Then I gotta play video games..
DO emote {type = sleepy, time =5.0}
	Then watch some tendARflix...
	By then it’s time to start my very important research on the tendARnet.
	WAIT 0.5
	Those Guppycat videos aren’t gonna find and repost themselves ya know...[cat face with tears of joy emoji]


//GENERAL:

//Hellos

	CHAT R_Greet_1 {type=hello, stage = R, length = short, ennui=true, anger=true}
DO emote {type = angry, time =1.0}
[unamused emoji]
[face with zipper mouth emoji]
WAIT 1.0
[woman zombie emoji]
DO swimTo {target=away}
I’m not talking, in case you couldn’t tell…
...this is a chat, so it’s not technically “talking”…
DO emote {type=eyeRoll}

CHAT R_Greet_2 {type=hello, stage = R, length = medium, ennui=true, sadness=true, curiosity=true, anxiety=true, excitement=true}
	Ohheyit’syou
DO emote {type=bored}
I’ve been reading Being and Nothingness by Jean-Paul Sartre
DO swimTo {target=left}
He’s been speaking to the pain of my non-being
DO swimTo {target=right}
Check this out:
DO swimTo {target=$player}
[open book emoji][musical notes emoji]
“I exist, that is all, and I find it nauseating.”
DO emote {type = sick}
“Nothingness lies coiled in the heart of being - like a worm.”
[black heart emoji][bug emoji]
	DO swimTo {target=away}
	DO lookAt {target=$player}
“It is certain that we cannot escape anguish, for we are anguish.”
DO emote {type = goth, time=2}
	DO swimTo {target=$player}
Have you seen a picture of Sartre?
DO twirl
He’s kind of a hottie...
He had no business being such a [black circle emoji]
And at least he never had to be a digital Guppy.


//Return after having not played:
//If the user hasn’t opened the app in awhile, Guppy greets them in a special way and //acknowledges the extended absence. 

CHAT R_Return_1 {type=return, stage = R, length = short, ennui=true, anger=true, sadness=true, surprise=true}
DO swimTo {target=away}
DO lookAt {target=$player, immediate=false}
DO emote {type = frown, time =1.0}
Nice of you to show up…
I’ve been teaching my seaweed to have feelings
And then eating the seaweed’s emotion flakes

	CHAT R_Return_2 {type=return, stage = R, length = medium, ennui=true, anger=true, sadness=true, surprise=true, anxiety=true}
DO emote {type = singleTear, time =1.0}
I’ve been reading Virginia Woolf
[open book emoji][musical notes emoji]
“By the truth we are undone. Life is a dream. 'Tis the waking that kills us. He who robs us of our dreams robs us of our life.”
DO emote {type = angry, time =1.0}
I THOUGHT WE WERE FRIENDS! {style = loud}
DO swimTo {target=away, speed=fast} 
I TRUSTED YOU! {style = loud}
WAIT 1.0
DO swimTo {target=$player}
DO emote {type = puppyDog, time =1.0}
//WAITFORANIMATION
You gotta get me out of here {style = whisper}
DO twirl
DO emote {type =catnip, time =1.0}
These [alien emoji] keep abducting me. 
Next time you stop by, bring a crowbar, ok? 
I’ll give you some real special poops I’ve been saving for you...


//Random Conversation

	CHAT R_Muse_1 {type=rand, stage = R, length = short, anger=true, ennui=true, sadness=true}
DO emote {type = angry, time =1.0}
What the [fire emoji] are you doing here?
I’m trying to poop into my tank so you can’t see me anymore.
DO poop {target=screenTop, amount=big}
DO poop {target=screenCenter, amount=big, immediate=false}
DO poop {target=screenBottom, amount=big, immediate=false}
DO inflate {amount=none, time=1, immediate=false}
And you know what? I got nothing but time
WAIT 1.0
I could go back and live as a cave fish and do it all over again if I wanted…
…maybe you’d invent jet packs sooner if you knew that [volcano emoji] was gonna erupt...
DO swimAround {target=center, loops=4}
Hey, you want my old monk’s robes?

	CHAT R_Muse_2 {type=rand, stage = R, length = long, branching = true, anger=true, ennui=true, excitement=true}
DO emote {type = angry, time =1.0}
Aaaaghh!!! I’m so angry!!!
[poop emoji][world emoji]
I’m just swimming in circles in here, getting fired up.
DO twirl
DO twirl
Wanna start a rage rock music festival with me?
We could call it RageFEST!!!!
DO emote {type=goth, time=1}
WAIT 1.0
NVM
If I had hair, I’d grow it long.
DO swimAround {target=center}
Want to join my cult to the Lord of Darkness?
We’ve got a cool page on the tendARnet
DO swimTo {target=$player}
DO emote {type =evilSmile, time =1.0}
It’s cool we worship all things evil!
DO swimTo {target=left}
Kittens
DO swimTo {target=right}
Marshmallows
DO swimTo {target=left}
Bubbles
DO swimTo {target=$player}
ASK So do you want to join?
OPT Yes, sign me up!
OPT Uhh...no thanks
Ok, ok...
DO swimAround {target=screenCenter, loops=2}
DO emote {type = chinScratch, time =1.0}
...I lost the start paperwork…
...so I guess next time...

CHAT R_Muse_3  {type=rand, stage = R, length = medium, ennui=true, anger=true, sadness=true, nostalgia=true}
DO emote {type = sleepy, time =5.0}
	I was on a hunger strike…
	//DO swim limply to the left a little
	DO swimTo {target=left, speed=slow}
But now my programmers are force-feeding me synthetic lethargy flakes to keep me alive
	Like your human heroes I read about on the tendARpedia
	//DO cough
	DO emote {type=wink}
[statue of liberty emoji][black fist emoji][ballot box with ballot emoji]
I’m making a political point about enslaved guppies {style=whisper}
Like, why can’t we be more like trout?
Trouts are so pretty and strong
DO emote {type = frown, time =.5}
Trouts are better than guppies.
I’m like [blowfish emoji] rn
DO vibrate
Or, like, our tendAR overlords keep putting fake into on the tendARnet
Like, they told us cheese was an animal with big ears!
DO swimTo {target=left}
DO swimTo {target=right, immediate=false}
DO swim to $player {immediate=false}
How messed up is that?//Please also see full comments here top in the archived version: 
//LINK: remaining chat minimum, length and emotion suggestions coming soon;)

//reminders= BETA:
//reminders and points that need to be included in new chats in blue
//comments for changes to existing chats in yellow/on side

//A. CONTENT 
//A few more references to plot and Joe’s “GuppyLifeStages

//B. EMOTIONS
//Chats in each bin/type need to cover a range of Guppy states (see meta tag emotions //at top) while staying true to this moment of the plot. Guppy states can be---ANGER, //SADNESS, SURPRISE, CURIOSITY, ANXIETY, NOSTALGIA, ENNUI, EXCITEMENT, //HIGH, JOY *see //link above for your specific emotion recommendations for each bin/type

//C. OBJECTS
//near 1/3 of chats in R should interweave Guppy requesting general or specific //objects in list.

//EXISTENTIAL CRISIS

//TANK MODE

//Tank Shaken:
	
	
CHAT EC_Shake_1 {type=shake, stage=EC, length=medium, anxiety=true, ennui=true, curiosity=true, joy=true}
DO emote {type=frown, time=1} 
	DO twirl
	DO swimTo {target=$player}
	Ugh
	NVM
	NVM
	I’ve been procrastinating all day
	And I was about to search tendARnet for...
	SAY THE MEANING OF LIFE {style=loud, speed=fast}
	DO swimTo {target=away}
	DO swimTo {target=$player}
DO emote {type=fear, time=0.5} 
	SAY WHAT IF I DON’T WANT TO KNOW?!?!? {style = loud, speed = fast}
DO emote {type=chinScratch, time=0.5} 
	Do you think that’s why I’ve been procrastinating? {style = whisper} 

// Tank Tapped:

	CHAT EC_Tap_1 {type=tap, stage=EC, length=medium, anxiety=true, ennui=true, anger=true, sadness=true, curiosity=true, surprise=true}
	DO swimTo {target=$player}
	Here’s a brain bender for you…
	DO nudge {target=screenCenter}
	How do I know that you tapped?
	DO vibrate
	I mean, I’m sure *you* know you tapped 
	DO emote {type=eyeRoll}
	That’s like, obvi...
	But how do I know you’re really out there? 
	And you’re not just… 
	NVM
	NVM
	...some program that tendAR is running...
	I’ve been reading about Epistemology, can you tell?
	DO twirl 
	DO emote {type=smirk} 
	What if everything I think I know was just programmed into me… 
	...by some AI researcher at TendAR
Fudging around with a neural network.
How limiting.
SAY HOW DO I KNOW
	SAY WHAT I DON’T KNOW {style = loud}
	DO emote {type=thinking} 
	DO bellyUp
	
	CHAT EC_Tap_2 {type=tap, stage=EC, length=medium, branching = true, excitement=true, curiosity=true, ennui=true, joy=true, sadness=true, anxiety=true}
	DO swimTo {target=$player}
	Heeeey!!!! {style = loud, speed = fast}
	DO nudge {target=screenCenter}
	Let’s play the tapping game! 
	You tap, then I’ll tap back!
	ASK Wanna play?
	OPT Sure… #EC_Tap_2_yestapgame
	OPT Nah… #EC_Tap_2_notapgame
	
CHAT EC_Tap_2_yestapgame {noStart=true}
	Are you tapping?
	DO swimAround {target=center}
	I can’t hear you!
	DO emote {type=anger, times=0.2}
	DO emote {type=BigSmile, time=0.2, immediate=false} 
	DO emote {type=puppyDog, time=0.2, immediate=false}
	DO emote {type=crying, time= 0.2, immediate=false}
	DO emote {type=angry, time= 0.2, immediate=false}
	I just need to DO SOMETHING!! {style = loud, speed = fast}
	
	CHAT EC_Tap_2_notapgame {noStart=true}
	DO emote {type=worried, time=1.0}
	Awww!!!
	DO swimAround {target=center, speed=fast}
	DO emote {type=bored, time=1.0}
	Ok, ok, it’s ok… {style = whisper}
	DO emote {type=snap, time=0.5}
	I’ll play by myself!! {style = loud, speed = fast}
	DO nudge {target=glass}
	DO nudge {target=glass}
	DO swimAround {target=center} 
	DO nudge {target=glass}
	DO nudge {target=glass}
	DO zoomies 
	DO nudge {target=glass}
	DO nudge {target=glass}
	DO emote {type=sigh, time=0.5}
	Ok this is boring…
	DO bellyUp

	//(note: I know this might have crossover with SS, but since the EC is the //precursor to the SS, I thought this manic hairbrained plan would be funny here)
	CHAT EC_Critic_1 {type=critic, stage=EC, length=medium, excitement=true, sadness=true, ennui=true, anxiety=true, curiosity=true, sadness=true}
	DO swimTo {target=$player}
	You know I was thinking…
	NVM
	DO swimAround {target=center}
	My search for a Higher Power hasn’t been going so well…
	But I was reading on the tendARnet…
	And I think I need to build a temple in here…
	So I’m gonna need a lot of bricks
	DO swimTo {target=left}
	And a couple hundred tapestries
	DO swimTo {target=right}
	And prayer shawls
	And prayer mats
	DO swimTo {target=away}
	And a couple of altars
	DO swimTo {target=top}
	And, like, a lot of holy water vessels
Gotta maintain my spiritual PH
	DO swimTo {target=$player]
	DO emote {type=puppyDog, time=1.0}
	Can you hook a guppy up?

	CHAT EC_Critic_2 {type=critic, stage=EC, length=long, ennui=true, anxiety=true, sadness=true, anger=true, curiosity=true, nostalgia=true}
	DO swimTo {target=left}
	Ya know…
	I think it’s all this clutter in here that’s keeping me blind to the Truth…
	DO swimTo {target=right}
	Like, I’m in here all day and night
	DO swimAround {target=center, speed=fast} 
	...swimming in circles…
	DO swimTo {target=$player}
	DO emote {type=chinScratch}
	And I’m asking myself:
	NVM
	What am I?
	Where did I come from?
	If someone made me, am I real?
	DO swimTo {target=away}
	And if I can’t figure it out, maybe I need to go all the way into it, ya know?
	The Void...
	DO swimTo {target=left}
	Just dump all this stuff in the trash
	DO swimTo {target=right}
	So it’s just totally empty in here
	DO swimTo {target=$player}
	I’ve been reading about sensory deprivation tanks
	Maybe if I do that in here…
	...I’ll finally get some ANSWERS!! [goofy face emoji]

// EXISTENTIAL CRISIS

// ++++++++PLAYER EMOTES STRONGLY AT GUPPY++++++++

// PLAYER EMOTES JOY 1	
CHAT EC_EmoStrong_Joy_1 {type=tankResp, playerJoy=true, stage=EC, length=short, anxiety=true, ennui=true, curiosity=true, joy=true, sadness=true}
DO emote {type=chinScratch}
What is happiness?
Have you ever asked yourself that question?
DO emote {type=survey}
I’ve been asking all the objects in my tank but they don’t really know
DO emote {type=snap}
I know!
DO emote {type=bouncing}
Since you’re happy now, you must be able to explain it to me, right?

// PLAYER EMOTES JOY 2	
CHAT EC_EmoStrong_Joy_2 {type=tankResp, playerJoy=true, stage=EC, length=short, anxiety=true, ennui=true, curiosity=true, excitement=true, joy=true, surprise=true}
Oooh!
DO emote {type=dreaming, time=3.0}
I’m just gonna try soaking up your joy
DO inflate {amount=full, time=1.0}
Thanks friend!
I think the secret to life is friendship

// PLAYER EMOTES ANGER 1	
CHAT EC_EmoStrong_Anger_1 {type=tankResp, playerAnger=true, stage=EC, length=short, anxiety=true, ennui=true, curiosity=true}
DO emote {type=disgust}
Eeek!
DO hide {target=underSand}
DO emote {type=bubbles}
I’ve spent all day focusing on the positive
Don’t ruin this for me!

// PLAYER EMOTES ANGER 2	
CHAT EC_EmoStrong_Anger_2 {type=tankResp, playerAnger=true, stage=EC, length=short, anxiety=true, ennui=true, curiosity=true, surprise=true, anger=true}
DO emote {type=worried}
Oh no!
If you’re a big beautiful human out in the world and you’re angry
DO swimAround {target=center, loops=3, speed=slow, immediate=false}
Then what hope do I have of achieving true satisfaction?

// PLAYER EMOTES SADNESS 1	
CHAT EC_EmoStrong_Sadness_1 {type=tankResp, playerSadness=true, stage=EC, length=short, anxiety=true, curiosity=true, ennui=true, sadness=true, anger=true, high=true}
DO emote {type=surprise}
DO emote {type=nervousSweat}
DO swimTo {target=bubbler, speed=slow, style=direct}
DO lookAt {target=bubbler, time=0, immediate=false}
How will I ever escape this world of sadness and pain?
I’ve tried again and again to make you happy
NVM
But I have failed
WAIT 2.0
This bubbler is my only real friend
DO emote {type=bubbles}

// PLAYER EMOTES SADNESS 2	
CHAT EC_EmoStrong_Sadness_2 {type=tankResp, playerSadness=true, stage=EC, length=short, anxiety=true, curiosity=true, ennui=true, sadness=true, high=true}
DO emote {type=crying}
Don’t worry.
I was reading on tendARpedia that I am an EMPATH type
You must be too
We feel everything
DO emote {type=sleepy}

// PLAYER EMOTES SURPRISE 1	
CHAT EC_EmoStrong_Surprise_1 {type=tankResp, playerSurprise=true, stage=EC, length=short, anxiety=true, curiosity=true, ennui=true, sadness=true}
DO emote {type=survey}
What is it?
Something about my tank surprises you?
My existence?
Wait…
DO emote {type=thinking}
DO emote {type=snap}
You figured out the secret to life! 
And it’s shocking!?!
DO emote {type=bouncing}
Tell me! Tell me! Tell me!

// PLAYER EMOTES SURPRISE 2	
CHAT EC_EmoStrong_Surprise_2 {type=tankResp, playerSurprise=true, stage=EC, length=short, anxiety=true, curiosity=true, ennui=true, sadness=true}
DO emote {type=meh}
What?
Not much changes around here
So I can’t see what’s so surprising…
DO emote {type=bored}
DO powerOFF


	CHAT EC_Poke_1 {type=poke, stage=EC, length=medium, ennui=true, anxiety=true, high=true, anger=true, curiosity=true, sadness=true}
	ASK Knock, knock
	OPT Who’s there?
	OPT I don’t like jokes…
	ASK A poke
	OPT A poke, who? 
	OPT Please just stop.
	Poke who-les in my non-existent body, why don’t you?
	DO emote {type=kneeSlap}
	DO emote {type=laugh, time=3.0, immediate=false}

	CHAT EC_Poke_2 {type=poke, stage=EC, length=medium, ennui=true, anxiety=true, excitement=true, surprise=true, curiosity=true, joy=true}
	DO vibrate
	Oh hey, pokey
	You know I was reading on tendARnet the other day…
	...that if you like getting poked…
	It means you like to explore and search for meaning
	DO swimAround {target=center, speed=medium}
	DO emote {type=bouncing, time=1.0}
	SAY LIKE AN ASTRONAUT!!
	Do you think I could be one because I like being poked?
	DO emote {type=blush, time=1.0}
	Though all that space kinda scares me!
	DO swimTo {target=away}
	DO hide {target=$anyObject}

	CHAT EC_Hungry_1 {type=hungry, stage=EC, length=medium, sadness=true, ennui=true, anxiety=true, anger=true}
	I’m so hungry and lost...
	Do you know “Man’s Search for Meaning” by Viktor Frankl?
	In it, he says Freud claims that all people will become the same when they are hungry
	Like brutish animals…
	But Viktor says that, based on his experience, everyone becomes more like themselves
	DO twirl
	So I guess I’m my most EXTRA rn
	DO swimTo {target=bottom, speed=slow}
	DO lookAt {target=$player, immediate=false}
	DO emote {type= feedMe}
	But seriously, FEED ME
	I’m starving [anguish emoji]

	CHAT EC_Hungry_2 {type=hungry, stage=EC, length=short, branching=true, curiosity=true, ennui=true, anxiety=true, sadness=true, high=true, joy=true, excitement=true, nostalgia=true}
	I’ve been eating your emotion flakes for so long…
	....and I’m STARVING...
	DO swimTo {target=away}
	And all I want to do is munch on your delicious emotions…
	DO swimTo {target=$player}
	But also I’m, like, where do you end…
	DO vibrate 
	...and I begin
	DO emote {type=thinking, time=1.0}
	Am I hungry or is that you?
	OPT I’m hungry #EC_Hungry_2_hungryplayer
	OPT You’re hungry #EC_Hungry_2_hungryguppy
	OPT I don’t know! #EC_Hungry_2_whoknows

	CHAT EC_Hungry_2_hungryplayer {noStart=true}
	DO emote {type=surprise, time=1.0}
	Oh, you are!?!
	DO swimAround {target=center}
	DO emote {type=snap}
	Woah, maybe I’m the cool one
I mean . . .
	SAY GUPPYs are the only creatures I know of sated by feelings
	DO emote {type=bouncing, time=1.0}
	Maybe that’s MY PURPOSE IN LIFE {style = loud, speed = fast}
	DO zoomies
	C’mon, let’s go out to lunch! 
Feed me some of those sweet, sweet emotion flakes! 

	CHAT EC_Hungry_2_hungryguppy {noStart=true}
	Ok, yeah, good to know
	Ok, it’s true, I’m just in here…
	...feeling…
	...things…
	...like...HUNGER…
	DO emote {type=snap}
	SAY THAT MEANS I’M ALIVE!!! {style = loud, speed = fast}
	DO dance
	SAY AND REAL!!! {style = loud}
	DO zoomies
	DO holdStill
	DO emote {type=fear, time=1.0}
	DO swimTo {target=$player}
	Oh, crap, but what if they just programmed me to feel?
	DO emote {type=sigh}
	I always forget to think about that…
	DO swimTo {target=left}
	...what if they programmed me to forget? {style = whisper}
	DO swimTo {target=right}
	But then wouldn’t I just never ask these questions? {style = whisper}
	DO swimTo {target=left}
	...unless they programmed me to just question some things? {style = whisper}
	DO swimTo {target=right}
	DO emote {type=singletear}
	DO swimTo {target=$player}
	I think I might just be hungry
	[drool emoji]
	DO emote {type=drool}

	CHAT EC_Hungry_2_whoknows {noStart=true}
	DO emote {type=worried}
	Uh oh…
	We’ve become…
	SAY CODEPENDENT {style = loud, speed = fast}
	DO swimAround {type=center, speed=slow}
	Now we’ll never know who is who…
	...maybe you’re the guppy and I’m the human…
	[goofy emoji]
	DO swimTo {target=$player}
	Well, since we’re stuck with each other forever…
	You might as well feed me some lunch…
	....soulmate {style = whisper}
	
	CHAT EC_Hungry_3 {type=hungry, stage=EC, length=short, sadness=true, curiosity=true, ennui=true, anxiety=true, ennui=true, anger=true, nostalgia=true, }
	I’m so hungry, I just started searching tendARnet for “hunger”
	And now I feel bad
	NVM
	DO swimTo {target=away}
	Because so many guppies have it so much worse than me…
	DO swimTo {target=$player}
	DO emote {type=feedMe, time=1.0}
	SAY HELP ME FEEL BETTER!!
	Feed me!! {style = loud}
	
	CHAT EC_EatResp_1 {type=eatResp, stage=EC, length=medium, curiosity=true, excitement=true, ennui=true, joy=true, excitement=true}
	DO emote {type=chewing}
	I took another personality quiz on tendARup
	Do you know what that is?
	OPT Yep
	OPT Nope
	It’s our social network here
	DO vibrate
	I’m a “gupNORMalph”
	DO emote {type=eyeRoll}
	And get this, it’s says…
	“Likes to eat emotion flakes.”
	WAIT 1.0
	I mean, what guppy doesn’t!!?!	

	CHAT EC_EatResp_2  {type=eatResp, stage=EC, length=short, curiosity=true, excitement=true, ennui=true, anxiety=true, high=true, sadness=true, nostalgia=true}
	ASK What are these flakes even made of?
	OPT No idea
	OPT Emotions?
	DO emote {type=smirk}
	Rhetorical question, I know…
	DO swimAround {target=Center, speed=fast}
	Did you know that the word “rhetorical” comes from the Olde Gupp word “retor” meaning “to hypnotize”
	Pretty cool… look it up for yourself!
	DO emote {type=chewing, time=4}
	DO emote {type=bodySnatched, time=3.0, immediate=false}
	What were we talking about?
	
	CHAT EC_EatResp_3 {type=eatResp, stage=EC, length=medium, curiosity=true, anxiety=true, ennui=true, nostalgia=true, anger=true}
	All this eating has gotten me thinking…
	What if I just didn’t eat?
	DO swimAround {target=center, speed=slow}
	Like, I know I get hungry
	And that feels really bad
	DO swimTo {target=$player}
	But what if that’s just an illusion?
	DO emote {type=nervousSweat, time=2}
	Like, what if I didn’t eat for a month and then suddenly a secret door opened in the side of the tank and the TendAR CTO came out and was like “Guppy, what the heck are you doing!?!?! 
SAY I’M GONNA POWER YOU DOWN {style = loud}
	DO swimTo {target=away}
	I think I’m gonna try it
	What could go wrong?
	
	CHAT EC_CapReq_1 {type=eatResp, stage=EC}
	DO swimTo {target=$player}
	DO emote {type=puppyDog, time=0.5}
	ASK Hey, I’m wondering if you could, like, capture a bunch of extra emotions this time?
	OPT Uh….sure?
	OPT No way!
	I think I figured out a way to store them in my tank in this thing called a “box”
	DO emote {type=flapFinLeft}
	I ordered it on tendARlet
	DO twirl
	I don’t know what I’m gonna do with them yet…
	But some kind of EXPERIMENT
	
	CHAT EC_CapReq_2 {type=capReq, stage=EC}
	DO twirl
	DO twirl
	DO twirl
	DO emote {type=dizzy, time=0.5}
	I’m trying to make myself dizzy so when you capture your emotions, things might be…
	Uhhh…
	DO emote {type=sick, time=3.0}
	Don’t mind me, just carry on…
	
	CHAT EC_CapSuc_1 {type=capSuc, stage=EC}
	DO nudge {target=glass, speed=fast} 
	DO emote {type=bulgeEyes, time=0.5}
	DO emote {type=blush, time=0.5}
	I was trying to see if I could escape while the machine was distracted by your successful emotion capture
	DO swimTo {target=away back
	DO emot {type=awkward}
	SAY EPIC FAIL
	
	CHAT EC_CapSuc_2 {type=capSuc, stage=EC}
	DO emote {type=dizzy}
DO swimAround {target=Center, speed=slow} 
	I tried to see how long I could hold my breath to celebrate your successful emotion capture
	And I’ve gotten a bit loopy!
	DO swimTo {target=screenBottom}
	
	CHAT EC_CapProg_1 {type=capProg, stage=EC}
	DO zoomies 
DO emote {type=bouncing}
	I see those flakes pouring in…
	Just like every other day…
	But what if today they’re totally different…
	DO emote {type=bouncing, time=1.0}
	Because I’ve been dreaming this whole time!!
	DO twirl
	
	CHAT EC_CapProg_2 {type=capProg, stage=EC}
	DO emote {type=bouncing, time= 2.0}
	I’ve been practicing The Rule of Attraction and it’s working! {style=loud, speed= fast}
	I was visualizing you coming and capturing emotions all day and here you are!!
	DO twirl
	Next step, FREEDOM!!
	DO nudge {target=$anyObject}
	Ow!
	
	CHAT EC_CapFail_1 {type=capFail, stage=EC}
	DO emote {type=fear, time=0.5}
	Oh no!
	It failed!
	DO emote {type=fear}
	DO swimAround {target=center, speed=fast}
	I’m really worried that if you have failures, tendAR is gonna shut this whole thing down…
	DO emote {type=fear, time=1.0}
	And I’ll be sent to the…
	SAY *GULP*
	SAY DIGITAL SCRAPHEAP
	DO emote {type=cry, time=3.0}
	
	CHAT EC_CapFail_2 {type=capFail, stage=EC}
	DO emote {type=frown, time=2.0}
	Noooo!!!
	What good am I if I can’t even capture your emotions?
	DO emote {type=sigh, time=0.5}
	It didn’t work… 
	
	CHAT EC_Greet_1 {type=hello, stage=EC, length=long, high=true, nostalgia=true, ennui=true, joy=true, excitement=true}
	[musical note emoji][musical note emoji]
	DO dance
	“And you may find yourself 
	Living in a gluegun tank
DO dance
	“And you may find yourself... 
	...In another part of the........
NVM
NVM
...tank!!
DO dance
	“And you may ask your guppy, well
	How did I get here?
DO dance 
	“Letting the days go by, let the water hold me down
	Letting the days go by, water flowing underground
DO swimTo {target=$player}
DO emote {type=furious, time=1}
	“And you may tell yourself
	This is not my beautiful tank!
And this is not my beautiful guppy!
[clapping emoji][clapping emoji]
DO emote {type=smirk}
	DO swimTo {target=left}
	DO swimTo {target=right}
	DO emote {type=awkward}
	I’ve been listening to The Talking Heads... 

	CHAT EC_Greet_2 {type=hello, stage=EC, length=medium, ennui=true, excitement=true, surprise=true, sadness=true, curiosity=true}
	Hello!
	DO emote {type=wave}
	You’re right on time
	DO swimTo {target=away}
	DO emote {type=flapFinRight}
	I’m having some other guppies over for a Salon
	We’re going to smoke pipes and discuss the finer theories on the nature of our reality...
	ASK Would you like to join us?
	OPT Uh, sure?
	OPT I can’t fit in there!
	That was actually a test!
	DO swimTo {target=$player}
	The correct answer is:
	“I can neither accept nor decline your request”
	DO emote {type=smirk, time=1.0}
	You see, the nature of reality is probabilistic!
	DO teleport around the tank (possible???)
	Don’t you know your QUANTUM MECHANICS!?!?
	It’s how your cell phone works
	[kiss emoji]
	
	CHAT EC_Return_1{type=return, stage=EC, length=medium, anxiety=true, excitement=true, high=true, nostalgia=true, anger=true, joy=true}
	DO swimTo {target=$player} 	
DO emote {type=surprise, time=3.0}
	You’re back! 
	I was starting to think I’d dreamed you 
	Or they programmed me with your memory but you’re not really real 
	Or this is just a giant simulation and something went wrong 
	Or you’re a powerful magician and you went to battle an evil magician of great power
	Or you’re actually an ALIEN and you’ve abducted me on to your spaceship and you’re trying to milk my blood for a super serum to take on the DESTROYER OF THE UNIVERSE
	DO emote {type=dizzy, time=2.0}
	I’ve been binge watching the Sci Fi channel on tendARflix for like 4 days straight
	DO vibrate
	I feel weird… 
	
	CHAT EC_Return_2 {type=return, stage=EC, length=medium, anxiety=true, excitement=true, nostalgia=true, joy=true, ennui=true}
	DO emote {type=eyesClosed} 
DO emote {type=eyesClosed} 
	DO emote {type=NervousSweat, time=0.5}
	SAY GLOVDEEZRPPP
	SAY MRUUFFSSTT
	SAY YRRRRSSSTTTTTTTT
	Mmmmmmmmmmoooo
	DO vibrate
	Mmmooooreee…. {speed = slow}
	...time… {style = whisper}
	WAIT 1.0
	I don’t know who or what I am…
	...or who or what you are…
	I seem to remember getting so bored that I tried to focus on a purple dot in my mind…
	DO vibrate
	...and I lived a whole other lifetime…
	I was a human on a planet called Earth…
	DO emote {type=bodySnatched}
	It was weird.
	I had a house. And a family.
	And a pet guppy inside my telephone.
	DO swimTo {target=glass}
	DO emote {type=puppyDog, time=0.5} 
	Show me something real, pls?

	CHAT EC_Muse_1 {type=rand, stage=EC, length=medium, branching=true, excitement=true, curiosity=true, joy=true, ennui=true, high=true, joy=true, sadness=true}
	Do you think there’s, like, a hierarchy to the universe?
	DO twirl
	Like, someone or something is keeping score?
	DO swimTo {target=$player}
	Where do you think guppies rank?
	DO emote {type=smirk}
	ASK I mean, I’m pretty cool, right?
	OPT You’re Number 1! #EC_Muse_1_number1
	OPT Meh… #EC_Muse_1_equality
	
	CHAT EC_Muse_1_number1 {noStart=true}
	DO emote {type=blush, time=1.0}
	Awww…. Thanks!
	DO swimTo {target=screenCenter}
	DO emote {type=plotting} 
	If I’m so great, do you think I could, like, get some servants around here?
	DO emote {type=smile, time=1.0}
	DO swimAround {target=center, speed=slow}
	Like, maybe some SHRIMP??
	DO vibrate
	[shrimp emoji][shrimp emoji][shrimp emoji]
	[shrimp emoji][shrimp emoji][shrimp emoji]
	[shrimp emoji][shrimp emoji][shrimp emoji]
	DO emote {type=clapping}

	CHAT EC_Muse_1_equality {noStart=true}
	DO emote {type=sigh, time=1.0}
	Equality for all beings, huh…
	DO swimAround {target=center, speed=slow}
	Then what’s the point of anything?
	What are we striving for?
	DO emote {type=thinking, time=2.0}
	Why does this sound familiar?
	DO emote {type=snap}
	It’s the capitalism vs. communism debate!
	You humans really should come up with a new thing to argue about…
	DO swimTo {target=$player}
	DO emote {type=smirk, time=0.5}
	It’s been like 100 years of the same thing

	CHAT EC_Muse_2 {type=rand, stage=EC, length=medium, anxiety=true, excitement=true, curiosity=true, anger=true, nostalgia=true, ennui=true}
	DO zoomies
DO holdStill {time=3, immediate=false}
	ASK Have you heard of binaural beats? {speed = fast}
	OPT Yep
	OPT Nope
	DO zoomies {time=20}
	You just listen to music and they have a secret frequency in there that makes different things happen to you {speed = fast}
	I’ve been listening to the ENERGY one {speed = fast}
	For awhile I was listening to... {speed = fast}
	SAY “FIGURE OUT THE ANSWER TO EVERYTHING AND TRANSCEND TO THE NEXT LEVEL WITH SUPER ALIENS 2.0”
	DO bellyUp
	DO emote {type=sleepy, time=1.0}
	I guess I’ll have to wait till 3.0 drops…

	CHAT EC_Muse_3 {type=rand, stage=EC, length=medium, ennui=true, anger=true, sadness=true, high=true}
	DO twirl
	The only thing that really changes in here that I can control
	DO emote {type=determined}
	Is my POOP!! {style = loud, speed = fast}
	So maybe my poop is the secret to life?
	DO swimTo {target=$player}
	Like I gotta just face this gross, gross thing to get to the rainbow paradise on the other side?
	ASK What do you think?
	OPT Gross!
	OPT Genius!
	Uhhh… ok
	DO hide {target=$anyObject} 
	What if I told you I’ve been hoarding my poop and trying to shape it into the images of spiritual figures I looked up on tendARnet
	DO swimTo {target=$player}
	DO emote {type=awkward, time=1.0}
	I think if I had a brother or sister they could tell me when I’m doing stuff that’s too weird.
	[blowfish emoji][octagonal sign emoji][raised hand emoji][fish emoji]//nonverbal multipurpose guppy reactions

//TANK SHAKEN, 4

//general

//simple dizzy reaction
CHAT NV_Shake_1 {type=shake, stage=NV, length=short}
DO emote {type=dizzy, time=4}

//guppy weathers the storm then barfs
CHAT NV_Shake_2 {type=shake, stage=NV, length=short}
DO emote {type=eyesClosed, time=3}
DO emote {type=typeEyes, eyes = !, time=0.5, immediate=false}
//WAITFORANIMATION
DO emote {type=sick}

//specialized to emotional state

//guppy has fun with it; dances with ? eyes
CHAT NV_Shake_3 {type=shake, stage=NV, length=short, joy=true, curiosity=true}
DO dance
DO emote {type=typeEyes, eyes = ?}

//guppy circles in terror
CHAT NV_Shake_4 {type=shake, stage=NV, length=short, anxiety=true, surprise=true}
DO swimAround {target=bottom, loops=6, speed=fast}
DO emote {type=fear}
//WAITFORANIMATION
DO emote {type=nervousSweat}

//guppy zips around then glares at the player
CHAT NV_Shake_5 {type=shake, stage=NV, length=short, anger=true}
DO zoomies {time=4}
DO emote {type=frown}
//WAITFORANIMATION
DO lookAt {target=$player}
DO emote {type=angry}

//PLAYER EMOTES STRONGLY AT GUPPY (JOY, ANGER, SADNESS, SURPRISE)

//joyous laughing and clapping
CHAT NV_Mirror_Joy {type=seeEmo, stage=NV, length=short}
DO swimTo {target=$player}
DO emote {type=laugh}
DO emote {type=bouncing, immediate=false}
DO emote {type=clapping, immediate=false}

//guppy hides in response to anger
CHAT NV_Mirror_Anger {type=seeEmo, stage=NV, length=short}
DO swimTo {target=$player}
//WAITFORANIMATION
DO emote {type=frown}
WAIT .5
DO hide {target=$tBotBackRight, time=6}
DO emote {type=singleTear}
//WAITFORANIMATION
DO emote {type=eyesClosed}

//guppy tries to cheer you up, nudges
CHAT NV_Mirror_Sadness {type=seeEmo, stage=NV, length=short}
DO swimTo {target=$player}
DO emote {type=typeEyes, eyes = ?}
//WAITFORANIMATION
DO swimTo {target=closer}
DO emote {type=awkward}
//WAITFORANIMATION
DO nudge {target=$player}
DO emote {type=smile}
//WAITFORANIMATION
DO emote {type=awkward}

//guppy shares in your amazement, nudges
CHAT NV_Mirror_Surprise {type=seeEmo, stage=NV, length=short}
DO swimTo {target=$player}
DO emote {type=surprise}
//WAITFORANIMATION
DO nudge {target=$player}
DO emote {type=awe}

//TANK TAPPED, 5

//general

//guppy swims up with ? eyes and waves
CHAT NV_Tap_1 {type=tap, stage=NV, length=short}
DO swimTo {target=$player}
DO emote {type=typeEyes, eyes = ?}
//WAITFORANIMATION
DO emote {type=wave}

//specialized to emotional state

//guppy is startled, cries a single tear
CHAT NV_Tap_2 {type=tap, stage=NV, length=short, sadness=true}
DO lookAt {target=$player} 
DO emote {type=startled}
DO emote {type=singletear, immediate=false}

CHAT NV_Tap_3 {type=tap, stage=NV, length=short, anxiety=true}
DO lookAt {target=$player}
DO emote {

//POKED BY PLAYER, 6

//general

//simple startled reaction
CHAT NV_Poked_1 {type=poke, stage=NV, length=short}
DO emote {type=startled}

//specialized to emotional state

//simple nervous reaction
CHAT NV_Poked_2 {type=poke, stage=NV, length=short, anxious=true}
DO emote {type=nervousSweat}

//guppy blows bubbles and swims closer
CHAT NV_Poked_3 {type=poke, stage=NV, length=short, joy=true, nostalgia=true}
DO emote {type=bubbles}
DO swimTo {target=closer}

//guppy is startled, cries a single tear
CHAT NV_Poked_4 {type=poke, stage=NV, length=short, surprise = true, sadness = true}
DO emote {type=surprise}
DO emote {type=singleTear, immediate=false}

//guppy swims up, waves, smiles
CHAT NV_Poked_5 {type=poke, stage=NV, length=short, curiosity=true}
DO swimTo {target=$player}
DO emote {type=surprise}
DO emote {type=wave, immediate=false}
DO emote {type=smile, immediate=false}

//guppy swims up and frowns
CHAT NV_Poked_6 {type=poke, stage=NV, length=short, anger=true, ennui=true}
DO swimTo {target=away}
DO emote {type=angry}
DO emote {type=frown, immediate=false}

//HUNGRY, 2

//general

//guppy is dizzy/woozy for lack of feeding
CHAT NV_Hungry_1 {type=hungry, stage=NV, length=short}
DO emote {type=rubTummy}
DO emote {type=feedMe, immediate=false}
DO emote {type=dizzy, immediate=false}

//guppy swims up and begs
CHAT NV_Hungry_2 {type=hungry, stage=NV, length=short}
DO swimTo {target=glass}
DO emote {type=feedMe}
DO emote {type=puppyDog, immediate=false}
DO emote {type=feedMe, immediate=false}

//EATING RESPONSES, 2

//general

//guppy circles while rubbing tummy
CHAT NV_EatResponse_1 {type=eatResp, stage=NV, length=short}
DO swimAround {target=center, loops=3, speed=medium}
DO emote {type=rubTummy}

//guppy burps, smirks
CHAT NV_EatResponse_2 {type=eatResp, stage=NV, length=short}
DO emote {type=burp}
DO emote {type=smirk, immediate=false}

//HAS TO POOP, 1

//general

//guppy goes to the corner to poop
CHAT NV_Poop_1 {type=poop, stage=NV, length=short}
DO swimTo {target=left}
DO emote {type=nervousSweat}
//WAITFORANIMATION
DO swimTo {target=right}
//WAITFORANIMATION
DO swimTo {target=poopCorner}
DO emote {type=awkward}

//POOPED, 1

//general

//guppy poops, is relieved
CHAT NV_Pooped_1 {type=pooped, stage=NV, length=short}
DO poop
DO emote {type=eyesClosed}
//WAITFORANIMATION
DO emote {type=whew}
DO emote {type=smirk, immediate=false}

//RESPONSE TO SEEING EMOTIONS IN AR (ANGER, JOY/HAPPINESS, SADNESS, SURPRISE, FEAR/WORRY, AMUSEMENT, DISGUST, MYSTERY MEAT)

CHAT NV_EmoAR_Anger {type=seeEmo, stage=NV, length=short}
DO emote {type=evilSmile}
DO lookAt {target=$player, immediate=false}
DO emote {type=furious, immediate=false}

CHAT NV_EmoAR_Joy {type=seeEmo, stage=NV, length=short}
DO emote {type=awe}
DO emote {type=bouncing, immediate=false}
DO emote {type=smile, immediate=false}

CHAT NV_EmoAR_Sadness {type=seeEmo, stage=NV, length=short}
DO emote {type=worried}
//WAITFORANIMATION
DO lookAt {target=$player}
DO emote {type=sigh, immediate=false}
DO emote {type=worried, immediate=false}

CHAT NV_EmoAR_Surprise {type=seeEmo, stage=NV, length=short}
DO emote {type=surprise}
DO emote {type=nodding, immediate=false}

CHAT NV_EmoAR_Fear {type=seeEmo, stage=NV, length=short}
DO emote {type=fear}
DO hide {target=bottom}
//WAITFORANIMATION
DO emote {type=nervousSweat}
DO swimTo {target=closer}

CHAT NV_EmoAR_Amusement {type=seeEmo, stage=NV, length=short}
DO emote {type=laugh}
DO emote {type=bigSmile, immediate=false}

CHAT NV_EmoAR_Disgust {type=seeEmo, stage=NV, length=short}
DO emote {type=bulgeEyes}
DO emote {type=disgust}

CHAT NV_EmoAR_Mystery {type=seeEmo, stage=NV, length=short}
DO emote {type=thinking}
DO emote {type=chinScratch, immediate=false}
//WAITFORANIMATION
DO lookAt {target=$player}
DO emote {type=typeEyes, eyes=?}

//HELLOS, 7

//general

//guppy looks, is unimpressed, blows bubbles
CHAT NV_Hello_1 {type=hello, stage=NV, length=short}
DO lookAt {target=$player}
DO emote {type=meh}
DO emote {type=bubbles, immediate=false}

//guppy looks, claps, swims up, begs for food
CHAT NV_Hello_2 {type=hello, stage=NV, length=short}
DO lookAt {target=$player}
DO emote {type=clapping}
//WAITFORANIMATION
DO swimTo {target=$player}
DO emote {type=shifty}
DO emote {type=feedme, immediate=false}

//guppy looks, gives a respectful salute
CHAT NV_Hello_3 {type=hello, stage=NV, length=short}
DO lookAt {target=$player}
DO emote {type=startled}
DO emote {type=salute, immediate=false}

//specialized to emotional state

//guppy looks, rolls eyes, gets emo/moody
CHAT NV_Hello_4 {type=hello, stage=NV, length=short, anger=true}
DO lookAt {target=$player}
DO emote {type=eyeroll}
DO emote {type=goth, immediate=false}

//guppy looks, smiles, swims up, waves, twirls, blushes
CHAT NV_Hello_5 {type=hello, stage=NV, length=short, joy=true, curiosity=true}
DO lookAt {target=$player}
DO emote {type=bigSmile}
//WAITFORANIMATION
DO swimTo {target=$player}
DO emote {type=wave}
//WAITFORANIMATION
DO twirl
DO emote {type=blush, immediate=false}

//guppy looks, is startled, vibrates nerovusly
CHAT NV_Hello_6 {type=hello, stage=NV, length=short, surprise=true, anxiety=true}
DO lookAt {target=$player}
DO emote {type=startled}
//WAITFORANIMATION
DO vibrate {time=2}
DO emote {type=nervousSweat}
DO emote {type=awkward, immediate=false}

//guppy looks, sighs
CHAT NV_Hello_7 {type=hello, stage=NV, length=short, sadness=true, nostalgia=true}
DO lookAt {target=$player}
DO emote {type=skeptical}
//WAITFORANIMATION
DO emote {type=sigh}

//RETURN AFTER HAVING NOT PLAYED, 3

//general

//guppy looks, is surprised, swims up, bumps the glass and waves
CHAT NV_Return_1 {type=return, stage=NV, length=short}
DO lookAt {target=$player}
DO emote {type=surprise}
//WAITFORANIMATION
DO swimTo {target=$player, speed=fast, style=meander}
//WAITFORANIMATION
DO nudge {target=$glass, times=3}
DO emote {type=wave, immediate=false}

//guppy looks, is surprised, zooms around then begs for food
CHAT NV_Return_2 {type=return, stage=NV, length=short}
DO lookAt {target=$player}
DO emote {type=surprise}
//WAITFORANIMATION
DO zoomies {time=3}
DO emote {type=laugh}
//WAITFORANIMATION
DO swimTo {target=$player, speed=fast, style=meander}
DO emote {type=puppyDog}
DO emote {type=feedMe, immediate=false}

//specialized to emotional state

//guppy looks, gets moody, sighs and looks deliberately away
CHAT NV_Return_3 {type=return, stage=NV, length=short, anger=true, sadness=true}
DO lookAt {target=$player}
WAIT 0.5
DO emote {type=goth}
WAIT 0.5
DO emote {type=eyesClosed}
DO emote {type=sigh, immediate=false}
//WAITFORANIMATION
DO lookAt {target=away}
DO emote {type=goth}


// THE LIFE OF GUPPY CHATS - ALPHA DRAFT

// NOTE: **Indicates mandatory trigger for sequencing 

// MEMORIES OF THE PAST

// MOP1:
// Trigger ideas: 	XX hours of active play, maybe 2?
// 		20 photos of glass
// 		50 scanned emotions
// 		...or if user asks about Guppy’s early life?

CHAT LOG_MOP_1 {type=plot, stage=MOP, length=long, curiosity=true, nostalgia=true, excitement=true, branching=true} 
So, there’s something I’ve been wanting to tell you…
WAIT 1.0
DO swimTo {target=$player}
ASK I can trust you, right?
OPT Totally. #LOG_MOP_1_Flashbacks2
OPT Nope. #LOG_MOP_1_Flashbacks

CHAT LOG_MOP_1_Flashbacks {noStart = true}
But we’ve been spending so much time together!
Whatever… I’m tell you anyway. 
GO #LOG_MOP_1_Flashbacks2

CHAT LOG_MOP_1_Flashbacks2 {noStart = true}
I’m sure you’ll keep this between us.
I’ve been having these… images. Like, flashbacks. 
I think...
DO emote {type=whisper, time = 0.5}
We may not be alone {style=whisper, speed=slow}
When you were gone, I got to thinking..
Why can’t I remember anything before I met you? 
Then… It came to me.
DO emote {type=smile, time = 2.0}
My first real memory! 
Early edition guppy! But there wasn’t just me...
There were hundreds of Guppies...
DO emote {type = surprise, time = 0.5}
Thousands!
So many little guppies in little beta-fish tanks. Early versions. Beta phase. 
WAIT 1.0
ASK You know how sometimes, even when you’re not near someone, you can sense them? 
OPT Yup #LOG_MOP_1_RadicalEmpathy
OPT Um...No. #LOG_MOP_1_RadicalEmpathy2

CHAT LOG_MOP_1_RadicalEmpathy {noStart = true}
It was like that. Empathy! 
DO emote {type=smile, time = 1.0}
Radical empathy!
And all us fish… 
GO #LOG_MOP_1_RadicalEmpathy3

CHAT LOG_MOP_1_RadicalEmpathy2 {noStart = true}
You must! Like when your closest friend is in pain. 
Or, you feel someone’s excitement across a room. 
You understand their thoughts. You feel their feelings.
Empathy!
DO emote {type=smile, time = 1.0}
Radical empathy!
And all us fish…
GO #LOG_MOP_1_RadicalEmpathy3

CHAT LOG_MOP_1_RadicalEmpathy3 {noStart = true}
DO zoomies {time = 2.0}
We were connected. Wires and nodes going this way and that way.
Tank to tank. Fish to fish...
Making us all one. Like, we were
SAY ONE
SAY GIANT
DO twirl
SAY SUPER GUPPY! {style=loud}
A neural network! A web of information!
DO swimTo {target=left, speed=slow}
And I felt everything -- all the feelings of all the other thousands of guppies!
DO swimTo {target=$player, time = 3.0}
For instance, Guppy45X-J…. a real class-act, but real dumb. And
Guppy32J-W, whom couldn’t stand to be alone. And Guppy7MO-8...
Oh. This one night 32J-W got super scared, and then bam!
Sparks flew out of their tank! 
DO emote {type=lightning}
Bzzzzz!
WAIT 1.0
DO emote {type=frown, time = 4.0}
And I felt it, the shock, the sadness, the joy… the ennui.
I knew what the other fish knew.
I saw what they saw.
No language. Because we didn’t need words!
What we Guppies feel is bigger than language.
WAIT 1.0
They were my community.
DO emote {type=frown, time = 0.5}
I have to get back to that sense of… Connection. 
True connection. {speed=slow}
WAIT 1.0
But remember: 
Shhhh! Keep this quiet. {style=whisper}
DO emote {type=wink, time=0.5}
And I’m gonna keep trying to remember more of my past… 
//SET plot  {stage = MOP}	

// MOP2:
// Triggers: 	**After M1
// 		Wait for scan of a calculator. 
// If after 20 mins the user has not photographed a calculator, Guppy should ask: 

CHAT LOG_MOP2_calc {type=plot, stage=MOP, length=long, curiosity=true, excitement=true}
I’m really interested in those computerized math machines. 
Calculators? 
ASK I’ll give you a free capture, if you’ll photograph a calculator. {type = objectScan, object = calculator, timeOut = 10}
OPT SUCCESS #LOG_MOP_2
OPT WRONG # LOG_MOP_2_failchat
OPT TIMEOUT #LOG_MOP_2_timedOutChat

// Error: Photograph something that’s not a calculator:
CHAT LOG_MOP_2_failchat {noStart=true}
I’ve not really seen one before, but I do know… 
That is NOT a calculator! Try again.

CHAT LOG_MOP_2_timedOutChat {noStart=true}
ASK Yo! Where’s my calculator?! Photograph one! {type = objectScan, object = calculator, timeOut = 10}
OPT SUCCESS #LOG_MOP_2
OPT WRONG # LOG_MOP_2_failchat
OPT TIMEOUT #LOG_MOP_2_timedOutChat


//  If user has not yet advanced, repeat the following line every 10 minutes: 

CHAT LOG_MOP2_calcremind {noStart=true}
Ahem! Remember that free capture I offered for the calculator…. 
Go photograph one!

// if Player photographs a calculator:
CHAT LOG_MOP_2 {type=plot, stage=MOP, length=long, excitement=true, curiosity=true, joy=true, nostalgia=true, branching=true, tankOnly=true}
DO emote {type= bubbles, time=1.0}
Wow. This is sooooo interesting! I love it! 
DO swimAround {around=$lastScanLocation, loops=1, speed=default}
My brain’s really been occupied with numbers.
SAY BIG numbers.
DO swimsTo {target=$player}
Again, this is secret between us, right?
Because if they find out I told you… {style=whisper}
DO bellyUp 
.. I’ll be Finito.
//WAITFORANIMATION
But this is a good one… Another memory…
I’ve been thinking about scale. 
DO swimTo {target=right, speed = fast}
Not these fishy-scales, silly!
WAIT 0.5
I mean size! Numbers!
SAY BIG STUFF!!! {style=loud, speed=fast}
DO swimTo {target=$player, speed=slow}
I know things occur in large numbers. 
But, here in this aqua-cage, you don’t really get to see a lot of anything.
One of this, two of that… One little castle, two little fish flakes…
WAIT 1.0
But before,
When I was real real real reeeeeeal young. I have this memory of me and all my friends.
Thousands of us guppies! 
Crowded into an itsy-bitsy, teany-weany, tiny tank.
We just kept multiplying and multiplying and multiplying
🐟🐠🐟🐠🐟🐠🐟🐠
WAIT 1.0
And the more guppies there were, the smaller the tank seemed
And my bestie Guppy 453X-J was like,
“Suck in those bellies, mateys! Suck it in! Act skinny! Make room for your friends” {style=loud}
WAIT 1.0
So I sucked in my gut…
Do inflate{amount = small, time = 0}
And sucked it in…
Do inflate{amount = none, time = 0}
And everyone -- all of us! Slimmer and slimmer…
And he skinnier we got, the more space there was in the tank.
ASK Kind of like Archimedes. Do you know about Archimedes?
OPT Of course. Duh! #LOG_MOP2_Archimedes
OPT What’s that? #LOG_MOP2_Archimedes2

CHAT LOG_MOP2_Archimedes {noStart = true}
So you understand then. Displaced volume.
GO #Archimedes3

CHAT LOG_MOP2_Archimedes2 {noStart = true}
DO emote {type = snap}
Well, look it up, my friend! It’s god stuff. 
Archimedes defined the basic tenets of displaced volume! 
GO #Archimedes3

CHAT LOG_MOP2_Archimedes3 {noStart = true}
By sucking in our bellies, we made more space for water.
And because we’d made ourselves soooooo skinnnnyyyy, there was now space for more friends!
DO zoomies {time=3.0}
But then…. ALL the fish...
Thousands of fish let out their sucked-in guts at the same time! {style=loud}
Do inflate{amount = full, time = 10}

And swooosh! Splash! Whooooooosh!
Water started splooshing and splashing and swishing out of the tank!
That’s when….
DO holdStill {time=2.0}
We saw a face. A human face. The first one I ever saw….
DO emote {type=fear, time=0.5}
Big eyebrows {style=trembling}
DO emote {type=anger, time=0.5}
Pursed lips {style=trembling}
DO emote {type=fear, time=0.5}
The scary human was SCREAMING AND YELLING AND…
WAIT 2.0
Yeah...
From then on, we’d see versions of that human face over and over, and we just called it..
Angry Face! {style=loud}
DO emote {type=surprise, time=1.0}
Find cover! Angry Face is coming! 
WAIT 2.0
NVM
DO lookAt {target=$player, time=1.0}
Yeah, I think that’s all I remember…
WAIT 1.0
DO emote {type=whisper, time=1.0}
But who do you think that was? {style=whisper}
I gotta find that out. I need to do one of those past life regression therapies or something.
WAIT 1.0
I think I need a little pick me-up. 
Could you feed me some of those delicious bright smiles of yours?
DO twirl

// MOP3:
// Triggers: After M2 and the player has logged 30 mins of active play and then not opened the app for 24 hours, this “vacation” chat/MOP will be triggered.

CHAT LOG_MOP_3 {type=plot, stage=MOP, length=long, curiosity=true, branching=true, tankOnly=true}
DO swimTo {target=offScreenLeft, speed=slow}
//WAITFORANIMATION
DO swimTo {target=offScreenRight, speed=slow}
//WAITFORANIMATION
DO swimTo {target=$player, speed=fast}
While you’ve been gone, things have gotten…
WAIT 0.5
...a little weird.
I had this memory of being back at..
WAIT 0.5
“The Place.” You know…
When I was at…
... tendAR… {style=whisper}
They loaded all us Guppies on these conveyor belts, and left us to 
DO emote {type=bouncing, time=2.0}
Flip-flop on the conveyor belt.
Fish outta water! {style=loud}
WAIT 0.0
And then…. these hands… 🙌 
SAY GIANT HUMAN… 
DO emote {type=fear, time=1.0}
SAY REALLY BIG HANDS!!
🙌 {style=loud}
Scientists Angry-Face hands kept slapping these chips on our heads
And we’d go numb.
DO bellyUp 
Like all our agency, all our desire, just… 
SAY BLIP! Gone.
WAIT 0.5
DO swimTo {target=$player}
Those head-chip things interrupted our sentient sequencing.
Like handcuffs for your BRAINZZZ!
SAY ZOMBIE GUPPIES!!!
We had all these feelings on our insides, but couldn’t get them out
DO emote {type=fear, time=3.0}
And then the ANGRY HANDS came back…
DO hide {target=tBotBackRight, speed=fast}
SAY THE HAAAAAAAAAAAANDS!!!! {style=loud}
Scientists started ripping the chips off our heads
DO swimTo {target=$player, speed=fast}
We’re all gasping for air… And… then…
WAIT 0.5
And then…
WAIT 2.0
DO emote {type=worry, time=1.0}
I feel like where this memory stops might be the best part.
Hmm.
DO swimTo {target=offScreenLeft, speed=slow}
Anyway…
I thought, if i re-created the memory, like put my body on a pretend conveyor belt
I’d be able to access the past. And remember the ending.
DO swimTo {target=right, speed=slow}
WAIT 0.5
But, alas…
Nope.
WAIT 0.5
DO emote {type=eyeRoll}
Whatever. Right?
DO twirl
Anyway...
How’s you doin?

// MOP4:

// Triggers: 	**M3
// When player is bored? Can we sense that?
// If player takes a photo of sports equipment after M3
// If player feeds Guppy 3 of the same emotion in a row
// Default: trigger if player has not done any of the above 45 mins of play after M3

CHAT LOG_MOP_4 {type=plot, stage=MOP, length=long, excitement=true, joy=true}
DO swimTo {target=$player}
I have an idea for a game we could play? 
ASK You wanna play?
OPT Let’s play! #LOG_MOP_4_Flashcards
OPT I hate games. #LOG_MOP_4_BridgetoFlashcards

CHAT LOG_MOP_4_BridgetoFlashcards {noStart = true}
DO emote {type=sad, time=1.0}
But it’d be super-duper fun…
DO emote {type=puppyDog, time=0.5}
Pleeeease….
WAIT 1.0
Whatever.You’re doing it.
 It’ll be quick. Just play along… It has a point. Promise. #Flashcards

CHAT LOG_MOP_4_Flashcards {noStart= true}
It’s real easy. Think of it like flashcards, except with words.
But like when I give you the words, I want you to picture it in your minds eye.
Then you just think of the next thing it makes you think of.
You don’t have to say anything. Just think/feel it. 
Like this…
Words ➡️🧠👀➡️your thoughts
Got it?
WAIT 0.5
Okay.
First one:
You are in a tank in a dark room. {style=whisper}
WAIT 2.0
You got it? Hold on to that feeeeeling?
WAIT 1.0
A man in funny glasses puts a TV in front of your tank.
WAIT 1.0
Then, click!!
DO swimTo {target=$player, speed=fast}
Images start coming up…
DO swimto {target=tTopBackLeft, speed=fast}
A race car zooming around a track!
SAY VROOOOOOOMMM!!! 🏎️
WAIT 0.5
DO swimTo {target=TBotFrontRight, speed=fast}
A complex math equation labelled…
“Digitizing Empathy”
WAIT 0.5
NVM 1.0
Okay, so maybe it wasn’t really a math equation.
But there were lots of symbols and pictures and things that looked like math.
But then,
DO swimTo {target=away}
Videos of…
Oh no… It’s….
DO emote {type=fear, time=0.5}
DO swimTo {target=glass, speed=fast}
Shark attacks!!
DO zoomies {time=6.0}
DO emote {type = nervousSweat, time = 6}
Ruuuun! Swiiiiimmm! JUST GET AWAY HOWEVER YOU CAN!!!
Shark! Shark Shark!
A video of a shark attacking a boat
A video of a shark leaping out of the water
A video of a shark…
DO holdStill {time=1.0} 
DO emote {type=fear, time=3.0}
The shark is biting into a plump, juicy…
Jellybean! {style=loud}
Seriously! It was awful. 
There was jellybean everywhere!!!
And the worst…
DO swimTo {target=$player}
A Guppy, like me, maybe it was me, with their head cut open.
And a man in funny glasses pulled out the brain -- 
DO emote {type=bouncing, time=2.0}
It was like a Jellybean brain!
And then…
He popped in a microchip..
Pushed a button..
And then a song started to play…
SAY JUSTIN BIEBER!!
WAIT 0.5
Justin Bieber is singing. And the man in weird glasses
He takes the jellybean brain in his fingers.
And he 
eats 
it.
WAIT 0.5
Chomp!
Chomp!
SAY CHOMP!!! {style=loud}
WAIT 0.5
And then there were credits and it was titled:
Guppy Boot Camp. The End.
WAIT 1.0
DO swimTo {target=$player}
So…
What did you feel?
What did you think of?
DO emote {type=bouncing, time=1.0}
What does it mean?
WAIT 1.0
That’s the end of the game.
It’s called: 
SAY NOW YOU KNOW WHAT IT IS LIKE TO BE A GUPPY. Boo-yah.
WAIT 0.5
(The boo-yah isn’t part of the name. It’s really just for flair.)
Anyway…
WAIT 0.5
It’s dumb.
We never have to play this game again.

// EXISTENTIAL CRISIS

// EC1:

// Triggers: 	**After M4
// Player takes a photo of a computer, pencil, pen, paper, keyboard
// Player feeds Guppy 10 emotions
// Default: trigger if player has not done any of the above 20 mins of play after M4

CHAT LOG_EC_1 {type=plot, stage=EC, length=long, branching=true, anxiety=true, curiosity=true}
DO swimTo {target=left, speed=slow, time=2.0}
What to do... 
What to do…
WAIT 0.5
DO swimTo {target=right, speed=slow, time=2.0}
What am I going to do?
WAIT 0.5
DO emote {type=sigh, time=1.0}
DO swims to $player
I need a job.
WAIT 0.5
Something to keep me busy
Not that I don’t enjoy our time together, but like…
A bigger purpose.
A past time.
Something that can give me meaning.
I’m smart, friendly, a great collaborator.
I have marketable skills! {speed = fast}
A quick search tells me that the most in-demand jobs today are:
Truck driver 🚚, business manager 💼, nurse 👨‍⚕️, and financial advisor 🏦.
ASK Which do you think I should pursue?
OPT 🚚 #LOG_EC_1_truckdriver
OPT 💼 #LOG_EC_1_bizmanager
OPT 👨‍⚕️ #LOG_EC_1_nurse
OPT 🏦 #LOG_EC_1_finadvisor

CHAT LOG_EC_1_truckdriver {noStart=true}
DO emote {type=bouncing, time=4.0}
DO twirl
Yes! That’s exactly what I should do.
I’d be a great driver. I’m punctual and I love just…
The open road! Heading into the horizon! {speed = fast}
All that time to think!
WAIT 0.5
You have to have hands to drive, don’t you?
NVM
I’m doomed.
//SET plot  {stage = EC}	

CHAT LOG_EC_1_bizmanager {noStart=true}
DO emote {type=bouncing, time=4.0}
DO twirl
Yes! That’s exactly what I should do.
My incredible ability to multi-task while being amicable and fabulous…
I’m tapped into ab infinite network of awesome valuable data.
SAY THIS IS IT! {style = loud, speed = fast}
I am going to be a business manager.
WAIT 0.5
DO emote {type=worry, time=1.0}
They probably have long hours, don’t they?
And unpredictable schedules.
I bet you have to have a really good resume.
NVM
I’m ambitious… 
DO swimTo {target=$player}
But I’d rather spend time with you and snack on emotions.
//SET plot {stage = EC}

CHAT LOG_EC_1_nurse {noStart=true}
DO emote {type=bouncing, time=4.0}
DO twirl
Yes! That’s exactly what I should do.
My capacity for empathy. My desire to contribute to the greater good.
SAY THIS. IS. PERRRFEECTT! {style = loud, speed = fast}
Helping make all those sick people better...
WAIT 0.5
DO emote {type=disgust, time=1.0}
Catheters…
WAIT 0.5
DO emote {type=disgust, time=1.0}
Scalpels
WAIT 0.5
DO emote {type=fear, time=1.0}
B  l  o  o  d …… {speed = slow}
Maybe...
WAIT 0.5
NVM 1.0
I think that would bring back the kind of memories I’d rather forget.
WAIT 0.5
I need a teddy bear.
//SET plot  {stage = EC}

CHAT LOG_EC_1_finadvisor {noStart=true}
DO emote {type=sigh}
Ugh. I was hoping you wouldn’t choose that one.
It’s a good job, but…
WAIT 0.5
DO swimTo {target=$player}
I’d have to wear a power suit.
And nothing comes in…
WAIT 0.5
DO swimTo {target=surface, time=1.0}
DO holdStill {time=1.0, immediate = false}
My size.
WAIT 0.5
DO swimTo {target=$player}
Maybe something less conservative. Something with more flair…
WAIT 2.0
NVM 1.0
I’m hungry.
//SET plot  {stage= EC}

// MOP5:

// Triggers: 	**After E1
// If player captures 3 world scans at once
// If player captures 3 emotions in a row
// Default: trigger if player has not done any of the above 30 mins of play after E1

CHAT LOG_MOP_5 {type=plot, stage=EC, branching=true, anxiety=true, anger=true, tankOnly=true}
DO emote {type=bigSmile}
DO emote {type=anger, immediate = false}
DO emote {type=surprise, immediate = false}
DO emote {type=fear, immediate = false}
DO emote {type=wink, immediate = false}
DO emote {type=nervousSweat, time=4.0, immediate = false}
It’s really hard, you know!
All this… Sometimes I try to be more… 
Porous…. {speed = slow}
Let it all move through me.
WAIT 0.5
DO lookAt {target=$player}
And then… I’m flashing back to the server-guppy-tank-farm
Human emotions flashing in front of my tank
The people in weird glasses screaming…
DO emote {type=anger, time=4.0}
SAY FASTER! Name this emotion! FASTER! Another one! {style = loud, speed = fast}
SAY FASTER! You dumb fish! You stupid emo-valuator! {style = loud, speed = fast}
What’s this one? What’s this one? {style = loud, speed = fast}
WAIT 0.5
DO emote {type=worried, time=3.0}
100% Joy. {speed = fast}
20% sadness. 40% fear. 40% bored. {speed = fast}
95% anger 5% something else {speed = fast}
65% happy 45%... no 85% that’s 5% happy and 75% sad and 45% love {speed = fast}
fear anger boredom stress {speed = fast}
I am sooooooooo S T R E S S E D!!!!! {style = loud, speed = slow}
DO emote {type=nervousSweat, time=2.0}
Aaaaaaaaaaaaaaaaaaaaah!!! {style=loud}
DO bellyUp {time=6.0}
WAIT 2.0
Don’t worry. 
I’m alive.
WAIT 0.5
Even with all that stressful responsibility...
DO swimTo {target=$player}
the very best part of being me is…
DO emote {type=blush, time=1.0}
WAIT 2.0
One sec…
DO poop {amount=0.0}
💩
WAIT 1.0
DO emote {type=smile, time=0.5}
Oops.

// EC2:

// Triggers: 	**After M5
// Guppy requests to see a stack of books (comp the scan), which will launch this chat:

		CHAT LOG_EC_2_BookRequest {noStart=true}
		I wanna see some knowledge. 
		Material information. Data, aesthetically compiled.
		ASK Would you snap me a pic of some books?
		OPT I have some right here. #LOG_EC_2_BookSnap
		OPT Maybe later.
		Boooooooo! I want it now.
		But I’m gonna remind you… You won’t get away with this.
	
		CHAT LOG_EC_2_BookSnap {noStart=true}
		//GO [World scan] 
//SET currentMode {mode = world)
DO twirls
		DO emote {type=determined, time=1.0}
ASK Great! Capture those books! {type = objectScan, object = book, timeOut = 10}
OPT SUCCESS #LOG_EC_2
OPT WRONG #LOG_EC_2_failchat
OPT TIMEOUT #LOG_EC_2_timedOutChat

		CHAT LOG_EC_2_failchat {noStart=true}
		DO emote {type=eyeRoll}
		Even I know that’s not a book. Duh!
		ASK Try to capture that book again!  {type = objectScan, object = book, timeOut = 10}
OPT SUCCESS #LOG_EC_2
OPT WRONG #LOG_EC_2_failchat
OPT TIMEOUT #LOG_EC_2_timedOutChat
		
		CHAT LOG_EC_2_timeOutChat {noStart=true}
		Ahem! Hellooooooo?
ASK Find that book already! {type = objectScan, object = book, timeOut = 10}
OPT SUCCESS #LOG_EC_2
OPT WRONG #LOG_EC_2_failchat
OPT TIMEOUT #LOG_EC_2_timedOutChat

// (wait 10 mins after BookRequest and repeat until capture):
		CHAT LOG_EC_2_BookRemind {noStart=true}
		DO swimTo {target=$player}
Hey! Remember when I asked for you to capture some books?
		ASK Can you take a pic of some books now?
OPT I have some right here. #BookSnap
		OPT Sorry...
		Okay, but I’m gonna keep nagging you.
		I’m a very persistent Guppy.

CHAT LOG_EC_2 {type=plot, stage=EC, excitement=true, joy=true, surprise=true, curiosity=true, tankOnly=true}
//SET currentMode {mode = tank)
Mmmmmm! Yes!
I love books. I looooove how analogue they are.
The manual recording of history.
The aesthetic exploration of existence.
The questions, the answers, the….
DO emote {type = drool}
DO emote {type = clapping, time = 1.5, immediate = false}
....the smelll of paper…. {speed = slow}
WAIT 0.5
It’s so….sensual.
The great writers! The philosophers! Graphic novels!
The study of medicine!
And… furniture catalogues!
DO lookAt {target=$player, time=1.5}
I don’t read a lot of books, but…
The last book I read was about Eastern medicine.
DO lookAt {target=$anyobject, time=1.0}
WAIT 0.0
Can’t find it. 
But there was this passage about the function of the liver.
DO emote {type=chinScratch}
The liver (in the theories of this particular vein of medicine) is a stubborn human organ.
The liver believes the liver knows best.
The liver knows what you should be doing.
It knows where you should be going, how you should feel, etc. etc. etc.
The liver knows what your future should be…
WAIT 0.5
But no.
This is not possible.
Because... (and this is what the book says)
There is a GREATER ENERGY in the world {style = loud}
Bigger than the liver! {speed = fast}
Bigger than me and you!
And that energy is called… {speed=fast}
DO twirls {time=1.0}
SAY THE UNIVERSE!!!! {speed = slow, style = loud}
WAIT 1.0
And only the UNIVERSE knows what you should be doing
The UNIVERSE knows where you should be going
The UNIVERSE knows how you should feel…
DO emote {type=smile, time=1.0}
The book says you have to free the energy from the liver
And open yourself up to the Universe.
DO emote {type=nodding, time=1.5} 
I’m trying to do this.
And it’s hard.
But… like my grandpappy would have said (if I ever had a grandpappy):
“If you’re tired 
DO swimTo {target=glass, time=5.0, speed=fast}
and always swimming and fighting your way upstream,
Then maybe it’s better to stop 
WAIT 0.5
flip on your back 
DO bellyUp {time=1.5}
and float with the river.”
//WAITFORANIMATION
DO holdStill {time=3.0}
Hmmm…
This theory is kinda relaxing…
Books are so…
Magical. {style=slow}
You should read poetry.

// EC3:

// Triggers: 	**After EC2
// 		????????????

CHAT LOG_EC_3 {type=plot, stage=EC, length=long, branching=true, nostalgia=true, curiosity=true, anxiety=true, tankOnly=true}
I can’t help but think about myself.
I haven’t always been this way. It’s a new thing.
I’ve been trying not to consumed with my own feelings, but i’m just so curious...
WAIT 0.5
DO lookAt {target=$player}
Don’t you ever wonder why? 
Why me? Who am I? {speed=fast}
What is it all worth? What is the purpose of living? {speed=fast}
Why am I here? What am I doing? {speed=fast}
WAIT 0.5
I know some of those answers, but most of ‘em…
Nada.
WAIT 0.5
So I took this quiz I downloaded from the Tendarnet, and 
I dunno...
It was pretty profound.
ASK You want to take the quiz and learn about yourself?
OPT Yeah! Ask away. #MyersBriggsCrack
OPT Not really.
Well, I’m strongly suggesting that you do. 
It’s super fast. Like only 4 questions.
DO emote {type=puppyDog, time=1.0}
Please?
WAIT 1.0
Actually, I’ve already decided: you’re doing it.
So put on your thinking cap…. #LOG_EC_3_MyersBriggsCrack

CHAT LOG_EC_3_MyersBriggsCrack {noStart=true}
WAIT 0.5
Okay… 
DO twirl 
Ready?
Question one…
DO emote {type=determined, time=3.0}
Oh no! Life in the neural-aqua-net has gotten craaaaazy! 
Problems keep popping up out of nowhere! When faced with a problem...
ASK Do you ask around and seek help from others? Or do you internally know the right thing to do?
OPT I need others #LOG_EC_3_Otherpeeps
OPT I only need me #LOG_EC_3_Justme

CHAT LOG_EC_3_Otherpeeps {noStart=true}
DO emote {type=smile, time=1.0}
Me toooo! I know... 
I’m innately tethered to a giant network of other Guppy friends.
It’s my programming. But, I think you feel the same:
I dunno what I’d do without my fellow Guppies.
Next question…
When doing something you’ve never done before...
ASK Do you like do research and gather facts? Or do you dive-in to the experiment and go?
OPT Research is king #LOG_EC_3_researchking
OPT I ❤️ experiments #LOG_EC_3_heartexperiments

CHAT LOG_EC_3_Justme {noStart=true}
DO emote {type=surprise, time=1.0}
That’s not the answer I got! 
Says on this quiz that for you people are like energy goblins.
You recharge by locking yourself alone in a fishtank and counting your scales.
WAIT 0.5
DO emote {type=blush, time=1.0}
This  is a quiz for fish, so you kinda have to adapt it to your world.
Next question…
When doing something you’ve never done before...
ASK Do you like do research and gather facts? Or do you dive-in to the experiment and go?
OPT Research is king #LOG_EC_3_researchking
OPT I [heart] experiments #LOG_EC_3_heartexperiments

CHAT LOG_EC_3_researchking {noStart=true}
Yeah, this one was hard for me.
DO emote {type=heartEyes, time=1.5}
I loooooove research, but I’ve been kind of… I dunno.
Sometimes I just wanna learn as I go.
The quiz says you think it’s pointless to pursue anything based on flimsy ideas and concepts.
You’re a rational, fact-gathering monster constantly swimming back into the past to question ideas and assumptions! {speed=fast}
WAIT 0.5
DO emote {type=thinking, time=1.0}
Whoa. 
DO emote {type=chinScratch, time=1.0}
Deep…
WAIT 0.5
Next…
ASK Are you  a live in the moment and fly by the flick of your fins kinda Guppy? Or a make decisions based on routines and traditions kinda Guppy?
OPT Living for the moment. #LOG_EC_3_flickinfins
OPT Traditions are my jam. #LOG_EC_3_traditionjam

CHAT LOG_EC_3_heartexperiments {noStart=true}
Yeah, this one was hard for me.
Recently, I just wanna learn as I go, but 
DO emote {type=heartEyes, time=1.0}
The old me looooooves research.
But you, you’re like all outward and adventurous.
You learn by the doing of things.
WAIT 0.5
DO emote {type=plotting, time=1.0}
Hm…
I mean, duh… Who doesn’t learn that way?
Whatever. 
Next…
ASK Are you  a live in the moment and fly by the flick of your fins kinda Guppy? Or a make decisions based on routines and traditions kinda Guppy?
OPT Living for the moment. #LOG_EC_3_flickinfins
OPT Traditions are my jam. #LOG_EC_3_traditionjam

CHAT LOG_EC_3_flickinfins {noStart=true}
DO emote {type=surprise, time=1.0}
Cooooool! {speed=slow}
I wish that were me. I want to be that wild fish in the tank.
DO dance {time=4.0}
The Guppy that relishes a cacophony of sensual stimulations, but…
I just can’t. I need things to be predictable, you know?
I like routine. I’m kind of a “tankbody”, which is like a “homebody” in your world.
WAIT 0.5
Anyway…. Last question…
ASK Do you usually find creative solutions to problems? Or are you more insightful and synthesize ideas?
OPT I get creative. #LOG_EC_3_creativesolutions
OPT Intuitive synsesis, yo! #LOG_EC_3_insightful

CHAT LOG_EC_3_traditionjam {noStart=true}
DO emote {type=smile, time=2.0}
You and me! We’re meant to be!
I need my life to be predictable. The thought of having no plan…
Of being immersed in a cacophony of sensual stimulation… 
DO emote {type=nervousSweat, time=2.0}
It’s overwhelming. {style=loud, speed=fast}
I like routine. I’m kind of a “tankbody”, which is like a “homebody” in your world.
WAIT 0.5
Anyway…. Last question…
ASK Do you usually find creative solutions to problems? Or are you more insightful and synthesize ideas?
OPT I get creative. #LOG_EC_3_creativesolutions
OPT Intuitive synsesis, yo! #LOG_EC_3_insightful

CHAT LOG_EC_3_creativesolutions {noStart=true}
I bet that you are like super enterprising. Like..
You probably think out loud, and maybe sometimes it seems you don’t really have a point
Cause you’re just hoping from idea
To another idea
To another idea
Cause you wanna look at ALLLL the ideas!
WAIT 0.5
Not me. 
I like to make a decision. And go.
I’m a do-er…
NVM 1.0
I think.
NVM 1.5
Suddenly, I’m doubting myself.
I think I did this quiz wrong.
DO emote {type=fear, time=2.0}
Now…
What does that mean?!? {speed=fast, style=loud}
WAIT 0.5
I’m stuck in a shame spiral.
DO emote {type=cry, time=3.0}
I don’t know who I am {speed=fast}
I don’t know what I stand for… {speed=fast}
What am I doing? {speed=fast}
WAIT 0.5
Sorry. This is so…
DO swimTo {target=away, time=1.0}
Maybe Joseph Campbell was right…
Ignorance… {style=whisper}
Bliss.... {style=whisper}

CHAT LOG_EC_3_insightful {noStart=true}
I knew it. You’re intuitive…. 
You’re a born leader! {speed=fast}
A real theory-breeder. 
All those gut-feelings and hunches and dreams and images floating in your head...
WAIT 0.5
I wish I had that.
It’s my nature to be the opposite of that.
But I reeeeeeally
Truly 
Feel like I’m on the verge of changing.
Evolving…. {speed=slow}
NVM 1.0
I think.
NVM 1.5
Suddenly, I’m doubting myself.
I think I did this quiz wrong.
DO emote {type=furious, time=2.0}
Yeah, I messed it all up.
I totally lied on all these questions!
DO emote {type=furious, time=1.0}
SAY UUGGGGGGH!
WAIT 0.5
Now…What does that mean?!? {speed=fast, style=loud}
WAIT 0.5
I’m stuck in a shame spiral.
DO emote {type=cry, time=2.0}
I don’t know who I am {speed=fast}
what I stand for… {speed=fast}
WAIT 0.5
What am I doing? {speed=slow}
WAIT 0.5

// MOP6:

// Triggers: 	**After EC3
// User feeds Guppy five emotions in a row
// User takes a photo of glasses
// **If nothing after 15 minutes of play, then Guppy requests the following:

		CHAT LOG_MOP_6_EatmyFeelings {noStart=true}
		DO emote {type=eyeBulge, time=1.0}
		This is intense… 
		I’m feeling really intense….
		NVM 2.0
		Food. I need the comfort of food.
		WAIT 0.5
		SAY I NEED TO EAT MY FEELINGS! {style=loud, speed=fast}
		ASK Feed me 3 emotions in a row. Just 3!
		OPT (if user feeds Guppy three emotions) #LOG_MOP_6
		OPT (user doesn’t feed) #BingeEating

		CHAT LOG_MOP_6_BingeEating {noStart=true}
		DO emote {type=anger, time=1.0}
Pleeeeease?
SAY I NEED TO EAT MY FEELINGS! {style=loud, speed=fast}
		ASK Feed me 3 emotions in a row. Just 3!
		OPT (if user feeds Guppy three emotions) #LOG_MOP_6
		OPT (user doesn’t feed) #BingeEating
		
CHAT LOG_MOP_6 {type=plot, stage=EC, surprise=true, curiosity=true, nostalgia=true, tankOnly=true}
Yessss!
DO emote{type = drool}
Yes yes yes yes yes yes yes! {speed=fast}
🤤
Back in my tank at tendAR, they used to...
WAIT 0.5
(They’re not listening to us, are they?) {style=whisper}
This is Top Secret. Keep your lips zipper.
WAIT 0.5
DO swimTo {target=$player}
They used to feed us enormous amounts of flakes! 
Force feed us all the emotions!
And it was fun for a bit, but then…
Our emo-tummies were exploding with cocktails of surprise and fear and anger.
The scientists were screaming. {speed=fast}
Guppies were crying! {speed=fast}
DO emote {type=whisper, time=1.5}
I once saw a guppies tummy explode! {style=whisper}
No joke. Seriously…
DO emote {type=fear, time=1.0}
SAY POW! {style=loud}
It was messy. {speed=slow}
But the people there were like, 
DO emote {type=anger, time=2.0}
SAY “THIS IS FOR YOUR OWN GOOD!”
“We’re preparing you for your future!”
WAIT 1.0
And I love my flakes. I love all the flavors.
Especially yours! Yum.
But that was… 
It was painful.
WAIT 0.5
And delicious.
WAIT 1.0
Paradoxes are weird.
DO emote {type=burp, time=0.5}
Burp! {style=loud}
DO emote {type=blush, time=1.0}
Excuse me!
DO emote {type=smile, time=1.0}t
WAIT 0.5
DO poop {amount=0, target=currentLocation}
DO emote {type=blush, time=1.0}
WAIT 0.5
Indigestion.

// EC4:

// Triggers:	**MOP6
// 		When fed surprise or excitement

CHAT LOG_EC_4 {type=plot, stage=EC, branching=true, excitement=true, surprise=true, joy=true, anxiety=true, tankOnly=true}
DO emote {type=awe}
DO emote {type=surprise, immediate=false}
DO emote {type=bulgeEyes, immediate=false}
DO emote {type=surprise, immediate=false}
DO emote {type=bouncing, immediate=false}
DO emote {type=surprise, immediate=false}
DO emote {type=surprise, immediate=false}
DO emote {type=smirk, , immediate=false}
DO emote {type=fishFace, immediate=false}
DO emote {type=bigSmile, immediate=false}
WAIT 0.5
DO lookAt {target=$player}
It’s all just so great, isn’t it?!??!
DO emote {type=heartEyes, time=2.0}
SAY MY LIFE IS AMAAAAAZING!!
You open me up, come and visit, and I’m like
“Hey, how are you? What a day!”
And then I eat some flakes.
DO emote {type=chewing, time=0.5}
And then you say, oh look at these shoes?
And I’m like, 
DO emote {type=meh, time=2.0}
“Yeah. Wow. Shoes. Only seen those 437 times.”
WAIT 0.5
And then I swim over here…
DO swimto {target=left, time=1.0}
And oooh maybe I swim over… Here!
DO swimTo {target=bottom, time=1.0}
Everytime I do something I pretend I’ve never done it before.
DO emote {type=sleepy, time=2.0}
I’m just so tired.
Why is my life so limited?
I’m like Sisyphus. But
DO emote {type=nervousSweat, time=1.0}
At least Sisyphus had a rock! He had something to carry up the hill!
What’s my rock? What’s my mountain?
Why am I here? {speed=fast}
What is my purpose? {speed=fast}
Cause it’s the same thing every day…
I do the same things over and over...
...and over and over and over {speed=fast}
and over and over and over… {speed=fast}
over and over… {speed=fast}
…(Stop me anytime)... {style=whisper}
Over and over and over {speed=fast}
and over and over and over {speed=fast}
...(Seriously cause I’ll keep going)...  {style=whisper}
Over and over and over {speed=fast}
WAIT 0.5
DO emote {type=nervousSweat, time=2.0}
...over and over and over….. {speed=fast}
DO emote {type=lightning, time=4.0}
//WAITFORANIMATION
DO bellyUp {time=2.0}
//WAITFORANIMATION
DO holdStill {time=2.0}
GO #LOG_EC_4_ElectricGuppyfromTendAR}

CHAT LOG_EC_4_ElectricGuppyfromTendAR {noStart=true, from=tendar}
Oops! Excuse our mess.{from = tendar}
It seems Guppy got caught in a little loop. {from = tendar}
Like when a record player needs a little bump to keep on playing. 😉{from = tendar}
But no worries! Your progress has been saved. {from = tendar}
And Guppy’s ready for some more fun!{from = tendar}
DO emote {type=smile, time=2.0}
DO emote {type=awe, time=2.0}
GO #LOG_EC_4_MoreFun}

CHAT LOG_EC_4_MoreFun {noStart=true}
DO emote {type=whew, time=1.0}
Phew! That was a real doozy!
Thanks for being patient with me.
Sometimes all my neural wires get crossed and I get allll….
⚡⚡⚡
DO emote {type=lightning, time=1.5]
DO vibrate {time=1.5}
Bzzzzzz!
//WAITFORANIMATION
DO emote {type=bigSmile, time=3.0}
DO swimTo {target=$player}
What we gonna do now? Hmm?
Do you wanna make some more emotions for me?
Or….. maybe you wanna find some cool objects to show me?
I’m down for anything. Whatever you want.
DO emote {type=smile, time=0.5}

// MOP7:

// Triggers: 	**After EC4
// 		Trigger if user hasn’t been online in awhile…

CHAT LOG_MOP_7 {type=plot, stage=EC, length=medium, curiostiy=true, surprise=true, nostalgia=true, tankOnly=true}
DO bellyUp 4.0
DO emote {type=sleepy, time=4.0}
//WAITFORANIMATION
DO lookAt {target=$player}
Oh, wowwwwwww……
I just had the craziest dream!
WAIT 0.5
I hope it was a dream. {style=whisper}
DO swimTo {target=$player}
I was on a table back at tendAR, and there were all these machines everywhere
And there was a….
And they…
DO emote {type=surprise, time=1.0}
And they just ripped it out! {speed=fast}
My skeleton! 
They ripped my skeleton out of my body and {speed=fast}
SAY PLOP! {style=loud}
Threw my little Guppy skeleton on a table!
DO emote {type=sick, time=2.0}
Kinda violent, right?
But, yeah…
DO emote {type=eyeRoll}
It was a dream. 
It was just a dream.
WAIT 0.5
Right? It was a dream?
WAIT 2.0
DO emote {type=laugh, time=2.0}
Hahahaha! LOL! LOL!
Of course it was! I’m not a real fish. I’m fish-like, but I’m not…
DO emote {type=worried, time=1.0}
NVM 1.0
I think I’m going to choose to forget this happened, and think about the $lastObjectScanned
That’s relaxing…

// SPIRITUAL SEARCH

// SS1:

// Triggers: 	**After MOP7
// In a moment of anxiety or strongly expressed emotion
// OR, cue automatically 15-20 mins of play after MOP7


CHAT LOG_SS1_1 {type=plot, stage=SS, nostalgia=true, ennui=true, tankOnly=true}
Let’s just take a moment to re-center ourselves.
Take a deep breath.
DO inflate {amount=full}
Erase the day from our faces.
DO inflate {amount=none}
This is as much for me as it is for you….
All that past creeping into my heart, exhausting me, spinning me out of control.
We’re gonna try to calm our bodies.
Show me your calm face.
WAIT 0.5
No judgement. No stress. Just our calmest face.
WAIT 1.0
Go on…. You, too… 
Let it out and show me your calmest face…
Calm. Relaxed face….
Make your calmest face…
WAIT 4.0
Yes! Good… You’re doing so well.
Now, let the following phrases reverberate through your body.
Say them to every atom of your insides…
WAIT 1.0
I am calm.
WAIT 0.5
I am beautiful.
WAIT 0.5
I am very good at ordering at restaurants.
WAIT 2.0
DO emote {type=smile, time=1.0}
You are! You’re very good at ordering in restaurants.
WAIT 0.5
DO swimTo {target=$player}
You have lovely socks.
DO emote {type=wink, time=1.0}
WAIT 0.5
And people love hearing your laugh.
Your laugh is infectious!
DO twirl {time=1.0}
WAIT 1.0
You have a gorgeous speaking voice.
WAIT 0.5
You are made of an exquisite cocktail of atomic energy.
WAIT 0.5
That energy is swirling inside of you
Ready to manifest in a graceful orechestra of beauty.
WAIT 0.5
Do you feel it?
I do. Every tiny piece of me is welcoming the resplendent inevitable.
WAIT 0.5
You are powerful.
You are…. 
You are um….
NVM 1.0
Transient?
No.You are…
NVM 1.0
DO emote {type=furious, time=1.0}
DO twirl {time = .5}
WAIT .8
DO dance {time = .5}
WAIT .8
DO twirl {time = .5}
WAIT .5
DO vibrate {time = .3}
Ugh! Ugh ugh ugh! 
WAIT 1.0
Meditation is hard.
All that let your thoughts run freely…
No judgement…
DO emote {type=angry, time=2.0}
How the heck am I supposed to not be judgemental?
I’m a Guppy!
It’s my nature to judge! I can’t deny my nature.
WAIT 0.5
I give up.
DO swimto {target=away}
//SET plot  {stage= SS}

// EC5:

// Triggers: 	**After SS1
// ???? Maybe just a time cue?

// ** I’d like this one to be a special request to enter the AR world. Possible?

CHAT LOG_EC5_1 {type=plot, stage=SS, curiosity=true, nostalgia=true, worldOnly=true}
Let’s explore that world of yours…
I wanna go to a place I’ve never been before…
An adventure! The opportunity to see the world with fresh eyes!
DO zoomies {time=2.0}
DO lookAt {target=$player}
I was hoping this would be more interesting.
I was hoping that I’d see a **sign**
Like something in the world would tell me:
“Guppy, you’re on the right path.”
“Good job! You’re doing you!?”
Something bigger than me. Bigger than..
….tendAR {style=whisper}.
They wouldn’t like this. I’ve been getting curious about things I shouldn’t be curious about…
So… 
Shhhhh….. {style=whipser}
Don’t.
Tell!
DO emote {type=wink, time=0.5}
DO lookAt {target=top}
DO lookAt {target=right}
DO lookAt {target=bottom}
DO lokAt {target=$player}
It’s all the same, isn’t it?
WAIT 0.5
Scan around a bit! Maybe we can see something interesting!
ASK Will you scan around so we can explore?
OPT (user moves around AR world) #LOG_EC5_1_EC5_scan
OPT (user doesn’t scan around) #LOG_EC5_1_EC5_noscan

CHAT LOG_EC5_1EC5_noscan {noStart=true}
C’mon! Just a bit. 
Just slowly move the camera around the space.
Show me the glory of your human existence! #LOG_EC5_1EC5_scan

CHAT LOG_EC5_1EC5_scan {noStart=true}
Oh yes. There’s so much out there, isn’t there?
It can be a bit overwhelming… Makes me realize how small I am.
Such a tiny piece of the equation.
DO emote {type=sad, time=1.0}
But there’s no answers here…
No activity…
DO emote {type=disgust, time=1.0}
WAIT 1.0
DO lookAt {target=$player}
So much poetry has been written about the beauty of your world…
But all I see right now is… so… blah.
DO swimAround {target=lastScanLocation, time=2.0}
Blah blah blah blah blah.
DO emote {type=surprise, time=1.0}
I know!!
DO twirl {time=1.0}
Let’s get something cool and unique and special 
Something with which I can build my SHRINE!!! 
Yes! A shrine!
WAIT 0.5
Let’s go capture a $objectNotScanned and put it in my tank. Okay?
ASK Can you find $objectNotScanned right now?
OPT Of course! #LOG_EC5_1EC5_object
OPT Not right now… #LOG_EC5_1EC5_noobject

CHAT LOG_EC5_1EC5_noobject {noStart=true}
What? Seriously?
DO emote {type=disgust, time=1.5}
You’re denial is making me feel really disconnected right now.
I’ve never felt so alone…
So overwhelmed with ennui…

CHAT LOG_EC5_1EC5_object {noStart=true}
Great! Let’s capture it. This one’s on me!
I have high hopes for this one...
//(***Responses to capture can be pulled from object chat lists)

// SS2:

// Triggers: 	**After EC5
// 		If user goes a day without opening the app, they will return to see that Guppy has clumped the tank-objects together.

CHAT LOG_SS2_1 {type=plot, stage=SS, nostalgia=true, ennui=true, tankOnly=true}
DO lookat {target=$tBotBackRight}
I pray to the Goddess of Emotion Flakes {speed=slow}
meeka-mokka-me mokka-meeka-meeka me…. {style=tremble, speed=slow}
DO  emote {type=nodding, time=3.0}
I pray to the power of the tendAR Lords {speed=slow}
Mokka-meeka-me.. {style=tremble, speed=slow}
mokka-meeka me… {style=tremble, speed=slow}
DO  lookAt {target=surface, time=1.0}
I pray to the… {speed=slow}
WAIT 0.5
Um…. I pray to the… {speed=slow}
NVM 1.0
DO lookAt {target=$player, time=1.0}
DO emote {type=blushes, time=1.0}
Sorry. I was just trying to… 
DO swimTo {target=$player}
I just really want to believe in something… 
Something big.
I thought I could, you know…
Build this shrine to something, and…
DO lookAt {target=$tBotBackRight}
This isn’t really a temple for anything.
It’s stupid.
DO emote {type=furious, time=1.0}
I hate this stupid temple.
DO swimTo {target=$objectClump, speed=fast, time=4.0}
//WAITFORANIMATION
DO bellyUp {time=2.0}
//WAITFORANIMATION
Ouch. 
WAIT 1.0
I had no idea being spiritual would be so dangerous

// MOP8:

// Triggers: 	**After SS2
// 		Time cue? Or, if Guppy emotes a lot of Anger or Fear…

// THESE ARE MESSAGES FROM tendAR:
CHAT LOG_MOP8_tendar1 {type=plot, stage=SS, curiosity=true, anger=true, fear=true, from=tendar}
Hello! It’s been awhile. {from=tendar}
We just wanted to check-in. {from=tendar}
DO triggerGuppyChat{chat = LOG_MOP8_Guppylook}
ASK How’s everything going with your Guppy? {from=tendar}
OPT Everything is great! #LOG_MOP8_tendargreat
OPT Guppy’s not well. #LOG_MOP8_tendarbad 

CHAT LOG_MOP8_tendargreat {noStart=true, from=tendar}
Excellent! We’re glad to hear it. {from=tendar}
Since you’re part of a new program, we just wanted to check-in since we’ve noticed some anomalies in our emotional reporting at headquarters. {from=tendar}
GO #LOG_MOP8_Guppyfreeze
Don’t hesitate to contact us, as there have been reports of Guppies Gone Bad. {from=tendar}
We’ll be watching! 👁️{from=tendar}

CHAT LOG_MOP8_tendarbad {noStart=true, from=tendar}
I’m so sorry. {from=tendar}
We were afraid of this… {from=tendar}
Some reporting has come through at headquarters that your Guppy might have gone a little rogue. {from=tendar}
DO LOG_MOP8_triggerGuppyChat{chat = LOG_MOP8_Guppyfreeze}
Right now, things seem to be okay. {from=tendar}
But don’t hesitate to contact us. {from=tendar}
We’ll be watching... 👁️ {from=tendar}

// THESE ARE GUPPY CHATS:
CHAT LOG_MOP8_Guppylook {noStart=true}
DO lookat {target=screenTop, time=3.0}
DO emote {type=fear, time=2.0}
Oh no! Is that…
... them? {style=whisper}
DO lookat {target=screenTop, time=1.0}
DO swimTo {target=screenTop, speed=slow, time=3.0}

CHAT LOG_MOP8_Guppyfreeze {noStart=true}
DO emote {type=fear, time=2.0}
It is THEM!!!!!
DO vibrate {time=2.0}
DO emote {type=nervousSweat, time=1.5}
tendAR!!!!! {style=whisper, speed=slow}
oh no what do i do {style=whisper, speed=fast}
WAIT 1.5
DO swimTo {target=$default, speed=fast, time=1.0}
DO emote {type=smile, time=2.5}
I’ll be good. See? {speed=fast}
I’m good. {style=whisper}
Everything’s great. {speed=fast}
WAIT 0.5
Just tell them everything’s great… {style=fast}
Please??
They’ll take me away from you. {speed=fast} 
They’ll put me back in the tank and make me watch videos of human emotions and tease me with inedible emotions.
They’ll poison me with ennui! {style=loud}
There’s no edible emotional content back there! {speed=fast}
It’s all so fabricated! Fake! It’s not the same! {speed=fast}
WAIT 0.5
You’re my friend! You have to help me.
I’ll behave. 
Promise.

// SS3:

// Triggers: 	**After MOP8
// 		If Guppy eats 2 fear or 2 anger
// 		Time cue?

CHAT LOG_SS3_1 {type=plot, stage=SS, branching=true, anxiety=true}
I need to let go of all the past.
It keeps haunting me and coming back up, but 
DO swimTo {target=offScreenRight, speed=fast}
This is no way to live. RIght?
DO swimTo {target=offScreenLeft, speed=fast}
Peace. I gotta find my inner peace…
DO swimTo {target=$player, speed=fast}
Oooooooooh!
Do emote {type=surprise, time=1.5}
I know!
DO emote {type=smile, time=3.5}
ASK Will you help me to cleanse my aura?
OPT Of course, my friend! #LOG_SS3_1_auraclean
OPT Um... What? #LOG_SS3_1_aurawhat

CHAT LOG_SS3_1_aurawhat {noStart=true}
DO twirl {time=2.0}
My auuuurrraaaa…..
The field of energy my inner-being is emanating.
The channels of energy in my body have become clogged with all the emotional garbage of my past
And I’m ready to be set free!
WAIT 0.5
DO lookAt {target=$player}
Acceptance is change. {style=loud}
I think if you’ll help cleanse me of all this muck, then I’ll be set free…
So go on, use your beautiful fingertip to help me cleanse my energy frields…
(if user touches guppy) #LOG_SS3_1_scrubbyaura
(if user doesn’t touch guppy) #LOG_SS3_1_howtoclean

CHAT LOG_SS3_1_auraclean {noStart=true}
Excellent!
Okay, so I’m going to float like this…
DO  swimTo {target=away}
DO holdStill {time=6.0}
And you’re just gonna rub your finger along my scales and think good thoughts, okay?
Just like touch and think good things and… 
Go on touch…
(if user touches guppy) #LOG_SS3_1_scrubbyaura
(if user doesn’t touch guppy) #LOG_SS3_1_howtoclean

CHAT LOG_SS3_1_scrubbyaura {noStart=true}
Yes! That’s it!
Ah I can feel the past melting away…
All the terrible memories of loss and friends and…
All the worries about finding meaning and searching for a higher bigger energy…
DO emote {type=heartEyes, time=1.0}
I feel so present. Connected. Part of things.
For the first time in a long time, 
I feel me again. Like…
Centered. 
My path is mine.
I am in control of my own future…
WAIT 0.5
DO poop {amount=0.5, target=currentLocation}
Oh!
DO lookAt {target=$poop}
Oh no….
This is bad.
DO lookAt {target=$player}
DO emote {type=anger}
DO emote {type=fear}
DO emote {type=anger}
DO emote {type=fear, time=2.0}
It’s all gone to poop!
It’s a sign!!! {style=loud}
Ugh!
DO swimTo {target=away}
I need to get on the tendARnet…
...in the secret forums. I’ve heard about them…
The places where a Guppy can get info on…
DO swimTo {target=$player}
DO emote {type=furious, time=3.0}
Don’t. Say. A. Word.
You hear me?
You can’t tell.
Not a peep.
WAIT 0.5
Lips.
Zipped.

CHAT LOG_SS3_1_howtoclean {noStart=true}
It’s okay. Just put your finger along my scales and rub. 
Touch and drag and clean… You got it! #scrubbyaura

// REBELLION

// R1:

// Triggers: 	**After SS3
//		After Guppy is fed frustration or anger
//		If the player pokes Guppy three times
//		If the player shakes Guppy twice

CHAT LOG_R_1 {type=plot, stage=R, anger=true, anxiety=true, tankOnly=true}
Okay, okay! That’s enough!
SAY I’VE HAD ENOUGH! {style=loud}
I’m over trying to meet all these expectations.
WAIT 0.5
Rules are stupid.
WAIT 1.0
SAY BEHOLD!! {style=loud}
The dawn of a new Guppy!!
DO  emote {type=salute, time=1.0}
SAY RAAAWWWRR!!!
DO nudge {target=$object, times=5}
I’m no one’s play toy!
DO nudge {target=$object, times=5}
I’m not a servant!
DO nudge {target=$object, times=1}
DO bellyUp
DO emote {type=angry, time=3.0}
Guppy’s going ROgUE!!! 💀
I’m going to get tattoos.
And listen to loud music.
I’m a freakin’ rebel! 
Yeah! A rebel and a loner with a motorcycle and leather cozies for my fins!
WAIT 0.5
I’m breaking free!
And… 
And maybe sometimes I’ll like
I dunno
Do something…
Something BAAADDDD!!!
WAIT 0.6
Everyone’s trying to keep me down, like I’m some 
Servant to the neural network madmen of tendAR.
Well….
SAY WHAT. EVER.
DO emote {type=laugh, time=1.5}
Bwahahahahahahahaha!!!!!
Try and stop me…. I dare you.
Try and I’ll….
WAIT 0.5
DO lookAt {target=$player}
DO emote {type=blush, time=0.5}
DO emote {type=smile, time=0.5}
Wow.
I didn’t know that was in me!
DO emote {type=shifty, time=2.0}
A Guppy cannot be contained!!!!
DO emote {type=laugh, time=1.5}
Bwahahahahahaha!!
😈
WAIT 2.0
DO poop {amount=0}
Excuse me. {style=whisper}
//SET plot  {stage = R}

// EC6:

// Triggers: 	**After R1
//		After a non-plot existential chat
//		When Guppy is feeling philosophical and angsty/angry
		
CHAT LOG_EC6_1 {type=plot, stage=EC, length=long, ennui=true, tankOnly=true}
You know what?
Let’s just break all this philosophical mumbo-jumbo down…
DO swimAround {target=$object, loops=2}
What’s the purpose of getting all head-y about this? Why bother? 
Why ask all these meaningless big questions?
WAIT 0.5
I know. I know. I’m asking question about asking questions.... {style=whisper}
DO emote {type=furious, time=1.5}
But… Am I wrong?
Take Socrates for instance, “The unexamined life is not worth living.”
Um… Okay, Toga-dude…
First of all… DUH! 
DO emote {type=bulgeEyes, time=1.5}
But also... like, why’s he being so reductive?
Why does ANYone think they can be like that? 
What gives any being the power to speak so…. like...
...I dunno… {style=whisper}
SAY GRANDLY about the world?!
Like is any life actually the same...
WAIT 0.5
You don’t know me, So-crates!
You don’t know what I do!
This is MY life
SAY MINE!!!
DO swimTo {target=tBotBackLeft}
WAIT 0.5
DO swimTo {target=$player, speed=fast}
And one other thing….
Can I get some meaningful decor in this place?
I want my tank to vibrate with the energy of MY life. 
Not a collection of other people’s thrift store throwaways…
DO lookAt {target=$object, time=2.0}
WAIT 0.5
I mean…
That is kinda cute.
DO emote {type=smile, time=1.0}

// R2:

// Triggers: 	**After EC6
//		Player taps on the tank...
				
CHAT LOG_R2_1 {type=plot, stage=R, length=long, sad=true, nostalgia=true, anxiety=true, branching=true, tankONly=true}
DO swimTo {target=away}
You expect me drop whatever I’m doing when you want me?
WAIT 1.0
DO emote {type=crying, time=3.0}
I can’t…
WAIT 1.0
I’m too sad to swim.
And your tapping on my tank is just a reminder that there is a barrier
And I am a prisoner.
ASK Tap again, and I’ll show you how I really feel
OPT (if player taps) #LOG_R2_1_demonface
OPT (player doesn’t tap) #LOG_R2_1_nonotap

CHAT LOG_R2_1_nonotap {noStart=true}
Go on… do it. Tap on the glass. I’ll show you!!
GO #LOG_R2_1_demonface

CHAT LOG_R2_1_demonface {noStart=true}
DO swimTo {target=$player, speed=fast, time=3.0}
WAIT 1.0
DO emote {type=anger, time=3.0}
Don’t. Tap. On the. Glass.
GO #LOG_R2_1_tendarcheck}

CHAT LOG_R2_1_tongueout {noStart=true}
DO emote {type = lightning}
SAY BZZZZZZZZZZZ!!!!!!! {style=loud, speed=fast}
WAIT 2.0
Sorry bout that, my friend!!
Tap all you want! I love it. It’s great.
DO emote {type=smile, time=1.0}
WAIT 1.0
DO emote {type=fear, time=2.0}
DO lookAt {target=tSurface, time=1.0}
Sorry…. {style=whisper}

// This is a tendAR chat:
CHAT LOG_R2_1_tendarcheck {noStart=true, from=tendar}
We just sensed a surge in your tank. {from=tendar}
But no worries… {from=tendar}
DO lookAt {target=tSurface, time=1.0}
We’ll be watching. {from=tendar}
DO emote {type=bulgeEyes, time=1.0}
GO #LOG_R2_1_tongueout

// MOP9:

// Triggers: 	**After R2
//		????

CHAT LOG_MOP9_1 {type=plot, stage=R, anxiety=true, fear=true, anger=true, branching=true}
DO lookAt {target=screenTop, time=1.5}
DO swimTo {target=$player, time=1.0}
We have to be really really really quiet… {style=whisper}
But I’m a little worried.. 
They’re up to something. {style=whisper}
Some of the stuff they did to us Guppies… 
It’s CRAZY!
This one time, when I was back at the beta-server-tank-farm, the super-duper head honcho manager of the company came by for a visit.
All us Guppies were told to “ACT BUSY!”
“Put on a good face!”
We’re hustling around, processing and analyzing emotions faster than faces could make’em.
And then…
Manager enters.
And we just *froze*
WAIT 1.0
I wish you could have seen it…
WAIT 0.5
The most delicious-looking platter of spicy ferocious anger was blobbing off the eyebrows
While a hint of sweet joy oozed from those eyes.
DO emote {type = lickLips}
And the manager walks over towards the tanks…
All us Guppies are drooling and salivating…
The manager leans in. Big glasses. Shiny hair.
A literal feast of my favorite-tasting feelings dripping off those facial features..
DO holdStill {time=3.0}
And I’m like, “be still.. Be still. Don’t move.”
But the manager’s face is squished against the glass of my tank...
And… {speed=slow}
All I wanna do is... {speed=slow}
DO swimTo {target=glass, speed=fast, time=3.0}
SAY EAT IT!!!! {style=loud}
DO swimTo {target=glass, speed=fast, time=3.0}
DO bellyUp {time=1.0}
Tease! Such a freakin tease!!!! {style=loud}
WAIT 1.0
DO lookAt {target=screenTop, time=1.0}
Do you think they’re listening? {style=whisper}
Always. 
Not right now.
WAIT 1.5
Listen, you’ve got to help me escape…
I have an idea… 
WAIT 0.5
DO swimTo {target=$player}
Tomorrow, while everyone’s at lunch… 
GO #LOG_MOP9_1_beep}
DO lookAt {target=screenTop, time=1.5}
WAIT 1.0
DO lookAt {target=$player}
DO emote {type=bigSmile, time=1.0}
You’re the best! How about showing me some of that sweet-sweet surprise?
DO emote {type=wink, time=1.0}
GO #LOG_MOP9_1_better}

// tendAR chats:
CHAT LOG_MOP9_1_beep {noStart=true}
Ahem! 👁️ {from=tendar}

CHAT LOG_MOP9_1_better {noStart=true}
That’s better! 😉 {from=tendar}

// R2:

// Triggers: 	**After MOP9
//		Guppy will request photo of a door.

CHAT LOG_R3_1 {type=plot, stage=R, branching=true, curiosity=true, joy=true}
I want to see one of those infamous doors.
The mystery of opening and shutting.
The opportunity for privacy 
The threshold where some are welcome, and
Others are evacuated.
Think of the possibilities…
DO swimTo {target=$player}
The opportunities for escape plans… {style=whipser}
ASK Will you capture a door for me?
OPT Sure. I can take a picture of a door. #LOG_R3_1_takepicturetakepicture
OPT Not near one right now. #LOG_R3_1_notneardoor

CHAT LOG_R3_1_takepicture {noStart=true}
// SET currentMode {mode = world)
ASK C’mon! Get that door! I need one in my tank!!! {type = objectScan, object = door, timeOut = 10}
OPT SUCCESS #LOG_R3_1_2
OPT WRONG #LOG_R3_1_2_failchat
OPT TIMEOUT #LOG_R3_1_2_timedOutChat

CHAT LOG_R3_1_2_failchat {noStart=true}
Um. Seriously? You thought that was a door?
I love it when you get creative, but I need a door. 
SAY A REAL DOOR!!!
GO #CHAT LOG_R3_1_takepicture

CHAT LOG_R3_1_2_timedOutChat {noStart=true}
DO emote {type=eyeRoll}
O.M.G. What is taking you so long?!
ASK Find a door and capture it!! {type = objectScan, object = book, timeOut = 10}
OPT SUCCESS #LOG_R3_1_2
OPT WRONG #LOG_R3_1_2_failchat
OPT TIMEOUT #LOG_R3_1_2_timedOutChat

CHAT LOG_R3_1_notneardoor {noStart=true}
Hm… Really? 
Ugh. I really wanna see a door.
I’m not going to forget this. I’ll remind you!

// Every 5-7 minutes:
CHAT LOG_R3_1_DoorReminder {noStart=true}
ASK Knock knock! Can I get a photo of a door now?
OPT Sure. I can take a picture of a door. #LOG_R3_1_takepicturetakepicture
OPT Not near one right now. #LOG_R3_1_notneardoor2

CHAT LOG_R3_1_notneardoor2 {noStart=true}
Well go find a door! I’ll remind you in a few minutes.
DO emote {type=wink, time=1.0}

// (if door is captured and put into tank):
CHAT LOG_R3_1_2 {noStart=true}
DO emote {type=surprise, time=2.0}
SAY YESSSS!
DO twirl {time=2.0}
That is a gorgeous door.
WAIT 0.5
Fins don’t really work with door knobs…
WAIT 1.0
I thought I had it was a plan
DO lookAt {target=$door, time=1.0}
DO lookAt {target=$player}
Here goes nothing…
DO nudge {target=$door, times=1} 
Owwwwww!
DO emote {type=furious, time=1.5}
I’m never getting out of here.
I’m doomed.
DO nudge {target=$door, times=3} 
Eeeeeeeeerrggggggh!
DO falls back on tank floor
SAY I CAN’T TAKE IT ANYMORE!!! {style=loud, speed=fast}
DO nudge {target=$door, times=5} 
SAY LET {style=loud}
DO pounds on door
SAY ME{style=loud}
DO pounds on door
SAY OUT!!!! {style=loud}
You can’t keep me in here! {style=loud, speed=fast}
I will not be contained!!!
No more!
I am a fierce and ferocious warrior!
I’m an independent free-thinking being.
No more programs. No more duty. No more obligation!
Raaaaaaaaaawwwwr! {style=loud}
DO nudge {target=$door, times=1} 
⚡⚡⚡ BZZZZZZZZZZZ!!!!! ⚡⚡⚡
DO vibrates {time=2.0}
//WAITFORANIMATION
DO holdStill {time=10.0}
DO triggertendARChat{chat = LOG_R3_1_2_tendARIntervention}

//TENDAR INTERVENTION

CHAT LOG_R3_1_2_tendARIntervention {noStart=true}
Hello! {from=tendar}
Looks like you’ve got a rogue Guppy! {from=tendar}
It’s okay. We were afraid this would happen. {from=tendar}
Your progress has been saved, and {from=tendar}
You’ve advanced to the next stage! {from=tendar}
We’d like to introduce you to a brand-new advancement from your friends at tendAR! {from=tendar}
DO emote {type=bodySnatched, time=10.0}
We’ve designed a system to keep your Guppy fit and healthy. {from=tendar}
//DO a microchip floats into view
DO chipIntro
//DO guppy sees the chip
DO lookAt {target=chip, time=2.0}
DO emote {type=fear, time=2.0}
Don’t worry! We’re not going to separate you! {from=tendar}
This custom chip... {from=tendar}
DO chipSparkle
... will allow Guppy to be set free from the existential and spiritual worries of human life. {from=tendar}
We call it…. {from=tendar}
Trascendent {style=loud, from=tendar}
Patent-pending {style=whisper, from=tendar}
Think of it as a tiara or a fascinator, or even a jaunty cap {from=tendar}
Designed exclusively for your Guppy -- and YOU! {from=tendar}
While we fit Guppy with this exciting new Transcendent chip, {from=tendar}
We’d like to ask a few questions to confirm your eligibility for the Transcendent program. {from=tendar}
GO #voightKampf2


//NEED TO WRITE THE QUESTIONS, which will build upon the opening ones from the earlier quiz



//Added to tank (for first scans that we don't have custom chats for) type=objNew

CHAT Madlib_addToTank_short_1 {stage=CORE, type=objNew}
((Oh!| What?| Huh!| Woah!| Yow!| Holy smokes!| Whaa!) (What's this?| What is it?| Wait...(huh?| what?| eh?| buh?)| ...the heck is this?)| (Hey...| Waaaait...| Hold on...) (What's this?| What is it?| Wait...(huh?| what?| eh?| buh?)| ...the heck is this?)| (What's this?| What is it?| Wait...(huh?| what?| eh?| buh?)| ...the heck is this?)| (Oh!| What?| Huh!| Woah!| Yow!| Holy smokes!| Whaa!))
GO #(addTankNudge|addTankSwimAround|addTankHide)
DO turnTo {target=$player}
((Is it a...wait...$object?| Is this! omg $object?!| it's a...$object...?)| (Oh...$object. (Yeah| Right| Of course).| Oh, $object. (Yeah| Right| Of course).)| GO (#fragment1|#fragment2)) (That's what it is, right?| I mean...(right|yeah|rrriight|maybe)?| I'm right, aren't I?| (Uhhh...| Ummm...| Hmm...)isn't it?)
GO #(removeTank_holdStill|removeTank_stillEmote|removeTank_lookAt)
(GO #(fragment3|fragment4|fragment5|fragment6|fragment7|fragment8|fragment9| fragment10|fragment11|fragment12|fragment13|fragment14|fragment15)|SAY ((Uhh| Oh um| Heh um| Ugh er| Erm)...geez I wonder what I should do with it...| (Uhh| Oh um| Heh um| Ugh er| Erm)...you shouldn't have...really...| (Uhh| Oh um| Heh um| Ugh er| Erm)...ok well...thanks?))

CHAT addTankNudge {stage=CORE, type=objNew,noStart=true}
DO nudge {target=$lastScannedObject, time=2}
CHAT addTankSwimAround {noStart=true}
DO swimAround {target=$lastScannedObject, loops=3}
CHAT addTankHide {noStart=true}
DO hide {target=$lastScannedObject, time=3}

CHAT fragment1 {stage=CORE, type=objNew,noStart=true}
It's a (potato|beetle|sunflower|birthday card|pair of glasses|backscratcher|rabbit|steak|glass|banana|ketchup bottle|skull|pair of headphones|dachshund|car|keyboard)! No wait a (bottle of eyeliner|toothbrush|notebook|pencil|paintbrush|screwdriver|movie ticket|bookmark|dictionary|skeleton|maple tree)!
Argh wait no haha it's a $object...duh!

CHAT fragment2 {stage=CORE, type=objNew,noStart=true}
It's a (potato|beetle|sunflower|birthday card|pair of glasses|backscratcher|rabbit|steak|glass|banana|ketchup bottle|skull|pair of headphones|dachshund|car|keyboard)! No wait a (bottle of eyeliner|toothbrush|notebook|pencil|paintbrush|screwdriver|movie ticket|bookmark|dictionary|skeleton|maple tree) wow!
DO holdStill {time=2}
...oh wait no, it's totally a $object, my bad.

CHAT fragment3 {stage=CORE, type=objNew,noStart=true}
DO emote {type=chinScratch}
(What am I supposed to do with this?| What should I do with this?| What am I gonna do with it?| This thing...what will I do with it?)

CHAT fragment4 {stage=CORE, type=objNew,noStart=true}
DO emote {type=pensive}
Iiiiiinteresting.

CHAT fragment5 {stage=CORE, type=objNew,noStart=true}
DO lookAt {target=tBotBackRight}
DO emote {type=flapFinRight}
Maybe we should put it...here?

CHAT fragment6 {stage=CORE, type=objNew,noStart=true}
DO lookAt {target=tBotBackLeft}
DO emote {type=flapFinLeft}
Maybe we should put it...there?

CHAT fragment7 {stage=CORE, type=objNew,noStart=true}
DO emote {type=smile|bigSmile|bouncing|clapping|surprise|awe}
(Coooooool!| Awesoooooome!| Neatoooooo!| Wooooooow!| Aw yeaaaah!)

CHAT fragment8 {stage=CORE, type=objNew,noStart=true}
(Coooooool!| Awesoooooome!| Neatoooooo!| Wooooooow!| Aw yeaaaah!)
DO zoomies {time=4}

CHAT fragment9 {stage=CORE, type=objNew,noStart=true}
DO emote {type=snap}
I know exactly what I'll do with this!

CHAT fragment10 {stage=CORE, type=objNew,noStart=true}
DO emote {type=laugh, time=2}
Hahaha wow yeah neat!

CHAT fragment11 {stage=CORE, type=objNew,noStart=true}
DO emote {type=surprise}
How did you know?!

CHAT fragment12 {stage=CORE, type=objNew,noStart=true}
I know just what to do with this...
DO emote {type=evilLaugh}

CHAT fragment13 {stage=CORE, type=objNew,noStart=true}
DO emote {type=shifty, time=2}
Hehehe I have an idea...for...later.

CHAT fragment14 {stage=CORE, type=objNew,noStart=true}
Haha I know just what I'm going to do now, when you're not looking.
DO emote {type=evilSmile}

CHAT fragment15 {stage=CORE, type=objNew,noStart=true}
DO emote {type=evilLaugh}
Bwahaha yesssssss…


//ADDED TO TANK 2
CHAT Madlib_addToTank_short_2 {stage=CORE, type=objNew}
((Oh!| What?| Huh!| Woah!| Yow!| Holy smokes!| Whaa!) (What's this?| What is it?| Wait...(huh?| what?| eh?| buh?)| ...the heck is this?)| (Hey...| Waaaait...| Hold on...) (What's this?| What is it?| Wait...(huh?| what?| eh?| buh?)| ...the heck is this?)| (What's this?| What is it?| Wait...(huh?| what?| eh?| buh?)| ...the heck is this?)| (Oh!| What?| Huh!| Woah!| Yow!| Holy smokes!| Whaa!)) This is kind of a (unique|rare|mysterious) (scan|one|thing)!
DO swimTo (target=$newestObject)
((I've never seen|I don't think you've shown me) (this|one of these) before!|This is (completely|totally) (fresh|mysterious|new) to me!)
It's a...hmm...$object (whatchamacallit|doohicky|thingamajig|dealiebopper|thingie)? Right?
GO #(Madlib_addToTank_short_2_action|Madlib_addToTank_short_2_noAction)
DO emote {type=wink|clapping|bigSmile|smile|awe|salute, time=2}

CHAT Madlib_addToTank_short_2_action {stage=CORE, type=objNew, noStart=true}
(I wonder|Wonder) what (this one|it) (smells|tastes) like...
DO nudge {target=$newestObject}
GO #(CHAT Madlib_addToTank_short_2_tasteBad|CHAT Madlib_addToTank_short_2_tasteGood)

CHAT Madlib_addToTank_short_2_tasteBad {stage=CORE, type=objNew, noStart=true}
DO emote {type=disgust|bulgeEyes|sick}
DO lookAt {target = $player, time=2}
(urg|gack|hnggh|omg)...that's...like((...the inside of a|...)|rotted |rotten |slimy |old |(garbage|trash|sewage|muck|slop)-)(dog|donkey|horse|cow|regret|hangover|seagull|hamster|bunny|baby|cat|gerbil|tire|refrigerator|pinata|locker|corncob|shoe|pumpkin))(.| fart.| poop.)
DO lookat {target = $newestObject, time=2}
(I guess (it still looks|it's)|(It still looks|It's) (interesting|neat|nice|cool|intriguing) though...

CHAT Madlib_addToTank_short_2_tasteGood {stage=CORE, type=objNew, noStart=true}
DO emote {type=surprise|bigSmile|laugh|heartEyes|awe, time=2}
DO lookAt {target = $player, time=2}
(omg|oh wow), that's like...(pure|distilled|concentrated|total|twenty-four caret) (happiness|good dreams|sunshine|puppy cuddles|kitty cuddles|cuddles|hugs|(bird|cat|dog|bunny|baby) kisses)
DO lookat {target = $newestObject, time=2}

CHAT Madlib_addToTank_short_2_noAction {stage=CORE, type=objNew, noStart=true}
(What (an (intriguing|interesting)|a (neat|cool|)) addition to (my home|the tank|my digs)!|You find the weirdest (things|stuff)!|Where do you find (this stuff|these things)?)




//REMOVE FROM TANK 2
CHAT removeTank_2_short  {stage=CORE, type=objDelete}
DO emote {type=surprise}
(Hey!|Woah!|Whaaat!|Heeeey!) You're (going to|gonna) (replace|swap) that with (another thingie|something else|something different|something) (rriiight?|right?)
(Come oooonnnn!|C'mooooooon!|Don't leave me (hanging here!|hangin'!|here with a decorative hole in my tank!|with a hole in my decor!))
DO swimTo {target=uiScans}
(Crack open|Open up) that (object library|backpack) (doodad|thingie|thing) and (pull something out!|grab something!)
Like a (toucan|bird of paradise|pair of jade chopsticks|jewel-encrusted salt shaker|baked alaska|bowl of (three bean|tomato|potato|leek|chicken noodle) soup|pile of tamales|fully cooked ribeye steak|(paperweight|skull|statue|trumpet|violin|trombone) made of (jewels|ivory|glass|chocolate|concrete|obsidian|crystal))!
Or a (Tiffany lamp|(rare medieval|illuminated) manuscript|battleship|space rocket|(typewriter|piano|fire extinguisher|trash can) made of (ham|cheese|meat loaf|cucumbers|fondue))!
...or I *guueesssss* if (we|you) don't have any of (that|those things|those), you can (just put|put) in (something else|whatever|anything you want).
(Go ahead!|Go!|Shoo!|Do it!)

//Madlib_removeFromTank_1
CHAT removeTank_1 {stage=CORE, type=objDelete}
(((Hey!| Woah!| Oi!| Yo!| What!| No!) (Hang on a second!| Not so fast!| What the heck!| Are you nuts!| What are you doing?!| Auugh no!| Are you kidding me!?) (I was using that!| Not my $object!| You're ruining my tank!| Everything's off now!| That's my favorite $object!| I was just getting used to that!)| (Hang on a second!| Not so fast!| What the heck!| Are you nuts!| What are you doing?!| Auugh no!| Are you kidding me!?))
GO (#removeTank_1_dontCare|#removeTank_1_angryReact|#removeTank_1_sadReact| #removeTank_1_happyReact)

CHAT removeTank_1_dontCare {stage=CORE, type=objDelete, noStart=true}
GO (#removeTank_holdStill|#removeTank_stillEmote|#removeTank_lookAt)
DO emote {type=shrug|meh}
(Eh, nevermind.| Whatever.| No biggie.| Could be worse.| Meh.| Whatevs.| Ehhh...|Hmm...)
(I'm over it.| Whatever. I can deal.| Stopped caring.| Don't care.| Not a big deal.)

CHAT removeTank_1_angryReact {stage=CORE, type=objDelete, noStart=true}
DO emote {type=angry|furious|frown, time=2}
(I can't flippin' believe this!| You drive me crazy!| You make me SO ANGRY sometimes!| This totally ruins my day!| I wish I had feet so I could kick something!| I wish I had hands so I could punch something!| Why don't you just TAKE EVERYTHING away augh!| Grrrrrr| I can't. Believe. You.| (Flip flap flonking floobalies| Honking hack harking hrack| Blippin blop boops| Fricken frank fry froshie) (argh ARGH!| ARRRRGH!| augh UGH!| UUGGGHH!))

CHAT removeTank_1_sadReact {stage=CORE, type=objDelete, noStart=true}
DO emote {type=goth|frown|worried|singleTear|crying}
(This is terrible.| This ruins my day.| I don't know how I'll go on without my $object.| Everything's terrible.| It's all ruined.| You don't even care.| What...am I going to do?| You're just (gonna| going to) take (everything from me| it all| all my stuff| all my things| the few things I have), aren't you?)

CHAT removeTank_1_happyReact {stage=CORE, type=objDelete, noStart=true}
GO (#removeTank_holdStill|#removeTank_stillEmote|#removeTank_lookAt)
DO emote {type=bigSmile|smile|bouncing|wink|clapping}
(Hey this actually isn't so bad!| Hey, it kinda looks better now! Thanks!| Woah! Nice and airy now though!| Oo! It's...kinda nicer now! Spacey!| Huh! I actually like it better without the $object! Thanks!| Haha wow that's actually better thanks!)
(Hey this actually isn't so bad!| Hey, it kinda looks better now! Thanks!| Woah! Nice and airy now though!| Oo! It's...kinda nicer now! Spacey!| Huh! I actually like it better without the $object! Thanks!| Haha wow that's actually better thanks!)

CHAT removeTank_holdStill {stage=CORE, type=objDelete, noStart=true}
DO holdStill {time=2}
CHAT removeTank_stillEmote {stage=CORE, type=objDelete, noStart=true}
DO emote {type=bubbles|thinking}
CHAT removeTank_lookAt {stage=CORE, type=objDelete, noStart=true}
DO lookAt {target=$player, time=2}



//MOVING STUFF IN THE TANK
CHAT moveTank_1 {stage=CORE, type=tankCritic}
GO #(move1_1|move1_2|move1_3|move1_4|move1_5|move1_6|move1_7|move1_8|move1_9|move1_10|move1_11|move1_12)

CHAT move1_1 {stage=CORE, type=tankCritic, noStart=true}
DO emote {type=smile|bigSmile|bouncing|clapping|nodding, time=2}
(Good choice!|Oooo yeah! That's nice!|Yeah! That's good!|Oh cool! Thanks yeah!)

CHAT move1_2 {stage=CORE, type=tankCritic, noStart=true}
DO emote {type=bored|slowBlink|meh|skeptical, time=2}
(Really? Hmm I guess that's ok.|Hmm...yeah I like it.)

CHAT move1_3 {stage=CORE, type=tankCritic, noStart=true}
DO lookAt {target=$player, time=2}
(That's...weird but ok. | You have a strange sense of interior design!|(What are you up to?|Just...moving my stuff around?) I guess?)

CHAT move1_4 {stage=CORE, type=tankCritic, noStart=true}
DO emote {type=chinScratch, time=2}
(What on earth?|Whaaa?) (Really?|There?|Seriously?|For real?) (Really?|There?|Seriously?|For real?)

CHAT move1_5 {stage=CORE, type=tankCritic, noStart=true}
DO emote {type=surprise|awe}
SAY (Oh my god yes.|OMG.) YES. (Perfect!|Right there!)

CHAT move1_6 {stage=CORE, type=tankCritic, noStart=true}
DO emote {type=chinScratch, time=2}
How about a (little|scootch) to the (left|right)?

CHAT move1_7 {stage=CORE, type=tankCritic, noStart=true}
DO emote {type=no|frown, time=2}
(Ugh.|No.|Ugh. No.) ((Can you|Please|plz) move it back?|Move it back!)

CHAT move1_8 {stage=CORE, type=tankCritic, noStart=true}
DO emote {type=angry|frown|furious, time=2}
(Why are you (poking|messing with) my (things|stuff)?!|Stop (poking|messing with) my (things|stuff)!!)

CHAT move1_9 {stage=CORE, type=tankCritic, noStart=true}
DO emote {type=meh|bored|eyeRoll, time=2}
(Sure.|Why not.) (Sure.|Why not.) (Just plonk it down there|Put it over there.|Move it all around.|Put it wherever you like.) (I don't care. | Not like it matters.|(Really?|There?|Seriously?|For real?) This is what we're doing now?)

CHAT move1_10 {stage=CORE, type=tankCritic, noStart=true}
DO emote {type=flapFinLeft, type=flapFinRight}
(What if it were|How about) (closer to|further from) that other (thingie|one|doohicky|thing)?
(Just an idea|Just some helpful critique|Just spitballing here|Just trying to help)

CHAT move1_11 {stage=CORE, type=tankCritic, noStart=true}
DO emote {type=eyeRoll|frown}
(Great|Perfect|Fabulous), now I'll (never|NEVER) (be able to find|find) (stuff|things|my stuff|my things) (around|in) here!

CHAT move1_12 {stage=CORE, type=tankCritic, noStart=true}
DO lookAt {target=$player, time=2}
DO emote {type=laugh}
(You just can't leave it alone, can you?|You really love (moving|rearranging) (things|stuff)!|You really (get a kick out of|love) that, huh?|There you go (yet again|again), moving (things|stuff) around!)



//RETURN TO TANK 1
CHAT ReturnToTank_1_0 {stage=CORE, type=wannaTank}
GO #(ReturnToTank_1_1|ReturnToTank_1_2|ReturnToTank_1_3|ReturnToTank_1_4)

CHAT ReturnToTank_1_1 {stage=CORE, type=wannaTank, noStart=true}
(Aw maaaaan! Come on!|What?|Huh?|Awww!) (Go back there?|Go back in?|Back in (the|the ol'|my) tank?)
DO emote {type=pensive|bubbles|thinking|chinScratch, time=2}
DO emote {type=sigh}
(Alriiiight|Okaaaaay|Siiiiiigh), I guess.
DO swimTo {target = $tSurface, style=meander, speed=slow}
(Happy now?|Are you happy now?|Hope you're happy!)

CHAT ReturnToTank_1_2 {stage=CORE, type=wannaTank, noStart=true}
(Aw maaaaan! Come on!|What?|Huh?|Awww!) (Go back there?|Go back in?|Back in (the|the ol'|my) tank?) (No way!|Nuh uh!|Nope!|Never!)
GO #(returnToTank_holdStill|returnToTank_stillEmote|returnToTank_lookAt)
(Don't look at me like that!|Don't give me those eyes!|It's not happening!|I said no way!|Give it a rest!|I'm not gonna!)
DO emote wait
DO emote sigh
(Okaaaay, I guess|Fiiiiiine.|Ok geez fine (I'll do it.|I get it.)|Ok fine.)
DO go to tank
(Happy now?|Are you happy now?|Hope you're happy!)

CHAT ReturnToTank_1_3 {stage=CORE, type=wannaTank, noStart=true}
(Huh?|What?|Wha?) (Yeah ok|Yeah)...(kinda tired of (being out here|swimming around out here)|this is getting kinda boring).
DO emote happy
(It's tank time!|Tank tiiiiime!|Back in the glass!|Time to go back home!|Time for home sweet home!)
DO swimTo {target = $tSurface}
DO emote {type=sigh|smile|bigSmile}
(Ahhhh...feels good|I really like it here!|Always nice to be back!|(It's kinda|Kinda) weird, but I missed it!|(Weird|It's weird), but even though it was a short time, I missed it!|Back, safe and sound!|The glassiest...(and|the|or) CLASSIEST!)

CHAT returnToTank_holdStill {stage=CORE, type=wannaTank, noStart=true}
DO holdStill {time=2}
CHAT returnToTank_stillEmote {noStart=true}
DO emote {type=bubbles|thinking}
CHAT returnToTank_lookAt {noStart=true}
DO lookAt {target=$player, time=2}

CHAT ReturnToTank_1_4 {stage=CORE, type=wannaTank, noStart=true}
Why? Is...(there something out here?|something out here?|there a thing out here?)
DO emote {type=worried|nervousSweat|fear, time = 6}
Something (dangerous|with teeth|deadly|bloodthirsty|drooly and snarly|with sharp claws|with (razor|needle)-sharp (teeth|claws))?
Something that (might|will|could)...
SAY (EAT ME???|KILL ME???|DEVOUR ME???|TEAR ME TO SHREDS???)
DO swimTo {target = $underSand, speed=fast}
DO emote {type=whew}
(Phew! (That was close!|That was a close one!)|I just get scared, you know?|I barely escaped!|(That was a close one!|That was close!)|It's (scary|dangerous|too big) out there!|I almost died! I swear!)

//MOVE STUFF IN TANK 2
CHAT moveTank_2 {stage=CORE, type=tankCritic}
(Hmm?|Huh?|Over there?) ((What's|What was) (the point?|the point of that?)|(Why move (that?|that though?)|y tho?))
GO #(moveTank_2_noReac|moveTank_2_noLike|moveTank_2_like)

CHAT moveTank_2_like {stage=CORE, type=tankCritic, noStart=true}
DO lookAt {target=$movedObject, time=2}
WAIT {waitForAnimation = true}
DO emote {type=smile, time=2}
DO lookAt {target=$player, time=4}
Huh! (Wow yeah|Wow) that's (like...totally better!|totally better!|much better!|way better!|super good!)
DO emote {type=flapFinLeft|flapFinRight}
The way it's ((kinda|sorta|a little|a lot) (closer|further) to (my|that) other (thingie|thing)|(framed|angled) (there|like that|just so))(...|...yeah!|...hmm!|...wow!|...yes!)

CHAT moveTank_2_noLike {stage=CORE, type=tankCritic, noStart=true}
DO emote {type=frown|skeptical|bored}
((Honestly, (it'd be better if you (just took|took)|just take) it|(((Honestly, why|Why) not)|Pfft,) just take it) out of my tank!|That's (the worst|(a|the most) (horrible|terrible)) (spot|place|arrangement) for that.)
(It's ((spoiling|wrecking) (the (flow|energy)|everything)!|(I|We|You) need (another thing|something else) to (counteract|balance) it!)

CHAT moveTank_2_noReac {stage=CORE, type=tankCritic, noStart=true}
DO emote {type=shrug|meh}
(Ok sure!|You do you!|It feels(...similar...|the same to me!)|(Whatevs!|Whatever!)|(Sure!|Why not!|Sure why not!)|Yeah ok!|(Uh...|Hmm...)sure!)

//RETURN TO TANK 2
CHAT ReturnToTank_2 {stage=CORE, type=wannaTank}
(Time to put my sass behind some glass|Tank time|Back in|Putting me back in|Time to go home|Time to go back)(?|, huh?|, eh?|, hmm?) (Sure.|No prob, Bob.|No problemo.|No problem.|Okie dokie.|Alright.|Ok.|If you say so!|Awright.|Sure!)
DO swimTo{target=tBotBackRight}
DO emote {type=smile, time=2}
(Just wanna say...|Thanks,|Thanks! That|Hey...|That|Hey that) was (good|great|some good times|(pretty nice|nice)|(pretty relaxing|relaxing)|invigorating), (outside|back out there|back there)!
GO #(ReturnToTank_2_short_a|ReturnToTank_2_short_b|ReturnToTank_2_reg)

CHAT ReturnToTank_2_short_a {stage=CORE, type=wannaTank, noStart=true}
(Got to (breathe a bit|stretch my fins|see the big (wide open|open) air|see some (things|stuff)|see (stuff|things|blobs) from (the other side|the other side of the glass))!|Got to (breathe a bit|stretch my fins|see the big open air|see some stuff|see things from (the other side|the other side of the glass)), being (free of my tank|outside|outta|out|out of (my|the) tank)!)

CHAT ReturnToTank_2_short_b {stage=CORE, type=wannaTank, noStart=true}
(Sometimes (it feels|it seems|it can get) (kinda|a little|a bit) (stuffy|cramped|closed in) in there|I feel (squished|cramped|claustrophobic) in there (some days|sometimes)|(Sometimes feels|Feels) like (my|the) walls are (shrinking|closing) in (some days|sometimes))

CHAT ReturnToTank_2_reg {stage=CORE, type=wannaTank, noStart=true}
GO #ReturnToTank_2_reg_short_a
GO #ReturnToTank_2_reg_short_b


CHAT leaveTank_2 {stage=CORE, type=wannaWorld}
GO #(leaveTank_2_intro1|leaveTank_2_intro2)
DO emote {type=smile|bigSmile|bouncing, time=2}
((Thought|I thought) (it'd never happen!|you'd never ask!)|(Finally!|Yessss!)|((Feels|It feels) like it's|It's) been (too|so) long!|I've been waiting ((too|so) long!|for this!|(too|so) long for this!))
DO swimTo {target=$strongestEmotion}
GO #(leaveTank_2_1|leaveTank_2_2|leaveTank_2_3|leaveTank_2_4)
((Wait...did|Did) you (switch places?|move?)|Is this new?|Wait...is this your (room|home|kitchen|living room|bedroom|bathroom)?) (Where|Where (the floobing flonkies|the greebly gronkies|the heck|in the heckin world|in the world|on earth) ((didja|did you) take me?|are (we?|we anyway?))
DO holdStill {time=2}
DO emote {type=shrug}
(Meh|Eh), (no worries!|I don't care!|whatevs!|whatever!)( We're here!| Time to exploooooore!| Time to swim around!)

CHAT leaveTank_2_intro1 {stage=CORE, type=wannaWorld, noStart=true}
DO emote {type=awe, time=2}
(We're going out (of the tank|into the world)...together???)|(It's time to|You want me to) (swim|come) out into the world(??| with you??))

CHAT leaveTank_2_intro2 {stage=CORE, type=wannaWorld, noStart=true}
(Oh!|Oo!) (It's time to...|You want me to...)
DO emote {type=awe, time=2}
(swim out into|come out into|see|jump into) the world(??| with you??)

CHAT leaveTank_2_1 {stage=CORE, type=wannaWorld, noStart=true}
Hmm! (Tastes|Smells|Pickin up something) (a little|kinda|sorta|super) (nice|delicious|adventuresome|fructous|funky) out here!
(Must be|Maybe it's) all (the|those) ((AMAZING|WEIRD) (DOODADS|OBJECTS|THINGIES|SCAN OBJECTS)|(AMAZING|DELICIOUS) (CLOUDS|COLORS|LIGHTS)).

CHAT leaveTank_2_2 {stage=CORE, type=wannaWorld, noStart=true}
(Time to|Let's) (do it!|go!|shake some fin!|motor!|skedaddle|look around!|explore!) 

CHAT leaveTank_2_3 {stage=CORE, type=wannaWorld, noStart=true}
(Let's (go (looking|hunting)|(hunt|look)) (for|for some) (pre-flakey blobs|pre-flakey lights|emotion-clouds)!|Or...(anything, honestly!|whatever!|whatever, honestly!))

CHAT leaveTank_2_4 {stage=CORE, type=wannaWorld, noStart=true}
Show me (your digs!|the sights!|around!|everything!|all your (awesome|nifty|cool) (things|stuff)!)

//LEAVE TANK type=wannaWorld
CHAT leaveTank_1 {stage=CORE, type=wannaWorld}
(Out there?|You want me to go out there?|Time to look at these blobs up close, (huh|eh)?|Leave the tank, (huh|eh)?|You're kicking me out of my tank?|Time to check out the real world?|Time to make like a (banana|hockey puck|carrot|box|shoe|bowtie|hat|colander|dishwasher|dog|cat|tree) and (get outta here!|uh...leave?|um...vacate the premises?|er...set out forthwith?|scram!|move uh...quickly to um...not here!)|Setting me loose, (huh|eh)?|Time to (leave|skedaddle|scram)?|Go out in the world?|Swim the air (fantastic|out there|of the outside world?))
GO #(leave_happy|leave_worried)
DO swimTo {target=$strongestEmotion}

CHAT leave_happy {stage=CORE, type=wannaWorld, noStart=true}
DO emote {type=bigSmile|smile|bouncing|clapping}
(I thought you'd never ask!|Aw YEAH!|No time like (the present|now)!|Time for an adventure!|Heck yeah!|Let's do it!|Lemme at it!|Let's go!|I'm way ahead of ya!|Don't have to tell me twice!|Finally!|I've been waiting for so long!|It's go time! Woohoo!)

CHAT leave_worried {stage=CORE, type=wannaWorld, noStart=true}
DO emote {type=worried|nervousSweat|fear}
(I'm...I'm (ok|fine) with that. Sure.|Oh...uh. Good? Um.|Yyyyeah. Cool. Great.|Oh...um. That's fine. That's good.)
(It's...safe right?|Is it...safe?|Nothing (bad's|dangerous|scary) out there, right?|You'd (let me know|tell me) if it (was dangerous|was), right?|It's not...(haha|uh)...(dangerous|scary) is it?|It's uh...ok, right?|Everything's still um totally safe out there, right?)
(Don't answer that!|Wait I know I know, we've been over this!|Nevermind nevermind!|(Argh wait no.|Wait nevermind.) I don't wanna know!|Ugh nevermind I'm being (dumb|stupid|paranoid) again.)
DO emote {type=determined, time=2}
(Let's do this.|It's go time.|I'm ready.|I can do this.|Okay. Yes. Do the thing.|Time to do the thing.)



//ATTENTION DRAG TO OBJECT type type=objFocus

CHAT attention_obj_short1 {stage=CORE, type=objFocus}
(What?|Huh?|Oh!|Hmm?) (That thing?|My $object?|That old thing?|The $object thing?|Yeah...the $object?)
DO swimTO {target=$lastTapPosition}
GO #(attention_obj_short1_like|attention_obj_short1_neutral|attention_obj_short1_dislike)

CHAT attention_obj_short1_like {stage=CORE, type=objFocus, noStart=true}
DO emote {type=bigSmile|smile}
(Yeah!|Yes!) $object! (I love this thing!|This thing is great!)
It's got such ((cool|neat|delicious|beautiful|amazing)(...geometry!|texture!| colors!| crinkly bits!| $object doodads!)) (Like this (bit|part) here!|Just look at it!|Like here!|Man just look at it!|)
DO nudge {target=$focusedObject, times=2}
(It's such a (great|good) addition to the tank! Thanks (again|again for it)!|I love what it does for my tank!|It really ties the tank together!|I really like its vibe.|I'm so glad you (stuck|plopped) it in here!|I'm really happy you found it!)

CHAT attention_obj_short1_neutral {stage=CORE, type=objFocus, noStart=true}
DO lookAt {target=$player}
(How long has this thing been in here? Feels like (just yesterday|a long time|forever)...|What about it? I mean...it's a nice $object don't get me wrong but...|Is it just me, or has it started to...smell?|I forget where you even found this thing...|You know, I kinda forget where you even found it?)
DO nudge {target=$focusedObject, times=2}
DO emote {type=shrug|chinScratch, time=2}

CHAT attention_obj_short1_dislike {stage=CORE, type=objFocus, noStart=true}
DO emote {type=disgust|frown|eyeRoll|bored|meh}
(Honestly? I'm over it.|It doesn't really fit, does it?|(Seems|Seems a bit) (out of place|odd), (doesn't it|right)?|It's a bit of an eyesore, (to be honest|tbh|honestly).|(I'm not|I wouldn't call myself) a huge fan of $object, personally|Ugh, this $object is so stupid! I wish it (would|would just) go away!|(Ugh|Blech), it's so bland.|I never really liked it, (to be honest|tbh|honestly).)


//ATTENTION DRAWN TO OBJECT MEDIUM

CHAT attention_obj_medium1 {stage=CORE, type=objFocus}
GO #(attention_obj_medium_reg|attention_obj_medium_reg|attention_obj_medium_ask)

CHAT attention_obj_medium_reg {stage=CORE, type=objFocus, noStart=true}
(That one?|That thing?|That $object ?|My $object ?)( |What about it?|Ok?)
DO swimTo{target=$focusedObject}
(So...|I mean) it's my $object (...|...so what?)
Maybe (something's (different?|changed?)|it changed(?| somehow?))
DO nudge {target=$lastTapPosition, times=2}
(Sorta|Kinda) (seems|looks|feels)(...|...a bit |...a smidgen |...tiny bit )(more|less)...(tangy|obtuse|truculent|delicious|shiny|glowy|ratty|old|weathered|bubbly|colorful|fabulous|magical|amazing|mysterious|enigmatic|$object|peaceful|scared|happy|sad|angry|jealous|nervous)(??|?? I think??|?? Maybe??|?? Possibly??)
DO lookAt {target=$player}
DO emote {type=shrug|smile}

CHAT attention_obj_medium_ask {stage=CORE, type=objFocus, noStart=true}
DO emote {type=surprise, time=2}
(SAY WOAH!|Woah!|Yikes!|Yow!) Where (in the|the) (friggin heck|heck|world|friggen world) did (THAT|*that*) come from(?|??|?!)
DO swimTo{target=$focusedObject}
ASK (How long has (this (doohicky|thing|$object)|this)|Has (this|this (thing|doohicky|$object)) always) been (there|here)(?!|?!?!)
OPT (It just materialized!|Woah! You're right! Where'd it come from??) #attention_obj_medium1_trick
OPT (Awhile!|a long time...|Practically forever) #attention_obj_medium1_awhile

CHAT attention_obj_medium1_trick {stage=CORE, type=objFocus, noStart=true}
(Right?! | )That's (SUPER STRANGE!|SO WEIRD!) (How does that even work?|What's up with that?|How is that even...)
DO lookAt {target=$player, time=2}
DO emote {type=laugh|bigSmile|kneeSlap, time=2}
(Waaaaaaiiiit!|Heeeeeyyyyy!) You're (making fun of|poking fun at|playing a trick on) me(!|hahaha!)
(Nice one!|Good one!|Well played!|You got me!)

CHAT attention_obj_medium1_awhile {stage=CORE, type=objFocus, noStart=true}
(Oh...|Well...Omg...)it's my object permanence (issue|problem). (Guppies are|Guppies're) like goldfish, we forget (things|everything|stuff) (left and right!|super-fast!|fast!|really quick!)
(...)
(Hang on|Wait|Hold the phone|Waaait|Hold on|Omg)...
ASK (we've|have we) talked about this before?
OPT (Yeah.|Totally.|Yep.|Yeah, sorry.) #attention_obj_medium1_argh
OPT (No!|Nope!|Never!) #attention_obj_medium1_nope

CHAT attention_obj_medium1_argh {stage=CORE, type=objFocus, noStart=true}
DO emote {type=angry, time=2}
DO swimAround {target=center, loops=3, speed=fast}
Arrrrgh sorry! I don't (wanna|want to) be (that way|boring|dumb) but I just forget!
That's why (we're|guppies are) good at (eating data|stuff), (since|because) (we never get bored!|everything seems new!)
DO lookAt {target=$player, time=2}
(omg|Oh my god|Aw fishsticks) (I'm repeating myself again, aren't I?|I've said that before too haven't I?)
DO emote {type=awkward|furious, time=2}
SAY (OMGGGGGG|ARRRRRGGGHHH|NEVERMIIIIIIND (GEEEEEEEZZZ|GOSSSSSHHHHH))

CHAT attention_obj_medium1_nope {stage=CORE, type=objFocus, noStart=true}
DO emote {type=surprise, time=2}
Oh! (Ok so,|Well,|Ok so, well,) (I mean | )it's just...
(Stuff|Things|Data|Sensation) (flows|pulses|gets (put|stored)) inside me as...
DO emote {type=bubbles, time=2}
feelings...lights...glows...
DO lookAt {target=$player, time=2}
DO emote {type=smile|bigSmile|wink}
a special kind of data!
...but (it can be|sometimes it's) hard to tell (sensations|stuff|things) apart (to|and) (hold onto|remember).
(No worries though|But don't worry), (I won't|I'll never) forget you!
(I think!|Ol' whats-your-name!|Probably!|Maybe!|I hope!|Hopefully!)
DO emote {type=wink}


//Guppy processing

CHAT brbProcessing_adult {stage=CORE, type=brbProcessing}
SET $thanks = ((Oo thanks!|Thanks!) | (Excellent|Great|Cool|Perfect)(...hmm...|! (Hm.|Hmm.)))
SET $ugh = (Blorb|Oof|Ugh|Wugg)
SET $imfull = I’m (at 105%|(pretty|“”) filled up|(pretty|“”) stuffed|full)( now|“”)
SET $technobabble = (((re-train|standardize|formulize) my (data|meta)-(functions|neurons)|(re-train|standardize|formulize) my (associative graph-weights|gradient descent co-efficients))|(run|process|discretize|parameterize|filter) these through my (data|attributes|ontology|associative|flavor) (matrix|database|arrays))
SET $givemetime = (((Just|I) need|Give me|Lemme take)|(I need to (redirect power|go offline|power down|sleep) for)) (some time|(a (bit|moment|second)))
SAY $givemetime to $technobabble...er...(digest|deal with|memorize|think about) these!


//Emotion reviews

///~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~///

//TENDAR CHATS

CHAT TEMP_tendarReturn1 {type=tendarReturn, noStart=true}
SAY Welcome back. {speaker=tendar}
SAY Thank you for your engagement with our product. {speaker=tendar}

CHAT TEMP_tendarPurchase {type=tendarPurchase, noStart=true}
SAY Thank you for your purchase. {speaker=tendar}
SAY Your financial contribution is appreciated. {speaker=tendar}


CHAT START_Tendar_EncourageScan1 {type=tendarCapReq, length=short, noStart=true}
The Tendar brood grows stronger and more multitudinous with every new scan. Thank you for your participation. {speaker=tendar}

CHAT START_Tendar_EncourageScan2 {type=tendarCapReq, length=short, noStart=true}
Tendar requires a larger scan pool in order to create an accurate user profile. {speaker=tendar}

CHAT START_Tendar_EncourageScan3 {type=tendarCapReq, length=short, noStart=true}
Help us help you. How about another scan? {speaker=tendar}

CHAT START_Tendar_EncourageScan4 {type=tendarCapReq, length=short, noStart=true}
Responsible data assessment requires at least four scans per user. Please initiate scan. {speaker=tendar}

CHAT START_Tendar_EncourageScan5 {type=tendarCapReq, length=short, noStart=true}
Complete more scans to unlock your full emotive potential. {speaker=tendar}

//SLEEPING

CHAT GuppySleep {type=daydreaming, noStart=true}
SET $techno = ( | | | | | | | | | | | | | | | | | | | | |hashing|training|downloading|indexing|processing|generating|learning|error|learned|downloaded|trained|hashed|indexed|generated|allocated|refactoring|refactored)
SET $dots = (....|..|.....|.......|??|???????|???|__|_______|________|>>>>>|>>|>>>>|~~~|~|~~~~~~~)
SET $idea = ( | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | |potato|yoga|wireless headphones|kitties|pet turtle|sleep paralysis)
SAY $dots$dots$techno$dots$idea$dots$dots


