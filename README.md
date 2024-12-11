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
- [Blazor]

## Architecture Overview
![ri (1)](https://github.com/user-attachments/assets/7a6d8467-ea97-4261-baff-1cd301c44ecb)

## UI 
- Blazor: https://ribluidev.azurewebsites.net/
- Angular (In progress): https://riauidev.azurewebsites.net/

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

### Monitoring
#### Metrics
![image](https://github.com/user-attachments/assets/34e24da9-5958-4aad-b089-7b05406a839e)
#### Logs
![image](https://github.com/user-attachments/assets/af049c32-f310-472b-8b78-153ba5c3a588)
#### Insights
![image](https://github.com/user-attachments/assets/e758ef04-d932-4a43-aea3-31b072ba5bfd)
![image](https://github.com/user-attachments/assets/5e47ede8-d8fb-4fa8-aa42-546d9c1608be)
![image](https://github.com/user-attachments/assets/fa1ce773-2080-4e12-9f41-d679617745d2)
![image](https://github.com/user-attachments/assets/9258c71f-ca3d-468f-a3f5-585561707e77)
![image](https://github.com/user-attachments/assets/b0b08c6f-ae6d-4820-9765-7c33befa693b)
![image](https://github.com/user-attachments/assets/4e35180b-2424-4ba4-82b5-a0291562a03b)
![image](https://github.com/user-attachments/assets/5d89ca93-ab78-4af3-8c4d-10f41456cde0)

#### Alerts
![image](https://github.com/user-attachments/assets/00d27cda-27b4-41f0-b83b-cba6b7cf004b)
![image](https://github.com/user-attachments/assets/80e32cfd-26e6-4eb7-b180-0078a79856d4)

#### Health check
- liveness:
  
  ![image](https://github.com/user-attachments/assets/f1abc32d-74d3-4901-afe8-a09f65baf9e3)

- readiness:
  ![image](https://github.com/user-attachments/assets/949e3288-7903-4703-b288-161668885916)

# Workflow
## Programs
### Create 
![image](https://github.com/user-attachments/assets/34fb17b7-61a7-4f7a-830c-823b9ff28a2e)
### Display
![image](https://github.com/user-attachments/assets/cf8a8cf6-fb63-43cd-886e-2fea4ebf4b9f)
### Edit
![image](https://github.com/user-attachments/assets/b626dcee-dd3f-4e5a-a3e7-b1d2a900da49)
### Cancel
![image](https://github.com/user-attachments/assets/2ce6b105-2fa3-4cf5-a0be-241fba37d6ce)













