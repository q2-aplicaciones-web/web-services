using Q2.Web_Service.API.Analytics.Domain.Model.Entities;
using Q2.Web_Service.API.Shared.Domain.Repositories;

namespace Q2.Web_Service.API.Analytics.Domain.Repositories;

public interface IManufacturerAnalyticsRepository : IBaseRepository<ManufacturerAnalytics>
{
    Task<ManufacturerAnalytics> FindByUserIdAsync(Guid userId);
}
