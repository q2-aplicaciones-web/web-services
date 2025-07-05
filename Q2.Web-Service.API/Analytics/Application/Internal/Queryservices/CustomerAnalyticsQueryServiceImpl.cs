using Q2.Web_Service.API.Analytics.Domain.Model.Entities;
using Q2.Web_Service.API.Analytics.Domain.Model.Queries;
using Q2.Web_Service.API.Analytics.Infrastructure.Persistence.EFC.Repositories;

namespace Q2.Web_Service.API.Analytics.Application.Internal.Queryservices
{
    public class CustomerAnalyticsQueryServiceImpl
    {
        private readonly CustomerAnalyticsRepository _customerAnalyticsRepository;

        public CustomerAnalyticsQueryServiceImpl(CustomerAnalyticsRepository customerAnalyticsRepository)
        {
            _customerAnalyticsRepository = customerAnalyticsRepository;
        }

        public CustomerAnalytics? Handle(GetCustomerAnalyticsByUserIdQuery query)
        {
            return _customerAnalyticsRepository.FindByUserId(query.UserId);
        }
    }
}