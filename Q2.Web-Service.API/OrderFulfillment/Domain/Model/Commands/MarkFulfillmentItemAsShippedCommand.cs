using Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.OrderFulfillment.Domain.Model.Commands
{
    // Simple record, validation should be handled elsewhere (e.g., service layer)
    public record MarkFulfillmentItemAsShippedCommand(FulfillmentItemId FulfillmentItemId);
}
