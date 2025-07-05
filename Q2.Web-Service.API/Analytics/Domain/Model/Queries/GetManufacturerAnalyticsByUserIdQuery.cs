// Q2.Web-Service.API/analytics/domain/model/queries/GetManufacturerAnalyticsByUserIdQuery.cs
using System;

namespace Q2.Web_Service.API.Analytics.Domain.Model.Queries
{
    public class GetManufacturerAnalyticsByUserIdQuery
    {
        public Guid UserId { get; }

        public GetManufacturerAnalyticsByUserIdQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}