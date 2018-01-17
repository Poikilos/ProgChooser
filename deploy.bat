@echo off
set MYFOLDERNAME=ProgChooser
set MYEXENAME=ProgChooser.exe
REM set AllUsersProjects=C:\Documents and Settings\All Users\Documents\Projects
set AllUsersProjects=C:\ProgramData
REM IF EXIST "C:\Users\Public\Documents" set AllUsersProjects=C:\Users\Public\Documents\Projects
IF NOT EXIST "%AllUsersProjects%" set AllUsersProjects=C:\PortableApps
REM IF EXIST "C:\Users\Public\Projects" set AllUsersProjects=C:\Users\Public\Projects
REM IF EXIST "C:\Users\Public" set AllUsersProjects=C:\Users\Public\Projects
md "%AllUsersProjects%\%MYFOLDERNAME%"
REM md "%AllUsersProjects%\%MYFOLDERNAME%\bin"
REM NOTE: ON NEXT LINE ONLY DELETE COPY IF AN ORIGINAL EXISTS (otherwise user may lose access to program)!
IF EXIST "%AllUsersProjects%\%MYFOLDERNAME%\%MYEXENAME%" del "%AllUsersProjects%\%MYFOLDERNAME%\%MYFOLDERNAME% - Copy.exe"
IF EXIST "%AllUsersProjects%\%MYFOLDERNAME%\%MYEXENAME%" Copy /y "%AllUsersProjects%\%MYFOLDERNAME%\%MYEXENAME%" "%AllUsersProjects%\%MYFOLDERNAME%\%MYFOLDERNAME% - Copy.exe"
REM IF EXIST "%AllUsersProjects%\%MYFOLDERNAME%\%MYEXENAME%" del "%AllUsersProjects%\%MYFOLDERNAME%\%MYEXENAME%"

set SOURCE_EXE=.\bin\%MYEXENAME%
IF EXIST "%SOURCE_EXE%" GOTO COPYEXE
set SOURCE_EXE=\\%COMPUTERNAME%\c$\Users\jgustafson\OneDrive\Projects-cs\%MYFOLDERNAME%\bin\%MYEXENAME%
IF EXIST "%SOURCE_EXE%" GOTO COPYEXE
set SOURCE_EXE=\\%COMPUTERNAME%\c$\Users\jgustafson\SkyDrive\Projects-cs\%MYFOLDERNAME%\bin\%MYEXENAME%
IF EXIST "%SOURCE_EXE%" GOTO COPYEXE
set SOURCE_EXE=\\Hslab-teacher\c$\Users\jgustafson\SkyDrive\Projects-cs\%MYFOLDERNAME%\bin\%MYEXENAME%
IF EXIST "%SOURCE_EXE%" GOTO COPYEXE
set SOURCE_EXE=\\Hslab-teacher\c$\Users\jgustafson\OneDrive\Projects-cs\%MYFOLDERNAME%\bin\%MYEXENAME%
IF EXIST "%SOURCE_EXE%" GOTO COPYEXE

GOTO NOSOURCE

:COPYEXE
copy /y "%SOURCE_EXE%" "%AllUsersProjects%\%MYFOLDERNAME%\"
IF ERRORLEVEL 1 goto COPY_ERROR
echo Copied 
GOTO COPIED %SOURCE_EXE% to %AllUsersProjects%\%MYFOLDERNAME%\

:COPY_ERROR
echo Failed to copy %SOURCE_EXE% to "%AllUsersProjects%\%MYFOLDERNAME%\"
GOTO PAUSEPROGRAM
:NOSOURCE
echo Failed to find source program such as %SOURCE_EXE%

:PAUSEPROGRAM
pause

:COPIED

:ENSURE_INSTALLER_IF_ON_DEST_JUST_SHOWS_WARNING
REM rename "%AllUsersProjects%\%MYFOLDERNAME%\deploy.bat" "deploy_bat"
REM rename "%AllUsersProjects%\%MYFOLDERNAME%\deploywithsource.bat" "deploywithsource_bat"
echo echo deploy from source code folder instead > "%AllUsersProjects%\%MYFOLDERNAME%\deploy.bat"
echo pause >> "%AllUsersProjects%\%MYFOLDERNAME%\deploy.bat"
echo explorer "\\AUCTIONSVR\C$\Users\jgustafson\OneDrive\Projects-cs\ProgChooser" >> "%AllUsersProjects%\%MYFOLDERNAME%\deploy.bat"

echo echo deploy from source code folder instead > "%AllUsersProjects%\%MYFOLDERNAME%\deploywithsource.bat"
echo pause >> "%AllUsersProjects%\%MYFOLDERNAME%\deploywithsource.bat"
echo explorer "\\AUCTIONSVR\C$\Users\jgustafson\OneDrive\Projects-cs\ProgChooser" >> "%AllUsersProjects%\%MYFOLDERNAME%\deploywithsource.bat"
REM KEEP PREVIOUS 4 LINES (or 6 for privacy) OTHERWISE USER MAY MESS UP THE ORIGINAL WHEN RUN FROM DEST (TRIES TO COPY OVER SELF AND MAY CAUSE DELETIONS)!

REM pause
explorer "%AllUsersProjects%\%MYFOLDERNAME%\"