// Q2.Web-Service.API/analytics/domain/model/queries/GetCustomerAnalyticsByUserIdQuery.cs
using System;

namespace Quri.Teelab.Api.Teelab.Analytics.Domain.Model.Queries
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