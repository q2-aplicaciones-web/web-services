using Q2.Web_Service.API.Shared.Domain.Repositories;
using Q2.Web_Service.Api.Teelab.Analytics.Domain.Model.Entities;

namespace Q2.Web_Service.API.Analytics.Domain.Repositories;

public interface ICustomerAnalyticsRepository : IBaseRepository<CustomerAnalytics>
{
    Task<CustomerAnalytics> FindByUserIdAsync(Guid userId);
}
