using Q2.Web_Service.API.DesignLab.Domain.Model.Commands;
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;
using Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Transform;

public class CreateImageLayerCommandFromResourceAssembler
{
    public static CreateImageLayerCommand ToCommandFromResource(CreateImageLayerResource resource, Guid projectId)
    {
        // Parse width and height from strings to integers
        if (!int.TryParse(resource.Width, out var width))
        {
            throw new ArgumentException($"Invalid width value: {resource.Width}");
        }
        
        if (!int.TryParse(resource.Height, out var height))
        {
            throw new ArgumentException($"Invalid height value: {resource.Height}");
        }

        return new CreateImageLayerCommand(
            new ProjectId(projectId),
            resource.ImageUrl,
            width,
            height
        );
    }
}