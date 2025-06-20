using Quri.Teelab.Api.Teelab.Analytics.Domain.Model.Entities;
using Quri.Teelab.Api.Teelab.Analytics.Interfaces.Rest.Resources;

namespace Q2.Web_Service.API.Analytics.Interfaces.Rest.Transform;

/// <summary>
/// Assembler to convert ManufacturerAnalytics entities to ManufacturerAnalyticsResource
/// </summary>
public static class ManufacturerAnalyticsResourceFromEntityAssembler
{
    public static ManufacturerAnalyticsResource ToResourceFromEntity(ManufacturerAnalytics entity)
    {
        if (entity == null) return null;

        return new ManufacturerAnalyticsResource(
            entity.UserId.ToString(),
            entity.TotalOrdersReceived,
            entity.PendingFulfillments,
            entity.ProducedProjects,
            entity.AvgFulfillmentTimeDays
        );
    }

    public static IEnumerable<ManufacturerAnalyticsResource> ToResourceFromEntityList(IEnumerable<ManufacturerAnalytics> entities)
    {
        return entities?.Select(ToResourceFromEntity).ToList() ?? new List<ManufacturerAnalyticsResource>();
    }
}
