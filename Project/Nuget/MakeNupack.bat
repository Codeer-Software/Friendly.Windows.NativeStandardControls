rd /s /q "../ReleaseBinary"
"C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\IDE\devenv.exe" "../Codeer.Friendly.Windows.NativeStandardControls/Codeer.Friendly.Windows.NativeStandardControls.sln" /rebuild Release
"C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\IDE\devenv.exe" "../Codeer.Friendly.Windows.NativeStandardControls/Codeer.Friendly.Windows.NativeStandardControls.sln" /rebuild Release-English
nuget pack friendly.windows.nativestandardcontrols.nuspec