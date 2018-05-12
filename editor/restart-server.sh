#!/bin/bash

#ssh -t $RED "cd /var/www/html/dialogic-gh && git stash && git pull && cd editor && dotnet run rednoise.org"
ssh -t $RED "cd /var/www/html/dialogic-gh && git stash && git pull && msbuild"
ssh -t $RED "systemctl restart dialogic-editor.service"
