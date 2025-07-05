using Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.OrderFulfillment.Domain.Model.Commands
{
    public record CancelFulfillmentItemCommand(FulfillmentItemId FulfillmentItemId);
}
