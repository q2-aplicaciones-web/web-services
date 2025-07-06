
using Q2.Web_Service.API.Shared.Domain.Repositories;
using Q2.Web_Service.API.Analytics.Domain.Model.Entities;

namespace Q2.Web_Service.API.Analytics.Domain.Repositories;

public interface IManufacturerAnalyticsRepository : IBaseRepository<ManufacturerAnalytics>
{
    Task<ManufacturerAnalytics> FindByManufacturerIdAsync(Guid manufacturerId);
}
