
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Entities;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Commands;

namespace Q2.Web_Service.API.OrderFulfillment.Domain.Services
{
    public interface IFulfillmentItemCommandService
    {
        FulfillmentItem? Handle(MarkFulfillmentItemAsShippedCommand command);
        FulfillmentItem? Handle(MarkFulfillmentItemAsReceivedCommand command);
        FulfillmentItem? Handle(CancelFulfillmentItemCommand command);
    }
}
