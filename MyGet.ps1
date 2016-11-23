$ErrorActionPreference = "Stop"

$packageVersion = ($env:PackageVersion)
$configuration = ($env:Configuration)
$msBuildExe = ($env:MsBuildExe)
$msBuildTarget = ($env:Targets)

#$packageVersion = "2.4.0-preview1"
#$configuration = "Release"
#$msBuildExe = "C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe"
#$msBuildTarget = "Build"

Write-Host "Loading MyGet.include.ps1"
# Initialization
$rootFolder = Split-Path -parent $script:MyInvocation.MyCommand.Path
. $rootFolder\myget.include.ps1
# Valid build runners
$validBuildRunners = @("myget")

MyGet-Write-Diagnostic " "
MyGet-Write-Diagnostic " "
MyGet-Write-Diagnostic "###### restore dependencies ######"
& "$PSScriptRoot\.nuget\nuget.exe" restore "$PSScriptRoot\Smooth.IoC.Dapper.Repository.UnitOfWork.sln"
if ($LASTEXITCODE -ne 0){
    MyGet-Die "nuget restore failed"
}

MyGet-Write-Diagnostic " "
MyGet-Write-Diagnostic " "
MyGet-Write-Diagnostic "###### Build debug ######"
& "$msBuildExe" "$PSScriptRoot\Smooth.IoC.Dapper.Repository.UnitOfWork.sln" /t:"$msBuildTarget" /p:Configuration="Debug"
if ($LASTEXITCODE -ne 0){
    MyGet-Die "build failed"
}
MyGet-Write-Diagnostic " "
MyGet-Write-Diagnostic " "
MyGet-Write-Diagnostic "###### Run all tests ######"
& "$PSScriptRoot\.nunit\nunit3-console.exe" "$PSScriptRoot\src\Smooth.IoC.Dapper.Repository.UnitOfWork.Tests\bin\Debug\net452\Smooth.IoC.Dapper.Repository.UnitOfWork.Tests.dll" --framework=net-4.5
if ($LASTEXITCODE -ne 0){
    MyGet-Die "tests failed"
}


MyGet-Write-Diagnostic " "
MyGet-Write-Diagnostic " "
MyGet-Write-Diagnostic "###### build configuration target the solution ######"
& "$msBuildExe" "$PSScriptRoot\Smooth.IoC.Dapper.Repository.UnitOfWork.sln" /t:"$msBuildTarget" /p:Configuration="$configuration"
if ($LASTEXITCODE -ne 0){
    MyGet-Die "build failed"
}

MyGet-Write-Diagnostic " "
MyGet-Write-Diagnostic " "
MyGet-Write-Diagnostic "###### Create the NuGet packages ######"
& "$PSScriptRoot\.nuget\nuget.exe" pack "$PSScriptRoot\NuGetSpecs\Smooth.IoC.Dapper.Repository.UnitOfWork.nuspec" -OutputDirectory "$PSScriptRoot\Releases" -Version "$packageVersion" -Properties configuration="$configuration" -Verbosity detailed
if ($LASTEXITCODE -ne 0){
    MyGet-Die "Nuget library packaging failed"
}
MyGet-Build-Success
