// Q2.Web-Service.API/analytics/domain/model/commands/UpdateManufacturerAnalyticsCommand.cs
using Quri.Teelab.Api.Teelab.Analytics.Domain.Model.Entities;

namespace Quri.Teelab.Api.Teelab.Analytics.Domain.Model.Commands
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