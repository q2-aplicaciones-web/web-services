using System.ComponentModel.DataAnnotations;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

/// <summary>
/// Resource for updating layer coordinates
/// </summary>
public record UpdateLayerCoordinatesResource(
    [Required(ErrorMessage = "X coordinate cannot be null")]
    int X,
    
    [Required(ErrorMessage = "Y coordinate cannot be null")]
    int Y,
    
    [Required(ErrorMessage = "Z coordinate cannot be null")]
    int Z
);
