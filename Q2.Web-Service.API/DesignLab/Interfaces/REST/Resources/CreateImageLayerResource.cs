using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

public record CreateImageLayerResource(
    [Required(ErrorMessage = "Project ID is required")]
    [SwaggerParameter("Project ID to add the image layer to", Required = true)]
    Guid ProjectId,
    
    [Required(ErrorMessage = "Image URL cannot be null or empty")]
    [Url(ErrorMessage = "Image URL must be a valid URL")]
    [SwaggerParameter("Valid URL pointing to the image resource", Required = true)]
    string ImageUrl,
    
    [Range(1, 10000, ErrorMessage = "Width must be between 1 and 10000 pixels")]
    [SwaggerParameter("Image width in pixels", Required = true)]
    int Width,
    
    [Range(1, 10000, ErrorMessage = "Height must be between 1 and 10000 pixels")]
    [SwaggerParameter("Image height in pixels", Required = true)]
    int Height
);
