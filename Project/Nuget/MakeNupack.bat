rd /s /q "../ReleaseBinary"
"%DevEnvDir%devenv.exe" "../Codeer.Friendly.Windows.NativeStandardControls/Codeer.Friendly.Windows.NativeStandardControls.sln" /rebuild Release
"%DevEnvDir%devenv.exe" "../Codeer.Friendly.Windows.NativeStandardControls/Codeer.Friendly.Windows.NativeStandardControls.sln" /rebuild Release-English
nuget pack friendly.windows.nativestandardcontrols.nuspec