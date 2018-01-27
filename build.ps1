$baseDir = resolve-path .
$buildFolder = "$baseDir\.build"
$nuGetExec = "$baseDir\lib\nuget_v4.5.0\nuget.exe"

if (Test-Path $buildFolder) {
	rd $buildFolder -rec -force | out-null
}

mkdir $buildFolder | out-null

&$nuGetExec Install psake -o "$buildFolder\packages"

#----------------------

remove-module [p]sake

# find psake's path
$psakeModule = (Get-ChildItem ("$buildFolder\packages\psake*\tools\psake\psake.psm1")).FullName | Sort-Object $_ | select -last 1
 
Import-Module $psakeModule

# you can write statements in multiple lines using `
Invoke-psake -buildFile "$baseDir\src\Build\default.ps1"
#			 -taskList Clean `
#			 -framework 4.5.2 `
#		     -properties @{ 
#				 "buildConfiguration" = "Release"
#				 "buildPlatform" = "Any CPU"} `
#			 -parameters @{ 
#				 "solutionFile" = "..\psake.sln"
#				 "buildNumber" = $buildNumber
#				 "branchName" = $branchName
#				 "gitCommitHash" = $gitCommitHash
#				 "isMainBranch" = $isMainBranch}

Write-Host "Build exit code:" $LastExitCode

# Propagating the exit code so that builds actually fail when there is a problem
exit $LastExitCode