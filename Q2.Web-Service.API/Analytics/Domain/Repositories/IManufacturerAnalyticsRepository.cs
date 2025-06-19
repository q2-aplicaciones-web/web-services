using Q2.Web_Service.API.Shared.Domain.Repositories;
using Quri.Teelab.Api.Teelab.Analytics.Domain.Model.Entities;

namespace Q2.Web_Service.API.Analytics.Domain.Repositories;

public interface IManufacturerAnalyticsRepository : IBaseRepository<ManufacturerAnalytics>
{
    Task<ManufacturerAnalytics> FindByUserIdAsync(Guid userId);
    
    
}
