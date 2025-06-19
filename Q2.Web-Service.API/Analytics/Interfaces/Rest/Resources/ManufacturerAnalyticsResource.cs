namespace Quri.Teelab.Api.Teelab.Analytics.Interfaces.Rest.Resources
{
    public record ManufacturerAnalyticsResource(
        string UserId,
        int TotalOrdersReceived,
        int PendingFulfillments,
        int ProducedProjects,
        double AvgFulfillmentTimeDays
    );
}