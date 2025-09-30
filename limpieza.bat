@echo off
echo ===========================================
echo  LIMPIEZA DE PROYECTO UNITY
echo ===========================================

REM --- Ir a la carpeta del script
cd /d "%~dp0"

REM --- Borrar carpetas de Unity (se regeneran solas)
echo Eliminando carpetas de Unity...
if exist Library rmdir /s /q Library
if exist Temp rmdir /s /q Temp
if exist Logs rmdir /s /q Logs

REM --- Borrar carpetas de compilación generadas por SDK
echo Eliminando obj y bin en la raíz...
if exist obj rmdir /s /q obj
if exist bin rmdir /s /q bin

REM --- Borrar carpetas de compilación dentro de Assets
echo Eliminando obj y bin dentro de Assets...
if exist Assets\obj rmdir /s /q Assets\obj
if exist Assets\bin rmdir /s /q Assets\bin

REM --- Borrar posibles archivos de configuración de SDK en Assets
echo Eliminando archivos de proyecto extraños dentro de Assets...
del /s /q Assets\*.csproj >nul 2>&1
del /s /q Assets\Directory.Build.* >nul 2>&1

echo.
echo ===========================================
echo  LIMPIEZA COMPLETA
echo  Ahora abre Unity y deja que reimporte el proyecto.
echo ===========================================
pause
