properties {
	$projectName = "TinyReturns"
	$baseDir = resolve-path ..\..\

	$buildFolder = "$baseDir\.build"

	$buildConfig = "Release"
	$buildTargetFolder = "$buildFolder\$buildConfig"

	$packagesFolder = "$buildFolder\packages"
	$srcFolder = "$baseDir\src"
	$libFolder = "$baseDir\lib"
	
	$databaseServer = "(localdb)\MSSQLLocalDB"
	$databaseName = $projectName

	$databaseScriptsFolder = "$baseDir\data\mssql\TinyReturns"
	
	$solutionFile = "$srcFolder\TinyReturnsCore.sln"

	$nuGetExec = (Find-PackagePath $libFolder "nuget") + "\nuget.exe"
}

task default -depends RequiresDotNet, RequiresMSBuild, Init, GetNuGetPackages, CompileAssembly, ResetDatabase

FormatTaskName "`r`n`r`n-------- Executing {0} Task --------"

task Init {
    if (Test-Path $buildFolder) {
        rd $buildFolder -rec -force | out-null
    }

    mkdir $buildFolder | out-null
}

task RequiresDotNet {
	$script:dotnetExe = (get-command dotnet).Source

    if ($dotnetExe -eq $null) {
        throw "Failed to find dotnet.exe"
    }

    Write-Host "Found dotnet here: $dotnetExe"
}

Task RequiresMSBuild {

    $script:msbuildExe = 
        resolve-path "C:\Program Files (x86)\Microsoft Visual Studio\*\*\MSBuild\*\Bin\MSBuild.exe"

    if ($msbuildExe -eq $null)
    {
        throw "Failed to find MSBuild"
    }

    Write-Host "Found MSBuild here: $msbuildExe"
}

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

task CompileAssembly -Depends RequiresMSBuild {
    exec { 
        & $dotnetExe msbuild /p:Configuration=$buildConfig /p:OutDir="$buildTargetFolder\" $solutionFile
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