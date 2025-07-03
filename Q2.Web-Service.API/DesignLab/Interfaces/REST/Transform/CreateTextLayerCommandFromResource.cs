using Q2.Web_Service.API.DesignLab.Domain.Model.Commands;
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;
using Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Transform;

public class CreateTextLayerCommandFromResource
{
    
    public static CreateTextLayerCommand ToCommandFromResource(
        CreateTextLayerResource resource, Guid projectId)
    {
        return new CreateTextLayerCommand(
            new ProjectId(projectId),
            resource.Text,
            resource.FontColor,
            resource.FontFamily,
            resource.FontSize,
            resource.IsBold,
            resource.IsItalic,
            resource.IsUnderlined
            );
    }
}