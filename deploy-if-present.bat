@ECHO OFF
SET EXE_NAME=ProgChooser.exe
C:
SET DEST_DIR=C:\ProgramData\ProgChooser
IF NOT EXIST "%DEST_DIR%" SET DEST_DIR=%USERPROFILE%\ProgChooser
IF EXIST "%DEST_DIR%\%EXE_NAME%" GOTO COPYFILES
SET DEST_DIR=%USERPROFILE%\Projects\ProgChooser
IF EXIST "%DEST_DIR%\%EXE_NAME%" GOTO COPYFILES
SET DEST_DIR=%USERPROFILE%\Documents\Projects\ProgChooser
IF EXIST "%DEST_DIR%\%EXE_NAME%" GOTO COPYFILES
SET DEST_DIR=C:\ProgramData\ProgChooser
IF EXIST "%DEST_DIR%\%EXE_NAME%" GOTO COPYFILES

echo An existing copy of %EXE_NAME% was not found in any known destination (nothing will be done)
PAUSE
GOTO ENDSILENTLY

:COPYFILES
copy 

:ENDSILENTLY