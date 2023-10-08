# ReimbursementPoC

## Running Unit tests:
dotnet test --test-adapter-path:. --logger:"xunit;LogFilePath=result.xunit.xml" /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:Exclude=\"[*.Interfaces]*,[*Tests?]*,[*]*Startup*,[*]*.Extensions*,[*]*Program*\"

## Adding certificats locally
https://learn.microsoft.com/en-us/aspnet/core/security/docker-compose-https?view=aspnetcore-7.0

## Run docker locally
docker-compose up -d