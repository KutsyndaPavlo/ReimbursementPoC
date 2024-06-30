#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["UI/ReimbursementPoC.Blazor.UI/ReimbursementPoC.Blazor.UI.csproj", "UI/ReimbursementPoC.Blazor.UI/"] 
RUN dotnet restore "UI/ReimbursementPoC.Blazor.UI/ReimbursementPoC.Blazor.UI.csproj"
COPY . .
WORKDIR "/src/UI/ReimbursementPoC.Blazor.UI"
RUN dotnet build "ReimbursementPoC.Blazor.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReimbursementPoC.Blazor.UI.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY UI/ReimbursementPoC.Blazor.UI/nginx.conf /etc/nginx/nginx.conf