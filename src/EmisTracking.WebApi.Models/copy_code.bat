@echo off
:: Set the output file name
set OUTPUT_FILE=MergedCode.txt

:: Delete the output file if it exists (to prevent appending to old content)
if exist "%OUTPUT_FILE%" del "%OUTPUT_FILE%" > nul

:: Write a header to the output file
echo Merged C# Files >> "%OUTPUT_FILE%"
echo ---------------- >> "%OUTPUT_FILE%"
echo. >> "%OUTPUT_FILE%"

:: Recursive function to go through subdirectories and merge .cs files
call :ProcessFolder "%cd%"

:: Notify that the process is complete
echo All .cs files have been recursively merged into "%OUTPUT_FILE%".
pause
exit /b

:ProcessFolder
setlocal
set current_folder=%~1

:: Loop through all .cs files in the current folder
for %%f in ("%current_folder%\*.cs") do (
    :: Write the relative path of the file as a header
    echo %%~pnxf >> "%OUTPUT_FILE%"
    echo. >> "%OUTPUT_FILE%"
    :: Append the file's contents
    type "%%f" >> "%OUTPUT_FILE%"
    echo. >> "%OUTPUT_FILE%"
)

:: Process subdirectories recursively
for /d %%d in ("%current_folder%\*") do (
    call :ProcessFolder "%%d"
)

endlocal
exit /b