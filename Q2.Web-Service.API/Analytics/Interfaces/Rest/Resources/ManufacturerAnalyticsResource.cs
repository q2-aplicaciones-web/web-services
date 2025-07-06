namespace Q2.Web_Service.API.Analytics.Interfaces.Rest.Resources
{
    /// <summary>
    /// Resource que representa las métricas de analytics de un manufacturer
    /// </summary>
    public record ManufacturerAnalyticsResource(
        string ManufacturerId,
        int TotalOrdersReceived,
        int PendingFulfillments,
        int ProducedProjects,
        double AvgFulfillmentTimeDays
    );
}