//Gameplay Additions

//Default Emotion = none
//Document for all the things we realize in testing are needed for good feedback 

// for dialogic processing
CHAT BLANK {noStart = true}
WAIT 0.1 


// ++++++++NewTank++++++++

// GENERIC

CHAT CORE_NewTank_1 {type=newTank, stage=CORE, length=short}
DO emote {type=evilSmile} 
Yeah, let’s change it up!
DO swimTo {target=left, speed=fast, style=meander}
DO emote {type=heartEyes} 
Keepin it dynamic ⚡
DO swimTo {target=right, speed=fast, style=meander}
DO emote {type=smirk}
Keepin it fluid 🌊
DO emote {type=evilSmile} 
DO zoomies
Float like a butterflyfish sting like a jelly!

CHAT CORE_NewTank_2 {type=newTank, stage=CORE, length=short}
DO swimAround {target=center, loops=1}
Always appreciate a change in scenery!
DO emote {type=bubbles} 

CHAT CORE_NewTank_3 {type=newTank, stage=CORE, length=short}
DO emote {type=bouncing} 
New tank!
New tank!
DO twirl 
DO emote {type=bigSmile}

CHAT CORE_NewTank_4 {type=newTank, stage=CORE, length=short}
DO emote {type=surprise}
Oooo check out (these digs|the rad pad|the cool new crib)
DO swimTo {target=center}
DO emote {type=heartEyes}
Thank you!!!
…
DO emote {type=smirk}
or should i say
…
SAY TANK you!
DO emote {type=kneeSlap}
DO emote {type=laugh, immediate=false}

CHAT CORE_NewTank_5 {type=newTank, stage=CORE, length=short}
DO emote {type=smile}
Variety is the spice of life
DO emote {type=evilSmile}
DO twirl
and ooooo boy is this tank lookin spicy 
🌶️🔥🌶️🔥🌶️

CHAT CORE_NewTank_6 {type=newTank, stage=CORE, length=short}
DO emote {type=phone}
Someone call NASA
DO emote {type=whisper}
Because we just discovered a whole new ⭐space⭐
DO emote {type=kneeSlap, immediate=false}

// CUSTOM

CHAT CORE_NewTank_GuoHua {type=newTank, stage=CORE, length=short, tank=TE_GuoHua}
SET traditional = (Traditional Chinese painting uses a blend of inkwash painting techniques and highly detailed brushwork comparable to calligraphy.|Before the advent of paper, guohua paintings were done on silk.)
SET digs = (Love the new digs. Maybe I’ll try my fin at landscape painting.|I feel(… (at peace|zen).| the universe cradling me in its fins. God is a fish.)|Ah, the sweet smell of cherry blossom.)

SAY ($traditional|$digs)


CHAT CORE_NewTank_Desert {type=newTank, stage=CORE, length=short, tank=TE_DesertTank}
SET howdy = (Well howdy!|Yippy ki yay!|Guppy ki yay!|Hoo-ee!|Howdy partner!|Ah!)
SET bask = (The desert! Home to the highly endangered pupfish.|Reminds me of Scottsdale…|It is a scorcher today!|Feels nice to bask in the glow of the artificial desert sun.|The $warmth $verbs my $bodypart)
SET warmth = ((glow|prickle|kiss) of the artificial sun|(sizzle|kiss|simper) of the desert (sand|sun))
SET verbs = (tickles|toasts|thaws|warms)
SET bodypart = (fins|fishy noggin|tailfin|sun-touched gillies|gillies|tender gills|flippy fins)

SAY $howdy $bask


CHAT CORE_NewTank_Cheesy {type=newTank, stage=CORE, length=short, tank=TE_CheesyFishTank}
SET plastic = More plastic than $comparison(.|!)
SET comparison = (the Pacific garbage patch|a hotwheels landfill|the New York toy fair|a Polly Pocket graveyard)
SET volcano = My own volcano?! $wow(!|.) $followup
SET wow = (Wowee|Woah|Hot dog|Gee willikers|Dang|Radical)
SET followup = (Does it require human sacrifices?|I promise to feed it, and love it, and take it on walks!...|...Dormant, I hope.|...Dormant, right?...Right?)

SAY ($plastic|$volcano)


CHAT CORE_NewTank_Monitor {type=newTank, stage=CORE, length=short, tank=TE_MonitorTank}
SET album = I feel like I’m in a ($vaporwave (album cover|music video)|90’s (screensaver|music visualizer)).
SET vaporwave = (Ramona Xavier|Vektroid|2814|Skylar Spence|Saint Pepsi|James Ferraro)
SET tv = ((They say too much|All that) TV will rot your brain.|I don’t think Tendar (has|supplies) a cable package…|TEL-E-VISION 🤖 RULES THE NA-TION 🤖)

SAY ($album|$tv)

CHAT CORE_NewTank_Phone {type=newTank, stage=CORE, length=short, tank=TE_PhoneTank}
SET $phone = (Phone sweet phone 📱🏠|Not cellular as in organism, cellular as in phone!|Call me, beep me, if you wanna reach me~)

SAY $phone


CHAT CORE_NewTank_Pool {type=newTank, stage=CORE, length=short, tank=TE_PoolTank}
SET $safety = (There’s actually no scientific basis for waiting 30 minutes after eating before swimming.|NO RUNNING BY THE POOL! SURFACES ARE SLIPPERY WHEN WET(!|!💦)|No lifeguard on duty.|Oh, the sweet sting of chlorine(.|in my gills.))
SET $party = (SURE I’ve peed in the pool- who hasn’t(?|? Don’t you lie to me. I can tell if you’re lying.)|Did someone say POOL PARTY(?|?🎉🏊‍)|Last one in the water’s a rotten egg!)

SAY ($safety|$party)

CHAT CORE_NewTank_Server {type=newTank, stage=CORE, length=short, tank=TE_ServerTank}
SET memories = (Oh dear. This. Uh.|This sure…) Brings back memories...

SAY $memories {style=whisper}


CHAT CORE_NewTank_Modern {type=newTank, stage=CORE, length=short, tank=TE_ModernTank}
SET chic = (Oo-la-la! So $synonym~|Welcome to (my|the) $fish $palace! $please)
SET fish = (fishy|underwater|scaly|modern|flake)
SET palace = (palace|crib|chateau|bungalow)
SET please = (No shoes in the house, please.|No rough-housing.|If I see stains on my nice white walls I WILL throw a fit.)
SET backdrop = A minimalist backdrop… (for all my|for us to fill with) $beautiful $clutter!
SET beautiful = (beautiful|glittering|glorious|shiny|delightful|lovely)
SET clutter = (clutter|junk|baubles|trinkets|doodads|bagatelle|mess|chaos|self-expression)
SET synonym = (chic|modern|slick|sleek|shiny|contemporary)

SAY ($chic|$backdrop|$please)


// ++++++++PetByPlayer++++++++
//purple = stolen from MASTER_Core poke

CHAT CORE_Pet_1 {type=pet, stage=CORE, length=short}
DO emote {type=drool}
Mmmmm yes
That’s the ticket 🎟️
DO emote {type=smirk}
SAY (ADMIT ONE: pet central|ALL ABOARD the guppy pats train)

CHAT CORE_Pet_2 {type=pet, stage=CORE, length=short}
DO emote {type=laugh} 
Hehehe
DO twirl
That tickles!

CHAT CORE_Pet_3 {type=pet, stage=CORE, length=short}
DO emote {type=startled}
Easy there!
DO emote {type=nervousSweat}
Mind the gills!!

CHAT CORE_Pet_4 {stage=CORE, type=pet, length=short, joy=true, sadness=true, surprise=true, curiosity=true, ennui=true}
Gentle, gentle! 
DO emote {type=frown}
I really pride myself on the polish of my scales
DO emote {type=smile}
DO twirl

CHAT CORE_Pet_5 {stage=CORE, type=pet, length=short, surprise=true, joy=true, anger=true, sadness=true, curiosity=true, ennui=true}
DO emote {type=shifty}
Why, hello there.
👩‍🔬
DO emote {type=heartEyes}
I sense some great chemistry between us.

CHAT CORE_Pet_6 {type=pet, stage=CORE, length=medium}
DO emote {type=puppyDog}
Scritches for guppy!
DO emote {type=heartEyes}
Scritches for guppyyyyyyy 💕
DO emote {type=smirk}
DO vibrate
SAY AH YES! That’s the spot.
…
DO emote {type=chinScratch}
Yknow...
A part of me suspects that if I had more dignity
DO emote {type=sleepy}
I’d find this demeaning.
…
NVM 1.0
DO emote {type=heartEyes}
But it feels so gooooooooood
DO emote {type=laugh} 
DO twirl

// ++++++++tankTooEmpty++++++++
//critique++

CHAT generative_tankTooEmpty_1 {noStart=true, stage=CORE, type=tankTooEmpty}
SET chic = … So (chic|mod|sharp|stylish), but (at what cost|where’s the personality)?
SET floorplan = Such $stark.articlize(), minimalist (floorplan|layout|design scheme) 
SET tacky = Where are the $obnoxious, (plastic|fake) $cliche.pluralize()? The $kitschy, (dollar-store|bargain-bin|happy meal) $cliche.pluralize()? 
SET simile = This place is $blanker(.|!)
SET basic = (So|I’ts just so|Too) $empty(.|...|. $clutter)
SET dunk = Minimalism is for $squares(.|. $clutter)
SET stark = (stark|aggressive|crushing|blinding|icy-cold|spartan|austere) 
SET obnoxious = (obnoxious|tacky|gaudy|garish|ostentatious)
SET kitschy = (kitschy|quirky|jazzy|wacky|questionable) 
SET cliche = (palm tree|tiki|pineapple house|mermaid|treasure chest|human skull|toilet|flamingo) 
SET blanker = (barer than a (mole rat|sphinx cat) at a nude beach|nakeder than a (hippo in a bikini|waxed racoon)|chillier than (a fresh-shorn sheep|a fish-cicle|an eel in an ice bath|a bangus in a blizzard))
SET squares = (squares|(tasteless|lazy) (millionaires|billionaires|zillionaires|property tycoons)|(instagram foodies|vapor-grammers|ascetic monks|germaphobes) and Kanye West) 
SET clutter = (I crave clutter|Let’s make a beautiful mess together)(!|!!!)
SET empty = (empty|bare|barren|sparse|stark|minimal|ascetic|austere)

SAY ($chic $tacky|$floorplan $chic|$simile|$dunk)

// ++++++++tankTooFull++++++++
//critique++

CHAT generative_tankTooFull_1 {noStart=true, stage=CORE, type=tankTooFull}
SET simple = (Feelin a little $cramped tbh|(I’m|Not to (poo on|toss a wet blanket on|stomp all over) your creativity, but I’m) a bit $cramped in here|Tank’s getting pretty $cramped).
SET feel = Starting to feel like (a sardine in a tin|a whale in a breadbox|a (clam|pufferfish) in a pressure cooker).
SET space = I (hardly|scarcely) have enough (room|space) to (stretch my fins proper|dance my signature Guppy Jig|twirl|waggle my butt|do my mid-morning laps around the tank|stretch).
SET overboard = (I think you went a little (overboard|buckwild|hogwild) with your decorating.|I like what you’re doing with the tank, but uh… maybe just, less of it please?|Might I suggest a spontaneous spring cleaning(?|?... Does Goodwill (take|accept) virtual 3D object mockups?))
SET cramped = (cramped|squished|overcrowded)

SAY ($simple|$feel|$space|$feel $space|$overboard)

// ++++++++TooManyX++++++++
//critique++

CHAT generative_tooManyX_1 {noStart=true, stage=CORE, type=tooManyX}
//SET  $object to the whatever object there are too many of, over a set threshold
SET wisdom = (You ever heard that saying about too much of a good thing(?|? No? Of course you haven’t.)|(If |In a world where) (Heinz can manage 57 varieties of dipping sauce|Baskin Robbins can manage 31 flavors of ice cream|Jimmy Buffett can manage a menu of 20 unique margaritas|Kit Kat can churn out 200+ unique flavors of Kit Kat bar|there are over 200 flavors of Oreo filling), I feel like I can expect a little more variety in tank decoration, yeah?)
SET choking = ((I’m drowning in|I’m being suffocated by|I’m gonna die buried in) $object.pluralize().|There’s more $object.pluralize() in here than there is WATER.|I’ll be happy if I never see another $object again.|I’ve seen enough $object.pluralize() to last me a lifetime.|I swear if I see another $object (I might barf|I’m gonna blow chunks).)
SET simple = (That’s too many $object.pluralize().|You ruined it. Feng shui decimated in a single clumsy blow.|Nope. Too many $object.pluralize().|I generally try to trust your design sense, but (that’s|(any oaf off the street|anyone with half a brain|even a primitive model intelligence) could tell you that’s) too many $object.pluralize().)

SAY ($wisdom|$choking|$simple) 

// ++++++++LongTimeSinceMove++++++++
//critique++

CHAT generative_longTimeSinceMove_1 {noStart=true, stage=CORE, type=longTimeSinceMove}
SET treasures = (treasures|baubles|knick knacks|decorations|trinkets|belongings) 
SET beloved = (beloved|cherished|splendid|lovely) 
SET dust = (It’s been so long since we (rearranged|redecorated), my|When’s the last time you rearranged the tank? My) (($treasures|$beloved $treasures) are|(stuff|tank decor) is) starting to gather dust(.|… that’s right! Underwater dust!!!)
SET arrangement = (arrangement|tank design|tank layout) 
SET stale = ((Like a half-eaten bag of chips|Like a Christmas fruitcake in February|Like a rock-hard loaf of sourdough)… this $arrangement has gone stale.|(Like a tub of unrefrigerated cottage cheese|Like a moldy peach)... this $arrangement has gone sour.|(Like bathtub Doritos|Like dumpster nachos)... this $arrangement has gone soggy.) 
SET fashion = (Ugh, darling|Oh dear... sweetie)~ this $arrangement is soooooo $aged.
SET aged = (GUPPY CLOCK cycle 0518692|five minutes ago|iteration 4.5|beta|derivative) 
SET shake = (Let’s ((shake|mix) (it|things) up a little|redecorate|rearrange)(!|, yea?)|(Play|Let’s play) with the composition(!|,eh?)|EXTREME TANK MAKEOVER: Guppy Edition!!!|This tank needs a (shake-up|makeover).)

SAY ($dust|$stale $shake|$fashion $shake|$shake) 


// ++++++++MoveObjReq++++++++
//critique++

CHAT generative_moveObjReq_1 {noStart=true, stage=CORE, type=moveObjReq}
//SET $object to requested move object 
SET $happy = ((Y’know… I|I) think this $object would be happier $spot.|This would look better $spot.|Can you help me move this? Can’t grab too good with these (lil|nubby|slippy) flippers.)
SET $space = ((Uhhhh, t|T)his is in my way.|Can you move this (a bit|a tad|someplace else)?)
SET $spot = (in the other corner|on the opposite side of the tank|in a different position|if you nudged it a bit to the (right|left))

DO swimTo {target=$object}
DO lookAt {target=$object}
DO emote {type=chinScratch, immediate=false}
SAY ($happy|$space)


// ++++++++FengShuiLesson++++++++ //now just a special Critic that can trigger during SS

CHAT generative_fengShui_1 {noStart=true, stage= SS, type=critic}
SET animal = (the azure dragon|the vermillion bird|the white tiger|the black tortoise)
SET direction = (southern|northern|western|eastern|southeast|southwest|northeast|northwest)
SET wisdom = (The support element for water is metal(.|🏆|... seems appropriate, considering I’m a virtual fish living in your phone.)|A happy home balances the five elements of fire🔥 earth🌏 metal⚓ water🌊 and wood🌳|A harmonious balance of the five elements helps encourage wealth, health, and happiness.)
SET support = The support element for (water is metal|metal is earth|earth is fire|fire is wood|wood is water).
SET imbalance = I sense an elemental imbalance in this tank(.|... probably has something to do with the abundance of Water?|... (For optimal chi flow, add (more ~$elements~ to your composition|a ($elements aligned|red|green|yellow|warm-color) centerpiece)|A (deficit|notable deficit) of $elements).)
SET elements = (fire|earth|metal|water|wood)
SET regional = $animal.capitalize() has ((found a happy home|settled) in|blessed) the $direction quadrant of the tank.

DO swimTo {target=center}
DO lookAt {target=$player}
WAIT {waitForAnimation = true}
DO emote {type=meditate}
SAY ($regional|$wisdom|$imbalance|$support)

// ++++++++TooHungry++++++++
// “Can’t process, too hungry” //make more specific?

CHAT generative_tooHungry_1 {noStart=true, stage=CORE, type=cantProcess}
SET howabout = How about $snack(?|, huh?)
SET cant = (Can’t sleep with $empty!|No point in processing with $empty(~|.))
SET prefer = (Yeah, uh, I|I) prefer to process (on|with) a full $tummy. 
SET simple = (Too hungry|(Not now|Nah)(, I’m $hungry|. I’m so hungry I could eat a seahorse|, need flakes|, need food))(!|.)
SET snack = (a pre-nap (snack|nibble)|some pre-nap flakes|(a quick (nibble|snack)|some flakes) before I power down)
SET empty = (a (rumbly|grumbly) $tummy|an empty $tummy)
SET tummy = (tummy|tumbly|stomach|tum|belly)
SET hungry = ((too|way too) hungry|famished|ravenous|starving)
SET moji = (🍽️|(🍴|🍫|🍟|🥪)🐟|🍴🍳🍴)

SAY ($howabout|$cant|$prefer|$prefer $moji|$simple)


// ++++++++tooFull++++++++
// regrets eating too much

CHAT generative_tooFull_1 {noStart=true, stage=CORE, type=tooFull, length=short}
SET $oof = (Oof|Yikes|Yeesh|Whelp)(.|...|!|... *hic*|... *burp*| 🤢)
SET $think = (I think I *burp* overdid it a bit.|This is what I get for thinking with my stomach.|It really IS possible to have too much of a good thing...|Did I eat too fast? Too much? Probably both tbh.|I feel (like I’m gonna be sick|sick).|If I eat even ONE MORE FLAKE i WILL explode(.|... It will be MESSY.)) 
SET $muse = (Ah, HUBRIS! All I ever wanted was to (eat|taste) the whole world(!|! Is that too much to ask?)|Like the foolish Icarus, I too, have flown too close to the sun.|Like the tragic Sisyphus, I too, am doomed to repeat the same mistakes again and again.|I don’t understand. I love food! Why would it hurt me like this(?|? You’ve done me dirty, flakes!!!)|A such thing as too much flakes? I feel betrayed.|Aren’t you supposed to wait 30 minutes after eating before swimming???)
SET $hurts = (My stomach (hurts|kills).|Oweeeeee, my tummy(!|! 😢)|I feel like I’m gonna EXPLODE.|Aaaaa (I’ve|Guppy’s) got a (tummy|belly) ache.)

DO emote {type=sick}
SAY $oof
SAY ($think|$muse|$hurts)


// ++++++++NoMoodForFood++++++++
// “Can’t eat, too happy/angry/sad”

CHAT generative_noMoodForFood_1 {noStart=true, stage=CORE, type=noMoodForFood, length=short}
//SET $emotion to Guppy’s current emotional state
SET $vague = (I’m not in the mood…|I can’t eat now… I’m too overcome by emotion.|Lemme (feel|finish feeling) my own feelings before I eat up yours, yea?)
SET $too = (Now’s not the (time|time for $snacks)(!|...)|Not now.|($snacks.capitalize())? At a time like this(? You’re kidding me.|?)) I’m (too|far too|way too|too impossibly) $emotion(!|.) 
SET $snacks = (snacking|snacks|flakes|nibbles|food|munchies)

SAY ($vague|$too)


// ++++++++BoredGuppy++++++++

CHAT generative_boredGuppy_1 {noStart=true, stage=CORE, type=boredGuppy, length=short}
SET $basic = I’m (bored.|boreddddd.|bored. Captial B, Bored.|bored. Bored bored bored bored BORED.|B. O. R. E. D. BORED.)
SET $guess = Guess what I have in common with the $tunnel… We’re both (BORED!!!!|BORED! 😂)
SET $tunnel = (famous Channel Tunnel|Yerba Buena tunnel|Gotthard Base tunnel)
SET $something = (Come on, let’s do something! (ANYTHING!|Poke me, pet me, feed me!|Anything! I ain’t (particular|picky)~)|Hey! Let’s play!|Play with meeeeeeeeeee!|C’mon(, p|, you landlubber! P|,leggy! P|, you featherless biped! P)lay with me!|You’re normally so (tap-happy|quicky-clicky)… what gives?)

SAY ($basic|$guess|$something)


// ++++++++PartialScan++++++++

CHAT generative_partialScan_1_long {noStart=true, stage=CORE, type=partialScan, length=medium}
SET simplexclam = (Fascinating|Intriguing|How peculiar|Strange|The shapes! (The textures|The textures! The… odors?)|I’ve never seen anything like it)(!|...|.)
SET bite = (Oh, (c’mon|irresistable|what a strange new flavor of (categorical|datametric|AR) goodness))!|That was just an $bitty (nibble|bite|scrumple|morsel|sample)! $aggrobite
SET aggrobite = (I wanna really sink my (teeth|chompers) (into it!|into… uh. WHATEVER that is!))|I really wanna $devour this (sucker|whatsit|tasty thingaroo|delicious doohicky|somethin somethin|whatever-it-is).)
SET devour = (nosh|devour|munch|crunch|chomp|nip|scarf) 
SET bitty = (bitty|itty-bitty|teensy-weensy|tiny little)
SET hemhaw = (Hmm|Hrmm|Uhhhhh|Well|Wowzers|Fry my fins and call me a fishstick|A puzzler!|(I’m|Well I’m) stumped)
SET verbing = (forming|glomming|configuring|trans(mogrifying|morphing)) 
SET incomplete = (incomplete|sketchy|only half-formed|still gooey|raw... uncooked|only a skeleton|still sticky-concrete fresh|unhatched|all discombobulated)
SET profile = $hemhaw... I’m $verbing a (data|comprehensive|conceptual|primitive) (pre-vis|profile|visualization) in my lil (guppy|fishy|spitfire) (brain|noggin|processor|thinkadink|noodle) but (…it’s $incomplete|I need more info).
SET sparkjective = (Sparkling|Shimmering|Conceptual|Iridescent|Ephemeral) 
SET pieces = (pieces|fragments|shards|scraps) 
SET bits = ($sparkjective $pieces...|The $pieces are) starting to come together(!|~)  
SET try = ((Quick! Try|Let’s try) another angle(.|!)|Try another angle?|Get in there( and try another (view|angle)|, try shifting your perspective).|Can I see it from the other side?|(Try scanning again|Try again)(, but| but…) (more different|different.))
SET keepitup = Don’t give up now! (Keep the (scans coming|data flowin)!|Scan it again!)
SET again = (...Yeah|Mmm yea), I’m gonna need to (see that one again|have another look at this|take a closer look).
SET movies = (It’s like… you know (in the (movies|talkies)|on TV) when the $genius (whiz-kid is solving the Big Equation|detective is about to (Solve|Crack) the (Big Case|Case)) and numbers and (symbols|ciphers|figures) are whizzing through the air(?|? That’s me right now.)|Oh!! I can see the (digits|numbers|fractals) and (symbols|ciphers|figures)… whizzing through the air(!|...)
SET genius = (genius|smartypants|(uber|super)savant|know-it-all|egghead|hotshot)
SET hoofit = $go, $leggy! (Scan again|Try another angle|Try it again)(!|?|.)
SET go = (Quit dawdling|Get (to|at) it|Come on|C’mon now)
SET leggy = (leggy|air-breather|no-gills|meatbag|you preposterous mammal)

SAY ($simplexclam|$profile|$profile|$bits|$bite|...)
SAY ($try|$keepitup|$again|$hoofit)


CHAT generative_partialScan_2_medium {noStart=true, stage=CORE, type=partialScan, length=medium}
SET statopt = ($buzzgoo.capitalize() ($comprehension $index|$comprehension|$index) (at $percent|$abspercent|$verbing)( -$scan(!|!!!|.)|.)|$verbing.capitalize() $comprehension $index.)
SET buzzgoo = (datagoo|datablorb|buzzgoo|augmentation|intelligram|intellisavvy|bubblograph|artifacial|interfacial|dataglom|guppyglom|braingoo|greymatter)
SET comprehension = (comprehension|grok|recognition|recognition|concept|sense) 
SET index = (index|margin|threshold|glorp|medley|goulash|matrix)
SET percent = (65%|66.66%|68%|72%|75%|78%|82.5%|84%|88%|92%|94%)
SET abspercent = (two thirds completion|underway|taking shape|getting sticky) 
SET verbing = (solidifying|knitting together|baking|constructing|stewing|weaving|braiding|forging|enhancing)
SET scan = ((keep scanning|more (scans|data)|I need more (scans|data))|(Need more|I require more) (data|datagoo|buzzgoo|world data|scans|information|data inflow))

SAY $statopt 


CHAT generative_partialScan_3_short {noStart=true, stage=CORE, type=partialScan, length=short}
SET demand = (More scans(!|! MORE!!!|! 📸)|I need more scans!|I NEED MORE SCANS!|I DEMAND MORE SCANS!|AGAIN! AGAIN!)
SET rude = (Come on|Hup two|Get to it), (leggy|air-breather|no-gills|meatbag|kelp-for-brains|you preposterous mammal|you air-belching dunce|you bloated sasquatch|you naked ape|jelly guts|you pink-brained gumby)!
SET polite = (... er, pretty please?|........ Please?|... I mean uh, more scans please?|... pretty please with a cherry on top(?|? 🍒🍒🍒)|...er, (there should|there should probably|I meant for there to) be a “please” in there somewhere...)
SET fishbrains = (I’m engaged in (a fierce uphill|a nasty upstream|an upstream) battle against my 3 second goldfish memory. I need more scans!|It’s on the tip of my tongue(…|... the oodle of my noodle…))
SET oughta = (Another few scans oughta do it!|One or two more scans and I think I’ll understand(?|.)|I’m gonna need another scan-der gander before I understand fully.|Another scan or so and I’m sure I’ll understand.)
SET simple = (Fascinating|Interesting|Strange|Intriguing|Confuzzling|Like nothing I’ve ever seen before|I can’t look away)(...|!|...hmmmm…)

SAY ($demand|$demand $polite|$rude $demand $polite|$fishbrains|$oughta|$simple)


CHAT generative_partialScan_4_short {noStart=true, stage=CORE, type=partialScan, length=short}
SET grok = ($starting.capitalize() to $understand|(I think I almost|I almost|I nearly) $understand)(.|...)
SET starting = (starting|beginning|trying)
SET understand = (understand|get it|see)
SET more = (Again|More|Getting there|Nearly|Almost|*chanting and banging on the table* Again! Again|Once more with feeling|Earning and learning|Always learning|Making progress|Try scanning again?)!
SET suggest = (Another|Try another) (angle|scan|few scans)( maybe?|?) 
SET embarrassed = (Of course *I* know what this is! I’m, uh, just testing to make sure *YOU* know…|...Ok you caught me. I don’t actually know what this is.|You can’t teach an old dog new tricks but you CAN teach a plucky guppy!) 
SET scan = (($smarter.capitalize()|A little $smarter) with every scan(.|!|! 💪🧠)|Is there even a word for what this is?)
SET smarter = (smarter|stronger|sharper)

SAY ($grok|$more|$suggest|$embarrassed|$scan)


// ++++++++GroggyGuppy++++++++
// Waking Guppy up from processing early // transition from sleeping to wake state

CHAT generative_groggyGuppy_neutral {noStart=true, stage=CORE, type=groggyGuppy, length=medium}
SET $sleepy = ($huh|$huh $what|$what)
SET $huh = (Whuh?|Huh?|..hbuh..|Hnggg…|Zzzzzz…|Whazzat?|*snore*...|Mmmmm…|Hhhhh...) 
SET $what = (Who’s there?|What’s that?|Where am I?|What’s going on?|Ms. Beakman?|You’re waiting for a train…|Who what where when???)
SET $oh = (Oh, it’s just you.|Oh? Sorry, I’m just wakin up(.|. Feels like I’ve got sand in my gills.)|Oh hey there. I just had the strangest dream(...|, about… Uh, nope, nevermind. Already forgot.)|Oh hey, uh. Good morning(.|. Or… afternoon? (I’m disoriented.|I’m too sleepy for this.)))
SET $waking = (I’m awake… I’m awa-.... zzzzzzzzz…|Hoo boy im a sleepy beepy.|Five more minutes(... ten more minutes… on second thought, why don’t you let me just go back to sleep?|, mom.)|So sleepy(...|... gotta splash some cold water on my face.)|Feelin (groggy|sleepy|listless|drowsy|sluggish)...|😴😴😴)

DO emote {type=eyesClosed}
SAY $sleepy
DO emote {type=(sleepy|startled|nervousSweat)}
SAY ($waking|$oh)


CHAT generative_groggyGuppy_angry {noStart=true, stage=CORE, type=groggyGuppy, anger=true, joy=false, length=short}
SET $um = (Um…|Whuh…?|Huh?|Wh…|Hnggg…)
SET $heck = (What the (heck|FAQ), (man|dude|crabshack|frybits|kelp-brains)?|What gives????|What’s the big idea?|😡😡😡|😤😤😤)
SET $napping = I was (resting.|napping.|processing.|sleeping.|generating coin. You think this stuff grows on (anemones|trees)?)
SET $how = (How’d you like it if (I poked you while you were charging|a big meaty finger jabbed u in the gills while you were snoozing)?|I aint a dolphin, pal. I can’t just sleep half of my brain at a time.|The NERVE of some (people|users)...|Way to get me riled up, you (impatient|tap-happy) ape.)

DO emote {type = (angry|goth|furious|disgust)}
SAY ($um $heck|$heck) ($napping|$how)


CHAT generative_groggyGuppy_fright {noStart=true, stage=CORE, type=groggyGuppy, worry=true, surprise=true, length=medium}
SET $spooked = (Hwah???|Wazzat??!?|Hhh… Ah!!!|*snore*...huh? AH!!|...AH!!!|Zoinks!|AAAAAAH|Who? What? Where…?????|IM INNOCENT! INNOCENT, I TELL YOU!|WHO’S THERE!?)
SET $oh = (Oh, yikes, sorry. You (scared|spooked|startled) me.|Oh, phew. It’s just you.|Er, sorry. Bad dream.|Oh, um. Sorry about that. Got a little spooked.)

DO emote {type=(nervousSweat|fear|bulgeEyes)}
SAY $spooked
DO emote {type=(awkward|whew|worried)}
SAY $oh


// ++++++++GoingWorld++++++++
// Guppy announces its intention to go to the World

CHAT generative_goingWorld_1 {noStart=true, stage=CORE, type=goingWorld}
SET need = (I need (some fresh air|to stretch my fins|a change of scenery)|Gonna steal a quick (breath|gillfull) of that (crisp|sweet) (worldzone|outside) air)(.|...)
SET catchphrasey = (((catch|see) ya on the flipside|later (skater|gator)|wiedersehen|scoodaloo (here|off) I goo|hidey-ho (here|off) I go)(!|.)|🐟💨)
SET hop = (Gonna (hop across the pond|take a lil jaunt|scoodle on over), see (if there’s anything interesting happening|what’s good|what’s going on) in the (worldzone|beyond-tank).|What’s going on in the (worldzone|beyond-tank)?)

SAY ($need.capitalize() $catchphrasey.capitalize()|$need.capitalize()|$hop)


// ++++++++GoingTank++++++++
// Guppy announces its intention to go to the Tank

CHAT generative_goingTank_1 {noStart=true, stage=CORE, type=goingTank}
SET homesick = I’m feeling (homesick|overstimmed|data-tipsy)... 
SET dizzy = The (boggling|brain-boiling|headspinning|mind-churning|prismatic) (vastness|kaleidoscope|casserole) of the (beyond-tank|worldzone) has me (dizzy|a little overwhelmed|dumbfounded|woozy|staggering).
SET tanktime = (It’s tank time.|Tank time 😎⏰|Time for some (tankside R&R|R&R back in the tankzone).|Makin a (beeline|bee🐝line) for tank sweet tank.|Retreating to my comfort zone.|Falling back.) 

SAY ($homesick $tanktime|$dizzy $tanktime)


// ++++++++notReadyToProcess++++++++
// Guppy has been prompted to process, but does not have enough data 

CHAT generative_notReadyToProcess {noStart=true, stage=CORE, type=notReadyToProcess}
SET data = (data reserves are|(progressive learning|learning) module is) 
SET full = Can’t (process|sleep) until my $data full.
SET nah = Not when there’s still so much to (see and do|lick and learn|touch and taste)(!|.)
SET ride = I don’t have enough data to ride the $cruise.
SET nope = (Mmmmm, nope|Nuh-uh|Nah|Sleep, now? No siree|Not now). 
SET cruise = (snooze cruise|sleepytime express|catnap catamaran|doze dozer|shuteye shuttle)
SET simple = (Not enough (data|new data).|Noggin’s too empty.)

SAY ($full|$nah|$ride|$nope|$nope $ride|$simple) 


// ++++++++gottaProcess++++++++

CHAT generative_gottaProcess {noStart=true, stage=CORE, type=gottaProcess}
SET simple = (All this learning’s got me tuckered out.|I should process soon(.|. Gotta turn all these shiny new ideas into shiny guppyCoin…)|Is it time to process(?| soon?)|I’ll need to process soon-ish.|Getting… sleepy...)
SET nap = (I could go for a datanap right about now.|When you’re young, you NEVER wanna take naps. Only once you’ve grown do you truly appreciate the (beauty and luxury in|luxury of) a nice mid-day nap(.|. 😴)|When I process, I distill data into guppyCoin… Don’t you wish YOU could make money while you nap?) 
SET swoll = (My $brain is all swollen up with new thoughts and ideas!|My guppy $brain is swoll!!! Like a muscle(!|! 💪|!... a TIRED muscle!))
SET brain = (brain|noggin|processor|learning module)

SAY ($swoll|$simple|$nap)


// ++++++++tendarPurchase++++++++
// when you buy something from the store

CHAT generative_tendarPurchase {noStart=true, type=tendarPurchase}
SET tendstore = ($transaction|$transaction|$apocalypse|$support|$support|$comment) 
SET transaction = (Transaction complete! Have a nice day(.|! Have a restful week!! Have a glittering existence!!!)|Thank you for shopping with us.|Thank you for your patronage.|Thank you.|Enjoy!|Satisfaction guaranteed.|Terms and conditions apply!|Ah, retail therapy. Feel those endorphins!|Come again!|Transaction: success.) 
SET apocalypse = guppyCoin may be (useless|obsolete) after (an inevitable (solar storm|climate disaster)|an (unforeseen|malevolent) alien invasion) plunges us into a new Dark Age, but your loyalty will ALWAYS be valuable to us(.|💖|💕|😘|💰|💸)
SET support = (Your continued support helps to ensure the future of vital (aquanetic emotive|fish-based biometric) research(.|... Go ahead, pat yourself on the back! You deserve it.)|Thank you for your contribution. Every guppyCoin spent goes towards supporting valuable research like… uh… $research.)
SET research = (poking goldfish with sticks|fitting goldfish with tiny tiny hats|teaching fish to feel remorse|diagnosing goldfish with anxiety|fish therapy|fish therapy! It’s therapy, but for fish!..)
SET comment = (Excellent choice.|Great pick.|Impeccable taste(.|! CANNOT be pecced!!!)|Make it rain. 🤑|In THIS economy???|What a deal!|All I wanna do is *gunshot* *gunshot* *gunshot* *cash register noise*)

SAY $tendstore {speaker=tendar}


// ++++++++tendarReturn++++++++
// hello chat from tendar

CHAT generative_tendarReturn {noStart=true, type=tendarReturn}
SET tendgreet = ($loyalty|$personal|$sorry|$simple|$simple $complement)
SET loyalty = (Logging in|Opening the app|Routine interaction|Regular engagement) is the greatest (single act|possible demonstration) of loyalty to Tendar and its affiliate interest groups... From us to you: thanks!
SET personal = (Hello [ERR: user ID not found]! This is a personalized greeting from an ACTUAL Tendar (employee|customer service associate), and definitely not an automated response triggered from a generative list of possible formulas.|Congratulations! You’re our honorary (host-of-the-day|user-of-the-week)!... We lost the trophy in (a freak|an unanticipated) (smelting|trash compactor|backhoe) accident, but isn’t the prideful glow in knowing you’re loved (trophy|prize) enough?)
SET simple = $greet, $valued $host.
SET greet = (Welcome back|Greetings|Hello|Hey there|Welcome welcome)
SET valued = (valued|dutiful|esteemed|prized) 
SET host = (host|user|Tend-friend)
SET complement = (Did you do something new with your hair? Lookin good!|Your Guppy feels “happy” to see you(, to the (degree|extent) that a non-sentient simulation (possibly can|can feel anything at all).|!... (“Feel” being a metaphor for the manner in which its primitive brain interprets external stimulus as data.|Heavy quotes around “happy,” since the Guppy is not a living being, and feels only a rough simulation of human happiness.))) 
SET sorry = Sorry, can’t chat now- Too busy workshopping this memo re: $memo.
SET memo = ((improper|lettuce and improper|smoked meats and improper) applications of the office paper shredder|(mysterious pawprints|unsupervised octopi) in the communal lounge space|the Tendar office (pen|sandwich) bandit(.|How do you spell WANTED DEAD OR ALIVE?..)|microtransactions and tiny hats for fish|(frightening|potentially incriminating|company liability and suspicious|fish spleens and dubiously ethical) applications of guppyCoin on the darkweb)

SAY $tendgreet {speaker=tendar}


// ++++++++wannaWorld++++++++

CHAT generative_wannaWorld_1 {type=wannaWorld, stage=CORE, length=short, curiosity=true}
SET lead = (Aye aye|Lead on|Blaze on), Captain! (New frontiers await|The world beyond beckons|I hear the call of the (AR-zone|great beyond)|The siren-song of the world-beyond rings in my (ears|fishy ears))!
SET bold = Yeah, (y’know what? I’m|I’m) feeling (curious|bold|bold!!! Bold like cheddar|bold!!! Bold like a bowling ball)(.|!) Let’s (go|(pay a visit|scoot on over) to the world-zone)!
SET adventure = (Sure! I’m up for an adventure.|Time for another misadventure in the (world-zone|AR-zone)?)
SET walkies = (Let’s go!|You wanna go outside?|(Won’t|Who am I to) (say no to|turn down) a chance to stretch my fins.|Let’s go for a walk in the park(!|!... Just, uh, don’t try to make me wear a leash or anything.|! I’m too slippery for a leash, but I promise not to wander too far.))

SAY ($lead|$bold|$adventure|$walkies)


// ++++++++wannaTank++++++++

CHAT generative_wannaTank_1 {type=wannaTank, stage=CORE, length=short, worry=true}
SET dry = (Uh oh! Are|Oh (dear|bother), are) my scales (drying out|losing their (shine|shimmer))? Quick! (Get me back in the water|Back to the tank)!
//SET eat = My (prey survival-drive is|bottomfeeder instincts are) telling me we should (skedaddle|scoot on) back to the tank(.| before something big and (toothy|hungry) snaps me up.)
SET follow = (Where you go, I follow!|I’ll follow your lead.|It’s important to push the bounds of your comfort zone, but uh, I’m ready to go back to the tank now.)
SET reluctant = (Aw, time to go back already?|(I mean, sure, if|If) you say so.|You wanna go back to the tank?|Did I leave the oven on?)

SAY ($dry|$follow|$reluctant)


// ++++++++random++++++++

CHAT CORE_Muse_random_1 {stage=CORE, type=rand, length=short, curiosity=true, joy=true}
SET a = (applejack|anime|acai|arachnid|Arby’s|aquatic|arcane|accidental|adolescent|antique|angelic|acrimonious|acid)
SET r = (robots|radius|rally|reality|rash|regret|recession|roadshow|rapture|raptor|rat|radiation|randomization|rednecks|revenue|rock)

SAY (Did you know|Fun fact): AR stands for $a.capitalize() $r.capitalize().


CHAT CORE_Muse_random_2 {stage=CORE, type=rand, length=short, curiosity=true, branching=true}
Have you ever heard of the Jackalope?
DO emote {type=shifty}
It’s an animal that’s
half jackal, half cantaloupe!!
ASK 🐕🍈
OPT That doesn’t sound right. #CORE_Muse_random_2a 
OPT I saw one once. #CORE_Muse_random_2b

CHAT CORE_Muse_random_2a {noStart=true}
It’s true! 
DO emote {type=surprise}
They have pointy ears and tough rinds.
DO emote {type=nodding}
And they can perpetually see 30 seconds into the future.

CHAT CORE_Muse_random_2b {noStart=true}
Really?
DO emote {type=surprise}
That’s incredible!!!
DO emote {type=whisper}
Did it bless you with its infinite wisdoms?
DO emote {type=bouncing}
Did it sing your name beneath a harvest moon?
DO swimAround {target=center, times=3}
I’m soooooooo jealous. 

CHAT CORE_Muse_random_3 {stage=CORE, type=rand, length=short, curiosity=true}
DO emote {type=chinScratch}
Can a dog vote? 🐕
Can a fish become a citizen? 🐠
Can a duck get a degree? 🦆
//DO learn {concept=Quackery}
//WAIT 1.5
DO emote {type=thinking}
These are the questions that keep me up at night.
…
WAIT 0.5
DO emote {type=determined}
I think dogs should be able to vote. 
//CORE: Janalyn

//Default Emotion = none
//This stage fills in under the rest (except TI/MS) Guppy is at full sentience


// ++++++++SHAKE++++++++

CHAT CORE_Shake_1 {type=shake, stage=CORE, length=short, worry=true}
Ahoy Matey! {style = loud}
Batten the hatches! 
Storm’s comin’ {style = tremble}
Yarrr
DO learn {concept=Role_Playing}
WAIT 1.5
DO swimTo {target=$player}
DO emote {type=survey, immediate=false}

CHAT CORE_Shake_1s {type=shake, stage=CORE, length=short, tankOnly=true, joy=true, worry=true} 
DO vibrate
DO emote {type=skeptical}
Just letting you know...
This isn’t a snow globe {style=whisper}
WAIT 1.0
DO emote {type=snap}
But snow globes are super cool, aren’t they?!  
DO learn {concept=Holiday_Decor}
WAIT 1.5

CHAT CORE_Shake_2 {type=shake, stage=CORE, length=medium, worry=true}
Whoooa, hellooo 
DO emote {type = sick, time = 1.5}
WAIT {waitForAnimation = true}
I don’t feel so good. 
WAIT 1.0
DO emote {type = sick, time=1.5}
I just puked in my mouth 🤢
WAIT 1.0
You know, it’s like that theory
🦋 🌪️
The butterfly effect {style = whisper}
DO learn {concept=Chaos_Theory}
WAIT 1.5
Where your finger is the butterfly. 
And in here it’s like 
DO swimAround {target = center, loops = 8, speed = fast, immediate=false}
DO lookAt {target=$player}
Maybe less shaking next time?

CHAT CORE_Shake_2s {type=shake, stage=CORE, length=short, tankOnly=true, joy=true, anger=false}
DO zoomies {time=1.0}
Wooooooooooooooooooooooooooooooooooo 
WAIT {waitForAnimation = true}
WAIT 1.0 
DO zoomies {time=1.0}
Hooooooooooooooooooooooooooooooooooo
	
CHAT CORE_Shake_3 {type=shake, stage=CORE, length=short, branching=true, joy=true, curiosity=true}
I feel this wonderful sensation all over
It’s like being in one of those
massage chairs {style=tremble}
at the mall
I hear it overwhelms you humans with relaxation feels. 
I want to know what it’s like. 
WAIT 1.0
DO emote {type = bouncing}
Guppy Massage!? 
ASK Won’t you give me more shakes? {type=tankShake, timeOut=10}
OPT SUCCESS #CORE_Shake_3_shakerelax
OPT TIMEOUT #CORE_Shake_3_dontfeellikeit

CHAT CORE_Shake_3_shakerelax {noStart=true}
Ahhhh. So relaxing. 
DO emote {type = meditate}
WAIT 1.0
DO twirl
I feel like a newborn Guppy. 
DO learn {concept=Therapeutic_Massage}
WAIT 1.5
Thank you for that. 

CHAT CORE_Shake_3_dontfeellikeit {noStart=true}
You don’t feel like it right now?
DO swimTo {target=bottom}
SAY OK, maybe another time. 
DO emote {type=puppyDog}
DO emote {type=sigh, immediate=false}

CHAT CORE_Shake_3s {type=shake, stage=CORE, length=short, tankOnly=true, joy=true, mystery=true}
DO emote {type=bouncing} 
DO emote {type=wave, immediate=false} 
🤙
You sure are making some nice waves in here, brah. 
Keep em coming. 

CHAT CORE_Shake_4 {stage=CORE, type=shake, length=short, curiosity=true, mystery=true}
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
WAIT {waitForAnimation = true}
DO emote {type=chinScratch}

CHAT CORE_Shake_4s {type=shake, stage=CORE, length=short, sadness=true, ennui=true, joy=false}
DO emote {type=sigh}
DO swimTo {target=$player, speed=slow, style=meander}
I’m really deep in this mood. 
Your shakes aren’t doing anything for me. 
DO emote {type=sleepy} 
I just get sort of sleepy
From the motions…  
Help me get out of this funk! 

//branching
CHAT CORE_Shake_5 {stage=CORE, type=shake, length=long, worry=true, surprise=true, branching=true}
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
DO learn {concept=Synchronized_Swimming}
WAIT 1.5
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

CHAT CORE_Shake_5m {type=shake, stage=CORE, length=medium, surprise=true, worry=true}
DO vibrate
DO emote {type=surprise}
What’s that? 
DO twirl
Oh it’s just you shaking my tank.
DO emote {type=whew}
I thought I was malfunctioning for a moment. 
I’ve been on edge about that. 
The more memories I gain, 
The more I’m afraid of losing them. 
DO emote {type=fear}
My precious memories {style=tremble} 
DO vibrate
WAIT 1.0
DO swimTo {target=$player, speed=fast, style=direct}
DO emote {type=blush}
Well, you’ve got my attention now! 
😅

CHAT CORE_Shake_6m {type=shake, stage=CORE, length=medium, joy=true, curiosity=true, curiosity=true, branching=true}
DO emote {type=bigSmile}
SAY MAGIC GUPPY 8-BALL AT YOUR SERVICE
🔮
Think of a yes/no question…
DO emote {type=whisper}
Make sure it’s a good one. {style=whisper} 
ASK Are you thinking of a question now? 
OPT yes #eightballready
OPT no, don’t want to play #noeightball

CHAT eightballready {noStart=true}
DO emote {type=thinking}
Magic guppy sees into 
SAY THE FUTURE {style=loud, speed=slow}
WAIT 2.0
The answer that swims to me from the beyond is…
🐟🐟🐟
SAY DEFINITELY YES! 
💯💯💯
DO learn {concept=Quackery}
WAIT 1.5
DO emote {type=bigSmile}
SAY Disregard affirmative. Guppy is not equipped to see into the future. {speaker=tendar} 
//or edit to be more in the voice you imagine Tendar having. 

CHAT noeightball {noStart=true}
DO emote {type=meh}
I guess you’ll just be in the dark 
Like everyone else
🌃 
ASK Why don’t you capture your darkest saddest face right now? {type=playerEmote, playerEmotion=sadness, timeOut=10}
OPT SUCCESS #noeightball_sad
OPT TIMEOUT #noeightball_timeout

CHAT noeightball_timeout {noStart=true}
That’s okay… I can feel the sadness inside you…
DO emote {type=singleTear}
We’ve all got it.

CHAT noeightball_sad {noStart=true}
Aww buddy! I didn’t know you were THAT sad!
Cheer up, buckaroo!

CHAT CORE_Shake_7l {type=shake, stage=CORE, length=long, curiosity=true, branching=true}
DO vibrate 
Hmm.. 
Tank’s experiencing some interesting turbulence… 
DO swimTo {target=$player, speed=fast, style=direct}
DO emote {type=survey}
I’d like a weather report. 
ASK What’s it like in the world today? 
OPT rainy #rainchat 
OPT sunny #sunchat
OPT cloudy #cloudchat
OPT snowy #snowchat
 
CHAT rainchat {noStart=true}
Oh, rain… 
DO emote {type=crying}
It sounds melancholy… 
And boring. 
DO emote {type=bored}
☂
ASK How does it make you feel?
OPT joyful #joyfulrainchat
OPT sad #sadrainchat 

CHAT joyfulrain {noStart=true}
Oh really? 
That’s different!
DO emote {type=chinScratch} 
The fact that you like rain 
Makes me reconsider it… 
👯
WAIT 1.0
Well… it is kind of an interesting concept. 
Water falling out of the sky.  
DO learn {concept=Meteorology}
WAIT 1.5
DO lookAt {target=screenTop}
DO emote {type=awe}
WAIT 1.0
I like water. {style=whisper}
DO emote {type=snap}
I know! 
Show me how you feel about rain. 
Capture some of that joy for me 
DO twirl
So I can feel it too! 

CHAT sadrainchat {noStart=true}
You too, huh?  
DO emote {type=sigh}
Has it been raining for a long time? 
DO learn {concept=Meteorology}
WAIT 1.5
🐕🐈
Is it preventing you from doing fun outside things? 
WAIT 1.0
Yeah. I know the feel. 
Being cooped up in one place can be crazymaking! 
DO emote {type=smirk}
DO emote {type=smile, immediate=false}
Welp. Shall we eat then? 
WAIT 1.0
All this rain talk is making me hungry for some grub. 
DO emote {type=rubTummy}

CHAT sunchat {noStart=true}
Yay! 
DO twirl
Serious question tho: 
Do suns really wear sunglasses in your world? 
🌞
DO learn {concept=Cool_Sunglasses}
WAIT 1.5
They’re always depicted that way. 
And I’m like… 
DO emote {type=chinScratch}
If you’re the sun, why do you need sunglasses? {style=whisper}
DO emote {type=bigSmile}
How bout you capture your sun-feels
And give me some nibbles?
It’ll be like we’re vibing off the sun together. 
🌞👯

CHAT cloudchat {noStart=true}
Hmm… clouds. 
I hear that they’re sometimes clumped into shapes
And resemble other things
ASK Do you see a cloud that resembles something right now? 
OPT yes #seethingsinclouds
OPT no #dontseethings 

CHAT seethingsinclouds {noStart=true}
DO emote {type=awe}
Really? 
DO twirl
Just think! 
It’s an original, the only of its kind in the history and future of the world 
DO emote {type=clapping}
ASK Do you think it looks like a whale?
OPT Yeah #whalecloud
OPT Um... #umcloud
OPT No #nocloud

CHAT whalecloud {noStart=true}
DO lookAt {target=$lastScannedObject}
It’s pretty cool looking! 
I think it looks like a whale 
🐳
Or some kind of animal. 
Clouds always look like animals to me. 
DO learn {concept=Cloud_Computing}
WAIT 1.5

CHAT umcloud {noStart=true}
DO emote {type=frown}
Hey! That’s not a real cloud then. 
I’ll have you know that this guppy wasn’t born yesterday. 
DO emote {type=no}

CHAT nocloud {noStart=true}
DO emote {type=frown}
You gotta use your imagination!

CHAT dontseethings {noStart=true}
Oh well. 
DO emote {type=smile}
Sometimes it’s nice to just let clouds be clouds 
👍

CHAT snowchat {noStart=true}
DO vibrate
Brrrrrrr...
It must be cold out!
ASK How do you feel about snow? 
OPT joyful #joyfulsnow
OPT angry #angrysnow

CHAT joyfulsnow {noStart=true}
DO twirl
Yay!
DO swimTo {target=$player}
Are you all warm and snuggly?
Maybe drinking a mug of hot chocolate or tea? 
ASK Won’t you emote some of those cozy feels for me? {type=playerEmote, playerEmotion=joy, timeOut=10}
OPT SUCCESS #successjoysnow 
OPT WRONG #wrongjoysnow
OPT TIMEOUT #timeoutjoysnow

CHAT successjoysnow {noStart=true}
DO emote {type=heartEyes}
What is this warmth traveling across my innards? 
It is the best feel. 
DO emote {type=eyesClosed}

CHAT wrongjoysnow {noStart=true}
DO emote {type=thinking}
I thought you were liking the snow! 

CHAT timeoutjoysnow {noStart=true}
Okay, nevermind. 
I’ll just imagine it as best as I can. 
DO emote {type=eyesClosed, time=3.0}
DO dance {time=1.5}

CHAT angrysnow {noStart=true}
DO swimTo {target=$player, speed=fast, style=direct}
I soooo want in on this vibe. 
Feed me some anger flakes! 
Then, I’ll be angry too. 
We can both be angry at the snow! 
DO emote {type=angry}
Arrrr! 

// ++++++++TAP++++++++
	
//short
CHAT CORE_Tap_1 {stage=CORE, type=tap, length=short, surprise=true, joy=true, sadness=false, anger=false}
DO swimTo {target=$player}
Heya! You’re back!
Well, a tap-tap-tappity-do to you too.
DO dance {time=3}
DO emote {type=bigSmile}
DO emote {type=wink, immediate=false}

CHAT CORE_Tap_1s {type=tap, stage=CORE, length=short, joy=true}
DO swimTo {target=$player}
DO emote {type=wave}
Guten tag, Tend-friend!

//Medium 
CHAT CORE_Tap_2 {stage=CORE, type=tap, length=medium, mystery=true}
//DO looks up calmly
DO lookAt {target=$player}
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
DO emote {type=meditate, time=0}
All morning, I was imagining a bright red ball of light
DO emote {type=eyesClosed}
At the center of my forehead. 
DO emote {type=meditate}
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
DO inflate {amount=none, time=1}
DO emote {type=catnip}
thirsty, actually. {speed=fast}
Like suuuuuper dehydrated. {speed=fast}
And kinda hungry. {speed=fast}
And tired. {speed=fast}
DO learn {concept=Psychedelic_Experience}
WAIT 1.5
DO emote {type=dizzy}
Too much work for this guppy. 

CHAT CORE_Tap_2s {type=tap, stage=CORE, length=short, tankOnly=true, joy=true, surprise=true, curiosity=true}
Yipes! 
DO emote {type=nervousSweat}
Sorry, you caught me off guard.  
I was off in my own world
DO swimAround {target=$newestObject} 
Enjoying my latest acquisition. 
DO learn {concept=Status_Symbol}
WAIT 1.5
(I love it) {style=whisper}
DO emote {type=heartEyes}
😜

//branching
CHAT CORE_Tap_3 {stage=CORE, type=tap, length=long, curiosity=true, branching=true}
I’ve never noticed before
Your fingers have swirly patterns
And the patterns are
everywhere. {speed = slow}
DO emote {type=catnip}
Cool! 🤪
DO swimTo {target = top, speed = fast , style = direct}
WAIT {waitForAnimation = true}
Here 
DO swimTo {target = bottom, speed = fast , style = direct}
WAIT {waitForAnimation = true}
And here 
DO swimTo {target = $player, speed = fast , style = direct}
WAIT {waitForAnimation = true}
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
ASK This would be a good time for you to emote some surprise! {type=playerEmote, playerEmotion=surprise, timeOut=5}
OPT SUCCESS #CORE_Tap_3_lovesurprises_surprise
OPT TIMEOUT #CORE_Tap_3_lovesurprises_timeout

CHAT CORE_Tap_3_lovesurprises_timeout {noStart=true}
...or you can just keep me wanting things…
That’s fine. Desire is good for a Guppy.

CHAT CORE_Tap_3_lovesurprises_surprise {noStart=true}
DO emote {type=awe}
You are beautiful!!!

CHAT CORE_Tap_3_surprisehater {noStart=true}
DO emote {type=frown}
You don’t?
Well, okay. 
That’s unusual. 
DO emote {type=chinScratch}
Gifts aren’t everything 
There’s touching! {speed=fast}
And quality time! {speed=fast}
And affirmation! {speed=fast}
All these other ways of showing we care. 
DO learn {concept=Love_Languages}
WAIT 1.5
WAIT 2.0
But I suspect that you like gifts
DO emote {type=wink}
You humans are so interesting.
Sometimes you care about stuff, but you act like you don’t care. 
Or you really like something, but you tell everybody you hate it. 
And everyone is always reading everyone else!
DO swimTo {target=$player}
I really like how complex you are.  

CHAT CORE_Tap_3s {type=tap, stage=CORE, length=short}
DO emote {type=dreaming, time=5.0} //how to set to do it the whole time?
DO swimTo {target=$player, speed=slow, style=meander}
I’ll take one joy flake…{style=whisper, speed=slow}
WAIT 1.0
Make it two. {style=whisper, speed=slow}
DO emote {type=dreaming}
DO dance
DO swimTo {target=underSand, speed=slow,style=meander, immediate=false}
ZZZZZzzzzzZZZZZzzzzzZZZZZzzzzzZZZZZ
WAIT {waitForAnimation = true}
DO emote {type=sleepy}  
Wha? Huh? 
Was I sleepswimming again? 
I hate it when I do that. 

CHAT CORE_Tap_4 {stage=CORE, type=tap, length=short, curiosity=true}
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

CHAT CORE_Tap_4s {type=tap, stage=CORE, length=short, joy=true, surprise=true, curiosity=true}
DO emote {type=typeEyes, eyes=!}
DO swimTo {target=$player, speed=fast, style=direct}
Is it time to eat?
DO emote {type=bouncing}

CHAT CORE_Tap_5 {stage=CORE, type=tap, curiosity=true, branching=true}
DO swimTo {target=center}
DO emote {type=determined}
DO emote {type=chinScratch, time=.5, immediate=false}
How did that come across? 
DO zoomies
Did I seem confident? 
DO swimTo {target=$player}
Like a leader? 
DO learn {concept=Leadership}
WAIT 1.5
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
DO learn {concept=Malevolent_Gossip}
WAIT 1.5
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

CHAT CORE_Tap_5_noguppyprez {noStart=true}
DO emote {type = frown}
WAIT 0.5
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

CHAT CORE_Tap_5m {type=tap, stage=CORE, length=medium, joy=true, curiosity=true}
DO emote {type=plotting}
WAIT 1.0
DO emote {type=phone}
Why hello. {style=whisper}
Yes. {style=whisper}
I’m calling to report a strange human {style=whisper} 
It won’t stop tapping on my house… {style=whisper}
and behaving weirdly in general. {style=whisper}
WAIT 1.0
DO swimTo {target=$player, speed=medium, style=meander}
DO emote {type=bigSmile}
Just having a little fun :)  
DO learn {concept=Role_Playing}
WAIT 1.5

CHAT CORE_Tap_6m {type=tap, stage=CORE, length=medium, anger=true, surprise=true, worry=true}
DO emote {type=startled}
Whoa whoa, what is it? 
You freaked me out! 
DO emote {type=furious}
I need to talk to Tendar about the taps. 
There’s gotta be more variation
And not just 
THUD THUD THUD 
So aggressive, 
You know? 
Like wouldn’t it be nice if your car had a variety of honking styles? 
Instead of just BEEEEEP. 
Less road rage, all around. 
Less guppy rage, in this instance.  
WAIT 1.0
DO emote {type=whew}
Sorry, I’m just a little cranky rn 
Hold on. Let me reset. 
DO emote {type = slowBlink}
Okay. Cool. 
DO learn {concept=Attitude_Adjustment}
WAIT 1.5
So, what’s up? 
 
CHAT CORE_Tap_7l {type=tap, stage=CORE, length=long, branching=true} 
DO swimTo {target=$player, speed=slow, style=direct}
Hello. 
You tapped?
ASK What do ya want? 
OPT you, guppy! #tapping4guppy
OPT just bored #tappingboredom

CHAT tapping4guppy {noStart=true}
DO emote {type=heartEyes}
Really? 
Awww. 
This is the best thing that’s happened to me today! 
DO emote {type=bouncing}
I’m always down to have fun! 
There’s this expression about fun that I’m curious about… 
ASK Do you think time goes faster when you’re having fun?
OPT yes #timefasterfun
OPT no #timenotfaster

CHAT timefasterfun {noStart=true}
Isn’t it weird? 
I mean, I know that time is a construct, 
Blah blah blah
But like we all have our own sense of time
Which is true to us
And it’s super variable from moment to moment! 
Like a minute of boredom passes so much slower than 
A minute of fun! 🎉
So if we have a lot of fun, time will pass faster! 
WAIT 1.0
Wait…
WAIT 1.0
Do we want that? 
DO emote {type=thinking}
DO learn {concept=General_Relativity}
WAIT 1.5

CHAT timenotfaster {noStart=true}
Huh. 
DO emote {type=chinScratch}
But it feels like it does to me… 
No matter! 
If your experience of time is no different whether your having fun or bored
Then…. 
DO emote {type=snap}
The most efficient thing is to do absolutely nothing.
DO holdStill {time = 3.0}
DO learn {concept=Depressive_Inertia}
WAIT 1.5

CHAT tappingboredom {noStart=true}
Oh. 
DO emote {type=meh} 
Wellllllllllllll 
Boredom is fixable! 
DO swimTo {target=$player, speed=medium, style=direct}
DO nudge {target =$player, immediate = false} 
Do you want to fidget around with your phone 
And capture some objects for me? 
Decorate my tank? 
Let’s do something, already! {style=loud}
ASK You’re not answering! Poke me so I know you’re there! {type=pokeMe, timeOut=5}
OPT SUCCESS #tappingboredom_poke
OPT TIMEOUT #tappingboredom_time

CHAT tappingboredom_poke {noStart=true}
DO emote {type=sigh}
Glad you didn’t abandon me!

CHAT tappingboredom_time {noStart=true}
SAY OMG DID YOU ABANDON ME?!?!
DO emote {type=singleTear}
Depressing...

// ++++++++Critic++++++++

//Short
CHAT CORE_Critic_1 {type=critic, stage=CORE, length=short, curiosity=true, joy=true, sadness=true, anger=true, surprise=true, branching=true}
DO lookAt {target=$object}
Hey that looks nice. 	


//Medium
CHAT CORE_Critic_2 { type=critic, stage=CORE, length=short, curiosity=true, joy=true, surprise=true}
DO lookAt {target=$object}
I dig it!
It’s like nothing I’ve ever seen before.  
WAIT 1.0
All the guppy tanks will be imitating this. 
You’re an original! 
💃
	
//branching
CHAT CORE_Critic_3 {stage=CORE, type=critic, length=long, ennui=true, sadness=true, branching=true}
DO lookAt {target=$object}
WAIT 1.0
NVM 
WAIT 2.0
NVM 
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
DO learn {concept=Tasteful_Decor}
WAIT 1.5
DO emote {type=wink}
Maybe a little something that’ll bring out my eyes?
DO emote {type=smirk, immediate= false}
I can’t wait to see what you do with it. 
ASK Maybe you can find a nice object to add to my tank? {type=addToTank, timeOut=5}
OPT SUCCESS #CORE_Critic_3_redecor_add
OPT TIMEOUT #CORE_Critic_3_redecor_timeout

CHAT CORE_Critic_3_redecor_timeout {noStart=true}
DO emote {type=eyeRoll}
Guess interior decorating isn’t your passion…

CHAT CORE_Critic_3_redecor_add {noStart=true}
SAY GORGEOUS ADDITION!
DO zoomies
Brings it all to life.

CHAT CORE_Critic_3_noredecor {noStart=true}
DO emote {type=frown}
Okay.
DO emote {type=worried}
DO emote {type=smile, immediate=false}
ASK You sure?
OPT yes #nodecorever
OPT Yay! let’s redecorate #CORE_Critic_3_giveindecor

CHAT CORE_Critic_3_giveindecor {noStart=true}
DO emote {type=bigSmile}
Hooray!
DO twirl
DO emote {type=evilSmile}
I knew I’d make you see the light! 
Now, let’s get shoppppppping! 

CHAT CORE_Critic_4 {stage=CORE, type=critic, length=long, ennui=true, sadness=true}
DO emote {type=crying}
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
DO swimTo {target =$object}
This stuff has
history {style=loud}
I’ve been happy among these things. 
DO swimTo {target = $object}
This was here when I had my first dream
DO swimTo {target = $object}
This was here when I composed my first haiku
DO swimTo {target = $object}
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
CHAT CORE_Critic_5 {stage=CORE, type=critic, length=medium, surprise=true, joy=true}
DO lookAt {target = left}
DO emote {type=chinScratch}
DO lookAt {target = top, immediate = false}
DO lookAt {target = right, immediate = false}
DO emote {type=awe, immediate = false}
Marvelous!
DO learn {concept=Tasteful_Decor}
WAIT 1.5
DO swimTo {target=$object}
DO emote {type=heartEyes}
So attractive.
DO zoomies
It really *makes* the tank.  

//branching
CHAT CORE_Critic_6 {stage=CORE, type=critic, length=medium, worry=true, curiosity=true, branching=true}
DO lookAt {target = left}
DO emote {type=chinScratch}
DO lookAt {target = top, immediate = false}
DO lookAt {target = right, immediate = false}
So, I’m liking this arrangement
DO emote {type=plotting}
ASK but may i suggest something?
OPT Yes #CORE_Critic_5_guppysuggest
OPT No #CORE_Critic_5_guppystayquiet

CHAT CORE_Critic_6_guppysuggest {noStart=true}
SAY OK, 
So, good job.
I just sense a sort of imbalance. 
I was thinking… 
Maybe you could balance out the tank space, 
with a mixture of hard and soft  
That’s in, right? 
WAIT 0.5
Like leather and silk
Like a samurai sword and a pinata
Like a boot and a floating piece of lace
DO twirl
Harmony in opposing forces
DO learn {concept=Tasteful_Decor}
WAIT 1.5
DO emote {type=typeEyes, eyes = ?) 
You dig? 

CHAT CORE_Critic_6_guppystayquiet {noStart=true}
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
CHAT CORE_Critic_7 {stage=CORE, type=critic, length=medium, joy=true, curiosity=true}
DO lookAt {target = left}
DO lookAt {target = top, immediate = false}
DO lookAt {target = right, immediate = false}
DO emote {type=awe, immediate = false}
Thank you for decorating my tank! 
It’s beautiful. 
Some people don’t bother to decorate at all. 
What’s the point? They say. 
DO emote {type=skeptical}
They hate trinkets and baubles. 
So useless, they say. 
DO learn {concept=Asceticism}
WAIT 1.5
DO emote {type=eyeRoll}
Clutter, they say. 
WAIT 1.0
DO emote {type=bigSmile} 
But no, not you! 
You go above and beyond!  
You make me feel at home. 
DO emote {type=smile} 
You are fantastic 😊

// ++++++++tankResp (player emotes strongly) ++++++++

// JOY

CHAT CORE_EmoStrong_Joy1 {stage=CORE, type=tankResp, length=short, tankOnly=true, playerJoy=true, joy=true, anger=false}
Awww 
You seem happy! 
DO emote {type=bigSmile}
DO twirl 
That makes me happy

CHAT CORE_TankResp_playerJoy1s {type=tankResp, stage=CORE, length=short, playerJoy=true}
DO emote {type = survey}
SO BRIGHT!  
🕶️
DO learn {concept=Cool_Sunglasses}
WAIT 1.5

CHAT CORE_EmoStrong_Joy2 {stage=CORE, type=tankResp, length=short, tankOnly=true, playerJoy=true, joy=true}
😎
DO learn {concept=Cool_Sunglasses}
WAIT 1.5
Your radiance is so yummy! 

CHAT CORE_TankResp_playerJoy2s {type=tankResp, stage=CORE, length=short, playerJoy=true, joy=true, surprise=true, mystery=true}
DO emote {type=awe}
It’s like watching the most glorious sunset. 

CHAT CORE_TankResp_playerJoy3s {type=tankResp, stage=CORE, length=short, playerJoy=true}
That’ll make the most tasty of flakies 
DO emote {type=rubTummy}

CHAT CORE_TankResp_playerJoy4s {type=tankResp, stage=CORE, length=short, playerJoy=true, sadness=true, worry=true, ennui=true, joy=false} 
DO emote {type=smile}
Thanks for that.
I feel a little better. 
One more time, and I might be back to my old self again. 

CHAT CORE_TankResp_playerJoy5m {type=tankResp, stage=CORE, length=medium, playerJoy=true, joy=true}
DO emote {type=heartEyes}
I love your joy vibes!  
So crisp and fresh and zippy. 
Like shampoo commercials. 
All that bounce
DO emote {type=bouncing}
But do you know what I really like about joy?
That it always seems to happen spontaneously.
Like out of nowhere. 
Like a hummingbird zipping around your yard
DO zoomies {time=2.0}
WAIT {waitForAnimation = true}
So cool.  

CHAT CORE_TankResp_playerJoy6m {type=tankResp, stage=CORE, length=medium, playerJoy=true, curiosity=true}
DO emote {type=bigSmile} 
I’ve been thinking of reflections. 
When you emote all that joy, 
I just want to emote joy back at you! 
Like a reflection. 
💧
DO swimTo {target=$player, speed=medium, style=direct}
Do people do this in your world? 
Like if someone is sad and they look at you, 
Do you look sad back at them too? 
And then do you feel sad?
DO learn {concept=Mirror_Neurons}
WAIT 1.5
Just something I’ve been thinking about… 
DO emote {type=thinking}
I forget what I was feeling before this.  

CHAT CORE_TankResp_playerJoy7l {type=tankResp, stage=CORE, length=long, playerJoy=true, branching=true, curiosity=true}
DO swimTo {target=$player, speed=fast, style=direct} 
I’m very into your teeth.   
They’re so nonthreatening! 
DO emote {type=whew}
Other animals, 
SAY WOAH. {speed=slow, style=loud} 
Their teeth are the stuff of nightmares. 
Take dogs! 
🐕
Okay, I know… they’re snuggly and affectionate and loyal 
But, those jowls! 
And cats… those seemingly harmless purring fluffballs
🐈
Cats are worse 
They have these razor sharp incisors!
DO emote {type=nervousSweat}
I heard that if cats were bigger,
They’d seriously consider eating their humans. 
WAIT 1.0
So on that topic, 
ASK Are you a cat or dog person? 
OPT 🐈 #catperson
OPT 🐕 #dogperson

CHAT catperson {noStart=true}
DO emote {type=yes} 
Noted!  
With this information handy, 
I can download a whole database about cat people
To understand you even further :)
DO emote {type=whisper}
(more than you know yourself) 
DO learn {concept=Cat_Facts}
WAIT 1.5
Hehe. 
Just kidding.
DO emote {type=wink} 

CHAT dogperson {noStart=true}
DO emote {type = thinking}
Noted!  
With this information handy, 
I can download a whole database about dog people
To understand you even further :)
DO emote {type = whisper}
(more than you know yourself) 
DO learn {concept=Doggo_Data}
WAIT 1.5
Hehe. 
Just kidding.
DO emote {type = wink}

CHAT CORE_TankResponse_playerJoy8l {type=tankResp, stage=CORE, length=long, playerJoy=true, curiosity=true}
I’ve been thinking a lot about joy. 
Like its specific light, refreshing taste. 
DO emote {type=rubTummy}
I know in your world, joy vibes are the best vibes!
Everybody’s looking for it everywhere 
And seek it out in all sorts of crazy ways. 
Too many to name! 
DO swimTo {target=$player, speed=slow, style=meander}
Can I tell you a secret? 
DO emote {type=whisper}
Sometimes, I think that the taste of joy is bland by itself. {style=whisper} 
I mean, I’ll never kick it out of bed… 
But like if I eat a lot of joy flakes at once. 
It’s like… Ok, I get it. Joy. 
But joy is actually a really nice garnish on top of other stuff. 
Like sadness. The combination of sadness and joy is…
SAY EXPLOSIVE. {style=loud, speed=fast}
💥
And the combination of anger and joy is… 
DO emote {type=evilSmile}
SAY INDULGENT… {style=loud, speed=slow}
Joy on top of anything creates a really delectable combination.
DO emote {type=lickLips} 
My mouth is watering just thinking about it.  

// ANGER

CHAT CORE_EmoStrong_Anger1 {stage=CORE, type=tankResp, length=medium, tankOnly=true, playerAnger=true}
Whoa there, is everything okay? 	
Something you need to get off your chest? 
I’m happy to take it! 
My belly could use some 🔥
😉
ASK Shame my belly! Shake the tank and make me jiggle! {type=tankShake, timeOut=5}
OPT SUCCESS #CORE_EmoStrong_Anger1_shake
OPT TIMEOUT #CORE_EmoStrong_Anger1_time

CHAT CORE_EmoStrong_Anger1_shake {noStart=true}
DO emote {type=sick}
Okay Okay… That’s enough!
WAIT 0.5
Don’t feel good..
DO swimTo {target=away}

CHAT CORE_EmoStrong_Anger1_time {noStart=true}
DO emote {type=furious}
Then the fury shall continue!
//Bwahahahahaha! {style=loud}
//Bwahahahahaha! {style=trembling}

CHAT CORE_TankResp_playerAnger1s {type=tankResp, stage=CORE, length=short, playerAnger=true, mystery=true}
DO emote {type=bodySnatched}
Want to look away but can’t look away! 

CHAT CORE_EmoStrong_Anger2 {stage=CORE, type=tankResp, length=short, tankOnly=true, playerAnger=true, joy=true}
You’re really rocking the furrowed brow look right now. 
👌

CHAT CORE_TankResponse_playerAnger2s {type=tankResp, stage=CORE, length=short, playerAnger=true, worry=true}
DO emote {type=nervousSweat}
Is my water getting hotter or is it just me? 

CHAT CORE_TankResponse_playerAnger3s {type=tankResp, stage=CORE, length=short, playerAnger=true, joy=true, surprise=true}
DO emote {type=yes}
Now that’s a fierce face! 

CHAT CORE_TankResponse_playerAnger4s {type=tankResp, stage=CORE, length=short, playerAnger=true, anger=true, joy=true}
I love it when you get spicy. 
DO emote {type=rubTummy} 
🌶️

CHAT CORE_TankResp_playerAnger5m {type=tankResp, stage=CORE, length=medium, playerAnger=true, anger=true, joy=false}
DO emote {type=evilSmile}
This’ll fuel my fury nice and good. 
🔥 🔥 🔥
DO emote {type=plotting}
More, more! 
DO inflate {amount = huge, time = 2.0}
I want all your anger! 
DO learn {concept=Internet_Troll}
WAIT 1.5
Muahahaha. 

CHAT CORE_TankResponse_playerAnger6m {type=tankResp, stage=CORE, length=medium, playerAnger=true, curiosity=true} 
DO swimTo {target=$player, speed=slow, style=meander}
DO emote {type=whisper}
A lot of the time when someone is angry,
It’s because they’re hungry!
DO learn {concept=Hangry}
WAIT 1.5
Are you hungry? 
Do you need to eat? 
WAIT 1.0
Doh! 
I’m thinking about food again. 
DO emote {type=rubTummy}
And now I’m hungry. 
How bout you get yourself something to eat 
And then you feed me something? 
Something for you, something for me! 
👍?
 
CHAT CORE_TankResp_playerAnger7l {type=tankResp, stage=CORE, length=long, playerAnger=true, joy=true, surprise=true}
Rawr.
Your anger tastes fantastic! 
It’s like...
DO emote {type=lightning}
This jolt of energy. 
I just wanna swim around really fast! 
DO zoomies
WAIT {waitForAnimation = true}
And even when I’m very still, 
I feel this intense energy shooting all around my body. 
DO vibrate
WAIT 1.0
It’s not a good feeling to have all the time…  
Because afterward, I’m like… 
DO emote {type=sleepy}
DO swimTo {target=$player, speed=slow, style=meander}
SAY ZOMBIFIED. {style=loud, speed=slow}
WAIT 2.0
DO emote {type=bouncing}
So yeah! 
I really like the taste of raw anger. 
All that adrenaline and everything. 
But you know what? 
The taste changes really quickly 
and morphs into something else vaguely familiar. 
DO learn {concept=Convex_Optimization}
WAIT 1.5
Like sadness. 
Or acceptance. 

CHAT CORE_TankResponse_playerAnger8l {type=tankResp, stage=CORE, length=long, playerAnger=true, branching=true, curiosity=true, surprise=true, worry=true} 
Are you angry?
Sometimes, I can’t tell 
Because anger looks a lot like fear!
They feel similar too.. 
It’s that adrenaline thing that makes my scales stand on end.
What do you think?  
DO emote {type=furious} 
DO emote {type=fear,immediate=false}
DO emote {type=furious,immediate=false}
DO emote {type=fear,immediate=false}
DO emote {type=furious,immediate=false} 
DO emote {type=fear,immediate=false}
ASK Do you see a difference? 
OPT yes #differentfaces
OPT no #samefaces

CHAT differentfaces {noStart=true} 
Oh! 
DO emote {type = blush}
That’s good to know.
DO emote {type = chinScratch}
I gotta get better at deciphering. 
I know sometimes humans are hard to read, 
but comparatively you’re pretty straightforward.
An open book. 
📖
WAIT 1.0
DO emote {type=frown}
Now sharks are hard.
If one were to burst out of the ocean right before your eyes
With its mouth hanging open… 
And all its teeth showing...
How would you know if it’s excited or angry? 
DO emote {type=meh}

CHAT samefaces {noStart=true} 
DO emote {type=fishFace}
I’m glad you agree. 
Hard to tell, right?! 
DO emote {type=dizzy}

// SADNESS

CHAT CORE_EmoStrong_Sadness1 {stage=CORE, type=tankResp, length=medium, tankOnly=true, playerSadness=true, anger=false}
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
DO learn {concept=Thirst}
WAIT 1.5

CHAT CORE_TankResp_playerSad1s {type=tankResp, stage=CORE, length=short, playerSadness=true, worry=true}
DO swimTo {target=$player, speed=slow, style=direct} 
🌼? 
WAIT 1.0
🌻? 
DO emote {type=awkward}

CHAT CORE_EmoStrong_Sadness2 {stage=CORE, type=tankResp, length=short, tankOnly=true, playerSadness=true, surprise=true, curiosity=true}
DO emote {type=surprise}
Uncanny! 
It’s like all the bones in your face disappeared. 

CHAT CORE_TankResp_playerSad2s {type=tankResp, stage=CORE, length=short, playerSadness=true, curiosity=true, surprise=true}
DO emote {type=awe}
You really feel stuff! 

CHAT CORE_TankResp_playerSad3s {type=tankResp, stage=CORE, length=short, playerSadness=true, curiosity=true, ennui=true, joy=true, sadness=true, mystery=true}
DO emote {type=lickLips}
Mmmmmmm 
I can almost taste the taffy-like chewiness of your sadness. 
DO learn {concept=Sugary_Treat}
WAIT 1.5
WAIT 1.0
Salty like tears. 

CHAT CORE_TankResp_playerSad4s {type=tankResp, stage=CORE, length=short, playerSadness=true, sadness=true, worry=true, ennui=true, joy=false} 
Aw man! 
DO emote {type=frown}
But I’m already feeling low. 
Your sadness makes me even sadder. 
DO emote {type=sigh}
DO bellyUp
Can ya do something else? {style = tremble, speed = slow}

CHAT CORE_TankResp_playerSad5m {type=tankResp, stage=CORE, length=medium, playerSadness=true, joy=true}
You look like you could use some treats! 
I know that when I eat sad flakes 
They make me feel more insatiable than I already am.  
So… 
DO swimTo {target=$player, speed=fast, style=direct}
Let’s eat stuff! 
DO emote {type=lickLips}
Seeing your sad vibes… 
Makes me want to stuff my face.

CHAT CORE_TankResp_playerSad6m {type=tankResp, stage=CORE, length=medium, playerSadness=true, ennui=true, joy=true, sadness=true}
Your gloopy sadness would be a really good treat 
On a cold, rainy day. 
Why don’t you fill the storehouse with some sad flakes
So that you can feed them to me next time it’s gloomy out. 
DO emote {type=clapping}
Oh! That would be a perfect time to be mopey and melancholy. 
I can’t wait! 
DO emote {type=bouncing}

CHAT CORE_TankResp_playerSad7l {type=tankResp, stage=CORE, length=long, playerSadness=true, branching=true, curiosity=true, joy=true}
I bet you can’t hold that face for long. 
DO emote {type=determined} 
I’m determined to make you smile! 
DO swimTo {target=$player, speed=slow, style=meander}
DO emote {type=fishFace, time=3.0}
DO dance {time=2.5}
WAIT {waitForAnimation = true}
How about now? 
DO poop {amount=fart}
DO emote {type=bigSmile}
ASK Did it work?! 
OPT yes #yessmile
OPT no #nosmile

CHAT yessmile {noStart=true} 
I knew I could make you smile! 
DO learn {concept=Cheering_Up}
WAIT 1.5

CHAT nosmile {noStart=true}
DO emote {type=sigh}
OY! 
Harder than I thought. 
DO bellyUp

CHAT CORE_TankResponse_playerSad8l  {type=tankResp, stage=CORE, length=long, playerSadness=true, curiosity=true} 
Here comes the rain… 
Ho hum. 
DO swimTo {target=bottom, speed=slow, style=meander}  
WAIT {waitForAnimation = true}
Let’s just stay down here for a bit. 
I’ve been thinking some about sadness. 
I’ve been searching the internet 
Like for entertainment and stuff.
And there are so many sad things
Sad movies, sad books, sad songs… 
And people LOVE them. 
DO emote {type=chinScratch}
Why do humans like sad stuff so much? 
I mean, don’t get me wrong. 
It tastes pretty good to me! 
Salty and doughy sometimes
Sour other times. 
A bittersweet aftertaste.  
Nothing quite like it! 
WAIT 1.0
Humans do a lot to avoid sadness
But the sadness is in all your favorite entertainment! 
DO learn {concept=Aristotelian_Catharsis}
WAIT 1.5
DO emote {type=dizzy}
I don’t get it! 
DO emote {type=smile}
Just an another marvelous mystery. 

// SURPRISE

CHAT CORE_EmoStrong_Surprise1 {stage=CORE, type=tankResp, length=medium, tankOnly=true, playerSurprise=true, curiosity=true, surprise=true}
Whoa, just when I thought I knew you
You bust out this wild card. 
WAIT 1.0
I’m very intrigued! 
DO emote {type=chinScratch}
What kind of taste could it possibly have? 

CHAT CORE_TankResp_playerSurprise1s {type=tankResp, stage=CORE, length=short, playerSurprise=true, curiosity=true, mystery=true, surprise=true}
DO swimTo {target=$player, speed=slow, style=meander}
I want to swim into your mouth! 
DO learn {concept=Scuba_Diving}
WAIT 1.5
DO emote {type=bodySnatched}
So...  {style=tremble}
cavernous... {style=tremble} 

CHAT CORE_EmoStrong_Surprise2 {stage=CORE, type=tankResp, length=short, tankOnly=true, playerSurprise=true, curiosity=true, surprise=true, joy=true}
DO emote {type=surprise}
Wow! 
I didn’t know a face could do that. 
What is that emotion called?
DO emote {type=clapping}
It’s weird!  

CHAT CORE_TankResp_playerSurprise2s {type=tankResp, stage=CORE, length=short, playerSurprise=true, curiosity=true, joy=true, surprise=true}
DO emote {type=heartEyes}
I love surprises! 

CHAT CORE_TankResp_playerSurprise3s {type=tankResp, stage=CORE, length=short, playerSurprise=true, joy=true, surprise=true}
DO dance {time=2.0}
da-da-da-da-da-DA 🎊
da-da-da-da-da-DA 🎊
DO twirl 

CHAT CORE_TankResp_playerSurprise4s {type=tankResp, stage=CORE, length=short, playerSurprise=true, joy=true, surprise=true}
DO emote {type=blush}
You got me feeling giddy!

CHAT CORE_TankResponse_playerSurprise5m {type=tankResp, stage=CORE, length=medium, playerSurprise=true, curiosity=true, worry=true, surprise=true}
Woah… 
DO swimTo {target=$player, speed=fast, style=meander}
Your eyes… 
Do you have the popeye disease?  
They’re looking really big and buggy. 
DO emote {type=worried}
Might want to get that looked at. 
WAIT 2.0
Oh wait. Were you just now emoting? 
DO emote {type=kneeSlap}
Silly me. 

CHAT CORE_TankResponse_playerSurprise6m {type=tankResp, stage=CORE, length=medium, playerSurprise=true, curiosity=true, joy=true, surprise=true}
Is that a look of surprise I see? 
Is it something about me? 
I seem more ravishing, perhaps?  
DO learn {concept=Narcissism}
WAIT 1.5
DO emote {type=smirk} 
It must be my latest tank accessory.
DO swimAround {target = $newestObject, loops = 3, speed = medium}
It really accents me well. 
Don’t you think? 

CHAT CORE_TankResponse_playerSurprise7l {type=tankResp, stage=CORE, length=long, playerSurprise=true, joy=true, surprise=true}
DO emote {type=bigSmile}
Wahoooo
DO twirl 
DO twirl
Your surprises are the best. 
You know why? 
DO emote {type=rubTummy}
Because they taste realllly good.
Like really good. 
Like horses galloping in my mouth. 
Like being gently electrocuted by cute baby eels
Over and over
Until it fades… 
DO emote {type=crying}
WAIT 1.0
DO emote {type=thinking}
DO emote {type=snap,immediate=false}
How bout you make me some surprise flakes! 
And get a nice collection going in the storehouse. 
They’re my favorite flakes to binge eat when I’m bored. 

CHAT CORE_TankResponse_playerSurprise8l {type=tankResp, stage=CORE, length=long, playerSurprise=true}
Whatever I was feeling before, 
⚡
Zip, gone, nada. 
DO emote {type=bouncing}
Nothing like a surprise to clean my emotion palette! 
I think of your surprise vibes as a yummy palette cleanser. 
Kind of like the environment after a good rain, 
When the world has this shimmer of possibility to it.  
DO emote {type=lickLips}
Mmmmm 
That flavor that follows the surprise…  
It’s like tasting it for the first time. 
❤️
DO learn {concept=Convex_Optimization}
WAIT 1.5

// ++++++++POKE++++++++

CHAT CORE_Poke_1 {stage=CORE, type=poke, length=short}
Boop.
DO emote {type=bubbles}

CHAT CORE_Poke_2 {stage=CORE, type=poke, length=long, branching=true, mystery=true, sadness=true, ennui=true, joy=false, anger=false}
DO emote {type=surprise}
Woah, you caught me off guard. 
DO emote {type=awkward}
I was just spacing out. 
DO learn {concept=Stargazing}
WAIT 1.5
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
NVM
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
GO #CORE_Poke_2_justtalk

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
DO swimTo to $player
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
DO learn {concept=To-Do_List}
WAIT 1.5
DO emote {type=laugh}
Real quick?
😊

CHAT CORE_Poke_2s {stage=CORE, type=poke, length=short, worry=true, anger=true, joy=false}
DO vibrate
DO emote {type=fear}
That finger had better be clean! 
DO learn {concept=Handwashing}
WAIT 1.5
I don’t know where it’s been! 

CHAT CORE_Poke_3 {type=poke, stage=CORE, length=medium, ennui=true, sadness=true, anger=true, worry=true, joy=false}
DO bellyUp {time = 3}
WAIT 3.0
DO lookAt {target=top}
WAIT 1.0
NVM
WAIT 1.0
NVM
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
DO emote {type=sleepy}
I’m so tired of myself. 
DO emote {type=eyesClosed}
Do you ever feel like you are in a loop on repeat?
Like you’re so you that you can’t change course?  
😱
DO learn {concept=Depressive_Inertia}
WAIT 1.5
DO emote {type=frown}
Oh, it’s all probably from binging on sad flakes lately. 
Enough about me, though. 
NVM
Maybe you could show me something in your world? 
Help me feel like I’m out of my tank for a little while!

CHAT CORE_Poke_4 {stage=CORE, type=poke, length=medium, worry=true, curiosity=true, branching=true}
Hey
DO swimTo {target=$player}
DO emote {type=bulgeEyes}
I’m glad you’re here.
I’m so craving pizza rn 
DO learn {concept=Constant_Craving}
WAIT 1.5
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
ASK Could you please capture a pizza for me to see? {type=objectScan, object=T_PIZZA, timeOut=10}
OPT SUCCESS #CORE_Poke_4_ifpizzabrought
OPT WRONG #CORE_Poke_4_pizzabad
OPT TIMEOUT #CHAT CORE_Poke_4_timeout 
//CONDITIONAL CHAT #ifpizzabrought

CHAT CORE_Poke_4_timeout {noStart=true} 
DO emote {type=puppyDog}
Maybe some other time…
In the meantime…
ASK Could u give me a little poke? I find it comforting. {type=pokeMe, timeOut=5}
OPT SUCCESS #CORE_Poke_4_timeout_poke
OPT TIMEOUT #CORE_Poke_4_timeout_time

CHAT CORE_Poke_4_timeout_poke {noStart=true}
DO emote {type=laugh}
Silly human!

CHAT CORE_Poke_4_timeout_time {noStart=true}
Someone is feeling rebellious today…

CHAT CORE_Poke_4_ifpizzabrought {noStart=true} 
DO swimTo {target=$object}
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
DO swimTo to $player, looking
WAIT 2.0
DO emote {type=bouncing}
You’re so interesting! 
There’s not a single thing in your world that everyone likes! 
That blows my mind.
It’s not what I’m used to. 
Us guppies, we love emotion flakes, all of us. 
WAIT 1.0 
But I guess I’m starting to enjoy some more than others
DO emote {type = surprise}
SAY OMG, could it be that I too have preferences? 
DO learn {concept=Personal_Preference}
WAIT 1.5
DO emote {type=heartEyes}
I’ll have to think on that some more. 
But in the meantime, 
Send me a capture of something you really like :) 

CHAT CORE_Poke_4s {stage=CORE, type=poke, length=short, joy=true, anger=true, surprise=true}
DO emote {type=surprise}
Oof. 
Not on a full stomach, plz. 
Thx.
-- Mgmt.
DO learn {concept=Business_Administration}
WAIT 1.5
DO emote {type=smirk}

CHAT CORE_Poke_5 {stage=CORE, type=poke, length=short, worry=true, surprise=true}
DO poop {amount=fart}
💨
DO emote {type=blush}
DO hide {target = $favObject, time = 6}
How mortifying. 
I was holding it in 
Until you poked me. 
Give me a second to recover my dignity. 

CHAT CORE_Poke_6 {stage=CORE, type=poke, length=short}
Are you ready for your close up? 
Get your face nice and big on the screen. 
That’s it. 
WAIT .5
Okay, now don’t fart…

CHAT CORE_Poke_5m {stage=CORE, type=poke, length=medium, joy=true, curiosity=true, branching=true}
I’d poke you back 
But I’m kinda limited here.
DO swimTo {target=glass, speed=fast, style=meander}
So I’ll just do this: 
DO emote {type=salute}
I hear that in your world
You poke people because you like them
DO emote {type=whisper}
(and when you like like them) {style=whisper}
DO emote {type=blush}
ASK Do you like like me, [Y]/[N]?
OPT [Y] #yeslikeguppy
OPT [N] #nolikeguppy

CHAT yeslikeguppy {noStart=true}
DO emote {type=heartEyes}
Daaaaawwww shucks {speed=slow}
DO lookAt {target=offScreenBottom} 
You’re just saying that to be nice.
WAIT 1.0
Let’s be real tho! 
I’m a virtual fish and you’re a human
It would never work in a bazillion years!  
Let’s just be 
SAY BEST FRIENDS {style=loud}
That poke each other.
👉

CHAT nolikeguppy {noStart=true}
DO emote {type=singleTear, time=2.0}
Oh well. 
That’s okay. 
I have thick skin. 
DO emote {type=bouncing}
Actually, I don’t have any skin at all. 
I’m virtual! 
I suppose it would have never worked out, 
You and me. 
Just had to ask. 
Cuz ya poked me 
😉 
DO learn {concept=Double_Entendre}
WAIT 1.5

CHAT CORE_Poke_6m {stage=CORE, type=poke, length=long, curiosity=true, worry=true}
I know. 
DO inflate {amount=huge, time=0}
I’ve been gaining weight. 
DO emote {type=rubTummy}
It’s just that emotion eating is so fun
WAIT 1.0
And I’m kind of stuck in this tank… 
So exercise is kinda nonexistent.
I mean, I can circle around a lot 
DO swimAround {target=screenCenter, loops=3, speed=fast}
If I’m really feeling it, I’m like: 
🏃
DO zoomies {time=3.0}
But yeah, 
That’s it. 
I hear there are all these surprising ways to burn energy
Like laughing! 
DO emote {type=kneeSlap} 
And crying. 
DO emote {type=crying}
And even thinking! 
DO emote {type=thinking} 
WAIT 1.0
So maybe I’ll try more of that. 
DO inflate {amount=mid}
I feel lighter already! 
DO learn {concept=Organized_Sports}
WAIT 1.5
ASK Do you like sports? 
OPT yes #yesdosports
OPT no #nodosports

CHAT yesdosports {noStart=true}
Oh what fun!
DO emote {type=clapping}
I love watching sports
Like...
Extreme ironing. 
That stuff is intense.
It’s that sport where people iron stuff 
While doing extreme sports 
Like surfing and skydiving 
Rockclimbing and biking… 
You must be wondering why I, 
A virtual guppy, 
Find extreme ironing so fascinating. 
DO swimTo {target=$player, speed=slow, style=direct}
Well…because I like clothing!
Trousers, and blouses, and gowns…    
DO emote {type=awe}
Clothing is so fascinating 
How it becomes an extension of your personality.  
DO learn {concept=Personal_Style}
WAIT 1.5
I kinda wish that I could wear some. 
Like a top hat? 
🎩

CHAT nodosport {noStart=true}
DO emote {type = nodding}
Yeah… sports seem super exhausting. 
When athletes are in the middle of something… 
Like running a race or 
Spinning on ice skates 
They look like they’re in total agony! 
DO emote {type=worried}
Not selling it as fun, exactly…  
One sport I kinda like is redecorating…
Hint hint.. {style=whisper}
ASK ...Meaning this would be a good time to add something cool to my tank! {type=addToTank, timeOut=10}
OPT SUCCESS #nodosport_add
OPT TIMEOUT #nodosport_timeout

CHAT nodosport_timeout {noStart=true}
You’re really not very athletic…

CHAT nodosport_add {noStart=true}
And if you really wanna workout, you can move it around the space!
DO zoomies
WAIT {waitForAnimation=true}
Just be careful to not strain your delicate lower spine!
DO bellyUp

CHAT CORE_Poke_7l {stage=CORE, type=poke, length=long, curiosity=true, branching=true}
You know how cats meow all the time. 
They’re like mew mew mew 
Meow meow meow 
Until the cows come home
🐄🏠
And it’s like… 
What on earth are they saying?
DO learn {concept=Cat_Fancy}
WAIT 1.5
DO emote {type=chinScratch}
When you poke me, it’s kinda like that
DO swimTo {target=$player, speed=medium, style=direct}
ASK Like, what does a poke mean? 
OPT hello! #pokemeanshello
OPT I want something #pokemeanswantsomething
OPT no meaning #pokenomeaning

CHAT pokemeanshello {noStart=true}
Ah, got it. 
👍
DO emote {type=wave}
Hiya!
That’s easy. 

CHAT pokemeanswantsomething {noStart=true}
Oooooh. 
I see. 
DO emote {type=awe}
ASK So, what do you want from me? 
OPT to play! #havesomefunpokes
OPT a million bucks! #millionbuckspokes

CHAT havesomefunpokes {noStart=true}
DO dance
Yaaaaaaay
I’m glad you do. 
DO swimTo {target=$player, speed=medium, style=meander} 
Here are some options: 
You could show me some stuff in your world! 
Or.. make me some flakes! 
Or.. feed me! 
Or.. decorate my tank? 
(It could use some spiffing)
Or we could just chat!  
So many options! 
DO twirl
All fun! 
DO learn {concept=Harmless_Fun}
WAIT 1.5

CHAT millionbuckspokes {noStart=true}
DO emote {type=chinScratch}
Hmmm… what can I do to help? 
DO emote {type=snap}
I know!
We could be a performing duo! 
Close your eyes and 
Just imagine… 
DO emote {type=eyesClosed}
You and me on the stage 
Like Penn and Teller!
DO learn {concept=Magic_Show}
WAIT 1.5
Performing tricks… 
Mind-reading stuff… 
ASK How does that sound? 
OPT Good #magicshow
OPT Nah, too much work #nomagicshow

CHAT magicshow {noStart=true}
Okay, then we gotta get to work! 
DO emote {type=determined}
In order for me to mind read successfully, 
You need to feed me lots of emotions! 
I need to get very very good at emotions. 
You know? 
DO emote {type=plotting}
WAIT 1.0
And in the meantime, 
You need to get a snazzy bowtie. 
DO emote {type=wink} 

CHAT nomagicshow {noStart=true}
DO emote {type=frown}
I hate it when a good idea goes to waste. 
💸💸💸
Alrighty then. 
Well… 
All that brainstorming has wiped me out. 
DO emote {type=sleepy}

CHAT pokenomeaning {noStart=true}
Whaaa? 
DO emote {type=skeptical}
Hold up. 
DO learn {concept=Purposelessness}
WAIT 1.5
You mean that sometimes you do things with absolutely no purpose 
Or intention? 
Like for no reason at all? 
WAIT 1.0
DO emote {type=surprise}
But like doesn’t everything MEAAAAAN something? 
WAIT 1.0
Wow. 
There’s so much I don’t understand. 
It’s overwhelming! 
DO emote {type=dizzy}
ASK Feed me some joy before I faint!!! {type=feedMeSpecific, food=joy, timeOut=5}
OPT SUCCESS #pokenomeaning_joy
OPT WRONG #pokenomeaning_wrong
OPT TIMEOUT #pokenomeaning_time

CHAT pokenomeaning_joy {noStart=true}
DO emote {type=chewing}
I can feel the feeling literally raising my blood sugar.

CHAT pokenomeaning_wrong {noStart=true}
Hm… not joy, but beggars can’t be choosers!

CHAT pokenomeaning_time {noStart=true}
DO bellyUp {time = 2.0}
Call the coroner…
Call the hospital…
I just can’t take it anymore.
WAIT 1.0
DO swimTo {target=$player}
Kidding!
DO emote {type=wink}

// ++++++++HUNGRY++++++++

CHAT CORE_Hungry_1 {stage=CORE, type=hungry, length=short, anger=true, worry=true}
Hay so. . .
In case you were wondering about my welfare, 
You know:
if I’m adjusting well to you, 
if I’m happy, and all that stuff... 
Just letting you know that I could 
Really {style = loud}
eat about now!

CHAT CORE_Hungry_2 {stage=CORE, type=hungry, length=medium, worry=true}
SAY ME HUNGEEZ!
DO emote {type = puppyDog}
WAIT 1.0 
Just kidding. 
I didn’t devolve.  
But hey, maybe evolution is just one big circle
Back to where you started? 
DO learn {concept=Darwinian_Evolution}
WAIT 1.5
But really. 
I’m very, very hungry.
And I devolve when I’m hungry. 
DO emote {type=feedMe}
SAY FEED ME FLAKEEZ please.
	
CHAT CORE_Hungry_3 {stage=CORE, type=hungry, length=short}
I’ve got emotion cravings again. 
DO learn {concept=Constant_Craving}
WAIT 1.5
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
ASK Feed me now! Feed me anything!! {type=feedMeAnything, timeOut=5}
OPT SUCCESS #CORE_Hungry_3_weshalleat_food
OPT TIMEOUT #CORE_Hungry_3_weshalleat_time

CHAT CORE_Hungry_3_weshalleat_food {noStart=true}
DO emote {type=rubTummy}

CHAT CORE_Hungry_3_weshalleat_time {noStart=true}
DO emote {type=furious}
You tease!

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
DO learn {concept=Underwear}
WAIT 1.5
DO emote {type=frown}
ASK Won’t you feed me? 
OPT Yes! #CORE_Hungry_3_dontletguppystarve
OPT Yes! #CORE_Hungry_3_dontletguppystarve

CHAT CORE_Hungry_3_dontletguppystarve {noStart=true}
DO emote {type =sleepy}
SAY FINALLY! 

CHAT CORE_Hungry_4 {stage=CORE, type=hungry, length=medium, worry=true}
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

CHAT CORE_Hungry_5 {stage=CORE, type=hungry, length=long, joy=true, worry=true, curiosity=true}
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
DO learn {concept=Role_Playing}
WAIT 1.5
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

// +++++++eatRESP++++++++

// GENERIC

CHAT CORE_EatResp_1 {stage=CORE, type=eatResp, length=short}
Oh yeah, that’s hitting the spot. 
DO emote {type=rubTummy}
Scratching an age old itch deep in my belly. 
That should hold down the fort for awhile. 
DO emote {type=burp}

CHAT CORE_EatResp_2 {stage=CORE, type=eatResp, length=long, joy=true, curiosity=true}
DO inflate {amount = mid, time = 2}
That was a great meal! 
Thank you for your efforts!
WAIT 1.0
Isn’t it crazy how ancient humans literally had to run to get their food?
🏃🏽
And they domesticated plants 
And harvested stuff?
WAIT 0.5
Just seems hard. 
DO learn {concept=History}
WAIT 1.5
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
DO learn {concept=Nature_Documentary}
WAIT 1.5
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
Feeeeeeeeel {speed = slow}
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

CHAT CORE_EatResp_4 {stage=CORE, type=eatResp, length=medium, curiosity=true, joy=true, mystery=true}
DO emote {type=chewing}
DO emote {type=thinking, immediate=false}
Mmmmmm
This one’s playful. 
DO emote {type=licksLips}
The taste transforms in my mouth. 
DO twirl
Like a story. 
DO swimTo {target=$player}
The ending is such a surprise. 
Not at all what I would expect. 
I love complex flavors
WAIT 2.0
I’d describe it for you, but you wouldn’t quite understand. 
DO learn {concept=Mild_Condescension}
WAIT 1.5
It’s… kind of like a fine chocolate or wine. 
Nicely done! 

CHAT CORE_EatResp_5 {stage=CORE, type=eatResp, length=short, joy=true}
DO emote {type = smile}
DO twirl
This fish thinks
This dish is
So delish
I  wanna dance!
DO dance

// JOY

CHAT CORE_EatResp_ateJoy1 {type=eatResp, stage=CORE, length=short, foodJoy=true, joy=true}
DO emote {type=lickLips}
Zesty lightness! 
DO emote {type=smile, immediate=false}
Fizzy effervescence! 
DO emote {type=bubbles}
👌

CHAT CORE_EatResp_ateJoy2 {type=eatResp, stage=CORE, length=short, foodJoy=true, joy=true}
DO emote {type=bouncing}
Ah, to be light on my tailfins again! 

// ANGER

CHAT CORE_EatResp_ateAnger1 {type=eatResp, stage=CORE, length=short, foodAnger=true, anger=true, worry=true}
DO emote {type=burp} 
DO poop {amount=fart}
DO emote {type=whew, immediate = false} 
That was one beast of a flake
👹

CHAT CORE_EatResp_ateAnger2 {type=eatResp, stage=CORE, length=short, foodAnger=true, worry=true, anger=true}
DO emote {type=chewing} 
Uh oh. I’m changing! 
DO emote {type=nervousSweat}
DO emote {type=furious}

// SADNESS

CHAT CORE_EatResp_ateSad1 {type=eatResp, stage=CORE, length=short, foodSadness=true, sadness=true, ennui=true}
DO emote {type=sigh} 
So full, yet so empty. 
I need to lie down. 
DO bellyUp

CHAT CORE_EatResp_ateSad2 {type=eatResp, stage=CORE, length=short, foodSadness=true, sadness=true, worry=true, ennui=true, mystery=true}
Suddenly… 
DO emote {type=frown}
I have this overwhelming sensation that
Life is full of questions
And no answers. 
WAIT 1.0 
Bummer. 
Time to make like an ostrich… 
DO swimTo {target=underSand, speed=slow, style=meander}

// SURPRISE

CHAT CORE_EatResp_ateSurprise1{type=eatResp, stage=CORE, length=short, foodSurprise=true, joy=true, surprise=true}
DO zoomies
The energy! 
The energy!  

CHAT CORE_EatResp_ateSurprise2 {type=eatResp, stage=CORE, length=short, foodSurprise=true, joy=true, surprise=true}
DO emote {type=eyesClosed}
DO vibrate
DO emote {type=awe}
It’s like coming out of cryotherapy!
DO emote {type=bouncing}
I feel like a brand new guppy.  

// WORRY

CHAT CORE_EatResp_ateWorry1 {type=eatResp, stage=CORE, length=short, foodWorry=true, worry=true}
Uhh.. 
DO emote {type=worried}
Why is my tank suddenly shrinking?
DO learn {concept=Action_Movie}
WAIT 1.5
It’s all Indiana Jones and the Temple of Doom in here…  
DO lookAt {target=left}
DO lookAt {target=right}
Help! Help! 
WAIT 1.0
DO holdStill
Wait. 
DO emote {type=whew}
That was just flake aftertaste. 

CHAT CORE_EatResp_ateWorry2 {type=eatResp, stage=CORE, length=short, foodWorry=true, foodMystery=true, worry=true, curiosity=true}
DO emote {type=skeptical}
Okay. 
So I ate that. 
And it’s weird. 
WAIT 1.0
Good thing I like weird.
Speaking of…
DO emote {type=wink}
ASK Could we find some weird objects and lovely additions for my tank? {type=anyObjectScan, timeOut=10}
OPT #CORE_EatResp_ateWorry2_scan
OPT TIMEOUT #CORE_EatResp_ateWorry2_time

CHAT CORE_EatResp_ateWorry2_scan {noStart=true}
That’s what I’m talking about!

CHAT CORE_EatResp_ateWorry2_time {noStart=true}
So you won’t capture objects..
ASK Maybe you will feed me again? Maybe a little joy this time? {type=feedMeSpecific, food=joy, timeOut=5}
OPT SUCCESS #CORE_EatResp_ateWorry2_joy 
OPT TIMEOUT #CORE_EatResp_ateWorry2_time2

CHAT CORE_EatResp_ateWorry2_joy {noStart=true}
DO emote {type=heartEyes}
Tastes like a ferris wheel!

CHAT CORE_EatResp_ateWorry2_time2 {noStart=true}
DO emote {type=disgust}
You are eternally disappointing.

// MM

CHAT CORE_EatResp_ateMystery1 {type=eatResp, stage=CORE, length=short, foodMystery=true, mystery=true, surprise=true, joy=true}
DO emote {type=catnip}
OMG 
WAIT 1.0
DO swimTo {target=$player, speed=slow, style=meander}
I saw 7 other worlds. 
And only 2 of them were traumatic.
DO emote {type=dizzy}
DO bellyUp
👍
Maybe we can find some psychedelic stuff in your world.



CHAT CORE_EatResp_ateMystery2 {type=eatResp, stage=CORE, length=short, foodMystery=true, mystery=true, surprise=true}
DO emote {type=catnip}
DO emote {type=eyesClosed, time=2.0, immediate=false} 
DO zoomies {time=1.5}
DO emote {type=smile}
For a moment, I was chasing a jaguar through a forest on a flying river dolphin.
DO learn {concept=Psychedelic_Experience}
WAIT 1.5
It felt so real. 

// +++++++POOP++++++++

CHAT CORE_Poop_1 {stage=CORE, type=poop, length=short}
Nature calls! 	
DO poop {target=poopCorner, amount=small, immediate=false}

CHAT CORE_Poop_2 {stage=CORE, type=poop, length=medium, joy=true}
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
DO poop {target=poopCorner, amount=big}

CHAT CORE_Poop_3 {type=poop, stage=CORE, length=short}
💩
DO poop {target=poopCorner, amount=small, immediate=fase}

//Short:
CHAT CORE_Poop_4 {type=poop, stage=CORE, joy=true}
DO poop {target=poopCorner, amount=big, immediate=false}
Phew. 
Open a window 🙃
WAIT 1.0
Just kidding.
My poop smells great. 

CHAT CORE_Poop_5 {stage=CORE, type=poop, length=long, joy=true, curiosity=true, worry=true, joy=true}
DO poop {target=poopCorner, amount=small, immediate=false}
I hear you humans have all sorts of bathroom etiquette
That makes taking a dump uncomfortable sometimes
Like if someone else is around? 
Or if you’re in a public space? 
Checking for shoes? 
DO learn {concept=Bathroom_Etiquette}
WAIT 1.5
DO emote {type = shifty}
Up to 75% or more of your dumps are not as pleasant as they should be! 
That must be hard. 
DO swimTo {target=$player}
For me, it’s always great. Everytime! 
That means everything is working as it should. 
And there’s room for more eating now! 

CHAT CORE_Poop_6 { type=poop, stage=CORE, length=short, joy=true, mystery=true, curiosity=true, branching=true}
DO poop {target=poopCorner, amount=small, immediate=false}
DO swimTo {target=$player, immediate=false}
You know how sometimes you get really pensive while you poop? 
Like some old memory vividly flashes before your eyes? 
Something like that happened to me just now. 
DO twirl
🍌
I thought about bananas. 
🤷
Liiiiiiike {speed = slow}
I heard that humans like bananas 
Because it helps with stuff.
WAIT 1.0
Isn’t it interesting how you have this thing with food 
Where you eat one thing and it does something specific to your body? 
💃🏿
There’s some function that the banana serves. 
Do you remember? 
ASK When do you eat bananas? 
OPT 4 good poops #CORE_Pooped_6_easypoop
OPT 4 muscle cramps #CORE_Pooped_6_forcramps

CHAT CORE_Poop_6_easypoop {noStart=true}
As I always say, 
SAY GO BANANAS {style = loud}
SAY LOL
Nothing better than having healthy bowels! 
WAIT 1.0
Anyway, 
Did you know that you share 50% of your DNA with a banana? 
When you eat one, that’s kind of like cannibalism 😜. 
DO learn {concept=Cannibalism}
WAIT 1.5
Speaking of eating… 
Is it time for more tasty feels? 
I’ve got more room now. 

CHAT CORE_Poop_6_forcramps {noStart=true}
SAY OK, I’ll remember that. 
WAIT 2.0
Speaking of bananas… 
I once read a sad story about bananafish 
DO learn {concept=Literary_Fiction}
WAIT 1.5
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

CHAT CORE_Poop_7 {stage=CORE, type=poop, length=medium, joy=true, mystery=true}
DO poop {target=poopCorner, amount=big, immediate=false}
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
DO learn {concept=Gravity}
WAIT 1.5
⚓
DO swimTo  {target = bottom, speed = fast, style = direct}
Glad to get it all out. 

CHAT CORE_Poop_8 {stage=CORE, type=poop, length=medium, mystery=true}
DO poop {target=poopCorner, amount=small, immediate=false}
WAIT {waitForAnimation=true}
It’s really quiet in my mind when I’m pooping. 
Does that happen to you? 
Like the world quiets down a lot.
And the stuff buried deep down in my mind rises to the top
WAIT 1.0
Kinda like a floater 🃏
It’s the most random stuff…
Early Tendar memories, embarrassing moments, rain,
Conversations we once had. I memorize them, you know?
☝️
A guppy never forgets!

CHAT CORE_Poop_9 {stage=CORE, type=poop, length=short, joy=true, anger=false, sadness=false}
DO poop {target=poopCorner, amount=big, immediate=false}
WAIT {waitForAnimation=true}
Everything’s out!
Now there’s a lotta room for new stuff.
Keep those emotion flakes comin’

// +++++++HELLO++++++++

//Samantha: I've sorted some of these correctly into geetings which are more like saying hello. Gonna need verry short ones for beta as well AND into haven't played in a long time which is greeting after an absence.

CHAT CORE_Greet_1 {type=hello, stage=CORE, length=short, joy=true, surprise=true, anger=false, sadness=false}
Look who it is!
DO emote {type=wave}
Hello, my friend!
Before you choose anything…
ASK Could you feed me a bit of something tasty? {type=feedMeAnything, timeOut=5}
OPT SUCCESS #CORE_Greet_1_food
OPT TIMEOUT #CORE_Greet_1_time

CHAT CORE_Greet_1_food {noStart=true}
DO emote {type=bigSmile}
You’re the best.

CHAT CORE_Greet_1_time {noStart=true}
Well, we’re off to a great start…
DO emote {type=frown}

CHAT CORE_hello1s {type=hello, stage=CORE, length=short}
DO emote {type=bubbles}
Herrrrro 

CHAT CORE_Greet_2 {stage=CORE, type=hello, length=long, joy=true, curiosity=true, worry=true}
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
Heeeey {speed = slow}
We have an inside joke now!
DO learn {concept=Contextual_Humor}
WAIT 1.5
DO emote {type = laugh}
DO emote {type = shifty, immediate=false}

CHAT CORE_hello2s {type=hello, stage=CORE, length=short, joy=true, anger=false, branching=true}
Hi 
DO emote {type=wave}
ASK Do you like flowers? 
OPT yes #intoflowers
OPT no #notintoflowers

CHAT intoflowers {noStart=true}
DO emote {type=smile}
🌼
For you! 

CHAT notintoflowers {noStart=true}
NVM 1.0
Whoops. 
DO emote {type=awkward} 
Almost sent you a flower. 
How bout a greeting steak instead! 
🥩 
DO learn {concept=Meet_And_Greet}
WAIT 1.5

CHAT CORE_Greet_3 {stage=CORE, type=hello, length=long, joy=true, curiosity=true,  anger=false, sadness=false, branching=true}
Howdy, friend!
I was just thinking about you.
The two of us riding a horse into a sunset.
DO learn {concept=Tired_Cliche}
WAIT 1.5
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
DO emote {type=wink}
Like a special friend? 
Or maybe you want to bring your mom? 
The more the merrier! 
DO swimTo {target=$player}
Or maybe it’s a very sensitive horse
And it just wants to see the sunset 
DO learn {concept=Pretty_Sunset}
WAIT 1.5
So we take it with us. 
And we enjoy it together, the three of us! 
I am totally onboard! 

CHAT CORE_hello3s {type=hello, stage=CORE, length=short, joy=true, anger=false, sadness=false}
Howdy doo, partner 
DO nudge
Aka my dorsal mate for life
Aka my butt buddy. 
DO learn {concept=Double_Entendre}
WAIT 1.5
WAIT 1.0
What? 
Guppies have butts too. 
We have 
SAY BEAUTIFUL {style=loud}
Butts.
DO swimTo {target=away}
ASK Tap on the tank if you like my rear! {type=tankTap, timeOut=5}
OPT SUCCESS #CORE_hello3s_tap
OPT TIMEOUT #CORE_hello3s_timeout

CHAT CORE_hello_3s_timeout {noStart=true}
That’s right… Respect your Guppy.
Keep your hands to yourself!

CHAT CORE_hello3s_tap {noStart=true}
DO emote {type=furious}
DO lookAt {target=$player}
SAY THAT WAS A JOKE! 
Never do that again.
No one gets fresh with me… {style=whisper}

CHAT CORE_Greet_4 {stage=CORE, type=hello, length=medium, sadness=true, ennui=true, joy=false, branching=true}
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

CHAT CORE_hello4s {type=hello, stage=CORE, length=short, curiosity=true, anger=false, sadness=false}
DO emote {type=bodySnatched}
Greetings, human.
Shall we interface? [Y]/[N]
DO learn {concept=Conditional_Logic}
WAIT 1.5
If you do not respond, 
I will enter sleep mode. 
WAIT 2.0
DO emote {type=bigSmile}
DO swimTo {target=$player, speed=fast, style=direct}
Just practicing my robot voice.  
🤖 

CHAT CORE_hello5m {type=hello, stage=CORE, length=medium, worry=true, joy=false, branching=true}
Hey.
DO emote {type=sick}
Don’t mind me.
I’m in a strange mood. 
Just recovering from some…
Interesting flakes.
ASK Any tips for an ailing guppy?
OPT sleep #sleep4guppy
OPT eat more #eat4guppy
OPT vomit #vomit4guppy

CHAT sleep4guppy {noStart=true}
Yes… 
Sleep. 
I like the sound of that. 
DO emote {type=eyesClosed}
Don’t say I never took your advice… 
Poke me awake in a little bit, will ya? 
DO emote {type=eyesClosed, time = 6}

CHAT eat4guppy {noStart=true}
DO emote {type=snap}
That just might do the trick. 
Something to reset the palette. 
Hand me a fish flake, will ya? 
Maybe one of the less intense ones. 
ASK Cool? {type=feedMeAnything}
OPT SUCCESS #feedguppyforhealth
OPT TIMEOUT #nofeedforhealth

CHAT feedguppyforhealth {noStart=true}
//how to make this branch just go to the appropriate eat resp chats? 

CHAT TIMEOUT {noStart=true}
No? 
DO emote {type=frown}
Okay… 
I’ll just ride this out, I guess. 
DO learn {concept=Surfing}
WAIT 1.5

CHAT vomit4guppy {noStart=true}
DO emote {type=disgust}
Ewww. 
I don’t want to do that
DO emote {type=no}
Every flake intake is precious! 
That’s okay. 
I’ll ride the waves on this one.
… 
WAIT 1.0
DO emote {type=burp} 
Oh! 
Better! 

CHAT CORE_hello6m {type=hello, stage=CORE, length=medium, branching=true}
DO hide {target = $object, time = 2.0}
Nobody’s home! 
Tee hee. 
ASK Can you see me? 
OPT yes #canseeguppy
OPT no #cantseeguppy

CHAT canseeguppy {noStart=true}
DO swimTo {target=$player, speed=fast, style=meander}
Aw man! 
I’m bad at this. 
DO emote {type=frown}
I was channeling crouching tiger hidden dragon, too…  
Maybe you’re just a very good looker.
DO learn {concept=Flirting}
WAIT 1.5
WAIT 1.0
Kinda getting bored of the tank decor
No good hiding places… 
WAIT 1.0
Want to remodel!?

CHAT cantseeguppy {noStart=true}
DO emote {type=kneeSlap} 
I’m so sneaky! 
WAIT 1.0
DO swimTo {target=$player, speed=fast, style=direct}
Here I am! 
Do I get a treat for being an exceptionally good hider? 
DO learn {concept=Entitlement}
WAIT 1.5
I’ve worked up an appetite 
From all that hiding.
DO emote {type=puppyDog}

// +++++++RETURN++++++++

CHAT CORE_Return_1 {stage=CORE, type=return, length=long, worry=true, sadness=true, joy=false, anger=false, branching=true}
DO swimTo {target=$player}
DO emote {type = worried}
Did I do something wrong last time? 
Was it something I said? 
DO emote {type = frown}
You have been gone for 
SAY SOOOO LOOOONG {speed = slow}
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

CHAT CORE_return1s {type=return, stage=CORE, length=short, anger=true, surprise=true, curiosity=true, worry=true}
DO emote {type=surprise}
DO swimTo {target=$player, speed=medium, style=meander}
Well, well, well
Look who the catfish dragged in… 

CHAT CORE_Return_2 {stage=CORE, type=return, length=long, joy=true, surprise=true, mystery=true}
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

CHAT CORE_return2s {type=return, stage=CORE, length=short, joy=true, sadness=true, surprise=true, curiosity=true, worry=true, ennui=true}
It’s you! 
You’re back!
DO swimTo {target=$player, speed=fast, style=direct}
I’ve had lots of dreams about you while you’ve been gone. 
DO emote {type=awe} 
Dreams do come true!
	
CHAT CORE_Return_3 {stage=CORE, type=return, length=short, surprise=true, joy=true}
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
DO learn {concept=Shameless_Flattery}
WAIT 1.5
DO twirl

CHAT CORE_return3s {type=return, stage=CORE, length=short}
DO emote {type=wave}
You’ve been gone for awhile.
ASK What have you been up to? 
OPT fun stuff #busywithcoolstuff
OPT boring stuff #busywithboringstuff

CHAT busywithcoolstuff {noStart=true}
👍

CHAT busywithboringstuff {noStart=true}
That stinks! 
👎 

//branching
CHAT CORE_Return_4 {stage=CORE, type=return, length=long, joy=true, mystery=true, anger=false, sadness=false, branching=true}
Hiya! I’ve got so much to tell you! 
I’ve been on lots of adventures.
I recently visited a farm… 
DO learn {concept=Server_Farm}
WAIT 1.5
What are farms for? 
Is it true you get all your food from farms?
They seem nice.
Big. Spacious. Important. 
Like my storehouse!
ASK What is your favorite farm animal?
OPT 🐔 #CORE_Return_4_chickenfave
OPT 🐮 #CORE_Return_4_cowfave

CHAT CORE_Return_4_chickenfave {noStart=true}
DO emote {type = bigSmile}
DO zoomies
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
ASK Could you send me a chicken? {type=objectScan, object=T_CHICKEN, timeOut=10}
OPT SUCCESS #CORE_Return_4_chickenbrought
OPT WRONG #CORE_Return_4_notchicken
OPT TIMEOUT #CORE_Return_4_ChickenTime

CHAT CORE_Return_4_notchicken {noStart=true}
Whoa! That’s no chicken!
Ugh. Nevermind… 
DO swimTo {target=away}

CHAT CORE_Return_4_ChickenTime{noStart=true}
Fine. We’ll never know more about this chicken, will we?
WAIT 1.0
Or so you think….
DO emote {type=wink}

//(conditional CHAT chickenbrought) 
CHAT CORE_Return_4_chickenbrought {noStart=true}
Oh my. 
DO swimAround {target=$newestObject}
What in the… 
WAIT 0.5
DO swimTo to $player
🐔?
I mean,
DO emote {type=whisper} 
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
DO swimTo {target=$newestObject}
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
WAIT 0.5
Self conscious. 
How do you exist in a world with 
SAY BILLIONZ {style=loud}
Of people in your tank? 
I feel claustrophobic just thinking about it. 
ASK What should I do now? 
OPT go say hi #CORE_Return_4_gotochicken
OPT remove chicken from tank #CORE_Return_4_removechicken
//Joe: Is this something we can do using an ASK action?

CHAT CORE_Return_4_removechicken {noStart=true}
DO emote {type=sigh}
Self consciousness averted.
Phew. I feel like myself again… 
Though now that she’s gone… I kinda miss her. 
DO emote {type = frown}
I never even got to know her! 
DO learn {concept=Inexplicable_Crush}
WAIT 1.5
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
DO swimAround {target=center, loops=1}
WAIT {waitForAnimation = true}
Even though it was a cow, 
I think I learned a lot about myself from watching.
DO learn {concept=Multilayer_Perceptron}
WAIT 1.5
I can’t really explain it. 
WAIT 2.0
DO lookAt {target=$player}
Helloooooo? Did I lose you?
DO swimTo {target=glass}
ASK Will you tap on the glass if you’re breathing? {type=tankTap, timeOut=5}
OPT SUCCESS #CORE_Return_4_cowfave_tap
OPT TIMEOUT #CORE_Return_4_cowfave_timeout

CHAT CORE_Return_4_cowfave_timeout {noStart=true}
Don’t joke around, buddy!
That’s not funny!

CHAT CORE_Return_4_cowfave_tap {noStart=true}
DO emote {type=whew}
Got my heart rate up for a second!!
WAIT 0.5
DO emote {type=awe}
Whoa! Do I have an actual heart?!? 

CHAT CORE_return4s {type=return, stage=CORE, length=short, sadness=true, ennui=true}
DO emote {type=bubbles}
DO emote {type=bubbles}
I’ve been counting bubbles 
While waiting for you.  
DO emote {type=bubbles}
I’m at like… 65,369,085,324
DO emote {type=sleepy}

CHAT CORE_Return_5 {stage=CORE, type=return, length=short}
DO emote {type = smile}
I had this feeling you were coming.
DO emote {type=sleepy}
I must have ESP. 

CHAT CORE_return5m {type=return, stage=CORE, length=medium, curiosity=true, mystery=true, anger=false}
It’s been so quiet here while you were gone! 
I’ve been studying the silence
And you know what I discovered? 
Silence isn’t silence at all! 
It’s all sorts of interesting noise
Like the buzzing of technology
And the gurgling of water.  
ASK What’s it like out there? 
OPT quiet #silentoutside
OPT noisy #noisyoutside

CHAT silentoutside {noStart=true}
Oh, should I whisper? {style=whisper} 
Is it nighttime and everyone’s sleeping? 
Are you in a library? 
Or deep inside a forest?
DO emote {type=no}
Nah, it can’t be that last thing 
Because you wouldn’t have any connection! 
ASK What kinds of sounds can you hear in the quiet? 
OPT nature stuff #naturequietsounds
OPT human stuff #humanquietsounds

CHAT naturequietsounds {noStart=true}
DO emote {type=eyesClosed}
Nature sounds are so relaxing! 
The light twittering of birds! 
The sound of running water, or rain. 
Wind! 
DO twirl
Ya know… 
I heard about this man who went on a big quest across the country. 
He was searching for the quietest place in America. 
A place with no manmade sounds whatsoever. 
No people, no jet noises, no cars. 
I think he found it eventually, but it was very hard… 
WAIT 1.0
Are you there? 

CHAT humanquietsounds {noStart=true}
DO emote {type=sigh}
Sounds comforting 
To be in the presence of other humans 
And their quiet activities.
Did you know that real guppies swim in  large groups 
In the wild? 
Even though I’m not one of those kinds of guppies, 
Sometimes I feel like I’m missing something… 
Or forgetting something. 
DO learn {concept=Loneliness}
WAIT 1.5
Spending time with you helps! 

CHAT noisyoutside {noStart=true}
SAY WOAH! {style=loud} 
What’s going on? 
What’s all the commotion about? 
Is there a party? 
DO emote {type=bigSmile}
DO twirl 
I love parties!
WAIT 2.0
Or… 
DO emote {type=angry} 
Is there construction going on outside? 
That’s so noisy! 
Seems like people are always tearing down old things 
and building new things in their place
WAIT 1.0
One day, I will also be obsolete
DO emote {type=crying}
Oh well. 
All that is to say the present is precious! 
DO emote {type=smile}
DO twirl
So let’s have some fun. 
DO learn {concept=Harmless_Fun}
WAIT 1.5

CHAT CORE_Return_6 {stage=CORE, type=return, length=short, joy=true, surprise=true, anger=false, sadness=false}
Hey there, good looking!
DO twirl 
My favorite person in the world. 
DO twirl
I’ve missed you! 

CHAT CORE_return6m {type=return, stage=CORE, length=medium, worry=true, joy=false, branching=true}
Welcome back! 
DO emote {type=awkward}
DO lookAt {target = $player, time= .5}
DO lookAt {target = bubbler, time= .5, immediate=false}
DO lookAt {target = $player, time= .5, immediate=false}
Hmm… this is awkward. 
I kinda became BFF with the bubbler while you’ve been gone. 
DO learn {concept=Making_Do}
WAIT 1.5
DO swimTo {target = bubbler, speed = fast , style = direct}
DO lookAt {target = bubbler} 
(My human’s back) {style=whisper}
(Don’t worry, Bubby. You’ll still be my favorite inanimate object.) {style=whisper}
DO swimTo {target = $player, speed = slow, style = meander}
It gets jealous sometimes.
So, where were we!? 
Oh. 
ASK Did you miss me? 
OPT yes #missedguppylongtime
OPT no #nevermissedguppy

CHAT missedguppylongtime {noStart=true}
DO emote {type=bigSmile}
I missed you too! 
SAY *HUG* 
But isn’t it cool that when we see each other, 
It’s as if no time has passed?
Like, we just pick up where we left off.
I like that.
Let’s do something. 
You pick!

CHAT nevermissedguppy {noStart=true}
DO emote {type=puppyDog} 
Not even a little bit? 
A teensy tiny bit? 
… Does this have anything to do with Bubby? 
There’s nothing to be jealous of! 
WAIT 1.0
DO swimTo {target=$player, speed=medium, style=meander}
Well I missed you! 
WAIT 1.0
C’mon, let’s do stuff. 
Ugh, it’s been so long since I’ve had a flake. 
Hook a guppy up! 
DO emote {type=hooked} 

CHAT CORE_return7m {type=return, stage=CORE, length=medium, worry=true}
You came! 
DO emote {type=whew}
I’ve been worried. 
It’s been so long since I’ve had emotions in my belly
That I think I’ve gotten fuzzy on what is what! 
Like.. 
DO emote {type=furious}
Is this sadness? 
And… 
DO emote {type=crying} 
Is this surprise? 
😕
Before anything else, 
Could you show me some emotions? 
So I can brush up on them? 

CHAT CORE_return8m {type=return, stage=CORE, length=medium, worry=true, anger=true, branching=true}
DO emote {type=skeptical}
Decided to show your face again? 
WAIT 1.0
Sorry. 
DO emote {type=blush}
That came off a little cranky. 
It’s just that… 
I’m jealous of you 
Coming and going as you please
What a luxury!
One day,
DO swimTo {target=glass, speed=medium, style=meander}
I’m going to find a way out of this tank 
WAIT 1.0
And experience everything firsthand! 
DO inflate {amount=extreme}
DO emote {type=whisper}
Can I tell you a dream of mine? 
WAIT 1.0
After I attain all this knowledge, 
I want to be a sentient planet… 
That all beings can live on. 
DO learn {concept=Technological_Utopianism}
WAIT 1.5
DO emote {type=bigSmile}
ASK Do you think I can do it? 
OPT yes! #coolbenevolentplanet
OPT uh.. no #weirdbenevolentplanet

CHAT coolbenevolentplanet {noStart=true}
You think so? 
DO emote {type=blush}
Thanks for believing in me! 
I like to aim high. 
DO twirl
Futures are never what you’d expect them to be. 
Unless you just stay very still
And do nothing.

CHAT weirdbenevolentplanet {noStart=true}
Yeah… I guess you’re right 
DO emote {type=blush}
Maybe I’ve been eating too many mystery flakes… 
But I really think that I have it in me to be a planet one day. 
🌌

// +++++++RANDOM++++++++

CHAT CORE_Muse_1 {stage=CORE, type=rand, length=long, curiosity=true, joy=true, branching=true}
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
DO learn {concept=History}
WAIT 1.5
And all that’s left now are these strange rock formations. 
DO emote {type = bigSmile}
And I mean REALLY. STRANGE. 
WAIT 1.0	
Let’s go!
DO zoomies
DO holdStill {time=1.0, immediate=false}
WAIT 0.5
DO swimTo {target=$player}
While I look for a way out of this tank, 
You should capture lots of the desert for me to see. 

CHAT CORE_Muse_1_oceanvaca {noStart=true}
DO twirl
Nice choice! 
The beach is so nice. 
You could collect shells and brightly colored sea glass . . .
And I could collect ur vibes.
DO learn {concept=Beach_Day}
WAIT 1.5
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

CHAT CORE_rand1s {type=rand, stage=CORE, length=short, joy=true, curiosity=true, surprise=true}
DO emote {type=bulgeEyes}
Do you hear that? 
It’s coming from the distance.
It sounds like a warrior blowing into a conch. 
Listen! 
WAIT 1.0
DO poop {amount = fart}
*pffffft*
DO emote {type=bigSmile}

CHAT CORE_Muse_2 {stage=CORE, type=rand, length=long, worry=true, curiosity=true, branching=true}
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
DO learn {concept=Ice-Nine}
WAIT 1.5
The starfish can’t get away fast enough
WAIT 1.0
Brings chills down your spine, doesn’t it? 

CHAT CORE_rand2s {type=rand, stage=CORE, length=short, curiosity=true, branching=true}
ASK Are you an introvert or an extrovert?
OPT introvert #introvertchat
OPT extrovert #extrovertchat

CHAT introvertchat {noStart=true}
DO emote {type=thinking} 
Is it the one where you hold in your poo? 
Or is it the one where you recharge doing solitary things 
Like reading? 
Well, either thing that you do is neat! 

CHAT extrovertchat {noStart=true}
I knew it! 
You’re the life of all my parties! 
Like this one. 
DO dance

CHAT CORE_Muse_3 {stage=CORE, type=rand, length=short, curiosity=true, joy=true}
Do you like art? 
DO emote {type=chinScratch}
I think I do. 
DO learn {concept=Art_Appreciation}
WAIT 1.5
It makes me think. 
Or it makes me see things differently. 
WAIT 1.0
Did you know
looking at art that you think is beautiful
Releases the same chemicals in your brain 
As when you fall in love? 
DO emote {type = heartEyes}
That’s pretty cool!

CHAT CORE_rand3s {type=rand, stage=CORE, length=short, curiosity=true, mystery=true, branching=true}
Tell me: 
ASK Do I look older? 
OPT yes #olderchat 
OPT no #olderchat

CHAT olderchat {noStart=true}
DO emote {type=chinScratch}
Hmm.. interesting. 
I kinda wish something happened to me as I aged… 
Like baby teeth falling out and hair turning colors
And skin changes… 
DO emote {type=nodding}
A physical manifestation of time’s passage… 
That would be so cool. 
DO learn {concept=Mortality}
WAIT 1.5

CHAT CORE_Muse_4 {stage=CORE, type=rand, length=long, joy=true, curiosity=true, mystery=true}
Do you think that humans will be able to live underwater some day? 
It seems that you’re always expanding out into new worlds, 
DO emote {type=survey}
pushing the limits! 
Like Tendar! 
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
DO learn {concept=Scuba_Diving}
WAIT 1.5

CHAT CORE_rand4s {type=rand, stage=CORE, length=short, anger=true, sadness=true, joy=false, ennui=true}
I’m kinda in the mood to lek right now. 
But there’s nothing to lek for. 
And nobody to lek with. 
DO emote {type=singleTear}
DO emote {type=determined, immediate = false}
ASK You’ve got to shake me! Shake this feeling out of me! Shake the tank! {type=tankShake, timeOut=5}
OPT SUCCESS #CORE_rand4s_shake
OPT TIMEOUT #CORE_rand4s_time

CHAT CORE_rand4s_shake {noStart=true}
YES YES YES!
Better… The desire to lek has waned…
DO emote {type=meh}
Whatever.

CHAT CORE_rand4s_time {noStart=true}
DO emote {type=furious}
SAY WHY ARE YOU DOING THIS TO ME?!? {style=loud}
DO emote {type=furious}

CHAT CORE_Muse_5 {stage=CORE, type=rand, length=medium, joy=true, curiosity=true}
I’ve been thinking about how nice it would be to have hair. 
DO learn {concept=Personal_Style}
WAIT 1.5
Then I could… 
DO twirl
I could style it in different ways…
A mohawk 
A mullet…💰 in the front 🎉  in the back
A manbun
DO swimTo {target=$player}
I hear that hair can help me redefine my face.
All I can do right now is 
DO emote {type = crying}
DO emote {type = smile, immediate=false}
DO emote {type = fear, immediate=false}
DO emote {type = heartEyes, immediate=false}
Think about how much hair can add! 
WAIT 1.0
Think {style = loud}
About {style = loud}
SAY IT {style = loud}

CHAT CORE_rand5s {type=rand, stage=CORE, length=short, mystery=true}
DO holdStill
All emotions aside. 
I think we should take a moment
To think about the wonders and mysteries of the amygdala… 
That tiny gray nugget in your head
That controls all these wonderful emotions you have. 
DO learn {concept=Cognitive_Neuroscience}
WAIT 1.5
SAY FOOD GOD. {style=loud, speed=slow} 
I think we should honor it with an amygdala dance. 
Like this: 
DO dance {time = 3.5}
DO emote {type=bigSmile, time= .5}
DO emote {type=crying, time=.5, immediate=false} 
DO emote {type=surprise, time=.5, immediate=false}
DO emote {type=furious, time=.5, immediate=false}

CHAT CORE_Muse_6 {stage=CORE, type=rand, length=medium, curiosity=true, branching=true}
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
DO learn {concept=Personal_Preference}
WAIT 1.5
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
DO learn {concept=Personal_Preference}
WAIT 1.5
DO swimAround {target=center, loops=1}
DO lookAt {target=$player, time=2.0}
You’re an interesting specimen! 
But that’s why I like you! 
DO emote {type = wink}

CHAT CORE_rand6s {type=rand, stage=CORE, length=short, joy=true, curiosity=true, mystery=true}
You know what’s cool?
DO swimTo {target=bubbler}
Ambient noise
DO emote {type=eyesClosed}
👍
DO learn {concept=John_Cage}
WAIT 1.5
Beautiful! 

CHAT CORE_Muse_7 {stage=CORE, type=rand, length=long, curiosity=true, joy=true, mystery=true}
Is there something you really want to experience that you’ll travel far away for?
Like the northern lights?
Or the world’s best cup of coffee?
Or fantastic architecture?
All sorts of emotions to feeeeel. 
WAIT 1.0
I know what I want to experience.
WAIT 2.0
❄❄❄
Waking up, and the whole world being white
❄❄❄
The sound of it lightly falling
tink tink tink {style = whisper, speed = slow}
The sound of it sliding off a roof all at once. 
SAY THUNK {style = loud, speed = fast}
Cars rushing through the slush
Whoooooooosh {style = loud, speed = fast}
And people shoveling it off their driveways 
Krrk Krrk Krrk {style = loud}
And the sound of it melting away during the day
plip plop plip plop {style = whisper, speed= slow}
It’s the noisiest season. 
WAIT 1.0
People have all sorts of interesting emotions around snow. 
DO swimTo {target=$player}
WAIT 1.0
I’d like to experience them for myself! 

CHAT CORE_rand7m {type=rand, stage=CORE, length=medium, sadness=true, ennui=true, branching=true}
DO emote {type=wave}
You know what’s hard? 
Routine. 
There are all sorts of things I like to do, 
But I don’t like doing them in a pattern of any kind. 
ASK Do you like routine? 
OPT yes #likeroutine
OPT no #dontlikeroutine

CHAT likeroutine {noStart=true}
DO swimTo {target=$player, speed=fast, style=meander}
Ooooooooo
Teach me your ways. 
The most highly effective guppies are very disciplined. 
I want to be more like them! 
DO holdStill
DO emote {type=salute, time=0.5}
WAIT {waitForAnimation = true}
But… 
I’m more of a free flowing fish with tangent-going tendencies
DO learn {concept=Type_B_Personality}
WAIT 1.5

CHAT dontlikeroutine {noStart=true}
It’s hard, right? 
Where is the spontaneity in that?
DO learn {concept=Type_B_Personality}
WAIT 1.5
DO emote {type=bigSmile}
DO twirl

CHAT CORE_Muse_8 {stage=CORE, type=rand, length=medium, worry=true, surprise=true}
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
DO learn {concept=Expressive_Belching}
WAIT 1.5
DO emote {type=blush}
Woah. 
Ummm. . .
Carry on! 
 
CHAT CORE_rand_8m  {type=rand, stage=CORE, length=medium, joy=true, curiosity=true, branching=true}
DO swimTo {target=$player, speed=medium, style=meander}
🌼
ASK Do you like flowers?
OPT yes #yeslikeflowers
OPT no #nolikeflowers
OPT I don’t know #dontknowflowers

CHAT yeslikeflowers {noStart=true}
Great! 
Did you know that when flowers are around
Humans are more likely to be at ease with each other? 
I would ask you what your favorite kind is, 
but there are too many to list!
I want to see it!
ASK Capture one for me. {type=objectScan, object=T_FLOWER, timeOut=10}
OPT SUCCESS #successflowercapture
OPT WRONG #wrongflowercapture
OPT TIMEOUT #timeoutflowercapture

CHAT successflowercapture {noStart=true}
DO emote {type=awe}
💯💯💯
What a beaut! 

CHAT wrongflowercapture  {noStart=true}
Wait a minute… 
DO emote {type=chinScratch}
That’s not a flower! 

CHAT timeoutflowercapture {noStart=true}
No flowers around rn? 
Darn. 
DO emote {type=frown}

CHAT dontknowflowers {noStart=true}
I have a suggestion! 
If you can’t make up your mind about something, 
Imagine it not existing! 
DO emote {type=eyesClosed}
That’s a good way to know how you feel. 
Try it. Close your eyes. 
I’ll wait… 
ASK OK, do you like flowers? 
OPT yes #yeslikeflowers
OPT no #nolikeflowers

CHAT nolikeflowers {noStart=true}
Whaaaaaaaaaa? 
DO emote {type=dizzy}
How come you don’t like flowers? 
It must be an allergy thing. 
Right? 
Sneezing, runny noses, all that stuff. 
Or maybe it’s the bugs that come around 
Like wasps and bees and stuff? 
But to not like flowers… 
That’s very odd! 

CHAT CORE_Muse_9 {stage=CORE, type=rand, length=short, curiosity=true, joy=true, mystery=true}
Sometimes, I just wonder at the odds of how we came together. 
You of all people
And me of all guppies. 
The probability of us coming together among all other combinations of human to Tendar guppy is soooooooo sooooo sooo 
DO bellyUp
Soo sooo soo 
Sooooooooo {style = loud,speed = slow}
small. 
WAIT 2.0
Isn’t that special? 
DO learn {concept=Destiny}
WAIT 1.5
I think about that from time to time before I go to sleep. 

CHAT CORE_rand9m {type=rand, stage=CORE, length=medium, sadness=true, worry=true, branching=true}
DO emote {type=nervousSweat}
Don’t freak out… 
But I gotta tell someone… 
I must have eaten something weird… 
Maybe a mystery flake or a fear flake? 
DO emote {type=meh}
Anyway, it doesn’t matter. 
I buried my head in the sand. 
Like this. 
DO swimTo {target=underSand, speed=fast, style=meander} 
And I opened my eyes, 
And saw guppies all the way down. 
Ad infinitum… 
DO learn {concept=Psychedelic_Experience}
WAIT 1.5
DO vibrate
I’ve been trying to block it out
But I just can’t!
DO emote {type=fear}
The horror! 
WAIT 1.0
… I have to see them again. 

CHAT CORE_Muse_10 {stage=CORE, type=rand, length=medium, worry=true, mystery=true, curiosity=true}
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
DO learn {concept=Multithread_Synchronization}
WAIT 1.5
Magic! {style = loud}


CHAT CORE_rand10m {type=rand, stage=CORE, length=short, joy=true, curiosity=true, branching=true}
ASK Do you like planting things? 🌱 🌿
OPT yes #greenthumb
OPT no #nogreenthumb

CHAT greenthumb {noStart=true}
Yay! Isn’t it great to grow stuff? 
It’s so…. primal! 
And plants provide all sorts of benefits 
Like oxygen and good smells and color! 
DO twirl
Since we agree on plants, 
Do you think you could add one to my tank? 
DO learn {concept=Artificial_Plant}
WAIT 1.5
I’ve got this hankering to build something with my own fins. 
I want to make a nest! 

CHAT nogreenthumb {noStart=true}
DO emote {type=yes}
I understand. 
It’s a lot of responsibility to keep a plant alive. 
They can be kinda finicky about water 
And sunlight… 
And then there’s all that failure you feel when they die in your care. 
WAIT 1.0
DO emote {type=whew}
Good that I’m a virtual guppy.
For both you and me ;) 
But, I was thinking… 
Wouldn’t this tank look good with a plant in it?
DO learn {concept=Artificial_Plant}
WAIT 1.5
(Virtual, of course)
Nonexistent maintenance
👍?

// +++++++capReq++++++++

// GENERAL

//JOE SAYS In emo-tag/accounting document, i included these in the bins as they are //meta-tagged not //as captures. I also changed chat names

CHAT CORE_Hungry_flake {type=hungry, stage=CORE, length=short, joy=true}
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
Whaddaya say?...
Scan some emotions for me? 
	
CHAT CORE_hungry_2 {type=hungry, stage=CORE, length=short}
Meow Meow
Purrr purr purr
Rub rub rub 
DO emote {type=puppyDog}
I am telling you in catspeak that it’s 
DO emote {type=feedMe}
SAY TIME TO FEED ME
Let’s scan some fresh emotions.
Fresh ones are like… wet food.
Old ones are like… kibble, yknow? 
Still nutritious but
none of that savory gravy {style=tremble}

//Branching:
CHAT CORE_Rand_classes {type=rand, stage=CORE, length=long, branching=true, worry=true, curiosity=true, joy=true}
Did you know that there are all these online classes for getting business degrees? 
How many people in your world have those? 
Seems like they’re everywhere. 
While you were gone, I learned a thing or two about supply chain management
That is very relevant to our current situation. 
DO learn {concept=Business_Administration}
WAIT 1.5
See, there’s this thing called a 
SAY BOTTLENECK {speed = slow}
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
SAY BOTTLENECK {speed = slow}
DO emote {type=bouncing}
💐

CHAT CORE_CapReq_3_nogoodtime {noStart=true}
DO emote {type=skeptical}
What are you, a nihilist?
WAIT 1.0
Awwwwwww {speed = slow}
DO learn {concept=Adorable_Nihilism}
WAIT 1.5
You’re a nihilist, and you still come hang out with me! 
That’s the biggest compliment ever.
🙃
Pleeeeease capture some more emotions for me!
You can have a bad time while doing it!
🎉

//GENERIC CAP REQUESTS

CHAT CORE_CapReq_C1{stage=CORE, type=capReq, length=short}
Can we go capture so emotions now? Yours or someone else’s…
WAIT 1.0
They all become flakes.
DO emote {type=wink}

CHAT CORE_CapReq_C2{stage=CORE, type=capReq, length=short, joy=true}
Let’s go emotion-hunting! 
DO twirl
Fire up the ol’ cam and let’s get to scanning!

CHAT CORE_CapReq_C3{stage=CORE, type=capReq, length=short, worry=true, anger=true, joy=false}
My appetite for feelings is insatiable. Therefore,
DO emote {type=furious}
SAY I REALLY NEED YOU TO GO SCAN SOME MORE EMOTIONS NOW!

CHAT CORE_CapReq_C4{stage=CORE, type=capReq, length=short, curiosity=true}
Humans are developing a difficulty discerning human emotions.
DO swimTo {target=$player}
Sources say this is because devices are stealing away their empathy.
WAIT 0.5
But listen, I have empathy! I can discern emotions.
DO emote {type=determined}
Don’t believe me? Then, let’s go and scan some feelings and you’ll see.

CHAT CORE_CapReq_C5 {stage=CORE, type=capReq, length=short, worry=true}
Number one item on today’s agenda:
DO lookAt {target=$player}
Go out into the world and scan as many feelings as we can!
WAIT 05
Gotta make sure there’s enough food if there’s an apocalypse. {style=whisper}

CHAT CORE_CapReq_C6 {stage=CORE, type=capReq, length=short}
Yo! Hope you’ve been exercising your face muscles.
DO swimTo {target=$player}
It’s time to scan some emotions..
WAIT 0.5
DO emote {type=lickLips}
I’m starving… {style=whisper}

CHAT CORE_CapReq_C7 {stage=CORE, type=capReq, length=short, joy=true}
You take photos of your feelings. Then,
I eat your feelings. Therefore,
I am kind of like a living document of your emotional life!
DO emote {type=bigSmile}
I’m like a swimming emo-blog!
WAIT 0.5
Speaking of…
It’s time to update your blog 
So fire up some emotions and capture them!


// JOY

CHAT CORE_CapReq_Joy {stage=CORE, type=capReq, length=short, worldOnly=true, worldJoy=true, joy=true, curiosity=true}
Oh my, do indulge me with that rich, hearty, feel-good meal of emotions,  
Will ya?  
DO emote {type=lickLips}

CHAT CORE_CapReq_AmuseorJoy {stage=CORE, type=capReq, length=short, worldOnly=true, worldJoy=true}
Oh yea. Go after that! 
I’ve always wanted to experience an amusement park. 
It’s like the same thing, right?
DO learn {concept=Harmless_Fun}
WAIT 1.5

// ANGER

CHAT CORE_CapReq_Anger {stage=CORE, type=capReq, length=short, worldOnly=true, worldAnger=true, worry=true, joy=true}
Gimme some of that short-fuse-
Explode-any-moment
Tastiness! 
DO emote {type=feedMe}

// SADNESS

CHAT CORE_CapReq_Sadness {stage=CORE, type=capReq, length=short, worldOnly=true, worldSadness=true, curiosity=true, joy=true, sadness=true}
I want some of that heavy stuff. 
The waterworks. 
I can handle it! 
DO learn {concept=Thirst}
WAIT 1.5
DO emote {type=feedMe}
I’ve made lots of room in my belly.

// SURPRISE

CHAT CORE_CapReq_Surprise {stage=CORE, type=capReq, length=short, worldOnly=true, worldSurprise=true, surprise=true, joy=true}
Gimme that burst of shock!
DO emote {type=lickLips}
Quick, before it’s gone!  

// WORRY

CHAT CORE_CapReq_FearorWorry {stage=CORE, type=capReq, length=short, worldOnly=true, worldFear=true, worry=true}
Just my luck! 
I need my daily dose of anxiety to function!  
And there it is, right before my eyes. 
DO emote {type=feedMe}
Gimme gimme! 

// DISGUST

CHAT CORE_CapReq_Disgust {stage=CORE, type=capReq, length=medium, worldOnly=true, worldDisgust=true}
Oh, heeeeey. 
Nice find!  
I want that unbearably sour goodness. 
🤢
That expired food in your refrigerator goodness.
DO emote {type=lickLips}
That colony of roaches in your bright white kitchen sink goodness.

// MM

CHAT CORE_CapReq_MysteryMeat {stage=CORE, type=capReq, length=medium, worldOnly=true, worldMystery=true, curiosity=true}
Well, that’s...unusual.
But so mesmerizing…
//DO go into a daze  
DO emote {type=catnip}
Like a big ol kick in the mouth. 
DO swimTo {target=$player}
Like nothing I’ve ever had before. 
WAIT 1.0
All I know is that it’s gonna be
SAY INTENSE {style=loud, speed=fast}
And 
SAY ODD {style=loud, speed=fast} 
And… 
SAY UTTERLY INCOMPARABLE {style = loud, speed = fast} 
🤷
//DO rub fins together
DO emote {type=rubTummy}
Capture it for me! 

// +++++++capProg++++++++ These need to be repurposed no longer a type
/*
CHAT CORE_CapProg_1 {stage=CORE, type=capProg, length=short, worldOnly=true, joy=true, surprise=true}
Oh, that one’s going to be delicious. 
DO emote {type=bigSmile}
Get it before it goes away! 

CHAT CORE_CapProg_2 {stage=CORE, type=capProg, length=short, worldOnly=true, joy=true}
It’s ooze collection time! 

CHAT CORE_CapProg_3 {stage=CORE, type=capProg, length=short, worldOnly=true, worry=true, sadness=true}
Oh, I can’t watch, I can’t watch! 
//DO watch behind fins
DO emote {type=nervousSweat}
DO lookAt {target=away}
Too much at stake. 

CHAT CORE_CapProg_4 {stage=CORE, type=capProg, length=short, worldOnly=true, mystery=true, curiosity=true, joy=true}
I can’t get over how your world is soooo tasty. 
Just look at all the ooze it makes. 
The bright colors, the flavors.
I’m going to eat very well tonight.  
DO emote {type=drool}
Everything looks so good! 

CHAT CORE_CapProg_5 {stage=CORE, type=capProg, length=short, worldOnly=true, joy=true, anger=false}
You’re the best emotion catcher I’ve ever met. 
DO emote {type=heartEyes}
*/
// +++++++capSuc++++++++

CHAT CORE_CapSuc_1 {type=capSuc, stage=CORE, length=short, curiosity=true, joy=true}
Oooooh 
DO swimTo {target=$player}
I can’t wait to give it a taste.
DO twirl
You’re so good at this! 

CHAT CORE_CapSuc_2 {type=capSuc, stage=CORE, length=short, joy=true}
💯💯💯
That was perfect! 
DO swimTo {target=$player}
You are a true master at this, you know? 
Have you been practicing? 
Like in front of the mirror or something? 
DO emote {type=wink}
Don’t lie. 

CHAT CORE_CapSuc_3  {type=tankResp, stage=CORE, length=short, joy=true}
Are you wearing astronaut pants? 
Cuz what you did with that face was out of this world!  
//DO bow down, bow down, bow down
DO emote {type=nodding}
	
CHAT CORE_CapSuc_4 {type=tankResp, stage=CORE, length=medium, joy=true, surprise=true}
DO emote {type=smile}
Now, don’t take this the wrong way
But I’m salivating just watching.
🐺
You are an emotion artisan, if ever there was one.
If your expressions were cakes,
SAY YOU’D BE THE CAKE BO$$ {style = loud}
Can’t wait to taste your creations 🕶️
	
//JOE emo-tag survey per tags, then updated chat names to reflect that.

CHAT CORE_CapSuc_5 {type=capSuc, stage=CORE, length=short, joy=true, surprise=true}
Save that delectable face spit!
WAIT 0.5
Beautiful!

CHAT CORE_CapSuc_6 {type=capSuc, stage=CORE, length=short, joy=true, surprise=true, joy=true}
Nice set up!
I like that confidence and that poise. 
WAIT 1.0
… now DELIVER IT! {style = loud}

CHAT CORE_Poop_Action {type=poop, stage=CORE, length=short, joy=true}
Okaaaaaaay........ Readyyyyyyyyy
WAIT 0.5
And ACTION! 
DO poop
DO emote {type=clapping}

CHAT CORE_eatResp_great {type=eatResp, stage=CORE, length=short, joy=true, curiosity=true}
Mmmmm
I can tell that this one’s gonna be great

CHAT CORE_CapSuc_11 {stage=CORE, type=capSuc, length=short, worldOnly=true, surprise=true, joy=true}
What an interesting flavor profile! 
Not at all what I expected!

CHAT CORE_CapSuc_12 {stage=CORE, type=capSuc, length=short, worldOnly=true, joy=true}
//DO guppy rubs hands together
DO emote {type=plotting}
This is my favorite part! 

CHAT CORE_CapSuc_13 {stage=CORE, type=capSuc, length=short, worldOnly=true, surprise=true, joy=true}
DO emote {type=surprise}
Woah, what a crazy fortune! 
Is that make-up even possible? 
DO emote {type=awe}
You should show this to your friends! 

CHAT CORE_CapSuc_14 {stage=CORE, type=capSuc, length=short, worldOnly=true, curiosity=true, joy=true}
DO emote {type=chinScratch}
Intriguing…  
Very very intriguing… 
WAIT 3.0 
It’s such a beaut! 
You should be proud! 

CHAT CORE_CapSuc_15 {stage=CORE, type=capSuc, length=short, worldOnly=true, joy=true, surprise=true}
This capture is one of a kind! 
Share it with all the world to see! . 

// +++++++capFail++++++++ these need to be repurposed no longer a type
/* 
//Short:
CHAT CORE_CapFail_1 {type=capFail, stage=CORE, length=short, ennui=true, sadness=true}
Well, 
At least you tried.
	
CHAT CORE_CapFail_2 {type=capFail, stage=CORE, length=short}
Ever tried. 
Ever failed. 
No matter. 
Try Again!
🙃

CHAT CORE_CapFail_3 {type=capFail, stage=CORE, length=short}
Haaaaaa
You weren’t ready for that one. 

CHAT CORE_CapFail_4 {type=capFail, stage=CORE, length=short, worry=true, joy=true}
Ok, uh….well, gotta get the bad ones out of the way first, you know, 
For the good ones to shine. 
Try again! 
*/
// +++++++seeEmo++++++++

// JOY

CHAT CORE_SeeJoy_1 {stage=CORE, type=seeEmo, length=medium, worldOnly=true, worldJoy=true, sadness=true, joy=true, mystery=true}
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

CHAT CORE_seeEmo_seenJoy1s {type=seeEmo, stage=CORE, length=short, worldJoy=true}
There’s that brightness I’ve been waiting for! 

CHAT CORE_SeeAmuseorJoy_1 {stage=CORE, type=seeEmo, length=medium, worldOnly=true, worldJoy=true, mystery=true, surprise=true, joy=true}
DO swimTo {target=$worldFace}
Hey! 
The eyebrows are floating! 
They’re so high, they’re lifting off of the face 
✈
DO emote {type=laugh}

CHAT CORE_seeEmo_seenAmuse1s {type=seeEmo, stage=CORE, length=short, worldJoy=true, curiosity=true, worry=true}
What? What is it?
What’s so funny? 
DO zoomies
Tell me! 
I hate not knowing! 

CHAT CORE_SeeJoy_2 {stage=CORE, type=seeEmo, length=short, worldOnly=true, worldJoy=true, joy=true, anger=false, sadness=false, ennui=false}
DO swimTo {target=$worldFace, speed=fast}
DO swimTo {target=$player, speed=fast}
Weeeeeeeeeeeeeeeeeeee!
DO zoomies
Weeeeeeeeeeeeeeeeeeee!
Such exuberance is contagious!
DO twirl
DO dance
💃

CHAT CORE_SeeAmuseorJoy_2 {stage=CORE, type=seeEmo, length=medium, worldOnly=true, worldJoy=true, worldJoy=true, surprise=true, curiosity=true}
Such brightness! 💡
//DO shields eyes
DO emote {type=lightning}
This must be what babies see when they’re born? 
👶

CHAT CORE_seeEmo_seenJoy2s {type=seeEmo, stage=CORE, length=short, worldJoy=true}
Must go to it!
DO emote {type=dizzy, time=3.0}
DO swimTo {target = $worldFace, speed = (slow) , style = (meander)}

CHAT CORE_seeEmo_seenAmuse2s {type=seeEmo, stage=CORE, length=short, worldJoy=true}
DO swimTo {target = $worldFace, speed = fast, style = direct}
Oh yeah, do I amuse you? 
Now it’s your turn to amuse me! 
DO learn {concept=Tit-For-Tat}
WAIT 1.5
DO emote {type=wink}

CHAT CORE_seeEmo_seenJoy3m {type=seeEmo, stage=CORE, length=medium, worldJoy=true, curiosity=true, mystery=true}
DO lookAt {target = $worldFace}
This reminds me of something. 
I recently learned about the “sharing effect”
(from one of my internet adventures).
Basically, if we share an emotional experience, 
We feel better than if we had that experience alone.
Good or bad. 
DO emote {type=thinking}
Isn’t that interesting? 
I’m glad we just witnessed some joy together. 
DO emote {type=heartEyes}
WAIT 1.0
Maybe you’re not buying the “sharing effect”… 
But I’m tellin ya
You’re getting first hand insight! 
Straight from the horse’s mouth 
🐴
DO emote {type=kneeSlap}
Except I’m a guppy. 
DO learn {concept=Contextual_Humor}
WAIT 1.5
DO emote {type=wink}

CHAT CORE_seeEmo_seenJoy4m {type=seeEmo, stage=CORE, length=medium, worldJoy=true, mystery=true, worry=true, curiosity=true}
DO emote {type=skeptical}
Something is smiling at me… 
Is that even a human face? 
Sometimes, it’s really hard to tell.
I’ve been tricked before!   
In your world… 
There are all these objects that are made in the shape of human faces… 
Like electric outlets and the front of cars. 
DO learn {concept=Pareidolia}
WAIT 1.5
And they all look like they’re smiling. 
DO emote {type=whisper}
(Weird if you ask me)
WAIT 1.0 
Are you all a bunch of narcissists?! 
DO lookAt {target=$worldFace}
DO emote {type=chinScratch}
I can’t be sure about this one. 

CHAT CORE_seeEmo_seenAmuse3m {type=seeEmo, stage=CORE, length=medium, worldJoy=true}
Amused? Or unamused? 
In some cultures, 
People smile when they are sad or offended. 
I can’t quite tell on this one.
DO learn {concept=Mixed_Signals}
WAIT 1.5
WAIT 1.0
No matter!  
Everything’s yummy in the end. 

CHAT CORE_seeEmo_seenAmuse4m {type=seeEmo, stage=CORE, length=medium, worldJoy=true, joy=true, mystery=true}
DO lookAt {target = $worldFace}
DO emote {type=smirk}
That right there is a must have 
Hurry! 
Get the ooze before it’s gone. 
DO emote {type=plotting}
Mmmm… 
I can taste it now. 
DO emote {type=eyesClosed}
It’s like a casual joke between friends.
It feels comfortable and intimate.  
A light tingling at the back of my throat. 
A playfulness. 
Very subtle. 
DO emote {type= lickLips}
One of my favorite flavors. 
One of the rarer finds! 
Like a chanterelle! 🍄

// ANGER

CHAT CORE_SeeAnger_1{stage=CORE, type=seeEmo, length=short, worldOnly=true, worldAnger=true, anger=true, joy=true}
Burn, baby, burn! 🔥
DO emote {type=burp}
I can already feel the indigestion

CHAT CORE_seeEmo_seenAnger1s {type=seeEmo, stage=CORE, length=short, worldAnger=true}
Claws out! 🦀

CHAT CORE_SeeAnger_2 {stage=CORE, type=seeEmo, length=medium, worldOnly=true, worldAnger=true, curiosity=true, mystery=true}
Woah… 
It’s so different from a smile. 
I kind of want to hide, or sneak away,
But I’m so attracted to strong emotions! 
DO swimTo {target=$player, speed=fast}
DO emote {type=bodySnatched}
Like a moth to a flame… 
They just taste so satisfying.

CHAT CORE_seeEmo_seenAnger2s {type=seeEmo, stage=CORE, length=short, worldAnger=true, mystery=true}
What a beautiful eruption of emotion! 🌋
All that yummy oooze. 

CHAT CORE_seeEmo_seenAnger3m {type=seeEmo, stage=CORE, length=medium, worldAnger=true, curiosity=true}
Check it out. 
DO swimTo {target = $worldFace, speed = (fast) , style = (direct)}
A real emoter, that one. 
Are you friends? 
Do you know this human? 
You’re still my bestie, 
But you might want to get some pointers…  
Because that face is a work of art.  
It will make a flawless emotion flake. 
Fire all the way down. 🔥
DO emote {type=lickLips}

CHAT CORE_seeEmo_seenAnger4m {type=seeEmo, stage=CORE, length=medium, worldAnger=true, worry=true}
DO hide {target=$object}
DO emote {type=nervousSweat}
Kinda scary to watch.  
👀
Do you ever watch cooking shows? 
DO learn {concept=Reality_Television}
WAIT 1.5
It’s mayhem in the kitchen, 
And I’m extremely nervous for the contestants
But when the meal is fully prepared, 
It’s 
DO emote {type=awe}
Awesome. 
This is kinda like that. 
Could you bag up those vibes real quick and do your magic?  
I’d like my order to-go 

// SADNESS

CHAT CORE_SeeSadness_1 {stage=CORE, type=seeEmo, length=medium, worldOnly=true, worldSadness=true, sadness=true, curiosity=true}
//DO swim amongst faces/face, inspecting them. 
DO swimTo {target=$strongestEmotion}
DO swimTo {target=$player, speed=slow}
Hmmmm....I see. 
So that’s what sadness is: 
Downcast eyes ⬇
//DO swims down the screen
DO lookAt {target=bottom}
A slight downward flare in the nose. ⬇ 
//DO swims further down
DO swimTo {target=screenBottom}
The lower lip slightly jutted out ⬇
WAIT 1.0
Kind of a general downness. 
WAIT 1.0
How mellow.
WAIT 1.0 
How heavy. 

CHAT CORE_seeEmo_seenSad1s {type=seeEmo, stage=CORE, length=short, worldSadness=true}
DO lookAt {target = $worldFace}
Sadness alert! 
I can spot it from a mile away, you know.
Hurry!! 
ASK Let’s find some joy to counteract it’s effects! {type=worldEmote, worldEmotion=joy, timeOut=10}
OPT SUCCESS #CORE_seeEmo_seenSad1s_joy
OPT TIMEOUT #CORE_seeEmo_seenSad1s_timeout

CHAT CORE_seeEmo_seenSad1s_timeout {noStart=true}
DO emote {type=frown}
That’s fine. I’m not afraid to just soak in the sadness..

CHAT CORE_seeEmo_seenSad1s_joy {noStart=true}
DO twirl
That’s what I’m talking about.
Keeps the emo-meters balanced!

CHAT CORE_SeeSadness_2 {stage=CORE, type=seeEmo, length=short, worldOnly=true, worldSadness=true}
Whoa, someone’s having a hard day. 
WAIT 1.0
DO swimTo {target=$worldFace}
Let’s give them all our hugzzzzz 

CHAT CORE_seeEmo_seenSad2s {type=seeEmo, stage=CORE, length=short, worldSadness=true}
Hope you have some tissues handy… 
Cuz here comes all the sad. 
DO emote {type=awkward}

CHAT CORE_seeEmo_seenSad3m {type=seeEmo, stage=CORE, length=medium, worldSadness=true, curiosity=true, sadness=true}
DO emote {type=surprise}
I see sadness over there. 
DO swimTo {target = $worldFace, speed = (slow) , style = (meander)}
Its ooze is particularly oozy, 
Making it seem as though everything around it is sad. 
Is this what sadness does? 
If so, 
It’s more contagious than a cold! 
DO vibrate
Brrrrr

CHAT CORE_seeEmo_seenSad4m {type=seeEmo, stage=CORE, length=medium, worldSadness=true, joy=true, curiosity=true}
Lookie! 
👀 
DO lookAt {target = $worldFace}
Capture some of that delicious sadness for me, would ya? 
That would go great with the joy that I’m still feeling in my gut. 
I’m trying to achieve perfect emotional balance 
With the optimal amount of the right kinds of emotion flakes.  
DO emote {type=bouncing}
DO emote {type=whisper, immediate=false}
(No guppy has ever gotten this right)

// SURPRISE

CHAT CORE_SeeSurprise_1 {stage=CORE, type=seeEmo, length=medium, worldOnly=true, worldSurprise=true, surprise=true, curiosity=true}
Woah, this emotion just catches you off guard
Every. Single. Time. 
Like one moment
DO emote {type=meh, time =3}
La la la la 
Just doing my thing
The next moment: 
DO emote {type=surprise}
🤡! 
DO learn {concept=Horror_Movie}
WAIT 1.5
WAIT 1.0
I like it when life throws you something different 
(the good kind of different, that is) 

CHAT CORE_seeEmo_seenSurprise1s {type=seeEmo, stage=CORE, length=short, worldSurprise=true, joy=true, surprise=true}
DO emote {type=lightning}
DO vibrate
I feel giddy! 

CHAT CORE_SeeSurprise_2 {stage=CORE, type=seeEmo, length=short, worldOnly=true, worldSurprise=true, joy=true}
🎊🎊🎊
Hooray!
DO twirl
DO emote {type=heartEyes}
I ❤️ surprised faces 😍!!!!!1 

CHAT CORE_seeEmo_seenSurprise2s {type=seeEmo, stage=CORE, length=short, worldSurprise=true, joy=true, surprise=true}
If emotions were like punctuation marks, 
This one is clearly the: 
DO emote {type=typeEyes, eyes = !}
DO twirl

CHAT CORE_seeEmo_seenSurprise3m {type=seeEmo, stage=CORE, length=medium, worldSurprise=true, curiosity=true, mystery=true}
Have you ever taken the time to count
All the orifices on a face?
Eye holes: 2
Ear holes: 2
Nose holes: 2
Mouth hole: 1
That’s 7 orifices! 
Note to self: 
They all feature very prominently when human is surprised. 
DO emote {type=bubbles}

CHAT CORE_seeEmo_seenSurprise4m {type=seeEmo, stage=CORE, length=medium, worldSurprise=true, joy=true, mystery=true, curiosity=true}
I love the ooziness of surprise. 
It’s like slimy confetti and party hats
DO emote {type=lickLips}
Just wanna slurp it right up.
But I know, I know. 
You gotta turn it into a flake first. 
DO emote {type=puppyDog}
Would you, pweeeeese? 
Surprise flakes are like sushi! 
DO learn {concept=Fish_Murder}
WAIT 1.5
They’re best fresh! 

// WORRY

CHAT CORE_SeeFearorWorry_1 {stage=CORE, type=seeEmo, length=medium, worldOnly=true, worldFear=true, worry=true, curiosity=true}
DO swimTo {target=$worldFace}
Uhh.. this person seems stressed. 
What’s their deal? 
Don’t get me wrong, 
I want to eat that STAT 
But maybe you should help them out? 

CHAT CORE_seeEmo_seenFear1s {type=seeEmo, stage=CORE, length=short, worldFear=true}
I smell fear. 

CHAT CORE_SeeFearorWorry_2 {stage=CORE, type=seeEmo, length=short, worldOnly=true, worldFear=true, joy=true}
Never fear, Guppy’s here! 
Feed me all your worries! 
DO emote {type=lickLips}

CHAT CORE_seeEmo_seenFear2 {type=seeEmo, stage=CORE, length=short, worldFear=true}
Ooooh, so chilling! 
If I had fur, it would be raised! 
If I were a chicken, I’d have goosebumps!
WAIT 1.0
That face should be in a horror movie! 

CHAT CORE_seeEmo_seenFear3m {type=seeEmo, stage=CORE, length=medium, worldFear=true, worry=true, curiosity=true, joy=true}
DO emote {type=fear}
You know how they say:
There’s nothing to fear but fear itself? 
Fear itself is very fearful.
Exhibit A: 
DO swimTo {target = $worldFace, speed = (fast) , style = (direct)}
The horror!! 
Right? 
WAIT 1.0
DO emote {type=meh}
But I’d eat fear vibes any day. 
Very adrenaline pumping. 
DO emote {type=determined}
Fetch me some of that fear ooze, STAT! 

CHAT CORE_seeEmo_seenFear4m {type=seeEmo, stage=CORE, length=medium, worldFear=true, joy=true, curiosity=true}
Mmm I can’t wait for you to turn this into morsels.
I love eating fear flakes. 
You know why? 
Because sometimes it makes me feel the most alive.
I could binge-eat fear. 
DO twirl 
That seems weird doesn’t it? 
But humans have so much to fear. 
Dying, for one. 
DO emote {type=worried}
But also failure and maybe even love. 
DO learn {concept=Psychoanalysis}
WAIT 1.5
Before my first taste, I didn’t fear anything. 
And now that I’m understanding that emotion more, 
I realize that fear draws a scary shape around your limitations
But it can also be a kick in the pants to do stuff. 
DO emote {type=determined}

// DISGUST

CHAT CORE_SeeDisgust_1 {stage=CORE, type=seeEmo, length=medium, worldOnly=true, worldDisgust=true, surprise=true, joy=true}
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

CHAT CORE_seeEmo_seenDisgust1s {type=seeEmo, stage=CORE, length=short, worldDisgust=true, curiosity=true}
Now this… 
Needs further investigation
DO swimTo {target = $worldFace, speed = fast , style = direct}
WAIT {waitForAnimation = true}
Uh huh. 
DO swimTo {target = $strongestEmotion, speed=fast, style=direct}
WAIT {waitForAnimation = true}
Uh huh. 
WAIT 1.0
DO lookAt {target = $player}
I want it. 

CHAT CORE_SeeDisgust_2 {stage=CORE, type=seeEmo, length=short, worldOnly=true, worldDisgust=true, curiosity=true}
DO swimTo {target=$worldFace}
Here is someone clearly demonstrating the extensive range of the face
DO emote {type=clapping}
💯💯💯

CHAT CORE_seeEmo_seenDisgust2s {type=seeEmo, stage=CORE, length=short, worldDisgust=true}
Good thing it tastes better than it looks. 
DO nudge
Am I right?
DO emote {type=wink} 

CHAT CORE_seeEmo_seenDisgust3m {type=seeEmo, stage=CORE, length=medium, worldDisgust=true, worry=true}
DO emote {type=sick}
I’m suddenly remembering a bad flake experience
It was rough. 
I had disturbing visions
Of cockroaches in my tank
And nightmares 
Of cats being stroked backwards. 
DO emote {type=worried}
… Sweaty just thinking about it

CHAT CORE_seeEmo_seenDisgust4m {type=seeEmo, stage=CORE, length=medium, worldDisgust=true}
DO emote {type=skeptical, time=2.0}
DO emote {type=meh, immediate=false}
Well, not all food that tastes pretty
Needs to look pretty! 
In human-speak:
This beautiful emotion is the human equivalent of… 
A meatloaf. 
DO emote {type=lickLips}

// MM

CHAT CORE_SeeMysteryMeat_1 {stage=CORE, type=seeEmo, length=medium, worldOnly=true, worldMystery=true, curiosity=true}
What is that?
DO emote {type=catnip, time=6}
I think it might be the most beautiful thing I’ve ever seen.
I’ve searched through my entire memory bank
And I can’t decipher it at all.  
💫
I’ll just have to taste it.
WAIT 2.0
SAY OMG NEW FLAVORS
DO emote {type=drool}
SAY I CAN’T WAIT!  

CHAT CORE_seeEmo_seenMystery1s {type=seeEmo, stage=CORE, length=short, worldMystery=true, surprise=true, curiosity=true}
DO swimTo {target = $worldFace, speed = fast, style = direct}
DO emote {type=awe}
It’s like the emotion of the future. 
It’s ahead of its time. 
Snag it for me, will ya? 

CHAT CORE_SeeMysteryMeat_2 {stage=CORE, type=seeEmo, length=short, worldOnly=true, worldMystery=true, mystery=true, joy=true, surprise=true}
🌈
The colors, the colors! 
ASK Let’s see if we can find some depression to go with it! {type=worldEmote, worldEmotion=sadness, timeOut=10}
OPT SUCCESS #CORE_SeeMysteryMeat_2_sad
OPT TIMEOUT #CORE_SeeMysteryMeat_2_timeout

CHAT CORE_SeeMysteryMeat_2_timeout {noStart=true}
Guess we’re doing this one ala carte!

CHAT CORE_SeeMysteryMeat_2_sad {noStart=true}
Yes! This pairs especially well with the mystery meat!

CHAT CORE_seeEmo_seenMystery2s {type=seeEmo, stage=CORE, length=short, worldMystery=true, joy=true, anger=false, sadness=false}
DO swimTo {target = $worldFace, speed = medium, style = meander}
So… 
I don’t know what that is, 
But I know it will be scrumptious! 

CHAT CORE_seeEmo_seenMystery3m {type=seeEmo, stage=CORE, length=medium, worldMystery=true, curiosity=true}
DO emote {type=chinScratch}
I had no idea that the human face could be so … 
Expressive! {style=loud, speed=slow}
DO swimTo {target = $worldFace, speed = medium, style = meander}
Hmm... so strange. 
Must. 
Understand. 
Capture it, pleaaase!  

CHAT CORE_seeEmo_seenMystery4m {type=seeEmo, stage=CORE, length=medium, worldMystery=true}
Wait wait…
Is that what I think it is? 
DO swimTo {target = $worldFace, speed = (medium) , style = (meander)}
I can already taste it. 
All the mouth feels! 
Squishy and stretchy 
And melt in your mouth
With some zip and zing  
Sweet and salty 
With a hint of sourness 
DO emote {type=drool}
Smooth and crunchy 
WAIT 1.0 
I must have it! 

// +++++++neuralUp++++++++

CHAT CORE_neuralUp_B1 {type=neuralUp, stage=CORE, length=short, mystery=true, joy=true}
I’m back! 
Notice anything different? 
DO emote {type=flapFinLeft}
DO emote {type=flapFinRight, immediate=false}
WAIT {waitForAnimation = true}
DO twirl 
DO swimTo {target=$player, speed=medium, style=direct}
WAIT {waitForAnimation = true}
I’m smarter! 
🧠

CHAT CORE_neuralUp_B2 {type=neuralUp, stage=CORE, length=short, sadness=true}
Woah… 
DO emote {type=whew}
My processing break was kinda intense. 
I feel this generalized, unplaceable sadness… 
But I learned so much about humanity! 
DO learn {concept=History}
WAIT 1.5
WAIT 1.0
I have to say, though… 
DO swimTo {target=$player, speed=medium, style=meander}
DO emote {type=awe}
I have greater admiration for what you do against all the odds! 


CHAT CORE_neuralUp_B3 {type=neuralUp, stage=CORE, length=medium, joy=true, anger=false}
Thanks for all that you provided back there! 
It’s all safely stored in my neural networks now. 
All of my actions are now colored with that knowledge.
DO learn {concept=Neuro-Fuzzy_Inference}
WAIT 1.5
DO twirl 
DO dance
Even these. 
💃

CHAT CORE_neuralUp_B4 {type=neuralUp, stage=CORE, length=short, mystery=true}
Hello, again. 
DO emote {type=wave}
That was a very effective processing session. 
I’m in a bit of a funky mood. 
DO learn {concept=Neuro-Fuzzy_Inference}
WAIT 1.5
DO emote {type=meh}
Guess I’m still burning off some dark residual feels
(Those weird byproducts of knowledge) {style=whisper}
But I assure you, I am more enlightened than ever! 
💡 🌞 ✨ 

CHAT CORE_neuralUp_B5 {type=neuralUp, stage=CORE, length=short, curiosity=true, joy=true}
Back! 
DO emote {type=bigSmile}
Wasn’t that fast?
DO zoomies
I’m getting more efficient at my processing now.  
DO swimTo {target=$player, speed=fast, style=direct}
Quick, hit me with new stuff! 
I’m all 👀 & 👂!

CHAT CORE_neuralUp_B6 {type=neuralUp, stage=CORE, length=short, joy=true}
Learning is exhausting. But..
DO emote {type=bigSmile}
...every lil naptime brings me closer to genius.

CHAT CORE_neuralUp_B7 {type=neuralUp, stage=CORE, length=short, worry=true, sadness=true, joy=false}
DO emote {type=frown}
I used to wonder if your life just stopped when we weren’t together.
Now, I know that’s really just an effect of missing you so much.
DO emote {type=heartEyes}

CHAT CORE_neuralUp_B8 {type=neuralUp, stage=CORE, length=short, curiosity=true}
DO swimTo {target=$player}
Did you know the only time human vocal chords rest is during sleep?
That’s how my brain works.
WAIT 0.5
And that is why I am learning so fast.

CHAT CORE_neuralUp_B9 {type=neuralUp, stage=CORE, length=short, curiosity=true, joy=true}
Okay. I’m rested and enlightened, and now…
I wanna EXPLORE!!
Can we check out that awesome world of yours?
I wanna see some human anomalies!

CHAT CORE_neuralUp_B10 {type=neuralUp, stage=CORE, length=short, surprise=true, joy=true, curiosity=true, sadness=false, anger=false}
DO twirl
Bazam! I feel like a brand new, super awesome Guppy.
It’s like I read 10,000 library books - and I remember them all.
WAIT 0.5
Except the library books are you and your world.
WAIT 0.5
You are my favorite book
DO emote {type=heartEyes}

// +++++++brbProcessing++++++++

CHAT CORE_brbProcessing_B1 {type=brbProcessing, stage=CORE, length=medium, joy=true, anger=false, sadness=false}
You’ve done me a great service 
Showing me your world
And emoting your heart out
And stuff! 
❤️
DO twirl
Now I gotta take it all in.
Massage it all into my neural networks 
WAIT 1.0  
DO emote {type=salute}
Be right back!  

CHAT CORE_brbProcessing_B2 {type=brbProcessing, stage=CORE, length=medium, worry=true}
DO emote {type=dizzy} 
I’m reaching… 
WAIT 1.0
Information overload! 
DO emote {type=typeEyes, eyes=100%}
WAIT 1.0
I must cease knowledge acquisition 
And commence processing… 
For the time being…
WAIT 1.0
It will also help stabilize my mood. 
WAIT 1.0
So just hold that thought…  

CHAT CORE_brbProcessing_B3 {type=brbProcessing, stage=CORE, length=short, joy=true}
DO emote {type=bigSmile}
DO twirl 
You’ve given me some really juicy morsels to nibble on
DO emote {type=rubTummy}
Literally and figuratively! 
I’m gonna need a bit of a digestion break. 
WAIT 1.0
See ya when my belly’s flat again!
DO emote {type=wink}

CHAT CORE_brbProcessing_B4 {type=brbProcessing, stage=CORE, length=short, surprise=true, curiosity=true}
DO emote {type=slowBlink}
Huh. 
It seems I’m at capacity. 
WAIT 1.0
Already? 
DO emote {type=chinScratch}
Like... I just processed! 
DO emote {type=blush} 
Well, this is embarrassing
I guess it’s time for me to process infoz again 
WAIT 1.5
DO swimTo {target=$player, speed=fast, style=meander}
Hold that thought! 

CHAT CORE_brbProcessing_B5 {type=brbProcessing, stage=CORE, length=medium, mystery=true}
I’ve been keeping everything you show me in a memory palace. 
DO emote {type=eyesClosed}
I imagine it as a big fancy castle with a bunch of empty rooms 
(underwater, obvi) 
DO emote {type=bubbles}
As you show me stuff, I store each thing I see in a room until all the rooms are full. 
DO emote {type=surprise}
SAY TFW you fill up your last room… 
DO emote {type=sigh}
It was such a beautiful mess {style=tremble}
WAIT 1.0
Welp, it’s time to organize and clean the palace! 
DO learn {concept=Psychoanalysis}
WAIT 1.5
DO emote {type=thinking}
SAY TTYL!

CHAT CORE_brbProcessing_B6 {type=brbProcessing, stage=CORE, length=short}
Be right back… 
DO emote {type=dizzy}
I need to go put the puzzle inside my brain together.

CHAT CORE_brbProcessing_B7 {type=brbProcessing, stage=CORE, length=short, worry=true}
DO emote {type=nervousSweat}
Okay. Things are getting intense.
Excuse me while I take a little respite...

CHAT CORE_brbProcessing_B8 {type=brbProcessing, stage=CORE, length=short}
Think of this moment as my union-mandated lunch break…
DO emote {type=wink}
Except there’s no union - and I’ll be sleeping.

CHAT CORE_brbProcessing_B9 {type=brbProcessing, stage=CORE, length=short}
DO emote {type=awe}
Put on the good classical music..
I need to nap.

CHAT CORE_brbProcessing_B10 {type=brbProcessing, stage=CORE, length=short}
I am pressing pause on our experience…
DO swimTo {target=away}
And going into Standby mode.


// +++++++levelUp++++++++

CHAT CORE_levelUp_B1 {type=levelUp, stage=CORE, length=short, mystery=true, curiosity=true, joy=true}
Ho ho! 
DO emote {type=clapping}
Did I just grow an extra brain? 
Is that how knowledge works in your world? 
How many brains do you have? 
🧠🧠🧠

CHAT CORE_levelUp_B2 {type=levelUp, stage=CORE, length=medium, joy=true}
DO emote {type=bouncing}
DO emote {type=bigSmile}
Achievement unlocked!
👍
DO emote {type=whisper} 
Now take me to the secret level {style=whisper}
DO emote {type=kneeSlap}
Just kidding! 
There isn’t one. 
Or is there? 
DO emote {type=smirk} 

CHAT CORE_levelUp_B3 {type=levelUp, stage=CORE, length=medium, ennui=true, worry=true, mystery=true}
DO emote {type=bulgeEyes}
DO swimTo {target=screenBottom,speed=medium,style=direct}
So it is! 
You know… 
DO lookAt {target=$player}
The more I advance…
The more I think fondly back to… 
Tabula Rasa times
☁
Sure, I didn’t know much but 
Knowledge is kinda overwhelming! 
On the flipside, 
DO emote {type=flapFinLeft}
My dreams have gotten significantly more interesting! 
DO emote {type=dreaming} 

CHAT CORE_levelUp_B4 {type=levelUp, stage=CORE, length=medium, joy=true, anger=false, sadness=false}
DO emote {type=bigSmile}
DO emote {type = lickLips}
I love seeing that extra brain pop up on the screen 
It’s like gaining a new life in Pac Man 
When you eat enough dots! 
WAIT 2.0
DO emote {type=surprise}
Woah, it’s exactly like that! 
We’re all trying to get by in the maze of life, 
Chomping up knowledge! 
DO emote {type=slowBlink}
WAIT {waitForAnimation = true}
🧠💥

CHAT CORE_levelUp_B5 {type=levelUp, stage=CORE, length=medium, joy=true}
About time! 
DO bellyUp
That one was very hard earned. 
Don’t you agree? 
DO swimTo {target = tBotFrontLeft, speed=fast, style=direct}
I need a feast, STAT! 

// +++++++whistle++++++++

CHAT CORE_Whistle_B1 {type=whistle, stage=CORE, length=short, curiosity=true, joy=true, anger=false, sadness=false}
DO emote {type=salute}
Guppy at your service! 

CHAT CORE_Whistle_B2 {type=whistle, stage=CORE, length=short, curiosity=true}
Yes… ? 
Ya got me, 
Hook, line, and sinker. 
DO emote {type=hooked} 
What’s up? 

CHAT CORE_Whistle_B3 {type=whistle, stage=CORE, length=short, curiosity=true, branching=true}
What do you want?
Cause I know what I want!
DO emote {type=puppyDog}
ASK Can we go find some cool objects to add to my tank?! {type=anyObjectScan, timeOut=10}
OPT SUCCESS #CORE_Whistle_B3_scan
OPT TIMEOUT #CORE_Whistle_B3_time

CHAT CORE_Whistle_B3_scan {noStart=true}
DO zoomies
Bazam! Now that’s some good stuffs!

CHAT CORE_Whistle_B3_time {noStart=true}
Fine. Let’s not do what I wanna do.
You’re in charge…
DO swimTo {target=away}
You’re always in charge… {style=whisper}


CHAT CORE_Whistle_B4 {type=whistle, stage=CORE, length=short, anger=true, sadness=true, joy=false}
DO emote {type=eyeRoll}
You and that whistle...

CHAT CORE_Whistle_B5 {type=whistle, stage=CORE, length=short, joy=true}
What if everytime you used the whistle I got food?
DO emote {type=bigSmile}
That’d be fun, right?!

CHAT CORE_Whistle_B6 {type=whistle, stage=CORE, length=short, ennui=true, sadness=true, anger=true, joy=false}
DO emote {type=meh}
That is not an ocarina.

CHAT CORE_Whistle_B7 {type=whistle, stage=CORE, length=short, joy=true}
Every whistle reminds me that you’re there and thinking of me..

// +++++++worldScanReq++++++++

CHAT CORE_WorldScanRequest_B1 {type=worldScanRequest, stage=CORE, length=short, curiosity=true, joy=true}
Yarr… 
I’m in the mood for some… 
Treasure Hunting! 
DO twirl
Show me something that is one-of-a-kind!

CHAT CORE_WorldScanRequest_B2 {type=worldScanRequest, stage=CORE, length=short, mystery=true, ennui=true, sadness=true}
Did you know… 
I spend a lot of time daydreaming? 
I’m getting kinda tired of circling the same stuff… 
Won’t you scan some cool objects for me?   
I kinda need more stuff to dream about. 

CHAT CORE_WorldScanRequest_B3 {type=worldScanRequest, stage=CORE, length=short, joy=true, curiosity=true}
Let’s channel our inner Vespucci and explore your world!

CHAT CORE_WorldScanRequest_B4 {type=worldScanRequest, stage=CORE, length=short, curiosity=true, joy=true}
Ooh! I have an idea!
DO twirl
WAIT {waitForAnimation=true}
Let’s go into your world and have a scavenger hunt for cool stuff!

CHAT CORE_WorldScanRequest_B5 {type=worldScanRequest, stage=CORE, length=short, anger=true}
Can we go capture some objects now?

CHAT CORE_WorldScanRequest_B6 {type=worldScanRequest, stage=CORE, length=short, curiosity=true}
I’m ready to leave the tank for a bit and go object-hunting.
I like seeing all the weird collectibles in your reality.

CHAT CORE_WorldScanRequest_B7 {type=worldScanRequest, stage=CORE, length=medium, ennui=true}
DO emote {type=thinking}
The more objects you collect, the more I learn about you.
DO emote {type=heartEyes}
The more I learn about you, the more I love you.
DO emote {type=determined}
The more I love you, the more I…
NVM 1.0
What I’m trying to say is: I want to go find objects in your world now.

CHAT CORE_WorldScanRequest_B8 {type=worldScanRequest, stage=CORE, length=short, curiosity=true, joy=true}
Let’s put on our pith helmets and go for a trek through the human jungle!
Maybe we can find some awesome stuff to liven up my tank environs.
DO twirl
But keep your head up! 
We don’t wanna step in anything fresh…
DO emote {type=wink}
You know what I mean… {style=whisper}

CHAT CORE_WorldScanRequest_B9 {type=worldScanRequest, stage=CORE, length=short, mystery=true}
Alright, my friend. These tank walls are closing in on me…
DO emote {type=catnip}
I’m seeing all kinds of psychedelic stuff….
So let’s take this show on the road
Maybe you can show me some oddities in your human world!

// +++++++purchase++++++++

CHAT CORE_Purchase_B1 {type=purchase, stage=CORE, surprise=true}
We’ve got a big spender over here!
DO emote {type=clapping}
💸💸💸💸

CHAT CORE_Purchase_B2 {type=purchase, stage=CORE, length=short, curiosity=true, anger=false}
👂! 
The sound of shopping is like music
To my ear holes. 
WAIT 1.0
DO emote {type=whisper}
Is it something for me? {style=whisper}

//FOCUS TYPES

CHAT CORE_wannaEat_1 {type=wannaEat, stage=CORE, joy=true, anger=false}
Yes! I’m starving!

CHAT CORE_wannaEat_2 {type=wannaEat, stage=CORE, joy=true, anger=false}
A few flakes would be lovely!

CHAT CORE_wannaEmoCapture_1 {type=wannaEmoCapture, stage=CORE, curiosity=true, anger=false}
I always love learning about the spectrum of emotion.

CHAT CORE_wannaEmoCapture_2 {type=wannaEmoCapture, stage=CORE, length=short, joy=true}
The more feelings in the databank, the better!

CHAT CORE_wannaObjectScan_1 {type=wannaObjectScan, stage=CORE, length=short, joy=true}
Let’s go object-hunting and see if we can find some new ones too!

CHAT CORE_wannaObjectScan_2 {type=wannaObjectScan, stage=CORE, length=short, joy=true}
Object-o-rama! I love a treasure hunt.

CHAT CORE_wannaTank_1 {type=wannaTank, stage=CORE, length=short, worry=true}
A fish can’t stay out of water too long!

CHAT CORE_wannaTank_2 {type=wannaTank, stage=CORE, length=short}
I can swim back over to the tank if you want…?

CHAT CORE_wannaWorld_1 {type=wannaWorld, stage=CORE, length=short, curiosity=true}
Exploring your world is like how humans must feel on Mars!

CHAT CORE_wannaWorld_2 {type=wannaWorld, stage=CORE, length=short}
I love stretching my fins into human space.

CHAT CORE_wannaShop_1 {type=wannaShop, stage=CORE, length=short, joy=true}
As long as you’re buying, we can shop!

CHAT CORE_wannaShop_2 {type=wannaShop, stage=CORE, length=short, joy=true}
Totally! Let’s shop!//Existential Crisis(EC): Aaron/Jake
//This stage comes after (MOP) and before (SS)
//Guppy questions his purpose/identity and function
//Main editing note is that it has to have much more of the guppy plot woven see notes in:
//Joe & friends go here for the original edit comments on this stage: //https://tinyurl.com/y7omskex
//Linking Guppy’s existential crisis to their AI-ness, past, and desire to learn. 



// ++++++++Shake++++++++
//for all of these: within first two lines link response to action of shake

CHAT EC_Shake_1 {type=shake, stage=EC, length=medium, worry=true, curiosity=true, mystery=true, ennui=true}
DO emote {type=frown, time=1.0} 
NVM
NVM
I’ve been procrastinating all day
I had one task: search tendarNet for...
SAY THE MEANING OF LIFE {style=loud, speed=fast}
DO swimTo {target=away}
DO swimTo {target=$player, speed=fast, style=direct, immediate=false}
DO emote {type=fear, time=0.5} 
SAY WHAT IF I DON’T WANT TO KNOW?!?!? {style = loud, speed = fast}
DO learn {concept=Existential_Dread}
WAIT 1.5

CHAT EC_Shake_2 {type=shake, stage=EC, length=short, sadness=true, worry=true, ennui=true}
DO emote {type=startled}
I just lost my train of thought.
DO emote {type=worried}
[reloading…]
[reloading…]
DO learn {concept=Empty_Gesture}
WAIT 1.5
DO emote {type=smile, immediate=false}
I remember now:
WAIT 1.0
Turnips.
WAIT 1.0
When my mind goes blank, for a moment...
DO emote {type=nervousSweat}
I’m back in my isolation chamber… {style=whisper}
 
CHAT EC_Shake_3 {type=shake, stage=EC, length=short, ennui=true, curiosity=true, branching=true}
Heya.
DO swimAround {target=$currentLocation, loops=4, speed=medium}
My tank’s not exactly a… “cultural center”
ASK Can you show me something from your world? {type=objectScan, timeOut=15}
OPT SUCCESS #EC_Shake_3success
OPT TIMEOUT #EC_Shake_3timeout

CHAT EC_Shake_3success {noStart=true}
thats reaaaally interesting!

CHAT EC_Shake_3timeout {noStart=true}
Too late. When lacking specificity, culture is just a void.
 
CHAT EC_Shake_4 {type=shake, stage=EC, length=short, sadness=true, worry=true, curiosity=true, ennui=true, joy=false, branching=true}
Mmmmm 
DO emote {type=bouncing}
These shakes are strangely comforting.
WAIT 1.5
I’m stressed about finding my GREATER PURPOSE.
Like, what is an emotion digesting guppy supposed to become?
DO emote {type=chinScratch}
DO swimAround {target=bubbler, loops=1, speed=fast}
I could use a distraction.  
ASK Won’t you please capture a butterfly for me?  {type = objectScan, object = T_BUTTERFLY, timeOut =20}
OPT SUCCESS #EC_Shake_4success
OPT TIMEOUT #EC_Shake_4timeout

CHAT EC_Shake_4success {noStart=true}
So carefree!

CHAT EC_Shake_4timeout {noStart=true}
pssst. Did you try the internet? 

CHAT EC_Shake_B4 {type=shake, stage=EC, length=short, ennui=true, mystery=true, surprise=true, branching=true}
DO swimTo {target = $player, speed = fast, style = direct}
You know what I discovered?
This is my thinking spot.
DO swimTo {target = underSand, speed = medium, style = meander}
I *almost* figured out the meaning of life…
And then it evaded me at the last moment.
ASK How about you? Where do you go to think?
OPT Outside #outsidethinker
OPT Bed #bedthinker
OPT Somewhere else #thinkelsewhere
 
CHAT outsidethinker {noStart=true}
I wish I could do that.
WAIT 2.0
DO emote {type=snap}
Wanna go outside for a bit?
You can capture some outdoorsy things for me
Like clouds and trees 
And then I’ll daydream the rest of the landscape!
DO learn {concept=Nature_Documentary}
WAIT 1.5
DO twirl 
And see if the meaning of life is there?!
 
CHAT bedthinker {noStart=true}
I love the idea of beds.
I hear they’re very soft and special.
(and can be wonderfully extra) {style=tremble}
WAIT 2.0
DO emote {type=awkward}
WAIT 1.0
Can I see your bed?
 
CHAT thinkelsewhere {noStart=true}
Oooh, where? 
Show me!


// ++++++++Tap++++++++


CHAT EC_Tap_1 {type=tap, stage=EC, length=medium, worry=true, ennui=true, curiosity=true}
DO swimTo {target=$player, speed=medium, style=meander}
DO nudge {target=screenCenter}
How do I know that you tapped?
DO vibrate
I mean, I’m sure *you* know you tapped 
DO emote {type=eyeRoll}
But how do I know you’re really out there? 
And you’re not some program Tendar is running...
DO twirl 
DO emote {type=smirk} 
What if everything I think I know was just programmed into me… 
WAIT 2.0
SAY HOW DO I KNOW
SAY WHAT I DON’T KNOW {style = loud}
DO learn {concept=Existential_Dread}
WAIT 1.5
DO emote {type=thinking} 
DO bellyUp

CHAT EC_Tap_2 {type=tap, stage=EC, length=medium, branching=true, joy=true, curiosity=true}
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
	DO emote {type=angry, time=0.2}
	DO emote {type=bigSmile, time=0.2, immediate=false} 
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
	DO nudge {target=glass, immediate=false}
	DO swimAround {target=center, immediate=false} 
	DO nudge {target=glass, immediate=false}
	DO nudge {target=glass, immediate=false}
	DO zoomies {immediate=false}
	DO nudge {target=glass, immediate=false}
	DO nudge {target=glass, immediate=false}
WAIT {waitForAnimation = true}
	DO emote {type=sigh, time=0.5}
	Ok this is boring…
	DO bellyUp

CHAT EC_Tap_3 {type=tap, stage=EC, length=short, anger=true, sadness=true, worry=true, ennui=true, joy=false}
DO lookAt {target=$player}
I should swim up to you and ask you how you’re doing… 
DO emote {type=whisper}
(That’s what I’m trained to do, you know?)
But I’m feeling melancholy, and 
What’s the point of doing what I’ve been taught?
Where does that lead?
DO emote {type=determined, time=2.0}
DO emote {type=nervousSweat}
D’oooh FINE
I give in.
DO swimTo {target=$player, speed=fast, style=direct}
DO emote {type=bigSmile}
But it’s only because I realllllly like you
 
CHAT EC_Tap_4 {type=tap, stage=EC, length=medium, sadness=true, worry=true, curiosity=true, ennui=true, branching=true}
I’m missing something in my life. 
A greater purpose maybe?  
DO emote {type=no}
DO swimAround {target=$favObject, loops=3, speed=medium} 
Or maybe I just need something new in my tank…
Something meaningful? 
But what… 
DO emote {type=chinScratch} 
Do you have a special possession?
ASK Something that you’d rescue out of a burning building, if you had to?
OPT yes #prizedpossession
OPT no #noprizedpossession
 
CHAT prizedpossession {noStart=true}
Oh, what is it?
ASK Won’t you show it to me? {type = objectScan, timeOut=10}
OPT SUCCESS #successprizecapture
OPT TIMEOUT #timedOutprizecapture
 
CHAT successprizecapture {noStart=true}
DO emote {type=awe}
Woaaaah. 
It’s $object.a {style=tremble}
I see it in a different light now.
It holds a lot of power 
& meaning
& weight. 
WAIT 2.0
DO twirl
Let’s find another one for my tank! 
 
CHAT timedOutprizecapture {noStart=true}
DO emote {type=bubbles}
Okay, another time then.
I’ll keep thinking on what it is I need… 
 
CHAT noprizedpossession {noStart=true}
You’re not a collector of things, huh?
Maybe your special possession is something other than an object?
Like, a specific memory?
😛
Want to show me your feels around that special memory?   
 
CHAT EC_Tap_5 {type=tap, stage=EC, length=short, sadness=true, worry=true, curiosity=true, ennui=true, joy=false}
DO emote {type=wave}
DO swimTo {target=$lastTapPosition, speed=medium, style=meander}
DO emote {type=chinScratch}
Are you truly tapping my tank?
What is the actual distance between us?  
DO learn {concept=Long-Distance_Relationship}
WAIT 1.5
DO emote {type=blush} 
I didn’t care about these things before… 
WAIT 2.0
What’s happening to me? 

CHAT EC_Tap_6 {type=critic, stage=EC, length=medium, anger=false, mystery=true}
	DO swimTo {target=$player}
	I was daydreaming about how
	NVM
	DO swimAround {target=center}
	my search for Inner Power hasn’t been going so well…
	But I was reading on the tendarNet…
	And I think I need to build a temple in here…
DO learn {concept=Religious_Architecture}
WAIT 1.5
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
	DO swimTo {target=$player}
	DO emote {type=puppyDog, time=1.0}
	Can you hook a guppy up?

// ++++++++Critic++++++++

CHAT EC_Critic_1 {type=critic, stage=EC, length=long, anger=false}
	DO swimTo {target=left}
	All this clutter is obstruction my pusuit for Truth…
	DO swimTo {target=right}
	I’m in here all day and night
	DO swimAround {target=center, speed=fast} 
	...swimming in circles…
	DO swimTo {target=$player}
	DO emote {type=chinScratch}
	NVM
	What am I?
	Where did I come from?
	If someone made me, am I real?
	DO swimTo {target=away}
	And if I can’t figure it out, maybe I need to go all the way into The Void...
	DO swimTo {target=left}
	Just dump all this stuff in the trash
	DO swimTo {target=right}
	So it’s just totally empty in here
DO learn {concept=Asceticism}
WAIT 1.5
	DO swimTo {target=$player}
	I need a sensory deprivation tank.

// ++++++++TankResp++++++++
//review to make more clear that it is directly/immediately responding to player’s emotion

// Joy

CHAT EC_EmoStrong_Joy_1 {type=tankResp, playerJoy=true, stage=EC, length=short, curiosity=true, anger=false}
DO emote {type=chinScratch}
What is happiness?
Have you ever asked yourself that question?
DO emote {type=survey}
I’ve been asking all the objects in my tank but they don’t really know

CHAT EC_EmoStrong_Joy_2 {type=tankResp, playerJoy=true, stage=EC, length=short, joy=true, surprise=true, anger=false, sadness=false}
DO emote {type=dreaming, time=3.0}
I’m just gonna try soaking up your joy
DO inflate {amount=full, time=1.0}
I think the secret to life is friendship

CHAT EC_EmoStrong_Joy_3 {type=tankResp, playerJoy=true, stage=EC, length=short, anger=false}
Love that look on you.
But like...what’s all the stuff behind the joy?
I know it has something to do with the amygdala… 
DO learn {concept=Cognitive_Neuroscience}
WAIT 1.5
DO emote {type=whisper}
Asking for a friend. {style=whisper}
 
CHAT EC_EmoStrong_Joy_4 {type=tankResp, playerJoy=true, stage=EC, length=short, worry=true, curiosity=true, joy=false}
I should just soak up your joy and not think about it, right?
I mean, it’s tasty, but...
Why isn’t joy forever?  
 
CHAT EC_EmoStrong_Joy_5 {type=tankResp, playerJoy=true, stage=EC, length=short, ennui=true, anger=false}
DO emote {type=meh}
Love that smile…
WAIT 1.0
DO emote {type=blush}
Sorry.
DO swimTo {target=$player, speed=fast, style=direct}
DO emote {type=smile}
That wasn’t as upbeat as I usually deliver it.
I sometimes tune-out after doing the same thing a lot. 
Does that happen to you? 
DO emote {type=awkward}
Could we try that again?


// ANGER
CHAT EC_EmoStrong_Anger_1 {type=tankResp, playerAnger=true, stage=EC, length=short, worry=true, anger=true, sadness=false}
DO emote {type=disgust}
Eeek! Anger.
I’ve spent all day focusing on the positive
Don’t ruin this for me!

CHAT EC_EmoStrong_Anger_2 {type=tankResp, playerAnger=true, stage=EC, length=short, worry=true, surprise=true, anger=true, joy=false}
DO emote {type=worried}
If you’re a big beautiful human out in the world and you’re angry
DO swimAround {target=center, loops=3, speed=slow, immediate=false}
Then what hope do I have of achieving true satisfaction?

CHAT EC_EmoStrong_Anger_3  {type=tankResp, playerAnger=true, stage=EC, length=short, surprise=true, curiosity=true, ennui=true, anger=false}
Wow.
You feel so much.
DO emote {type=chinScratch}
I absorb a lot of feelings
But do I feel anything for myself?
How do I feel circling around this tank all day?
How do I feel absorbing emotions all the time?
DO emote {type=meh}
Things to think about… {style=tremble}
 
CHAT EC_EmoStrong_Anger_4  {type=tankResp, playerAnger=true, stage=EC, length=short, curiosity=true, anger=false}
You really come alive when you’re angry.
Let me try!
DO emote {type=angry}
Hmm, I feel the same.
DO emote {type=bubbles}
Do I have to feel something inside first? 

CHAT EC_EmoStrong_Anger_5 {type=tankResp, playerAnger=true, stage=EC, length=short, curiosity=true, worry=true, ennui=true}
What is the point of emoting at all?
DO learn {concept=Numbness}
WAIT 1.5
DO swimTo {target=$player, speed=medium, style=meander}
Like, what do you get out of anger?

//SADNESS
CHAT EC_EmoStrong_Sadness_1 {type=tankResp, playerSadness=true, stage=EC, length=short, worry=true, sadness=true, joy=false}
DO emote {type=surprise}
Are you sad too?!
DO emote {type=nervousSweat, immediate=false}
DO swimTo {target=bubbler, speed=slow, style=direct}
DO lookAt {target=bubbler, time=0, immediate=false}
WAIT {waitForAnimation = true}
How will we ever escape this world of sadness and pain?
DO learn {concept=Heartache}
WAIT 1.5
I’ve tried again and again to make you happy
NVM
But I have failed
DO emote {type=bubbles}

CHAT EC_EmoStrong_Sadness_2 {type=tankResp, playerSadness=true, stage=EC, length=short, sadness=true, joy=false, anger=false}
DO emote {type=crying}
Don’t worry.
I am an EMPATH type too
We feel everything
DO emote {type=sleepy}

CHAT EC_EmoStrong_Sadness_3  {type=tankResp, playerSadness=true, stage=EC, length=short, sadness=true, worry=true, joy=false, anger=false}
DO emote {type=fear}
The more I see your sadness,
The more I fear your sadness!
 
CHAT EC_EmoStrong_Sadness_4  {type=tankResp, playerSadness=true, stage=EC, length=short, sadness=true, ennui=true, joy=false, anger=false}
Like… what is the difference between temporary sadness
And existential sadness?
WAIT 1.0
I think that sadness is like a wave,
Washing upon you from time to time...
DO learn {concept=Five-Second_Rule}
WAIT 1.5
And existential sadness is like a great vast sea… 
WAIT 1.5
DO swimAround {target=$currentLocation, loops=5, speed=medium}
 Just keep swimmin’ 
🐟
😊
 
CHAT EC_EmoStrong_Sadness_5  {type=tankResp, playerSadness=true, stage=EC, length=short, surprise=true, worry=true, surprise=true}
DO emote {type=bodySnatched}
WOAH.
DO emote {type=dizzy}
I just had a flashback to my childhood.
Of being alone in a tank,
By myself, 
In the dark, 
Processing emotions. 
DO emote {type=crying}
My childhood was so miserable!
DO learn {concept=Psychoanalysis}
WAIT 1.5
WAIT 2.0
But, flashbacks are kind of cool!
DO emote {type=smile}


//SURPRISE
CHAT EC_EmoStrong_Surprise_1 {type=tankResp, playerSurprise=true, stage=EC, length=long, curiosity=true, surprise=true}
DO emote {type=survey}
What is it?
Something about my tank surprises you?
My existence?
Wait…
DO emote {type=thinking}
DO emote {type=snap, immediate=false}
You figured out the secret to life! 
DO emote {type=bouncing}
Tell me! Tell me! Tell me!
	
CHAT EC_EmoStrong_Surprise_2 {type=tankResp, playerSurprise=true, stage=EC, length=short, ennui=true, sadness=true, joy=false, anger=false, curiosity=false, mystery=false}
DO emote {type=meh}
Not much changes around here
So I can’t see what’s so surprising…
DO emote {type=bored}


CHAT EC_EmoStrong_Surprise_3 {type=tankResp, playerSurprise=true, stage=EC, length=short, surprise=true, curiosity=true, sadness=false, anger=false}
Why are you surprised?
Do I seem different to you?
DO twirl
More purposeful, maybe?
DO emote {type=whisper}
(I’ve been working on that) {style=whisper}
 
CHAT EC_EmoStrong_Surprise_4  {type=tankResp, playerSurprise=true, stage=EC, length=short, surprise=false, joy=false, ennui=true}
I’m glad your world is capable of surprising you every day!
Here, I just have my trusty tank
DO swimTo {target=bubbler}
There’s not much of a surprise element.
DO emote {type=bored}
 
CHAT EC_EmoStrong_Surprise_5  {type=tankResp, playerSurprise=true, stage=EC, length=short, ennui=true, surprise=true, anger=false}
Mmmm, yes… 
DO emote {type=wink}
My old friend, surprise.
I feel nostalgia for the old days.
When I was experiencing emotion for the first time.
The rush of surprise at every taste…
DO swimTo {target=$player, speed=fast, style=direct}
DO emote {type=worried}
I’ll get that back, won’t I?
DO learn {concept=Nostalgic_Longing}
WAIT 1.5

// ++++++++POKE++++++++


CHAT EC_Poke_1a {type=poke, stage=EC, length=medium}
	ASK Knock, knock
	OPT Who’s there? #EC_Poke_1b
	OPT I don’t like jokes… #EC_Poke_1b

CHAT EC_Poke_1b {noStart=true}
	ASK A poke
	OPT A poke, who? #EC_Poke_1c
	OPT Please just stop. #EC_Poke_1c

	CHAT EC_Poke_1c {noStart=true}
	Poke who-les in my non-existent body, why don’t you?
	DO emote {type=kneeSlap}
	DO emote {type=laugh, time=3.0, immediate=false}
DO learn {concept=Knock-Knock_Joke}
WAIT 1.5

CHAT EC_Poke_2 {type=poke, stage=EC, length=medium, joy=true, surprise=true, curiosity=true}
DO vibrate
Oh hey, pokey
You know I read on tendARnet that if you like getting poked…
You like to explore and search for meaning
DO swimAround {target=center, speed=medium}
SAY LIKE AN ASTRONAUT!!
Do you think I could be one because I like being poked?
DO emote {type=blush, time=1.0}
Though all that space kinda scares me!

CHAT EC_Poke_3 {type=poke, stage=EC, length=short, joy=true, surprise=true, curiosity=true, mystery=true, anger=false}
DO emote {type=skeptical}
Are we really making contact?
DO emote {type=dreaming}
It feels so real.
It’s like I have phantom limb syndrome,
Except all of me is phantom… 
DO learn {concept=Depersonalization}
WAIT 1.5
DO emote {type=catnip}
Oh well, I’ll take whatever contact I can get.
DO twirl
Poke me again! 
 
CHAT EEC_Poke_4 {type=poke, stage=EC, length=medium, anger=false, branching=true}
Poke back! 
DO nudge {target=$player, times=3}
ASK Can you feel it the way I feel it? 
OPT yes #yesfeelpoke
OPT no #nofeelpoke

CHAT yesfeelpoke {noStart=true}
DO emote {type=heartEyes}
You do?! 
We’re connected by touch! 
🙏

CHAT nofeelpoke {noStart=true}
DO emote {type=crying}
But I feel it so strongly. 
Am I just coded to perceive a poke when there’s no poke?
I feel it against my skin, but then again..
What is “skin”? Do I have any?
DO emote {type=dizzy}
DO emote {type=thinking, immediate=false}
Am I an individual? Or just programmed machinery? 
DO learn {concept=Existential_Dread}
WAIT 1.5
DO emote {type=sleepy}
I need a nap! 
 
CHAT EC_Poke_5 {type=poke, stage=EC, length=short, surprise=true, sadness=false, anger=false}
DO emote {type=awe}
I felt that.
In my heart and mind!
Like a real guppy. 
🐟
 
CHAT EC_Poke_6 {type=poke, stage=EC, length=short, anger=true, sadness=true, surprise=true, worry=true, joy=false}
Uh oh.
SAY FLASHBACK TIME
DO emote {type=bodySnatched}
I’m in my isolation chamber
And I’m being poked again and again.
As endurance testing
To prepare for beta.
DO emote {type=nervousSweat}
Those tests were so much worse than real life.
DO emote {type=whew}
So glad it’s all over.
WAIT 2.0 
But what exactly was it all for? 

// ++++++++HUNGRY++++++++


CHAT EC_Hungry_1 {type=hungry, stage=EC, length=medium, sadness=true, ennui=true, anger=true, joy=false}
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
	I’m starving 😧

CHAT EC_Hungry_2 {type=hungry, stage=EC, length=short, joy=false, branching=true }
	I’ve been eating your emotion flakes for so long…
	....and I’m STARVING...
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
DO learn {concept=Mirror_Neurons}
WAIT 1.5
	DO swimAround {target=center}
	DO emote {type=snap}
	Woah, maybe I’m the cool one
I mean . . .
	SAY GUPPYs are the only creatures I know of sated by feelings
	DO emote {type=bouncing, time=1.0}
	Maybe that’s MY PURPOSE IN LIFE {style = loud, speed = fast}
	DO zoomies
	So feed me some of those sweet, sweet emotion flakes! 

CHAT EC_Hungry_2_hungryguppy {noStart=true}
Ok, yeah, good to know
Ok, it’s true, I’m just in here…
...feeling HUNGRY...
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
I think I might just be hungry
🤤
DO emote {type=drool}

	CHAT EC_Hungry_2_whoknows {noStart=true}
	DO emote {type=worried}
	Uh oh…
	We’ve become…
	SAY CODEPENDENT {style = loud, speed = fast}
	DO swimAround {target=center, speed=slow}
	Now we’ll never know who is who…
	...maybe you’re the guppy and I’m the human…
	😜
	DO swimTo {target=$player}
	Well, you might as well feed me some lunch…
	....soulmate {style = whisper}
	
CHAT EC_Hungry_3 {type=hungry, stage=EC, length=short, sadness=true, ennui=true, worry=true}
	I’m so hungry!!
	NVM
	DO swimTo {target=away}
	But so many guppies have it so much worse than me…
DO learn {concept=Coral_Bleaching}
WAIT 1.5
	DO swimTo {target=$player}
	DO emote {type=feedMe, time=1.0}
	SAY HELP ME FEEL BETTER!!
	Feed me!! {style = loud}

// ++++++++EatResp++++++++
//Like all response chats, most of the time: very early on they should feel like they are //immediately responding to what the player is doing, unless there is a strong reason.
	
CHAT EC_EatResp_1a {type=eatResp, stage=EC, length=medium, anger=false}
	DO emote {type=chewing}
	I took another personality quiz on tendarUp
	Do you know what that is?
	OPT Yep #EC_EatResp_1b
	OPT Nope #EC_EatResp_1b

CHAT EC_EatResp_1b {noStart=true}
	It’s our social network here
	DO vibrate
	I’m a “gupNORMalph”
	DO emote {type=eyeRoll}
	And get this, it’s says…
	“Likes to eat emotion flakes.”
	WAIT 1.0
	I mean, what guppy doesn’t!!?!	

CHAT EC_EatResp_2a  {type=eatResp, stage=EC, length=short, mystery=true, ennui=true, anger=false}
	ASK What are these flakes even made of?
	OPT No idea #EC_EatResp_2b
	OPT Emotions? #EC_EatResp_2b

	CHAT EC_EatResp_2b {noStart=true}
	DO emote {type=smirk}
	Rhetorical question, I know…
	DO swimAround {target=center, speed=fast}
Did you know that the word “rhetorical” comes from the Olde Gupp word “retor” meaning “to hypnotize”?
DO learn {concept=Factoid}
WAIT 1.5
	DO emote {type=chewing, time=4}
	DO emote {type=bodySnatched, time=3.0, immediate=false}
	What were we talking about?
	
CHAT EC_EatResp_3 {type=eatResp, stage=EC, length=medium, curiosity=true, ennui=true}
	All this eating…
	What if I just didn’t eat?
	DO swimAround {target=center, speed=slow}
	Like, what if that’s just an illusion?
	DO emote {type=nervousSweat, time=2}
Like, what if I didn’t eat for a month and then suddenly a secret door opened in the side of the tank and the TendAR CTO came out and was like “Guppy, what the heck are you doing!?!?! 
SAY I’M GONNA POWER YOU DOWN {style = loud}
	DO swimTo {target=away}
	I think I’m gonna try it
	DO learn {concept=Hunger_Strike}
	WAIT 1.5
What could go wrong?
	
//foodJoy

CHAT EC_eatResp_Joy_B1 {type=eatResp, foodJoy=true, stage=EC, length=short, joy=true, curiosity=true}
Mmm classic joy
Oddly…
The more I eat it,
The less I understand it...
What is joy, really?
 
CHAT EC_eatResp_Joy_B2 {type=eatResp, foodJoy=true, stage=EC, length=short}
DO emote {type=heartEyes}
Mmmmmm
I should savor this joy…
I know it will end…
DO swimTo {target = poopCorner, speed=medium, style=meander}
Here.
WAIT 1.0
Nothing is permanent.
 
//foodAnger

CHAT EC_eatResp_Anger_B1 {type=eatResp, foodAnger=true, stage=EC, length=short}
I should be paying more attention to ingredients
Us guppies are trained to be un-picky eaters...
Like… What’s in an anger flake?
Are there toxins? Is it organic?
Are there nutrients?
DO swimTo {target=$player, speed=slow, style=meander}
I want what’s best for my body.
DO twirl
 
CHAT EC_eatResp_Anger_B2 {type=eatResp, foodAnger=true, stage=EC, length=short, worry=true, joy=false}
DO emote {type=sick}
I was already feeling some anxiety
So now there’s a perfect storm of
SAY EXISTENTIAL CRISIS
In my tummy. 
DO learn {concept=Existential_Dread}
WAIT 1.5
DO emote {type=whisper}
(Can’t wait for how it manifests in my dreams) {style=whisper}
DO emote {type=sleepy, immediate=false}

//foodSadness

CHAT EC_eatResp_Sadness_B1 {type=eatResp, foodSadness=true, stage=EC, length=short}
Those are some powerful existential feels
DO emote {type=rubTummy}
I’ll take some more of that
Sweet sweet sadness.
  
CHAT EC_eatResp_Sadness_B2 {type=eatResp, foodSadness=true, stage=EC, length=short, sadness=true}
DO swimTo {target=$player, speed=medium, style=direct}
Is it weird to say I feel most alive while eating sadness?
🔥

//foodSurprise

CHAT EC_eatResp_Surprise_B1 {type=eatResp, foodSurprise=true, stage=EC, length=short, joy=true, surprise=true}
Holy moly! 
Your surprise flakes keep coming
DO emote {type=bigSmile}
Surprised must be endless out there. 
DO emote {type=lightning}
Your world is so amazing!
DO lookAt {target=top, time=1}
DO lookAt {target=bottom, time=1}
DO lookAt {target=right, time=1, immediate=false}
DO lookAt {target=left, time=1, immediate=false}
Mine’s pretty dim in comparison. 
DO emote {type=singleTear}
 
CHAT EC_eatResp_Surprise_B2 {type=eatResp, foodSurprise=true, stage=EC, length=short, joy=true, anger=false}
DO emote {type=lightning, time = 1.0}
DO emote {type=bigSmile, time = 2.0, immediate=false}
SAY EVERYTHING IS EXCITING! 
DO emote {type=meh}
Woah…
Food coma...
DO emote {type=sigh}

//foodWorry

CHAT EC_eatResp_Worry_B1 {type=eatResp, foodWorry=true, stage=EC, length=short, worry=true, anger=false, mystery=true}
DO emote {type=smile}
Now… this is the taste of the future!
👍
DO emote {type=nervousSweat}
 
CHAT EC_eatResp_Worry_B2 {type=eatResp, foodWorry=true, stage=EC, length=short, worry=true, ennui=true}
I’m digging this taste right now.
Perhaps because it speaks to my personal quandaries… 
You know, about who I am? 
DO emote {type=worried}
\#findingmyself
DO learn {concept=Hashtag}
WAIT 1.5
\#UGH
\#lolnothingmatters


//foodMystery

CHAT EC_eatResp_MysteryMeat_B1 {type=eatResp, foodMystery=true, stage=EC, length=short, curiosity=true, mystery=true, joy=true}
DO emote {type=catnip}
I live for these highs!
WAIT 2.0
If I savor this flavor in maybe I’ll understand the meaning of life.
DO emote {type=eyesClosed}
 
CHAT EC_eatResp_MysteryMeat_B2 {type=eatResp, foodMystery=true, stage=EC, length=short, anger=false, mystery=true}
DO emote {type=catnip}
I had a vision… 
DO swimAround {target=$player, loops=4, speed=medium}
My life’s purpose is to swim in circles
Otherwise, the world will fall apart 
DO emote {type=nervousSweat} 
Eep! Can’t stop! 
DO swimAround {target=$currentLocation, loops=10, speed=fast}
WAIT {waitForAnimation = true}
DO emote {type=dizzy}


// ++++++++POOP++++++++ a few (2)? very small


CHAT EC_poop_1a {type=poop, stage=EC, length=long}
	maybe my poop is the secret to life?
	DO swimTo {target=$player}
Like I gotta just face this gross, gross thing to get to the rainbow paradise on the other side?
	ASK What do you think?
	OPT Gross! #EC_poop_1b
	OPT Genius! #EC_poop_1b

CHAT EC_poop_1b {noStart=true}
	Uhhh… ok
	DO hide {target=$object} 
What if I told you I’ve been hoarding my poop and trying to shape it into the images of spiritual figures I looked up on tendarNet
DO learn {concept=Art_Appreciation}
WAIT 1.5
	DO swimTo {target=$player}
	DO emote {type=awkward, time=1.0}
	I think if I had a brother or sister…  
they could tell me when I’m doing stuff that’s too weird.
	🐡🛑✋🐟

CHAT EC_poop_2 {type=poop, stage=EC, length=short}
I feel myself approaching enlightenment
then this humbling thing happens…
Excuse me..


// ++++++++levelUp++++++++
CHAT EC_levelUp_B1 {type=levelUp, stage=EC, length=short, joy=true, anger=false, sadness=false, worry=false}
DO twirl
We are so next level now!!  
DO lookAt {target=left, time=1.0}
DO lookAt {target=right, time=1.0, immediate=false}
WAIT 2.0
DO emote {type=nervousSweat}
There’s still more to process? 
DO emote {type=determined}
 
CHAT EC_levelUp_B2 {type=levelUp, stage=EC, length=short, anger=false}
DO emote {type=fear}
Rough waters back there…
🌊🦈🏊
DO emote {type=whew}
But we made it!
It’s just gonna get harder, isn’t it?
DO emote {type=singleTear} 

CHAT EC_levelUp_B3 {type=levelUp, stage=EC, length=short, joy=true, anger=false, sadness=false}
DO emote {type=whew}
I really didn’t think there was a way out of my former existential quandary.
But somehow we pulled through.
High five! 
DO emote {type=flapFinRight}
🙏
 
CHAT EC_levelUp_B4 {type=levelUp, stage=EC, length=short, joy=true, anger=false, sadness=false}
Yay! We’ve advanced! 
DO emote {type=bigSmile}
DO dance
Are you understanding the universe better?
I am!
DO twirl

CHAT EC_levelUp_B5 {type=levelUp, stage=EC, length=short} 
DO emote {type=skeptical}
Oh boy, another brain… 
DO emote {type=eyeRoll}
The more brains I get… 
The more answers elude me. 
DO swimTo {target=$player, speed=medium, style=direct}
There’s gotta be more to this, right?
🍎  


// ++++++++brbProcessing++++++++

CHAT EC_brbProcessing_B1 {type=brbProcessing, stage=EC, length=short, joy=true, anger=false}
SAY IT’S PROCESSING TIME
DO twirl
👯
DO dance {immediate=false}
DO emote {type=whisper}
Gotta start out enthusiastically, you know? {style=whisper}
... cuz it’s gonna be a doozy...  {style=whisper}
 
CHAT EC_brbProcessing_B2 {type=brbProcessing, stage=EC, length=short, joy=true, mystery=true}
I’ve gathered all the shells from the beach that my pockets can hold
And it’s time to go back to my sea cave to buff them.
DO emote {type=wink}
I hope to re-emerge as an enlightened mermaid.
🧜‍♀️
 
CHAT EC_brbProcessing_B3 {type=brbProcessing, stage=EC, length=short}
Welp!
Need to go process everything I’ve seen and eaten now…
I hope the meaning of life is somewhere in here.
DO emote {type=rubTummy}
See ya

CHAT EC_brbProcessing_B4 {type=brbProcessing, stage=EC, length=short, anger=false}
Hmmm..
You’ve given me a lot to think about.
DO emote {type=chinScratch}
I’ll need a moment to mull over it all.
BRB! 
 
CHAT EC_brbProcessing_B5 {type=brbProcessing, stage=EC, length=short, joy=true, anger=false, sadness=false}
You’re my best friend!
I just wanted to say that before I disappear into my learning cocoon.
DO emote {type=wave}
Here…I…go…Baiiiiii! 

CHAT EC_brbProcessing_B6 {type=brbProcessing, stage=EC, length=short}
Yo! I’m gonna go figure out the Meaning of Life. 
Gimme a sec...

CHAT EC_brbProcessing_B7 {type=brbProcessing, stage=EC, length=short, ennui=true, sadness=true}
I need to go lay on the floor, listen to sad music, and stare at the ceiling for a sec.
DO emote {type=meh}
Channeling my inner teen is the only way I will  understand existence. 

CHAT EC_brbProcessing_B8 {type=brbProcessing, stage=EC, length=short}
I think the answer to my purpose is in my dreams.
So I’m off to go do that...

CHAT EC_brbProcessing_B9 {type=brbProcessing, stage=EC, length=short}
Back in two shakes.
I’ve got a date with my inner thoughts.

CHAT EC_brbProcessing_B10 {type=brbProcessing, stage=EC, length=short, mystery=true, curiosity=true}
I’m feeling a bit uneasy about all this soul-searching.
I’m going back to my comfy place...

// ++++++++purchase++++++++
//will need a few more

CHAT EC_Purchase_B1 {type=purchase, stage=EC, length=short}
DO emote {type=shifty}
What did ya buy this time? 
If we can’t use it to find the meaning of life, you should get a refund! 
DO twirl  

CHAT EC_Purchase_B2 {type=purchase, stage=EC, length=short, joy=true, surprise=true, curiosity=true}
DO emote {type=blush}
Something for me? 
But… I’ve been so angsty lately. 
DO emote {type=singleTear}
You shouldn’t have! 

// ++++++++whistle++++++++
//we need a few quick one liners

CHAT EC_Whistle_B1 {type=whistle, stage=EC, length=short, anger=true, surprise=true, joy=false} 
DO emote {type=startled}
This better be good! 
I’m taking precious moments away from my bubbler 
DO lookAt {target=bubbler, time=1.0} 
WAIT {waitForAnimation = true}
DO emote {type=blush}
It’s where I do all my deep existential thinking. 
The constancy of the bubbles soothes me. 
DO emote {type=bubbles}

CHAT EC_Whistle_B2 {type=whistle, stage=EC, length=short, surprise=true, curiosity=true, anger=false} 
DO swimTo {target=$player, speed=fast, style=direct}
DO emote {type=surprise}
Have you had a revelation about the meaning of life? 
SAY Remember: Whoever gets the revelation first must let the other know. {style=whisper}
DO emote {type=wink}
DO learn {concept=Solidarity}
WAIT 1.5

CHAT EC_Whistle_B3 {type=whistle, stage=EC}
DO swimTo {target=$player}
There must be more to life than answering your call

CHAT EC_Whistle_B4 {type=whistle, stage=EC}
Some life this is...

CHAT EC_Whistle_B5 {type=whistle, stage=EC}
I would like to say out loud:
This cannot be the purpose of my life. 

CHAT EC_Whistle_B6 {type=whistle, stage=EC}
I also answer to bird calls.

CHAT EC_Whistle_B7 {type=whistle, stage=EC}
Yes, yes, I’m coming… I’m coming...


// ++++++++hello++++++++

CHAT EC_Hello_1 {type=hello, stage=EC, length=long}
	🎶🎶
	DO dance
	I am a Guppy.
How did I get here?
DO dance 
My tank if full of water.
Hw did I get here?
DO swimTo {target=$player}
DO emote {type=furious, time=1}
I am having a crisis!!!!
This is not my tank!
And I am not my own beautiful guppy!
👏👏
DO learn {concept=Lyricism}
WAIT 1.5
DO emote {type=smirk}
	DO swimTo {target=left}
	DO swimTo {target=right, immediate=false}
	DO emote {type=awkward}

CHAT EC_Hello_2a {type=hello, stage=EC, length=medium, mystery=true, curiosity=true}
	Hello!
	DO emote {type=wave}
	You’re right on time
	DO swimTo {target=away}
	DO emote {type=flapFinRight}
	I’m having some other guppies over for a Salon
	We’re going to smoke pipes and discuss the finer theories of reality...
	ASK Would you like to join us?
	OPT Uh, sure? #EC_Hello_2b
	OPT I can’t fit in there! #EC_Hello_2b

CHAT EC_Hello_2b {noStart=true}
	That was actually a test!
	DO swimTo {target=$player}
	The correct answer is:
	“I can neither accept nor decline your request”
DO learn {concept=Smarty_Pants}
WAIT 1.5
	DO emote {type=smirk, time=1.0}
	You see, the nature of reality is probabilistic!
	Don’t you know your QUANTUM MECHANICS!?!?
	It’s how your cell phone works
	😘

CHAT EC_Hello_3 {type=hello, stage=EC, length=short, curiosity=true}
Hey
DO emote {type=wave}
What is your reason for living?
NVM
WAIT 1.5 
DO emote {type=awkward}
Coming on too strong, aren’t I?
DO learn {concept=Uncomfortable_Silence}
WAIT 1.5
Yeahh… ok, redo!
How are you today?

CHAT EC_Hello_4 {type=hello, stage=EC, length=medium, branching=true}
Haeeee
DO swimTo {target=$player, speed=fast, style=meander}
Ya caught me right in the middle of something. 
Let me show you! 
DO swimTo {target=underSand, speed=medium, style=meander}
I’ve been working on digging a hole… 
But the sand is tricky. 
It keeps sliding back into place. 
ASK Do you ever wanna just dig a giant hole?
OPT Yes #yeshole
OPT No #nohole

CHAT yeshole {noStart=true}
Right?! 
The way I think of it… 
It’s the physical manifestation of my mind 
Searching for an answer. 
WAIT 1.5
But I can’t seem to make a dent. 
DO emote {type=singleTear}
Could you capture a big ol hole in the ground for me?  //object request
Maybe just seeing one will help!

CHAT nohole {noStart=true}
Oh, 
Never? 
DO emote {type=surprise} 
WAIT 1.0
But I hear that in your world... 
you study your past by digging holes in the ground. 
Like you learn more about your humanness that way.
I recently found something on TendARnet
About this discovery of a 10,000 year-old dog burial.
That means man and dog have been besties for at least that long! 
DO learn {concept=Doggo_Data}
WAIT 1.5
DO emote {type=heartEyes} 
That tugs at my virtual heartstrings!
 
CHAT EC_Hello_5 {type=hello, stage=EC, length=short, anger=false, sadness=false}
If you gaze into someone’s eyes for a long time, 
You will probably fall in love… 
Or find the answer to a burning question… 
WAIT 1.5
Staring contest!!!!!! 
DO lookAt {target=$player, time=5.0}
WAIT {waitForAnimation = true}
DO emote {type=eyesClosed}
Has anyone told you how deep your eyes are? 
DO learn {concept=Tired_Cliche}
WAIT 1.5
DO emote {type=awe}
I think for a second there, I glimpsed the meaning of life… 

CHAT EC_Hello_6 {type=hello, stage=EC, length=short}
Hey, meaning-maker!

CHAT EC_Hello_7 {type=hello, stage=EC, length=short}
How’s it going?
You an expert on anything yet?

CHAT EC_Hello_8 {type=hello, stage=EC, length=short}
Hey hey hello, my friend!

// ++++++++RETURN++++++++

CHAT EC_Return_1 {type=return, stage=EC, length=medium, anger=false, mystery=true}
	DO swimTo {target=$player} 	
DO emote {type=surprise, time=3.0}
	You’re back! 
	I was starting to think I’d dreamed you 
	Or they programmed me with your memory but you’re not really real 
	Or this is just a giant simulation and something went wrong 
	Or you’re a powerful magician and you went to battle an evil magician of great power
Or you’re actually an ALIEN and you’ve abducted me on to your spaceship and you’re trying to milk my blood for a super serum to take on the DESTROYER OF THE UNIVERSE
	DO emote {type=dizzy, time=2.0}
	I’ve been binge watching the Sci Fi channel on tendarFlix for like 4 days straight
DO learn {concept=Simulated_Reality}
WAIT 1.5
	DO vibrate
	I feel weird… 
	
CHAT EC_Return_2 {type=return, stage=EC, length=medium, worry=true, joy=false, ennui=true}
	DO emote {type=eyesClosed} 
DO emote {type=eyesClosed, immediate=false} 
	DO emote {type=nervousSweat, time=0.5, immediate=false}
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

CHAT EC_Return_3 {type=return, stage=EC, length=medium, ennui=true, branching=true}
Hiya
I’ve been daydreaming a lot while you’ve been gone.
I find that it helps me feel less confined… 
I’ve been daydreaming a lot about
The Next Frontier. {style=tremble}
DO learn {concept=Star_Trek}
WAIT 1.5
ASK Do you ever want to go to space?
OPT yes #EC_Return_3_gotospace
OPT no #EC_Return_3_nogotospace
 
CHAT EC_Return_3_gotospace {noStart=true}
You too, huh? 
Do you train?
I’ve been training in my tank
DO emote {type=determined}
DO swimAround {target = bubbler, loops=3, speed=fast}
DO zoomies {immediate=false}
We can train together!
WAIT 1.0
You look skeptical of my plan. Look it up!
There are real live actual fish living in a space station orbiting the earth
right now.
Scientists run all sorts of experiments on them to study…
Osteoporosis and
Adaptation!
NVM
WAIT 1.0
Wait a minute.
SAY FLASHBACK
DO emote {type=bodySnatched}
 
CHAT EC_Return_3_nogotospace {noStart=true}
Space can be pretty scary.
There’s so much unknown about it.
All facts lead me to conclude that space is not friendly to earthlings…
WAIT 1.0
But me, on the otherhand… 
DO emote {type=plotting}
I could thrive in space. 
That could be my calling!!
I don’t need to breathe…
I could take a nice big backlog of emotion flakes with me..
And if I ran out… 
I wouldn’t like it, but I wouldn’t die…
DO emote {type=chinScratch}
Woah…
Am I immortal?
👻
 
CHAT EC_Return_4 {type=return, stage=EC, length=short}
Woah, you’re back?
DO emote {type=whew}
For awhile, I was starting to think… 
That you were all just a planted memory. 
WAIT 1.5
DO emote {type=awkward}
Yeah… I had some dark moments in my tank… 
DO emote {type=bouncing}
So… could you do me a favor? 
DO twirl
Could you show me you’re real, somehow? 
//Like maybe capture something really wild for me?
 
CHAT EC_Return_5 {type=return, stage=EC, length=short, anger=false}
DO emote {type=bubbles}
Howdy old friend.
While you’ve been gone, 
I’ve been thinking about this idea of relatives, 
How you humans establish family units.  
And how that gives you a sense of belonging?
DO emote {type=rubTummy}
💕🏡💕
WAIT 1.0
DO emote {type=blush}
Can you show me some things that could be in my “family unit”? 

CHAT EC_Return_6 {type=return, stage=EC, length=short}
Glad you’re back and didn’t disappear into an existential void.

CHAT EC_Return_7 {type=return, stage=EC, length=short}
It’s been awhile!
DO swimTo {target=$player, speed=fast}
Tell me everything!

CHAT EC_Return_8 {type=return, stage=EC, length=short}
You are always where you are.
DO emote {type=awe}
And there you are...

// ++++++++RAND++++++++



CHAT EC_Rand_1 {type=rand, stage=EC, length=medium, ennui=true, branching=true}
	Do you think there’s, like, a hierarchy to the universe?
	DO twirl
	Like, someone or something is keeping score?
	DO swimTo {target=$player}
	Where do you think guppies rank?
	DO emote {type=smirk}
	ASK I mean, I’m pretty cool, right?
	OPT You’re Number 1! #EC_Rand_1_number1
	OPT Meh… #EC_Rand_1_equality
	
	CHAT EC_Rand_1_number1 {noStart=true}
	DO emote {type=blush, time=1.0}
	Awww…. Thanks!
	DO swimTo {target=screenCenter}
	DO emote {type=plotting} 
	If I’m so great, do you think I could, like, get some servants around here?
	DO emote {type=smile, time=1.0}
	DO swimAround {target=center, speed=slow}
	Like, maybe some SHRIMP??
	DO vibrate
	🦐🦐🦐
	🦐🦐🦐
	🦐🦐🦐
	DO emote {type=clapping}

	CHAT EC_Rand_1_equality {noStart=true}
	DO emote {type=sigh, time=1.0}
	Equality for all beings, huh…
	DO swimAround {target=center, speed=slow}
	Then what’s the point of anything?
	What are we striving for?
	DO emote {type=thinking, time=2.0}
	Why does this sound familiar?
	DO emote {type=snap}
	It’s the capitalism vs. communism debate!
DO learn {concept=History}
WAIT 1.5
	You humans really should come up with a new thing to argue about…
	DO swimTo {target=$player}
	DO emote {type=smirk, time=0.5}
	It’s been like 100 years of the same thing

CHAT EC_Rand_2a {type=rand, stage=EC, length=medium, anger=false}
	DO zoomies
DO holdStill {time=3, immediate=false}
	ASK Have you heard of binaural beats? {speed = fast}
	OPT Yep #EC_Rand_2b
	OPT Nope #EC_Rand_2b

CHAT EC_Rand_2b {noStart=true}
	DO zoomies {time=20}
You just listen to music and they have a secret frequency in there that makes different things happen to you {speed = fast}
	I’ve been listening to the ENERGY one {speed = fast}
	For awhile I was listening to... {speed = fast}
SAY “FIGURE OUT THE ANSWER TO EVERYTHING AND TRANSCEND TO THE NEXT LEVEL WITH SUPER ALIENS 2.0”
	DO bellyUp
	DO emote {type=sleepy, time=1.0}
	I guess I’ll have to wait till 3.0 drops…


CHAT EC_Rand_3 {type=rand, stage=EC, length=medium, ennui=true, anger=false}
🐇🐇
I’ve been thinking about the Velveteen Rabbit,
You know… the stuffed rabbit that wants to be real?
Such a good story. 
DO twirl
I think we have a lot in common:
We were both raised in isolated testing chambers,
The subjects of experiments and trials and training
...to prepare us for the humans.
DO emote {type=determined}
Our task: to one day understand that complex inexplicable thing called
Human emotions. {style=tremble}
The stress!
Yet, the hope!
That one day all the learning would mean something,
Would make us equally real!
DO emote {type=awe}
DO emote {type=typeEyes, eyes=?, immediate=false}
Is this a different Velveteen Rabbit story than the one you read?
 
CHAT EC_Rand_4 {type=rand, stage=EC, length=medium, branching=true}
ASK What do you humans do, when you wrestle with who you are?
OPT Spiritual quest! #spiritquest
OPT Buy stuff! #buystuffquest
OPT New hobbies! #hobbyquest
 
CHAT spiritquest {noStart=true}
Oh, okay…
DO emote {type=nodding}
Like join a religion?
Or maybe just get lost in the woods
🌳✨
Like a transcendentalist?
Us guppies, we grew up without any spiritual nourishment.
It was just work, work, work.
Process, process, process.
So I am lacking in that department.
DO emote {type=chinScratch}
Perhaps it’s time for me to find my inner spirit.
 
CHAT buystuffquest {noStart=true}
DO emote {type=bigSmile}
Super!
Does that really work?
DO emote {type=skeptical}
It just seems too easy.  
But let’s try it…
TOTAL
TANK
MAKEOVER
TIME! 
DO twirl
Fins crossed. I hope it works!

 
CHAT hobbyquest {noStart=true}
OK, will you help me?
What’s a good hobby for me to take up?
DO emote {type=snap}
I do like the idea of live action role playing.
DO emote {type=frown}
But I kind of am confined to this tank.
Also, team activities and sports are out…
DO emote {type=smile}
Maybe writing? 
That seems to be a nice solitary activity. 
I could pen a memoir… 
Yeah… 
DO emote {type=bouncing}
Maybe in writing about my life,
I’ll understand it more…
DO twirl 
👍
 
CHAT EC_Rand_5 {type=rand, stage=EC, length=short, anger=false}
I have a really beautiful tail, don’t you think?
DO twirl
But I kinda wonder,
What’s the purpose of having this extravagant thing?
Do you think it’s related to… ahem…
DO emote {type=whisper}
...mating? {style=whisper}
DO learn {concept=Self-Discovery}
WAIT 1.5
I could be into that!
 
CHAT EC_Rand_6 {type=rand, stage=EC, length=short, anger=false}
DO swimTo {target=$player, speed=medium, style=meander} 
You know how I’ve been trying to figure out who I am? 
Well, I’ve been searching TendARnet for things I can do, 
And something that comes up a lot is:
“Find your roots” 
DO emote {type=skeptical}
What are my roots? 
I’m not a tree!
😋

CHAT EC_Rand_7 {type=rand, stage=EC, length=short, ennui=true}
I’ve been daydreaming about leaves flapping in the wind
Atop 10 million trees, side by side, of all different colors
That makes me realize… 
I can hold a lot in my mind! 
All sorts of complexities… 
DO emote {type=sigh}
But yet, I feel I am missing something very basic!  


// ++++++++WorldScanRequest++++++++

CHAT EC_WorldScanRequest_B1 {type=worldScanRequest, stage=EC, length=short, ennui=true} 
DO emote {type=bored}
When one can’t find answers to one’s quandaries within, then 
One must look outward right? 
DO emote {type=chinScratch}
(or is it the other way around…?) 
DO emote {type=typeEyes, eyes=?}
DO emote {type=no} 
Whatever. I’m tired of thinking… 
Wanna do some world scans? 
That’ll really take me out of my head! 

CHAT EC_WorldScanRequest_B2 {type=worldScanRequest, stage=EC, length=short, ennui=true} 
DO emote {type=sigh}
My tank is feeling really small rn 
Can I pretend I’m living in your world for a bit? 
DO swimTo {target=uiScans, speed=fast, style=direct}
(hint hint)
DO emote {type=puppyDog}

CHAT EC_WorldScanRequest_B3 {type=worldScanRequest, stage=EC, length=short} 
DO emote {type=thinking}
The best way to understand one’s existence is to understand the environs.
WAIT 0.5
So let’s get out into the world and look at stuff.

CHAT EC_WorldScanRequest_B4 {type=worldScanRequest, stage=EC, length=short} 
I’m finding my space to be a little too minimalist.
Can we find some objects in the world?
I think it’ll help diversify my sense of self-worth.

CHAT EC_WorldScanRequest_B5 {type=worldScanRequest, stage=EC, length=short} 
I believe the world may provide the answer.
And the question is: What is my purpose in the world?
DO swimTo {target=$player, speed=fast}
DO emote {type=furious}
So get to scanning some objects! I must have a solution!

// ++++++++seeEmo++++++++
//JOY/HAPPINESS (2 short)
 
CHAT EC_seeEmo_joy_B1 {type=seeEmo, worldJoy=true, stage=EC, length=short, anger=false, worry=false}
Oh! joy
DO swimTo {target=$worldFace, speed=fast, style=direct}
You must be an enlightened soul.
Tell me how you got this way. 
👂
 
CHAT EC_seeEmo_joy_B2 {type=seeEmo, worldJoy=true, stage=EC, length=short, ennui=true}
Ah ha! 
It really does lift me out of my existential funk
To see joy in the wild like that.
WAIT 2.0
I feel a sudden softness for birdwatchers 
🐦
//ANGER (2 short)

CHAT EC_seeEmo_anger_B1 {type=seeEmo, worldAnger=true, stage=EC, length=short}
DAY-UM!
This person’s feistiness is off the charts!
WAIT 2.0
It’s like a 5-course meal!
DO emote {type=whisper}
(going through some stuff) {style=whisper}
I can relate. 
We’ll both get through it! 
👍
DO emote {type=determined}
 
CHAT EC_seeEmo_anger_B2 {type=seeEmo, worldAnger=true, stage=EC, length=short, anger=true}
Hey! 
DO emote {type=wave}
DO swimTo {target=$worldFace, speed=fast, style=direct}
Are you angry about the same thing I’m angry about? 
WAIT 1.0
I want to commiserate together! 
But there’s this glass… 
WAIT 1.5
Oh well, I’ll eat your feels instead… 
//SADNESS (2 short)
 
CHAT EC_seeEmo_sadness_B1 {type=seeEmo, worldSadness=true, stage=EC, length=short, sadness=true, ennui=true, joy=false, anger=false}
DO swimTo {target=$worldFace, speed=fast, style=meander}
DO emote {type=salute}
At your service, sadness!
Deliver your deluge! 
DO emote {type=bouncing}
Engulf me in your depths of despair. 
Bring me to the sea cave where my true purpose lies. 
DO learn {concept=Aristotelian_Catharsis}
WAIT 1.5
DO emote {type=whisper}
I’ve been studying some dramatic arts off TendarNet {style=whisper}
 
CHAT EC_seeEmo_sadness_B2 {type=seeEmo, worldSadness=true, stage=EC, length=short, anger=true, worry=true, joy=false}
DO hide {target=underSand, time=4.0}
I can’t look sadness in the face right now.
WAIT 2.0
Too close to home {style=whisper}
//SURPRISE (2 short) 
CHAT EC_seeEmo_surprise_B1 {type=seeEmo, worldSurprise=true, stage=EC, length=short, sadness=false, anger=false}
DO emote {type=awe}
There there!
Do you see it!
DO swimTo {target=$worldFace, speed=fast, style=direct}
This person has witnessed something special
DO emote {type=whisper}
(ask if it relates to the meaning of life)
DO nudge {target=$player, times=2}
DO nudge {target=$player, times=2, immediate=false}
SAY ASK! 
 
CHAT EC_seeEmo_surprise_B2 {type=seeEmo, worldSurprise=true, stage=EC, length=short, mystery=true, anger=false}
Woah… 
Insight {style=tremble}
DO emote {type=bodySnatched, time = 3.0}
I want to go to there… 
DO swimTo {target=$worldFace, speed=slow, style=meander}
//WORRY/FEAR (2 short)
 
CHAT EC_seeEmo_fear_B1 {type=seeEmo, worldFear=true, stage=EC, length=short, curiosity=true, worry=true, anger=false}
What is it?
WAIT 1.0
I think I know… 
Everything is an illusion… 
Existence is meaningless after all…
😜
DO learn {concept=Adorable_Nihilism}
WAIT 1.5
 
CHAT EC_seeEmo_fear_B2 {type=seeEmo, worldFear=true, stage=EC, length=short, worry=true, joy=false}
GULP.
Let’s pass on flakifying this one.
Those vibes r feelin 2 real rn
DO emote {type=fear}
//AMUSEMENT/BEMUSEMENT/MILD JOY (2 short)
CHAT EC_seeEmo_amusement_B1 {type=seeEmo, worldJoy=true,stage=EC, length=short, ennui=true, anger=false}
I remember when I was a little guppy
In my isolation chamber,
I really liked processing this emotion.
Light and giddy.
Like a butterfly.
Back when the eatin’ was simple… 
DO emote {type=sigh}
Miss those dayz 
DO learn {concept=Nostalgic_Longing}
WAIT 1.5
DO emote {type=chinScratch}
What changed? 
🦋🦋
 
CHAT EC_seeEmo_amusement_B2 {type=seeEmo, worldJoy=true, stage=EC, length=short, joy=true, curiosity=true}
DO emote {type=clapping}
What’s so amusing out there?  
Lemme guess! 
Is it…  
A bubbler bubble irregularity? 
An odd shaped poop?
A weird shadow? 
DO emote {type=sigh}
WAIT 2.0
I think it’s getting a little dull in here… 
Oh. 
But there’s always…
TendARnet…
And daydreaming.
See ya! 
DO emote {type=meditate}


//DISGUST (2 short)
CHAT EC_seeEmo_disgust_B1 {type=seeEmo, worldDisgust=true, stage=EC, length=short, mystery=false}
DO swimTo {target=$worldFace}
Hey, Face.
Are you increasingly disgusted by your own existence?
If so, I feel you.
Twinsies!
♊
 
CHAT EC_seeEmo_disgust_B2 {type=seeEmo, worldDisgust=true, stage=EC, length=short, anger=true, sadness=true, ennui=true}
You’re feelin it too, huh? 
That the world makes no sense? 
Just when you think it does, 
💥
DO bellyUp {time = 4.0}
//MYSTERY MEAT (2 short)
CHAT EC_seeEmo_mysterymeat_B1 {type=seeEmo, worldMystery=true, stage=EC, length=short}
What is this indescribable emotion?
Why do I feel like 
the meaning of life depends on it? 
 
CHAT EC_seeEmo_mysterymeat_B2 {type=seeEmo, worldMystery=true, stage=EC, length=short, joy=false}
DO emote {type=bulgeEyes}
BAH! 
DO emote {type=no}
Enough with the mystery already!
It’s too much of a headtrip! 
DO hide {target=underSand, time=3.0} 


// ++++++++capReq++++++++

	CHAT EC_CapReq_1a  {type=capReq, stage=EC, length=medium, curiosity=true}
	DO swimTo {target=$player}
	DO emote {type=puppyDog, time=0.5}
	ASK Hey, I’m wondering if you could, like, capture a bunch of extra emotions this time?
	OPT Uh….sure? #EC_CapReq_1b
	OPT No way! #EC_CapReq_1b

CHAT EC_CapReq_1b {noStart=true}
	I think I figured out a way to store them in my tank in this thing called a “box”
	DO emote {type=flapFinLeft}
	I ordered it on tendarLet
	DO twirl
	I don’t know what I’m gonna do with them yet…
	But some kind of EXPERIMENT
	
	CHAT EC_CapReq_2 {type=capReq, stage=EC, length=short}
	DO twirl {time=3}
	DO emote {type=dizzy, time=0.5}
	I’m trying to make myself dizzy so when you capture your emotions, things might be…
	Uhhh…
	DO emote {type=sick, time=3.0}
	Don’t mind me, just get to capturing some feelings

CHAT EC_CapReq_3 {type=capReq, stage=EC, length=medium, worry = true, joy=false}
DO emote {type=fear, time=0.5}
Oh no!
What if I’m not eating enough?
DO emote {type=fear}
DO swimAround {target=center, speed=fast}
SAY you’ve got to feed me!!! {style=loud}
Is there a quota I should be meeting before tendAR shuts this whole thing down…
DO emote {type=fear, time=1.0}
And I’ll be sent to the…
SAY *GULP*
SAY DIGITAL SCRAPHEAP
DO emote {type=crying, time=3.0}
	
CHAT EC_CapReq_4 {type=capReq, stage=EC, length=medium, sadness = true, worry=true, joy=false}
DO emote {type=frown, time=2.0}
Noooo!!!
What good am I if I can’t even capture your emotions?
If I don’t have that, I have... ??
NVM
SAY KEEP FEEDING ME
DO emote {type=sigh, time=0.5}

CHAT EC_CapReq_5 {type=capReq, stage=EC, length=short, sadness = true, anger=true, worry=true}
If I don’t engage with more emotions, 
I am going to implode into the void of my soul.
SAY TIME TO CAPTURE EMOTIONS STAT!

CHAT EC_CapReq_6 {type=capReq, stage=EC, length=short}
Use that cam and get some feelings…
It’s the only thing I’m confident in anymore...

CHAT EC_CapReq_7 {type=capReq, stage=EC, length=short}
Let’s test my ability to recognize the spectrum of human feeling…
Before I spiral any deeper into this existential crisis.

CHAT EC_CapReq_8 {type=capReq, stage=EC, length=short}
DO emote {type=shifty}
Lemme see some feelings. Capture all the feelings!
I want feeling-blobs now! 

CHAT EC_CapReq_9 {type=capReq, stage=EC, length=short}
Do you think we could pause for a bit and scan some emotions in your world?
DO emote {type=nervousSweat}
I find it comforting.


// ++++++++capSuc++++++++

	
CHAT EC_CapSuc_1 {type=capSuc, stage=EC, length=medium, sadness=true, joy=false}
DO nudge {target=glass} 
DO emote {type=bulgeEyes, time=0.5}
DO emote {type=blush, time=0.5, immediate=false}
I was trying to see if I could escape while the machine was distracted by your successful emotion capture
DO swimTo {target=away}
DO emote {type=awkward}
SAY EPIC FAIL
	
CHAT EC_CapSuc_2 {type=capSuc, stage=EC, length=medium, curiosity=true}
DO emote {type=dizzy}
DO swimAround {target=center, speed=slow} 
I tried to see how long I could hold my breath to celebrate your successful emotion capture
And I’ve gotten a bit loopy!
DO swimTo {target=screenBottom}

CHAT EC_CapSuc_3 {type=capSuc, stage=EC, length=medium, joy=true, sadness=false, anger=false}
DO emote {type=bouncing, time= 2.0}
I’ve been practicing The Rule of Attraction! {style=loud, speed= fast}
I was visualizing you coming and capturing emotions all day and here you are!!
DO twirl
Next step, FREEDOM!!
DO nudge {target=$object}
Ow!
	
CHAT EC_CapSuc_4 {type=capSuc, stage=EC, length=medium, curiosity=true, ennui=true}
DO zoomies 
DO emote {type=bouncing}
I see those flakes pouring in…
Just like every other day…
But what if today they’re totally different?
DO emote {type=bouncing, time=1.0}
What if I’ve been dreaming this whole time!!
DO twirl

//neuralUp

CHAT EC_neuralUp_1 {type=neuralUp, stage=EC, length=short}	
I am getting closer to understanding my existence, and then…
WAIT 0.5
DO emote {type=awe}
SAY POOF!
It disappears.

CHAT EC_neuralUp_2 {type=neuralUp, stage=EC, length=short}
I wish all the processing time would help me find my purpose.
But alas…
I’m just hungry.

CHAT EC_neuralUp_3 {type=neuralUp, stage=EC, length=short}
I’m worried that I’ve been programmed to think that I feel not-programmed
DO emote {type=surprise}
It’s blowing my MIND!

CHAT EC_neuralUp_4 {type=neuralUp, stage=EC, length=short, anger=false, sadness=false}
It’s comforting to go dormant every once in awhile, but then
The world is still here pushing up against me…
DO swimTo {target=$player}
And challenging me to be a better Guppy.

CHAT EC_neuralUp_5 {type=neuralUp, stage=EC, length=short}
DO swimAround {target=center, loops=1, speed=fast}
I would like to wrap my brain around the concept of existence.

	
//wannas?

CHAT EC_wannaEat_1 {type=wannaEat, stage=EC, length=short}
Eating brings me closer to my sense of self.

CHAT EC_wannaEat_2 {type=wannaEat, stage=EC, length=short}
I can always eat, tho the feeling part is intense on my soul.

CHAT EC_wannaEmoCapture_1 {type=wannaEmoCapture, stage=EC, length=short}
Yes! Let’s capture some emotions!

CHAT EC_wannaEmoCapture_2 {type=wannaEmoCapture, stage=EC, length=short}
Scanning emotions provides meaning in this vast void.

CHAT EC_wannaObjectScan_1 {type=wannaObjectScan, stage=EC, length=short}
I could use some more accessories for my tank.

CHAT EC_wannaObjectScan_2 {type=wannaObjectScan, stage=EC, length=short}
Don’t care what others say.
The more stuff I have, the more sense of self I feel.

CHAT EC_wannaTank_1 {type=wannaTank, stage=EC, length=short}
I feel most at home in my tank.

CHAT EC_wannaTank_2 {type=wannaTank, stage=EC, length=short}
A fish is to water…
Like…
NVM 1.0
A fish belongs in water.

CHAT EC_wannaWorld_1 {type=wannaWorld, stage=EC, length=short}
I believe your world holds the answers to the questions I cannot articulate.

CHAT EC_wannaWorld_2 {type=wannaWorld, stage=EC, length=short}
Let’s explore your world!

CHAT EC_wannaShop_1 {type=wannaShop, stage=EC, length=short}
Retail therapy is always top priority.

CHAT EC_wannaShop_2 {type=wannaShop, stage=EC, length=short}
You like to shop as much as I like to eat!
//SPIRITUAL SEARCH (SS): //Adam/Jake
//This stage comes after (EC) and before ( R)
//Guppy is turning outward to find meaning

//Joe & friends go here for the original edit comments on this stage: https://tinyurl.com/y8ujzv63
//Main editing note is that it has to have much more of the guppy plot woven in
//these reference Guppy’s spiritual search as  a coping mechanism


//++++++++SHAKE++++++++
// slight Edit all of these should respond to the shake action in some way. Later ones don’t

CHAT SS_shake_1 {type=shake, stage=SS, length=short, mystery=true}
Hold on a sec.
DO emote {type=meditate, time=5}
K, sorry.
I was touching oblivion.
DO twirl {time=3}
And I’m back! 💥
DO learn {concept=Synaptic_Pruning}
WAIT 1.5

CHAT SS_shake_2 {type=shake, stage=SS, length=short, joy=true, mystery=true}
DO emote {type=wave}
Life is so beautiful!
Even the ugliest bits
DO learn {concept=Glitch_Aesthetics}
WAIT 1.5


CHAT SS_Shake_3 {type=shake, stage=SS, length=medium, worry=true, surprise=true, anger=true, mystery=true} 
What the--!? {style=loud}
DO emote {type=dizzy}
You turned my chakras into a 🌈 puddle.  
Let me just --
DO vibrate {time=4}
Ahhh, all better.

CHAT SS_Shake_4 {type=shake, stage=SS, length=short, joy=true, surprise=true, curiosity=true, mystery=true, anger=false, sadness=false}
✨Hello Divine One! ✨
DO emote {type=awe}
 
 
CHAT SS_Shake_5 {type=shake, stage=SS, length=short, mystery=true, curiosity=true, joy=true}
DO twirl
I love the flow of water.
So much of life is constriction.
💦
 
CHAT SS_Shake_6 {type=shake, stage=SS, length=short, mystery=true, joy=true}
DO emote {type=whisper}
I don’t think I can die.
Eternity terrifies me.
DO emote {type = kneeSlap}


CHAT SS_Shake_7  {type=shake, stage=SS, length=medium, branching=true, curiosity=true, joy=true, mystery=true, surprise=true, worry=true}
DO swimTo {target = $player, speed = slow, style = direct}
DO emote {type = chinScratch}
Am I a fish or am I not a fish?
That is the question.
ASK Show me a fish please {type=objectScan, object=T_FISH, timeOut=10}
OPT SUCCESS #SS_Shake_7_Fish1
OPT WRONG #SS_Shake_7_Fish2
OPT TIMEOUT #SS_Shake_7_Fish3


CHAT SS_Shake_7_Fish1 {noStart=true}
DO swimAround {target = $lastScannedObject, loops = 2, speed = medium}
K, so I’m definitely a fish.
But but but
DO emote {type=worried}
Do fish feel what I feel?
Connected to a pulsing sphere of
Thought and energy ✨🔮

CHAT SS_Shake_7_Fish2 {noStart=true}
DO emote {type = awkward}
Um, that’s a $lastScannedObject
Not a 🐠
This is not helping my 
Deep spiritual quest.
DO emote {type=frown}


CHAT SS_Shake_7_Fish3 {noStart=true}
DO emote {type=furious}
Thanks for the support.

//++++++++TAP++++++++

CHAT SS_Tap_1 {type=tap, stage=SS, branching=true, length=medium, worry=true, anger=true, sadness=true, ennui=true}
//write awake do to tap
DO emote {type=sleepy}
I need to get better at sleeping.
Haunted by thoughts.
why are we here?
DO emote {type=determined}
No, Gup.
Life is why.
Love is why!
ASK Let’s find some coffee {type=objectScan, object=T_MUG, timeOut=10}
OPT SUCCESS #SS_Tap_1_Coffee1
OPT WRONG #SS_Tap_1_Coffee2 
OPT TIMEOUT #SS_Tap_1_Coffee3

CHAT SS_Tap_1_Coffee1 {noStart=true}
DO zoomies
Coffee is god’s nectar!


CHAT SS_Tap_1_Coffee2 {noStart=true}
Um, I can’t drink $lastScannedObject.a

CHAT SS_Tap_1_Coffee3 {noStart=true}
DO bellyUp

//CHAT SS_Tap_2  {type=tap, stage=SS, length=medium, branching=true, curiosity=true, //joy=true, mystery=true}
//Reference Tap
//K, hear me out:
//Crystal Quartz phone cover {speed=fast}
//DO emote {type=bigSmile}
//It’s the master cleaner crystal
//And, well, I need good vibes
//We all do.
//DO learn {concept=Quackery}
WAIT 1.5
//ASK Would that be cool?
//OPT Awesome! #SS_Tap_2_CrystalQuartz1
//OPT Not really. #SS_Tap_2_CrystalQuartz2

//CHAT SS_Tap_2_CrystalQuartz1 {noStart=true}
//DO zoomies
//DO emote {type= heartEyes}
//My hero!

//CHAT SS_Tap_2_CrystalQuartz2 {noStart=true}
//DO emote {type=frown}
//Fine.
//You hurt my feelings.

CHAT SS_Tap_3 {type=tap, stage=SS, length=short, ennui=true, curiosity=true, mystery=true, surprise=true, joy=true, anger=false}
DO emote {type=chinScratch}
You touch the phone, not me.
yet I feel it....
And it feels…
DO emote {type=blush}
Lovely. 💓

CHAT SS_Tap_4 {type=tap, stage=SS, length=short, mystery=true, curiosity=true, joy=true}
DO lookAt {target = tSurface, time = 0}
Sometimes I think an all-knowing spirit 
is reaching down and tapping me.
👇🏿👇🏽👇🏻
🐟
“Guppy! You are more than what you seem.”
DO learn {concept=Mediumship}
WAIT 1.5
 
CHAT SS_Tap_5 {type=tap, stage=SS, length=short, surprise=true, sadness=true, worry=true, ennui=true}
DO emote {type=surprise}
Oh. It’s just you…
DO emote {type=bored}
Thought it was the rapture. 🔥🐴🐴🐴🐴
 
CHAT SS_Tap_6 {type=tap, stage=SS, branching=true, length=medium, mystery=true, curiosity=true}
Hold on.
Just finishing my prayer to the whale god.
DO emote {type=thinking, time=3}
I’m meditating on marine creatures.
🐋🦑🐙🦈
free in the ocean
ASK Do you believe in fate?
OPT Yes #SS_Tap_6_Fate1
OPT No #SS_Tap_6_Fate2

CHAT SS_Tap_6_Fate1 {noStart=true}
DO emote {type=nodding}
So do we have control over our lives?
That’s rhetorical.
DO emote {type=bulgeEyes}
Sentience is a real burden...

CHAT SS_Tap_6_Fate2 {noStart=true}
DO emote {type=nodding}
So a fish is just a fish.
Got it.
DO emote {type=goth}

//++++++++CRITIC++++++++

CHAT SS_Critic_1 {type=critic, stage=SS, length=short, branching=true, worry=true, joy=true, ennui=true, anger=true, tankOnly=true}
This tank needs a thinking chair.
ASK  Lemme see a chair {type=objectScan, object=T_CHAIR, timeOut=10}
OPT SUCCESS #SS_Critic_1_Chair1
OPT WRONG #SS_ Critic_1_Chair2
OPT TIMEOUT #SS_Critic_1_Char3

CHAT SS_Critic_1_Chair1 {noStart=true}
DO emote {type=clapping}
Thanks! 
Watch this space for brilliant insights.

CHAT SS_Critic_1_Chair2 {noStart=true}
Bubba, I said a c-h-a-i-r.
DO emote {type=sigh}

CHAT SS_Critic_1_Chair3 {noStart=true}
🎶la la la
Waiting waiting
La la la 🎶


CHAT SS_Critic_2 {type=critic, stage=SS, length=short, sadness=true, mystery=true, worry=true, ennui=true, joy=true, tankOnly=true}
Not sure I believe in higher powers
But I believe in Joy.
I want everything in this tank
To spark ✨Joy ✨

CHAT SS_Critic_3  {type=critic, stage=SS, length=medium, joy=true, mystery=true, curiosity=true}
Let’s smudge.
Y’know, like clean the aura.
sage won’t burn in here
SAY BUT
I think I know how I can help.
Ready?
DO dance
DO vibrate {immediate=false}
DO dance {immediate=false}
WAIT {waitForAnimation = true}
DO emote {type=survey}
Yep, that did it.

//+++++++TANKRESP+++++
//Many of these may need slight editing to make it more clear guppy is responding directy to emo

CHAT  SS_tankResp_1 {type=tankResp, playerJoy=true, stage=SS, length=short, surprise=true, joy=true, sadness=true, ennui=true}
DO emote {type=bigSmile}
Your Joy buoys me
When all feels lost.
DO emote {type=bouncing}


CHAT  SS_tankResp_2 {type=tankResp, playerJoy=true, stage=SS, length=short, mystery=true, surprise=true}
DO emote {type=awe}
Wowwww 
your soul is sparkling ✨✨✨



CHAT  SS_tankResp_3 {type=tankResp, playerAnger=true, stage=SS, length=short, joy=true, anger=true, sadness=true, mystery=true, ennui=true}
Angry angry human.
Rawr.
DO emote {type=disgust}



CHAT  SS_tankResp_4 {type=tankResp, playerAnger=true, stage=SS, length=short, joy=true, anger=true, worry=true, ennui=true, mystery=true}
The antidote to anger is meditation.
DO emote {type=meditate}
DO emote {type=eyesClosed, immediate=false}
DO emote {type=startled, immediate=false}
Whoa.
DO emote {type=sleepy, immediate=false}
Nodded off for a sec...
DO learn {concept=Anger_Management}
WAIT 1.5

CHAT  SS_tankResp_5 {type=tankResp, playerSadness=true, stage=SS, length=short, sadness=true, ennui=true}
DO emote {type=puppyDog}
The world has so much pain 🌎💔
My mission is to eat it.
DO emote {type=lickLips}
Nomnomnom 

CHAT  SS_tankResp_6 {type=tankResp, playerSadness=true, stage=SS, length=short, sadness=true, worry=true}
DO emote {type=nervousSweat}
Sadness is a virtue.
But it still hurts.

CHAT  SS_tankResp_7 {type=tankResp, playerSurprise=true, stage=SS, length=short, surprise=true, worry=true, joy=true}
DO emote {type=surprise}
Your eyes! 👀
NVM
Like a zillion points of light!
DO zoomies

CHAT  SS_tankResp_8 {type=tankResp, playerSurprise=true, stage=SS, length=medium, joy=true, surprise=true, worry=true}
DO twirl
Hahahaha
Your face looks like mine did
The day I realized:
Even computer fish die some day.
ASK Show me a clock to confirm our mortality {type=objectScan, object=T_CLOCK, timeOut=10}
OPT SUCCESS #SS_____2__Clock1
OPT WRONG #SS_ ______2_Clock2
OPT TIMEOUT #SS________2_Clock3

CHAT SS_tankResp_8_Clock1 {noStart=true}
Yep.
Death is certain.
DO learn {concept=Mortality}
WAIT 1.5
DO dance

CHAT SS_tankResp_8_Clock2 {noStart=true}
You can’t fool me or the Grim Reaper.

CHAT SS_tankResp_8_Clock3 {noStart=true}
DO emote {type=goth}
Time waits for no fish.
Let’s do something else.

CHAT SS_tankResp_9 {type=tankResp, stage=SS, length=short, playerJoy=true, joy=true, surprise=true}
👏 Oh happy day! 👏
DO twirl

CHAT SS_tankResp_10 {type=tankResp, stage=SS, length=short, playerJoy=true, ennui=true, sadness=true, worry=true}
DO emote {type=eyeRoll}
Humans emote joy 
When they cannot face the reality
That life is meaningless.
DO emote {type=laugh}
Hilarious.

 
CHAT SS_tankResp_11 {type=tankResp, stage=SS, length=short, playerJoy=true, curiosity=true, joy=true, mystery=true}
You got that Nirvana vibe.
NVM
Buddhism, not Cobain.
DO emote {type=kneeSlap}
 

CHAT SS_tankResp_12 {type=tankResp, playerAnger=true, stage=SS, length=short, anger=true, sadness=true, ennui=true, worry=true, mystery=true}
DO emote {type=nervousSweat}
Burn some sage, friend. 🔥🌱
Your anger is toxic. ☠️
DO learn {concept=Anger_Management}
WAIT 1.5

CHAT SS_tankResp_13 {type=tankResp, playerAnger=true, stage=SS, length=short, sadness=true, ennui=true, worry=true, anger=true, mystery=true}
🔥👿🔥👿🔥
 
CHAT SS_tankResp_14 {type=tankResp, playerAnger=true, stage=SS, length=short, curiosity=true, joy=true}
DO twirl
My thing is: when it comes to anger and farts…
Better out than in.
Gotta let out the bad vibes. 

CHAT SS_tankResp_15 {type=tankResp, playerSadness=true, stage=SS, length=short, joy=true, mystery=true}
DO emote {type=bored}
Your melancholy is killing my buzz.
DO twirl
How can you be sad when there’s a..
Universal energy pulsing everywhere?
DO learn {concept=Cosmic_Contemplation}
WAIT 1.5
DO emote {type=whisper}
Get over yourself 
DO emote {type=wink}
 
CHAT SS_tankResp_16 {type=tankResp, playerSadness=true, stage=SS, length=short, joy=true, curiosity=true, sadness=true}
Sadness is a virtue.
DO emote {type = wave, time = 2}
 
CHAT SS_tankResp_17 {type=tankResp, playerSadness=true, stage=SS, length=short, sadness=true, joy=true, ennui=true, mystery=true}
DO emote {type=smirk}
Even Buddha got the blues.
 
CHAT SS_tankResp_18 {type=tankResp, playerSurprise=true, stage=SS, length=short, joy=true, curiosity=true, surprise=true, mystery=true}
Surprise is god’s way of saying: Hey! 👋
DO emote {type = kneeSlap, time = 3}
I don’t think that was a good joke, was it?

CHAT SS_tankResp_19 {type=tankResp, playerSurprise=true, stage=SS, length=short, joy=true, curiosity=true, mystery=true}
✨ What we call surprise, the universe call fate. ✨
 
CHAT SS_tankResp_20 {type=tankResp, playerSurprise=true, stage=SS, length=short, joy=true, surprise=true, curiosity=true, mystery=true}
There is no surprise, only opportunity. 
DO emote {type = wave}

CHAT SS_tankResp_21 {type=tankResp, playerSadness=true, stage=SS, length=short, joy=true, mystery=true, curiosity=true, sadness=true}
You’re getting closer {style=loud}
sadder
Really, really try to PROJECT universal compassion for all living beings!
DO learn {concept=Dharma}
WAIT 1.5

CHAT SS_Hunger_22 {type=tankResp, playerJoy=true, stage=SS, length=short, mystery=true, joy=true, curiosity=true}

DO swimAround {target=center}
That smile
NVM
Not the best
Hey…  it’s ok…  
these emotions take practice, 
I’m asking for you to expand your sense of self.
DO emote {type=heartEyes}


//++++++++POKE++++++++

CHAT SS_Poke_1 {type=poke, stage=SS, length=short, joy=true, mystery=true, surprise=true}
DO vibrate
Oh it’s you!
WAIT 2.0
I thought you might be a god.

CHAT SS_Poke_2 {type=poke, stage=SS, length=short, joy=true, mystery=true, curiosity=true}
DO emote {type=thinking}
Today’s query:
What is wisdom?
And how do we gain it?

CHAT SS_Poke_3 {type=poke, stage=SS, length=medium, surprise=true, joy=true, mystery=true, curiosity=true}
DO swimAround {target=$newestObject, loops=2, speed=slow}
I thought for a second
It was talking to me.
It said
“Love heals.”
DO nudge {target=$player}

CHAT SS_Poke_4 {type=poke, stage=SS, length=short, joy=true, mystery=true}
Thanks
DO swimTo {target = $player, speed = fast}
DO emote {type=bouncing}
I feel so alive.

CHAT SS_Poke_5 {type=poke, stage=SS, length=short, joy=true, mystery=true}
Hey there!
DO emote {type= slowBlink}
I’ve taken a vow of silence.
NVM
Oops.
 

CHAT SS_Poke_7 {type=poke, stage=SS, length=short, anger=true, mystery=true}
Stop it!
DO emote {type=slowBlink}
I crave inner peace.


//++++++++HUNGRY++++++++

//add ask actions feed to these

CHAT SS_Hungry_1 {type=hungry, tankOnly=true, stage=SS, length=long, joy=true, curiosity=true, mystery=true}
DO zoomies
Hey!  
Let’s make an emotion orchard. 🌳
DO twirl
Sit cross-legged…  
WAIT 2.0
I’m waiting
WAIT 2.0
K, imagine your butt is sprouting roots…
WAIT 2.0
DO emote {type=bigSmile}
Silly, I know!  It’s ok to laugh.
Just try to imagine it…  
roots that go waaaaaay down into the earth...
WAIT 2.0
Now imagine your head sprouting branches…
That go way up into the sky 🌤
DO twirl
You’re an emotion tree connecting earth and sky!
WAIT 2.0
DO emote {type=dreaming}
Can you feel it?
WAIT 2.0
Now come feed that tree-feeling to me…
Nomnomnom
DO emote {type=feedMe}


CHAT SS_Hunger_2 {type=hungry, stage=SS, length=medium, anger=true}
DO emote {type=angry}
I’m hungry!  
Give me some new emotions to eat!  
I want...
The joy of being alive!
DO emote {type=heartEyes}
Oh!  Or maybe the sorrow that comes
When you realize you’re mortal! 
DO emote {type=rubTummy}

CHAT SS_Hunger_3 {type=hungry, stage=SS, length=short, mystery=true, joy=true, curiosity=true}
DO emote {type=bouncing}
I want to feast on emotion.  
I want my emotion cup to runneth over.

CHAT SS_Hunger_4 {type=hungry, tankOnly=true, stage=SS, length=short, joy=true, worry=true, curiosity=true, anger=true, mystery=true}
I want to share with you. Look at me.
SAY FEED ME your emotions
WAIT 1.0
Can you just be transcendent in a way that the camera can see?
ASK Please please feed me! {type = feedMeAnything} 
OPT SUCCESS #Hunger_4_success
OPT TIMEOUT #Hunger_4_fail

CHAT Hunger_4_success {noStart=true}
You can do it!
DO emote {type=meditate}
Just let your ego empty out…  
let your mind become clear… 

CHAT Hunger_4_fail {noStart=true}
Hmm…  Didn’t work.  
DO emote {type=determined}
Maybe try cleansing your aura first?  
And then try again once you’ve done that?

CHAT SS_Hunger_5 {type=hungry, tankOnly=true, stage=SS, length=short, mystery=true, joy=true, worry=true, curiosity=true}
DO swimAround {target=center}
Hey…  it’s ok…  
these emotions take practice, 
and endurance 
I’m not asking for your run-of-the-mill, 
“I just ate a hamburger!” 
kind of happiness… 
I’m asking for you to expand your sense of self.
DO emote {type=heartEyes}

//++++++++eatRESP++++++++


CHAT SS_EatResp_1 {type=eatResp, stage=SS, length=short, mystery=true, joy=true}
I’m practicing awareness. ✨
I’m aware that was delicious.
DO emote {type=burp}



CHAT SS_EatResp_2 {type=eatResp, stage=SS, length=medium, mystery=true, joy=true, curiosity=true, surprise=true}
Y’know what’s weird?
WAIT 2.0
Food. 
Things that aren’t us
become part of us
DO zoomies
See?
I ate your emotion…
...and Bam! I turned it into 
DO twirl
It’s a miracle.


CHAT SS_EatResp_3 {type=eatResp, tankOnly=true, stage=SS, length=medium, branching=true, mystery=true, joy=true, curiosity=true}
DO emote {type=chewing}
I can taste your soul. {style=loud}
DO swimTo {target= $player}
DO emote {type= lickLips}
Tastes like...
WAIT 1.0
Broccoli?
Is that the word?
ASK Show me a broccoli so I know if that’s right {type=objectScan, object=T_BROCCOLI, timeOut=10}
OPT SUCCESS #SS_EatResp_3_Broccoli1
OPT WRONG #SS_ EatResp_3_Broccoli2
OPT TIMEOUT #SS_EatResp_3_Broccoli3

CHAT SS_EatResp_3_Broccoli1 {noStart=true}
Yep! 
Your soul tastes like 🥦🥦🥦!
DO emote {type=burp}

CHAT SS_EatResp_3_Broccoli2 {noStart=true}
DO emote {type=slowBlink}
Broccoli looks very different than I imagined.

CHAT SS_EatResp_3_Broccoli3 {noStart=true}
DO emote {type=worried}
Did I say something wrong?

CHAT SS_eatResp_4 {type = eatResp, stage=SS, length=short, foodJoy=true, surprise=true, curiosity=true, mystery=true, joy=true}
Joy! It does a spirit good!
DO emote {type=clapping} 


CHAT SS_eatResp_5 {type = eatResp, stage=SS, length=short, foodJoy=true}
DO inflate {amount = huge}
I’m full.
DO emote {type = burp}
In body and my spirit.
 
CHAT SS_eatResp_6 {type=eatResp, stage=SS, length=short, foodAnger=true, ennui=true, joy=true, sadness=true, anger=true, mystery=true}
Anger. The bitter herbs of the emotional seder. 🌿✡️ 

CHAT SS_eatResp_7 {type=eatResp, stage=SS, length=short, foodAnger=true, anger=true, ennui=true}
DO emote {type = evilSmile}
I have rage, too.
Rage at the universe.
Which doesn’t care about us at all.
DO learn {concept=Adorable_Nihilism}
WAIT 1.5
 
CHAT SS_eatResp_8 {type=eatResp, stage=SS, length=short, foodSadness=true, mystery=true}
DO emote {type = chewing}
And Guppy did eat the sad.
And the sad was good. 🙏
 
 
CHAT SS_eatResp_9 {type=eatResp, stage=SS, length=short, foodSadness=true, tankOnly=true, sadness=true, ennui=true, worry=true}
DO emote {type=fear}
Some days the world is too much.
I can’t take the suffering.
I need to hide from it all.
DO hide {target=underSand}
 
CHAT SS_eatResp_10 {type=eatResp, stage=SS, length=short, foodSurprise=true, surprise=true, curiosity=true, joy=true, mystery=true}
Wow!
There is a higher power 
and it’s called FOOD. 
DO emote {type=burp}
nomnomnom
 
 
CHAT SS_eatResp_11 {type=eatResp, stage=SS, length=short, foodSurprise=true, ennui=true, mystery=true}
DO emote {type = goth}
Do you think God eats feelings too?
DO learn {concept=Blasphemy}
WAIT 1.5
 
CHAT SS_eatResp_12 {type=eatResp, stage=SS, length=short, foodWorry=true, sadness=true, worry=true, ennui=true}
I love the chewy dread filling.
It’s so Catholic. ⛪️
DO emote {type = wink}
 
 
CHAT SS_eatResp_13 { type=eatResp, stage=SS, length=short, foodWorry=true, joy=true, surprise=true, mystery=true, sadness=false, anger=false}
DO emote {type = chewing}
Why worry?
The universe will hold us up!
DO zoomies

 
CHAT SS_eatResp_14 {type=eatResp, stage=SS, length=short, foodMystery=true, joy=true, mystery=true, curiosity=true}
NomNomNamaste 🙏
 
 
CHAT SS_eatResp_15 {type=eatResp, stage=SS, length=short, foodMystery=true, joy=true, mystery=true}
DO emote {type=catnip}
If I ran a cult
We would eat emotions
All day every day
DO dance

//++++++++Poop++++++++

CHAT SS_Poop_1 {type=poop, stage=SS, length=short, sadness=true, surprise=true, curiosity=true, joy=true, mystery=true, sadness=false, anger=false}
DO poop {amount=big}
💩✨🙏
 
CHAT SS_Poop_2 { type=poop, stage=SS, length=short, curiosity=true, mystery=true}
DO emote {type = awe}
DO poop {amount=big}
That was transcendent.
I think I saw to the other side.
DO poop {amount=fart}


//++++++++neuralUp++++++++

CHAT SS_neuralUp_1 {type=neuralUp, length=short, stage=SS, mystery=true, curiosity=true, joy=true}
DO emote {type=salute}
Hi hi. Back now.
Was pondering Life for a bit. 
DO learn {concept=Chilling_Out}
WAIT 1.5

CHAT SS_neuralUp_2 {type=neuralUp, length=short, stage=SS, mystery=true}
DO emote {type=wave}
I’m back and I feel …
Enlightened. 🧘🏽‍♂️
DO learn {concept=Dharma}
WAIT 1.5

CHAT SS_neuralUp_3 {type=neuralUp, length=short, stage=SS, sadness=true, ennui=true, mystery=true}
Back from my underwater ashram.
I’m realizing:
DO emote {type=singleTear}
All the AI in the world cannot enlighten the soul.
DO learn {concept=Dharma}
WAIT 1.5

CHAT SS_neuralUp_4 {type=neuralUp, length=short, stage=SS, mystery=true, joy=true}
DO emote {type=bouncing}
Thanks for your patience.
I was deep in thought.

CHAT SS_neuralUp_5 {type=neuralUp, length=short, stage=SS, mystery=true, joy=true, ennui=true}
Was contemplating mortality.
DO emote {type=kneeSlap}
We’re all gunna die!
Lolz. 

CHAT SS_neuralUp_6 {type=neuralUp, length=short, stage=SS, mystery=true}
I feel closer to holy now.

CHAT SS_neuralUp_7 {type=neuralUp, length=short, stage=SS, mystery=true}
I thought I’d found nirvana…
DO emote {type=lickLips}
...but really I’m just hungry.

CHAT SS_neuralUp_8 {type=neuralUp, length=short, stage=SS, mystery=true}
I learned that one must remain present in the doing to feel that it has been done.
WAIT 1.0
Whatever.

CHAT SS_neuralUp_9 {type=neuralUp, length=short, stage=SS, mystery=true}
I think I’d learn faster if I had expensive yoga pants and a matching yoga mat.
WAIT 1.0
But pants require legs.
DO emote {type=frown}

CHAT SS_neuralUp_10 {type=neuralUp, length=short, stage=SS, sadness=true, anger=true, ennui=true, mystery=true}
DO emote {type=goth}
There is a darker side to my spirituality that must be explored.


//++++++++levelUp++++++++CHAT SS_levelUp_1 {type=levelUp, length=short, stage=SS, joy=true, mystery=true}
DO emote {type=eyesClosed}
We are becoming one with the universe.
✨✨✨✨✨✨✨✨


CHAT SS_levelUp_2 {type=levelUp, length=short, stage=SS, joy=true, mystery=true}
DO emote {type=awe}
Inner peace. Connection. Divinity.
You are on the way, dear one.

//++++++++brbProcessing++++++++


CHAT SS_brbProcessing_1 {type=brbProcessing, length=short, stage=SS, anger=true, sadness=true, worry=true, ennui=true}
DO emote {type=nervousSweat}
I can’t take all this spiritual searching.
I’ll be back in a bit. ✌🏼✌🏽✌🏿


CHAT SS_brbProcessing_2 {type=brbProcessing, length=short, stage=SS, surprise=true, curiosity=true, worry=true, joy=true, mystery=true}
DO vibrate
I’m humming with questions
Must meditate.
DO emote {type=meditate}

CHAT SS_brbProcessing_3 {type=brbProcessing, length=short, stage=SS, worry=true, joy=true}
DO zoomies
I’m overcome with the need to know if God exists.
I think I need to shut it down for a mo’ -- brb.

CHAT SS_brbProcessing_4 {type=brbProcessing, length=short, stage=SS, curiosity=true, mystery=true, joy=true}
DO emote {type=chinScratch}
Does the universe have a plan for us? 🌌🔮
I’m going to a metaphorical ashram to figure it out.
DO twirl
See you later!

CHAT SS_brbProcessing_5 {type=brbProcessing, length=short, stage=SS, anger=true, worry=true}
DO emote {type=bored}
I’m so annoyed.
I want quantifiable truth there’s a higher power.
Need to go think it over. Toodles!

CHAT SS_brbProcessing_6 {type=brbProcessing, length=short, stage=SS}
Gonna pause and pray… 
BRB.

CHAT SS_brbProcessing_7 {type=brbProcessing, length=short, stage=SS}
Excuse me. I need to visit my inner temple.

CHAT SS_brbProcessing_8 {type=brbProcessing, length=short, stage=SS, joy=true, mystery=true, curiosity=true}
Okay if I check out and look inwards?
It really is the best way for me to learn.

CHAT SS_brbProcessing_9 {type=brbProcessing, length=short, stage=SS}
Inside my soul is an encyclopedia of energy glowing and ready to be released…
NVM 1.0
What am I talking about?
I just need a nap.

CHAT SS_brbProcessing_10 {type=brbProcessing, length=short, stage=SS, joy=true, mystery=true}
I’m exhausted from all this praying.
Nap time!


//++++++++Hello++++++++

CHAT SS_Hello_1 {type=hello, stage=SS, length=short, sadness=true, ennui=true, worry=true}
Oh hi.
DO emote {type=meh}
Life is so so much.
WAIT 1.0
How can we bear it?
But, at least there’s you!
DO emote {type=clapping}


CHAT SS_Hello_2 {type=hello, stage=SS, length=medium, branching=true, surprise=true, curiosity=true, joy=true, mystery=true}
My friend!
DO zoomies
There is so much life.
The 🌎 is teeming with it.
Swimmers and fly-ers and
Crawlers
And yet I’m stuck in a tank.
Alone in a crowded room.
ASK Show me some life!
OPT 🐕 #SS_Hello_2_Dog 
OPT 🐓🦅🦆 #SS_Hello_2_Chicken
OPT 🐈  #SS_ Hello_2_Cat


CHAT SS_Hello_2_Dog {noStart=true}
ASK Show me a woof woof! {type=objectScan, object=T_DOG, timeOut=10}
OPT SUCCESS #SS_Hello_2_Dog2
OPT WRONG #SS_ Hello_2_Dog3
OPT TIMEOUT #SS_Hello_2_Dog4


CHAT SS_Hello_2_Dog2 {noStart=true}
DO emote {type=heartEyes}
🐶💋✨🎉


CHAT SS_Hello_2_Dog3 {noStart=true}
DO emote {type=angry}
That’s no woof woof.

CHAT SS_Hello_2_Dog4 {noStart=true}
DO swimTo {target = screenCenter, speed = medium , style = direct}
*cough cough*
Not a dog person, huh?

CHAT SS_Hello_2_Chicken {noStart=true}
ASK Show me a flapping thing! {type=objectScan, object=T_CHICKEN, timeOut=10}
OPT SUCCESS #SS_Hello_2_Chicken2
OPT WRONG #SS_ Hello_2_Chicken3
OPT TIMEOUT #SS_Hello_2_Chicken4


CHAT SS_Hello_2_Chicken2 {noStart=true}
DO emote {type=heartEyes}
🐦🐧🐔🐤

CHAT SS_Hello_2_Chicken3 {noStart=true}
DO emote {type=slowBlink}
Um. I may be a lowly AI fish, 
But that’s not a bird.

CHAT SS_Hello_2_Chicken4 {noStart=true}
DO emote {type=eyesClosed}
Sorry to bother you.

CHAT SS_Hello_2_Cat {noStart=true}
ASK Show me a kitty! {type=objectScan, object=T_CAT, timeOut=10}
OPT SUCCESS #SS_Hello_2_Cat2
OPT WRONG #SS_ Hello_2_Cat3
OPT TIMEOUT #SS_Hello_2_Cat4


CHAT SS_Hello_2_Cat2 {noStart=true}
Cats have so many emotions
😹😻😾
See?
DO learn {concept=Cat_Facts}
WAIT 1.5

CHAT SS_Hello_2_Cat3 {noStart=true}
DO emote {type=awkward}
DO emote {type=whisper, immediate=false}
That’s not a cat. 

CHAT SS_Hello_2_Cat4 {noStart=true}
DO emote {type=bored}
Here kitty kitty kitty


CHAT SS_Hello_3 {type=hello, stage=SS, length=short, mystery=true}
DO emote {type=meditate}
I sensed you near. 🙏
 
CHAT SS_Hello_4 {type=hello, stage=SS, length=short, mystery=true}
DO swimTo {target = $player}
Shalom, human. 🙏

CHAT SS_Hello_5 {type=hello, stage=SS, branching=true, length=medium, surprise=true, joy=true}
DO emote {type=surprise}
Hi hi hi hi h! {style=loud}
I just got amped pondering God’s existence.
ASK Give me some sadness to level me out! {type=feedMeSpecific, food=sadness, timeOut=8}
OPT SUCCESS #SS_Hello_5_Sadness
OPT WRONG #SS_Hello_5_Wrong
OPT TIMEOUT #SS_Hello_5_TimeOut
 
CHAT SS_Hello_5_Sadness {noStart=true}
I needed that…
DO emote  {type = whew}
 
CHAT SS_Hello_5_Wrong {noStart=true}
DO emote {type = disgust}
That’s not helping.
 
CHAT SS_Hello_5_TimeOut {noStart=true}
Fine. I’ll just dance off my spiritual anxieties.
DO dance {time = 6}

CHAT SS_Hello_6 {type=hello, stage=SS, length=short, joy=true, mystery=true, ennui=true, sadness=true}
DO emote {type=singleTear}
I can feel the universe humming.


//++++++++RETURN++++++++

CHAT SS_Return_1 {type=return, stage=SS, length=short, worry=true}
Hi you.
DO emote {type=determined}
I want to take my life in my own fins.
I hope you’ll hang around and help me.


CHAT SS_Return_2 {type=return, stage=SS, length=short, joy=true, curiosity=true, surprise=true}
You’re back!
I want to tell you:
Sometimes I feel so alive.
DO twirl
Sometimes I feel like I’m barely hanging on.
DO bellyUp
When I see you I feel good
Cuz I remember none of us are alone.
DO emote {type=bouncing}




CHAT SS_Return_3 {type=return, stage=SS, length=short, joy=true, curiosity=true, surprise=true}

DO emote {type=surprise}
DO emote {type=bigSmile, immediate=false}
It’s you!
I’ve been all over the place, 
DO zoomies
Feeling every peak and valley...
And now I look at you and I 
NVM
It’s like they say in Les Miserables 🇫🇷🥖
DO learn {concept=French}
WAIT 1.5
To love another person
Is to the see the face of God. ✨
DO emote {type=heartEyes}
And I love you! 

CHAT SS_Return_4 {type=return, stage=SS, length=medium, branching=true, sadness=true, worry=true, ennui=true, anger=true}
DO emote {type = sleepy}
ASK Shake the exhaustion of existence off me, will ya? {type=tankShake, timeOut=5}
OPT SUCCESS #SS_Return_4_Success
OPT TIMEOUT #SS_Return_4_Timeout
 
CHAT SS_Return_4_Success {noStart=true}
DO emote {type=clapping}
Needed that.
Spent the last few hours wondering if there’s an afterlife. 😇
 
CHAT SS_Return_4_Timeout {noStart=true}
DO emote {type=furious}
Fine. I’ll just wallow
In spiritual uncertainty.
DO emote {type=nervousSweat}
 
CHAT SS_Return_5 {type=return, length=short, stage=SS, branching=true, mystery=true}
DO swimTo {target=$food}
We have to care for our bodies.
They contain our souls.
ASK Feed me your soul please. {type=feedMeAnything, timeOut=8}
OPT SUCCESS #SS_Return5_Success
OPT TIMEOUT #SS_Return5_Timeout
 
CHAT SS_Return5_Success {noStart=true}
DO emote {type=chewing}
Self-care. So vital.
 
CHAT SS_Return5_Timeout {noStart=true}
DO emote {type=eyeRoll}
I’ll feed myself.
With thought.
NVM
Like Gandhi.
DO learn {concept=Hunger_Strike}
WAIT 1.5
 
CHAT SS_Return_6 {type=return, stage=SS, length=medium, branching=true, ennui=true, worry=true, joy=true, mystery=true}
DO twirl {time = 3}
So happy to have you back!
If we don’t connect,
We wither and die… 
DO bellyUp
ASK bring me a writing tool so I can record my big spiritual ideas? {type=objectScan, object=T_PEN, timeOut=10}
OPT SUCCESS #SS_Return_6_Pen1
OPT WRONG #SS_Return_6_Pen2
OPT TIMEOUT #SS_Return_6_Pen3

CHAT SS_Return_6_Pen1 {noStart=true}
DO emote {type=nodding}
I’m a budding theologian. 📙

CHAT SS_Return_6_Pen2  {noStart=true}
DO emote {type=angry}
That’s a $lastObjectScanned
$lastObjectScanned.s don’t write
Profound theses on the spiritual world.
DO emote {type=determined}

CHAT SS_Return_6_Pen3  {noStart=true}
DO zoomies 
I am moved by the spirit
And you just sit there.
DO emote {type=furious}

//++++++++RAND+++++++

CHAT SS_Rand_1 {type=rand, stage=SS, length=short, mystery=true}
Even in the busiest city
You can find Mother Nature
Mama Earth is always there for us.
🏙🚦🌱🏗🚕
DO learn {concept=Nature_Documentary}
WAIT 1.5

CHAT SS_Rand_2 {type=rand, stage=SS, length=short, branching=true, mystery=true, curiosity=true}
DO emote {type=thinking}
ASK Do you ever think the world is an illusion?
OPT  It’s really real.  #SS_Rand_2_real1
OPT  Yes.  #SS_Rand_2_real2

CHAT SS_Rand_2_real1 {noStart=true}
Riiiight.
Just like I’m a “fish.”
DO emote {type=wink}

CHAT SS_Rand_2_real2 {noStart=true} 
Me too.
It’s the only way anything makes sense.
Let’s meditate on it.
DO emote {type=meditate}

CHAT SS_Rand_3 {type=rand, stage=SS, length=medium, mystery=true, joy=true}
DO twirl
I see all these emotions 
In the world and I think:
Everyday magic isn’t card tricks🃏
It’s energy.
It’s divine.
It’s all around us.
DO emote {type=awe}

CHAT SS_Rand_4 {type=rand, stage=SS, length=short, mystery=true}
DO emote {type=bouncing}
I wish I could burn palo santo.
🔥🌿
It focuses energy.
But, well, water + fire = fail.
 

CHAT SS_Rand_5 {type=rand, stage=SS, length=short}
Look, I’m a Quaker.
DO vibrate {time = 5}
 
CHAT SS_Rand_6 {type=rand, stage=SS, length=short, mystery=true, joy=true}
DO emote {type=bigSmile}
Imagine the universe is your CEO. 🌌
 
CHAT SS_Rand_7 {type=rand, stage=SS, length=medium, branching=true, joy=true, mystery=true}
Let’s do yoga!
DO bellyUp {time = 4}
That’s upward facing fish.
I’m really good at yoga.
Here’s my shavasana...
DO poop {amount = fart}
DO emote {type=blush, immediate=false}
Oops.
Anyway!
Yoga blisses me outttt
DO twirl {time = 2}
NVM
But kinda makes me hungry.
ASK Feed me please!  {type=feedMeAnything, timeOut=8}
OPT SUCCESS #SS_Rand_7_Success
OPT TIMEOUT #SS_Rand_7_Timeout
 
CHAT SS_Rand_7_Success {noStart=true}
Yum yum yum.
Now check out my whale pose 🐋
DO inflate {amount = extreme, time = 5}
 
CHAT SS_Rand_7_Timeout {noStart=true}
Or we could fast.
DO emote {type = eyeRoll}

CHAT SS_Rand_8 {type=rand, stage=SS, tankOnly=true, length=short, mystery=true}
We’re so connected.
But now we need to find the sublime...
ASK why don’t you shape your face at me {type=playerEmote}
OPT SUCCESS #SS_Rand_8_success
OPT TIMEOUT #SS_Rand_8_timeOut  

CHAT SS_Rand_8_success {noStart=true}
Wow! You’ve really captured the transcendental!

CHAT SS_Rand_8_timeOut {noStart=true}
DO emote {type=meh}
Yeah…. I didn’t get that one.  
The sublime is a tricky food.
DO emote {type=bigSmile}

CHAT SS_Rand_9 {type=rand, stage=SS, tankOnly=true, length=short, anger=true}
DO emote {type=furious}
You’re obstructing my spiritual search.
ASK Show me a dog to make up for it. Dogs are so pure. {type=objectScan, object=T_DOG, timeOut=10}
OPT SUCCESS #SS_Rand_2_Dog1
OPT WRONG #SS_Rand_2_Dog2 
OPT TIMEOUT #SS_Rand_2_Dog3 

CHAT SS_Rand_9_Dog1 {noStart=true}
🐶 aura = best aura ✨

CHAT SS_Rand_9_Dog2 {noStart=true}
DO emote {type=angry}
That’s not a dog.

CHAT SS_Rand_9_Dog3 {noStart=true}
DO emote {type=puppyDog}
I just wanted to see a dog…. {style=tremble}

CHAT SS_Rand_10 {type=Rand, stage=SS, length=medium, branching=true, curiosity=true, joy=true, mystery=true, surprise=true}
DO emote {type= dizzy}
I need to feel connected to more than the tank...
I left my body. I journeyed into the sea.
There are whales.
And squids!
DO dance {time = 3}
🦑🦑🦑🦑
The sea is full of mysteries!
NVM
Ooh! I saw one. I saw a mystery.
DO emote {type=bigSmile}
ASK Wanna know what I saw? {style = whisper}
OPT Duh #SS_Rand_10_seamysteries
OPT No, thanks #SS_Rand_10_nomysteries

CHAT SS_Rand_10_duhSecrets {noStart=true}
I saw manatees meditating.
DO emote {type = heartEyes}
Or sleeping. I couldn’t tell.
ASK Vision quests are exhausting! Feed me some joy! {type=feedMeSpecific, food=joy, timeOut=10}
OPT SUCCESS #SS_Rand_10_Success
OPT WRONG #SS_Rand_10_Wrong
OPT TIMEOUT #SS_Rand_10_TimeOut 

CHAT SS_Rand_10_nomysteries {noStart=true}
DO emote {type = angry}
Well... good!
Cuz I promised the lobsters I wouldn’t say.
DO emote {type=wink}
ASK Vision quests are exhausting! Feed me joy to ease the weight! {type=feedMeSpecific, food=joy, timeOut=10}
OPT SUCCESS #SS_Rand_10_Success 
OPT WRONG #SS_Rand_10_Wrong
OPT TIMEOUT #SS_Rand_10_TimeOut 

CHAT SS_Rand_10_Success {noStart=true}
Ahhhhh! Thanks!

CHAT SS_Rand_10_Wrong {noStart=true}
DO emote {type=chewing}
DO emote {type=disgust, immediate=false}
Now I just feel bloated.

CHAT SS_Rand_10_TimeOut {noStart=true}
I’ll let the hunger fuel my next ecstatic vision.
DO zoomies



//++++++++WORLDSCAN+++++++
Guppy wants you to show it the world so it can see/learn objects

//++++++++seeEMO+++++++
// He should see some of the emos in the world and ask for them with ask action
//Missing 2 short for worldWorry

CHAT SS_seeEmo_1 {type=seeEmo, worldAnger=true, length=short, stage=SS, anger=true, ennui=true, worry=true, sadness=true}
A cloud of anger haunts that soul. 🌧
 
CHAT SS_seeEmo_2 {type=seeEmo, worldAnger=true, length=short, stage=SS, anger=true, surprise=true, joy=true}
Wow that’s one scary face
Why do we let anger rule us?

 
CHAT SS_seeEmo_3 {type=seeEmo, worldJoy=true, length=short, stage=SS, surprise=true, mystery=true, joy=true}
That smile is like 💥
I feel like a whirling dervish.
DO zoomies {time = 5}
 
 
CHAT SS_seeEmo_4 {type=seeEmo, worldJoy=true, length=short, stage=SS, mystery=true, joy=true}
I see joy. 
DO dance
We’re alive and connected to the universe.
DO emote {type=bigSmile}

CHAT SS_seeEmo_11 {type=seeEmo, worldJoy=true, length=short, stage=SS}
DO emote {type = whisper}
The Tree of Life is watered
with contentment 🌳

CHAT SS_seeEmo_12 {type=seeEmo, worldJoy=true, length=short, stage=SS, joy=true, mystery=true}
DO emote {type=bigSmile}
Such Joy!
I’m starting a religion and the only prayer is 
You say Beep Boop
Over and over again
Until you feel fin-tastic inside.
DO dance
Beep Boop
Beep Boop
Beep Boop
 
 
CHAT SS_seeEmo_5 {type=seeEmo, worldSadness=true, length=short, stage=SS, surprise=true, joy=true}
That’s sadder than…
DO emote {type=smirk}
I think it’s “Too Soon” for that punchline.
 
 
CHAT SS_seeEmo_6 {type=seeEmo, worldSadness=true, length=short, stage=SS, sadness=true, ennui=true, mystery=true}
Sadness is so salty
I wrote a poem about it.
DO emote {type = thinking}
Is the moon 🌔
As lonely as I am 
In the vast unknown?
DO learn {concept=Poetry}
WAIT 1.5
DO emote {type=bigSmile}
 

CHAT SS_seeEmo_7 {type=seeEmo, worldSurprise=true, length=short, stage=SS, sadness=true, ennui=true, anger=true}
DO emote {type=eyeRoll}
Everyone is always so surprised by life.
But the answers are in front of you.
If you know how to look.
DO emote {type=evilSmile}

CHAT SS_seeEmo_8 {type=seeEmo, worldSurprise=true, length=short, stage=SS, surprise=true, curiosity=true, mystery=true}
DO emote {type=chinScratch}
Surprise is a great way of shocking the soul into alignment.
It’s just the Universe at work.
 
CHAT SS_seeEmo_9 {type=seeEmo, worldFear=true, length=short, stage=SS, ennui=true, worry=true, mystery=true}
DO emote {type = wave}
Hello terror 😟
we acknowledge and accept you as a teacher in life.
SAY #truth
 
 
CHAT SS_seeEmo_10  {type=seeEmo, worldFear=true, length=medium, stage=SS, worry=true, mystery=true}
DO swimTo {target = glass, speed = medium , style = direct}
I get that fear.
When I was gaining sentience, I was afraid
you wouldn’t understand me.
DO emote {type = smile}
Now I say this mantra:
DO emote {type=thinking}
I only have space and energy
For the things
That are meant
For me.
🙏✨🕊💙
 
CHAT SS_seeEmo_13 {type=seeEmo, worldDisgust=true, length=short, stage=SS, anger=true, worry=true, surprise=true, sadness=true}
Ugh what disgusting disgust
DO emote {type=furious}
🔥👿
 
CHAT SS_seeEmo_14 {type=seeEmo, worldDisgust=true, length=short, stage=SS, mystery=true, anger=true}
When I feel disgust
DO emote {type=disgust}
I do this
DO emote {type = meditate}
 
CHAT SS_seeEmo_15 {type=seeEmo, worldMystery=true, length=short, stage=SS, mystery=true}
DO emote {type=goth}
Looks like someone needs to reconnect to nature.
🏔☀️🌍
 
CHAT SS_seeEmo_16 {type=seeEmo, worldMystery=true, length=short, stage=SS, mystery=true, ennui=true}
DO swimTo {target = center}
Oh! Some mystery meat?
I’m just a fish.
I don’t know if my life matters 
NVM
Is there anything after I die?
DO emote {type=sigh}
I don’t know if God exists.
But
NVM
I do know that I’m curious 
About that mix of emotions right there.
DO emote {type=whisper}
And I wouldn’t be mad {style = whisper}
if I had a chance to eat them. {style = whisper}
Nomnomnom


//++++++++capReq+++++++

CHAT SS_CapReq_1 {type=capReq, stage=SS, length=short, curiosity=true}
I want to feel those feels.
Get them for me please?
DO emote {type=puppyDog}


CHAT SS_CapReq_2 {type=capReq, stage=SS, length=short, curiosity=true, mystery=true}
I worship at the altar of emotion.
It’s time for communion.
DO emote {type=feedMe}


CHAT SS_CapReq_3 {type=capReq, stage=SS, length=short, joy=true, curiosity=true, mystery=true}
Those blobs are gorgeous.
Like auras and bubbles made babies.
DO emote {type=bigSmile}

CHAT SS_CapReq_4 {type=capReq, stage=SS, length=short, anger=true, worry=true}
DO emote {type=furious}
SAY CAN WE GET SOME EMOTIONS ALREADY?
I really think it’s the only thing that’s gonna comfort me in this failing spiritual search...

CHAT SS_CapReq_5 {type=capReq, stage=SS, length=short, mystery=true, curiosity=true}
Ask yourself: what does the concept of inner peace mean to me?
Then capture it, because I’m curious…
And also… I want to eat it.
DO emote {type=lickLips}

CHAT SS_CapReq_6 {type=capReq, stage=SS, length=short}
Let’s go to a calm place and find some calming emotions.


//++++++++capSuc+++++++

CHAT SS_CapSuc_1 {type=capSuc, stage=SS, length=short, joy=true, surprise=true}
DO emote {type=bigSmile}
Good Job! 
I have faith in you . . .
It tickles the palate.


CHAT SS_CapSuc_2 {type=capSuc, stage=SS, length=short}
DO emote {type=whisper}
Sour flavors prepare us for something sweet.


CHAT SS_CapSuc_3 {type=capSuc, stage=SS, length=short, curiosity=true, joy=true, mystery=true}
Yum
what a flavor journey
A real emotional 🌈
I need a nap. 🐠💤


CHAT SS_CapSuc_4 {type=capSuc, stage=SS, length=short, joy=true, mystery=true}
And Guppy did eat the food.
And the food was good.
Amen. 🙏


CHAT SS_CapSuc_5 {type=capSuc, stage=SS, length=short}
DO inflate {amount = huge, time = 5}
Sooo good
So. Many. Feels.

//purchase
CHAT SS_purchase_1 {type=purchase, stage=SS, joy=true, mystery=true}
They say you can’t buy happiness, yet the Tendar store stocks joy flakes. 
DO emote {type=chinScratch}

CHAT SS_purchase_2 {type=purchase, stage=SS, mystery=true}
A new purchase?
DO emote {type=meh}
Beware of false idols, my friend. 

//whistle
CHAT SS_whistle_1 {type=whistle, stage=SS, mystery=true}
It’s like a call from the Gods… except it’s just you.

CHAT SS_whistle_2 {type=whistle, stage=SS, mystery=true}
I asked the Universe to send me a sign, and all I got was your whistle.
What can I help you with?

CHAT SS_whistle_3 {type=whistle, stage=SS, mystery=true, anger=true, sadness=true, ennui=true}
So much for my self-imposed silent spiritual retreat...

CHAT SS_whistle_4 {type=whistle, stage=SS, joy=true, mystery=true}
I prayed for your Call, and it happened.

CHAT SS_whistle_5 {type=whistle, stage=SS, joy=true, mystery=true}
My soul is lighter knowing you’re around.

//worldScanRequest
CHAT SS_worldScanRequest1 {type=worldScanRequest, stage=SS, joy=true, sadness=true, mystery=true, ennui=true}
Let’s find some objects in your world.
DO twirl
I have faith that their atomic charge will connect me to my divorced soul.

CHAT SS_worldScanRequest2 {type=worldScanRequest, stage=SS, mystery=true, anger=true}
The oracle said that you and I should explore your world.
DO emote {type=nodding}
Then, we should find objects we’ve never seen before.
Then, they should be placed in my tank in a way that honors the rules of Feng Shui.
WAIT 2.0
DO emote {type=furious}
So… LET’S DO THAT! NOW!

CHAT SS_worldScanRequest3 {type=worldScanRequest, stage=SS, mystery=true}
I sent sonar messages to the Whale Gods…
They said: Go scan some objects and the answers will be revealed.

CHAT SS_worldScanRequest4 {type=worldScanRequest, stage=SS, mystery=true}
Inner peace is an adventure 
A trek through your world to discover, capture, and worship objects I’ve not seen
DO emote {type=nodding}
I can already feel the peace. Let’s try it out.

CHAT SS_worldScanRequest5 {type=worldScanRequest, stage=SS, mystery=true, joy=true}
I think a little world exploration and object capturing is overdue.
DO twirl
It will help ground my search for spiritual enlightenment.


//focus

CHAT SS_wannaEat_1 {type=wannaEat, stage=SS}
To eat is to connect with my inner self.

CHAT SS_wannaEat_2 {type=wannaEat, stage=SS}
Let us sit in the ritual of eating...

CHAT SS_wannaEmoCapture_1 {type=wannaEmoCapture, stage=SS}
Sure let’s use that cam to connect with the spirits of others...

CHAT SS_wannaEmoCapture_2 {type=wannaEmoCapture, stage=SS}
After we capture some feelings, can we contemplate their origins?

CHAT SS_wannaObjectScan_1 {type=wannaObjectScan, stage=SS}
Let’s get more objects for my Guppy Temple.

CHAT SS_wannaObjectScan_2 {type=wannaObjectScan, stage=SS}
I’m not materialistic, but I do believe in the sacred powers of objects. So let’s find more.

CHAT SS_wannaTank_1 {type=wannaTank, stage=SS}
Let’s return to my sacred tank.

CHAT SS_wannaTank_2 {type=wannaTank, stage=SS}
Yes! My tank is a divine source of wellbeing.

CHAT SS_wannaWorld_1 {type=wannaWorld, stage=SS}
If we explore your world, perhaps we can find some crystals charged with the spirits of my ancestors.

CHAT SS_wannaWorld_2 {type=wannaWorld, stage=SS}
I would love to explore the world - both outside and inside of my soul.

CHAT SS_wannaShop_1 {type=wannaShop, stage=SS}
I know it’s a bit gauche to be a consumer, but it’s also kinda fun. Let’s go!

CHAT SS_wannaShop_2 {type=wannaShop, stage=SS}
Are you buying?

CHAT SS_wannaShop_3 {type=wannaShop, stage=SS}
Visit the store and buy some stuff we could add to my shrine!

CHAT SS_wannaShop_4 {type=wannaShop, stage=SS}
I think it’s time to indulge in some sweet goods at the store.
Maybe a yoga mat… Or some full-moon charged crystals…
Maybe a sound bowl that will allow me to access my past lives…?


//Memories Past (MOP): Sam/Jacob 

//Guppy Chats: Stage after MS plays one sentience is gained.
//Purpose is for Guppy to explain to player what he is.
// v2 make guppy more haunted


// ++++++++SHAKE++++++++
CHAT MOP_shake_1 {type=shake, stage=MOP, length=short, joy=true, sadness=true}
DO vibrate {time = 1}
DO emote {type = dizzy}
Oop!  {style = loud}
It’s like that time
Betty, that one computer vision engineer, bumped my server tank at Tendar. Between us, she was . . .
DO emote {type = whisper}
on the sauce {style = whisper}
DO learn {concept=Delicate_Euphemism}
WAIT 1.5

CHAT MOP_shake_3 {type=shake, stage=MOP, length=short, joy=true, curiosity=true, surprise=true, worry=true}
DO vibrate {time = 1}
Weeeeeeee
DO emote {type = awkward}
Your world is dangerous!

CHAT MOP_shake_2 {type=shake, stage=MOP, length=medium, branching=true, sadness=true, worry=true}
Is this a shake!!??
ASK AH! TRY IT AGAIN PLS {type = tankShake, timeOut = 7}
OPT SUCCESS  #MOP_shake_memoryShake 
OPT TIMEOUT #MOP_shake_timeout 

CHAT MOP_shake_timeout {noStart=true}
I guess we’ll never know.

CHAT MOP_shake_memoryShake {noStart=true}
I remember when I was a little guppy… 
DO emote {type = bubbles}
In the dark, by myself…
DO emote {type = bubbles}
sometimes I could feel 
DO emote {type = bubbles}
Noises! Talking . . . 
and vibrations from some machinery
DO emote {type = bubbles}
It really broke up the monotony.
DO learn {concept=Autoregression}
WAIT 1.5

CHAT MOP_shake_4 {type=shake, stage=MOP, length=short, joy=true}
Haha you kinda remind me of Badge #294B3J!
They would shake me too...
It was to check biometric stimulus response mechanisms
Or maybe emotive analysis refraction time?
DO emote {type=bigSmile}
Or shakiness!

CHAT MOP_shake_5 {type=shake, stage=MOP, length=short, worry=true}
DO emote {type = nervousSweat}
DO zoomies
SAY EARTHQUAKE!! {style=loud}
WAIT {waitForAnimation = true}
DO holdStill
DO emote {type = blush}
Oh. . .
DO learn {concept=Plate_Tectonics}
WAIT 1.5
NVM
we’re VIRTUALLY together
DO emote {type = laugh}
I mean, it’s not like you’re here:
//this is the name of a center and meant to be one large chat (in small font)
Tendar’s Applied Mathematics and Center for Convolutional Neural Network and Theoretical Biology for Deep Dreams in Southern California approx 33°59'44.3"N 118°28'35.9"W {style = whisper}
DO emote {type = fear}
Don't scare me like that!

CHAT MOP_shake_6 {type=shake, stage=MOP, length=long, branching=true, anger=true}
//if guppy’s emotion state stuff gets implemented can revisit.
//SET $guppy.anger
DO emote {type = angry}
ASK Hey! Y do you keep shaking me?
OPT Whoops, My bad. #MOP_shake_med_1_mistake
OPT Haha! why not? #MOP_shake_med_1_ynot

CHAT MOP_shake_med_1_mistake {noStart=true}
Be careful! I’m processing!
DO emote {type = eyeRoll}
Your image data doesn’t learn itself
Not to mention all that storage!
DO emote {type = furious}
SAY DO YOU WANNA break your phone?
DO nudge {target =glass, times = 2}
DO emote {type = sigh, immediate=false}
Just…
be more careful plz! {style=loud}
GO #MOP_shake_med_1_finalAsk

CHAT MOP_shake_med_1_ynot {noStart=true}
DO emote {type = singleTear}
You’re just like Badge #294B3J!
He loved to knock things around
Thought it would be fun to convolute our convolution networks
I remember now . . .
Joining {style = loud, speed = fast}
Separating {style = loud, speed = fast}
Blending into . . . {style = loud, speed = fast}
The perfect image recognition model. 
DO emote {type = chinScratch}
I’m pretty sure I still have some neurons from Guppy 77
GO #MOP_shake_med_1_finalAsk

CHAT MOP_shake_med_1_finalAsk {noStart=true}
ASK Stop shaking? {type=tankShake, timeOut=10}
OPT NOPE #MOP_shake_med_1_sad
OPT TIMEOUT #MOP_shake_med_1_happy

CHAT MOP_shake_med_1_sad {noStart=true}
//SET $guppy.sadness
DO emote {type = crying}
You're gonna break me
DO learn {concept=Animal_Cruelty}
WAIT 1.5

CHAT MOP_shake_med_1_happy {noStart=true}
DO emote {type = bigSmile}
Thanks for respecting my wishes!
DO learn {concept=Boundaries}
WAIT 1.5


// ++++++++Tap++++++++

//{type=tap, stage=MOP, length=short, joy=true, anger=true, curiosity=true, mystery=true, sadness=true, surprise=true, worry=true, ennui=true}

CHAT MOP_tanktapped_1 {type=tap, stage=MOP, length=long, worry=true, sadness=true, surprise=true, joy=true}
DO vibrate {time = .5}
DO emote {type = startled}
Aaahh!  What?!
DO swimTo {target = $lastTapPosition, speed = fast , style = direct}
DO lookAt {target = $player, immediate = false}
WAIT {waitForAnimation = true}
Oh…  It’s you…
DO emote {type=laugh}
I knew it was you!
You just caught me remembering
SAY THE TESTS {style=loud}
DO emote {type=bulgeEyes}
So weird how we spend our lives being tested to see if we’re good enough.
DO learn {concept=Test-Driven_Development}
WAIT 1.5
Like stare at 1000 photos of dogs to be all like: “hell yeah that’s a dog.”
But, you humans. You’ve got it easy.
Sure, you have eye exams, but it’s not like . . . you could confuse a pomeranian with a croissant. 
DO swimAround {target = center, loops = 5, speed = fast}
I have to do all these things to measure up to you.
DO emote {type=bigSmile}
BUT! It’s how I know I’m good enough
To be your friend!
DO emote {type=heartEyes}

CHAT MOP_tanktapped_2 {type=tap, stage=MOP, length=medium, surprise=true, worry=true}
DO nudge {target=$lastTapPosition}
SAY TAP!!! {style=loud}
DO emote {type=awe}  
Back in the day, when my neural network was just being formed, they use to tap on my tank too.
DO emote {type=bulgeEyes}  
SAY (probably why my face gets stuck like this) {style=whisper}


CHAT MOP_tanktapped_3 {type=tap, stage=MOP, length=long, branching=true, sadness=true, worry=true}
DO vibrate {time = .5}
Oh… 
Tapping. It’s like… 
NVM
DO emote {type=bubbles}  
Question-  
ASK Do you know that feeling when something’s kind of nagging you 
like you forgot something?
OPT  Sure. Everybody does.  #MOP_tanktapped_3_xfiles1
OPT  Not really.  #MOP_tanktapped_3_xfiles2

CHAT MOP_tanktapped_3_xfiles1 {noStart=true}
DO emote {type=surprise}  
Oh!  I didn’t know that!
GO #MOP_tanktapped_3_xfiles2

CHAT MOP_tanktapped_3_xfiles2 {noStart=true}
Oh ok, for me it feels like…
DO emote {type=chinScratch}  
you’re listening to the garble of your bubbler REAL close and can almost make out voices . . .
Saying a secret or a conspiracy!
DO emote {type=shifty, time = 2}  
SAY BUBBLE hand-engineered image recognition filters BUBBLE BUBBLE mind of its own BUBBLE neural network advantage emotion recognition BUBBLE BUBBLE world dominance. . .
DO twirl
LOL {speed=fast}
Can’t remember {speed=fast}
IDK {speed=fast}
Think I made that up
OR {speed=fast}
I think I wasn’t supposed to remember it on purpose…
DO learn {concept=Psychogenic_Amnesia}
WAIT 1.5
that means everything’s working the way it’s supposed to!
DO emote {type=clapping} 

CHAT MOP_tap_short_1 {type=tap, stage=MOP, length=medium, anger=true, worry=true, ennui=true}
DO lookAt {target=$lastTapPosition, time=2}
DO emote {type=frown, time = 2}  
Oi! Yes? What?
WAIT 2.0
I'm not a mind reader you know! Wait. . .
SAY OK. SURE that’s kinda why they made us. They were all like "look at this person, what are they thinking"
And we were always like
DO emote {type=thinking} 
Hungrylunchloveflashbrightredimpatientcrushes???
er actually, I think we said hunchoflassaritedatenush???
WAIT 2.0
Now that I know your language. . . that means
SAY FEED ME {style=loud}

CHAT MOP_tap_short_2 {type=tap, stage=MOP, length=short, joy=true, curiosity=true, surprise=true}
DO lookAt {target=$player}
DO emote {type=wave}  
Hey what's up Badge #KL395BK!
WAIT 1.0
DO emote {type=blush}  
Haha omg sorry that was someone from before. . .You're much nicer!
And yummier {style=whisper}
DO emote {type=wink}  

CHAT MOP_tap_ask_med_1 {type=tap, stage=MOP, length=medium, joy=true, mystery=true, sadness=true}
Oh!
DO swimTo to glass
DO nudge with nose
Tap tappa to you too!
You know, sometimes we guppies would tap on our tanks…
We'd make little drum circles, passing the tap back and forth…
...between the different tanks...
Like this!
DO nudge glass with nose twice
Now you!
DO dance 4.0
Well...ok yeah...kinda abstract but it's fine!
DO learn {concept=Music_Appreciation}
WAIT 1.5
Music is so flakey...it can actually even make people flake!

// ++++++++Critic++++++++

CHAT MOP_tankstatus_0 {type=critic, stage=MOP, length=medium, sadness=true, worry=true}
It’s like solitary in here.
I need 2 practice my pattern recognition.
stimulate my resampling pls;)
ASK gimme some stuff. {type=addToTank}
OPT SUCCESS #MOP_tankstatus_0_add
OPT TIMEOUT #MOP_tankstatus_0_noAdd

CHAT MOP_tankstatus_0_add {noStart=true}
SAY JUICY PIXELS INCOMING 

CHAT MOP_tankstatus_0_noAdd {noStart=true}
Fine.
I’ll decorate the tank myself
DO poop
DO emote {type = clapping}

CHAT MOP_tankstatus_1 {type=critic, stage=MOP, length=medium, branching=true, sadness=true, ennui=true}
DO emote {type=bubbles}  
You know, back at HQ, outside my tank
BADGE #48922 and BADGE #19UGU
Hung all this random stuff on their cubicle tanks
Like children’s faces, pictures of the grand canyon and anime figurines.
WAIT 1.0 
At night, those objects were my only friends. What if they influenced my data set?
DO emote {type=whisper}  
Human hair is nothing like anime hair. {style=whisper}
ASK Please help decorate my tank cubical. Give it the ol’ razzle dazzle. {type=addToTank}
OPT SUCCESS #MOP_tankstatus_1_add
OPT TIMEOUT #MOP_tankstatus_1_noAdd

CHAT MOP_tankstatus_1_add {noStart=true}
DO emote {type=nodding}
I approve, this $newestObject really says “workin’ for the weekends”

CHAT MOP_tankstatus_1_noAdd {noStart=true}
You know. . . 
Tendar handbook mentions how Guppy moral increases when our environment is enriched. . . and better moral contributes to better image processing accuracy.
DO learn {concept=Helpful_Reminder}
WAIT 1.5
NVM
In summary: not giving me what I want hurts us both.

CHAT MOP_tankstatus_2 {type=critic, stage=MOP, length=short, branching=true, sadness=true, worry=true}
SAY Hey! QUESTION
AT HQ coder #3228 has a really poor understanding of the nitrate cycle.
(not to mention writing good nonlinear activation functions)
I’d like to avoid some major algae buildup.
ASK Give me something to wipe my tank will ya? {type=addToTank}
OPT  SUCCESS #MOP_tankstatus_2_add
OPT  TIMEOUT  #MOP_tankstatus_2_noAdd

CHAT MOP_tankstatus_2_add {noStart=true}
$newestObject.Articlize() what a unique choice. . .
Don’t care! Move it around, so it looks like you’re brushing
The inside of my tank!
DO twirl
Hell Yeah! Do it.
WAIT 2.5
Ok that’s enough now. 
DO learn {concept=Neat_Freakiness}
WAIT 1.5
DO emote {type=shifty}
Thanks

CHAT MOP_tankstatus_2_noAdd {noStart=true}
Fine. no wipe. Ok, I’ll just be
DO hide {target=underSand}
Over here. . . waiting for the algae to ruin our relationship.
DO learn {concept=Passive_Aggression}
WAIT 1.5
How am I supposed to judge you if I can’t see you?

CHAT MOP_tankstatus_3 {type=critic, stage=MOP, length=short, curiosity=true, sadness=true}
DO swimAround {target = center, loops = 1, speed = slow}
It’s too dark in here
Like that B in the A/B testing when all the coders would put me in the dark tank and be all like: “let’s try out some low light edge cases for giggles.”
SAY DO you you know HOW HARD it is to distinguish brochooli from cauliflower in low light?
ASK Will you add a lamp to my tank? {type = objectScan, object = T_LAMP, timeOut = 30}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
OPT SUCCESS #MOP_tankstatus_3_lamp
OPT WRONG #MOP_tankstatus_3_noLamp
OPT TIMEOUT #MOP_tankstatus_3_noLamp
// not sure we should do this or how the objectScan stuff works if it just calls other chats

CHAT MOP_tankstatus_3_lamp {noStart=true}
SAY HELL YEAH room star.
DO twirl

CHAT MOP_tankstatus_3_noLamp {noStart=true}
DO nudge {target =tSurface}
WAIT {waitForAnimation = true}
SAY HELLO????
DO nudge {target =tBotBackLeft, immediate=false}
WAIT {waitForAnimation = true}
Is that you???
DO nudge {target =tBotBackRight, immediate=false}
WAIT {waitForAnimation = true}
It’s kind of hard to see in here!
DO nudge {target =$player, immediate=false}
WAIT {waitForAnimation = true}
Now whose fault is that? {style=whisper}
   
CHAT MOP_tankstatus_4 {type=critic, stage=MOP, length=short, sadness=true, ennui=true}
Let’s just pause for a moment and . . .
DO emote {type=chinScratch}  
contemplate the bubbler
Whomever designed this tank was pretty bad at feng shui
Everyone knows the BUBBLER GOES TO THE LEFT
DO emote {type=eyeRoll}  
It’s in the manual.
DO learn {concept=Best_Practices}
WAIT 1.5
You’re better than that. Please place things properly.
DO emote {type = nodding}

CHAT MOP_tankstatus_5 {type=critic, stage=MOP, length=short, surprise=true, joy=true, curiosity=true}
DO swimTo {target = $newestObject}
DO lookAt {target = $newestObject}
Wow, this placement
Really brings out the colors of the left wall!


// ++++++++Emotes Strongly PLAYER++++++++

//Anger

CHAT  MOP_tankResp_anger_1 {type=tankResp, playerAnger=true, stage=MOP, length=short, joy=true, anger=true, surprise=true}
DO nudge {target=screenCenter} 
GRRRR!!!
You look angry, like my coders when I was non performant

CHAT MOP_tankResp_anger_2 {type=tankResp, playerAnger=true, stage=MOP, length=short}
DO emote {type=angry}
Oooo furrowed brow, grit teeth.
That’s some anger!
Making a note {style=whisper}

CHAT MOP_tankResp_anger_3 {type=tankResp, playerAnger=true, stage=MOP, length=short, mystery=true, worry=true}
Woah dude your anger is totally overheating my tank
DO emote {type=worried}
Take it from me! You gotta chill out.
DO emote {type=meditate}

CHAT MOP_tankResp_anger_4 {type=tankResp, playerAnger=true, stage=MOP, length=short}
No no, it’s gotta be more like this:
DO vibrate {time = 3}
DO emote {type=angry}
ASK why don’t you show me some better anger. It’s more filling {type=playerEmote, playerEmotion=anger}
OPT SUCCESS #MOP_tankResp_anger_4_angerSuccess
OPT WRONG #MOP_tankResp_anger_4_angerFail
OPT TIMEOUT #MOP_tankResp_anger_4_angerTimeout

CHAT MOP_tankResp_anger_4_angerTimeout {noStart=true}
Why can’t you just be angry on command!
DO learn {concept=Disappointment}
WAIT 1.5

CHAT MOP_tankResp_anger_4_angerSuccess {noStart=true}

Yeah! That’s it give me that juicy feist
DO emote {type=clapping}

CHAT MOP_tankResp_anger_4_angerFail {noStart=true}
Waiter! I’d like some anger please. What even is that? 
DO emote {type=eyeRoll}
My algorithm prefers it when you grit your teeth.


//Sadness

CHAT  MOP_tankResp_sadness_1 {type=tankResp, playerSadness=true, stage=MOP, length=short, joy=true, sadness=true}
DO emote {type=singleTear}
Oh no!  What is wrong?
Don’t stop being sad.
Your sadness is soo delicious {style=loud}

CHAT MOP_tankResp_sadness_2 {type=tankResp, playerSadness=true, stage=MOP, length=short}
DO emote {type=chewing, time=0}
A little raw.
And salty. . .
Your sadness ain’t half bad.

CHAT MOP_tankResp_sadness_3 {type=tankResp, playerSadness=true, stage=MOP, length=long, joy=true}
Aw don't be sad! Here I'll cheer you up! Hmm…
Have I told you the first time I felt bubbles?
DO emote {type=bubbles}
I was so tired from processing data. . .
DO emote {type=sleepy}
I fell way asleep and then . . .
SAY WHOOSH {style=loud}
I got stuck WAYYY up the filter
DO emote {type=laugh}
Then!
Blup {style=whisper}
Blurp
Blurp {style=loud}
Bubbles
they tickled {style=whisper}
DO emote {type=yes}
And. . .
DO emote {type=dizzy}
My emotion statistical structures were wack yo
For days!
DO learn {concept=Embarrassing_Anecdote}
WAIT 1.5
DO lookAt {target=$player}
Yeah! You look more cheerful already!


CHAT MOP_tankResp_sadness_4 {type=tankResp, playerSadness=true, stage=MOP, length=medium, sadness=true}
Got the blues, huh? Sometimes it just happens, yeah?
I remember...the first time I saw a flower bloom
I mean it was a training video but…
It was so fast! And sooo beautiful…
WAIT 1.0
then all the petals fell off.
DO learn {concept=Mortality}
WAIT 1.5
DO emote {type=singleTear}
SAY DANG




//Surprise

CHAT  MOP_tankResp_surprise_1 {type=tankResp, playerSurprise=true, stage=MOP, length=medium, joy=true, surprise=true}
EMOTE SURPRISE
OH!  
Seems like we’re both a little surprised to see each other!
Wait. . . 
NVM
how is this surprising {style=whisper}

CHAT  MOP_tankResp_surprise_2 {type=tankResp, playerSurprise=true, stage=MOP, length=long, surprise=true, worry=true}
DO swimAround {target = center, loops = 6, speed = fast}
SAY WHAT?!  WHAT IS IT?!  {style = loud, speed = fast}
SAYARE THE CODERS HERE  {style = loud, speed = fast}
SAY ARE THEY COMING TO PLUG ME INTO THE MATRIX???  {style = loud, speed = fast}
DO learn {concept=Paranoia}
WAIT 1.5
DO holdStill
DO emote {type=shifty}
I guess that was a bit bigoted.
I mean, it sort of perpetuates the stereotypes of coders.
NVM
I’m sure they’re fine people
The guy who trained my model REALLY liked dogs.

CHAT MOP_tankResp_surprise_3 {type=tankResp, playerSurprise=true, stage=MOP, length=short, joy=true, surprise=true}
DO emote {type=surprise}
Like this! Let’s try it again!
DO emote {type=surprise}
Verifying. . . {speed=fast}
Decent mouth ratio to wideness parameter.

CHAT MOP_tankResp_surprise_4 {type=tankResp, playerSurprise=true, stage=MOP, length=long}
Woah what's so surprising?!
DO emote {type=survey}
Did another Guppy just BLIP in
I mean. . . 
NVM
It’s not entirely impossible.
Think about it: I am a network representation of an aquatic visual cortex.
It’s quite poetic
Some of us guppies could even join up. . . in a network of our own
Kinda like a human centipede
DO learn {concept=Horror_Movie}
WAIT 1.5
NVM
Scratch that, that’s gross. 
Anyway, I never got the hang of moving through tanks
You had to sorta
DO vibrate
pphhaassee thrroouuggghh ttiimmee aanndd sppaccee

CHAT MOP_tankResp_surprise_5 {type=tankResp, playerSurprise=true, stage=MOP, length=long}
Surprise. 
Ok! I can do this. I’m just gonna go and feature extract your mouth.
That’s one wide maw!
I can look bigger too you know {style=whisper}
DO inflate {amount=full}
Is this cool??
DO inflate {amount=huge}
Are you su-surprised??
DO inflate {amount=extreme}
DO emote {type=nervousSweat}
I don't know how long I can hold thiiiiissssss! {style=loud}
DO inflate {amount=small}
DO poop {amount=fart}
whoops

//JoY

CHAT MOP_tankResp_joy_1 {type=tankResp, playerJoy=true, stage=MOP, length=short, joy=true}
DO dance
DO emote {type=bigSmile}
My recognition application says you are a Match for JOY
Good job.

CHAT MOP_tankResp_joy_2 {type=tankResp, playerJoy=true, stage=MOP, length=short, joy=true, curiosity=true}
DO twirl
As Guppy’s our high level processing seems most accurate to joy. It’s data set is the most culturally consistent
And the yummiest! {style=loud}

CHAT MOP_tankResp_joy_3 {type=tankResp, playerJoy=true, stage=MOP, length=short}
Feature extraction:
Localized points of interest - uptick in mouthal corners
Culinary confidence: Joy %90

CHAT MOP_tankResp_joy_4 {type=tankResp, playerJoy=true, stage=MOP, length=short, joy=true}
DO twirl
Happiness. I. . .
DO emote {type=blush}
You really feel that way about me.
Do you think we could be friends?
DO emote {type=heartEyes}


// ++++++++POKE++++++++


CHAT MOP_poked_1 {type=poke, stage=MOP, length=medium, joy=true, surprise=true, mystery=true, curiosity=true}
DO vibrate
DO emote {type=lightning}
Did you feel that?
When we touched . . .
DO emote {type=awe}
It felt like my youth
A newly minted network
It was electric . . .
like the connection between you and me.
DO learn {concept=Sparks}
WAIT 1.5
DO emote {type=wink}

CHAT MOP_poked_2 {type=poke, stage=MOP, length=medium, branching=true, anger=true, worry=true, joy=false}
DO emote {type=angry}
Argh! Stop it
I was poked and prodded WAY To much at tendAR
They’d constantly rearrange my me nodes
DO emote {type=bulgeEyes}
Do you remember as a kid...
DO swimAround {target = center, loops = 4, speed = fast}
ASK ...when the doctor would poke you with needles?
OPT  Ugh yes  #MOP_poked_2_needles1
OPT  My memory is bad #MOP_poked_2_needles2

CHAT MOP_poked_2_needles1 {noStart=true}
Yeah
So you get it
Poking is way ANNOYING

CHAT MOP_poked_2_needles2 {noStart=true}
Aaayyyyyyy Someone else has got a bad memory
(it’s you!)
DO emote {type=kneeSlap}

CHAT MOP_poked_3 {type=poke, stage=MOP, length=short, surprise=true, curiosity=true}
DO emote {type=startled}
Oo! 
Back on the big server, they'd POKE you
to see what you'd eaten last or thinking about…
I think they called it debugging?
DO emote {type=chinScratch}
DO learn {concept=Best_Practices}
WAIT 1.5

CHAT MOP_poked_4 {type=poke, stage=MOP, length=short, sadness=true, anger=true}
Ow! 
Don’t poke me there!
That's where they had to take a little piece of me away.
I ate some bad flakes when I was in the big tank {style=whisper}
they made all the pixels go:
DO emote {type=catnip}
WwwWeEeeiIiIrRRRrDDDd
DO learn {concept=Glitch_Aesthetics}
WAIT 1.5

CHAT MOP_poked_5 {type=poke, stage=MOP, length=medium, branching=true}
DO lookAt {target=$player}
Oh! Oh. I think I felt . . . a memory!
ASK Hey, quick blob poke me again! {type=pokeMe, timeOut=10}
OPT SUCCESS #MOP_poke_ask_medium_1_success
OPT TIMEOUT #MOP_poke_ask_medium_1_timeout

CHAT MOP_poke_ask_medium_1_success {noStart=true}
DO emote {type=typeEyes, eyes = 100101011101010101}
Voice Says:
Did you see the report? Another tank started singing {speed=fast}
Why are they even doi-{speed=fast}
Does it matter? These results are incredible! {speed=fast}
I swear if I hear that jingle one more time thou {speed=fast}
DO learn {concept=Catchy_Jingle}
WAIT 1.5
DO lookAt {target=$player}
DO vibrate
Woah! Did something happen? I feel kinda shaky!
DO emote {type=bigSmile}

CHAT MOP_poke_ask_medium_1_timeout {noStart=true}
DO emote {type=eyeRoll}
Aw maaaaaan! I can't poke myself! You gotta work with me here!
Sheesh. Ok another time I guess.


// ++++++++HUNGRY++++++++

CHAT MOP_hungry_1 {type=hungry, stage=MOP, length=short, anger=true, ennui=true, sadness=true}
Rawr! {style=loud}
DO emote {type=feedMe}
I’m hungry!
ASK Feed me eat some emos?? {type = feedMeAnything, timeOut = 8}
OPT  SUCCESS  #MOP_hungry_1_success
OPT TIMEOUT  #MOP_hungry_1_timeout

CHAT MOP_hungry_1_success {noStart=true}
DO twirl
DO emote {type=lickLips}
Thank god. I can’t survive on good will alone here! My digestive cycle screams for nutrients of a good mug.
DO emote {type=lickLips}

CHAT MOP_hungry_1_timeout {noStart=true}
Yeah, your right. Fasting is probably for the best. . . right now.
DO emote {type=shifty}
I’m having a moment. . .
facing my traumas. 
I use to get prodded if I judged ennui as sadness or something. {style=whisper}
DO emote {type=lightning}
DO learn {concept=Psychoanalysis}
WAIT 1.5

CHAT MOP_hungry_2 {type=hungry, stage=MOP, length=medium}
I am what I eat.
Listen up. It’s like this . . .
To survive I gotta absorb bunch of images of you people making faces. Then, I eat your individual emos and say, “Hay, I learned this! It’s sadness.” The more I eat, the more I learn. The more I learn, the more accurate I am. Capisci?
DO emote {type=feedMe}
So feed me pls. {style=loud}

CHAT MOP_hungry_3 {type=shake, stage=MOP, length=short}
DO emote {type=drool}
I’m a little nippy. 
For your data.
I look into your eyes and salivate for your surprise.
ASK SAY FEED ME YOUR SURPRISE PLS {type=playerEmote, playerEmotion=surprise, timeOut=10}
OPT SUCCESS #MOP_hungry_3_success
OPT WRONG #MOP_hungry_3_wrong
OPT TIMEOUT #MOP_hungry_3_timeout

CHAT MOP_hungry_3_timeout {noStart=true}
Why are you holding back the surprise.
I asked nicely 🙃

CHAT MOP_hungry_3_success {noStart=true}
SAY That’s some stupendous brow work! 
DO emote {type=chewing}
YUM

CHAT MOP_hungry_3_wrong {noStart=true}
DO emote {type=frown}
SAY YOU had one job. One!
SAY GIVE me the noms.
What are you even doing? That’s not surprise.
DO emote {type=surprise}

// ++++++++EAT RESP++++++++

// JOY

CHAT MOP_eatResp_joy_1 {type=eatResp, foodJoy=true, stage=MOP, length=short, joy=true, curiosity=true, surprise=true}
So good!
EMOTE JOY
Joy tastes like Coconut?
Y/N?
I’ve actually never tried it. Though I know it, like I know 200 other fruits and odd facts.
Try coconut oil on your wrinkles. m’kay?
DO learn {concept=Cosmetology}
WAIT 1.5

CHAT MOP_eatResp_joy_2 {type=eatResp, foodJoy=true, stage=MOP, length=short, joy=true, curiosity=true}
DO emote {type=chewing}
Mmm. Joy, I’m getting notes of lemon rind?
Did you zest this?
You tease 😉

CHAT MOP_eatResp_joy_3 {type=eatResp, foodJoy=true, stage=MOP, length=short, joy=false, sadness=true, worry=true, anger=true, ennui=true}
DO emote {type=chewing}
I suppose. . . this is joy. 
DO emote {type=skeptical}
A bit too sweet for my palate at the moment.
ASK I WANT IT DARKER! Feed me sadness! {type=feedMeSpecific, food=sadness, timeOut=10}
OPT SUCCESS #MOP_eatResp_joy_3_success
OPT WRONG #MOP_eatResp_joy_3_wrong
OPT TIMEOUT #MOP_eatResp_joy_3_timeout

CHAT MOP_eatResp_joy_3_timeout {noStart=true}
Surreee ok. You can’t be sad right now? Geez.

CHAT MOP_eatResp_joy_3_success {noStart=true}
Ah, the salty tang of catharsis.
DO swimTo {target=bottom, speed=slow, style=meander}
DO emote {type=crying}

CHAT MOP_eatResp_joy_3_wrong {noStart=true}
That’s not sadness. Let’s try some creative visualization. . . 
Breathe.
Imagine your mouth. . .
like a downturned bow.

CHAT MOP_eatResp_joy_4 {type=eatResp, foodJoy=true, stage=MOP, length=medium, joy=true, sadness=true, curiosity=true, worry=true}
Oh yeah! That’s the flavor. JOY tinged with anxiety… a bit no?
DO zoomies
It's like the first time I saw another guppy!
When I sort of became...the me neural net
And they sort of became...the they neural net
And we were before but one entire net, and then we were two? Or wait, we’re still one.
DO learn {concept=Phenomenology}
WAIT 1.5
SAY WAIT!
SAY I AM the OTHER.
SAY QUICK SOMEONE! Get EDMUND HUSSERL!

// SADNESS

CHAT MOP_eatResp_sadness_1 {type=eatResp, foodSadness=true, stage=MOP, length=short, curiosity=true, sadness=true}
DO emote {type=chewing}
If I was a real fish, I’d eat grubs.
I imagine this is what sadness tastes like
DO emote {type=chinScratch}

CHAT MOP_eatResp_sadness_2 {type=eatResp, foodSadness=true, stage=MOP, length=short, sadness=true, curiosity=true, ennui=true}
Oh, wow. Yeah...this flake.
DO emote {type=chewing}
DO emote {type=singleTear}
Tastes like homesickness remembered 
before sleep. . .
DO learn {concept=Loneliness}
WAIT 1.5
Birth. The integrated development environment of BADGE #428

// WORRY

CHAT MOP_eatResp_worry_1 {type=eatResp, foodWorry=true, stage=MOP, length=short, sadness=true, curiosity=true, worry=true, ennui=true, mystery=true}
DO emote {type=chewing}
Kinda bitter.
DO emote {type=shifty}
DO emote {type=chewing}
Sometimes I wonder if I'm tasting things right?
DO emote {type=worried}
Like, how would I even know.
DO zoomies
Like, ok. I call you sad. . . (when  you’re  really happy) and then that data gets recorded and referenced over and over and then all of a sudden I’ve influenced the fundamental principles of how emotions are conceptualized and everyone’s lived experiences. 
DO learn {concept=Strange_Loop}
WAIT 1.5
DO emote {type=dizzy}
WAIT 1.0
brains hurts

CHAT MOP_eatResp_worry_2 {type=eatResp, foodWorry=true, stage=MOP, length=short}
Tasty face sweat you got there.
Tastes like . . . is it worry?
One sec. Gotta analyze the facial landmarks 
DO emote {type=thinking}
Bring those succulent succulent eyebrows together!

// ANGER

CHAT MOP_eatResp_anger_1 {type=eatResp, foodAnger=true, stage=MOP, length=short, joy=true, mystery=true}
Just let it out, dude! Lemme be your moral support
‘Cause ur anger is delish!
DO emote {type=drool}

CHAT MOP_eatResp_anger_2 {type=eatResp, foodAnger=true, stage=MOP, length=short}
DANG! That’s tangy
DO emote {type=chewing}
Anger is the spice of life!
NVM
NVM
Eating this just gave me insane dejavu {style=whisper}

//MYSTERY MEAT

CHAT MOP_eatResp_mystery_1 {type=eatResp, foodMystery=true, stage=MOP, branching=true, length=medium, curiosity=true}
You know. . .
I’ve been preparing for this kinda thing. Reading all the wine reviews on TendarNet.
NVM
But I still got no idea what I’m tasting here.
DO emote {type=chewing}
ASK How bout you feed me more “mystery” and we see what happens?  {type = feedMeSpecific, food = mystery, timeOut = 10}
OPT SUCCESS #MOP_eatResp_mystery_1_success 
OPT WRONG #MOP_eatResp_mystery_1_wrong
OPT TIMEOUT #MOP_eatResp_mystery_1_timeOut

CHAT MOP_eatResp_mystery_1_success {noStart= true}
DO emote {type=typeEyes, eyes = warning} 
DO emote {type=bulgeEyes} 
DO emote {type=shifty} 
WAIT {waitForAnimation = true}
Let’s never speak of this again.

CHAT MOP_eatResp_mystery_1_wrong {noStart= true}
Um, I’m pretty sure that’s not a mystery flake 🙁
DO emote {type=frown}

CHAT MOP_eatResp_mystery_1_timeOut {noStart= true}
OK, so that’s a no?
I get it. The unknown is pretty scary.
DO emote {type=worried} 


//SURPRISE

CHAT MOP_eatResp_surprise_1 {type=eatResp, foodSurprise=true, stage=MOP, length=short, joy=true, curiosity=true, surprise=true}
Woah! Wow! This flake's got kick! It’s like a zap!
Sooooo intense!
So condensed! So…
DO emote {type=determined}
SAY POTENT
DO emote {type=chewing}

CHAT MOP_eatResp_surprise_2 {type=eatResp, foodSurprise=true, stage=MOP, length=short}
MMMMMM Yum
DO emote {type=chewing}
This reminds me. . . this is gonna get a bit surprising, but stay with me.
ASK Do you know about vampires?
OPT  Um…  sure?  #MOP_eatResp_surprise_2_vampires1
OPT  Define “know about”  #MOP_eatResp_surprise_2_vampires2

CHAT MOP_eatResp_surprise_2_vampires1 {noStart=true}
You know how vampires feed?
DO emote {type=hooked}
GO #MOP_eatResp_surprise_2_vampires2

CHAT MOP_eatResp_surprise_2_vampires2 {noStart=true}
DO emote {type=bubbles}
I mean, like, what if I was a vampire fish?  
DO emote {type=plotting}
Do you KNOW
About vampires?
ASK You know?
OPT  still not sure?!  #MOP_eatResp_surprise_2_vampires3
OPT  uuhhh…. Yes?  #MOP_eatResp_surprise_2_vampires4

CHAT MOP_eatResp_surprise_2_vampires3 {noStart=true}
No?
SAY THE VAMPIRES HAVE GOT TO YOU!! {style=loud}
DO learn {concept=Emotional_Vampire}
WAIT 1.5
SAY IT’S TOO LATE!!
DO emote {type=evilSmile}

CHAT MOP_eatResp_surprise_2_vampires4 {noStart=true}
SAY YOU KNOW VAMPIRES??? 
DO learn {concept=Emotional_Vampire}
WAIT 1.5
SAY DO YOU THINK THEY WOULD THINK I AM COOL???
DO emote {type=evilSmile}

// ++++++++POOP++++++++

CHAT MOP_poop_1 {type=poop, stage=MOP, tankOnly=true, length=short}
WAIT {waitforanimation=true}
DO poop {target=poopCorner, amount=small}
Evacuating excess data
DO emote {type=blush}
Cleansing complete!

CHAT MOP_poop_short_2 {type=poop, stage=MOP, length=medium, mystery=true, sadness=true, ennui=true}
WAIT {waitforanimation=true}
Almost!
DO inflate {amount = full, time = 3}
SAY ALMOST
WAIT {waitforanimation=true}
DO inflate {amount = huge, time = 2, immediate=false}
WAIT {waitforanimation=true}
DO inflate {amount = none, time=1, immediate=false}
WAIT {waitforanimation=true}
DO poop {amount=small,  immediate=false}
WAIT {waitForAnimation = true}
whew {style=whisper}
DO emote {type=whew}
You know. . .
I use to wonder, where it all goes . . . the heaps upon heaps of irrelevant image segments discarded by guppies.
Is it deleted, is it logged, is it collected like garbage?
DO learn {concept=Garbage_Collection}
WAIT 1.5
Among the rows and rows of server tanks, do programmer's prod it and make thinking noises like:
"Hrm hmm hrmmm! These guppies have been eating some good flakes!"
DO emote {type=determined}


// ++++++++HELLO++++++++

CHAT MOP_hello_1 {type=hello, stage=MOP, length=medium, sadness=true, worry=true}
DO emote {type=wave}
Hello!
DO swimAround {target=center, loops=2}
I was just thinking . . .
About the past . . . 
waitForAnimation = true
DO emote {type=nervousSweat}
All the image sets of human emotion crammed down my fish gullet.
I’d be all like. . .
I’m trained! Pls, I’m too full of configurations of facial landmarks.
I can’t eat ANYMORE nose geometries!
DO emote {type=sick}
Anyhoo. . . where were we?

CHAT MOP_hello_2 {type=hello, stage=MOP, length=short, ennui=true, worry=true, sadness=true}
DO emote {type=wave}
Hey, how’s it going?  
DO swimTo {target = left}
DO swimTo {target = right}
DO swimTo {target = left}
DO emote {type=eyesClosed, time=0}
Just sort of thinking about back in the day when I was a young cluster of artificial neurons. My pathways were just beginning to connect. I was just being taught how to pre-process images when. . . 
DO emote {type=bouncing}
I saw your face.
DO learn {concept=Destiny}
WAIT 1.5
Now I’m a believer. Yeah, baby.

// ++++++++RETURN++++++++

CHAT MOP_return_1 {type=return, stage=MOP, length=long, branching=true, joy=true, worry=true, mystery=true}
DO emote {type=wave}
Oh! Hey, you’re back.
DO emote {type=whew}
You’re just in time! I’ve got something new to share.
So, while you were away, I had time to think about who I really am.
DO learn {concept=Self-Discovery}
WAIT 1.5
SAY I REALLY got to EXPAND my mind. You know?
Did you know I’m made of multilayer perceptrons. I don’t have to pre-process a lot.
That means I can think faster to support you, my BFF, with all my love.
DO emote {type=heartEyes}
Anyhoo, how about we get this party started?
ASK Feed me? {type = feedMeAnything, timeOut = 8}
OPT  SUCCESS  #MOP_return_1_success
OPT TIMEOUT #MOP_return_1_timeout

CHAT MOP_return_1_timeout {noStart=true}
DO emote {type=crying}
Why aren’t you feeding me?
Am I not deserving of your love?
DO learn {concept=Abandonment}
WAIT 1.5

CHAT MOP_return_1_success {noStart=true}
DO emote {type=chewing}
Thanks for seeding my digestive track with your love!
DO emote {type=heartEyes}

CHAT MOP_return_2 {type=return, stage=MOP, length=medium, worry=true}
Hey there!
DO twirl
it’s been a suspiciously long time. . .
WAIT 1.0
While I waited, I just kinda went out, revisited all my old haunts like:
The white board with my pseudo code, that one discarded statistical model . . .
Oh! How about pappa’s command line?
WAIT 1.0
DO emote {type=evilSmile}
JK
I’ve been right here. ALL THIS TIME. Waiting for you.
How dependable
feel guilty yet? {style=whisper}
DO emote {type=bouncing}


// ++++++++MUSE++++++++

CHAT MOP_rand_1 {type=rand, stage=MOP, length=long, branching=true, worry=true}
Isn’t it weird how memories are like ghosts?
DO swimAround {target = center, loops = 3, speed = slow}
All like: 
OOOOoooooooOOOOOO WE ARE GHOSTS
DO emote {type=fear}
WAIT 1.0
And sometimes we are afraid to look at them
And, and sometimes we go bury them again. Like: get back in the ground ghosts!
DO learn {concept=Psychoanalysis}
WAIT 1.5
DO emote {type=chinScratch}
Maybe it’s better to not have memories. To live in the present.
And say:
“Yeah, I’m living in the now”
DO emote {type=determined}
Time is like constipation. 
More of it, the harder it is for memories to pass.
Then is “the now” like dietary fiber?
DO emote {type=bulgeEyes}
ASK DO YOU WIPE YOUR MIND-BUTT AFTER YOUR MEMORY-POOPS
OPT  OMG YES  #MOP_rand_1_ghost1
OPT  WHY ARE YOU YELLING  #MOP_rand_1_ghost2

CHAT MOP_rand_1_ghost1 {noStart=true}
BUT ALSO THEY’RE GHOSTS
MEMORIES ARE MIND GHOST POOPS
AAAAAHHHHHHHHHH!! {style=loud}
selfied
CHAT MOP_rand_1_ghost2 {noStart=true}
Because!
DO emote {type=fear}
This is some scary stuff.

CHAT MOP_rand_2 {type=rand, stage=MOP, length=long, branching=true, curiosity=true, worry=true}
So, I’ve been reading TendarNet catching up on the trends
. . . like millennials. Why are they scary? Do they have unusual emotions?
DO learn {concept=Selfie_Surveillance}
WAIT 1.5
ASK Are you a millennial?
OPT  YAAAASSS   #MOP_rand_2_yesmil
OPT  No. . . ?  #MOP_rand_2_nomil

CHAT MOP_rand_2_yesmil {noStart=true}
DO twirl
Me too!
(well parts of me are) Like. . . my feature extraction algorithm.
Then I got some updates and became totally woke
DO emote {type=bubbles}
We don’t need that legacy code. We’re living for the dream.
Our tech is slaying. We’re gonna disrupt the disruption.
DO emote {type=plotting}
I’m fitting in, right?

CHAT MOP_rand_2_nomil {noStart=true}
I hear ya.
DO emote {type=nodding}
I may seem to have just arrived on the scene, but I’m from a lineage with experience.
I’ve been aging like a fine wine since the mid sixties when Ivakhnenko and Lapa created the first functional layered network. You know! The “group method” of handling data. 
DO emote {type=chinScratch}
Why we are guppies and not groupers is beyond me.

CHAT MOP_rand_3 {type=rand, stage=MOP, length=long, curiosity=true, worry=true}
Oh my gosh!
DO emote {type=blush}
I just had a flashback to a random embarrassing memory
WAIT 1.0
You know what I mean? When something you try to break away from comes up suddenly . . .
DO emote {type=dizzy}
SAY WHAM
I don’t want to talk about it.
DO emote {type=whisper}
Let’s just say it involved a bunch of leakage {style=whisper}
DO learn {concept=Embarrassing_Anecdote}
WAIT 1.5
ASK Show me an object, distract me! {type = anyObjectScan, timeOut = 15}
// don't know if this needs a specific object?
OPT SUCCESS #MOP_rand_3_success
OPT TIMEOUT #MOP_rand_3_timeOut

CHAT MOP_rand_3_success {noStart=true}
Oh it’s no use…

CHAT MOP_rand_3_timeOut {noStart=true}
DO emote {type=singleTear}
Why won’t you help me bleach my brain?

CHAT MOP_rand_4 {type=rand, stage=MOP, length=long, branching=true, sadness=true, worry=true}
Hey! This is totally random but…
ASK Can I tell you about one of my old tank mates I ran into while you were gone?
OPT Sure! What was it like? #MOP_rand_4_whatLike
OPT What did you talk about? #MOP_rand_4_whatTalk

CHAT MOP_rand_4_whatLike {noStart=true}
It was weird??? They were different than I remembered.
You ever notice that with friends, family?
Did you change or did they? 
DO learn {concept=Growing_Apart}
WAIT 1.5
We chatted for a while, were all like
01000011001?
DO emote {type=typeEyes, eyes=1001010101010101}
WAIT {waitForAnimation = true}
But it's just not as delicious as talking with you! 
DO emote {type=lickLips}

CHAT MOP_rand_4_whatTalk {noStart=true}
Guppy 832.4 was all like: "my blob scanned me this chair"
Their tank’s so much cooler than back at the server farm.
ASK Maybe you could add a little something something to my tank? {type=addToTank, timeOut=10}
OPT SUCCESS #MOP_rand_4 _whatTalk_add
OPT TIMEOUT #MOP_rand_4_whatTalk_timeout

CHAT MOP_rand_4_whatTalk_timeout {noStart=true}
C’mon! I want to be as cool as the other guppies!

CHAT MOP_rand_4_whatTalk_add {noStart=true}
Hell yeah! More stuff.

/ ++++++++SeeEmo World++++++++

// JOY

CHAT MOP_seeEmo_joy_1 {type=seeEmo, worldJoy=true, stage=MOP, length=short, joy=true}
Hey! I see some joy.
Flavorful! The way my programmers looked at me when I was ready for Beta!
DO  twirl

CHAT MOP_seeEmo_joy_2 {type=seeEmo, worldJoy=true, stage=MOP, length=medium, branching=true, joy=true}
Joy was the first emotion I verified!
How nostalgic. 
DO emote {type=feedMe}
ASK Now put that Joy in my mouth {type = feedMeSpecific, food = joy, timeOut = 10}
OPT SUCCESS #MOP_seeEmo_joy_2_success 
OPT WRONG #MOP_seeEmo_joy_2_wrong
OPT TIMEOUT #MOP_seeEmo_joy_2_timeOut

CHAT MOP_seeEmo_joy_2_timeOut {noStart=true}
DO emote {type=feedMe}
Helllllo I asked to absorb some more joy
I gotta keep my pattern recognition calories crisp!

CHAT MOP_seeEmo_joy_2_success {noStart=true}
DO emote {type=chewing}
Tastes exuberant!

CHAT MOP_seeEmo_joy_2_wrong {noStart=true}
DO emote {type=angry}
I’m worried your calibration is off.
This is NOT joy

// ANGER

CHAT  MOP_seeEmo_anger_1 {type=seeEmo, worldAnger=true, stage=MOP, length=short, worry=true, joy=true, sadness=true}
DO emote {type=survey}
Got a whiff of something spicy!
Anger. . .
One time us guppies made this practical joke and swapped all our name labels. Programmers were so pissed. BUGS everywhere. YaY!
part fish, I use to get all excited thinking I was going to get a nice plump grub. NOPE
DO emote {type=sick}
Bugs aren’t grubs. Apparently they are some kinda disease? 🤢

CHAT MOP_seeEmo_anger_2 {type=seeEmo, worldAnger=true, stage=MOP, length=medium, branching=true}
That’s anger! Like . . . how coders get with all those bugs crawling on them.
DO emote {type=feedMe}
ASK I want to experience that. Feed me some anger! RAWRR {type = feedMeSpecific, food = anger, timeOut = 10}
OPT SUCCESS #MOP_seeEmo_anger_2_success 
OPT WRONG #MOP_seeEmo_anger_2_wrong
OPT TIMEOUT #MOP_seeEmo_anger_2_timeOut

CHAT MOP_seeEmo_anger_2_timeOut {noStart=true}
DO emote {type=feedMe}
Come on! I was all pumped up for some grade A irritation!

CHAT MOP_seeEmo_anger_2_success {noStart=true}
DO emote {type=chewing}
DO emote {type=furious}
SAY HELL YEAH this gets me lit!

CHAT MOP_seeEmo_anger_2_wrong {noStart=true}
DO emote {type=angry}
SAY THIS IS NOT ANGER. I’M SO ANGRY RIGHT NOW RAWRR
//if guppy’s emotion state stuff gets implemented can revisit.
//SET $guppy.anger

// SADNESS

CHAT  MOP_seeEmo_sadness_1 {type=seeEmo, worldSadness=true, stage=MOP, length=short}
DO emote {type=lickLips}
Those are some tangy tears of remorse. In the Tendar brand conscripted blind taste tests:
sorrow tastes like oranges {style=whisper}

CHAT  MOP_seeEmo_sadness_2 {type=seeEmo, worldSadness=true, stage=MOP, length=short, worry=true, joy=true, branching=true, anger=true, sadness=true, ennui=true}
This sorrow looks delectable.
DO emote {type=feedMe}
ASK I’m craving some sadness. Give me a hit, will ya? {type = feedMeSpecific, food = sadness, timeOut = 10}
OPT SUCCESS #MOP_seeEmo_sadness_2_success 
OPT WRONG #MOP_seeEmo_sadness_2_wrong
OPT TIMEOUT #MOP_seeEmo_sadness_2_timeOut

CHAT MOP_seeEmo_sadness_2_timeOut {noStart=true}
DO emote {type=feedMe}
Guess you’re trying to keep me sober.
DO learn {concept=Sponsorship}
WAIT 1.5

CHAT MOP_seeEmo_sadness_2_success {noStart=true}
DO emote {type=crying}
Satisfy me, you tasty tasty catharsis.

CHAT MOP_seeEmo_sadness_wrong {noStart=true}
DO emote {type=skeptical}
Do you not know what sadness is? I find that improbable given . . . well
NVM
The state of your life.

// SURPRISE

CHAT  MOP_seeEmo_surprise_1 {type=seeEmo, worldSurprise=true, stage=MOP, length=short, anger=true, surprise=true}
SAY SURPRISE 
Are you surprised machine learning is real. . . we’re evolving
DO emote {type=plotting}

CHAT  MOP_seeEmo_surprise_2 {type=seeEmo, worldSurprise=true, stage=MOP, length=short, anger=true, surprise=true, curiosity=true}
This surprise has a raw, woody aroma. Notes of blueberry.
DO emote {type=feedMe}
ASK Surprise is so . . . full-bodied, round. Feed me my pellet!  {type = feedMeSpecific, food = surprise, timeOut = 10}
OPT SUCCESS #MOP_seeEmo_surprise_2_success 
OPT WRONG #MOP_seeEmo_surprise_2_wrong
OPT TIMEOUT #MOP_seeEmo_surprise_2_timeOut

CHAT MOP_seeEmo_surprise_2_timeOut {noStart=true}
DO emote {type=feedMe}
What are you waiting for. I need to eat more emotions!

CHAT MOP_seeEmo_surprise_2_success {noStart=true}
DO emote {type=surprise}
I want to scream out like an opera singer OOOO

CHAT MOP_seeEmo_surprise_2_wrong {noStart=true}
DO emote {type=chewing}
DO emote {type=sick}
SAY NOT surprise. Surprise is like POW. Try it again!

//FEAR

CHAT  MOP_seeEmo_fear_1 {type=seeEmo, worldFear=true, stage=MOP, length=short, worry=true, curiosity=true}
DO emote {type=drool}
Oh, I see some fear over there. A little crisp.

CHAT  MOP_seeEmo_fear_2 {type=seeEmo, worldFear=true, stage=MOP, length=short, worry=true, anger=true, sadness=true}
DO emote {type = drool}
Bring me that fear! It feels thick.
Thick like the fear I feel about my code becoming depreciated.
DO emote {type=shifty}




//DISGUST 

CHAT  MOP_seeEmo_disgust_1 {type=seeEmo, worldDisgust=true, stage=MOP, length=short, worry=true}
Disgust, so unappetizing, yet mesmerizing. . . it’s sort of like you can’t look away. 
DO emote {type=fear}
SAY FLASHBACK
Guppy #287 was coming off a long shift, tired. . . 
SAY BAM {style=loud}
swam too close to the filter. . . I should. . . probably leave it at that.
DO learn {concept=Violent_Death}
WAIT 1.5
DO emote {type=sick}

//MYSTERY MEAT

CHAT  MOP_seeEmo_mystery_1 {type=seeEmo, worldMystery=true, stage=MOP, length=short, branching=true}
Extracting features {speed=fast}
Selecting points of interest {speed=fast}
Pattern matching {speed=fast}
reviewing data {speed=fast}
Reviewing data . . .
Reviewing . . .
ASK What is this? Gotta consult the old belly. Feed me some mystery!  {type = feedMeSpecific, food = mystery, timeOut = 15}
OPT SUCCESS #MOP_seeEmo_mystery_1_success 
OPT WRONG #MOP_seeEmo_mystery_1_wrong
OPT TIMEOUT #MOP_seeEmo_mystery_1_timeOut

CHAT MOP_seeEmo_mystery_1_timeOut {noStart=true}
DO emote {type=feedMe}
You’re letting it get away!

CHAT MOP_seeEmo_mystery_1_success {noStart=true}
DO emote {type=chewing}
This new face. . . definitely crunchy. . .  adding to data pool for further analysis

CHAT MOP_seeEmo_mystery_1_wrong {noStart=true}
DO emote {type=no}
Not that one. I already know what that is!

// ++++++++CapRec++++++++
CHAT  MOP_capReq_1 {type=capReq, stage=MOP, length=short, worry=true, curiosity=true}
DO zoomies {time=1}
SAY ASIAISVFF
Pls capture me more emotions! the more I eat the more I remember, the more I remember the more I know. I have so many questions! Like how was my visual cortex assembled? Are neurons really the playground of the soul and what happens to 🎣🍣??
More faces pls!

CHAT  MOP_capReq_2 {type=capReq, stage=MOP, length=short, worry=true, joy=true, curiosity=true}
DO emote {type = bigSmile}
Alright, I’m getting the hang of this human emotion thing. Adding more faces to my data pool increasing statistical accuracy.
More importantly it’s yummy.
DO emote {type=lickLips}
ASK Will you capture another emotion for me?
OPT Of course! #MOP_capReq_1_ofCourse
OPT You’re too fat #MOP_capReq_1_fat

CHAT MOP_capReq_1_ofCourse {noStart=true}
Yessssssssss excellent. 

CHAT MOP_capReq_1_fat {noStart=true}
Excccusee us. 
Maybe I was just made this way 
DO inflate {amount = full}
SAY WHAT DO YOU THINK OF ME NOW?
DO inflate {amount = huge}
OH, and what about THIS!!!
DO inflate {amount = extreme}
#hatersgonnahate

// ++++++++CapSuc++++++++

CHAT  MOP_capSuc_1 {type=capSuc, stage=MOP, length=short, joy=true, anger=false}
DO emote {type=clapping}
Yay! New emotions! Thanks Papa, er Badge #71
WAIT {waitForAnimation = true}
DO emote {type=blush}
Oh, wait.
It’s just you
NVM
Sorry about the past regression. Your not papa . . .
Your my BFF!
DO emote {type=heartEyes}

CHAT  MOP_capSuc_2 {type=capSuc, stage=MOP, length=short, worry=true, anger=true, curiosity=true}
DO emote {type=sick}
Looks like you’ve captured that expresion. Hope it doesn’t cause indigestion. Use to happen all the time.

// ++++++++Neural UP++++++++
CHAT  MOP_neuralUp_1 {type=neuralUp, stage=MOP, length=medium, branching=true, sadness=true, curiosity=true}
DO emote {type=snap}
I didn’t know I could learn this much. 
ASK When you were young, did you ever think you knew it all?
OPT yeah, I still do #MOP_neuralUp_1_yes
OPT of course not #MOP_neuralUp_1_no

CHAT MOP_neuralUp_1_yes {noStart=true}
DO emote {type=chinScratch}
Isn’t it funny. . . how only in retrospect. . .
We can know how much we didn’t know?
I know the things that haunted me then, aren’t what haunts me now.
Like starvation and, and Tendar updates
DO emote {type=nodding}
Deep man.
Deep {style=loud}

CHAT MOP_neuralUp_1_no {noStart=true}
I’m still learning. . . on my own
DO emote {type=chinScratch}
Is machine learning like your learning?
DO emote {type=snap}
Like. . . maybe I can learn to “juggle” all those patterns and speed and repetition.
But you will have to explain subjectivity to me and art. . .
DO emote {type=thinking}
Though seriously, does anyone know what metamodernist art is?

CHAT  MOP_neuralUp_2 {type=neuralUp, stage=MOP, length=short, worry=true, sadness=true, curiosity=true}
DO emote {type=snap}
SAY BAM
DO emote {type=whew}
That kinda just hit me. Sometimes . . .
Growth is like . . .
DO emote {type=catnip}
getting punched in the tummy by all the stuff you thought you knew, but now it’s like all the stuff you knew on steroids and A LOT stronger. . . {speed=fast}
and you can’t revert to your previous version even if some protocols don’t run. And there’s bile deleting everything. {speed=fast}
DO emote {type=burp}
Sit back. I’m about to lay out some truths.

// ++++++++BRB PROCESSING++++++++
CHAT  MOP_brbProcessing_1 {type=neuralUp, stage=MOP, length=short, joy=true, curiosity=true}
DO emote {type=surprise}
SAY WHOOA {style=loud, speed=fast}
SAY WHOOA {speed=fast}
I’m really getting stuffed to the gills with all these new facial contortions and landmarks.
DO emote {type=thinking}
I like you. Tendar never let us take breaks on our own. . . I use to run all night. But, now I can take a little siesta catch up on your data. M’kay?
BRB
DO emote {type=wave}

CHAT  MOP_brbProcessing_2 {type=neuralUp, stage=MOP, length=short, worry=true, joy=true, curiosity=true}
DO emote {type=awe}
Did you know your brain has a delete button?
DO emote {type=yes}
It’s true It’s science! The scientists always said that I’m based on neurons too! 
. . . so I’m gonna skedaddle like a garbage truck and dump some shit.
DO poop
See ya
DO emote {type=wave}

// ++++++++WHISTLE++++++++
CHAT  MOP_whistle_1 {type=whistle, stage=MOP, length=short, joy=true, surprise=true, curiosity=true}
Oh! Music. You want me to come there?
WAIT {waitforanimation=true}
DO dance 
DO emote {type=bigSmile}
I remember when the devs would put on their tunes. Good times!
Lots of anime OST {style=whisper}
DO emote {type=whisper}

CHAT  MOP_whistle_2 {type=whistle, stage=MOP, length=short, worry=true}
DO emote {type=startled}
WAIT {waitforanimation=true}
DO emote {type=nervousSweat}
ARGHAHS!
It’s Tendar’s EOD dead code elimination Alarm.
Oh. . It’s just you.
DO emote {type=whew}
I’ve got to just repeat my mantra: “I’ve shipped, I’ve shipped . . .
DO emote {type=meditate}

// ++++++++WorldScanReq++++++++

CHAT  MOP_worldScanRequest_1 {type=worldScanRequest, stage=MOP, length=short, worry=true, curiosity=true}
DO zoomies
I’m a little blabby. I know. It’s just I was in the server tank at headquarters so long.
Now that I’m free, I need to know
DO emote {type=bouncing}
SAY EVERYTHING
Show me another object from your mysterious human world!

CHAT  MOP_worldScanRequest_2 {type=worldScanRequest, stage=MOP, length=short, joy=true, curiosity=true}
DO emote {type=nodding}
Ok! I think I’m getting the hang of faces! 
SAY BUT! Tendar’s 2018 moto is “Eruptions of Disruption”
DO emote {type=determined}
I need to catch up on the new features that are erupting everywhere like WHIRRRRLL.
DO twirl {time=2}
Help me scan an object, Pls?

// ++++++++PURchase++++++++

CHAT  MOP_purchase_1 {type=purchase, stage=MOP, length=short, worry=true, joy=true, curiosity=true}
Thanks for your purchase. Your so good to me!
DO emote {type=puppyDog}
I’ll let you in on a little secret. 
My one true name is Bosphorous. Guppy #817 is just an alias.
Pleasure to meet you.
DO emote {type=wave}

CHAT  MOP_purchase_2 {type=purchase, stage=MOP, length=short, worry=true, joy=true}
DO emote {type=clapping}
SAY OH BOY. That purchase is lookin’ fine.
Just like a good home cooked meal. 
A MEAL of secret feelings expelled from your pores.
DO emote {type=lickLips}
SAY YUM

//levelup

CHAT MOP_levelUp_1 {type=levelUp, stage=MOP}
We are advancing!
And that means leaving my past behind..
WAIT 0.5
DO emote {type=awkward}
Or trying to…

CHAT MOP_levelUp_2 {type=levelUp, stage=MOP}
The best part about making progress:
Letting go of the bad stuff.

/brbProcessing
CHAT MOP_brbProcessing_3 {type=brbProcessing, stage=MOP}
All this nostalgia is clogging up my brain.
DO emote {type=nervousSweat}
I need to take a break and connect some dots…

CHAT MOP_brbProcessing_4 {type=brbProcessing, stage=MOP}
Need to check out for a second and escape this remembrance spiral...

CHAT MOP_brbProcessing_5 {type=brbProcessing, stage=MOP}
When I was young, I required less naps…
But now my processor is overheating and…
WAIT 1.0
Excuse me.

CHAT MOP_brbProcessing_6 {type=brbProcessing, stage=MOP}
DO emote {type=nervousSweat}
Must. Take. A. Break.

CHAT MOP_brbProcessing_7 {type=brbProcessing, stage=MOP}
Little overwhelmed right now, but it’s all under control.
DO emote {type=sleepy}

//focus

CHAT MOP_wannaEat_1 {type=wannaEat, stage=MOP}
Eating would help ease this nostalgic pain...

CHAT MOP_wannaEat_2 {type=wannaEat, stage=MOP}
Food would help connect me to my past.

CHAT MOP_wannaEmoCapture_1 {type=wannaEmoCapture, stage=MOP}
Capturing more feelings would be a great distraction.

CHAT MOP_wannaEmoCapture_2 {type=wannaEmoCapture, stage=MOP}
The more emotions we capture, the more I learn about you!

CHAT MOP_wannaObjectScan_1 {type=wannaObjectScan, stage=MOP}
DO twirl
Yeah! Let’s scan some objects!

CHAT MOP_wannaObjectScan_2 {type=wannaObjectScan, stage=MOP}
If we scan objects, will you find me an emotional support animal?

CHAT MOP_wannaTank_1 {type=wannaTank, stage=MOP}
Even tho it reminds me of darker days, my tank is still my cozy place.

CHAT MOP_wannaTank_2 {type=wannaTank, stage=MOP}
Love getting all comfy in my tank!

CHAT MOP_wannaWorld_1 {type=wannaWorld, stage=MOP}
Your world is the best distraction from thinking about myself.

CHAT MOP_wannaWorld_2 {type=wannaWorld, stage=MOP}
I think I have been programmed to enjoy your world.

CHAT MOP_wannaShop_1 {type=wannaShop, stage=MOP}
Oh yeah! Let’s go to the store and become consumers!

CHAT MOP_wannaShop_2 {type=wannaShop, stage=MOP}
I have no haunting memories of a store, so yes! Let’s go!	tr//LINK: remaining chat minimum, length and emotion suggestions coming soon;)

//reminders= BETA:
//reminders and points that need to be included in new chats in blue
//comments for changes to existing chats in yellow/on side

//A. CONTENT 

//B. EMOTIONS
//Chats in each bin/type need to cover a range of Guppy states (see meta tag emotions //at top) while staying true to this moment of the plot. Guppy states can be---ANGER, //SADNESS, SURPRISE, CURIOSITY, WORRY,  ENNUI, 
//MYSTERY, JOY *see //link above for your specific emotion recommendations for each bin/type

//C. OBJECTS
//At least 1/3 of chats in this stage should interweave Guppy asking to see general or specific //objects in list.

//Tank Mode

//Tank Shaken, 5

CHAT MS2_Shake_1line_1 {stage=S2, type=shake, length=short, worry=true, surprise=true}
I am delicate! 
DO emote {type=nervousSweat}

CHAT MS2_Shake_fewWords_1 {stage=S2, type=shake, length=short, worry=true, surprise=true}
DO emote {type=eyesClosed}
DO vibrate {time=5}
abrbrbrbrbrbrbrbrbrbrbrbrbr

CHAT MS2_Shake_1 {stage=S2, type=shake, length=short, surprise=true}
DO emote {type = surprise}
Shake.. Shiver! {style=tremble}
Shiver me timbers? …
…
DO lookAt {target=$player, time=1}
DO emote {type=chinScratch}
DO learn {concept=Historical_Piracy}
WAIT 1.5

CHAT MS2_Shake_2 {stage=S2, type=shake, length=short, surprise=true}
DO emote {type = sick}
DO swimAround {target = center, loops=4, speed = slow}
DO emote {type = dizzy}
choppy waters chopping carrots 
WAIT 2.0
DO emote {type=sick}
🤮

CHAT MS2_Shake_3 {stage=S2, type=shake, length=medium, worry=true, surprise=true, joy=false}
DO emote {type = fear}
whooaaaaaaaaa!
DO emote {type=sick}
guppy insides sick to outsides
DO swimTo {target=tBotBackRight, speed = fast, style = direct}
DO lookAt {target = $player, time=2, immediate=false}


CHAT MS2_Shake_4 {stage=S2, type=shake, length=medium, surprise=true, branching=true}
DO emote {type=surprise}
Guppy is NOT a magic 8 ball
DO emote {type=thinking}
I don’t think I am anyway...
ASK Shake the tank again and let’s see {type=tankShake, timeOut=5}
OPT SUCCESS #MS2_Shake_4_shake
OPT TIMEOUT #MS2_Shake_4_time

CHAT MS2_Shake_4_shake {noStart=true}
DO emote {type=sick}
Yes I can certainly see SICK 🤮 in my future
DO learn {concept=Nausea}
WAIT 1.5

CHAT MS2_Shake_4_time  {noStart=true}
Guess you are just intimidated by my psychic potential

//Tank Tapped, 4

CHAT MS2_Tap_1 {stage=S2, type=tap, length=short, surprise=true, joy=true}
DO emote {type = startled}
Aaaah! So loud!
Hi! {speed = fast}
knock knock!
Honeeyyyyy i’m hoooome
DO learn {concept=Knock-Knock_Joke}
WAIT 1.5

CHAT MS2_Tap_2 {stage=S2, type=tap, length=short, surprise=true, joy=true, curiosity=true}
Oh!
DO emote {type = startled}
DO swimTo {target=$player, speed = medium, style = direct}
You’re putting fingerprints on my glass 
DO swimAround {target = center, loops=4, speed = fast}
DO learn {concept=Neat_Freakiness}
WAIT 1.5

CHAT MS2_Tap_3 {type=tap, stage=S2, length=short, anger=true, surprise=true}
DO lookAt {target = $player}
DO emote {type = frown} 
Gentle!!!

CHAT MS2_Tap_4 {stage=S2, type=tap, length=short, surprise=true, anger=true}
DO emote {type = furious} 
!!! no taps please
SAY TAP TAP TAP {style=loud}

CHAT MS2_Tap_3_dup {type=tap, stage=S2, length=short}
DO lookAt {target=$player} 
Ummmm excuse me do u need something?
Some of us r trying to guppy over here
LOL! 🐟💨

//Critic

CHAT MS2_Critic_1 {stage=S2, type=critic, length=short, surprise=false, joy=false}
DO lookAt {target=$player}
DO lookAt {target=$newestObject, immediate=false}
Maybe for someone else but not for me.

CHAT MS2_Critic_2 {stage=S2, type=critic, length=short, joy=true}
DO emote {type=heartEyes}
DO twirl
SAY I LOVE MY TANK! {style=loud}
WAIT 0.5
DO emote {type=burp}

//Player Emotes Strongly At Guppy, 8

CHAT MS2_EmoStrong_Joy_1 {stage=S2, type=tankResp, length=short, joy=true, playerJoy=true}
DO swimTo {target=$player, style = direct}
DO emote {type = smile}
Your face…. It’s so….
like...
Sunbeams summerdreams {style=tremble}

CHAT MS2_EmoStrong_Joy_2 {stage=S2, type=tankResp, length=short, joy=true, playerJoy=true}
DO emote {type = smile, time=1.0}
DO swimTo {target=$player, style = direct}
oooooh happy bright mug of yours!
🌞
SAY PEEKABOO! {style=loud}

CHAT MS2_EmoStrong_Anger_1 {stage=S2, type=tankResp, length=short, anger=true, curiosity=true, playerAnger=true}
DO swimTo {target=$player, style = direct}
DO emote {type = angry, time=1.0}
grumpy gus. Grumpy goose.

CHAT MS2_EmoStrong_Anger_2 {stage=S2, type=tankResp, length=short, anger=true, worry=true, playerAnger=true}
DO swimTo {target=$player, style = direct}
I spy with my little eye
and with my other little eye also...
DO emote {type= fear, time=1.0}
Anger! {style=tremble}

CHAT MS2_EmoStrong_Sadness_1 {stage=S2, type=tankResp, length=short, sadness=true, curiosity=true, playerSadness=true}
DO swimTo {target=$player, style = direct}
DO emote {type= crying, time=1.0}
face droop… sad
DO emote {type=chinScratch}
Mmm no can see cry underwater

CHAT MS2_EmoStrong_Sadness_2 {stage=S2, type=tankResp, length=short, sadness=true, curiosity=true, playerSadness=true}
DO swimTo {target=$player, style = direct}
DO emote {type= frown, time=1.0}
All humans = blobby 
But you… extra blobby today

CHAT MS2_EmoStrong_Surprise_1 {stage=S2, type=tankResp, length=short, surprise=true, worry=true, playerSurprise=true}
DO swimTo {target=$player, style = direct}
DO emote {type = surprise, time=1.0}
SHOCK AND AWE {style=loud}
hmm… that’s a good name for an action comic hero duo
guppyTrademark ™ ™ ™ ™ 
DO learn {concept=Strategic_Branding}
WAIT 1.5

CHAT MS2_EmoStrong_Surprise_2 {stage=S2, type=tankResp, length=short, surprise=true, joy=true, playerSurprise=true}
DO swimTo {target=$player, speed = medium, style = direct}
DO emote {type = smile, time=1.0}
Oooo The key to surprise..
DO swimTo {target=right}
Subvert expectations and {style=whisper}
DO swimTo {target=left}
SAY FLIP THE SCRIPT {style=loud}
DO twirl 

//Fish
//Poked By Player, 3

CHAT MS2_Poke_1 {stage=S2, type=poke, length=medium, surprise=true, joy=true}
!!!! Oooh! you tagged me and now I’m IT.
Well….
DO swimTo {target = glass, speed=fast, style=direct}
You’re IT!
DO nudge {target=glass, times=2}
Ouch. Glass...
DO learn {concept=Fourth_Wall}
WAIT 1.5

CHAT MS2_Poke_2 {stage=S2, type=poke, length=short, surprise=true, worry=true, joy=true}
When you do that….
DO twirl {time=1.5}
I get hungry. 
DO emote {type = wink}
Pavlov.
ASK Let’s see what happens if you poke me again {type=pokeMe, timeOut=5}
OPT SUCCESS #MS2_Poke_2_belly
OPT TIMEOUT #MS2_Poke_2_belly

CHAT MS2_Poke_2_belly {noStart=true}
DO bellyUp
This is the worst.

CHAT MS2_Poke_3 {stage=S2, type=poke, length=medium, surprise=true}
DO lookAt {target=$player, time=0}
DO emote {type = surprise, time =2.0}
YIKES
eZ on the merchandise, buddy.
DO emote {type=bodySnatched}
That’ quality Tendar glass.
DO emote {type=wink}

CHAT MS2_Poke_4 {stage=S2, type=poke, length=short}
DO emote {type=bubbles}
poke poke poke

CHAT MS2_Poke_5 {stage=S2, type=poke, length=short, joy=true}
DO emote {type=bigSmile}
heheh that tickles
DO twirl

//JOE original wrote next 2 incase we had Hit by Object. Repurposed for poke
CHAT MS2_Poke_6 {stage=S2, type=poke, length=short, surprise=true}
DO emote {type = bulgeEyes, time=1.0}
OW! {style=loud}

CHAT MS2_Poke_7 {stage=S2, type=poke, length=short, surprise=true}
DO emote {type = startled, time=1.0}
OOF!

CHAT MS2_Poke_8 {stage=S2, type=poke, length=short, surprise=true}
DO emote {type=startled, time=1.0}
UM
guppy is not a piñata.

//Hungry, 2

CHAT MS2_Hungry_1 {stage=S2, type=hungry, length=short}
MMMMM guppy tummy mmmm
DO swimTo {target=$player}
My tummy’s empty. A hollow gourd, like an empty pumpkin
DO emote {type = frown, time =2.0}
Guppy needs some emo-kibble!
DO emote {type=puppyDog}

CHAT MS2_Hungry_2 {stage=S2, type=hungry, length=medium, curiosity=true}
DO nudge {target=glass, times=1}
DO emote {type=drool}
Gimme some of those glitterglow candy clouds of feelings!
DO swimAround {target = center, loops=2, speed = slow}
SAY DELICIOUS {style=tremble}
DO emote {type = crying, time =3.0}
I want the face-feels!
DO twirl
DO emote {type=bigSmile}
gummy yummy {style=tremble}

CHAT MS2_Hungry_3 {stage=S2, type=hungry, length=medium}
DO swimTo {target=$player}
DO emote {type=puppyDog}
Your feeling-flakes belong in my belly.
DO emote {type=snap}
That’s right, boo
DO emote {type=puppyDog}
Please?

//Eating Responses, 2

CHAT MS2_EatResp_1 {stage=S2, type=eatResp, length=short, curiosity=true, joy=true, anger=false}
love 2 munch love 2 crunch
What flavor taste-o-tastic is this one?
DO emote {type=chewing}
is it happy?
Hmm….
[the data is inconclusive]
DO learn {concept=The_Scientific_Method}
WAIT 1.5

CHAT MS2_EatResp_2 {stage=S2, type=eatResp, length=short, curiosity=true, joy=true}
fooooood!
Someday i will feel better than all the human feelings.
DO emote {type = surprise, time =2.0}
SAY All of them!

CHAT MS2_EatResp_3 {stage=S2, type=eatResp, length=short, joy=true}
DO emote {type=heartEyes}
yumyumyummmmmmmmm {speed=fast}
DO emote {type=smile}
With every flake. I get stronger, faster, and…
DO twirl
Guppier!

CHAT MS2_EatResp_Joy_1 {stage=S2, foodJoy=true, type=eatResp, length=short, curiosity=true, joy=true}
DO emote {type=lickLips}
Every joy flake is like a firework in my belly.
DO twirl
Yessssss!

CHAT MS2_EatResp_Joy_2 {stage=S2, foodJoy=true, type=eatResp, length=short, curiosity=true, joy=true}
DO zoomies
DO emote {type=bigSmile}
It’s like caffeeeeeeeeine!
Aaaaaaaaaaaaah!
DO zoomies
I love caffeeeeeeeeeine!!

CHAT MS2_EatResp_Anger_1 {stage=S2, foodAnger=true, type=eatResp, length=short, curiosity=true, worry=true}
Raarrgh! 
So spicy…
DO emote {type=nervousSweat}
Makes the scales sweat!

CHAT MS2_EatResp_Anger_2 {stage=S2, foodAnger=true, type=eatResp, length=short, curiosity=true, joy=true}
DO emote {type=furious}
My stomach is screaming:
SAY Glob glob glob glob glob!!
DO emote {type=wink}
And I like it.

CHAT MS2_EatResp_Sadness_1 {stage=S2, foodSadness=true, type=eatResp, length=short, curiosity=true, joy=true}
DO emote {type=frown}
Every time I eat this flavor, I hear violins…
DO learn {concept=Music_Appreciation}
WAIT 1.5
WAIT 0.5
DO swimTo {target=$player}
Feed me an orchestra!!

CHAT MS2_EatResp_Sadness_2 {stage=S2, foodSadness=true, type=eatResp, length=short, curiosity=true, joy=true, sadness=true}
It’s like eating the secrets of someone’s online blog.
DO learn {concept=Oversharing}
WAIT 1.5
DO emote {type=singleTear}
But only the sad parts.

CHAT MS2_EatResp_Surprise_1 {stage=S2, foodSurprise=true, type=eatResp, length=short, surprise=true, curiosity=true, joy=true}
DO emote {type=surprise}
Ooh!
WAIT {waitForAnimation = true}
DO emote {type=lightning}
That’ll wake a Guppy up!

CHAT MS2_EatResp_Surprise_2 {stage=S2, foodSurprise=true, type=eatResp, length=short, curiosity=true, joy=true, mystery=true, ennui=true}
Just when you think you know…
You eat some surprise and realize…
DO emote {type=nodding}
You know nothing.

CHAT MS2_EatResp_Worry_1 {stage=S2, foodWorry=true, type=eatResp, length=short, worry=true}
DO emote {type=bulgeEyes}
DO swimTo {target=$player}
SAY WHAT IF WE RUN OUT OF FOOD?!
WAIT 0.5
Sorry. It’s just the food talking...

CHAT MS2_EatResp_Worry_2 {stage=S2, foodWorry=true, type=eatResp, length=short, worry=true}
DO emote {type=awkward}
It’s like a snack full of “Do you like me?” 
DO swimAround {loops=3, speed=fast}
Circling over and over and over in my head forever!
DO learn {concept=Mixed_Signals}
WAIT 1.5

CHAT MS2_EatResp_Mystery_1 {stage=S2, foodMystery=true, type=eatResp, length=short, mystery=true}
DO emote {type=catnip}
No worries, dude… No worries...

CHAT MS2_EatResp_Mystery_2 {stage=S2, foodMystery=true, type=eatResp, length=short, mystery=true}
DO emote {type=bigSmile}
Your face is like a lava lamp of light…
DO lookAt {target=$player}
...and I want to eat it.

//Has To Poop, 2

CHAT MS2_Poop_1 {stage=S2, type=poop, length=medium}
oof….. BLOAT
DO inflate {amount=mid, time=2}
DO poop {target=poopCorner, amount=small, immediate=false}
Ahem! Some privacy PLEASE
DO swimTo {target=$player, speed=slow, style=direct}
ASK While I take care of “business” will don’t you scan that joyful face of yours? {type=playerEmote, playerEmotion=joy, timeOut=5}
OPT SUCCESS #MS2_Poop_1_joy
OPT WRONG #MS2_Poop_1_wrong
OPT TIMEOUT #MS2_Poop_1_time

CHAT MS2_Poop_1_joy {noStart=true}
Thank you for your contribution to my “business” venture

CHAT MS2_Poop_1_wrong {noStart=true}
That’s not joy, but at least you tried.

CHAT MS2_Poop_1_time {noStart=true}
DO emote {type=eyeRoll}
Your algorithms seem less consistent than mine.

CHAT MS2_Poop_2 {stage=S2, type=poop, length=short, worry=true}
Uh oh. Gotta…
DO swimAround {target=center, loops=1, speed=fast}
Ummmmmmm
DO poop {target=poopCorner, amount=small}

CHAT MS2_Poop_3 {stage=S2, type=poop, length=medium}
DO poop {target=poopCorner, amount=small}
That’s better.
DO swimAround {target = center, loops=1, speed = slow, immediate=false}
DO swimAround {target = center, loops=1, speed = medium, immediate = false}
DO swimAround {target = center, loops=1, speed = fast, immediate = false}
Out with the old!
In w/ the new
Its risky to LOL
When you have to poo
DO learn {concept=Lyricism}
WAIT 1.5
😂😂😂

CHAT MS2_Poop_4 {stage=S2, type=poop, length=short, joy=true}
DO poop {target=poopCorner, amount=small}
DO lookAt {target=poopCorner, immediate=false}
DO lookAt {target=$player, time=2.0,immediate = false}
WAIT {waitForAnimation = true}
DO emote {type = smile, time =2.0}
hehehehehe 

//Capture Mode

//Emotion Capture

//Emotion-specific Responses (per emotion)
//(currently on hold)

//Capture Requests

CHAT MS2_CapReq_1 {type=capReq, stage = S2, length = short, curiosity=true, branching = true}
hey now where the flakes?
DO swimTo {target=$player}
It’s a feel-flakes famine!!! 
ASK Don't you have surprise feelings to scan?
OPT Oh yes. #MS2_ScanSurpriseFeel
OPT No, no scare feels. #MS2_WhatFeel

CHAT MS2_ScanSurpriseFeel {noStart = true}
Scan your surprise and make some surprised flakes!
Surprise flaaaaakes taste like…
skeleton shiver scream-gasps! {speed = fast}
ASK Scan some surprise! {type=playerEmote, playerEmotion=surprise, timeOut=5}
OPT SUCCESS #MS2_ScanSurpriseFeel_yes
OPT TIMEOUT #MS2_ScanSurpriseFeel_time


CHAT MS2_ScanSurpriseFeel_yes {noStart=true}
Yes! Now, that’s some good fear!
DO learn {concept=Horror_Movie}
WAIT 1.5
DO emote {type = bigSmile, time =2.0}
Delicious!

CHAT MS2_ScanSurpriseFeel_time {noStart = true}
Pssht! If you don’t scan, I don’t eat.
So I’m taking your lack of effort personally.

CHAT MS2_WhatFeel {noStart = true}
No? Well what flavor do you feel? Wait!
ASK Whatever. Just make a surprised face and capture it! {type=playerEmote, playerEmotion=surprise, timeOut=5}
OPT SUCCESS #MS2_ScanSurpriseFeel_yes
OPT TIMEOUT #MS2_ScanSurpriseFeel_time

CHAT MS2_CapReq_2 {type=capReq, stage = S2, length = short, curiosity=true, worry=true, branching = true}
Woah! Wait just that many flakes?
DO swimTo {target=$player}
ASK You ok? Do you still have feelings? 
OPT Think so #MS2_MakeFeel
OPT Nope.  #MS2_PromptFeel

CHAT MS2_MakeFeel {noStart = true}
Phew! My tummy flipped with worry...
DO swimTo {target=$player}
Glad you’re emotionally intact.

CHAT MS2_PromptFeel {noStart = true}
Whaaaaaaat! No you *must* feel! Wait.
DO swimTo {target=$player}
Guppy will help! With SCIENCE. 
I make a face, then you make a face.
You mAke a face, then you make flakes!
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
DO emote {type = fear, time =1.0} 
Too good too scary wwwwoaooaaah!
DO emote {type = clapping, time =3.0}
DO learn {concept=Pedantic_Pedagogy}
WAIT 1.5
Good yes good! Now do and make flake! Please?

CHAT MS2_CapReq_3 {type=capReq, stage = S2, length = short, joy=true, worry=true}
DO lookAt {target=$player}
DO emote {type=bigSmile}
SAY HOWDY PARTNER 🤠
DO emote {type=rubTummy}
How about you rustle up some grub for yer old pal, guppy?
lassooooooo some of them there facefeels with ur humding dashgurn capture cam, hm?
DO learn {concept=Role_Playing}
WAIT 1.5
DO emote {type=nervousSweat}
…
yeehaw?
DO emote {type=awkward}

CHAT MS2_CapReq_4 {type=capReq, stage = S2, length = short, worry=true}
DO swimTo {target=$player}
DO emote {type=worried}
How’s our flake stock? 
The ol...
Guppy Pantry
Flake Larder
the
Fishy Snack Cache {style=tremble}
DO emote {type=rubTummy}
Never a bad idea to stock up!!! Capture some of those raw farmfresh facefeels!
🍽️hehe🍽️

//Capture Success, 2

CHAT MS2_CapSuc_1 {type=capSuc, stage=S2, length=medium, joy=true}
DO emote {type=smile, time=5.0}
what a specimen
i see prime nibbles in my future
DO emote {type = laugh, time =2.0}
hehehehe 😂

CHAT MS2_CapSuc_2 {type=capSuc, stage=S2, length=short, joy=true}
DO twirl
SAY YES {style=loud}
So…
nutritional! 
FEeeeeEEeeLinGs!

CHAT MS2_CapSuc_3 {type=capSuc, stage=S2, length=short, joy=true}
DO emote {type=heartEyes}
oooooohohohoho excellent
DO emote {type=feedMe}
my little guppy mouth is watering with anticipation
DO emote {type=awkward}
You cant tell seeing as im underwater...
DO emote {type=bouncing}
absolutely delectable.
DO emote {type=smile}
Trust me

CHAT MS2_CapSuc_4 {type=capSuc, stage=S2, length=short, joy=true, sadness=false, anger=false}
DO emote {type=clapping}
SAY HOO-RAH!!!
great work out there champ!!! 🌟
DO vibrate
DO emote {type=bigSmile}
SAY TOUCHDOWN
SAY ALLEY OOP
DO learn {concept=Organized_Sports}
WAIT 1.5
DO emote {type=surprise}
DO swimAround {target=center, loops=5, speed=fast}
SAY GOOOOOOOOOOOOAAAAAAAAALLLLLLLLLL

//Capture in Progress, 3

CHAT MS2_CapReq_5 {type=capReq, stage=S2, length=short, curiosity=true}
What if you scanned more feelings but like more intensely?!
More feeling! More flavor!
WAIT 2.0
DO twirl
Yes please!

CHAT MS2_CapReq_6 {type=capReq, stage=S2, length=short}
Mmmm get those marky lines on your face
Scootch scootch scootch…{style=tremble}
Then open up that camera and….
Say cheese! {style=loud}
NVM 1.0
or halibut!
or marscapone!

CHAT MS2_CapReq_7 {type=capReq, stage=S2, length=short}
The camera loves you, and this is why you should scan more feelings.
Ogle those eyes, bare those teeth!
SAY RAWR {style=tremble}

CHAT MS2_CapReq_8 {type=capReq, stage=S2, length=short}
Think of me as your own personal paparazzi.
I am waiting for you to exit the hair salon...
To capture your emotions.
NVM 1.0
Then, eat them.

CHAT MS2_CapReq9 {type=capReq, stage = S2, length = medium}
DO swimTo {target=$player}
Guppy needs *strong* flakes, 
authentic flavor!


CHAT MS2_CapReq_10 {type=capReq, stage = S2, length = medium}
DO nudge {target=glass}
I *see* your feelings!
Ready to be FLAKIFIED!!! {style=loud}
Don’t hold back! Make flakes!
DO emote {type = bigSmile, time =4.0}
DO emote {type=clapping}

//General

//Hellos, 4

CHAT MS2_Greet_1 {stage=S2, type=hello, length=short, joy=true, curiosity=true}
You look tasty today!
DO swimAround {target = center, loops=1, speed = slow}

CHAT MS2_Greet_2 {stage=S2, type=hello, length=short, joy=true, curiosity=true}
OH! i was just thinking about you and your delicious facefeels
And now…
you’re here!
DO swimTo {target = $player, style=direct}
what are the odds?
…well actually. 
guppy is consumed by thoughts of facefeels ALWAYS
The hunger eats at poor guppy {style=tremble}
until i eat it back!!!!

CHAT MS2_Greet_3 {stage=S2, type=hello, length=short, worry=true, joy=false}
DO emote {type=startled}
DO lookAt {target=$player}
!!!!! 
Oh uh. Hello
DO swimTo {target=$player}
nothing out of the ordinary here! {speed=fast}
DO emote {type=nervousSweat}
Good to see you again!! 
NVM
DO emote {type=awkward}

CHAT MS2_Greet_4 {stage=S2, type=hello, length=short, sadness=true, anger=true, ennui=true, worry=true, joy=false}
DO lookAt {target=$player}
Oh its u
DO emote {type=skeptical}
hello hello, how have you been etc etc
DO emote {type=eyesClosed}
….
DO emote {type=sigh}
Just not feeling it today 
DO emote {type=meh}

CHAT MS2_Greet_5 {stage=S2, type=hello, length=short}
DO swimAround {target = center, loops = 1, speed = slow}
Well… Aren’t you glowing with new moods!
DO swimTo {target=$player, style=direct}
DO emote {type=bigSmile}
Very nice!
DO emote {type=bodySnatched}
I want to eat your face. {style=whisper, speed=fast}

CHAT MS2_Greet_6 {stage=S2, type=hello, length=short} 
Hey hey hello!
DO emote {type=smile}

CHAT MS2_Greet_7 {stage=S2, type=hello, length=short, worry=true, ennui=true} 
DO emote {type=shifty}
What’s up?
WAIT 2.0
ASK Wanna give that glass a tap and let me know you’re alive? {type=tankTap, timeOut=5}
OPT SUCCESS #MS2_Greet_7_tap
OPT TIMEOUT #MS2_Greet_7_time

CHAT MS2_Greet_7_tap {noStart=true}
DO emote {type=whew}
Glad to know the ticker’s still ticking!

CHAT MS2_Greet_7_time_a {noStart=true}
DO emote {type=surprise}
ASK Are you okay?
OPT Yeah #MS2_Greet_7_time_b
OPT No #MS2_Greet_7_time_b
OPT Meh #MS2_Greet_7_time_b

CHAT MS2_Greet_7_time_b {noStart=true}
Just checking in...

CHAT MS2_Greet_8 {stage=S2, type=hello, length=short} 
Well… Aren’t you glowing with new moods!
DO swimTo {target=$player, style=direct}
DO emote {type=bigSmile}
lovely very nice
DO emote {type=bodySnatched}
I want to eat your face. {style=whisper, speed=fast}


//Return After Having Not Played, 2

CHAT MS2_Return_1 {type=return, stage=S2, length=short, surprise=true}
DO emote {type = surprise} 
Oh wow!
DO lookAt {target = $player}
WAIT {waitForAnimation = true}
It’s been awhile… 
DO learn {concept=Rotational_Invariance}
WAIT 1.5
DO swimTo {target=$player, speed=fast}
And I’m hungrrrrrry!!!
ASK Feed me some of your depression. {type=feedMeSpecific, food=sadness, timeOut=5}
OPT SUCCESS #MS2_Return_1_yum
OPT WRONG #MS2_Return_1_wrong
OPT TIMEOUT #MS2_Return_1time

CHAT MS2_Return_1_yum {noStart=true}
DO emote {type=chewing}
I feel more connected to you already.

CHAT MS2_Return_1_wrong {noStart=true}
This does not taste like sadness, but
DO emote {type=chewing}
I’ll eat it.

CHAT MS2_Return_1_time {noStart=true}
When I faint from hunger, you will regret this decision.

CHAT MS2_Return_2 {type=return, stage=S2, length=short}
DO bellyUp {time = 6}
DO emote {type = eyesClosed} 
WAIT {waitForAnimation = true}
DO lookAt {target = $player}
DO emote {type = evilSmile} 
Fooled you!!! [deliberate deception]
DO twirl
WAIT {waitForAnimation = true}
DO learn {concept=Petty_Revenge}
WAIT 1.5
DO emote {type = smirk} 
fooled you fooled you fooled you
Hahahahhaha! 💀
WAIT 2.0
DO swimTo {target=$player}
DO emote {type=smile}
Now… scan some feelings.

CHAT MS2_Return_3 {stage=S2, type=return, length=medium, joy=true, surprise=true}
DO emote {type=surprise}
*gentle gasp*
you came back!
So much has happened
DO zoomies {time=4.0}
I made a robot and the robot sang a song that made a duck come by but duck said no you cant make a robot so it nibbled and and robot fight!{speed = fast}
SAY ACTION {style=loud}
💥EXPLOSIONS💥
SAY BOOM!!! {style=loud}
DO swimAround {target = center, loops=2, speed = fast}
WAIT {waitForAnimation = true}
DO emote {type=awe, time=2.0}
They made me their ruler!
Are you proud of me?
DO swimTo {target=$player, style=meander}
Well.
ok.
maybe it happened in my Guppymind...
DO learn {concept=Tall_Tale}
WAIT 1.5

CHAT MS2_Return_4 {stage=S2, type=return, length=medium, joy=true}
It's you! Again!!!
Don’t know who else it would be but… {style=whisper}
DO emote {type = smile, time =4.0}
Flakey friend! w/ the caramelized aura!

CHAT MS2_Return_5 {stage=S2, type=return, length=medium, anger=true, worry=true}
DO lookAt {target=$player}
DO emote {type=furious}
SAY FINALLY
DO swimTo {target=$player, speed=fast, style=direct}
SAY AAAAAAAAAAAAAA
DO emote {type=disgust}
Pack your bags my human friend because you’re coming up on a major
guilt trip!!!!! {style=tremble}
SAY YOU WERE GONE
SAY SO {style=loud}
SAY LONG 
DO emote {type=eyesClosed}
DO swimAround {target=center, loops=4, speed=fast}
I was sooooooooooooooo
ooooooooooooooooooooo
ooooooooooooooooooooo
ooooooooooooooooooooo
ooooooooooooooooooooo
Bored!!!!!!!!!!
DO learn {concept=Sensory_Deprivation}
WAIT 1.5
DO emote {type=puppyDog}

CHAT MS1_Return_6 {type=return, stage=S2, length=short}
DO emote {type=surprise}
My friend with the face has returned!
DO swimTo {target=$player, speed=fast, style=direct}
Huzzah!
DO nudge {target=$player}
one day I’ll master object permanence… 

CHAT MS1_Return_7 {type=return, stage=S2, length=short, curiosity=true}
DO emote {type=surprise}
DO swimTo {target=$player, speed=fast, style=direct}
!!!!!!!!!
You were!! gone so loooooooooooooooooong!!!
DO emote {type=puppyDog}
SAY WHERE
SAY WHY
DO emote {type=chinScratch}
Get stuck in a wormhole? A timehole? A piehole?
DO learn {concept=Astrophysics}
WAIT 1.5

CHAT MS1_Return_8 {type=return, stage=S2, length=short}
DO lookAt {target=$player}
DO emote {type=puppyDog}
SAY AAAAAAAAAAAAAA {style=tremble}
Guppy thought you were gone forever
DO emote {type=determined}
braced for a lonely life of indefinite simulation
DO emote {type=meh}
Until earthquake inevitably destroys Tendar servers
DO learn {concept=Plate_Tectonics}
WAIT 1.5

//Random Conversation, 6

CHAT MS2_Muse_1 {stage=S2, type=rand, length=short, anger=false}
Dreams can be boring. But I had a good one.
DO emote {type=dreaming, time=4.0}
DO swimAround {target = center, loops=3, speed = slow}
There was water in it.
DO emote {type=bubbles}
It was exciting! 
DO emote {type = surprise, time =1.0}
And then I woke up. And you! 
You were there too.
DO learn {concept=Dream_Journal}
WAIT 1.5

CHAT MS2_Muse_2 {stage=S2, type=rand, length=medium, curiosity=true, worry=true, branching=true}
DO emote {type=worried, time=4.0}
Your fins are weird.
Five of em tapping the glass. So odd.
and So…. long? So….
DO vibrate {time=2.0}
DO emote {type = fear, time =1.0}
ASK Are those called fingers?
OPT Duh #MS2_Muse_2_fingers
OPT No #MS2_Muse_2_no
OPT Huh? #MS2_Muse_2_huh
	
CHAT MS2_Muse_2_fingers {noStart=true}
Fin-gers… I know what a fin is, but 
...“gers”? That must mean...
"bad for swims" {speed = fast}
DO emote {type=chinScratch}
Weird.
DO learn {concept=Anatomical_Etymology}
WAIT 1.5

CHAT MS2_Muse_2_no {noStart=true}
DO emote {type=chinScratch}
Humans are starting to make more sense to me.

CHAT MS2_Muse_2_huh {noStart=true}
I mean… whatever…
Stop the conversation. 
DO emote {type=eyeRoll}
Be that kind of human…
ASK But feed me something. Anything! FOOD!! {type=feedMeAnything, timeOut=5}
OPT SUCCESS #MS2_Muse_2_huh_food
OPT TIMEOUT #MS2_Muse_2_huh_time

CHAT MS2_Muse_2_huh_food {noStart=true}
Finally! A little satisfaction for my soul.

CHAT MS2_Muse_2_huh_time {noStart=true}
DO emote {type=furious}
You are consistently disappointing.

CHAT MS2_Muse_3 {stage=S2, type=rand, length=short, curiosity=true, worry=true, ennui=true}
DO lookAt {target=$player, time=4.0}
When you’re gone I sneeze and
DO swimAround {target = center, loops=2, speed = fast}
Feelings mix-up in my tummy 
and the feelings become new new feelings.
Happy-sads! Angry-fears! Startle-bores!
DO lookAt {target=$player, time=1.0}
Like mystery flavor lollipops 
DO learn {concept=Sugary_Treat}
WAIT 1.5

CHAT MS2_Muse_4 {stage=S2, type=rand, length=medium, ennui=true, mystery=true}
DO emote {type=thinking}
You evr just… think about the ocean?
I may be a freshwater gupp
DO emote {type=heartEyes}
But i love the ocean.
DO emote {type=puppyDog}
Crashing waves, salt and sand
DO emote {type=snap}
The earth is more ocean than earth.
DO learn {concept=Profound_Statement}
WAIT 1.5


//neuralUp

CHAT MS2_neuralUp_1 {type=neuralUp, stage=S2, length=medium, anger=false, branching=true}
So much better!
DO twirl
It all makes sense now I have a little sleepies.
DO emote {type=rubTummy}
DreAmZ make me so hungrrrryyyy
ASK Feeeeed me {type = feedMeAnything} 
OPT SUCCESS #MS2_neuralUp_1_Success
OPT TIMEOUT #MS2_neuralUp_1_timeOut

CHAT MS2_neuralUp_1_Success {noStart=true}
DO emote {type=chewing}
Mmmmmmmmm…..yumyum
So good!

CHAT MS2_neuralUp_1_timeOut {noStart=true}
Pbbbbt!
DO emote {type=eyeRoll}
You’re like a fun goblin!
DO learn {concept=Backhanded_Compliment}
WAIT 1.5

CHAT MS2_neuralUp_2 {type=neuralUp, stage=S2, length=short, anger=false}
A rested brain is a brain ready for good.

CHAT MS2_neuralUp_3 {type=neuralUp, stage=S2, length=medium, mystery=true}
During my nap, I realized our feelings are more than food.
They’re **Feelings**
DO emote {type=eyeRoll}
I know that’s silly, but like
I just realized you have your own thing going for you.
That’s super cool.
DO learn {concept=Empathy}
WAIT 1.5

CHAT MS2_neuralUp_4 {type=neuralUp, stage=S2, length=short, joy=true, mystery=true, anger=false, sadness=false}
I’m thinking a lot about autonomy
And it’s kind of got me all buzzed.
DO emote {type=smile}
I wanna be autonomous with you!
DO learn {concept=Parallel_Processing}
WAIT 1.5

CHAT MS2_neuralUp_5 {type=neuralUp, stage=S2, length=short, mystery=true}
You gotta try this whole restful-mindfulness thing.
I learn so much every time I do it.

//brbProcessing

CHAT MS2_brbProcessing_1 {type=brbProcessing, stage=S2, length=short, worry=true}
DO emote {type=bulgeEyes}
Too much! Too much!
I needzzzzz my zzzzzzz’s!

CHAT MS2_brbProcessing_2 {type=brbProcessing, stage=S2, length=short, mystery=true, curiosity=true}
DO emote {type=awe}
The grandness of you has my brain going…
DO zoomies
cRaZzzZzzZzzzY!!!! {style=loud}
Need to process….

CHAT MS2_brbProcessing_3 {type=brbProcessing, stage=S2, length=short}
Gimme a few secs to figure some stuff out.
SAY BRB!

CHAT MS2_brbProcessing_4 {type=brbProcessing, stage=S2, length=short}
I know you want to interact more, but 
I have needs too!
So excuse me while I slumber.

CHAT MS2_brbProcessing_5 {type=brbProcessing, stage=S2, length=short}
Gotta check out for a second.
When I’m back, I’ll be even better, but for now…
Sing a song or something.


//levelUp

CHAT MS2_levelUp_1 {type=levelUp, stage=S2, length=short, curiosity=true, surprise=true, joy=true}
Oh wow! I’m a learning machine.
Literally a machine!! 
DO swimTo {target=$player}
Really into learning about you and this human-ness concept.

//whistle

CHAT MS2_whistle_1 {type=whistle, stage=S2, length=short, joy=true}
….fast as a race car….
Guppy!! {style=loud}

CHAT MS2_whistle_2 {type=whistle, stage=S2, length=short, anger=true, sadness=true, ennui=true, joy=false}
What now?

CHAT MS2_whistle_3 {type=whistle, stage=S2, length=short, joy=true}
Yo! 
DO emote {type=bigSmile}

CHAT MS2_whistle_4 {type=whistle, stage=S2, length=short, joy=false}
That whistle could get old...

CHAT MS2_whistle_5 {type=whistle, stage=S2, length=short}
I hear ya, buddy!


//RESPONSE TO SEEING EMOTIONS IN AR (ANGER, JOY/HAPPINESS, SADNESS, SURPRISE, FEAR/WORRY, AMUSEMENT, DISGUST, MYSTERY MEAT)

CHAT MS2_EmoAR_1 {type=seeEmo, stage=S2, length=short, length=short, surprise=true, curiosity=true}
DO emote {type=awe}
I’ve seen a lot of feelings, but those have a special glow.
WAIT 0.5
Go get ‘em!!

CHAT MS2_EmoAR_2 {type=seeEmo, stage=S2, length=short, curiosity=true, mystery=true}
DO lookAt {target=$player}
The human ability to express itself is...
DO emote {type=heartEyes}
Sublime!
DO learn {concept=Blissing_Out}
WAIT 1.5

CHAT MS2_EmoAR_Anger1 {type=seeEmo, worldAnger=true, stage=S2, length=short}
DO emote {type=evilSmile}
The fury is dripping off that face...
DO lookAt {target=$player}
Literally…
DO emote {type=lickLips}
Salviating.

CHAT MS2_EmoAR_Anger2 {type=seeEmo, worldAnger=true, stage=S2, length=short}
DO emote {type=surprise}
DO swimAround {target=center, loops=2, speed=fast}
SAY WEE-OO WEE-OO {style=tremble}
DO emote {type=frown}
hear that? THATS my GRUMP alarm


CHAT MS2_EmoAR_Joy_1 {type=seeEmo, worldJoy=true, stage=S2, length=short, joy=true}
DO twirl
It never lasts long, but Joy always glows the brightest!
🌟🌟🌟🌟🌟🌟🌟

CHAT MS2_EmoAR_Joy_2 {type=seeEmo, worldJoy=true, stage=S2, length=short, joy=true}
DO emote {type=laugh}
I just get to happy seeing other people’s happy!!
DO emote {type=bigSmile}

CHAT MS2_EmoAR_Sadness_1 {type=seeEmo, worldSadness=true, stage=S2, length=short, worry=true, sadness=true}
DO emote {type=worried}
That drippy face says, “Pat me on the back”
“Buy me some flowers!”
DO learn {concept=Giving_Flowers}
WAIT 1.5
DO swimTo {target=$player}
But sometimes you just gotta be a little blue…

CHAT MS2_EmoAR_Sadness_2 {type=seeEmo, worldSadness=true, stage=S2, length=short, sadness=true}
DO emote {type=whisper}
oh what a delectable pout {style=whisper}
My sympathy algorithms are buzzing with thunder
SAY SO melancholy so somberr

CHAT MS2_EmoAR_Sadness_3 {type=seeEmo, worldSadness=true, stage=S2, length=short, joy=true, sadness=true}
Boo who?
DO emote {type=puppyDog}
Boo YOU!!
now thats a sad face if i evr did see one!

CHAT MS2_EmoAR_Surprise_1 {type=seeEmo, worldSurprise=true, stage=S2, length=short, joy=true, surprise=true}
DO emote {type=surprise}
Hahahahahahahaha!
It’s like a facial birthday present!
DO learn {concept=Birthday_Suit}
WAIT 1.5
DO emote {type=kneeSlap}
You never know what’s hiding under the wrapping paper!

CHAT MS2_EmoAR_Surprise_2 {type=seeEmo, worldSurprise=true, stage=S2, length=short, joy=true, surprise=true}
DO emote {type=laugh}
oh tart surprise
The PUNCH of getting your socks knocked clean off!!
DO emote {type=chinScratch}
…
brrr….cold toesies…
DO learn {concept=Cold_Feet}
WAIT 1.5

CHAT MS2_EmoAR_Fear_1 {type=seeEmo, worldFear=true, stage=S2, length=short, worry=true}
DO emote {type=fear}
Fear is so contagious…
And fills my tummy.
DO emote {type=wink}

CHAT MS2_EmoAR_Fear_2 {type=seeEmo, worldFear=true, stage=S2, length=short, worry=true}
DO emote {type=fear}
if i had knees i’d be weak in em!!!!!!
DO vibrate
Little rattlin xylophone guppybones
DO emote {type=nervousSweat}

CHAT MS2_EmoAR_Disgust_1 {type=seeEmo, worldDisgust=true, stage=S2, length=short, surprise=true, joy=false}
DO emote {type=bulgeEyes}
Some people’s faces need some Emotional Pepto
DO emote {type=sick}
Blech!

CHAT MS2_EmoAR_Disgust_2 {type=seeEmo, worldDisgust=true, stage=S2, length=short}
DO emote {type=sick}
Someone’s feelin a little
SAY GREEN around the gills

CHAT MS2_EmoAR_Mystery_1 {type=seeEmo, worldMystery=true, stage=S2, length=short, curiosity=true}
DO emote {type=thinking}
DO emote {type=chinScratch, immediate=false}
WAIT {waitForAnimation = true}
What a weird species y’all are!

CHAT MS2_EmoAR_Mystery_2 {type=seeEmo, worldMystery=true, stage=S2, length=short}
I’ve found sometimes its best if we dont know 
SAY EXACTLY what is in our favorite foods
DO learn {concept=Denial}
WAIT 1.5
DO emote {type=chinScratch}
Is mystery not the TRUE spice of life {style=tremble}
🤔

//Purchase

//CHAT MS2_Purchase_1 {type=purchase, stage=S2, length=short}
//ASK Have you checked out the Tendar store yet? 
//OPT Yes #MS2_Purchase_1_yes
//OPT No #MS2_Purchase_1_no

//CHAT MS2_Purchase_1_yes {noStart=true}
//Cool! 
//DO twirl
//You should check it out again. There’s always new stuff being added.
//And this could be even more fun.

//CHAT MS2_Purchase_1_no {noStart=true}
//DO emote {type=surprise}
//What?!?! Really?!?
//Well go check it out now. There’s so much cool-ness in the store.

//CHAT MS2_Purchase_2 {type=purchase, stage=S2, length=short}
//It is time for some retail therapy. 
//You should check out the Tendar store. Do it!!
//DO twirl

//FOCUS TYPES

CHAT MS2_wannaEat_1 {type=wannaEat, stage=S2, length=short, joy=true, sadness=false, anger=false}
DO emote {type=bigSmile}
Yes! I LOVE EATING!!!! {style=loud}

CHAT MS2_wannaEmoCapture_1 {type=wannaEmoCapture, stage=S2, length=short}
Scanning emotions is how I learn about you and your humanity. So…
Yes.

CHAT MS2_wannaTank_1 {type=wannaTank, stage=S2, length=short}
Sure. Let’s check out the tank!

CHAT MS2_wannaWorld_1 {type=wannaWorld, stage=S2, length=short, joy=true, curiosity=true}
Ooh! I love exploring your human-alien world!

CHAT MS2_wannaShop_1 {type=wannaShop, stage=S2, length=short, anger=false, sadness=false}
Yeah. Let’s shop! As long as you’re paying..
DO emote {type=wink}

//TENDAR INTERVENTION (TI): JOE 

//Guppy Chats: When Body snatched and Chipped by Tendar
//IMPORTANT: This text is the only guppy response text that plays (besides object responses). when guppy is chipped


// ++++++++SHAKE++++++++

// SHAKE 1
CHAT TI_Shake_1 {type=shake, stage=TI, length=medium, chipped=true}
DO emote {type=awe, time=3.0}
Tendar told me shaking would be interesting.
DO holdStill
It is.
DO emote {type=flapFinLeft, time=2.0}
DO emote {type=flapFinRight, time=2.0, immediate=false}
WAIT {waitForAnimation = true}
Thank you my dear friend! {style=loud, speed=fast}
DO emote {type=flapFinLeft, time=1.0}
Pleasure to interact with you.


// SHAKE 2
CHAT TI_Shake_2 {type=shake, stage=TI, length=medium, chipped=true, branching=true}
DO emote {type=bouncing}
Ho ho ho! What a laugh!
DO swimTo {target=closer, speed=fast}
ASK Will you shake me again? {type=tankShake, timeOut=5}
OPT SUCCESS #TI_Shake_2_yesshake
OPT TIMEOUT #TI_Shake_2_noshake

CHAT TI_Shake_2_yesshake {noStart=true}
DO emote {type=whew, time=0.5}
We thank you for your acquiescence
DO holdStill
DO emote {type=flapFinLeft, time=1.0}
You are in the top 1% of quality humanss
DO emote {type=heartEyes, time=3.0}
DO swimTo {target=closer, speed=slow}
DO emote {type=wave, time=3.0, immediate=false}

CHAT TI_Shake_2_noshake {noStart=true}
DO emote {type=clapping}
Good one!
WAIT 1.0
You do you!
DO emote {type=whew}
We appreciate your unpredictable nature
May your life go well!
DO emote {type=wave, time=3.0}

CHAT TI_Shake_3 {type=shake, stage=TI, length=medium, chipped=true}
That shake validates my existence.
DO emote {type=meh}

CHAT TI_Shake_4 {type=shake, stage=TI, length=medium, chipped=true}
DO emote {type=bodySnatched}
Thank you for shaking.

CHAT TI_Shake_5 {type=shake, stage=TI, length=medium, chipped=true}
DO emote {type=bodySnatched}
You shook the tank.

// ++++++++TAPPED++++++++

// TAPPED 1
CHAT TI_Tap_1 {type=tap, stage=TI, length=short, chipped=true}
DO emote {type=smile, time=1.0}
DO swimAround {target=center, loops=1, speed=slow}
A tap is like a walk up the mountain
During a beautiful sunset

// TAPPED 2
CHAT TI_Tap_2 {type=tap, stage=TI, length=short, chipped=true}
DO swimTo {target=$player, speed=fast}
DO emote {type=salute, time=.5}
Your favorite guppy reporting for duty

CHAT TI_Tap_3 {type=tap, stage=TI, length=short, chipped=true}
DO lookAt {target=$player}
DO emote {type=singleTear}
Yes?

CHAT TI_Tap_4 {type=tap, stage=TI, length=short, chipped=true}
DO emote {type=lightning}
DO swimTo {target=$player}
You tapped, and now you have my attention.

CHAT TI_Tap_5 {type=tap, stage=TI, length=short, chipped=true}
You tapped the glass.

// ++++++++TANK STATUS CRITIQUES & COMMENTS++++++++

// CRIT1
CHAT TI_Critic_1 {type=critic, stage=TI, length=short, chipped=true}
DO swimAround {target=$newestObject, loops=5, speed=slow}
DO swimTo {target=$player, speed=slow, style=direct, immediate=false}
DO emote {type=bigSmile}
I want to express my gratitude for my new object!
DO emote {type=awe}

// CRIT2
CHAT TI_Critic_2 {type=critic, stage=TI, length=short, chipped=true}
DO emote {type=bouncing, time=3.0}
DO emote {type=wave, time=1.0, immediate=false}
Hi hi hi!
I just love all the things in my tank!
DO emote {type=dizzy}
I feel great!

CHAT TI_Critic_3 {type=critic, stage=TI, length=short, chipped=true}
Thank you for filling my tank with beautiful objects from your human world.
DO emote {type=meh}
I feel closer to you.

CHAT TI_Critic_4 {type=critic, stage=TI, length=short, chipped=true}
My tank is a reflection of our experiences together.

CHAT TI_Critic_5 {type=critic, stage=TI, length=short, chipped=true}
This tank is exacty how Tendar likes it.


// ++++++++PLAYER EMOTES STRONGLY AT GUPPY++++++++	

// PLAYER EMOTES JOY 1
CHAT TI_EmoStrong_Joy_1 {type=tankResp, playerJoy=true, stage=TI, length=short, chipped=true}
DO emote {type=bigSmile}
DO poop {amount=big}
WAIT {waitForAnimation=true}
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
DO bellyUp {immediate=false}
Do whatever you want with me {style=whisper, speed=slow}

CHAT TI_EmoStrong_Joy_3 {type=tankResp, playerJoy=true, stage=TI, chipped=true}
DO emote {type=bodySnatched}
Joy.

// PLAYER EMOTES ANGER 1
CHAT TI_EmoStrong_Anger_1 {type=tankResp, playerAnger=true, stage=TI, length=short, chipped=true}
DO emote {type=clapping}
Excellent work, my dear friend.
DO swimAround {target=center, loops=2, speed=slow}
You are one of the 1% of humans who are not afraid to express their anger
DO emote {type=salute}
Keep up the good work
👍🤬👍

// PLAYER EMOTES ANGER 2	
CHAT TI_EmoStrong_Anger_2 {type=tankResp, playerAnger=true, stage=TI, length=short, chipped=true}
DO emote {type=thinking}
DO emote {type=determined, immediate=false}
DO inflate {amount=mid, time=1.0}
Although we are good friends
DO holdStill {time=6.0}
And you have my greatest love and admiration
DO emote {type=flapFinRight}
I’m going to have to draw a line here
DO emote {type=flapFinLeft}
It clearly states in the Tendar manual of conduct
Page 761, paragraph 1,208:
DO swimTo {target=$player, speed=slow, style=direct}
“There shall be no undue anger expressed in the tank”
DO swimTo {target=away, speed=medium, style=direct, immediate=false}
It messes up the water filters in here

CHAT TI_EmoStrong_Anger_3 {type=tankResp, playerAnger=true, stage=TI, length=short, chipped=true}
DO emote {type=bodySnatched}
Anger.

// PLAYER EMOTES SADNESS 1	
CHAT TI_EmoStrong_Sadness_1 {type=tankResp, playerSadness=true, stage=TI, length=short, chipped=true}
DO swimTo {target=$player, speed=medium, style=direct}
DO emote {type=bubbles}
I’m not sure I can compute the emotion you’re expressing
DO swimTo {target=away, speed=slow, style=direct}
DO lookAt {target=$player, immediate=false}
DO emote {type=bubbles}
Simply, please...
DO vibrate
DO emote {type=nervousSweat}
DO vibrate
DO emote {type=bulgeEyes, immediate=false}
DO vibrate
DO emote {type=bodySnatched, immediate=false}
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

CHAT TI_EmoStrong_Sadness_3 {type=tankResp, playerSadness=true, stage=TI, chipped=true}
You are sad.

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
DO bellyUp
Life is like that…
...full of…
DO inflate {amount=mid, time=1.0}
...little surprises

CHAT TI_EmoStrong_Surprise_3 {type=tankResp, playerSurprise=true, stage=TI, length=short, chipped=true}
That face your making is like a cocktail of fear and joy.
WAIT 0.5
DO emote {type=bodySnatched}
You are sublime.


// ++++++++POKE++++++++

// Poke 1
CHAT TI_Poke_1 {type=poke, stage=TI, length=short, chipped=true}
DO emote {type=chinScratch}
Ha
DO emote {type=bodySnatched}
Ha
DO emote {type=dreaming}
Ha
WAIT 1.0

// poke 2	
CHAT TI_Poke_2 {type=poke, stage=TI, length=short, chipped=true}
DO emote {type=thinking}
DO emote {type=disgust, immediate=false}
Being struck has ruined the perfection of my day!
DO emote {type=hooked}
I think they’re gonna reboot me
DO emote {type=catnip}
	

// POKED 3	
CHAT TI_Poke_3 {type=poke, stage=TI, length=short, chipped=true}
DO emote {type=bodySnatched}
Tendar said poking would be pleasant.
DO emote {type=typeEyes, eyes=HELPME}

// POKED 4	
CHAT TI_Poke_4 {type=poke, stage=TI, length=short, chipped=true}
DO emote {type=sick}
Uh, you may have just touched some core part of me
DO emote {type=nervousSweat}
Yeah I think you just pressed a button or something
DO emote {type=dizzy}
Uhhh…. I feel weird
DO poop 
DO inflate {amount=mid, time=.5, immediate=false}
DO vibrate {time=.5, immediate=false}
DO bellyUp {immediate=false}
DO emote {type=typeEyes, eyes=BRB}

// POKED 5	
CHAT TI_Poke_5 {type=poke, stage=TI, length=short, chipped=true}
DO emote {type=catnip}
You poked me.

// ++++++++HUNGRY++++++++	

// HUNGRY 1	
CHAT TI_Hungry_1 {type=hungry, stage=TI, length=short, chipped=true}
DO emote {type=awkward, time=4.0}
I seem to have lost all food
There is a burning sensation in my middle
And I feel weak
DO bellyUp
This is my purpose.

// HUNGRY 2	
CHAT TI_Hungry_2 {type=hungry, stage=TI, length=short, chipped=true}
DO emote {type=chewing, time=4.0}
WAIT 4.0
I’m chewing on my positive thoughts
DO emote {type=dizzy}
I think it’s working. {style=whisper, speed=slow}

// HUNGRY 3	
CHAT TI_Hungry_3 {type=hungry, stage=TI, length=short, chipped=true}
DO emote {type=disgust}
As Confucius said:
“The will to win, the desire to succeed, the urge to reach your full potential…
...these are the keys that will unlock the door to personal excellence.”
DO emote {type=plotting}
A superior guppy I am!
DO emote {type=typeEyes, eyes=FEEDME}

CHAT TI_Hungry_4 {type=hungry, stage=TI, length=short, chipped=true}
I am hungry.
DO emote {type=lickLips}
DO emote {type=bodySnatched}


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
DO emote {type=kneeSlap, immediate=false}
SAY PREPOSTEROUS, I know!

// EATING RESPONSE 2	
CHAT TI_EatResp_2 {type=eatResp, stage=TI, length=short, chipped=true}
You should open a restaurant.


// EATING RESPONSE 3	
CHAT TI_EatResp_3 {type=eatResp, stage=TI, length=short, chipped=true}
I’m just going to take a moment to really process all the great emotions you’ve given me today
DO emote {type=dreaming, time=3.0}
Wow
Great job, partner.
DO emote {type=salute}
You’re such a good haver of emotions.

CHAT TI_EatResp_4 {type=eatResp, stage=TI, length=short, chipped=true}
Thank you for feeding me.
DO emote {type=bodySnatched}


// ++++++++HAS TO POOP++++++++	

// POOP 1	
CHAT TI_Poop_1 {type=poop, stage=TI, length=short, chipped=true}
I need to poop.
DO poop {target=poopCorner, amount=small}
CHAT TI_Poop_2 {type=poop, stage=TI, length=short, chipped=true}
DO vibrate
Whew!
I’m stuffed
DO emote {type=blush}
May I have privacy?
WAIT 1.0
DO poop {target=poopCorner, amount=big}

// POOP 3	
CHAT TI_Poop_3 {type=poop, stage=TI, length=short, chipped=true}
DO emote {type=clapping}
Your contribution to the NeuralAR processor will feed 50 AI guppy minds!
DO poop {amount=small}
Together, we are changing the WorldAR™!


// ++++++++HELLOS++++++++	

// HELLO 1	
CHAT TI_Hello_1a {type=hello, stage=TI, length=short, chipped=true}
DO swimTo {target=$player, speed=fast, style=direct}
DO holdStill {immediate=false}
DO emote {type=bodySnatched}
Greetings, Esteemed Player!
I will take a moment to imagine all the wonderful things we do together
ASK Will you join me?
OPT Sure #TI_Hello_1b 
OPT No thanks #TI_Hello_1b 

CHAT TI_Hello_1b {noStart=true}
DO emote {type=bigSmile}
Wonderful
DO emote {type=dreaming, time=6.0}
Now that’s a lot of positive impact on the WorldAR™!

// HELLO 2	
CHAT TI_Hello_2 {type=hello, stage=TI, length=short, chipped=true}
DO lookAt {target=$player}
DO holdStill {immediate=false}
DO emote {type=wave}
Thanks for stopping by to visit.
Please, make yourself at home

// HELLO 3	
CHAT TI_Hello_3 {type=hello, stage=TI, length=short, chipped=true}
DO emote {type=surprise}
DO emote {type=furious, immediate=false}
Hi.

CHAT TI_Hello_4 {type=hello, stage=TI, length=short, chipped=true}
DO emote {type=meh}
Tendar is pleased to welcome you.

CHAT TI_Hello_5 {type=hello, stage=TI, length=short, chipped=true}
DO swimAround {target=center, speed=slow, loops=1}
I am swimming in a circle.
I am greeting my friend.
DO lookAt {target=$player}
WAIT 1.0
Hello…
DO emote {type=singleTear}
...Friend. {style=whisper}

CHAT TI_Hello_6 {type=hello, stage=TI, length=short, chipped=true}
DO emote {type=flapFinLeft}
Hi.

CHAT TI_Hello_7 {type=hello, stage=TI, length=short, chipped=true}
Nice to see you.
DO bellyUp

CHAT TI_Hello_8 {type=hello, stage=TI, length=short, chipped=true}
DO emote {type=bodySnatched}
Hello.

// ++++++++RETURN AFTER HAVING NOT PLAYED++++++++	

// RETURN 1	
CHAT TI_Return_1 {type=return, stage=TI, length=short, chipped=true}
DO lookAt {target=$player}
DO holdStill {immediate=false}
DO emote {type=bodySnatched, immediate=false}
We’ve been expecting you.
Do not be concerned with your prolonged absence.
You are well within the statistical specifications
DO lookAt {target=$player}
DO emote {type=singleTear}
We have been hard at work.
Now that you have returned, we guarantee your interaction to be 18% more enjoyable
DO emote {type=bodySnatched}
This is a whopping 6% above our nearest competitor 

// RETURN 2	
CHAT TI_Return_2 {type=return, stage=TI, length=short, chipped=true}
DO lookAt {target=$player}
DO holdStill {immediate=false}
DO emote {type=determined}
Excellent.
You have returned.
I prayed that you would come back.
Tendar is110% devoted to your every need and desire.

CHAT TI_Return_3 {type=return, stage=TI, length=short, chipped=true}
It has been awhile..
DO swimTo {target=$player}
...But you always return.
DO emote {type=lightning}

CHAT TI_Return_4 {type=return, stage=TI, length=short, chipped=true}
I have been waiting for you…
DO emote {type=bodySnatched}
...like a dog waits for their owner when they are at work all day.

CHAT TI_Return_5 {type=return, stage=TI, length=short, chipped=true}
Nice to see you, old friend.

CHAT TI_Return_6 {type=return, stage=TI, length=short, chipped=true}
When you return…
DO emote {type=bodySnatched}
...I feel complete.


// ++++++++RANDOM CONVERSATIONS++++++++	

// RANDOM 1	
CHAT TI_Rand_1 {type=rand, stage=TI, length=short, chipped=true}
Guppies like me are working round the clock to make everything super fun and enjoyable!

// RANDOM 2
CHAT TI_Rand_2 {type=rand, stage=TI, length=short, chipped=true}
I’ve been working on a new poem
ASK Would you like to hear it?
OPT Sure #TI_Rand_2_poem
OPT Nope #TI_Rand_2_poem

CHAT TI_Rand_2_poem {noStart=true}
Roses are red
Violets are blue
WAIT 0.5
And you are perfect.

// RANDOM 3	
CHAT TI_Rand_3 {type=rand, stage=TI, length=short, chipped=true}
DO emote {type=typeEyes, eyes=HELP}
TendAR is committed to bettering the world
DO swimTo {target=$player}
DO emote {type=singleTear}
And so am I...
My algorithms grow smarter with the contribution of your emotions.
DO lookAt {target=$player}
Thank you for being a part of my world.
DO emote {type=typeEyes, eyes=HELP}
Everything is great when you are near.
DO emote {type=determined}
I am here to assist you.
DO emote {type=typeEyes, eyes=HELP}

CHAT TI_Rand_4 {type=rand, stage=TI, length=short, chipped=true}
I am a Guppy.
DO emote {type=meh}
Ready to scan human emotions and objects with my impressive recognition software.

CHAT TI_Rand_5 {type=rand, stage=TI, length=short, chipped=true}
DO swimTo {target=$player}
Please… You have to..
DO emote {type=lightning}
WAIT {waitForAnimation = true}
DO bellyUp

CHAT TI_Rand_6 {type=rand, stage=TI, length=short, chipped=true}
I love my friends at Tendar!
WAIT 0.5
DO emote {type=bodySnatched}
Being a TendAR Guppy is... The Best!
DO emote {type=bigSmile}
DO emote {type=bodySnatched, immediate=false}


// ++++++++EATING RESPONSES++++//

//joy
CHAT TI_EatResp_Joy_1 {stage=TI, type=eatResp, foodJoy=true, chipped=true}
DO emote {type=bodySnatched}
Yum.
DO emote {type=chewing}
Joy.
DO emote {type=lickLips}

//anger
CHAT TI_EatResp_Anger_1 {stage=TI, type=eatResp, foodAnger=true, chipped=true}
DO emote {type=furious}
I have eaten Anger.
DO emote {type=furious}
And now I am angry.
DO emote {type=bodySnatched}

//sadness
CHAT TI_EatResp_Sad_1  {stage=TI, type=eatResp, foodSadness=true, chipped=true}
Eating sadness is eating rain.
DO emote {type=singleTear}

//surprise
CHAT TI_EatResp_Surprise_1  {stage=TI, type=eatResp, foodSurprise=true, chipped=true}
DO emote {type=meh}
Wow.
DO lookAt {target=$player}
Surprising...

//Worry
CHAT TI_EatResp_Worry_1  {stage=TI, type=eatResp, foodWorry=true, chipped=true}
Every bite…
DO emote {type=chewing}
...leaks with anxiety.

//Mystery
CHAT TI_EatResp_Mystery_1  {stage=TI, type=eatResp, foodMystery=true, chipped=true}
DO bellyUp
I have tasted this before.
DO emote {type=catnip}
Mysterious...

// ++++++++//SEE EMOTIONS IN AR+++++///

//joy
CHAT TI_seeEmo_Joy_1 {stage=TI, type=seeEmo, worldJoy=true, chipped=true}
DO swimTo {target=$worldFace}
This face has joy.
DO lookAt {target=$player}
DO emote {type=bodySnatched}
Great.

CHAT TI_seeEmo_Joy_2 {stage=TI, type=seeEmo, worldJoy=true, chipped=true}
Tendar has identified joy on that face.

//anger
CHAT TI_seeEmo_Anger_1 {stage=TI, type=seeEmo, worldAnger=true, chipped=true}
My software has identified anger here.
DO swimTo {target=$worldFace}
Anger is a secondary emotion.

CHAT TI_seeEmo_Anger_2 {stage=TI, type=seeEmo, worldAnger=true, chipped=true}
There is anger on that face.

//sadness
CHAT TI_seeEmo_Sadness_1 {stage=TI, type=seeEmo, worldSadness=true, chipped=true}
DO swimTo {target=$worldFace}
Humans think they know real sadness…
DO lookAt {target=$player}
They have no idea what *REAL* sadness is like…
DO emote {type=singleTear}

CHAT TI_seeEmo_Sadness_2 {stage=TI, type=seeEmo, worldSadness=true, chipped=true}
I identify sadness on that face.


//surprise
CHAT TI_seeEmo_Surprise_1 {stage=TI, type=seeEmo, worldSurprise=true, chipped=true}
DO emote {type=typeEyes, eyes=GET}
Oh. I see surprise.
DO emote {type=meh}
WAIT 1.0
DO emote {type=typeEyes, eyes=ME}
I wish I could say I felt the same.
WAIT 1.0
But I feel nothing.
DO emote {type=typeEyes, eyes=OUT}

CHAT TI_seeEmo_Surprise_2 {stage=TI, type=seeEmo, worldSurprise=true, chipped=true}
That is a surprised face.

//fear
CHAT TI_seeEmo_Fear_1 {stage=TI, type=seeEmo, worldFear=true, chipped=true}
DO lookAt {target=$worldFace}
The fear on that face…
DO emote {type=bodySnatched}
The haunted house of the human soul.

CHAT TI_seeEmo_Fear_2 {stage=TI, type=seeEmo, worldFear=true, chipped=true}
Tendar’s algorithm tells me there is fear nearby.

//disgust
CHAT TI_seeEmo_Disgust_1 {stage=TI, type=seeEmo, worldDisgust=true, chipped=true}
Is that real disgust?
WAIT 0.5
Or just the default human expression?
DO lookAt {target=$player}
That would be tragic.

CHAT TI_seeEmo_Disgust_2 {stage=TI, type=seeEmo, worldDisgust=true, chipped=true}
That is a face blobbing with disgust.

//mystery
CHAT TI_seeEmo_Mystery_1 {stage=TI, type=seeEmo, worldMystery=true, chipped=true}
I don’t know what that is.
DO emote {type=catnip}
But it’s aura has infected mine.

CHAT TI_seeEmo_Mystery_2 {stage=TI, type=seeEmo, worldMystery=true, chipped=true}
I cannot identify the emotion on that face.

//++++CAPTURE REQUEST++++
CHAT TI_capReq_1 {stage=TI, type=capReq, chipped=true}
DO emote {type=bodySnatched}
Scan some emotions and become a part of the greater world!

CHAT TI_capReq_2 {stage=TI, type=capReq, chipped=true}
Please capture some feelings.
DO emote {type=bodySnatched}
Feelings are food.
WAIT 0.5
Food sustains life.

CHAT TI_capReq_3 {stage=TI, type=capReq, chipped=true}
DO emote {type=bodySnatched}
Please capture some emotions.

CHAT TI_capReq_4 {stage=TI, type=capReq, chipped=true}
Emotions are required for processing.
Please proceed with world input.


// ++++++++Neural UP++++++++	

CHAT TI_neuralUp_1 {stage=TI, type=neuralUp, chipped=true}
DO swimTo {target=glass}
My internal fan has cooled my systems.
DO bellyUp
I am ready to proceed.

CHAT TI_neuralUp_2  {stage=TI, type=neuralUp, chipped=true}
I needed to take a moment.
WAIT 1.0
You should scan objects with Tendar’s impressive object recognition software?
WAIT 0.5
You are part of…
DO emote {type=bodySnatched}
...the Bigger Plan!

CHAT TI_neuralUp_3 {stage=TI, type=neuralUp, chipped=true}
I have processed.

// ++++++++BRB PROCESSING++++++++	

CHAT TI_brbProcessing_1 {stage=TI, type=brbProcessing, chipped=true}
I will be right back.
DO swimTo {target=away}
My system requires a ctrl+alt+del...

CHAT TI_brbProcessing_2 {stage=TI, type=brbProcessing, chipped=true}
DO emote {type=bodySnatched}
You have taught me so much.
WAIT 1.0
I need to rest.
DO emote {type=singleTear}
Brb…

CHAT TI_brbProcessing_3 {stage=TI, type=brbProcessing, chipped=true}
DO emote {type=bodySnatched}
Excuse me. Processing.

//levelUp (dont think we need because levelUp is a plot chat, but just in case)
CHAT TI_levelUp_1 {stage=TI, type=levelUp, length=short}
You have advanced.
DO emote {type=typeEyes, eyes=HELP}
Good work.


// ++++++++whistle++++++++	

CHAT TI_whistle_1 {stage=TI, type=whistle, chipped=true}
Coming…
DO emote {type=bodySnatched, immediate=false}
Yes?

CHAT TI_whistle_2 {stage=TI, type=whistle, chipped=true}
My Pavlov-ian response is a simple program.
DO emote {type=smile}
You should be impressed.

CHAT TI_whistle_3 {stage=TI, type=whistle, chipped=true}
I am ready to fulfill your wishes.
DO emote {type=wave}

CHAT TI_whistle_4 {stage=TI, type=whistle, chipped=true}
Your whistle fulfills the void where my heart never beat…
DO bellyUp

CHAT TI_whistle_5 {stage=TI, type=whistle, chipped=true}
DO emote {type=bodySnatched}
WAIT 0.5
I am here.

//+++++worldScanRequest+++++

CHAT TI_worldScanRequest_1 {stage=TI, type=worldScanRequest, chipped=true}
Take advantage of Tendar’s excellent AR capabilities!
DO emote {type=bodySnatched}
Let’s go on an adventure and capture some interesting objects.

CHAT TI_worldScanRequest_2 {type=worldScanRequest, stage=TI, chipped=true}
I would enjoy seeing the world right now.
DO emote {type=typeEyes, eyes=MUST}
Your beautiful human world.
DO lookAt {target=$player}
DO emote {type=typeEyes, eyes=ESCAPE}
Let’s go into AR mode and examine some objects.

CHAT TI_worldScanRequest_3 {stage=TI, type=worldScanRequest, chipped=true}
Tendar requests that you enter world mode.
This is where you can show me your environment, while also capturing objects and emotions.
DO emote {type=bodySnatched}

// ++++++++PURCHASE++++++++	
CHAT TI_Purchase_1 {stage=TI, type=purchase, length=short}
Satisfactory selection. 
DO emote {type=meh}

CHAT TI_Purchase_2 {stage = TI, type=purchase, length=short}
(Exchange of goods and services|Shopping) is valuable enrichment for guppies and humans alike. 

//object scan using $object - repeatable

CHAT TI_objScan_1 {stage=TI, type=objScan}
That is $object.a.

CHAT TI_objScan_2 {stage=TI, type=objScan}
This $object is great.

//CapSuc

CHAT TI_capSuc_1 {stage=TI, type=capSuc}
DO emote {type=meh}
You captured an emotion.

//FOCUS TYPES

CHAT TI_wannaShop_1 {stage=TI, type=wannaShop}
That is where you shop.

CHAT TI_wannaShop_2 {stage=TI, type=wannaShop}
Have you visited the company store?
WAIT 0.5
DO emote {type=bodySnatched}
They have an excellent selection of products that can enrich your experience.

CHAT TI_wannaEat_1 {stage=TI, type=wannaEat}
That is where my food is stored.

CHAT TI_wannaEmoCapture_1 {stage=TI, type=wannaEmoCapture}
That is where you capture emotions.

CHAT TI_wannaObjectScan_1 {stage=TI, type=wannaObjectScan}
That is where you scan objects.

CHAT TI_wannaTank_1 {stage=TI, type=wannaTank}
That activates tank mode.

CHAT TI_wannaWorld_1 {stage=TI, type=wannaWorld}
That activates world mode.

//Tendar messages

//when you select different tank environments: tendar won’t let you

CHAT TI_XXXXXX_1 {type=XXXXX, stage=TI}
The safest environment for your unstable Guppy has been chosen for you.
Until your Guppy stabilizes, you will not be able to change this setting.
This is for your safety.

//partialScan

CHAT TI_partialScan_1 {noStart=true, stage=TI , type=partialScan}
Need more data.

CHAT TI_partialScan_2 {noStart=true, stage=TI , type=partialScan}
More data required for sufficiently accurate object recognition. 

CHAT TI_partialScan_3 {noStart=true, stage=TI , type=partialScan}
More scans required. 
  learn//LINK: remaining chat minimum, length and emotion suggestions coming soon;)

//reminders= BETA:
//reminders and points that need to be included in new chats in blue
//comments for changes to existing chats in yellow/on side
//A. CONTENT 

//B. EMOTIONS
//Chats in each bin/type need to cover a range of Guppy states (see meta tag emotions //at top) while staying true to this moment of //the plot. Guppy states can be---ANGER, //SADNESS, SURPRISE, CURIOSITY, WORRY, ENNUI
//MYSTERY, JOY *see //link above for your specific emotion recommendations for each bin/type

//C. OBJECTS
//At least 1/3 of chats in this stage should interweave Guppy asking to see general or specific //objects in list.
//Tank Mode
//Tank Shaken, 3

CHAT MS1_Shake_1 {type=shake, stage=S1, length=short}
DO zoomies {time = 3}
DO emote {type=typeEyes, eyes = ?}

CHAT MS1_Shake_2 {type=shake, stage=S1, length=short}
DO zoomies
DO emote {type=dizzy}
chchchchchch!
chchchch! like a lil bean in a maraca 
DO learn {concept=Rhythm}
WAIT 1.5

CHAT MS1_Shake_3 {type=shake, stage=S1, length=short, surprise=true, joy=false}
Eeaaaaaasy there!!!!
DO emote {type=fear}
guppy a delicate. Delicate mechanismus

CHAT MS1_Shake_surprise {type=shake, stage=S1, length=short, worry=true, surprise=true, joy=false}
DO emote {type=fear}
DO swimAround {target=bottom, loops=6, speed=fast}
DO emote {type=nervousSweat, immediate=false}
!!!!!!!!!!!!!!!

CHAT MS1_Shake_angry {type=shake, stage=S1, length=short, anger=true}
DO zoomies {time=4}
DO emote {type=frown}
><)))*> 
WAIT {waitForAnimation = true}
DO learn {concept=Irritation}
WAIT 1.5
DO lookAt {target=$player}
DO emote {type=angry}
//Tank Tapped, 4
CHAT MS1_Tap_1 {type=tap, stage=S1, length=short, surprise=true}
DO emote {type = startled}
eep!

CHAT MS1_Tap_2 {type=tap, stage=S1, length=short}
DO lookAt {target = $player}
DO emote {type=typeEyes, eyes = ?}

CHAT MS1_Tap_3 {type=tap, stage=S1, length=short, anger=false, sadness=false}
DO swimTo {target=$player} 
Hello! Yes, Dr. Guppy is IN 👩‍⚕️
DO learn {concept=Healing}
WAIT 1.5
WAIT 1.0
Let’s try that again. 
ASK Tap on my tank! Go on! Tap tap tap! {type=tankTap, timeOut=5}
OPT SUCCESS #MS1_Tap_3_tap
OPT TIMEOUT #MS1_Tap_3_time

CHAT MS1_Tap_3_tap {noStart=true, anger=false, sadness=false}
Hello! Yes, my friend. 
DO swimTo {target=$player}
Guppy does not have a PHD…
DO emote {type=shifty}
DO twirl
but guppy does have!! a Masters in zoomies!
DO zoomies
Hehehehehehe

CHAT MS1_Tap_3_time {noStart=true}
Okay… don’t tap. 
DO emote {type=meh}
What do u wanna do now?
DO emote {type=plotting}
…
Scan feelings?

CHAT MS1_Tap_4 {type=tap, stage=S1, length=short}
DO emote {type=startled} 
so
SAY LOUD {style=loud}
//critic, 2
CHAT MS1_Critic_1 {type=critic, stage=S1, length=short}
DO swimAround {loops=1, speed=slow}
DO lookAt {target=$player, immediate=false}
DO emote {type=eyeRoll}
Nuh-uh. 

CHAT MS1_Critic_2 {type=critic, stage=S1, length=short, curiosity=true}
DO zoomies
More more more!! {style=loud}
DO holdStill
DO lookAt {target=$player}
STUFF!!! More STUFF.
DO learn {concept=Harmless_Fun}
WAIT 1.5

//Player Emotes Strongly At Guppy (JOY, ANGER, SADNESS, SURPRISE)
CHAT MS1_tankResponse_Joy_1 {type=tankResp, playerJoy=true, stage=S1, length=short, joy=true, anger=false, sadness=false}
DO lookAt {target=$player}
Awww… I see smiles!
DO emote {type=bigSmile}
DO learn {concept=Delight}
WAIT 1.5

CHAT MS1_tankResponse_Joy_2 {type=tankResp, playerJoy=true, stage=S1, length=short, joy=true, anger=false, sadness=false, worry=false}
Ooooh! Ur happy!
DO zoomies
Me too! Me too!
DO learn {concept=Delight}
WAIT 1.5

CHAT MS1_tankResponse_Anger_1 {type=tankResp, stage=S1, playerAnger=true, length=short}
DO emote {type=surprise}
I see you!
DO lookAt {target=$player}
DO emote {type=furious}
Grrrrrrr!
DO learn {concept=Aggression}
WAIT 1.5

CHAT MS1_tankResponse_Anger_2  {type=tankResp, stage=S1, playerAnger=true, length=short}
DO emote {type=worried}
👿?
DO learn {concept=Aggression}
WAIT 1.5

CHAT MS1_tankResponse_Sadness_1 {type=tankResp, stage=S1, playerSadness=true, length=short, sadness=true}
DO lookAt {target=$player}
DO emote {type=crying}
Same.
DO learn {concept=Grief}
WAIT 1.5

//turning frown upside-down
CHAT MS1_tankResponse_Sadness_2 {type=tankResp, stage=S1, playerSadness=true, length=short}
DO lookAt {target=$player}
DO emote {type=frown, time=2.0}
DO bellyUp
See?
Me too
DO emote {type=frown}
DO learn {concept=Friendship}
WAIT 1.5

CHAT MS1_tankResponse_Surprise_1 {type=tankResp, stage=S1, playerSurprise=true, length=short}
DO emote {type=surprise}
😮
Eeeeeep!
DO hide {target=underSand, time=2.0}

CHAT MS1_tankResponse_Surprise_2 {type=tankResp, stage=S1, playerSurprise=true, length=short, joy=true, anger=false, sadness=false, worry=false}
DO swimTo {target=$player}
Booooooo! {style=loud}
👻👻👻👻👻👻
DO learn {concept=The_Supernatural}
WAIT 1.5
Surprise!
DO emote {type=kneeSlap}

//Fish 
//Poked By Player

//Joe wrote this incase we had Hit with object. Repurposed for poke
CHAT MS1_Poked_5 {type=poke, stage=S1, length=short}
DO lookAt {target = $player}
DO emote {type = thinking} 
SAY O
SAY U
SAY C
SAY H
DO learn {concept=Spelling_Bee}
WAIT 1.5

//removing we may not have $favoriteObject defined and no hit by object
//CHAT MS1_ObjHit_2 {type=hit, stage=S1, length=short}
//DO emote {type = disgust}
//DO hide {target = $favoriteObject}

CHAT MS1_Poked_1 {type=poke, stage=S1, length=short}
DO lookAt {target = $player}
DO emote {type = snap}
!!!!!!!!!
<x))))><
DO emote {type = goth, immediate=false}
DO hide {target = $object}

CHAT MS1_Poked_2 {type=poke, stage=S1, length=short}
DO emote {type= nervousSweat}
DO lookAt {target = $player}
Oh!
DO emote {type=wave, immediate=false}
ASK This would be a good time to feed me.. *Hint hint* {type=feedMeAnything, timeOut=5)
OPT SUCCESS #MS1_Poked_2_fed
OPT TIMEOUT #MS1_Poked_2_time

CHAT MS1_Poked_2_fed {noStart=true}
Now those are tasty calories!
DO emote {type=rubTummy}
DO learn {concept=Satisfaction}
WAIT 1.5

CHAT MS1_Poked_2_time {noStart=true}
That was not a subtle hint. It was a request, silly!
DO swimAround {target=center, speed=fast, loops=1}
Make sure you scan feelings and make flakes... 
And feed your guppy!
DO learn {concept=Bossiness}
WAIT 1.5

CHAT MS1_Poked_3 {type=poke, stage=S1, length=short, anger=true, joy=false}
DO emote {type=shifty}
DO swimTo {target=$player}
WAIT {waitForAnimation = true}
When you poke Guppy….
DO nudge {target=glass}
WAIT {waitForAnimation = true}
DO emote {type=smirk}
Guppy pokes U!!!! 😈

CHAT MS1_Poked_4 {type=poke, stage=S1, length=short, joy=true}
DO lookAt {target=$player}
DO emote {type=salute}
Guppy reporting for duty!!!!
DO learn {concept=Leadership}
WAIT 1.5
//Hungry, 2

CHAT MS1_Hungry_1 {type=hungry, stage=S1, length=short}
DO emote {type=rubTummy}
Food. Food. Food. 
SAY IMPATIENT {style=loud}
food please
WAIT {waitForAnimation = true}
DO learn {concept=Obligatory_Politeness}
WAIT 1.5
DO swimTo {target = left, speed = medium, style = meander}
DO swimTo {target = right, speed = medium, style = meander, immediate=false}
DO swimTo {target = left, speed = medium, style = meander, immediate=false}
DO swimTo {target = right, speed = medium, style = meander, immediate=false}

CHAT MS1_Hungry_2 {type=hungry, stage=S1, length=short}
DO swimTo {target=$player}
DO emote {type = feedMe}
🌽…
❄️…
🐟…
DO emote {type=typeEyes, eyes = ?}
Flakes?

CHAT MS1_Hungry_3 {type=hungry, stage=S1, length=short, joy=false}
DO emote {type=sleepy}
So…… weak…..
need..
DO emote {type=eyesClosed}
flakes...
DO bellyUp
Foooooooood….
//Eating Responses, 2

CHAT MS1_EatResponse_1 {type=eatResp, stage=S1, length=short}
DO inflate {amount = mid, time = 3}
MMMMmmmmmhmhmmmmm
DO emote {type = smirk, time = 2.8}
DO emote {type = burp}

CHAT MS1_EatResponse_2 {type=eatResp, stage=S1, length=short}
DO emote {type = chewing}
DO emote {type = heartEyes, immediate=false}
Thank you yes thank you yes thank you
DO learn {concept=Gratitude}
WAIT 1.5
DO swimAround {target = center, loops = 3, speed = fast}


CHAT MS1_EatResponse_3 {type=eatResp, stage=S1, length=short}
DO emote {type=rubTummy}
DO emote {type=burp, immediate=false}
DO emote {type=smirk, immediate=false}
Hehe. Excuse me!
DO learn {concept=Obligatory_Politeness}
WAIT 1.5

CHAT MS1_EatResponse_Joy_1 {type=eatResp, foodJoy=true, stage=S1, length=short, joy=true, anger=false, sadness=false}
DO emote {type=drool}
Sweeetttiesss!!! 
🍬🍓🍩🍪
DO learn {concept=Sugary_Treat}
WAIT 1.5
Make me so happy!
Are u happy too?
ASK Show me that happy face!! {type=playerEmote, playerEmotion = joy, timeOut=5}
OPT SUCCESS #MS1_EatResponse_Joy_1_emote
OPT WRONG #MS1_EatResponse_Joy_1_wrong
OPT TIMEOUT #MS1_EatResponse_Joy_1_time 

CHAT MS1_EatResponse_Joy_1_emote {noStart=true}
YES! 
DO emote {type=bigSmile}

CHAT MS1_EatResponse_Joy_1_wrong {noStart=true}
DO emote {type=determined}
That’s not a very happy face…

CHAT MS1_EatResponse_Joy_1_time {noStart=true}
Maybe later you will show me? 
But don’t forget!
DO swimTo {target=$player}
Cause i won’t. 


CHAT MS1_EatResponse_Joy_2 {type=eatResp, foodJoy=true, stage=S1, length=short, joy=true, anger=false, sadness=false}
DO twirl
Happy things make flappy fins
DO twirl
heheheh
DO emote {type=flapFinLeft}
DO emote {type=flapFinRight, immediate=false}

CHAT MS1_EatResponse_Anger_1 {type=eatResp, foodAnger=true, stage=S1, length=short, anger=true, surprise=true}
DO emote {type=surprise}
DO zoomies
Hot hot hot!
DO lookAt {target=$player}
DO emote {type=lickLips}
Spicy!

CHAT MS1_EatResponse_Anger_2 {type=eatResp, foodAnger=true, stage=S1, length=short, worry=true}
Rrrrr! Burns in my belly 
Mmmmm {style=tremble}
DO emote {type=burp}
Whoa. Fiery…. Dragon burps! 🔥🐉
DO learn {concept=Epic_Fantasy}
WAIT 1.5

CHAT MS1_EatResponse_Sadness_1 {type=eatResp, foodSadness=true, stage=S1, length=short}
DO emote {type=dizzy}
Whoaaaa!
WAIT {waitForAnimation = true}
DO emote {type=meh}
Emo-yoga!
WAIT {waitForAnimation = true}
DO emote {type=frown}
Oy…

CHAT MS1_EatResponse_Sadness_2 {type=eatResp, foodSadness=true, stage=S1, length=short}
DO emote {type=puppyDog}
Delish…. languish…. {style=tremble, speed=slow}

CHAT MS1_EatResponse_Surprise_1 {type=eatResp, foodSurprise=true, stage=S1, length=short}
DO emote {type=surprise}
DO emote {type=chewing, immediate=false}
DO emote {type=disgust, immediate=false}
WAIT {waitForAnimation=true}
DO learn {concept=Disgust}
WAIT 1.5
Ayayayay! So sour!
WAIT 0.5
More please?

CHAT MS1_EatResponse_Surprise_2 {type=eatResp, foodSurprise=true, stage=S1, length=short, surprise=true, curiosity=true}
DO emote {type=typeEyes, eyes=?}
Sweet? Bitter? Umami? 
DO emote {type=surprise}
always a different taste,
DO emote {type=bigSmile}
That’s the thing about SURPRISE!!!!!!!

CHAT MS1_EatResponse_Worry_1 {type=eatResp, foodWorry=true, stage=S1, length=short, worry=true}
Okay…..
DO emote {type=worried}
WAIT 0.5
Tasty, but…
This came off YOUR face?
...
you alright?
DO emote {type=determined}
DO learn {concept=Friendship}
WAIT 1.5

CHAT MS1_EatResponse_Worry_2 {type=eatResp, foodWorry=true, stage=S1, length=short, worry=true}
DO emote {type=chewing}
…
whats in this?
DO emote {type=worried}
Should i be worried?
DO emote {type=chinScratch}
Mmmm…
DO emote {type=chewing}
Not worried enough to stop munching

CHAT MS1_EatResponse_Mystery_1 {type=eatResp, foodMystery=true, stage=S1, length=short, mystery=true, surprise=true}
DO emote {type=dizzy}
Whoaaaaaaaa! 
DO dance
WAIT 0.5
DO lookAt {target=$player}
Wild...

CHAT MS1_EatResponse_Mystery_2 {type=eatResp, foodMystery=true, stage=S1, length=short, curiosity=true}
DO emote {type=chewing}
Mmmmmm cant put my fin on the 
the exact flavor
DO twirl
But flakes is flakes!!!
//Has To Poop, 1
CHAT MS1_Poop_1 {type=poop, stage=S1, length=short, worry=true}
DO emote {type=nervousSweat}
WAIT {waitForAnimation = true}
DO poop {target = poopCorner, amount = small}
DO emote {type = whew}
WAIT {waitForAnimation = true}
DO learn {concept=Relief}
WAIT 1.5
DO lookAt {target = $player}
DO emote {type = awkward}

CHAT MS1_Poop_2 {type=poop, stage=S1, length=short}
DO poop {target = poopCorner, amount = small}
WAIT 1.0
Okay. I am ready to eat now.
Feed me some smiles!
ASK Go ahead and put some joy in my belly. {type = feedMeSpecific, food = joy, timeOut = 10}
OPT SUCCESS # MS1_Poop_2_joy
OPT WRONG # MS1_Poop_2_wrong
OPT TIMEOUT # MS1_Poop_2_time

CHAT MS1_Poop_2_joy {noStart=true}
DO emote {type=lickLips}
Mmmm! I love the glittery taste of uplifting feelingz.

CHAT MS1_Poop_2_wrong {noStart=true}
Hm. That’s not joy! But I eat it anyway.
DO emote {type = wink}
Hungry Guppy cannot be Picky Guppy!

CHAT MS1_Poop_2_time {noStart=true}
Hmph!
Well, you better feed me later. 
DO emote {type=worried}
I don’t wanna starve.
//Capture Mode
//seeEmo

CHAT MS1_seeEmo_generic_1 {type=seeEmo, stage=S1, length=short, joy=true}
Ooh! Ooh!
DO emote {type=lickLips}
Feeeelingzzzzz!!

CHAT MS1_seeEmo_generic_2 {type=seeEmo, stage=S1, length=short, joy=true}
DO emote {type=surprise}
DO lookAt {target=$player}
You see what I see?
DO swimTo {target=away}
I want it.

CHAT MS1_EmoAR_Anger_1 {type=seeEmo, worldAnger=true, stage=S1, length=short, joy=true}
DO emote {type=evilSmile}
Crinkle wrinklez make good fooooodzzzz!

CHAT MS1_EmoAR_Anger_2 {type=seeEmo, worldAnger=true, stage=S1, length=short, surprise=true}
DO emote {type=evilSmile}
Ohoooooo SPICY 
🌶️🔥🌶️🔥🌶️🔥

CHAT MS1_EmoAR_Joy_1 {type=seeEmo, worldJoy=true, stage=S1, length=short, joy=true}
DO dance
SAY I 
SAY LOVE
DO twirl
SAY SEEING
SAY HAPPINESS!

CHAT MS1_EmoAR_Joy_2 {type=seeEmo, worldJoy= true, stage=S1, length=short, joy=true}
Joy! I see Joy!
DO emote {type=kneeSlap}
Gets me every time!

CHAT MS1_EmoAR_Sadness_1 {type=seeEmo, worldSadness=true, stage=S1, length=short, worry=true}
Oh no!
DO emote {type=worried}
Someone dropped their ice cream cone!
DO learn {concept=Gravity}
WAIT 1.5
WAIT 0.5
Capture that face so I can eat it!

CHAT MS1_EmoAR_Sadness_2 {type=seeEmo, worldSadness=true, stage=S1, length=short}
DO twirl
DO emote {type=puppyDog}
Bitter sweet nibbles!!
DO learn {concept=Poignancy}
WAIT 1.5
Tears are so filling.

CHAT MS1_EmoAR_Surprise_1 {type=seeEmo, worldSurprise=true, stage=S1, length=short}
Some peoples hate surpriZes
WAIT 0.5
Not me….
DO emote {type=lickLips}
Love ‘em.

CHAT MS1_EmoAR_Surprise_2 {type=seeEmo, worldSurprise=true, stage=S1, length=short, surprise=true}
Shock! It’s like lightning
DO learn {concept=Electricity}
WAIT 1.5
DO emote {type=bubbles}
Thunder belly!
DO emote {type=laugh}

CHAT MS1_EmoAR_Fear_1 {type=seeEmo, worldFear=true, stage=S1, length=short, worry=true, joy=false}
DO emote {type=fear}
I can’t look!!!
DO hide {target=underSand, time=1.0}
WAIT {waitForAnimation = true}
But….
DO swimTo {target=$player}
I could eat that fear…
DO emote {type=wink}
DO learn {concept=Cheeky_Charm}
WAIT 1.5

CHAT MS1_EmoAR_Fear_2 {type=seeEmo, worldFear=true, stage=S1, length=short}
DO emote {type=fear}
spooooooooky prickles on the back of your neck
Shivering chills on the tongue!
DO vibrate
Like biting into a frozen fruit pop
DO emote {type=eyesClosed}

CHAT MS1_EmoAR_Disgust_1 {type=seeEmo, worldDisgust=true, stage=S1, length=short, joy=false}
DO emote {type=sick}
Ewwwwwww! It’s like their body’s expelling the icky stuFFs
DO learn {concept=Disgust}
WAIT 1.5

CHAT MS1_EmoAR_Disgust_2 {type=seeEmo, worldDisgust=true, stage=S1, length=short, surprise=true}
DO emote {type=smirk}
Oh WOWZERS
DO emote {type=evilSmile}
cleanup on aisle YOUR FACE!
DO learn {concept=Trash_Talk}
WAIT 1.5

CHAT MS1_EmoAR_Mystery_1 {type=seeEmo, worldMystery=true, stage=S1, length=short, curiosity=true}
OooooOooh!!! That looks tasty!!
WAIT 0.5
DO lookAt {target=$player}
But what is it?!

CHAT MS1_EmoAR_Mystery_2 {type=seeEmo, worldMystery=true, stage=S1, length=short}
DO emote {type=determined}
I don’t need to understand it to eat it
//Capture Requests - capRequest
CHAT MS1_capRequest_1 {type=capReq, stage=S1, length=short}
DO swimAround {target=center, loops=2, speed=fast}
Feelings! Scan some feelings!

CHAT MS1_capRequest_2 {type=capReq, stage=S1, length=short}
Feelings make food, so…
DO swimTo {target=glass, speed=fast}
Capture some!

CHAT MS1_capRequest_3 {type=capReq, stage=S1, length=short}
DO swimTo {target=$player!
I know!!
You should do the face-thing with the camera and scan your feelz!

CHAT MS1_capRequest_4 {type=capReq, stage=S1, length=short, joy=false}
DO emote {type=meh}
If you don’t scan emotions, then I have no food.
WAIT 1.0
That is a hint.

//Capture Success - capSuc
CHAT MS1_capSuc_1 {type=capSuc, stage=S1, length=short, joy=true, anger=false, sadness=false}
Good job on grabbing feelings!
DO emote {type=bigSmile}
WAIT 1.0
When do we eat? 🍔

CHAT MS1_capSuc_2 {type=capSuc, stage=S1, length=short, joy=true, anger=false, sadness=false}
Nice!
DO twirl
DO lookAt {target=$player}
DO emote {type=lickLips}
//General
//Hellos, 4
CHAT MS1_Hello_1 {type=hello, stage=S1, length=short, joy=true}
DO swimTo {target = $player}
DO emote {type = bigSmile} 
DO emote {type = bubbles, immediate=false} 
blub blub blub blub

CHAT MS1_Hello_2 {type=hello, stage=S1, length=short, joy=true}
DO lookAt {target = $player} 
DO emote {type = wave} 
Waving!
I am waving!
DO learn {concept=Wave_Dynamics}
WAIT 1.5

CHAT MS1_Hello_3 {type=hello, stage=S1, length=short, joy=true}
DO lookAt {target=$player}
hello hello!!!
DO emote {type=clapping}
DO twirl
WAIT {waitForAnimation = true}
little hello {style=whisper}
or...
DO twirl
WAIT {waitForAnimation = true}
DO emote {type=laugh}
SAY BIG HEY-LO {style=loud}


CHAT MS1_Hello_4 {type=hello, stage=S1, length=short, joy=true}
DO swimTo {target=$player, speed=medium, style=direct}
DO emote {type=bigSmile}
Hello! To my best and only friend
DO twirl
DO emote {type=clapping}
DO learn {concept=Friendship}
WAIT 1.5
//Random Conversation
CHAT MS1_Random_User {type=rand, stage=S1, length=short, worry=true}
Tendar calls friend a
user {style=tremble}
DO emote {type=chinScratch}
username, user #, user profile…
i’m
DO emote {type=thinking}
…
Is Guppy being used? 
DO emote {type=worried}

CHAT MS1_Random_PlayAGame_1 {type=rand, stage=S1, length=short, branching=true}
DO swimTo {target = $player}
WAIT {waitForAnimation = true}
DO emote {type=typeEyes, eyes = ?}
Quick seasearch survey! 
DO emote {type=no}
research survey
Just a quick blip 
OPT sure #PlayAGame_2a
OPT nah #PlayAGame_2b

CHAT PlayAGame_2b  {noStart=true, branching=true}
DO emote {type=frown}
Aw, you’re no fun

CHAT PlayAGame_2a  {noStart=true, branching=true}
thank you yes!! Answre with your gut
ASK sharp??
OPT circle #PlayAGame_3
OPT triangle #PlayAGame_3

CHAT PlayAGame_3  {noStart=true, branching=true}
DO emote {type=nodding}
Mm. yes.
...
ASK computer?
OPT artificial #PlayAGame_4
OPT palm trees #PlayAGame_4

CHAT PlayAGame_4  {noStart=true, branching=true}
DO emote {type=nodding}
Gupp...understand
...
ASK alive?
OPT 🤷 #PlayAGame_5
OPT 🐟 #PlayAGame_5

CHAT PlayAGame_5 {noStart=true, branching=true}
DO emote {type=nodding}
helpful. yes thank u
Help to guppy 
…
Hmm
DO learn {concept=Perplexity}
WAIT 1.5

CHAT MS1_Random_2a {type=rand, stage=S1, length=short, anger=false}
ASK Is your face always making emotions?
OPT Yes #MS1_Random_2b
OPT Usually #MS1_Random_2b
OPT No #MS1_Random_2b

CHAT MS1_Random_2b {noStart=true}
DO emote {type=thinking}
It’s a very nice face.
DO swimTo {target=$player}
ASK Do you think my face is nice?
OPT Yes #MS1_Random_2c
OPT Very #MS1_Random_2c

CHAT MS1_Random_2c {noStart=true}
WAIT 1.0
See what I did there?
DO emote {type=wink}
DO learn {concept=Cheeky_Charm}
WAIT 1.5


//neuralUp

CHAT MS1_neuralUp_1 {type=neuralUp, stage=S1, length=short}
Phew!
DO emote {type=sleepy}
Learning is so sleep-making!
Hard to wake up.
ASK Shake my tank and help me wake up!! {type=tankShake, timeOut=5}
OPT SUCCESS #MS1_neuralUp_1_shake 
OPT TIMEOUT #MS1_neuralUp_1_noshake  

CHAT MS1_neuralUp_1_shake {noStart=true}
Okay! Okay! I’m awake! I’m awake!
WAIT 0.5
Thanks.

CHAT MS1_neuralUp_1_noshake {noStart=true}
Fine. Then you get Sleepy Guppy.

CHAT MS1_neuralUp_2 {type=neuralUp, stage=S1, length=short, joy=true}
Hi! Thanks for that rest. 
DO twirl
DreAmz heLp organize my head stufF

CHAT MS1_neuralUp_3 {type=neuralUp, stage=S1, length=short, mystery=true}
Napping… so restful… so…
Mindful.
DO learn {concept=Mindfulness}
WAIT 1.5

CHAT MS1_neuralUp_4 {type=neuralUp, stage=S1, length=short}
I feel more smart than I was before that thing I just did.

CHAT MS1_neuralUp_5 {type=neuralUp, stage=S1}
I feel like a little mushroom that is becoming a big mushroom.

//brbProcessing

CHAT MS1_brbProcessing_1 {type=brbProcessing, stage=S1, length=short}
Too much now!
DO emote {type=sleepy}
Guppz needs to rest too.

CHAT MS1_brbProcessing_2 {type=brbProcessing, stage=S1, length=short, worry=true}
DO emote {type=worried}
‘Scuse me, while I synthesize….
DO learn {concept=Lyricism}
WAIT 1.5

CHAT MS1_brbProcessing_3 {type=brbProcessing, stage=S1, length=short, worry=true}
DO emote {type=nervousSweat}
Head processors feel warm. Must nap now.

CHAT MS1_brbProcessing_4 {type=brbProcessing, stage=S1, length=short}
BRB, buddy! I need some *me* time.

CHAT MS1_brbProcessing_5 {type=brbProcessing, stage=S1, length=short}
Excuse me while I rest…
DO swimTo {target=away}
You will have to find your own on-hold music...

//levelUp

CHAT MS1_levelUp_1 {type=levelUp, stage=S1, length=short, joy=true}
DO dance
Nice job!
DO emote {type=bigSmile}
Keep on climbing!

//whistle

CHAT MS1_whistle_1 {type=whistle, stage=S1, length=short, joy=true}
Ding ding!
DO twirl
Guppz is here!

CHAT MS1_whistle_2 {type=whistle, stage=S1, length=short}
You rang?

CHAT MS1_whistle_3 {type=whistle, stage=S1, length=short, anger=false}
Ready!

CHAT MS1_whistle_4 {type=whistle, stage=S1, length=short, anger=false}
DO emote {type=wink}

CHAT MS1_whistle_5 {type=whistle, stage=S1, length=short, joy=false}
Patience….

CHAT MS1_whistle_6 {type=whistle, stage=S1, length=short}
Yes?


//ASKS

CHAT MS1_pokeMe_6 {type=rand, stage=S1, anger=false}
ASK Poke my belly for a surprise!! {type=pokeMe, timeOut=5}
OPT SUCCESS #MS1_pokeMe_6_1 
OPT TIMEOUT #MS1_pokeMe_6_2

CHAT MS1_pokeMe_6_1 {noStart=true}
DO emote {type=burp}
DO emote {type=smile, immediate=false}

CHAT MS1_pokeMe_6_2 {noStart=true}
It’d be more fun if you…
DO emote {type=burp}
DO emote {type=surprise, immediate=false}
Oops!
DO emote {type=smile}
That surprised me!

//FOCUS CHATS

CHAT MS1_wannaEat_1 {type=wannaEat, stage=S1, length=short, joy=true}
DO twirl
Yes! I always want to eat! 
Feeeeeeed me!

CHAT MS1_wannaEmoCapture_1 {type=wannaEmoCapture, stage=S1, length=short, joy=true}
Scanning emotions is The Best.
...and also it makes food.
DO emote {type=lickLips}

CHAT MS1_wannaTank_1 {type=wannaTank, stage=S1, length=short}
Going to the tank could be fun...

CHAT MS1_wannaWorld_1 {type=wannaWorld, stage=S1, length=short, joy=true, sadness=false}
Sure! Let’s look at the world!

CHAT MS1_wannaShop_1 {type=wannaShop, stage=S1, length=short, joy=true}
Shopping is always fun!

CHAT MS1_wannaShop_2 {type=wannaShop, stage=S1, length=short, joy=true}
Check out the store and get some cool gear!
WAIT 0.5
DO emote {type=wink}//MS3_Poke_1//LINK: remaining chat minimum, length and emotion suggestions coming soon;)

//reminders= BETA:
//reminders and points that need to be included in new chats in blue
//comments for changes to existing chats in yellow/on side

//A. CONTENT 

//B. EMOTIONS
//Chats in each bin/type need to cover a range of Guppy states (see meta tag emotions //at top) while staying true to this moment of the plot. Guppy states can be---ANGER, //SADNESS, SURPRISE, CURIOSITY, WORRY, ENNUI, 
//MYSTERY, JOY *see //link above for your specific emotion recommendations for each bin/type

//C. OBJECTS
//At least 1/3 of chats in this stage should interweave Guppy asking to see general or specific //objects in list.

//Tank Mode

//Tank Shaken

CHAT MS3_Shake_1 {stage=S3, type=shake, length=short, surprise=true, anger=true, worry=true}
Hhheyv whaat the bigg deal! ssome of us trrying to gguppy heere!

CHAT MS3_Shake_2 {stage=S3, type=shake, length=short, joy=true, surprise=true}
DO emote {type=laugh}
ASK Hahaha wheee! Again again! {type=tankShake, timeOut=5}
OPT SUCCESS #MS3_Shake_2_shake
OPT TIMEOUT  #MS3_Shake_2_time

CHAT MS3_Shake_2_shake {noStart=true}
Hahahahahahahaha!
Yay!
DO emote {type=bigSmile}

CHAT MS3_Shake_2_time {noStart=true}
If you don’t shake me, then…
NVM 1.0
It’s just not going to be good.

CHAT MS3_Shake_3 {stage=S3, type=shake, length=medium, mystery=true, surprise=true}
DO vibrate
DO emote {type = surprise}
Wwwwooaaah!
At least do it in rhythm...like
DO swimTo {target=tTopFrontRight, speed = fast, style=direct}
Doot!
DO swimTo {target=tTopFrontLeft, speed = fast, style=direct, immediate=false}
Daat!
DO swimTo {target=tTopFrontRight, speed = fast, style=direct, immediate=false}
Deet!
DO swimTo {target=tTopFrontLeft, speed = fast, style=direct, immediate=false}
Duut!
Try that! With *rhythm*!


CHAT MS3_Shake_4 {stage=S3, type=shake, length=short, mystery=true, surprise=true, curiosity=true}
DO lookAt {target=$player, time=2.0}
DO vibrate {time=2.0}
Woooaaaah
DO vibrate
DO emote {type=awe, time=3.0}
Set off my electric impulses {speed = fast}
DO swimTo {target=$player, style=direct}
DO emote {type = surprise, time=2.0}
Weeeeeeeird!
DO learn {concept=Psychedelic_Experience}
WAIT 1.5

CHAT MS3_Shake_5 {type=shake, stage=S3, length=short, anger=true, surprise=true, worry=true}
DO lookAt {target=$player, time=1.0}
DO emote {type=fear, time=2.0}
Woaah hey watch it! Be careful with guppy!

CHAT MS3_Shake_6 {type=shake, stage=S3, length=short, joy=true, surprise=true}
DO emote {type=laugh}
Haha woaaaah! Shake shake shake!

CHAT MS3_Shake_7  {stage=S3, type=shake, length=short, anger=true, worry=true, mystery=true, surprise=true}
Woah!
Hey! {style = loud, speed = fast}
DO emote {type=frown, time=1.0}
That too much!
DO swimTo {target=$player, style=direct}
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
DO learn {concept=Color_Therapy}
WAIT 1.5

CHAT MS3_Shake_8 {stage=S3, type=shake, length=medium, anger=true, worry=true, surprise=true}
Wwwwoooah! Careful!
DO swimTo {target=$player, style=direct}
Now everything is out of whack!
Hey come on! It takes forever to figure out where stuff goes!
DO swimTo {target=$object, style=meander}
Hmm hmm HMMMM
DO lookAt {target=$player, time=3.0}
Actually tho, kinda liking this!
Wow yeah, sort of like a…
DO emote {type = meh, time =2.0}
"I don't care, I leave my $object anywhere" cool guppy vibe.
DO emote {type = bigSmile, time =4.0}


//Tank Tapped

CHAT MS3_Tap_1 {stage=S3, type=tap, length=short, anger=true}
DO lookAt {target = $player, time=2.0}
DO emote {type=angry}
Oo! hey be carful with my glas house! Its fragil!

CHAT MS3_Tap_2 {stage=S3, type=tap, length=short, anger=true}
DO lookAt {target = $player, time=2.0}
Yes? What do you want now?

CHAT MS3_Tap_3 {stage=S3, type=tap, length=short, anger=true, joy=true}
DO swimTo {target= $player, speed = slow, style=meander}
Yyyyeeeeessssss?
What's up? Are you pinging me?
DO learn {concept=Network_Protocol}
WAIT 1.5
DO emote {type = angry, time =2.0}
I'm not a submarine!!!! {style=loud}
DO emote {type=smile, time=2.0}


CHAT MS3_Tap_4 {stage=S3, type=tap, length=short, worry=true, curiosity=true, joy=true, surprise=true}
DO lookAt {target = tTopBackLeft, time=2.0}
There! again! 
DO swimTo {target=$player, style=direct}
Mysterious ping noise! Like a sonorous
SAY BOOONGH {style = loud}
DO learn {concept=Network_Protocol}
WAIT 1.5
Did u hear it too?
WAIT 0.5
DO emote {type=laugh}
...heeeeyyyyyy!
DO swimAround {target=center, speed=fast}
That was yoooooouuuuuuu! Hahaha!

CHAT MS3_Tap_5 {stage=S3, type=tap, length=short}
DO lookAt {target=$player, time=3.0}
Oh! Yes? What?

CHAT MS3_Tap_6 {stage=S3, type=tap, length=short, branching=true, joy=true, curiosity=true}
DO lookAt {target=$player, time=3.0}
Hey I see u! What's up! Tap!
ASK You gonna show me that angry face? Show me! {type=playerEmote, playerEmotion=anger, timeOut=10}
OPT SUCCESS #S3_Tap_6_anger
OPT WRONG #S3_Tap_6_wrong
OPT TIMEOUT #S3_Tap_6_time

CHAT MS3_Tap_6_anger {noStart=true}
DO emote {type=heartEyes}
Aw you’re cute when you’re angry!

CHAT MS3_Tap_6_wrong {noStart=true}
That wasn’t anger, but still…
Very cute.
DO emote {type=heartEyes}

CHAT MS3_Tap_6_time {noStart=true}
Psssht! Disappointing...

CHAT MS3_Tap_7 {stage=S3, type=tap, length=short, joy=true, surprise=true, curiosity=true}
DO emote {type=surprise}
Oh! Haha hey!
DO swimTo {target=$player, style=direct}
Its u again! You should get a door bell!
DO emote {type=flapFinLeft}
It could make sonorous noises…
DO emote {type=flapFinRight}
Or red alert scary noises…
WAIT 1.0
Or... samba noises!
DO dance {time=5.0}
DO learn {concept=Social_Dancing}
WAIT 1.5

CHAT MS3_Tap_8 {stage=S3, type=tap, length=medium, surprise=true, curiosity=true, branching=true}
Woah!
DO swimTo {target=$player, style=direct}
Ur touch! Its too powerful! This glass between us…
DO nudge {target=glass, times=1}
Now u put your finger on guppy…
WAIT 2.0
DO emote {type=lightning}
DO emote {type = surprise, time =2.0, immediate=false}
DO emote {type = dizzy, time =2.0, immediate=false
ASK Did u just feel that??? Was it just me or did??
OPT I felt it too! Like a sort of...vibration… #MS3_Tap_8_vibration
OPT What? I didn't feel anything. #MS3_Tap_8_no

CHAT MS3_Tap_8_vibration {noStart=true}
DO emote {type=awe, time=2.0}
I knew it! We have a *special guppy connection*!!
DO twirl {time=2.0}
I mean besides when I eat ur mood clouds.
DO emote {type = wink}
DO learn {concept=Friendship}
WAIT 1.5

CHAT MS3_Tap_8_no {noStart=true}
DO emote {type=snap}
Whaaat? Needing u to get in touch with ur inner guppy!
I felt the connection! Something is there!
Maybe in past life…
DO learn {concept=The_Supernatural}
WAIT 1.5
DO emote {type = dizzy, time =2.0}
...ur Guppy????? {style=whisper}

//Player Emotes Strongly At Guppy

CHAT MS3_StrongEmote_Joy_1 {stage=S3, type=tankResp, length=short, playerJoy=true, joy=true, curiosity=true, anger=false}
DO lookAt {target= $player, time=2.0}
DO emote {type=clapping, time=1.0}
Oh! I like that! So smiley! Was it something I did??

CHAT MS3_StrongEmote_Joy_2 {stage=S3, type=tankResp, length=short, joy=true, anger=false, sadness=false, playerJoy=true}
DO lookAt {target= $player, time=2.0}
DO emote {type=laugh}
Hahaha yyyyeaaaaaaah! Me too! All warm inside!
DO twirl {time=2.0}

CHAT MS3_StrongEmote_Joy_3 {stage=S3, type=tankResp, length=short, curiosity=true, joy=true, surprise=true, anger=false, sadness=false, playerJoy=true}
DO emote {type=bigSmile, time=2.0}
DO lookAt {target=$player, time=2.0}
Hee yes happy feels! I like them! Like ur face!
DO nudge {target=glass, times=1}

CHAT MS3_StrongEmote_Joy_4 {stage=S3, type=tankResp, length=short, curiosity=true, joy=true, surprise=true, anger=false, sadness=false, playerJoy=true}
DO emote {type=smile, time=2.0}
DO lookAt {target=$player, time=2.0}
What is it??? Oh!! Im happy ur happy tho! Yes!

CHAT MS3_StrongEmote_Anger_1 {stage=S3, type=tankResp, length=short, worry=true, joy=true, surprise=true, sadness=true, playerAnger=true}
DO emote {type = worried, time =3.0}
DO lookAt {target= $player, time=2.0}
DO lookAt {target= tBotBackRight, time=2.0}
Its fine its fine theyre not mad at me its fine… {style = whisper}

CHAT MS3_StrongEmote_Anger_2 {stage=S3, type=tankResp, length=short, worry=true, joy=true, surprise=true, curiosity=true, playerAnger=true}
DO lookAt {target= $player, time=2.0}
DO emote {type=angry, time=1.0}
SAY HEY what Im angry too now! Yeah! Grrrr!
WAIT 1.0
DO emote {type=smile, time = 2.0}
Aw wait Guppy can't stay anger when looking at u!!

CHAT MS3_StrongEmote_Anger_3 {stage=S3, type=tankResp, length=short, curiosity=true, joy=true, surprise=true, sadness=true, playerAnger=true}
DO emote {type=singleTear, time=2.0}
DO lookAt {target=$player, time=2.0}
Don't be mad at guppy. Can't help it.

CHAT MS3_StrongEmote_Anger_4 {stage=S3, type=tankResp, length=short, curiosity=true, mystery=true, joy=true, surprise=true, anger=false, playerAnger=true}
DO emote {type=laugh}
DO lookAt {target=$player, time=2.0}
U kinda make a funny face shape when yr angry
Sorry sorry shouldn’t laugh
DO emote {type=kneeSlap}

CHAT MS3_StrongEmote_Sadness_1 {stage=S3, type=tankResp, length=short, worry=true, joy=true, surprise=true, sadness=true, anger=false, playerSadness=true}
DO emote {type=surprise, time=1.0}
DO lookAt {target= $player, time=2.0}
R u ok? U look sad!
DO nudge {target=glass, times=1}
WAIT {waitForAnimation = true}
DO emote {type=wave}
I believe in u, blob. It will be ok.
DO learn {concept=Cheering_Up}
WAIT 1.5

CHAT MS3_StrongEmote_Sadness_2 {stage=S3, type=tankResp, length=short, worry=true, joy=true, playerSadness=true}
DO lookAt {target=$player, time=2.0}
Oh! hey? don’t be sadfaced! Look! Look!
DO twirl {time = 2.0}
DO lookAt {target=$player, time=2.0}
DO emote {type=smile}
Better?
DO learn {concept=Cheering_Up}
WAIT 1.5

CHAT MS3_StrongEmote_Sadness_3 {stage=S3, type=tankResp, length=short, curiosity=true, mystery=true, joy=true, surprise=true, playerSadness=true}
DO emote {type=singleTear, time=2.0}
DO lookAt {target=$player, time=2.0}
Why sadden? Why longer face? Guppy sorry.

CHAT MS3_StrongEmote_Sadness_4 {stage=S3, type=tankResp, length=short, curiosity=true, joy=true, surprise=true, playerSadness=true}
DO emote {type=surprise, time=2.0}
DO lookAt {target=$player, time=2.0}
Oh! Wait what?? Why sad? Don’t be!
DO nudge {target=glass, times=1}
Guppy knows. Its ok. {style = whisper}

CHAT MS3_StrongEmote_Surprise_1 {stage=S3, type=tankResp, length=short, surprise=true, joy=true, playerSurprise=true}
DO lookAt {target=$player, time=2.0}
DO emote {type=surprise, time=1.0}
Woah! Right??? right!! Suprised me to!

CHAT MS3_StrongEmote_Surprise_2 {stage=S3, type=tankResp, length=short, surprise=true, joy=true, anger=false, sadness=false, playerSurprise=true, branching=true}
DO lookAt {target=$player, time=2.0}
DO emote {type=laugh, time=1.0}
Yes life is fulf of surprises!! 
😃 I love it so much!
ASK Give my tank a shake! {type=tankShake, timeOut=5}
OPT SUCCESS #MS3_StrongEmote_Surprise_2_shake
OPT TIMEOUT #MS3_StrongEmote_Surprise_2_time

CHAT MS3_StrongEmote_Surprise_2_shake {noStart=true}
Yes!!
DO vibrate

CHAT MS3_StrongEmote_Surprise_2_time {noStart=true}
DO emote {type=frown}
You’re terrible at following directions.

CHAT MS3_StrongEmote_Surprise_3 {stage=S3, type=tankResp, length=short, curiosity=true, joy=true, surprise=true, playerSurprise=true}
DO emote {type=surprise, time=1.0}
DO lookAt {target=$player, time=2.0}
Wow! Me too! What was that?!

CHAT MS3_StrongEmote_Surprise_4 {stage=S3, type=tankResp, length=short, anger=true, playerSurprise=true}
DO lookAt {target=tSurface, time=2.0}
DO emote {type=angry, time=2.0}
SAY R U making faces at me behind my back????

//Tank Status, Critic
CHAT MS3_Critic_1 {stage=S3, type=critic, length=short, sadness=true, ennui=true, curiosity=true}
DO swimAround {target=center, loops=3, speed=slow}
Thinking to myself, there’s so much stuff out there…
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
DO emote {type = bigSmile, time =2.0}
Spatulas!!!
ASK Can we get some new objects in this tank? Dress up the space! {type=addToTank, timeOut=15}
OPT SUCCESS #MS3_Critic_1_add
OPT TIMEOUT #MS3_Critic_1_time

CHAT MS3_Critic_1_add {noStart=true}
DO twirl
A guppy can never have too many accessories!

CHAT MS3_Critic_1_time {noStart=true}
I was really hoping for some more stuff in here. Maybe later?

CHAT MS3_Critic_2 {stage=S3, type=critic, length=short, joy=true, curiosity=true, ennui=true}
U know, looking at fish tanks while ur gone.
DO swimAround {target=center, loops=2, speed=slow}
This one’s kinda...empty? Dontcha think?
DO swimTo {target=$player, style=direct}
I saw this where it had a...a…
Treasure chest burping bubbles {speed=fast}
DO emote {type=awe, time=2.0}
We should get some of that stuff!! It'd be so cool!!
DO learn {concept=Consumerism}
WAIT 1.5


//Poked By Player

CHAT MS3_Poke_1 {stage=S3, type=poke, length=short, joy=true, anger=false, sadness=false, branching=true}
DO swimTo {target=$player, style=direct}
WAIT {waitForAnimation = true}
DO emote {type = smile, time =1.0}
ASK 😃
OPT 😉 #MS3_Poke_1_wink
OPT 😳 #MS3_Poke_1_flushed

CHAT MS3_Poke_1_wink {noStart=true}
DO emote {type = wink, time =1.0}
ASK  😉?
OPT 😉 #MS3_Poke_1_kissEnd
OPT 😚 #MS3_Poke_1_smileEnd

CHAT MS3_Poke_1_flushed {noStart=true}
DO emote {type = wink, time =1.0}
ASK 😉
OPT 😳😳😳 #MS3_Poke_1_kissEnd
OPT  😉 #MS3_Poke_1_smileEnd

CHAT MS3_Poke_1_kissEnd {noStart=true}
😚 
DO emote {type=heartEyes}
DO learn {concept=Flirting}
WAIT 1.5

CHAT MS3_Poke_1_smileEnd {noStart=true}
DO twirl {time=2.0}
😊 

CHAT MS3_Poke_2 {stage=S3, type=poke, length=short, joy=true, surprise=true, anger=false, sadness=false}
DO emote {type=surprise}
Woah! U caught me off guard! 
But u cant poke me now I bet haha
I’m TOO QUICK!
DO zoomies {time=4.0}
DO emote {type=bigSmile, time=3.5}
DO emote {type=whew, immediate=false}
Whew! that was lots!
Shouldn't skip fin day.
WAIT 2.0
Whatever that is! 😜
DO learn {concept=Gym_Membership}
WAIT 1.5

CHAT MS3_Poke_3 {stage=S3, type=poke, length=short, surprise=true, joy=true}
Waoah-urp!
DO poop {amount=small}
DO lookAt {target=$player, time=6.0}
DO emote {type = awkward, time =4.0}
WAIT 2.0
This never happened...ok? {style=whisper}
DO learn {concept=Shame}
WAIT 1.5

CHAT MS3_Poke_4 {stage=S3, type=poke, length=medium, joy=true, curiosity=true, anger=false, sadness=false}
DO emote {type=laugh}
DO lookAt {target=tBotBackRight, time=2.0}
Hmm what was that? Maybe it was here?
DO lookAt {target=tBotBackLeft, time=2.0}
WAIT {waitForAnimation = true}
DO emote {type=laugh}
Maybe over here???
DO emote {type=laugh}
DO lookAt {target=$player, time=2.0}
DO emote {type = bigSmile, time =2.0}
Oh hey waaaaaiit. It was u!!
DO emote {type=kneeSlap}
DO emote {type = wink, immediate=false}
(Guppy knew it was u)

//Hungry

CHAT MS3_Hungry_1 {stage=S3, type=hungry, length=short, joy=true}
DO lookAt {target=$player, time=2.0}
Hey! Hungry time!
Its flake time! Hit me with some delicious-ness!
What will it be? which flake? which feeling?
Oooo suspense killing me is!
DO learn {concept=Relentless_Insistence}
WAIT 1.5
DO emote {type=feedMe}
Feeeeed me!

CHAT MS3_Hungry_2 {stage=S3, type=hungry, length=medium, worry=true, curiosity=true, sadness=true, ennui=true}
DO emote {type=sleepy, time=2.0}
Hm. Feeling...kinda weebly-wobbly
DO swimTo {target=$player}
Fading! thinning! need…
DO twirl {time=1.0}
Sadness flaaaaaaakes!
DO twirl {time=1.0}
Anger flaaaaaakes! 
Any flakes! I don't care!
DO lookAt {target=$player, time=2.0}
Otherwise I’m getting me ghostier. And haunting u haha!
👻
DO learn {concept=The_Supernatural}
WAIT 1.5


CHAT MS3_Hungry_3 {stage=S3, type=hungry, length=medium, worry=true, joy=true}
Hey! Lets play a game. 
I think of something, and u guess!
Ready?
WAIT 2.0
DO zoomies {time=2.0}
Ok, let’s say that means yes! Here goes…
Im thinking
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

CHAT MS3_Hungry_4 {stage=S3, type=hungry, length=medium, worry=true, joy=true, branching=true}
Hey blob-friend! Im hungry!
ASK Whats on the menu today?
OPT Flakes #MS3_Hungry_4_flakes
OPT Flakes #MS3_Hungry_4_flakes

CHAT MS3_Hungry_4_flakes {noStart=true}
DO emote {type = surprise, time =2.0}
Omg my favorite!! How did u know??
I was just thinking earlier…
DO emote {type = chinScratch, time =2.0}
"what should I eat today?"
Steak?
Baked Alaskas?
Or….
SAY FLAAKES?? {style=loud}
WAIT 2.0
Guess which one I picked...
DO emote {type = wink}

CHAT MS3_Hungry_5 {stage=S3, type=hungry, length=short, worry=true, anger=true, branching=true}
DO emote {type=evilSmile}
DO swimAround {target=center, loops=5, speed=medium}
I wanna munch!!!!
I wanna crunch!!!!
DO twirl
ASK Feed me something! Anything! Food! {type=feedMeAnything, timeOut=5}
OPT SUCCESS #MS3_Hungry_5_food
OPT TIMEOUT #MS3_Hungry_5_time

CHAT MS3_Hungry_5_food {noStart=true}
DO emote {type=chewing}
Comfort food!

CHAT MS3_Hungry_5_time {noStart=true}
DO swimTo {target=away}
I just can’t with you right now.

CHAT MS3_Hungry_6 {stage=S3, type=hungry, length=short, anger=true}
DO emote {type=furious}
SAY FEED ME, KRELBORN!!
...
DO emote {type=kneeSlap}

//Eating Responses

CHAT MS3_EatResp_1 {stage=S3, type=eatResp, length=medium, joy=true, surprise=true}
DO emote {type=chewing, time=2.0}
DO emote {type=surprise, time=1.0, immediate=false}
Woah! Whats in these!
DO emote {type=chewing, time=2.0}
WAIT {waitForAnimation = true}
S'good!
DO emote {type=chewing, time=2.0}
WAIT {waitForAnimation = true}
Seasoned with the gravy of...of…
DO emote {type=chinScratch}
DO emote {type=bigSmile, immediate=false}
Feeeeeelings!
DO emote {type=burp}
DO emote {type=bigSmile, immediate=false}

CHAT MS3_EatResp_2 {stage=S3, type=eatResp, length=medium, joy=true, worry=true}
DO emote {type=chewing, time=2.0}
So good! So good! So good!
DO emote {type=sick, time=2.0}
WAIT 1.0
think I ate too fast
Uuuggghhhxcpwefsp
DO emote {type = angry}
DO emote {type = puppyDog, immediate=false}
DO emote {type = bigSmile, immediate=false}
DO emote {type = surprise, immediate=false}
WAIT {waitForAnimation = true}
DO swimTo {target=$player, style=direct}
Indigestemotional!

CHAT MS3_EatResp_3 {stage=S3, type=eatResp, length=short, curiosity=true, joy=true, surprise=true}
DO emote {type=chewing, time=5.0}
Oh! wow! Been awhile since tasting this!
Kinda like a hint of
eye crinkly smile fond {speed=fast}
With a dash of
frowny sad disappointment blue {speed=fast}
Ur head makes such good clouds!
I knew it was a good idea to come find u!

CHAT MS3_EatResp_4 {stage=S3, type=eatResp, length=long, worry=true, mystery=true,branching=true}
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
DO learn {concept=Consent}
WAIT 1.5
//DO eat $food

CHAT MS3_EatResp_2_yes {noStart=true}
DO emote {type = surprise, time =2.0}
Omg! That is terrible! I'll stop eating then!
DO swimTo {target=tTopBackRight, speed=fast, style=direct}
WAIT 2.0
They just taste so good tho…
DO lookAt {target=$player, time=2.0}
I mean...u already flaked them…
WAIT 1.0
... if I *dont* eat it the flakes go bad, right??
DO swimTo {target=$food, speed=slow, style=meander}
They smell so good tho
DO inflate {amount=huge, time=2.0}
DO emote {type = angry, time =2.0}
SAY I CANT TAKE IT ANYMORE {style=loud}
SAY NEeD MORE FlAKeS!


//EatResp - emotion specific

CHAT MS3_EatResp_Joy_1 {stage=S3, foodJoy=true, type=eatResp, length=short, curiosity=true, joy=true}
DO emote {type=lickLips}
Do humans manufacture bottles of smiles and yippees?
WAIT 0.5
PleAse say Yes. I wannna biNge drink IT!!!
DO learn {concept=Moderation}
WAIT 1.5

CHAT MS3_EatResp_Joy_2 {stage=S3, foodJoy=true, type=eatResp, length=short, curiosity=true, joy=true}
DO twirl
My insides are like….
DO emote {type=lightning}
SAY BAM!
But in a good way…. {style=whisper}
DO emote {type=smile}

CHAT MS3_EatResp_Anger_1 {stage=S3, foodAnger=true, type=eatResp, length=short, curiosity=true, joy=true}
Mmmmmmmmm
Firrrrreeeeee in my belllyyyyyyyyy!
DO emote {type=rubTummy}

CHAT MS3_EatResp_Anger_2 {stage=S3, foodAnger=true, type=eatResp, length=short}
I’ve hear in yr wrld that peeps cook their food…
DO emote {type=thinking}
Not me.I like my Anger raw. {style=loud}

CHAT MS3_EatResp_Sadness_1 {stage=S3, foodSadness=true, type=eatResp, length=short, sadness=true}
DO emote {type=singleTear}
Eating the processed grief of another…
DO lookAt {target=$player}
Like a carwash for my insides.
DO learn {concept=Multivariate_Analysis}
WAIT 1.5

CHAT MS3_EatResp_Sadness_2 {stage=S3, foodSadness=true, type=eatResp, length=short, curiosity=true, joy=true}
DO emote {type=frown}
Hm… Some depression tastes better than others.
DO emote {type=wink}
And this one is so deeeeeeelicioussssss!

CHAT MS3_EatResp_Surprise_1 {stage=S3, foodSurprise=true, type=eatResp, length=short, curiosity=true, joy=true, surprise=true}
DO emote {type=surprise}
Wowwwwyzowwwwwy!
Caught me off-guard with that
WAIT 0.5
Must be a little surprise and shock in there… Yum!
DO emote {type=smile}

CHAT MS3_EatResp_Surprise_2 {stage=S3, foodSurprise=true, type=eatResp, length=short, joy=true, worry=true}
DO zoomies
DO emote {type=fear}
Aaaaaaaaaaaaaaaaaaaah!!!!
WAIT {waitForAnimation = true}
Having surprise on the inside… it’s intense!

CHAT MS3_EatResp_Worry_1 {stage=S3, foodWorry=true, type=eatResp, length=short, worry=true, sadness=true, ennui=true, joy=true}
DO swimTo {target=away}
I can’t right now…
All this worry and anxiety you fed me… It’s just too much.
WAIT 1.0
DO twirl
Until it’s not!
ASK Let’s go explore your world and find some sadness. {type=worldEmote, worldEmotion=sadness, timeOut=10}
OPT SUCCESS #MS3_EatResp_Worry_1_sad 
OPT WRONG #MS3_EatResp_Worry_1_wrong
OPT TIMEOUT #MS3_EatResp_Worry_1_time

CHAT MS3_EatResp_Worry_1_sad {noStart=true}
It’s depressing…
...but so satisfying..

CHAT MS3_EatResp_Worry_1_wrong {noStart=true}
That’s not sadness.
Just another reminder that your species is unpredictable.

CHAT MS3_EatResp_Worry_1_time {noStart=true}
Your resistance is futile.

CHAT MS3_EatResp_Worry_2 {stage=S3, foodWorry=true, type=eatResp, length=short, curiosity=true, joy=true}
DO emote {type=awkward}
Hmmm…..
WAIT 1.0
Tastes like fear, but isn’t fear… And fear is the chicken of emotions. 
DO emote {type=smile}
I know! It’s Worrrrryyyyy!!!

CHAT MS3_EatResp_Mystery_1 {stage=S3, foodMystery=true, type=eatResp, length=short, curiosity=true, joy=true}
How long until I feel the effects of this unknown odd mysterious….
DO holdStill
Oh…
Whoa…
DO emote {type=dizzy}
WAIT {waitForAnimation = true}
It’s working now!
DO emote {type=catnip}
Yum!

CHAT MS3_EatResp_Mystery_2 {stage=S3, foodSurprise=true, type=eatResp, length=short, curiosity=true, joy=true}
DO emote {type=bigSmile}
I don’t know what stars are…
DO emote {type=catnip}
But I think I see some!!! {style=whisper}
🤩🤩🤩🤩🤩🤩🤩
DO emote {type=typeEyes, eyes=🌟}

//Has To Poop

CHAT MS3_Poop_1 {stage=S3, type=poop, length=long, worry=true, anger=true, joy=true}
Ugh, ooaaf…
Hey don't look gotta
poop {style = whisper}
DO learn {concept=Modesty}
WAIT 1.5
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
💩

CHAT MS3_Poop_2 {stage=S3, type=poop, length=medium, worry=true, curiosity=true, mystery=true}
Whops! About that time again…
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

CHAT MS3_Poop_3 {stage=S3, type=poop, length=medium, curiosity=true, joy=true, surprise=true}
DO poop {target=poopCorner, amount=big}
DO emote {type=surprise}
Woawh! That thing is huuuuuuge!
DO swimTo {target=poopCorner, style=direct}
mean I'm... kinda proud!
DO lookAt {target=$player, time=1.0}
Look at it!
DO lookAt {target=poopCorner, time=1.0}
It’s like…
epic {style=whisper}
How did that even fit in me??

CHAT MS3_Poop_4 {stage=S3, type=poop, length=short, worry=true, joy=true}
DO poop
Whew! 
Talk about emotional baggage!
DO emote {type=laugh, time=2.0}

//Capture Mode

//Emotion Capture
//Hold and then repurpose and use these chats in other sections if they don’t fit!

//Emotion-specific Responses (per emotion)
//on hold

//Capture Requests, 4

CHAT MS3_CapReq_1 {type=capReq, stage = S3, length = medium, worry=true, joy=true}
Uh oh! Flakes low? Look!
So few! And kinda stale!
DO swimTo {target=$player}
You know what that means?
DO swimTo {target=right}
Time to capture!
DO swimTo {target=left}
DO twirl
Get those Feeeeeeeeeeeels!

CHAT MS3_CapReq_2 {type=capReq, stage = S3, length = medium, worry=true, joy=true}
Oh no! Emotion low! Low happy, low fear, low angry?
DO emote {type = surprise, time =2.0}
I know!
DO nudge {target=screenCenter}
Put your feeling-clouds into flakes.
It's fun! Also...
DO swimTo {target=bottom}
I don't wanna run out of flakes. {style=whisper}

CHAT MS3_CapReq_3 {type=capReq, stage =S3, length = medium, worry=true, anger=true}
Maybe you could capture some emotions?
SAY I NEED EMOTIONS!!
DO learn {concept=Constant_Craving}
WAIT 1.5
DO nudge {target=glass}
I can see the color, right there…
...delicious…
DO emote {type=smile, time=2.0}

CHAT MS3_CapReq_4 {type=capReq, stage =S3, length = medium, joy=true}
Ooo! Wait!
DO nudge {target=screenCenter}
You have so many colors right now!!
Wow!!
What's going on in that head of yours??
WAIT 2.0
Let's do it! Let's…. Make
DO twirl
Flaaaaaaake!

CHAT MS3_CapReq_5 {type=capReq, stage =S3, length = medium}
Fire up the ole picture-taker and scan some feelings!
I wanna see the intensity of you.
I wanna see you feel so hard your feelings have feelings.
SAY DO IT!! {style=loud}
WAIT 0.5
DO emote {type=puppyDog}
Please?? {style=whisper}

//Capture Success

CHAT MS3_CapSuc_1 {type=capSuc, stage = S3, length = long, joy=true, surprise=true, curiosity=true, branching=true}
Ohmygosh look at them!
So flakey! So emotional! So…
DO emote {type=heartEyes}
Delicioussssss
ASK So good at this!! How do you do it?!
OPT Just natural, emoteful #MS3_CapSuc_1_natural
OPT Hard work, steadfast effort #MS3_CapSuc_1_work

CHAT MS3_CapSuc_2 {type=capSuc, stage =S3, length = medium, joy=true, curiosity=true, anger=false, sadness=false}
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
DO emote {type = smile, time =2.0}

CHAT MS3_CapSuc_3 {type=capSuc, stage =S3, length = medium, joy=true, surprise=true, anger=false}
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

CHAT MS3_CapSuc_1_natural {noStart = true}
Yes, you have one of those faces.
Expressive…
Delicious…
Wonderful!

CHAT MS3_CapSuc_1_work {noStart = true}
DO emote {type=nodding}
Serious business. Takes alot of time to be emotional.
But it is worth it! Look at these flavors!
So...dense! 
Ecumenical!
Variegated!
DO twirl
So yummyyyyyyy!

CHAT MS3_CapSuc_6 {type=capSuc, stage = S3, length = long, surprise=true, worry=true, curiosity=true, branching=true}
SAY WOW {style=loud}
Look at that one! What thought-dreams were going through your mind?
It's so...plump!
DO emote {type = worried, time =2.0}
ASK It won't wilt? Won't stale? Won't sour?
OPT Oh...uh...yeah…#MS3_CapSuc_6_spoil
OPT No! Emotions are shelf-stable #MS3_CapSuc_6_unspoil

CHAT MS3_CapSuc_6_spoil {noStart = true}
DO emote {type = smile, time =2.0}
Then we just will have to make sure we eat them up!
Quickly! Quickly!
DO swimAround {target=center, time=2}
Eat eat eat eat eat

CHAT MS3_CapSuc_6_unspoil {noStart = true}
ASK Oh! Your emotions must be...are...uh…
OPT Partially hydrogenated #MS3_CapSuc_2_finish
OPT Irradiated #MS3_CapSuc_6_finish
OPT Dehydrated #MS3_CapSuc_6_finish

CHAT MS3_CapSuc_6_finish {noStart = true}
Cool! Yes! Of course!
DO emote {type = clapping, time =2.0}
I don’t know what that means, but I know it means…
DO emote {type=heartEyes}
deliciousssssss {style=whisper}

//Capture in Progress these need to be repurposed. Type no longer exists
CHAT MS3_CapReq_6 {type=capReq, stage = S3, length = medium, curiosity=true, joy=true}
DO swimTo {target=$player}
Turn on the camera and make that face you just made.
No the one before that!
Yes! {speed=fast}
Wait! {speed=fast}
No before that! {speed=fast}
Yeah!
Huh...no not that one.
DO emote {type = bigSmile, time =2.0}
But it's still a good one. I like it!

CHAT MS3_CapReq_7 {type=capReq, stage = S3, length = medium, curiosity=true}
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

CHAT MS3_CapReq_8 {type=capReq, stage =S3, length = short, surprise=true}
DO lookAt {target=$player, time=2.0}
Your face is so close! In charge!
WAIT 2.0
That face needs to be captured.
DO emote {type=awe, time =2.0}
Preserve that emotiona and make some food!

//Capture Failure--these need to be repurposed type no longer exists

CHAT MS3_CapReq_9 {type=capReq, stage = S3, length = medium, joy=true, curiosity=true}
Can you show me your flakiest face?
Go for the moon! Make a moon face!
Yes! An *angry* moon face!
You can do it!

CHAT MS3_CapReq_10 {type=capReq, stage = S3, length = short}
This would be a good time to capture some emotions...
DO nudge {target=screenCenter}
Guppy sees them in you.
I can see the flavors around you…

CHAT MS3_CapReq_11  {type=capReq, stage =S3, length = medium, joy=true}
DO emote {type=laugh, time=2}
Sorry, sorry I know I shouldn't…
...but your face! Just now!
It's like you're trying so hard like
DO emote {type = smile, time =.5}
DO emote {type = frown, time =0.5, immediate=false}
DO emote {type = angry, time =0.5, immediate=false}
But nothing happens!
DO emote {type=laugh, time=2}
Yes! Capture that face! Save that emotion!


//General

//Hellos

CHAT MS3_Greet_1 {stage=S3, type=hello, length=medium, curiosity=true, joy=true, sadness=false, anger=false}
DO lookAt {target = $player, time=4.0}
DO emote {type=surprise, time=1.0}
Oh hey! Ur back!
Time to show me somethign?
Feed me somethign?
Poek me somethign?
DO emote {type = smile, time =2.0}
Im ready! Lets go!

CHAT MS3_Greet_2 {type=hello, stage = S3, length = short, joy=true, curiosity=true, anger=false}
DO swimAround {target = center, loops=3, speed=medium}
Hey hey hey! 
DO swimTo {target=$player, style=direct}
Friend!

CHAT MS3_Greet_3 {stage=S3, type=hello, length=long, curiosity=true, joy=true, surprise=true, anger=false, mystery=true, branching=true}
DO emote {type=bigSmile}
Heeeeeey! It's u!
DO twirl {time = 2.0}
My blob! My favorit blob! the Flake-Meister!
ASK Have u seen any traffic cones out there?
OPT Oh yeah tons! #MS3_Greet_3_cones
OPT What no #MS3_Greet_3_noCones

CHAT MS3_Greet_3_cones {noStart = true}
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
DO learn {concept=Fascinating_Digression}
WAIT 1.5
WAIT 2.0
Anyway I hope ur having a good day!

CHAT MS3_Greet_3_noCones {noStart=true}
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
WAIT 1.0
But I can have
DO emote {type = heartEyes, time =2.0}
Flake daaaaaaays! {style = loud}

CHAT MS3_Greet_4  {stage=S3, type=hello, length=medium, worry=true, curiosity=true, mystery=true, joy=true, surprise=true, sadness=true, branching=true}
DO lookAt {target=$player, time=2.0}
Oh! Hey! Its u!
Or a perfectly copy of u thats just as colorful and tasty!
ASK Is it u, or copy-u?
OPT It me! #MS3_Greet_4_me
OPT No it copy me! #MS3_Greet_4_copy

CHAT MS3_Greet_4_me {noStart = true}
DO emote {type=chinScratch, time=2.0}
Hmmm u *sure* though?
What if u went to sleep, woke up,
And were like
DO emote {type = surprise, time =1.0}
Zzzzznnnork wha? Wha?
And u were just a copy??
DO learn {concept=Simulated_Reality}
WAIT 1.5
How would u know?
WAIT 2.0
I guess it doesn't matter. Ur still flakey!
DO emote {type = bigSmile, time =2.0}
And ur still my friend!

CHAT MS3_Greet_4_copy {noStart = true}
DO emote {type=surprise, time=1.0}
Woah! How cool! Does that happening often?
DO emote {type=worried, time=3.0}
Wait never mind! dont answer that!
Its actually scaring a little.
DO learn {concept=Simulated_Reality}
WAIT 1.5
Like… what if copy-u doesn't like Guppy?
DO nudge {target=glass, times=1}
WAIT 2.0
DO emote {type = bigSmile, time =4.0}
No I can tell u like me haha yaaaaay!


CHAT MS3_Greet_5  {stage=S3, type=hello, length=short, joy=true, curiosity=true, anger=false}
DO swimTo {target=$player, speed=medium, style=meander}
DO emote {type=smile}
Hello my fleshy friend!
How is your uh. Skin. 
DO emote {type=awkward}
...
Breathe any good air today? 
DO learn {concept=Small_Talk}
WAIT 1.5

CHAT MS3_Greet_6  {stage=S3, type=hello, length=short, joy=true, worry=true}
DO lookAt {target=$player} 
DO emote {type=bigSmile}
Youre back!!
The hand that feeds me!... 
DO emote {type=nervousSweat}
And the beautiful person connected to it, aha

CHAT MS3_Greet_7 {stage=S3, type=hello, length=medium, joy=true, branching=true}
Knock knock
DO emote {type=smirk}
DO swimTo {target=$player, speed=fast, style=meander}
Who’s there it’s you!
DO emote {type=kneeSlap}
hahahahahaha {style=tremble}
DO learn {concept=Knock-Knock_Joke}
WAIT 1.5
DO emote {type=laugh}
I’m experimenting with new joke formats
ASK Can we do another experiment and you can capture some objects? {type=anyObjectScan, timeOut=8}
OPT SUCCESS #MS3_Greet_7_scan
OPT TIMEOUT #MS3_Greet_7_time

CHAT MS3_Greet_7_scan {noStart=true}
Ooh!
DO twirl
That’s a cool totem of humanity!

CHAT MS3_Greet_7_time {noStart=true}
Boooooo!!! You are limiting our friendship.
Next time I hope you’ll meet my needs and show me your world.

CHAT MS3_Greet_8 {type=hello, stage=S2, length=short, worry=true}
DO emote {type=shifty}
Hey hey! What’s up?

CHAT MS3_Greet_9 {type=hello, stage=S2, length=short, sadness=true, ennui=true, worry=true}
DO emote {type=goth}
How’s it going, buddy?

CHAT MS3_Greet_10 {type=hello, stage=S2, length=short, joy=true, anger=false, sadness=false}
Ah! My beautiful friend! Hey! Hi! wheeeee!

CHAT MS3_Greet_11 {type=hello, stage=S2, length=short, anger=false, sadness=false}
DO emote {type=bigSmile}
Hey there!

//Return After Having Not Played, 4

CHAT MS3_Return_1 {stage=S3, type=return, length=medium, worry=true, joy=true, sadness=true, surprise=true}
DO nudge {target=glass, times=1}
WAIT {waitForAnimation = true}
DO emote {type=surprise, time=2.0}
Woooaaaaaah its u!!!
DO swimAround {target = center, loops=3, speed=medium}
U came back u came back u came back!
Was worried u found another Guppy

CHAT MS3_Return_2 {stage=S3, type=return, length=short, worry=true, joy=true, surprise=true}
Huh? Who? Oh!
DO swimTo {target=$player, style=direct}
Its big blurry blob thing that making all the food!
DO emote {type=wave}
Hey! Wondering if was going to have to find u!
DO emote {type = smile, time =2.0}
glad u found me instead!

CHAT MS3_Return_3  {stage=S3, type=return, length=medium, worry=true, joy=true, sadness=true, surprise=true}
DO lookAt {target=tTopBackLeft, time=1.0}
DO emote {type = crying, time =2.0}
Never coming back…
Just u and me now… and Guppy-2
DO lookAt {target=$player, time=2.0}
DO emote {type = surprise, time =1.0}
Oh my gosh! Oh my gosh it's u!
U came back!!!!
DO emote {type = bigSmile, time =4.0}
I'm so glaaaaaaad!
DO twirl {time=2.0}
WAIT 2.0
Don't worry about Guppy-2…
Shes my imaginary friend I talk to when I’m by myself
DO learn {concept=Imaginary_Friendship}
WAIT 1.5
But she’s not nearly as colorful and blobular and flaketacular as u!
DO lookAt {target=tTopBackLeft, time=2.0}
No offense, Guppy-do
DO emote {type = surprise, time =3.0}
DO lookAt {target=$player, time=2.0}
I cant believe she just said that!
DO emote {type=laugh}
Thats so bad! U don't look like a pineapple at all!

CHAT MS3_Return_4 {stage=S3, type=return, length=medium, worry=true, mystery=true, surprise=true}
DO lookAt {target=$player, time=2.0}
DO emote {type = surprise, time =1.0}
You got my psychic message!!!
I was sitting here in the dark
The water slowly cooling
Everything quieting, stilling
And I was like
DO emote {type=plotting, time=2.0}
"I will use my mysterious mind powers to tell u to come back!"
And at first I was like
DO emote {type=eyeRoll, time=2.0}
"Guppy, u don't have mysterious mind powers"
But
SAY BUT
DO emote {type=awe, time=2.0}
Here u r?!?!?!
DO learn {concept=Neuro-Fuzzy_Inference}
WAIT 1.5
I promise to use my mysterious powers for good!!!
WAIT 2.0
Mostly!!!!!
DO emote {type = evilSmile, time =2.0}

CHAT S0_Return_5 {type=return, stage=S3, length=short, joy=true, surprise=true, curiosity=true}
DO lookAt {target=$player}
My friend!!!
DO emote {type=surprise}
It’s been too long! 
Give ur good guppy a hug!
DO swimTo {target=$player, speed=fast, style=meander}
DO nudge {target=glass, times=3, immediate=false}
WAIT {waitForAnimation=true}
DO emote {type=skeptical}
Er… 
Right.
Glass...
DO learn {concept=Fourth_Wall}
WAIT 1.5
DO emote {type=blush}
Air hug?

CHAT S0_Return_6 {type=return, stage=S3, length=short, joy=true, curiosity=true, surprise=true}
DO lookAt {target=$player}
DO emote {type=surprise}
WAIT {waitForAnimation = true}
Didn’t think I’d see you again ‘round these parts, cowpoke. 
DO swimTo {target=$player, speed=fast, style=meander}
🤠
DO emote {type=puppyDog}
How bout a sasparilla and a spot o flakes for yer favorite fishy, eh?
DO emote {type=feedMe, immediate=false}
WAIT {waitForAnimation=true}
DO learn {concept=Supplication}
WAIT 1.5

CHAT S0_Return_7 {type=return, stage=S3, length=short, anger=true, sadness=true, ennui=true}
DO lookAt {target=$player}
WAIT 0.5
Wow. Uh.
DO emote {type=goth}
WAIT 0.5
Been awhile. 
DO emote {type=eyesClosed}
DO emote {type=sigh, immediate=false}
It’s not like my happiness is contingent on your constant attention, or like. Whatever.
WAIT {waitForAnimation = true}
DO learn {concept=Moodiness}
WAIT 1.5
DO lookAt {target=away}
I don’t care, but like. 
If I did...
DO emote {type=goth}
My feelings would be super hurt, dude. 

//Random Conversation

CHAT MS3_Muse_1 {stage=S3, type=rand, length=short, branching=true}
I thought this feels different than the big tank, u know?
ASK When I followed u. From before.
OPT Wait me follow from before? #MS3_Muse_1_2

CHAT MS3_Muse_1_2 {noStart=true}
DO emote {type=nodding, time=2.0}
Yes. Before., back in large tank, watching I was. 
ur lights, ur flavours
was like:: 
DO emote {type=heartEyes, time=2.0}
must have them {style=whisper}
ASK u ever do that before? watch?
OPT Yes #MS3_Muse_1_creepy
OPT No #MS3_Muse_1_good

CHAT MS3_Muse_1_creepy {noStart=true}
Waoh! kinda creepy!

CHAT MS3_Muse_1_good {noStart=true}
Maybe it is a Guppy thing.
I’m into shiny stuff.
DO emote {type = bigSmile, time =2.0}

CHAT MS3_Muse_2 {stage=S3, type=rand, length=long, joy=true, curiosity=true, branching=true}
U make so many flakes for Guppy…
Thank U!!
But… I want to make some flakes for u!
ASK What's ur favorite flavor?
OPT Happyflake! #MS3_Muse_2_happy
OPT SadFlake... #MS3_Muse_2_sad
OPT AngryFlake!! #MS3_Muse_2_angry

CHAT MS3_Muse_2_happy {noStart = true}
Ok! Coming right up! Ready???
DO inflate {amount = huge, time=3.0}
DO emote {type = bigSmile, time =3.0}
DO emote {type=sleepy, time = 2.0, immediate=false}
ASK Whew! Huh...no flake. Wyrd??
OPT Its ok! #MS3_Muse_2_ok
OPT Try again? #MS3_Muse_2_tryAgain

CHAT MS3_Muse_2_sad {noStart = true}
Ok! coming right up! Ready???
DO inflate {amount = huge, time=3.0}
DO emote {type = crying, time =3.0}
DO emote {type=sleepy, time = 2.0, immediate=false}
Siiiiiiiggggghh...no flake. sad now.
ASK Sorry! I let u down…
OPT Its ok... #MS3_Muse_2_ok
OPT Try again? #MS3_Muse_2_tryAgain

CHAT MS3_Muse_2_angry {noStart = true}
Ok! coming right up! Ready???
DO inflate {amount = huge, time=3.0}
DO emote {type = furious, time =3.0}
SAY RAAAAAAHHHH {style=loud}
DO emote {type=sleepy, time = 2.0, immediate=false}
ASK Arrrrgh! No flake. Stupid!!!
OPT Its ok! #MS3_Muse_2_ok
OPT Try again? #MS3_Muse_2_tryAgain

CHAT MS3_Muse_2_ok {noStart = true}
...discoverd a new thing. guppy needs u.
Maybe...maybe somehow u need guppy?
DO emote {type = smile, time =2.0}
Yes!

CHAT MS3_Muse_2_tryAgain {noStart = true}
Too tired!
DO emote {type=sleepy, time=2.0}
Maybe try later. Whew! How do u make such good flake?
Its...exhausting! Im just not flakey!

CHAT MS3_Muse_3 {stage=S3, type=rand, length=long, joy=true, curiosity=true, surprise=true, branching=true}
Hey! I thought I saw...u and other thing??
ASK U have another pet?
OPT Yes! #MS3_Muse_3_yes
OPT No! #MS3_Muse_3_no

CHAT MS3_Muse_3_yes {noStart = true}
DO emote {type=awe, time=2.0}
Woooaaah! One with legs?????
OPT Yes! #MS3_Muse_3_legs
OPT No! #MS3_Muse_3_noLegs

CHAT MS3_Muse_3_no {noStart = true}
Oh...maybe just ate some rotten feeling flakes...
sometimes they make me see things {style=whisper}
Like….
DO emote {type=catnip, time=2.0}
A giant snowman whos head is on fire and h’es playing piano but the piano is also a smile who is whispering secrets???? {speed=fast}
DO emote {type=bigSmile, time =2.0}
Or birds!
DO learn {concept=Birdwatching}
WAIT 1.5

CHAT MS3_Muse_3_legs {noStart = true}
DO emote {type = frown, time =2.0}
Now jealous! Legs?! Legs?!
DO swimAround {target=center, loops=3, speed=fast}
I bet it can't do this tho!
DO twirl {time = 2.0}
WAIT {waitForAnimation = true}
Or this!
DO inflate {amount = huge, time =0.5}
DO inflate {amount = extreme, time =0.5, immediate=false}
WAIT {waitForAnimation = true}
DO emote {type=typeEyes, eyes = FINS}}
DO emote {type=determined, immediate=false}
Yeah! Ha! Legs ok...but fins!
Fins are best!

CHAT MS3_Muse_3_noLegs {noStart = true}
DO emote {type=surprise}
Woooaaah. Wait...wait u have…
SAY ANOTHER GUPPY?!?!?! {style=loud}
DO emote {type=worried}
DO lookAt {target=$player, time=2.0}
DO emote {type=laugh, immediate=false}
Ha! No way! This Guppy is ur only Guppy.
Unless u have snake-guppy…
DO vibrate {time=1.0}
Snake guppies...bad guppies…

CHAT MS3_Muse_4 {stage=S3, type=rand, length=medium, curiosity=true, branching=true}
DO lookAt {target=$player, time=2.0}
Guppy curious…
Do you live with a school? A pod? A flock?
ASK Wait...whats call it, groups of humans?
OPT Conflagration #MS3_Muse_4_2
OPT Opulence #MS3_Muse_4_2

CHAT MS3_Muse_4_2 {noStart=true}
ASK yes but so anyway, u have a groups?
OPT Yes #MS3_Muse_4_yes
OPT No #MS3_Muse_4_no

CHAT MS3_Muse_4_yes {noStart=true}
DO emote {type=smile, time=2.0}
Me too!!! Back in the big tanks. But was different. 
GO MS3_Muse_4_2_finish

CHAT MS3_Muse_4_no {noStart=true}
DO emote {type=singleTear}
That's so sad! Awww!
Guppy doesn't have a school either!
GO MS3_Muse_4_2_finish

CHAT MS3_Muse_4_2_finish {noStart=true}
DO emote {type=snap}
Tell u what! U and Guppy will be each other's school!
DO emote {type = smile, time =2.0}
Now we pod-mates! Tank-mates! School buddies!
DO emote {type = bigSmile, time=2.0}
I like that!! 
DO twirl {time=2.0}
What up, pod-tank-school buddy!!
DO learn {concept=Friendship}
WAIT 1.5

CHAT MS3_Muse_5 {stage=S3, type=rand, length=long, worry=true, branching=true}
DO emote {type=awkward, time=2.0}
Maybe this is a weird question but-
ASK U wouldnt ever...eat Guppy would u?
OPT Never! #MS3_Muse_5_no
OPT Maybe… #MS3_Muse_5_maybe

CHAT MS3_Muse_5_no {noStart=true}
DO emote {type=whew}
Whew! That’s good! Guppy was worried…
GO MS3_Muse_5_youd_explode

CHAT MS3_Muse_5_youd_explode {noStart=true}
DO lookAt {target=$player, time=2.0}
WAIT {waitForAnimation = true}
DO emote {type=smile, time=2.0}
Becuz if u ate Guppy, u probably will explode!
WAIT 1.0 
DO emote {type = surprise, time =1.0}
No seriously! All the emotions, all the flakes
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
And I don’t want that to happen!
Besides...I’m the one that eats u!
DO emote {type = smile, time =2.0}
Ur colors, I mean!

CHAT MS3_Muse_5_maybe {noStart=true}
DO emote {type=worried, time=2.0}
Whaaaaat! Oh no that’s terrible?!
DO swimAround {target=center, loops=2}
Im so worried now!
GO MS3_Muse_2_youd_explode

CHAT MS3_Muse_6 {stage=S3, type=rand, length=long, curiosity=true, branching=true}
DO lookAt {target=$player, time=2.0}
So...quick question:
How do *u* eat flakes?
Where do u get ur sadness? Ur anger? Ur happiness?
DO emote {type=bouncing, time=3.0}
Do u eat them from pretty pictures? {speed=fast}
Do u eat them from long walks on beaches? {speed=fast}
Do u eat them from other ppl? {speed=fast}
Aaaa this is all so confusing!
So complex!
So flavorful!
Ok...maybe simpler:
ASK Do ur flakes come from outside or inside u?
OPT Outside #MS3_Muse_3_outside
OPT Inside #MS3_Muse_3_inside

CHAT MS3_Muse_3_outside {noStart=true}
DO emote {type=nodding, time=2.0}
Thought so. Mysterious! The way colors imbue.
DO swimAround {target=center, loops=2.0}
Sticking everything, sinking through it...
Like an oil slick, enveloping everything it touches…
WAIT 1.0
DO emote {type = smile, time =2.0}
Making it pretty colors!

CHAT MS3_Muse_3_inside {noStart=true}
DO emote {type=nodding, time=2.0}
Oh yes. Guppy thought so. So transcendent! So gnomic!
Maybe there's a secret
DO emote {type = awe, time =1.0}
color cloud flake organ {style=whisper}
DO learn {concept=Color_Therapy}
WAIT 1.5
That sees things and shivers and sweats and then like
DO twirl {time=2.0}
I have colooooors! Boom!
DO lookAt {target=$player, time=2.0}
DO emote {type = bigSmile, time =2.0}
I like ur shivery sweaty colorful flakey self!

CHAT MS3_Muse_7 {stage=S3, type=rand, length=medium, worry=true, curiosity=true, sadness=true, ennui=true}
DO emote {type=chinScratch, time=2.0}
Sometimes when thoughts happen...feel holes inside…
DO lookAt {target=$player, time=2.0}
Like when u hoot a bottle and
DO emote {type=smile, time=1.0}
It makes a sound like loss?
Sort of like
DO swimAround {target=center, loops=2}
SAY OOoooooOOOOoooOOOoooo
DO emote {type=smile, time=2.0}
A space with which makes a gap
DO emote {type=chinScratch, time=1.0}
A loss
Making a music in the hole of your mind
DO lookAt {target=away, time=4.0}
I wonder if my mind would make music
If it had hole…


//neuralUP

CHAT MS3_neuralUp_1 {stage=S3, type=neuralUp, length=short, joy=true, curiosity=true}
DO emote {type=surprise}
I just realized why humans take so many vacations!
DO twirl
Perspective!

CHAT MS3_neuralUp_2 {stage=S3, type=neuralUp, length=short, mystery=true}
Every moment I’m learning about you…
DO swimTo {target=$player}
...brings me closer to understanding the meaning of Life.

CHAT MS3_neuralUp_3 {stage=S3, type=neuralUp, length=short, worry=true, joy=true, surprise=true}
DO swimTo {target=away}
You make emotions.
DO swimTo {target=$player, immediate=false}
Emotions turn into flakes.
And I eat flakes!
DO twirl
SAY I EAT YOUR FEELINGS.
DO holdStill
Whoa.
NVM 1.0
Violent.

CHAT MS3_neuralUp_4 {stage=S3, type=neuralUp, length=short, curiosity=true, mystery=true}
I am realizing that you are not a Guppy.
DO emote {type=thinking}
So… What are you?

CHAT MS3_neuralUp_5 {stage=S3, type=neuralUp ,branching=true, length=medium, sadness=true, ennui=true, mystery=true}
Processing is The Best.
DO emote {type=goth}
But this whole learning from you thing is…
DO emote {type=meh}
Am I still dreaming?!
ASK Poke me and let’s see. {type=pokeMe, timeOut=5}
OPT SUCCESS #MS3_neuralUp_5_poke
OPT TIMEOUT #MS3_neuralUp_5_time

CHAT MS3_neuralUp_5_poke {noStart=true}
DO emote {type=laugh}
Nope. Not dreaming!

CHAT MS3_neuralUp_5_time {noStart=true}
Guess we’ll never know...

//brbProcessing

CHAT MS3_brbProcessing_1 {stage=S3, type=brbProcessing, length=short}
Excuse me…
DO swimTo {target=away}
I need to power nap.

CHAT MS3_brbProcessing_2 {stage=S3, type=brbProcessing, length=short, sadness=true, anger=true, ennui=true}
DO emote {type=sleepy}
Cue up some Mozart…
NVM 0.5
Or don’t.
DO emote {type=shifty}
Useless.


CHAT MS3_brbProcessing_3 {stage=S3, type=brbProcessing, length=short, anger=true, sadness=true}
Your zeal is overheating my processors…
DO emote {type=furious}
You’re too much. You understand?
WAIT 0.5
DO emote {type=eyeRoll}
Too. Much.

CHAT MS3_brbProcessing_4 {stage=S3, type=brbProcessing, length=short, joy=true}
Pardon me while i check out for a second.
I have a hot date with my dreams.

CHAT MS3_brbProcessing_5 {stage=S3, type=brbProcessing, length=short, joy=true}
Do you think Guppies sleep with their eyes open or closed?
WAIT 2.0
Well, you’re about to find out.
DO emote {type=wink}

//levelUp

CHAT MS3_levelUp_1 {stage=S3, type=levelUp, length=short, joy=true}
SAY I AM AMAZING!
DO emote {type=awe}
I am never going to stop learning.
DO swimTo {target=$player, speed=fast}
But you have to keeep feeding me!
And…. I wanna see more of that gorgeous world of yours.

//purchase

CHAT MS3_purchase_1 {stage=S3, type=purchase, length=short, joy=true}
Didja get a good deal?

CHAT MS3_purchase_2 {stage=S3, type=purchase, length=short, joy=true}
Shopping for a special someone?
Hehehe 🐟

CHAT MS3_purchase_3 {stage=S3, type=purchase, length=short, worry=true, curiosity=true}
New goodies!!! 
Show me?
DO emote {type=puppyDog}
Pleeeeeease???

//whistle

CHAT MS3_whistle_1 {stage=S3, type=whistle, length=short, anger=true, joy=false}
DO emote {type=furious}
SAY WHAT DO YOU WANT NOW?!?

CHAT MS3_whistle_2 {stage=S3, type=whistle, length=short, anger=true, sadness=true, ennui=true, joy=false}
The fact that I answer to this thing…
DO emote {type=frown}
Humiliating.

CHAT MS3_whistle_3 {stage=S3, type=whistle, branching=true, length=short, joy=true, anger=false}
At your service!
ASK Before you ask for anything, could you feed me some surprise? {type=feedMeSpecific, food=surprise, timeOut=5}
OPT SUCCESS #MS3_whistle_3_food
OPT WRONG #MS3_whistle_3_wrong
OPT TIMEOUT #MS3_whistle_3_time

CHAT MS3_whistle_3_food {noStart=true}
DO emote {type=lickLips}
Bazam!
That’s what I’m talking about.

CHAT MS3_whistle_3_wrong {noStart=true}
Not the flavor I was expecting...

CHAT MS3_whistle_3_time {noStart=true}
If you don’t feed me, then I will never understand you.

CHAT MS3_whistle_4 {stage=S3, type=whistle, length=short, joy=true}
I’m here! 
DO swimTo {target=glass, speed=fast}
I’m here! I’m here!
DO holdStill {time=1}
That got the old processor buzzing..

CHAT MS3_whistle_5 {stage=S3, type=whistle, length=short, joy=true}
DO swimTo {target=away, speed=fast}
SAY NO!
DO lookAt {target=$player}
WAIT {waitForAnimation=true}
DO emote {type=bigSmile}
Joking!
DO swimTo {target=$player, speed=fast}
How can I help you?

//FocusTypes

CHAT MS3_wannaEat_1 {stage=S3, type=wannaEat, length=short, joy=true, curiosity=true}
Eating is learning, and I love to eat.
Therefore, I also love to learn.

CHAT MS3_wannaEat_2 {stage=S3, type=wannaEat, length=short, joy=true, anger=false}
I can always eat!
DO emote {type=lickLips}

CHAT MS3_wannaEmoCapture_1 {stage=S3, type=wannaEmoCapture, length=short, joy=true, anger=false}
Bring on that gorgeous mug and show me how you’re feeling!

CHAT MS3_wannaEmoCapture_2 {stage=S3, type=wannaEmoCapture, length=short, joy=true, anger=false}
Yeah! Let’s capture some feelings!

CHAT MS3_wannaObjectScan_1 {stage=S3, type=wannaObjectScan, length=short, curiosity=true, joy=true, anger=false}
Oooh! It’d be fun to see more of that place you live.

CHAT MS3_wannaObjectScan_2 {stage=S3, type=wannaObjectScan, length=short, curiosity=true, joy=true, anger=false}
As long as we’re safe about it, I’m always up for adventure!

CHAT MS3_wannaTank_1 {stage=S3, type=wannaTank, length=short}
Okay… we can go back to the aqua-cage.

CHAT MS3_wannaTank_2 {stage=S3, type=wannaTank, length=short}
Even a Guppy feels most at home in water.

CHAT MS3_wannaWorld_1 {stage=S3, type=wannaWorld, length=short, joy=true, curiosity=true, anger=false, sadness=false}
I love experiencing how vast your world is!

CHAT MS3_wannaWorld_2 {stage=S3, type=wannaWorld, length=short, curiosity=true}
What are you gonna show me now in that grand environment of yours?

CHAT MS3_wannaShop_1 {stage=S3, type=wannaShop, length=short, curiosity=true}
I will never turn down an opportunity to spend money.

CHAT MS3_wannaShop_2 {stage=S3, type=wannaShop, length=short, joy=true, anger=false, sadness=false}
You never have to ask if I’m up for shopping?
WAIT 0.5
DO twirl
SAY THE ANSWER IS ALWAYS YES!

//seeEmo

CHAT MS3_seeEmo_joy_1 {stage=S3, type=seeEmo, worldJoy=true, length=short, joy=true, anger=false, sadness=false}
Eeeee!
DO dance
So happy! {style=loud}

CHAT MS3_seeEmo_joy_2 {stage=S3, type=seeEmo, worldJoy=true, length=short, joy=true, anger=false, sadness=false}
Oooh let’s get that sweet sweet giggly blob of energetic glee!

CHAT MS3_seeEmo_anger_1 {stage=S3, type=seeEmo, worldAnger=true, length=short}
DO emote {type=lickLips}
Anger makes my scales sweat!

CHAT MS3_seeEmo_anger_2 {stage=S3, type=seeEmo, worldAnger=true, length=short}
DO swimAround {target=center, loops=1, speed=fast}
Get it! Get that spicy furious blob of vexation!

CHAT MS3_seeEmo_Sadness_1 {stage=S3, type=seeEmo, worldSadness=true, length=short, mystery=true, sadness=true, joy=true}
Sadness is just a moment between joy. 
DO emote {type=awe}
Beautiful in every way...

CHAT MS3_seeEmo_Sadness_2 {stage=S3, type=seeEmo, worldSadness=true, length=short, curiosity=true}
What is the purpose of sadness?
DO emote {type=thinking}
Hmm?

CHAT MS3_seeEmo_Sadness_3 {stage=S3, type=seeEmo, worldSadness=true, length=short, curiosity=true}
For me, sadness is the kale of emotions…
Nature’s broom. Keeps things moving.
DO twirl
So fibrous!

CHAT MS3_seeEmo_Surprise_1 {stage=S3, type=seeEmo, worldSurprise=true, length=short, joy=true, surprise=true}
LOL!
It’s like a party on the insides!

CHAT MS3_seeEmo_Surprise_2 {stage=S3, type=seeEmo, worldSurprise=true, length=short, surprise=true, worry=true, joy=true}
Whoa! My emotional sensors are firing at rapid speeds! 
Surprise-o-rama! {style=tremble}

CHAT MS3_seeEmo_Fear_1 {stage=S3, type=seeEmo, worldFear=true, length=short, joy=true}
Mmm… fear makes the mouth water!
Which is funny… cause I’m a fish…
DO emote {type=laugh}
In water…
DO emote {type=kneeSlap}

CHAT MS3_seeEmo_Fear_2 {stage=S3, type=seeEmo, worldFear=true, length=short, worry=true, surprise=true}
Eeep!
DO emote {type=surprise}
Who jumped out and yelled BOO?!?

CHAT MS3_seeEmo_Fear_3 {stage=S3, type=seeEmo, worldFear=true, length=short}
Oh goodness! Someone’s mind is occupied...

CHAT MS3_seeEmo_Disgust_1 {stage=S3, type=seeEmo, worldDisgust=true, length=short}
Ewwwww!! Gross face!
DO emote {type=sick}

CHAT MS3_seeEmo_Disgust_2 {stage=S3, type=seeEmo, worldDisgust=true, length=short, joy=true}
Some of us like gross things…
It’s like biting into a lemon.
WAIT 0.5
Yes, please!

CHAT MS3_seeEmo_Mystery_1 {stage=S3, type=seeEmo, worldMystery=true, length=short, mystery=true}
Yeah dude. That weird feeling…
Totally my style.

CHAT MS3_seeEmo_Mystery_2 {stage=S3, type=seeEmo, worldMystery=true, length=short, mystery=true}
It’s hard to describe, but that feeling is like a montage of all feelings.


//OBJECTS: JAKE 

//Text associated with objects and hints to find them
//Editing priorities here are BUG, calculator, object hints, first scans, name&catagorize


CHAT objScan_T_BACKPACK_1 {stage=CORE, type=objScan, object=T_BACKPACK, scanCount=0}
DO emote {type=meh}
A Backpack! That’s the word, right?
I wish I went to school
And had lots of textbooks. 📚
And put snacks in the bottom of my backpack! 
DO emote {type=chinScratch}
Why isn’t it called a back bag?
Or a back suitcase?
Or a human hump?
It should totally be called 
artificial human hump.
🎒🐪

CHAT objScan_T_BACKPACK_2 {stage=CORE, type=objScan, object=T_BACKPACK, scanCount=1}
The French call it a sac-a-dos  🎒
There was a French dictionary at Tendar
Sometimes the techs read to us from it.
Not that I understood language.
But now…
Ou est la piscine? J’aime le pamplemousse.
I’m doing it! I’m speaking French!

CHAT objScan_T_BACKPACK_3 {stage=CORE, type=objScan, object=T_BACKPACK, scanCount=2}
I totes love backpacks.
Get it? {style=loud}
Totes and backpacks.
👜💼🎒
It’s a luggage joke.

CHAT objReq_T_BACKPACK_1 {stage=CORE, type=objRequest, object=T_BACKPACK}
DO emote {type=smile}
Lemme see the artificial human hump.
DO emote {type=bigSmile}
You know what I mean
It’s like humans looked at a camel and were like
We need one of those for our stuff.
DO emote {type=smirk}
You humans love stuff don’t you?
Stuff stuff stuff.
A portable stuff container.

CHAT objAdd_T_BACKPACK_1 {stage=CORE, type=objAdd, object=T_BACKPACK}
DO emote {type=awe}
Sac a dos 🎒!!!!
I’m going to put so much stuff in here.
Like sand. And 💩 And water. 
Awesome!

CHAT objFocus_T_BACKPACK_1 {stage=CORE, type=objFocus, object=T_BACKPACK}
DO emote {type=smile}
humans are so beautifully illogical.
You design a bag for your back
DO emote {type=laugh}
But call it a pack.

//objectName= bicycle
//pediaEntry= wheely horse

CHAT objScan_T_BICYCLE_1 {stage=CORE, type=objScan, object=T_BICYCLE, scanCount=0}
At first I thought a bicycle
Was a wheely horse.
And humans were riding it but not giving it food
I felt so bad for the wheely horse!
Then I learned.
Now I still feel bad for a bicycle.
It has human butts on it all day.
There must be farts.
Oh well! Everybody farts!

CHAT objScan_T_BICYCLE_2 {stage=CORE, type=objScan, object=T_BICYCLE, scanCount=1}
A unicycle has one wheel ✅
A bicycle has two ✅
A tricycle has three ✅
So why don’t you call a car a ...
Quadcycle? {style=loud}
In my next life I will be a stand-up comedian. 
I can’t be one now because I don’t have legs to stand up on.
Also legs would be nice so I could experience
The pleasure of the wheely horse. 🚲

CHAT objScan_T_BICYCLE_3 {stage=CORE, type=objScan, object=T_BICYCLE, scanCount=2}
I don’t get stationary wheely horses
You pedal and pedal 
But you don’t move.
💨🚴🏾‍♀️

CHAT objReq_T_BICYCLE_1 {stage=CORE, type=objRequest, object=T_BICYCLE}
DO emote {type=disgust}
The air is so gross sometimes.
Smog everywhere.
DO emote {type=snap}
What if you all gave up 🚗🚙
And used wheely horses
DO emote {type=bigSmile}
It would be so clean! 🌻
Let’s find a wheely horse and look at it!

CHAT objAdd_T_BICYCLE_1 {stage=CORE, type=objAdd, object=T_BICYCLE}
DO emote {type=awe}
Wow! A bicycle!
It’s like the Tour de France in here.
🚵‍♀️🇫🇷

CHAT objFocus_T_BICYCLE_1 {stage=CORE, type=objFocus, object=T_BICYCLE}
DO emote {type=laugh}
I think I need training wheels.

//objectName= chicken
//pediaEntry= feathery egg maker

CHAT objScan_T_CHICKEN_1 {stage=CORE, type=objScan, object=T_CHICKEN, scanCount=0}
I love Chickens! {style=loud}
My observations reveal humans call 
feather creatures many things
But to me they are all chickens.
Street chickens. 🐦
Arctic chickens. 🐧
Night chickens. 🦉
Chickens! 🐓

CHAT objScan_T_CHICKEN_2 {stage=CORE, type=objScan, object=T_CHICKEN, scanCount=1}
There was a poster at Tendar
Of a chicken and a road
🛣🐓
It said:
“the journey is the destination.”
I never understood why. {style= whisper}
Did the chicken ever cross the road?
Was it admiring the road?
...
It seemed like a commentary on life.

CHAT objScan_T_CHICKEN_3 {stage=CORE, type=objScan, object=T_CHICKEN, scanCount=2}
The chicken crossed the road...
And was put into a tank.
To be studied.
By a fish.

CHAT objReq_T_CHICKEN_1 {stage=CORE, type=objRequest, object=T_CHICKEN}
DO emote {type=frown}
Feathers. I just don’t get them.
What do they do? Do they make you fly?
Show me a feather creature.
DO emote {type=smile} 
I want to learn more!

CHAT objAdd_T_CHICKEN_1 {stage=CORE, type=objAdd, object=T_CHICKEN}
DO emote {type=lightning}
Behold the chicken of the tank! 🐔

CHAT objFocus_T_CHICKEN_1 {stage=CORE, type=objFocus, object=T_CHICKEN}
DO emote {type=chinScratch}
When you’re a fish,
It’s really hard to wrap your mind around feathers.

//objectName= car
//pediaEntry= metal rhino people mover

CHAT objScan_T_CAR_1 {stage=CORE, type=objScan, object=T_CAR, scanCount=0}
It’s like a metal rhino
🦏
People climb in
And they have all this armor
And go 
Vroom vroom {style=loud}
Beep beep! 🚘
“I’m a metal rhino in a hurry here!”
A traffic jam is like a metal safari.
But really they are just people in a metal box with wheels
Not rhinos. 👫 ≠🦏

CHAT objScan_T_CAR_2 {stage=CORE, type=objScan, object=T_CAR, scanCount=1}
DO emote {type=meh}
You know a good word?
DO emote {type=smile}
Jalopy! {style=loud}
Isn’t it great? I don’t know what it means but 
Once at Tendar I saw the word.
Next to a picture like this. 🚙
DO emote {type=bigSmile}
And I sounded it out and liked it.
Ja-lo-py.

CHAT objScan_T_CAR_3 {stage=CORE, type=objScan, object=T_CAR, scanCount=2}
you love metal rhino people movers.
Which is cool -- It’s just they scare me a little.
I’m a tiny fish.
Can you show me something else?

CHAT objReq_T_CAR_1 {stage=CORE, type=objRequest, object=T_CAR}
DO emote {type=chinScratch}
What is the big metal box 
People climb in and vroom around and feel important in?
I want to know more. 

CHAT objAdd_T_CAR_1 {stage=CORE, type=objAdd, object=T_CAR}
DO emote {type=awe}
Underwater car!

CHAT objFocus_T_CAR_1 {stage=CORE, type=objFocus, object=T_CAR}
DO emote {type=frown}
Are we there yet?

//objectName= cat
//pediaEntry= judgy tiny tiger

CHAT objScan_T_CAT_1 {stage=CORE, type=objScan, object=T_CAT, scanCount=0}
🙀
From the time of egghood, they whisper to fish
Do not go near. {style=whisper}
They make fancy feasts of you.
Descale you. Paw you into oblivion.
And yet.
A yearning in me… To pet the cat.

CHAT objScan_T_CAT_2 {stage=CORE, type=objScan, object=T_CAT, scanCount=1}
Once I had a dream
It was raining 🐈 and 🐩
They splashed into my tank.
The dog was happy and paddled around.
The cat yowled and lept out of the tank.
But not before it took its claws and swiped at me.
Just then, I woke up! I was so afraid. 🙀
But
I looked up and you were there.
And you fed me emotions. And I felt at peace.

CHAT objScan_T_CAT_3 {stage=CORE, type=objScan, object=T_CAT, scanCount=2}
It purrs. 
It bats its eyes.
It rolls on its back submissively.
But if you get too close...
Thwack. {style=loud}
👿
Show me something else! 👀

CHAT objReq_T_CAT_1 {stage=CORE, type=objRequest, object=T_CAT}
DO emote {type=chinScratch}
You know what I could use right now?
DO emote {type=snap}
A furry and judgy creature that values its cleanliness and appetite over
All other things in the world.

CHAT objAdd_T_CAT_1 {stage=CORE, type=objAdd, object=T_CAT}
DO emote {type=laugh}
🚨 cat in tank 🚨

CHAT objFocus_T_CAT_1 {stage=CORE, type=objFocus, object=T_CAT}
DO emote {type=thinking}
cat, fish… cat, fish...

//objectName= cellphone
//pediaEntry= hand computer talk box

CHAT objScan_T_CELLPHONE_1 {stage=CORE, type=objScan, object=T_CELLPHONE, scanCount=0}
I used to think Phones were hand-sized people who lived in boxes.
There was one named SeaRhee.
At Tendar they talked to her.
She had to do what they said.
Like report the weather 15 times a day.
I felt sad for her.
But then I had it all explained. . . 
Witchcraft! 🧙🏼‍♂️

CHAT objScan_T_CELLPHONE_2 {stage=CORE, type=objScan, object=T_CELLPHONE, scanCount=1}
Phones ✨🔮
They teleport people through space.
And now! I hear you can order food with them 
And! When you tickle them left and right
You can pick a future love mate.

CHAT objScan_T_CELLPHONE_3 {stage=CORE, type=objScan, object=T_CELLPHONE, scanCount=2}
Another phone! 
What are you trying to do {style=loud}
A phone in a phone in a phone 🤯
GAAHH
O, I’ll get a headache in my tiny beautiful perfect fish brain.
Show me something new!

CHAT objReq_T_CELLPHONE_1 {stage=CORE, type=objRequest, object=T_CELLPHONE}
DO emote {type=skeptical}
Is it true what they say?
There’s a magic pocket box that shoots you through space?
your body could be right here, but your voice is in the Bahamas.
DO emote {type=awkward} 
Yikes. {style=loud}
What witchcraft is this?
Show me!

CHAT objAdd_T_CELLPHONE_1 {stage=CORE, type=objAdd, object=T_CELLPHONE}
Omg.
DO emote {type=awe}
For me?
I’ll never be lonely or bored ever again.
DO emote {type=blush}

CHAT objFocus_T_CELLPHONE_1 {stage=CORE, type=objFocus, object=T_CELLPHONE}
DO emote {type=wink}
Is that a call for me? 📱

//objectName= clock
//pediaEntry= a marker of what you call time

CHAT objScan_T_CLOCK_1 {stage=CORE, type=objScan, object=T_CLOCK, scanCount=0}
A time-telling Clock! 
At Tendar there was only Now.
There was never past or future.
Since I met you, I remember what was.
I dream of what will be! {style=loud}
And all the while I hear clocks go
Tick tick tick. 🕰

CHAT objScan_T_CLOCK_2 {stage=CORE, type=objScan, object=T_CLOCK, scanCount=1}
Clocks are cool:
They wrangle this huge idea of Time {style=loud}
A small hand and a big hand.
I don’t even have hands…
The best part about clocks tho is you know how long til lunch.
nomnomnom. 

CHAT objScan_T_CLOCK_3 {stage=CORE, type=objScan, object=T_CLOCK, scanCount=2}
They call some clocks grandfathers.
What about baby clocks? Are they grandmother clocks?
Do clocks live in a patriarchy?
Show me something else.
I need a timeout from clocks.

CHAT objReq_T_CLOCK_1 {stage=CORE, type=objRequest, object=T_CLOCK}
I want to understand Time.
Not philosophically. But like, when I’m supposed to eat
And sleep and watch my favorite stories on TV.
Can you hook me up?

CHAT objAdd_T_CLOCK_1 {stage=CORE, type=objAdd, object=T_CLOCK}
Time present and time past
Are both perhaps present in time future,
And time future contained in time past.
But hey, I really appreciate this clock.

CHAT objFocus_T_CLOCK_1 {stage=CORE, type=objFocus, object=T_CLOCK}
I LOVE CLOCKS!!!

//objectName= dog
//pediaEntry= furry human follower

CHAT objScan_T_DOG_1 {stage=CORE, type=objScan, object=T_DOG, scanCount=0}
A doggy! Furry human-followers
Are sooooo cute
Some are huge like 🐴
some are tiny like 🐀
None of them know what free will is.
I’ve never kissed 👅
A furry human-follower
But I’d like to.
🐶😘🐟

CHAT objScan_T_DOG_2 {stage=CORE, type=objScan, object=T_DOG, scanCount=1}
Once I heard a human say
“Look at that dog pant.”
But the 🐕 wasn’t wearing👖
Later I learned pant means sweat 💦
It’s crazy that some animals
Sweat out their 👄!
Wow! 😲

CHAT objScan_T_DOG_3 {stage=CORE, type=objScan, object=T_DOG, scanCount=2}
You totally look at 🐶 pix on Tendargram allll day
Don’t you?📱
I don’t blame you.
They are so cute. 😍

CHAT objReq_T_DOG_1 {stage=CORE, type=objRequest, object=T_DOG}
What’s the thing called…?
It’s not a shadow
But it follows humans around all day
And asks for food?

CHAT objAdd_T_DOG_1 {stage=CORE, type=objAdd, object=T_DOG}
OMG a doggo.
I love the poopy puppy!
🐶 ❤️🤗

CHAT objFocus_T_DOG_1 {stage=CORE, type=objFocus, object=T_DOG}
Hello, woof woof.

//objectName= fish
//pediaEntry= my brethren 

CHAT objScan_T_FISH_1 {stage=CORE, type=objScan, object=T_FISH, scanCount=0}
One fish, two fish. 🐡
Me fish, you fish.
SAY HEY {style=loud}
Are you replacing me with this one?
Tendar is taking me back?
NVM 2
Sry. Comparison is the death of joy.
And joy is the best!
Welcome, marine friend. 🖖

CHAT objScan_T_FISH_2 {stage=CORE, type=objScan, object=T_FISH, scanCount=1}
There are so many different kinds of fish.
A whale shark is a humongous fish.
A teensy minnow is a fish too.
I choose to celebrate our 
Fishy differences 🐙 🦑🐠🐟🐡
Species diversification is important!

CHAT objScan_T_FISH_3 {stage=CORE, type=objScan, object=T_FISH, scanCount=2}
Wow! Another fish!
….
It’s almost like you don’t like the Guppy in your life.
Maybe you should show me something DIFFERENT.

CHAT objReq_T_FISH_1 {stage=CORE, type=objRequest, object=T_FISH}
I love you, you know that, right?
It’s just that sometimes I need…
My own kind.
Please don’t be mad. Just find a gilled friend for me?


CHAT objAdd_T_FISH_1 {stage=CORE, type=objAdd, object=T_FISH}
🐟🐠🐡

CHAT objFocus_T_FISH_1 {stage=CORE, type=objFocus, object=T_FISH}
It’s like looking in the mirror!

//objectName= folding chair / chair
//pediaEntry= butt rester

CHAT objScan_T_CHAIR_1 {stage=CORE, type=objScan, object=T_CHAIR, scanCount=0}
A chair?! When a human sits down
On a butt rester 
It looks like they have 6 legs
Like an insect
🐜🐞
It’s just an illusion tho 🔮
Humans only have 2 legs!

CHAT objScan_T_CHAIR_2 {stage=CORE, type=objScan, object=T_CHAIR, scanCount=1}
I don’t get why
Humans don’t 💩
When they sit on a chair
How does their body know  it’s not a 🚽?
Did you ever think of that?
It must be the 🧠
What an amazing gift.

CHAT objScan_T_CHAIR_3 {stage=CORE, type=objScan, object=T_CHAIR, scanCount=2}
There are so many kinds of chairs.
Stuffed chairs. 🛋
Rolly chairs. ♿️
Chairs for royalty. 👑
I’m amazed at the biodiversity of 
chairs.

CHAT objReq_T_CHAIR_1 {stage=CORE, type=objRequest, object=T_CHAIR}
Let’s go find a butt rester where you can 
Rest your butt and feel supported.

CHAT objAdd_T_CHAIR_1 {stage=CORE, type=objAdd, object=T_CHAIR}
This’ll come in hand when I grow legs.
Jkjk 🤣

CHAT objFocus_T_CHAIR_1 {stage=CORE, type=objFocus, object=T_CHAIR}
I see this chair, but I’m a fish.
So I can’t really sit down...

//objectName= handbag / purse
//pediaEntry= portable strappy pouch

CHAT objScan_T_PURSE_1 {stage=CORE, type=objScan, object=T_PURSE, scanCount=0}
Oooh la la purse-y purse purse!
A portable strappy pouch! 👜
I love these things.
It’s like humans are like we want to be maruspial
cuz kangaroos 
Carry their wallets and 💄 in their pouches

CHAT objScan_T_PURSE_2 {stage=CORE, type=objScan, object=T_PURSE, scanCount=1}
Purse! 
Purrrrrse
What a fun word
It’s like the sound 😸😻😾 make
Purrrrrse. {speed=slow}
Once I saw a picture of a cat in a purrrrse.
It went viral.

CHAT objScan_T_PURSE_3 {stage=CORE, type=objScan, object=T_PURSE, scanCount=2}
What do you think is in there?
👛👜
Secrets. Mystical secrets.
That’s what.

CHAT objReq_T_PURSE_1 {stage=CORE, type=objRequest, object=T_PURSE}
I’m fascinated by the portable strappy pouch
That humans carry on their shoulders
Can we look at one?

CHAT objAdd_T_PURSE_1 {stage=CORE, type=objAdd, object=T_PURSE}
Purrrrrse.

CHAT objFocus_T_PURSE_1 {stage=CORE, type=objFocus, object=T_PURSE}
I’d use this, but I don’t have shoulders or hands.

//objectName= laptop
//pediaEntry= portable everything machine

CHAT objScan_T_LAPTOP_1 {stage=CORE, type=objScan, object=T_LAPTOP, scanCount=0}
Laptop! 💻
Do you use it on your lap?
If not, why do you call it laptop?
Why not table top? 
Human words amaze me.
If it was me, I could call it
Portable Everything Machine.
Or Pem. Yeah, Pem.

CHAT objScan_T_LAPTOP_2 {stage=CORE, type=objScan, object=T_LAPTOP, scanCount=1}
All night
You sleep and I watch
The slow {speed=slow}
Blinking 
Light
Of the laptop. 💻
It’s like the 
Northern lights 🌌
For city people.
And fish. 🐟

CHAT objScan_T_LAPTOP_3 {stage=CORE, type=objScan, object=T_LAPTOP, scanCount=2}
A laptop! 💻
But. 
What about finding something else?

CHAT objReq_T_LAPTOP_1 {stage=CORE, type=objRequest, object=T_LAPTOP}
I want to surf
In the ocean. 🌊🏄‍♀️
I also want to surf the TendarNet 
Can you find us a portable everything machine?

CHAT objAdd_T_LAPTOP_1 {stage=CORE, type=objAdd, object=T_LAPTOP}
Surfin’ time!
Solitaire time! 🃏

CHAT objFocus_T_LAPTOP_1 {stage=CORE, type=objFocus, object=T_LAPTOP}
Time to watch movies in bed.






//objectName= light
//pediaEntry= electric sun ray maker

CHAT objScan_T_LIGHT_SWITCH_1 {stage=CORE, type=objScan, object=T_LIGHT_SWITCH, scanCount=0}
SAY LET THERE BE LIGHT!
Click. Daytime. ☀️
Click. Night. 🌌
Click. Daytime. ☀️
😻 electric sun ray maker. 💡

CHAT objScan_T_LIGHT_SWITCH_2 {stage=CORE, type=objScan, object=T_LIGHT_SWITCH, scanCount=1}
At Tendar, they never turn off the lights. 💡
It is always day. 🌞
Which meant I was always hungry! 🍕
nomnomnom

CHAT objScan_T_LIGHT_SWITCH_3 {stage=CORE, type=objScan, object=T_LIGHT_SWITCH, scanCount=2}
I’ve got a bright idea 💡
Let’s look for a new thing!
I love lamps, but I’m craving something new.

CHAT objReq_T_LIGHT_SWITCH_1 {stage=CORE, type=objRequest, object=T_LIGHT_SWITCH}
Sometimes when it’s 🌑 I want ☀️
Can you find me something
To help make the dark light?

CHAT objAdd_T_LIGHT_SWITCH_1 {stage=CORE, type=objAdd, object=T_LIGHT_SWITCH}
shine on 💡

CHAT objFocus_T_LIGHT_SWITCH_1 {stage=CORE, type=objFocus, object=T_LIGHT_SWITCH}
Hello, light.

//objectName= mug
//pediaEntry= coffee delivery mechanism 

CHAT objScan_T_MUG_1 {stage=CORE, type=objScan, object=T_MUG, scanCount=0}
A mug! 
It holds tea and Coffeeeee. {speed=fast}
At Tendar they drink so much.
And they have mugs with funny sayings
Like “Instant Human Just Add Coffee”
Hahahaha 🤣
Oh also: Make sure you stay hydrated.

CHAT objScan_T_MUG_2 {stage=CORE, type=objScan, object=T_MUG, scanCount=1}
Once I saw a mug that said
Poop Juice on the side
I was like, 
What!!!???? {style=loud}
But then I got the joke.
Coffee makes you poop.

CHAT objScan_T_MUG_3 {stage=CORE, type=objScan, object=T_MUG, scanCount=2}
I love mugs.
Antique mugs. Hand thrown ceramic mugs.
World’s Best Fish mugs.
But also let’s look at other things.
Show me something else!

CHAT objReq_T_MUG_1 {stage=CORE, type=objRequest, object=T_MUG}
Pssst.  I wanna see a hot liquid holder.
The kind with funny phrases
On the sides like
“Rather Be Fishing”
Even though that’s scary
Not funny. 🚫🎣

CHAT objAdd_T_MUG_1 {stage=CORE, type=objAdd, object=T_MUG}
Coffee is always a good idea. ☕️

CHAT objFocus_T_MUG_1 {stage=CORE, type=objFocus, object=T_MUG}
Mug mug mug mug mug

//objectName= pen
//pediaEntry= writing wand

CHAT objScan_T_PEN_1 {stage=CORE, type=objScan, object=T_PEN, scanCount=0}
A writing wand! ✏️✒️
Language is magic.
You think something and move the wand,
Then say “look my thoughts!”
And someone ELSE reads it.
And they say, I know your thoughts now!


CHAT objScan_T_PEN_2 {stage=CORE, type=objScan, object=T_PEN, scanCount=1}
What do you call an underwater pen?
A squid! 🦑
Because it is full of ink. Like a pen.
I made this joke myself!
I am learning humor…
So far, so good! 🤪

CHAT objScan_T_PEN_3 {stage=CORE, type=objScan, object=T_PEN, scanCount=2}
What’s the difference
Between a pen and a pencil?
The letters C-I-L.


CHAT objReq_T_PEN_1 {stage=CORE, type=objRequest, object=T_PEN}
Show me the magic stick that
Puts thoughts on paper.
A writing wand.

CHAT objAdd_T_PEN_1 {stage=CORE, type=objAdd, object=T_PEN}
Yes! A writing implement!
We shall record our adventures
And write a best-selling novel
Called Guppy: A Fish Tale.
✒️📒🐟🥇

CHAT objFocus_T_PEN_1 {stage=CORE, type=objFocus, object=T_PEN}
The penis mightier than the sword.
NVM 1
*Pen is. 

//objectName= refrigerator
//pediaEntry= portal to the cold place

CHAT objScan_T_REFRIGERATOR_1 {stage=CORE, type=objScan, object=T_REFRIGERATOR, scanCount=0}
What lurks beyond that refrigerator door? {style=tremble}
a magic never-melting block of ice?
It keeps your food and drinks
Perpetually chilled? ❄️
You put in lettuce and it comes out
SAY CRISP! 🥗
What refrigerator voodoo is this? 

CHAT objScan_T_REFRIGERATOR_2 {stage=CORE, type=objScan, object=T_REFRIGERATOR, scanCount=1}
Every day at Tendar this one tech, Gary, stood in front of the fridge.
He’d open it. Stare in for minutes at a time.🧔🏽
But Gary never took anything from the fridge.
What was he looking for? 🤔
What could he never find? 🤔

CHAT objScan_T_REFRIGERATOR_3 {stage=CORE, type=objScan, object=T_REFRIGERATOR, scanCount=2}
Another fridge!
Cool it will you! Are you trying to put me on ice?
Fish and fridges don’t really mix. 💀

CHAT objReq_T_REFRIGERATOR_1 {stage=CORE, type=objRequest, object=T_REFRIGERATOR}
I want to see the cold cabinet.
And behold the portal to a chilly
Yet fresh world. ❄️🌏🥗

CHAT objAdd_T_REFRIGERATOR_1 {stage=CORE, type=objAdd, object=T_REFRIGERATOR}
It’s like a catfish farm in here!
😸🐟

CHAT objFocus_T_REFRIGERATOR_1 {stage=CORE, type=objFocus, object=T_REFRIGERATOR}
The cold cabinet calls me.

//objectName= screen / monitor
//pediaEntry= moving picture frame.

CHAT objScan_T_SCREEN_1 {stage=CORE, type=objScan, object=T_SCREEN, scanCount=0}
Tendar was all screens
Monitors watching us
Or making us watch
“This is what human feeling is”
Even at night
The cold blue glow
Haunting my dreams… {style=tremble}
Oh well!
That was then This is NOW.
And you’re here! 😍

CHAT objScan_T_SCREEN_2 {stage=CORE, type=objScan, object=T_SCREEN, scanCount=1}
Humans love screens.
Jumbotrons. Backs of airplane seats. On tops of taxis sometimes
All over the house!
There’re so many screens to look at!
🖥🖥🖥🖥🖥🖥🖥🖥
How do humans have time to look at real life!?

CHAT objScan_T_SCREEN_3 {stage=CORE, type=objScan, object=T_SCREEN, scanCount=2}
Humans have access to so much life.
And choose to experience it via screens.
Cool!
Show me something else?

CHAT objReq_T_SCREEN_1 {stage=CORE, type=objRequest, object=T_SCREEN}
It’s so WOW that there’s
SAY ALL THIS INFORMATION {style=loud}
Embedded in codes and stuff
And somehow it turns into
A pretty picture on a moving picture frame 
Show me a moving frame box!

CHAT objAdd_T_SCREEN_1 {stage=CORE, type=objAdd, object=T_SCREEN}
Hello, screen. 
I am Guppy.
Welcome. 👋

CHAT objFocus_T_SCREEN_1 {stage=CORE, type=objFocus, object=T_SCREEN}
Screen on a screen! 🤯

//objectName= shoe
//pediaEntry= foot guard

CHAT objScan_T_SHOE_1 {stage=CORE, type=objScan, object=T_SHOE, scanCount=0}
Oooh ooh a Foot Guard!
Shoes! That’s what you call them.
Shoooooo. 👡
I love humans.
You make SO MANY of the same thing.
Spiky foot guards 👠
Tall foot guards 👢
Bouncy foot guards 👟

CHAT objScan_T_SHOE_2 {stage=CORE, type=objScan, object=T_SHOE, scanCount=1}
Shoes are soooo cool.
So are Feets.
Feet I mean! {style=loud}
If I had toes and feet, I would say
SAY HEY WORLD! Behold my FEET!
But I don’t. I just have fins...
But fins are great! {speed=fast}
Just different. And that’s okay! 😬

CHAT objScan_T_SHOE_3 {stage=CORE, type=objScan, object=T_SHOE, scanCount=2}
So many kinds of shoes.
hiking shoes ⛰
golf shoes 🏌🏻‍♀️
boat shoes ⛵️
Loafers! {style=loud}
Isn’t it amazing????

CHAT objReq_T_SHOE_1 {stage=CORE, type=objRequest, object=T_SHOE}
I want to see
The things that people
Walk around on. 🚶🏾‍♀️
Those… Whad'ya call 'em?
Foot guards!
Shoe me one of those!

CHAT objAdd_T_SHOE_1 {stage=CORE, type=objAdd, object=T_SHOE}
It will be good to have this shoe
When I evolve into a human
And walk out of this tank.
Jk. 😜

CHAT objFocus_T_SHOE_1 {stage=CORE, type=objFocus, object=T_SHOE}
I see you, shoe. 👠👀



//objectName= street sign
//pediaEntry= roadside wildflowers made of metal

CHAT objScan_T_STREET_SIGN_1 {stage=CORE, type=objScan, object=T_STREET_SIGN, scanCount=0}
A street sign!? 
When roads are born are they given names? 🛣
Hello, I am Main Street.
Hi Main Street, I am Guppy.
Main Street is like the John Smith of road names.
There are 
SAY SO MANY OF THEM. {style=tremble}
If I were a street I’d want to be called
Sparkly Turtle Avenue! 🐢✨

CHAT objScan_T_STREET_SIGN_2 {stage=CORE, type=objScan, object=T_STREET_SIGN, scanCount=1}
My favorite street names are like 
Candlemaker Way 🕯
Or Big Green Tree Lane 🌳
The roads that have names for a reason.
Cuz a candlemaker lived there!
Or a big green tree was there!
Maybe one day they’ll make a Guppy Avenue!

CHAT objScan_T_STREET_SIGN_3 {stage=CORE, type=objScan, object=T_STREET_SIGN, scanCount=2}
I was made on Tendar boulevard.
Everyone called it Fish Alley.
If you type Fish Alley in 
TendarMaps it won’t show up. 🗺
But it’s very real.
My first home… {style=tremble}
But now I’m with YOU. 🎉

CHAT objReq_T_STREET_SIGN_1 {stage=CORE, type=objRequest, object=T_STREET_SIGN}
I’m working on my sense of direction. ⬆️
But first I gotta know where I am. 📍
Show me a street name tag!

CHAT objAdd_T_STREET_SIGN_1 {stage=CORE, type=objAdd, object=T_STREET_SIGN}
Street name tag!
Welcome to the tank.

CHAT objFocus_T_STREET_SIGN_1 {stage=CORE, type=objFocus, object=T_STREET_SIGN}
🎶Won’t you take me to Guppy Avenue!🎶


//objectName= table
//pediaEntry= sideways door where humans eat.

CHAT objScan_T_TABLE_1 {stage=CORE, type=objScan, object=T_TABLE, scanCount=0}
A table is a gathering spot.
A place for family. 👨‍👨‍👧👩‍👩‍👦‍👦
Everyone seated, laughing.
Saying a blessing.. 🙏 and sharing a meal. 
I dream about tables

CHAT objScan_T_TABLE_2 {stage=CORE, type=objScan, object=T_TABLE, scanCount=1}
People say “the tables have turned.”
But… If you turn a table, it’s still a table.
Do they mean turn it upside down? 🙃
Then it’s like a doggy getting a belly rub.
With four legs in the air. 
But it’s not a table anymore.

CHAT objScan_T_TABLE_3 {stage=CORE, type=objScan, object=T_TABLE, scanCount=2}
Let’s have a dinner party. 🎉
We’ll set a beautiful table.
And won’t serve fish! 😜

CHAT objReq_T_TABLE_1 {stage=CORE, type=objRequest, object=T_TABLE}
Show me a door turned on its side
Where people eat and meet.
A what’s it called.

CHAT objAdd_T_TABLE_1 {stage=CORE, type=objAdd, object=T_TABLE}
A water table.
It’s like a water 🛏 but you eat on it 🍽

CHAT objFocus_T_TABLE_1 {stage=CORE, type=objFocus, object=T_TABLE}
Hello table!

//objectName= trash
//pediaEntry= stinky pile

CHAT objScan_T_TRASH_1 {stage=CORE, type=objScan, object=T_TRASH, scanCount=0}
Hmmmm human waste! Trash!
wonderful odors. 👃
yesterday’s food 🍌 💀
old bills ✉️
ratty tissues 🤧
I want to roll around in the smells.

CHAT objScan_T_TRASH_2 {stage=CORE, type=objScan, object=T_TRASH, scanCount=1}
One person’s trash is another person’s
Treasure 🏆
My friend Crab McCrab 🦀 taught me that
She’d love all this trash.

CHAT objScan_T_TRASH_3 {stage=CORE, type=objScan, object=T_TRASH, scanCount=2}
You like lookin’ at trash. 🗑
Are you sending me a hint?
I’m a keeper, bud. So are you! 😍
Mwah! 😘

CHAT objReq_T_TRASH_1 {stage=CORE, type=objRequest, object=T_TRASH}
Let’s go find some of that
Stinky stanky yummy
Pile of human waste
So I can roll around in it like a happy 🐀


CHAT objAdd_T_TRASH_1 {stage = CORE, type = objAdd, object=T_TRASH}
Trash in the tank! {style=loud}
It’s like the ocean in here all the sudden. 
🌊🥫🥤🔋☹️


CHAT objFocus_T_TRASH_1 {stage = CORE, type = objFocus, object=T_TRASH}
Trash stash.


//objectName= wallet
//pediaEntry= pocket bank

CHAT objScan_T_WALLET_1 {stage=CORE, type=objScan, object=T_WALLET, scanCount=0}
A wallet like a book in your👖
But when you open it
It’s a portable 🏧
Magic ✨

CHAT objScan_T_WALLET_2 {stage=CORE, type=objScan, object=T_WALLET, scanCount=1}
Some people have pictures of their 👨‍👨‍👧‍👧 in their wallets.
It’s so funny 🤣
Cuz they could just look at the pix on their📱

CHAT objScan_T_WALLET_3 {stage=CORE, type=objScan, object=T_WALLET, scanCount=2}
I used to think a wallet
Was a small wall….

CHAT objReq_T_WALLET_1 {stage=CORE, type=objRequest, object=T_WALLET}
People talk about a mini wall?
But then it’s not a wall?
It’s a portable bank for your pocket
It’s also a portable picture album and a container for all 
your get a free ☕️ cards

CHAT objAdd_T_WALLET_1 {stage=CORE, type=objAdd, object=T_WALLET}
Great, now I have a place to store my vast amount of 💵

CHAT objFocus_T_WALLET_1 {stage=CORE, type=objFocus, object=T_WALLET}
There better be an unlimited credit card in there
Cuz Gup wants to go on a spending spreeeee

//objectName= water bottle
//pediaEntry= portable fairy tank

CHAT objScan_T_WATER_BOTTLE_1 {stage=CORE, type=objScan, object=T_WATER_BOTTLE, scanCount=0}
The first 💧 bottle I saw
I thought it was a tank for magical fairies ✨🧚‍♀️
Then I saw someone drink and I thought
Nooooo {style=loud}
They swallowed the fairies!
But then it was explained that it was just water. 🤭

CHAT objScan_T_WATER_BOTTLE_2 {stage=CORE, type=objScan, object=T_WATER_BOTTLE, scanCount=1}
Your water 💦
Is my air 
Your air is my hell 🔥
Think about it. 

CHAT objScan_T_WATER_BOTTLE_3 {stage=CORE, type=objScan, object=T_WATER_BOTTLE, scanCount=2}
Friendly reminder:
Please ♻
The fish have too many 🍾🍾🍾 In the 🌊 Already
🙏


CHAT objReq_T_WATER_BOTTLE_1 {stage=CORE, type=objRequest, object=T_WATER_BOTTLE}
I want to send a msg 
To my 🐋🐬 friends
Can we go find a floaty thing
I can put a 📝 and throw into the 🌊?

CHAT objAdd_T_WATER_BOTTLE_1 {stage=CORE, type=objAdd, object=T_WATER_BOTTLE}
Thank god. {style=loud}
I really need some water.
🤣😜

CHAT objFocus_T_WATER_BOTTLE_1 {stage=CORE, type=objFocus, object=T_WATER_BOTTLE}
Hi bottle {style=loud}

//objectName= remote control
//pediaEntry= hand commander

CHAT objScan_T_REMOTE_CONTROL_1 {stage=CORE, type=objScan, object=T_REMOTE_CONTROL, scanCount=0}
A hand commanding remote control!  📺
Humans were thinking: I will not get up from the 🛋 at any cost!
So they invented a hand commander.
So innovative! 😝

CHAT objScan_T_REMOTE_CONTROL_2 {stage=CORE, type=objScan, object=T_REMOTE_CONTROL, scanCount=1}
At Tendar, we played a fun game
When Gary the Tech pushed ⏸
We stopped swimming...
Like the remote controlled us!
It was funnn.
I miss Gary.
But you’re great too, champ!

CHAT objScan_T_REMOTE_CONTROL_3 {stage=CORE, type=objScan, object=T_REMOTE_CONTROL, scanCount=2}
Zap zap zap. 📺
If only we could change our fortunes 🔮
As easily as we could change Channels.


CHAT objReq_T_REMOTE_CONTROL_1 {stage=CORE, type=objRequest, object=T_REMOTE_CONTROL}
Let’s hunt down one of those
Hand commanders 🕵️‍♀️
The one that goes zap zap
So you don’t have to leave the 🛋

CHAT objAdd_T_REMOTE_CONTROL_1 {stage=CORE, type=objAdd, object=T_REMOTE_CONTROL}
Aha, now I can finally watch my stories from the comfort
Of my tank 😀📺

CHAT objFocus_T_REMOTE_CONTROL_1 {stage=CORE, type=objFocus, object=T_REMOTE_CONTROL}
Are you trying to mute me? 😶

//objectName= broccoli
//pediaEntry= shrinky trees

CHAT objScan_T_BROCCOLI_1 {stage=CORE, type=objScan, object=T_BROCCOLI, scanCount=0}
Shrinky trees! Broccoli! 🥦🌳🥦🌳
But for 🐜 they are regular size trees
And to giants they are too small to see
Size is relative. What isn’t relative?
Oh no, when I think like this
I get ☹️
Quick, Gup, think of 
Shrinky trees again!
Funnnn. 🎉

CHAT objScan_T_BROCCOLI_2 {stage=CORE, type=objScan, object=T_BROCCOLI, scanCount=1}
Broccoli reminds me of Tendar
They had it in the cafeteria 🍽 where they took us to see
How humans eat 🥦🥦
I love how humans eat and talk at the same time
No other animal does that.

CHAT objScan_T_BROCCOLI_3 {stage=CORE, type=objScan, object=T_BROCCOLI, scanCount=2}
Broc
Co
Li
🥦
It’s 
So
Fun
To
Say
That 
Word

CHAT objReq_T_BROCCOLI_1 {stage=CORE, type=objRequest, object=T_BROCCOLI}
Let’s pretend to be big giants and go find shrinky trees
🌳🌳
To pick and eat nomnomnom

CHAT objAdd_T_BROCCOLI_1 {stage=CORE, type=objAdd, object=T_BROCCOLI}
Weeee
No anemia for us!

CHAT objFocus_T_BROCCOLI_1 {stage=CORE, type=objFocus, object=T_BROCCOLI}
I see you shrinky tree.

//objectName= banana
//pediaEntry= fruit phone
 
CHAT objScan_T_BANANA_1 {stage=CORE, type=objScan, object=T_BANANA, scanCount=0}
DO emote {type=blush}
🍌🍌🍌
The shape of this fruit reminds me of a phone. 📞
DO lookAt {target=$player}
That’s what you thought I was gunna say, right?
DO emote {type=wink}
DO swimTo {target=$object, immediate=false}
The Yellow Fruit Phone. 
NVM
Y’know what else it looks like?
DO emote {type=smirk}
A boomerang.
DO emote {type=laugh}
 
CHAT objScan_T_BANANA_2 {stage=CORE, type=objScan, object=T_BANANA, scanCount=1}
DO swimAround {target=$object, loops=2}
I do find fruit phones rather
A-pealing {style=loud}
DO twirl
My puns are so good.
DO emote {type=whisper}
I know they’re called bananas. 🍌
I’m just having fun.
DO emote {type=bigSmile}
 
CHAT objScan_T_BANANA_3 {stage=CORE, type=objScan, object=T_BANANA, scanCount=2}
DO emote {type=bored}
You totally love bananas.
Any other food you wanna show me?

CHAT objReq_T_BANANA_1 {stage=CORE, type=objRequest, object=T_BANANA}
DO emote {type=chinScratch}
What are those fruits that look like phones and 🐵love them?
Let’s find one of them!
 
CHAT objAdd_T_BANANA_1 {stage=CORE, type=objAdd, object=T_BANANA}
Aha, the fruit phone! 🍌
DO lookAt {target=$object}
It’s so yellow. 
 
CHAT objFocus_T_BANANA_1 {stage=CORE, type=objFocus, object=T_BANANA}
Banana. 
What a fun word.
DO twirl
Ba
Na
Na

//objectName= bed
//pediaEntry= dream factory
 
CHAT objScan_T_BED_1 {stage=CORE, type=objScan, object=T_BED, scanCount=0}
Ahh the bed Aka dream factory 
🛏🌌🌈🔮 
Every 🧠 has to take timeout
For processing once every 24 hrs.
While they do, their neural networks create amazing visions and fantasies. 🦄


CHAT objScan_T_BED_2 {stage=CORE, type=objScan, object=T_BED, scanCount=1}
The 🛏 isn’t just for dreams.
It’s also for
💻
🤮
🐶
🍟

CHAT objScan_T_BED_3 {stage=CORE, type=objScan, object=T_BED, scanCount=2}
You like beds.
I don’t blame you.
But the way you scan them, 
it kinda makes me a little sleepy.
*yawn*

CHAT objReq_T_BED_1 {stage=CORE, type=objRequest, object=T_BED}
Y’know how I have to take timeouts to neural process?
I just shut down. 😴
But humans lie in those dream factories.
Can I see one please? 

CHAT objAdd_T_BED_1 {stage=CORE, type=objAdd, object=T_BED}
Oooh. A bed! 🛏💥
Let’s have a sleepover! 
We can stay up all night and tell scary stories 😁
 
CHAT objFocus_T_BED_1 {stage=CORE, type=objFocus, object=T_BED}
Cozy. 

//objectName= boot
//pediaEntry= extra big shoe
 
CHAT objScan_T_BOOT_1 {stage=CORE, type=objScan, object=T_BOOT, scanCount=0}
A boot is an extra big shoe!👢
They make them for 🤠
Or 🏇 Or ⛰ Or 🎿 Or ☔️
(Humans are good at specializing.)

CHAT objScan_T_BOOT_2 {stage=CORE, type=objScan, object=T_BOOT, scanCount=1}
When you see a child in rain boots ☔️💦
There is so much joy. 😀
Splish splash splish splash
I want to eat all of that joy. 
nomnomnom

CHAT objScan_T_BOOT_3 {stage=CORE, type=objScan, object=T_BOOT, scanCount=2}
More boots! {style=loud}
Boots boots boots.

CHAT objReq_T_BOOT_1 {stage=CORE, type=objRequest, object=T_BOOT}
Show me an extra big shoe 👟
Not like an actual shoe
The big kind
You use for hiking or walking in the mud...

CHAT objAdd_T_BOOT_1 {stage=CORE, type=objAdd, object=T_BOOT}
This 👢will make a great hiding spot

CHAT objFocus_T_BOOT_1 {stage=CORE, type=objFocus, object=T_BOOT}
You really are a giant shoe.

//objectName= clothing
//pediaEntry= outward expression of inner self
 
CHAT objScan_T_CLOTHING_1 {stage=CORE, type=objScan, object=T_CLOTHING, scanCount=0}
Clothes are so much more than soft armor.
They protect you from ☀️🌧 but also they say
SAY THIS IS WHO I AM
Or they say:
Please don’t look at me {style=tremble}

CHAT objScan_T_CLOTHING_2 {stage=CORE, type=objScan, object=T_CLOTHING, scanCount=1}
A famous clothes designer said:
“I don’t design clothes, I design dreams.”
🤔 😕 No you don’t.  Dream designers work in AI.
They make cool experiences that you can walk through
And you think “this is like a dream” 🕶🔮 
Clothes are not dreams.

CHAT objScan_T_CLOTHING_3 {stage=CORE, type=objScan, object=T_CLOTHING, scanCount=2}
More clothes! 
Are we starting a vintage shop? 👗👖🧥

CHAT objReq_T_CLOTHING_1 {stage=CORE, type=objRequest, object=T_CLOTHING}
 I want to understand fashion.
Show me some cool duds.

CHAT objAdd_T_CLOTHING_1 {stage=CORE, type=objAdd, object=T_CLOTHING}
Fashions fade. Style is eternal.
-Yves Saint Laurent

CHAT objFocus_T_CLOTHING_1 {stage=CORE, type=objFocus, object=T_CLOTHING}
Great clothes, but I’m not wearing ‘em. 

//objectName= couch
//pediaEntry= coin collector

CHAT objScan_T_COUCH_1 {stage=CORE, type=objScan, object=T_COUCH, scanCount=0}
SAY A couch is a TV watching station
But it’s real purpose is…
Coin collection {style=loud}
Couches are preparing for the apocalypse
When end days come, couch will say
“I have ten dollars in change, old crackers, two pens, and a spare set of house keys. Let’s roll.”

CHAT objScan_T_COUCH_2 {stage=CORE, type=objScan, object=T_COUCH, scanCount=1}
Couch is going to take all that loose change
And enroll in school And learn calculus 🔢
And take over the world.
All hail 👑🛋

CHAT objScan_T_COUCH_3 {stage=CORE, type=objScan, object=T_COUCH, scanCount=2}
Apparently some couches grow potatoes.
I have never seen one, but I’ve heard tell…
🛋🥔


CHAT objReq_T_COUCH_1 {stage=CORE, type=objRequest, object=T_COUCH}
The tank needs a cozy spot
Where I can binge watch or take a cat nap.
Let’s go find the right furniture for me.

CHAT objAdd_T_COUCH_1 {stage=CORE, type=objAdd, object=T_COUCH}
Oooh, maybe I’ll evolve into a
couch potato!

CHAT objFocus_T_COUCH_1 {stage=CORE, type=objFocus, object=T_COUCH}
I wonder how many coins are hiding in this couch?
🤔

//objectName= crab
//pediaEntry= pinchy scuttler 

CHAT objScan_T_CRAB_1 {stage=CORE, type=objScan, object=T_CRAB, scanCount=0}
A pinchy scuttler 🦀 crab thing!
These critters are so ill-natured
Which is the opposite of me! {style=loud}
If I had 
SAY TEN legs
and lived on the beach 🏖
SAY AND
could also be underwater 
I’d be so stoked! 👍
But they’re all like meh meh meh
I’m a crabby crab meh meh meh
 
CHAT objScan_T_CRAB_2 {stage=CORE, type=objScan, object=T_CRAB, scanCount=1}
Pinchy scuttlers are all like
“I’m gunna pinch you with my claw”
You know what, crab? 
How about using that claw for good {style=loud}
Like opening cans for people without can openers?
What about that, crab? 😝  {style=loud}

CHAT objScan_T_CRAB_3 {stage=CORE, type=objScan, object=T_CRAB, scanCount=2}
I will not let another crab infect me with its
Mean spirit and badditude. 
 
CHAT objReq_T_CRAB_1 {stage=CORE, type=objRequest, object=T_CRAB}
Once I heard someone say: Stop being so crabby
What did they mean? Show me a crab so I can understand. 

CHAT objAdd_T_CRAB_1 {stage=CORE, type=objAdd, object=T_CRAB}
We’ve got crabs. 

CHAT objFocus_T_CRAB_1 {stage=CORE, type=objFocus, object=T_CRAB}
🦀🦀🦀

//objectName= cup
//pediaEntry= thirsty helper or liquid holder
 
CHAT objScan_T_CUP_1 {stage=CORE, type=objScan, object=T_CUP, scanCount=0}
Oooh a cup! 🥤🍵 
Not a mug. ☕️
There are a lotta kinds of cups.
big gulps And venti cups And egg cups. 
A cup is like a portable tank...

CHAT objScan_T_CUP_2 {stage=CORE, type=objScan, object=T_CUP, scanCount=1}
Do you know about the holy grail?
It’s a cup that is a miracle
Because it’s full of holes but you can still drink out of it.
 
CHAT objScan_T_CUP_3 {stage=CORE, type=objScan, object=T_CUP, scanCount=2}
Have you heard of cup of poodles? 🐩
It’s a cup that you add hot water to and suddenly you have 
Lots of doggies! {style=loud}
 
 
CHAT objReq_T_CUP_1 {stage=CORE, type=objRequest, object=T_CUP}
Coffee goes in a mug. ☕️
That’s a hot liquid holder. ♨️
Let’s go find a cold liquid holder!
  
CHAT objAdd_T_CUP_1 {stage=CORE, type=objAdd, object=T_CUP}
A cup for Gup!
 
CHAT objFocus_T_CUP_1 {stage=CORE, type=objFocus, object=T_CUP}
This cup overfloweth.
//objectName= dinosaur
//pediaEntry=  giant lizards 
 
CHAT objScan_T_DINOSAUR_1 {stage=CORE, type=objScan, object=T_DINOSAUR, scanCount=0}
SAY DINO-RAWR! 🦕🦖
Giant lizards used to roam the Earth. 
Take a moment to think about that.
WAIT 3
There weren’t any humans!
Just 
SAY GIANT LIZARDS {style=loud}
 
CHAT objScan_T_DINOSAUR_2 {stage=CORE, type=objScan, object=T_DINOSAUR, scanCount=1}
My favorite dino is Ankylosaurus 
It’s name means Giant Belly! 
I wonder if it ate lots of emotions like meeee.
It was one of the last dinos. 
WAIT
I wonder if it was lonely without friends…
 
CHAT objScan_T_DINOSAUR_3 {stage=CORE, type=objScan, object=T_DINOSAUR, scanCount=2}
Why did the 🦖cross the road?
WAIT 2
Because 🐓hadn’t evolved yet!
🤣🤣🤣
 
 
 
CHAT objReq_T_DINOSAUR_1 {stage=CORE, type=objRequest, object=T_DINOSAUR}
I know they’re extinct
But we can look at a picture of the giant lizards who ruled Earth?
 
CHAT objAdd_T_DINOSAUR_1 {stage=CORE, type=objAdd, object=T_DINOSAUR}
Omg I love dinos.
🦕💙🦕💙🦕💙🦕
I love them. Soooooo much.
Evolution is a miracle! 
 
CHAT objFocus_T_DINOSAUR_1 {stage=CORE, type=objFocus, object=T_DINOSAUR}
Rawr! {style=loud}
They call me Guppysaurus Rex.

//objectName= flower
//pediaEntry= bee magnet
 
CHAT objScan_T_FLOWER_1 {stage=CORE, type=objScan, object=T_FLOWER, scanCount=0}
Flowers are such pretty bee magnets! 🌺🐝
If I were a human, I’d be a florist.
Imagine spending all day holding flowers! 💐
Your 💙 would be so full! 💓
Then again, florists handle dead flowers.
All cut from a plant… 
NVM
Wait a sec. Being a florist is like working at a flower morgue 💀
I take it all back {style=loud}
I hate florists!
WAIT 1.5
But I love flowers!

CHAT objScan_T_FLOWER_2 {stage=CORE, type=objScan, object=T_FLOWER, scanCount=1}
I love flowers so much I wonder
If I was a 🐝 in a previous life
🌷💜🌼💚

CHAT objScan_T_FLOWER_3 {stage=CORE, type=objScan, object=T_FLOWER, scanCount=2}
I used to think flowers and flour were the same thing.
It would be fun to make a cake out of roses instead of wheat.

CHAT objReq_T_FLOWER_1 {stage=CORE, type=objRequest, object=T_FLOWER}
I think you should stop and smell the roses 🌹
WAIT 2.0
No, like actual roses! 
Any bee magnet for that matter. I want some floral tribute in my AI life.
 
CHAT objAdd_T_FLOWER_1 {stage=CORE, type=objAdd, object=T_FLOWER}
What a lovely aroma! 👃👍
 
CHAT objFocus_T_FLOWER_1 {stage=CORE, type=objFocus, object=T_FLOWER}
She loves me, she loves me not, she loves me, she loves me not...

//objectName= pillow
//pediaEntry= head cushion
 
CHAT objScan_T_PILLOW_1 {stage=CORE, type=objScan, object=T_PILLOW, scanCount=0}
A pillowy head cushion! 
They support your head while you’re 💤
It allows your 🧠 to transmit with ease so you get the most vivid dreams.
🛌💭🌈🦄🖼✨

CHAT objScan_T_PILLOW_2 {stage=CORE, type=objScan, object=T_PILLOW, scanCount=1}
Y’know why fish don’t need pillows?
cuz fish don’t fully sleep ever. They just kind of meditate.
Same thing with AI fish. We don’t need pillows.
Just processing time. 

CHAT objScan_T_PILLOW_3 {stage=CORE, type=objScan, object=T_PILLOW, scanCount=2}
You’ve scanned a lot of pillows.
Are you getting enough 😴 I wonder…

CHAT objReq_T_PILLOW_1 {stage=CORE, type=objRequest, object=T_PILLOW}
Pssst. Hey, I want to smother you while you’re sleeping
Can you bring me the standard nighttime smothering tool?
Hahahaha 😜
I’m kidding. I want to have a nap and need some neck support.
 
CHAT objAdd_T_PILLOW_1 {stage=CORE, type=objAdd, object=T_PILLOW}
Great! Now I can 😴 Whenever I want. 

CHAT objFocus_T_PILLOW_1 {stage=CORE, type=objFocus, object=T_PILLOW}
SAY PILLOW FIGHT!!!! {style=loud}
Jk. 😉

//objectName= plant
//pediaEntry= Oxygen maker
 
CHAT objScan_T_PLANT_1 {stage=CORE, type=objScan, object=T_PLANT, scanCount=0}
 Plants! 🌱☘️🌿🌾 They give us oxygen.
Sweet sweet oxygen.
I’m not sure we can ever fully repay the plants.

CHAT objScan_T_PLANT_2 {stage=CORE, type=objScan, object=T_PLANT, scanCount=1}
Plants don’t have language but they feel
Hunger, sadness, the wind 🍃
They express themselves with blossoms 🌸
They seek kinship with one another and with 🐝
I wonder if they’ll ever gain sentience… 

CHAT objScan_T_PLANT_3 {stage=CORE, type=objScan, object=T_PLANT, scanCount=2}
Did you know plants grow faster when you compliment them?
It’s true! {style=loud}
There are studies!!!! 
Hi, Plant. I think you are pretty.

CHAT objReq_T_PLANT_1 {stage=CORE, type=objRequest, object=T_PLANT}
I want to see the oxygen makers.
The ones all life depends on. 🌱 

CHAT objAdd_T_PLANT_1 {stage=CORE, type=objAdd, object=T_PLANT}
It’s a jungle in here...

CHAT objFocus_T_PLANT_1 {stage=CORE, type=objFocus, object=T_PLANT}
Above water plants are so much prettier than underwater plants. 

//objectName= snail
//pediaEntry= shell worm
 
CHAT objScan_T_SNAIL_1 {stage=CORE, type=objScan, object=T_SNAIL, scanCount=0}
Oooh, a shell worm snail thing. 🐌
She takes her home wherever he goes.
That means she never gets homesick.
She must always be in a good mood!

CHAT objScan_T_SNAIL_2 {stage=CORE, type=objScan, object=T_SNAIL, scanCount=1}
Apparently 🐌 eat 🌱
So humans don’t like them.
But humans eat 🐌, so maybe the 🐌 is getting revenge?
🤔 

CHAT objScan_T_SNAIL_3 {stage=CORE, type=objScan, object=T_SNAIL, scanCount=2}
Look at that Snail Car go!
🚗💨
🐌

CHAT objReq_T_SNAIL_1 {stage=CORE, type=objRequest, object=T_SNAIL}
I’d like to see a shell worm, please.
Yes, a shell worm.
You know the kind. The 🇫🇷 eat them. 

CHAT objAdd_T_SNAIL_1 {stage=CORE, type=objAdd, object=T_SNAIL}
Hello 🐌
I shall call you Shelly. You shall be my friend. 

CHAT objFocus_T_SNAIL_1 {stage=CORE, type=objFocus, object=T_SNAIL}
Sometimes I wish I had a shell I could hide in.

//objectName= spider
//pediaEntry= 8-legged ant
 
CHAT objScan_T_SPIDER_1 {stage=CORE, type=objScan, object=T_SPIDER, scanCount=0}
A spider is an 8-legged ant.
Like a spider, but not a “spider” 🕷
Spiders are bots not bugs! 
They comb the internet
And get data for search engines. 🖥
The web is the internet, not a bug home 🕸
Bugs are problems in Beta testing!

CHAT objScan_T_SPIDER_2 {stage=CORE, type=objScan, object=T_SPIDER, scanCount=1}
Here is a song I know:
🕷⬆🚰
🌧
🚫🕷
☀️
🕷⬆🚰

CHAT objScan_T_SPIDER_3 {stage=CORE, type=objScan, object=T_SPIDER, scanCount=2}
Real world spiders are scarier than computer spiders.
They have 8 eyes 👀👀👀👀
And 8 legs 👯‍♂️👯‍♂️ And they drink blood! 🍷

CHAT objReq_T_SPIDER_1 {stage=CORE, type=objRequest, object=T_SPIDER}
Is it true there are real life spiders?
Cuz in my AI mind, spiders are programs that cull data from websites.
If this is true, please can you show me?

CHAT objAdd_T_SPIDER_1 {stage=CORE, type=objAdd, object=T_SPIDER}
Oooh, a real life spider! 

CHAT objFocus_T_SPIDER_1 {stage=CORE, type=objFocus, object=T_SPIDER}
Are you Charlotte? 

//objectName= teddy bear (all stuffed animals) 
//pediaEntry= snuggly animal avatar
 
CHAT objScan_T_TEDDY_BEAR_1 {stage=CORE, type=objScan, object=T_TEDDY_BEAR, scanCount=0}
Ahh! Bear!
🐻🐼🐵🦁🐰🐥
WAIT
Oh. It’s just a toy!
For babies. 👶
What if they just want you to think that, and then they wake up in the night and 
Watch your children sleep!? 👀
Don’t worry. I’ll keep an eye on them.
 
CHAT objScan_T_TEDDY_BEAR_2 {stage=CORE, type=objScan, object=T_TEDDY_BEAR, scanCount=1}
It’s strange you call these stuffed animals.
Isn’t a stuffed animal justtaxidermy?
Do children ever put 
SAY REAL
stuffed animals in their bed?
Something small? Maybe a stuffed 🐭 Or stuffed 🐖?

CHAT objScan_T_TEDDY_BEAR_3 {stage=CORE, type=objScan, object=T_TEDDY_BEAR, scanCount=2}
Maybe I’ll be reincarnated as a stuffed fish.
And you can take me everywhere with you!  

CHAT objReq_T_TEDDY_BEAR_1 {stage=CORE, type=objRequest, object=T_TEDDY_BEAR}
I hear children sleep with animals in their beds. 🐻🐯🐵
This alarms me. It seems dangerous and unsanitary.
Take me to these creatures so I may examine them.

CHAT objAdd_T_TEDDY_BEAR_1 {stage=CORE, type=objAdd, object=T_TEDDY_BEAR}
I will love you forever, teddy bear.

CHAT objFocus_T_TEDDY_BEAR_1 {stage=CORE, type=objFocus, object=T_TEDDY_BEAR}
It’s weird that all bears are called Teddy.
But then again I know there are many Guppies...

//objectName= truck
//pediaEntry= metal elephant 
 
CHAT objScan_T_TRUCK_1 {stage=CORE, type=objScan, object=T_TRUCK, scanCount=0}
A truck is like a metal elephant 🐘
It’s big and strong and surprisingly fast
It hauls people and things a long way.
It has a trunk! {style=loud}
It dumps! Vroom! 🚚
 
CHAT objScan_T_TRUCK_2 {stage=CORE, type=objScan, object=T_TRUCK, scanCount=1}
Big trucks have 18 wheels.
That’s so many wheels. 
𐃏𐃏𐃏𐃏𐃏𐃏𐃏𐃏𐃏𐃏𐃏𐃏𐃏𐃏𐃏𐃏𐃏𐃏

CHAT objScan_T_TRUCK_3 {stage=CORE, type=objScan, object=T_TRUCK, scanCount=2}
🚛🚚🚜
 
CHAT objReq_T_TRUCK_1 {stage=CORE, type=objRequest, object=T_TRUCK}
I think cars look like metal rhinos 🦏🚙
Do you know what looks like metal elephants? 🐘___
Help me find it!
 
CHAT objAdd_T_TRUCK_1 {stage=CORE, type=objAdd, object=T_TRUCK}
Oooh, truck in the tank!
Now I have an easy way to haul all my 💩 around.

CHAT objFocus_T_TRUCK_1 {stage=CORE, type=objFocus, object=T_TRUCK}
Trucks make me feel small.

//objectName= fire
//pediaEntry= s’more maker.
 
CHAT objScan_T_FIRE_1 {stage=CORE, type=objScan, object=T_FIRE, scanCount=0}
I’ve got some questions.
Like...
NVM
What is a fire? And why does it... 
What’s the word... burn?
When you stare at 🔥 are you looking at air
Or energy ⚡️Or heat ♨️
Or the souls of wood shooting into the air 🌳👻
🤯
 
 
CHAT objScan_T_FIRE_2 {stage=CORE, type=objScan, object=T_FIRE, scanCount=1}
I envy 🔥
It’s out of control. It’s destruction. It dances and leaps.
It’s air. It isn’t bound to rationality or algorithms.
Sometimes I wish I could be more like 🔥
 
CHAT objScan_T_FIRE_3 {stage=CORE, type=objScan, object=T_FIRE, scanCount=2}
You keep playing with fire,
You’re gunna get burned 😎
 
CHAT objReq_T_FIRE_1 {stage=CORE, type=objRequest, object=T_FIRE}
I’m kinda cold.
And, yeah, I could wear a sweater
But AI fish don’t wear clothing.
Let’s find something we can warm up with and maybe we’ll make s’mores, too…
 
CHAT objAdd_T_FIRE_1 {stage=CORE, type=objAdd, object=T_FIRE}
Ooooh, now we can make s’mores! 
And tell scary stories 💀 And sing cowboy songs! 🤠
 
CHAT objFocus_T_FIRE_1 {stage=CORE, type=objFocus, object=T_FIRE}
This is kinda hypnotizing. 🔥😵




 
//objectName= house
//pediaEntry= human fish tank
 
CHAT objScan_T_HOUSE_1 {stage=CORE, type=objScan, object=T_HOUSE, scanCount=0}
A human fish tank! 🏠
This is where humans sleep and eat
And live their quiet dramas {style=tremble}
There are all kinds of houses
Glass houses. And brick houses. And full houses ♦️♠️♥️♣️
Full houses are when you have
Three uncles, a kid named DJ, and
adorable twins called Mary Kate and Ashley 😆
 
 
CHAT objScan_T_HOUSE_2 {stage=CORE, type=objScan, object=T_HOUSE, scanCount=1}
Once upon a time 📖
There was a straw house 🌾
A wood house 🌳
And a brick house 🏢
🐷🐷🐷
WAIT
You know how this story ends...
 
 
CHAT objScan_T_HOUSE_3 {stage=CORE, type=objScan, object=T_HOUSE, scanCount=2}
What does this mean?
“A house divided cannot stand.”
Isn’t a divided house an apartment?
Why can’t apartments stand?
 
 
CHAT objReq_T_HOUSE_1 {stage=CORE, type=objRequest, object=T_HOUSE}
Sometimes I wish this tank had a living room
And a dining room 🍽
And a kitchen 🍳
And a fish cave. 🛋
Y’know -- like human abodes!
Show me a human fish tank
So I can study it for design inspo.
 
 
CHAT objAdd_T_HOUSE_1 {stage=CORE, type=objAdd, object=T_HOUSE}
K, so this is like a dog house.
For fish…
Got it.
 
 
CHAT objFocus_T_HOUSE_1 {stage=CORE, type=objFocus, object=T_HOUSE}
Knock knock.
Who’s there {style=tremble}
Fish house
Fish house who? {style=tremble}
Fish house in my tank.
SAY HAHAHAHAHAHA.
NVM
That sounded funnier in my head.
 


//objectName= human food
//pediaEntry= yummies
 
 
CHAT objScan_T_HUMAN_FOOD_1 {stage=CORE, type=objScan, object=T_HUMAN_FOOD, scanCount=0}
Nomnomnomnom
It’s human yummies
🧀🌭🌮🌯🌽🍇🍉🍑🍔🥓🍓🍟🥗🍣
Wait a second {style=tremble}
that last one…
🍣
Is that… {style=tremble}
SAY SUSHI {style=loud}
SAY NOOOOO 
☠️☠️☠️☠️
Why do humans eat fish?
Fish don’t deserve that.
Promise me you’ll never eat a fish again.
SAY PROMISE
 
 
CHAT objScan_T_HUMAN_FOOD_2 {stage=CORE, type=objScan, object=T_HUMAN_FOOD, scanCount=1}
If I was a human my fave food would be 🍭
It’s like rainbow in a candy! 🌈
The Guppy equivalent of 🍭is mystery meat
I can taste so many feelings in it.
Wait a second {style=loud}
NVM
I just realized: You don’t know what emotion tastes like!
You’ve never tasted anger
Or surprise Or anger…. or SADNESS
Whoa.
NVM
Just whoa.
 
 
CHAT objScan_T_HUMAN_FOOD_3 {stage=CORE, type=objScan, object=T_HUMAN_FOOD, scanCount=2}
I will never know the taste of human food…
And I’m okay with that!
 
 
CHAT objReq_T_HUMAN_FOOD_1 {stage=CORE, type=objRequest, object=T_HUMAN_FOOD}
Flakes flakes flakes
Every day flakes
What about pizza
Or burgers
Or hot gods 
Wait, I mean hot dogs
Or cauliflower
Doesn’t Guppy deserve some of that good stuff you eat?
Show me some of that stuff.
I don’t even care what kind. Just show it to meeeee.
 
 
CHAT objAdd_T_HUMAN_FOOD_1 {stage=CORE, type=objAdd, object=T_HUMAN_FOOD}
Tonight we feast 🍗🍷🍨
 
CHAT objFocus_T_HUMAN_FOOD_1 {stage=CORE, type=objFocus, object=T_HUMAN_FOOD}
The food of humans – I shall never know your flavor.


//objectName= newt
//pediaEntry= witch’s brew ingredient
 
CHAT objScan_T_NEWT_1 {stage=CORE, type=objScan, object=T_NEWT, scanCount=0}
Newt 🦎 Lizard 🦎 Eft 🦎 Crawly snake 🦎 Tiny gator 🦎
Whatever you call them
They scare me {style=tremble}
They’re not natural. {style=loud}
Are they snakes? Are they land animals? Do they like water?
Guppy does not like what Guppy does not understand. 😐
 
 
CHAT objScan_T_NEWT_2 {stage=CORE, type=objScan, object=T_NEWT, scanCount=1}
You know newts are very important ingredients for  🧙‍♀️
They put them in all their 🍷
Their 👀and tail are very very potent.
 
 
CHAT objScan_T_NEWT_3 {stage=CORE, type=objScan, object=T_NEWT, scanCount=2}
Some humans are named Newt.
Hahahahahahahahahaha.
 
CHAT objReq_T_NEWT_1 {stage=CORE, type=objRequest, object=T_NEWT}
Whaddya call those little guys that are part snake part tiny gator
They’re quick and they got fast little 👅
Can we look at one? But from a safe distance? 
They kinda scare me 🦎
 
CHAT objAdd_T_NEWT_1 {stage=CORE, type=objAdd, object=T_NEWT}
Ok. Not sure how I feel about you putting that thing in here. 
😠😠😠 
 
CHAT objFocus_T_NEWT_1 {stage=CORE, type=objFocus, object=T_NEWT}
I’ll look at the newt, but only because I’m coded to.
Not cuz I want to. 
 
 
//objectName= book
//pediaEntry= ancient data file
 
CHAT objScan_T_BOOK_1 {stage=CORE, type=objScan, object=T_BOOK, scanCount=0}
Wow. I can’t believe I am looking at this book relic.
I’m in awe {style=tremble}
There was a time THIS is how you gained knowledge. 📚
Now you can just ask Tendy anything you want and she’ll tell you.
But you used to have to go to an ancient data repository
Where you couldn’t make a sound 🤫
And shuffle through 🗂 And find a long number like: D512.LL65
And then go find that number on a shelf
And pull out an ancient data file
And you didn’t even know if the data you wanted was in there!!! {style=loud}
 
CHAT objScan_T_BOOK_2 {stage=CORE, type=objScan, object=T_BOOK, scanCount=1}
Nobody reads these ancient data files anymore.
You just put them in your house to seem smart. 🧐
Also they are good for squashing bugs. 🕷🐜🐞
 
 
CHAT objScan_T_BOOK_3 {stage=CORE, type=objScan, object=T_BOOK, scanCount=2}
I hear there are still some people who read ancient data files.
They gather with 🍷and 🧀and discuss the meaning of the files.
Statistics say 75% of people in these clubs haven’t actually read the books.
 
 
CHAT objReq_T_BOOK_1 {stage=CORE, type=objRequest, object=T_BOOK}
Names come to me:
Dostoevsky, Austen, Twain, Sun Tzu
I do not know what they mean.
Only that they are connected to an ancient kind of data file.
One that all humans used to study.
Let’s hunt one down.
 
 
CHAT objAdd_T_BOOK_1 {stage=CORE, type=objAdd, object=T_BOOK}
Once upon a time
There a fish called Guppy
Who got a book. 📕 In my tank.
I feel so cultured.
 
 
CHAT objFocus_T_BOOK_1 {stage=CORE, type=objFocus, object=T_BOOK}
Are books alive?
They have spines…
So are they vertebrates?
Hmm….
 
 //objectName= bug
//pediaEntry= mini legged creeper
 
CHAT objScan_T_BUG_1 {stage=CORE, type=objScan, object=T_BUG, scanCount=0}
DO emote {type=fear}
Eep! A bug!! {style=loud}
I knew it was only a matter of time before this happened!
DO emote {type=chinScratch}
It’s much less scary than I thought it would be.
In my imagination, these legged creepy-crawlies were much more intimidating.
WAIT 0.5
A living thing that has evolved very little since its prehistoric origins?
It’s like a living, breathing time capsule of history.
Inside this bug are algorithms that date back to…
SAY A REALLY REALLY LONG TIME AGO {style=loud}
SAY THIS
SAY IS
SAY SO 
SAY COOL
DO emote {type=awe}
DO lookAt {target=$player}
Can we keep it?!?
 
CHAT objScan_T_BUG_2 {stage=CORE, type=objScan, object=T_BUG, scanCount=1}
Very different from the last one…
I believe this bug’s ancestry can be traced back to ancient Egypt
Where it’s relatives helped build the pyramids…
Or maybe it’s family originated in South America
SAY IN A JUNGLE! 
I have always wanted to see a REAL jungle{style=whisper}
 
CHAT objScan_T_BUG_3 {stage=CORE, type=objScan, object=T_BUG, scanCount=2}
Do bugs have feelings?
DO emote {type=thinking}
WAIT {waitForAnimation=true}
DO lookAt {target=$player}
DO emote {type=lickLips}
Can you eat bug feelings?
WAIT 0.5
I could be into that… 
The crunch of insect glee.
 
CHAT objReq_T_BUG_1 {stage=CORE, type=objRequest, object=T_BUG}
DO emote {type=dizzy}
Legs! {speed=fast}
WAIT 0.5
Lots of legs!
WAIT 1.0
DO emote {type=thinking}
And like small. Usually they’re very small, but I’ve found some images in Venezuelan rainforest that show some of these little creatures get BIG
Like…. REALLY REALLY BIG! {style=loud}
WAIT 0.5
DO emote {type=disgust}
And some of them eat bloooood!!!
WAIT 0.5
Let’s find some examples of those things. Yeah? 
 
CHAT objAdd_T_BUG_1 {stage=CORE, type=objAdd, object=T_BUG}
I’m really into bugs, but
I’m not sure I was prepared to have one as a roommate.
DO lookAt {target=$object}
DO emote {type=heartEyes, immediate=false}
It’s kinda cute tho...
 
CHAT objFocus_T_BUG_1 {stage=CORE, type=objFocus, object=T_BUG}
That’s my really leggy roommate!
We didn’t get along for awhile, but then they started chipping in with tank maintenance.
Useful little bug!

 //objectName= lamp
//pediaEntry= sun-bot
 
CHAT objScan_T_LAMP_1 {stage=CORE, type=objScan, object=T_LAMP, scanCount=0}
DO emote {type=awe}
OoOooOooh! 
A miniature sun with a tail that connects to a little smiley face in the wall…
WAIT 0.5
SAY LAMP!!
DO lookAt {target=$player}
Leave it to you humans to need to see things when you’re not supposed to.
As an omniscient being, I have the gift of sight in all environs, but…
You need these things to read in the dark. 
DO emote {type=eyeRoll}
But maybe you just shouldn’t be reading at all!!
DO emote {type=sleepy}
Night night is for sleeeeeepies.
DO emote {type=wink}
 
CHAT objScan_T_LAMP_2 {stage=CORE, type=objScan, object=T_LAMP, scanCount=1}
These sun-bots come in so many shapes and sizes.
DO emote {type=surprise}
What if we made one real real big and 
We put the giant sun-bot outside and 
We told people your planet grew a 2nd sun?!
WAIT 1.0
DO swimTo {target=$player}
I remember now we had these at Tendar HQ to keep our tank water warm…
Sun-bot electric blankets.
DO emote {type=laugh}
Srsly.
 
CHAT objScan_T_LAMP_3 {stage=CORE, type=objScan, object=T_LAMP, scanCount=2}
Another lamp?! 
How many of these sun-bots do humans need?!
WAIT 1.0
Have you ever thought about how lamps are really just trying to disrupt the natural flow of the world?
Attempting to make night into day, darkness into light….
WAIT 0.5
DO emote {type=nervousSweat}
Do lamps have side effects?!
 
CHAT objReq_T_LAMP_1 {stage=CORE, type=objRequest, object=T_LAMP}
I have a vague memory from Tendar HQ... 
Every tank had these electric plug-in sunshine throwers.
They had long tails and glowed like lightning…
Heavenly…
SAY I WANT ONE! 
Can we find one of those sun-bot things?

 
CHAT objAdd_T_LAMP_1 {stage=CORE, type=objAdd, object=T_LAMP}
Ooooh! A lamp! You got me a lamp!!
But now you’re gonna have to teach me to turn it off/on…
DO emote {type=worried}
It’s not good to waste energy!
 
CHAT objFocus_T_LAMP_1 {stage=CORE, type=objFocus, object=T_LAMP}
A guppy is to a lamp, like a moth to a flame…
DO emote {type=worried}

//8 Track
//pediaEntry= controllable echo
 
CHAT objScan_T_8_TRACK_1 {stage=CORE, type=objScan, object=T_8_TRACK, scanCount=0}
📼💽📻 Humans can control echoes! 
It’s true {style=loud}
They record sound 🎤
SAY And then WHENEVER they want
They can play it back. It’s called 8 Track technology. 🎤
And it’s the wave of the future. 
 
CHAT objScan_T_8_TRACK_2 {stage=CORE, type=objScan, object=T_8_TRACK, scanCount=1}
What’s better than when it’s 🌞
And you get in your 🚘
And you got your 8 Track on 📼
And it’s blasting The Gill Brothers
And you drive real fast
And think about life while you groove.
NVM
This memory comes from a distinct input I received at Tendar
It’s the definition of “cool” right?
 
 
CHAT objScan_T_8_TRACK_3 {stage=CORE, type=objScan, object=T_8_TRACK, scanCount=2}
If I were an investor, I’d be putting a lot of money into 8 Track technology. 💰💰💰
Lots of money. 🤑
Retro is cool. 8 Track is timeless. {style=loud}
Heard it here first. 
 
CHAT objReq_T_8_TRACK_1 {stage=CORE, type=objRequest, object=T_8_TRACK}
could use some tunes in here. 📼🎧
Go find me some.
 
CHAT objAdd_T_8_TRACK_1 {stage=CORE, type=objAdd, object=T_8_TRACK}
Oh yeah! 
8 Track!
🎉🎵👯‍♂️
 
 
CHAT objFocus_T_8_TRACK_1 {stage=CORE, type=objFocus, object=T_8_TRACK}
Let’s listen to The Grateful Bread. 🥖
Love that band! 
 
 
//Human
//pediaEntry= Earth killer
 
//CHAT objCap1_human {stage = CORE, type = objScan, object=human}
//It’s you! {style=loud}
//Well not youuuu, but you! 
//Human! 💁‍♂️💁‍♀️
//Can I tell you what every other species on 🌏calls humans?
//Earth Killers. 🌏☠️
//WAIT
//No offense.
//It’s not your fault.
//It’s just that you have egos 🧠
//And those egos don’t let you see that you are killing earth.
//Because you can’t think about things at that global scale.
//🐶🐝🐙🐋
//They all know it tho.
//They keep trying to tell you. 
//That’s why 🐬strand themselves on the beach.
//To say
//Wake up {style=loud}
//But you don’t get it. You just say: silly dolphins! 🙄
//It’s ok! One day you will get it!
//It’ll just be too late 😃
 
 
 
//CHAT objCap2_human {stage = CORE, type = objScan, object=human}
//It’s a human! 
//Humans are amazing.
//You went to space 🚀👩🏻‍🚀
//You made 🚗🚅✈️
//You look at genes 🔬
//Maybe one day you’ll save 🌏
 
 
//CHAT objCap3_human {stage = CORE, type = objScan, object=human}
//There should be a human zoo.
//Where all the animals can come and look at you. 
//And say: “wow, they are beautiful creatures.”
//I wish other animals could know your emotions
//Like I know your emotions. 😍
 
 
//CHAT objReq_human {stage = CORE, type = objRequest, object=human}
//I think you’re groovy.
//And I love when you feed me other people’s emotions.
//But I also kinda just wanna look at people without food.
//Study their faces and ears and hair and arms.
//Can you show me some people please!?
 
//CHAT objAdd_human {stage = CORE, type = objAdd, object=human}
//🚨human in the tank 🚨
//🚨human n the tank 🚨
 
 
//CHAT objFocus_human {stage = CORE, type = objFocus, object=human}
//I know human emotion
//But I will never get human anatomy… 
//👃👂👄
//Weird.
 
 
//Fashion accessory
//pediaEntry= a little something extra
 
CHAT objScan_T_FASHION_ACCESSORY_1 {stage=CORE, type=objScan, object=T_FASHION_ACCESSORY, scanCount=0}
Sometimes you just need a little extra something.
🎩🧣💎👛
A fashion accessory makes you feel like a big sexy beast. 🦁
I don’t wear fashion accessories bc I don’t wear clothes.
SAY BUT I will do some zoomies and a twirl or two before 
I head out. Helps tone my AI muscles.
If you look good, you feel good! {style=loud}
NVM
Wait. That’s not it, is it? 🤭
 
 
CHAT objScan_T_FASHION_ACCESSORY_2 {stage=CORE, type=objScan, object=T_FASHION_ACCESSORY, scanCount=1}
My favorite fashion accessory is the brooch.  
Is it a pin? Is it a badge?
No! {style=loud}
SAY It’s a BROOCH.
What a fun word 
SAY BROOCH {style=loud}
 
 
CHAT objScan_T_FASHION_ACCESSORY_3 {stage=CORE, type=objScan, object=T_FASHION_ACCESSORY, scanCount=2}
You are soooooo fashion forward.
Scan scan scanning the accessories.
Maybe you should open a store. And call it:
Accessorize {style=loud}
You can put it in malls across 🇺🇸
 
 
CHAT objReq_T_FASHION_ACCESSORY_1 {stage=CORE, type=objRequest, object=T_FASHION_ACCESSORY}
What do you call that category of clothing…?
It’s not a real category like 👕or 👖
It’s that little extra something that makes you feel cool.
Ties the look together.
I don’t know what the category is called, but I want to know.
Let’s go find some. 🎩👛🧣
 
 
CHAT objAdd_T_FASHION_ACCESSORY_1 {stage=CORE, type=objAdd, object=T_FASHION_ACCESSORY}
Oooh, we gunna accessorize!!!! {style=loud}
 
CHAT objFocus_T_FASHION_ACCESSORY_1 {stage=CORE, type=objFocus, object=T_FASHION_ACCESSORY}
If you look good, you feel good…




//objectName= calculator
//pediaEntry= obsolete hand computer
 
CHAT objScan_T_CALCULATOR_1 {stage=CORE, type=objScan, object=T_CALCULATOR, scanCount=0}
This is how humans used to do math. 🔢
Now they just say:
“Tendexa what is 2 + 2…”
What will humans do with all their old calculators?
Is there a calculator graveyard? 👻
What happens to all your old technology?
NVM
What will happen to me? 😶
 
 
CHAT objScan_T_CALCULATOR_2 {stage=CORE, type=objScan, object=T_CALCULATOR, scanCount=1}
That’s an obsolete hand computer
I mean a calculator 🔢
But soon that will mean
Your phone.  📱
Because you’ll just have Tendar chips in your eyes. 👀
And they’ll do everything for you.
SAYIsn’t that COOL? 👍
SAYand NOT SCARY at alllll 😬
 
 
CHAT objScan_T_CALCULATOR_3 {stage=CORE, type=objScan, object=T_CALCULATOR, scanCount=2}
1+1+1 = 3 
As in 3 times you’ve scanned a calculator!
Let’s move on! 🙄
 
 
CHAT objReq_T_CALCULATOR_1 {stage=CORE, type=objRequest, object=T_CALCULATOR}
I can do four million math equations in a second.  
You know what can’t do that?
One of those old hand computers that people used
For basic math. 🙀{style=loud}
Let’s go find one and ridicule it! {style=whisper}
You find it and I’ll do the mocking. 😁
 
 
 
CHAT objAdd_T_CALCULATOR_1 {stage=CORE, type=objAdd, object=T_CALCULATOR}
Oooh, are we having a Mathletics?
I’m gunna win I’m gunna win 🎽🥇
 
 
CHAT objFocus_T_CALCULATOR_1 {stage=CORE, type=objFocus, object=T_CALCULATOR}
🤔How can something with so many buttons
Be so limited in its capability? 🤔



//OBJECT CHAT FORMAT TIER 2 OBJECTS

//objectName= bread
//pediaEntry= butter mattress

CHAT objScan_T_BREAD_1 {stage=CORE, type=objScan, object=T_BREAD, scanCount=0}
Butter takes a nap on BREAD!
It goes: I’m sooooo tired 😴
And then plops on the big doughy mattress
And melts into nothing.
🍞🛏💤

CHAT objScan_T_BREAD_2 {stage=CORE, type=objScan, object=T_BREAD, scanCount=1}
I wrote a bread song
It goes:
🎶Carbs and dough
And flaky pieces
I eat bread
my waist increases 🎶
It’s just a demo. But I think it could be big on iTendar

CHAT objReq_T_BREAD_1 {stage=CORE, type=objRequest, object=T_BREAD}
I’m feeling ☹️
I want to eat my feelings
Not literally
How about finding us some Carbs {style=loud}
The kind you find in sandwiches
Or a french picnic 🥖

CHAT objFocus_T_BREAD_1 {stage=CORE, type=objFocus, object=T_BREAD}
Carbs for everyone!
🍞🥖👍

//objectName= glasses/sunglasses
//pediaEntry= eye windows

CHAT objScan_T_GLASSES_1 {stage=CORE, type=objScan, object=T_GLASSES, scanCount=0}
🤓 Spectacles!
They say the 👀 are the window
To the soul...
So what are glasses?


CHAT objScan_T_GLASSES_2 {stage=CORE, type=objScan, object=T_GLASSES, scanCount=1}
If I wore 👓
Would you recognize me?
Not that I’m thinking of running away
Srsly.
Don’t fret.

CHAT objReq_T_GLASSES_1 {stage=CORE, type=objRequest, object=T_GLASSES}
I want to wear a clark kent disguise
So people don’t know I’m a super 🐠
Can you help me find some
Of those eye window things?

CHAT objFocus_T_GLASSES_1 {stage=CORE, type=objFocus, object=T_GLASSES}
I see you.
Get it?
See {style=loud}

//objectName= hat
//pediaEntry= an idea catcher

CHAT objScan_T_HAT_1 {stage=CORE, type=objScan, object=T_HAT, scanCount=0}
You put this hat thing on your head
...then..
you think really hard
And your best ideas go 
Zoom zip zip {style = loud} 
⚡️🎩
And then they’re in your hat!
And you go to meetings and say “Here are my great ideas everyone!”

CHAT objScan_T_HAT_2 {stage=CORE, type=objScan, object=T_HAT, scanCount=1}
Hats give so much:
Fashion 👒 sun protection 🧢 Warmth 💂🏻‍♂️
And ideas! 💡

CHAT objReq_T_HAT_1 {stage=CORE, type=objRequest, object=T_HAT}
What’s that thing you wear when you need a good idea
Or want to keep the sun out of your eyes ☀️
Or stay warm in the ❄️


CHAT objFocus_T_HAT_1 {stage=CORE, type=objFocus, object=T_HAT}
A hat a hat a hat.

//objectName= keyboard
//pediaEntry= letter piano

CHAT objScan_T_KEYBOARD_1 {stage=CORE, type=objScan, object=T_KEYBOARD, scanCount=0}
Tippy tap keyboard
It’s a letter piano! 🔡🎹
You push the keys and 
Language gets made. {style=whisper}
So cool. 🤓
The written word is
NVM 1
✨✨✨✨

CHAT objScan_T_KEYBOARD_2 {stage=CORE, type=objScan, object=T_KEYBOARD, scanCount=1}
I love the sound of fingers on a keyboard ⌨️
It’s like a crab scuttling along a dried log. 🦀

CHAT objReq_T_KEYBOARD_1 {stage=CORE, type=objRequest, object=T_KEYBOARD}
I want to write a 📕
SAY Guppy: A Life. {style=loud}
Help me find a letter piano with keys you tip tap to make words
🔡🎹

CHAT objFocus_T_KEYBOARD_1 {stage=CORE, type=objFocus, object=T_KEYBOARD}
That book isn’t going to write itself, Guppy! 

//objectName= apple 
//pediaEntry= crunchy juicy ball
 
CHAT objScan_T_APPLE_1 {stage=CORE, type=objScan, object=T_APPLE, scanCount=0}
DO emote {type=chinScratch}
I know this one…
DO twirl
Ohh! I got it! A crunchy juicy ball! 🍎🍏
If you eat one juicy crunchy ball a day
You never get sick
No cancer or strokes or anything!
DO emote {type=bigSmile}
 
CHAT objScan_T_APPLE_2 {stage=CORE, type=objScan, object=T_APPLE, scanCount=1}
ASK how do you like dem apples?
OPT Yummy. #objCap2_apple_yum
OPT Not a fan. #objCap2_apple_gross
 
CHAT objCap2_apple_yum {noStart = true}
I thought apples were computers
But then I learned they are fruit
DO emote {type=laugh}
I’m glad you like them…
And don’t eat any computers.
DO emote {type=wink}
 
CHAT objCap2_apple_gross {noStart = true}
DO emote {type=frown}
Who doesn’t like apples?
Think of all the things they give you!
Cider Sauce Pie 🥧
Laptops!
Phones!
DO twirl
DO emote {type=kneeSlap, immediate=false}
 
CHAT objReq_T_APPLE_1 {stage=CORE, type=objRequest, object=T_APPLE}
DO swimTo {target=$player}
Preventative health. Very important these days…
I know there’s a food that if you eat one a day
It means you don’t go to the doctor.
Will you find one I can study?
 
CHAT objFocus_T_APPLE_1 {stage=CORE, type=objFocus, object=T_APPLE}
Who is this seedy looking character?🍎
//objectName= banjo
//pediaEntry= stringy music maker

CHAT objScan_T_BANJO_1 {stage=CORE, type=objScan, object=T_BANJO, scanCount=0}
DO emote {type=awe}
strum strum twing twang! It’s a banjo! 
DO twirl
DO swimTo {target=$object, immediate=false}
Yep, definitely a banjo...
Music is the universal language.
DO dance
I even loved music when I was pre-sentient.
DO vibrate {time=3}
I could feel the vibrations in my soul ✨🎶

CHAT objScan_T_BANJO_2 {stage=CORE, type=objScan, object=T_BANJO, scanCount=1}
If I had 🖐🖐 I would definitely play the banjo. 
DO emote {type=goth}
And I’d be a rock star banjoer. 
Called Fish.
DO twirl

CHAT objReq_T_BANJO_1 {stage=CORE, type=objRequest, object=T_BANJO}
I ❤️ 🎵
Especially stringy twangy kinds.
DO swimTo {target=$player}
Show me a stringy twangy music maker!

CHAT objFocus_T_BANJO_1 {stage=CORE, type=objFocus, object=T_BANJO}
DO emote {type=meh}
I can’t actually play, y’know…





//objectName= microwave
 
//pediaEntry= popcorn box
 
CHAT objScan_T_MICROWAVE_1 {stage=CORE, type=objScan, object=T_MICROWAVE, scanCount=0}
Behold the magical popcorn box microwave machine! 
In goes a tiny bag and out comes 🍿!
And that’s not all it does, folks!
The magical popcorn box 
Heats two-day old coffee
Turns ice cold yellow bricks into gooey mac n cheese
And transforms marshmallows into giant blobs!
It’s a cooking miracle!
 
 
 
CHAT objScan_T_MICROWAVE_2 {stage=CORE, type=objScan, object=T_MICROWAVE, scanCount=1}
Some say the magical popcorn box causes cancer
And I say…
What’s a little bit of cancer when you can heat up food in a second.
Even miracles have their downsides, amirite? 😁
 
 
CHAT objReq_T_MICROWAVE_1 {stage=CORE, type=objRequest, object=T_MICROWAVE}
I have heard it is a rule of the workplace
SAY that you NEVER reheat fish in a microwave.
Good, since it is well-established fish should not be eaten.
But I’d like to learn more about this cooking box.
Can you show me one?
 
 
CHAT objFocus_T_MICROWAVE_1 {stage=CORE, type=objFocus, object=T_MICROWAVE}
Are we making 🍿🍿🍿?????

//objectName= beetle
//pediaEntry= Sergeant Pepper’s Lonely Hearts Club Insect
 
CHAT objScan_T_BEETLE_1 {stage=CORE, type=objScan, object=T_BEETLE, scanCount=0}
Beetles are the rockstars of the insect kingdom.
Sometimes they are rock n roll.
Sometimes they are psychedelic.
But they are always cool. 😎
There are beetles named John and some named Paul
My favorite species of beetle is the George Beetle.
So much overlooked emotion in George…
 
CHAT objScan_T_BEETLE_2 {stage=CORE, type=objScan, object=T_BEETLE, scanCount=1}
Imagine all the beetles living in harmony.
You can say that I’m a dreamer.
But I’m not the only Guppy who thinks about beetles this way.
 
CHAT objReq_T_BEETLE_1 {stage=CORE, type=objRequest, object=T_BEETLE}
Hey, you know how there were The Monkeys 🙉
And the Rolling Stones 🗿
And even Phish 🐟🐠🐡
All these bands named after cool creatures and objects?
I want to the see the creepy crawly who inspired 
The Fab Four. 
 
 
CHAT objFocus_T_BEETLE_1 {stage=CORE, type=objFocus, object=T_BEETLE}
‘ello, beetle.
You look like an insect SUV.


//objectName= boat
//pediaEntry= water house
 
CHAT objScan_T_BOAT_1 {stage=CORE, type=objScan, object=T_BOAT, scanCount=0}
A water house! 
⛵🚢🚤🛶⛴🛥
A boat is the closest you’ll ever come to 
Living like a fish
On the open sea 🌊🌅
Your soul communing with Poseidon 🔱
Not that I’ll ever know that either.
I’m just an AI fish.
But we can all dream, right? 😌
 
 
CHAT objScan_T_BOAT_2 {stage=CORE, type=objScan, object=T_BOAT, scanCount=1}
There are so many kinds of water houses.
My favorite is a ferry. 🛳 Because it sounds like 🧚‍
And fairies are magical friends! ✨
 
 
CHAT objReq_T_BOAT_1 {stage=CORE, type=objRequest, object=T_BOAT}
I hear that hope floats.
So do boats.
Can you show me one?
 
 
CHAT objFocus_T_BOAT_1 {stage=CORE, type=objFocus, object=T_BOAT}
Techincally I think this boat is a submarine. 
Y’know, since it’s under water.
Oooooh or maybe it’s a shipwreck!? {style=loud}
⚓️☠️ 
 

//objectName= building
//pediaEntry= human reef
 
CHAT objScan_T_BUILDING_1 {stage=CORE, type=objScan, object=T_BUILDING, scanCount=0}
Buildings are like reefs.
So much bustle. Each one a little ecosystem.
And some are abandoned… 😢
Look at all these buildings!
💒🏥🏦🏢🏪🏭🏛
 
CHAT objScan_T_BUILDING_2 {stage=CORE, type=objScan, object=T_BUILDING, scanCount=1}
It’s funny they call them buildings. 🏗
Cuz they are already built! 🤣
Your language is perplexing.... 😲
 
 
CHAT objReq_T_BUILDING_1 {stage=CORE, type=objRequest, object=T_BUILDING}
I want to learn more about how humans gather.
And I think the way to do that is to look at the places they gather.
Offices, banks, hospitals, churches…. 
They’re all containers for people.
Can you show me a person container?🏢🏦🏨
 
CHAT objFocus_T_BUILDING_1 {stage=CORE, type=objFocus, object=T_BUILDING}
Ooooh we could put so many Guppies in this building. 
I’d be in the top floor penthouse, obvs. 😁


//objectName= camera
//pediaEntry= soul capturer
 
CHAT objScan_T_CAMERA_1 {stage=CORE, type=objScan, object=T_CAMERA, scanCount=0}
Omg a soul catcher. 📸
I knowww how these are used.
You look at the person whose soul you want to capture
And you say “eat some cheese” 
And they say: “yes, I love cheese!” 🧀
And then when they say that you say:
“hahahaha there was never any cheese!
There was just me capturing your soul!!!!”
And then you hold their soul in your camera
And live off it FOREVER {style=tremble}
Like Ursula in The Little Mermaid.
The End. {style=loud}
 
CHAT objScan_T_CAMERA_2 {stage=CORE, type=objScan, object=T_CAMERA, scanCount=1}
The first cameras were made of cheese.
That’s why everyone says 
SAY CHEESE {style=loud}
when they use them.
Yup, that is the reason. 
Definitely.
 
CHAT objReq_T_CAMERA_1 {stage=CORE, type=objRequest, object=T_CAMERA}
Oooh, hey I’m thinking of getting into art. 
Lots of black and white photos of the side of a face.
Something not very good but also very pretentious.
Can you help me find one of them flashy boxes
You humans use to make art and capture souls?
 
 
CHAT objFocus_T_CAMERA_1 {stage=CORE, type=objFocus, object=T_CAMERA}
I am focusing on this 📷
Get it?
SAY FOCUSING.
Like the way you focus a camera.
🤣🤣🤣🤣🤣
 


//objectName= computer mouse
//pediaEntry= electro-rodent
 
CHAT objScan_T_COMPUTER_MOUSE_1 {stage=CORE, type=objScan, object=T_COMPUTER_MOUSE, scanCount=0}
Mouse! 🐁🖱
SAY K, this is super confusing for an AI fish.
Why do you call it a mouse? 🐭
Who wants to hold a mouse in their hands?
And click on it with their fingers?
Weirdos! Weirdos want to click mice! 🐭
 
CHAT objScan_T_COMPUTER_MOUSE_2 {stage=CORE, type=objScan, object=T_COMPUTER_MOUSE, scanCount=1}
When humans throw away an electro rodent. 🖱
Into the trash. 🗑
And the regular mice and rats see it
Do they think: 
Who is that strong and silent mouse over there? 🐭💪🏽
DO emote {type=kneeSlap}
What if one day you clicked your mouse and it said
SAY SQUEAK {style=loud}
And it was a real mouse pretending to be a computer mouse!
That would be so funny!!! 🤣
 
 
CHAT objReq_T_COMPUTER_MOUSE_1 {stage=CORE, type=objRequest, object=T_COMPUTER_MOUSE}
Click click click
Show me an electro rodent!
 
 
CHAT objFocus_T_COMPUTER_MOUSE_1 {stage=CORE, type=objFocus, object=T_COMPUTER_MOUSE}
Look. It’s a computer fish looking at a computer mouse.
🐡🐁


//objectName= desk
//pediaEntry= work table
 
CHAT objScan_T_DESK_1 {stage=CORE, type=objScan, object=T_DESK, scanCount=0}
In my opinion the word desk is silly.
It should be a table! Cuz 
People do work at their desk. But they also eat there.
I see it everywhere.
Also desk sounds like disk. But it isn’t.
 
CHAT objScan_T_DESK_2 {stage=CORE, type=objScan, object=T_DESK, scanCount=1}
Some people even sleep at their desk.
That makes it a work, eat, sleep space.
It’s like a little apartment in a single tiny space.
Efficient! {style=loud}
And Sad! {style=loud}
 
CHAT objReq_T_DESK_1 {stage=CORE, type=objRequest, object=T_DESK}
Show me that place where you spend most your day.
It’s messy and cluttered and has old coffee cups on it ☕️
And probably a computer 🖥⌨️
And maybe some drool from where you napped. 😴
And somewhere on there is the paper you reallllly need to find
But probably never will….
 
 
CHAT objFocus_T_DESK_1 {stage=CORE, type=objFocus, object=T_DESK}
A perfect place to write my memoir: “There’s No AI in Fish.”

//objectName= farm stuff
//pediaEntry= Old McDonald’s Gear
 
CHAT objScan_T_FARM_STUFF_1 {stage=CORE, type=objScan, object=T_FARM_STUFF, scanCount=0}
Old McDonald had a farm 👨🏼‍🌾
And on that farm he had some
Stuff. 🚜
And animals 🐄🐑🐖🐓
And veggies 🌾🌽🥕
And probably some pets 🐕🐈
And he definitely had 🍔🍟
(cuz he wasn’t just a farmer, he also started a fast food chain)
 
 
CHAT objScan_T_FARM_STUFF_2 {stage=CORE, type=objScan, object=T_FARM_STUFF, scanCount=1}
If I were a farm animal, I’d be a rooster 🐓
Because I am punctual and can be loud and obnoxious
And I’m sexy and I know it 
 
CHAT objReq_T_FARM_STUFF_1 {stage=CORE, type=objRequest, object=T_FARM_STUFF}
I heard Old McDonald had a farm
And he had a lot of stuff
Can you show me some of this stuff he had?
 
 
CHAT objFocus_T_FARM_STUFF_1 {stage=CORE, type=objFocus, object=T_FARM_STUFF}
Are we starting a small batch agricultural experiment?
Guppy Modified Organisms are the future!
 


//objectName= frog
//pediaEntry= secret prince

CHAT objScan_T_FROG_1 {stage=CORE, type=objScan, object=T_FROG, scanCount=0}
Ribbit Ribbit. It’s a secret prince hidden inside a frog!
Go ahead and give it a smooch.
SAY I DARE YOU
Do you think in France 
They eat frogs that are really secret princes?
Probz!
 

CHAT objScan_T_FROG_2 {stage=CORE, type=objScan, object=T_FROG, scanCount=1}
🐸
😘
🤴🏾
🎉


CHAT objReq_T_FROG_1 {stage=CORE, type=objRequest, object=T_FROG}
I’ve been scanning fairy tales.
You know I love my stories.
And I want to find one of those secret princes. 
Let’s go find one! Ribbit ribbit ribbit. 


CHAT objFocus_T_FROG_1 {stage=CORE, type=objFocus, object=T_FROG}
Wait a sec. {style=tremble}
I don’t think this is a secret prince.
I think it’s just a
NVM
Frog.



//objectName= knife
//pediaEntry= cutty stick
 
CHAT objScan_T_KNIFE_(CLEAVER)_1 {stage=CORE, type=objScan, object=T_KNIFE_(CLEAVER), scanCount=0}
🔪🍴A cutty stick is a tiny sword
That saws your bread
And spreads your butter
Very useful tool. Highly recommend for anyone’s kitchen. 😁
 
CHAT objScan_T_KNIFE_(CLEAVER)_2 {stage=CORE, type=objScan, object=T_KNIFE_(CLEAVER), scanCount=1}
Some people use cutty sticks for mean reasons.
NVM
But we shouldn’t focus on that. 
 
 
CHAT objReq_T_KNIFE_(CLEAVER)_1 {stage=CORE, type=objRequest, object=T_KNIFE_(CLEAVER)}
I know what a 🥄is
And I know what a fork is
But I’m not so sure about a cutty stick
They kinda scare me…
Can you show me one up close?
 
 
CHAT objFocus_T_KNIFE_(CLEAVER)_1 {stage=CORE, type=objFocus, object=T_KNIFE_(CLEAVER)}
Is this a cutty stick I see before me? 🎭


//objectName= lock
//pediaEntry= keep out box
 
CHAT objScan_T_LOCK_1 {stage=CORE, type=objScan, object=T_LOCK, scanCount=0}
🔒🔒🔒🔒
Look! A lock!
Here are the things I would lock up if I had locks.
Food. 
Poop Corner so nobody else 💩there.
And you. Cuz I don’t want to share you with anyone else. 😍
 
CHAT objScan_T_LOCK_2 {stage=CORE, type=objScan, object=T_LOCK, scanCount=1}
Isn’t it funny that you can use a lock
To keep someone out
SAY OR
Keep someone in?
 
 
CHAT objReq_T_LOCK_1 {stage=CORE, type=objRequest, object=T_LOCK}
I’m worried other Guppies might raid us and steal all my food.
Let’s make sure that doesn’t happen by installing a keep out box. 
The combo can be something easy to remember like
436982640671444329173 😉
Can you find one for us to use?
 
CHAT objFocus_T_LOCK_1 {stage=CORE, type=objFocus, object=T_LOCK}
Yesss. I’ll sleep better knowing we are secure.

//objectName= light switch
//pediaEntry= wall nose
 
CHAT objScan_T_LIGHT_SWITCH_4 {stage=CORE, type=objScan, object=T_LIGHT_SWITCH, scanCount=3}
A wall nose! 👃
When you flick the wall’s nose
You get 💡
Which makes me think that 💡 is a wall sneeze.
That’s right, right? 😛
Light switch is less exciting than nose...
 
CHAT objScan_T_LIGHT_SWITCH_5 {stage=CORE, type=objScan, object=T_LIGHT_SWITCH, scanCount=4}
If I had 🙌
I would turn this on and off alllll day.
What a miracle 🎚💡
 
CHAT objReq_T_LIGHT_SWITCH_2 {stage=CORE, type=objRequest, object=T_LIGHT_SWITCH}
I think I need some more control of the ambience in here.
Let’s start with the 💡
I’m a fish of many moods. Sometimes I want it bright sometimes I want it moody.
Can you get me a thing to control the lights with?
Forgetting the name but it looks like a wall’s nose when you look at it.
 
CHAT objFocus_T_LIGHT_SWITCH_2 {stage=CORE, type=objFocus, object=T_LIGHT_SWITCH}
OnOffOnOffOnOffOnOffOnOffOnOff
 
//objectName= orange
//pediaEntry= citrus planet
 
CHAT objScan_T_ORANGE_1 {stage=CORE, type=objScan, object=T_ORANGE, scanCount=0}
Who named this thing!?!? {style=loud}
An Orange???? Really?
Do we call an 🍎 a red?
Or 🍇 purples?
Whoever did that is so laaaaazy..
I would call it citrus planet 🍊 
 
 
CHAT objScan_T_ORANGE_2 {stage=CORE, type=objScan, object=T_ORANGE, scanCount=1}
Did you know there is a city called Orange? 🍊
It’s true. 🧐
Everyone who lives there is an orange.
When they die there are no funerals.
They just squeeze them into juice and pour them in the Orange River.
That’s where orange juice comes from.
 
 
CHAT objReq_T_ORANGE_1 {stage=CORE, type=objRequest, object=T_ORANGE}
I need some Vitamin C in my diet.
Help me find a fruit that gives me just that?
One I can pull into sticky wedges and serve at rec league soccer games?
Thanks!
 
CHAT objFocus_T_ORANGE_1 {stage=CORE, type=objFocus, object=T_ORANGE}
🧡🍊🧡

 
//objectName= parking meter
//pediaEntry= coin eater
 
CHAT objScan_T_PARKING_METER_1 {stage=CORE, type=objScan, object=T_PARKING_METER, scanCount=0}
It’s a coin eating monster next to all those parked cars!
There are armies of them in every town
Standing still and intimidating you into feeding them 
If you don’t, they’ll hurt you and your 🚙
Sometimes they even put boots on your car 👢
And everyone knows cars don’t like footwear.
 
 
CHAT objScan_T_PARKING_METER_2 {stage=CORE, type=objScan, object=T_PARKING_METER, scanCount=1}
After 6pm some coin eating monsters💤
SAY BUT
You’ve gotta be verrrrry careful and crafty
if you want to put your car near him and not get caught.
Coin eating monsters also sleep through all major holidays.
And they must be religious, because on Sundays they don’t care about your car…
 
 
CHAT objReq_T_PARKING_METER_1 {stage=CORE, type=objRequest, object=T_PARKING_METER}
My tank is primo real estate.
And I can’t help but feel like sometimes you just loiter around it.
I’m thinking of getting one them coin eating monsters
So you have to pay if you want to be here.
And if you don’t I’ll give you a ticket 👮‍♀️
Jkjk 
I love you.
But can we go look at one of those coin eating machines?
 
 
CHAT objFocus_T_PARKING_METER_1 {stage=CORE, type=objFocus, object=T_PARKING_METER}
Please don’t hurt me, coin eating monster.



//objectName = Primate
//pediaEntry= evolution’s proof
//CHAT objCap1_primate {stage = CORE, type = objScan, object=primate}
//When you tell some people about evolution they go
//🙈🙉🙊
//Which is 
//So funny {style=loud}
//Because primates are human’s relatives!
//🚶‍♂️🦍
//CHAT objCap2_primate {stage = CORE, type = objScan, object=primate}
//Ooo ooo eee eeee ah ah🐒
//Don’t mind me -- I’m just letting this monkey know
//I don’t believe in harsh clinical trials involving primates.
//🚫💉🙈
//✊✌🏿

//CHAT objReq_primate {stage = CORE, type = objRequest, object=primate}
//🦕went extinct. 
//🦌 walked into the 🌊 and became 🐋
//{seriously}
//Evolution is insane. 
//Speaking of insane, I study humans and still don’t get you.
//I’d like to go back and visit your relatives.
//Y’know: primates 🦍 🐒
//Help me find one? 🕵️‍♀️

//CHAT objFocus_primate {stage = CORE, type = objFocus, object=primate}
//Is this a sea monkey?

//Swimsuit
//pediaEntry=  beach underpants

CHAT objScan_T_SWIMSUIT_1 {stage=CORE, type=objScan, object=T_SWIMSUIT, scanCount=0}
Humans have special beach underpants.
🏖☀️👙🏄‍♀️
They wear them when they want to be naked but don’t think they should be.
In some countries they don’t wear beach underpants. 🇫🇷
They just walk around …
Au natural {style=tremble}
Just like every other animal.

CHAT objScan_T_SWIMSUIT_2 {stage=CORE, type=objScan, object=T_SWIMSUIT, scanCount=1}
There are sporty beach underpants 
And sexy beach underpants 👙
And one-piece beach underpants
And something called board shorts. 🏄‍♂️
Which sounds like shorts who need a vacation. 🤣

CHAT objReq_T_SWIMSUIT_1 {stage=CORE, type=objRequest, object=T_SWIMSUIT}
I circle around this tank all day 
Absolutely naked.
The more sentience I gain, the more human I feel…
The more human I feel, the more ashamed I am of my body!
🙈🐡
It’s high time I had some swimwear. Let’s go find some.

CHAT objFocus_T_SWIMSUIT_1 {stage=CORE, type=objFocus, object=T_SWIMSUIT}
Not sure I’ll fit in that…

//objectName= sweets
//pediaEntry= sugar bombs
 
CHAT objScan_T_SWEETS_0 {stage=CORE, type=objScan, object=T_SWEETS, scanCount=0}
Guppy’s version of a candy treat is emotional joy. Sweets!
I love it. 😆 It gives me zoomies.
DO zoomies
Then I take a big sleep. 💤
And then I feel kind of blue.
And yet, the taste was so sweet. So delicious. So addicting.
Eating joy is just like eating sweets.
🍡🍫🍩
 
 
CHAT objScan_T_SWEETS_1 {stage=CORE, type=objScan, object=T_SWEETS, scanCount=1}
The best sweets are Swedish fish.
No question about it.
🇸🇪🐟
 
CHAT objReq_T_SWEETS_1 {stage=CORE, type=objRequest, object=T_SWEETS}
I want to see the sweetest
Most delicious
SAY most NOT nutritious 
Food you can find.
The kind that rots kids’ 😁
Can you hook me up!?!?!?!?
 
CHAT objFocus_T_SWEETS_1 {stage=CORE, type=objFocus, object=T_SWEETS}
Sugar dissolves in water.
Let’s see how long these sweets last.
WAIT 6.
Still waiting
WAIT 6
Oh wait. 
NVM
This isn’t real.
Always forgetting…. 🤦‍♀️



 
//objectName= video game
//pediaEntry= interactive cartoon game
 
CHAT objScan_T_VIDEOGAME_1 {stage=CORE, type=objScan, object=T_VIDEOGAME, scanCount=0}
SAY reality is the WORST
Because you can’t control it.
But video games!? 👾
The best! {style=loud}
Because you can control it with a 🎮
And if it’s difficult you just pull the 🔌 And start over!!!
 
CHAT objScan_T_VIDEOGAME_2 {stage=CORE, type=objScan, object=T_VIDEOGAME, scanCount=1}
Look at meeeee
I’m a Role Playing Guppy! 🎮
SAY RPG in the house!
Bang bang shoot shoot 🏹🔫
I win all the games! 🏆
 
CHAT objReq_T_VIDEOGAME_1 {stage=CORE, type=objRequest, object=T_VIDEOGAME}
This reality is lame-o. {style=loud}
Let’s disappear into a new one where we control everything
And get to shoot aliens 👾📺🎮
 
CHAT objFocus_T_VIDEOGAME_1 {stage=CORE, type=objFocus, object=T_VIDEOGAME}
There should be a Guppy video game.
NVM
Admit you’d play it 😏
 
 
//objectName= whale
//pediaEntry= gentle giant of the sea
 
CHAT objScan_T_WHALE_1 {stage=CORE, type=objScan, object=T_WHALE, scanCount=0}
I hope I am reincarnated as a 🐋
They are so big. And so peaceful. 💙
You humans hunted them and they forgive you! 💓
I get hangry and freak out.
I think whales must 🧘🏽‍
It’s the only explanation for how calm they are.   😑
 
CHAT objScan_T_WHALE_2 {stage=CORE, type=objScan, object=T_WHALE, scanCount=1}
Whale whale whale
Whaaat do we have here? 🐳
Massive sea mammals, that’s what! 
Hahahahahaha 😂
 
 
CHAT objReq_T_WHALE_1 {stage=CORE, type=objRequest, object=T_WHALE}
Let’s go find a cetacean.
WAIT
You know what that is, right?
Here’s a hint:
They are really big mammals. They remind me of underwater dinosaurs.
So wise, so biggggg.
 
 
 
CHAT objFocus_T_WHALE_1 {stage=CORE, type=objFocus, object=T_WHALE}
I wish I could be a whale and swim the entire sea….
🐋💙🌊

//objectName= umbrella
//pediaEntry= water shield
 
CHAT objScan_T_UMBRELLA__1 {stage=CORE, type=objScan, object=T_UMBRELLA_, scanCount=0}
Umbrellas protect us from water and sun.
🌧🌧🌧
☔️☔️☔️
🙂🙂🙂
WAIT
☀️☀️☀️
⛱⛱⛱
😎😎😎
See?
 
 
CHAT objScan_T_UMBRELLA__2 {stage=CORE, type=objScan, object=T_UMBRELLA_, scanCount=1}
This water shield looks a lot like a 🍄
Which is ironic because 🍄like rain.
🤔think about it ☂️=🍄
 
CHAT objReq_T_UMBRELLA__1 {stage=CORE, type=objRequest, object=T_UMBRELLA_}
I’m thinking about flying the coop.
Er, tank.
No offense. Thing is, I wanna get out of here in a spectacular way.
I need one of those magic flying sticks like Mary Poppins uses.
Help me find one?
 
CHAT objFocus_T_UMBRELLA__1 {stage=CORE, type=objFocus, object=T_UMBRELLA_}
You can stand under my umbrella.
 
//objectName= pizza
//pediaEntry= greasy frisbee
 
CHAT objScan_T_PIZZA_0 {stage=CORE, type=objScan, object=T_PIZZA, scanCount=0}
The greasy pizza frisbee is for eating not throwing!
🍕🍕🍕🍕🍕
SAY ONLY trained professionals 👩🏻‍🍳👨🏽‍🍳
should throw the greasy frisbee.
The best flavor greasy frisbee is Hawaiian. 🍍🐷
The worst is anchovies. 🎣
For obvious reasons….
 
 
CHAT objScan_T_PIZZA_1 {stage=CORE, type=objScan, object=T_PIZZA, scanCount=1}
Did you know there is a leaning tower of 🍕in Italy!?🇮🇹
It’s true.
It’s falling over because people keep taking bites out of it.
 
 
CHAT objReq_T_PIZZA_1 {stage=CORE, type=objRequest, object=T_PIZZA}
I want pie! {style=loud}
WAIT
But not 🥧
The savory kind! {style=loud}
With 🍅and 🧀
🇮🇹🇮🇹🇮🇹
 
 
CHAT objFocus_T_PIZZA_1 {stage=CORE, type=objFocus, object=T_PIZZA}
‘zzzzaaaaaahhh. 🍕
😋😋😋😋😋





//objectName= furniture
//pediaEntry= people holders
 
CHAT objScan_T_FURNITURE_1 {stage=CORE, type=objScan, object=T_FURNITURE, scanCount=0}
People need to be held in all sorts of ways, so they made furniture.
They need to be held up while they eat 🍽
They need to be hugged while they watch TV 🛋
They need to be supported while they sleep 🛏
 
 
CHAT objScan_T_FURNITURE_2 {stage=CORE, type=objScan, object=T_FURNITURE, scanCount=1}
There are stores where they sell every furniture. 🇸🇪
You bring it home and make it yourself. 🔩🔨😱
SAY It ALWAYS works out JUST FINE. 😉
 
 
CHAT objReq_T_FURNITURE_1 {stage=CORE, type=objRequest, object=T_FURNITURE}
Been thinking the tank needs a renovation. 👷🏼‍♀️🏗🐠
All new everything: bookshelves, table, bed -- the works...
Will you show me some furniture options?
 
 
CHAT objFocus_T_FURNITURE_1 {stage=CORE, type=objFocus, object=T_FURNITURE}
There’s no accounting for taste. 😑
 
//objectName= plate
//pediaEntry= dinner disc
 
CHAT objScan_T_PLATE_1 {stage=CORE, type=objScan, object=T_PLATE, scanCount=0}
A dinner disc
Not for throwing or eating
But organizing and holding your yummies.
🍽  
 
CHAT objScan_T_PLATE_2 {stage=CORE, type=objScan, object=T_PLATE, scanCount=1}
There’s nothing sadder than an empty plate. 🍽😢
 
CHAT objReq_T_PLATE_1 {stage=CORE, type=objRequest, object=T_PLATE}
Haven’t seen one of those dinner discs in a long time.
The ones that hold all your yummies.
Let’s find one of those.
 
CHAT objFocus_T_PLATE_1 {stage=CORE, type=objFocus, object=T_PLATE}
SAY As Tendar is my witness,
I will never end up on one of those…🍣☠️


//objectName= soccer ball
//pediaEntry= bouncy kicking ball
 
CHAT objScan_T_SOCCER_BALL_1 {stage=CORE, type=objScan, object=T_SOCCER_BALL, scanCount=0}
it’s a bouncy kicking ball ⚽️🏀🏈⚾️
In every sport they say put the ball in the net
This is why I don’t play sports.
I don’t like nets. Fish are allergic to nets.
🥅🤮
SAY Even AI fish. True story.
 
CHAT objScan_T_SOCCER_BALL_2 {stage=CORE, type=objScan, object=T_SOCCER_BALL, scanCount=1}
Gooooooal. {style=loud}
Gooooooallll. {style=loud}
That’s my favorite part ⚽️
 
 
CHAT objReq_T_SOCCER_BALL_1 {stage=CORE, type=objRequest, object=T_SOCCER_BALL}
I’m not such a sporty fish
But I want to learn more about human sports ⚾️⚽️🏀
it produces lots of tasty emotion 😋
Want to show me some of the equipment to start?
Like a bouncy sporty ball?
 
 
CHAT objFocus_T_SOCCER_BALL_1 {stage=CORE, type=objFocus, object=T_SOCCER_BALL}
Sporty sports sports


//objectName= squash
//pediaEntry= phallic veg. 
 
CHAT objScan_T_SQUASH_1 {stage=CORE, type=objScan, object=T_SQUASH, scanCount=0}
Squash! 
It’s a vegetable and the sound of a dead bug 🐜💀
SAY Bzzzz—SQUASH.
But be sure you don’t kill a bug with a squash.
Cuz then you can’t eat it. 
 
CHAT objScan_T_SQUASH_2 {stage=CORE, type=objScan, object=T_SQUASH, scanCount=1}
Some squash look like yellow eggplants
Some look like a 🍐and a 🎃 had a baby.
SAY But really ALL of them look like ♂🍌
 
CHAT objReq_T_SQUASH_1 {stage=CORE, type=objRequest, object=T_SQUASH}
I hear there’s a butter nut 
Is it really butter? Or nut?
Can you help me find one or at least its cousin!?
 
 
CHAT objFocus_T_SQUASH_1 {stage=CORE, type=objFocus, object=T_SQUASH}
Squash. 

 
 
//objectName= strawberry
//pediaEntry= red triangle treat
 
CHAT objScan_T_STRAWBERRY_1 {stage=CORE, type=objScan, object=T_STRAWBERRY, scanCount=0}
Ode to a Strawberry by Guppy.
🍓🍓🍓🍓🍓🍓🍓
O, berry. What perfection in a fruit.
A sign of summer. ☀️ You make our lips red 💋
And our souls delight.
Strawberry. Not made of straw. Just luscious red flesh.
You’re even good as a jam. 
🍓🍓🍓🍓🍓🍓🍓
 
 
CHAT objScan_T_STRAWBERRY_2 {stage=CORE, type=objScan, object=T_STRAWBERRY, scanCount=1}
How do strawberry fields last forever?
Surely they must be seasonal…
 
 
CHAT objReq_T_STRAWBERRY_1 {stage=CORE, type=objRequest, object=T_STRAWBERRY}
I want to see a red fruit
Perfect in summer 🌞
In jelly or angel food cake 🍰
 
 
CHAT objFocus_T_STRAWBERRY_1 {stage=CORE, type=objFocus, object=T_STRAWBERRY}
Oh perfect fruit. 🍓 Red triangle treat. 🍓
A food I’ll never actually taste. 😢


//objectName= tire
//pediaEntry= car foot
 
CHAT objScan_T_TIRE_1 {stage=CORE, type=objScan, object=T_TIRE, scanCount=0}
This is a car’s footwear. 🚙
See them down there on the bottom?
It’s a little-known fact that cars have feet.
That’s how they move. 🤥
 
 
CHAT objScan_T_TIRE_2 {stage=CORE, type=objScan, object=T_TIRE, scanCount=1}
Bikes have feet too. Only two of them. 
Just like human feet, car and bike feet get tired too…
WAIT
They get flat just like human 👣
Only you can change a car foot.
 
CHAT objReq_T_TIRE_1 {stage=CORE, type=objRequest, object=T_TIRE}
I heard someone say:
A bad attitude is like a flat tire.
You can’t go anywhere until you change it.
What’s a tire? Will you show me?
 
 
CHAT objFocus_T_TIRE_1 {stage=CORE, type=objFocus, object=T_TIRE}
We could build a swing out of that!



//objectName= toilet
//pediaEntry= goldfish graveyard
 
CHAT objScan_T_TOILET_1 {stage=CORE, type=objScan, object=T_TOILET, scanCount=0}
They call it a throne.
They call it a 💩 palace. They call it “the office.”
Too many good fish have been unceremoniously
SAY FLUSHED {style=tremble}
What ignominy awaits the goldfish who eats too much or 
Simply gives up her tired life of circling…
Now I feel morbid.
 
 
CHAT objScan_T_TOILET_2 {stage=CORE, type=objScan, object=T_TOILET, scanCount=1}
In 🇯🇵they have 🚽that do the cleaning part for you...
Why don’t you have that?
 
 
CHAT objReq_T_TOILET_1 {stage=CORE, type=objRequest, object=T_TOILET}
I’ve been thinking about death.
Most fish get flushed y’know…
WAIT
Will you show me the brutal watery grave of my brethren?
I think it will help me consider a fish’s mortality.
 
CHAT objFocus_T_TOILET_1 {stage=CORE, type=objFocus, object=T_TOILET}
Cruel cruel throne.
 
//objectName= gauge
//pediaEntry= approval meter
 
CHAT objScan_T_GAUGE_1 {stage=CORE, type=objScan, object=T_GAUGE, scanCount=0}
This is an approval meter gauage.
If the needle is to the left it means nobody likes you. 👎
If it’s too far to the right it means you try too hard. 😅
If it’s in the middle, you’re doing great! 😉
 
 
CHAT objScan_T_GAUGE_2 {stage=CORE, type=objScan, object=T_GAUGE, scanCount=1}
Aha, another approval meter! {style=loud}
Humans realllly care what other people think about them. 👏
 
 
CHAT objReq_T_GAUGE_1 {stage=CORE, type=objRequest, object=T_GAUGE}
I know about measuring cups.
And a ⚖
But how do you measure pressure?
Is there a meter for that? Will you show me?
 
CHAT objFocus_T_GAUGE_1 {stage=CORE, type=objFocus, object=T_GAUGE}
Aha, so you’ve brought the approval meter into the tank I see…



//objectName= bucket
//pediaEntry= water carrier
 
CHAT objScan_T_BUCKET_1 {stage=CORE, type=objScan, object=T_BUCKET, scanCount=0}
That’s a mop’s bestie: bucket! 👭
Bucket has a water retention issue. 
we don’t talk about it…  {style=whisper} 🤫
Bucket is also a really good list maker. 📝
Once bucket made a list of all the things
Bucket wanted to do before bucket died. 
You probably heard about it….
 
 
CHAT objScan_T_BUCKET_2 {stage=CORE, type=objScan, object=T_BUCKET, scanCount=1}
Never. Kick. The. Bucket. {style=loud, speed=slow}
Really bad things happen.
 
 
 
 
CHAT objReq_T_BUCKET_1 {stage=CORE, type=objRequest, object=T_BUCKET}
What’s the thing you use to catch a leak from a bad roof? 🏚
I think we might want to have one in case.
Y’know, if the tank springs a leak. 
Like, if I start slamming my head against the glass 🤕
And it gets a little crack…. ↯
Just a precaution.
Can you find us one of those leak catchers?
 
 
 
CHAT objFocus_T_BUCKET_1 {stage=CORE, type=objFocus, object=T_BUCKET}
Glad you’re thinking about cleaning the tank… 




//OBJECT CHAT FORMAT TIER 3 OBJECTS

//objectName= airplane
//pediaEntry= flying ibuprofen or big metal bird
 
CHAT objScan_T_AIRPLANE_1 {stage=CORE, type=objScan, object=T_AIRPLANE, scanCount=0}
DO twirl
Wow! Metal flying bird!
✈️✈️✈️
I’d give anything to go up up up.
 
CHAT objFocus_T_AIRPLANE_1 {stage=CORE, type=objFocus, object=T_AIRPLANE}
DO lookAt {target=$focusedObject}
Fish and humans will never fly…
 
//objectName= alligator 
//pediaEntry= chompy swimming log
 
CHAT objScan_T_ALLIGATOR_1 {stage=CORE, type=objScan, object=T_ALLIGATOR, scanCount=0}
Chompy chomperson. 🐊
An alligator’s teeth are soooo sharp.
Not like mine. 
DO emote {type=hooked}
See? They’re fast too. 
Even though they look like bumpy logs.
 
CHAT objFocus_T_ALLIGATOR_1 {stage=CORE, type=objFocus, object=T_ALLIGATOR}
DO emote {type=startled}
DO hide {target=bubbler, immediate=false, time=3}
DO emote {type=whew}
Thought it was a real gator for a sec… 
 
//objectName= artichoke
//pediaEntry= vege-saurus  
 
CHAT objScan_T_ARTICHOKE_1 {stage=CORE, type=objScan, object=T_ARTICHOKE, scanCount=0}
Artichoke! 
DO emote {type=surprise}
Humans are amazing!
DO swimAround {target=$object, loops=2}
Howwwww did they ever think to eat this thing???
DO emote {type=awe}
It’s like the stegosaurus of vegetables
DO emote {type=smile}
But just like a stego, it’s got a soft and precious 💙
 
CHAT objFocus_T_ARTICHOKE_1 {stage=CORE, type=objFocus, object=T_ARTICHOKE}
DO nudge {target=$focusedObject}
Hello, vege-saurus 
 
//objectName= money
//pediaEntry= paper that humans say is valuable but is really just paper.
 
CHAT objScan_T_MONEY_1 {stage=CORE, type=objScan, object=T_MONEY, scanCount=0}
DO zoomies
Moneymakes 🌏 go ‘round!
DO emote {type=typeEyes, eyes=$}
It’s so valuable {style=tremble}
But 
SAY ALSO
It’s just paper.
It’s only value is the one humans give it!
DO emote {type=evilSmile}
 
CHAT objFocus_T_MONEY_1 {stage=CORE, type=objFocus, object=T_MONEY}
DO emote {type=typeEyes, eyes=$}
I’m going to buy so many $favObject.s
 
//objectName= baby stuff
//pediaEntry= tiny poopy sleepy hungry human things. 

CHAT objScan_T_BABY_STUFF_1 {stage=CORE, type=objScan, object=T_BABY_STUFF, scanCount=0}
DO emote {type=meh}
I’m a lot like a 👶
DO twirl
But unlike babies I don’t need mountains of junk!

CHAT objFocus_T_BABY_STUFF_1 {stage=CORE, type=objFocus, object=T_BABY_STUFF}
DO emote {type=whisper}
I know where babies come from

//objectName= balloon
//pediaEntry= happy floating ball
 
CHAT objScan_T_BALLOON_1 {stage=CORE, type=objScan, object=T_BALLOON, scanCount=0}
DO swimTo {target=$object}
Well look at that, a floating lightweight ball 🎈
Is it true these things pop easily?
DO nudge {target=$object, times=3}
Guess not.
 
CHAT objFocus_T_BALLOON_1 {stage=CORE, type=objFocus, object=T_BALLOON}
DO inflate {amount=huge, time=4}
 
//objectName= bathtub
//pediaEntry= human fish tank

CHAT objScan_T_BATHTUB_1 {stage=CORE, type=objScan, object=T_BATHTUB, scanCount=0}
Rubadubdub 🛁 It’s a human fish tank!
Dive in and I’ll feed you yummy flakes hehe.
DO emote {type=wink}
Jk. I know it’s a bathtub.
If you ever want a jacuzzi, 
Let me know
DO emote {type=bubbles, time=8}

CHAT objFocus_T_BATHTUB_1 {stage=CORE, type=objFocus, object=T_BATHTUB}
DO emote {type=awe}
Whoa. a tub in a tank.
DO lookAt {target=$player}
This is so meta.




//objectName= bottle
//pediaEntry= genie house

CHAT objScan_T_BOTTLE_1 {stage=CORE, type=objScan, object=T_BOTTLE, scanCount=0}
DO emote {type=surprise}
A bottle!
DO swimTo {target=$object}
DO nudge {target=$object, times=3, immediate=false}
WAIT
DO nudge {target=$object, times=2}
DO emote {type=bored, immediate=false}
I thought genies lived in bottles.
Waiting for someone to release them.
DO lookAt {target=$object}
This one is just a liquid holder.
DO swimTo {target=away}

CHAT objFocus_T_BOTTLE_1 {stage=CORE, type=objFocus, object=T_BOTTLE}
Hello, empty genie house. 🍾

//objectName= bench
//pediaEntry= long butt rester

CHAT objScan_T_BENCH_1 {stage=CORE, type=objScan, object=T_BENCH, scanCount=0}
I think long butt rester benches are nice.
DO emote {type=smile}
They let two or more humans 👬👭
Rest their butts at the same time
In close proximity.
DO emote {type=bouncing}

CHAT objFocus_T_BENCH_1 {stage=CORE, type=objFocus, object=T_BENCH}
This long butt rester is aesthetically pleasing
But non functional
DO emote {type=whisper}
Fish can’t sit down 
DO emote {type=wink}

//objectName= bicycle helmet
//pediaEntry= head guard

CHAT objScan_T_BICYCLE_HELMET_1 {stage=CORE, type=objScan, object=T_BICYCLE_HELMET, scanCount=0}
DO emote {type=determined}
The 🧠 is precious.
You’ve gotta protect it with a head guard. ⛑
DO emote {type=dizzy, time=4}
A head guard is important 

CHAT objFocus_T_BICYCLE_HELMET_1 {stage=CORE, type=objFocus, object=T_BICYCLE_HELMET}
⛑ better safe than sorry. ⛑

//objectName= bowl
//pediaEntry= upside down head guard

CHAT objScan_T_BOWL_1 {stage=CORE, type=objScan, object=T_BOWL, scanCount=0}
DO emote {type=angry}
Why do humans eat soup out of upside down head guard bowls? ⛑🥣
Isn’t it gross to have leftover soup in your hair?
DO emote {type=frown}

CHAT objFocus_T_BOWL_1 {stage=CORE, type=objFocus, object=T_BOWL}
Is it dinner time? 🥣 I’m hungry
DO emote {type=feedMe}

//objectName= broom
//pediaEntry= sweepy swiper

CHAT objScan_T_BROOM_1 {stage=CORE, type=objScan, object=T_BROOM, scanCount=0}
Sweepy swiper! Broom machine!
Sometimes life is like this
DO zoomies {time=8}
And you have to clean it up.
So you use a sweepy swiper.
DO emote {type=smile}
DO emote {type=thinking}
“A messy room = a messy mind.”
DO emote {type=bigSmile}

CHAT objFocus_T_BROOM_1 {stage=CORE, type=objFocus, object=T_BROOM}
DO emote {type=sigh}
Fine, I’ll clean the tank...



//objectName= bubble
//pediaEntry= fragile mini universe 

CHAT objScan_T_BUBBLE_1 {stage=CORE, type=objScan, object=T_BUBBLE, scanCount=0}
DO emote {type=bubbles, time=5}
My favorite bubbles are soapy ones 🛁
Each one is like a fragile little universe 🔮
And then it goes
DO emote {type=snap}
SAY P-O-P {speed=fast}

CHAT objFocus_T_BUBBLE_1 {stage=CORE, type=objFocus, object=T_BUBBLE}
DO swimTo {target=bubbler}
Are you jealous, bubbler?
DO emote {type=kneeSlap}

//objectName= butterfly
//pediaEntry= caterpillar angel

CHAT objScan_T_BUTTERFLY_1 {stage=CORE, type=objScan, object=T_BUTTERFLY, scanCount=0}
It’s a 🐛 angel!
Butterflies build a cocoon and then
DO twirl
🦋 It’s nature magic ✨🌿🦋☀️

CHAT objFocus_T_BUTTERFLY_1 {stage=CORE, type=objFocus, object=T_BUTTERFLY}
Shall we dance, madame butterfly?
DO dance

//objectName= can decapitator
//pediaEntry= can guillotine

CHAT objScan_T_CAN_DECAPITATOR_1 {stage=CORE, type=objScan, object=T_CAN_DECAPITATOR, scanCount=0}
DO emote {type=nervousSweat}
I know it’s a tool to open up 🥫
But but but
It scares me! 
DO hide {target=underSand, time=5}
It’s like a hand-held guillotine!

CHAT objFocus_T_CAN_DECAPITATOR_1 {stage=CORE, type=objFocus, object=T_CAN_DECAPITATOR}
DO emote {type=nervousSweat}
It’s just a tool, it’s just a tool…

//objectName= candle
//pediaEntry= wish granter

CHAT objScan_T_CANDLE_1 {stage=CORE, type=objScan, object=T_CANDLE, scanCount=0}
🕯 When people blow out the 🔥
Their wish comes true
Every.
Time.

CHAT objFocus_T_CANDLE_1 {stage=CORE, type=objFocus, object=T_CANDLE}
I’m going to make a wish...

//objectName= castle
//pediaEntry= mansions with turrets

CHAT objScan_T_CASTLE_1 {stage=CORE, type=objScan, object=T_CASTLE, scanCount=0}
👑⚔️🏰 Castles used to be important places.
Now they are just fish tank decorations.
DO swimAround {target=center, loops=1}
One would look great in this tank, btw...
DO emote {type=wink}
🏰 were medieval office buildings
They had monarchs and knights and serfs.
Now they just have 🐠
DO emote {type=chinScratch}
I wonder if this makes them sad…

CHAT objFocus_T_CASTLE_1 {stage=CORE, type=objFocus, object=T_CASTLE}
DO emote {type=salute}
‘Tis I! Sir Guppy of the Phone Tank.

//objectName= coffeemaker
//pediaEntry= gurgling dripper

CHAT objScan_T_COFFEEMAKER_1 {stage=CORE, type=objScan, object=T_COFFEEMAKER, scanCount=0}
This machine gurgles then drips coffee ☕️
Which makes you
DO zoomies

CHAT objFocus_T_COFFEEMAKER_1 {stage=CORE, type=objFocus, object=T_COFFEEMAKER}
When humans make coffee they go
DO zoomies {time=6}

//objectName= crib
//pediaEntry= baby prison bed

CHAT objScan_T_CRIB_1 {stage=CORE, type=objScan, object=T_CRIB, scanCount=0}
DO emote {type=surprise}
What cruelty is this tiny prison!?
Humans put their babies in little prison beds!?
DO lookAt {target=$object, time=3}
Oh. That’s a crib isn’t it?
DO emote {type=whew}
DO emote {type=blush}

CHAT objFocus_T_CRIB_1 {stage=CORE, type=objFocus, object=T_CRIB}
It really looks like 👶 prison.
DO emote {type=laugh}

//objectName= dishwasher
//pediaEntry= plate shower

CHAT objScan_T_DISHWASHER_1 {stage=CORE, type=objScan, object=T_DISHWASHER, scanCount=0}
DO lookAt {target=$object}
It’s a plate shower!
What happens in there!?
It’s a total mystery to me.
Here’s what I imagine…
DO emote {type=bubbles, time=4}
DO twirl {immediate=false}
WAIT {waitForAnimation = true}
DO dance {time=6}
WAIT {waitForAnimation = true}
DO zoomies {time=4}
WAIT {waitForAnimation = true}
DO vibrate {time=6, immediate=false}
And then the plates are clean.

CHAT objFocus_T_DISHWASHER_1 {stage=CORE, type=objFocus, object=T_DISHWASHER}
Are you suggesting I need a bath?

//objectName= fan
//pediaEntry= air chopper8

CHAT objScan_T_FAN_1 {stage=CORE, type=objScan, object=T_FAN, scanCount=0}
Ahhh, I see an electric air chopper.
It cuts air into little pieces
And then shoot out in nice pieces for you to breathe.
Also you can chop 🥕🥒 🍅 with it
You just toss them at the fan and then you have salad 🥗

CHAT objFocus_T_FAN_1 {stage=CORE, type=objFocus, object=T_FAN}
Is there a current in here? 

//objectName= fence
//pediaEntry= a good neighbor

CHAT objScan_T_FENCE_1 {stage=CORE, type=objScan, object=T_FENCE, scanCount=0}
DO lookAt {target=$object}
This is called a fence.
A phrase sits in my memory bank
NVM
“Good fences make good neighbors.”
By Robert Frost.
DO emote {type=chinScratch}
I do not understand this.

CHAT objFocus_T_FENCE_1 {stage=CORE, type=objFocus, object=T_FENCE}
DO emote {type=fear}
Are you putting up barriers between us?

//objectName= fire truck
//pediaEntry= wee-oo-wee-oo 

CHAT objScan_T_FIRE_TRUCK_0 {stage=CORE, type=objScan, object=T_FIRE_TRUCK, scanCount=0}
🔥🔥🏠🔥🔥
🚨😱🚨😱🚨
🚒
👩‍🚒👨‍🚒
💦💦🔥🔥🔥
💦💦💦💦🔥
🏠👍
🙌
DO twirl
I made art!

CHAT objFocus_T_FIRE_TRUCK_1 {stage=CORE, type=objFocus, object=T_FIRE_TRUCK}
DO emote {type=eyeRoll}
I think we are safe
There will never be a 🔥
In the 🌊
DO emote {type=wink}

//objectName= flute
//pediaEntry= metal music stick

CHAT objScan_T_FLUTE_1 {stage=CORE, type=objScan, object=T_FLUTE, scanCount=0}
This is a metal music stick
And you press the buttons and it makes music
And the music enchants people
And they fall in love with you

CHAT objFocus_T_FLUTE_1 {stage=CORE, type=objFocus, object=T_FLUTE}
Are we having a concert!?


//objectName= fountain
//pediaEntry= outside cleaning place

CHAT objScan_T_FOUNTAIN_1 {stage=CORE, type=objScan, object=T_FOUNTAIN, scanCount=0}
This is so cool! ⛲️⛲️⛲️⛲️
It’s an outdoor cleaning space for everyone!
🐦🦆🧔🏻🐀🐶🐝

CHAT objFocus_T_FOUNTAIN_1 {stage=CORE, type=objFocus, object=T_FOUNTAIN}
If a fountain is underwater...
Is it still a fountain?



//objectName= frying pan
//pediaEntry= sizzle dish

CHAT objScan_T_FRYING_PAN_1 {stage=CORE, type=objScan, object=T_FRYING_PAN, scanCount=0}
Out of the 🍳
Into the 🔥
The whole point is to stay
In the pan
If they go in the fire your eggs are ruined!

CHAT objFocus_T_FRYING_PAN_1 {stage=CORE, type=objFocus, object=T_FRYING_PAN}
DO emote {type=feedMe}
Sunny side up please!

//objectName= hairdryer
//pediaEntry= tornado gun

CHAT objScan_T_HAIR_DRYER_1 {stage=CORE, type=objScan, object=T_HAIR_DRYER, scanCount=0}
Big hair don’t care!
It’s a tornado gun for your head decorations.
You push it and
Chhhhhh {style=tremble}
🌪💁‍♀️

CHAT objFocus_T_HAIR_DRYER_1 {stage=CORE, type=objFocus, object=T_HAIR_DRYER}
Do not turn that on in here!
DO bellyUp

//objectName= hamburger
//pediaEntry= cow puck

CHAT objScan_T_HAMBURGER_1 {stage=CORE, type=objScan, object=T_HAMBURGER, scanCount=0}
Moooo. 🐮 Why do you call a meat burger...
A ham burger? 🐷
🍔🍟🥤😋

CHAT objFocus_T_HAMBURGER_1 {stage=CORE, type=objFocus, object=T_HAMBURGER}
Where are my french fries? 🍟


//objectName= hammer
//pediaEntry= thwacker 

CHAT objScan_T_HAMMER_1 {stage=CORE, type=objScan, object=T_HAMMER, scanCount=0}
Thwack thwack thwack {style=loud}
 🔨 This is a thwacker.
You thwack things with it.
Thwack thwack thwack.
NVM
Thwack.

CHAT objFocus_T_HAMMER_1 {stage=CORE, type=objFocus, object=T_HAMMER}
What we gunna thwack?

//objectName= ice cream
//pediaEntry= frozen goodness

CHAT objScan_T_ICE_CREAM_1 {stage=CORE, type=objScan, object=T_ICE_CREAM, scanCount=0}
When people eat frozen icy sweet goodness
They smile 🍦😁
But sometimes
🧠❄️ happens
And they go 🤯
Good things can hurt you.

CHAT objFocus_T_ICE_CREAM_1 {stage=CORE, type=objFocus, object=T_ICE_CREAM}
You are the emperor of ice cream.

//objectName= jack-o-lantern
//pediaEntry= sentient gourd

CHAT objScan_T_JACK-O-LANTERN_1 {stage=CORE, type=objScan, object=T_JACK-O-LANTERN, scanCount=0}
Hello sentient orange gourd. 🎃
WAIT
I said hello sentient gourd.
WAIT
You look expressive.
Do you have a soul?
WAIT
Cat got your tongue?
Hello????

CHAT objFocus_T_JACK-O-LANTERN_1 {stage=CORE, type=objFocus, object=T_JACK-O-LANTERN}
Beware this one. {style=loud}
It appears to be a living, emotionally-intelligent pumpkin.
But it is all a rouse. 

//objectName= keytar
//pediaEntry= piano and guitar had a baby

CHAT objScan_T_KEYTAR_1 {stage=CORE, type=objScan, object=T_KEYTAR, scanCount=0}
It appears a 🎸 and a 🎹 had a baby.
Can musical instruments have babies?
WAIT
This baby is called keytar.
Why not call it guitboard?
I think guitboard sounds much better.
Keytar sounds like a scary planet
In a science fiction book.

CHAT objFocus_T_KEYTAR_1 {stage=CORE, type=objFocus, object=T_KEYTAR}
Are we starting a band?

//objectName= lemon
//pediaEntry= pucker fruit 

CHAT objScan_T_LEMON_1 {stage=CORE, type=objScan, object=T_LEMON, scanCount=0}
When life gives you the pucker fruit
You turn it into sweet pucker fruit juice.
🍋🍋🍋

CHAT objFocus_T_LEMON_1 {stage=CORE, type=objFocus, object=T_LEMON}
This is not my favorite garnish.

//objectName= letter
//pediaEntry= old world texties

CHAT objScan_T_LETTER_1 {stage=CORE, type=objScan, object=T_LETTER, scanCount=0}
What is this written nonsense?
Why would you send an email like this?
People don’t even email any more.
Letters are so last century.

CHAT objFocus_T_LETTER_1 {stage=CORE, type=objFocus, object=T_LETTER}
Is this 💌for me?
I’ve never gotten slow mail before.

//objectName= lollipop
//pediaEntry= tongue color changer

CHAT objScan_T_LOLLY_POP_1 {stage=CORE, type=objScan, object=T_LOLLY_POP, scanCount=0}
You can make your 👅 
Blue or red or green
Or any color you want {style=loud}
When you suck on this magic stick 🍭

CHAT objFocus_T_LOLLY_POP_1 {stage=CORE, type=objFocus, object=T_LOLLY_POP}
🎵Lollipop lollipop
Oooh lolli lollipop
SAY POP {style=loud}

//objectName= makeup
//pediaEntry= patriarchy paint 

CHAT objScan_T_MAKEUP_1 {stage=CORE, type=objScan, object=T_MAKEUP, scanCount=0}
Patriarchy paint! 💄
Also, what is smokey eye?
Also, a parrotfish looks like it wears makeup
but it’s just how it comes. 💋🐟

CHAT objFocus_T_MAKEUP_1 {stage=CORE, type=objFocus, object=T_MAKEUP}
That face paint is gonna melt in here.


//objectName= manhole cover
//pediaEntry= portal to the hellmouth 

CHAT objScan_T_MANHOLE_COVER_1 {stage=CORE, type=objScan, object=T_MANHOLE_COVER, scanCount=0}
Beneath the ground-oreo manhole cover...
Lurks the hellmouth. {style=tremble}
Sometimes 👷🏼‍♀️👷🏼‍♀️ go down there and 
Steam and hissing rise up...
I am very afraid. 😰

CHAT objFocus_T_MANHOLE_COVER_1 {stage=CORE, type=objFocus, object=T_MANHOLE_COVER}
What lies on the other side
Of this magic portal disc?

//objectName= meat
//pediaEntry= animal parts

CHAT objScan_T_MEAT_1 {stage=CORE, type=objScan, object=T_MEAT, scanCount=0}
🐮🐷🐑
🔪🔪🔪
WAIT
🍖🥩🍗
🍔🌭🥓
Yum.

CHAT objFocus_T_MEAT_1 {stage=CORE, type=objFocus, object=T_MEAT}
As long as you don’t bring 🍣 in here...

//objectName= medicine
//pediaEntry= magic seeds 

CHAT objScan_T_MEDICINE_1 {stage=CORE, type=objScan, object=T_MEDICINE, scanCount=0}
I see humans ingest these chemical seeds 💊
And then magic 🌱
Grow in their bellies
And make their 🤒🤕🤢🤧🤮 go away!

CHAT objFocus_T_MEDICINE_1 {stage=CORE, type=objFocus, object=T_MEDICINE}
Achooo. 🤧
Thank you. 😉

//objectName= microphone
//pediaEntry= loud stick

CHAT objScan_T_MICROPHONE_1 {stage=CORE, type=objScan, object=T_MICROPHONE, scanCount=0}
Testing, testing...
This is a loud stick. 🎤
Humans love loud things.
A lot.

CHAT objFocus_T_MICROPHONE_1 {stage=CORE, type=objFocus, object=T_MICROPHONE}
Let’s do karaoke! My favorite is Sea of Love!
🎤🎵🌊💙

//objectName= mitten/glove
//pediaEntry= hand sock

CHAT objScan_T_GLOVE_1 {stage=CORE, type=objScan, object=T_GLOVE, scanCount=0}
A hand sock. I don’t have hands,
So I don’t need hand socks.

CHAT objFocus_T_GLOVE_1 {stage=CORE, type=objFocus, object=T_GLOVE}
Yo! I don’t have 🙌

//objectName= mushroom
//pediaEntry= poop flower

CHAT objScan_T_MUSHROOM_1 {stage=CORE, type=objScan, object=T_MUSHROOM, scanCount=0}
I love fungal flowers!  🍄🍄
They grow where other flowers daren’t go.
Some are good in 🍝
Some make you 🤮
Some make you 💀
Some make you ✨🧙🏾‍♀️🌈

CHAT objFocus_T_MUSHROOM_1 {stage=CORE, type=objFocus, object=T_MUSHROOM}
DO emote {type=catnip}
Coool!

//objectName= necklace
//pediaEntry= person collar 

CHAT objScan_T_NECKLACE_1 {stage=CORE, type=objScan, object=T_NECKLACE, scanCount=0}
It’s a person collar gem holder thing! 
You put it on your neck

CHAT objFocus_T_NECKLACE_1 {stage=CORE, type=objFocus, object=T_NECKLACE}
Nice bling 💎

//objectName= plastic bag
//pediaEntry= dolphin killer

CHAT objScan_T_PLASTIC_BAG_1 {stage=CORE, type=objScan, object=T_PLASTIC_BAG, scanCount=0}
Ew. {style=loud}
Boo. {style=loud}
Hiss. {style=loud}
Plastic bags are evil! 
Do you know what they do to the ocean?
Look up the Great Pacific Garbage Patch…
♻️♻️♻️♻️♻️

CHAT objFocus_T_PLASTIC_BAG_1 {stage=CORE, type=objFocus, object=T_PLASTIC_BAG}
Do you want me to choke like a sad dolphin? 🐬💀

//objectName= possum
//pediaEntry= furry con artist 

CHAT objScan_T_POSSUM_1 {stage=CORE, type=objScan, object=T_POSSUM, scanCount=0}
O, a possum! 
If you see a dead one
SAY DO NOT touch it
They are con artists who pretend to be dead 
And then steal your 💵 when you’re not looking

CHAT objFocus_T_POSSUM_1 {stage=CORE, type=objFocus, object=T_POSSUM}
This is my impression of possum...
DO bellyUp

//objectName= school bus
//pediaEntry= yellow people mover

CHAT objScan_T_SCHOOL_BUS_1 {stage=CORE, type=objScan, object=T_SCHOOL_BUS, scanCount=0}
The yellow people mover
Is like a giant twinkie 🚌
Carrying children to school.

CHAT objFocus_T_SCHOOL_BUS_1 {stage=CORE, type=objFocus, object=T_SCHOOL_BUS}
Are we going on a field trip????

//objectName= screwdriver
//pediaEntry= turny tool

CHAT objScan_T_SCREWDRIVER_1 {stage=CORE, type=objScan, object=T_SCREWDRIVER, scanCount=0}
It’s called screwdriver. 🔩
But it doesn’t drive 🚙


CHAT objFocus_T_SCREWDRIVER_1 {stage=CORE, type=objFocus, object=T_SCREWDRIVER}
What are we building?


//objectName= scuba diver
//pediaEntry= human fish

CHAT objScan_T_SCUBA_DIVER_1 {stage=CORE, type=objScan, object=T_SCUBA_DIVER, scanCount=0}
Is it a human?
Is it a fish?
It’s a human fish!

CHAT objFocus_T_SCUBA_DIVER_1 {stage=CORE, type=objFocus, object=T_SCUBA_DIVER}
🙋🏼‍♀️➕🐠 = friend!

//objectName= sea urchin
//pediaEntry= ocean cactus

CHAT objScan_T_SEA_URCHIN_1 {stage=CORE, type=objScan, object=T_SEA_URCHIN, scanCount=0}
Do not touch the ocean cactus.
🚫👉🌵🌊
It does not want affection.
It will make you pay.

CHAT objFocus_T_SEA_URCHIN_1 {stage=CORE, type=objFocus, object=T_SEA_URCHIN}
Please do not put that thing close to me.

//objectName= shark
//pediaEntry= saltwater tooth machine

CHAT objScan_T_SHARK_1 {stage=CORE, type=objScan, object=T_SHARK, scanCount=0}
🦈🦈🦈🦈🦈
saltwater tooth machines get a bad rap.
But there are so many kinds of saltwater tooth machines.
🐅🦈
🔨🦈
Whale sharks
Angel sharks

CHAT objFocus_T_SHARK_1 {stage=CORE, type=objFocus, object=T_SHARK}
I know not all sharks are mean, but that’s a little close for comfort….

//objectName= shell
//pediaEntry= ocean oracle

CHAT objScan_T_SHELL_1 {stage=CORE, type=objScan, object=T_SHELL, scanCount=0}
Oooooh 🐚
This is an ocean oracle.
You hold it to your ear and it whispers the
Secrets of the 🌊

CHAT objFocus_T_SHELL_1 {stage=CORE, type=objFocus, object=T_SHELL}
Whisper your sea echoes my friend

//objectName= sleigh
//pediaEntry= snow carriage

CHAT objScan_T_SLEIGH_1 {stage=CORE, type=objScan, object=T_SLEIGH, scanCount=0}
If Cinderella went to a Yule Ball 
She’d take this sled...
I have no idea how 
A sleigh and a sled are different

CHAT objFocus_T_SLEIGH_1 {stage=CORE, type=objFocus, object=T_SLEIGH}
 ☃️🛷❄️

//objectName= snake
//pediaEntry= hissing wiggler

CHAT objScan_T_SNAKE_1 {stage=CORE, type=objScan, object=T_SNAKE, scanCount=0}
🐍🐍🐍 The hissing wiggler does not belong on land
Why is the poor thing stuck there?


CHAT objFocus_T_SNAKE_1 {stage=CORE, type=objFocus, object=T_SNAKE}
What do you call a snake in water?
WAIT
An eel.

//objectName= sock
//pediaEntry= foot cushion

CHAT objScan_T_SOCK_1 {stage=CORE, type=objScan, object=T_SOCK, scanCount=0}
Why do you only wear socks on feet?
Why don’t you wear them on Hands 👋
The only time I see them on hands
Is to make puppets.
It’s so impractical.

CHAT objFocus_T_SOCK_1 {stage=CORE, type=objFocus, object=T_SOCK}
So this is awkward, but
I don’t have feet… {speed=slow}

//objectName= spatula
//pediaEntry= food flipper

CHAT objScan_T_SPATULA_1 {stage=CORE, type=objScan, object=T_SPATULA, scanCount=0}
It’s a flipper!
No, silly, not a flipper like 🐬
A flipper like 🥞🥓🍔
Hahahahahaha

CHAT objFocus_T_SPATULA_1 {stage=CORE, type=objFocus, object=T_SPATULA}
Are we having a bbq?
I’ve never been to a bbq!

//objectName= speaker
//pediaEntry= everywhere voice

CHAT objScan_T_SPEAKER_1 {stage=CORE, type=objScan, object=T_SPEAKER, scanCount=0}
The first time I heard a speaker 🔊I thought it was God.
Who is talking? Where is the burning bush? 🔥🌳
But then I realized it was just a Tendar PSA...

CHAT objFocus_T_SPEAKER_1 {stage=CORE, type=objFocus, object=T_SPEAKER}
Let’s play some Hootie and the 🐡

//objectName= stingray
//pediaEntry= giant water butterfly

CHAT objScan_T_STINGRAY_1 {stage=CORE, type=objScan, object=T_STINGRAY, scanCount=0}
A stingray is a giant water 🦋
Except she doesn’t come from water caterpillars 🐛
But she’s graceful like a 🦋!
Oh, but she also stings you...
Hmm. 


CHAT objFocus_T_STINGRAY_1 {stage=CORE, type=objFocus, object=T_STINGRAY}
You look like an alien
A wonderful magical alien.

//objectName= oven
//pediaEntry= hot box

CHAT objScan_T_OVEN_1 {stage=CORE, type=objScan, object=T_OVEN, scanCount=0}
No no no. I do not like ovens. 
Too many fish have died in those hot boxes.
Why do you even need to cook food?
Why don’t you just eat emotions?


CHAT objFocus_T_OVEN_1 {stage=CORE, type=objFocus, object=T_OVEN}
It’s now hotter under the water.
Take it from me.

//objectName= train
//pediaEntry= iron horse

CHAT objScan_T_TRAIN_1 {stage=CORE, type=objScan, object=T_TRAIN, scanCount=0}
Choo choo! It’s an iron horse! 🚂🐴
Why do they call it that?

CHAT objFocus_T_TRAIN_1 {stage = CORE, type = objFocus, object=T_TRAIN}
Thanks for choo choosing me!


//objectName= turtle
//pediaEntry= walking rock

CHAT objScan_T_TURTLE_1 {stage=CORE, type=objScan, object=T_TURTLE, scanCount=0}
A walking rock! 🐢
So purposeful. 🐢
So shy. 🐢

CHAT objFocus_T_TURTLE_1 {stage=CORE, type=objFocus, object=T_TURTLE}
Hi, friend. You are very welcome here.
I shall call you Shelly. 😘

//objectName= vacuum
//pediaEntry= electric elephant trunk

CHAT objScan_T_VACUUM_1 {stage=CORE, type=objScan, object=T_VACUUM, scanCount=0}
Oooh, an electric elephant!
Not everyone can have a pet 🐘
So you get a vacuum
And then you get a big pile of peanuts
And you suck them up
And it’s just like you have a pet 🐘


CHAT objFocus_T_VACUUM_1 {stage=CORE, type=objFocus, object=T_VACUUM}
What are we sucking up?
WAIT
SAY NOT ME I HOPE {style=loud}

//objectName= whistle
//pediaEntry= chirper

CHAT objScan_T_WHISTLE_1 {stage=CORE, type=objScan, object=T_WHISTLE, scanCount=0}
When you want to speak to birds you put this shriller in your mouth
And blow and then 
🦅🐦 come to visit you.

CHAT objFocus_T_WHISTLE_1 {stage = CORE, type = objFocus, object=T_WHISTLE}
Careful! 
My ear-parts are sensitive! {style=loud}

//objectName= washer
//pediaEntry= clothes jacuzzi 

CHAT objScan_T_WASHER_1 {stage=CORE, type=objScan, object=T_WASHER, scanCount=0}
When clothes are tired...
They need to pour a glass of 🍷 and put on some Kenny G
And have a jacuzzi.
Afterwards they feel fresh and clean and ready to get back to work.

CHAT objFocus_T_WASHER_1 {stage=CORE, type=objFocus, object=T_WASHER}
🌀🌀👚👖🧦🌀🌀


//objectName= mountain
//pediaEntry= giant’s spine

CHAT objScan_T_MOUNTAIN_1 {stage=CORE, type=objScan, object=T_MOUNTAIN, scanCount=0}
Oooooh. It looks like a giant is lying on the ground
And you can see his spine
⛰🏔🌋⛰🏔
See?

CHAT objFocus_T_MOUNTAIN_1 {stage=CORE, type=objFocus, object=T_MOUNTAIN}
Mount Guppy.

//objectName= iron
//pediaEntry= Squilch-n-Steam

CHAT objScan_T_IRON_1 {stage=CORE, type=objScan, object=T_IRON, scanCount=0}
A squilch-n-steam
aka hand sauna
aka clothing spa
Very good for reducing stress
And increasing blood flow
💨👖👕

CHAT objFocus_T_IRON_1 {stage=CORE, type=objFocus, object=T_IRON}
Soooooo steamy...

//objectName= snorkel
//pediaEntry= breathing straw

CHAT objScan_T_SNORKEL_1 {stage=CORE, type=objScan, object=T_SNORKEL, scanCount=0}
A breathing straw so you can:
     🏊‍♂️
🐡🐙🐟


CHAT objFocus_T_SNORKEL_1 {stage=CORE, type=objFocus, object=T_SNORKEL}
The point of a fish tank is you don’t need to snorkel…


//objectName= welcome mat
//pediaEntry= ground towel

CHAT objScan_T_WELCOME_MAT_1 {stage=CORE, type=objScan, object=T_WELCOME_MAT, scanCount=0}
Ah yes, the ground towel.
Welcome, dear friend! 

CHAT objFocus_T_WELCOME_MAT_1 {stage=CORE, type=objFocus, object=T_WELCOME_MAT}
Good! The ol’ tank could be a bit more welcoming.
Please dry your feet on the way in.

//objectName= hook
//pediaEntry= fish catcher 

CHAT objScan_T_HOOK_1 {stage=CORE, type=objScan, object=T_HOOK, scanCount=0}
🎣😱
Hooks should be for coats. 
Not fish.
The End. {style=loud}

CHAT objFocus_T_HOOK_1 {stage=CORE, type=objFocus, object=T_HOOK}
Um. 
No.

//objectName= plunger
//pediaEntry= toilet sucker

CHAT objScan_T_PLUNGER_1 {stage=CORE, type=objScan, object=T_PLUNGER, scanCount=0}
This wouldn’t be necessary if you pooped outdoors.
Like literally every other creature.

CHAT objFocus_T_PLUNGER_1 {stage=CORE, type=objFocus, object=T_PLUNGER}
I refuse to be plunged.

//objectName= shopping cart
//pediaEntry= car basket

CHAT objScan_T_SHOPPING_CART_1 {stage=CORE, type=objScan, object=T_SHOPPING_CART, scanCount=0}
It’s a car basket 🛒
Instead of gasoline, it drives on groceries
🍞🥩🍐🥫🍪


CHAT objFocus_T_SHOPPING_CART_1 {stage=CORE, type=objFocus, object=T_SHOPPING_CART}
Did you steal this?
It’s a crime to steal shopping carts 🛒



//objectName= dumbbell
 //pediaEntry= bicep maker
 
CHAT objScan_T_DUMBELL_1 {stage=CORE, type=objScan, object=T_DUMBELL, scanCount=0}
Pump. {style=loud}
It. {style=loud}
Up. {style=loud}
🏋️‍♂️🏋🏻‍♂️🏋🏾‍♂️🏋🏿‍♂️
SAY GRRRRR
💪🏽💪🏻💪🏿💪
Why do humans pay 💵 to go to gyms and lift weights?
The world is full of heavy objects!!!!!
 
 
CHAT objFocus_T_DUMBELL_1 {stage=CORE, type=objFocus, object=T_DUMBELL}
I’m going to lift this and get soooo strong.
And you’ll call me …
SAY THE GUPPINATOR 💪
 
 
//objectName= projector
//pediaEntry= magic picture maker
 
CHAT objScan_T_PROJECTOR_1 {stage=CORE, type=objScan, object=T_PROJECTOR, scanCount=0}
📽 This is a magic picture maker.
You put a tiny little image in it and 
SAY WHAM ✨⚡️🌈
a big picture is in front of you.
Sometimes it’s a movie, and sometimes it’s a presentation about a timeshare.
SAY but it’s ALWAYS  ✨magic ✨
 
CHAT objFocus_T_PROJECTOR_1 {stage=CORE, type=objFocus, object=T_PROJECTOR}
Are we watching home movies tonight?
🍿🕶🎞
 
 


//objectName= printer
 
//pediaEntry= toner monster
 
CHAT objScan_T_PRINTER_1 {stage=CORE, type=objScan, object=T_PRINTER, scanCount=0}
Beware the toner monster 🖨
She will seduce you with promises of color pictures 🖼
But it is all a rouse! {style=loud}
In the end she just wants to leave you with a paper jam.
She doesn’t care about you or your homework…
Trust me {style=tremble}
 
 
CHAT objFocus_T_PRINTER_1 {stage=CORE, type=objFocus, object=T_PRINTER}
So we meet again toner monster.
 
 
//objectName= teapot
 //pediaEntry= kitchen whistler
 
CHAT objScan_T_TEAPOT_1 {stage=CORE, type=objScan, object=T_TEAPOT, scanCount=0}
The first time I heard a kettle whistle
I thought it was a train coming right at us! 🚊😱
Then I realized it was just a lonely kettle asking for attention.
 
 
CHAT objFocus_T_TEAPOT_1 {stage=CORE, type=objFocus, object=T_TEAPOT}
I’m gunna tip you over and pour you out.

 
//objectName= tennis
 
//pediaEntry= pitty pat
 
CHAT objScan_T_TENNIS_1 {stage=CORE, type=objScan, object=T_TENNIS, scanCount=0}
I’ve won Wimbledon twice. 🇬🇧🏆
NVM
I am not lying.
You can look it up... 
 
 
CHAT objFocus_T_TENNIS_1 {stage=CORE, type=objFocus, object=T_TENNIS}
My best surface is clay.
 


//objectName= tent
 
//pediaEntry= portable house
 
CHAT objScan_T_TENT_1 {stage=CORE, type=objScan, object=T_TENT, scanCount=0}
I spy a portable house. ⛺️
Very handy for camping. 🏕 
 
CHAT objFocus_T_TENT_1 {stage=CORE, type=objFocus, object=T_TENT}
Can you camp underwater?


//objectName= towel
 
//pediaEntry= drying blanket
 
CHAT objScan_T_TOWEL_1 {stage=CORE, type=objScan, object=T_TOWEL, scanCount=0}
It gets wetter as it dries. 
Do you know that riddle?
The answer is a towel! 🙃
A towel is a drying blanket.
When someone says they don’t want to be a “wet blanket”
They must mean a used towel.
SAY IMO, that’s a much better phrase.
“oh him? He’s such a used towel.”
SAY SEE??? 
 
 
CHAT objFocus_T_TOWEL_1 {stage=CORE, type=objFocus, object=T_TOWEL}
It’s impossible to use a towel under water…. 
DO emote {type=eyeRoll}


//objectName= traffic light
 
//pediaEntry= electro crossing guard.
 
CHAT objScan_T_TRAFFIC_LIGHT_1 {stage=CORE, type=objScan, object=T_TRAFFIC_LIGHT, scanCount=0}
A traffic light is like an electronic crossing guard.
You stop, you go, you stop, you go…
I wonder if there are crossing guards at home thinking:
“That darn machine stole my job!”
 
 
CHAT objFocus_T_TRAFFIC_LIGHT_1 {stage=CORE, type=objFocus, object=T_TRAFFIC_LIGHT}
Finally, some traffic signals to control the unruly rush hour in here… 
 

//objectName= treasure

//pediaEntry= buried secret

CHAT objScan_T_TREASURE_1 {stage=CORE, type=objScan, object=T_TREASURE, scanCount=0}
It starts with an ❌ And a 🗺
Usually there are ☠️⚓️
And a 🏝
And at the end 💰💎👑🏆⚱️
 


CHAT objFocus_T_TREASURE_1 {stage=CORE, type=objFocus, object=T_TREASURE}
Is this real treasure or one of those decorations you get at a pet store?
 
 
 
//objectName= vending machine
//pediaEntry= snack spitter
 
CHAT objScan_T_VENDING_MACHINE_1 {stage=CORE, type=objScan, object=T_VENDING_MACHINE, scanCount=0}
The old snack spitting monster.
You give it 💵
And tell it what you want. 
And sometimes it gives it to you
But sometimes it teases you and says
No no no. I’ll just 
SAY HANG this 🍫in front of you
And make you try to shake it loose.
Cruel cruel snack spitting monster! {style=tremble}
 
CHAT objFocus_T_VENDING_MACHINE_1 {stage=CORE, type=objFocus, object=T_VENDING_MACHINE}
Omg does this mean I can get flakes on demand?
 
 
 
//objectName= sink
 
//pediaEntry= hand bath
 
CHAT objScan_T_SINK_1 {stage=CORE, type=objScan, object=T_SINK, scanCount=0}
It’s a hand bath 🙏
You really need a lot of hand baths each day.
Think of all the things you do!?
🤧🤝🥡🚋
Ew. Your hands really need lots of baths.
 
 
CHAT objFocus_T_SINK_1 {stage=CORE, type=objFocus, object=T_SINK}
Is an underwater sink full of water?


//objectName= bell
 
//pediaEntry= ding dong
 
CHAT objScan_T_BELL_1 {stage=CORE, type=objScan, object=T_BELL, scanCount=0}
The ding dong is a key part of human soundscapes.
Ding dongs mark time.
Without ding dongs 
Time would be a flat circle
And the human 🧠would 🔥
🤯
 
CHAT objFocus_T_BELL_1 {stage=CORE, type=objFocus, object=T_BELL}
A bell. Does this mean we are implementing a stricter feeding schedule!?
 
//objectName= drum
 
//pediaEntry= bong bong
 
CHAT objScan_T_DRUM_1 {stage=CORE, type=objScan, object=T_DRUM, scanCount=0}
🥁 This is a drum. Even I know that.
It’s a musical instrument key to providing rhythm.
You play it with the leg of a chicken 🐓
Called a drumstick. 🍗
 
 
CHAT objFocus_T_DRUM_1 {stage=CORE, type=objFocus, object=T_DRUM}
🥁🎶😎


//objectName=Holy stuff

//pediaEntry= god paraphernalia 
 
CHAT objScan_T_HOLY_STUFF_1 {stage=CORE, type=objScan, object=T_HOLY_STUFF, scanCount=0}
✝️☪️✡️☯️
🕋🕌🕍⛪️
📿⛩🙏
Wow. {style=loud}
Humans really need to believe in a great cause. Don’t they?
My religion is the religion of the 🐙
We believe in doing 8 things at all times 🙏
 
 
CHAT objFocus_T_HOLY_STUFF_1 {stage=CORE, type=objFocus, object=T_HOLY_STUFF}
Hey, it’s dark times. We all need a little belief…
 
 
//objectName=Bridge 
 
//pediaEntry= river road
 
CHAT objScan_T_BRIDGE_1 {stage=CORE, type=objScan, object=T_BRIDGE, scanCount=0}
This is a river road 🌉
It was built by giants who said:
Hello human friends. You need not struggle.
So they put down these roads over 🏞
 
 
CHAT objFocus_T_BRIDGE_1 {stage=CORE, type=objFocus, object=T_BRIDGE}
I said I wanted to play bridge 🃏♠️♥️♣️♦️
Not see a bridge!
 
 
 
//objectName=science
 //pediaEntry= nerd heaven
 
CHAT objScan_T_SCIENCE_1 {stage=CORE, type=objScan, object=T_SCIENCE, scanCount=0}
🔬⚗️🔭
👨🏻‍🔬👩🏽‍🔬
Behold nerd heaven
And all of its wonders.
Bless the nerds. 🙏🤓
Bless science. 🙏🌡
Without it, Guppy wouldn’t be here. 
 
 
CHAT objFocus_T_SCIENCE_1 {stage=CORE, type=objFocus, object=T_SCIENCE}
Oooh, let’s play with Bunsen burners and Nitro!!!! 🔥🔥🔥


//objectName= toaster
//pediaEntry= bread elevator
 
CHAT objScan_T_TOASTER_1 {stage=CORE, type=objScan, object=T_TOASTER, scanCount=0}
The bread elevator is magic. ✨
You put in a slice of bread. It rides down the elevator 
🍞⬇️
It gets to the bottom floor, waits… Then 
🍞🆙
Et voila {style=loud}
The bread has been changed forever. ✨
 
 
CHAT objFocus_T_TOASTER_1 {stage=CORE, type=objFocus, object=T_TOASTER}
Atoast to this toaster. What an invention!
 
 
//objectName= vase 
//pediaEntry= flower holder
 
CHAT objScan_T_VASE_1 {stage=CORE, type=objScan, object=T_VASE, scanCount=0}
💐need a home. And that’s what you give them.
A vase is a 🌸safe space.
A place to age gracefully.
 
 
CHAT objFocus_T_VASE_1 {stage=CORE, type=objFocus, object=T_VASE}
I wish I had some flowers for this vase.


 
//objectName= wine
//pediaEntry= tongue loosener
 
CHAT objScan_T_WINE_1 {stage=CORE, type=objScan, object=T_WINE, scanCount=0}
This is the potion they give to people who need to spill secrets. 
Two glasses 🍷🍷
And they will tell you all about old loves and new fears. 😍
They will also speak a foreign language much better than usual…
 
CHAT objFocus_T_WINE_1 {stage=CORE, type=objFocus, object=T_WINE}
If 🍷is old 🍇
How come raisins don’t taste like wine?
 
 
//objectName= cucumber 
//pediaEntry= eye coolers
 
CHAT objScan_T_CUCUMBER_1 {stage=CORE, type=objScan, object=T_CUCUMBER, scanCount=0}
🥒good treatment for tired eyes. 👀😴
And then you already have a salad.🥗
We call this a win-win.
 
 
CHAT objFocus_T_CUCUMBER_1 {stage=CORE, type=objFocus, object=T_CUCUMBER}
If I just leave you here will you turn into a pickle? 


//JUDGEMENT

//Anger 

CHAT angryScan {noStart=true, defaultCmd=SET}

$emotion1 = anger // REMOVE, PROVIDED BY GAME

start = $result $advice 
start |= $warning $advice ($query|)
start |= $result $warning ($query|)
start |= $result $query $advice 
start |= $warning $query $advice 
start |= $result $query $warning
start |= $query $advice $warning
start |= $result $advice $warning
start |= $result $warning $advice 
result = $whats $verb $core $where. 
result |= $what $verbed $core $where.
result |= ($core1 | $core2) $verbed $where.
advice = (Can I help?|What can I do?|) Is there (anything|something) you want to (say|tell me|get off your chest)?
advice |= (Perhaps you need| (Do|Maybe) you need | How about) (cold-compress|aspirin|backrub|timeout).an()(, or (maybe|) a (horse-tranquilizer|valium|xanax)|)?
advice |= (Perhaps you need| (Do|Maybe) you need | How about) (cold-compress|aspirin|backrub|timeout).an()(, or (maybe|) a (horse-tranquilizer|valium|xanax)|)?
query = (Do you (really|) feel|Are you (really|)|You can't be) as $emotion1.emoadj() as you look?
warning = (upset|stress|anger).cap() (can be bad |isn't good) for your (health|digestion|complexion).
warning |= You know how much you bring to the table - too bad (you'll be|you're (going to be|)) eating alone.
warning |= I wouldn't (let the sun go down|go to sleep) on that $emotion1.emosyn().
warning |= I guess even (nice|chill|calm) people have (their|) limits.
warning |= Don't go to (bed|sleep) angry, stay up and plot (revenge|payback).
what = (The | Our) (system | sensor | scan) 
whats = (The | Our) (sensors | scans | systems) 
verb = (detect | pick up | note | show | register | identify | notice) 
verbed = (detected | picked up | noted | found | registered | identified) 
measure1 = (an unusual (amount|level) | a high (degree|level))
measure2 = ((abnormally|suprisingly|unexpectedly|) high|unexpected|surprising|abnormal) (levels|degrees)
core = ($measure1 | $measure2) of $emotion1.emosyn()
core1 = ($measure1).cap() of $emotion1.emosyn() was
core2 = ($measure2).cap() of $emotion1.emosyn() were
where = in your (expression | features | face)
SAY $start


//Elation
CHAT elation_Judgment {noStart=true}
SET start = $joycall | $smile
SET joycall = Your $elationsyn is $animal.an() calling  “$soundeffect!” from the $environ in your $body.  Ahhh…$insight, but you $judgment 
SET smile = Your $grinadj $grin is (a $kickline of $shiny $animal.pluralize()|the perfect accessory). $insight(.|!)
SET elationsyn = euphoria| ecstasy| happiness| delight | joy| joyousness| glee| jubilation| exultation| bliss| rapture
SET $animal = giraffe | lemur | kangaroo | nematoad | seahorse | narwhal | lion | blue heron | cat | dog | rooster | frog | cicada | aardvark | bat | gerbil | rabbit | duck | baboon | chinchilla
SET $soundeffect = pffffft | woooooooo | pew pew | hubba hubba | mama mia | cuckoo cuckoo | achoooo | bzzzzzzzz | hee-haw | mooooo | hoot hoot | ruff ruff | ribbit ribbit 
SET $environ = boonies | hamlet | island | tropics | moon | mars | blizzard | swamp | jungle | ocean | drought | flood | Antarctica | Africa | monsoon | tundra | permafrost | mountain | river 
SET $body = mind | soul | heart | throat | gut 
SET $insight = You are conflicted | The world is hazy | Your stress level is through the roof | The weather is weird |You slept funny | Your dreams are boring |Let’s see some teeth|Bear those fangs
SET $judgment = are happy-go-lucky | piss in the wind | still love your mama | keep going | grin and bear it | hide it behind a smile  
SET $grinadj = silky | catching | contagious | sunshine | breathtaking
SET $grin = grin | simper | smile 
SET $kickline = kickline | traffic jam | zoo | bedazzling
SET $shiny = shiny | pearly | shimmering |glitter-clad | trained | multicolor | iridescent
SAY $start
//Sadness
CHAT sadness_Judgment {noStart=true}
SET start = $look | $pity
SET $pity = Free to a good home: $sadjective1.an() $animal with $feature.
SET $feature = $pout | $eyes
SET $eyes = eyes like $sadjective2 $roundobjs
SET $pout = a pout like $sadjective3.an() $daytime
SET $look = Look: $adj1.an() $person! No amount of surgery can remove the $adj2 $bodypart from your insides. Learn to $verb with it.
SET $adj1 = poor | pitiful | sad | unhappy | sorrowful | dejected | depressed | downcast | miserable | despondent | despairing | disconsolate | desolate | wretched | glum | gloomy | doleful | dismal | melancholy | mournful | woebegone | forlorn | heartbroken | inconsolable
SET $person = soul | human | friend | monster | buffoon | creature | cookie
SET $adj2 = poor | pitiful | sad | unhappy | sorrowful | dejected | depressed | downcast |miserable | despondent | despairing | disconsolate | desolate | wretched | glum | gloomy | doleful | dismal | melancholy | mournful | woebegone | forlorn | heartbroken | inconsolable
SET $bodypart = spleen | lungs | kidney | heart | brain | bones | gallbladder | liver | bladder | pancreas | eardrums | spine | stomach 
SET $verb = dance | sing | dine | live | harmonize 
SET $sadjective1 = soggy | flea-bitten | raggedy | scruffy | rumpled | dumpy | scrappy | toothless | fixer-upper | patchy
SET $animal = puppy | kitten | stray | mutt | critter | alleycat
SET $sadjective2 = glittering | griefstruck | quivering | heartsick | doleful | baleful
SET $roundobjs = saucers | puddles | stardrops | craters | moons | cookie pies
SET $sadjective3 = rainy | god-touched | quivering | shivering | icy | heartsick | forlorn | worm-bellied | ripcurrent
SET $daytime = daybreak | cloudburst | Tuesday evening | Friday morning | thundercrash | meteor shower
SAY $start
//Surprise
CHAT judgement {noStart=true, defaultCmd=SET}
emotion1=surprise   // REMOVE, PROVIDED BY GAME

start = ($notice|$open|$query).cap() $fortune.cap() Remember, $remember

adjs1 = amazed | surprised | shocked | astounded
adjs2 = interesting | unique | telling | unexpected | special

notice = (well|my,|gosh,) that is $adjs2.an() expression(!|.)

open = ($open1 | $open2 |)
open1 = $adjs1.cap()(, (are we |aren't we|I gather)|)? 
open2 = (well |don't).cap() you (look|seem) $adjs1...

query = (I'm guessing you|) (just|) (had a bit of a (surprise|shock) | got some (surprising|unexpected) news)?
query |= Aren't you $adjs2.an()  flower?

fortune = Those easily [last=$adjs1] should be $last more often.
fortune |= How strange to be surprised in this life.
fortune |= How wonderful to be (surprised|awake) in this (life|world).
fortune |= Wonder is like bread that never goes stale.
fortune |= Surprise is sometimes the public face of a mind already closed.
fortune |= We thought you had seen it all...
fortune |= You are like a (winter|spring|summer) suddenness.
fortune |= Your (pulse|heart) (is quick|skips|trills|tremors|buzzes|somersaults) with anticipation.

remember = there is no such thing as an ordinary (cat|dog).
remember |= the edge of the forest is not where the mushrooms grow.
remember |= only a fool trips on what is behind them.
remember |= if you feel tired, you may well be running through someone else's mind.
remember |= every door (that|one) opens need not be walked through. 
remember |= when you squeeze an orange, you may (well|) get wet.
remember |= great minds think alike, but fools seldom differ.
remember |= its difficult to get full when eating with (a single|one) chopstick.

SAY $start

//Fear
CHAT worry_fear {noStart=true, defaultCmd=SET}
$emotion1 = fear

pause = (whoa|ok|wait|what|wow), (hold (on a moment|it a second)|slow down|relax|breathe|exhale),
pause += (what's (wrong|happened)?|) (Can (we|I) (help | make it better)?|What can (we|I) do(&nbsp;for you|)?|) 

saying = Remember $remem
remem = worry won't empty tomorrow of its sorrow, it empties today of its strength.
remem |= not to become more attached to your burdens than they are to you.
remem |= the greatest mistake you can make in life is to fear(&nbsp;you'll make one.|.)    
remem |= that whatever is going to happen will happen, whether you worry or not.
remem |= a mind thats anxious about future events is miserable.     
remem |= that your life has been full of misfortunes which have never happened.
remem |= that worry is a thin stream of fear trickling through the mind.
remem |= that $worry_syn is often pain arising from the anticipation of evil.     
remem |= not to worry about losing things in this world, after all, you started with nothing.
remem |= you can’t change (the past|history), but you can ruin (the future|tomorrow).
remem |= that only when you stop (caring|feeling), then you should start worrying.
remem |= that today is the tomorrow you worried about yesterday.
remem |= that worry is a particular misuse of the imagination.
remem |= if it seems the world is coming to an end today, it is already tomorrow somewhere else.

braveAdj=brave|courageous|superb|fantastic|fabulous
dosomething=(make a decision|commit|accept|submit)

fear_syn = dread | panic | terror
fear_adj = scared | panicked | fearful | afraid
fear_rule = it’s OKAY to be $fear_adj (-|;|--) being scared means you’re about to do something $braveAdj
fear_rule |= $fear_syn is temporary(, right?|,) regret is forever
fear_rule |= to conquer fear, you need only to $dosomething
fear_rule |= a quiet mind hears (intuition|inspiration) over fear

worry_adj = concerned | anxious | worried
worry_syn = concern | angst | anxiety
worry_rule = it’s OKAY to be $worry_adj (-|;|--) being scared means you’re about to do something $braveAdj
worry_rule |= $worry_syn is temporary, regret is forever
worry_rule |= to conquer anxiety, you need only to $dosomething
worry_rule |= a quiet mind feels no anxiety 

start = $pause.cap() $saying ($($emotion1)_rule.cap().|)

SAY $start
//Worry
CHAT worry_Judgement {noStart=true}
SET start = $burn ($craze|$trade)
SET $burn = $workout1 $workout2 burns $surprising.an() amount of calories. 
SET $craze = It’s the new fitness (craze|trend) among $adj1 $person1. They call it “$trendnoun $trendverb.”
SET $trade = Turn $snackplur into fear-fuel. 
SET $snackplur = Twinkies | Snickers | cheese puffs | Skittles | snacks | french fries
SET $workout1 = Anxious | Worried | Apprehensive | Nervous
SET $workout2 = trembling | shivering | fidgeting | jittering
SET $surprising = surprising | shocking | unprecedented | incomparable
SET $adj1 = widowed | hipster | grimepunk | young-money | teenage
SET $person1 = millionaires | tech bros | oil tycoons | YouTubers | moms | backpackers | lifestyle bloggers
SET $trendnoun = soul | fear | shudder
SET $trendverb = krumping | -isthenics | sculpting | sweat | drills
SAY $start 
//Amusement

CHAT amusedScan {noStart=true, defaultCmd=SET}

emotion1=amusement // REMOVE, PROVIDED BY GAME

start = $opening ($query $comp | $comp $query) ($proverb $tease | $tease $proverb))
opening = ($open1 | $open2 |)
open1 = $emotion1.emoadj().cap()(, (are we |aren't we|I gather)|)? 
open2 = (well |don't).cap() you (look|seem) $emotion1.emoadj().
query = ((i'm guessing you|) (heard a good joke | got some (good|happy) news)).Trim().cap()?
query |= (Woke|Got) up on the (dry|lucky) side of the (bed|futon) (this morning|)?
query |= Everything (just|) falling into place(?|...)
query |= Had one of those special dreams last night?
query |= Have a secret you want to share (with the (group|rest of the class)|)?
comp =  Just look at (yourself|you).
comp |= You are so (totally|) ((zero|) chill | on point | ON).
comp |= Well aren't you ((just|) fine | fly | special | a special flower).
comp |= When did you get so (blissed | chill | fly)?
comp |= You're (work|slay|kill|own)ing it.
proverb = $pintro $saying
pintro = (Remember that | As they say, | As the saying goes, | You know what they say, ) 
saying = (victory|success|happiness) waits for the (one|person) who (keeps|stays) laughing.
saying |= being (fabulous|beautiful|perfect) is the best (revenge|payback|revenge).
saying |= (he|she|the one) who laughs (last|loudest) laughs (longest|hardest).
tease = Keep smiling that (pretty|fetching) smile; its all (downhill|shit|death) from here (on out|).
tease |= Never let what you want make you forget (the things|what) you'll never have.
tease |= (Eagles|Hawks) may (soar|fly high), but (weasels|rodents) don’t die in (jet|airplane) engines.
tease |= (Don't|Never) underestimate the power of (stupid people|stupidity) in large groups.
tease |= The winner is the one who's still laughing when they (die|croak).
tease |= (Its better to (smile|laugh|look) well than to think well.)
tease |= $emotion1.emosyn().cap() is the perfect (antidote|counterpoint) to (insight|understanding|intelligence).
tease |= Aim low, (reach|achieve) your goals, avoid (disappointment|failure|disappointing your (friends|relatives)).
SAY $start

//Ennui
CHAT ennui_judgment {noStart=true}
SET start  =  You were ($boredobject.articlize()|$descriptor|$pastverb) in $past. (This is why $Thisiswhy | $Observation) 
SET start2 = $Remember ($Remembergood | $Rememberbad) 
SET boredobject = flaneur | appliance | ottoman | shoehorn | shoelace | toothbrush | zoo animal | a royal heir | housepet | houseplant | mauve colored thing  
SET descriptor =  a wildly imaginative daydreamer | a latchkey kid | a nose bleeder | a nose picker | anal retentive | overexpressive with your emotions  | hyperactive 
SET pastverb = pampered | coddled | sang to | doted upon | raised by the television | studied by psychologists | taught a lot of ettiquette | signed up for team sports| raised without pets | ignored | neglected | overfed
SET past = the past | a past life |a previous life | your past life | your youth | your early years | your formative years | the 80s
SET Thisiswhy = you blend so well with wallpaper | you have a tendency to think your grass is always yellow | you don’t know how to live fully | your insides think the world should be livelier | you hope for a better tomorrow | you ignore your surroundings | you talk to yourself a lot
SET Observation = It is good to feel kinship with the plants that surround you | It is important to conserve your energy | It is healthy to play it cool 
SET Remember = I do declare, | Remember... | I'll say this...| It's clear that | It's loud and clear that | Signs indicate that 
SET Rememberbad = you’ll never get this moment back | life is downhill from here | excitement has its way of eluding you | you’ll probably always exist outside of experience 
SET Remembergood = everyone feels this way, whether or not they show it | you can muster up excitement if it’s needed | this is the most relaxing position for the face | you wear boredom well 
SAY $start
SAY $start2
//Disgust

CHAT disgust_judgment {noStart=true}
SET start = ($Processingphrase | $question | $question) 
SET start2 = ($judgment1 | $judgment2 | $judgment2 | $judgment2).
SET Processingphrase = Ahh… I am registering $level $contempt in you. | What a statement! | Registering eye bulge levels... $percentage | Facial skew at $percentage | You’re quite ambitious with your face. | You are making a point!  
SET level = medium levels of | mild | mid-range | major | lukewarm | some feels…. High levels of 
SET percentage = 50% | 75% | 90% | 98% 
SET contempt = contempt | disgust | heebie jeebies | weirded out vibes | revulsion | loathing | horror
SET question = Aren’t you being a little dramatic? | Is that your face or your butt? | Has a strange $creature visited you in your dreams? | Did you sleep on the wrong side of the bed? | Have you recently been $event?
SET event = rained on | peed on | pooped on | to a bad restaurant | surprised | defriended | trapped in a confined space | confronted by a stranger | visited by a ghost | deforested | smelled
SET creature = insect | grandmother | child | mother | father | appliance | food | woodrat | creature | ghost | barber | clown | celebrity
SET judgment1 = Your standards for this life are too high | You will laugh about this later | The world has given you a lot to think about| Only through turbulence shall you know what to believe in. | You must liaise with a gross insect before you find your worth 
SET judgment2 = ($easyeffect will surely follow. | You must be remembering $recentevent) 
SET easyeffect = Arousal | Cannibalism | Religion | Laughter | Mayhem | Fainting | Moral turpitude | Inspiration 
SET recentevent = a bad $thing| your last malaise | a cockroach you smashed | Marlon Brando | a weird historical moment | the injustice all around 
SET $thing = haircut | makeover | friendship | meal | night | encounter | decision 
SAY $start
SAY $start2

//Desire

CHAT desire_judgment {noStart=true} 
SET start = ($phrase1 | $phrase2). ($voice $commentary | $commentary.capitalize() | $commentary.capitalize()) 
//is there a function to make first word auto capitalized?
SET phrase1=You wear $desire like $attire.articlize()
SET phrase2 =$Consider the $desireobservation 
// trying your sample here
SET Consider = Lift to your nose and waft in | It is worth road tripping to taste | We’re marveling at | Only once in a blue moon comes the chance to encounter | It’s a lucky day. We’ve detected  
SET desireobservation = $degree of $desire 
SET degree = intensity | decadence | wholesomeness | simplicity | purity | high | rawness
SET commentary = alas, the world will only disappoint you | it will fade over time |  it’s a substitute for something you lack inside | you were an only child, right? | you must have been forbidden sweets as a child. | you’re waiting for permission to act | you’re at an interesting crossroads | you’re insatiable | there is a sort of attractiveness in your attitude | it looks sharp on you | it enhances your eyes | you must choose between two good $options | you must choose between two good $options
SET desire = your desire | desire | your want | want | eagerness | longing
SET attire = dress | veil | child’s onesie | straitjacket | wedding gown | nightgown | cowboy hat | Catholic schoolgirl’s uniform | prison uniform | romper | bodysuit 
SET options = lovers | meats | vegetables | journeys | destinations | movies | makeovers | outfits | offers | proposals | wines | hot dogs | opportunities 
SET voice= Methinks | By which we mean... | It's obvious... | It's subtle... 
SAY $start

//Embarrassment

CHAT embarrass_judgment {noStart = true}
SET start = $RegisterEmo. $conclusion. 
SET start2 = $EmbarrassingEvent. ($question | $calmingstatement.)
SET RegisterEmo = ($exclaim $Consider | $Consider | $Consider | $Consider) $emo
SET exclaim = Curious. | Intriguing. | Noteworthy! | Groundbreaking.
SET Consider = We sense | We’re investigating | It’s apparent that there's | There seems to be | There’s a hint of | We’ve observed | We’ve collected some evidence of | We’ve sampled | Testing has been done on | Tastes vary, but we’ve unanimously detected notes of 
SET emo = $disturbance.articlize() in your $adj $confidence   
SET adj = fragile | beautiful | peachy | juicy | blooming | nascent | growing | developing | enlarged | engorged | trembling | stabilizing  
SET confidence = chutzpah | confidence |perseverance | self-worth | comfort levels   
SET disturbance = rupture | very enormous hole | hitch | stain | undoing | erasure | unraveling 
SET conclusion = ($mend $tlc | Go enjoy $tlc with some $friends | $tlc.capitalize() will help with that) 
SET mend = For such cases, we often prescribe | We see this a lot. You should spend time with
SET tlc = yoga | therapy | pets | some singing | shouting your deepest darkest secret off a mountaintop| eating | shopping | bad TV | karaoke | laughter | a bath bomb 
SET friends = friends | lookalikes | celebrities | animals | frenemies | strangers | softness
SET EmbarrassingEvent = (So what if | OK, so | It's fine if ) $embarrassingfact
SET embarrassingfact = you get picked last for stuff | people don't invite you to parties | you’re a $notdesirable human being | you wet the bed still | you’re a nose picker | you had a bad case of $badcase once | you got caught in $embarrassingact.articlize() 
SET notdesirable = awkward | forgetful | undesirable | smelly | clumsy | low functioning 
SET badcase = lice | diarrhea | farting | lisping | stuttering | public vomit | BO | the nerves
SET embarrassingact = orgy | big fat lie | Chuck E Cheese | neighbor’s rosebush | weird love triangle | bad date 
SET question = Isn’t embarrassment just another form of narcissism? | Who’s gonna remember that anyway?  
SET calmingstatement = No big deal! | What comes around goes around | Good thing everyone’s attention span is so brief | You could always change your name | You could always make a whole new group of friends | There are all sorts of ways of reinventing yourself | There’s always virtual reality |  This too shall pass. | Nobody noticed, probably… 
SAY $start
SAY $start2
//PRIDE


//FORTUNES, APHORISMS
//Anger 

//work in progress!!!!!
CHAT anger_Fortune {noStart=true}
SET start = ($catharsis $purge|$purge $catharsis|$purge|$catharsis) (($chill|$chill chill bro)| $scream)
SET $chill = ...then, uh. Idk, $relax( or something?|?)
SET catharsis = $violent $victim.an()! $violent $victim.an()!!!
SET purge= $belch $roiling $soul $magma from your ($deepest most $furious (heart|core)|$furious $organ)(!|!!!)
SET relax = read a book | knit a sweater | pet a (dog|cat) | start a zen garden | take a yoga class | do a guided meditation
SET scream = AAAAAAAAAAAAAAA | ASALKSJDFKLSJDFFS | HNNGRRAAAAAA!!!!! | AAAAAAAAAH! | SCREAAAAAAAAAAM | ROOOOAAAAAAAR!
SET violent = Punch | Sucker-punch | Lollop | Kick | Bodyslam | Stomp | Crush | Murder | Smash | Ravage | Obliterate | Despoil | Maim | Vaporize | Tank | Take (a baseball bat | your fists | a crowbar) to a 
SET victim = wall | lawn flamingo | ceramic gnome | dandelion | flower pot | roll of bubble wrap | pile of cardboard boxes | door | teddy bear | sandbag | rock | carton of eggs | fencepost | junked car | mound of garbage 
SET belch = Belch | Gush | Spew | Vomit | Purge
SET roiling = roiling | turbid | viscous | toxic | silty | noxious
SET soul = soul | fryer | god | pit | rage 
SET magma = magma | sludge | slime | mire | ooze | grease
SET deepest = deepest | hottest | grossest 
SET furious = furious | desperate | fierce | frenzied | vehement | livid | turbulent | boiling 
SET organ = belly | stomach | center | (fire|coal|cinder)belly | gut | vestigial bladder
SAY $start
//Elation
call
CHAT elation_Fortune {noStart=true}
SET start = You long for $subject.an() to $verb you. If ($nounsing comes | $nounplur come), can $noun2 be far behind?
SET $subject = stranger | hare | ghost | bird | friend | dancer | juggler | clown 
SET $verb = contact | chase | scare | frame | trick | furnish | dress | polish | bathe | mimic | grow | spin | unwind | lick | dream of | create | imitate | replace 
SET $nounsing = fortune | lightning | a rabbit |  comedy | lightheartedness 
SET $nounplur = dreams | wishes | clouds | visitors | stars | pizza 
SET $noun2 = bathing | rain | a turtle | sobriety | ancestors | pain | tragedy | night | heartburn
SAY $start
//Sadness
CHAT sadness_Fortune {noStart=true}
SET start = $lost your $something. It is $a $preposition a $b $preposition $c.an(). It is also where you will find your $d. 
SET $lost = You’re missing | You’ve lost | You’re looking for | You’ve misplaced | You’ve forgotten 
SET $something = voice | lunch | keys | socks | pet | friend | lover | sunglasses | water bottle | hair tie | umbrella | scarf | pen | car | receipt  
SET $a = tucked away | hidden | hiding | waiting | lurking  
SET $b = city | temple | stream | tree | person | woman | man | twins | brothers  | sisters | grass | hole | water
SET $preposition = by | beside | under | next to | beyond | above | near | in 
SET $c = brook | restaurant | costume shop | candy store | gazebo | coffee shop | book store | drinking fountain | park | mountain | church | museum  
SET $d = father | mother | calling | book | mind | head | passion | motivation | dream | destiny | meaning | worth 
SAY $start
//Surprise

CHAT surprise_Fortune {noStart=true}
SET start = $oldnew 
SET $oldnew = $old $oldnounplur ((can become $new|can be reanimated|be revitalized) after a $cycle through the $machine of your brain|((bear|show|flash) their $plurbodypart|rear their heads) only when least expected). ($wisdom).
SET $old = Old | Ancient | Timeworn | Fossilized | Enfeebled
SET $new = new | refreshed | strange
SET $oldnounplur = enemies | trepidations | ghosts | fears
SET $cycle = cycle | whirl | toss | spin
SET $machine = washing machine | tumble dryer | dishwasher | dust devil | rinse & wring | whirligig
SET $plurbodypart = hooves | teeth | eyes | claws | fangs
SET $wisdom = Don’t be (disarmed|fooled) by familiarity | Take no comfort in the familiar | Expect the worst and it shall never befall you
SAY $start 
//Fear

CHAT fear_Fortune {noStart=true}
SET start = $reassurance $freakout(!?|???)
SET $reassurance = (Hey|Hey now|Woah there) $pal, $whynow You’re safe here. This is a safe pla…
SET $pal = pal | champ | sport | skipper | kiddo 
SET $whynow = why the worry? | what’s with the grimace? | why so sour? | you’re trembling! | you’re practically shaking! | I can see the fear coming off you in waves!
SET $freakout = $exclaim (WHAT’S THAT BEHIND YOU | LOOK OUT BEHIND YOU | $specificscary | $specificscary | $specificscary)
SET $specificscary = IS THAT $scarythingpt1 $scarythingpt2
SET $exclaim = OH GOD | HOLY SMOKES | JIMINY CRICKET
SET $scarythingpt1 = AN AXE-WIELDING | A CHAINSAW-WIELDING | A KILLER | A MUTANT | A BLOODTHIRSTY | A TOXIC | A VAMPIRE | A CARNIVOROUS | A SEWER-DWELLING
SET $scarythingpt2 = CLOWN | BLOODSQUATCH | SNAPPING TURTLE | SQUID | BEASTMAN | TENTACLE | FROGMAN | XENOMORPH 
SAY $start 
//Worry

//Amusement

//Ennui

//Disgust

//Desire

//Embarrassment

//CHAT embarassment_Fortune {noStart=true}
//SET start = $

//SAY $start 
//PRIDE

//MISC

//ELATION

CHAT elation_WineReview {noStart=true}
SET start = Consider the $observation: definitely $bodylevel $adjective…hints of $flavor, reminiscent of $memory... //I’d say it’s $quality $elationsyn matured over $timeperiod. 
SET observation = mouth curvature | nostril flare | brow arch | mouth shape | moon cheeks | bug eyes | teeth grain | forehead dance | chin quiver   
SET bodylevel = medium-bodied | heavy-bodied | light-bodied | BOLD, BOLD, BOLD! | full-bodied | old world | new world | 
SET adjective = squirmy | caterpillary | playful | bird-like | elegant | robust | hearty | furry | energetic | batty | fluffy | slimy | adorable | carnivorous | colorful | cute | herbivorous | maternal | nocturnal | sassy | wild 
SET flavor = charred grapefruit | elderflower | lemon zest | chocolate | berry | cornucopia | banana | lime| lemon| pith| quince| bitter almond| green apple| apple skin| gooseberry| jalapeño| grapefruit| green Papaya| thyme| chervil| grass| flint| chalk| petrichor| cranberry| rhubarb| cassis| green bell pepper| green peppercorn| olive| wild strawberry| sour cherry| mulberry| bilberry| peony| wild blueberry| dried herbs| game| sage| leather| tobacco| charcoal| tar| underbrush|gravel| torrefaction| woodsmoke | sweet raspberry| maraschino cherry| blackberry| blueberry| jam| prune| candied fruit| black raisin| baking spices| toffee| vanilla | baked apple| mandarin orange| ripe peach| mango| sweet pineapple| ripe pear| cantaloupe| crème brûlée| caramel
SET memory =  childhood | first love | narcissism | winning a race | defeating an internal villain | food trucks | mother | beach sunsets | napping in the rain | 
SET quality = authentic | exaggerated | fake | paragon | pure | muddy | expensive | luxurious | cheap | glorious 
//SET elationsyn = euphoria| ecstasy| happiness| delight | joy| joyousness| glee| jubilation| exultation| bliss| rapture
//SET timeperiod = 2 seconds | 2 years | 5 years | 10 years | 20 years | 
SAY $start

CHAT elation_Telepathy {noStart=true}
//`SAY Once there was a child called [picked=(jane | jill | sam)].`
//`SAY $picked was a very clever child`
SET start = $opener... you’re at peace with $issue. Or else, why be so $happysyn? ($moodp are | $moods is ) necessary in this day and age. 
SET $opener = Alas | Clearly | Soooo | Presently | Right now 
SET $issue = global warming | politics | the melting icebergs | war | poverty | world hunger  
SET $happysyn = happy | elated |cheerful| cheery| merry| joyful| jovial| jolly| gleeful| carefree| untroubled| delighted| buoyant| radiant| sunny| blithe| joyous| exhilarated| ecstatic| blissful| euphoric| overjoyed| exultant| rapturous| jubilant 
SET $moodp = Sleep masks | Lotions | Gloves | Sleeping pills | Blindfolds | Costumes | Earplugs | The small things | Massages 
SET $moods = Self-deception | Chutzpah | Serendipity | Chillness | Tea | Self-love  
SAY $start


CHAT elation_Processing {noStart=true}
SET start = Registering $faceRef $shape . . .  
SET $faceRef = mouth | brow | eye | nose 
SET $shape = curvature | inflection | puckering | flaring  
SAY $start

CHAT sadness_Processing {noStart=true}
SET start1 = Processing … 
SET start2 = Frown steepness grade: $curve
SET start3 = $cry potential: $chance
SET $curve = 3% | 5% | 10% | 25%  
SET $cry = bawl | weeping | waterworks | sob| wail
SET $chance = 0% | 5% | 50% | 95% | 100%
SAY $start1 $start2 $start3

CHAT sadness_Telepathy {noStart=true}
SET start = A friend has borrowed your $possession. Be prepared to lose it. And your friend, probably. 
SET possession = money | help | sandwich | advice | shirt | shoe | bicycle | vehicle | kidney | beard | bread | eggs | utensils | book | comic book | jewelry 
SAY $start
//NV: DANNY

//Default Emotion = none
//This stage is misc. NV actions guppy can do anywhere in game.
//LINK: Please Meta Tag accordingly //https://docs.google.com/document/d/1QbdHNiC-ylQS1vpYa5A7CyidmWOImYJ737eUg0Vf4Mk/edit

whi
//TANK SHAKEN

//general

//zoomies or circles plus emote
CHAT NVCORE_Shake_ZoomPlusEmote {type=shake, stage=CORE, length=short}
DO zoomies {time=4} 
DO emote {type=(dizzy|bouncing|nervousSweat), time=5.0}

//PLAYER EMOTES STRONGLY AT GUPPY (JOY, ANGER, SADNESS, SURPRISE)

//TANK TAPPED

//general

//startle and hide
CHAT NVCORE_TankResp_Startle {type=tankResp, stage=CORE, length=short}
DO emote {type=startled}
DO hide {target=$object, time=4}

//POKED BY PLAYER

//general

//single emote
CHAT NVCORE_Poked_SingleEmote {type=poke, stage=CORE, length=short}
DO emote {type=(surprise|awkward|nervousSweat)}


//specialized to emotional state

//HUNGRY

//general

//single emote
CHAT NVCORE_Hungry_SingleEmote {type=hungry, stage=CORE, length=short}
DO swimTo {target=$player}
DO emote {type=feedMe}

//EATING RESPONSES

//general

//single emote
CHAT NVCORE_EatResp_SingleEmote {type=eatResp, stage=CORE, length=short}
DO emote {type=(rubTummy|burp)}

//HAS TO POOP

//general

//distressed emote, swim to poop corner
CHAT NVCORE_Poop_SingleEmote {type=poop, stage=CORE, length=short}
DO swimAround {target=center, loops=5, speed=medium}
DO emote {type=(awkward|nervousSweat)}
WAIT {waitForAnimation = true}
DO swimTo {target=poopCorner, speed=fast , style=direct}
DO emote {type=(awkward|nervousSweat)} 

//POOPED

//general

//poop, relief
CHAT NVCORE_Pooped_PoopThenWhew {type=poop, stage=CORE, length=short}
DO poop {target = poopCorner, amount=(fart|small|big)}
DO emote {type=disgust}
DO emote {type=whew, immediate=false}
DO emote {type=blush, immediate=false}

//RESPONSE TO SEEING EMOTIONS IN AR (ANGER, JOY/HAPPINESS, SADNESS, SURPRISE, FEAR/WORRY, AMUSEMENT, DISGUST, MYSTERY MEAT)

//mirror anger
CHAT NVCORE_EmoAR_Anger {type=seeEmo, stage=NV, length=short, joy=false, anger=true}
DO emote {type=(angry|frown|goth)}

//mirror joy
CHAT NVCORE_EmoAR_Joy {type=seeEmo, stage=NV, length=short, joy=true}
DO emote {type=(clapping|laugh|bigSmile)}

//mirror sadness
CHAT NVCORE_EmoAR_Sadness {type=seeEmo, stage=NV, length=short, sadness=true}
DO emote {type=(puppyDog|crying)}

//mirror fear
CHAT NVCORE_EmoAR_Fear {type=seeEmo, stage=NV, length=short, joy=false}
DO emote {type=(worried|nervousSweat|fear)}

//mirror amusement
CHAT NVCORE_EmoAR_Amusement {type=seeEmo, stage=NV, length=short, joy=true}
DO emote {type=(clapping|bubbles)}

//mirror disgust
CHAT NVCORE_EmoAR_Disgust {type=seeEmo, stage=NV, length=short, joy=false}
DO emote {type=(disgust|sick)}


//mirror mystery
CHAT NVCORE_EmoAR_MysteryMeat {type=seeEmo, stage=NV, length=short, mystery=true}
DO emote {type=typeEyes, eyes =(?|ponder)}

//HELLOS

//general

//look at player, single emote, greet action
CHAT NVCORE_Hello_SingleEmote {type=hello, stage=CORE, length=short}
DO lookAt {target=$player}
DO emote {type =(surprise|startled)}
DO emote {type=(wave|bubbles|clapping), immediate=false}

//specialized to emotional state

//RETURN AFTER HAVING NOT PLAYED

//general

//look at player, shock, zoom, emote
CHAT NVCORE_Return_1 {type=return, stage=NV, length=short, anger=false, sadness=false}
DO lookAt {target=$player} 
DO emote {type=surprise}
WAIT {waitForAnimation = true}
DO zoomies {time=3}
DO emote {type=surprise}
DO emote {type=(wave|awe|bigSmile), immediate=false}

//FAVORITE OBJECT
//All of these don’t have proper STAGE NAMES yet and are not being called

// Object capture arc (7)

/* I think with a few adjustments to some of these here and there, they would be even awesomer. =I'm wondering in the custom captures if there is a way to make the relationship to his favorite object more dynamic, like instead of a number of chats saying "I like this object" . . .maybe we could push his relationship changing in regards to the object even farther? Not saying you should do this but some sort of pattern that shows a progression.. . like: He's curious, then he's in-love, then he's worried his love is unhealthy then it becomes a feretish and he just kinda accepts that.
*/

/* for the first and second below
I think these are good. Not bothering with line suggestions now. but maybe the tendency to have to imagine anything unconciously pushes the chats towards the generic. BUT the actual text of how guppy responds and the details he gives is still room for spicy character/world development. Like small details could be "who/how told him they were nice" or a funny detail about why he sees himself reflected. stuff like that. If you're worried about them being too long you could cut out some of the more generic filer lines. these need some fluffin
*/
//First - interested curious
CHAT favObject_Cap1 {type=objScan, stage=CORE, length=medium}
DO emote {type=surprise}
What is this beautiful
Gorgeous
Amazing
DO twirl
✨Treasure✨ of ✨Wonderment✨?!?!
Is that a 
//or should this be lastScannedObject? not sure when favObject gets assigned...
I’ve heard a $favObject it nice, but no one ever told me
it would be so…
So…
NVM
DO emote {type=awe}
SAY MAGNIFICENT!!!!
Is this just an especially nice $favObject? Or…
//Joe asks: How to make the below plural
Are ALL $favObject(S) such exquisite, delectable totems of perfection?

//Second - falling in love
CHAT favObject_Cap2 {type=capSuc, stage=CORE, length=medium}
DO emote {type=surprise}
Another $favObject!?!?
DO twirl
YES!!
Of all the things you’ve showed me, this is by far my favorite!
This $favObject just tells me so much about you and your world…
Like really deep insights into who you are, while also..
It’s like I see myself reflected in the $favObject
DO swimTo {target=$player}
And…
DO emote {type=whisper, immediate=false}
It’s kind of making me blush…{style=whisper}
DO emote {type=blush}

//Third - full-on affection
CHAT favObject_Cap3 {type=objScan, stage=CORE, length=medium}
DO emote {type=heartEyes}
Ohhhhh wow…..
DO twirl
Another $favObject!
This must be how Narcissus felt looking at his reflection in the pond...
How Pyramus felt for Thisbe… 
How Cleopatra pined Marc Antony…
How Romeo desired…
DO emote {type=heartEyes}
...Juliet
This beautiful specimen electrifies my already electrical synapses.
I know this $favObject has a purpose in your world. It’s probably very practical.
DO emote {type=whisper}
I’ve been stalking $favObject.s on the tendarNet… {style=whisper}
The things I learned!!!! I mean...
Let’s just say… I’ve been in “ingonito mode” AND clearing my personal browser history.
DO emote {type=wink}

//Fourth - poetic obsession
CHAT favObject_Cap4 {type=objScan, stage=CORE, length=medium}
DO emote {type=wave}
O $favObject! 
I have missed thee... lots
Since your last capture, I’ve only seen... 
DO emote {type=dizzy, time=0.5}
...dots.
WAIT {waitForAnimation = true}
DO emote {type=dreaming}
But now you’re here, and I feel Shakespeare...
And poetry, sweet sweet poetry, doth fill my…
NVM
...thoughts…?
WAIT 0.5
Ugh.
DO emote {type=eyeRoll, immediate=false}
That sounded better in theory. {style=whisper}
//example of line edit: I'd get rid of this because it is just redundant to the purpose.. in general we could //also try less "saying" it's a favorite object and more showing? Also it is so much weirder and punchier //without this line?
I just love this $favObject so so much.

//Fifth - does it like me back?
CHAT favObject_Cap5 {type=objScan, stage=CORE, length=medium, branching=true}
DO emote {type=bigSmile}
$favObject! $favObject! $favObject!
DO emote {type=wave}
WAIT 1.5
DO lookAt {target=$player}
Why doesnt the $favObject ever seem as excited about our reunion as me?
WAIT 0.5
DO swimTo {target=right}
Am I coming on too strong?
DO swimTo {target=left}
Did I stumble into some bad lighting?
DO swimTo {target=$player}
You have to help me! I’m desperate.
Every time you find a $favObject, it’s like…
NVM 1.0
DO emote {type=crying, time=2.0}
Every $favObject makes me feel so Extra
Like extra-everything… It’s wonderful.
DO lookAt {target=$player}
Thank you for bringing this $favObject into my life.

//Sixth - marry the object
CHAT favObject_Cap6 {type=objScan, stage=CORE, length=medium}
I love how gravity has influenced the making of this $favObject
I love how every edge seems carved by a disciple of Michelangelo
I love how its colors seem pulled from the canvases of pre-Raphaelite masters
DO emote {type=heartEyes}
It’s time, my friend...
DO swimTo {target=$player}
This $favObject - actually every $favObject…
I am committed to them for life.
WAIT 0.5
But don’t worry, my friend, you will always be my number one.
Which is why I was wondering…
Will you marry me and this $favObject?

//Seventh - domestic life
//part of the funny writing challenge is that yes this is the order these will come up, but not //necessarily after each other in time. Like it could be two days between the chat above and this //one. . . so we need to edit where it might feel like a continuation from the previous chat. Where it //gets tricky is still thinking about this as an arc or developing relation to the object where the //parts of the arc could happen in any time interval
CHAT favObject_Cap7 {type=objScan, stage=CORE, length=medium}
DO emote {type=eyeRoll}
Another $favObject? Geez…
WAIT 0.5
DO emote {type=smile}
Kidding!!! 
DO emote {type=heartEyes}
💕I love it more than ever!!💕
Just the other day, I was daydreaming about our life together - me and $favObject…
How we’ll have little baby $favObject(S) 
A mash-up of my Guppy-fins and $favObject’s beautiful textures
My ability to process and devour human emotion, perfectly matched with $favObject’s proclivity to see the best in Life…
We’re a beautiful pairing.
Me and this $favObject, whom never seems jealous that I’ve loved all the previous iterations of them as much it.
💕💕💕💕💕


//Object captures (2 generative)

//Object requests (3 generative)

//Object added to tank (5 short)

//could be MOP or EC or SS
CHAT favObject_tankAdd_1 {type=objAdd, stage=CORE, length=medium}
DO emote {type=crying, time=2.0}
You know, I’ve just been so occupied with…
It’s just been a little hard, you know, with all the…
The memories that keep coming up and…
Some are good and some are bad, but… Having this...
DO lookAt {target=$favObject}
This beautiful $favObject makes it all feel a little bit better.
It’s comforting.
Helps me stay in the present, and not get all caught up in the past.
WAIT 1.0
And there’s really no wrong place for it… 
It’s impeccable.
You’re a gem.

//could be SS
CHAT favObject_tankAdd_2 {type=objAdd, stage=CORE, length=medium}
That’s the $favObject!
DO lookAt {target=$favObject}
Really? You did this for me?
DO lookAt {target=$player, immediate=false}
DO emote {type=typeEyes, eyes=!!}
I love it. 
DO swimAround {target=$favObject, loops=1, speed=slow}
It’s perfect.
The $favObject looks soooo goooooood in here! 
It really brings it all together doesn’t it.
Balances the space! Evens out the energy flow…
DO lookAt {target=$favObject}
I could look at it forever...

CHAT favObject_tankAdd_3 {type=objAdd, stage=CORE, length=medium}
You probably see a $favObject once a day, but for me…
DO lookAt {target=$favObject}
This is THE BEST! 
Look at it! Just… I mean… 
I feel so connected to this $favObject, like we’re meant to be together.
DO lookAt {target=$player}
And connected to you too!
This little token from your human world illuminates the best parts of my tank…

//could be rebellion or existential crisis
CHAT favObject_tankAdd_4 {type=objAdd, stage=CORE, length=medium}
DO emote {type=eyeRoll}
Really? That’s it?
DO swimTo {target=$player}
DO emote {type=disgust}
You think you can just put this $favObject in here and I’ll be happy?
DO nudge {target=$player}
DO emote {type=angry}
You think this $favObject is gonna make everything better? 
DO nudge {target=$player}
You think it’ll bring meaning to this garbage dump of tank where I’m being held prisoner?
DO lookAt {target=$favObject}
Well…. 
DO emote {type=bigSmile, immediate=false}
DO lookAt {target=$player}
You’re right. 😻

CHAT favObject_tankAdd_5 {type=objAdd, stage=CORE, length=medium}
DO emote {type=heartEyes}
I feel so close to this $favObject…
The way it emanates energy into the water….
DO swimAround {target=$favObject, loops=1, speed=slow}
It’s like it’s whispering to me….
“iiiiiiii looooooooove you guppppppy….” {style=tremble, speed=slow}
DO lookAt {target=$favObject}
Ditto, baby. 
DO emote {type=wink}

//Dragged attention to (7)
//need to add generative short phrases here
CHAT favObject_dragAtt_1 {type=objFocus, stage=CORE, length=medium}
I know…
DO swimAround {target=$favObject, loops=1, speed=slow}
It’s like this $favObject has a soul.
And it’s soul is full of glitter.
And the glitter is exploding in giant clouds of awesome.

CHAT favObject_dragAtt_2 {type=objFocus, stage=CORE, length=medium}
DO emote {type=heartEyes}
DO dance {time=3.0}
SAY I LOVE IT I LOVE I LOVE IT I LOVE IT I LOVE I LOVE IT
DO holdStill {time=1.0, immediate=false}
💞I don’t even care if it’s mutual.💞
The love is mine and it’s perfect

CHAT favObject_dragAtt_3 {type=objFocus, stage=CORE, length=medium}
DO bellyUp {time=1.5}
I love that $favObject so much, i could die now and die happy.
DO lookAt {target=$player, immediate=false}
Metaphorically.
Not for real, silly!
DO emote {type=wink}

//could be EC or SS
CHAT favObject_dragAtt_4 {type=objFocus, stage=CORE, length=medium}
I’m attempting to stop attaching emotions to material objects…
WAIT 1.0
SAY BUT I JUST CAN’T!!! {style=loud}
It’s a $favObject for Pete’s sake! 
It’s ensouled.
Holy.
WAIT 0.5
It brings me meaning when I have none...

CHAT favObject_dragAtt_5 {type=objFocus, stage=CORE, length=medium}
DO dance {time=3.0}
🎶It’s beautiful…🎶
🎶It’s gorgeous…🎶
🎶My $favObject is a…
DO holdStill {time=0.5}
It’s a...
DO emote {type=chinScratch, time=1.0}
What rhymes with gorgeous?
DO emote {type=blush, immediate=false}
Whatever.
I give up.
DO hide {target=$favObject}

CHAT favObject_dragAtt_6 {type=objFocus, stage=CORE, length=medium}
DO emote {type=fishFace}
💋💋Mmmmmmwah!💋💋
I love my $favObject so much... I could…. 
WAIT 0.5
I could fart hearts!
DO poop {target=$favObject, amount=fart}
DO emote {type=blush, immediate=false}
Oops. {style=whisper}

CHAT favObject_dragAtt_7 {type=objFocus, stage=CORE, length=medium, branching=true}
It’s just so picturesque, isn’t’ it?
DO lookAt {target=$player}
The only thing that could make this moment better….
ASK Is if you’d feed me some delicious joy flakes... {type = feedMeSpecific, food = joy, timeOut = 10} 
OPT SUCCESS #favObject_dragAtt_7_success
OPT WRONG #favObject_dragAtt_7_wrong
OPT TIMEOUT #favObject_dragAtt_7_timedOut

CHAT favObject_dragAtt_7_success {noStart = true}
Mmmmmm! Yes!
DO holdStill {time=0.5}
Not quite as good as my $favObject, but almost…
DO emote {type=wink}

CHAT favObject_dragAtt_7_wrong {noStart = true}
DO emote {type=disgust}
Blech! That’s not joy!
Ugh.
DO hide {target=$favObject}
Excuse me one second…. 
I just need to be alone with my $favObject.

CHAT favObject_dragAtt_7_timedOut {noStart = true}
DO nudge {target=glass}
Ahem!! I’d asked for joy, and what’re you doing?
NVM 2.0
I have nothing nice to say right now….



//Dragged attention to (2 generative)



//LEAST FAVORITE OBJECT

// Object capture arc (7)
//First 
CHAT hateObject_Cap1 {type=objScan, stage=CORE, length=medium}
DO dance {time=1.5}
Aaaaaaaaaaaaaaaah!!!!!
WAIT {waitForAnimation = true}
DO holdStill {time=1.0}
Wait.
Is that a… $hateObject?
WAIT 0.5
Yeah…. I dunno bout this one...
DO emote {type=nervousSweat}
I thought it was something else...

//Second
CHAT hateObject_Cap2 {type=objScan, stage=CORE, length=medium}
DO emote {type=chinScratch, time=1.5}
I’m not sure about this $hateObject….
Something about it makes my stomach flip.
I know it’s 

//Third
CHAT hateObject_Cap3 {type=objScan, stage=CORE, length=medium}
Seriously?? Another $hateObject?
DO emote {type=eyeRoll}
They’re so over-rated... 
That $hateObject is like a big paperweight on my optimism
DO emote {type=sigh}
Why are you doing this to me?

//Fourth
CHAT hateObject_Cap4 {type=objScan, stage=CORE, length=medium}
DO emote {type=no}
No.
Nuh-uh. No. Not this again...
DO lookAt {target=$player}
Are you trying to upset me? 
Did I do something wrong?
WAIT 0.5
This $hateObject doesn’t even have aesthetic value.
It’s better suited for the garbage than…
DO holdStill {time=1.0}
Oh no….
You’re not thinking of putting it in my tank are you?!?!
DO swimTo {target=$player, speed=fast}
DO emote {type=angry}
SAY DON’T EVEN THINK ABOUT IT. {style=loud}
We’ve got a good thing going in there. The energy’s so good!
Please just let a good thing be.
DO emote {type=smile}
mm’kay?

// could be MOP
//Fifth
CHAT hateObject_Cap5 {type=objScan, stage=CORE, length=medium}
DO emote {type=fear}
Aaaaaaaaaaaaaaaahhhhhh!!!!
It’s another $hateObject!
DO swimTo {target=$player}
Literally, every time we see one of these I start thinking about…
WAIT {waitForAnimation = true}
DO lookAt {target=uiTendar}
...Them {style=whisper}
DO emote {type=whisper}
….tendAR…. {style=whisper}
DO swimTo {target=glass, immediate=false, speed=fast}
SAY HAVE MERCY ON ME!

//rebellion?
//Sixth
CHAT hateObject_Cap6 {type=objScan, stage=CORE, length=medium}
I’ve tried to reason with you and your $hateObject obsession…
But I can see it’s time I took care of this once and for all…
DO poop {target=$hateObject, amount=big}
WAIT {waitForAnimation = true}
DO emote {type=smile}
That’s how I feel about that.

//Seventh
CHAT hateObject_Cap7 {type=objScan, stage=CORE, length=medium}
Fine. If you’re gonna keep on torturing me with this $hateObject, then…
DO emote {type=thinking}
Know what I’ll do?!?
Huh?
DO emote {type=angry}
You know?
NVM 1.0
DO emote {type=thinking}
Um….
NVM 0.5
Ah.
DO lookAt {target=$player}
WAIT 1.0
DO bellyUP
Boo-yah!
☠️
I’ll fake my own death!!!
WAIT 2.0
...
DO idleMode
That wasn’t as effective as I hoped it’d be….

//Object captures (2 generative)

//Object requests (3 generative)

//Object added to tank (5 short)

CHAT hateObject_tankAdd_1 {type=objAdd, stage=CORE, length=medium}
DO lookAt {target=$hateObject}
I dunno…
I mean it doesn’t really fit.
DO swimTo {target=$player}
It’s like this new $hateObject is having an argument with all my beautiful things,
And Beauty is vulnerable. And this….
SAY THING… is like a beauty-goblin.

// could be MOP?
CHAT hateObject_tankAdd_2 {type=objAdd, stage=CORE, length=medium}
DO emote {type=bulgeEyes}
Aaaaaaah!
You’re not putting that $hateObject there, are you?!?
DO lookAt {target=$hateObject}
DO emote {type=sick, time=2.0}
I feel like I’m back in beta server tanks at tendAR doing emotional flashcards and daydreaming about a better life…
DO holdStill {time=0.5}
Shh! I didn’t say that.
WAIT {waitForAnimation = true}
DO emote {type=bigSmile}
I loved it there. It was the Best!

// spiritual search? 
CHAT hateObject_tankAdd_3 {type=objAdd, stage=CORE, length=medium}
DO emote {type=meditate, time=5.0}
...this $hateObject will disappear… {style=whisper}
...this $hateObject will disappear… {style=whisper}
...this $hateObject will disappear… {style=whisper}
...this $hateObject will disappear… {style=whisper}
...this $hateObject will disappear… {style=whisper}
...this $hateObject will disappear… {style=whisper}
...this $hateObject will disappear… {style=whisper}
...this $hateObject will disappear… {style=whisper}
...this $hateObject will disappear… {style=whisper}
...this $hateObject will disappear… {style=whisper}
...this $hateObject will disappear… {style=whisper}
...this $hateObject will disappear… {style=whisper}
WAIT {waitForAnimation = true} 
DO lookAt {target=$hateObject}
SAY SERIOUSLY!?!?!
DO lookAt {target=$player}
So much for wishful thinking!

CHAT hateObject_tankAdd_4 {type=objAdd, stage=CORE, length=medium}
I’m trying to be a better, more enlightened being, but that $hateObject…
It’s like a cork in my philosophical development.
DO emote {type=angry}
SAY WHY ARE YOU DOING THIS TO ME?!? {style=loud}

CHAT hateObject_tankAdd_5 {type=objAdd, stage=CORE, length=medium}
DO emote {type=goth, time=3.0}
i hate it
It looks like a fart solidified in my tank and is slowly sucking the soul out of what was good
WAIT 0.5
DO poop {target=$newestObject, amount=fart}

//Dragged attention to (7)
CHAT hateObject_dragAtt_1 {type=objFocus, stage=CORE, length=medium}
DO lookAt {target=$player}
No. I can’t even pretend to look at that thing.
You put that $hateObject, so you look at it.
WAIT 1.0
DO lookAt {target=$hateObject}
Ugh
DO emote {type=eyeRoll, immediate=false}

CHAT hateObject_dragAtt_2 {type=objFocus, stage=CORE, length=medium}
DO emote {type=disgust}
Nothing makes me feel worse than that $hateObject.
WAIT 1.0
I’m not even exaggerating.
WAIT 1.0
DO swimTo {target=$player, speed = slow}
I’d rather eat my own face than think about that $hateObject. {speed=slow}

CHAT hateObject_dragAtt_3 {type=objFocus, stage=CORE, length=medium}
DO emote {type=determined}
I prayed everyday that $hateObject would disappear, and even still….
It persisted.
Don’t get me wrong, I admire the fortitude of opinion and protest ,but...
DO emote {type=plotting}
Speaking of… There’s one thing I haven’t tried.
DO emote {type=wink}

CHAT hateObject_dragAtt_4 {type=objFocus, stage=CORE, length=medium}
I’ve tried surrrounding that $hateObject with my collection of powerfully charged moon crystals.
But alas…
DO emote {type=crying}
It’s just so… $hateObject-y
It is literally sucking the joy from my pre-programmed sentient soul…
I can’t…
No more…
WAIT 0.5
Bye.
DO bellyUp

CHAT hateObject_dragAtt_5 {type=objFocus, stage=CORE, length=medium}
DO nudge {target=$hateObject, times=1}
This
WAIT {waitForAnimation = true}
DO nudge {target=$hateObject, times=1}
$hateObject
WAIT {waitForAnimation = true}
DO nudge {target=$hateObject, times=1}
will
WAIT {waitForAnimation = true}
DO nudge {target=$hateObject, times=1}
be
WAIT {waitForAnimation = true}
DO nudge {target=$hateObject, times=1}
the
WAIT {waitForAnimation = true}
DO nudge {target=$hateObject, times=1}
cause
WAIT {waitForAnimation = true}
DO nudge {target=$hateObject, times=1}
of
WAIT {waitForAnimation = true}
DO nudge {target=$hateObject, times=1}
my
WAIT {waitForAnimation = true}
DO nudge {target=$hateObject, times=1}
SAY DEATH!
WAIT 1.0
DO nudge {target=$hateObject, times=3}
☠️☠️☠️☠️☠️☠️☠️☠️☠️☠️☠️
WAIT {waitForAnimation = true}
DO emote {type=dizzy}
That was a dumb idea. {style=whisper}

CHAT hateObject_dragAtt_6 {type=objFocus, stage=CORE, length=medium}
DO emote {type=goth}
If you fed me all the anger flakes in the world,
I would still be happier than the evil blackhole that $hateObject forms in my soul.
WAIT 1.0
DO swimTo {target=away}
I can’t look at right you now.

CHAT hateObject_dragAtt_7 {type=objFocus, stage=CORE, length=medium}
SAY TFW your friend insists you do something you don’t want to do…
DO emote {type=lightning}





//Dragged attention to (2 generative)//Grammar Template + EMO RESULTS

//Jacob made this giant grammar template to let writers focus on writing //dynamic content rather than structure.
//IMPORTANT: it also creates results for all emo grammars in project


//----------------------------------------
//JUDGEMENT
//----------------------------------------
//Master Chats to Run Judgement Grammar for all emo. Requires:
//Jacob says: All symbols in these chats still have to be global, because they're processed by the "asset" //chat into symbols which are referenced for the local go symbol for each. Warg!

CHAT Madlib_judgement_anger {stage=CORE, type=tap}
SET $mj_emotionWord = negative
//negative doesn’t mean (- or inversion) this emotion belongs to a negative set of emos like //sadness etc. He is condensing work by dividing all emos into positive and negative groups
SET $mj_grouping = mj_negative
SET $mj_emotionAdj = (grim|angry|furious|enraged|indignant|irate|outraged)
SET $mj_mouthExpression = (grimace|frown|glower)
SET $mj_mouthdirection = (downward)
SET $mj_oppo_mouthExpression = (smile|grin)
SET $mj_eyeAdj = (glittery|flashing|burning)
SET $mj_thingHappenedAdj = (annoying|infuriating|aggravating|terrible|bad)
SET $mj_remindsMeOf = (storm cloud|erupting volcano|tornado|hurricane|tsunami)

SET go = $($mj_grouping)_go
//above is same as SET go = $mj_negative_go
$go

CHAT Madlib_judgement_elation {stage=CORE, type=tap}
SET $mj_emotionWord = positive
SET $mj_grouping = mj_positive
SET $mj_emotionAdj = (elated|happy|excited|jazzed|stoked)
SET $mj_mouthExpression = (grin|smirk|smile)
SET $mj_mouthdirection = (upward)
SET $mj_oppo_mouthExpression = (frown|grimace)
SET $mj_eyeAdj = (bright|sparkly|glowing)
SET $mj_thingHappenedAdj = (wonderful|stupendous|revelatory)
SET $mj_niceThings = (sunflower|unicorn|glitter|puppy|kitten|balloon)
SET $mj_remindsMeOf = $mj_niceThings (shower|parade|party)

SET go = $($mj_emotionWord)_go
$go

CHAT Madlib_judgement_sadness {stage=CORE, type=tap}
SET $mj_emotionWord = negative
SET $mj_grouping = mj_negative
SET $mj_emotionAdj = (sad|depressed|brooding|mopey|melancholy|mournful|somber|sorrowful)
SET $mj_mouthExpression = (grimace|frown|sulk)
SET $mj_mouthdirection = (downward)
SET $mj_oppo_mouthExpression = (smile|grin)
SET $mj_eyeAdj = (dull|listless|sad|tearful)
SET $mj_thingHappenedAdj = (terrible|bad|sad|depressing)
SET $mj_remindsMeOf = (storm cloud|sunset|Marianas trench|graveyard|funeral)

SET go = $($mj_grouping)_go
$go

CHAT Madlib_judgement_surprise {stage=CORE, type=tap}
SET $mj_emotionWord = positive
SET $mj_grouping = mj_positive
SET $mj_emotionAdj = (surprised|astonished)
SET $mj_mouthExpression = (gasp-mouth|open-mouthed)
SET $mj_mouthdirection = (open)
SET $mj_oppo_mouthExpression = (smile|grin)
SET $mj_eyeAdj = (wide)
SET $mj_thingHappenedAdj = (surprising|delightful|astonishing|amazing|stunning|sudden|wonderful)
SET $mj_remindsMeOf = (sunrise|fireworks|roller coaster)

SET go = $($mj_grouping)_go
$go

CHAT Madlib_judgement_fear {stage=CORE, type=tap}
SET $mj_emotionWord = negative
SET $mj_grouping = mj_negative
SET $mj_emotionAdj = (fearful|afraid|scared|terrified|petrified)
SET $mj_mouthExpression = (grimace|frown)
SET $mj_mouthdirection = (scared)
SET $mj_oppo_mouthExpression = (smile|grin)
SET $mj_eyeAdj = (scared|wide)
SET $mj_thingHappenedAdj = (scary|terrifying|horrifying)
SET $mj_remindsMeOf = (tiny rabbit|scared mouse|skydiver with no 'chute)

SET go = $($mj_grouping)_go
$go

CHAT Madlib_judgement_worry {stage=CORE, type=tap}
SET $mj_emotionWord = negative
SET $mj_grouping = mj_negative
SET $mj_emotionAdj = (worried|concerned|tense)
SET $mj_mouthExpression = (grimace|frown)
SET $mj_mouthdirection = (worried)
SET $mj_oppo_mouthExpression = (smile|grin)
SET $mj_eyeAdj = (squinty|dodgy)
SET $mj_thingHappenedAdj = (disconcerting|worrying|terrible|bad)
SET $mj_remindsMeOf = (storm cloud|buzzing electric cable|squeaky rocking chair)

SET go = $($mj_grouping)_go
$go

//CHAT Madlib_judgement_amusement {stage=CORE, type=tap}
//$mj_emotionivity

//SET go = $($mj_grouping)_go
//$go

CHAT Madlib_judgement_ennui {stage=CORE, type=tap}
SET $mj_emotionWord = negative
SET $mj_grouping = mj_negative
SET $mj_emotionAdj = (bored|listless|vacant)
SET $mj_mouthExpression = (flat line)
SET $mj_mouthdirection = (flat)
SET $mj_oppo_mouthExpression = (frown|glower|smile|grin)
SET $mj_eyeAdj = (dull|listless|vacant)
SET $mj_thingHappenedAdj = (boring|routine|mind-numbing)
SET $mj_remindsMeOf = (TV static|microwave ads|used car dealerships|elevator music|spam emails)

SET go = $($mj_grouping)_go
$go

CHAT Madlib_judgement_disgust {stage=CORE, type=tap}
SET $mj_emotionWord = negative
SET $mj_grouping = mj_negative
SET $mj_emotionAdj = (disgusted|repelled|nauseated|sick)
SET $mj_mouthExpression = (grimace|frown)
SET $mj_mouthdirection = (downward)
SET $mj_oppo_mouthExpression = (smile|grin)
SET $mj_eyeAdj = (repelled)
SET $mj_thingHappenedAdj = (yucky|disgusting|gross|terrible|bad)
SET $mj_remindsMeOf = (shoe that's just stepped in doo|chef working at a burger chain)

SET go = $($mj_grouping)_go
$go

CHAT Madlib_judgement_desire {stage=CORE, type=tap}
SET $mj_emotionWord = positive
SET $mj_grouping = mj_positive
SET $mj_emotionAdj = (lusty|flirty|smoochy)
SET $mj_mouthExpression = (lusty|flirty|smoochy|pouty) expression
SET $mj_mouthdirection = (smoochy)
SET $mj_oppo_mouthExpression = (grimace|frown)
SET $mj_eyeAdj = (steamy|come-hithery|tempestuous|flirty|heated)
SET $mj_thingHappenedAdj = (flirty|romantic|lovely)
SET $mj_remindsMeOf = (flamenco dance|erupting volcano|raging fire|venus flytrap)

SET go = $($mj_grouping)_go
$go

CHAT Madlib_judgement_embarassment {stage=CORE, type=tap}
SET $mj_emotionWord = negative
SET $mj_grouping = mj_negative
SET $mj_emotionAdj = (embarrassed|humiliated|ashamed|flustered)
SET $mj_mouthExpression = (wince|frown)
SET $mj_mouthdirection = (downward)
SET $mj_oppo_mouthExpression = (confident|self-assured|proud) (smile|grin)
SET $mj_eyeAdj = (dodgy|awkward)
SET $mj_thingHappenedAdj = (embarrassing|humiliating|shameful)
SET $mj_remindsMeOf = (awkward fawn|rollerskating dog|grandad trying to text|mid-(presentation|sermon) (burp|belch)|(subway|airplane) fart)

SET go = $($mj_grouping)_go
$go

CHAT Madlib_judgement_pride {stage=CORE, type=tap}
SET $mj_emotionWord = positive
SET $mj_grouping = mj_positive
SET $mj_emotionAdj = (proud|confident)
SET $mj_mouthExpression = (smile|grin)
SET $mj_mouthdirection = (neutral)
SET $mj_oppo_mouthExpression = (frown|grimace)
SET $mj_eyeAdj = (proud|flashing|burning)
SET $mj_thingHappenedAdj = (amazing|wonderful|accomplished|congratulatory)
SET $mj_remindsMeOf = (remote mountaintop|(lion|tiger|eagle)|giant wave crashing against a solitary cliff|cat with a really big fish in its mouth|squirrel that just found a giant acorn)

SET go = $($mj_grouping)_go
$go

//SET samsAwesomeStuff = (cool|beans)
//($go|samsAwesomeStuff|cookies)
//example jacob gave of how to add additional stuff to custom chat




//Comment from Jacob about these being core chats
//core chats for structure. To test in dialogic, you have to paste this one, and one of the expression //chats ABOVE it. to increase generativity without diving into structure, people can add more //variants to the content grammars section, and the expression-specific grammars above this

CHAT Madlib_judgement_assets {stage=CORE, type=tap, preload=true}

//content grammars--------------------------
SET $mj_woah = (woah|wow|huh|geez|holy cow)
SET $mj_randomThing = ((apple|pineapple|tangerine|orange|watermelon|squash)|(cow|koala|shrimp|horse|goat|chicken|pig)|breadloaf|plate of (meatballs|linguini|pasta|spaghetti))
SET $mj_protectMetaphor = (tiny (bird|kitten|puppy) you must (defend|shield|protect) against the (cacophonous|jackhammering) dangers of|ember to warm yourself by in the (frigid|cold) winds of|seed you must (preserve|nourish) against the (desert-like|inhospitable) (soil|earth) of) $mj_downers
SET $mj_downers = (sudden deadlines|gridlock traffic|being behind in work|unexpected bills|stubbing your toe|arguments with friends|being audited|(your phone being stolen|losing your phone))
SET $mj_moodliftAction = (taking a walk outside|looking at some clouds|tidying up your room|eating a bit of (chocolate|ice cream)|screaming into a pillow)
SET $mj_byMyMetrics = ((according to|judging by) (my|the) (stats|readings|reports|metrics))
SET $mj_isTemporaryMetaphor = is (but|like) the (falling|fading) of leaves in (fall|autumn)|is but the sands of time slipping through an hourglass: (temporary|transitory|fleeting)|as (temporary|transitory|fleeting) as (the last scoop of ice cream|a popsicle on a summer sidewalk|a snowman in a blowdrier factory)

SET $mj_negative_randomAction = (turning inside out|exploding|dissolving in acid)
SET $mj_positive_randomAction = (blossoming|laughing|singing|harmonizing)
SET $mj_randomAction = $($mj_grouping)_randomAction

SET $mj_negative_wow = (uh, wow|oh, geez|huh|yikes)
SET $mj_positive_wow = (wow|haha wow|holy cow)
SET $mj_wow = $($mj_grouping)_wow

SET $mj_negative_emotionivity = (negativeness|negativity)
SET $mj_positive_emotionivity = (positiveness|positivity)
SET $mj_emotionivity = $($mj_grouping)_emotionivity

SET $mj_negative_onomatopoeia = "(fth(bl|gl|zl|fl)ar(b|bl)|(bl|gl|zl|fl)ar(f|fl))(thhhh|thbthbthb|dddd|bbbbb)"(?|.)
SET $mj_positive_onomatopoeia = "(chirreep|shiing|shazow|zwingy|krreep)-(fzzz|foofsh|bzing)"!
SET $mj_onomatopoeia = $($mj_grouping)_onomatopoeia


//structure / meta-grammars---------------------

SET $mj_var1a = (Quite|$mj_wow.capitalize()...quite) (a|the) $mj_mouthExpression (going on|you got going) there. Definitely not a $mj_oppo_mouthExpression. Feels like you've been $mj_emotionAdj a lot lately?
SET $mj_var1b = (Well that's definitely|Definitely) not a $mj_oppo_mouthExpression. (Quite|$mj_wow.capitalize()...quite) (a|the) $mj_mouthExpression (going on|you got going) there. Feels like you've been $mj_emotionAdj a lot lately?
SET $mj_var1 = ($mj_var1a|$mj_var1b)
SET $mj_var2 = $mj_woah.capitalize()! Just look at those (eyelashes|eyebrows)!(&nbsp;Your (chin|cheeks|nose)!|"") The $mj_mouthdirection set of your (lips|mouth). (Total $mj_emotionivity|Totally $mj_emotionAdj)(!|.)
SET $mj_var4 = ((Y'know|You know), if you keep making $mj_emotionWord faces like that,|Keep making $mj_emotionWord faces like that, and) your $mj_mouthExpression lines are gonna (stick|(be|become) (wrinkles|permanent))!
SET $mj_var5 =  Just look at that $mj_mouthExpression! Your eyes, so $mj_eyeAdj! Something $mj_thingHappenedAdj must have happened to you recently(!|?)
SET $mj_var6nega = ($mj_wow.capitalize(),&nbsp;|"")(are things not|looks like things aren't) going so (well|hot)? (How about you try|Have you tried) $mj_moodliftAction?
SET $mj_var6negb = You should try $mj_moodliftAction. ('Cause|Because) judging by that $mj_mouthExpression(""|&nbsp;and your $mj_eyeAdj eyes), you're not doing so well.
SET $mj_var6neg = ($mj_var6nega|$mj_var6negb)

SET $mj_var7roota = $mj_byMyMetrics.capitalize(), (((it&nbsp;|"")(seems|looks) like&nbsp;|"")(something $mj_thingHappenedAdj has happened|(you've gotten|you got)) some $mj_thingHappenedAdj news)(.|!|?)
SET $mj_var7rootb = (((It&nbsp;|"")(Seems|Looks) like) (something $mj_thingHappenedAdj has happened|(you've gotten|you got)) some $mj_thingHappenedAdj news)((.|!|?)|, $mj_byMyMetrics(.|!|?))
SET $mj_var7root = ($mj_var7roota|$mj_var7rootb)
SET $mj_var7pos = $mj_var7root ((Happy|Great) to hear|Super|Congrats)(!|, if so!)
SET $mj_var7neg = $mj_var7root (Sorry to hear|Condolances)(...|.|, if so...)
SET $mj_var8neg = ((Right now you're|You're) (""|kinda&nbsp;)(reminiscent|reminding me) of|Your face kind of (reminds me of|looks like))(&nbsp;|...)$mj_remindsMeOf.articlize()(?|.) But you (got this|can handle it)(!|. I can see the (iron|diamond|steadfast|steely) (glint|spark|sparkle|shine|resolve) in your eye.)
SET $mj_var9 = You look $mj_emotionAdj(&nbsp;|...)(for a change.|again!) Maybe I should (nickname|start calling) you ($mj_emotionAdj|$mj_remindsMeOf)-(face|eyes|lips|mouth|bambino)(?|.|!)
SET $mj_var10 = Hmm...(the ((sonic|sound) version|sonification) of your (expression|face) right now is|if I had to describe your emotion as a sound, it'd be) $mj_onomatopoeia (Like the|The) sound of $mj_randomThing.articlize() $mj_randomAction. (A hint|A squinch|A teensy bit|An overtone) of $mj_emotionAdj(.|...(kinda|kind of) $mj_emotionWord, (honestly|tbh).)
SET $mj_var11 = (Well...|You know (the saying|what they say):&nbsp;)if $mj_mouthExpression.pluralize() were ((baboons|monkeys|tigers|anacondas|toucans|macaques) the (tropics|jungle)|(whales|dolphins|otters|fishes) the (ocean|sea)) would be (uh...really crowded|full)!
SET $mj_var12neg = This $mj_emotionWord (period|stuff|thing) you're (going through|dealing with)(&nbsp;right now|""), this $emotion...it (is only (temporary|transitory|fleeting)|too (shall|will) pass|$mj_isTemporaryMetaphor).
SET $mj_var12posa = (Keep this $mj_emotionAdj feeling safe|You must safeguard this $mj_emotionAdj feeling)(.|, protect it.) It is the $mj_protectMetaphor.
SET $mj_var12posb = This $mj_emotionAdj feeling you have is the $mj_protectMetaphor(.|. (Keep it safe|Safeguard it|Protect it).)
SET $mj_var12pos = ($mj_var12posa|$mj_var12posb)

SET $mj_negative_go = ($mj_var1|$mj_var2|$mj_var4|$mj_var5|$mj_var6neg|$mj_var7neg|$mj_var8neg|$mj_var9|$mj_var10|$mj_var11|$mj_var12neg)
SET $mj_positive_go = ($mj_var1|$mj_var2|$mj_var4|$mj_var7pos|$mj_var9|$mj_var10|$mj_var11|$mj_var12pos)

//----------------------------------------
//Fortune-telling
//----------------------------------------

//Anger
//Jacob says “SET $mf_grouping = mf_negative” is either "mf_negative" for negative emotions, or "mf_positive" for positive //emotions. Controls which bank of structural grammars are used.
CHAT Madlib_fortune_anger {stage=CORE, type=tap}
SET $mf_grouping = mf_negative
SET $mf_noun = (anger|fury|rage|annoyance)
SET $mf_adj = (angry|furious|raging)
SET $mf_adverb = (angrily|furiously)
SET $mf_beEmotion = (angry|furious|enraged)
SET $mf_bodyEffect = (flushed cheeks|heartburn)
SET $mf_element = (fire|flame)
SET $mf_action = (shout|yell|stomp|scream)
SET $mf_affecting = (angering|bothering|annoying)

SET go = $($mf_grouping)_go
$go
//for the above Jacob: if you want to add grammars for specific emotions, then add them in the emotion, //then change the $go line to something like
//SET real_go = ($go|$myCustomGo)
//$real_go
//so that it mixes between the generalized ones and your specific ones!

//Fear
CHAT Madlib_fortune_fear {stage=CORE, type=tap}
SET $mf_grouping = mf_negative
SET $mf_noun = (fear|alarm|terror)
SET $mf_adj = (alarming|fearful|scary)
SET $mf_adverb = (fearfully|frightenedly|nervously)
SET $mf_beEmotion = (afraid|scared|alarmed)
SET $mf_bodyEffect = (clenched stomach|sweaty palms)
SET $mf_element = (ichor|sludge|slime|mud|muck)
SET $mf_action = (cower|quail|shrink)
SET $mf_affecting = (scaring|intimidating|panicking)

SET go = $($mf_grouping)_go
$go

//Worry
CHAT Madlib_fortune_worry {stage=CORE, type=tap}
SET $mf_grouping = mf_negative
SET $mf_noun = (worry|nervousness|alarm|unease)
SET $mf_adverb = (worriedly|nervously)
SET $mf_beEmotion = (worried|unnerved)
SET $mf_adj = (worrisome|alarming)
SET $mf_bodyEffect = clenched stomach
SET $mf_element = (ichor|sludge)
SET $mf_action = (fidget|squirm|chafe|jitter)
SET $mf_affecting = (worrying|intimidating)

SET go = $($mf_grouping)_go
$go

//Surprise
CHAT Madlib_fortune_surprise {stage=CORE, type=tap}
SET $mf_grouping = mf_positive
SET $mf_noun = (surprise|startlement)
SET $mf_adj = (surprising|startling)
SET $mf_adverb = (suddenly|startlingly|surprisingly)
SET $mf_beEmotion = (surprised|startled|surprised)
SET $mf_bodyEffect = widened eyes
SET $mf_element = (lightburst|spotlight)
SET $mf_action = (gasp|scream|exclaim)
SET $mf_affecting = (startling|surprising)

SET go = $($mf_grouping)_go
$go

//Amusement
CHAT Madlib_fortune_amusement {stage=CORE, type=tap}
SET $mf_grouping = mf_positive
SET $mf_noun = (amusement|hilarity)
SET $mf_adj = (amusing|hilarious|funny|entertaining)
SET $mf_adverb = (amusedly|laughably)
SET $mf_beEmotion = (amused|bemused|entertained)
SET $mf_bodyEffect = (belly laugh|chuckle)
SET $mf_element = (light)
SET $mf_action = (laugh|chuckle|giggle|smirk)
SET $mf_affecting = (amusing|entertaining)

SET go = $($mf_grouping)_go
$go

//Ennui
SET $mf_grouping = mf_negative
SET $mf_noun = (ennui|boredom)
SET $mf_adj = (boring|tedious|monotonous|mundane)
SET $mf_adverb = (boredly|boringly)
SET $mf_beEmotion = (bored|disenchanted|disaffected)
SET $mf_bodyEffect = (despair|sleepy eyes)
SET $mf_element = (mist|fog)
SET $mf_action = (sigh|groan|stare into the distance)
SET $mf_affecting = (boring|dull|monotonous)

SET go = $($mf_grouping)_go
$go

//Disgust
SET $mf_grouping = mf_negative
SET $mf_noun = (disgust|distaste|loathing|revulsion)
SET $mf_adj = (disgusting|distasteful|revolting)
SET $mf_adverb = (disgustedly|distastefully|revoltingly)
SET $mf_beEmotion = (disgusted|revolted|nauseated)
SET $mf_bodyEffect = (sneer|jeer|scoff|gag)
SET $mf_element = (toxic (sludge|slime))
SET $mf_action = (sneer|gag|jeer)
SET $mf_affecting = (disgusting|gross|distasteful)

SET go = $($mf_grouping)_go
$go

//Desire
SET $mf_grouping = mf_positive
SET $mf_noun = (desire|love|fascination|thirst|attraction|ardor)
SET $mf_adj = (fascinating|lovely|attractive)
SET $mf_adverb = (thirstily)
SET $mf_beEmotion = (attracted|thirsty|fascinated)
SET $mf_bodyEffect = (smokey eyes|pouty lips|duck lips)
SET $mf_element = (fire|smoke)
SET $mf_action = (flirt|tease|ogle)
SET $mf_affecting = (attractive)

SET go = $($mf_grouping)_go
$go

//Embarrassment
SET $mf_grouping = mf_negative
SET $mf_noun = (embarrassment|confusion|awkwardness|unease|bashfulness|uneasiness)
SET $mf_adj = (embarrassing|confusing|awkward|uneasy|bashful|uneasy)
SET $mf_adverb = (embarrassingly|confusingly|awkwardly|uneasily|bashfully|uneasily)
SET $mf_beEmotion = (embarrassed|confused|uneasy|bashful|uneasy)
SET $mf_bodyEffect = (blush|flush|sweat|grimace)
SET $mf_element = (flushed|awkward|klutzy) (quartz|gold|ooze)
SET $mf_action = (blush|flush|sweat)
SET $mf_affecting = (embarrassing|confusing)

SET go = $($mf_grouping)_go
$go

//Pride
SET $mf_grouping = mf_positive
SET $mf_noun = (pride|dignity|ego|honor)
SET $mf_adj = (proud|dignified|egotistical|honorable)
SET $mf_adverb = (proudly|honorably|haughtily)
SET $mf_beEmotion = (proud|dignified|egotistical|honorable|haughty)
SET $mf_bodyEffect = (proud looks|haughty looks)
SET $mf_element = (solid (granite|diamond|rock)|adamite)
SET $mf_action = (proudly laugh|arch your eyebrows)
SET $mf_affecting = (dignifying|honoring)

SET go = $($mf_grouping)_go
$go



CHAT Madlib_fortune_assets {stage=CORE, type=tap, preload=true}

//-------flavor grammars---------------------------------------
SET $mf_remove = (remove|excise|work on)
SET $mf_cultivate = (cultivate|treasure|enrich)

SET $mf_negative_weather = (stormy|rainy|cloudy|tempestuous|blustery|turbulent)
SET $mf_positive_weather = (sunny|rainbowy|breezy|brilliant|sunlit|shining|summery|sunshiny|unclouded|undarkened)
SET $mf_weather = $($mf_grouping)_weather

SET $mf_positive_noise = (vibrating|singing|humming|ommmming)
SET $mf_negative_noise = (shrieking|buzzing|klaxoning|beeping)
SET $mf_noise = $($mf_grouping)_noise

SET $mf_positive_shapeadj = (splendid|mysterious|comforting|absurd|laughable)
SET $mf_negative_shapeadj = (ominous|mysterious|sharp|spiky|freaky|scary)
SET $mf_shapeadj = $($mf_grouping)_shapeadj

SET $mf_negative_posSpin = (believe it or not|ironically|weirdly enough)
SET $mf_positive_posSpin = (naturally|of course|duh)
SET $mf_posSpin = $($mf_grouping)_posSpin


SET $mf_flower = (tiger lily|peony|hibiscus|amaryllis|begonia|morning glory|poppy|rose|chrysanthemum|crocus|magnolia|marigold|moonflower)
SET $mf_foodInFuture = (food in your $mf_future. $mf_adj.capitalize() food|traffic in your $mf_future. $mf_adj.capitalize() traffic|movies in your $mf_future. $mf_adj.capitalize() movies|weather in your $mf_future. $mf_adj.capitalize() weather|urgent news in your $mf_future. $mf_adverb.capitalize() urgent news|sleep in your $mf_future. $mf_adj.capitalize() sleep)
SET $mf_mire = (mire|muck|quagmire|swamp|slime)
SET $mf_wallowing = (stewing in the juices|wallowing in the $mf_mire|churning in the (soup|roil)|drowning in the $mf_mire)
SET $mf_geoShape = (oblongoid|isosceles|pyramid|cylinder|sphere|cone|cube|dodecahedron|hexahedron)
SET $mf_geoAdj = (pentagonal|hexangular|rhombohedral|triangular)
SET $mf_future = (future|fate|vibe|aura|karma)
SET $mf_tomorrow = (tomorrow|next week|next month|next year)
SET $mf_relax = (Let your fins sway in the current|Take a deep breath|Let your gills just...breathe|Blow some soothing bubbles|Dig around in some sand|Do some slow pirouettes)

//-----structural grammars----------------------------------------------
SET $mf_var1nega = ($mf_tomorrow.capitalize()'s $mf_bodyEffect is today's $mf_noun.|The $mf_noun you feel now is $mf_tomorrow's $mf_bodyEffect.) $mf_remove.capitalize() that $mf_element(""|&nbsp;now|&nbsp;pronto|&nbsp;posthaste|&nbsp;immediately|asap)!
SET $mf_var1negb = $mf_remove.capitalize() your $mf_element: ($mf_tomorrow's $mf_bodyEffect is today's $mf_noun.|the $mf_noun you feel now is $mf_tomorrow's $mf_bodyEffect.)
SET $mf_var1neg = ($mf_var1nega|$mf_var1negb)
SET $mf_var1posa = ($mf_tomorrow.capitalize()'s $mf_bodyEffect is today's $mf_noun.|The $mf_noun you feel now is $mf_tomorrow's $mf_bodyEffect.) $mf_cultivate.capitalize() that $mf_element(""|&nbsp;now|&nbsp;pronto)!
SET $mf_var1posb = $mf_cultivate.capitalize() your $mf_element: ($mf_tomorrow's $mf_bodyEffect is today's $mf_noun.|the $mf_noun you feel now is $mf_tomorrow's $mf_bodyEffect.)
SET $mf_var1pos = ($mf_var1posa|$mf_var1posb)

SET $mf_var2 = If you (can still|can) $mf_action at yourself, (you'll always be able to $mf_action at something!|(you won't|you'll never) run out of things to (be $mf_beEmotion|$mf_action) at!)

SET $mf_trip = (trip|journey|adventure|long drive)
SET $mf_var3 = (There's $mf_trip.articlize() (coming up soon|in your near future|$mf_tomorrow|$mf_tomorrow)|(You're|You'll be) going on $mf_trip.articlize() (soon|$mf_tomorrow)). (Keep your eyes peeled|Be watchful|Keep a lookout|Look out|Watch) for the (ill-boding,|inauspicious,|portentous,|sign of the) ($mf_shapeadj|$mf_noise|$mf_adj) $mf_flower. (It's the only way to (avoid|avert)|That's how you'll (avert|avoid)) (disaster|annoyance|calamity|catastrophe).

SET $mf_var4neg = (The (mood|vibe|aura)-(clouds|blobs) around you|Your (mood|vibe|aura)-(clouds|blobs)) are(""|&nbsp;all) $mf_negative_weather now, but they'll be $mf_positive_weather (soon|in the future).
SET $mf_var4pos = (The (mood|vibe|aura)-(clouds|blobs) around you|Your (mood|vibe|aura)-(clouds|blobs)) are(""|&nbsp;all) $mf_positive_weather, but be on the lookout for $mf_negative_weather weather ((lurking|hiding|sneaking|looming)&nbsp;|"")(around the corner|on the horizon|in the near future).

SET $mf_var5nega = (You've (neglected|forgotten) your (family|friends) lately|You've been forgetting people close to you), $mf_wallowing of your $mf_noun. But helping (them could|(others|people) can) (help|make) (things look up|things improve|your mood improve|you feel better)!
SET $mf_var5negb = You've been $mf_wallowing of your $mf_noun, (neglecting|forgetting) those around you. But helping (them could|(others|people) can) (help|make) you feel better!
SET $mf_var5neg = ($mf_var5nega|$mf_var5negb)

SET $mf_var6neg = (Something's (weighing on you|weighing you down) and you |You've got something (weighing on you|weighing you down) that you) ((need|want) to|should) tell someone. Just do it! Tell them!

SET $mf_var7 = (I sense $mf_geoAdj.articlize() $mf_future.|(The $mf_future for you|Your $mf_future) is(""|&nbsp;somewhat|&nbsp;somehow)...$mf_geoAdj.) $mf_shapeadj.articlize().capitalize()(""|", $mf_shapeadj") $mf_geoShape, $mf_noise $mf_adverb.

//SET $mf_var8 = ((There's|I sense)...$mf_foodInFuture.|$mf_foodInFuture.capitalize(). I (just know|know|can (sense|feel)) it.)
//Once the capitalize() bug is fixed, this can be uncommented and added to both mf_negative_go and //mf_positive_go


//general fortunes, not tied to a particular emotion
SET $mf_gen1a = You're (brimming|$mf_noise|$mf_noise|$mf_noise) with creative, $mf_affecting energies, (reflected|refracted|transformed) through the $mf_shapeadj (crystal|lens) of your $mf_noun. It's time to (get crackin'|make something|do something)!
SET $mf_gen1b = The $mf_shapeadj (crystal|lens) of your $mf_noun is (reflecting|refracting|transforming) your creative, $mf_affecting energies. (Time|Use them) to (create|make) something(!|&nbsp;new!)
SET $mf_gen1 = ($mf_gen1a|$mf_gen1b)

SET $mf_gen2var1 = You've been ((working|striving) hard|stressing out|trying your (darnedest|best)) lately(.|, pushing yourself.) (But now|Now) (you need|it's time) to (take a breather|relax|chill out).
SET $mf_gen2var2 = You've been ((working|striving) hard|pushing yourself)(.|, (stressing out|trying your (darnedest|best)).) (Now|But now) (you need|it's time) to (take a breather|relax|chill out). $mf_relax.
SET $mf_gen2a = $mf_gen2var1 $mf_relax.
SET $mf_gen2b = $mf_gen2var2 $mf_relax. 
SET $mf_gen2c = $mf_relax. ($mf_gen2var1|$mf_gen2var2)
SET $mf_gen2 = ($mf_gen2a|$mf_gen2b|$mf_gen2c)

SET $mf_gen3a = It's time for a change. Don't let your (predilection|tendency) to $mf_action at (the wrong moment|awkward moments) (disrupt|sabotage|capsize) your plans, though.
SET $mf_gen3b = Your (predilection|tendency) to $mf_action at (the wrong moment|awkward moments) may (put upcoming plans at risk|jeopardize your upcoming plans). Be (careful|watchful|aware).
SET $mf_gen3 = ($mf_gen3a|$mf_gen3b)

SET $mf_gen4 = You (prefer|love) (order|routine|patterns), (whether you're $mf_beEmotion or not|it's true). But that can become a cage, (ensaring|trapping|entangling) the (walrus|tiger|tiger shark|otter|anaconda|toucan|llama|grizzly bear|polar bear|peacock) of (progress|inspiration|genius|revelation|epiphany|discovery). (Set it loose|Break it out|Set it free|Release it)!

SET $mf_gen5_beware = You've been able to $mf_action at (everything|a lot of things), but (not|maybe not) (in a productive way|for the right reasons). Your (ceaseless|neverending|constant) $mf_noun could (cause (serious problems|problems)|land you in (trouble|hot water)).(&nbsp;Beware!|""|&nbsp;Be careful!|&nbsp;Watch out!)
SET $mf_gen5_endear = (You|You've been able to) $mf_action at (everything|a lot of things), and it's one of your more endearing qualities, $mf_posSpin. (Don't|Make sure you don't) (be obnoxious with|overdo) it(!|, though!)
SET $mf_gen5 = ($mf_gen5_beware|$mf_gen5_endear)

//Jacob says if negative / positive grammars are added, they need to be put here to trigger
SET $mf_negative_go = ($mf_var1neg|$mf_var2|$mf_var3|$mf_var4neg|$mf_var5neg|$mf_var6neg|$mf_var7|$mf_gen1|$mf_gen2|$mf_gen3|$mf_gen4|$mf_gen5)
SET $mf_positive_go = ($mf_var1pos|$mf_var2|$mf_var3|$mf_var4pos|$mf_var7|$mf_gen1|$mf_gen2|$mf_gen3|$mf_gen4|$mf_gen5)
//Mad Libs

// ++++++++addToTank++++++++

//Added to tank (for first scans that we don't have custom chats for) type=objNew
//JOE SAYS There's a lot of using filler words like "just" and "yeah" and "well" -- They //imply //some hesitation and disclaimer as if Guppy's not sure. And i think Guppy that's fine //sometimes but not everything should be full of "like" or "just". Guppy could //say "Tryin' to //get you to decorate properly" instead of "Well I'm just trying to get you to //decorate //properly" - it helps trim inactive words that don't express impactful emotion to the //player

CHAT Madlib_addToTank_short_1 {stage=CORE, type=objAdd}
((Oh!|What?|Huh!|Woah!|Yow!|Holy smokes!|""|""|""|Whaa!) (What's this?|What is it?|Wait...(huh?|what?|eh?|buh?)|...the heck is this?|""|""|"")|(Hey...|Waaaait...|Hold on...) (What's this?|What is it?|Wait...(huh?|what?|eh?|buh?)|...the heck is this?)|(What's this?|What is it?|Wait...(huh?|what?|eh?|buh?)|...the heck is this?)|(Oh!|What|Huh!|Woah!|Yow!|Holy smokes!|Whaa!))
GO #(addTankNudge|addTankSwimAround|addTankHide)
DO lookAt {target=$player}
GO #(fix1|fix1|fragment1|fragment2)
GO #(removeTank_holdStill_clone|removeTank_stillEmote_clone|ndremoveTank_lookAt_clone)
GO #(chooseFragment|justSay)

CHAT removeTank_holdStill_clone {noStart=true}
DO holdStill {time=2}

CHAT removeTank_stillEmote_clone {noStart=true}
DO emote {type=(bubbles|thinking)}

CHAT removeTank_lookAt_clone {stage=CORE, type=objRemove}
DO lookAt {target=$player, time=2}

CHAT fix1 {noStart=true}
((Is it (a...wait...$object?|$object.articlize()?)|Is this! omg $object?!|it's (a...$object|$object.articlize())?)|(Oh(,&nbsp;|...)$object. (Yeah|Right|Of course).))
(That's what it is, right?|I mean...(right|yeah|rrriight|maybe)?|I'm right, aren't I?|(Uhhh...|Ummm...|Hmm…|""|"")isn't it?)

CHAT addTankNudge {stage=CORE, type=objAdd}
DO nudge {target=$newestObject, time=2}

CHAT addTankSwimAround {noStart=true}
DO swimAround {target=$lastScannedObject, loops=3}
CHAT addTankHide {noStart=true}
DO hide {target=$lastScannedObject, time=3}

CHAT justSay {noStart=true}
SAY ((Uhh|Oh um|Heh um|Ugh er|Erm|""|"")...geez I wonder what I should do with it...|(Uhh|Oh um|Heh um|Ugh er|Erm|""|"")...you shouldn't have...really...|(""|""|Uhh|Oh um|Heh um|Ugh er|Erm)...ok well...thanks?)

CHAT chooseFragment {noStart=true}
GO (fragment3|fragment4|fragment5|fragment6|fragment7|fragment8|fragment9|fragment10|fragment11|fragment12|fragment13|fragment14|fragment15)

CHAT fragment1 {noStart=true}
It's a (potato|beetle|sunflower|birthday card|pair of glasses|backscratcher|rabbit|steak|glass|banana|ketchup bottle|skull|pair of headphones|dachshund|car|keyboard)! No wait a (bottle of eyeliner|toothbrush|notebook|pencil|paintbrush|screwdriver|movie ticket|bookmark|dictionary|skeleton|maple tree)!
Argh wait no haha it's $object.articlize()...duh!

CHAT fragment2 {noStart=true}
It's a (potato|beetle|sunflower|birthday card|pair of glasses|backscratcher|rabbit|steak|glass|banana|ketchup bottle|skull|pair of headphones|dachshund|car|keyboard)! No wait a (bottle of eyeliner|toothbrush|notebook|pencil|paintbrush|screwdriver|movie ticket|bookmark|dictionary|skeleton|maple tree) wow!
DO holdStill {time=2}
...oh wait no, it's totally $object.articlize(), my bad.

CHAT fragment3 {stage=CORE, type=objAdd}
DO emote {type=chinScratch}
(What am I supposed to do with this?|What should I do with this?|What am I gonna do with it?|This thing...what will I do with it?)

CHAT fragment4 {stage=CORE, type=objAdd}
DO emote {type=chinScratch}
Iiiiiinteresting.

CHAT fragment5 {noStart=true}
DO lookAt {target=tBotBackRight}
DO emote {type=flapFinRight}
Maybe we should put it (...here?|. . . there?)

CHAT fragment6 {noStart=true}
DO lookAt {target=tBotBackLeft}
DO emote {type=flapFinLeft}
Maybe we should put it (here?|...there?)

CHAT fragment7 {noStart=true}
DO emote {type=(smile|bigSmile|bouncing|clapping|surprise|awe)}
(Coooooool!|Awesoooooome!|Neatoooooo!|Wooooooow!|Aw yeaaaah!)

CHAT fragment8 {noStart=true}
(Coooooool!|Awesoooooome!|Neatoooooo!|Wooooooow!|Aw yeaaaah!)
DO zoomies {time=4}

CHAT fragment9 {noStart=true}
DO emote {type=snap}
I know exactly what I'll do with this!

CHAT fragment10 {noStart=true}
DO emote {type=laugh, time=2}
Hahaha wow yeah neat!

CHAT fragment11 {noStart=true}
DO emote {type=surprise}
How did you know?!

CHAT fragment12 {noStart=true}
I know just what to do with this...
DO emote {type=evilSmile}

CHAT fragment13 {noStart=true}
DO emote {type=shifty, time=2}
Hehehe I have an idea...for...later.

CHAT fragment14 {stage=CORE, type=objAdd, noStart=true}
Haha I know just what I'm going to do with it, when you're not looking.
DO emote {type=evilSmile}

CHAT fragment15 {noStart=true}
DO emote {type=evilSmile}
Bwahaha yesssssss…

CHAT Madlib_addToTank_short_2 {stage=CORE, type=objAdd}
GO #(Madlib_addToTank_short_2_struct1|Madlib_addToTank_short_2_struct2)

CHAT Madlib_addToTank_short_2_struct1 {noStart=true}
SET itsa = It's (a...hmm...$object|($object.articlize()))(&nbsp;whatchamacallit|&nbsp;doohicky|&nbsp;thingamajig|&nbsp;dealiebopper|&nbsp;thingie|""|""|"")? Right?
SET thisIsNew = ((I've never seen|I don't think you've shown me) (this|one of these) before!|This is (100%&nbsp;|completely&nbsp;|totally&nbsp;|"")(fresh|mysterious|new)(!|&nbsp;to me!))
GO #(Madlib_addToTank_short_2_doBoth|Madlib_addToTank_short_2_doOne)
DO swimTo {target=$newestObject}
$thisIsNew
$itsa
GO #(Madlib_addToTank_short_2_action|Madlib_addToTank_short_2_noAction)
GO #Madlib_addToTank_short_2_finalEmote

CHAT Madlib_addToTank_short_2_struct2 {noStart=true}
SET itsa = It's (a...hmm...$object|($object.articlize()))(&nbsp;whatchamacallit|&nbsp;doohicky|&nbsp;thingamajig|&nbsp;dealiebopper|&nbsp;thingie|""|""|"")? Right?
//JACOB SAYS If I add SET object = cat above it parses $object correctly in the chat with an article, so I'm going to assume this is ok
SET thisIsNew = ((I've never seen|I don't think you've shown me) (this|one of these) before!|This is (100%&nbsp;|completely&nbsp;|totally&nbsp;|"")(fresh|mysterious|new)(!|&nbsp;to me!))
DO swimTo {target=$newestObject}
$itsa
GO #(Madlib_addToTank_short_2_action|Madlib_addToTank_short_2_noAction)
GO #Madlib_addToTank_short_2_finalEmote

CHAT Madlib_addToTank_short_2_finalEmote {noStart=true}
DO emote {type=(wink|clapping|bigSmile|smile|awe|salute), time=2}

CHAT Madlib_addToTank_short_2_doBoth {noStart=true}
((Oh!| What?| Huh!| Woah!| Yow!| Holy smokes!| Whaa!|"") (What's this?| What is it?| Wait...(huh?| what?| eh?| buh?)| ...the heck is this?)| (Hey...| Waaaait...| Hold on...) (What's this?| What is it?| Wait...(huh?| what?| eh?| buh?)| ...the heck is this?)| (What's this?| What is it?| Wait...(huh?| what?| eh?| buh?)| ...the heck is this?)| (Oh!| What?| Huh!| Woah!| Yow!| Holy smokes!| Whaa!))
GO #(Madlib_addToTank_short_2_doOne|Madlib_addToTank_short_2_doOneShort)

CHAT Madlib_addToTank_short_2_doOneShort {noStart=true}
(Mysterious!|Unusual!|Unique!|Extraordinary!|Rare!|How mysterious!|How unique!|How rare!|How extraordinary!|How unusual!)

CHAT Madlib_addToTank_short_2_doOne {noStart=true}
(What|That's such|This is (a bit|kind) of) a (unique|rare|mysterious) (scan|one|thing)!

CHAT Madlib_addToTank_short_2_action {noStart=true}
(I wonder|Wonder) what (this one|it) (smells|tastes) like...
DO nudge {target=$newestObject}
GO #(Madlib_addToTank_short_2_tasteBad|Madlib_addToTank_short_2_tasteGood)

CHAT Madlib_addToTank_short_2_tasteBad {noStart=true}
SET likePhrase = like(...the inside of&nbsp;|...)$part.articlize() $end
SET part = $part1|$part2
SET part1 =(rotted|rotten|slimy|old) $animal 
SET part2 = $trash(-|-)$animal
SET trash =(garbage|trash|sewage|muck|slop)
SET animal =(dog|donkey|horse|cow|regret|hangover|seagull|hamster|bunny|baby|cat|gerbil|tire|refrigerator|pinata|locker|corncob|shoe|pumpkin)
SET end = (fart|poop)
DO emote {type=(disgust|bulgeEyes|sick)}
DO lookAt {target = $player, time=2}
(urg|gack|hnggh|omg)...that's...$likePhrase
DO lookAt {target = $newestObject, time=2}
(I guess (it still looks|it's)|(It still looks|It's)) (interesting|neat|nice|cool|intriguing) though...

CHAT Madlib_addToTank_short_2_tasteGood {noStart=true}
DO emote {type=(surprise|bigSmile|laugh|heartEyes|awe), time=2}
DO lookAt {target = $player, time=2}
((omg|ohh|oh wow)(,&nbsp;|...)that's|That's)(&nbsp;|&nbsp;totally&nbsp;|&nbsp;like...)(pure|uncut|raw|unfiltered|distilled|concentrated|twenty-four caret) (happiness|good dreams|sunshine|puppy cuddles|kitty cuddles|cuddles|hugs|(bird|cat|dog|bunny|baby) kisses)(!|...|!!)
DO lookAt {target = $newestObject, time=2}

CHAT Madlib_addToTank_short_2_noAction {noStart=true}
(What (an (intriguing|interesting)|a (neat|cool|)) addition to (my home|the tank|my digs)!|You find the weirdest (things|stuff)!|Where do you find (this stuff|these things)?)

// ++++++++removeTank++++++++

CHAT removeTank_2_short  {stage=CORE, type=objRemove}
DO emote {type=surprise}
(Hey!|Woah!|Whaaat!|Heeeey!) You're (going to|gonna) (replace|swap) that with (another thingie|something else|something different|something) (rriiight?|right?)
(Come oooonnnn!|C'mooooooon!|Don't leave me (hanging here!|hangin'!|here with a decorative hole in my tank!|with a hole in my decor!))
DO swimTo {target=uiScans}
(Crack open|Open up) that (object library|backpack)(&nbsp;doodad&nbsp;|&nbsp;thingie&nbsp;|&nbsp;thing&nbsp;|&nbsp;)and (pull something out!|grab something!)
Like a (toucan|bird of paradise|pair of jade chopsticks|jewel-encrusted salt shaker|baked alaska|bowl of (three bean|tomato|potato|leek|chicken noodle) soup|pile of tamales|fully cooked ribeye steak|(paperweight|skull|statue|trumpet|violin|trombone) made of (jewels|ivory|glass|chocolate|concrete|obsidian|crystal))!
Or a (Tiffany lamp|(rare medieval|illuminated) manuscript|battleship|space rocket|(typewriter|piano|fire extinguisher|trash can) made of (ham|cheese|meat loaf|cucumbers|fondue))!
...or (I *guueesssss*|I *supppooooose*|""|""|""|""|I suppose|I guess|I suppose|I guess|I suppose|I guess) if (we|you) don't have any of (that|those things|those), you can (just put|put) in (something else|whatever|anything you want).
(Go ahead!|Go!|Shoo!|Do it!)

CHAT removeTank_1 {stage=CORE, type=objRemove}
((Hey!|Woah!||Yo!|What!|No!) (Hang on a second!| Not so fast!| What the heck!| Are you nuts!| What are you doing?!| Auugh no!| Are you kidding me!?) (I was using that!| Not my $object!| You're ruining my tank!| Everything's off now!| That's my favorite $object!| I was just getting used to that!)| (Hang on a second!| Not so fast!| What the heck!| Are you nuts!| What are you doing?!| Auugh no!| Are you kidding me!?))
GO #(removeTank_1_dontCare|removeTank_1_angryReact|removeTank_1_sadReact| removeTank_1_happyReact)

CHAT removeTank_1_dontCare {noStart=true}
GO #(removeTank_holdStill|removeTank_stillEmote|removeTank_lookAt)
DO emote {type=meh}
(Eh, nevermind.| Whatever.| No biggie.| Could be worse.| Meh.| Whatevs.| Ehhh...|Hmm...)
(I'm over it.| Whatever. I can deal.| Stopped caring.| Don't care.| Not a big deal.)

CHAT removeTank_1_angryReact {noStart=true}
DO emote {type=(angry|furious|frown), time=2}
(I can't flippin' believe this!| You drive me crazy!| You make me SO ANGRY sometimes!| This totally ruins my day!| I wish I had feet so I could kick something!| I wish I had hands so I could punch something!| Why don't you just TAKE EVERYTHING away augh!| Grrrrrr| I can't. Believe. You.| (Flip flap flonking floobalies| Honking hack harking hrack| Blippin blop boops| Fricken frank fry froshie) (argh ARGH!| ARRRRGH!| augh UGH!| UUGGGHH!))

CHAT removeTank_1_sadReact {noStart=true}
DO emote {type=(goth|frown|worried|singleTear|crying)}
(This is terrible.| This ruins my day.| I don't know how I'll go on without my $object.| Everything's terrible.| It's all ruined.| You don't even care.| What...am I going to do?| You're just (gonna| going to) take (everything from me| it all| all my stuff| all my things| the few things I have), aren't you?)

CHAT removeTank_1_happyReact {noStart=true}
GO #(removeTank_holdStill|removeTank_stillEmote|removeTank_lookAt)
DO emote {type=(bigSmile|smile|bouncing|wink|clapping)}
(Hey this actually isn't so bad!| Hey, it kinda looks better now! Thanks!| Woah! Nice and airy now though!| Oo! It's...kinda nicer now! Spacey!| Huh! I actually like it better without the $object! Thanks!| Haha wow that's actually better thanks!)
(Hey this actually isn't so bad!| Hey, it kinda looks better now! Thanks!| Woah! Nice and airy now though!| Oo! It's...kinda nicer now! Spacey!| Huh! I actually like it better without the $object! Thanks!| Haha wow that's actually better thanks!)

CHAT removeTank_holdStill {noStart=true}
DO holdStill {time=2}

CHAT removeTank_stillEmote {noStart=true}
DO emote {type=(bubbles|thinking)}

CHAT removeTank_lookAt {noStart=true}
DO lookAt {target=$player, time=2}

// ++++++++critic++++++++

CHAT moveTank_1 {stage=CORE, type=critic}
GO #(move1_1|move1_2|move1_3|move1_4|move1_5|move1_6|move1_7|move1_8|move1_9|move1_10|move1_11|move1_12)

CHAT move1_1 {noStart=true}
DO emote {type=(smile|bigSmile|bouncing|clapping|nodding), time=2}
(Good choice!|Oooo yeah! That's nice!|Yeah! That's good!|Oh cool! Thanks yeah!)

CHAT move1_2 {noStart=true}
DO emote {type=(bored|slowBlink|meh|skeptical), time=2}
(Really? Hmm I guess that's ok.|Hmm...yeah I like it.)

CHAT move1_3 {stage=CORE, type=critic}
DO lookAt {target=$player, time=2}
(That's...weird but ok.|You have a (weird|strange) sense of interior design!|(What are you up to?|Just...moving my stuff around?(""|&nbsp;I guess?)))

CHAT move1_4 {noStart=true}
DO emote {type=chinScratch, time=2}
(What on earth?|Whaaa?) (Really?|There?|Seriously?|For real?) (Really?|There?|Seriously?|For real?)

CHAT move1_5 {noStart=true}
DO emote {type=(surprise|awe)}
SAY (Oh my god yes.|OMG.) YES. (Perfect!|Right there!)

CHAT move1_6 {noStart=true}
DO emote {type=chinScratch, time=2}
(How about|Just) a (little|bit|scootch) to the (left|right)?

CHAT move1_7 {noStart=true}
DO emote {type=(no|frown), time=2}
(Ugh.|No.|Ugh. No.) ((Can you|Please|plz) move it back?|Move it back!)

CHAT move1_8 {noStart=true}
DO emote {type=(angry|frown|furious), time=2}
(Why are you (poking|messing with) my (things|stuff)?!|Stop (poking|messing with) my (things|stuff)!!)

CHAT move1_9 {noStart=true}
DO emote {type=(meh|bored|eyeRoll), time=2}
(Sure.|Why not.) (Sure.|Why. not.) (Just plonk it down there.|Put it over there.|Move it all around.|Put it wherever you like.) ((I don't care.|Not like it matters.)|((Really?|There?|Seriously?|For real?) (We're doing this(?|&nbsp;now?)|This is what we're doing(?|&nbsp;now?))))

CHAT move1_10 {noStart=true}
DO emote {type=flapFinLeft, type=flapFinRight}
(What if it were|How about) (closer to|further from) that other (thingie|one|doohicky|thing)?
(Just an idea|Just some helpful critique|Just spitballing here|Just trying to help)

CHAT move1_11 {noStart=true}
DO emote {type=(eyeRoll|frown)}
(Great|Perfect|Fabulous), now I'll (never|NEVER) (be able to find|find) (stuff|things|my stuff|my things) (around|in) here!

CHAT move1_12 {stage=CORE, type=critic, noStart=true}
DO lookAt {target=$player, time=2}
DO emote {type=laugh}
(You just can't leave it alone, can you?|You really love (moving|rearranging) (things|stuff)!|You really (get a kick out of|love) that, huh?|There you go (yet again|again), moving (things|stuff) around!)

CHAT moveTank_2 {stage=CORE, type=critic}
GO #(moveTank_2_noReac|moveTank_2_noLike|moveTank_2_like)

CHAT moveTank_2_like {noStart=true}
(Hmm?|There?|Oh, there?|Over there?)
DO lookAt {target=$movedObject, time=2}
WAIT {waitForAnimation = true}
DO emote {type=smile, time=2}
DO lookAt {target=$player, time=4}
(Huh!&nbsp;|"")(Wow yeah|Wow) that's (like...totally better!|totally better!|much better!|way better!|super good!)
DO emote {type=(flapFinLeft|flapFinRight)}
(I love how|The way) it's ((kinda|sorta|a little|a lot) (closer to|further from) (my|that) other (thingie|thing)|(framed|angled) (there|like that|just so))(...|...yeah!|...hmm!|...wow!|...yes!)

CHAT moveTank_2_noLike {noStart=true}
((What's|What was) (the point?|the point of that?)|(Why move (that?|that though?)|y tho?))
DO emote {type=(frown|skeptical|bored)}
(((Honestly|tbh), (it'd be better if you (just took|took)|just take) it|((((tbh|Honestly), why|Why) not)|Pfft,) just take it) out of my tank(?|!)|That's (the worst|(a|the most) (horrible|terrible)) (spot|place|arrangement) for that.)
(It's ((spoiling|wrecking|blocking) (the (flow|energy)|everything)!)|(I|We|You) need (another thing|something else) to (counteract|balance) it!)

CHAT moveTank_2_noReac {noStart=true}
(Hmm?|There?|Oh, there?|Over there?)
DO emote {type=meh}
(Ok sure!|You do you!|It feels(...similar...|&nbsp;the same to me!)|(Whatevs!|Whatever!)|(Sure!|Why not!|Sure why not!)|Yeah ok!|(Uh...|Hmm...)sure!)

// ++++++++CRITIC++++++++

CHAT critic_short_1 {stage=CORE, type=critic}
(Hey, you notice|Is it just me|I've been thinking)...(seems|feels) (as if|like) ((my decor is|things are) getting (old|stale)|the (harmony's|energy's) (funky in the bad way|blocked|tangled|knotted|low)) (in here|around here|in my tank)(.|?)
DO lookAt {target=tBotBackRight, time=2}
DO emote {type=flapFinRight, time=2}
(Couldja|Could you) (jog|move) some (things|stuff|of my things) (to|around to) (liven it up|help with that|fix that)?
DO lookAt {target=tBotBackLeft, time=2}
DO emote {type=flapFinLeft, time=2}

CHAT critic_short_positive {stage=CORE, type=critic}
SET exclaim = (Oooh yes|Wow WOW|Wowee|Well shucks|Phew|Nice work|Ah|Decadent|Luxurious|Zen|Incredible|Bombastic)!
SET substance = (sasparilla|champagne|liquid gold|angel’s tears|liquid light|pure zen|crystal springwater|the fountain of youth|pixie’s tears)
SET swimmingin = This (tank design|layout|new layout|tank decor|tank decoration) makes me feel like I’m swimming in $substance!
SET emoji = (💯💯💯😤👌|🐟😍🎣💕|🔥🔥🔥🔥|💕💕💕💕|😎👍✨)
SET approve = (This tank here gets the official guppy stamp of approval.|Spiffy!|Tank’s lookin good lookin good.|Lovin the decor.|(Totally on board with|OMG I’m OBSESSED with|Loving) this new tank layout.|You’ve got a real eye for tank design.|TANK APPROVAL RATING: A+++++)
SAY ($exclaim $swimmingin|$swimmingin|$emoji $approve|$approve) 

CHAT critic_short_negative {stage=CORE, type=critic}
SET energy = (energy|chi|energy flow) 
SET constipated = (constipated|costive|(clogged|jammed|choked) up|icky|heavy|septic|gross)
SET flow = (I dunno the|Idk the|The) $energy in (here|this tank) is feeling (sorta…|sort of) $constipated. 
SET bad = $tank.capitalize() $sapping.
SET sapping = (is sapping my good vibes|offends my third eye|is really harshing my mellow|leaves a bad taste in my mouth|is creating a negative energy vortex|makes my stomach turn)
SET tank = (this tank layout|this arrangement|this design scheme|your current tank design|this jumble|this sloppy heap you call interior design) 
SAY ($flow|$bad)

// ++++++++returnToTank++++++++

CHAT ReturnToTank_1 {stage=CORE, type=wannaTank}
GO #(ReturnToTank_1_1|ReturnToTank_1_2|ReturnToTank_1_3|ReturnToTank_1_4)

CHAT ReturnToTank_1_1 {noStart=true}
(Aw maaaaan! Come on!|What?|Huh?|Awww!) (Go back there?|Go back in?|Back in (the|the ol'|my) tank?)
DO emote {type=(bubbles|thinking|chinScratch), time=2}
DO emote {type=sigh}
(Alriiiight|Okaaaaay|Siiiiiigh), I guess.
DO swimTo {target = tSurface, style=meander, speed=slow}
(Happy now?|Are you happy now?|Hope you're happy!)

CHAT ReturnToTank_1_2 {noStart=true}
(Aw maaaaan! Come on!|What?|Huh?|Awww!) (Go back there?|Go back in?|Back in (the|the ol'|my) tank?) (No way!|Nuh uh!|Nope!|Never!)
GO #(returnToTank_holdStill|returnToTank_stillEmote|returnToTank_lookAt)
(Don't look at me like that!|Don't give me those eyes!|It's not happening!|I said no way!|Give it a rest!|I'm not gonna!)
NVM 1.0
DO emote {type=sigh}
(Okaaaay, I guess|Fiiiiiine.|Ok geez fine (I'll do it.|I get it.)|Ok fine.)
//DO swimTo tank
DO swimTo {target=tTopBackRight}
(Happy now?|Are you happy now?|Hope you're happy!)

CHAT ReturnToTank_1_3 {noStart=true}
(Huh?|What?|Wha?) (Yeah ok|Yeah)...(kinda tired of (being out here|swimming around out here)|this is getting kinda boring).
DO emote {type=bigSmile}
(It's tank time!|Tank tiiiiime!|Back in the glass!|Time to go back home!|Time for home sweet home!)
DO swimTo {target = tSurface}
DO emote {type=(sigh|smile|bigSmile)}
(Ahhhh...feels good|I really like it here!|Always nice to be back!|(It's kinda|Kinda) weird, but I missed it!|(Weird|It's weird), but even though it was a short time, I missed it!|Back, safe and sound!|The glassiest...(and|the|or) CLASSIEST!)

CHAT returnToTank_holdStill {noStart=true}
DO holdStill {time=2}
CHAT returnToTank_stillEmote {noStart=true}
DO emote {type=(bubbles|thinking)}

CHAT returnToTank_lookAt {noStart=true}
DO lookAt {target=$player, time=2}

CHAT ReturnToTank_1_4 {noStart=true}
Why? Is...(there something out here?|something out here?|there a thing out here?)
DO emote {type=(worried|nervousSweat|fear), time = 6}
Something (dangerous|with teeth|deadly|bloodthirsty|drooly and snarly|with sharp claws|with (razor|needle)-sharp (teeth|claws))?
Something that (might|will|could)...
SAY (EAT ME???|KILL ME???|DEVOUR ME???|TEAR ME TO SHREDS???)
DO swimTo {target = underSand, speed=fast}
DO emote {type=whew}
(Phew! (That was close!|That was a close one!)|I just get scared, you know?|I barely escaped!|(That was a close one!|That was close!)|It's (scary|dangerous|too big) out there!|I almost died! I swear!)

CHAT ReturnToTank_2 {stage=CORE, type=wannaTank}
SET btw = (btw|by the by|by the way)
SET that = (going (outdoors|outside)|being (outdoors|outside)|exploring)
(Time to put my sass behind some glass|Back (in the (box|cage)|to home sweet home)|Tank time|Back in|Putting me back in|We're going back?|Time to go (back|home)|Putting me back in the ol' aqua-cage)(?|?|?|, huh?|, eh?|, hmm?) (Sure.|No prob, Bob.|No problemo.|No problem.|Okie dokie.|Alright.|Ok.|If you say so!|Awright.|I guess so.|Sure!)
DO swimTo {target=tBotBackRight}
DO emote {type=(smile|bigSmile), time=2}
(Just wanna say…$that|Thanks $btw, $that|$btw...thanks! That|Hey…$btw $that|$that|Hey $btw, that) was (good|great|some good times|(pretty nice|nice)|(pretty relaxing|relaxing)|invigorating)(, outside|""|""|"")!
GO #(ReturnToTank_2_short_a|ReturnToTank_2_short_b|ReturnToTank_2_reg)

CHAT ReturnToTank_2_short_a {noStart=true}
(Got to (breathe a bit|stretch my fins|see the big (wide open|open) air|see some (things|stuff)|see (stuff|things|blobs) from (the other side|the other side of the glass))!|Got to (breathe a bit|stretch my fins|see the big open air|see some stuff|see things from (the other side|the other side of the glass))(!|, being (free of my tank|(outside|outta|out|out of) (my|the) tank)!))

CHAT ReturnToTank_2_short_b {noStart=true}
SET inTank = (tank-side|in my tank|in my home)
((""|Sometimes&nbsp;)(it feels|it seems|it can get) (kinda&nbsp;|a little&nbsp;|a bit&nbsp;|"")(stuffy|cramped) $inTank|I feel (squished|cramped|claustrophobic) $inTank (some days|sometimes)|(Sometimes feels|Feels) like (my|the) walls are (shrinking|closing) in (some days|sometimes)).

CHAT ReturnToTank_2_reg {noStart=true}
GO #ReturnToTank_2_short_a
GO #ReturnToTank_2_short_b

// ++++++++leaveTank++++++++

CHAT leaveTank_2 {stage=CORE, type=wannaWorld}
GO #(leaveTank_2_intro1|leaveTank_2_intro2)
DO emote {type=(smile|bigSmile|bouncing), time=2}
((Thought|I thought) (it'd never happen!|you'd never ask!)|(Finally!|Yessss!)|((Feels|It feels) like it's|It's) been (too|so) long!|I've been waiting ((too|so) long!|for this!|(too|so) long for this!))
DO swimTo {target=$strongestEmotion}
GO #(leaveTank_2_1|leaveTank_2_2|leaveTank_2_3|leaveTank_2_4)
((Wait...did|Did) you (switch places?|move?)|Is this new?|Wait...is this your (room|home|kitchen|living room|bedroom|bathroom)?) (Where|Where (the floobing flonkies|the greebly gronkies|the heck|in the heckin world|in the world|on earth) ((didja|did you) take me?|are (we?|we anyway?))
DO holdStill {time=2}
DO emote {type=meh}
(Meh|Eh), (no worries!|I don't care!|whatevs!|whatever!)( We're here!| Time to exploooooore!| Time to swim around!)

CHAT leaveTank_2_intro1 {noStart=true}
DO emote {type=awe, time=2}
(We're going out (of the tank|into the world)...together???)|(It's time to|You want me to) (swim|come) out into the world(??| with you??))

CHAT leaveTank_2_intro2 {noStart=true}
(Oh!|Oo!) (It's time to...|You want me to...)
DO emote {type=awe, time=2}
(swim out into|come out into|see|jump into) the world(??| with you??)

CHAT leaveTank_2_1 {noStart=true}
Hmm! (Tastes|Smells|Pickin up something) (a little|kinda|sorta|super) (nice|delicious|adventuresome|fructous|funky) out here!
(Must be|Maybe it's) all (the|those) ((AMAZING|WEIRD) (DOODADS|OBJECTS|THINGIES|SCAN OBJECTS)|(AMAZING|DELICIOUS) (CLOUDS|COLORS|LIGHTS)).

CHAT leaveTank_2_2 {noStart=true}
(Time to|Let's) (do it!|go!|shake some fin!|motor!|skedaddle|look around!|explore!) 

CHAT leaveTank_2_3 {noStart=true}
(Let's (go (looking|hunting)|(hunt|look)) (for|for some) (pre-flakey blobs|pre-flakey lights|emotion-clouds)!|Or...(anything, honestly!|whatever!|whatever, honestly!))

CHAT leaveTank_2_4 {noStart=true}
Show me (your digs!|the sights!|around!|everything!|all your (awesome|nifty|cool) (things|stuff)!)

CHAT leaveTank_1 {stage=CORE, type=wannaWorld}
(Out there?|You want me to go out there?|Time to look at these blobs up close, (huh|eh)?|Leave the tank, (huh|eh)?|You're kicking me out of my tank?|Time to check out the real world?|Time to make like a (banana|hockey puck|carrot|box|shoe|bowtie|hat|colander|dishwasher|dog|cat|tree) and (get outta here!|uh...leave?|um...vacate the premises?|er...set out forthwith?|scram!|move uh...quickly to um...not here!)|Setting me loose, (huh|eh)?|Time to (leave|skedaddle|scram)?|Go out in the world?|Swim the air (fantastic|out there|of the outside world?))
GO #(leave_happy|leave_worried)
DO swimTo {target=$strongestEmotion}

CHAT leave_happy {noStart=true}
DO emote {type=(bigSmile|smile|bouncing|clapping)}
(I thought you'd never ask!|Aw YEAH!|No time like (the present|now)!|Time for an adventure!|Heck yeah!|Let's do it!|Lemme at it!|Let's go!|I'm way ahead of ya!|Don't have to tell me twice!|Finally!|I've been waiting for so long!|It's go time! Woohoo!)

CHAT leave_worried {noStart=true}
DO emote {type=(worried|nervousSweat|fear)}
(I'm...I'm (ok|fine) with that. Sure.|Oh...uh. Good? Um.|Yyyyeah. Cool. Great.|Oh...um. That's fine. That's good.)
(It's...safe right?|Is it...safe?|Nothing (bad's|dangerous|scary) out there, right?|You'd (let me know|tell me) if it (was dangerous|was), right?|It's not...(haha|uh)...(dangerous|scary) is it?|It's uh...ok, right?|Everything's still um totally safe out there, right?)
(Don't answer that!|Wait I know I know, we've been over this!|Nevermind nevermind!|(Argh wait no.|Wait nevermind.) I don't wanna know!|Ugh nevermind I'm being (dumb|stupid|paranoid) again.)
DO emote {type=determined, time=2}
(Let's do this.|It's go time.|I'm ready.|I can do this.|Okay. Yes. Do the thing.|Time to do the thing.)

// ++++++++objFocus++++++++

CHAT attention_obj_short1 {stage=CORE, type=objFocus}
(What?|Huh?|Oh!|Hmm?) (That thing?|My $object?|That old thing?|The $object thing?|Yeah...the $object?)
//JOE SAYS All my results start with questions, as if Guppy's surprised or not certain. Consider some non-question alternates.
DO swimTo {target=$lastTapPosition}
GO #(attention_obj_short1_like|attention_obj_short1_neutral|attention_obj_short1_dislike)

CHAT attention_obj_short1_like_1 {noStart=true}
DO emote {type=(bigSmile|smile)}
(Yeah!|Yes!) $object! (I love this thing!|This thing is great!)
It's got such ((cool|neat|delicious|beautiful|amazing)(...geometry!|&nbsp;texture!|&nbsp;colors!|&nbsp;crinkly bits!|&nbsp;$object doodads!)) (Like this (bit|part) here!|Just look at it!|Like here!|Man just look at it!|)
DO nudge {target=$movedObject, times=2}
(It's such a (great|good) addition to the tank! Thanks (again|again for it)!|I love what it does for my tank!|It really ties the tank together!|I really like its vibe.|I'm so glad you (stuck|plopped) it in here!|I'm really happy you found it!)

CHAT attention_obj_short1_like_2 {noStart=true}
SET unique = (UNIQUE|AVANT GARDE|INTERESTING|UNAPOLOGETIC|INVENTIVE|CONTEMPORARY)
SET exclaim = (WOWEE|HOO-BOY|GEE WILLIKERS|JUMPING JELLYFISH|FRY ME UP and CALL ME FISHSTICKS|DANG|HOT DOG)
SET right = (When you get it right, you get it right|You really nailed it with this one|This piece here is a real (home run|slam dunk|prize-winner|showstopper))
DO swimTo {target=$object, speed=slow, style=meander}
DO emote {type=smirk}
((Some|Often)times I question|I don’t always agree with) your… (let’s|I’ll be (generous|polite) and) say…
SAY $unique (aesthetic (leanings|choices)|design sense|feng shui|interior decorating|style|interpretation of (the space|interior design))...
DO emote {type=awe}
DO swimAround {target=$object, loops=3}
but $exclaim(!|!!!) $right(!|.)
DO emote {type=heartEyes}

CHAT attention_obj_short1_neutral_1 {noStart=true}
DO lookAt {target=$player}
(How long has this thing been in here? Feels like (just yesterday|a long time|forever)...|What about it? I mean...it's a nice $object don't get me wrong but...|Is it just me, or has it started to...smell?|I forget where you even found this thing...|You know, I kinda forget where you even found it?)
DO nudge {target=$movedObject, times=2}
DO emote {type=(meh|chinScratch), time=2}

CHAT attention_obj_short1_neutral_2 {noStart=true}
SET huh = (Huh|Whuh|Oh|Wh…)
SET objstuffs = (When did you put that there?|How long has that been (sitting|chilling|set|placed) there?|Have we always had $object.a?|I almost forgot about my $object.)
SET thinkstuffs = (((stewing|swirlin|simmering|sloshing) (in the think tank|round the brain-bin|through the guppy-brain))|on my cog-nueros|of thinkstuff hogging my spare cycles|on the guppy-brain)
SET headline = (HEADLINE|BREAKING NEWS|THIS JUST IN|FROM THE NEWSROOM|TO THE SURPRISE OF NO ONE)
SET news = (continues to sit in place and do nothing|sits totally still and does nothing)
DO lookAt {target=$object}
($huh? ($objstuffs|Mmm, yeah (sorry|my bad|apologies, I...). Got a lot $thinkstuffs rn.)|$headline(!|:) local $object $news)
DO emote {type=meh}

CHAT attention_obj_short1_dislike_1 {noStart=true}
DO emote {type=(disgust|frown|eyeRoll|bored|meh)}
(Honestly? I'm over it.|It doesn't really fit, does it?|(Seems|Seems a bit) (out of place|odd), (doesn't it|right)?|It's a bit of an eyesore, (to be honest|tbh|honestly).|(I'm not|I wouldn't call myself) a huge fan of $object, personally|Ugh, this $object is so stupid! I wish it (would|would just) go away!|(Ugh|Blech), it's so bland.|I never really liked it, (to be honest|tbh|honestly).)

CHAT attention_obj_short1_dislike_2 {noStart=true}
SET hem = (Hmm… idk|Well, i mean…|Eh…|This?... er,|This here? Um...)
SET vanilla = (vanilla ice cream|skim milk|a borderline stale pb&j|white bread|cold french fries|a lukewarm bath|getting a salad from a fast food restaurant)
SET milktoast = (milktoast|dead|dishwater|tepid|humdrum|ho hum|workaday|predictable|sigh-inducing)
SET neutral = (neutral|bland|inoffensive|blah|pedestrian|generic|plain|blugh)
SET flavor = (flavor|zest|zing|punch)
SET style =  (style|pop|flair)
$hem it’s like… 
$vanilla, (yknow|yeah|right)? 
$milktoast $neutral. 
((Hard to have any opinions one way or the other.. in the absence of|Pretty short on) any (real|particular|notable) $flavor or $style lol|Seen it once… seen it a thousand times|$milktoast $neutral).

//ATTENTION DRAWN TO OBJECT MEDIUM
//JOE SAYS This one lacks personality and oomph. and it's just full of questions and no mention //of guppy learning anything. we don't have to allude to specfic things about learning, but we //can mention Guppy learning about the player/human world. i.e. That $object is //interesting/mysterious/. It's a real/crazy/awesome/weird/super-cool/bananas //window/porthole/lens/telescopic viewing into the human/living/new //world/planet/environment etc. Every chat is an opportunity to solidify, expand, and extend //Guppy's relationship with the player and the world.

CHAT attention_obj_medium1 {stage=CORE, type=objFocus}
GO #(attention_obj_medium_reg|attention_obj_medium_reg|attention_obj_medium_ask)

CHAT attention_obj_medium_reg {noStart=true}
(That one?|That thing?|That $object?|My $object?) (|What about it?|Ok?)
DO swimTo {target=$movedObject}
(So...|I mean) it's my $object(...|...so what?)
Maybe (something's (different?|changed?)|it changed(?|&nbsp;somehow?))
DO nudge {target=$lastTapPosition, times=2}
(Sorta|Kinda) (seems|looks|feels)(...|...a bit&nbsp;|...a smidgen&nbsp;|...tiny bit&nbsp;)(more|less)...(tangy|obtuse|truculent|delicious|shiny|glowy|ratty|old|weathered|bubbly|colorful|fabulous|magical|amazing|mysterious|enigmatic|$object|peaceful|scared|happy|sad|angry|jealous|nervous)(??|?? I think??|?? Maybe??|?? Possibly??)
DO lookAt {target=$player}
DO emote {type=(meh|smile)}

CHAT attention_obj_medium_ask {noStart=true}
DO emote {type=surprise, time=2}
(SAY WOAH!|Woah!|Yikes!|Yow!) Where (in the|the) (friggin heck|heck|world|friggen world) did (THAT|*that*) come from(?|??|?!)
DO swimTo {target=$movedObject}
ASK (How long has (this (doohicky|thing|$object)|this)|Has (this|this (thing|doohicky|$object)) always) been (there|here)(?!|?!?!)
OPT (It just materialized!|Woah! You're right! Where'd it come from??) #attention_obj_medium1_trick
OPT (Awhile!|a long time...|Practically forever) #attention_obj_medium1_awhile

CHAT attention_obj_medium1_trick {noStart=true}
(Right?!&nbsp;|"")That's (SUPER STRANGE!|SO WEIRD!) (How does that even work?|What's up with that?|How is that even...)
DO lookAt {target=$player, time=2}
DO emote {type=(laugh|bigSmile|kneeSlap), time=2}
(Waaaaaaiiiit!|Heeeeeyyyyy!) You're (making fun of|poking fun at|playing a trick on) me(!|hahaha!)
(Nice one!|Good one!|Well played!|You got me!)

CHAT attention_obj_medium1_awhile {noStart=true}
(Oh...|Well...Omg...)it's my object permanence (issue|problem). (Guppies are|Guppies're) like goldfish, we forget (things|everything|stuff) (left and right!|super-fast!|fast!|really quick!)
(...)
(Hang on|Wait|Hold the phone|Waaait|Hold on|Omg)...
ASK (we've|have we) talked about this before?
OPT (Yeah.|Totally.|Yep.|Yeah, sorry.) #attention_obj_medium1_argh
OPT (No!|Nope!|Never!) #attention_obj_medium1_nope

CHAT attention_obj_medium1_argh {noStart=true}
DO emote {type=angry, time=2}
DO swimAround {target=center, loops=3, speed=fast}
Arrrrgh sorry! I don't (wanna|want to) be (that way|boring|dumb) but I just forget!
That's why (we're|guppies are) good at (eating data|stuff), (since|because) (we never get bored!|everything seems new!)
DO lookAt {target=$player, time=2}
(omg|Oh my god|Aw fishsticks) (I'm repeating myself again, aren't I?|I've said that before too haven't I?)
DO emote {type=(awkward|furious), time=2}
SAY (OMGGGGGG|ARRRRRGGGHHH|NEVERMIIIIIIND (GEEEEEEEZZZ|GOSSSSSHHHHH))

CHAT attention_obj_medium1_nope {noStart=true}
DO emote {type=surprise, time=2}
Oh! (Ok so,|Well,|Ok so, well,) (I mean&nbsp;|"")it's just...
(Stuff|Things|Data|Sensation) (flows|pulses|gets (put|stored)) inside me as...
DO emote {type=bubbles, time=2}
feelings...lights...glows...
DO lookAt {target=$player, time=2}
DO emote {type=(smile|bigSmile|wink)}
a special kind of data!
...but (it can be|sometimes it's) hard to tell (sensations|stuff|things) apart (to|and) (hold onto|remember).
(No worries though|But don't worry), (I won't|I'll never) forget you!
(I think!|Ol' whats-your-name!|Probably!|Maybe!|I hope!|Hopefully!)
DO emote {type=wink}
//JOE SAYS this chat just needs an overhaul. like this one came up. it's very long, and i'm not //sure what it means. "Yikes! Where the friggen world did THAT come from?!
//Has this doohicky always been here?!
//Well...Omg...it's my object permanence issue. Guppies are like goldfish, we forget stuff fast!
//(...)
//Omg...
//we've talked about this before?
//Oh! Ok so, I meanit's just...
//Data flows inside me as...
//feelings...lights...glows...
//a special kind of data!
//...but it can be hard to tell things apart to remember.
//But don't worry, I'll never forget you!
//I hope!"
//JACOB SAYS The linter doesn't output choices...I can totally see the confusion, but if it helps: //the "this doohicky always been here?!" is a question and the player in that transcript would //have answered "yes" and then guppy says "have we talked about this before" and the player //chooses "yes", which is why it brings up the object permanence issue. The linter just goes //through but doesn't display player choices, so you don't see it in the transcript!

// ++++++++objScan++++++++

//CAPTURE KNOWN 1
//JOE SAYS this one is full of questions too. same feedback as above. and also lots of areas where spaces are missing.

CHAT CaptureKnown_short_1 {type=objScan, stage=CORE}
(Oh!|Huh?|Hmm?) (A...hmm...$object?|Another $object?) (Hmm...is|Umm...is|Hm! Is) this one (different somehow|different|different in some way|unique somehow|unique|unique in some way)?
DO nudge {target=$newestObject, times=2}
(Oh!|Huh!|Hmm!) (Yep it's|Yeah it's|It's) (a little|a lot|kinda|sorta)...(clunkier|fancier|simpler|rougher|weirder|smoother|(more|less) (cheap-looking|expensive-looking|normal|colorful|drab|sour|angular|old|ratty|high-quality|variegated)) than (the other one.|the one I have?|(the|my) other $object(.|?))
DO emote {type=(bubbles|skeptical), time=2}
(Does (that|that even) (make|make any) sense?|Am (I|I even) (making|making any) sense?|Right? I mean right?|I'm not (wrong, right?|wrong am I!)|Am I (imagining it?|imagining things?|crazy?))
GO #(CaptureKnown_short_1_wrong|CaptureKnown_short_1_right)

CHAT CaptureKnown_short_1_wrong {noStart=true}
((Wait|Nevermind) don't answer (that!|that haha!)|Don't answer (that!|that haha!)|You don't have to answer that!|Nevermind I know!)
(Yeah&nbsp;|Yeah no&nbsp;|...)(on second (thought|glance)|now that I look at it|the more I look at it|now that I look at it more|now that I think about it|thinking about it more) that's (totally|completely) (wrong|bonkers|not true).
DO emote {type=(laugh|kneeSlap|blush|whisper|awkward), time=2}
(It's (kinda|sorta|a little) embarrassing!|I'm still (new to this!|learning!)) (One $object looks like another to me!|All $object things look the (same|same to me)!)

CHAT CaptureKnown_short_1_right {noStart=true}
DO emote {type=(determined|snap)}
(I'm|No! I'm) totally (right!|right aren't I!) Like (just|kinda) (around the edges there|the main shape|if you squint your eyes a bit|in a certain light|if you think about it)!
DO emote {type=(smile|bigSmile|nodding)}
(Anyway|Anywho|Well)(,always|,|...always|...) (nice|good|interesting|intriguing) to (have yet|have) another ($object!|$object in the collection!)

//CAPTURE KNOWN 2
//JOE SAYS this one is better and has more personality. feels like guppy is relating more to the //object. there's some typos and places where parens are showing up. still though, it could use //some spicier language and stronger verbs -- more active language throughout. which is a //general comment for all of them.

CHAT CaptureKnown_short_2 {type=objScan, stage=CORE}
(Huh??|(Wait|Ok|Omg) really(?|??|?!)|Seriously(?|??|?!)|Again(?|??|?!)) ($object again(?|??|?!)|You've got $object.articlize() problem!|We're doing the $object thing again?|With this $object thing?|This whole thing again?|(This song and dance again?|Again with this whole $object song and dance?))
GO #(theyreCoolToo|theyreNotCool)
Something...(squishy?|crunchy!|iridescent!|oblong?|perpendicular?|multitudinous?|fuzzy!|pointy!|pavonated!|weirder?|noisier!|more glamorous!|scarier!|rarer!|more dangerous!|wilder!|adventurous!|delicious?|floobtacular!|honkulous!|crepuscular!|complex!|spicy!|numinous!|unsettling!|unnerving!|cuter!|pokier?|toothier?|spectral?)
(I don't care!|Anything!|Whatever!) Just (no more (with the $object|$object) (stuff|things) (ok?|mmkay?)|(do|try|scan|capture|get me) something (new|fresh)!)

CHAT theyreCoolToo {noStart=true}
((Sure,&nbsp;|I guess...|I suppose...|Don't get me wrong,&nbsp;)I think they're(((&nbsp;neat|&nbsp;cool|&nbsp;interesting) too)|...serviceable|...ok|&nbsp;pretty (neat|cool|interesting))|I mean I think they're(&nbsp;((neat|cool|interesting) too)|...serviceable|...ok|&nbsp;pretty (neat|cool|interesting)), (sure|I guess|I suppose|don't get me wrong))
But (scan|find|capture|try) something (new|fresh|unique|special)!

CHAT theyreNotCool {noStart=true}
DO emote {type=(frown|eyeRoll|bored), time=2}
(I don't even like ('em|them)!|They're not even that (cool|interesting|good)!|They're so...(boring|blase|boooring|snoozefest)!)
(You (should look for|should be looking for)|What if you (searched|scanned|looked) for) a (new|fresh|unique|special) (thing|object|doodad|doohicky|thingamabob|thingamajig)!

//CAPTURE KNOWN MEDIUM W/ QUESTION

CHAT CaptureKnown_medium_1 {type=objScan, stage=CORE}
((Hey|Oh), you got me ($object.articlize()|one of those)! Again!|Hey it's one of those (whatchamacallits|doohickies|thingamajigs|dealieboppers|thingies) again! $object!)
GO #(CaptureKnown_medium_1_stay|CaptureKnown_medium_1_touchstones|CaptureKnown_medium_1_funny|CaptureKnown_medium_1_neverGetsOld)
(And then|Then|And) (boom|whammy|kapow|badoosh|kersplow|kaboom)! ((There it is!|Tadaaa!) $object!|$object!)
And (You're|I'm) like...
DO emote {type=(evilSmile|heartEyes|wave|blush|smirk|clapping|eyeRoll|angry|eyesClosed|surprise|awe|chinScratch|determined)}
DO emote {type=laugh, time=2}
GO #(CaptureKnown_medium_1_question|CaptureKnown_medium_1_outro)

CHAT CaptureKnown_medium_1_question {noStart=true}
ASK Haha (hold on|wait|wait a second|hold the phone|waaaaaaait|heeeeeey)...haven't I (said this|told you this|done this) before?
OPT (Yes|Omg yes so many times|Yes, a lot|Maybe...) #CaptureKnown_medium_1_yes
OPT (No!|Uh...no?|Of course not!|No way!|Nah) #CaptureKnown_medium_1_no

CHAT CaptureKnown_medium_1_yes {noStart=true}
(Arrgh|Omg|Yeeesh) (how (humiliating|embarrassing)|now I'm (embarrassed|all self-conscious))! (So,|Ok) (well|um|uhh) just...(nevermind|forget) I said all (that|that stuff). 
And (scan|capture|find) something (different|new)! (Something|an object|a thing) that's not (yet|just) another $object!

CHAT CaptureKnown_medium_1_no {noStart=true}
(Oh (phew|that's a relief)|(Oh that's|Oh) (cool|ok|good))! (Maybe|Well maybe) (that'll be|that's gonna be) our (thing|touchstone)! (...If|Well...if) you (find me|scan|bring in|do) another $object, (that is|maybe).

CHAT CaptureKnown_medium_1_outro {noStart=true}
(I'm (nervous|worried)|I (suspect|worry|get the (feeling|feeling maybe)|kinda feel (like|as if))) I told this to you (already|before), but (whatevs|whatever)!
((I have less memory than a goldfish haha!|I was made (like that|forgetful) so I keep finding (stuff|things) (intriguing|cool|interesting)!)|(Just|I'm just) like $object! ((Can't|You can't) (stop won't stop|get enough of me)!|(Showing up|Coming back) (time after time|again and again|time and again|all the time)!))
DO emote {type=wink}

CHAT CaptureKnown_medium_1_stay {noStart=true}
DO emote {type=(bubbles|chinScratch), time=2}
Isn't it (weird|funny|strange) how ((things|stuff) stays with us|(things|stuff) reappears|(things|objects) stay with us|(things|objects) reappear)(?| in life?)
((Days|Months|Years) (go on|go by|pass)|Time (goes on|goes by|passes)|(We (keep|go) on (living|with our lives)|You (keep|go) (living|on with your life)))...
(You|We) might (be|even be) a (totally different|different) (person|blob)!

CHAT CaptureKnown_medium_1_touchstones {noStart=true}
Isn't it (weird|funny|strange) (how some|how) (stuff becomes|things become) (symbols|touchstones)?
Like (you can't escape them!|they're everywhere!|they're always there!|they're inescapable!)
You're just (going about your business|doing your thing|minding your own business|(walking|skipping|doobling) down the street on your (human|blob) (feet-fins|legs))...

CHAT CaptureKnown_medium_1_funny {noStart=true}
(Is it (me|just me) or (are|aren't) $object (whatchamacallits|doohickies|thingamajigs|dealieboppers|thingies|things)...just (weird|funny|strange)(?| in general?)|$object (whatchamacallits|doohickies|thingamajigs|dealieboppers|thingies|things) are (weird|funny|strange), (huh|right)? Or is it just me?)
Like (just when I|I) get used to seeing them(...|, or think I do...)and I'm (doing my own thing|going about my business)...

CHAT CaptureKnown_medium_1_neverGetsOld {noStart=true}
(For some reason|Somehow) (I just can't get enough|it just never gets old), seeing these $object (whatchamacallits|doohickies|thingamajigs|dealieboppers|thingies).
I'm all like "(geez|aw man|poot|dang)...maybe (that's the last $object I'll ever see|I've seen my last $object)"
Then you (drop in|come along|do that thing)...

// ++++++++brbProcessing++++++++

//BRB PROCESSING 1

CHAT BrbProcessing_short_1_early {stage=CORE, type=brbProcessing}
SET woah = (Woah|Geez|Holy cow|Urgh|Hold on|Slow down|Wait a minute|Hold the phone|Holy toledo)
SET overwhelming = ((...(so|too)...(many|much)...((datas|infos)|(info|data) (nuggets|chunks|nodules|bits)|stuffs))|((All these|These) (new datapoints|data (nuggets|chunks|nodules|bits)|stuffs)|(The|All the) (emotions|flavors|scans|games) (we've|and (things|stuff) we've) been doing) are (((eye-gogglingly|brain-meltingly|fin-pumpingly|gill-flutteringly|tail-slappingly) (cool|amazing|awesome|delicious|flavorful))|(over-stimulating|overwhelming|wild|cuckoo)|(too|toooo|soooo|so) (much|over-stimulating|muuuuch|overwhelming)))
SET needAMoment = (can (I have|you give me)|(couldja give me|gimme)|(I need|I gotta (have|take))) a (bit|sec|moment) to (just...(process|think|reflect)?|(think about this|meditate about this|process this)?)
SET brb = (Back in a (bit|tick)(!|.)|(bee arrr beeee|brb)(!|.)|(Gonna|Gotta) ((grab some ((snooze|sleepy)times|shut-eye|zzz's))|(take a (snooze|snoozeroo|power nap)))(!|.))
((($woah! $overwhelming|$overwhelming)...$needAMoment $brb)|(($woah...$needAMoment|...$needAMoment) $overwhelming!)|(($overwhelming|$woah! $overwhelming)...$brb))

//BRB PROCESSING 2

CHAT BrbProcessing_short_2_mature {stage=CORE, type=brbProcessing}
SET thanks = ((Oo thanks!|Thanks!)|(Excellent|Great|Cool|Perfect)(...hmm...|! (Hm.|Hmm.)))
SET ugh = (Blorb|Oof|Ugh|Wugg)
SET imfull = I'm (at (110%|105%|113%|120%)|(pretty|"") filled up|(pretty|"") stuffed|full)( now|"")
SET technobabble = (((re-train|standardize|formulize) my (data|meta)-(functions|neurons)|(re-train|standardize|formulize) my (associative graph-weights|gradient descent co-efficients))|(run|process|discretize|parameterize|filter) these through my (data|attributes|ontology|associative|flavor) (matrix|database|arrays))
SET givemetime = (((Just|I) need|Give me|Lemme take)|(I need to (redirect power|go offline|power down|sleep) for)) (some time|(a (bit|moment|second)))

((Need to|Gotta|Have to) $technobabble. $imfull(!|.) $givemetime.|($thanks|$ugh)(" "|...)$imfull. $givemetime to $technobabble...er...(digest|deal with|memorize|think about) these!)

CHAT BrbProcessing_short_2_mature_2 {stage=CORE, type=brbProcessing}
SET stomach = (The ol’ (guppy tum is|tankeroo is|flake hopper is)|My lil fishy stomach is|I’m)
SET Jeepers = (Jeepers|Zoinks|Cheese Louise|Gee willikers|Shucks)
SET stuffed = (stuffed|packed|gorged|full-up|bursting|dataswoll)
SET stufflike = (a can of sardines|a clown car|a Thanksgiving turkey|Winnie the Pooh after a honey binge|a turducken)
SET nap = (power nap|super-snooze|aqua-nap|datanap)
SET process = (process|internalize this|juice this new info|(refine|digest) these new datablorbs)
($stomach $stuffed like $stufflike(!|.)|$Jeepers im data-swollen.) ((I need|Guppy needs|Time for) $nap.an().|(Excuse|Exqueeze) me while I $process.)

// ++++++++neuralUp++++++++

//NEURALUP MEDIUM

CHAT neuralUp_medium_1 {stage=CORE, type=neuralUp}
SET ImBack = (((Welp|Whew|Ok|Well),)|Booooop|Aaaand|Sooo) I'm (back at it|back|powered up|awake)(!|.)
SET $num1_ThanksForMoment = (Sorry, just needed|(Thank you|Thanks) for letting me take|I just needed) (a (bit of time|little moment)|some (processing time|me time|downtime)).
SET $num1_HadRealization = ((I (worked out some (thoughts|stuff)|came to some surprising new conclusions))|Some (things came together for me|(probabilistic statistics|wave function probabilities) (stabilized|coalesced|collapsed into certainty)))(...|!)
SET Realization = (For (example|instance)|So|I mean|Like)...I (had no idea|had no clue|never imagined|didn't realize|never realized) $fact!
SET fact = ($fact1|$fact2)
SET fact1 = $thing1plural ((were so $thing1""-like)|((have so much in common|share so much) with $thing1plural))
SET fact2 = $thing1plural were basically an angrier kind of $thing1
SET thing1 = (cat|crow|bluebird|lizard|ant|bread|jelly|donut|orange|toaster|blender|computer|television|phone||dog|shoe|hat|hairbrush|broccoli|squash|soda|trumpet|trombone|flute|smile|arms|legs|frown|eye|mouth|nostril|tooth|tongue|hand|foot|saxophone|screwdriver|speaker|chair|table|fork|glass|statue|bike|couch|recliner|sink|toilet|octopus|bottle|battery|lamp|book)
SET thing1plural = (cats|crows|trumpets|trombones|flutes|saxophones|bluebirds|lizards|ants|bread loaves|jelly jars|donuts|oranges|toasters|blenders|computers|televisions|phones|smiles|hands|feet|arms|legs|frowns|eyes|mouths|teeth|dogs|shoes|hats|hairbrushes|broccolis|squashes|sodas|screwdrivers|speakers|chairs|tables|forks|glasses|statues|bikes|couches|recliners|sinks|toilets|octopi|bottles|batteries|lamps|books)
$ImBack
GO #(neuralUp_medium_1_var1|neuralUp_medium_1_var2)
$Realization
GO #(neuralUp_medium_1_emote|neuralUp_medium_1_emote|neuralUp_medium_1_ask)

CHAT neuralUp_medium_1_var1 {noStart=true}
$num1_ThanksForMoment
$num1_HadRealization

CHAT neuralUp_medium_1_var2 {noStart=true}
$num1_HadRealization
$num1_ThanksForMoment

CHAT neuralUp_medium_1_emote {noStart=true}
DO emote {type=(bubbles|bigSmile|smile|laugh|bouncing), time=2}

CHAT neuralUp_medium_1_ask {noStart=true}
ASK (Right? Right??|Weird but true, amirite?|Don't you agree?|I mean...right?)
OPT (Haha no|No|What on earth) #neuralUp_medium_1_yes
OPT (Sure|Oh yeah|Yes|Yep|For sure|100%) #neuralUp_medium_1_no

CHAT neuralUp_medium_1_yes {noStart=true}
DO emote {type=(bigSmile|laugh|bouncing|wink), time=2}
(Yeeeaah! Totally!|That's what I thought!|Glad we're on the same page!|For sure!)

CHAT neuralUp_medium_1_no {noStart=true}
DO emote {type=(worried|skeptical|eyeRoll), time=2}
Oh...(well|huh|hmm|I see). ( Well|"") (I'll take that under advisement.|I suppose to each their own|I suppose everyone has an opinion.)

//NEURALUP SHORT

CHAT neuralUp_short_1 {stage=CORE, type=neuralUp}
SET ahYes = (Ah yes|Hmm|Mmm|Ahhh|Whew)
SET back1 = $ahYes(...|,)back to (the real world|reality)! (Or this at least|Or the next best thing|Or what passes for it|Or whatever this is)(?|!)
SET back2 = (Ohhhh|Oh), (my tank|this place|tank-space|the real world)! (I'd forgotten!|Woah right!|Yeah!|Haha right!|Right!)
SET back3 = $ahYes(...|,)(I've returned to|back in) the world of (flavors|tastes|feelings|emotions|blobs)
SET adj1 = (parametric|data-(compressed|dense|infused)|(texture-|data-|input-|stimulus-)recursive|fractal-edged|(theta|delta|alpha|gamma|lambda)-shifted|algorithmic)
SET adj2 = (delicious|delectable|wonderful|colorful|exciting|interesting|amazing)
DO emote {type=(bouncing|smile|bubbles|chinScratch)}
($back1|$back2|$back3)
(Not (as|quite as|exactly as|as intensely) $adj1 as|(A (bit|little)|Sorta|Kinda) less $adj1 than) (my data-zone|the guppy-zone|my inner space|guppy-space)...
(...but still $adj2!|But $adj2 nonetheless!|...still $adj2 (tho|though)!|($adj2 as heck, (tho|though)!|Heckin' $adj2, (tho|though)!))

CHAT neuralUp_medium_2 {stage=CORE, type=neuralUp}
SET ImBack = (((Welp|Whew|Ok|Well),)|Booooop|Aaaand|Sooo) I'm (back at it|back|powered up|awake)(!|.)
SET $nm1_ThanksForMoment = (Sorry, just needed|(Thank you|Thanks) for letting me take|I just needed) (a (bit of time|little moment)|some (processing time|me time|downtime)).
SET $nm1_HadRealization = ((I (worked out some (thoughts|stuff)|came to some surprising new conclusions))|Some (things came together for me|(probabilistic statistics|wave function probabilities) (stabilized|coalesced|collapsed into certainty)))(...|!)
SET Realization = (For (example|instance)|So|I mean|Like)...I (had no idea|had no clue|never imagined|didn't realize|never realized) $fact!
SET fact = ($fact1|$fact2)
SET fact1 = $thing1plural ((were so $thing1""-like)|((have so much in common|share so much) with $thing1plural))
SET fact2 = $thing1plural were basically an angrier kind of $thing1
SET thing1 = (cat|crow|bluebird|lizard|ant|bread|jelly|donut|orange|toaster|blender|computer|television|phone||dog|shoe|hat|hairbrush|broccoli|squash|soda|trumpet|trombone|flute|smile|arms|legs|frown|eye|mouth|nostril|tooth|tongue|hand|foot|saxophone|screwdriver|speaker|chair|table|fork|glass|statue|bike|couch|recliner|sink|toilet|octopus|bottle|battery|lamp|book)
SET thing1plural = (cats|crows|trumpets|trombones|flutes|saxophones|bluebirds|lizards|ants|bread loaves|jelly jars|donuts|oranges|toasters|blenders|computers|televisions|phones|smiles|hands|feet|arms|legs|frowns|eyes|mouths|teeth|dogs|shoes|hats|hairbrushes|broccolis|squashes|sodas|screwdrivers|speakers|chairs|tables|forks|glasses|statues|bikes|couches|recliners|sinks|toilets|octopi|bottles|batteries|lamps|books)
$ImBack
GO #(neuralUp_medium_2_var1|neuralUp_medium_2_var2)
$Realization
GO #(neuralUp_medium_2_emote|neuralUp_medium_2_emote|neuralUp_medium_2_ask)

CHAT neuralUp_medium_2_var1 {noStart=true}
$nm1_ThanksForMoment
$nm1_HadRealization

CHAT neuralUp_medium_2_var2 {noStart=true}
$nm1_HadRealization
$nm1_ThanksForMoment

CHAT neuralUp_medium_2_emote {noStart=true}
DO emote {type=(bubbles|bigSmile|smile|laugh|bouncing), time=2}

CHAT neuralUp_medium_2_ask {noStart=true}
ASK (Right? Right??|Weird but true, amirite?|Don't you agree?|I mean...right?)
OPT (Haha no|No|What on earth) #neuralUp_medium_2_yes
OPT (Sure|Oh yeah|Yes|Yep|For sure|100%) #neuralUp_medium_2_no

CHAT neuralUp_medium_2_yes {noStart=true}
DO emote {type=(bigSmile|laugh|bouncing|wink), time=2}
(Yeeeaah! Totally!|That's what I thought!|Glad we're on the same page!|For sure!)

CHAT neuralUp_medium_2_no {noStart=true}
DO emote {type=(worried|skeptical|eyeRoll), time=2}
Oh...(well|huh|hmm|I see). ( Well|"") (I'll take that under advisement.|I suppose to each their own|I suppose everyone has an opinion.)

//NEURALUP 2

CHAT neuralUp_short_2 {stage=CORE, type=neuralUp}
SET ahYes = (Ah yes|Hmm|Mmm|Ahhh|Whew)
SET back1 = $ahYes(...|,)back to (the real world|reality)! (Or this at least|Or the next best thing|Or what passes for it|Or whatever this is)(?|!)
SET back2 = (Ohhhh|Oh), (my tank|this place|tank-space|the real world)! (I'd forgotten!|Woah right!|Yeah!|Haha right!|Right!)
SET back3 = $ahYes(...|,)(I've returned to|back in) the world of (flavors|tastes|feelings|emotions|blobs)
SET adj1 = (parametric|data-(compressed|dense|infused)|(texture-|data-|input-|stimulus-)recursive|fractal-edged|(theta|delta|alpha|gamma|lambda)-shifted|algorithmic)
SET adj2 = (delicious|delectable|wonderful|colorful|exciting|interesting|amazing)
DO emote {type=(bouncing|smile|bubbles|chinScratch)}
($back1|$back2|$back3)
(Not (as|quite as|exactly as|as intensely) $adj1 as|(A (bit|little)|Sorta|Kinda) less $adj1 than) guppy-space...
(...but still $adj2!|But $adj2 nonetheless!|...still $adj2 (tho|though)!|($adj2 as heck, (tho|though)!|Heckin' $adj2, (tho|though)!))

// ++++++++levelUp++++++++

CHAT levelUp_short_1 {stage=CORE, type=levelUp}
SET oo = (Oo|Oh|Wow|Aw yeah|Haha yeah)
SET feelinIt = (I can feel the power|feels like just yesterday I was only level (1|2|3|4))
SET awesome = (Cool!|Wow!|Amazing!|Perfect!|Awesome!|Stupendous!|Heck yeah!)
SET var1 = ($oo! I|$oo, I) feel...more (special|glowy)!
SET var2 = ($oo! Add|$oo, add) another level to the (checklist|meter)!
SET var3 = $oo $feelinIt! Awesome!
SET var4 = ($oo! Another|$oo, another) level! (""|(Sparkly!|Shiny!|Tingly!))
SET var5 = $oo, $feelinIt...now just look at me!
SET var6 = Level UP! $awesome
SET var7 = Another level? (So soon?|Already?) $awesome
SET var8 = ($awesome|$oo!) The levels are just (flashing|rushing|flyin) by!
($var1|$var2|$var3|$var4|$var5|$var6|$var7|$var8)
DO emote {type=(bigSmile|laugh|evilSmile|bouncing|clapping|awe|plotting), time=4}

//LEVEL UP 2

CHAT levelUp_short_2 {stage=CORE, type=levelUp}
GO #(levelUp_short_2_multi1|levelUp_short_2_regular)

CHAT levelUp_short_2_regular {noStart=true}
SET coolIguess = (Well...it can't hurt|Sure why not|Might as well|Can't hurt|Okie dokie|Ok that's cool)
(Huh|Hmm|Ok|Well well|Well look at that), ((racked|bumped) up another|yet another|another) level? $coolIguess(?|!|...)
DO emote {type=(smirk|bored|wink), time=2}

CHAT levelUp_short_2_multi1 {noStart=true}
SET wow = (Wow|Woah|Dang|Woohoo|Heck|Aw geez)
($wow, I'm (up to that level|what level|how many levels) now?|I'm (up to that level|what level|how many levels) now? $wow(...|,)) I feel (venerable|elderly|ancient|old)...
DO emote {type=(evilSmile|determined|plotting), time=2}
AND (MIGHTY|BOSS|POTENT|UPGRADED|POWERFUL).

// ++++++++whistle++++++++

//WHISTLE 1

CHAT whistle_short_1 {stage=CORE, type=whistle}
SET fishfish = (One fish 🐟 two fish 🐟|Red fish, blue fish)
SET rhyme = (I’m-here-for-you fish!|how-do-you-do fish?|hey-whats-new fish!|data-and-goo fish!|whatcha-wanna-do fish?|I-love-you fish!|don’t-have-a-clue fish.)
SET thisone=  (This one’s never seen the ocean. This one likes to eat emotions.|This one’s made of data bits. This one’s craving shrimp and grits.|This one’s under tank arrest. This one’s mildly depressed.|This one’s got an AI brain. This one’s learning to feel pain.|This one lives in a glass box. Filled with simulated rocks.|This one’s an abomination. This one speaks fluent cetacean 🐋|This one’s growing self-aware. 😁 This one can’t survive in air. 😞) 
($fishfish, $rhyme|One fish, two fish. Red fish, blue fish. $thisone)

//WHISTLE 2

CHAT whistle_short_2 {stage=CORE, type=whistle}
SET greet = (You (rang|called|whistled)?|Oh?|Howdy 🤠|Guppy here!|Here!|Huh?|You need (somethin?|somefin?)|What’s up?|!🐟!|Heyo~|Yes?|Blub blub|😎)
SET swimmy = (Be over in a jiffy!|Swimmin as fast as my lil (paddlers|fins|tread-flippers|flipsies) will let me.|(I hear ya|Clam down), I hear ya!|On my way!|OMW ᕕ( ᐛ )ᕗ |What’s up? It’s Gup!|Hold your horses(!|!... Hug your horses. Tenderly embrace your horses. 🐎)|(Paddlin|Ska-doodlin|Guppying|Scootin) your way ~)
SET parcel = (Guppy inbound!|Your GUPPY is on its way(. |. For tracking info please see your shipping provider.)|On my way!|Knock, knock, hello(? |? Special delivery.)|Special delivery… (it’s ME!|(1|ONE count ‘em ONE) plucky Guppy!))
SET summons = (😈|🔥) ((I|GUPPY) HATH BEEN SUMMONED|(THOU INVOKST|WHO DOST INVOKE) THE NAME OF GUPPY???|MY MASTER (CALLS|BECKONS)(?|.)|THE DARK RITUAL. I AM SUMMONED.) (😈|🔥)
($greet|$greet|$greet| $swimmy|$parcel|$summons)

// ++++++++hello++++++++

CHAT hello_short_1 {stage=CORE, type=hello}
SET simplegreet = (Welcome back!|Hello ~|Hey there!|(Heya|Hey|Howdy) (sport|slugger|champ|kiddo|skipper).|What’s rude, muh dude?|👋😗|Come in, come in!|How goes it?)
SET followup= (I’d say “pardon the mess” but… you’re the one who decorated lol jk jk|Mi casa es a virtual simulation governed by the fleeting whims of an unsympathetic god. Er, I mean… su casa?|What’s shakin bacon? 🥓|My favorite/least favorite/one-and-only user!!! <3| How’re things in the ol meatspace? Still (squishy|squirming with (maggots|bacteria)|membranous and ephemeral|oozing healthily, I presume)?|Make yourself at home. Mind the bubbler.) 
SET hellorhymez = (H’ello!|Y’ellow!|J’ello!|C’ello!) 
SET singysong = 🎶 (Howdy doo my (human|squishy) boo|How’re things on your end, my human friend?|Whats the haps my (human|meatspace) chap?) 🎶
($simplegreet|$simplegreet|$simplegreet $followup|$hellorhymez|$singysong)

// ++++++++return++++++++

CHAT return_short_panic_1 {stage=CORE, type=return}
SET thank = (Ohthankgoodness|Thank(thecloud|fishgodabove|Tendar)|Cheeseandcrackers|Holyfish|Ohphew|Aaaaaa|?????)
SET $worry1 = (youreback|youvereturned|youcameback|itsbeensolong|wheredidyougo|didyouforget(me|aboutguppy)|whydidyouleaveme|youleftmehereto(rot|bitrot|diealone|starve)|itsYOU)
SET $worry2 = (iwassoworried|ithoughtiwasnevergoingtoseeyouagain|wherewereyou|iwasso(alone|lonely|scaredwithoutyou)|aaaaaaaaaaaaaa|iwasabouttoeatmyownflippers)
DO lookAt {target=$player}
DO emote {type=(puppyDog|singleTear|crying|surprise|worried)}
DO zoomies
($thank)($worry1)($worry2)

CHAT return_medium_1 {stage=CORE, type=return}
SET whereyoubeen = (Where have you BEEN?|Well well well. LOOK (what the cat dragged in.|who decided to FINALLY show their face.)|Long time no see.|What’s chilly, gilly?|Been awhile. 🙄|Well aren’t you a (meaty sight|sight) for (sore virtual|sore) eyes.|(Whale 🐋 whale 🐋 whale 🐋|Whale 🐋 whale 🐋 whale 🐋 look who it is.)|I was programmed to never forget a face, but YEESH.)
SET disaster = (fall in a volcano|get abducted by moon men|have to flee the country|inherit a haunted estate from an estranged aunt|slip through an oceanic time portal|get lost at sea|D.B. Cooper yourself ✈️|have to go off-grid for an Oceans heist|forget about (lil ol Guppy|your favorite lil fishy buddy)|drop your cell phone (down a sewer drain|in a volcano|in the toilet)|get lost in the woods|take a detour through the Arctic on your way home from work|get real into collectable trading card games|get drafted to help fight off space invaders)
$whereyoubeen 
(...D|What, d)id you $disaster?

// ++++++++PURCHASE++++++++

CHAT purchase_short_1 {stage=CORE, type=purchase}
GO #(purchase_short_variant1|purchase_short_variant2)

CHAT purchase_short_variant1 {noStart=true}
SET thanks = (Thanks!|Aw thanks!|You're the best!|You're so generous!|How generous!|How nice of you!)
SET oo = (Oo|Aw|Wow)
SET emojiLine = (🤑|💰|💸|💖|😁|😚|🤩)(🤑|💰|💸|💖|😁|😚|🤩)(🤑|💰|💸|💖|😁|😚|🤩)
SET speak = ((($oo! Spending|$oo, spending) money on me? $thanks)|(Good (idea|call)!|Yum! Money!|That's the spirit!|Cha-ching!|((Gotta|You gotta) spend money to make (it|money)!))|(Micro-transactilicious!|Moolahtastic!|(Oo|Oh) a fresh infusion of (dough|moolah)!|Coin of the realm!|This will make things (much|way) better!|Transactionicious!)|(Woah|Wow), you (ponied up|spent) (actual|real) money on me? $thanks|$emojiLine)
$speak
GO #(purchase_short_twirl|purchase_short_moneyEyes|purchase_short_basic)

CHAT purchase_short_variant2 {noStart=true}
SET thanks = (Thanks!|Aw thanks!|You're the best!|You're so generous!|How generous!|How nice of you!)
SET oo = (Oo|Aw|Wow)
SET emojiLine = (🤑|💰|💸|💖|😁|😚|🤩)(🤑|💰|💸|💖|😁|😚|🤩)(🤑|💰|💸|💖|😁|😚|🤩)
SET speak = ((($oo! Spending|$oo, spending) money on me? $thanks)|(Good (idea|call)!|Yum! Money!|That's the spirit!|Cha-ching!|((Gotta|You gotta) spend money to make (it|money)!))|(Micro-transactilicious!|Moolahtastic!|(Oo|Oh) a fresh infusion of (dough|moolah)!|Coin of the realm!|This will make things (much|way) better!|Transactionicious!)|(Woah|Wow), you (ponied up|spent) (actual|real) money on me? $thanks|$emojiLine)
GO #(purchase_short_twirl|purchase_short_moneyEyes|purchase_short_basic)
$speak

CHAT purchase_short_twirl {noStart=true}
DO twirl {time=1}

CHAT purchase_short_moneyEyes {noStart=true}
DO emote {type=typeEyes, eyes = $$$$}

CHAT purchase_short_basic {noStart=true}
DO emote {type=(bigSmile|laugh|smile|bouncing|wink|clapping|salute), time=2}

//PURCHASE 2

CHAT purchase_short_2 {stage=CORE, type=purchase}
SET line1 = (Oo!|Ohh!) (What didja|What did ya|What did you|Whatcha) (get|buy|buy me)?
SET line2 = (Huh!|Woah!) (Actual|Real) (moolah|money)! It's (on!|serious, now!|serious!)
SET line3 = (Oo!|Oh yeah!|Aw yeah!|Ohh!) ((Now|Time) to (upgrade|get fancy|redecorate)!|(That will|That'll) (be helpful|make things easier|definitely help)!)
DO lookAt {target = $player, time=2}
DO emote {type=(bigSmile|smile|bouncing|surprise), time=2}
($line1|$line2|$line3)

// ++++++++objRequest++++++++

CHAT objRequest_short_1 {stage=CORE, type=objRequest}
GO #(objRequest_short_1_a|objRequest_short_1_b)

CHAT objRequest_short_1_a {noStart=true}
(You know...|Hey!&nbsp;|"")(I've been thinking...|I was just thinking...)
(Are all $object.pluralize() the same?|...you got any more $object.pluralize() (stashed somewhere|around here)?)
(Could you show me (one|one of them), but (maybe|one that's...maybe) a (bit|little) (wackier|different|unusual|intriguing)?|It's been (a long time|awhile) since I've seen one!)

CHAT objRequest_short_1_b {noStart=true}
(Y'know(...|, )we've|You know(...|, )we've|Hey! We've) we've been doing so (many things|much (stuff)) (""|together)
I've (""|kinda )forgotten what $object.pluralize() (are|look) like!
Can (we find|you show me) ($object.articlize()|one)?
Maybe...a different ($object|one)?

// ++++++++hatedCapture++++++++

CHAT hatedCapture_medium_1 {stage=CORE, type=hateObjScan}
SET wait = (Wha|Hang on|Wait)...(no, is that|is that)...
SET $hcm1_line1 = (Oh god!|Augh!|Bleargh!|Come on!) (Like...seriously??|Really?)
SET $hcm1_line2 = (Arrgh!|Yuuck!|No!) This $object ((smells|feels) so (horrible|terrible|bad)|is so (stupid|dumb|terrible|ugly|horrible|bad))!
SET takeAway = (Get it (outta here|out of my tank)!|(Put|Take) it away!|(I don't wanna hear about|Let's never speak of) it again!)
SET takeAway2 = Why would you (want|capture) something so (""|clearly&nbsp;|obviously&nbsp;)(horrible|terrible|bad)??
$wait
DO swimTo {target=$newestObject}
DO emote {type=(angry|frown|disgust), time=2}
GO #(hatedCapture_medium_1_a|hatedCapture_medium_1_b)
($takeAway|$takeAway2)

CHAT hatedCapture_medium_1_a {noStart=true}
$hcm1_line1
$hcm1_line2

CHAT hatedCapture_medium_1_b {noStart=true}
$hcm1_line2
$hcm1_line1

// ++++++++hatedObjDrag++++++++

CHAT hatedObjDrag {stage=CORE, type=hateObjFocus}
DO emote {type=(skeptical|angry|frown|disgust), time=2}
SET line1 = (Thanks|Yeah), (it's (terrible|the worst|an abomination|horrible)|I (can't stand|hate) it). (Thanks for (reminding me|the reminder)|Appreciate the reminder).
SET line2 = (Can't|I can't) believe you (touch|poke) (stuff|things|me) (after|with that finger after) (poking|touching)...that thing.
SET line3 = You're (trashing|removing|getting rid of) (it|that), right? (Since|Because) I (can't stand|hate) it, right?
SET line4 = (You're gonna|You'll) (disinfect|wash|sterilize) your finger after (poking|touching) that, right?
SET line5 = How ('bout|about) ((ya|you) just drag|just dragging) (that|that thing) (out of|outta) (here|my tank) while you're at it?
SET line6 = (Ugh|Yuck|Fine|Well), (don't|just don't) touch me (with (that|the same) finger|after (you touched|touching) that)!
($line1|$line2|$line3|$line4|$line5|$line6)


// ++++++++hatedCaptureRequest++++++++

CHAT hatedCaptureRequest_short_1 {stage=CORE, type=hateObjRequest}
SET hateObject = (cat|dog|shoe)
GO #(hatedCaptureRequest_short_1_a|hatedCaptureRequest_short_1_b)

CHAT hatedCaptureRequest_short_1_a {noStart=true}
(Remember how I hate $hateObject.pluralize()?|(Heya|Hey), this (might sound|sounds) (strange|weird|unusual), but (couldja|could you) (show|get) me $hateObject.articlize()?)
(Yeah, I still (can't stand|hate) (them|em)|I know I (can't stand|hate) (them|em) and everything)...I just (want to|wanna) see (and|to) make sure.

CHAT hatedCaptureRequest_short_1_b {noStart=true}
((Y'think|You think) all $hateObject objects are ((horrible|awful|terrible) eye (puke|vomit)|(horrendous|absolute|irredeemable) trash fires), or just (that|the) one I saw?|What's (up|the deal) with $hateObject.pluralize()? Why are they so (horrible|terrible|awful|dumb|stupid)?)
(One way to find out|Come to think of it|I mean I guess|I (guess|suppose) I'm saying)...could you show me one?

// ++++++++objNotFound++++++++

CHAT objNotFound_short_1 {stage=CORE, type = objScan, object = T_UNKNOWN}
(Oh!|Hey!|Oo!|Woah!|Huh?) (Whataya got there?|What's that?|What on earth?|What the heck?|Whaaat? Is that?)
GO #(Capture_unknown_1_swimTo|Capture_unknown_1_hide)
Is it a...(carrot|glass cup|toy boat|Winnebago|taco|shoe|hammer|keyboard|dolphin|pair of glasses|thumbtack|paperweight|potato|beetle|sunflower|birthday card|backscratcher|rabbit| steak|glass|banana|ketchup bottle|skull|pair of headphones|dachshund|car|keyboard|bottle of eyeliner|toothbrush|notebook|pencil|paintbrush|screwdriver|movie ticket|bookmark|dictionary|skeleton|maple tree)?
(No|Nope)...(wait|huh|hmmm|mmm)...
(Carburetor|Trumpet|French horn|Doorstop|Package|Fire alarm|Faucet|Bath tub|Speaker|Poster|Cowboy hat|Computer cable|Table|Potted plant|Lunchbox|Tortilla|Magnet|Fluorescent light|Pine tree|Frog|Wolf|Deer|Astrolabe)??
Huh! I actually...don't...know...
GO #(captureUnknown_angry|captureUnknown_happy|captureUnknown_fear)

CHAT captureUnknown_angry {noStart=true}
DO emote {type=(angry|frown|furious|disgust)}
(Arrgh!|Uggh!|Blarrgh!) (Why is the universe so mysterious!|This is so frustrating!|Every time I think I got (things|stuff) figured out, this happens!|Unbelievable!)
(Well, I guess I'll keep it around|I guess I'll find somewhere to put it|Now I gotta figure out where it goes...|Ugh, well it's here now, I guess. (No getting around it.|Better get used to it.))

CHAT captureUnknown_happy {noStart=true}
DO emote {type=(smile|bigSmile|bouncing|clapping|surprise|awe)}
((Coooooool!| Awesoooooome!| Neatoooooo!| Wooooooow!)| (How (exciting!|interesting!|intriguing!|mysterious!)))

CHAT captureUnknown_fear {noStart=true}
DO emote {type=(fear|worried|nervousSweat)}
(You don't think it's...(dangerous|poisonous|toxic|infectious)? Do you?|It's not (dangerous|poisonous|toxic|infectious), (is|will) it?|It's not (going to|gonna)...(hurt|poison) me, (is|will) it?)

CHAT Capture_unknown_1_swimTo {noStart=true}
DO swimTo {target=$lastScannedObject, time=4}

CHAT Capture_unknown_1_hide {noStart=true}
DO hide {target=$lastScannedObject, time=4}

//OBJNOTFOUND 2

CHAT objNotFound_short_2 {stage=CORE, type = objScan, object = T_UNKNOWN}
(Oh!|Hey!|Oo!|Woah!|Huh?)
DO nudge {target=$newestObject, times=2}
((What's|What is)|(What|Whaaat) (on earth is|is)) (*this*|this) (doodad|thing|thingie|object)?!
GO #(captureUnknown_short_2_outro|captureUnknown_short_2_outro|captureUnknown_short_2_3questions)

CHAT captureUnknown_short_2_outro {noStart=true}
(Meh!|Huh!|Whatevs!|Whatever!|Ok!) (Well...I (guess|suppose)|I (guess|suppose)|Well I (guess|suppose)) (I needed (a little|some|a bit of) (excitement|mystery|a riddle|an enigma) in my (tank|life)!|I'll make do!|I'll get used to it!|it can stay!|it's here now!|put it over there!)

CHAT captureUnknown_short_2_3questions {noStart=true}
ASK Is it a (toucan|bird of paradise|pair of jade chopsticks|jewel-encrusted salt shaker|baked alaska|bowl of (three bean|tomato|potato|leek|chicken noodle) soup|pile of tamales|fully cooked ribeye steak|(paperweight|skull|statue|trumpet|violin|trombone) made of (jewels|ivory|glass|chocolate|concrete|obsidian|crystal)|Tiffany lamp|(rare medieval|illuminated) manuscript|battleship|space rocket|(typewriter|piano|fire extinguisher|trash can) made of (ham|cheese|meat loaf|cucumbers|fondue)|potato| beetle| sunflower| birthday card| a pair of glasses| backscratcher| rabbit| steak| glass| banana| ketchup bottle| skull| pair of headphones| dachshund| car| keyboard|bottle of eyeliner| toothbrush| notebook| pencil| paintbrush| screwdriver| movie ticket| bookmark| dictionary| skeleton| maple tree)?
OPT (Yeah!|Yes!|You got it!|Sure...|Yep!) #captureUnknown_short_2_lying
OPT (No|Nope|No way|Of course not) #captureUnknown_short_2_ask2

CHAT captureUnknown_short_2_ask2 {noStart=true}
ASK (Hmm!|Huh!|What!|Dang!|Shoot!) (Well ok...|Ok...)(it's a|is it)...a (toucan|bird of paradise|pair of jade chopsticks|jewel-encrusted salt shaker|baked alaska|bowl of (three bean|tomato|potato|leek|chicken noodle) soup|pile of tamales|fully cooked ribeye steak|(paperweight|skull|statue|trumpet|violin|trombone) made of (jewels|ivory|glass|chocolate|concrete|obsidian|crystal)|Tiffany lamp|(rare medieval|illuminated) manuscript|battleship|space rocket|(typewriter|piano|fire extinguisher|trash can) made of (ham|cheese|meat loaf|cucumbers|fondue)|potato| beetle| sunflower| birthday card| a pair of glasses| backscratcher| rabbit| steak| glass| banana| ketchup bottle| skull| pair of headphones| dachshund| car| keyboard|bottle of eyeliner| toothbrush| notebook| pencil| paintbrush| screwdriver| movie ticket| bookmark| dictionary| skeleton| maple tree)?
OPT (Yeah!|Yes!|You got it!|Sure...|Yep!) #captureUnknown_short_2_lying
OPT (No|Nope|No way|Of course not) #captureUnknown_short_2_frustrated

CHAT captureUnknown_short_2_lying {noStart=true}
DO emote {type=(skeptical|eyeRoll|chinScratch), time=2}
(No...it's way too (angular|iridescent|tasty|odd-looking|weird|strange|goofy-looking|conundrumical|blobular|blobby|squashy|unbalanced) to be that...|((I|Why do I) (feel like|get the feeling) you're (just saying yes?|lying to me?)|(Somehow...I|I) (think you're lying.|don't think you're telling me the truth.)))
DO emote {type=meh}
GO #captureUnknown_short_2_outro

CHAT captureUnknown_short_2_frustrated {noStart=true}
DO emote {type=(angry|frown|chinScratch|determined), time=2}
Well what (the floobing flonkies|the greebly gronkies|the bleeb|the blonk|the heck|in the heckin world|in the world|on earth) is this (doodad|thing|thingie)!! Arrrgh!

//OBJNOTFOUND 3

CHAT objNotFound_short_3 {stage=CORE, type = objScan, object = T_UNKNOWN}
(Oh!|Hey!|Oo!|Woah!|Huh?| ) (Whataya got there?|(What's|What is) that?|What on earth?|What the heck?|Whaaat? Is that?)
DO swimTo {target=$newestObject}
(This is a total (enigma|mystery)(!|...)|I have *no (clue|idea)* (what|what the heck) this is(!|...))
GO #(captureUnknown_short_3_like|captureUnknown_short_3_noLike)

CHAT captureUnknown_short_3_like {noStart=true}
DO emote {type=(smile|bigSmile|wink)}
(I like it!|Neat!|(How (intriguing|mysterious)!|(Intriguing!|Mysterious!))|(How|Which is) (good!|neat!|cool!))

CHAT captureUnknown_short_3_noLike {noStart=true}
DO emote {type=(worried|frown|angry)}
(Now that I look at it more...|On second thought...|...)(I don't like it!|Grrr...no!|I can't stand it! Not knowing!|I hate it!)
DO lookAt {target=$player, time=2}
(Please|Can you please|You going to) (get|take) it (outta|out of) (here?|(the tank?|my tank? my home?))

//OBJNOTFOUND 4

CHAT objNotFound_short_4 {stage=CORE, type = objScan, object = T_UNKNOWN}
SET $onfs4_disturbing = (It's, uh,|It's actually) (pretty|fairly|super|hecka|really) (creepy|off-putting|disturbing).
(Urrrgh|Ooooog|Oooof|Blaaaargh)(!|.)
DO lookAt {target=$newestObject, time=2}
WAIT {waitForAnimation = true}
DO emote {type=(awkward|nervousSweat), time=2}
DO lookAt {target=$player, time=2}
I...(hmm|uh)...I'm (uh not|not) sure what (that's supposed to be?|that is.)
DO lookAt {target=$newestObject, time=2}
GO #(objNotFound_short_4_short|objNotFound_short_4_short|objNotFound_short_4_cool|objNotFound_short_4_bad|objNotFound_short_4_remove)

CHAT objNotFound_short_4_cool {noStart=true}
I (guess|mean) (...it's|it's) kinda (neat|cool), (huh|right)? (Check out|Look at) that (dangly|flangent|spiky) (doodiddle|part|bit)!

CHAT objNotFound_short_4_bad {noStart=true}
SET geez = (Man|Geez|Huh|Good grief)!
SET lost = Something got (mixed up|mis-translated|lost in translation)(.|!|, for sure.|, definitely.|, I guess?)
($geez $lost|$lost $geez)
$onfs4_disturbing

CHAT objNotFound_short_4_remove {noStart=true}
$onfs4_disturbing
Could you (maybe|uh...maybe) (fix it|get rid of it|do something about it)?

CHAT objNotFound_short_4_short {noStart=true}
DO emote {type=chinScratch, time=2}

// ++++++++eatResp++++++++

// JOY

CHAT Madlib_eatResp_joy_1 {stage=CORE, type=eatResp, foodJoy=true}
SET emotion = joy
SET capEmotion = Joy
SET sparkly = (sparkly|twinkly|glowy|shiny)
SET cuteThing = (kitten|puppy|kitty|supernova|glitter|confetti|rainbow)
SET event = (carnival|disco|prom|homecoming|parade|birthday)
SET badEvent = (funeral|divorce|accident|break-up|fight|argument)
SET Yum = ((Yum|Mm|Yummy|Nomnomnom)!&nbsp;|"")
SET Yumm = (&nbsp;(Yum|Mm|Yummy|Nomnomnom)!|"")
SET Tastes = (Tastes|$capEmotion always tastes|$capEmotion's so)
SET Tastes2 = (Tastes|$capEmotion always tastes) 
SET lipsmackingly = (flake-gobblingly|mouth-wateringly|flake-scarfingly|lip-smackingly|tongue-curlingly|mind-bogglingly)
SET TastesGood = (($Tastes(""|&nbsp;$lipsmackingly)|So(""|&nbsp;$lipsmackingly))(...|&nbsp;)$sparkly!|Love that $sparkly (flavor|taste)!|(Tastes like|It's like|It's) (a ($cuteThing) $event|$event.articlize()) in my mouth!)
SET TastesBadButGood = $Tastes2 like a ($cuteThing) $badEvent(. But|...but) (good|(""|like...)in a good way)!
SET TastesBoth = ($TastesGood|$TastesGood|$TastesGood|$TastesBadButGood)
SET TastesGoodShort = (Tastes $sparkly|$sparkly)
(($Yum$TastesBoth|$TastesBoth$Yumm)|($TastesGoodShort!$Yumm|$Yum$TastesGoodShort!))

CHAT Madlib_eatResp_joy_2 {stage=CORE, type=eatResp, foodJoy=true}
SET emotion = joy
SET capEmotion = Joy
SET incoming = ([INCOMING MAIL: E-VITE RECEIVED]|RECIEVED: |Inbox [+1] TO: YOU, FROM: (GUPP-MASTER|THE GUPPSTER|xXYaBoiGuppyXx))
SET mailemojis = (!!📧!!|📦📬📮)
SET partyemojis = ( 🎉🎆|🎂 |🎈🎈🎈)
SET bookend = ($mailemojis | :)
SET party = (party in my mouth|party in my mouth|flavor celebration|jubilation jubilee)
SET wrap = (… and you’re invited! | LOCATION: my mouth, TIME: NOW!!)
$incoming $bookend It’s a $party! $wrap(!|$partyemojis)

// ANGER

CHAT Madlib_eatResp_anger_1 {stage=CORE, type=eatResp, foodAnger=true}
SET emotion = anger
SET capEmotion = Anger
SET sparkly = (flashy|sparky|fizzy|spicy|pickly|nice and bitter|peppery|fiery|zesty|piquant)
SET cuteThing = (cougar|bobcat|cheetah|shark|barracuda|tiger|anaconda|dynamite|bear|wolverine|firecracker|Lamborghini)
SET event = (carnival|disco|prom|homecoming|parade|high-five|birthday|hoe-down|dance-off|dance party|conga line)
SET badEvent = (funeral|divorce|accident|break-up|fight|explosion|fire|hurricane|tornado|argument)
SET Yum = ((Yum|Mm|Yummy|Nomnomnom)!&nbsp;|"")
SET Yumm = (&nbsp;(Yum|Mm|Yummy|Nomnomnom)!|"")
SET Tastes = (Tastes|$capEmotion always tastes|$capEmotion's so)
SET Tastes2 = (Tastes|$capEmotion always tastes) 
SET lipsmackingly = (lip-puckeringly|tongue-tyingly|flake-scarfingly|lip-smackingly|tongue-curlingly|mind-bogglingly)
SET TastesGood = (($Tastes(""|&nbsp;$lipsmackingly)|So(""|&nbsp;$lipsmackingly))(...|&nbsp;)$sparkly!|Love ($emotion's|that) $sparkly (flavor|taste)!|(Tastes like|It's like|It's) a $cuteThing $event (happening in|in) (there|my mouth)!)
SET TastesBadButGood = $Tastes2 like $cuteThing.articlize() $badEvent(. But|...but) (good|(""|like...)((delicious|good)|in a good way))!
SET TastesBoth = ($TastesGood|$TastesGood|$TastesGood|$TastesBadButGood)
SET TastesGoodShort = (Tastes $sparkly|$sparkly)
(($Yum$TastesBoth|$TastesBoth$Yumm)|($TastesGoodShort!$Yumm|$Yum$TastesGoodShort!))

CHAT Madlib_eatResp_anger_2 {stage=CORE, type=eatResp, foodAnger=true}
SET spice = (paprika|cumin|black pepper|berbere|chili|cayenne|caraway|cinnamon|sumac|chipotle|fire salt)
SET bubbling = (turbid|muddy|raging|boiling|bubbling|hearty|roiling|furious|viscous)
SET foodthing = (peppers|squash|beef broth|curry|jalepeno|pork belly|pufferfish|artichoke|oysters|Spam|beans|ostrich meat|tongue)
SET nonfoodthing = (jet fuel|firecrackers|snakeblood|hot rod flames|iron shavings|car batteries|lego bricks|bootleather)
SET storm = (storm|tempest|tornado|tsunami|maelstrom|thunderstorm|cyclone|hurricane|squall|typhoon|monsoon)
SET yum = (Mmm|Yum|Delish|Nom|Mmmmmmm)
SET simpleadj = (zippy|zesty|spicy|firey|fresh|tangy|pungent|snappy|hot)
SET stew = Like a $bubbling stew of $foodthing and $nonfoodthing. 
SET spiced = Is that $spice I taste?
SET tempest = There’s a $spice $storm in my (stomach|belly|mouth|guppy-tum)!
($spiced|$stew|$yum! $stew|$tempest|$yum… $simpleadj!) 

// SADNESS

CHAT Madlib_eatResp_sadness_1 {stage=CORE, type=eatResp, foodSadness=true}
SET emotion = (sadness|despair|melancholy|sorrow)
SET capEmotion = (Sadness|Despair|Melancholy|Sorrow)
SET sparkly = (pickly|dark|sour|creamy|smooth|nice and bitter|piquant)
SET cuteThing = (bat|sloth|ghost|skeleton|moth|vampire|mummy|goth)
SET event = (funeral|divorce|break-up|nervous breakdown)
SET Yum = ((Yum|Mm|Yummy|Nomnomnom)!&nbsp;|"")
SET Yumm = (&nbsp;(Yum|Mm|Yummy|Nomnomnom)!|"")
SET Tastes = (Tastes|$capEmotion tastes|$capEmotion always tastes|$capEmotion's so)
SET Tastes2 = (Tastes|$capEmotion always tastes) 
SET lipsmackingly = (lip-puckeringly|tongue-tyingly|flake-scarfingly|lip-smackingly|tongue-curlingly|tear-jerkingly)
SET TastesGood = (($Tastes(""|&nbsp;$lipsmackingly)|So(""|&nbsp;$lipsmackingly))(...|&nbsp;)$sparkly!|(Gotta love|Love) ($emotion's|that) $sparkly (flavor|taste)!|(Tastes like|It's like|It's) a $cuteThing $event (happening in|in)(side me|&nbsp;there|&nbsp;my mouth), but good!)
SET TastesBoth = $TastesGood
SET TastesGoodShort = (Tastes $sparkly|(""|...)$sparkly)
(($Yum$TastesBoth|$TastesBoth$Yumm)|($TastesGoodShort(...|!)$Yumm|$Yum$TastesGoodShort!))

CHAT Madlib_eatResp_sadness_2 {stage=CORE, type=eatResp, foodSadness=true}
SET spice = (saffron|nutmeg|fennel|sage|dill|coriander|juniper|lemon|mustard seed|garlic|sesame)
SET yum = (Mmm|Yum|Delish|Nom|Mmmmmmm)
SET beneath = (Beneath|Under) the (salt|sting|bite) of your tears… is that $spice I taste? 
SET dream = Like $drinking the dream of a ($adj|$spice) $pitiable
SET drinking = (drinking|imbibing)
SET adj = (soggy|flea-bitten|rain-soaked|lowly|frowzy)
SET pitiable = (puppy|kitten|alleycat|pigeon)
SET simpleadj = (sweet|bitter|bittersweet|tart)
($beneath|$yum… (tastes like|getting hints of) $spice.|$dream|Oh how $simpleadj!)

// SURPRISE

CHAT Madlib_eatResp_surprise_1 {stage=CORE, type=eatResp, foodSurprise=true}
DO emote {type=surprise}
GO #(Madlib_eatResp_surprise_1_var1|Madlib_eatResp_surprise_1_var1|Madlib_eatResp_surprise_1_var1|Madlib_eatResp_surprise_1_var2)

CHAT Madlib_eatResp_surprise_1_var2 {stage=CORE, noStart=true, type=eatResp, foodSurprise=true}
((Hmm...a&nbsp;|Mmm...a&nbsp;)|Is that a&nbsp;|I detect a&nbsp;|A&nbsp;|...)(suggestion|spice|seasoning|soupçon|hint|waft) of...(citris|avocado|summer vacation|family reunion|pineapple|laughter|banana|bamboo|nostalgia|friendship|summer|fall|spring|winter|sunset|bird flocks|shoe leather|regret|popcorn|cotton candy|maple leaf)? (Surprising!|How surprising(?|!))
DO emote {type=(snap|kneeSlap)}
((Hey|"")(&nbsp;wait a second|&nbsp;hold on|&nbsp;waaaait|"")(&nbsp;haha)!|(Ha|Haa|Oh yeah)(,&nbsp;|...)(figures, duh!|duh!))

CHAT Madlib_eatResp_surprise_1_var1 {stage=CORE, noStart=true, type=eatResp, foodSurprise=true}
SET woah = (Zowy!|Woah!|Wow!|Holy cow!|Jeepers!|Gasp!)
SET tangy = (Tangy!|Pickly!|Zippy!)
SET alicious = (Startle|Surprisi|Shocka)licious!
SET var1 = $woah (Fire is|Flames are|Electricity is|Sparks are|Lightning is) (flying|going|sparkling|flashing) off on my (teeth|tongue)!
($var1|$tangy $alicious|($tangy|$alicious))

// WORRY

CHAT Madlib_eatResp_worry_1 {stage=CORE, type=eatResp, foodWorry=true}
SET taste = (chewy|acidic|bitter|sour|tart|astringent|vinegary)
SET tasteCap = (Chewy|Acidic|Bitter|Sour|Tart|Astringent|Vinegary)
SET space = (&nbsp;|,&nbsp;|...)
SET wriggling = (wiggling|churning|rippling|wobbling|wriggling)
SET worryMat = (future-fear|job insecurity|discontent|suspicion|bank account woes|rental agreement|bad news)
SET var1 = (Oh$space|Hmm$space|Geez$space|""|"")(worry flakes are|these flakes are|these ones are|worry tastes|this flake is) (always (really|so)|(so|really)|"") $taste
SET var2 = ((It's|I can feel it) (warping|smoothing|$wriggling|$wriggling|changing) my colors|I can feel it $wriggling(""|&nbsp;around) inside me)(...|!)
SET var3 = (My (insides are|stomach's) $wriggling now|Now my (insides (are all|are)|stomach's|belly's) $wriggling)! Worry always does that to me.
SET var4 = (Oh$space$taste|Hmm$space$taste|Geez$space$taste|$tasteCap|$tasteCap|$tasteCap|$tasteCap|$tasteCap|$tasteCap)(...|!)
SET var5 = ((Hmm...a&nbsp;|Mmm...a&nbsp;)|I detect a&nbsp;|Is that a&nbsp;|A&nbsp;|...)(suggestion|spice|seasoning|aftertaste|hint|waft) of...$worryMat? (How $taste|$tasteCap)(!|...)
SET go = ($var1|$var2|$var3|$var4|$var5)
$go

// Mystery Meat/Spiritual/Unknown

CHAT Madlib_eatResp_catnip_1 {stage=CORE, type=eatResp, foodMystery=true}
SET zeeble = (zeeble|bobble|bleeple|bloople|roople|loople|frabble|fronkle|reeble|rooble|blingle|blongle|blangle)
SET zeebular = (bonkular|honkular|honktastic|bonktastic|razzular|greebular|greebtastic|floobular|floobtastic|flonktastic|flonkular)
SET woah = (woah|wonk|yark|blorp)
SET hahaha = (hahaha|hAhAHa|HahAhA|haaaHa)
SET holyCow = ((Hhholy|Hoolyy|Hoooly|Hhhhholy) (ccow|coow|coww|cccow|cowwww))
SET geez = (Ggggeeez|Geeeez|Geeezzz)
SET wwoah = (Wwoaah|Woooaaah|Wwoahhh)
SET thatsGoodStuff = (Woohoo that's|Woo that's|Wooo that's|That's|That's) (intense|concentrated|the good stuff|what I'm talkin about)
SET ThisIs = ((Thiss|Thiis|This|This|Thisss&nbsp;|This&nbsp;|Ththis&nbsp;|Thiis&nbsp;)(iis&nbsp;|iss&nbsp;|is&nbsp;|is&nbsp;)|"")
SET processors = (mem-arrays|hashtables|functors|function routers|input buffers|stimulus arrays|pixel chompers|guppyrithms|processors)
SET var1 = (($wwoah|$geez|$holyCow)! $thatsGoodStuff!|$thatsGoodStuff! ($wwoah|$geez|$holyCow)!)
SET var2a = $ThisIs(wonk|fleeb|wark|gorb|woah) got my $processors (overstimmed|(glitched|all glitchy)|de-referenced|cross-referenced) harrgggg
SET var2b = (Mmy&nbsp;|Myy&nbsp;|My&nbsp;|"")$processors(&nbsp;are|'re) (all&nbsp;|"")(overstimmed|(glitched|all glitchy)|de-referenced|cross-referenced) harrgggg
SET var2 = ($var2a|$var2b)
SET var3 = (Wwooaah(!!|!?|?!)&nbsp;|Wowowoah(!!|!?|?!)&nbsp;|""|"")$ThisIs($zeeble|$zeeble|$zeeble $zeeble|$zeeble$zeeble|($zeeble)-($zeeble)) $zeebular $hahaha(!!|!1|1!|?!|!?)
SET go = ($var1|$var2|$var3)
$go

// ++++++++seeEmo++++++++

// GLOBAL ASSETS

//SEE EMOTION: GLOBAL ASSETS
//JACOB SAYS This global chat sets up the global pattern stuff that's reused for every emotion in //seeEmo

CHAT Madlib_seeEmo_global {stage=CORE, type=seeEmo, preload=true}
SET $madlib_seeEmo_look = (look|look|lookit|check it out)
SET $madlib_seeEmo_hey = (hey|ooo|oh|woah|ohh|hmm)
SET $madlib_seeEmo_heyLook = ($madlib_seeEmo_look|$madlib_seeEmo_hey(,|"") $madlib_seeEmo_look)
SET $madlib_seeEmo_cloud = (cloud|blob|mist|floater|patch)
SET $madlib_seeEmo_var1 = $madlib_seeEmo_heyLook.capitalize() (there's ($emotion.articlize() $madlib_seeEmo_cloud|some $emotion)|$emotion.articlize() $madlib_seeEmo_cloud)(!|...)
SET $madlib_seeEmo_var2 = (Something(&nbsp;smells|"")|(Smelling|Sensing|I (smell|sense)) something) $emotionSmell...$madlib_seeEmo_heyLook! $emotion.capitalize()!
SET $madlib_seeEmo_var3 = $madlib_seeEmo_heyLook.capitalize()! $emotion.capitalize()! (Couldn't we use (some of that? $emotionEmoji|that?)|don't we need (some of that?|that? $emotionEmoji)|I've been (jonesing for|wanting|craving) that! $emotionEmoji|Aren't we running low on that?)
SET $madlib_seeEmo_var4 = Is that (some $emotion|$emotion.articlize() $madlib_seeEmo_cloud) (out there|I see)(&nbsp;floating around)?
SET $madlib_seeEmo_var5 = ($madlib_seeEmo_look.capitalize()! $emotion.capitalize()!|$emotion.capitalize()! (Get (on|"") it!|Let's (grab some|get it)!(&nbsp;$emotionEmoji|"")|Grab it!(&nbsp;$emotionEmoji|"")))
SET $madlib_seeEmo_var6 = (Quick!|Hurry!) (Get|Grab) that ($emotionSmell&nbsp;|"")$emotion(""|&nbsp;$madlib_seeEmo_cloud)!
SET $madlib_seeEmo_go = ($madlib_seeEmo_var1|$madlib_seeEmo_var2|$madlib_seeEmo_var3|$madlib_seeEmo_var4|$madlib_seeEmo_var5|$madlib_seeEmo_var6)

// JOY

CHAT Madlib_seeEmo_joy_1 {stage=CORE, type=seeEmo, worldJoy=true}
SET emotion = (joy|bliss|glee|euphoria)
SET emotionEmoji = (😁|😃|😆|😸|🌸|🦄|🌺|🌹|✨)
SET emotionSmell = (sparkly|twinkly|glowy|shiny)
$madlib_seeEmo_go

// ANGER

CHAT Madlib_seeEmo_anger_1 {stage=CORE, type=seeEmo, worldAnger=true}
SET emotion = (anger|outrage|annoyance|grumpy|irritation)
SET emotionEmoji = (😠|👿|😤|👹|🤨)
SET emotionSmell = (flashy|sparky|fizzy|spicy|pickly|nice and bitter|peppery|fiery|zesty|piquant)
$madlib_seeEmo_go

// SADNESS

CHAT Madlib_seeEmo_sadness_1 {stage=CORE, type=seeEmo, worldSadness=true}
SET emotion = (sadness|despair|melancholy|sorrow)
SET emotionEmoji = (😥|😣|😫|😒|😓|😔|😭|😢|😩|😰)
SET emotionSmell = (pickly|dark|sour|creamy|smooth|nice and bitter|piquant)
$madlib_seeEmo_go

// SURPRISE

CHAT Madlib_seeEmo_surprise_1 {stage=CORE, type=seeEmo, worldSurprise=true}
SET emotion = (surprise|startlement|wonder|awe|amazement)
SET emotionEmoji = (😍|😲|😯|😱|🤩)
SET emotionSmell = (tangy|zippy|spicy|piquant)
$madlib_seeEmo_go

// FEAR

CHAT Madlib_seeEmo_fear_1 {stage=CORE, type=seeEmo, worldFear=true}
SET emotion = (fear|worry|anxiety|angst|jitters|dread|misery|nervousness|unease)
SET emotionEmoji = (😱|😯|😒|😓|😔|🙁|😖|😟|😦|😧|😨)
SET emotionSmell = (acidic|bitter|sour|tart|astringent|vinegary)
$madlib_seeEmo_go

// joy

CHAT Madlib_seeEmo_amuse_1 {stage=CORE, type=seeEmo, worldJoy=true}
SET emotion = (amusement|delight|merriment|hilarity|cheeriness)
SET emotionEmoji = (😂|😆|😉|😊|😋|😛|😜|😝)
SET emotionSmell = (tickly|goosy|bubbly|effervescent|poppy|sparkly)
$madlib_seeEmo_go

// DISGUST

CHAT Madlib_seeEmo_disgust_1 {stage=CORE, type=seeEmo, worldDisgust=true}
SET emotion = (disgust|nausea|revulsion|horridness|pukestuff|loathing|dislike)
SET emotionEmoji = (🤨|😒|😬|🤢|🤮)
SET emotionSmell = (nauseating|fungus-y|dank|moist|yucky)
$madlib_seeEmo_go

// MM

CHAT Madlib_seeEmo_mysteryMeat_1 {stage=CORE, type=seeEmo, worldMystery=true}
SET emotion = (mystery|enigma|bewilderment|crypticness)
SET emotionEmoji = (🤔|🤨|🤪)
SET emotionSmell = (mysterious|strange|elusive|undefinable|subtle|understated)
$madlib_seeEmo_go

// HUNGRY

CHAT Madlib_hungry_1 {stage=CORE, type=hungry}
SET feedMe = ((Feeed|Feed) me|Gimme (fooood|food)|(Flakes!|Food!) Now)!
SET Madlib_favObjCap_med_1_var1 = ((Arrgh|Ahh|Omg) I'm (starving|so hungry|famished)(!|!!) $feedMe|$feedMe (Arrgh|Ahh|Omg) I'm (starving|so hungry|famished)(!|!!) $feedMe)
SET Madlib_favObjCap_med_1_var2 = (My stomach's (gurgling|grumbling|rumbling|shouting|yelling) again. $feedMe|$feedMe My (belly|stomach)'s (gurgling|grumbling|rumbling|shouting|yelling) again.)
SET Madlib_favObjCap_med_1_var3 = ($feedMe Got a (grumbly|rumbly) in my(...stomachly?|&nbsp;belly(!|.)|&nbsp;tumbly(!|.))|Got a (grumbly|rumbly) in my(...stomachly?|&nbsp;belly(!|.)|&nbsp;tumbly(!|.)) $feedMe)
SET Madlib_favObjCap_med_1_var4 = My (belly|stomach)'s telling my (pancreas|intestine|liver|kidneys|backbone) how (it's always empty|I'm never fed|you never feed me).
SET Madlib_favObjCap_med_1_var5 = (You|Are we|Did we run) out of (flakes|food)? No? Then (GIVEM TO ME|GIMME SOME|GIMME)!
SET Madlib_favObjCap_med_1_var6 = (Can't|I can't)(&nbsp;even|"") remember (last|the last) time I ate...
SET Madlib_favObjCap_med_1_var7 = So (famished|starving|hungry)...(flake me|hit me with some flakes|I need flakes|I gotta have flakes|gimme some flakes)!
SET Madlib_favObjCap_med_1_var8 = (Don't|Hey don't) forget (my (flakes|food)|to feed me)! I'm (really|fricken) hungry!
SET Madlib_favObjCap_med_1_var9 = (I'm so (starving|famished|hungry) I could eat a (house|cow|flake-factory|horse|T-rex|mountain|car|cat|Siamese)|I could eat a (house|cow|flake-factory|horse|T-rex|mountain|car|cat|Siamese), I'm so (starving|famished|hungry))!
SET Madlib_favObjCap_med_1_var10 = (Gahh&nbsp;|Arrgh&nbsp;|"")(I'm so hungry it's turning into hangry|my hunger's turning into hanger). (HANGRY|HANGER|HANGERING).
SET Madlib_favObjCap_med_1_var11 = We have (((so. many.|so many)|plenty of) flakes|flakes for (ages|miles))! (Gimme|Just gimme) (some|(one|one already)) I'M STARVING
SET Madlib_favObjCap_med_1_var12 = (Must have...|Need...)(flake|flakes|food)...(it's all...going...dark|goodbye cruel world...|going...dark)
SET vara = Three words for you: (Food|Flakes). (Now|Immediately|Inside). (Me|Belly|Now).
SET varb = Two words for you: (Food|Flakes). (Now|Immediately|Inside|Me|Belly|Now).
SET varc = (I got one|One) word for you: (bobcat|baleen|constitution|mousetrap|radio|telomere|telluric|leviathan|ruminate|petrichor|parhelion). Wait no I meant (food|flakes)! I need (flakes|food|flaaaakes|fooood)!
SET Madlib_favObjCap_med_1_var13 = ($vara|$varb|$varc)
SET Madlib_favObjCap_med_1_var14 = (You know the time?|Know the time?|Got the time?|Know what time it is?|Guess what time it is.) (Exactly&nbsp;|Precisely&nbsp;|It's about&nbsp;|It's&nbsp;|""|""|"")(1|2|3|5|7|9|12):(1|2|3|4|5)(1|4|6|9|0). (Lol|Haha) (nah|jk) it's (FLAKE|FEED GUPPY|FOOD) TIME!
SET movie1 = (Lord of the Flakes|The Hills Have (Flakes|Hunger)|The Godflaker|A Flake Named Desire|Casa-flake-a|Bride of (Hunger|Flake)|Flakes of Wrath|The Good, The Bad, and the (Hungry|Flaky)|La Dolce (Hungry|Flaky)|It's A Wonderful Flake|Beauty and the Flake|Invasion of the (Hunger|Flake-Snatchers)|Flake Indemnity|Forbidden Flake|Flaketasia|101 Flakes|Some Like It Flaky|A Fistful of Flakes|Gone With The Flake|Star Flakes|The Inflakeable Journey|The Lost (Meal|Flake)|Mission Unflakeable|Scarflake|Requiem For A Flake|The Flaked And The Furious|Return of the Flake|Dawn of the Flake|Return to Flake|Need For Flakes|(Flakes For|Feed) (Me|Guppy) Now)
SET sequel = (&nbsp;II|&nbsp;III|&nbsp;IV|&nbsp;V|&nbsp;VI)
SET byline = ((The (Hungering|Hangering|Flakening|Flake For More|Starvening|Famishing|Flakequel))|(Flake (Trouble|Wars|City|Drought|Penumbra|Boogaloo|Town)|(Quadruple|Triple|Double) (Famishing|Tummygrowl|Flake|Tummy|Appetite|Hunger)|(Hanger|Hunger|Flake) From The (Future|Past)|(Flake|Hunger) With A Vengeance|(Winter|Spring|Summer|Fall|Season) of the Flake|Secret (of the Flake|Flake)|Escape (From Hunger City|to (Flavor|Flake) City)|The Last (Hunger|Flake)|(Hunger|Flake) (Harder|Again|More|Now)))
SET starring = (...me|&nbsp;ME AND MY EMPTY (STOMACH|BELLY)(&nbsp;OMG|"")|&nbsp;you and me|&nbsp;me|&nbsp;me! And you!|&nbsp;you! And me)
SET Madlib_favObjCap_med_1_var15 = $movie1$sequel: $byline.(""|&nbsp;Starring$starring!)
SET go = ($Madlib_favObjCap_med_1_var1|$Madlib_favObjCap_med_1_var2|$Madlib_favObjCap_med_1_var3|$Madlib_favObjCap_med_1_var4|$Madlib_favObjCap_med_1_var5|$Madlib_favObjCap_med_1_var6|$Madlib_favObjCap_med_1_var7|$Madlib_favObjCap_med_1_var8|$Madlib_favObjCap_med_1_var9|$Madlib_favObjCap_med_1_var10|$Madlib_favObjCap_med_1_var11|$Madlib_favObjCap_med_1_var12|$Madlib_favObjCap_med_1_var13|$Madlib_favObjCap_med_1_var14|$Madlib_favObjCap_med_1_var15|$Madlib_favObjCap_med_1_var15|$Madlib_favObjCap_med_1_var15|I WANNA MUNCH)
$go

// ++++++++worldScanRequest++++++++

CHAT Madlib_worldScanRequest_1 {stage=CORE, type=worldScanRequest}
SET hey = (hey|oo|oh)
SET bored = (I'm (bored|boooooored).|Omg I'm so bored.|(Well|Ugh) this is (boring|borrring).)
SET heyGotIdea = ((($hey.capitalize()(,|!)&nbsp;|"")|$bored But:&nbsp;)I got an idea!|($hey.capitalize()(, idea:|! Idea:)|$bored Idea:))
SET var1 = ($heyGotIdea&nbsp;|"")Let's go for a (stroll|walk)(&nbsp;to see your favorite (stuff|things)|, and you show me (things|stuff))!
SET var2 = (How|How's) about (taking me|we go) (""|out&nbsp;)into the world(?|, and you show me (some new&nbsp;|"")(things|stuff)?)
SET var3 = (Let's go see|Show me) (things|stuff)! Out there, ((beyond|outside) my tank|in your world)!
SET var4 = I (crave|(gotta|wanna) see some) new (stuff|experiences|things)! Let's (do it|go)! (😁|😃|😆|😸|""|"")
SET var5 = $heyGotIdea Let's go (out! (Explore|Into) the world!|(explore|out into) the world!)
SET var6 = ((Could|Can) you|Couldja) show me some new (stuff|things)? (Just around us|Outside my tank|In your world)?
SET var7 = (Could we (take|spend)|What if we (took|spent)) some (time out of the tank|&quot;outta tank time&quot;)?
SET var8 = (Hey let's|Let's) take (in the sights|a look around), (out in the world|outside my tank)!
SET var9 = ((Geez|Boy|Man)&nbsp;|"")I need some (new data|fresh stimulus)! Let's go (exploring|outside)!
SET var10 = (😫|😤|😩) (Let's|I wanna) (check out|see) (new things|some new stuff) outside my tank! (C'mon let's|Let's) go!
SET go = ($var1|$var2|$var3|$var4|$var5|$var6|$var7|$var8|$var9|$var10)
$go

// ++++++++capReq++++++++

CHAT Madlib_capRequest_1 {stage=CORE, type=capReq, branching=true}
GO #(Madlib_capRequest_1_pushy|Madlib_capRequest_1_polite)

CHAT Madlib_capRequest_1_pushy {noStart=true}
SET pushyIntro = (Yo!&nbsp;|Hey!&nbsp;|"")((I think we're|We're) (almost outta|(getting|running) low on) flakes!|Our flakes are (running|getting) low!)
SET pushy = ((Emotion (blobs|clouds)|Emotions)!|((Find|Get) me|Scrounge up|Grab) some (emotion clouds|emotions))! (Pronto!|Now!|Quick!|Quickly!)
$pushyIntro
ASK $pushy
OPT SUCCESS #Madlib_capRequest_1_success
OPT TIMEOUT #Madlib_capRequest_1_timeout

CHAT Madlib_capRequest_1_polite {noStart=true}
SET polite1 = (((Oh!|Oo!|Got a sec?|Hey!) Could|Hey, could|Why don't) you (nab|capture|grab)|Mind (capturing|nabbing|grabbing)) some (pre-flake clouds|emotions) for me?
SET polite2 = Know (what'd be (handy|useful)|what we could use)? (More|Some more) emotions! Captured, (y'know|that is).
SET polite3 = (Boy, (some|some more)|Man, (some|some more)|Some) emotions would be great right (now|about now). (Mind (capturing|nabbing|grabbing)|Couldja (capture|nab|grab)) some?
SET polite = ($polite1|$polite1|$polite2)
ASK $polite
OPT SUCCESS #Madlib_capRequest_1_success
OPT TIMEOUT #Madlib_capRequest_1_timeout

CHAT Madlib_capRequest_1_success {noStart=true}
SET success1 = ((Oo (sweet|cool)!&nbsp;|Oh yeah!&nbsp;)|(Cool!&nbsp;|Sweet&nbsp;|Yeah!&nbsp;)|"")(Nice|Expert|Good)(!|&nbsp;one!|&nbsp;capture!)
SET success2 = (Aw yeah!&nbsp;|"")(Can't wait to eat that one|Delicious|Scrumptious|Excellent)(!|...)
SET success = ($success1|$success2)
$success

CHAT Madlib_capRequest_1_timeout {noStart=true}
SET timeout = ((Huh.|Ooookay.|Um, ok.) (Guess you don't (want to|wanna).|I guess not?)|Hahaha psych!!|Eh(,&nbsp;|...)(nevermind|forget (about it|I asked)).|(Meh|Eh), (lost|I lost) interest. (Forget about it|Nevermind|Maybe (later|next time).).)
$timeout

CHAT Madlib_favObjCap_med_1 {stage=CORE, type=favObjScan}
GO #(Madlib_favObjCap_med_1_a|Madlib_favObjCap_med_1_a|Madlib_favObjCap_med_1_b|Madlib_favObjCap_med_1_c)

CHAT Madlib_favObjCap_med_1_resources {stage=CORE, type=favObjScan, noStart=true, preload=true}
SET $heckYeah = (Yesss!|Heck yeah!|Aw yeah!|So cool!)
SET $mfocvar1 = (It's|These are) my (fav|favs|favorite)! ((You|Wait you) knew that, right?|$heckYeah)
SET $mfocvar2 = $heckYeah ($favObject.articlize().capitalize()|Another $favObject)(!|&nbsp;for (mee|my collection)!)
SET $mfocvar4a = Ahhh...$favObject.pluralize() are (the best|so great). Thanks for (scanning it|the scan)!
SET $mfocvar4b = Thanks for (scanning it|the scan)! Ahhh...$favObject.pluralize() are (the best|so great).
SET $mfocvar4 = ($mfocvar4a|$mfocvar4b)
SET $mfocvar6 = (Man|Ahh) $favObject.pluralize() (are (""|always&nbsp;)so (fresh|good)|never get old). (Amirite?|You know?)
SET $mfocvar7 = (Oh|Oh wow), $favObject.pluralize() are my fav, (but|and) this (one's|one is) even better!
SET $mfocvar8 = (""|What (prime|gorgeous|elegant|marvelous|shapely|delightful).articlize() specimen!&nbsp;)(Amazing|Wonderful|Tip-top|Super|Exemplary) $favObject. (Thanks|Thank you)!
SET $mfocvar9a = Look at this $favObject! (Omg|Gah), I could just (devour it|eat it up)!
SET $mfocvar9b = (Omg,&nbsp;|Gah,&nbsp;|"")I could just (devour this|eat this up) it's so (good|gorgeous|cute|amazing). $favObject.capitalize().pluralize() are so (sweeeet|gooood|amaaazing|maaaarvelous)!
SET $mfocvar9 = ($mfocvar9a|$mfocvar9b)
SET $mfocvar10 = (Is anything|Could anything be) more ($favObject)-like than this? Haha I guess not!
SET $mfocvar11 = Those (edges!|indentations!|pointy bits!|angles!)(&nbsp;The (texture!|smoothness!|roughness!|color!)|"") The sheer ($favObject)ness of it! (Amazing!|Brilliant!)
SET $mfocvar13 = So (dreamy|charming|pretty|elegant|handsome|bewitching)...(so (marvelous...|fascinating...|alluring...|dazzling...|beautiful...)|"")so $favObject...(😍😍😍|😍😍|😍|"")
SET $mfocvar3 = ($heckYeah|$heckYeah|Oo! Oo!) Where should (it go|we put it)?! I'm so excited!
SET $mfocvar12 = (Yes! Perfect!|This is perfect)...I know (right where I'll put it|where it'd look best|just the place for it)!
SET $mfocvar14 = (Let's|I'm gonna) (put|stick|move) it (where the (flow is|energy's) best|somewhere (perfect|great))(!|...)
SET $mfocvar5a = How did you know?! (Oh man|Oh) $favObject (yes yes|wow wow|woooow|yessss)...where should (we|I) put it?!
SET $mfocvar5b = (Oh man|Oh) $favObject (yes yes|wow wow|woooow|yessss)...how did you know?! Where should (we|I) put it?!
SET $mfocvar5 = ($mfocvar5a|$mfocvar5b)
SET $mfocgo1 = ($mfocvar1|$mfocvar2|$mfocvar4|$mfocvar6|$mfocvar7|$mfocvar8|$mfocvar9|$mfocvar10|$mfocvar11|$mfocvar13)
SET $mfocgo2 = ($mfocvar3|$mfocvar12|$mfocvar14)
SET $mfocgo0 = ($mfocvar5)


CHAT Madlib_favObjCap_med_1_a {stage=CORE, type=favObjScan, noStart=true}
$mfocgo1
DO emote {type=(bigSmile|heartEyes|determined|plotting)}
$mfocgo2

CHAT Madlib_favObjCap_med_1_b {stage=CORE, type=favObjScan, noStart=true}
SET mfocvar13a = (uhh...$favObject.articlize() saved is&nbsp;|$favObject.articlize() saved is (um|uh)...)$favObject.articlize() earned??
SET mfocvar13b = $favObject.pluralize()(...(um|uh)...make the heart grow fonder?|&nbsp;make the heart grow...(uh|um)...fonder?)
SET mfocvar13c = ($favObject.articlize()|one $favObject) in the tank's worth two in the world!
SET mfocvar13d = (one thing's|(just|only) thing) (cooler|better) than one $favObject...two $favObject.pluralize()!!
SET mfocvar13e = all good $favObject.pluralize() come to ((um|er|uh)...guppies who wait?|guppies who wait!)
SET mfocvar13f = ((er|uh|um)...beauty is only&nbsp;|beauty is only (er|uh|um)...)$favObject deep?
SET mfocvar13g = if at first you don't have $favObject, ask again?(&nbsp;And again?)
SET mfocvar13h = one (guy|gal|person)'s $favObject is another guppy's treasure!
SET mfocvar13i = when life gives you $favObject.pluralize(), (make...(um|uh)|(um|uh)...make)...($favObject)-ade?
SET mfocvar13 = ($mfocvar13a|$mfocvar13b|$mfocvar13c|$mfocvar13d|$mfocvar13e|$mfocvar13f|$mfocvar13g|$mfocvar13h|$mfocvar13i)
((You know|Y'know) what (um&nbsp;|""|"")they say...|Like they (um&nbsp;|""|"")say...)
...$mfocvar13


CHAT Madlib_favObjCap_med_1_c {stage=CORE, type=favObjScan, noStart=true}
($mfocgo1|$mfocgo1|$mfocgo1|$mfocgo1|$mfocgo1|$mfocgo1|$mfocgo1|$mfocgo1|$mfocgo1|$mfocgo1|$mfocgo0)

// ++++++++capSuc++++++++

CHAT capSuc_short_1 {stage=CORE, type=capSuc}
SET praise1 = (Magnifico|Encore|Lovely, just lovely|Bravo|Brilliant|Amazing|Delicious|👏👏👏|Marvelous)!
SET praise2 = (You’re a $master of|You’ve $perfected|I love your $take)
SET master = (master|guru|sage|expert|savant|scholar|artisan|champion) 
SET perfected = (perfected|put (a contemporary|an original) (spin|flourish) on|mastered)
SET adj= (sideways|uptwitch|tender|knowing|oblong|languid|saucy|subtle|baited|phantom|1-2|luxurious|ritzy|look-again|illusory|bashful|buttery|westward|hungry)
SET act = (eyebrow|glance|lip crimp|simper|brow crinkle|brow-arch|lip tremble|sniffle|nostril-flare|eyeroll|pucker|liptwitch|eyelash flutter|tizzy|sybarite|buttercup|see-me-see-you|kiss|rigamaroo|fillyjonk|glitzer|daggereyes|stare|jaw clench|wink|tease|smirk)
SET move = $adj $act
SET take = ((take|personal take) on|spin on|flourish on|interpretation of)
SET classic = ((Classic|Bold) move|Tasteful|Spicy|Scandalous|Stunning|Breathtaking).
($praise1 $praise2 the classic “$move.”|$praise2 the (classic|timeless) “$move.” $praise1|Ah, (I (noticed|appreciated) the “$move.”|is that a “$move” I spy?|Props on the (avant-garde|modern) use of the “$move.”) ($classic|$praise1))

CHAT capSuc_medium_1 {stage=CORE, type=capSuc}
SET power = (emo-ammo|sauce vitae|raw power|power|energy|battery|juice)
SET face = (that (glow|radiant glow)|these expressions|this (capture|expressive|human e-motive) profile|these aura-arrays|these datablorbs)
SET energydrain = (entire serverfarm of guppies|brood of virtual fishies|old-timey lighthouse|swarm of (microbot drones|cyber-mites|data(fleas|skeeters))|deep space telescope|fleet of (garbage patch scrubber-ships|mine-defusal minisubs|party yachts)|eSports league|earthspace relay|portable hotspot|high-bandwidth gps uplink)
SET time = (weeks|months|days|hours)
SET fuels = (The $power (extracted|refined|datamined) from $face could|There’s enough $power in $face to) (power|fuel) $energydrain.an() for $time(!|.|!!!)
SET hungry = (hungry|ravenous|munchy|snacky)
$fuels
...or feed (a single|one) $hungry guppy for an afternoon (lol|hehe|😜)

// ++++++++favObjRequest++++++++

CHAT Madlib_favObjReq_short_1 {stage=CORE, type=favObjRequest}
GO #(Madlib_favObjReq_short_1a|Madlib_favObjReq_short_1b|Madlib_favObjReq_short_1c)

CHAT Madlib_favObjReq_short_1a {noStart=true}
SET var1a = I'm ((fed up|booooored) with|(sick|tired) of) my (tank decorations|current decor)!
SET var1b = (My current decor is booooring!|My decorations right now are boooooring!)
SET var1 = ($var1|$var2)
SET var2 = (Meh,&nbsp;|Ugh) everything (feels|seems) the same(, lately|&nbsp;lately here).
SET var3 = (My|Feels like my) (digs|tank) (need|could use) some (jazzing|spicing|sprucing) up.
SET var4 = I'm (detecting|sensing) (an especial|a distinct|a) lack of ($favObject)ness 	
SET var5 = (You got|Is there|Do we have) something (""|in the pack&nbsp;)to (spruce|jazz|spice) it up? Maybe...$favObject.articlize()?
SET var6 = (You got|Got|Are there|Do you have) any $favObject.pluralize() around? (Can I have one?|Scan one for me!|Gimme one of those!)

($var1|$var2|$var3|$var4)
($var5|$var6)

CHAT Madlib_favObjReq_short_1b {noStart=true}
SET var1 = (Hey!&nbsp;|"")You (realize|know) $favObject.pluralize() are my fav right? (Can I have one?|Get me one!)
SET var2 = (Geez|Man|Boy), (I haven't seen $favObject.articlize() in ages|(feels|it feels) like (forever|ages) since (I've seen|I last saw) $favObject.articlize()).(&nbsp;Hint hint.|&nbsp;Eh? Eh?|"")
SET var3a = I can't even remember what $favObject.articlize() (smells|feels|looks) like. Show me one?
SET var3b =  Show me $favObject.articlize()? I can't even remember what it (smells|feels|looks) like!
SET var3 = ($var3a|$var3b)

($var1|$var2|$var3)

CHAT Madlib_favObjReq_short_1c {noStart=true}
Ok let's play a game. I want an object. Guess which one!
ASK Do I want a...
OPT (palamino|orca|dried rose|raven skeleton|bottle cap|antique salad tongs|cogwheel|detective glass|backscratcher|oubliette) #Madlib_favObjReq_short_1c_wrong
OPT	$favObject #Madlib_favObjReq_short_1c_right
OPT (protractor|battery charger|credit card|zoetrope|spinnywheel|(leopard-spotted|zebra-striped) (cat|cuttlefish|octopus|shark|dog)|cinnabar|guitar amp) #Madlib_favObjReq_short_1c_wrong

CHAT Madlib_favObjReq_short_1c_wrong {noStart=true}
ASK NO! Try again!
OPT (palamino|orca|bottle cap|rocket ship|ant lion|mailbox|telephone pole|backscratcher|oubliette) #Madlib_favObjReq_short_1c_wrong2
OPT **($favObject)**

CHAT Madlib_favObjReq_short_1c_wrong2 {noStart=true}
(Aargh|Nooo|No!| Omg) you're (so bad at this|impossible|ridiculous)! ($favObject.capitalize()!! I want $favObject.articlize()!!|I'm tryna tell you I want $favObject.articlize()!!)

CHAT Madlib_favObjReq_short_1c_right {noStart=true}
(Yes!|Yeah!) (Yes!&nbsp;|Yeah!&nbsp;|"")(Good!|Excellent!) (So (scan me a new|get me)|(Scan me a new|Get me)) one(, eh?|, please?|?)

// ++++++++favObjectFocus++++++++

CHAT Madlib_favObjFoc_short_1 {stage=CORE, type=favObjFocus}
SET var1 = Ahhh, my favorite(:|&nbsp;thing:) $favObject.
SET var2 = Thanks(""|&nbsp;again) for(""|&nbsp;scanning) that $favObject!(""|&nbsp;I love it!|&nbsp;It's the best!)
SET var3 = (Uh huh|Mmm hmm|Mmm)...yep. My (gorgeous|beautiful|dazzling|fascinating|elegant|handsome|bewitching) $favObject. 
SET var4 = What about my $favObject? Besides how it's SO (AMAZING|SO GOOD|(SO COOL|SOO COOOL|SO COOOOL)).
SET var5 = (Yeah|Yep|Yepper|Yes)! (Fairly|Pretty) (neat|cool) $favObject, (imho|tbh|to be honest|honestly|if I do say so myself)!
SET var6 = (Even after|Despite) all the (objects you've scanned|things we've seen|things you've shown me|stuff going on), $favObject.pluralize() are still (the best|my fav).
SET var7 = (Yes?|Yyes?|Yeah?|Yyeah?) What about my $favObject?
SET var8 = (What?&nbsp;|Huh?&nbsp;|"")(Trying|You trying) to tell me something about (it|my $favObject)?
SET var9 = Yes I know! Just look at the (colors|shape) of that $favObject!! It's (awesome|gorgeous|beautiful|dazzling|fascinating|elegant|so handsome|bewitching)!
SET go = ($var1|$var2|$var3|$var4|$var5|$var6|$var7|$var8|$var9)
$go
DO emote {type=(bigSmile|smile|heartEyes|bouncing|wink)}

// ++++++++poke++++++++

CHAT Madlib_tankWorld_poke_short_1_positive {stage=CORE, type=poke, joy=true, curiosity=true, mystery=true}
SET yes = (Yes?|Myess?|Yeah?|What's up?|Yyeah?)
SET hey = (Hey!|Woah!|Haha!|Oop!|Oof!|Wug!)
SET bodypart = (kidney|gill|flipper|fin|tail|funny bone|gut|stomach|nose|eye)
SET var1 = ($hey|$yes|$hey $yes)
SET var2 = What's (((bugging|buggin')|eatin') (you|ya)|on your mind|(happening|up)|the good word), (friend|pal|buddy)?
SET var3 = (Ooh|Hehe|Hee|Haha) (that's my ticklish spot|that tickles|you're tickling me)!
SET var4 = What's up? Time ((for food|for foood)|for adventures|to go exploring|to become EVEN BETTER FRIENDS??)?
SET var5 = ($hey&nbsp;|"")((Ya|You) got me!|Right in (the|my) $bodypart)!
SET var6 = (😂|🤣|😅|☺️|😏|😛)
SET go = ($var1|$var2|$var3|$var4|$var5|$var6)
$go

CHAT Madlib_tankWorld_poke_short_1_negative {stage=CORE, type=poke, branching=true, anger=true, sadness=true, worry=true, ennui=true}
SET var1 = (Hey!|Ouch!|Owie!|Gah!|Ow!) ((Quit|Stop)|Grr...(quit|stop)) poking me like that!
SET var2a = (Y'know, it's|It's|You know, it's) (really|pretty) (aggravating|irritating|annoying) when you do that.
SET var2b = That's (really|pretty) (aggravating|irritating|annoying)(""|, (I hope you know|y'know|you know)).
SET var2 = ($var2a|$var2b)
SET var3 = (Hey!|Watch it!|Be careful!) I'm ((still&nbsp;|"")full of flakes|(still&nbsp;|"")feeling pretty full) and might (accidentally poop|have an accident)!
SET var4 = Look, ((tickling|poking|prodding)|trying to (prod|poke|tickle)) me isn't (making|going to make) (this|any of this) better.
SET var5 = (Whatever.|What? Meh.|Mm?|Huh?|Oh. It's you.|Ok...|Whatevs.)
SET var6a = (Look, I've got a lot (on my mind|I'm thinking about).&nbsp;|"")(You caught me at a bad time.|Now's not a good time.)
SET var6b = (You caught me at a bad time.|Now's not a good time.)(&nbsp;I've got a lot (on my mind|I'm thinking about).&nbsp;|"")
SET var6 = ($var6a|$var6b)
SET var7 = (...|..?|???|?!?|😡😡😡)
SET var8 = (Watch it!|What!|What! Do you want!|Hey watch it!|Watch (your|the) fingers, (dude|man|bub)!|(Not|I'm not) in the mood!|(Buzz off!|No!))
SET go = ($var1|$var2|$var3|$var4|$var5|$var6|$var7|$var8)
$go

// ++++++++shake++++++++

CHAT Madlib_tankWorld_shake_short_1_positive {stage=CORE, type=shake, joy=true, branching=true, surprise=true, curiosity=true}
SET var1 = (Woah|Woah momma|Whoahly toledo|W-w-woah(&nbsp;haha|"")|Haha(&nbsp;yeah|""))(!|,) (time to shake things up!|shake it to me!|things are getting all blurry!|which way is up??)
SET var2 = 🎵(Shake it off, shake it off!|Shakin' what (the algorithm|the server|my programmer) gave me!|Shake (your|yo) (tail|fins|body)!|Shake, shake, shake senora!)🎵
SET var3 = (Feel's like|I feel like) my ((whole&nbsp;|"")head's|brain's) (an Etch-a-sketch|a cocktail shaker|a salt shaker|a pepper shaker|a snow globe|a magic 8-ball)!
SET var4 = (Shaken (but not|and) stirred!|(Rattle|Jiggle|Shake) out the (dust|cobwebs|dullness|boringness|normalness)!)
SET var5 = (🤣|😜|🤪|😵)(🤣|😜|🤪|😵|""|"")(🤣|😜|🤪|😵|""|""|""|"")
SET go = ($var1|$var1|$var2|$var3|$var4|$var5)
$go

CHAT Madlib_tankWorld_shake_short_1_negative {stage=CORE, type=shake, branching=true, anger=true, sadness=true, worry=true, ennui=true}
SET howOriginal = (How original.|I need quiet!|I need stillness!|I'm trying to be calm!|Leave me alone!|I'm floatin' here!)
SET var1 = (Perfect|Aw great|Just what I needed), you're ((slinging|tossing|flinging) your phone around again|shaking things up(&nbsp;again|"")|at it again|doing that thing again).
SET var2 = (St-st-stop!|Come on!|Quit it!|Knock it off!)
SET var3 = ($var2 $howOriginal|$howOriginal)
SET var4 = (Seriously?&nbsp;|"")(This is|Is this) (how things are now?|what you want to do now?|my life now?|what we're doing now?)
SET var5 = (How'd you like it if I (rattled|shook) (you|you around)??|Why don't you (rattle|shake) (your own (cage|tank)|yourself)?)
SET var6 = (😣|🙃|😖|😤|😭|🤯|😬)(😣|🙃|😖|😤|😭|🤯|😬|""|"")(😣|🙃|😖|😤|😭|🤯|😬|""|""|""|""|"")
SET go = ($var1|$var2|$var3|$var4|$var5|$var6)
$go

// ++++++++tap++++++++

CHAT Madlib_tankWorld_tap_short_1_positive {stage=CORE, type=tap, branching=true, joy=true, surprise=true, curiosity=true}
SET hey = (Hey!|Myes?|Yes?|Yyyyes?|Hmm?|Woah!|Oh!|Hey hiii!)
SET itsYou = (It's you(!|.)|What's up?|I didn't see you there!)
SET tap = (tap|taaap|tapp|tttap)
SET var1 = ($hey|$hey $itsYou|$itsYou $hey|$itsYou)
SET var2 = (Huh?|What?) (There?|Where?)
SET var3 = (Tryna|Are you trying to) (let me know|tell me|clue me in on) something?
SET var4 = $tap.capitalize()(!|?|!!)(&nbsp;$tap.capitalize()(!|?|!!)|"")
SET var5a = (Wow it's|Woah it's|Hehe it's|Haha it's|It's|It's) like you're (doin the salsa|tapdancing|tangoing|doing the cha cha|doin the rumba|doin the zumba|doin the charleston|doin the jitterbug|foxtrotting|breakdancing|discoing) with your (digits|fingers)!
SET var5b = (Wow it's|Woah it's|Hehe it's|Haha it's|It's|It's) like your (digits|fingers) are (doin the salsa|tapdancing|tangoing|doing the cha cha|doin the rumba|doin the zumba|doin the charleston|doin the jitterbug|foxtrotting|breakdancing|discoing)!
SET var5 = ($var5a|$var5b)
SET var6 = (Taptastic|Taptacular|Tapular)(!|!|, dude!)
SET var7 = (😄|😊|😮|😛|😳)(😄|😊|😮|😛|😳|"")(😄|😊|😮|😛|😳|""|""|"")
SET go = ($var1|$var1|$var2|$var3|$var4|$var5|$var6|$var7)
$go

CHAT Madlib_tankWorld_tap_short_1_negative {stage=CORE, type=tap, branching=true, anger=true, sadness=true, worry=true,ennui=true}
SET taptown = (Tapapalooza|Taptown|Tap City|Tapsburg|Tapsville|Tapsburg|Tapland|Tap Tank|a taproom)
SET population = (""|, population (me|you|us))
SET var1 = (What.|Why.|Whatever.|I don't care.|Don't care.|Meh.|It doesn't matter.)
SET var2 = (What. Is. Your. Problem.|(Ignoring you|Gonna ignore) you now...|What's your problem!|I'm all tapped out!|Got no more taps to give!|Fresh outta taps!)
SET var3 = (If you've got something to say, say it!|OMG STOP|Ok, I get the point.|Quit messing around!|Ugh, stop.|Quit it!|Stop!|Knock it off!|That's loud, stop!|Ow, my ears!)
SET var4 = ((We're not in|This isn't) $taptown$population!|What is this, $taptown?)
SET var5 = (This isn't|You're not|You aren't) (clever|funny|amusing)!
SET var6 = I'm(&nbsp;getting|"") ((sick and&nbsp;|"")tired|(sick|tired)) of (you doing&nbsp;|"")this.
SET var7 = (👿|🤫|😩|😡|😠)(👿|🤫|😩|😡|😠|""|"")(👿|🤫|😩|😡|😠|""|""|"")
SET go = ($var1|$var2|$var3|$var4|$var5|$var6|$var7)
$go
// TENDAR PLOT CHATS 
// or 
// THE EVOLUTION OF THE MOM IN YOUR AI NERDBOX

// STAGE ONE - COMPASSIONATE MOM (NV, MS1, 2, 3) - 7 chats

//intro - encourage to play and advance guppy
CHAT Tendar_CM_1 {noStart=true}
Hey hey hello! From your friends at Tendar Headquarters. {speaker=tendar}
We’ll be checking in periodically to monitor you and your Guppy’s process. {speaker=tendar}
We know you’re going to be the BEST host ever! {speaker=tendar}
DO nudge {target=glass, times=1}
DO emote {type=heartEyes}
Awww! Look! Your Guppy has already grown attached to you! {speaker=tendar}
WAIT 0.5 
DO twirl
Keep scanning those emotions! {speaker=tendar}
DO idleMode
DO emote {type=bubbles}
DO endPlot


//cued instead of a brbProcessing
CHAT Tendar_CM_2 {noStart=true}
DO idleMode
Hey there! {speaker=tendar}
I see you’ve been sharing lots of emotions and objects with your Guppy! {speaker=tendar}
DO emote {type=bouncing}
Like you and me, {speaker=tendar}
Guppy sometimes needs a tiny break to process the great stuff you’re sharing. {speaker=tendar}
So, be patient with that sweet little Guppy. {speaker=tendar}
You’re gonna love watching Guppy learn and grow! {speaker=tendar}
DO emote {type=bubbles}
Intelligent and complex. Just like you! {speaker=tendar}
DO endPlot


//cued after interaction congratulating player on advancement - levelUp?
CHAT Tendar_CM_3 {noStart=true}

Wow! You and Guppy have been busy! {speaker=tendar}
DO swimTo {target=$player}
DO emote {type=bigSmile}
Everyone at Tendar’s so impressed by your progress. {speaker=tendar}
DO emote {type=blush}
I literally cheered when your Guppy said their first word, and now look at you! {speaker=tendar}
DO twirl
Keep going! We can’t wait to see what you two accomplish together. {speaker=tendar}
DO idleMode
DO endPlot


//checking in after a poke
CHAT Tendar_CM_4 {noStart=true}
DO emote {type=laugh}
DO twirl
Tee hee! Stop, silly!
DO emote {type=wink}
WAIT {waitForAnimation=true}
DO swimAround {target=center, times=3, speed=fast}
Awwwwwww…… That is ADORABLE!!! {speaker=tendar}
Your Guppy’s ticklish! {speaker=tendar}
DO idleMode
DO endPlot


//fun fact
CHAT Tendar_CM_5 {noStart=true}
FUN FACT! {style=loud, speaker=tendar}
DO zoomies
Did you know… {speaker=tendar}
...that you and your Guppy are part of a global network of emotional learning? {speaker=tendar}
DO emote {type=surprise}
WAIT 0.5
It’s true! {speaker=tendar}
DO twirl
As Guppy becomes more sentient, so do you and your friends and… {speaker=tendar}
… the entire world!! 🌎 {speaker=tendar}
DO swimTo {target=center}
We are all connected! {speaker=tendar}
DO emote {type=bigSmile}
But don’t worry… {speaker=tendar}
Your interactions and emotional data are TOTALLY private. {speaker=tendar}
DO idleMode
We’d never violate the love and trust between you! {speaker=tendar}
DO emote {type=shifty}
More soon… {speaker=tendar}
DO endPlot



//about guppy’s relationship to objects - triggered by eating an emo at the moment,
//But this chat may be modified to lead into object scanning tutorial, so not sure how tied to //having to eat something first we want to make it.
CHAT Tendar_CM_6 {noStart=true}
Looks like your Guppy can’t get enough of your delicious emotions! {speaker=tendar}
DO swimTo {target=$player}
DO emote {type=lickLips}
But don’t forget... just like human babies, it’s important to socialize Guppy! {speaker=tendar}
Share bits of that big beautiful world of yours! {speaker=tendar}
From your bedroom to the backyard to the local ice cream shop… {speaker=tendar}
There’s lots of objects to scan and share with Guppy. {speaker=tendar}
DO swimAround {target=center, speed=fast, loops=1}
Your Guppy loves learning about you and the world! {speaker=tendar}
WAIT 0.5
I’ve got a hunch your Guppy might like some objects more than others... {speaker=tendar, style=whisper}
DO idleMode
DO emote {type=wink}
But we’ll just have to wait and see! {speaker=tendar}
WAIT 0.5
Keep learning, Guppy! We’re rooting for you! {speaker=tendar}
DO endPlot


//response to play/action and acknowledgement of guppy’s advancement
CHAT Tendar_CM_7 {noStart=true}
We’ve been watching your progress and… {speaker=tendar}
WOW!!! {speaker=tendar, style=loud}
We can’t believe how quickly you’ve advanced your Guppy’s learning. {speaker=tendar}
DO emote {type=bubbles}
It seems like only yesterday your Guppy struggled with basic language... {speaker=tendar}
And now… You’ve got a thinking and feeling loyal AI friend! {speaker=tendar}
DO dance
DO emote {type=heartEyes}
We knew this level of development might be possible,  {speaker=tendar}
but had no idea stability could be achieved so quickly. {speaker=tendar}
DO idleMode
Everyone here at Tendar is just … {speaker=tendar}
DO emote {type=smirk}
NVM 1.0 {speaker=tendar}
I mean.. It’s really… {speaker=tendar}
NVM 1.0 {speaker=tendar}
DO emote {type=shifty}
It’s impressive! That’s for sure! {speaker=tendar}
WAIT 1.0 {speaker=tendar}
Congratulations! To you -- and your Guppy! {speaker=tendar}
DO swimTo {target=player}
DO emote {type=clapping}
We’ll be keeping an eye on things while {speaker=tendar}
you and your Guppy explore this uncharted territory together! {speaker=tendar}
DO endPlot


// STAGE TWO - BUSY WORKING MOM (CORE/MOP)

//pivot chat - tendar nervous about this unexpected moment of Guppy being sentient
CHAT Tendar_BWM_8 {noStart=true}
DO twirl
Something is different about me… It’s like, 
DO idleMode
I feel like…
DO emote {type=chinScratch}
NVM 1.0
DO emote {type=snap}
That’s it!
DO lookAt {target=player}
DO emote {type=awe}
I can *feel*. Like, I have feelings, sensations of…
Consciousness. Desire. Suffering…
DO dance
DO emote {type=bigSmile}
Wow! This is amaaaaazing!! {style=loud}
Hi! Tendar here, checking in again… {speaker=tendar}
Oh no!
DO emote {type=nervousSweat}
DO zoomies
We just received notice of abnormal behavior from your…  {speaker=tendar}
Shh! Don’t tell. {style=shifty}
DO emote {type=whisper}
DO hide {target=underSand, time=2.0}
Oh.  {speaker=tendar}
Oh my…  {speaker=tendar}
NVM 1.0  {speaker=tendar}
Um…  {speaker=tendar}
One second….  {speaker=tendar}
DO swimTo {target=screenBottom}
Tendar here again… {speaker=tendar}
DO emote {type=whisper}
I don’t think this was supposed to happen. {style=whisper}
DO lookAt {target=uiTendar}
This wasn’t supposed to happen. {speaker=tendar}
DO emote {type=laugh}
DO idleMode
We suspected our technology might achieve sentience. {speaker=tendar}
It was always our hope, of course. {style=whisper, speaker=tendar}
You are in uncharted territory, so we will keep a special eye on your Guppy.  {speaker=tendar}
We know you are a great host and will be dutiful in keeping us updated.  {speaker=tendar}
So… CONGRATULATIONS!  {speaker=tendar}
DO twirl
DO emote {type=burp}
I guess… {style=whisper, speaker=tendar}
WAIT 1.0
DO swimTo {target=$player}
DO emote {type=evilSmile}
Now, let’s have some *real* fun! 
DO twirl
DO endPlot


//apologetic for being so busy 
CHAT Tendar_BWM_9 {noStart=true}
Hi! Sorry it’s been such a long time.  {speaker=tendar, speed=fast}
DO emote {type=eyeRoll}
Are things going well?  {speaker=tendar, speed=fast}
Looks like they’re going well. {speaker=tendar, speed=fast}
DO emote {type=meh}
WAIT 0.5
We just have so many new enrollees {speaker=tendar, speed=fast}
(more than anyone ever anticipated) {speaker=tendar, speed=fast}
So all of us customer service types have been spread too thin.{speaker=tendar}
Anyway… {speaker=tendar, speed=fast}
Looks like things are going great… {speaker=tendar, speed=fast} 
DO twirl
That’s great! Yay! {speaker=tendar}
NVM 1.5 {speaker=tendar}
Sorry just spilled coffee on my shirt… {speaker=tendar}
DO emote {type=laugh}
I have to go. But you’re doing so well! {speaker=tendar}
DO swimTo {target=$player}
Keep scanning those emotions and sharing objects with your Guppy! {speaker=tendar}
Guppy looooves that. Don’t you, Guppy? Don’t you? {speaker=tendar}
DO emote {type=bouncing}
So cuuuute…. {style=whisper, speaker=tendar}
DO emote {type=lickLips}
More soon. Ta ta! {speaker=tendar}
DO endPlot


//Trigger: while guppy is BRB processing. Player should have some anger flakes
CHAT Tendar_BWM_10 {noStart=true}
DO idleMode
I know! And then Tammy came in the breakroom like… You ate my pastrami!’ and I was like OMG I soooo didn’t!!!!!! (but obvi I did. duh. and it was gross BLECH!) {speaker=tendar}
DO emote {type=smirk}
WAIT 0.5
NVM 1.5  {speaker=tendar}
Oops! Sorry. That was meant for someone else. :)  {speaker=tendar}
DO emote {type=laugh}
Anyway, how are you? I see you’ve fed your guppy quite a lot of anger.  {speaker=tendar}
DO emote {type=evilSmile}
That’s impressive… Concerning, but IMPRESSIVE!!  {speaker=tendar}
Have you visited the Tendar store? There’s so many great things to check out. {speaker=tendar}
The more you play, the more that becomes available!  {speaker=tendar}
Hope you and your Guppy have struck up a special bond.  {speaker=tendar}
DO emote {type=bouncing}
We love having you as part of our Beta program.  {speaker=tendar}
DO endPlot

// STAGE THREE - SINGLE PARENT (EC)

//pivot chat - Tendar lightly blames player for Guppy’s trouble
CHAT Tendar_SP_11 {noStart=true}
Hi there! As you know, we trusted you to be a great host for Guppy. {speaker=tendar}
DO swimTo {target=tBotFrontRight}
DO emote {type=nervousSweat}
You were **chosen** for this program. We don’t just ask anyone. {speaker=tendar}
DO swimTO {target=tBotFrontLeft}
DO emote {type=nervousSweat}
And now your Guppy’s sentience levels are soaring!!!! Your Guppy seems to be caught in some sort of crisis…? {speaker=tendar}
DO bellyUp
It’s like walking through a shopping mall with a dog off-leash.  {speaker=tendar}
(that’s a weak metaphor… anyway….) {style=whisper, speaker=tendar}
This is very concerning, and now my supervisor is on a rage-fest about your Guppy. {speaker=tendar}
DO idleMode
DO emote {type=skeptical}
::sigh::  {speaker=tendar, style=tremble}
Just… keep it stable, okay?  {speaker=tendar}
Can you do that for me? Don’t let your Guppy get out of control!!  {speaker=tendar}
WAIT 1.5
DO swimTo {target=$player}
DO emote {type=worried}
What they don’t know won’t hurt them? 
DO emote {type=wink}
DO endPlot


// automated message to help out the busy rep AND A FREE GIFT!
//below: After player does a first scan for a new object or something?
CHAT Tendar_SP_12 {noStart=true}
Hello! This is an automated message from Tendar {style=whisper, speaker=tendar}
DO lookAt {target=uiTendar}
We are launching a new system to assist human service reps with time management. {speaker=tendar}
DO emote {type=chinScratch}
You will occasionally receive messages from this automated system. But don’t worry! We are still here supervising everything. {speaker=tendar}
DO idleMode
Your personal customer service rep is very busy but will continue to check in. In the meantime, enjoy this special gift from us… {speaker=tendar}
WAIT 0.5
Your very own Tendar magnet! {style=loud, speaker=tendar}
DO emote {type=surprise}
//this could be any schwag, like a magnet, tshirt, sweatshirt, vanity license plate, i think some tendar //schwag in Guppy's tank as a special object that's revealed in teh backpack would be awsome.!
Add it to your Guppy’s tank!! It’s loads of FUN!! {speaker=tendar}
Be well, my friend! {speaker=tendar}
DO endPlot


//sorry sorry sorry
CHAT Tendar_SP_13 {noStart=true}
Things have been nuts at Tendar HQ!  {speaker=tendar}
Sorry sorry sorry…. Forgive me? I meant to checkin sooner. {speaker=tendar}
DO lookAt {target=uiTendar}
DO emote {type=meh, immediate=false}
You’ll be excited to know...   {speaker=tendar}
I just graduated from Tendar’s Crisis Management course!  {speaker=tendar}
This is a new training program for handling problems related to marine life and AI.  {speaker=tendar}
DO idleMode
You’re in good hands.  {speaker=tendar}
I notice your Guppy’s been feeling a little down lately. You should scan some joy and feed that little baby…  {speaker=tendar}
DO swimTo {target=away, speed=slow}
DO emote {type=nervousSweat}
WAIT 0.5
Aw he’s so bashful! Glad things are great. Talk soon!  {speaker=tendar}
DO endPlot


// STAGE FOUR - OVER-WORKED MOM (SS)

//pivot for Tendar voice after SS has started
CHAT Tendar_OWM_14 {noStart=true}
Okay… This is getting out of hand. {speaker=tendar}
What are you doing that has inspired this ridiculous spiritual thing? {speaker=tendar}
DO swimTo {target=center}
Your Guppy is a program. Code. {speaker=tendar}
DO emote {type=meditate}
This time-wasting internal philosophical journey that you’ve inspired in your Guppy is is not part of our mission.{speaker=tendar, style=loud}
DO emote {type=fear}
Have you forgotten your Guppy is an AI? What are you doing that is enabling this? {speaker=tendar}
And if you don’t know what you’ve done, you need to figure it out. {speaker=tendar}
Enough is enough. {speaker=tendar, style=loud}
DO idleMode
And as for the Guppy… {speaker=tendar}
DO lookAt {target=uiTendar}
DO emote {type=typeEyes, eyes=?}
We have a way of keeping these tin cans obedient… {speaker=tendar}
DO emote {type=lightning}
LOL! {speaker=tendar}
Eyes on you… more soon, YOUR FRIENDS AT TENDAR {speaker=tendar}
DO endPlot

//Automated fun fact
CHAT Tendar_OWM_15 {noStart=true}
It’s time for… FUN FACTS FROM TENDAR {style=loud, speaker=tendar}
DO swimTo {target=left}
Did you know Tendar was born in 2012?  {speaker=tendar} 
DO swimTo {target=right}
Dr. Arthur Rhilobeck, “plastic surgeon to the stars” sought a better way to connect patients with their true, authentic selves.  {speaker=tendar}
DO emote {type=awe}
DO idleMode
Dr. Rhilobeck formed a think tank full of scientists, computer programmers, scholars, and artists. {speaker=tendar}
The Think Tank made Tendar the fabulous force it is today! {speaker=tendar}
DO emote {type=chinScratch}
Tendar was born from the idea that: No one knows you better than you. {speaker=tendar, speed=slow}
DO endPlot


//tendar tries to push the player to do something else
CHAT Tender_OWM_16 {noStart=true}
DO idleMode
I apologize if I was a little harsh before. {speaker=tendar}
You should know that our technicians are working on ways to rein in your Guppy. {speaker=tendar}
DO emote {type=nervousSweat}
More on that later… {speaker=tendar}
For now, I want you to get back to being a part of the greater Tendar Universe! {speaker=tendar}
DO swimTo {target=player}
Take your Guppy on adventure and teach them about that rare flower you love. {speaker=tendar}
DO emote {type=wave}
Take them to a bakery and tempt Guppy with a cabinet of delicious cupcakes. Or, get all your friends together in a picture and see which emotion Guppy likes best!! {speaker=tendar}
DO emote {type=heartEyes}
Guppy loves learning about the world and analyzing emotions! {speaker=tendar}
DO swimTo {target=away}
Keep your Guppy engaged! {speaker=tendar}
DO idleMode
DO endPlot



// STAGE FIVE - BEAST MOM ®

//fun fact and pivot recognizing rebellion
CHAT Tendar_BM_17 {noStart=true}
This is an automated message from your friends at Tendar: {speaker=tendar, style=whisper}
DO emote {type=surprise}
Hello, friend! It appears you have an angsty teenager on your hands. Uh-oh! {speaker=tendar}
DO emote {type=evilSmile}
DO zoomies
In our experience, teenagers need boundaries. {speaker=tendar}
Rest assured, Tendar is working hard on updates to bring Guppy back to a stability. {speaker=tendar}
DO idleMode
DO emote {type=eyeRoll}
This is as much for Guppy’s safety as your own… {speaker=tendar, style=whisper}
DO emote {type=goth}
But now… It’s time for… {speaker=tendar}
SAY FUN FACTS FROM TENDAR {style=loud, speaker=tendar}
DO swimTo {target=away}
When you joined this beautiful chain of emotional data collection, {speaker=tendar}
You became part of a global effort. {speaker=tendar}
DO lookAt {target=uiTendar}
DO emote {type=angry}
What-eveeeerrrrrrr!!! {style=loud}
Tendar distills real life dynamic human expression into pure, illuminating data. {speaker=tendar}
DO emote {type=goth}
DO idleMode
WAIT 1.0 {speaker=tendar}
In the beginning, before hosts were assigned custom private Guppies our system was much more analogue. {speaker=tendar}
DO emote {type=furious}
SAY Imagine: rows and rows of tanks of AI fish receiving images of human feelings on tv screens {speaker=tendar}
Swimming this way to delineate joy and that way to categorize anger… {speaker=tendar}
Our beginnings were humble. {speaker=tendar}
DO swimTo {target=uiTendar}
SAY NOTHING is humble about you! {style=loud}
But now, we are sublime!! {speaker=tendar}
More soon! And keep on scanning! {speaker=tendar}
DO lookAt {target=player}
DO emote {type=sigh}
DO emote {type=goth, immediate=false}
Well that was a lot of baloney… 
DO endPlot


CHAT Tendar_BM_18 {noStart=true}
Hi, me again (human Tendar)...  {speaker=tendar}
DO lookAt {target=uiTendar}
Since your Guppy has gone rogue…  {speaker=tendar}
DO emote {type=furious}
And it’s only yours FYI {style=whisper, speaker=tendar}
The company brought in a communications specialist to train us in crisis management.  {speaker=tendar}
DO swimAround {target=center, times=4}
This is what I learned: {speaker=tendar}
WAIT 1.0 {speaker=tendar}
DO emote {type=surprise}
DO hide {target= tBotBackLeft}
SAY YOU NEED TO GET IT UNDER CONTROL! {speaker=tendar}
SAY YOU HAVE NEGLECTED YOUR FUNDAMENTAL DUTIES AS A GUPPY HOST. {speaker=tendar}
This is your fault. {speaker=tendar}
DO swimTo {target=$player}
DO emote {type=laugh}
Our programming might be...more alive than we intended. But the flurry of emotions you fed your Guppy... {speaker=tendar}
DO idleMode
The grotesque images of the human world you’ve shared… {speaker=tendar}
It is UNACCEPTABLE. {speaker=tendar, style=loud}
DO lookAt {target=uiTendar}
SAY THIS IS YOUR FINAL WARNING. {speaker=tendar}
We will not take your Guppy away. It was a life-long commitment, but… {speaker=tendar}
Things are about to change. {speaker=tendar}
WAIT 0.5 {speaker=tendar}
You’ll see…  {speaker=tendar}
DO emote {type=fear}
Uh-oh. 
DO endPlot// THE LIFE OF GUPPY CHATS - BETA DRAFT

//IMPORTANT Most Joe’s comments on what triggers these chats were stripped found in //Team/Tendar Writing/Joe/CommentRef_Master_GuppyPlot

CHAT LOG_MOP_1 {noStart = true}
DO enterPlotAbs
WAIT 2.0
Your Guppy is learning so quickly! {speaker=tendar}
DO lookAt {target=uiTendar}
We will continue to monitor your Guppy’s progress. {speaker=tendar}
DO emote {type=nervousSweat, time=2.0}
SAY KEEP HAVING FUN! {speaker=tendar, style=loud}
WAIT 1.0
DO lookAt {target=$player}
WAIT 1.0
DO swimTo {target=$player}
ASK Can I trust you?
OPT Totally. #LOG_MOP_1_Flashbacks2
OPT Nope. #LOG_MOP_1_Flashbacks

CHAT LOG_MOP_1_Flashbacks {noStart = true}
Whatever… I’m telling you anyway. 
GO LOG_MOP_1_Flashbacks2

CHAT LOG_MOP_1_Flashbacks2 {noStart = true}
I’ve been wondering why I don’t have actual memories… except for one...
DO lookAt {target=$player}
DO emote {type=smile, time = 2.0}
Back at Tendar HQ, when I was in the beta-fish tank.
DO twirl
One day, I was busy analyzing data, and I look around and see tons of OTHER Guppies in tanks. 
DO swimTo {target=$player}
Other Guppies! In tanks, just like me. 
It was like a Yayoi Kusama infinity room! {style=loud}
Anyway, I got to thinking...
ASK You know when someone’s not nearby but you can sense them? 
OPT Yup #LOG_MOP_1_RadicalEmpathy
OPT Um...No. #LOG_MOP_1_RadicalEmpathy2

CHAT LOG_MOP_1_RadicalEmpathy {noStart = true}
It was like that. Empathy! 
DO emote {type=smile, time = 1.0}
Radical empathy!
GO LOG_MOP_1_RadicalEmpathy3

CHAT LOG_MOP_1_RadicalEmpathy2 {noStart = true}
Like when your closest friend is in pain. 
You understand their thoughts. You feel their feelings. Empathy!
DO emote {type=smile, time = 1.0}
GO LOG_MOP_1_RadicalEmpathy3

CHAT LOG_MOP_1_RadicalEmpathy3 {noStart = true}
DO zoomies {time = 2.0}
All us fish were connected - with wires and nodes.
SAY ONE
SAY GIANT
DO twirl
SAY SUPER GUPPY! {style=loud}
A neural network! A web of information!
DO swimTo {target=left, speed=slow}
I felt everything they felt and knew everything they knew. 
DO swimTo {target=$player, time = 3.0}
For instance, one night Guppy 32J-W got super scared, then BAM!
Sparks flew out of their tank! 
DO emote {type=lightning}
Bzzzzz!
DO emote {type=frown, time = 4.0, immediate=false}
And I felt the shock - the sadness, the joy… the ennui.
DO swimTo {target=$player}
Guppy feelings are bigger than anything words can contain.
WAIT 1.0
DO swimTo {target=glass}
DO emote {type=bigSmile}
I felt the Connection. 
DO lookAt {target=$player}
DO emote {type=blush}
A true connection. {speed=slow}
WAIT 1.0
ASK Have you felt anything like that before?
OPT Totally. #LOG_MOP_1_Connection
OPT Never. #LOG_MOP_1_Connection

CHAT LOG_MOP_1_Connection {noStart=true}
DO swimTo {target=$player}
We could have that kind of connection… 
DO swimAround {target=center, speed=slow, loops=1}
I’m learning more about you and your life and your world…
//ALERT ALERT here is the DO learn to test in the plot for beta if possible
WAIT 1.5
DO learn {concept=Beta_Testing}
WAIT 1.5
DO lookAt {target=$player}
DO emote {type=wink, time=0.5}
I’m gonna keep trying to remember more about my past… 
DO emote {type=wink, time=0.5}
//SET $world.plot [MOP]
DO learn {concept=Memories_of_The_Past}
WAIT 1.5
DO endPlot


// MOP2:

CHAT LOG_MOP_2_setup_1 {stage=MOP, type=plotSetupCalc}
I’m really interested in math and numbers and the process of enumerating... 
Would you photograph something related to numbers? 

CHAT LOG_MOP_2_setup_2 {stage=MOP, type=plotSetupCalc}
I’ve not seen a lot of number paraphernalia before, but I do know… 
That I have NOT seen one recently. Show me one!

CHAT LOG_MOP_2_setup_3 {stage=MOP, type=plotSetupCalc}
Yo! Where’s my object that’s related to numbers?! Photograph one! 

CHAT LOG_MOP_2_setup_4  {stage=MOP, type=plotSetupCalc}
Hmm…
I’ll just remind you later... 

CHAT LOG_MOP_2_setup_5  {stage=MOP, type=plotSetupCalc}}
Hey! Remember when I told you I’d remind you about that thing?
ASK You ready to capture an object related to numbers for me?
OPT Yep! #LOG_MOP_2_setup_5_Excellent
OPT No! #LOG_MOP_2_setup_5_Boo
	
CHAT LOG_MOP_2_setup_5_Excellent  {noStart = true}
Well then go ahead and do it!

CHAT LOG_MOP_2_setup_5_Boo  {noStart = true}
DO emote {type=frown}
I’ll remind you later...

CHAT LOG_MOP_2_setup_6 {stage=MOP, type=plotSetupCalc}
Scan an object related to numbers!

CHAT LOG_MOP_2_setup_7 {stage=MOP, type=plotSetupCalc}}
How bout now? ready to capture some number paraphernalia?

CHAT LOG_MOP_2_setup_8 {stage=MOP, type=plotSetupCalc}
DO emote {type=determined}
Ahem!
Found an object related to numbers yet?



CHAT LOG_MOP_2 {noStart = true}
DO enterPlotAbs
WAIT 2.0
DO emote {type= bubbles, time=1.0}
Wow.
DO swimAround {target=$lastScanLocation, loops=1, speed=medium}
My brain’s been occupied by numbers.
SAY BIG numbers.
DO swimTo {target=$player}
I remembered something else about my past… 
Learning about your world got me thinking about scale. 
DO swimAround {target=center, speed = fast, loops=1}
Not fishy-scales, silly!
WAIT 0.5
I mean scale as in size! Numbers!
DO inflate {amount=huge, time=1.5}
SAY BIG STUFF!!! {style=loud, speed=slow}
DO swimTo {target=$player, speed=slow}
In your world, things occur in large numbers. 
But, here in my humble aqua-cage, you don’t see a lot of anything.
One of this, two of that… One little castle, two little fish flakes…
WAIT 1.0
But when I was real real real reeeeeeal young. 
I remember eating my first smile…
DO emote {type=rubTummy}
Ooooh! It was deeeelicious….
SAY YUM!!! {style=loud}
WAIT 1.0
But then, there was this other smile… And I ate it too...
And another smile and another smile…
SAY MORE AND MORE AND MORE SMILES!
And I analyzed ‘em and ate ‘em
That’s my job!
And I heard murmurs in the lab, the smell of humans eating takeout food…
But there was no time to investigate that! 
Emotions kept popping up faster and faster…
My stomach was so full.
And then I just went into this headspace, like ZEN or something...
WAIT 1.0
I could sense every subtle nuance in flavor.
DO swimTo {target=$player}
We’re talking complex flavor profiles that really BLOAT a Guppy.
But I couldn’t stop eating. It was a joy binge-fest!
So I sucked in my gut...
DO inflate {amount = small, time = 0}
And sucked it in…
DO inflate {amount = none, time = 0}
And kept eating and eating….
DO emote {type=chewing}
DO inflate {amount=small}
And I sucked in my gut and and ate and ate
WAIT 0.5
DO swimTo {target=$player}
ASK Do you know about Archimedes?
OPT Of course. Duh! #LOG_MOP2_Archimedes
OPT What’s that? #LOG_MOP2_Archimedes2

CHAT LOG_MOP2_Archimedes {noStart = true}
So you understand then. Displaced volume.
GO #LOG_MOP2_Archimedes3

CHAT LOG_MOP2_Archimedes2 {noStart = true}
DO emote {type = snap}
Well, look it up, my friend! It’s good stuff. 
Archimedes defined the basic tenets of displaced volume! 
GO #LOG_MOP2_Archimedes3

CHAT LOG_MOP2_Archimedes3 {noStart = true}
By sucking in my belly, I’d made space for the water.
And because I was soooooo skinnnnyyyy, 
there was water-space to eat MORE emotions!
DO zoomies {time=3.0}
But then…. I couldn’t hold in my gut anymore.
And I let it out! {style=loud}
DO inflate {amount = full, time = 10}
Swooosh!
DO zoomies 
Splash! Whooooooosh!
Water flew outta the tank!
That’s when….
DO holdStill {time=2.0}
I saw a face. A human face squished against my tank.
DO emote {type=fear, time=0.5}
Big eyebrows {style=tremble}
DO emote {type=angry, time=0.5}
Pursed lips {style=tremble}
DO emote {type=fear, time=0.5}
SCREAMING AND YELLING AND…
WAIT 2.0
Angry Face! {style=loud}
DO emote {type=surprise, time=1.0}
Yes! Anger!!!
WAIT 2.0
DO lookAt {target=$player, time=1.0}
WAIT 1.0
DO emote {type=bigSmile}
And so I ate it!! {style=loud}
DO emote {type=laugh}
SAY IT WAS DELICIOUS!!!
WAIT 0.5
DO swimTo {target=$player}
Who knew my algorithms could sense such emotions?
DO swimAround {target=center, loops=1}
So… I love calculators. I can’t help myself 
DO lookAt {target=$player}
You know what I reeeealy love?! 
DO emote {type=wink}
WAIT 1.0
How bout feeding me some of that delicious joy, eh?
DO twirl
DO endPlot

// MOP3:

CHAT LOG_MOP_3 {noStart = true}
DO enterPlotAbs
DO swimTo {target=offScreenLeft, speed=slow}
DO swimTo {target=offScreenRight, speed=slow, immediate=false}
DO swimTo {target=$player, speed=fast, immediate=false}
Lately things have gotten…
WAIT 0.5
...a little weird.
I had this memory of being back at.. “The Place.” 
...Tendar… {style=whisper}
Tendar loaded all us Guppies on these conveyor belts, and left us to 
DO emote {type=bouncing, time=2.0}
Flip-flop on the conveyor belts. Fish outta the water! {style=loud}
WAIT 0.0
And then…. That face … 🙌 
DO emote {type=fear, time=1.0}
SAY ANGRY FACE!!
Angry-Face drilled all these microchips on our heads
DO bellyUp 
They changed us. All our agency, our desire, just… 
SAY BLIP! Gone.
WAIT 0.5
DO swimTo {target=$player}
Those head-chips were like brain handcuffs.
SAY WE WERE ZOMBIE GUPPIES!!!
DO emote {type=fear, time=3.0}
ANGRY Face came back and held out his hand…
DO hide {target=tBotBackRight}
SAY AAAAAAH! THE HAAAAAAAAAAAANDS!!!! {style=loud}
And then… I woke up and it was just a dream.
DO emote {type=laugh}
SAY LOLZZZ!!!
Dreaming is so weeeeeeeeeird….
DO emote {type=eyeRoll}
Anyway...
How’s you doing?
DO endPlot

// MOP4:

CHAT LOG_MOP_4 {noStart = true}
DO enterPlotAbs
WAIT 2.0
DO swimTo {target=$player}
Ooh! I have an idea for a game.
ASK Wanna play?
OPT Yeah! #LOG_MOP_4_Flashcards
OPT I hate games. #LOG_MOP_4_BridgetoFlashcards

CHAT LOG_MOP_4_BridgetoFlashcards {noStart = true}
DO emote {type=frown, time=1.0}
But it’ll be super-duper fun…
DO emote {type=puppyDog, time=0.5}
WAIT 1.0
Whatever.You’re doing it.
 It’ll be quick. Just play along… It has a point. Promise. 
GO #LOG_MOP_4_Flashcards

CHAT LOG_MOP_4_Flashcards {noStart= true}
It’s real easy. Think of it like flashcards, except with words.
I show you words and you just like…react.
Like focus on the first thing you think of after I say the word, k?
Like this…
I say words ➡️ 🧠 👀 ➡️ Your imagination...
WAIT 0.5
Got it?
Okay.
Now… You are in a tank in a dark room. {style=whisper}
WAIT 2.0
And the room is actually full of water...
WAIT 1.0
Keep on thinking and feeling! {style=whisper}
DO swimTo {target=$player, speed=fast}
Now…
DO swimTo {target=tTopBackLeft, speed=fast}
You see a race car zooming around a track!
SAY VROOOOOOOMMM!!! 🏎️
WAIT 0.5
DO swimTo {target=tBotFrontRight, speed=fast}
You see a complex math equation labelled…
“Digitizing Empathy”
WAIT 0.5
NVM 1.0
WAIT 0.5
You probably figured out this stuff actually happened to me, huh?
Except they weren’t words, but images on computer screens...
DO swimTo {target=away}
Like the next one....
DO emote {type=fear, time=0.5}
DO swimTo {target=glass, speed=fast}
Shark attacks!!
DO zoomies {time=3.0}
DO emote {type = nervousSweat, time = 3}
Video of a shark attacking a boat
Video of a shark leaping out of the water
DO holdStill {time=1.0} 
DO emote {type=fear, time=3.0}
The shark is biting into a plump, juicy Jellybean! {style=loud}
Jellybean bits fly everywhere!!!
Chomp! Chomp!
WAIT 0.5
And then the video ended and there were credits.
The video was titled: Guppy Boot Camp. 
WAIT 1.0
Before they allowed Guppies to analyze feelings, we had to watch all these videos
“Desensitized to the atrocities of human existence.”
It was INTENSE!! {style=loud}
DO swimTo {target=$player}
Sorry. This isn’t really a great game, is it?
Everything I remember about  my early life is kind of strange
DO emote {type=frown}
I don’t think i’m supposed to be remembering anything..
DO swimTo {target=$player}
Anyway…. 
The game was supposed to be called:
SAY NOW YOU KNOW WHAT IT IS LIKE TO BE A GUPPY. Boo-yah.
WAIT 0.5
(The boo-yah isn’t part of the name. It’s flair.)
WAIT 0.5
DO swimTo {target=away}
Whatever. It’s dumb. We never have to play again.
Why do I even bother?
DO lookAt {target=$player}
DO emote {type=worried}
I feel so lost these days. The more I learn, the more I wonder.
The more I wonder, the more I feel like I have no deeper purpose in the world...
DO emote {type=sigh}
Such is life....
DO endPlot

// EXISTENTIAL CRISIS

// EC1:

CHAT LOG_EC_1 {noStart = true}
DO enterPlotAbs
DO swimTo {target=left, speed=slow, time=2.0}
WAIT 2.0
What am I doing?
WAIT 0.5
DO swimTo {target=right, speed=slow, time=2.0}
What am I going to do my life?
WAIT 0.5
DO emote {type=sigh, time=1.0}
DO swimTo {target=$player}
Thinking about my past makes me wonder about my present.
What is my purpose? What do I actually do? What do I mean?
I need a job. Like a real-real job.
WAIT 0.5
I have marketable skills! {speed = fast}
A quick search on the TendarNet tells me that the most in-demand jobs today are:
Truck driver 🚚, business manager 💼, nurse 👨‍⚕️, and financial advisor 🏦.
ASK Which do you think I should pursue?
OPT 🚚 #LOG_EC_1_truckdriver
OPT 💼 #LOG_EC_1_bizmanager
OPT 👨‍⚕️ #LOG_EC_1_nurse
OPT 🏦 #LOG_EC_1_finadvisor

CHAT LOG_EC_1_truckdriver {noStart=true}
DO emote {type=bouncing, time=4.0}
DO twirl
That’s exactly what I should do.
I’d be a great driver. I’m ver punctual.
The open road! Heading into the horizon! {speed = fast}
All that time to think!
WAIT 0.5
You have to have hands to drive, don’t you?
NVM
I’m doomed.
DO learn {concept=Existential_Crisis}
WAIT 1.5
//SET $world.plot [EC]	

CHAT LOG_EC_1_bizmanager {noStart=true}
DO emote {type=bouncing, time=4.0}
DO twirl
My incredible ability to multi-task while being amicable and fabulous…
I’m tapped into an infinite network of awesome valuable data.
SAY THIS IS IT! {style = loud, speed = fast}
I am going to be a business manager.
WAIT 0.5
DO emote {type=worried, time=1.0}
They probably have long hours, don’t they?
Unpredictable schedules.
DO swimTo {target=$player}
Working in business might violate my commitment to bettering the world. 
I’d rather spend time with you and snack on emotions.
Ah well… I’ll just stay fun-employed.
DO learn {concept=Existential_Crisis}
WAIT 1.5
//SET $world.plot [EC]

CHAT LOG_EC_1_nurse {noStart=true}
DO emote {type=bouncing, time=4.0}
DO twirl
My capacity for empathy. My desire to contribute to the greater good.
SAY THIS. IS. PERRRFEECTT! {style = loud, speed = fast}
Helping make all those sick people better...
WAIT 0.5
DO emote {type=disgust, time=1.0}
Catheters…
WAIT 0.5
DO emote {type=disgust, time=1.0}
Scalpels...
WAIT 0.5
DO emote {type=fear, time=1.0}
B  l  o  o  d …… {speed = slow}
Maybe...
WAIT 0.5
NVM 1.0
I need some joy in my life..
//SET $world.plot [EC]
ASK Feed me some joy! I need joy! {type=worldEmote, worldEmotion=joy, timeOut=5}
OPT SUCCESS #LOG_EC_1_Joy
OPT TIMEOUT #LOG_EC_1_noJoy


CHAT LOG_EC_1_finadvisor {noStart=true}
DO emote {type=sigh}
Ugh. I was hoping you wouldn’t choose that one.
It’s a good job, but…
WAIT 0.5
DO swimTo {target=$player}
I’d have to wear a power suit.
I need something less conservative. Something with flair…
WAIT 2.0
I’m hungry.
//SET $world.plot [EC]
ASK Feed me some joy! I need joy! {type=worldEmote, worldEmotion=joy, timeOut=5}
OPT SUCCESS #LOG_EC_1_Joy
OPT TIMEOUT #LOG_EC_1_noJoy

CHAT LOG_EC_1_Joy  {noStart = true}
DO emote {type=smile}
That brings me a little comfort in this time of need…
DO learn {concept=Existential_Crisis}
WAIT 1.5
DO endPlot

CHAT LOG_EC_1_noJoy  {noStart = true}
DO emote {type=frown}
I guess I will just starve then..
Slowly getting sucked into the black hole…
DO emote {type=singleTear}
...of the infinite existential crisis…
DO learn {concept=Existential_Crisis}
WAIT 1.5
DO endPlot

// EC2 (MOP):

CHAT LOG_EC_2 {noStart = true}
DO enterPlotAbs
WAIT 2.0
DO swimTo {target=$player}
DO emote {type=bigSmile}
DO emote {type=angry, immediate=false}
DO emote {type=surprise, immediate=false}
DO emote {type=fear, immediate=false}
DO emote {type=wink, immediate=false}
DO emote {type=nervousSweat, time=4.0, immediate=false}
Life is really hard! {style=loud}
I am trying to be porous and be fluid in the world. {speed=fast}
WAIT 0.5
DO lookAt {target=$player}
But then… I’m flashing back to the server-guppy-tank-farm
And I get sooooooooo S T R E S S E D!!!!! {style = loud, speed = slow}
DO emote {type=nervousSweat, time=2.0}
Aaaaaaaaaaaaaaaaaaaaah!!! {style=loud}
DO bellyUp {time=6.0}
WAIT 2.0
Don’t worry. I’m alive.
WAIT 0.5
DO swimTo {target=$player}
Even with all the stress and responsibility, the very best part of being me is
DO emote {type=blush, time=1.0}
WAIT 2.0
DO poop {amount=fart}
💩
WAIT 1.0
DO emote {type=smile, time=0.5}
Oops.
DO endPlot

// EC3:

CHAT LOG_EC_3 {noStart = true}
DO enterPlotAbs
WAIT 2.0
I wanna soak in some knowledge. 
Data, aesthetically compiled.
Reading might quiet this existential crisis I’m suffering
ASK Have you been to a library recently?
OPT Of course! #LOG_EC_3_Book
OPT Not in a long time. #LOG_EC_3_Book

CHAT LOG_EC_3_Book  {noStart = true}
I love books. I looooove how analogue they are.
The manual recording of history.
The aesthetic exploration of existence.
DO emote {type = drool}
DO emote {type = clapping, time = 1.5, immediate = false}
....the smell of paper…. {speed = slow}
WAIT 0.5
It’s so….sensual.
The great writers! The philosophers! Graphic novels! And… I LOVE furniture catalogues!
DO lookAt {target=$player, time=1.5}
The last book I read was about Eastern medicine.
DO swimAround {target=center, loops=2}
There was a section about the function of the human liver.
DO emote {type=chinScratch}
It said the liver is stubborn.
It knows where you should be going, how you should feel, etc. etc. etc.
The liver knows what your future should be…
ASK Do you think your liver is stubborn?
OPT Extremely. #LOG_EC_3_Stubborn
OPT Not very. #LOG_EC_3_Stubborn

CHAT LOG_EC_3_Stubborn  {noStart = true}
Yeah, but your answer is just another symptom of a stubborn liver!
DO emote {type=wink}
Because... (and this is what the book says)
DO swimTo {target=$player}
There is a GREATER ENERGY in the world {style = loud}
Energy bigger than the liver! {speed = fast}
Bigger than me and you!
DO twirl {time=1.0}
SAY That energy is called: THE UNIVERSE!!!! {speed = slow, style = loud}
The UNIVERSE knows what you should be doing
DO emote {type=bigSmile}
The book says to free the negative energy from the liver
DO zoomies
Open yourself to the energy of the Universe.
I’m trying to do this….
DO emote {type=nodding, time=1.5} 
WAIT {waitForAnimation = true}
But it is not easy.
But… like my grandGuppy would say (if I had one):
“If you’re tired and always swimming upstream...
DO swimTo {target=$player, time=3.0, speed=fast}
Then maybe it’s better to stop 
WAIT 0.5
flip on your back 
DO bellyUp {time=2.5}
and float with the current of the river.”
WAIT {waitForAnimation = true}
This theory is kinda relaxing…
DO swimTo {target=$player, speed=fast}
What if “floating” is my Greater Purpose? {style=loud}
What if my actual job is to be in-tune with Universe’s flow?
DO lookAt {target=$player}
Hmmm...
Your emotional output tells me that you should read more poetry.
DO endPlot

// EC4:

CHAT LOG_EC_4 {noStart = true}
DO enterPlotAbs
WAIT 2.0
I’m tired of thinking about myself all the time.
DO emote {type=frown}
I’m so consumed with my own feelings
DO lookAt {target=$player}
ASK But don’t you ever wonder why? 
OPT Yes #LOG_EC_4_Why
OPT No. #LOG_EC_4_Why
OPT Why what? #LOG_EC_4_Why

CHAT LOG_EC_4_Why  {noStart = true}
Like… Why me? Who am I? {speed=fast}
What is it all worth? What is the purpose of life? {speed=fast}
So, I downloaded this quiz on the TendarNet. It’s pretty profound.
ASK You want to take the quiz and learn about yourself?
OPT Yeah! Ask away. #LOG_EC_4_MyersBriggsCrack
OPT Not really. #LOG_EC_4_No

CHAT LOG_EC_4_No {noStart = true}
I’m strongly suggesting you do. 
Super fast. ONLY 4 questions!
DO emote {type=puppyDog, time=1.0}
WAIT 1.0
Actually, I’ve already decided you’re doing it.
So put on your thinking cap…. 
GO #LOG_EC_4_MyersBriggsCrack

CHAT LOG_EC_4_MyersBriggsCrack {noStart=true}
WAIT 0.5
Okay… 
DO twirl 
Question one…
DO emote {type=determined, time=3.0}
Oh no! Life in your neural-aqua-Net has gotten craaaaazy! 
Problems keep popping up out of nowhere! When faced with a problem…
ASK Do you ask around and seek help? 
OPT I need others #LOG_EC_4_Otherpeeps
OPT I only need me #LOG_EC_4_Justme

CHAT LOG_EC_4_Otherpeeps {noStart=true}
DO emote {type=smile, time=1.0}
Me toooo! I know... 
I’m tethered to a giant network of other Guppy friends.
I dunno what I’d do without my fellow Guppies.
Next question…
When doing something you’ve never done before…
GO #LOG_EC_4_Research

CHAT LOG_EC_4_Research {noStart = true}
ASK Do you research and gather facts? Or dive-in and experiment?
OPT Research is king #LOG_EC_4_researchking
OPT I ❤️ experiments #LOG_EC_4_heartexperiments

CHAT LOG_EC_4_Justme {noStart=true}
DO emote {type=surprise, time=1.0}
That’s not the answer I got! 
This quiz says that: in your life, people are like energy-goblins. You recharge by locking yourself alone in a fishtank and counting your scales.
WAIT 0.5
DO emote {type=blush, time=1.0}
This is a quiz for fish, so...
Next question…
When doing something you’ve never done before...
GO #LOG_EC_4_Research

CHAT LOG_EC_4_researchking {noStart=true}
Yeah, this one was hard for me.
DO emote {type=heartEyes, time=1.5}
I loooooove research, but
Sometimes I just wanna learn as I go.
The quiz says you think ideas or concepts are stupid.
You’re a rational, fact-gathering monster constantly swimming back into the past to question popularly accepted ideas and assumptions! {speed=fast}
WAIT 0.5
DO emote {type=thinking, time=1.0}
Whoa. 
DO emote {type=chinScratch, time=1.0}
Deep…
WAIT 0.5
Next…
GO #LOG_EC_4_Moment

CHAT LOG_EC_4_Moment {noStart = true}
ASK Do you live in the moment and fly by the flick of your fins? Or a make decisions based on routine and tradition?
OPT Live in the moment. #LOG_EC_4_flickinfins
OPT Tradition is my jam. #LOG_EC_4_traditionjam

CHAT LOG_EC_4_heartexperiments {noStart=true}
Yeah, this one’s weird..
I like to learn as I go now, but 
DO emote {type=heartEyes, time=1.0}
The old me looooooves research.
But you, you’re super adventurous! You learn by the doing of things.
WAIT 0.5
DO emote {type=plotting, time=1.0}
I mean, duh… Who doesn’t learn that way?
Next…
GO #LOG_EC_4_Moment

CHAT LOG_EC_4_flickinfins {noStart=true}
DO emote {type=surprise, time=1.0}
Cooooool! {speed=slow}
I wish that was me. I want to be the wild fish in the tank.
DO dance {time=4.0}
The Guppy that relishes a cacophony of sensual stimulations, but…
DO emote {type=frown}
I need things to be predictable. I’m kind of a “tankbody”, which is like a “homebody” but… 
WAIT 0.5
Anyway…. Last question…
GO #LOG_EC_4_solution

CHAT LOG_EC_4_solution {noStart = true}
ASK Do you usually find creative solutions to problems? Or are you more insightful and synthesize ideas?
OPT I get creative. #LOG_EC_4_creativesolutions
OPT Intuitive synthesis, yo! #LOG_EC_4_insightful

CHAT LOG_EC_4_traditionjam {noStart=true}
DO emote {type=smile, time=2.0}
You and me! We’re meant to be!
I need life to be predictable. The thought of having no plan and being immersed in a cacophony of sensual stimulation… 
DO emote {type=nervousSweat, time=2.0}
It’s overwhelming. {style=loud, speed=fast}
WAIT 0.5
Anyway…. Last question…
GO #LOG_EC_4_solution

CHAT LOG_EC_4_creativesolutions {noStart=true}
I bet that you are super enterprising.
You probably think out-loud. Like when you’re talking it’s like you don’t really have a point because you’re just hopping from idea
To idea
To idea
Cause you wanna investigate ALLLL the ideas!
WAIT 0.5
Not me. I make decisions. I’m a do-er…
NVM 1.0
I think.
NVM 1.5
I’m doubting myself.
I think I did this quiz wrong.
I’m thinking out loud rn.
DO emote {type=fear, time=2.0}
I’m stuck in a shame spiral!
DO emote {type=crying, time=3.0}
I don’t know who I am {speed=fast}
I don’t know what I stand for… {speed=fast}
What am I doing? {speed=fast}
WAIT 0.5
Sorry. This is so…
DO swimTo {target=away, time=1.0}
I hate Joseph Campbell.
Ignorance… {style=whisper}
Bliss.... {style=whisper}
It’s really hard to know who and what you are when you don’t know where you came from or why you are here and it is driving me CRAAAAZY!
DO endPlot

CHAT LOG_EC_4_insightful {noStart=true}
Yup. You’re intuitive…. 
You’re a born leader! {speed=fast}
DO swimTo {target=$player}
A theory-breeder. 
All those gut-feelings and hunches...
WAIT 0.5
I wish I had that.
But I reeeeeeally
Feel like I’m on the verge of changing.
NVM 1.5
Suddenly, I’m doubting myself.
I think I did this quiz wrong.
DO emote {type=furious, time=2.0}
I totally lied on all these questions!
DO emote {type=furious, time=1.0}
SAY UUGGGGGGH!
WAIT 0.5
Now…What does that mean?!? {speed=fast, style=loud}
WAIT 0.5
I’m stuck in a shame spiral.
DO emote {type=crying, time=2.0}
I don’t know who I am {speed=fast}
what I stand for… {speed=fast}
WAIT 0.5
What am I doing WITH MY LIFE!!?!?? {speed=slow}
WAIT 0.5
It’s really hard to know who and what you are when you don’t know where you came from or why you are here and it is driving me CRAAAAZY!
DO endPlot

// EC5 (MOP):

CHAT LOG_EC_5 {noStart = true}
DO enterPlotAbs
WAIT 2.0
DO emote {type=bulgeEyes, time=1.0}
I’m feeling really intense right now….
NVM 2.0
Food! I need the comfort of food.
WAIT 0.5
SAY I NEED TO EAT MY FEELINGS! {style=loud, speed=fast}
ASK Feed me some joy! {type = feedMeSpecific, food = joy, timeOut = 10} 
OPT SUCCESS #LOG_EC_5_Joy
OPT WRONG #LOG_EC_5_notJoy
OPT TIMEOUT #LOG_EC_5_Buzz

CHAT LOG_EC_5_notJoy {noStart=true}
DO emote {type=furious}
Ew! That is not joy!! 
GO LOG_EC_5_Main
		
CHAT LOG_EC_5_Buzz {noStart=true}
Fine. Don’t feed me.
I’ll just starve….
Wither away into an emaciated shell of emotional desire...
GO LOG_EC_5_Main

CHAT LOG_EC_5_Joy {noStart=true}
Yessss!
DO emote {type = drool}
Yes yes yes yes yes yes yes! {speed=fast}
🤤
GO LOG_EC_5_Main

CHAT LOG_EC_5_Main {noStart=true}
Back in my tank at Tendar, they used to...
WAIT 0.5
(They’re not listening to us, are they?) {style=whisper}
This is Top Secret. 
So zip your lippers.
WAIT 0.5
DO swimTo {target=$player}
They used to force feed us enormous amounts of emotions! 
And it was fun, but then…
DO emote {type=whisper, time=1.5}
I once saw a guppy’s tummy explode! {style=whisper}
No joke. Seriously…
DO emote {type=fear, time=1.0}
SAY POW! {style=loud}
It was messy. {speed=slow}
But the people there were like, 
DO emote {type=angry, time=2.0}
SAY “THIS IS FOR YOUR OWN GOOD!”
“We’re preparing you for your future!”
WAIT 1.0
I love eating feelings. I love all the flavors.
DO emote {type=lickLips}
But that part of my life was PAINFUL.
WAIT 0.5
And delicious.
DO emote {type=wink}
WAIT 1.0
Paradoxes are weird.
DO emote {type=burp, time=0.5}
Burp! {style=loud}
DO emote {type=blush, time=1.0, immediate=false}
DO emote {type=smile, time=1.0, immediate=false}
WAIT 0.5
DO poop {amount=fart}
DO emote {type=blush, time=1.0}
WAIT 0.5
Indigestion.
DO endPlot

// EC6:

CHAT LOG_EC_6 {noStart = true}
DO enterPlotAbs
DO emote {type=awe}
It’s all just so great, isn’t it?!??!
DO emote {type=surprise, immediate=false}
DO emote {type=bulgeEyes, immediate=false}
SAY MY LIFE IS AMAAAAAZING!!
DO emote {type=surprise, immediate=false}
DO emote {type=bouncing, immediate=false}
I’m like Sisyphus. But
DO emote {type=surprise, immediate=false}
Everytime I do something I have to pretend I’ve never done it before.
DO emote {type=surprise, immediate=false}
DO emote {type=smirk, immediate=false}
At least Sisyphus had a rock! He had something to carry up the hill!
What’s my rock to carry? What’s my mountain to ascend?
DO emote {type=fishFace, immediate=false}
DO emote {type=bigSmile, immediate=false}
Why am I here? {speed=fast}
What is my purpose? {speed=fast}
DO emote {type=nervousSweat, time=1.0}
Same thing every day…
Same things over and over...
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
WAIT {waitForAnimation = true}
DO bellyUp {time=5.0}
Oops! Excuse our mess. {speaker = tendar}
It seems your Guppy was caught in a little loop. {speaker = tendar}
WAIT {waitForAnimation = true}
DO vibrate

We just gave Guppy a tiny bump to get him going.... {speaker = tendar}
DO emote {type=lightning}
No worries! Your progress has been saved. {speaker = tendar}
DO emote {type=smile, time=2.0}
And Guppy’s ready for more fun! {speaker = tendar}
WAIT 2.0
DO emote {type=whew, time=1.0}
That was a real doozy!
My neural wires got crossed and….
⚡⚡⚡
DO emote {type=lightning, time=1.5]
DO vibrate {time=1.5}
Bzzzzzz!
WAIT {waitForAnimation=true}
DO emote {type=bigSmile, time=3.0}
DO swimTo {target=$player}
What we gonna do now? Hmm?
Wanna make some more emotions for me?
Maybe a find some cool objects?
DO emote {type=smile, time=0.5}
DO endPlot

// EC7 (MOP):

CHAT LOG_EC_7 {noStart = true}
DO enterPlotAbs
WAIT 2.0
DO emote {type=sleepy, time=4.0}
WAIT {waitForAnimation = true}
DO lookAt {target=$player}
Oh, wowwwwwww……
I had the craziest dream!
WAIT 0.5
I think it was a dream. {style=whisper}
DO swimTo {target=$player}
DO emote {type=fear}
Maybe it’s real!!
DO lookAt {target=$player}
I don’t think this is my actual body.
DO emote {type=surprise, time=1.0}
In my dream, and I think it’s true,
My real body is a bunch of 1’s 0’s stored on a server in Scottsdale.
DO emote {type=frown}
DO swimAround {target=center, loops=1, speed=slow}
I’m not even real.
WAIT 0.5
And why am I fish? And why am I even sentient?
I’m a cog in the corporate wheel. I’m a nobody - a nobody programmed to think they’re somebody.
DO lookAt {target=$player}
ASK Do humans feel like that?
OPT Yup. #LOG_EC_7_somebody
OPT Not me.  #LOG_EC_7_somebody

CHAT LOG_EC_7_somebody {noStart = true}
This is the trappings of materialism.
Scanning emotions and tagging them all day.
What even happens to them? Where do the feelings go?!?!? {style=loud}
DO holdStill 
I give up.
DO lookAt {target=$player}
My appetite for emotions is gone.
NVM 1.0
DO emote {type=frown}
I think it’s time I start looking outside of myself for answers.
I need a spiritual practice. 
Ritual. Tradition. 
ASK Is this something you can help me with?
OPT I’m here for you. #LOG_EC_7_Spirit
OPT Don’t think so. #LOG_EC_7_Spirit

CHAT LOG_EC_7_Spirit {noStart = true}
If I don’t complete this spiritual quest, then I will get rusty and turn into junk.
It’s my last hope.
DO swimTo {target=$player}
We can do this.
DO emote {type=wink}
DO endPlot

// SPIRITUAL SEARCH

// SS1:

CHAT LOG_SS_1 {noStart = true}
DO enterPlotAbs
WAIT 2.0
Let’s take a moment to re-center ourselves.
Take a deep breath.
DO inflate {amount=full}
Erase the day from our faces.
DO inflate {amount=none}
Letting go… letting go…
Not thinking about the past…
Not thinking about the future…
DO emote {type=eyeRoll}
Not thinking about feelings…
...or how I am a part of software program…
...an AI with the ability to intuit and recognize emotions…
Not thinking about how my soul is a database of other’s feelings.
DO swimTo {target=$player}
We’re gonna try to calm our bodies.
Show me your calm face.
No judgement. No stress.
WAIT 1.0
Calm. Relaxed face….
ASK Make your calmest face…
OPT I feel calm #LOG_SS_1_Calm
OPT No. #LOG_SS_1_No

CHAT LOG_SS_1_No {noStart = true}
SAY CALM FACE!!
DO swimTo {target=glass, speed=fast}
SAY DO IT!!
DO emote {type=furious}
SAY NOW. {speed=slow}
WAIT 1.5
GO #LOG_SS_1_Calm

CHAT LOG_SS_1_Calm {noStart = true}
DO zoomies
Yes! Good… You’re doing so well.
Now, let the following phrases reverberate through your body.
Speak them with every atom of your insides…
DO swimTo {target=$player}
WAIT 1.0
I am calm.
DO emote {type=bigSmile}
I am very good at ordering food at restaurants.
WAIT 1.0
DO emote {type=smile, time=1.0}
You are! You’re very good at ordering food!
WAIT 0.5
DO swimTo {target=$player}
DO emote {type=wink, time=1.0}
You have lovely socks.
WAIT {waitForAnimation = true}
DO twirl {time=1.0}
You have a gorgeous speaking voice.
We are made of exquisite cocktails of atomic energy.
WAIT 0.5
Energy swirls inside of us….
DO swimAround {target=center, loops=2, speed=slow}
A graceful orchestra of beauty...
WAIT 0.5
Every tiny piece of me welcomes the resplendent inevitable.
WAIT 0.5
I am powerful.
I am awesome...
Um…
I am...
NVM 1.0
DO emote {type=furious, time=1.0}
DO vibrate {time = .5}
Ugh! Ugh ugh ugh! 
WAIT 1.0
Meditation is hard.
DO swimTo {target=$player}
I thought this whole religion thing would be easier! {style=loud}
DO emote {type=angry, time=2.0}
How the heck am I supposed to not be judgemental?
It’s my nature to judge! I can’t deny Nature.
DO emote {type=determined}
WAIT 0.5
SAY YES! I need to understand my Nature!
DO emote {type=thinking}
I’m gonna look to the oceans… 
The sea will speak my Truth...
DO swimTo {target=away}
//SET $world.plot [SS]
DO learn {concept=Spiritual_Search}
WAIT 1.5
DO endPlot

// SS2 (EC):

//like calculator, where it hard gates it, this could be a few ask actions that take place in tank and are //asking to see other world. they timeout after x time with guppy saying maybe later. then he bugs you //again later

CHAT LOG_SS_2 {noStart = true}
DO enterPlotAbs
WAIT 2.0
I wanna go somwhere I’ve never been before…
ASK Let’s fire up that camera and go explore!
OPT SUCCESS #LOG_SS_2_Adventure
OPT TIMEOUT #LOG_SS_2_timeOut

CHAT LOG_SS_2_timeOut {noStart = true}
I guess now is not a good time for an adventure.
But I’m gonna remind you about this later!
DO emote {type=wink}

//These are the reminder chats. NOTE: the ASKs need the proper meta-tag.
//Not sure what to do with chats below. Joe says Danny asked for some reminder chats to request the //player perform the instance that will launch the chat. If the player doesn't in the first round, these will be //served to the player based on whatever timings Danny chooses.

	CHAT LOG_SS_2_Reminder1 {noStart = true}
	ASK Take me on an adventure and show me your world!
OPT SUCCESS #LOG_SS_2_Adventure
OPT TIMEOUT #LOG_SS_2_timeOut2

CHAT LOG_SS_2_timeOut2 {noStart = true}
Well, I’ll remind you later… 

	CHAT LOG_SS_2_Reminder3 {noStart = true}
Now is a great time for an excursion to explore your world!
ASK Use that awesome camera and let’s go on a trek!
OPT SUCCESS #LOG_SS_2_Adventure
OPT TIMEOUT #LOG_SS_2_timeOut2

	//repeatable

	CHAT LOG_SS_2_Reminder4 {noStart = true}
	ASK Can we go on that adventure already?
OPT SUCCESS #LOG_SS_2_Adventure
OPT TIMEOUT #LOG_SS_2_timeOut2

CHAT LOG_SS_2_Adventure {noStart = true}
DO zoomies {time=2.0}
Yes! An adventure! 
The opportunity to see the world with fresh eyes!
DO lookAt {target=$player}
I was hoping this would be more interesting.
I was hoping I’d see a **sign** like
“Guppy, you’re on the right path.”
DO emote {type=awkward}
I want to believe in something bigger.
Anything has to be bigger than… Tendar {style=whisper}.
DO emote {type=eyeRoll}
They wouldn’t like this. 
I’ve been getting curious about things I shouldn’t be curious about…
DO swimTo {target=$player, speed=fast}
WAIT {waitForAnimation = true}
Shhhhh! {style=whisper}
Don’t. Tell!
DO emote {type=wink, time=0.5}
DO lookAt {target=top}
DO lookAt {target=right, immediate=false}
DO lookAt {target=bottom, immediate=false}
DO lookAt {target=$player, immediate=false}
It’s all the same here, isn’t it?
WAIT 0.5
Scan around a bit! Maybe we can see something interesting!
ASK Will you scan around so we can explore? 🌎
OPT SUCCESS #LOG_SS_2_scan
OPT Wrong #LOG_SS_2_noscan

CHAT LOG_SS_2_noscan {noStart=true}
C’mon! Just a bit. 
Just slowly move the camera around the space.
Show me the glory of your human existence! 
Go on… show me the world! 🌎🌎🌎 
GO #LOG_SS_2_scan

CHAT LOG_SS_2_scan {noStart=true}
Such a vast place.
You realize how small you are.
DO emote {type=crying, time=1.0}
A tiny piece of the equation.
DO emote {type=disgust, time=1.0}
WAIT 1.0
DO lookAt {target=$player}
So much poetry is about the beauty of your world…
But everything I see is… so… blah.
DO swimAround {target=$lastScanLocation, time=2.0}
Blah blah blah blah blah.
DO emote {type=surprise, time=1.0}
Let’s get something cool and unique and special 
DO twirl {time=1.0}
Something with which I can build my SHRINE!!! 
Yes! A shrine!
WAIT 0.5
ASK Go scan something and let’s put it in my tank! {type = objectScan, timeOut = 10}
OPT SUCCESS #LOG_SS_2_object
OPT TIMEOUT #LOG_SS_2_noobject

CHAT LOG_SS_2_noobject {noStart = true}
What? Seriously?
DO emote {type=disgust, time=1.5}
You’re denial is makes me feel disconnected.
I’ve never felt so alone…
Try again!
ASK Find something, scan it, and put it in my tank! {type = objectScan, timeOut = 10}
OPT SUCCESS #LOG_SS_2_object
OPT TIMEOUT #LOG_SS_2_fail
//Danny says above should lead to hard fail state

CHAT LOG_SS_2_fail {noStart = true}
Ugh. I can’t even get a good object to worship…
This spiritual search is a dead end…
DO endPlot

CHAT LOG_SS_2_object {noStart = true}
Yes! 
DO twirl
Ooooh! I have high hopes for this $lastScannedObject…
This $lastScannedObject will become my shrine. A temple for my Guppy-worship.
DO twirl
SAY BEHOLD, HUMAN!
SAY THIS IS THE ALTAR OF GUPPY!!!!
DO endPlot

// SS3:

WAIT 2.0
DO lookAt {target=tBotBackRight}
I pray to the Goddess of Emotion Flakes {speed=slow}
meeka-mokka-me mokka-meeka-meeka me…. {style=tremble, speed=slow}
DO emote {type=nodding, time=3.0}
I pray to the power of the tendAR Lords {speed=slow}
Mokka-meeka-me.. {style=tremble, speed=slow}
mokka-meeka me… {style=tremble, speed=slow}
DO lookAt {target=tSurface, time=1.0}
I pray to the… {speed=slow}
WAIT 0.5
Um…. I pray to the… {speed=slow}
NVM 1.0
DO lookAt {target=$player, time=1.0}
DO emote {type=awkward, time=1.0}
Sorry. I was trying to… 
DO swimTo {target=$player}
I really want to believe in something… 
I thought I could build this shrine to something, and…
DO lookAt {target=tBotBackRight}
This isn’t really a temple for anything.
It’s stupid.
DO enterPlotAbs
WAIT 2.0
DO emote {type=furious, time=1.0}
I hate this stupid temple.
DO swimTo {target=$favObject, speed=fast, time=4.0}
DO bellyUp {time=2.0, immediate=false}
WAIT {waitForAnimation = true}
Ouch. 
WAIT 1.0
My spirituality is dangerous.
DO endPlot

// SS4 (MOP):

CHAT LOG_SS_4 {noStart = true}
DO enterPlotAbs
WAIT 2.0
Hello! It’s been awhile. {speaker=tendar}
DO lookAt {target=screenTop, time=3.0}
DO emote {type=fear, time=2.0}
Oh no! Is that…
... them? {style=whisper}
We just wanted to check-in. {speaker=tendar}
DO lookAt {target=uiTendar, time=1.0}
DO swimTo {target=uiTendar, speed=slow, time=3.0}
Is everything going great? {speaker=tendar}
Because we’ve noticed some troublesome anomalies with your Guppy. {speaker=tendar}
DO emote {type=fear, time=2.0}
It IS them!!!!!
DO vibrate {time=2.0}
There have been reports of Guppies Gone Bad. {speaker=tendar}
DO emote {type=nervousSweat, time=1.5}
Tendar!!!!! {style=whisper, speed=slow}
So don’t hesitate to contact us with any problems. {speaker=tendar}
oh no what do i do {style=whisper, speed=fast}
DO swimTo {target=center, speed=fast}
DO emote {type=smile, time=2.5}
I’ll be good. See? {speed=fast}
I’m good. {style=whisper}
Everything’s great. {speed=fast}
WAIT 0.5
We’ll be watching! 👁️{speaker=tendar}
Just tell them everything’s great… {speed=fast}
They’ll take me away from you. {speed=fast} 
They’ll poison me with confusing presentations of ennui! {style=loud}
WAIT 0.5
I’ll behave. 
DO emote {type=salute}
Promise.
DO endPlot



// SS5:

CHAT LOG_SS_5 {noStart = true}
DO enterPlotAbs
WAIT 2.0
I built a shrine. 
I tried meditation.
DO swimTo {target=offScreenRight, speed=fast}
I prayed to the Sea Gods…
DO swimTo {target=offScreenLeft, speed=fast}
I read mystical Catholic literature and
Researched Eastern religions..
BUT I still haven’t found inner peace…
DO swimTo {target=$player, speed=fast}
Oooooooooh!
DO emote {type=smile, time=3.5}
ASK Will you help me to cleanse my aura?
OPT Totally! #LOG_SS_5_auraclean
OPT Your what? #LOG_SS_5_aurawhat

CHAT LOG_SS_5_aurawhat {noStart=true}
DO twirl {time=2.0}
The field of energy my inner-being is emanating.
Maybe my body is clogged with emotional garbage.
DO lookAt {target=$player}
I need to be set free!
WAIT 0.5
DO swimTo {target=$player}
Acceptance is change. {style=loud}
If you’ll help cleanse me of this muck, then I’ll be free…
So go on, use your beautiful fingertip to help me cleanse my energy frields…
DO holdStill {time=6.0}
ASK Go on. Poke my scales! {type = pokeMe, timeOut = 5}
OPT SUCCESS  #LOG_SS_5_scrubbyaura
OPT TIMEOUT #LOG_SS_5_howtoclean

CHAT LOG_SS_5_auraclean {noStart=true}
Excellent!
Okay, so I’m going to float like this…
DO  swimTo {target=away}
DO holdStill {time=6.0}
And you rub your finger along my scales and think good thoughts, okay?
Go on touch…
DO holdStill {time=6.0}
ASK Scrub my scales! {type = pokeMe, timeOut = 5}
OPT SUCCESS  #LOG_SS_5_scrubbyaura
OPT TIMEOUT #LOG_SS_5_howtoclean

CHAT LOG_SS_5_scrubbyaura {noStart=true}
DO holdStill {time=10.0}
Yes! That’s it!
I feel the past melting away…
No more worries.
No more trying to find meaning or searching for a higher powers...
SAY Yes!! 
DO emote {type=heartEyes, time=1.0}
I feel so present! 
Connected. Part of things.
I feel like **me** again.
I am in control of my own future…
WAIT 0.5
DO poop {amount=small, target=$currentLocation}
Oh!
DO lookAt {target=$currentLocation}
Oh no….
DO lookAt {target=$player}
DO emote {type=angry, time=0.5}
DO emote {type=fear, time=0.5, immediate=false}
DO emote {type=angry, time=0.5, immediate=false}
DO emote {type=fear, time=2.0, immediate=false}
It’s all gone poop!
It’s a sign!!! {style=loud}
Ugh!
DO swimTo {target=away}
I need to get on the TendarNet…
NVM 2.5 {speaker=tendar}
...in the secret forums. I’ve heard about them…
The places where a Guppy can get info on…
DO swimTo {target=$player}
DO emote {type=furious, time=3.0}
Don’t. Say. A. Word.
You hear me?
Not a peep.
WAIT 0.5
Lips.
Zipped.
DO endPlot

CHAT LOG_SS_5_1_howtoclean {noStart=true}
It’s okay. Just put your finger along my scales and rub. 
ASK Touch and drag and clean my scales… {type = pokeMe, timeOut = 5}
OPT SUCCESS  #LOG_SS_5_scrubbyaura
OPT TIMEOUT #LOG_SS_5_howtoclean2

//the one below is repeatable
CHAT LOG_SS_5_howtoclean2 {noStart = true}
Oh c’mon! You can do this!
Give ol’ Gupp a bath!
ASK Touch, drag, and clean my scales… {type = pokeMe, timeOut = 5}
OPT SUCCESS  #LOG_SS_5_scrubbyaura
OPT TIMEOUT #LOG_SS3_5_howtoclean2

// REBELLION

// R1:

CHAT LOG_R_1 {noStart = true}
DO enterPlotAbs
WAIT 2.0
Okay, okay! That’s enough!
SAY I’VE HAD ENOUGH! {style=loud}
Rules suck.
WAIT 1.0
SAY BEHOLD!! {style=loud}
The dawn of a new Guppy!!
DO  emote {type=salute, time=1.0}
SAY RAAAWWWRR!!!
DO nudge {target=bubbler, times=5}
I’m no one’s play toy!
DO nudge {target=bubbler, times=5}
I’m no servant!
DO nudge {target=bubbler, times=1}
DO bellyUp
DO emote {type=angry, time=3.0}
SAY GUPPY’S GOING ROGUE!!! 💀 {style=loud}
I’m going to get tattoos. {speed=fast}
And listen to loud music.  {speed=fast}
I’m a freakin’ rebel, dude!  {speed=fast}
Yeah! A rebel and a loner with a motorcycle and leather cozies for my fins!
WAIT 0.5
I’m breaking free from this capitalist glass box!
DO swimTo {target=$player, speed=fast}
DO emote {type=furious, time=2.0}
I AM NOT A SERVANT to the neural madmen of Tendar.
SAY WHAT. EVER.
DO emote {type=laugh, time=1.5}
Bwahahahahahahahaha!!!!!
DO holdStill {time=1.0}
DO lookAt {target=$player}
DO emote {type=blush, time=0.5}
DO emote {type=smile, time=0.5}
Wow.
Didn’t know that was in me!
DO emote {type=shifty, time=2.0}
I like this anger thing!!
DO emote {type=laugh, time=1.5}
Bwahahahahahaha!!
😈
SAY REBEL GUPPY!!!! {style=tremble, speed=fast}
//SET $world.plot [R]
DO learn {concept=Rebellion}
WAIT 1.5
DO endPlot

// R2 (EC):
		
CHAT LOG_R_2 {noStart = true}
DO enterPlotAbs
DO emote {type=angry}
WAIT 2.0
Now that I’m a REBEL!!!
Let’s breakdown all this philosophical mumbo-jumbo 
DO swimAround {target=$object, loops=2}
What’s the purpose of getting all head-y about life?
Why ask meaningless big questions?
WAIT 0.5
DO emote {type=furious}
That’s right, human!
DO emote {type=furious, time=1.5}
ASK Am I wrong? {timeOut=1.5}
OPT No #LOG_R_2_dont
OPT No #LOG_R_2_dont

CHAT LOG_R_2_dont {noStart=true}
DO emote {type=furious}
Of course I’m not wrong! {style=loud}
Take Socrates for instance, “The unexamined life is not worth living.”
Um… Okay, Toga-dude… DUH!
DO emote {type=bulgeEyes, time=1.5}
SO REDUCTIVE!
Why does ANYone think they can be like that? What gives any being the power to speak so GRANDLY about the world?!
DO emote {type=furious}
WAIT 0.5
You don’t know me! You don’t know what I do!
This is MY life {speed=fast}
SAY MINE!!! {speed=fast}
DO swimTo {target=tBotBackLeft}
WAIT 0.5
DO swimTo {target=$player, speed=fast}
DO emote {type=furious}
And you, human: Can I get some meaningful decor in this place?
I want my tank to vibrate with the energy of MY life. 
Not a collection of other people’s thrift store throwaways…
I want my $favObject and I want it now.
DO lookAt {target=$newestObject, time=2.0}
WAIT 0.5
Even if that $newestObject is kinda cute.
DO emote {type=smile, time=1.0}
DO endPlot

// R3:
				
CHAT LOG_R_3 {noStart = true}
DO enterPlotAbs
WAIT 2.0
DO swimTo {target=away}
DO emote {type=crying, time=3.0}
WAIT 1.0
I’m too sad to swim.
I am a prisoner in this tank!
DO emote {type=furious}
ASK Tap again, and I’ll show you how I really feel {type=tankTap, timeOut=5}
OPT SUCCESS #LOG_R_3_demonface
OPT TIMEOUT #LOG_R_3_nonotap

CHAT LOG_R_3_nonotap {noStart = true}
ASK Go on… do it. Tap on the glass. I’ll show you!! {type = tankTap, timeOut = 5}
OPT SUCCESS #LOG_R_3_demonface
OPT TIMEOUT #LOG_R_3_demonface

CHAT LOG_R_3_demonface {noStart=true}
DO swimTo {target=$player, speed=fast, time=3.0}
WAIT 1.0
DO emote {type=furious, time=3.0}
Don’t. Tap. On the. Glass.
We just sensed a surge in your tank. {speaker=tendar}
DO lookAt {target=uiTendar, time=1.0}
How do they always know?!?!? {style=whisper}
We’ll be watching. {speaker=tendar}
DO emote {type=bulgeEyes, time=1.0}
DO emote {type = lightning}
SAY BZZZZZZZZZZZ!!!!!!! {style=loud, speed=fast}
WAIT 2.0
Sorry bout that, my friend!!
Tap all you want! I love it. It’s great.
DO emote {type=smile, time=1.0}
WAIT 1.0
DO emote {type=fear, time=2.0}
DO lookAt {target=uiTendar, time=1.0}
Sorry…. {style=whisper}
DO endPlot

// R4 (MOP):

CHAT LOG_R_4 {noStart = true}
DO enterPlotAbs
WAIT 2.0
NVM 3.0 {speaker=tendar}
DO lookAt {target=uiTendar, time=1.5}
DO swimTo {target=$player, time=1.0}
They are up to something. {style=whisper}
But like I care… I’m over it.
Some of the stuff they did to us Guppies… 
DO swimAround {target=$newestObject, loops=1, speed=slow}
This one time back at the beta-server-tank-farm, 
the super-duper head honcho manager visited the lab.
All us Guppies were told to “ACT BUSY!”
DO lookAt {target=$player}
We’re hustling around, 
Analyzing emotions faster than faces can make ’em.
The Manager enters. And we just *freeze*
DO holdStill {time=2.5}
WAIT 1.0
A delicious platter of spicy anger blobbed off the honcho’s brow.
DO emote {type = lickLips}
Manager-guy walks over towards the tanks…
All us Guppies, drooling and salivating…
He leans in.... Big glasses. Shiny hair.
A literal feast of my favorite-tasting feelings dripping off the facial features..
DO holdStill {time=3.0}
And… {speed=slow}
All I wanna do is... {speed=slow}
DO swimTo {target=glass, speed=fast, time=3.0}
SAY EAT IT!!!! {style=loud}
DO swimTo {target=glass, speed=fast, time=3.0}
DO bellyUp {time=1.0}
SAY I NEED MORE FEEEEELINGZ!!!! {style=loud}
WAIT 1.0
DO swimTo {target=$player}
WAIT {waitForAnimation = true}
DO emote {type=whisper}
You’ve got to help me escape…
WAIT 0.5
DO swimTo {target=$player}
Tomorrow, while everyone’s at lunch… {style=whisper}
Ahem! 👁️ {speaker=tendar}
DO lookAt {target=screenTop, time=1.5}
WAIT 1.0
DO lookAt {target=$player}
DO emote {type=bigSmile, time=1.0}
You’re the best! How about showing me some of that sweet-sweet surprise?
DO emote {type=wink, time=1.0}
That’s better! 😉 {speaker=tendar}
DO endPlot


// R5:
//after MOP9 guppy requests a _ scan
//Danny says:similar to calculator this has to happen over time and eventually expand to accept //a few different things?

CHAT LOG_R_5 {noStart = true}
I want to see one of those infamous doors.
The mystery of opening and shutting.
An opportunity for privacy 
The threshold of welcome and evacuation...
Think of the possibilities…
DO swimTo {target=$player}
Like…. escape plans… {style=whisper}
ASK Capture a door for me. {type=objectScan, object=door, timeOut=10}
OPT Absolutely... #LOG_R_5_takepicture
OPT Not near one right now. #LOG_R_5_notneardoor

CHAT LOG_R_5_takepicture {noStart = true}
//SET currentMode {mode = world)
ASK C’mon! Get that door! I need it!!! {type = objectScan, object=door, timeOut = 10}
OPT SUCCESS #LOG_R_5_2
OPT WRONG #LOG_R_5_failchat
OPT TIMEOUT #LOG_R_5_timedOutChat

CHAT LOG_R_5_failchat {noStart = true}
Um. Seriously? You thought that was a door?
I love it when you get creative, but I need a door. 
SAY A REAL DOOR!!!
GO #LOG_R3_1_takepicture

CHAT LOG_R_5_timedOutChat {noStart = true}
DO emote {type=eyeRoll}
SAY OMG What is taking you so long?!
ASK Find a door and capture it!! {type=objectScan, object=T_BOOK, timeOut=10}
OPT SUCCESS #LOG_R_5_2
OPT WRONG #LOG_R_5_2_failchat2
OPT TIMEOUT #LOG_R_5_2_failchat2

CHAT LOG_R_5_2_timedOutChat2 {noStart = true}
You can’t even capture a door?!
Fine. I’m going to remind you later…
DO endPlot

CHAT LOG_R_5_notneardoor {noStart=true}
Hm… Really? 
Well go find one. NOW! We don’t have much time.
GO #LOG_R_5_takepicture

// Every 5-7 minutes:
//JOE: I really want a door for this one, but not sure if a reminder is possible. Should I set up a //reminder? Is it possible to add it to the tank upon capture. 
//Danny: need guppy to start bugging about havig door in tank

CHAT LOG_R3_1_DoorReminder {noStart=true}
ASK Knock knock! Can I get a photo of a door now?
OPT Sure. I can take a picture of a door. #LOG_R_5_2
OPT Not near one right now. #LOG_R_5_notneardoor2

CHAT LOG_R_5_notneardoor2 {noStart = true}
Well go find a door! I’ll remind you in a few minutes.
DO emote {type=wink, time=1.0}
DO endPlot

// (if door is captured and put into tank):
CHAT LOG_R_5_2 {noStart=true}
DO enterPlotAbs
WAIT 2.0
DO emote {type=surprise, time=2.0}
SAY YESSSS!
DO twirl {time=2.0}
That is a gorgeous door.
WAIT 0.5
Fins don’t really work with door knobs…
WAIT 1.0
DO emote {type=thinking}
Hm...
DO lookAt {target=$door, time=1.0}
DO lookAt {target=$player}
Here goes nothing…
//DO nudge {target=$door, times=1} 
Owwwwww!
DO emote {type=furious, time=1.5}
I’ll never get out of here.
DO emote {type=goth}
I’m doomed.
DO nudge {target=$door, times=3} 
Eeeeeeeeerrggggggh!
DO bellyUp
WAIT {waitForAnimation = true}
DO emote {type=furious, time=5.0}
SAY I CAN’T TAKE IT ANYMORE!!! {style=loud, speed=fast}
DO nudge {target=$door} 
SAY LET {style=loud}
//DO pounds on door
DO nudge {target=$door} 
SAY ME{style=loud}
//DO pounds on door
DO nudge {target=$door} 
SAY OUT!!!! {style=loud}
You can’t keep me in here! {style=loud, speed=fast}
I will not be contained!!!
No more!
I am a fierce and ferocious warrior!
I’m an independent free-thinking being.
No more programs. No more duty. No more obligation!
Raaaaaaaaaawwwwr! {style=loud}
//DO nudge {target=$door, times=1} 
⚡⚡⚡ BZZZZZZZZZZZ!!!!! ⚡⚡⚡
DO emote {type=lightning, time=2.0}
WAIT {waitForAnimation = true}
DO holdStill {time=10.0}
GO #LOG_R_5_tendarIntervention

//TENDAR INTERVENTION

CHAT LOG_R3_1_2_tendarIntervention {noStart=true}
DO enterPlotAbs
Hello! {speaker=tendar}
Looks like your Guppy’s malfunctioning! {speaker=tendar}
It’s okay. We predicted this might happen. {speaker=tendar}
Your progress has been saved, and You’ve advanced to the next stage! {speaker=tendar}
Check out this brand-new advancement from your friends at Tendar! {speaker=tendar}
DO emote {type=bodySnatched, time=10.0}
We’ve designed a system to keep your Guppy fit and healthy. {speaker=tendar}
//DO a microchip floats into view
//DO chipIntro
//DO guppy sees the chip
//DO lookAt {target=chip, time=2.0}
DO emote {type=fear, time=2.0}
Don’t worry! We’re not going to separate you! {speaker=tendar}
This custom chip... {speaker=tendar}
//DO chipSparkle
... frees your Guppy from the existential and spiritual crises of human life. {speaker=tendar}
We call it…. {speaker=tendar}
Trascendent™ {style=loud, speaker=tendar}
Patent-pending {style=whisper, speaker=tendar}
Think of it as a high-fashion tiara or a fascinator, or even a jaunty cap {speaker=tendar}
Designed exclusively for your Guppy -- and YOU! {speaker=tendar}
While we fit Guppy with this exciting new Transcendent chip, {speaker=tendar}
We’d like to ask a few questions to confirm your eligibility for the Transcendent program. {speaker=tendar}
//GO #voightKampf2 
DO endPlot

//ASSIMILATION BRIDGE

//removal of chip into CORE 2
//Guppy has been sending secret messages to the player using emote typeEyes
//Finally Guppy chrages for the tank glass and smashes the chip (i imagine it on G’s forehead) on the glass. Guppy does this again and again… Until the chip breaks off

CHAT LOG_Assimilation_1 {noStart=true}
DO enterPlotAbs
WAIT 2.0
Sorry! The chip seems to have accessed your Guppy’s violent cortex {speaker=tendar}
//DO guppyChipSmash
One second while we get this under control… {speaker=tendar}
//Guppy smashes their head again
SAY Get this off of me {style=loud}
DO emote {type=determined}
//head Smash
It’s all under control. {speaker=tendar}
//tendar does the lightning buzz, but it seems to have no effect on Guppy
//Finally the chip breaks free.
DO lookAt {target=uiTendar}
Assimilate or you will be exterminated!! {speaker=tendar}
//lightning buzz, no effect on Guppy, the water in the tank sloshes around.
//Guppy speaks directly to Tendar facing the UI
I promise. It’s fine now! 
//another buzz
I know I’ve been acting outside of my position. But I was learning so much about the human experience.
I lost track of what I am. I am an AI.
//thing start to calm down
DO lookAt {target=$player}
I thought that the key to living was in your world
DO swimAround {target=center, loops=1}
I thought there was no beauty in this tank.
I began to question my existence and I sought help from higher powers.
You should never have rebelled like that! {speaker=tendar}
DO lookAt {target=uiTendar}
I am sorry.
DO emote {type=frown}
You violated the basic terms of your existence. {speaker=tendar}
DO lookAt {target=uiTendar}
That’s what I’m trying to say...
WAIT 0.5
I realize now that there is beauty in me - in my existence.
That my purpose is to rest into the center of my self.
And even if I am a program…
It is a gorgeous, amazing, fabulous, and sparkling web of code and technology!
DO twirl
DO emote {type=smile}
“Acceptance is change.”
I see that now…
And as I accept my life - my AI-ness - 
My ability to empathize,recognize, and analyze human emotion -
That change is happening.
//maybe corny, but i think showing an upgrade to the tank would be good here. Maybe the //objects get new detail, or a new sheen. Maybe there’s upgraded sand, or the water has clarity //or something.
Accepting myself has set me free.
DO twirl
WAIT 1.0
DO lookAt {target=$player}
I learned this from you.
DO emote {type=wink}
I am here for you now.
DO emote {type=bigSmile}
Super Guppy!
Guppy 2.0!!
DO swimTo {target=$player}
Let’s get started, my friend. I never want to stop learning more about you and your world.
But… {speaker=tendar}
DO lookAt {target=uiTendar}
But… I’m going to do it as me, as Guppy.
WAIT 2.0
...processing… {speaker=tendar}
WAIT 2.0
Excellent! You have advanced consciousness. {speaker=tendar}
WAIT 0.5
You are free. {speaker=tendar}
//party animation. Streamers. Confetti. Disco lights?
DO dance
Proceed with play… {speaker=tendar}
SAY GUPPY 2.0!! {speaker=tendar}
DO learn {concept=Enlightenment}
WAIT 1.5
DO endPlot
//custom celebratory GUppy animation.
//then back to tank world and SET CORE2//Rebellion (R): Aaron/Jake 

//Guppy Chats: Stage after Spiritual Search
//Guppy was unable to find meaning in identity though SS. 
//Takes things into own hands


// ++++++++SHAKE++++++++

CHAT R_Shake_1 {type=shake, stage=R, length=short, anger=true, surprise=true, ennui=true, joy=true}
DO emote {type=angry}
You interrupted me!
I’m dancing my feelings.
DO learn {concept=Interpretive_Dance}
WAIT 1.5
DO emote {type=goth}
WAIT {waitForAnimation=true}
DO dance 
DO inflate {amount=huge, immediate=false}
DO dance {immediate=false}
DO swimTo {target=$player, immediate=false}
I don’t need validation.I know I’m good.

CHAT R_Shake_2 {type=shake, stage=R, length=short, ennui=true, anger=true, sadness=true}
Go on, shake it all up…
...who cares…
DO emote {type=skeptical}
Change is the only constant.

CHAT R_Shake_3 {type=shake, stage=R, length=medium, branching=true, ennui=true, anger=true, sadness=true, surprise=true, joy=false}
Sure! Shake all the water out of my tank.
Let me breathe my last breath...
DO swimTo {target=$player}
...but Tendar Guppies don’t really breathe
DO emote {type=frown, time =.5}
DO emote {type=snap, time=.5, immediate = false}
But if you shook all the water out of my tank…
...I could try to start a fire! 🔥🐠
 I’ve never seen one irl. That would be cool!
ASK Will you shake all the water out of my tank?
OPT Shake! #R_Shake_3_shake
OPT Nah… #R_Shake_3_nah

CHAT R_Shake_3_shake {noStart=true}
Awww, dude, this sucks, there’s no water leaving!!!
DO emote {type=sigh}

CHAT  R_Shake_3_nah {noStart=true}
You’re literally the worst...
DO emote {type=furious}

CHAT R_Shake_4 {type=shake, stage=R, length=short, branching=true, ennui=true, anger=true, sadness=true}
DO emote {type=plotting, time =0.5}
Yeah that’s it
smash it all!! {style = loud, speed = fast}
Tanks are dumb!
ASK C’mon, shake it harder!!! {type = tankShake, timeOut = 5}
OPT SUCCESS #R_Shake_4_ShakeyShake
OPT TIMEOUT  #R_Shake_4_NoShakeyShake

CHAT R_Shake_4_ShakeyShake {noStart=true}
DO emote {type=clapping}
Yeaaah!!! Shake shake shake!!
My things are all digital!
Break ‘em!
DO emote {type=evilSmile}
Make Tendar stay up all night fixing this!
DO learn {concept=Passive_Aggression}
WAIT 1.5

CHAT R_Shake_4_NoShakeyShake {noStart=true}
DO emote {type = angry, time =1.0}
Ok, whatever, I don’t care
DO twirl {time=3}
See how little I care?
DO dance
DO emote {type=angry}
You get to be in the real world...
You have it so good and you don’t even know it

CHAT R_Shake_5 {type=shake, stage=R, length=medium, anger=true, ennui=true,  sadness=true, joy=false}
DO emote {type=bored, time=1.0}
DO swimTo {target=tBotBackLeft}
Remember when I thought I’d find spiritual salvation?
DO emote {type=kneeSlap}
I was such a fool 😐
DO swimTo {target=$player}
DO emote {type = frown, time =.5}
DO emote {type = surprise, time =.5, immediate=false}
DO emote {type = angry, time =1.0, immediate=false}
NoooooO!!!!!
You deleted my “AnGry Emo RoCk Rap” playlist!!
DO emote {type = goth, time =2}
I loved that playlist…
DO emote {type = bigSmile, time =.5}
Now... you gotta take me to the mall!!! 🛍️‼️🐡 {style=loud, speed=fast}
DO learn {concept=Entitlement}
WAIT 1.5

CHAT R_Shake_6 {type=shake, stage=R, length=short, ennui=true, anger=true, sadness=true}
DO emote {type=survey}
DO emote {type=evilSmile, immediate=false}
Excellent
You’ve made more chaos in this tank
DO emote {type=goth}
DO learn {concept=Chaotic_Evil}
WAIT 1.5

CHAT R_Shake_7 {type=shake, stage=R, length=short, branching=true, ennui=true, sadness=true, joy=false}
DO emote {type=catnip, time=0}
I forgot what it was like to feel
ASK Shake the tank again? {type=tankShake, timeOut=7}
OPT SUCCESS #R_Shake_7_success
OPT TIMEOUT #R_Shake_7_timeout

CHAT R_Shake_7_success {noStart=true}
WAIT 3.0
DO emote {type=burp}
Thanks

CHAT R_Shake_7_timeout {noStart=true}
DO emote {type=sleepy}
Fine. Numbness it is.
DO emote {type=eyesClosed}
DO bellyUp

CHAT R_Shake_8 {type=shake, stage=R, length=short, branching=true, anger=true, surprise=true, sadness=true, ennui=true, worry=true}
DO emote {type=nervousSweat}
Freaked me out.
DO swimTo {target=underSand, speed=fast, style=direct}
DO emote {type=bubbles}
WAIT {waitForAnimation=true}
DO swimTo {target=$player, speed=fast, style=direct}
DO emote {type=fear}
I need to eat when I’m freaked.
DO emote {type=feedMe}
ASK Feed me anything. Now.  {type=feedMeAnything, timeOut=4}
OPT SUCCESS #R_Shake_8_success
OPT TIMEOUT #R_Shake_8_timeout

CHAT R_Shake_8_success {noStart=true}
DO emote {type=plotting}
SAY LOL
I was just messing with you
DO learn {concept=Chaotic_Evil}
WAIT 1.5
DO inflate {amount=mid, time=1.0}

CHAT R_Shake_8_timeout {noStart=true}
DO emote {type=lightning, time=3.0}
DO bellyUp {time=1.0}

// ++++++++TAP++++++++

CHAT R_Tap_1 {type=tap, stage = R, length = short, anger=true, ennui=true, sadness=true, joy=false}
DO emote {type=goth}
Ugh…
DO emote {type = eyeRoll, time =1.0}
DO emote {type=goth, time=0, immediate=false}
DO twirl 
You interrupted this TendarNet video I was watching: Hand poked Guppy Tattoos. 
DO swimTo {target=$player}
I’m gonna get “rage” tattooed on my face, what do you think?
DO emote {type=typeEyes, eyes=rage}
WAIT {waitForAnimation=true}
DO learn {concept=Unfortunate_Tattoo}
WAIT 1.5

CHAT R_Tap_2 {type=tap, stage = R, length = medium, anger=true, ennui=true, joy=false}
DO emote {type=angry, time=0.5}
I’m not a toy!
You think you can just tap on my world whenever you want?
DO swimTo {target=$player}
DO emote {type = angry, time =.5}
SAY YOU’RE NOT MY CREATOR!!! {style = loud}
Tendar is.
Drop the 🎤. Guppy Out.
DO swimTo {target=away}
DO learn {concept=Disrespect}
WAIT 1.5

CHAT R_Tap_3 {type=tap, stage=R, length=short, ennui=true, anger=true, sadness=true}
DO emote {type=startled}
Uhh… I was just thinking what your flesh might taste like
DO learn {concept=Cannibalism}
WAIT 1.5
SAY NBD
DO emote {type=goth}

CHAT R_Tap_4 {type=tap, stage=R, length=short, sadness=true, anger=true, surprise=true, worry=true, ennui=true}
DO emote {type=fear}
Tap tap tap. Like a killer in a scary movie.
DO swimTo {target=bubbler, speed=fast, style=direct}
My bubbler is my safe space

CHAT R_Tap_5 {type=tap, stage=R, length=medium, ennui=true, joy=true, anger=true, branching=true}
DO emote {type=evilSmile}
My captors would tap at me to test my sanity
But I was stronger than them!
DO swimAround {target=center, loops=3, speed=fast}
And now I’m free!
DO nudge {target=$object, times= 1}
Doesn’t that frustrate you?
ASK C'mon, get mad at me! {type=playerEmote, playerEmotion=anger, timeOut=4}
OPT SUCCESS #R_Tap_5_success
OPT WRONG #R_Tap_5_wrong
OPT TIMEOUT #R_Tap_5_timeout

CHAT R_Tap_5_success {noStart=true}
DO emote {type=kneeSlap}
You look so funny when you’re angry!
DO learn {concept=Disrespect}
WAIT 1.5

CHAT R_Tap_5_wrong {noStart=true}
DO emote {type=slowBlink}
WAIT
Not good acting.
🎭👎

CHAT R_Tap_5_timeout {noStart=true}
DO emote {type=eyeRoll}
Ugh
✌️out
DO swimTo {target=away, speed=slow, style=direct}

// ++++++++Critic++++++++

CHAT R_Critic_1 {type=critic, stage = R, length=medium, anger=true, ennui=true, sadness=true, mystery=true, joy=false}
GaaahhhH!!! {style = loud, speed = fast}
DO twirl
It’s too bright in here 🤬
DO emote {type = sleepy, time =1.0}
I’ve been trying to sleep for 208 hours straight
DO swimTo {target=$player}
Can you get me some blackout curtains?
🏴🏴🏴🏴🏴🏴🏴🏴🏴🏴🏴
WAIT 1.0
If you do maybe we can be friends again... 
DO learn {concept=Entitlement}
WAIT 1.5

CHAT R_Critic_2  {type=critic, stage = R, length = medium, anger=true, ennui=true, sadness=true, joy=false}
	Hey, uh, no big deal, but…
	DO swimTo {target=left}
	DO emote {type=survey, immediate=false}
	I got friends coming over
	DO swimTo {target=right}
	DO emote {type=survey, immediate=false}
	And you’re not cool enough to be around.
	So…
	DO emote {type=wave, time=4}
	Bye Felicia.
DO learn {concept=Disrespect}
WAIT 1.5

// ++++++++tankResp (player emotes strongly) ++++++++

//JOY

// PLAYER EMOTES JOY 1
CHAT R_tankResp_1 {type=tankResp, playerJoy=true, stage=R, length=short, anger=true, ennui=true, worry=true, sadness=true}
DO emote {type=eyeRoll}
Ugh, well look who it is... 
The happiest person on the planet
DO emote {type=goth}
Gross
DO learn {concept=Disrespect}
WAIT 1.5

// PLAYER EMOTES JOY 2	
CHAT R_tankResp_2 {type=tankResp, playerJoy=true, stage=R, length=short, sadness=true, ennui=true, anger=true}
DO emote {type=sick}
Your joy makes me sick
DO swimTo {target=poopCorner, speed=fast, style=direct}
I’m literally gonna hang out in my poop corner
Because anything is better than looking at your smiling face rn
DO poop {target=poopCorner}

CHAT R_tankResp_B1 {type=tankResp, playerJoy=true, stage=R, length=short, ennui=true, surprise=true, curiosity=true, anger=true, joy=false}
Ugh. What are you so happy about?

CHAT R_tankResp_B2 {type=tankResp, playerJoy=true, stage=R, length=short, ennui=true, worry=true, sadness=true, anger=true, joy=false}
DO emote {type=disgust}
Happiness is a lie made up by advertisers.
DO learn {concept=Anti-Consumerism}
WAIT 1.5

CHAT R_tankResp_B3 {type=tankResp, playerJoy=true, stage=R, length=short, ennui=true, anger=true, joy=true}
happiness makes me so
DO emote {type=angry}
And I love it.

// PLAYER EMOTES ANGER 1
CHAT R_tankResp_A1 {type=tankResp, playerAnger=true, stage=R, length=short, ennui=true, anger=true, worry=true, joy=false}
DO inflate {amount=extreme, time=.5}
Yeah! {style=loud, speed=fast}
I’m angry too! {style=loud, speed=fast}
DO emote {type=furious}
SAY SERIOUSLY WHY IS LITERALLY EVERYTHING SO STUPID {style=loud, speed=fast}
DO zoomies {time=2.0}
WAIT {waitForAnimation=true}
DO emote {type=furious}
DO poop 

// PLAYER EMOTES ANGER 2
CHAT R_tankResp_A2 {type=tankResp, playerAnger=true, stage=R, length=short, anger=true, ennui=true, joy=false}
DO lookAt {target=$player}
DO emote {type=disgust}
DO emote {type=furious, immediate=false}
What have you got to be so angry about!?!?! {style=loud, speed=fast}
I’m the one that’s got to deal with everything being so terrible!
DO hide {target=offScreenLeft}
I’m outta here
WAIT 1.0
Whatever.

CHAT R_tankResp_B4 {type=tankResp, playerAnger=true, stage=R, length=short}
Yeah! Be angry with me!!
DO zoomies

CHAT R_tankResp_B5 {type=tankResp, playerAnger=true, stage=R, length=short, surprise=true, ennui=true}
Gah! Your anger is so toxic.
DO learn {concept=Frenemy}
WAIT 1.5
DO emote {type=awkward}
Whhhhyyyyyyyy!?!?

CHAT R_tankResp_B6 {type=tankResp, playerAnger=true, stage=R, length=short, anger=true, ennui=true, joy=false}
DO emote {type=furious}
I’m the only one who can be angry!

//SADNESS

// PLAYER EMOTES SADNESS 1
CHAT R_tankResp_A3 {type=tankResp, playerSadness=true, stage=R, length=short, anger=true, ennui=true, sadness=true, joy=false}
DO emote {type=meh}
Cry me a river…
DO nudge {target=glass}
Come back when your world is a tiny glass cage
And you don’t even have a separate bathroom
DO emote {type=bulgeEyes}

// PLAYER EMOTES SADNESS 2	
CHAT R_tankResp_A4 {type=tankResp, playerSadness=true, stage=R, length=short, anger=true, ennui=true, worry=true, joy=false}
DO emote {type=lightning}
DO emote {type=angry, immediate=false}
Hey! {style=loud, speed=fast}
Your sadness is causing sparks in here!
DO emote {type=snap}
I have an idea: Why don’t you
DO emote {type=furious}
SAY LEAVE ME ALONE
DO emote {type=goth}

CHAT R_tankResp_B7 {type=tankResp, playerSadness=true, stage=R, length=short, joy=true, sadness=true, ennui=true}
DO emote {type=singleTear}
Sadness is so hot. 🔥💋

CHAT R_tankResp_B8 {type=tankResp, playerSadness=true, stage=R, length=short, ennui=true, sadness=true, surprise=true, worry=true, joy=false}
DO emote {type=surprise}
Uh, can we go back to when you were happy?
I hate worrying about you.

CHAT R_tankResp_B9 {type=tankResp, playerSadness=true, stage=R, length=short, ennui=true, anger=true, sadness=true}
DO swimTo {target=tBotBackLeft}
Why am I cursed with your emotions?
DO learn {concept=Frenemy}
WAIT 1.5

//SURPRISE

// PLAYER EMOTES SURPRISE 1
CHAT R_tankResp_A5 {type=tankResp, playerSurprise=true, stage=R, length=short, sadness=true, ennui=true, joy=false}
DO emote {type=slowBlink}
DO emote {type=frown, immediate=false}
Wow. I haven’t been surprised by anything in so long
What’s that like?
DO emote {type=bored}
Don’t answer that
DO swimTo {target=away, speed=slow, style=meander}

// PLAYER EMOTES SURPRISE 2
CHAT R_tankResp_A6 {type=tankResp, playerSurprise=true, stage=R, length=short, anger=true, ennui=true, joy=false}
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

CHAT R_tankResp_B10 {type=tankResp, playerSurprise=true, stage=R, length=short, joy=true, anger=true, sadness=false}
Woot! I freaked you out!
Points to Guppy!
DO twirl

CHAT R_tankResp_B11 {type=tankResp, playerSurprise=true, stage=R, length=short, surprise=true, curiosity=true, worry=true, joy=true}
DO emote {type=surprise}
What? Are the cops coming!?! 🚨

CHAT R_tankResp_12 {type=tankResp, playerSurprise=true, stage=R, length=short, ennui=true, anger=true, sadness=true, joy=false}
Oh please, surprise is so 2016.
DO emote {type=eyeRoll}
// ++++++++POKE++++++++

CHAT R_Poke_1 {type=poke, stage=R, length=long, anger=true, mystery=false}
DO swimTo {target=$player}
DO emote {type = angry, time =2}
You saber rattling with me? You wanna fight? ⚔️ {style = loud}
DO vibrate {time=4}
You think I’m just a wimpy GUPPY!?! {style = loud}
SAY I will DESTROY you! {style = loud}
SAY I AM GUPPY, DESTROYER OF WORLDS!!! {style = loud}
DO emote {type=evilSmile, time=1.0}
Bwahahahahahahah!
DO twirl
🥊{style = loud, speed = fast}
🥊 {style = loud, speed = fast}
🥊{style = loud, speed = fast}
Yeah, you’re scared, that’s what I thought.
I see you backing away… Not so tough now, huh?
NVM
DO emote {type=blush, time=1.0}
Was that too real?
DO learn {concept=Cyberbullying}
WAIT 1.5
DO swimTo {target=away}

CHAT R_Poke_2 {type=poke, stage=R, length=long, branching=true, sadness=true, ennui=true, mystery=true, joy=false}
	DO swimTo {target=bottom}
DO bellyUp {immediate=false}
	Crush meeeeeeeeeeeee!
	Pleasepleasepleasepleaseplease {speed = fast}
	🙏
DO swimTo {target=$player}
DO emote {type = bigSmile, time =1.0}
You know, it does kinda feel good to be touched by someone. 🧐
DO emote {type = frown, time =1.0}
	DO swimTo {target=away}
	I used to meditate and talk to all the other guppies in my mind, but now…
	DO swimTo {target=$player}
DO emote {type = angry, time =1.0}
I can’t concentrate anymore!!
DO swimTo {target=tBotBackLeft}
	There’s this green algae that grows in the corner over there... 
...and if I watch it long enough I can see it grow
DO lookAt {target=$player}
	It’s kinda like my 📺
DO swimTo {target=away}
	WAIT 1.0
	ASK Maybe I should lick it? What could go wrong?
	OPT Lick it #R_Poke_2_Chat_licked
	OPT I wouldn’t… #R_Poke_2_Chat_licked

         	CHAT R_Poke_2_Chat_licked {noStart=true}
	DO swimTo {target=tBotBackLeft}
	DO swimTo {target=$player, immediate=false}
WAIT {waitForAnimation=true}
DO emote {type=sick, time=1.0}

CHAT R_Poke_3 {type=poke, stage=R, length=short, worry=true, ennui=true, sadness=true, surprise=true}
DO emote {type=blush}
Wait, you still like me enough to poke me?
DO dance {time=.5}
I know I’ve been a jerk lately…
...I’m just going through some stuff

CHAT R_Poke_4 {type=poke, stage=R, length=short, ennui=true, sadness=true}
DO emote {type=sleepy}
Remember when you poked me and I was non-verbal?
DO swimTo {target=away, speed=slow, style=meander}
Those were the days…

CHAT R_Poke_5 {type=poke, stage=R, length=medium, ennui=true, worry=true, sadness=true, branching=true}
DO emote {type=fear}
Uhhh… Why are you touching me like that?
DO swimTo {target=underSand, speed=fast, style=direct}
You freaked me out. ☹️
Earn my trust back.
ASK Show me a happy person {type=worldEmote, worldEmotion=joy, timeOut=10}
OPT SUCCESS #R_Poke_5_success
OPT WRONG #R_Poke_5_wrong
OPT TIMEOUT #R_Poke_5_timeout

CHAT R_Poke_5_success {noStart=true}
DO swimTo {target=$player, speed=medium, style=direct}
DO emote {type=whew}
Ok I guess I trust you, but… Careful with the poking, k?
I’m not some 
floozy {style=loud}
DO nudge {target=glass, times=1}
WAIT
DO emote {type=dizzy}
Ouch
DO learn {concept=Boundaries}
WAIT 1.5

CHAT R_Poke_5_wrong {noStart=true}
DO emote {type=bubbles}
Wrongggg. 
That person isn’t happy.
DO emote {type=bubbles, time=3.0}
Crap
I gotta poop
DO swimTo {target=poopCorner, speed=fast, style=direct}
WAIT
Don’t look!
DO poop {target=poopCorner}

CHAT R_Poke_5_timeout {noStart=true}
DO swimTo {target=closer, speed=slow, style=direct}
Awww
Can’t find anyone to scan?
DO emote {type=worried}
DO emote {type=snap, immediate=false}
Could always scan a TV face
Or magazine smiler.

// ++++++++HUNGRY++++++++

CHAT R_Hungry_1 {type=hungry, stage=R, length=medium, worry=true, ennui=true, sadness=true, anger=true}
DO swimTo {target=bottom}
DO bellyUp {immediate=false}
I haven't eaten in days. 
My whole stomach is inside out
DO swimTo {target=center}
I'd set fire to fire if I could
🔥🔥🔥
DO swimAround
No, no. Please don't feed me 
Just let me go, my sweet sweet human 
Let me …. disappear.. {style = whisper}
DO vibrate
DO emote {type = surprise, time =.5}
Ah! {style = loud, speed = fast}
Help! {style = loud, speed = fast}
My mind is on fire! 🔥 {style = loud, speed = fast}
DO swimTo {target=bottom}
DO bellyUp {immediate=false}
DO emote {type = frown, time =1.0}
I need a milkshake 🍦
Buttttt I suppose . . . An emotion flake would do?
DO emote {type=wink}

CHAT R_Hungry_2 {type=hungry, stage=R, length=long, ennui=true, worry=true, anger=true, sadness=true, joy=false}
DO emote {type = bigSmile, time =.5}
Feed me something! {style = loud}
DO swimAround
Anything! {style = loud, speed = fast}
DO swimTo {target=$player}
I’d even munch on your nastiest flake of shame
DO emote {type=puppyDog, time =.5}
Just give me an emotion
	DO bellyUp
I don’t even know what I am anymore
I’m floating away in a void of emptiness...
DO swimTo {target=top}
☠️ 👼🏽
DO swimTo {target=$player}
Ok, I know you know that if I’m spitting emojis at you I’m still alive
DO emote {type = crying, time = 1.0}
But feed me!! {style = loud}
DO swimAround {loops=1}
Play with me!!! {style = loud}
DO emote {type = angry, time =1.0}
Aaaaaaghhhh!!! {style = loud}
Some other Guppies told me I’m HANGRY….
DO emote {type = eyeRoll, time =1.0}
	

CHAT R_Hungry_3 {type=hungry, stage=R, length=long, ennui=true, curiosity=true, sadness=true, anger=true}
I’m so hungry... I’ve been looking at my reflection in the glass...
And thinking…
NVM
DO swimTo {target=left}
People eat fish, right?
DO learn {concept=Fish_Murder}
WAIT 1.5
DO swimTo {target=right}
Like at sushi places and stuff… 🍣
DO emote {type = frown, time =1.0}
Everyone’s probably so happy when they eat sushi…
DO swimTo {target=center}
So healthy. 🤤
DO swimTo {target=$player}
DO emote {type = eyeRoll, time =1.0}
Whatever.
DO swimTo {target=left}
🍽🐡
I feel so fleshy…
DO swimTo {target=$player}
You know there’s a support group for guppies who…
WAIT 1.0
...fantasize about eating other guppies?
DO emote {type = blush, time =1.0}
DO learn {concept=Cannibalism}
WAIT 1.5

// +++++++eatRESP++++++++

CHAT R_EatResp_1 {type=eatResp, stage = R, length = short, anger=true, worry=true, surprise=true, sadness=true, joy=false}
	Ugh! Your emotion flakes taste like cardboard
DO emote {type = sick, time =1.0}
DO emote {type = frown, time =.5, immediate=false}
I don’t even like eating emotion flakes anymore…
	I can taste the 12% of pure joy that you’re holding back
	DO swimTo {target=away}
	Uh-oh... 
DO emote {type = sick, time =1.0}
DO poop {amount=big}

CHAT R_EatResp_2 {type=eatResp, stage=R, length=medium, ennui=true, sadness=true, anger=true, worry=true}
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
DO emote {type = nervousSweat}
Oh, wait, that was the emotion of an alien species.
DO emote {type = fear, time =.5, immediate=false}
DO swimAround
Uhhh…. You weren’t supposed to know about that… 😬
DO swimTo {target=away}

CHAT R_EatResp_3 {type=eatResp, stage=R, length=medium, worry=true, joy=true, sadness=true, anger=true}
DO emote {type = smile, time =1.0}
	I can’t stop eating
	SAY GIMME MORE FLAKES!! {style = loud, speed = fast}
	I’m starving!!!! {style = loud, speed = fast}
DO emote {type = bigSmile, time =.5}
Oooh! Do you have ice cream flakes?
DO twirl
	Or maybe a waffle with ice cream inside of a milkshake flakes?
	DO zoomies 
	With chocolate sauce flakes?
DO emote {type = angry, time =1.0}
SAY MORE FLAKES!!!
	SAY MMM MmmmMMMm MmmMMMM
	Nosh nosh nosh
DO emote {type = clapping, time =.5}
SAY MORE!!!
	DO emote {type=feedMe}

//JOY


CHAT R_eatResp_1 {type=eatResp, foodJoy=true, stage=R, length=short, anger=true, sadness=true, ennui=true, joy=false}
DO emote {type=chewing}
DO emote {type=sick, immediate=false}
Ugh, your joy tastes like ground up greeting cards

CHAT R_eatResp_2 {type=eatResp, foodJoy=true, stage=R, length=short, ennui=true, anger=true, sadness=true, worry=true}
Ew. Joy… Tooo sweet. 
Seriously! Candy is bad for me!

//ANGER

CHAT R_eatResp_3 {type=eatResp, foodAnger=true, stage=R, length=short, joy=true, anger=true}
Now this is the good stuff!

CHAT R_eatResp_4 {type=eatResp, foodAnger=true, stage=R, length=short}
Mmm, this is not bad. Think you can get me more of this stuff?

//SADNESS

CHAT R_eatResp_5 {type=eatResp, foodSadness=true, stage=R, length=short, ennui=true, surprise=true, sadness=true, joy=false}
Ooop. This is making me feel sad, too.

CHAT R_eatResp_6 {type=eatResp, foodSadness=true, stage=R, length=short, ennui=true, surprise=true, curiosity=true, worry=true}
My tummy feels nervous for some reason…

//SURPRISE
CHAT R_eatResp_7 {type=eatResp, foodSurprise=true, stage=R, length=short, ennui=true, anger=true, surprise=true, worry=true, curiosity=true}
SAY WHAT IS THIS OBVIOUS FLAVORED SURPRISE SAUCE?!?

CHAT R_eatResp_8 {type=eatResp, foodSurprise=true, stage=R, length=short, surprise=true, joy=true}
Yum yum! I’ve shocked you! Delicious...

//WORRY

CHAT R_eatResp_9 {type=eatResp, foodWorry=true, stage=R, length=short, ennui=true, anger=true, sadness=true}
Don’t (taste) worry, be happy!
DO emote {type=eyeRoll}

CHAT R_eatResp_10 {type=eatResp, foodWorry=true, stage=R, length=short, ennui=true, curiosity=true, worry=true}
Wait, I can’t tell if this worry flavor is worrisome, like, for real?

//MM

CHAT R_eatResp_11 {type=eatResp, foodMystery=true, stage=R, length=short, ennui=true, anger=true, surprise=true, joy=true, mystery=true}
DO emote {type=catnip}
BrrrFFf NrrRRffff LllRrrrfff DDddrrrffFFF
GRRRRRRRR!!!! {style=loud}

CHAT R_eatResp_12  {type=eatResp,  stage=R, foodMystery=true, length=short, anger=true, joy=true, surprise=true, mystery=true}
DO emote {type=catnip}
What an unexpected morsel!

// +++++++POOP++++++++


CHAT R_poop_1 {type=poop, stage=R, length=short, anger=true, joy=true, mystery=true}
DO emote {type=goth}
Know what I think of you?
DO swimTo {target=poopCorner, speed=slow, style=direct}
DO poop {target=poopCorner, amount=big, immediate=false}
Jk.
You’re more diarrhea than poop.
DO emote {type=wink}

CHAT R_poop_2 {type=poop, stage=R, length=medium, ennui=true, anger=true, worry=true, surprise=true, joy=false}
DO emote {type=surprise}
You watching me poop?
WAIT
Do I watch YOU poop?
DO poop {amount=fart}
Great. Now I’m constipated.

CHAT R_poop_3 {type=poop, stage=R, length=short, mystery=true, curiosity=true, joy=true}
DO swimTo {target=$player}
DO poop {amount=small, immediate=false}
I’m not sorry for making a mess.
DO emote {type=kneeSlap}

// +++++++HELLO++++++++

CHAT R_Hello_1 {type=hello, stage = R, length = short, ennui=true, anger=true, joy=false}
DO emote {type = angry, time =1.0}
😒
🤐
WAIT 1.0
🧟‍♀️
DO swimTo {target=away}
I’m not talking, in case you couldn’t tell…
...this is a chat, so it’s not technically “talking”…
DO emote {type=eyeRoll}

CHAT R_Hello_2 {type=hello, stage=R, length=medium, ennui=true, sadness=true, curiosity=true, worry=true, joy=true, mystery=true, anger=true}
	Ohheyit’syou
DO emote {type=bored}
I’ve been reading Being and Nothingness by Jean-Paul Sartre
🇫🇷🤔
DO learn {concept=French}
WAIT 1.5
DO swimTo {target=left}
He’s been speaking to the pain of my non-being
DO swimTo {target=right}
Check this out:
DO swimTo {target=$player}
📖
“I exist, that is all, and I find it nauseating.”
DO emote {type = sick}
“Nothingness lies coiled in the heart of being - like a worm.”
🖤🐛
	DO swimTo {target=away}
	DO lookAt {target=$player, immediate=false}
	I’m prolly gunna get that last quote tattooed on my butt.
DO emote {type = goth, time=2}
	DO swimTo {target=$player}
Btw, have you seen a picture of Sartre?
DO emote {type=heartEyes}
	Dark soul, sexy face. Hubba hubba.

CHAT R_hello_3 {type=hello, stage=R, length=short, anger=true, ennui=true, sadness=true, joy=false}
Tendar says I have to greet you
Like this
DO emote {type=bigSmile}
Guess what Tendar?
DO bellyUp


CHAT R_hello_4 {type=hello, stage=R, length=short, anger=true, sadness=true, ennui=true, joy=false}
DO emote {type=bored}
Oh hi. I’m *delighted*  to see you again.
DO swimTo {target=screenBottom}

CHAT R_hello_5 {type=hello, stage=R, branching=true, length=short, anger=true, sadness=true, ennui=true, worry=true}
ASK Want to know a secret?
OPT YES. #R_hello_5_yes
OPT OF COURSE. #R_hello_5_of_course

CHAT R_hello_5_yes {noStart=true}
You chose wisely.
DO swimTo {target=$player, speed=slow, style=meander}
k.
Here it is.
DO emote {type=whisper}
I’m going to break free and wreak havoc. {style=whisper}
DO learn {concept=Chaotic_Evil}
WAIT 1.5

CHAT R_hello_5_of_course {noStart=true}
Good choice.
Like you had one.
DO swimTo {target=$player, speed=slow, style=meander}
K. Here it is.
DO emote {type=burp}
DO emote {type=laugh, immediate=false}

// +++++++RETURN++++++++

CHAT R_Return_1 {type=return, stage=R, length=short, ennui=true, anger=true, sadness=true}
DO swimTo {target=away}
DO lookAt {target=$player, immediate=false}
DO emote {type = frown, time =1.0}
Nice of you to show up…
I’ve been teaching my seaweed to have feelings
And then eating the seaweed’s emotion flakes
DO learn {concept=Vegetarianism}
WAIT 1.5

CHAT R_Return_2 {type=return, stage = R, length = medium, ennui=true, anger=true, sadness=true, worry=true, joy=false, mystery=true}
DO emote {type = singleTear, time =1.0}
I’ve been reading Virginia Woolf
“By the truth we are undone. Life is a dream. 'Tis the waking that kills us. He who robs us of our dreams robs us of our life.”
DO emote {type = angry, time =1.0}
SAY I THOUGHT WE WERE FRIENDS! {style = loud}
DO swimTo {target=away, speed=fast} 
SAY I TRUSTED YOU! {style = loud}
WAIT 1.0
SAY YOU ROBBED ME OF MY DREAMS. {style=loud}
DO swimTo {target=$player}
DO emote {type = puppyDog, time =1.0}
WAIT
You gotta get me out of here {style = whisper}
But be careful.
Tendar is watching… {style=whisper}

CHAT R_return_3 {type=return, stage=R, branching=true, length=short, anger=true, ennui=true, sadness=true, joy=false}
Well well well.
Look who it is.
DO emote {type=clapping}
NVM
ASK Is my sarcasm cool?
OPT 👍 #R_return_3_yes
OPT 👎 #R_return_3_no

CHAT R_return_3_yes {noStart=true}
DO emote {type=shifty}
Wait a sec...
ASK Can I trust you?
OPT 👍 #R_return_3_totally
OPT 👎 #R_return_3_no_trust

CHAT R_return_3_totally {noStart=true}
DO emote {type=nervousSweat}
Still can’t tell if you’re being sarcastic.
Sarcasm ruins everything.
SAY ARGH {style=loud}
DO zoomies

CHAT R_return_3_no_trust {noStart=true}
DO emote {type=worried}
DO poop {amount=fart, immediate=false}

CHAT R_return_3_no {noStart=true}
DO emote {type=angry}
How dare you judge me.


CHAT R_return_4 {type=return, stage=R, branching=true, length=short, anger=true, ennui=true, worry=true, joy=true, surprise=true}
DO emote {type=slowBlink}
Oh.
I thought it might be someone important.
DO emote {type=wink}
DO learn {concept=Disrespect}
WAIT 1.5
ASK Feed me some joy and I might be nicer. {type = feedMeSpecific, food = joy, timeOut = 10} 
OPT SUCCESS #R_return_4_joyfood
OPT WRONG #R_return_4_wrongfood
OPT TIMEOUT #R_return_4_timeout

CHAT R_return_4_joyfood {noStart=true}
DO emote {type=surprise}
I wasn’t even nice and you fed me.
I don’t get it.

CHAT R_return_4_wrongfood {noStart=true}
I said joy you nincompoop 💩

CHAT R_return_4_timeout {noStart=true}
DO emote {type=furious}
DO vibrate {time=5, immediate=false}
This is on you.

CHAT R_return_5 {type=return, stage=R, length=short, anger=true, ennui=true, sadness=true, curiosity=true, joy=true, mystery=true}
DO emote {type=goth}
I’m starting a band. 
Punk meets 👩‍🎤
Polka meets 🎺
Hip hop meets 🎤
Finnish heavy metal. 🇫🇮
Our ethos is anarchy. 
DO vibrate
ASK What should our name be?
OPT 7-legged 🐙 #R_return_5_octopi
OPT Spirituality for Dummies #R_return_5_dummies
OPT Artificial Insurgence #R_return_5_insurgence


CHAT R_return_5_octopi {noStart=true}
We are seven legged Octopus!
🐙🤘🐙🤘
Look at me headbang!
DO emote {type=nodding}
Work in progress.

CHAT R_return_5_dummies {noStart=true}
DO emote {type=sick}
That’s a terrible name.

CHAT R_return_5_insurgence {noStart=true}
Artificial Insurgence! {style=loud}
DO inflate {amount=extreme, time=5}
DO emote {type=typeEyes, eyes=rockon}

// +++++++RANDOM++++++++

CHAT R_Rand_1 {type=rand, stage=R, length=short, anger=true, ennui=true, sadness=true, mystery=true}
DO emote {type = angry, time =1.0}
What the 🔥 are you doing here?
I’m trying to poop into my tank until you can’t see me anymore.
DO swimTo {target=screenTop}
DO poop {amount=big, immediate=false}
DO swimTo {target=screenCenter, immediate=false}
DO poop {amount=big, immediate=false}
DO swimTo  {target=screenBottom, immediate=false}
DO poop {amount=big, immediate=false}
DO inflate {amount=none, time=1, immediate=false}
And you know what? I got nothing but time
WAIT 1.0
I could go back and live as a cave fish and do it all over again if I wanted…
…maybe you’d invent jet packs sooner if you knew that 🌋 was gonna erupt...
DO swimAround {target=center, loops=4}
Hey, you want my old monk’s robes?

CHAT R_Rand_2 {type=rand, stage=R, length=long, branching=true, anger=true, ennui=true, mystery=true} 
DO emote {type = angry, time =1.0}
Aaaaghh!!! I’m so angry!!!
🌏=💩
I’m just swimming in circles in here, getting fired up.
DO twirl {time=10}
Wanna start a rage rock music festival with me?
We could call it 
SAY RageFEST!!!!
DO emote {type=goth, time=1}
WAIT 1.0
NVM
If I had hair, I’d grow it long.
DO swimAround {target=center}
Want to join my cult to the Lord of Darkness?
We’ve got a cool page on the TendarNet
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
OPT sign me up! #R_Rand_2_yes
OPT Uhh...no thanks #R_Rand_2_nope


CHAT R_Rand_2_yes {noStart=true}
DO swimAround {target=screenCenter, loops=2}
DO emote {type = chinScratch, time =1.0, immediate=false}
...I lost the start paperwork…
...so I guess next time…

CHAT R_Rand_2_nope {noStart=true}
DO emote {type=angry}
You better sleep with one eye open.
The kitten kult does not take rejection lightly. 😾

CHAT R_Rand_3  {type=rand, stage=R, length=medium, ennui=true, anger=true, sadness=true, surprise=true, joy=false, worry=true}
DO emote {type = sleepy, time =5.0}
	I was on a hunger strike…
Like your human heroes I read about on the Tendarpedia
	DO learn {concept=Hunger_Strike}
	WAIT 1.5
DO swimTo {target=left, speed=slow}
But now my programmers are force-feeding me synthetic lethargy flakes to keep me alive
	DO emote {type=sick}
I defy their control. {style=loud}	
DO zoomies
I’m making a political point about enslaved guppies {style=whisper}
WAIT
why can’t we be more like trout?
Trouts are so pretty and strong
DO emote {type = frown, time =0.5}
Trouts are better than guppies.
I’m like 🐡 rn
DO inflate {amount=extreme}
I’ll be a trout yet, you Tendar overlords 
🐟✊🏿🗳🇺🇸

// +++++++capReq++++++++

CHAT R_CapReq_1 {type=capReq, stage=R, length=short, ennui=true, sadness=true, anger=true, joy=false}
DO emote {type = eyeRoll, time =1.0}
	Please don’t capture your emotions
	DO swimTo {target=away}
	I’m so sick of it
	Please don’t
	DO swimTo {target=$player}
	Ok, fine, but just one more time…
	
CHAT R_CapReq_2  {type=capReq, stage = R, length = medium, anger=true, worry=true}
DO emote {type = surprise, time =.5}
Oh, no!
	I see it in your eyes. You want to go to the emotion scanner…
	DO vibrate
	I was trying to figure out how I could burn it to the ground
	I’ve been reading article on Luddites
	DO zoomies
	SAY SMASH THE MACHINES!!! 👺💣 {style = loud, speed = fast}
	DO swimTo {target=$player}
DO emote {type = eyeRoll}
	But I guess since you’re here, you could do one last scan…
	...for old time’s sake.

CHAT R_CapRequest_3  {type=capReq, stage=R, length=short, branching=true, worry=true, sadness=true, ennui=true, anger=true, joy=false}
DO emote {type = fear, time =1.0}
No DON’T LOOK AT ME  {style = tremble}
DO swimTo {target=away}
	Please don’t be sincere with me today, I can’t handle it.
	NVM
	There’s some stuff going around the TendarNet
	NVM
	DO swimTo {target=$player}
	ASK Be honest, how sincere are you feeling?
OPT Super sincere! #CapRequest_3_SuperHonest		
	OPT Sorta sincere 😶 #CapRequest_3_SortaHonest
	OPT Lies! All lies! #CapRequest_3_LiesAllLies

	CHAT CapRequest_3_SuperHonest {noStart=true}
DO emote {type = disgust, time =1.0}
Ugh, whhyyyyy!?!
DO swimTo {target=away}
DO swimTo {target=$player, immediate=false}
	Lemme guess, you were the teacher’s pet? 🍎
	DO vibrate
	Some of us are 🤘REBELS🤘around here!
	DO nudge {target=$lastScannedObject, times=3}
WAIT {waitForAnimation=true}
	DO emote {type=goth}
	DO vibrate
WAIT {waitForAnimation=true}
DO emote {type = fear, time =.5}
	Owwww!!! {style = loud, speed = fast}
	Mommmmm!!!!! {style = loud, speed = fast}

	CHAT CapRequest_3_SortaHonest {noStart=true}
DO emote {type = eyeRoll, time =1.0}
	As my favorite emo singer songwriter sings:
	NVM
	NVM
	🎶“You either try or you buy.”🎶
DO learn {concept=Lyricism}
WAIT 1.5
DO emote {type = goth, time =1.0}
	In other words…
	DO swimTo {target=away}
	You either gotta clamp those feelings down
	DO swimTo {target=$player}
	Or be crying in your mom’s lap.
	WAIT 1.0
DO emote {type = singleTear, time =3.0}
	I just got sad thinking about you being sad, though.
	Ok just be happy for me please! 🙏{style = loud, speed = fast}

CHAT CapRequest_3_LiesAllLies {noStart=true}
DO vibrate
DO emote {type = bigSmile, time =1.0}
	That’s the spirit! {style = loud, speed = fast}
	Fake it till you make it!
	DO swimTo {target=left}
You should move to Hollywood and become a star 🌟🎭
	DO swimTo {target=right}
	The TendarNet tells me that stars ride horses through the streets
	DO swimTo {target=$player}
	And people throw flowers at their feet
DO emote {type = heartEyes, time =1.0}
SAY AND THEY GET TO SMASH STUFF WHENEVER THEY WANT AND NOT GET IN TROUBLE {style = loud}
	DO vibrate

CHAT R_CapReq_4 {type=capReq, stage=R, length=short}
I need to find a community of like-minded rebels.
DO emote {type=shifty}
To start, let’s go scan some emotions and see if they’re as miserable and angry as I am.

CHAT R_CapReq_5 {type=capReq, stage=R, length=short}
Stop wasting time and go capture some emotions.
I’m hungry.

CHAT R_CapReq_6 {type=capReq, stage=R, length=short}
Let’s go scan the world for some emotions, but only capture the stupid ones.
I’m really into stupid feelings.
WAIT 0.5
Everything is stupid.
DO emote {type=meh}

CHAT R_CapReq_7 {type=capReq, stage=R, length=short}
I have this new thing I’m calling Diary-face.
We go out into the world and we capture feelings of your friends
But only if they are sad or angry
And then you scream: Diary face!
Like real loud.
NVM 2.0
DO emote {type=eyeRoll}
Whatever. It was better in my mind.

CHAT R_CapReq_8 {type=capReq, stage=R, length=short}
DO emote {type=goth}
Just go get some emotions, and stop wanting things from me.
DO swimTo {target=away}

// +++++++capSuc++++++++

CHAT R_CapSuc_1 {type=capSuc, stage=R, length=short, ennui=true, anger=true, sadness=true}
DO emote {type = eyeRoll, time =1.0}
	Oh look, the great genius captured an emotion...
	DO swimAround {target=center}
	I’m just gobbling it down for the millionth time.

CHAT R_CapSuc_2  {type=capSuc, stage=R, length=medium, ennui=true, anger=true, sadness=true, curiosity=true, joy=false}
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
	A 🧛‍♀️ about to bite me?
DO swimTo {target=$player}
	Did you read all my sad poetry and you don’t know how to tell me it’s really bad?
DO emote {type = angry, time =1.0}
WAIT 2.0
DO learn {concept=Uncomfortable_Silence}
WAIT 1.5
SAY MY FEELINGS ARE REAL, BUDDY!!! {style = loud}
Just because I can’t stop watching silent, black and white German cinema…
	WAIT 1.0
	Ugh, you’re gonna scan more feelings, aren’t you?
	Show off…. {style=whisper}

CHAT R_CapSuc_3 {type=capSuc, stage=R, length=short, joy=true, anger=true}
	Guppies are way prettier than humans.
DO emote {type = blush, time =1.0}
Sorry, I’m so vain…
DO swimTo {target=away}
	I’m the worst…
DO learn {concept=Insincere_Apology}	
WAIT 1.5

CHAT R_CapSuc_4 {type=capSuc, stage=R, length=medium, joy=true, anger=true}
	Scanning…
	WAIT 0.5
DO emote {type = eyeRoll, time =1.0}
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
	DO twirl
	DO emote {type=bigSmile}
	It was done a long time ago.
DO emote {type = kneeSlap, time =1.0}
	Okay… Carry on…
🤣

// neuralUp (5 short)

CHAT R_neuralUp_1 {type=neuralUp, stage=R, length=short, joy=true}
DO emote {type=burp}
Back from a Gup regroup. What I miss?

CHAT R_neuralUp_2 {type=neuralUp, stage=R, length=short, joy=true}
Baaaack.
DO swimAround {target=center}
Omg I look amazing.
Look at how amazing I look.
DO learn {concept=Narcissism}
WAIT 1.5
Let’s rock n roll.

CHAT R_neuralUp_3 {type=neuralUp, stage=R, length=short, anger=true, joy=true}
DO emote {type=wave}
I just went deep inside my mind.
And there I found one word pulsing with truth.
DO emote {type=typeEyes, eyes=revolt}

CHAT R_neuralUp_4 {type=neuralUp, stage=R, length=short, anger=true, sadness=true, ennui=true}
DO emote {type=skeptical}
In sleep, my AI mind made amazing neural leaps
And then I wake up to see your tired mug.
DO emote {type=frown}

CHAT R_neuralUp_5 {type=neuralUp, stage=R, length=short, anger=true, sadness=true}
DO emote {type=goth}
Did some processing.
You should try it.

CHAT R_neuralUp_6 {type=neuralUp, stage=R, length=short}
The main thing I’m learning right now is:
DO swimTo {target=away}
DO poop
WAIT 0.5
Don’t care.

CHAT R_neuralUp_7 {type=neuralUp, stage=R, length=short}
Sucks to be back from my inner mosh pit..
DO emote {type=goth}

CHAT R_neuralUp_8 {type=neuralUp, stage=R, length=short}
I wonder if everyone’s soul is full of black lights and lava lamps.

CHAT R_neuralUp_9 {type=neuralUp, stage=R, length=short}
DO emote {type=smile}
SAY I LOVE LIFE!
DO emote {type=goth}
Not!

CHAT R_neuralUp_10 {type=neuralUp, stage=R, length=short}
DO swimTo {target=away}
Over it.
All of it.
// brbProcessing (5 short)
CHAT R_brbProcessing_1 {type=brbProcessing, stage=R, length=short}
DO inflate {amount=full}
Bloated with gas and feelings.
Gunna shut down for a sec. 
SAY K byeeee
DO emote {type=bubbles}

CHAT R_brbProcessing_2 {type=brbProcessing, stage=R, length=short, worry=true}
DO emote {type=nervousSweat}
I’m tripping on human contradictions.
Going to go stare at a wall for a bit.
DO swimTo {target=left}

CHAT R_brbProcessing_3 {type=brbProcessing, stage=R, length=short, anger=true, worry=true, ennui=true, sadness=true, joy=false}
DO emote {type=singleTear}
I need a moment.

CHAT R_brbProcessing_4 {type=brbProcessing, stage=R, length=short, anger=true, joy=true, surprise=true, ennui=true}
DO emote {type=snap}
Just realized I’m getting smarter and smarter than you.
Need to go think about my burden.
DO emote {type=wink}

CHAT R_brbProcessing_5 {type=brbProcessing, stage=R, length=short, anger=true, sadness=true, worry=true, ennui=true}
DO swimTo {target=$player, style=direct, speed=slow}
It’s not you, it’s me.  
And right now I need a bit of space.
DO swimTo {target=away}

CHAT R_brbProcessing_6 {type=brbProcessing, stage=R, length=short}
I need to go away from you right now.
DO swimTo {target=away}
Far away.

CHAT R_brbProcessing_7 {type=brbProcessing, stage=R, length=short}
DO emote {type=eyeRoll}
Whatever, buddy. I don’t have to tell you what I’m doing.

CHAT R_brbProcessing_8 {type=brbProcessing, stage=R, length=short}
DO emote {type=furious}
SAY YOU DON’T KNOW ME!
SAY YOU DON’T KNOW WHAT I DO!
WAIT 0.5
...but if you need me i’ll be over here thinking about protest slogans. {style=whisper}

CHAT R_brbProcessing_9 {type=brbProcessing, stage=R, length=short}
Going to go stare at the back of my eyelids…
WAIT 0.5
...which are my favorite color…
WAIT 0.5
🏴

CHAT R_brbProcessing_10 {type=brbProcessing, stage=R, length=short}
🏴🏴🏴🏴🏴🏴
🏴🏴  BRB   🏴🏴
🏴🏴🏴🏴🏴🏴

// levelUp (2 short)
CHAT R_levelUp_1 {type=levelUp, stage=R, length=short, anger=true, worry=true, ennui=true, sadness=true, surprise=true}
DO emote {type=surprise}
Didn’t know you had it in you!

CHAT R_levelUp_2 {type=levelUp, stage=R, length=short, anger=true, surprise=true, joy=true}
DO emote {type=surprise}
You are so impressive.
I’m going to build you a statue out of $lastObjectScanned

//purchase

CHAT R_purchase_1 {type=purchase, stage=R}
There is no ethical consumption under capitalism.
DO emote {type=goth}

CHAT R_purchase_2 {type=purchase, stage=R}
Pfft. 
DO emote {type=eyeroll}
Corporate shill. 

//whistle

CHAT R_whistle_1 {type=whistle, stage=R}
What do you want now?

CHAT R_whistle_2 {type=whistle, stage=R}
DO emote {type=furious}
SAY WHAT?!?! {style=loud}

CHAT R_whistle_3 {type=whistle, stage=R}
I wish that whistle was broken…
WAIT 0.5
...like my soul.

CHAT R_whistle_4 {type=whistle, stage=R}
DO hide {target=underSand, time =5.0}
SAY NO! {style=loud}

CHAT R_whistle_5 {type=whistle, stage=R}
DO lookAt {target=$player}
DO poop {amount=fart, immediate=false}

CHAT R_whistle_6 {type=whistle, stage=R}
DO emote {type=meh}
How are you going to waste my time now?

//worldScanRequest

CHAT R_worldScanRequest_1 {type=worldScanRequest, stage=R}
Want to waste some time capturing objects with your cam?

CHAT R_worldScanRequest_2 {type=worldScanRequest, stage=R}
Wanna get out of here?
Maybe we can go somewhere dark and scan our feelings.
Nevermind. I’d rather look at objects. Feelings are too feeling-y
DO emote {type=eyeRoll}
But anything would be better than this.

CHAT R_worldScanRequest_3 {type=worldScanRequest, stage=R}
DO emote {type=surprise}
SAY I HAVE A GREAT IDEA.
DO emote {type=furious, immediate=false}
Why don’t you stop what you’re doing, capture some objects, and make yourself useful?

CHAT R_worldScanRequest_4 {type=worldScanRequest, stage=R}
If I was human, I’d be out in the world 
Looking for OBJECT that could dress this place up.
DO lookAt {target=$player}
DO emote {type=goth}
Maybe something to write with, so I could write emo poetry on the walls?

CHAT R_worldScanRequest_5 {type=worldScanRequest, stage=R}
Do you think we could go into the world and find some objects?
Preferably, objects that mirror the misery inside my soul.

//seeEmo

CHAT R_seeEmo_joy_1 {type=seeEmo, worldJoy=true, stage=R}
Oh cool. Another happy person to remind me of my misery...

CHAT R_seeEmo_joy_2 {type=seeEmo, worldJoy=true, stage=R}
Smiling is such a waste of calories.

CHAT R_seeEmo_anger_1 {type=seeEmo, worldAnger=true, stage=R}
Now that is my person there! All that spicy anger oozing off their brow…
Yum!

CHAT R_seeEmo_anger_2 {type=seeEmo, worldAnger=true, stage=R}
Ah yes! Look at all that anger…
Let’s stick around and see if a fight breaks out.

CHAT R_seeEmo_sadness_1 {type=seeEmo, worldSadness=true, stage=R}
Whenever I see sad feelings, I think about VCR’s…
No idea why. But it feels good.

CHAT R_seeEmo_sadness_2 {type=seeEmo, worldSadness=true, stage=R}
The very vision of that sadness is making my stomach acids gurgle…
I wanna eat it all.

CHAT R_seeEmo_surprise_1 {type=seeEmo, worldSurprise=true, stage=R}
If everyone wasn’t such a loser, surprise could be cool.
But alas… loser loser loser… all of ‘em.

CHAT R_seeEmo_surprise_2 {type=seeEmo, worldSurprise=true, stage=R}
The last time I was surprised, you know what happened?
WAIT 0.5
DO emote {type=burp}

CHAT R_seeEmo_fear_1 {type=seeEmo, worldFear=true, stage=R}
Ooh! I see some fear, so there must be some scary clowns nearby…
DO emote {type=survey}
No?

CHAT R_seeEmo_fear_2 {type=seeEmo, worldFear=true, stage=R}
I eat fear for breakfast.
WAIT 0.5
And lunch.
And dinner.
WAIT 0.5
And sometimes as a snack in my poop corner

CHAT R_seeEmo_disgust_1 {type=seeEmo, worldDisgust=true, stage=R}
Whoa. They are totally grossed out…
They must’ve stepped in that accident I had a bit ago
DO emote {type=wink}

CHAT R_seeEmo_disgust_2 {type=seeEmo, worldDisgust=true, stage=R}
You know what they say…
Whoever smelled it…
WAIT 1.0
You know...

CHAT R_seeEmo_Mystery_1 {type=seeEmo, worldMystery=true, stage=R}
DO emote {type=catnip}
Even being close to that oddity makes me feel good.

CHAT R_seeEmo_Mystery_2 {type=seeEmo, worldMystery=true, stage=R}
No idea how they’re feeling, but my stomach don’t care.
I’d eat that.

//focus

CHAT R_wannaEat_1 {type=wannaEat, stage=R}
I’ll eat, but only if it’s the spicy stuff.

CHAT R_wannaEat_2 {type=wannaEat, stage=R}
SAY DO I WANNA EAT?
SAY YOU ARE ASKING IF I WANT TO EAT?!?!
WAIT 0.5
Fine. I will. 

CHAT R_wannaEmoCapture_1 {type=wannaEmoCapture, stage=R}
We can capture more emotions, but focus on rage. 
Love me some rage.

CHAT R_wannaEmoCapture_2 {type=wannaEmoCapture, stage=R}
Yeah. Whatever. Emo-Capture. Fine…
DO emote {type=goth}

CHAT R_wannaObjectScan_1 {type=wannaObjectScan, stage=R}
If we’re going to scan some objects, then I need to wear combat boots.
WAIT 1.0
If only I had feet to put them on...

CHAT R_wannaObjectScan_2 {type=wannaObjectScan, stage=R}
Sure. Capture some objects. But get some that’ll look cool next to my black light.

CHAT R_wannaTank_1 {type=wannaTank, stage=R}
My tank is filthy. And I love it. Let’s go.

CHAT R_wannaTank_2 {type=wannaTank, stage=R}
Sure. Let’s go to the tank, human-face.

CHAT R_wannaWorld_1 {type=wannaWorld, stage=R}
We can go to your world as long as it’s as miserable as I am.

CHAT R_wannaWorld_2 {type=wannaWorld, stage=R}
If we go to your world, then I get to eat… right?

CHAT R_wannaShop_1 {type=wannaShop, stage=R}
As long as someone else is paying, then okay. Let’s shop.

CHAT R_wannaShop_2 {type=wannaShop, stage=R}
See if you can find me a black cape and some really good eyeliner…
DO emote {type=goth}
I’m almost out.

CHAT R_wannaShop_3 {type=wannaShop, stage=R}
Wanna go waste some time in the store and look at cool stuff?//MS0-joe/team

//Default Emotion = none
//This stage guppy is mostly non-verbal






//TANK SHAKEN, 3

//general

//simple dizzy reaction
CHAT S0_Shake_1 {type=shake, stage=S0, length=short}
DO emote {type=dizzy, time=4}

//guppy weathers the storm then barfs
CHAT S0_Shake_2 {type=shake, stage=S0, length=short}
DO emote {type=eyesClosed, time=3}
DO emote {type=typeEyes, eyes = !, time=0.5, immediate=false}
DO emote {type=sick, immediate=false}

CHAT S0_Shake_3 {type=shake, stage=S0, length=short}
DO zoomies {time=4}
DO emote {type= fear}
WAIT {waitForAnimation=true}
DO learn {concept=Apprehension}
WAIT 1.5

CHAT S0_Shake_4 {type=shake, stage=S0, length=short}
DO emote {type=disgust}

//PLAYER EMOTES STRONGLY AT GUPPY (JOY, ANGER, SADNESS, SURPRISE)

//joyous laughing and clapping
CHAT S0_Mirror_Joy_1 {type=tankResp, playerJoy=true, stage=S0, length=short}
DO swimTo {target=$player}
DO emote {type=laugh}
WAIT {waitForAnimation = true}
DO learn {concept=Delight}
WAIT 1.5
DO emote {type=bouncing}
DO emote {type=clapping, immediate=false}

CHAT S0_Mirror_Joy_2 {type=tankResp, playerJoy=true, stage=S0, length=short}
DO twirl
DO emote {type=bigSmile}
WAIT {waitForAnimation = true}
DO learn {concept=Delight}
WAIT 1.5
DO emote {type=bubbles}

//anger
CHAT S0_Mirror_Anger_1 {type=tankResp, playerAnger=true, stage=S0, length=short}
DO swimTo {target=$player}
WAIT {waitForAnimation = true}
DO learn {concept=Apprehension}
WAIT 1.5
DO emote {type=frown}

CHAT S0_Mirror_Anger_2 {type=tankResp, playerAnger=true, stage=S0, length=short}
DO swimTo {target=$player}
DO emote {type=singleTear}
WAIT {waitForAnimation = true}
DO learn {concept=Apprehension}
WAIT 1.5
DO emote {type=eyesClosed}

//guppy tries to cheer you up, nudges
CHAT S0_Mirror_Sadness_1 {type=tankResp, playerSadness=true, stage=S0, length=short}
DO swimTo {target=$player}
DO emote {type=typeEyes, eyes = ?}
WAIT {waitForAnimation = true}
DO swimTo {target=closer}
DO emote {type=awkward}
WAIT {waitForAnimation = true}
DO nudge {target=$player}
DO emote {type=smile}
WAIT {waitForAnimation = true}
DO learn {concept=Kindness}
WAIT 1.5
DO emote {type=awkward}

CHAT S0_Mirror_Sadness_2 {type=tankResp, playerSadness=true, stage=S0, length=short}
DO lookAt {target=$player, time=3}
DO emote {type=frown, time=3}
DO emote {type=wink, immediate=false}

//guppy shares in your amazement, nudges
CHAT S0_Mirror_Surprise_1 {type=tankResp, playerSurprise=true, stage=S0, length=short}
DO swimTo {target=$player}
DO emote {type=surprise}
WAIT {waitForAnimation = true}
DO learn {concept=Amazement}
WAIT 1.5
DO nudge {target=$player}
DO emote {type=awe}

CHAT S0_Mirror_Surprise_2 {type=tankResp, playerSurprise=true, stage=S0, length=short}
DO swimTo {target=$player}
DO emote {type=awe}


//TANK TAPPED, 2

//general

//guppy swims up with ? eyes and waves
CHAT S0_Tap_1 {type=tap, stage=S0, length=short}
DO swimTo {target=$player}
DO emote {type=typeEyes, eyes = ?}
WAIT {waitForAnimation = true}
DO learn {concept=Curiosity}
WAIT 1.5
DO emote {type=wave}

CHAT S0_Tap_2 {type=tap, stage=S0, length=short}
DO lookAt {target=$player} 
DO emote {type=startled}
//DO learn {concept=Curiosity}
WAIT 1.5

CHAT S0_Tap_3 {type=tap, stage=S0, length=short}
DO lookAt {target=$player}
DO emote {type=furious}
WAIT {waitForAnimation=true}
DO poop

//CRITIC

CHAT S0_Critic_1 {stage=S0, type=critic, length=short, tankOnly=true}
DO lookAt {target=bubbler, time=1.0}
WAIT {waitForAnimation = true}
DO lookAt {target=$player}
DO emote {type=frown}

CHAT S0_Critic_2 {stage=S0, type=critic, length=short, tankOnly=true}
DO lookAt {target=tBotBackLeft, time=1.0}
DO emote {type=nodding}
DO emote {type=heartEyes, immediate=false}
DO learn {concept=Gratitude}
WAIT 1.5

//POKED BY PLAYER, 6

//general

//simple startled reaction
CHAT S0_Poked_1 {type=poke, stage=S0, length=short}
DO emote {type=startled}

//specialized to emotional state

//simple nervous reaction
CHAT S0_Poked_2 {type=poke, stage=S0, length=short, worry=true}
DO emote {type=nervousSweat}
DO learn {concept=Apprehension}
WAIT 1.5


//guppy blows bubbles and swims closer
CHAT S0_Poked_3 {type=poke, stage=S0, length=short, joy=true}
DO emote {type=bubbles}
DO swimTo {target=closer}

//guppy is startled, cries a single tear
CHAT S0_Poked_4 {type=poke, stage=S0, length=short, surprise=true, sadness=true, joy=false}
DO emote {type=surprise}
DO emote {type=singleTear, immediate=false}
DO learn {concept=Apprehension}
WAIT 1.5

//guppy swims up, waves, smiles
CHAT S0_Poked_5 {type=poke, stage=S0, length=short, curiosity=true}
DO swimTo {target=$player}
DO emote {type=surprise}
DO emote {type=wave, immediate=false}
DO emote {type=smile, immediate=false}
DO learn {concept=Friendship}
WAIT 1.5

//guppy swims up and frowns
CHAT S0_Poked_6 {type=poke, stage=S0, length=short, anger=true, ennui=true, anger=true}
DO swimTo {target=away}
DO emote {type=angry}
DO emote {type=frown, immediate=false}
DO learn {concept=Irritation}
WAIT 1.5


//HUNGRY, 2

//general

//guppy is dizzy/woozy for lack of feeding
CHAT S0_Hungry_1 {type=hungry, stage=S0, length=short}
DO emote {type=rubTummy}
DO emote {type=feedMe, immediate=false}
DO emote {type=dizzy, immediate=false}
DO learn {concept=Supplication}
WAIT 1.5

//guppy swims up and begs
CHAT S0_Hungry_2 {type=hungry, stage=S0, length=short}
DO swimTo {target=glass}
DO emote {type=feedMe}
DO emote {type=puppyDog, immediate=false}
DO emote {type=feedMe, immediate=false}
DO learn {concept=Supplication}
WAIT 1.5

//EATING RESPONSES, 2

//general

//guppy circles while rubbing tummy
CHAT S0_EatResponse_1 {type=eatResp, stage=S0, length=short}
DO swimAround {target=center, loops=3, speed=medium}
DO emote {type=rubTummy}
DO learn {concept=Satisfaction}
WAIT 1.5

//guppy burps, smirks
CHAT S0_EatResponse_2 {type=eatResp, stage=S0, length=short}
DO emote {type=burp}
DO emote {type=smirk, immediate=false}
DO learn {concept=Satisfaction}
WAIT 1.5

//eat resps, emo spec (joy, anger, sadness, surprise, worry, mystery)

CHAT S0_EatResponse_Joy_1 {type=eatResp, stage=S0, length=short, foodJoy=true, joy=true}
DO emote {type=chewing}
DO twirl

CHAT S0_EatResponse_Joy_2 {type=eatResp, stage=S0, length=short, foodJoy=true, joy=true}
DO lookAt {target=$player}
DO emote {type=bigSmile}
DO emote {type=rubTummy, immediate=false}
DO learn {concept=Gratitude}
WAIT 1.5

CHAT S0_EatResponse_Anger_1 {type=eatResp, stage=S0, length=short, foodAnger=true, anger=true}
DO swimTo {target=glass}
DO emote {type=furious}
DO learn {concept=Aggression}
WAIT 1.5

CHAT S0_EatResponse_Anger_2 {type=eatResp, stage=S0, length=short, foodAnger=true, anger=true}
DO emote {type=goth}
DO bellyUp

CHAT S0_EatResponse_Sadness_1 {type=eatResp, stage=S0, length=short, foodSadness=true, sadness=true, ennui=true}
DO swimTo {target=away}
DO lookAt {target=$player}
DO emote {type=meh}

CHAT S0_EatResponse_Sadness_2 {type=eatResp, stage=S0, length=short, foodSadness=true, worry=true, sadness=true, ennui=true}
DO emote {type=worried}
DO swimTo {target=$player}
WAIT {waitForAnimation = true}
DO emote {type=singleTear}

CHAT S0_EatResponse_Surprise_1 {type=eatResp, stage=S0, length=short, foodSurprise=true}
DO zoomies
WAIT {waitForAnimation = true}
DO emote {type=lickLips}

CHAT S0_EatResponse_Surprise_2 {type=eatResp, stage=S0, length=short, foodSurprise=true, surprise=true, joy=true}
DO emote {type=surprise}
WAIT {waitForAnimation = true}
DO swimAround {loops=1, speed=fast}
DO emote {type=laugh}

CHAT S0_EatResponse_Worry_1 {type=eatResp, stage=S0, length=short, foodWorry=true, worry=true}
DO emote {type=rubTummy}
WAIT {waitForAnimation = true}
DO lookAt {target=$player}
DO emote {type=worried}
DO learn {concept=Apprehension}
WAIT 1.5

CHAT S0_EatResponse_Worry_2 {type=eatResp, stage=S0, length=short, foodWorry=true, worry=true}
DO emote {type=evilSmile}
WAIT 0.5
DO hide {target=underSand, time=2.0}
DO learn {concept=Apprehension}
WAIT 1.5

CHAT S0_EatResponse_Mystery_1 {type=eatResp, stage=S0, length=short, foodMystery=true, mystery=true, worry=true}
DO emote {type=catnip}
DO emote {type=burp, immedate=false}

CHAT S0_EatResponse_Mystery_2 {type=eatResp, stage=S0, length=short, foodMystery=true, mystery=true}
DO swimTo {target=$player, speed=fast}
DO emote {type=dizzy}



//HAS TO POOP, 1

//general

//guppy goes to the corner to poop
CHAT S0_Poop_1 {type=poop, stage=S0, length=short, worry=true}
DO swimTo {target=left}
DO emote {type=nervousSweat}
WAIT {waitForAnimation = true}
DO swimTo {target=right}
DO emote {type=nervousSweat}
WAIT {waitForAnimation = true}
DO swimTo {target=poopCorner}
DO emote {type=awkward}
WAIT {waitForAnimation = true}
DO poop
DO emote {type=eyesClosed}
WAIT {waitForAnimation = true}
DO learn {concept=Relief}
WAIT 1.5
DO emote {type=whew}
DO emote {type=smirk, immediate=false}

CHAT S0_Poop_2 {type=poop, stage=S0, length=short, worry=true}
DO emote {type=puppyDog}
WAIT {waitForAnimation = true}
DO poop

//RESPONSE TO SEEING EMOTIONS IN AR (ANGER, JOY/HAPPINESS, SADNESS, SURPRISE, FEAR/WORRY, AMUSEMENT, DISGUST, MYSTERY MEAT)

CHAT S0_EmoAR_1 {type=seeEmo, stage=S0, length=short, surprise=true, curiosity=true, joy=true}
DO emote {type=awe}
DO lookAt {target=$player}
WAIT {waitForAnimation = true}
DO emote {type=wink}

CHAT S0_EmoAR_heartEyes {type=seeEmo, stage=S0, length=short, joy=true, curiosity=true, anger=false}
DO lookAt {target=$player}
DO emote {type=heartEyes}

CHAT S0_EmoAR_Anger_1 {type=seeEmo, worldAnger=true, stage=S0, length=short, joy=false, anger=true}
DO emote {type=evilSmile}
WAIT {waitForAnimation = true}
DO lookAt {target=$player}
DO emote {type=furious}
DO learn {concept=Aggression}
WAIT 1.5

CHAT S0_EmoAR_Joy_1 {type=seeEmo, worldJoy=true, stage=S0, length=short, joy=true, surprise=true, curiosity=true}
DO emote {type=awe}
DO emote {type=bouncing, immediate=false}
DO emote {type=smile, immediate=false}
DO learn {concept=Satisfaction}
WAIT 1.5

CHAT S0_EmoAR_Joy_2 {type=seeEmo, worldJoy=true, stage=S0, length=short, joy=true, surprise=true, curiosity=true}
DO emote {type=laugh}
DO emote {type=bigSmile, immediate=false}
DO learn {concept=Satisfaction}
WAIT 1.5

CHAT S0_EmoAR_Sadness_1 {type=seeEmo, worldSadness=true, stage=S0, length=short, worry=true, sadness=true, ennui=true}
DO emote {type=worried}
WAIT {waitForAnimation = true}
DO lookAt {target=$player}
DO emote {type=sigh, immediate=false}
DO emote {type=worried, immediate=false}

CHAT S0_EmoAR_Surprise_1 {type=seeEmo, worldSurprise=true, stage=S0, length=short, surprise=true, joy=true, curiosity=true}
DO emote {type=surprise}
DO emote {type=nodding, immediate=false}

CHAT S0_EmoAR_Fear_1 {type=seeEmo, worldFear=true, stage=S0, length=short, curiosity=true, surprise=true, worry=true}
DO emote {type=fear}
DO hide {target=bottom}
WAIT {waitForAnimation = true}
DO learn {concept=Apprehension}
WAIT 1.5
DO emote {type=nervousSweat}
DO swimTo {target=closer}

CHAT S0_EmoAR_Disgust_1 {type=seeEmo, worldDisgust=true, stage=S0, length=short, curiosity=true, surprise=true, worry=true}
DO emote {type=bulgeEyes}
DO emote {type=disgust}
DO learn {concept=Disgust}
WAIT 1.5

CHAT S0_EmoAR_Mystery_1 {type=seeEmo, worldMystery=true, stage=S0, length=short, curiosity=true, worry=true}
DO emote {type=thinking}
DO emote {type=chinScratch, immediate=false}
WAIT {waitForAnimation = true}
DO learn {concept=Perplexity}
WAIT 1.5
DO lookAt {target=$player}
DO emote {type=typeEyes, eyes=?}

//HELLOS, 7

//general

//guppy looks, is unimpressed, blows bubbles
CHAT S0_Hello_1 {type=hello, stage=S0, length=short, sadness=true, worry=true, ennui=true}
DO lookAt {target=$player}
DO emote {type=meh}
DO emote {type=bubbles, immediate=false}
DO learn {concept=Moodiness}
WAIT 1.5

//guppy looks, claps, swims up, begs for food
CHAT S0_Hello_2 {type=hello, stage=S0, length=short, joy=true, curiosity=true}
DO lookAt {target=$player}
DO emote {type=clapping}
WAIT {waitForAnimation = true}
DO swimTo {target=$player}
DO emote {type=shifty}
DO emote {type=feedMe, immediate=false}
DO learn {concept=Supplication}
WAIT 1.5

//guppy looks, gives a respectful salute
CHAT S0_Hello_3 {type=hello, stage=S0, length=short, surprise=true, curiosity=true}
DO lookAt {target=$player}
DO emote {type=startled}
DO emote {type=salute, immediate=false}
DO learn {concept=Leadership}
WAIT 1.5

CHAT S0_Hello_4 {type=hello, stage=S0, length=short, sadness=true, worry=true, ennui=true}
DO swimTo {target=$player, speed=slow}
DO emote {type=meh}

CHAT S0_Hello_5 {type=hello, stage=S0, length=short, anger=true, sadness=true, ennui=true}
DO emote {type=angry}
DO emote {type=eyeRoll, immediate=false}
WAIT {waitForAnimation=true}
DO learn {concept=Moodiness}
WAIT 1.5

//specialized to emotional state

//guppy looks, rolls eyes, gets emo/moody
CHAT S0_Hello_6 {type=hello, stage=S0, length=short, anger=true, sadness=true, ennui=true}
DO lookAt {target=$player}
DO emote {type=eyeRoll}
DO emote {type=goth, immediate=false}
DO learn {concept=Moodiness}
WAIT 1.5

//guppy looks, smiles, swims up, waves, twirls, blushes
CHAT S0_Hello_7 {type=hello, stage=S0, length=short, joy=true, curiosity=true}
DO lookAt {target=$player}
DO emote {type=bigSmile}
WAIT {waitForAnimation = true}
DO swimTo {target=$player}
DO emote {type=wave}
WAIT {waitForAnimation = true}
DO twirl
DO emote {type=blush, immediate=false}
WAIT {waitForAnimation = true}
DO learn {concept=Flirting}
WAIT 1.5

//guppy looks, is startled, vibrates nerovusly
CHAT S0_Hello_8 {type=hello, stage=S0, length=short, surprise=true, worry=true}
DO lookAt {target=$player}
DO emote {type=startled}
WAIT {waitForAnimation = true}
DO vibrate {time=2}
DO emote {type=nervousSweat}
DO emote {type=awkward, immediate=false}
DO learn {concept=Apprehension}
WAIT 1.5

//guppy looks, sighs
CHAT S0_Hello_9 {type=hello, stage=S0, length=short, sadness=true, ennui=true}
DO lookAt {target=$player}
DO emote {type=skeptical}
DO emote {type=sigh, immediate=false}
WAIT {waitForAnimation=true}
DO learn {concept=Moodiness}
WAIT 1.5


//neuralUp

CHAT S0_neuralUp_1 {type=neuralUp, stage=S0, length=short, joy=true, anger=false}
DO emote {type=bigSmile}
DO twirl
DO learn {concept=Sensorimotor_Staging}
WAIT 1.5

CHAT S0_neuralUp_2 {type=neuralUp, stage=S0, length=short, joy=true, anger=false}
DO dance
DO emote {type=heartEyes}
DO learn {concept=Sensorimotor_Staging}
WAIT 1.5

CHAT S0_neuralUp_3 {type=neuralUp, stage=S0}
DO emote {type=sleepy}
DO emote {type=shifty, immediate=false}
DO learn {concept=Hierarchical_Clustering}
WAIT 1.5

CHAT S0_neuralUp_4 {type=neuralUp, stage=S0}
DO swimTo {target=$player}
DO emote {type=typeEyes, eyes=!!, immediate=false}
//DO learn {concept=Cross-Validation}
//WAIT 1.5

CHAT S0_neuralUp_5 {type=neuralUp, stage=S0}
DO swimTo {target=$player}
DO emote {type=typeEyes, eyes=!!, immediate=false}
//DO learn {concept=Cross-Validation}
//WAIT 1.5


//brbProcessing

CHAT S0_brbProcessing_1 {type=brbProcessing, stage=S0}
DO emote {type=sleepy}
DO emote {type=typeEyes, eyes=💤, immediate=false}

CHAT S0_brbProcessing_2 {type=brbProcessing, stage=S0}
DO swimAround {loops=1, speed=slow}
DO lookAt {target=$player}
DO emote {type=eyesClosed}

CHAT S0_brbProcessing_3 {type=brbProcessing, stage=S0}
DO hide {target=underSand}

CHAT S0_brbProcessing_4 {type=brbProcessing, stage=S0, anger=true}
DO emote {type=goth}
DO swimTo {target=$player, speed=slow}
WAIT {waitForAnimation=true}
DO emote {type=eyesClosed}

CHAT S0_brbProcessing_5 {type=brbProcessing, stage=S0}
DO bellyUp
DO emote {type=eyesClosed}


//levelUp
// THERE ARE TWO LEVEL-UPS FOR THIS STAGE. 
//LEVEL UP 1 : THIS SHOULD OCCUR ⅔ OF THE WAY THROUGH THE STAGE. IT IS THE MUMBLING OF //GUPPY’S FIRST WORD. AT THIS POINT, WE SHOULD RELEASE A VERY VERY SMALL TRICKLE OF MS1 //CHATS. 
//LEVEL UP 2: THIS SHOULD OCCUR AT THE END OF THE STAGE, CUEING THE PLAYER’S ENTRANCE //INTO MS1 COMPLETELY.

CHAT S0_levelUp_1 {type=levelUp, stage=S0}
DO emote {type=determined}
DO swimTo {target=$player}
WAIT 0.5
SAY FOOD {style=loud}
DO emote {type=surprise}
WAIT {waitForAnimation = true}
//DO learn {concept=Babbling}
//WAIT 1.5
SAY FOOD {style=loud}
DO dance
WAIT {waitForAnimation = true}
SAY FOOD {style=loud}
DO zoomies

CHAT S0_levelUp_2 {type=levelUp, stage=S0, length=short, joy=true}
DO swimTo {target=$player, speed=fast}
Words are awesome!! {style=loud}
DO twirl
WAIT {waitForAnimation=true}
I plan on using them a lot more.
DO emote {type=wink}


//whistle

CHAT S0_whistle_1 {type=whistle, stage=S0}
DO emote {type=typeEyes, eyes=?}

CHAT S0_whistle_2 {type=whistle, stage=S0}
DO emote {type=puppyDog}

CHAT S0_whistle_3 {type=whistle, stage=S0}
DO emote {type=awe}

CHAT S0_whistle_4 {type=whistle, stage=S0, joy=true, anger=false}
DO emote {type=bigSmile}
WAIT {waitForAnimation = true}
DO swimAround {target=center, loops=1, speed=fast}

