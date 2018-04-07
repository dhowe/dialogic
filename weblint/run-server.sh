# start with: screen -S lint ./run-server.sh, then do: ctrl-a d

HOME=/Users/dhowe/git/dialogic

bash -c 'clear; cd "$HOME/weblint/bin/Debug"; export PKG_CONFIG_PATH="/Applications/Visual Studio.app/Contents/Resources/lib/pkgconfig:/Library/Frameworks/Mono.framework/Versions/5.8.0/lib/pkgconfig"; export PKG_CONFIG_LIBDIR=""; export PATH="/Library/Frameworks/Mono.framework/Commands:/Applications/Visual Studio.app/Contents/Resources:/Applications/Visual Studio.app/Contents/MacOS:/usr/bin:/bin:/usr/sbin:/sbin:/usr/local/bin"; export MONO_GAC_PREFIX="/Applications/Visual Studio.app/Contents/Resources"; export XBUILD_FRAMEWORK_FOLDERS_PATH=""; "/Library/Frameworks/Mono.framework/Versions/Current/Commands/mono" --debug "$HOME/weblint/bin/Debug/weblint.exe" ; echo $? > /var/folders/w5/9kg3d9h93rz3ks72r74wbb_r0000gn/T/tmp32ed623c.tmp; echo; read -p "Press any key to continue..." -n1; exit'; exit;
