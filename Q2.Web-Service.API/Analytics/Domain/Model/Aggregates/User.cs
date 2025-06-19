// Q2.Web-Service.API/analytics/domain/model/aggregates/User.cs
using Quri.Teelab.Api.Teelab.Analytics.Domain.Model.Entities;

namespace Quri.Teelab.Api.Teelab.Analytics.Domain.Model.Aggregates
{
    public class User
    {
        public CustomerAnalytics CustomerAnalytics { get; private set; }
        public ManufacturerAnalytics ManufacturerAnalytics { get; private set; }

        public User(CustomerAnalytics customerAnalytics, ManufacturerAnalytics manufacturerAnalytics)
        {
            CustomerAnalytics = customerAnalytics;
            ManufacturerAnalytics = manufacturerAnalytics;
        }

        public void UpdateCustomerAnalytics(CustomerAnalytics newAnalytics)
        {
            CustomerAnalytics = newAnalytics;
        }

        public void UpdateManufacturerAnalytics(ManufacturerAnalytics newAnalytics)
        {
            ManufacturerAnalytics = newAnalytics;
        }
    }
}