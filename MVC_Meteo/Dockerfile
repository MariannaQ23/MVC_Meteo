#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MVC_Meteo/MVC_Meteo.csproj", "MVC_Meteo/"]
COPY ["ModelliMeteo/ModelliMeteo.csproj", "ModelliMeteo/"]
COPY ["ServiziMeteo/ServiziMeteo.csproj", "ServiziMeteo/"]
RUN dotnet restore "./MVC_Meteo/MVC_Meteo.csproj"
COPY . .
WORKDIR "/src/MVC_Meteo"
RUN dotnet build "./MVC_Meteo.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MVC_Meteo.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MVC_Meteo.dll"]