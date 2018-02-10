mode con:cols=140 lines=2500

CALL "%VS140COMNTOOLS%"\\vsvars32.bat

msbuild.exe "..\TypescriptGeneration.sln" /t:Rebuild /m /p:Configuration=Release
