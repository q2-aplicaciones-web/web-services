using Q2.Web_Service.API.IAM.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Q2.Web_Service.API.IAM.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        // IAM Context
        
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(u => u.Username).IsRequired().HasMaxLength(50);
        builder.Entity<User>().Property(u => u.PasswordHash).IsRequired().HasMaxLength(255);
        
        // Create unique index on Username
        builder.Entity<User>().HasIndex(u => u.Username).IsUnique();
        
        // Configure table name
        builder.Entity<User>().ToTable("users");
    }
}