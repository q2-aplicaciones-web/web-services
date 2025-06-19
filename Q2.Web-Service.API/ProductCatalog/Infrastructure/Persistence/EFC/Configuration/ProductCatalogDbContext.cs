using Microsoft.EntityFrameworkCore;
using Q2.WebService.API.ProductCatalog.Domain.Model.Aggregates;
using Q2.WebService.API.ProductCatalog.Domain.Model.Entities;

namespace Q2.WebService.API.ProductCatalog.Infrastructure.Persistence.EFC.Configuration
{
    /// <summary>
    /// Database context for Product Catalog
    /// </summary>
    public class ProductCatalogDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        
        public ProductCatalogDbContext(DbContextOptions<ProductCatalogDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Apply configuration for Product
            modelBuilder.Entity<Product>(entity =>
            {
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
                        .HasMaxLength(3)
                        .IsRequired();
                });

                entity.Property(e => e.Likes)
                    .HasColumnName("likes")
                    .IsRequired();

                entity.OwnsOne(e => e.Rating, rating =>
                {
                    rating.Property(p => p.Value)
                        .HasColumnName("rating")
                        .IsRequired();
                });
                
                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .IsRequired();

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at");

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
            });

            // Apply configuration for Comment
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comments");

                entity.HasKey(e => e.Id);

                entity.OwnsOne(e => e.UserId, userId =>
                {
                    userId.Property(p => p.Value)
                        .HasColumnName("user_id")
                        .IsRequired();
                });

                entity.Property(e => e.Text)
                    .HasColumnName("text")
                    .HasMaxLength(1000)
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .IsRequired();

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at");
            });
        }
    }
}
