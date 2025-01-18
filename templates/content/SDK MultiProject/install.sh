#!/bin/bash

folder=MyLanguage.Build.Tasks
dotnet build $folder/MyLanguage.Sdk.csproj

# collect paths
dotnet_path=$(dirname $(which dotnet))
latest_version=$(ls -d "$dotnet_path/sdk/"* | sort -V | tail -n 1 | xargs basename)
target_path="$dotnet_path/sdk/$latest_version/Sdks/MyLanguage.Sdk"

mkdir -p "$target_path"

cp -R $folder/Sdk "$target_path"
cp -R $folder/Core "$target_path"
cp -R $folder/Tasks "$target_path"


debug_path="$folder/bin/Debug/$dotnet_version"
tasks_bin_path="$target_path/Tasks/bin"
mkdir -p "$tasks_bin_path"
cp -R "$debug_path"/* "$tasks_bin_path"

echo "Installation was successfully."
