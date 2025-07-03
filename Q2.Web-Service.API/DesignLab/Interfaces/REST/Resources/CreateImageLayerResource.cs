using System.ComponentModel.DataAnnotations;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

public record CreateImageLayerResource(
    string ProjectId,
    
    [Required(ErrorMessage = "Image URL cannot be null or empty")]
    [Url(ErrorMessage = "Image URL must be a valid URL")]
    string ImageUrl,
    
    [Required(ErrorMessage = "Width cannot be null or empty")]
    string Width,
    
    [Required(ErrorMessage = "Height cannot be null or empty")]
    string Height
);
