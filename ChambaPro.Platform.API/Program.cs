using ChambaPro.Platform.API.IAM.Application.Internal.CommandServices;
using ChambaPro.Platform.API.IAM.Application.Internal.OutboundServices;
using ChambaPro.Platform.API.IAM.Application.Internal.QueryServices;
using ChambaPro.Platform.API.IAM.Domain.Repositories;
using ChambaPro.Platform.API.IAM.Domain.Services;
using ChambaPro.Platform.API.IAM.Infrastructure.Persistence.EFC.Repositories;
using ChambaPro.Platform.API.Reservation.Application.Internal.CommandServices;
using ChambaPro.Platform.API.Reservation.Application.Internal.QueryServices;
using ChambaPro.Platform.API.Reservation.Domain.Repositories;
using ChambaPro.Platform.API.Reservation.Domain.Services;
using ChambaPro.Platform.API.Reservation.Infrastructure.Persistence.EFC.Repositories;
using ChambaPro.Platform.API.Review.Application.Internal.CommandServices;
using ChambaPro.Platform.API.Review.Application.Internal.QueryServices;
using ChambaPro.Platform.API.Review.Domain.Models.Repositories;
using ChambaPro.Platform.API.Review.Domain.Services;
using ChambaPro.Platform.API.Review.Infrastructure.Persistence.EFC;
using ChambaPro.Platform.API.Review.Interfaces.Rest.Assemblers;
using ChambaPro.Platform.API.Service.Application.CommandServices;
using ChambaPro.Platform.API.Service.Application.QueryServices;
using ChambaPro.Platform.API.Service.Domain.Repository;
using ChambaPro.Platform.API.Service.Domain.Service;
using ChambaPro.Platform.API.Service.Infrastructure.Persistence.EFC.Repositories;
using ChambaPro.Platform.API.Shared.Domain.Repositories;
using ChambaPro.Platform.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using ChambaPro.Platform.API.Shared.Infrastructure.Mediator.Cortex.Configuration;
using ChambaPro.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using ChambaPro.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Chambapro.Platform.API.User.Domain.Repositories;
using Chambapro.Platform.API.User.Infrastructure.Persistence.EF;
using ChambaPro.Platform.API.IAM.Infrastructure.Pipeline.Middleware.Components;
using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var corsOrigin = Environment.GetEnvironmentVariable("CORS_ORIGIN") ?? "http://localhost:5173";

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policy => policy.WithOrigins(corsOrigin)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

if (connectionString == null) throw new InvalidOperationException("Connection string not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    else if (builder.Environment.IsProduction())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error)
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging();
});

builder.Services.AddScoped<DbContext>(sp => sp.GetRequiredService<AppDbContext>());

builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "WASwarm.ChambaPro.API",
            Version = "v1",
            Description = "WASwarm ChambaPro Platform API",
            TermsOfService = new Uri("https://wasarwm-chambapro.com/tos"),
            Contact = new OpenApiContact
            {
                Name = "AWSawm",
                Email = "contact@awswarm.com"
            },
            License = new OpenApiLicense
            {
                Name = "Apache 2.0",
                Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
            }
        });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
});


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.Configure<TokenSettings>(
    builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();

builder.Services.AddScoped<IReserveCommandService, ReserveCommandService>();
builder.Services.AddScoped<IReserveQueryService, ReserveQueryService>();
builder.Services.AddScoped<IReserveRepository, ReserveRepository>();

builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();

builder.Services.AddScoped<IReviewCommandService, ReviewCommandService>();
builder.Services.AddScoped<IReviewQueryService, ReviewQueryService>();

builder.Services.AddScoped<IReviewRepository, ReviewRepository>();


builder.Services.AddScoped<IServiceCommandService, ServiceCommandService>();
builder.Services.AddScoped<IServiceQueryService, ServiceQueryService>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();




builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));

builder.Services.AddCortexMediator(
    configuration: builder.Configuration,
    handlerAssemblyMarkerTypes: new[] { typeof(Program) }, configure: options =>
    {
        options.AddOpenCommandPipelineBehavior(typeof(LoggingCommandBehavior<>));
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigins");

app.UseMiddleware<RequestAuthorizationMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
