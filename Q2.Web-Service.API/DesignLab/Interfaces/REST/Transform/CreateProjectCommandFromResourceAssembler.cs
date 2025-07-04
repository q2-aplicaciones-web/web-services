using Q2.Web_Service.API.DesignLab.Domain.Model.Commands;
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;
using Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Transform;

public class CreateProjectCommandFromResourceAssembler
{
    public static CreateProjectCommand ToCommandFromResource(CreateProjectResource resource)
    {
        // Validate and parse enum values with proper error handling
        if (!Enum.TryParse<EGarmentColor>(resource.GarmentColor, true, out var garmentColor))
        {
            throw new ArgumentException($"Invalid garment color: {resource.GarmentColor}. Valid values are: {string.Join(", ", Enum.GetNames<EGarmentColor>())}");
        }

        if (!Enum.TryParse<EGarmentGender>(resource.GarmentGender, true, out var garmentGender))
        {
            throw new ArgumentException($"Invalid garment gender: {resource.GarmentGender}. Valid values are: {string.Join(", ", Enum.GetNames<EGarmentGender>())}");
        }

        if (!Enum.TryParse<EGarmentSize>(resource.GarmentSize, true, out var garmentSize))
        {
            throw new ArgumentException($"Invalid garment size: {resource.GarmentSize}. Valid values are: {string.Join(", ", Enum.GetNames<EGarmentSize>())}");
        }

        return new CreateProjectCommand(
            new UserId(resource.UserId),
            resource.Title,
            garmentColor,
            garmentGender,
            garmentSize
        );
    }
}