using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

public record CreateTextLayerResource(
    [Required(ErrorMessage = "Project ID is required")]
    [SwaggerParameter("Project ID to add the text layer to", Required = true)]
    Guid ProjectId,
    
    [Required(ErrorMessage = "Text cannot be null or empty")]
    [StringLength(500, ErrorMessage = "Text cannot exceed 500 characters")]
    [SwaggerParameter("Text content for the layer", Required = true)]
    string Text,
    
    [Required(ErrorMessage = "Font color cannot be null or empty")]
    [SwaggerParameter("Font color in hex format (e.g., #FF0000)", Required = true)]
    string FontColor,
    
    [Required(ErrorMessage = "Font family cannot be null or empty")]
    [SwaggerParameter("Font family name (e.g., Arial, Times New Roman)", Required = true)]
    string FontFamily,
    
    [Range(1, 200, ErrorMessage = "Font size must be between 1 and 200")]
    [SwaggerParameter("Font size in pixels", Required = true)]
    int FontSize,
    
    [SwaggerParameter("Whether the text should be bold")]
    bool IsBold,
    
    [SwaggerParameter("Whether the text should be italic")]
    bool IsItalic,
    
    [SwaggerParameter("Whether the text should be underlined")]
    bool IsUnderlined
);