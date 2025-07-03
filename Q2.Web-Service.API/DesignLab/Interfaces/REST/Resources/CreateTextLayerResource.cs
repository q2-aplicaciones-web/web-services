using System.ComponentModel.DataAnnotations;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

public record CreateTextLayerResource(
    string ProjectId,
    
    [Required(ErrorMessage = "Text cannot be null or empty")]
    [StringLength(500, ErrorMessage = "Text cannot exceed 500 characters")]
    string Text,
    
    [Required(ErrorMessage = "Font color cannot be null or empty")]
    string FontColor,
    
    [Required(ErrorMessage = "Font family cannot be null or empty")]
    string FontFamily,
    
    [Range(1, 200, ErrorMessage = "Font size must be between 1 and 200")]
    int FontSize,
    
    bool IsBold,
    bool IsItalic,
    bool IsUnderlined
);