@echo off
cd /d %~dp0
C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe -i "WinFundStatisticsService.exe"
net start BaoLiFundStatisticsSerivce
pause
@pause



