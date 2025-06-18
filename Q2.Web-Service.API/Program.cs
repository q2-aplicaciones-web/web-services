using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<Q2.Web_Service.API.analytics.infrastructure.persistence.jpa.repositories.CustomerAnalyticsRepository>();
builder.Services.AddScoped<Q2.WebService.API.Analytics.Application.Internal.QueryServices.CustomerAnalyticsQueryServiceImpl>();
builder.Services.AddScoped<Q2.Web_Service.API.analytics.infrastructure.persistence.jpa.repositories.ManufacturerAnalyticsRepository>();
builder.Services.AddScoped<Q2.WebService.API.Analytics.Application.Internal.QueryServices.ManufacturerAnalyticsQueryServiceImpl>();
builder.Services.AddDbContext<Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Configuration.AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();