// Q2.Web-Service.API/analytics/domain/model/entities/ManufacturerAnalytics.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Quri.Teelab.Api.Teelab.Analytics.Domain.Model.ValueObjects;

namespace Quri.Teelab.Api.Teelab.Analytics.Domain.Model.Entities
{
    [Table("manufacturer_analytics")]
    public class ManufacturerAnalytics
    {
        [Key]
        [Column("id")]
        public AnalyticsId Id { get; private set; }

        [Column("user_id")]
        public Guid UserId { get; private set; }

        [Column("total_orders_received")]
        public int TotalOrdersReceived { get; private set; }

        [Column("pending_fulfillments")]
        public int PendingFulfillments { get; private set; }

        [Column("produced_projects")]
        public int ProducedProjects { get; private set; }

        [Column("avg_fulfillment_time_days")]
        public double AvgFulfillmentTimeDays { get; private set; }

        // Constructor protegido requerido por EF Core
        protected ManufacturerAnalytics() { }

        public ManufacturerAnalytics(
            AnalyticsId id,
            Guid userId,
            int totalOrdersReceived,
            int pendingFulfillments,
            int producedProjects,
            double avgFulfillmentTimeDays)
        {
            Id = id;
            UserId = userId;
            TotalOrdersReceived = totalOrdersReceived;
            PendingFulfillments = pendingFulfillments;
            ProducedProjects = producedProjects;
            AvgFulfillmentTimeDays = avgFulfillmentTimeDays;
        }
    }
}