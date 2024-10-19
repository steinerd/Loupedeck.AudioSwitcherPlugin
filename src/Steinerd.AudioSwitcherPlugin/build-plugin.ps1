$version = "1.0"
$project = "AudioSwitcher"
$assemblyName = "Steinerd.AudioSwitcherPlugin"
$dllName = "$assemblyName.dll"
$pluginPath= "$($env:LOCALAPPDATA)\Logi\LogiPluginService\Plugins\"
$buildPath = ".builds"
$outputFileName = "$project"
$zipPath = "$buildPath\$outputFileName.zip"
$pluginName = "$outputFileName.lplug4"
$loupedeckYaml = ".\metadata\LoupedeckPackage.yaml"
$cwd = Get-Location

$dllPath = (Get-Content $pluginPath\$assemblyName.link).Trim()

New-Item -Path "$buildPath" -Force -Name "bin" -ItemType "directory" > $null
New-Item -Path "$buildPath\bin" -Force -Name "win" -ItemType "directory" > $null

Copy-Item $loupedeckYaml -Force -Destination $buildPath > $null
Copy-Item "$dllPath*" -Destination "$buildPath\bin\win\" -Recurse > $null

$compress = @{
	Path = "$buildPath\*"
	CompressionLevel = "Fastest"
	DestinationPath = $zipPath
}
Compress-Archive @Compress > $null

Rename-Item $zipPath -Force -NewName $pluginName > $null