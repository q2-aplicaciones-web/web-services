namespace Quri.Teelab.Api.Teelab.Analytics.Interfaces.Rest.Resources
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