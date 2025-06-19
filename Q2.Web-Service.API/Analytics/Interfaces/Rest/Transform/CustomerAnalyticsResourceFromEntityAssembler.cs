using Quri.Teelab.Api.Teelab.Analytics.Domain.Model.Entities;
using Quri.Teelab.Api.Teelab.Analytics.Interfaces.Rest.Resources;

namespace Q2.Web_Service.API.Analytics.Interfaces.Rest.Transform;

/// <summary>
/// Assembler to convert CustomerAnalytics entities to CustomerAnalyticsResource
/// </summary>
public static class CustomerAnalyticsResourceFromEntityAssembler
{
    public static CustomerAnalyticsResource ToResourceFromEntity(CustomerAnalytics entity)
    {
        if (entity == null) return null;

        return new CustomerAnalyticsResource(
            entity.UserId.ToString(),
            entity.TotalProjects,
            entity.Blueprints,
            entity.DesignedGarments,
            entity.Completed,
            entity.Id.Value
        );
    }

    public static IEnumerable<CustomerAnalyticsResource> ToResourceFromEntityList(IEnumerable<CustomerAnalytics> entities)
    {
        return entities?.Select(ToResourceFromEntity).ToList() ?? new List<CustomerAnalyticsResource>();
    }
}
