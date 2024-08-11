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

## Architecture Overview
![ri (1)](https://github.com/user-attachments/assets/7a6d8467-ea97-4261-baff-1cd301c44ecb)

## Local deployment in docker

### Running Unit tests:
dotnet test --test-adapter-path:. --logger:"xunit;LogFilePath=result.xunit.xml" /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:Exclude=\"[*.Interfaces]*,[*Tests?]*,[*]*Startup*,[*]*.Extensions*,[*]*Program*\"

### Adding certificates locally
https://learn.microsoft.com/en-us/aspnet/core/security/docker-compose-https?view=aspnetcore-7.0

### Run docker locally
docker-compose up -d

## Azure DevOps CI/CD 

### Infrastructure deployment (DEV environment)

#### Service connections
Add 'Azure Resource Manager' service connection with name "azureResourceManagerConnection"

#### Library 
Create 'dev-variable-group' variable group in the library.
Configure the following variables:
- subscriptionId;
- sqlAdminLogin;
- sqlAdminPassword;

![1](https://github.com/user-attachments/assets/58eca04e-86ae-42cc-a0b3-dd0b206c7b43)

#### Infrastructure pipeline
Create 'Infrastructure deployment' pipeline using 'cicd/infrastructure-azure-pipelines.yml'.
Run  'Infrastructure deployment' pipeline.

### Build/Deploy

#### Service connections
- Add 'Docker Registry' service connection with name 'AzureContainerRegistryConnection'
- Add 'AKS' service connection with name 'AzureKubernetesConnection'
![image](https://github.com/user-attachments/assets/86a46d45-52bd-42d3-92e3-9228642e36a7)


#### Build/Deploy pipelines
Add build pipelines for each component using yml files located in "/cicd" folder.

