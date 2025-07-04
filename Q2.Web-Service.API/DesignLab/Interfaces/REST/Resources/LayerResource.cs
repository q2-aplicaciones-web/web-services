using Swashbuckle.AspNetCore.Annotations;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

public record LayerResource(
    [SwaggerParameter("Layer unique identifier")]
    Guid Id,
    
    [SwaggerParameter("X coordinate position")]
    int X,
    
    [SwaggerParameter("Y coordinate position")]
    int Y,
    
    [SwaggerParameter("Z-index for layer ordering")]
    int Z,
    
    [SwaggerParameter("Layer opacity (0.0 to 1.0)")]
    float Opacity,
    
    [SwaggerParameter("Whether the layer is visible")]
    bool IsVisible,
    
    [SwaggerParameter("Layer type (TEXT, IMAGE)")]
    string Type,
    
    [SwaggerParameter("Creation timestamp")]
    string CreatedAt,
    
    [SwaggerParameter("Last update timestamp")]
    string UpdatedAt,
    
    [SwaggerParameter("Layer-specific properties based on type")]
    Dictionary<string, object> Details
);
