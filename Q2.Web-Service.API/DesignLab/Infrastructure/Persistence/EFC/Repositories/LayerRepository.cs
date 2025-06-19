using Q2.Web_Service.API.DesignLab.Domain.Model.Aggregates;
using Q2.Web_Service.API.DesignLab.Domain.Model.Entities;
using Q2.Web_Service.API.DesignLab.Domain.Repositories;
using Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Q2.Web_Service.API.DesignLab.Infrastructure.Persistence.EFC.Repositories;

public class LayerRepository(AppDbContext context) : BaseRepository<Layer>(context), ILayerRepository
{
    
}