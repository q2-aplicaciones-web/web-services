using Q2.Web_Service.API.Analytics.Domain.Model.Entities;
using Q2.Web_Service.API.Analytics.Domain.Model.Queries;
using Q2.Web_Service.API.Analytics.Infrastructure.Persistence.EFC.Repositories;

namespace Q2.Web_Service.API.Analytics.Application.Internal.Queryservices
{
    public class ManufacturerAnalyticsQueryServiceImpl
    {
        private readonly ManufacturerAnalyticsRepository _manufacturerAnalyticsRepository;

        public ManufacturerAnalyticsQueryServiceImpl(ManufacturerAnalyticsRepository manufacturerAnalyticsRepository)
        {
            _manufacturerAnalyticsRepository = manufacturerAnalyticsRepository;
        }

        public ManufacturerAnalytics? Handle(GetManufacturerAnalyticsByManufacturerIdQuery query)
        {
            return _manufacturerAnalyticsRepository.FindByManufacturerId(query.ManufacturerId);
        }
    }
}