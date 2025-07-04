using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

public record UpdateImageLayerResource(
    [Required(ErrorMessage = "Image URL cannot be null or empty")]
    [Url(ErrorMessage = "Image URL must be a valid URL")]
    [SwaggerParameter("Updated valid URL pointing to the image resource", Required = true)]
    string ImageUrl,
    
    [Required(ErrorMessage = "Width is required")]
    [SwaggerParameter("Updated image width as string (will be converted to integer)", Required = true)]
    string Width,
    
    [Required(ErrorMessage = "Height is required")]
    [SwaggerParameter("Updated image height as string (will be converted to integer)", Required = true)]
    string Height
);
