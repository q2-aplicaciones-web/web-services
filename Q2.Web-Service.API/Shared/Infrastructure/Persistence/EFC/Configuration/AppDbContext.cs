using Microsoft.EntityFrameworkCore;
using Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Q2.Web_Service.API.DesignLab.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Q2.Web_Service.API.IAM.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Q2.Web_Service.API.OrdersProcessing.Infrastructure.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Q2.Web_Service.API.Analytics.Infrastructure.Persistence.EFC.Configuration;
using Q2.Web_Service.API.Analytics.Domain.Model.Entities;
using Q2.Web_Service.API.Analytics.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Configuration
{
    /// <summary>
    ///     Application database context
    /// </summary>
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Q2.Web_Service.API.ProductCatalog.Domain.Model.Aggregates.ProductLike> ProductLikes { get; set; }
        // OrderFulfillment
        public DbSet<Q2.Web_Service.API.OrderFulfillment.Domain.Model.Aggregates.Fulfillment> Fulfillments { get; set; }
        public DbSet<Q2.Web_Service.API.OrderFulfillment.Domain.Model.Aggregates.Manufacturer> Manufacturers { get; set; }
        public DbSet<Q2.Web_Service.API.OrderFulfillment.Domain.Model.Entities.FulfillmentItem> FulfillmentItems { get; set; }
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
                    v => v != null ? v.Value : null,
                    v => v != null ? new Q2.Web_Service.API.ProductCatalog.Domain.Model.ValueObjects.ProjectId(v) : null
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

            // Configuración para ProductLike
            builder.Entity<Q2.Web_Service.API.ProductCatalog.Domain.Model.Aggregates.ProductLike>(entity =>
            {
                entity.ToTable("product_likes");
                entity.HasKey(e => new { e.ProductId, e.UserId });
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            // --- OrderFulfillment ---
            builder.Entity<Q2.Web_Service.API.OrderFulfillment.Domain.Model.Aggregates.Fulfillment>(entity =>
            {
                entity.ToTable("fulfillments");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.OrderId)
                    .HasConversion(
                        v => v.Value,
                        v => new Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects.OrderId(v)
                    )
                    .HasColumnName("order_id");
                entity.Property(e => e.Status).HasColumnName("status");
                entity.Property(e => e.ReceivedDate).HasColumnName("received_date");
                entity.Property(e => e.ShippedDate).HasColumnName("shipped_date");
                entity.Property(e => e.ManufacturerId).HasColumnName("manufacturer_id");
                entity.HasOne(e => e.Manufacturer)
                    .WithMany(m => m.Fulfillments)
                    .HasForeignKey(e => e.ManufacturerId);
                entity.Ignore(e => e.Items); // handled by FulfillmentItem
            });

            builder.Entity<Q2.Web_Service.API.OrderFulfillment.Domain.Model.Aggregates.Manufacturer>(entity =>
            {
                entity.ToTable("manufacturers");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.UserId)
                    .HasConversion(
                        v => v.Value,
                        v => new Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects.UserId(v)
                    )
                    .HasColumnName("user_id");
                entity.Property(e => e.Name).HasColumnName("name");
                // Flatten Address value object into Manufacturer entity
                entity.Property<string>("Address_Street").HasColumnName("address_street");
                entity.Property<string>("Address_City").HasColumnName("address_city");
                entity.Property<string>("Address_Country").HasColumnName("address_country");
                entity.Property<string>("Address_State").HasColumnName("address_state");
                entity.Property<string>("Address_Zip").HasColumnName("address_zip");
                entity.Ignore(e => e.Fulfillments); // handled by Fulfillment
            });

            builder.Entity<Q2.Web_Service.API.OrderFulfillment.Domain.Model.Entities.FulfillmentItem>(entity =>
            {
                entity.ToTable("fulfillment_items");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasConversion(
                        v => v.Value,
                        v => new Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects.FulfillmentItemId(v)
                    )
                    .HasColumnName("id");
                entity.Property(e => e.ProductId)
                    .HasConversion(
                        v => v.Value,
                        v => new Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects.ProductId(v)
                    )
                    .HasColumnName("product_id");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.Status).HasColumnName("status");
                entity.Property(e => e.FulfillmentId).HasColumnName("fulfillment_id");
            });

            // Convención opcional de nombres en snake_case si has definido la extensión
            builder.UseSnakeCaseNamingConvention(); // Asegúrate de tener implementada esta extensión
        }

        public DbSet<Q2.Web_Service.API.ProductCatalog.Domain.Model.Aggregates.Product> Products { get; set; }
        // ...existing code...
    }
}
