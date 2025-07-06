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
    /// Implementación del servicio de consulta de analytics de customers que calcula KPIs en tiempo real
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
                // 1. Obtener total de proyectos del usuario
                var totalProjects = _projectFacade.GetProjectCountByUserId(query.UserId);

                // 2. Obtener IDs de proyectos del usuario
                var userProjectIds = _projectFacade.FetchProjectIdsByUserId(query.UserId);

                // 3. Clasificar proyectos según tengan productos o no
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

                // 4. Obtener órdenes completadas
                var userOrders = _orderProcessingFacade.FetchOrdersByUserId(query.UserId);
                int completed = userOrders.Count;

                // 5. Crear y retornar la entidad
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
                // En caso de error, retorna null para indicar que no se pudieron obtener los analytics
                return null;
            }
        }
    }
}
