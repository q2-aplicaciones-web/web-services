using Q2.Web_Service.API.DesignLab.Domain.Model.Commands;
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;
using Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Transform;

public class UpdateTextLayerCommandFromResourceAssembler
{
    public static UpdateTextLayerCommand ToCommandFromResource(UpdateTextLayerResource resource, Guid layerId)
    {
        return new UpdateTextLayerCommand(
            new LayerId(layerId),
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
