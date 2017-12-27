set -e

OUTDIR=java

find . | grep "$OUTDIR/GuppyScript*.\(java\|class\|interp\|tokens\)" |  xargs rm

echo
ANTLR4='/usr/bin/java -jar /usr/local/lib/antlr-4.7.1-complete.jar'
echo ANTLR: $ANTLR4 -o $OUTDIR GuppyScript.g4 
$ANTLR4 -o $OUTDIR GuppyScript.g4

echo
pushd $OUTDIR > /dev/null
JAVAC='/usr/bin/javac'
echo JAVAC: $JAVAC GuppyScript*.java
$JAVAC GuppyScript*.java
popd > /dev/null

cp DialogicListener.java $OUTDIR

echo
echo CREATED:
ls $OUTDIR


exit

echo
pushd $OUTDIR > /dev/null
JAVAC='/usr/bin/javac'
echo JAVAC: $JAVAC *.java
$JAVAC *.java
popd > /dev/null
