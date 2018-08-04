CHAT Console
Good morning, human

ASK How are you today?
OPT good #good
OPT meh #meh

CHAT Continue
SAY Ok, let's continue
//ASK Whats your name? (you can use the onscreen keypad)
//OPT good #Good
//OPT meh #Meh

CHAT good
Excellent
GO Continue

CHAT meh
Sorry to hear that
GO Continue

CHAT FreeText
ASK Whats your name? (you can use the onscreen keypad)
SET $user.name = $RESPONSE;