using Cortex.Mediator.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Q2.Web_Service.API.DesignLab.Application.Internal.CommandServices;
using Q2.Web_Service.API.DesignLab.Application.Internal.QueryServices;
using Q2.Web_Service.API.DesignLab.Domain.Repositories;
using Q2.Web_Service.API.DesignLab.Domain.Services;
using Q2.Web_Service.API.DesignLab.Infrastructure.Persistence.EFC.Repositories;
using Q2.Web_Service.API.Shared.Domain.Repositories;
using Q2.Web_Service.API.Shared.Infrastructure.Mediator.Cortex.Configuration;
using Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Q2.WebService.API.ProductCatalog.Domain.Repositories;
using Q2.WebService.API.ProductCatalog.Infrastructure.Persistence.EFC.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add Cors

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

if (connectionString == null)
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.UseNpgsql(connectionString).LogTo(Console.WriteLine, LogLevel.Debug).EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }
    else if (builder.Environment.IsProduction())
    {
        options.UseNpgsql(connectionString).LogTo(Console.WriteLine, LogLevel.Error);
    }
});


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen(options => {options.EnableAnnotations();});

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ILayerRepository, LayerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProjectQueryService, ProjectQueryService>();
builder.Services.AddScoped<IProjectCommandService, ProjectCommandService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services
    .AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Mediator configuration

builder.Services.AddCortexMediator(
    configuration: builder.Configuration,
    handlerAssemblyMarkerTypes: new[] { typeof(Program) }, configure: options =>
    {
        options.AddOpenCommandPipelineBehavior(typeof(LogginCommandBehavior<>));
    }
);


var app = builder.Build();

// Verify if the database exists
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
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();