# expects DialogicEditor-X.Y.Z.zip built for the platform 

if [ $# -lt "1"  ]
then
    echo
    echo "  error:   version required"
    echo
    echo "  usage:   exec-editor.sh [1.3.9]"
    echo
    exit
fi

rm -rf exec/*
unzip -d exec DialogicEditor-$1.zip
chmod +x exec/DialogicEditor
cd exec && ./DialogicEditor rednoise.org
