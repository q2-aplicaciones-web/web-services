using Q2.Web_Service.API.OrdersProcessing.Domain.Model.Commands;
using Q2.Web_Service.API.OrdersProcessing.Interfaces.REST.Resources;
using Q2.Web_Service.API.OrdersProcessing.Domain.Model.Entities;

namespace Q2.Web_Service.API.OrdersProcessing.Interfaces.REST.Transform;

public static class CreateOrderProcessingFromEntityAssembler
{
    public static CreateOrderCommand ToCommand(CreateOrderProcessingResource resource)
    {
        var items = resource.Items?.Select(i => new Item(i.ProductId, i.Quantity)).ToList() ?? new List<Item>();
        return new CreateOrderCommand(resource.UserId, items);
    }
}