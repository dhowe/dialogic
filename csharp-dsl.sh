set -e

OUTDIR=cs

find . | grep "$OUTDIR/GScript*.\(cs\|interp\|tokens\)" |  xargs rm

echo
ANTLR4='/usr/bin/java -jar /usr/local/lib/antlr-4.7.1-complete.jar -Dlanguage=CSharp'
echo ANTLR: $ANTLR4 -o $OUTDIR GScript.g4 
$ANTLR4 -o $OUTDIR GScript.g4

echo
pushd $OUTDIR > /dev/null
CSC='/Library/Frameworks/Mono.framework/Versions/Current/Commands/csc /nologo'
echo : $CSC GScript*.cs
$CSC GScript*.cs
popd > /dev/null


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
