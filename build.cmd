@echo off
setlocal
pushd "%~dp0"
nuget restore ^
    && call :build Debug ^
    && call :test Debug ^
    && call :build Release ^
    && call :test Release
popd
goto :EOF

:build
if "%PROCESSOR_ARCHITECTURE%"=="x86" set MSBUILD=%ProgramFiles%
if defined ProgramFiles(x86) set MSBUILD=%ProgramFiles(x86)%
set MSBUILD=%MSBUILD%\MSBuild\14.0\bin\msbuild
if not exist "%MSBUILD%" (
    echo Microsoft Build Tools 2015 does not appear to be installed on this
    echo machine, which is required to build the solution. You can install
    echo it from the URL below and then try building again:
    echo https://www.microsoft.com/en-us/download/detailscd .aspx?id=48159
    exit /b 1
)
"%MSBUILD%" AngryArrays.sln /p:Configuration=%1 /v:m %2 %3 %4 %5 %6 %7 %8 %9
goto :EOF

:test
packages\NUnit.Runners.2.6.4\tools\nunit-console.exe bin\%1\AngryArrays.Tests.dll
goto :EOF

