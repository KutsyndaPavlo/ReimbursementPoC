#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Sevices/Vendor/ReimbursementPoC.Vendor.API/ReimbursementPoC.Vendor.API.csproj", "Sevices/Vendor/ReimbursementPoC.Vendor.API/"]
COPY ["Sevices/Vendor/ReimbursementPoC.Vendor.Application/ReimbursementPoC.Vendor.Application.csproj", "Sevices/Vendor/ReimbursementPoC.Vendor.Application/"]
COPY ["Infrastructure/EventBusServiceBus/ReimbursementPoC.Infrustructure.EventBusServiceBus.csproj", "Infrastructure/EventBusServiceBus/"]
COPY ["Infrastructure/EventBus/ReimbursementPoC.Infrustructure.EventBus.csproj", "Infrastructure/EventBus/"]
COPY ["Sevices/Administration/ReimbursementPoC.Administration.IntergrationEvents/ReimbursementPoC.Administration.IntergrationEvents.csproj", "Sevices/Administration/ReimbursementPoC.Administration.IntergrationEvents/"]
COPY ["Sevices/Vendor/ReimbursementPoC.VendorDomain/ReimbursementPoC.Vendor.Domain.csproj", "Sevices/Vendor/ReimbursementPoC.VendorDomain/"]
COPY ["Sevices/Vendor/ReimbursementPoC.Vendor.Infrastructure/ReimbursementPoC.Vendor.Infrastructure.csproj", "Sevices/Vendor/ReimbursementPoC.Vendor.Infrastructure/"]
RUN dotnet restore "Sevices/Vendor/ReimbursementPoC.Vendor.API/ReimbursementPoC.Vendor.API.csproj"
COPY . .
WORKDIR "/src/Sevices/Vendor/ReimbursementPoC.Vendor.API"
RUN dotnet build "ReimbursementPoC.Vendor.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReimbursementPoC.Vendor.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReimbursementPoC.Vendor.API.dll"]