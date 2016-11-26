$ErrorActionPreference = "Stop"

$packageVersion = ($env:PackageVersion)
$configuration = ($env:Configuration)
$msBuildExe = ($env:MsBuildExe)
$msBuildTarget = ($env:Targets)

$CommandDirectory = split-path -parent $script:MyInvocation.MyCommand.Path
$rootFolder = ($env:SourcesPath)
. $CommandDirectory\MyGet.Build.ps1
