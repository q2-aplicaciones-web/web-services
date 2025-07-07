// Q2.Web-Service.API/analytics/domain/model/entities/CustomerAnalytics.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Q2.Web_Service.API.Analytics.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.Analytics.Domain.Model.Entities
{
    /// <summary>
    /// Entidad que representa las métricas analíticas calculadas para un customer
    /// </summary>
    [Table("customer_analytics")]
    public class CustomerAnalytics
    {
        [Key]
        [Column("id")]
        public AnalyticsId Id { get; private set; } = null!;

        [Column("user_id")]
        public Guid UserId { get; private set; }

        [Column("total_projects")]
        public int TotalProjects { get; private set; }

        [Column("blueprints")]
        public int Blueprints { get; private set; }

        [Column("designed_garments")]
        public int DesignedGarments { get; private set; }

        [Column("completed")]
        public int Completed { get; private set; }

        // Constructor protegido requerido por EF Core
        protected CustomerAnalytics() 
        {
            Id = null!;
        }

        public CustomerAnalytics(
            AnalyticsId id,
            Guid userId,
            int totalProjects,
            int blueprints,
            int designedGarments,
            int completed)
        {
            Id = id;
            UserId = userId;
            TotalProjects = totalProjects;
            Blueprints = blueprints;
            DesignedGarments = designedGarments;
            Completed = completed;
        }

        /// <summary>
        /// Crea una nueva instancia con métricas calculadas en tiempo real
        /// </summary>
        public static CustomerAnalytics CreateFromRealTimeData(
            Guid userId,
            int totalProjects,
            int blueprints,
            int designedGarments,
            int completed)
        {
            return new CustomerAnalytics(
                new AnalyticsId($"customer_{userId}"),
                userId,
                totalProjects,
                blueprints,
                designedGarments,
                completed
            );
        }
    }
}