#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Sevices/Administration/ReimbursementPoC.Administration.API/ReimbursementPoC.Administration.API.csproj", "Sevices/Administration/ReimbursementPoC.Administration.API/"]
COPY ["Sevices/Administration/ReimbursementPoC.Administration.Application/ReimbursementPoC.Administration.Application.csproj", "Sevices/Administration/ReimbursementPoC.Administration.Application/"]
COPY ["Infrastructure/EventBusServiceBus/ReimbursementPoC.Infrustructure.EventBusServiceBus.csproj", "Infrastructure/EventBusServiceBus/"]
COPY ["Infrastructure/EventBus/ReimbursementPoC.Infrustructure.EventBus.csproj", "Infrastructure/EventBus/"]
COPY ["Sevices/Administration/ReimbursementPoC.Administration.Domain/ReimbursementPoC.Administration.Domain.csproj", "Sevices/Administration/ReimbursementPoC.Administration.Domain/"]
COPY ["Sevices/Administration/ReimbursementPoC.Administration.IntergrationEvents/ReimbursementPoC.Administration.IntergrationEvents.csproj", "Sevices/Administration/ReimbursementPoC.Administration.IntergrationEvents/"]
COPY ["Sevices/Administration/ReimbursementPoC.Administration.Infrastructure/ReimbursementPoC.Administration.Infrastructure.csproj", "Sevices/Administration/ReimbursementPoC.Administration.Infrastructure/"]
RUN dotnet restore "Sevices/Administration/ReimbursementPoC.Administration.API/ReimbursementPoC.Administration.API.csproj"
COPY . .
WORKDIR "/src/Sevices/Administration/ReimbursementPoC.Administration.API"
RUN dotnet build "ReimbursementPoC.Administration.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReimbursementPoC.Administration.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReimbursementPoC.Administration.API.dll"]