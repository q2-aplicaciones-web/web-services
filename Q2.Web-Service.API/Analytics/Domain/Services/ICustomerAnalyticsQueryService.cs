using Q2.Web_Service.API.Analytics.Domain.Model.Entities;
using Q2.Web_Service.API.Analytics.Domain.Model.Queries;

namespace Q2.Web_Service.API.Analytics.Domain.Services
{
    /// <summary>
    /// Interfaz para el servicio de consulta de analytics de customers
    /// </summary>
    public interface ICustomerAnalyticsQueryService
    {
        /// <summary>
        /// Maneja la consulta de analytics de customer por userId
        /// </summary>
        /// <param name="query">Query con el userId</param>
        /// <returns>Analytics del customer o null si no se encuentra</returns>
        CustomerAnalytics? Handle(GetCustomerAnalyticsByUserIdQuery query);
    }
}
