rm GuppyScript*.cs
rm GuppyScript*.tokens
"/usr/bin/java" -cp /Users/dhowe/.nuget/packages/antlr4.codegenerator/4.6.4/build/../tools/antlr4-csharp-4.6.4-complete.jar org.antlr.v4.CSharpTool -encoding UTF-8 -no-listener -visitor -Dlanguage=CSharp -package Dialogic /Users/dhowe/git/dialogic/dialogic/GuppyScript.g4

sed -i '' '/\[System.CLSCompliant(false)\]/d' GuppyScript*.cs

