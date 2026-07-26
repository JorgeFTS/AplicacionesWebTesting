FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ChambaPro.Platform.API/ChambaPro.Platform.API.csproj", "ChambaPro.Platform.API/"]
RUN dotnet restore "ChambaPro.Platform.API/ChambaPro.Platform.API.csproj"
COPY . .
WORKDIR "/src/ChambaPro.Platform.API"
RUN dotnet build "./ChambaPro.Platform.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ChambaPro.Platform.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
    
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "ChambaPro.Platform.API.dll"]