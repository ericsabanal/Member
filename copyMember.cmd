@echo off

set basedir=\\ERIC-PC\Users\Public
set folder=Member
set src=C:\Users\csf\Desktop\Member
set dest=%basedir%\Visual Studio 2023\%folder%

echo.
echo.
echo This is copying Member project to My PC 192.168.13.74
echo.
echo.
pause.


set opts=/mir /w:2 /r:2

robocopy %opts% "%src%" "%dest%"