using System;
using System.Linq;
using Q2.Web_Service.API.Analytics.Domain.Model.Entities;
using Q2.Web_Service.API.Analytics.Domain.Model.Queries;
using Q2.Web_Service.API.Analytics.Domain.Services;
using Q2.Web_Service.API.DesignLab.Interfaces.ACL;
using Q2.Web_Service.API.ProductCatalog.Interfaces.ACL.ProductCatalogContextFacadeNS;
using Q2.Web_Service.API.OrdersProcessing.Interfaces.REST.ACL;

namespace Q2.Web_Service.API.Analytics.Application.Internal.Queryservices
{
    /// <summary>
    /// Customer analytics query service implementation that calculates KPIs in real-time
    /// </summary>
    public class CustomerAnalyticsRealTimeQueryServiceImpl : ICustomerAnalyticsQueryService
    {
        private readonly IProjectContextFacade _projectFacade;
        private readonly IProductCatalogContextFacade _productCatalogFacade;
        private readonly IOrderProcessingContextFacade _orderProcessingFacade;

        public CustomerAnalyticsRealTimeQueryServiceImpl(
            IProjectContextFacade projectFacade,
            IProductCatalogContextFacade productCatalogFacade,
            IOrderProcessingContextFacade orderProcessingFacade)
        {
            _projectFacade = projectFacade ?? throw new ArgumentNullException(nameof(projectFacade));
            _productCatalogFacade = productCatalogFacade ?? throw new ArgumentNullException(nameof(productCatalogFacade));
            _orderProcessingFacade = orderProcessingFacade ?? throw new ArgumentNullException(nameof(orderProcessingFacade));
        }

        public CustomerAnalytics? Handle(GetCustomerAnalyticsByUserIdQuery query)
        {
            if (query?.UserId == null || query.UserId == Guid.Empty)
                return null;

            try
            {
                var totalProjects = _projectFacade.GetProjectCountByUserId(query.UserId);
                var userProjectIds = _projectFacade.FetchProjectIdsByUserId(query.UserId);

                int blueprints = 0;
                int designedGarments = 0;
                
                foreach (var projectId in userProjectIds)
                {
                    bool hasProducts = _productCatalogFacade.ProjectHasProducts(projectId.ToString());
                    if (hasProducts)
                        designedGarments++;
                    else
                        blueprints++;
                }

                var userOrders = _orderProcessingFacade.FetchOrdersByUserId(query.UserId);
                int completed = userOrders.Count;

                return CustomerAnalytics.CreateFromRealTimeData(
                    query.UserId,
                    (int)totalProjects,
                    blueprints,
                    designedGarments,
                    completed
                );
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
