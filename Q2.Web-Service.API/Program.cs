using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using Microsoft.EntityFrameworkCore;
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
using Q2.Web_Service.API.IAM.Infrastructure.Pipeline.Middleware.Extensions;
using Q2.Web_Service.API.IAM.Infrastructure.Tokens.JWT.Configuration;
using Q2.Web_Service.API.IAM.Infrastructure.Tokens.JWT.Services;
using Q2.Web_Service.API.Shared.Domain.Repositories;
using Q2.Web_Service.API.Shared.Infrastructure.ASP.Configuration;
using Q2.Web_Service.API.Shared.Infrastructure.Mediator.Cortex.Configuration;
using Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Q2.WebService.API.ProductCatalog.Domain.Repositories;
using Q2.WebService.API.ProductCatalog.Infrastructure.Persistence.EFC.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

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
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// ProductCatalog Bounded Context
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Analytics Bounded Context - TODO: Fix namespaces in repository implementations
// builder.Services.AddScoped<ICustomerAnalyticsRepository, CustomerAnalyticsRepository>();
// builder.Services.AddScoped<IManufacturerAnalyticsRepository, ManufacturerAnalyticsRepository>();

// Mediator Configuration
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LogginCommandBehavior<>));

// Add Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var tokenSettings = builder.Configuration.GetSection("TokenSettings");
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = tokenSettings["Issuer"],
        ValidAudience = tokenSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings["Secret"] ?? "defaultsecretkey"))
    };
});

// Add Authorization
builder.Services.AddAuthorization();

// Add Cortex Mediator for Event Handling
builder.Services.AddCortexMediator(
    configuration: builder.Configuration,
    handlerAssemblyMarkerTypes: new[] { typeof(Program) }, configure: options =>
    {
        options.AddOpenCommandPipelineBehavior(typeof(LogginCommandBehavior<>));
    });

var app = builder.Build();

// Verify if the database exists and create it if it doesn't
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Apply CORS Policy
app.UseCors("AllowAllPolicy");

app.UseHttpsRedirection();

// Add Authentication and Authorization to Pipeline (in the correct order)
app.UseAuthentication();
app.UseAuthorization();

// Add Authorization Middleware to Pipeline (after authentication)
app.UseRequestAuthorization();

app.MapControllers();

app.Run();