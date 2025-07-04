using Q2.Web_Service.API.DesignLab.Domain.Model.Entities;
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;
using Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Transform;

public class LayerResourceFromEntityAssembler
{
    public static LayerResource FromEntity(Layer entity)
    {
        var details = new Dictionary<string, object>();
        
        if (entity.LayerType == ELayerType.Image)
        {
            var imageLayer = entity as ImageLayer;
            if (imageLayer != null)
            {
                details.Add("imageUrl", imageLayer.ImageUrl.ToString());
                details.Add("width", imageLayer.Width);
                details.Add("height", imageLayer.Height);
            }
        } 
        else if (entity.LayerType == ELayerType.Text)
        {
            var textLayer = entity as TextLayer;
            if (textLayer != null)
            {
                details.Add("text", textLayer.Text);
                details.Add("fontColor", textLayer.FontColor);
                details.Add("fontFamily", textLayer.FontFamily);
                details.Add("fontSize", textLayer.FontSize);
                details.Add("isBold", textLayer.IsBold);
                details.Add("isItalic", textLayer.IsItalic);
                details.Add("isUnderlined", textLayer.IsUnderline);
            }
        }

        return new LayerResource(
            entity.Id.Id,
            entity.X,
            entity.Y,
            entity.Z,
            entity.Opacity,
            entity.IsVisible,
            entity.LayerType.ToString().ToUpper(),
            entity.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss.fff"),
            entity.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss.fff"),
            details
        );
    }
}