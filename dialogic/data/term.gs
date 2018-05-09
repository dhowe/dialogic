CHAT C1 {type=a,stage=b}
SAY Running C1

CHAT OnTapEvent {noStart=true,resumeAfter=true}
DO #TapResponse
SAY I see you!
FIND {type=a,stage=b}