# ---- Imagen base de ejecución ----
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# ---- Build ----
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
# Primero solo el .csproj: Docker cachea el restore si no cambian las dependencias
COPY ["ChambaPro.Platform.API/ChambaPro.Platform.API.csproj", "ChambaPro.Platform.API/"]
RUN dotnet restore "ChambaPro.Platform.API/ChambaPro.Platform.API.csproj"
COPY . .
# El .csproj referencia ..\.dockerignore; el .dockerignore lo excluye del contexto,
# así que creamos uno vacío para que el publish no falle.
RUN touch /src/.dockerignore
WORKDIR /src/ChambaPro.Platform.API
RUN dotnet publish "ChambaPro.Platform.API.csproj" -c Release -o /app/publish /p:UseAppHost=false --no-restore

# ---- Imagen final ----
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
# Corre como usuario no-root (APP_UID ya viene definido en las imágenes oficiales de .NET)
USER $APP_UID
ENTRYPOINT ["dotnet", "ChambaPro.Platform.API.dll"]
