// Q2.Web-Service.API/analytics/domain/model/queries/GetCustomerAnalyticsByUserIdQuery.cs
using System;

namespace Q2.Web_Service.Api.Teelab.Analytics.Domain.Model.Queries
{
    public class GetCustomerAnalyticsByUserIdQuery
    {
        public Guid UserId { get; }

        public GetCustomerAnalyticsByUserIdQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}