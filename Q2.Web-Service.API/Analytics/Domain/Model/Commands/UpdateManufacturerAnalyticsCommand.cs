// Q2.Web-Service.API/analytics/domain/model/commands/UpdateManufacturerAnalyticsCommand.cs
using Q2.Web_Service.Api.Teelab.Analytics.Domain.Model.Entities;

namespace Q2.Web_Service.Api.Teelab.Analytics.Domain.Model.Commands
{
    public class UpdateManufacturerAnalyticsCommand
    {
        public ManufacturerAnalytics ManufacturerAnalytics { get; }

        public UpdateManufacturerAnalyticsCommand(ManufacturerAnalytics manufacturerAnalytics)
        {
            ManufacturerAnalytics = manufacturerAnalytics;
        }
    }
}