mode con:cols=140 lines=2500

nuget.exe update -self

CALL Build.bat

mkdir Publish
NuGet Pack TypescriptGeneration.nuspec -OutputDirectory Publish
Nuget push "Publish\TypescriptGeneration*.nupkg" -Source https://www.nuget.org/api/v2/package
rmdir Publish /s /q
pause