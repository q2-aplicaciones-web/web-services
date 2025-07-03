using Q2.Web_Service.Api.Teelab.Analytics.Domain.Model.Entities;
using Q2.Web_Service.Api.Teelab.Analytics.Domain.Model.Queries;
using Q2.Web_Service.API.analytics.infrastructure.persistence.jpa.repositories;

namespace Q2.WebService.API.Analytics.Application.Internal.QueryServices
{
    public class ManufacturerAnalyticsQueryServiceImpl(ManufacturerAnalyticsRepository manufacturerAnalyticsRepository)
    {
        private readonly ManufacturerAnalyticsRepository _manufacturerAnalyticsRepository = manufacturerAnalyticsRepository;

        public ManufacturerAnalytics Handle(GetManufacturerAnalyticsByUserIdQuery query)
        {
            return _manufacturerAnalyticsRepository.FindByUserId(query.UserId);
        }
    }
}