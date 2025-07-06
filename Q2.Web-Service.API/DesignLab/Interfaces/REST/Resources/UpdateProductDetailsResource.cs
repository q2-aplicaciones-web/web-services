using System.ComponentModel.DataAnnotations;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

public record UpdateProductDetailsResource(
    string? PreviewUrl,
    [Required] string Status,
    [Required] string GarmentColor,
    [Required] string GarmentSize,
    [Required] string GarmentGender);
