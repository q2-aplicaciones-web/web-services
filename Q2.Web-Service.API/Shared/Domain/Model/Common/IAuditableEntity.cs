using System;

namespace Q2.Web_Service.API.Shared.Domain.Model.Common
{
    /// <summary>
    /// Interface for entities that track creation and update timestamps
    /// </summary>
    public interface IAuditableEntity
    {
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
    }
}
