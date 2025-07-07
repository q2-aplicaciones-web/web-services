
using Q2.Web_Service.API.DesignLab.Domain.Model.Entities;
using Q2.Web_Service.API.DesignLab.Domain.Model.ValueObjects;
using Q2.Web_Service.API.Shared.Domain.Repositories;

namespace Q2.Web_Service.API.DesignLab.Domain.Repositories;

public interface ILayerRepository : IBaseRepository<Layer>
{
    /// <summary>
    /// Get layer by LayerId (Guid-based)
    /// </summary>
    /// <param name="layerId">The LayerId to search for</param>
    /// <returns>Layer entity if found, null otherwise</returns>
    Task<Layer?> GetByIdAsync(LayerId layerId);
    
    /// <summary>
    /// Update an existing layer asynchronously
    /// </summary>
    /// <param name="layer">The layer entity to update</param>
    /// <returns>Task representing the asynchronous operation</returns>
    Task UpdateAsync(Layer layer);
}