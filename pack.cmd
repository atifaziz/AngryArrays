@echo off
pushd "%~dp0"
call build ^
    && NuGet pack -Symbol AngryArrays.nuspec ^
    && NuGet pack AngryArrays.Source.nuspec
popd
goto :EOF
