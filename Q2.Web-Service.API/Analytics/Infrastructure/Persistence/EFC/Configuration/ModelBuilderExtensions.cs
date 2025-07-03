using Microsoft.EntityFrameworkCore;
using Q2.Web_Service.Api.Teelab.Analytics.Domain.Model.Aggregates;
using Q2.Web_Service.Api.Teelab.Analytics.Domain.Model.Entities;
using Q2.Web_Service.Api.Teelab.Analytics.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.Analytics.Infrastructure.Persistence.EFC.Configuration;

public static class ModelBuilderExtensions
{

    public static void ApplyAnalyticsConfiguration(this ModelBuilder builder)
    {
        ConfigureCustomerAnalytics(builder);
        ConfigureManufacturerAnalytics(builder);
    }
    
    private static void ConfigureCustomerAnalytics(ModelBuilder builder)
    {
        var entity = builder.Entity<CustomerAnalytics>();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id)
            .HasConversion(
                v => v.Value,
                v => new AnalyticsId(v))
            .HasColumnName("id");
        entity.Property(e => e.UserId).HasColumnName("user_id");
        entity.Property(e => e.TotalProjects).HasColumnName("total_projects");
        entity.Property(e => e.Blueprints).HasColumnName("blueprints");
        entity.Property(e => e.DesignedGarments).HasColumnName("designed_garments");
        entity.Property(e => e.Completed).HasColumnName("completed");
        entity.ToTable("customer_analytics");
    }
    
    private static void ConfigureManufacturerAnalytics(ModelBuilder builder)
    {
        var entity = builder.Entity<ManufacturerAnalytics>();
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id)
            .HasConversion(
                v => v.Value,
                v => new AnalyticsId(v))
            .HasColumnName("id");
        entity.Property(e => e.UserId).HasColumnName("user_id");
        entity.Property(e => e.TotalOrdersReceived).HasColumnName("total_orders_received");
        entity.Property(e => e.PendingFulfillments).HasColumnName("pending_fulfillments");
        entity.Property(e => e.ProducedProjects).HasColumnName("produced_projects");
        entity.Property(e => e.AvgFulfillmentTimeDays).HasColumnName("avg_fulfillment_time_days");
        entity.ToTable("manufacturer_analytics");
    }
}