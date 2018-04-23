#!/bin/bash

# start with: screen -S lint ./run-server.sh, then do: ctrl-a d

ROOT="/Users/dhowe/git/dialogic"

if [ ! -d "$ROOT" ]; then
    echo >&2 "$ROOT not found!"
    exit
fi

# first do the build 
$ROOT/weblint/make-server.sh

if [ ! -f "$ROOT/weblint/bin/Debug/weblint.exe" ]; then
    echo >&2 "exe not found!"
    exit
fi

bash -c 'clear; cd "/Users/dhowe/git/dialogic/weblint/bin/Debug"; export PKG_CONFIG_PATH="/Applications/Visual Studio.app/Contents/Resources/lib/pkgconfig:/Library/Frameworks/Mono.framework/Versions/5.8.1/lib/pkgconfig"; export PKG_CONFIG_LIBDIR=""; export PATH="/Library/Frameworks/Mono.framework/Commands:/Applications/Visual Studio.app/Contents/Resources:/Applications/Visual Studio.app/Contents/MacOS:/usr/bin:/bin:/usr/sbin:/sbin:/usr/local/bin"; export MONO_GAC_PREFIX="/Applications/Visual Studio.app/Contents/Resources"; export XBUILD_FRAMEWORK_FOLDERS_PATH=""; "/Library/Frameworks/Mono.framework/Versions/5.8.1/bin/mono32" --debug "/Users/dhowe/git/dialogic/weblint/bin/Debug/weblint.exe"'
