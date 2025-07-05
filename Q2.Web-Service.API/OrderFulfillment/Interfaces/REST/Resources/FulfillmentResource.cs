using System;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.OrderFulfillment.Interfaces.REST.Resources
{
    public record FulfillmentResource(
        string Id,
        string OrderId,
        FulfillmentStatus Status,
        DateTime? ReceivedDate,
        DateTime? ShippedDate,
        string? ManufacturerId,
        DateTime CreatedAt,
        DateTime? UpdatedAt
    );
}
