rd /s /q "../ReleaseBinary"
"C:\Program Files\Microsoft Visual Studio\2022\Professional\Common7\IDE\devenv.exe" "../Codeer.Friendly.Windows.NativeStandardControls/Codeer.Friendly.Windows.NativeStandardControls.sln" /rebuild Release
"C:\Program Files\Microsoft Visual Studio\2022\Professional\Common7\IDE\devenv.exe" "../Codeer.Friendly.Windows.NativeStandardControls/Codeer.Friendly.Windows.NativeStandardControls.sln" /rebuild Release-English
nuget pack friendly.windows.nativestandardcontrols.nuspec