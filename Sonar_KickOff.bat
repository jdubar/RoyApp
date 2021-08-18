D:
cd "D:\Source\Repos\RoyApp\RoyApp"

dotnet sonarscanner begin /k:"RoyApp" /d:sonar.host.url="http://192.168.36.52:9999"  /d:sonar.login="4924be8f51f3e738c97db2c4f1531db7e938f28b" /d:sonar.cs.vscoveragexml.reportsPaths=**/*.coveragexml

rmdir /s /q "D:\Source\Repos\RoyApp\RoyApp\TestResults"
mkdir "D:\Source\Repos\RoyApp\RoyApp\TestResults"

dotnet build

"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" /EnableCodeCoverage "D:\Source\Repos\RoyApp\RoyAppTests\bin\Release\.netcoreapp,version=v3.1\RoyAppTests.dll"

dotnet test --collect "Code Coverage"

"D:\Source\Repos\RoyApp\RoyAppTests\bin\Release\.netcoreapp,version=v3.1\publish\CodeCoverage\amd64\CodeCoverage.exe" analyze /output:"D:\Source\Repos\RoyApp\RoyApp\TestResults\RoyAppTests.coveragexml" "D:\Source\Repos\RoyApp\RoyApp\TestResults\*\*.coverage"

"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" /Logger:trx "D:\Source\Repos\RoyApp\RoyAppTests\bin\Release\.netcoreapp,version=v3.1\RoyAppTests.dll"

dotnet sonarscanner end /d:sonar.login="4924be8f51f3e738c97db2c4f1531db7e938f28b"