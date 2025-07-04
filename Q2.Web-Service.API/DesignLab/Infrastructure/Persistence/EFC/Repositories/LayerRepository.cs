using Q2.Web_Service.API.DesignLab.Domain.Model.Aggregates;
using Q2.Web_Service.API.DesignLab.Domain.Model.Entities;
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;
using Q2.Web_Service.API.DesignLab.Domain.Repositories;
using Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Q2.Web_Service.API.DesignLab.Infrastructure.Persistence.EFC.Repositories;

public class LayerRepository(AppDbContext context) : BaseRepository<Layer>(context), ILayerRepository
{
    /// <inheritdoc />
    public async Task<Layer?> GetByIdAsync(LayerId layerId)
    {
        return await Context.Set<Layer>()
            .FirstOrDefaultAsync(l => l.Id == layerId);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Layer layer)
    {
        Context.Set<Layer>().Update(layer);
        await Context.SaveChangesAsync();
    }
}