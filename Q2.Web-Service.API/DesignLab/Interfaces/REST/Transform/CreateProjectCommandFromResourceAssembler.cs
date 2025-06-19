using Q2.Web_Service.API.DesignLab.Domain.Model.Commands;
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;
using Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Transform;

public class CreateProjectCommandFromResourceAssembler
{
    public static CreateProjectCommand ToCommandFromResource(CreateProjectResource resource)
    {
        try
        {
            return new CreateProjectCommand(
                new UserId(Guid.Parse(resource.UserId)),
                resource.Title,
                Enum.Parse<EGarmentColor>(resource.GarmentColor, true),
                Enum.Parse<EGarmentGender>(resource.GarmentGender, true),
                Enum.Parse<EGarmentSize>(resource.GarmentSize, true)
            );
        }
        catch (ArgumentException ex) when (ex.Message.Contains("was not found"))
        {
            throw new ArgumentException($"Invalid enum value provided. {ex.Message}", ex);
        }
    }
}