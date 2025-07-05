using Q2.Web_Service.API.DesignLab.Domain.Model.Entities;
using Q2.Web_Service.API.DesignLab.Domain.Model.Queries;
using Q2.Web_Service.API.DesignLab.Domain.Repositories;
using Q2.Web_Service.API.DesignLab.Domain.Services;

namespace Q2.Web_Service.API.DesignLab.Application.Internal.QueryServices;

public class LayerQueryService(ILayerRepository layerRepository) : ILayerQueryService
{
    public Layer Handle(GetLayerByIdQuery query)
    {
        var layer = layerRepository.GetByIdAsync(query.LayerId).Result;
        return layer ?? throw new ArgumentException($"Layer with ID {query.LayerId} not found.");
    }
}
