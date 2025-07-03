namespace Q2.Web_Service.Api.Teelab.Analytics.Interfaces.Rest.Resources
{
    public record ManufacturerAnalyticsResource(
        string UserId,
        int TotalOrdersReceived,
        int PendingFulfillments,
        int ProducedProjects,
        double AvgFulfillmentTimeDays
    );
}