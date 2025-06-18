using Quri.Teelab.Api.Teelab.Analytics.Domain.Model.Entities;
using Quri.Teelab.Api.Teelab.Analytics.Domain.Model.Queries;
using Q2.Web_Service.API.analytics.infrastructure.persistence.jpa.repositories;

namespace Q2.WebService.API.Analytics.Application.Internal.QueryServices
{
    public class CustomerAnalyticsQueryServiceImpl(CustomerAnalyticsRepository customerAnalyticsRepository)
    {
        private readonly CustomerAnalyticsRepository _customerAnalyticsRepository = customerAnalyticsRepository;

        public CustomerAnalytics Handle(GetCustomerAnalyticsByUserIdQuery query)
        {
            return _customerAnalyticsRepository.FindByUserId(query.UserId);
        }
    }
}