# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000 5001


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GamingOne.HiLo.Api/GamingOne.HiLo.Api.csproj", "GamingOne.HiLo.Api/"]
COPY ["Common/Application/Application.csproj", "Common/Application/"]
COPY ["Common/SharedKernel/SharedKernel.csproj", "Common/SharedKernel/"]
COPY ["Modules/Gaming/GamingOne.Modules.Gaming.Application/GamingOne.Modules.Gaming.Application.csproj", "Modules/Gaming/GamingOne.Modules.Gaming.Application/"]
COPY ["Modules/Gaming/GamingOne.Modules.Gaming.Domain/GamingOne.Modules.Gaming.Domain.csproj", "Modules/Gaming/GamingOne.Modules.Gaming.Domain/"]
COPY ["Modules/Gaming/GamingOne.Modules.Gaming.Infrastructure/GamingOne.Modules.Gaming.Infrastructure.csproj", "Modules/Gaming/GamingOne.Modules.Gaming.Infrastructure/"]
RUN dotnet restore "./GamingOne.HiLo.Api/GamingOne.HiLo.Api.csproj"
COPY . .
WORKDIR "/src/GamingOne.HiLo.Api"
RUN dotnet build "./GamingOne.HiLo.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GamingOne.HiLo.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GamingOne.HiLo.Api.dll"]