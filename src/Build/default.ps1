properties {
	$projectName = "TinyReturns"
	$baseDir = resolve-path .

	$buildFolder = "$baseDir\.build"
	$srcFolder = "$baseDir\src"
	
	$databaseServer = "(localdb)\MSSQLLocalDB"
	$databaseName = $projectName

	$databaseScriptsFolder = "$baseDir\data\mssql\TinyReturns"

	$roundhouseExec = "$srcFolder\"
	
	$solutionFile = "$srcFolder\TinyReturns.sln"
}