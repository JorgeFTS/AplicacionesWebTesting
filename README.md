# ChambaPro — Backend (Platform API)

API REST de **ChambaPro**, una plataforma que conecta clientes con técnicos: búsqueda, contratación, agenda de reservas y reseñas.

> **Estado:** en desarrollo. Estoy agregando la capa DevOps/QA: Docker, CI/CD con GitHub Actions, más tests y despliegue en Azure con Terraform.
> TODO: confirmar el nombre del equipo e integrantes del proyecto original y acreditarlos aquí.

## Stack

- .NET 9 / C# · ASP.NET Core Web API
- Entity Framework Core + MySQL
- Autenticación con JWT · contraseñas con BCrypt
- Arquitectura DDD / CQRS (Cortex.Mediator), por módulos: `IAM`, `Reservation`, `Review`, `Service`, `User`
- Swagger/OpenAPI · mensajes en español e inglés
- Tests: xUnit + Moq

## Cómo correrlo con Docker (recomendado)

Requisitos: Docker y Docker Compose.

```bash
cp .env.example .env        # y completa MYSQL_PASSWORD, MYSQL_ROOT_PASSWORD y TOKEN_SECRET
docker compose up --build
```

- API: http://localhost:8080
- Swagger: http://localhost:8080/swagger
- Health check: http://localhost:8080/health (responde 200 si la API llega a la base de datos)

Las tablas se crean automáticamente al arrancar. Para borrar todo, incluida la base de datos: `docker compose down -v`.

## Cómo correrlo sin Docker

1. Ten MySQL corriendo en local.
2. Copia `ChambaPro.Platform.API/appsettings.Development.example.json` a `appsettings.Development.json` y completa tus valores (ese archivo está en `.gitignore`).
3. `dotnet run --project ChambaPro.Platform.API` (queda en http://localhost:5214).

## Configuración

| Variable | Para qué sirve |
|---|---|
| `ConnectionStrings__DefaultConnection` | Conexión a MySQL |
| `TokenSettings__Secret` | Clave para firmar los JWT (mínimo 32 caracteres) |
| `CORS_ORIGIN` | Origen del frontend permitido (por defecto `http://localhost:4173`) |
| `EnableSwagger` | `true` para exponer Swagger fuera de Development |

Nunca subas secretos al repositorio: usa `.env` (ignorado por git) o variables de entorno.

## Tests

```bash
dotnet test
```

## Roadmap DevOps / QA

- [x] Secretos fuera del repositorio
- [ ] Endpoint `/health` (escrito, pendiente de compilar y probar)
- [ ] Dockerfile multi-stage con usuario no-root (escrito, pendiente de probar)
- [ ] `docker-compose` con MySQL + API (escrito, pendiente de probar)
- [ ] CI con GitHub Actions (build, tests, cobertura)
- [ ] Publicar imagen en GitHub Container Registry
- [ ] Más tests (IAM, Review, servicios de aplicación)
- [ ] Despliegue en Azure (Container Apps + MySQL Flexible Server)
- [ ] Infraestructura como código con Terraform
