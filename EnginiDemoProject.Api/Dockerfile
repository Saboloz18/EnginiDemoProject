#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EnginiDemoProject.Api/EnginiDemoProject.Api.csproj", "EnginiDemoProject.Api/"]
COPY ["EnginiDemoProject.Application/EnginiDemoProject.Application.csproj", "EnginiDemoProject.Application/"]
COPY ["EnginiDemoProject.Domain/EnginiDemoProject.Domain.csproj", "EnginiDemoProject.Domain/"]
COPY ["EnginiDemoProject.Presistence/EnginiDemoProject.Presistence.csproj", "EnginiDemoProject.Presistence/"]
RUN dotnet restore "./EnginiDemoProject.Api/EnginiDemoProject.Api.csproj"
COPY . .
WORKDIR "/src/EnginiDemoProject.Api"
RUN dotnet build "./EnginiDemoProject.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EnginiDemoProject.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EnginiDemoProject.Api.dll"]