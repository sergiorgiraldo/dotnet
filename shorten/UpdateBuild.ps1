#Get Path to csproj
$path = "$PSScriptRoot\shorten.csproj"

#Read csproj (XML)
$xml = [xml](Get-Content -Encoding UTF8 $path)

#Retrieve Version Nodes
$assemblyVersion = $xml.Project.PropertyGroup.AssemblyVersion
$fileVersion = $xml.Project.PropertyGroup.FileVersion

#Split the Version Numbers
$avMajor, $avMinor, $avBuild, $avRelease  = $assemblyVersion.Split(".")
$fvMajor, $fvMinor, $fvBuild, $fvRelease = $fileVersion.Split(".")

#Increment Revision
$avRelease = [Convert]::ToInt32($avRelease,10)+1
$fvRelease = [Convert]::ToInt32($fvRelease,10)+1

#Put new version back into csproj (XML)
$xml.Project.PropertyGroup.AssemblyVersion = "$avMajor.$avMinor.$avBuild.$avRelease"
$xml.Project.PropertyGroup.FileVersion = "$fvMajor.$fvMinor.$fvBuild.$fvRelease"

#Save csproj (XML)
$xml.Save($path)