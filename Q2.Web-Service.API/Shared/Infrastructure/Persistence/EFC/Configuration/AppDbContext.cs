using Microsoft.EntityFrameworkCore;
using Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Q2.Web_Service.API.DesignLab.Infrastructure.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Quri.Teelab.Api.Teelab.Analytics.Domain.Model.Entities;
using Quri.Teelab.Api.Teelab.Analytics.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Configuration
{
    /// <summary>
    ///     Application database context
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Agrega automáticamente las fechas CreatedAt y UpdatedAt (si estás usando un interceptor)
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddCreatedUpdatedInterceptor(); // Requiere el paquete: EntityFrameworkCore.CreatedUpdatedDate
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración personalizada para DesignLab (puedes agregar más configuraciones por módulo)
            modelBuilder.ApplyDesignLabConfiguration();

            // Configuración para CustomerAnalytics
            modelBuilder.Entity<CustomerAnalytics>()
                .Property(e => e.Id)
                .HasConversion(
                    v => Guid.Parse(v.Value),
                    v => new AnalyticsId(v.ToString())
                );

            // Convención opcional de nombres en snake_case si has definido la extensión
            modelBuilder.UseSnakeCaseNamingConvention(); // Asegúrate de tener implementada esta extensión
        }

        // DbSet para la entidad CustomerAnalytics
        public DbSet<CustomerAnalytics> CustomerAnalytics { get; set; }
    }
}
