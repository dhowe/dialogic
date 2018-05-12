CHAT Amusement {noStart=true, defaultCmd=SET}
emotion1=amusement

start = $open ($ques $col | $col $ques) ($proverb $tease | $tease $proverb)

open = $emotion1.emoadj().cap()(, (are we |aren't we|I gather)|)?

// IMPROVE THIS
ques = ((i'm guessing you|) (heard a good joke | got some (good|happy) news)?
ques |= woke up on the dry side of the bed?
ques |= seeing the world through rose-tinted specs?
ques |= everything falling into place?).Trim().cap()

col = (just look at (yourself|you) | you are so (totally|) ((zero|) chill
col |= on point|(on|) fleek|ON) | well aren't you ((just|) fine|fly)
col |= you're (working|slaying|killing|owning) it).cap().

proverb = (Remember that | As they say | As the saying goes) $sayings
sayings = (victory|success|happiness) waits for the (one|person) who (keeps|stays) laughing.
sayings |= being (fabulous|beautiful|perfect) is the best (revenge|payback|revenge).

tease = Keep smiling that (pretty|fetching) smile; its all (downhill|shit|death) from here (on out|).
tease |= Never let what you want make you forget (the things|what) you'll never have.
tease |= (Eagles|Hawks) (soar|fly high), but (weasels|rodents) don’t die in jet engines.
tease |= (Don't|Never) underestimate the power of (stupid people|stupidity) in large groups.
tease |= The (victor|winner) is the one who's still laughing as they (die|kick).
tease |= (Its better to ((smile|laugh) well | look happy) than to think well.)
tease |= $emotion1.emosyn().cap() is the perfect (antidote|counterpoint) to (insight|understanding|intelligence).
tease |= Aim low, (reach | achieve) your goals, avoid (disappointment | failure | disappointing your (friends|relatives)).

SAY $start

