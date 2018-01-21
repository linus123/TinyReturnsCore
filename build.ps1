$baseDir = resolve-path .
$buildFolder = "$baseDir\.build"
$nuGetExec = "$baseDir\lib\nuget_v4.5.0\nuget.exe"

if (Test-Path $buildFolder) {
	rd $buildFolder -rec -force | out-null
}

mkdir $buildFolder | out-null

&$nuGetExec Install psake -o "$buildFolder\package"