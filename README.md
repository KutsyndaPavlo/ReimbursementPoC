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
Add 'Azure Resource Manager' service connection
![1](https://github.com/KutsyndaPavlo/ReimbursementPoC/assets/2121990/fce3ef90-4038-4717-a7f4-ab6f75bb0343)

#### Library 
Create 'dev-variable-group' variable group in the library.
Configure the following variables:
- arm_connection;
- subscriptionId;
- sqlAdminLogin;
- sqlAdminPassword;
![Capture](https://github.com/KutsyndaPavlo/ReimbursementPoC/assets/2121990/fae5c3c0-f1bf-4fb4-81b7-cd54d9980e5e)

#### Pipeline
Create 'Infrastructure deployment' pipeline using 'deploy/pipelines/infrastructure-deploy-arm.yaml'.
Run  'Infrastructure deployment' pipeline.

### Build 

#### Service connections
Add 'Docker Registry' service connection with name 'acr_connection'
![1](https://github.com/KutsyndaPavlo/ReimbursementPoC/assets/2121990/8335f5aa-b28f-4877-9b90-5e1e051cda2f)

#### Library 
Configure the following variables for 'dev-variable-group':
- acr_connection;
![2](https://github.com/KutsyndaPavlo/ReimbursementPoC/assets/2121990/d1015b31-b388-487b-ae93-53aceb9316aa)

#### Build pipelines
Add build pipelines for each components using yml files located in "/build" folder.

### Deploy

#### Angular UI deployment
Create "Angular UI" deployment pipeline
![3](https://github.com/KutsyndaPavlo/ReimbursementPoC/assets/2121990/b43dfbd6-867f-4a3e-b541-2e5be7a56125)



