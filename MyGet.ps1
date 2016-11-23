$ErrorActionPreference = "Stop"

$packageVersion = ($env:PackageVersion)
$configuration = ($env:Configuration)
$msBuildExe = ($env:MsBuildExe)
$msBuildTarget = ($env:Targets)

# restore dependencies
& "$PSScriptRoot\.nuget\nuget.exe" restore "$PSScriptRoot\Smooth.IoC.Dapper.Repository.UnitOfWork.sln"
if ($LASTEXITCODE -ne 0){
    throw "nuget restore failed"
}

# build the solution
& "$msBuildExe" "$PSScriptRoot\Smooth.IoC.Dapper.Repository.UnitOfWork.sln" /t:"$msBuildTarget" /p:Configuration="$configuration"
if ($LASTEXITCODE -ne 0){
    throw "build failed"
}

& "$PSScriptRoot\.nunit\nunit-console.exe" "$PSScriptRoot\src\Smooth.IoC.Dapper.Repository.UnitOfWork.Tests\bin\$configuration\Smooth.IoC.Dapper.Repository.UnitOfWork.Tests.dll" /exclude=ExternalDatabase /noshadow  /framework:v4.5
if ($LASTEXITCODE -ne 0){
    throw "tests failed"
}

# create the NuGet packages
& "$PSScriptRoot\.nuget\nuget.exe" pack "$PSScriptRoot\NuGetSpecs\Smooth.IoC.Dapper.Repository.UnitOfWork.nuspec" -OutputDirectory "$PSScriptRoot\Releases" -Version "$packageVersion" -Properties configuration="$configuration" -Verbosity detailed
if ($LASTEXITCODE -ne 0){
    throw "Nuget library packaging failed"
}

