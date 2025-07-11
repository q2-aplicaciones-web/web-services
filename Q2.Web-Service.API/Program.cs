using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Q2.Web_Service.API.DesignLab.Application.Internal.CommandServices;
using Q2.Web_Service.API.DesignLab.Application.Internal.QueryServices;
using Q2.Web_Service.API.DesignLab.Domain.Repositories;
using Q2.Web_Service.API.DesignLab.Domain.Services;
using Q2.Web_Service.API.DesignLab.Infrastructure.Persistence.EFC.Repositories;
using Q2.Web_Service.API.IAM.Application.Internal.CommandServices;
using Q2.Web_Service.API.IAM.Application.Internal.OutboundServices;
using Q2.Web_Service.API.IAM.Application.Internal.QueryServices;
using Q2.Web_Service.API.IAM.Domain.Repositories;
using Q2.Web_Service.API.IAM.Domain.Services;
using Q2.Web_Service.API.IAM.Infrastructure.Hashing.BCrypt.Services;
using Q2.Web_Service.API.IAM.Infrastructure.Persistence.EFC.Repositories;
using Q2.Web_Service.API.IAM.Infrastructure.Tokens.JWT.Configuration;
using Q2.Web_Service.API.IAM.Infrastructure.Tokens.JWT.Services;
using Q2.Web_Service.API.OrdersProcessing.Application.Internal.CommandServices;
using Q2.Web_Service.API.OrdersProcessing.Application.Internal.QueryServices;
using Q2.Web_Service.API.OrdersProcessing.Domain.Repositories;
using Q2.Web_Service.API.OrdersProcessing.Domain.Services;
using Q2.Web_Service.API.OrdersProcessing.Infrastructure.Persistence.EFC.Repositories;
using Q2.Web_Service.API.Shared.Domain.Repositories;
using Q2.Web_Service.API.Shared.Infrastructure.ASP.Configuration;
using Q2.Web_Service.API.Shared.Infrastructure.Mediator.Cortex.Configuration;
using Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Q2.Web_Service.API.Shared.Infrastructure.Stripe;
using Q2.Web_Service.API.Shared.Infrastructure.Stripe.Configuration;
using Q2.Web_Service.API.Shared.Infrastructure.Stripe.Middleware;
using System.Text;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Add secrets configuration file
builder.Configuration.AddJsonFile("Properties/secrets.json", optional: true, reloadOnChange: true);

// Cargar variables de entorno desde .env
Env.Load();

// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

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
            .LogTo(Console.WriteLine, LogLevel.Error);
});

builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Q2.Web-Service.API",
            Version = "v1",
            Description = "Q2 Web Service API",
            TermsOfService = new Uri("https://q2-webservice.com/tos"),
            Contact = new OpenApiContact
            {
                Name = "Q2 Team",
                Email = "contact@q2.com"
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

// Dependency Injection

// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// DesignLab Bounded Context
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ILayerRepository, LayerRepository>();
builder.Services.AddScoped<IProjectCommandService, ProjectCommandService>();
builder.Services.AddScoped<IProjectQueryService, ProjectQueryService>();
builder.Services.AddScoped<ILayerCommandService, LayerCommandService>();
builder.Services.AddScoped<ILayerQueryService, LayerQueryService>();

// IAM Bounded Context
// Configurar TokenSettings para usar JWT_SECRET desde variable de entorno
builder.Services.Configure<TokenSettings>(options =>
{
    options.Secret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? string.Empty;
});
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<ITokenService, TokenService>();


// OrdersProcessing Bounded Context
builder.Services.AddScoped<IOrderProcessingRepository, OrderProcessingRepository>();
builder.Services.AddScoped<IOrderProcessingCommandService, OrderProcessingCommandService>();
builder.Services.AddScoped<IOrderProcessingQueryService, OrderProcessingQueryService>();

// Stripe Configuration
builder.Services.AddStripeServices(builder.Configuration);

// OrderFulfillment Bounded Context
builder.Services.AddScoped<
    Q2.Web_Service.API.OrderFulfillment.Domain.Services.IManufacturerQueryService,
    Q2.Web_Service.API.OrderFulfillment.Application.Internal.Queryservices.ManufacturerQueryServiceImpl>();

builder.Services.AddScoped<
    Q2.Web_Service.API.OrderFulfillment.Domain.Repositories.IManufacturerRepository,
    Q2.Web_Service.API.OrderFulfillment.Infrastructure.Persistence.EFC.Repositories.ManufacturerRepository>();

builder.Services.AddScoped<
    Q2.Web_Service.API.OrderFulfillment.Domain.Services.IManufacturerCommandService,
    Q2.Web_Service.API.OrderFulfillment.Application.Internal.Commandservices.ManufacturerCommandServiceImpl>();

builder.Services.AddScoped<
    Q2.Web_Service.API.OrderFulfillment.Domain.Services.IFulfillmentCommandService,
    Q2.Web_Service.API.OrderFulfillment.Application.Internal.Commandservices.FulfillmentCommandServiceImpl>();

builder.Services.AddScoped<
    Q2.Web_Service.API.OrderFulfillment.Domain.Repositories.IFulfillmentRepository,
    Q2.Web_Service.API.OrderFulfillment.Infrastructure.Persistence.EFC.Repositories.FulfillmentRepository>();

builder.Services.AddScoped<
    Q2.Web_Service.API.OrderFulfillment.Domain.Repositories.IFulfillmentItemRepository,
    Q2.Web_Service.API.OrderFulfillment.Infrastructure.Persistence.EFC.Repositories.FulfillmentItemRepository>();

builder.Services.AddScoped<
    Q2.Web_Service.API.OrderFulfillment.Domain.Services.IFulfillmentItemCommandService,
    Q2.Teelab.Api.Teelab.OrderFulfillment.Application.Internal.Commandservices.FulfillmentItemCommandServiceImpl>();

builder.Services.AddScoped<
    Q2.Web_Service.API.OrderFulfillment.Domain.Services.IFulfillmentItemQueryService,
    Q2.Web_Service.API.OrderFulfillment.Application.Internal.Queryservices.FulfillmentItemQueryServiceImpl>();

builder.Services.AddScoped<
    Q2.Web_Service.API.OrderFulfillment.Domain.Services.IFulfillmentQueryService,
    Q2.Web_Service.API.OrderFulfillment.Application.Internal.Queryservices.FulfillmentQueryServiceImpl>();

// Analytics Bounded Context
builder.Services.AddScoped<
    Q2.Web_Service.API.Analytics.Domain.Services.IManufacturerAnalyticsQueryService,
    Q2.Web_Service.API.Analytics.Application.Internal.Queryservices.ManufacturerAnalyticsRealTimeQueryServiceImpl>();

builder.Services.AddScoped<
    Q2.Web_Service.API.Analytics.Domain.Services.ICustomerAnalyticsQueryService,
    Q2.Web_Service.API.Analytics.Application.Internal.Queryservices.CustomerAnalyticsRealTimeQueryServiceImpl>();

// Context Facades
builder.Services.AddScoped<
    Q2.Web_Service.API.DesignLab.Interfaces.ACL.IProjectContextFacade,
    Q2.Web_Service.API.DesignLab.Application.ACL.ProjectContextFacadeImpl>();

builder.Services.AddScoped<
    Q2.Web_Service.API.ProductCatalog.Interfaces.ACL.ProductCatalogContextFacadeNS.IProductCatalogContextFacade,
    Q2.Web_Service.API.ProductCatalog.Application.ACL.ProductCatalogContextFacadeImpl>();

builder.Services.AddScoped<
    Q2.Web_Service.API.OrdersProcessing.Interfaces.REST.ACL.IOrderProcessingContextFacade,
    Q2.Web_Service.API.OrdersProcessing.Application.ACL.OrderProcessingContextFacadeImpl>();

builder.Services.AddScoped<
    Q2.Web_Service.API.OrderFulfillment.Interfaces.ACL.IOrderFulfillmentContextFacade,
    Q2.Web_Service.API.OrderFulfillment.Application.ACL.OrderFulfillmentContextFacadeImpl>();

// Temporary registration until interfaces are properly implemented
builder.Services.AddScoped<Q2.Web_Service.API.Analytics.Application.Internal.Queryservices.ManufacturerAnalyticsRealTimeQueryServiceImpl>();
builder.Services.AddScoped<Q2.Web_Service.API.Analytics.Application.Internal.Queryservices.CustomerAnalyticsRealTimeQueryServiceImpl>();

// Mediator Configuration
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LogginCommandBehavior<>));
// ProductCatalog Bounded Context
builder.Services.AddScoped<Q2.Web_Service.API.ProductCatalog.Infrastructure.Persistence.EFC.repositories.IProductLikeRepository, Q2.Web_Service.API.ProductCatalog.Infrastructure.Persistence.EFC.repositories.ProductLikeRepository>();
builder.Services.AddScoped<Q2.Web_Service.API.ProductCatalog.Domain.Services.IProductLikeService, Q2.Web_Service.API.ProductCatalog.Application.Internal.CommandServices.ProductLikeServiceImpl>();
builder.Services.AddScoped<Q2.Web_Service.API.ProductCatalog.Domain.Services.IProductCommandService, Q2.Web_Service.API.ProductCatalog.Application.Internal.CommandServices.ProductCommandServiceImpl>();
builder.Services.AddScoped<Q2.Web_Service.API.ProductCatalog.Infrastructure.Persistence.EFC.Repositories.IProductRepository, Q2.Web_Service.API.ProductCatalog.Infrastructure.Persistence.EFC.Repositories.ProductRepository>();
builder.Services.AddScoped<Q2.Web_Service.API.ProductCatalog.Domain.Services.IProductQueryService, Q2.Web_Service.API.ProductCatalog.Application.Internal.QueryServices.ProductQueryServiceImpl>();
builder.Services.AddScoped<Q2.Web_Service.API.ProductCatalog.Interfaces.ACL.IProjectContextFacade, Q2.Web_Service.API.ProductCatalog.Interfaces.ACL.ProjectContextFacade>();

