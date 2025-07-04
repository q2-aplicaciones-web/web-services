using Swashbuckle.AspNetCore.Annotations;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

public record ProjectResource(
        [SwaggerParameter("Project unique identifier")]
        Guid Id,
        
        [SwaggerParameter("Project title")]
        string Title,
        
        [SwaggerParameter("User ID who owns the project")]
        Guid UserId,
        
        [SwaggerParameter("Preview image URL")]
        string PreviewUrl,
        
        [SwaggerParameter("Project status (Blueprint, Garment, Template)")]
        string Status,
        
        [SwaggerParameter("Garment color (Black, Gray, LightGray, White, Red, Pink, LightPurple, Purple, LightBlue, Cyan, SkyBlue, Blue, Green, LightGreen, Yellow, DarkYellow)")]
        string Color,
        
        [SwaggerParameter("Garment size (XS, S, M, L, XL, XXL)")]
        string Size,
        
        [SwaggerParameter("Garment gender (Men, Women, Unisex, Kids)")]
        string Gender,
        
        [SwaggerParameter("List of layers in the project")]
        List<LayerResource> Layers,
        
        [SwaggerParameter("Creation timestamp")]
        string CreatedAt,
        
        [SwaggerParameter("Last update timestamp")]
        string UpdatedAt
);
