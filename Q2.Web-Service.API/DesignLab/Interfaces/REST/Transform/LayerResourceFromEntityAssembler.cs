using Q2.Web_Service.API.DesignLab.Domain.Model.Entities;
using Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Transform;

public class LayerResourceFromEntityAssembler
{
    
    public static LayerResource FromEntity(Layer entity)
    {
        return new LayerResource(
            entity.Id.Id,
            entity.X,
            entity.Y,
            entity.Z,
            entity.Opacity,
            entity.IsVisible,
            entity.LayerType,
            entity.CreatedAt.ToString(),
            entity.UpdatedAt.ToString(),
            
        );

    }
}