// Q2.Web-Service.API/analytics/domain/model/commands/UpdateCustomerAnalyticsCommand.cs
using Quri.Teelab.Api.Teelab.Analytics.Domain.Model.Entities;

namespace Quri.Teelab.Api.Teelab.Analytics.Domain.Model.Commands
{
    public class UpdateCustomerAnalyticsCommand
    {
        public CustomerAnalytics CustomerAnalytics { get; }

        public UpdateCustomerAnalyticsCommand(CustomerAnalytics customerAnalytics)
        {
            CustomerAnalytics = customerAnalytics;
        }
    }
}