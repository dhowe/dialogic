# run from directory with DialogicEditor-X.Y.Z.zip
# created for the required platform 

rm -rf tmp/*
unzip -d tmp DialogicEditor-0.6.05.zip
chmod +x tmp/DialogicEditor
cd tmp && ./DialogicEditor rednoise.org
