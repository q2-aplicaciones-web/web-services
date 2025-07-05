using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Aggregates;
using Q2.Web_Service.API.OrderFulfillment.Interfaces.REST.Resources;

namespace Q2.Web_Service.API.OrderFulfillment.Interfaces.REST.Transform
{
    public static class FulfillmentResourceFromEntityAssembler
    {
        public static FulfillmentResource ToResourceFromEntity(Fulfillment entity)
        {
            return new FulfillmentResource(
                entity.Id.ToString(),
                entity.OrderId.ToString(),
                entity.Status,
                entity.ReceivedDate,
                entity.ShippedDate,
                entity.GetManufacturerId()?.ToString(),
                entity.CreatedAt,
                entity.UpdatedAt
            );
        }
    }
}
