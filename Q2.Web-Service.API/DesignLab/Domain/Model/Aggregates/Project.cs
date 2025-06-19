using Q2.Web_Service.API.DesignLab.Domain.Model.Commands;
using Q2.Web_Service.API.DesignLab.Domain.Model.Entities;
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.DesignLab.Domain.Model.Aggregates;

public partial class Project
{
    public ProjectId Id { get; private set; } = null!;
    public string Title { get; private set; } = null!;
    public UserId UserId { get; private set; } = null!;
    public Uri? PreviewUrl { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    public ICollection<Layer> Layers { get; private set; } = null!;
      // Constructor protegido requerido por EF Core
    protected Project() 
    {
        // Initialize DateTime properties to avoid null constraint violations
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }public Project(CreateProjectCommand command)
    {
        Id = new ProjectId(Guid.NewGuid());
        Title = command.Title;
        UserId = command.UserId;
        PreviewUrl = null;
        Color = command.GarmentColor;
        Gender = command.GarmentGender;
        Size = command.GarmentSize;
        Status = EProjectStatus.Blueprint; // Default status
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        Layers = new List<Layer>();
    }
    
    // Remove Layer method
    public void RemoveLayer(Layer layer)
    {
        if (Layers == null || !Layers.Contains(layer))
        {
            throw new ArgumentException("Layer not found in the project", nameof(layer));
        }

        Layers.Remove(layer);
        UpdatedAt = DateTime.UtcNow; // Update the project timestamp
    }
    
    // Add Layer method
    public void AddLayer(Layer layer)
    {
        if (Layers == null)
        {
            Layers = new List<Layer>();
        }

        if (Layers.Any(l => l.Id == layer.Id))
        {
            throw new InvalidOperationException("Layer with the same ID already exists in the project.");
        }

        Layers.Add(layer);
        UpdatedAt = DateTime.UtcNow; // Update the project timestamp
    }
}