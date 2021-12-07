Write-Output "--------------------------------------------------------------------------------------"
Set-Location -Path "D:\Source\Repos\RoyApp"
Write-Output "--------------------------------------------------------------------------------------"
#dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura
#dotnet test RoyAppTests/RoyAppTests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
<#
$result = & dotnet test --configuration Debug --no-restore --no-build --collect "XPlat Code Coverage"
$x = $result -match "coverage.cobertura.xml"
$x = $x.Trim()
reportgenerator -reports:"$x" -targetdir:"coveragereport" -reporttypes:Html
#>
dotnet test RoyApp.sln --collect:"XPlat Code Coverage"
reportgenerator -reports:RoyAppTests/**/coverage.cobertura.xml -targetdir:RoyAppTests/CoverageReport -reporttypes:Cobertura
Write-Output "--------------------------------------------------------------------------------------"
dotnet build-server shutdown
Write-Output "--------------------------------------------------------------------------------------"
#dotnet sonarscanner begin /k:"RoyApp" /d:sonar.host.url=http://192.168.36.52:9999 /d:sonar.login="4924be8f51f3e738c97db2c4f1531db7e938f28b" /d:sonar.cobertura.reportPath="$x" /d:sonar.coverage.exclusions="**Tests*.cs"
#dotnet sonarscanner begin /k:"RoyApp" /d:sonar.host.url=http://192.168.36.52:9999 /d:sonar.login="4924be8f51f3e738c97db2c4f1531db7e938f28b" /d:sonar.cobertura.reportPath="$x"
dotnet sonarscanner begin /k:"RoyApp" /d:sonar.host.url=http://192.168.36.52:9999 /d:sonar.login="4924be8f51f3e738c97db2c4f1531db7e938f28b" /d:sonar.cobertura.reportPath="./RoyAppTests/CoverageReport/Cobertura.xml"
Write-Output "--------------------------------------------------------------------------------------"
dotnet build
Write-Output "--------------------------------------------------------------------------------------"
dotnet sonarscanner end /d:sonar.login="4924be8f51f3e738c97db2c4f1531db7e938f28b"
Write-Output "--------------------------------------------------------------------------------------"