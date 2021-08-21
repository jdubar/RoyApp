$appName = "RoyApp"
$testName = "RoyAppTests"
$repoPath = "D:\Source\Repos\RoyApp"
$VSPath = "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow"
$sonarLogin = "4924be8f51f3e738c97db2c4f1531db7e938f28b"
$sonarURL = "http://192.168.36.52:9999"
$testFile = "$testName.dll"
$line = "%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%"
#-------------------------------------------------------------------------------------------------
Set-Location -Path "$repoPath\$appName"
#-------------------------------------------------------------------------------------------------
Write-Output $line
Write-Output "Running: dotnet sonarscanner begin /k:""$appName"" /d:sonar.host.url=""$sonarURL"" /d:sonar.login=""$sonarLogin"" /d:sonar.cs.vscoveragexml.reportsPaths=**/*.coveragexml"
Write-Output $line
& dotnet sonarscanner begin /k:"$appName" /d:sonar.host.url="$sonarURL" /d:sonar.login="$sonarLogin" /d:sonar.cs.vscoveragexml.reportsPaths=**/*.coveragexml
#-------------------------------------------------------------------------------------------------
Write-Output $line
Write-Output "Running: dotnet build"
Write-Output $line
& dotnet build
#-------------------------------------------------------------------------------------------------
Write-Output $line
Write-Output "Running: ""$VSPath\vstest.console.exe"" /EnableCodeCoverage """+$repoPath+"\$testName\bin\Release\.netcoreapp,version=v3.1\$testFile"""
Write-Output $line
$result = & "$VSPath\vstest.console.exe" /EnableCodeCoverage "$repoPath\$testName\bin\Release\.netcoreapp,version=v3.1\$testFile"
$x = $result -like "*.coverage"
$x = $x.Trim()
#-------------------------------------------------------------------------------------------------
Write-Output $line
Write-Output "Running: dotnet test --collect ""Code Coverage"""
Write-Output $line
& dotnet test --collect "Code Coverage"
#-------------------------------------------------------------------------------------------------
Write-Output $line
Write-Output "Running: """+$repoPath+"\$testName\bin\Release\.netcoreapp,version=v3.1\publish\CodeCoverage\amd64\CodeCoverage.exe"" analyze /output:"""+$repoPath+"\$appName\TestResults\$testName.coveragexml"" ""$x"""
Write-Output $line
& "$repoPath\$testName\bin\Release\.netcoreapp,version=v3.1\publish\CodeCoverage\amd64\CodeCoverage.exe" analyze /output:"$repoPath\$appName\TestResults\$testName.coveragexml" "$x"
#-------------------------------------------------------------------------------------------------
Write-Output $line
Write-Output "Running: ""$VSPath\vstest.console.exe"" /Logger:trx """+$repoPath+"\$testName\bin\Release\.netcoreapp,version=v3.1\$testFile"""
Write-Output $line
& "$VSPath\vstest.console.exe" /Logger:trx "$repoPath\$testName\bin\Release\.netcoreapp,version=v3.1\$testFile"
#-------------------------------------------------------------------------------------------------
Write-Output $line
Write-Output "Running: ""dotnet sonarscanner end /d:sonar.login=""$sonarLogin"""
Write-Output $line
& dotnet sonarscanner end /d:sonar.login="$sonarLogin"
Write-Output $line
#-------------------------------------------------------------------------------------------------