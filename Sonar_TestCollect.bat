"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" /EnableCodeCoverage "D:\Source\Repos\RoyApp\RoyAppTests\bin\Release\.netcoreapp,version=v3.1\RoyApp.dll"

dotnet test --collect "Code Coverage"

"D:\Source\Repos\RoyApp\RoyAppTests\bin\Release\.netcoreapp,version=v3.1\publish\CodeCoverage\amd64\CodeCoverage.exe" analyze /output:"D:\Source\Repos\RoyApp\RoyAppTests\TestResults\b6d769d8-e907-4d91-867d-57aaf8fa1c08\salad_OFFICE_2021-08-17.13_47_02.coverage.xml" "D:\Source\Repos\RoyApp\RoyAppTests\TestResults\b6d769d8-e907-4d91-867d-57aaf8fa1c08\salad_OFFICE_2021-08-17.13_47_02.coverage"