using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.DesignLab.Domain.Model.Commands;

public record CreateProjectCommand(UserId UserId, string Title, EGarmentColor GarmentColor, EGarmentGender GarmentGender, EGarmentSize GarmentSize);