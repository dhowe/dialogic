
OUTDIR=java
rm $OUTDIR/*.* 2> /dev/null

set -e


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

echo
echo CREATED:
ls $OUTDIR

cp DialogicListener.java $OUTDIR

echo
pushd $OUTDIR > /dev/null
JAVAC='/usr/bin/javac'
echo JAVAC: $JAVAC *.java
$JAVAC *.java
popd > /dev/null
