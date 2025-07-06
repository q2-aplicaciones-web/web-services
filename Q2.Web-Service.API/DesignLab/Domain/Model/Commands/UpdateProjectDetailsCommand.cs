using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.DesignLab.Domain.Model.Commands;

public record UpdateProjectDetailsCommand(
    ProjectId ProjectId,
    Uri? PreviewUrl,
    EProjectStatus Status,
    EGarmentColor GarmentColor,
    EGarmentSize GarmentSize,
    EGarmentGender GarmentGender);
