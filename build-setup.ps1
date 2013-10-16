# set msbuild path
$lib = [System.Runtime.InteropServices.RuntimeEnvironment]
$rtd = $lib::GetRuntimeDirectory()
$env:Path = "$env:Path;$rtd";

$ErrorActionPreference = "Stop"

(git log -1 | where { $_ -match "commit|date" }) -join "`n" > VSS3WayMerge\bin\build-info.txt

msbuild VSS3WayMerge\Vss3WayMerge.csproj /target:Clean /p:Configuration=Release
msbuild Setup\Setup.wixproj /target:Clean /p:Configuration=Release

msbuild VSS3WayMerge\Vss3WayMerge.csproj /target:Build /p:Configuration=Release
msbuild Setup\Setup.wixproj /target:Build /p:Configuration=Release

rm Setup-*.msi
mv Setup\bin\Release\Setup.msi .\Setup-VSS3WayMerge-0.9.x.msi