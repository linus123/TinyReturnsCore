properties {
	$projectName = "TinyReturns"
	$baseDir = resolve-path ..\..\

	$buildFolder = "$baseDir\.build"
	$packagesFolder = "$buildFolder\packages"
	$srcFolder = "$baseDir\src"
	$libFolder = "$baseDir\lib"
	
	$databaseServer = "(localdb)\MSSQLLocalDB"
	$databaseName = $projectName

	$databaseScriptsFolder = "$baseDir\data\mssql\TinyReturns"
	
	$solutionFile = "$srcFolder\TinyReturns.sln"

	$nuGetExec = (Find-PackagePath $libFolder "nuget") + "\nuget.exe"
}

task default -depends GetNuGetPackages, ResetDatabase

FormatTaskName "`r`n`r`n-------- Executing {0} Task --------"

task GetNuGetPackages {
	Exec {
		&$nuGetExec Install roundhouse -o $packagesFolder
	}
}

task ResetDatabase {

	$roundhouseExec = (Find-PackagePath $packagesFolder "roundhouse") + "\tools\rh.exe"

	$versionFile = "$databaseScriptsFolder\_BuildInfo.xml"
	$versionPath = "//buildInfo/version"

	Exec {
		&$roundhouseExec /s=$databaseServer /d=$databaseName /f=$databaseScriptsFolder /env=local /vf=$versionFile /vx=$versionPath /drop /silent
	}


	Exec {
		&$roundhouseExec /s=$databaseServer /d=$databaseName /f=$databaseScriptsFolder /env=local /vf=$versionFile /vx=$versionPath /simple /silent
	}
}

# --------------------------

function Find-PackagePath
{
	[CmdletBinding()]
	param(
		[Parameter(Position=0,Mandatory=1)]$packagesPath,
		[Parameter(Position=1,Mandatory=1)]$packageName
	)

	return (Get-ChildItem ($packagesPath + "\" + $packageName + "*")).FullName | Sort-Object $_ | select -last 1
}