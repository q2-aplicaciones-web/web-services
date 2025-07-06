using Q2.Web_Service.API.Analytics.Domain.Model.Entities;
using Q2.Web_Service.API.Analytics.Domain.Model.Queries;

namespace Q2.Web_Service.API.Analytics.Domain.Services
{
    /// <summary>
    /// Interfaz para el servicio de consulta de analytics de manufacturers
    /// </summary>
    public interface IManufacturerAnalyticsQueryService
    {
        /// <summary>
        /// Maneja la consulta de analytics de manufacturer por ID
        /// </summary>
        /// <param name="query">Query con el manufacturerId</param>
        /// <returns>Analytics del manufacturer o null si no se encuentra</returns>
        ManufacturerAnalytics? Handle(GetManufacturerAnalyticsByManufacturerIdQuery query);
    }
}
