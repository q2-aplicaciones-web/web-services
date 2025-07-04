using Q2.Web_Service.API.DesignLab.Domain.Model.Commands;
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;
using Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Transform;

public class CreateImageLayerCommandFromResourceAssembler
{
    public static CreateImageLayerCommand ToCommandFromResource(CreateImageLayerResource resource)
    {
        return new CreateImageLayerCommand(
            new ProjectId(resource.ProjectId),
            resource.ImageUrl,
            resource.Width,
            resource.Height
        );
    }
}