using Q2.Web_Service.API.DesignLab.Domain.Model.Commands;
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.DesignLab.Domain.Services;

public interface ILayerCommandService
{
    Task<Guid?> Handle(CreateTextLayerCommand command);
    Task<Guid?> Handle(CreateImageLayerCommand command);
    Task<Guid?> Handle(DeleteProjectLayerCommand command);
}