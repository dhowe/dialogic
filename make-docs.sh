#!/bin/sh

if [ $# -lt "1"  ]
then
    echo
    echo "  error:   version required"
    echo
    echo "  usage:   make-docs.sh [1.3.9]"
    exit
fi

export version=$1
doxygen docs/Doxyfile 2>&1 | grep -v "Unexpected html tag <img>"
perl -i.bak -p0e 's%<p><a href="https://travis.*?</object>\n</div>%%igs' docs/html/index.html
perl -i.bak -p0e 's%: Dialogic</title>% v$ENV{version}</title>%igs' docs/html/index.html
rm docs/html/index.html.bak

