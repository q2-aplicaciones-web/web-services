using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

public record CreateProjectResource(
    [Required(ErrorMessage = "Title cannot be null or empty")]
    [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
    [SwaggerParameter("Project title", Required = true)]
    string Title,
    
    [Required(ErrorMessage = "User ID cannot be null or empty")]
    [SwaggerParameter("User ID who owns the project", Required = true)]
    Guid UserId,
    
    [Required(ErrorMessage = "Garment color is required")]
    [SwaggerParameter("Garment color. Valid values: Black, Gray, LightGray, White, Red, Pink, LightPurple, Purple, LightBlue, Cyan, SkyBlue, Blue, Green, LightGreen, Yellow, DarkYellow", Required = true)]
    string GarmentColor,
    
    [Required(ErrorMessage = "Garment size is required")]
    [SwaggerParameter("Garment size. Valid values: XS, S, M, L, XL, XXL", Required = true)]
    string GarmentSize,
    
    [Required(ErrorMessage = "Garment gender is required")]
    [SwaggerParameter("Garment gender. Valid values: Men, Women, Unisex, Kids", Required = true)]
    string GarmentGender
);
