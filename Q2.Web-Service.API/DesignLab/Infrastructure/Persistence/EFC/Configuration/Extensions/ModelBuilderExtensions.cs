using Microsoft.EntityFrameworkCore;
using Q2.Web_Service.API.DesignLab.Domain.Model.Aggregates;
using Q2.Web_Service.API.DesignLab.Domain.Model.Entities;
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.DesignLab.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyDesignLabConfiguration(this ModelBuilder builder)
    {
        ConfigureProject(builder);
        ConfigureLayer(builder);
    }

    private static void ConfigureProject(ModelBuilder builder)
    {
        var entity = builder.Entity<Project>();
        entity.ToTable("Projects");
        entity.HasKey(p => p.Id);

        entity.Property(p => p.Id)
            .IsRequired()
            .ValueGeneratedOnAdd()
            .HasConversion(
                id => id.Id,
                value => new ProjectId(value)
            );

        entity.Property(p => p.UserId)
            .IsRequired()
            .HasConversion(
                id => id.Id,
                value => new UserId(value)
            );        entity.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(30);

        entity.Property(p => p.Color)
            .IsRequired();

        entity.Property(p => p.PreviewUrl)
            .HasConversion(
                uri => uri != null ? uri.ToString() : null,
                str => str != null ? new Uri(str) : null
            );        entity.Property(p => p.CreatedAt)
            .IsRequired();

        entity.Property(p => p.UpdatedAt)
            .IsRequired();

        entity.Property(p => p.Status)
            .IsRequired();

        entity.Property(p => p.Gender)
            .IsRequired();

        entity.Property(p => p.Size)
            .IsRequired();

        entity
            .HasMany(p => p.Layers)
            .WithOne()
            .HasForeignKey(l => l.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void ConfigureLayer(ModelBuilder builder)
    {
        var entity = builder.Entity<Layer>();
        entity.ToTable("Layers");
        entity.HasKey(l => l.Id);

        // Configure inheritance - Table Per Hierarchy (TPH)
        entity.HasDiscriminator<ELayerType>("LayerType")
            .HasValue<TextLayer>(ELayerType.Text)
            .HasValue<ImageLayer>(ELayerType.Image);

        entity.Property(l => l.Id)
            .IsRequired()
            .ValueGeneratedOnAdd()
            .HasConversion(
                id => id.Id,
                value => new LayerId(value)
            );

        entity.Property(l => l.ProjectId)
            .IsRequired()
            .HasConversion(
                id => id.Id,
                value => new ProjectId(value)
            );

        entity.Property(l => l.X).IsRequired();
        entity.Property(l => l.Y).IsRequired();
        entity.Property(l => l.Z).IsRequired();
        entity.Property(l => l.Opacity).IsRequired();
        entity.Property(l => l.IsVisible).IsRequired();
        entity.Property(l => l.LayerType).IsRequired();

        entity.Property(l => l.CreatedAt)
            .IsRequired()
            .ValueGeneratedOnAdd();

        entity.Property(l => l.UpdatedAt)
            .IsRequired()
            .ValueGeneratedOnAddOrUpdate();

        // Configure TextLayer specific properties
        builder.Entity<TextLayer>(textEntity =>
        {
            textEntity.Property(t => t.Text).IsRequired();
            textEntity.Property(t => t.FontColor).IsRequired();
            textEntity.Property(t => t.FontFamily).IsRequired();
            textEntity.Property(t => t.FontSize).IsRequired();
            textEntity.Property(t => t.IsBold).IsRequired();
            textEntity.Property(t => t.IsItalic).IsRequired();
            textEntity.Property(t => t.IsUnderline).IsRequired();
        });

        // Configure ImageLayer specific properties
        builder.Entity<ImageLayer>(imageEntity =>
        {
            imageEntity.Property(i => i.ImageUrl)
                .IsRequired()
                .HasConversion(
                    uri => uri.ToString(),
                    str => new Uri(str)
                );
            imageEntity.Property(i => i.Width).IsRequired();
            imageEntity.Property(i => i.Height).IsRequired();
        });
    }
}