sonarqube global analysis token: sqa_da13d32640b966827a776f231d86767a25458b29

# execute in test project to generate sample test coverage (dotnet-coverage-tool-coverage for demo purposes)
dotnet-coverage collect "dotnet test" -f xml -o "coverage.xml"
dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -reports:".coverage.xml" -targetdir:"coverage.report" -reporttypes:"html"

#execute in project root
cd .\src
dotnet sonarscanner begin /k:"SonarqubeSampleProject" /v:"1.0.0" /d:sonar.host.url="http://host.docker.internal:9000" /d:sonar.token="sqa_da13d32640b966827a776f231d86767a25458b29" /d:sonar.branchName="main" /d:sonar.cs.opencover.reportsPaths=".\..\tests\MyTestResults\coverage.opencover.xml" /d:sonar.cs.vstest.reportsPath=".\..\tests\MyTestResults\TestResults.trx"

dotnet build -p:Version=2.0.0

cd .\..\tests\
dotnet test --no-build /p:Version=2.0.0 /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=MyTestResults/ --logger "trx;logfilename=TestResults.trx" --results-directory="MyTestResults"

cd .\..\src\
dotnet sonarscanner end /d:sonar.token="sqa_da13d32640b966827a776f231d86767a25458b29"