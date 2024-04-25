# ReimbursementPoC

## Technologies:

- [.NET Core 6]
- [MS SQL Server]
- [Entity Framework Core 6]
- [IdentityServer4]
- [Swashbuckle]
- [Dapper]
- [MassTransit]
- [RabbitMQ]
- [Newtonsoft.Json]
- [FluentValidation]
- [MediatR]
- [Polly]
- [NUnit]
- [Docker]
- [Specflow]


## Running Unit tests:
dotnet test --test-adapter-path:. --logger:"xunit;LogFilePath=result.xunit.xml" /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:Exclude=\"[*.Interfaces]*,[*Tests?]*,[*]*Startup*,[*]*.Extensions*,[*]*Program*\"

## Adding certificats locally
https://learn.microsoft.com/en-us/aspnet/core/security/docker-compose-https?view=aspnetcore-7.0

## Run docker locally
docker-compose up -d