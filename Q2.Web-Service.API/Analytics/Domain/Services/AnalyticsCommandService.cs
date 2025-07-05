// Q2.Web-Service.API/analytics/domain/services/AnalyticsCommandService.cs
using Q2.Web_Service.API.Analytics.Domain.Model.Entities;

namespace Q2.Web_Service.API.Analytics.Domain.Services
{
    /// <summary>
    /// Servicio de comandos para lógica de dominio de analytics.
    /// Maneja operaciones de negocio de solo lectura para analytics.
    /// </summary>
    public class AnalyticsCommandService
    {
        /// <summary>
        /// Calcula la tasa de cumplimiento para un fabricante.
        /// </summary>
        /// <param name="analytics">Entidad ManufacturerAnalytics</param>
        /// <returns>Tasa de cumplimiento como double entre 0 y 1</returns>
        public double CalculateFulfillmentRate(ManufacturerAnalytics analytics)
        {
            int totalOrders = analytics.TotalOrdersReceived;
            int pending = analytics.PendingFulfillments;
            if (totalOrders == 0) return 0.0;
            return (double)(totalOrders - pending) / totalOrders;
        }

        /// <summary>
        /// Calcula la tasa de finalización para un cliente.
        /// </summary>
        /// <param name="analytics">Entidad CustomerAnalytics</param>
        /// <returns>Tasa de finalización como double entre 0 y 1</returns>
        public double CalculateCompletionRate(CustomerAnalytics analytics)
        {
            int total = analytics.TotalProjects;
            int completed = analytics.Completed;
            if (total == 0) return 0.0;
            return (double)completed / total;
        }
    }
}