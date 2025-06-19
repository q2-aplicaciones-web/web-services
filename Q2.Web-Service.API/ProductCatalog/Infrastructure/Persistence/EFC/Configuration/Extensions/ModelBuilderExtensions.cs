using Microsoft.EntityFrameworkCore;
using Q2.WebService.API.ProductCatalog.Domain.Model.Aggregates;
using Q2.WebService.API.ProductCatalog.Domain.Model.Entities;

namespace Q2.Web_Service.API.ProductCatalog.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
/// Database context for Product Catalog
/// </summary>
public static class ModelBuilderExtensions 
{
    public static void ApplyProductCatalogConfiguration(this ModelBuilder builder)
    {
        ConfigureProduct(builder);
    }

    private static void ConfigureProduct(ModelBuilder builder)
    {
        var entity = builder.Entity<Product>();
        entity.ToTable("Products");
        entity.HasKey(e => e.Id);
        entity.OwnsOne(e => e.ProjectId, projectId =>
        {
            projectId.Property(p => p.Value)
                .HasColumnName("project_id")
                .IsRequired();
        });
        entity.OwnsOne(e => e.ManufacturerId, manufacturerId =>
        {
            manufacturerId.Property(p => p.Value)
                .HasColumnName("manufacturer_id")
                .IsRequired();
        });
        entity.OwnsOne(e => e.Price, price =>
        {
            price.Property(p => p.Amount)
                .HasColumnName("price_amount")
                .IsRequired();
            price.Property(p => p.Currency)
                .HasColumnName("price_currency")
                .IsRequired();
        });

        // Configure collection of primitive type
        entity.HasMany(p => p.Comments)
            .WithOne()
            .HasForeignKey("ProductId")
            .OnDelete(DeleteBehavior.Cascade);

        // Configure collections of primitive types (Tags and Gallery)
        entity.Property<List<string>>("_tags")
            .HasColumnName("tags")
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

        entity.Property<List<string>>("_gallery")
            .HasColumnName("gallery")
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
    }
}