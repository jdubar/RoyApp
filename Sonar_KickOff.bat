D:
cd "D:\Source\Repos\RoyApp\RoyApp"

dotnet sonarscanner begin /k:"RoyApp" /d:sonar.host.url="http://192.168.36.52:9999"  /d:sonar.login="4924be8f51f3e738c97db2c4f1531db7e938f28b"

dotnet build

dotnet sonarscanner end /d:sonar.login="4924be8f51f3e738c97db2c4f1531db7e938f28b"