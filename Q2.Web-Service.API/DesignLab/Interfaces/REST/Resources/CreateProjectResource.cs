using System.ComponentModel.DataAnnotations;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

public record CreateProjectResource(
    [Required(ErrorMessage = "Title cannot be null or empty")]
    [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
    string Title,
    
    [Required(ErrorMessage = "User ID cannot be null or empty")]
    string UserId,
    
    [Required(ErrorMessage = "Garment color cannot be null or empty")]
    string GarmentColor,
    
    [Required(ErrorMessage = "Garment size cannot be null or empty")]
    string GarmentSize,
    
    [Required(ErrorMessage = "Garment gender cannot be null or empty")]
    string GarmentGender
);
