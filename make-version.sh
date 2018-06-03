#!/bin/sh

export version=`cat dialogic/dialogic.csproj | grep "<PackageVersion" | sed 's/ *<PackageVersion>\([0-9.]*\).*/\1/'`

#echo VERSION=$version
#if [ $# -lt "1"  ]
#then
#    echo
#    echo "  error:   version required"
#    echo
#    echo "  usage:   make-docs.sh [1.3.9]"
#    exit
#fi

doxygen docs/Doxyfile 2>&1 | grep -v "Unexpected html tag <img>"
perl -i.bak -p0e 's%<p><a href="https://travis.*?</object>\n</div>%%igs' docs/html/index.html
perl -i.bak -p0e 's%: Dialogic :fish:</title>% v$ENV{version}</title>%igs' docs/html/index.html
perl -i.bak -p0e 's%:fish:%%igs' docs/html/index.html
rm docs/html/index.html.bak

perl -p0e 's%Dialogic</title>%Dialogic v$ENV{version}</title>%igs' editor/data/template.html > editor/data/index.html

#head docs/html/index.html
#head -n 70 docs/html/index.html

echo git-tag.sh $version
