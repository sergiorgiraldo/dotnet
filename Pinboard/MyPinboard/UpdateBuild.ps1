#Get Path to csproj
$path = "$PSScriptRoot\MyPinboard.csproj"

#Read csproj (XML)
$xml = [xml](Get-Content -Encoding UTF8 $path)

#Retrieve Version Nodes
$assemblyVersion = $xml.Project.PropertyGroup.AssemblyVersion
$fileVersion = $xml.Project.PropertyGroup.FileVersion

#Splitthe Version Numbers
$avMajor, $avMinor, $avPatch, $avBuild = $assemblyVersion.Split(".")
$fvMajor, $fvMinor, $fvPatch, $fvBuild = $fileVersion.Split(".")

#Increment Build
$avBuild= [Convert]::ToInt32($avBuild,10)+1
$fvBuild= [Convert]::ToInt32($fvBuild,10)+1

#Put new version back into csproj (XML)
$xml.Project.PropertyGroup.AssemblyVersion = "$avMajor.$avMinor.$avPatch.$avBuild"
$xml.Project.PropertyGroup.FileVersion = "$fvMajor.$fvMinor.$fvPatch.$fvBuild"
$xml.Project.PropertyGroup.Version = "$fvMajor.$fvMinor.$fvPatch"

#Save csproj (XML)
$xml.Save($path)