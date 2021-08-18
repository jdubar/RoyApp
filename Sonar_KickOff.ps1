$line = "%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%"
Set-Location -Path "D:\Source\Repos\RoyApp\RoyApp"
Write-Output $line
Write-Output "Running: dotnet sonarscanner begin /k:""RoyApp"" /d:sonar.host.url=""http://192.168.36.52:9999"" /d:sonar.login=""4924be8f51f3e738c97db2c4f1531db7e938f28b"" /d:sonar.cs.vscoveragexml.reportsPaths=**/*.coveragexml"
Write-Output $line
& dotnet sonarscanner begin /k:"RoyApp" /d:sonar.host.url="http://192.168.36.52:9999" /d:sonar.login="4924be8f51f3e738c97db2c4f1531db7e938f28b" /d:sonar.cs.vscoveragexml.reportsPaths=**/*.coveragexml
Write-Output $line
Write-Output "Running: dotnet build"
Write-Output $line
dotnet build
Write-Output $line
Write-Output "Running: ""C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe"" /EnableCodeCoverage ""D:\Source\Repos\RoyApp\RoyAppTests\bin\Release\.netcoreapp,version=v3.1\RoyAppTests.dll"""
Write-Output $line
$result = & "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" /EnableCodeCoverage "D:\Source\Repos\RoyApp\RoyAppTests\bin\Release\.netcoreapp,version=v3.1\RoyAppTests.dll"
$x = $result -like "*.coverage"
$x = $x.Trim()
Write-Output $line
Write-Output "Running: dotnet test --collect ""Code Coverage"""
Write-Output $line
& dotnet test --collect "Code Coverage"
Write-Output $line
Write-Output "Running: ""D:\Source\Repos\RoyApp\RoyAppTests\bin\Release\.netcoreapp,version=v3.1\publish\CodeCoverage\amd64\CodeCoverage.exe"" analyze /output:""D:\Source\Repos\RoyApp\RoyApp\TestResults\RoyAppTests.coveragexml"" ""$x"""
Write-Output $line
& "D:\Source\Repos\RoyApp\RoyAppTests\bin\Release\.netcoreapp,version=v3.1\publish\CodeCoverage\amd64\CodeCoverage.exe" analyze /output:"D:\Source\Repos\RoyApp\RoyApp\TestResults\RoyAppTests.coveragexml" "$x"
Write-Output $line
Write-Output "Running: ""C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe"" /Logger:trx ""D:\Source\Repos\RoyApp\RoyAppTests\bin\Release\.netcoreapp,version=v3.1\RoyAppTests.dll"""
Write-Output $line
& "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" /Logger:trx "D:\Source\Repos\RoyApp\RoyAppTests\bin\Release\.netcoreapp,version=v3.1\RoyAppTests.dll"
Write-Output $line
Write-Output "Running: ""dotnet sonarscanner end /d:sonar.login=""4924be8f51f3e738c97db2c4f1531db7e938f28b"""
Write-Output $line
& dotnet sonarscanner end /d:sonar.login="4924be8f51f3e738c97db2c4f1531db7e938f28b"
Write-Output $line