using System;

namespace Q2.Web_Service.API.OrderFulfillment.Interfaces.REST.Resources
{
    public record CreateFulfillmentResource(
        string OrderId,
        string ManufacturerId
    );
}
