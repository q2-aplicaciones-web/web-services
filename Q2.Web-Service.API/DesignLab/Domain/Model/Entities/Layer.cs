using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.DesignLab.Domain.Model.Entities;

public class Layer
{
    public LayerId Id { get; private set; }
    public ProjectId ProjectId { get; private set; }
    public int X { get; private set; }
    public int Y { get; private set; }
    public int Z { get; private set; }
    public float Opacity { get; private set; }
    public bool IsVisible { get; private set; }
    
    public ELayerType LayerType { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
}