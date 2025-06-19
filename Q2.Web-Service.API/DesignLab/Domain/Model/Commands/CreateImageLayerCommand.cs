
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.DesignLab.Domain.Model.Commands;

public record CreateImageLayerCommand(ProjectId ProjectId, String ImageUrl, int Width, int Height);