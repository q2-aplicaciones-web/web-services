using System;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.OrderFulfillment.Interfaces.REST.Resources
{
    public record FulfillmentItemResource(
        string Id,
        string ProductId,
        int Quantity,
        FulfillmentItemStatus Status,
        Guid FulfillmentId
    );
}
