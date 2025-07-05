using Microsoft.EntityFrameworkCore;
using Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Q2.Web_Service.API.DesignLab.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Q2.Web_Service.API.IAM.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Q2.Web_Service.API.OrdersProcessing.Infrastructure.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Q2.Web_Service.API.Analytics.Infrastructure.Persistence.EFC.Configuration;
using Quri.Teelab.Api.Teelab.Analytics.Domain.Model.Entities;
using Quri.Teelab.Api.Teelab.Analytics.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Configuration
{
    /// <summary>
    ///     Application database context
    /// </summary>
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {

        // Agrega automáticamente las fechas CreatedAt y UpdatedAt (si estás usando un interceptor)
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.AddCreatedUpdatedInterceptor(); // Requiere el paquete: EntityFrameworkCore.CreatedUpdatedDate
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configuración personalizada para DesignLab (puedes agregar más configuraciones por módulo)
            builder.ApplyAnalyticsConfiguration();
            builder.ApplyDesignLabConfiguration();
            builder.ApplyIamConfiguration();
            builder.ApplyOrdersProcessingConfiguration();

            // Configuración para CustomerAnalytics
            builder.Entity<CustomerAnalytics>()
                .Property(e => e.Id)
                .HasConversion(
                    v => Guid.Parse(v.Value),
                    v => new AnalyticsId(v.ToString())
                );

            // Configuración para ProjectId en Product (ValueObject)
            builder.Entity<Q2.Web_Service.API.ProductCatalog.Domain.Model.Aggregates.Product>()
                .Property(p => p.ProjectId)
                .HasConversion(
                    v => v.Value,
                    v => new Q2.Web_Service.API.ProductCatalog.Domain.Model.ValueObjects.ProjectId(v)
                );

            // Configuración para Money en Product (ValueObject)
            builder.Entity<Q2.Web_Service.API.ProductCatalog.Domain.Model.Aggregates.Product>()
                .OwnsOne(p => p.Price, m =>
                {
                    m.Property(x => x.Amount).HasColumnName("Price_Amount");
                    m.Property(x => x.Currency).HasColumnName("Price_Currency");
                    // Remove any key or foreign key mapping for Money
                    m.Ignore("Id");
                    m.Ignore("ProductId");
                    m.WithOwner().HasForeignKey("Id");
                    m.HasKey("Id");
                });

            // Convención opcional de nombres en snake_case si has definido la extensión
            builder.UseSnakeCaseNamingConvention(); // Asegúrate de tener implementada esta extensión
        }

        public DbSet<Q2.Web_Service.API.ProductCatalog.Domain.Model.Aggregates.Product> Products { get; set; }

    }
}
