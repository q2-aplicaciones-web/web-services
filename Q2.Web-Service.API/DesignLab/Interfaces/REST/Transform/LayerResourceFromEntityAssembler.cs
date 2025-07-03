using Q2.Web_Service.API.DesignLab.Domain.Model.Entities;
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;
using Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Transform;

public class LayerResourceFromEntityAssembler
{
    
    public static LayerResource FromEntity(Layer entity)
    {
        Dictionary<string, object> Details = new Dictionary<string, object>
        {
            { "ProjectId", entity.ProjectId.Id }
        };
        if (entity.LayerType == ELayerType.Image)
        {
            if (entity is ImageLayer imageLayer)
            {
                Details.Add("ImageUrl", imageLayer.ImageUrl); // Placeholder for actual image URL
                Details.Add("Width", imageLayer.Width);
                Details.Add("Height", imageLayer.Height);
            }
        } else if (entity.LayerType == ELayerType.Text)
        {
            if (entity is TextLayer textLayer)
            {
                Details.Add("TextContent", textLayer.Text);
                Details.Add("FontColor", textLayer.FontColor);
                Details.Add("FontFamily", textLayer.FontFamily);
                Details.Add("FontSize", textLayer.FontSize);
                Details.Add("IsBold", textLayer.IsBold);
                Details.Add("IsItalic", textLayer.IsItalic);
                Details.Add("IsUnderlined", textLayer.IsUnderline);
            }
        }

        return new LayerResource(
            entity.Id, // Cambiado: ahora es Guid
            entity.X,
            entity.Y,
            entity.Z,
            entity.Opacity,
            entity.IsVisible,
            entity.LayerType,
            entity.CreatedAt.ToString(),
            entity.UpdatedAt.ToString(),
            Details
        );

    }
}