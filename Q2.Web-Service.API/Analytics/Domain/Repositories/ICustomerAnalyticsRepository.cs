using Q2.Web_Service.API.Analytics.Domain.Model.Entities;
using Q2.Web_Service.API.Shared.Domain.Repositories;

namespace Q2.Web_Service.API.Analytics.Domain.Repositories;

public interface ICustomerAnalyticsRepository : IBaseRepository<CustomerAnalytics>
{
    Task<CustomerAnalytics> FindByUserIdAsync(Guid userId);
}
