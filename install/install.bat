@echo off
SET DESTFOLDERFULLNAME=C:\ProgramData\ProgChooser
md %DESTFOLDERFULLNAME%
SET SRC_FILE_PATH=..\bin\Release\ProgChooser.exe
IF NOT EXIST "%SRC_FILE_PATH%" echo ERROR: nothing done since missing %SRC_FILE_PATH% (compile first such as in SharpDevelop)
IF NOT EXIST "%SRC_FILE_PATH%" GOTO ON_ERROR
copy ..\ProgChooser.exe "%DESTFOLDERFULLNAME%\"
IF ERRORLEVEL 1 goto ON_ERROR

regedit.exe /s .\reg\HKCR Applications ProgChooser.exe.reg
IF ERRORLEVEL 1 goto ON_ERROR
regedit.exe /s .\reg\HKCR sln_auto_file.reg
IF ERRORLEVEL 1 goto ON_ERROR
regedit.exe /s .\reg\HKCU Software Classes Applications ProgChooser.exe.reg
IF ERRORLEVEL 1 goto ON_ERROR
regedit.exe /s .\reg\HKCU Software Classes sln_auto_file.reg
IF ERRORLEVEL 1 goto ON_ERROR
regedit.exe /s .\reg\HKCU Software Microsoft Windows CurrentVersion Explorer FileExts .sln.reg
IF ERRORLEVEL 1 goto ON_ERROR
regedit.exe /s .\reg\HKLM SOFTWARE Classes sln_auto_file.reg
IF ERRORLEVEL 1 goto ON_ERROR

GOTO ENDSILENTLY
:ON_ERROR
pause
:ENDSILENTLY