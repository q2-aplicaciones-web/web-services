// Q2.Web-Service.API/analytics/domain/model/entities/CustomerAnalytics.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Q2.Web_Service.Api.Teelab.Analytics.Domain.Model.ValueObjects;

namespace Q2.Web_Service.Api.Teelab.Analytics.Domain.Model.Entities
{
    [Table("customer_analytics")]
    public class CustomerAnalytics
    {
        [Key]
        [Column("id")]
        public AnalyticsId Id { get; private set; }

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
        protected CustomerAnalytics() { }

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
    }
}