@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)

set version=
if not "%PackageVersion%" == "" (
   set version=-Version %PackageVersion%
)

REM Build
"%programfiles(x86)%\MSBuild\14.0\Bin\MSBuild.exe" Smooth.IoC.Dapper.Repository.UnitOfWork.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false
