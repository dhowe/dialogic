#!/bin/bash

# OSX only for now

export TERM=xterm-color

DIR=/Users/$USER/git/dialogic/editor
DLL=$DIR/bin/Debug/netcoreapp2.0/DialogicEditor.dll
VERS=`cat ../dialogic/dialogic.csproj | grep "<PackageVersion" | sed 's/ *<PackageVersion>\([0-9.]*\).*/\1/'`

if [ $# -lt "1"  ]; then
    if [[ "$OSTYPE" == "darwin"* ]]; then
        #echo found osx
        if [ ! -f "$DLL" ]; then
            echo >&2 "dll not found!"
            exit
        fi
        dotnet $DLL
    else
        echo >&2 "Invalid OS! Expecting OS X"
        exit
    fi      
fi

echo Creating DialogicEditor version=$VERS, target=ubuntu.16.04-x64
echo

rm DialogicEditor-$VERS.zip

dotnet publish -c release -r ubuntu.16.04-x64

ln -s bin/release/netcoreapp2.0/ubuntu.16.04-x64/publish DialogicEditor
zip -j -r DialogicEditor-$VERS.zip DialogicEditor/*
zip -ur DialogicEditor-$VERS.zip data

scp run-editor.sh DialogicEditor-$VERS.zip $USER@$RED:~/dialogic-editor

unlink DialogicEditor
#ls -l
#jar tf DialogicEditor-$VERS.zip | less

    
