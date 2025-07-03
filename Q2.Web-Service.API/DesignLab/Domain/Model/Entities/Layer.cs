using Q2.Web_Service.API.DesignLab.Domain.Model.Commands;
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.DesignLab.Domain.Model.Entities;

public class Layer
{
    // EF Core necesita una propiedad Guid para la clave primaria
    public Guid Id { get; private set; }
    // Value object expuesto para el dominio
    public LayerId LayerId => new LayerId(Id);
    public ProjectId ProjectId { get; private set; }
    public int X { get; private set; }
    public int Y { get; private set; }
    public int Z { get; private set; }
    public float Opacity { get; private set; }
    public bool IsVisible { get; private set; }
    public ELayerType LayerType { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    // Constructor protegido requerido por EF Core
    protected Layer() { }

    public Layer(CreateTextLayerCommand command)
    {
        Id = Guid.NewGuid();
        ProjectId = command.ProjectId;
        X = 0; // Default value, can be changed later
        Y = 0; // Default value, can be changed later
        Z = 0; // Default value, can be changed later
        Opacity = 1.0f; // Default opacity
        IsVisible = true; // Default visibility
        LayerType = ELayerType.Text; // Set layer type based on command
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public Layer(CreateImageLayerCommand command)
    {
        Id = Guid.NewGuid();
        ProjectId = command.ProjectId;
        X = 0; // Default value, can be changed later
        Y = 0; // Default value, can be changed later
        Z = 0; // Default value, can be changed later
        Opacity = 1.0f; // Default opacity
        IsVisible = true; // Default visibility
        LayerType = ELayerType.Image; // Set layer type based on command
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public Layer(Guid id, ProjectId projectId)
    {
        Id = id;
        ProjectId = projectId;
        X = 0;
        Y = 0;
        Z = 0;
        Opacity = 1.0f;
        IsVisible = true;
        LayerType = ELayerType.Text;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}