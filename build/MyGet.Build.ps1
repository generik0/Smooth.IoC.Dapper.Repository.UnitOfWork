$ErrorActionPreference = "Stop"

# Initialization
Write-Host "Loading MyGet.include.ps1"
. $CommandDirectory\myget.include.ps1
# Valid build runners
$validBuildRunners = @("myget")

MyGet-Write-Diagnostic " "
MyGet-Write-Diagnostic " "
MyGet-Write-Diagnostic "###### restore dependencies ######"
& "$CommandDirectory\.nuget\nuget.exe" restore "$rootFolder\Smooth.IoC.Dapper.Repository.UnitOfWork.sln"
if ($LASTEXITCODE -ne 0){
    MyGet-Die "nuget restore failed"
}

MyGet-Write-Diagnostic " "
MyGet-Write-Diagnostic " "
MyGet-Write-Diagnostic "###### Build debug ######"
& "$msBuildExe" "$rootFolder\Smooth.IoC.Dapper.Repository.UnitOfWork.sln" /t:"$msBuildTarget" /p:Configuration="Debug"
if ($LASTEXITCODE -ne 0){
    MyGet-Die "build failed"
}
MyGet-Write-Diagnostic " "
MyGet-Write-Diagnostic " "
MyGet-Write-Diagnostic "###### Run all tests ######"
dotnet test $rootFolder\src\Smooth.IoC.Dapper.Repository.UnitOfWork.Tests\ --no-build
if ($LASTEXITCODE -ne 0){
    MyGet-Die "tests failed"
}


MyGet-Write-Diagnostic " "
MyGet-Write-Diagnostic " "
MyGet-Write-Diagnostic "###### build configuration target the solution ######"
& "$msBuildExe" "$rootFolder\Smooth.IoC.Dapper.Repository.UnitOfWork.sln" /t:"$msBuildTarget" /p:Configuration="$configuration"
if ($LASTEXITCODE -ne 0){
    MyGet-Die "build failed"
}

MyGet-Write-Diagnostic " "
MyGet-Write-Diagnostic " "
MyGet-Write-Diagnostic "###### Create the NuGet packages ######"
& "$rootFolder\.nuget\nuget.exe" pack "$rootFolder\NuGetSpecs\Smooth.IoC.Dapper.Repository.UnitOfWork.nuspec" -OutputDirectory "$rootFolder\Releases" -Version "$packageVersion" -Properties configuration="$configuration" -Verbosity detailed
if ($LASTEXITCODE -ne 0){
    MyGet-Die "Nuget library packaging failed"
}
MyGet-Build-Success
