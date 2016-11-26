$ErrorActionPreference = "Stop"

$packageVersion = ($env:PackageVersion)
$configuration = ($env:Configuration)
$msBuildExe = ($env:MsBuildExe)
$msBuildTarget = ($env:Targets)

$CommandDirectory = split-path -parent $script:MyInvocation.MyCommand.Path
set-location "$workingDirectory"
cd ../
$rootFolder = (Get-Location).Path
Write-Host Path is $rootFolder
. $CommandDirectory\MyGet.Build.ps1
