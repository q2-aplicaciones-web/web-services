using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.DesignLab.Domain.Model.Commands;

public record UpdateLayerCoordinatesCommand(
    ProjectId ProjectId,
    LayerId LayerId,
    int X,
    int Y,
    int Z
);