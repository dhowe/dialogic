#!/bin/bash

if [[ ! -f Dialogic.g4 ]] ; then
    echo '$0 should be run from the directory containing Dialogic.g4'
    exit
fi

JAVA=/usr/bin/java
NUGET_DIR=~/.nuget
ANTLR_JAR=${NUGET_DIR}/packages/antlr4.codegenerator/4.6.4/tools/antlr4-csharp-4.6.4-complete.jar

mv Dialogic*.cs /tmp
#mv Dialogic*.tokens /tmp

"$JAVA" -cp $ANTLR_JAR org.antlr.v4.CSharpTool -encoding UTF-8 -no-listener -visitor -Dlanguage=CSharp -package Dialogic.Antlr Dialogic.g4

sed -i '' '/\[System.CLSCompliant(false)\]/d' Dialogic*.cs

