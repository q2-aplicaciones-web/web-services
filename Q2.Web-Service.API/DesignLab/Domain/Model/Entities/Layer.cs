using Q2.Web_Service.API.DesignLab.Domain.Model.Commands;
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.DesignLab.Domain.Model.Entities;

public abstract class Layer
{
    public LayerId Id { get; protected set; }
    public ProjectId ProjectId { get; protected set; }
    public int X { get; protected set; }
    public int Y { get; protected set; }
    public int Z { get; protected set; }
    public float Opacity { get; protected set; }
    public bool IsVisible { get; protected set; }
    
    public ELayerType LayerType { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime UpdatedAt { get; protected set; }

    // Constructor protegido requerido por EF Core
    protected Layer() 
    {
        Id = null!; // Will be set by EF Core
        ProjectId = null!; // Will be set by EF Core
    }

    protected Layer(ProjectId projectId, ELayerType layerType)
    {
        Id = new LayerId(Guid.NewGuid());
        ProjectId = projectId;
        X = 0; // Default value, can be changed later
        Y = 0; // Default value, can be changed later
        Z = 0; // Default value, can be changed later
        Opacity = 1.0f; // Default opacity
        IsVisible = true; // Default visibility
        LayerType = layerType;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the coordinates of the layer
    /// </summary>
    /// <param name="x">X coordinate (horizontal position)</param>
    /// <param name="y">Y coordinate (vertical position)</param>
    /// <param name="z">Z coordinate (depth/layer index)</param>
    public void UpdateCoordinates(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
        UpdatedAt = DateTime.UtcNow;
    }
}