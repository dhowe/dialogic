#!/bin/bash

if [[ ! -f dialogic.csproj ]] ; then
    echo `basename "$0"` should be run from the directory containing dialogic.csproj
    exit
fi

JAVA=/usr/bin/java
NUGET_DIR=~/.nuget
ANTLR_JAR=${NUGET_DIR}/packages/antlr4.codegenerator/4.6.4/tools/antlr4-csharp-4.6.4-complete.jar

mv antlr/Dialogic*.cs /tmp 2>&1
mv antlr/Dialogic*.tokens /tmp 2>&1

"$JAVA" -cp $ANTLR_JAR org.antlr.v4.CSharpTool -o antlr -encoding UTF-8 -no-listener -visitor -Dlanguage=CSharp -package Dialogic.Antlr Dialogic.g4

sed -i '' '/\[System.CLSCompliant(false)\]/d' antlr/Dialogic*.cs

