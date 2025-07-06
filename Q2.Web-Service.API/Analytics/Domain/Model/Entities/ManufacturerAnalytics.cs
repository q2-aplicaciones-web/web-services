// Q2.Web-Service.API/analytics/domain/model/entities/ManufacturerAnalytics.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Q2.Web_Service.API.Analytics.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.Analytics.Domain.Model.Entities
{
    /// <summary>
    /// Entidad que representa las métricas analíticas calculadas para un manufacturer
    /// </summary>
    [Table("manufacturer_analytics")]
    public class ManufacturerAnalytics
    {
        [Key]
        [Column("id")]
        public AnalyticsId Id { get; private set; } = null!;

        [Column("manufacturer_id")]
        public Guid ManufacturerId { get; private set; }

        [Column("total_orders_received")]
        public int TotalOrdersReceived { get; private set; }

        [Column("pending_fulfillments")]
        public int PendingFulfillments { get; private set; }

        [Column("produced_projects")]
        public int ProducedProjects { get; private set; }

        [Column("avg_fulfillment_time_days")]
        public double AvgFulfillmentTimeDays { get; private set; }

        // Constructor protegido requerido por EF Core
        protected ManufacturerAnalytics() 
        {
            Id = null!;
        }

        public ManufacturerAnalytics(
            AnalyticsId id,
            Guid manufacturerId,
            int totalOrdersReceived,
            int pendingFulfillments,
            int producedProjects,
            double avgFulfillmentTimeDays)
        {
            Id = id;
            ManufacturerId = manufacturerId;
            TotalOrdersReceived = totalOrdersReceived;
            PendingFulfillments = pendingFulfillments;
            ProducedProjects = producedProjects;
            AvgFulfillmentTimeDays = avgFulfillmentTimeDays;
        }

        /// <summary>
        /// Crea una nueva instancia con métricas calculadas en tiempo real
        /// </summary>
        public static ManufacturerAnalytics CreateFromRealTimeData(
            Guid manufacturerId,
            int totalOrdersReceived,
            int pendingFulfillments,
            int producedProjects,
            double avgFulfillmentTimeDays)
        {
            return new ManufacturerAnalytics(
                new AnalyticsId($"manufacturer_{manufacturerId}"),
                manufacturerId,
                totalOrdersReceived,
                pendingFulfillments,
                producedProjects,
                avgFulfillmentTimeDays
            );
        }
    }
}