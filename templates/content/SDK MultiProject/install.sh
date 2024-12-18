#!/bin/bash

dotnet_path=$(dirname $(which dotnet))

latest_version=$(ls -d $dotnet_path/sdk/* | sort -V | tail -n 1 | xargs basename)

target_path="$dotnet_path/sdk/$latest_version/sdk/MyLanguage"
mkdir -p "$target_path"

cp -R ./SDK "$target_path"
cp -R ./Core "$target_path"
cp -R ./Tasks "$target_path"

debug_path="./bin/Debug/$latest_version"
tasks_bin_path="$target_path/Tasks/bin"
mkdir -p "$tasks_bin_path"
cp -R "$debug_path"/* "$tasks_bin_path"

echo "Installation successfull."
