#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Sevices/Identity/ReimbursementPoC.Identity.API/ReimbursementPoC.Identity.API.csproj", "Sevices/Identity/ReimbursementPoC.Identity.API/"]
RUN dotnet restore "Sevices/Identity/ReimbursementPoC.Identity.API/ReimbursementPoC.Identity.API.csproj"
COPY . .
WORKDIR "/src/Sevices/Identity/ReimbursementPoC.Identity.API"
RUN dotnet build "ReimbursementPoC.Identity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReimbursementPoC.Identity.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReimbursementPoC.Identity.API.dll"]