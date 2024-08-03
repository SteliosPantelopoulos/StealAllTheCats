#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["StealAllTheCats/StealAllTheCats/StealAllTheCats.csproj", "StealAllTheCats/StealAllTheCats/"]
COPY ["StealAllTheCats/StealAllTheCats.Domain/StealAllTheCats.Domain.csproj", "StealAllTheCats/StealAllTheCats.Domain/"]
COPY ["StealAllTheCats/StealAllTheCats.Infrastructure/StealAllTheCats.Infrastructure.csproj", "StealAllTheCats/StealAllTheCats.Infrastructure/"]
RUN dotnet restore "./StealAllTheCats/StealAllTheCats/StealAllTheCats.csproj"
COPY . .
WORKDIR "/src/StealAllTheCats/StealAllTheCats"
RUN dotnet build "./StealAllTheCats.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./StealAllTheCats.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StealAllTheCats.dll"]