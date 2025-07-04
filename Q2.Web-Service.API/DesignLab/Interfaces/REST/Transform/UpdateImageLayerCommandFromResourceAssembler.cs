using Q2.Web_Service.API.DesignLab.Domain.Model.Commands;
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;
using Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Transform;

public class UpdateImageLayerCommandFromResourceAssembler
{
    public static UpdateImageLayerCommand ToCommandFromResource(UpdateImageLayerResource resource, Guid layerId)
    {
        // Validate and parse width and height
        if (!int.TryParse(resource.Width, out var width) || width <= 0)
        {
            throw new ArgumentException($"Width must be a positive integer, got: {resource.Width}");
        }
        
        if (!int.TryParse(resource.Height, out var height) || height <= 0)
        {
            throw new ArgumentException($"Height must be a positive integer, got: {resource.Height}");
        }

        return new UpdateImageLayerCommand(
            new LayerId(layerId),
            resource.ImageUrl,
            width,
            height
        );
    }
}
