#!/bin/bash

# first make in this directory with $ msbuild 

export TERM=xterm-color

# start with: screen -S lint ./run-server.sh, then do: ctrl-a d

if [ ! -f "/Users/dhowe/git/dialogic/weblint/bin/Debug/weblint.exe" ]; then
    echo >&2 "exe not found!"
    exit
fi

cd "/Users/dhowe/git/dialogic/weblint/bin/Debug";
export PKG_CONFIG_PATH="/Applications/Visual Studio.app/Contents/Resources/lib/pkgconfig:/Library/Frameworks/Mono.framework/Versions/5.8.1/lib/pkgconfig"; 
export PKG_CONFIG_LIBDIR=""; 
export PATH="/Library/Frameworks/Mono.framework/Commands:/Applications/Visual Studio.app/Contents/Resources:/Applications/Visual Studio.app/Contents/MacOS:/usr/bin:/bin:/usr/sbin:/sbin:/usr/local/bin"; 
export MONO_GAC_PREFIX="/Applications/Visual Studio.app/Contents/Resources"; 
export XBUILD_FRAMEWORK_FOLDERS_PATH=""; 
"/Library/Frameworks/Mono.framework/Versions/5.8.1/bin/mono32" --debug "/Users/dhowe/git/dialogic/weblint/bin/Debug/weblint.exe"
