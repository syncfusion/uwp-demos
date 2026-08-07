@echo off
setlocal enabledelayedexpansion

:: Check if version argument is provided
if "%~1"=="" (
    echo Usage: RenameSDKReference.bat [version]
    echo Example: RenameSDKReference.bat 20.1.0.1
    exit /b 1
)

set VERSION=%~1
echo Updating SDK references and version to "%VERSION%"...

:: Loop through all .csproj files in the current directory and subdirectories
for /r %%f in (*.csproj) do (
    echo Processing "%%f"...
    
    :: Replace the SDKReference Version in Include attribute
    powershell -Command "(Get-Content '%%f') -replace 'Syncfusion Document SDK for UWP XAML, Version=[\d.]+', 'Syncfusion Document SDK for UWP XAML, Version=%VERSION%' | Set-Content '%%f'"
)

echo Done. Renamed SDK references and updated version to "%VERSION%" in all .csproj files.