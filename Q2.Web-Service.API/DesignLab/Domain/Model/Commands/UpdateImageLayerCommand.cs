using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.DesignLab.Domain.Model.Commands;

public record UpdateImageLayerCommand(
    LayerId LayerId,
    string ImageUrl,
    int Width,
    int Height
);
