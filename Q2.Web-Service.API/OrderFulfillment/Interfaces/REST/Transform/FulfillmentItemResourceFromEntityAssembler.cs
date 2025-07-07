using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Entities;
using Q2.Web_Service.API.OrderFulfillment.Interfaces.REST.Resources;

namespace Q2.Web_Service.API.OrderFulfillment.Interfaces.REST.Transform
{
    public static class FulfillmentItemResourceFromEntityAssembler
    {
        public static FulfillmentItemResource ToResourceFromEntity(FulfillmentItem entity)
        {
            return new FulfillmentItemResource(
                entity.Id.ToString(),
                entity.ProductId.ToString(),
                entity.Quantity,
                entity.Status,
                entity.FulfillmentId
            );
        }
    }
}
