@echo off

set "sourceFolder=%~dp0\Maps"
set "destinationFolder=%~dp0\bin\Debug\net6.0"

xcopy "%sourceFolder%\*" "%destinationFolder%\" /s /i /y

