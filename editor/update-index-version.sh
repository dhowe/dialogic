#!/bin/sh

export version=`cat ../dialogic/dialogic.csproj | grep "<PackageVersion" | sed 's/ *<PackageVersion>\([0-9.]*\).*/\1/'`

perl -p0e 's%Dialogic</title>%Dialogic v$ENV{version}</title>%igs' data/template.html > data/index.html

head -n5 data/index.html
echo
echo Updated index.html with version $version
echo
