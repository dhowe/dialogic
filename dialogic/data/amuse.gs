CHAT Amusement {noStart=true, defaultCmd=SET}
emotion1=amusement

start = $open ($ques $col | $col $ques) ($pos $neg | $neg $pos)

open = $emotion1.emoadj().cap()(, (are we |aren't we|I gather)|)?

ques = ((i'm guessing you|) (heard a good joke | got some (good|happy) news)?
ques |= woke up on the dry side of the bed?
ques |= seeing the world through rose-tinted specs?
ques |= everything falling into place?).Trim().cap()

col = (just look at (yourself|you) | you are so (totally|) ((zero|) chill
col |= on point|(on|) fleek|ON) | well aren't you ((just|) fine|fly)
col |= you're (working|slaying|killing|owning) it).cap().

pos = (Remember that | As they say | As the saying goes) $saying 
saying = (victory|success|happiness) is always (possible|waiting)
saying += for the (one|person) who (keeps|stays) laughing.
saying |= being (fabulous|beautiful|perfect) is the best (revenge|comeback|payback|revenge).

neg = Keep smiling that (pretty|fetching) smile; its all (downhill|shit|death) from here (on out|).
neg |= Never let what you want make you forget (the things|what) you'll never have.
neg |= (Eagles|Hawks) (soar|fly high), but (weasels|rodents) don’t die in jet engines.
neg |= (Don't|Never) underestimate the power of (stupid people|stupidity) in large groups.
neg |= The (victor|winner) is the one who's still laughing as they (die|kick).
neg |= (Its better to ((smile|laugh) well | look happy) than to think well.)
neg |= $emotion1.emosyn().cap() is the perfect (antidote|counterpoint) to (insight|understanding|intelligence).
neg |= Aim low, (reach | achieve) your goals, avoid (disappointment | failure | disappointing your (friends|relatives)).

SAY $start

