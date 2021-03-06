#Get Path to csproj
$path = "$PSScriptRoot\shorten.csproj"

#Read csproj (XML)
$xml = [xml](Get-Content -Encoding UTF8 $path)

#Retrieve Version Nodes
$assemblyVersion = $xml.Project.PropertyGroup.AssemblyVersion
$fileVersion = $xml.Project.PropertyGroup.FileVersion

#Splitthe Version Numbers
$avMajor, $avMinor, $avBuild = $assemblyVersion.Split(".")
$fvMajor, $fvMinor, $fvBuild = $fileVersion.Split(".")

#Increment Build
$avBuild= [Convert]::ToInt32($avBuild,10)+1
$fvBuild= [Convert]::ToInt32($fvBuild,10)+1

#Put new version back into csproj (XML)
$xml.Project.PropertyGroup.AssemblyVersion = "$avMajor.$avMinor.$avBuild"
$xml.Project.PropertyGroup.FileVersion = "$fvMajor.$fvMinor.$fvBuild"

#Save csproj (XML)
$xml.Save($path)