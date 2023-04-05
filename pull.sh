#!/usr/bin/env sh
echo moment of doubt!
read
rm -r Assets Logs Packages Temp UserSettings
git fetch --all
git reset --hard origin/main
