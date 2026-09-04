@echo off
setlocal

cd "%~dp0"

For %%a in (
"AlliED\bin\Release\net48\*.dll"
"AlliED\bin\Release\net48\*.exe"
"AlliED\bin\Release\net48\*.config"
"AlliED\bin\Release\net48\*.pdb"
) do (
xcopy /s /d "%%~a" dist\
)
