
OUTDIR=cs
rm $OUTDIR/*.* 2> /dev/null

set -e

echo
ANTLR4='/usr/bin/java -jar /usr/local/lib/antlr-4.7.1-complete.jar'
echo ANTLR: $ANTLR4 -o gen GuppyScript.g4 
$ANTLR4 -Dlanguage=CSharp -o gen GuppyScript.g4

echo
pushd gen > /dev/null
CSC='/Library/Frameworks/Mono.framework/Versions/Current/Commands/csc'
echo CSC: $CSC GuppyScript*.cs
$CSC GuppyScript*.cs
popd > /dev/null

echo
echo FILES:
ls $OUTDIR
