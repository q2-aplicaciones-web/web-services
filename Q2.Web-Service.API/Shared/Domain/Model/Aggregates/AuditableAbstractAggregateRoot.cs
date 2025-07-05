using System;
using Q2.Web_Service.API.Shared.Domain.Model.Common;

namespace Q2.Web_Service.API.Shared.Domain.Model.Aggregates
{
    public abstract class AuditableAbstractAggregateRoot<T> : IAuditableEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
