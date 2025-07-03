using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

public record LayerResource(
    Guid Id,
    int X,
    int Y,
    int Z,
    float Opacity,
    bool IsVisible,
    string LayerType,
    string CreatedAt,
    string UpdatedAt,
    Dictionary<string, object> Details
);
