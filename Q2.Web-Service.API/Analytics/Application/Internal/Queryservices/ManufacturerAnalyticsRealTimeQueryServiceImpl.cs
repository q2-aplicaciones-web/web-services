using System;
using System.Linq;
using Q2.Web_Service.API.Analytics.Domain.Model.Entities;
using Q2.Web_Service.API.Analytics.Domain.Model.Queries;
using Q2.Web_Service.API.Analytics.Domain.Services;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects;
using Q2.Web_Service.API.OrderFulfillment.Interfaces.ACL;

namespace Q2.Web_Service.API.Analytics.Application.Internal.Queryservices
{
    /// <summary>
    /// Manufacturer analytics query service implementation that calculates KPIs in real-time
    /// </summary>
    public class ManufacturerAnalyticsRealTimeQueryServiceImpl : IManufacturerAnalyticsQueryService
    {
        private readonly IOrderFulfillmentContextFacade _orderFulfillmentFacade;

        public ManufacturerAnalyticsRealTimeQueryServiceImpl(
            IOrderFulfillmentContextFacade orderFulfillmentFacade)
        {
            _orderFulfillmentFacade = orderFulfillmentFacade ?? throw new ArgumentNullException(nameof(orderFulfillmentFacade));
        }

        public ManufacturerAnalytics? Handle(GetManufacturerAnalyticsByManufacturerIdQuery query)
        {
            if (query?.ManufacturerId == null || query.ManufacturerId == Guid.Empty)
                return null;

            try
            {
                if (!_orderFulfillmentFacade.ManufacturerExists(query.ManufacturerId))
                    return null;

                var fulfillments = _orderFulfillmentFacade.GetFulfillmentsByManufacturerId(query.ManufacturerId);

                var totalOrdersReceived = fulfillments.Count;
                
                var pendingFulfillments = fulfillments.Count(f => 
                    f.Status == FulfillmentStatus.PENDING || 
                    f.Status == FulfillmentStatus.PROCESSING);
                
                var producedProjects = fulfillments.Count(f => 
                    f.Status == FulfillmentStatus.SHIPPED || 
                    f.Status == FulfillmentStatus.DELIVERED);
                
                var avgFulfillmentTimeDays = CalculateAverageFulfillmentTime(fulfillments);

                return ManufacturerAnalytics.CreateFromRealTimeData(
                    query.ManufacturerId,
                    totalOrdersReceived,
                    pendingFulfillments,
                    producedProjects,
                    avgFulfillmentTimeDays
                );
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Calculates the average fulfillment time in days
        /// </summary>
        private double CalculateAverageFulfillmentTime(System.Collections.Generic.List<FulfillmentInfo> fulfillments)
        {
            var completedFulfillments = fulfillments
                .Where(f => f.ReceivedDate != null && f.ShippedDate != null)
                .ToList();

            if (!completedFulfillments.Any())
                return 0.0;

            var totalDays = completedFulfillments
                .Select(f => (f.ShippedDate!.Value - f.ReceivedDate!.Value).TotalDays)
                .Sum();

            return totalDays / completedFulfillments.Count;
        }
    }
}
