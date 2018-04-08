#!/bin/bash

# start with: screen -S lint ./run-server.sh, then do: ctrl-a d

HOME="/Users/dhowe/git/dialogic"

if [ ! -d "$HOME" ]; then
    echo >&2 "$HOME not found!"
    exit
fi

# first do the build 
$HOME/weblint/make-server.sh

if [ ! -f "$HOME/weblint/bin/Debug/weblint.exe" ]; then
    echo >&2 "exe not found!"
    exit
fi

# then launch the server
bash -c 'clear; cd "/Users/dhowe/git/dialogic/weblint/bin/Debug"; export PKG_CONFIG_PATH="/Applications/Visual Studio.app/Contents/Resources/lib/pkgconfig:/Library/Frameworks/Mono.framework/Versions/5.8.0/lib/pkgconfig"; export PKG_CONFIG_LIBDIR=""; export PATH="/Library/Frameworks/Mono.framework/Commands:/Applications/Visual Studio.app/Contents/Resources:/Applications/Visual Studio.app/Contents/MacOS:/usr/bin:/bin:/usr/sbin:/sbin:/usr/local/bin"; export MONO_GAC_PREFIX="/Applications/Visual Studio.app/Contents/Resources"; export XBUILD_FRAMEWORK_FOLDERS_PATH=""; "/Library/Frameworks/Mono.framework/Versions/Current/Commands/mono" --debug "weblint.exe"'
