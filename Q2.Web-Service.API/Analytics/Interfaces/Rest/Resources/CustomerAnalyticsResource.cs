namespace Q2.Web_Service.Api.Teelab.Analytics.Interfaces.Rest.Resources
{
    public record CustomerAnalyticsResource(
        string UserId,
        int TotalProjects,
        int Blueprints,
        int DesignedGarments,
        int Completed,
        string Id
    );
}