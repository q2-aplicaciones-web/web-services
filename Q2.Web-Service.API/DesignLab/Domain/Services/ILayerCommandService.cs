using Q2.Web_Service.API.DesignLab.Domain.Model.Commands;
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.DesignLab.Domain.Services;

public interface ILayerCommandService
{
    Task<LayerId?> Handle(CreateTextLayerCommand command);
    Task<LayerId?> Handle(CreateImageLayerCommand command);
    Task<LayerId?> Handle(DeleteProjectLayerCommand command);
}