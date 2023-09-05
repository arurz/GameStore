#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /GameStoreApi
COPY ["GameStoreApi.Hosting/GameStoreApi.Hosting.csproj", "GameStoreApi.Hosting/"]
COPY ["GameStoreApi.Application/GameStoreApi.Application.csproj", "GameStoreApi.Application/"]
COPY ["GameStoreApi.Data/GameStoreApi.Data.csproj", "GameStoreApi.Data/"]
COPY ["GameStoreApi.Persistence/GameStoreApi.Persistence.csproj", "GameStoreApi.Persistence/"]
COPY ["GameStoreApi.Infrastructure/GameStoreApi.Infrastructure.csproj", "GameStoreApi.Infrastructure/"]
RUN dotnet restore "GameStoreApi.Hosting/GameStoreApi.Hosting.csproj"
COPY . .
WORKDIR "/GameStoreApi/GameStoreApi.Hosting"
RUN dotnet build "GameStoreApi.Hosting.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameStoreApi.Hosting.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameStoreApi.Hosting.dll"]