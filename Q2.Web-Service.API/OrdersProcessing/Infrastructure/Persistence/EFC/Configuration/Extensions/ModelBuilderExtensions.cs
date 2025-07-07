using Microsoft.EntityFrameworkCore;
using Q2.Web_Service.API.OrdersProcessing.Domain.Model.Aggregates;
using Q2.Web_Service.API.OrdersProcessing.Domain.Model.Entities;

namespace Q2.Web_Service.API.OrdersProcessing.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyOrdersProcessingConfiguration(this ModelBuilder builder)
    {
        ConfigureOrderProcessing(builder);
        ConfigureItem(builder);
    }

    private static void ConfigureOrderProcessing(ModelBuilder builder)
    {
        var entity = builder.Entity<OrderProcessing>();
        
        entity.ToTable("order_processing");
        entity.HasKey(o => o.Id);

        entity.Property(o => o.Id)
            .HasColumnName("id")
            .IsRequired()
            .ValueGeneratedOnAdd();

        entity.Property(o => o.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        entity.Property(o => o.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        // Configurar relación con Items (unidireccional)
        entity.HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey("OrderProcessingId")
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void ConfigureItem(ModelBuilder builder)
    {
        var entity = builder.Entity<Item>();
        
        entity.ToTable("order_items");
        entity.HasKey(i => i.Id);

        entity.Property(i => i.Id)
            .HasColumnName("id")
            .IsRequired()
            .ValueGeneratedOnAdd();

        entity.Property(i => i.ProductId)
            .HasColumnName("product_id")
            .IsRequired();

        entity.Property(i => i.Quantity)
            .HasColumnName("quantity")
            .IsRequired();

        entity.Property(i => i.OrderProcessingId)
            .HasColumnName("order_processing_id")
            .IsRequired();
    }
}
