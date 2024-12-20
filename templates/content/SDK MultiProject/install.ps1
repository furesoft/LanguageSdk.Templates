$dotnetPath = (Get-Command dotnet).Source | Split-Path -Parent

$latestVersion = (Get-ChildItem "$dotnetPath\sdk" | Sort-Object Name -Descending | Select-Object -First 1).Name

$targetPath = "$dotnetPath\sdk\$latestVersion\sdk\MyLanguage"
New-Item -ItemType Directory -Force -Path $targetPath

Copy-Item -Path ".\SDK" -Destination "$targetPath" -Recurse -Force
Copy-Item -Path ".\Core" -Destination "$targetPath" -Recurse -Force
Copy-Item -Path ".\Tasks" -Destination "$targetPath" -Recurse -Force

$debugPath = ".\bin\Debug\$latestVersion"
$tasksBinPath = "$targetPath\Tasks\bin"
New-Item -ItemType Directory -Force -Path $tasksBinPath
Copy-Item -Path "$debugPath\*" -Destination $tasksBinPath -Recurse -Force

Write-Host "Installation successfull"
