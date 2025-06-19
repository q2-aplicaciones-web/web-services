// Q2.Web-Service.API/analytics/domain/model/queries/GetManufacturerAnalyticsByUserIdQuery.cs
using System;

namespace Quri.Teelab.Api.Teelab.Analytics.Domain.Model.Queries
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