using System;

namespace Q2.Web_Service.API.OrderFulfillment.Domain.Model.Queries
{
    // Simple record, validation should be handled elsewhere (e.g., service layer)
    public record GetAllFulfillmentItemsByFulfillmentIdQuery(Guid FulfillmentId);
}
