using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Q2.WebService.API.ProductCatalog.Application.ACL;
using Q2.WebService.API.ProductCatalog.Application.Internal.CommandServices;
using Q2.WebService.API.ProductCatalog.Application.Internal.QueryServices;
using Q2.WebService.API.ProductCatalog.Domain.Repositories;
using Q2.WebService.API.ProductCatalog.Domain.Services;
using Q2.WebService.API.ProductCatalog.Infrastructure.Persistence.EFC.Configuration;
using Q2.WebService.API.ProductCatalog.Infrastructure.Persistence.EFC.Repositories;
using Q2.WebService.API.ProductCatalog.Interfaces.ACL;

namespace Q2.WebService.API.ProductCatalog
{
    /// <summary>
    /// Extension methods to register ProductCatalog services
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Adds all ProductCatalog services to the service collection
        /// </summary>
        public static IServiceCollection AddProductCatalogServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Implementación futura
            return services;
        }
    }
}