// Add Cortex Mediator for Event Handling
builder.Services.AddCortexMediator(
    configuration: builder.Configuration,
    handlerAssemblyMarkerTypes: new[] { typeof(Program) }, configure: options =>
    {
        options.AddOpenCommandPipelineBehavior(typeof(LogginCommandBehavior<>));
    });

var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? "ZyX7p9@LkR$8m2BvR#q6WdE!fUj^HsNm";
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
    };
});

var app = builder.Build();

// Ensure database exists and create it if it doesn't
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    var logger = services.GetRequiredService<ILogger<Program>>();
    
    try
    {
        logger.LogInformation("Checking if database exists...");
        
        // Check if database can be connected to
        var canConnect = context.Database.CanConnect();
        if (!canConnect)
        {
            logger.LogInformation("Database does not exist. Creating database...");
            var created = context.Database.EnsureCreated();
            if (created)
            {
                logger.LogInformation("Database was created successfully");
            }
            else
            {
                logger.LogWarning("Database creation returned false - database may already exist");
            }
        }
        else
        {
            logger.LogInformation("Database already exists");
            
            // Check if there are any pending migrations and apply them
            var pendingMigrations = context.Database.GetPendingMigrations().ToList();
            if (pendingMigrations.Any())
            {
                logger.LogInformation($"Applying {pendingMigrations.Count} pending migrations...");
                context.Database.Migrate();
                logger.LogInformation("Migrations applied successfully");
            }
            else
            {
                logger.LogInformation("No pending migrations");
            }
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while ensuring database exists");
        throw;
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Apply CORS Policy
app.UseCors("AllowAllPolicy");

// Initialize Stripe
app.UseStripeInitialization();

app.UseHttpsRedirection();

app.UseMiddleware<JwtMiddleware>();

app.UseAuthentication(); // <-- Agregado
app.UseAuthorization();

app.MapControllers();

app.Run();