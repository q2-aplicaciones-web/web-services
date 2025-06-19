using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

public record ProjectResource(
        Guid Id,
        string Title,
        Guid UserId,
        string PreviewUrl,
        EProjectStatus Status,
        EGarmentColor Color,
        EGarmentSize Size,
        EGarmentGender Gender,
        List<LayerResource> Layers,
        String CreatedAt,
        String UpdatedAt
);
