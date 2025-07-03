// Q2.Web-Service.API/analytics/domain/model/commands/UpdateCustomerAnalyticsCommand.cs
using Q2.Web_Service.Api.Teelab.Analytics.Domain.Model.Entities;

namespace Q2.Web_Service.Api.Teelab.Analytics.Domain.Model.Commands
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