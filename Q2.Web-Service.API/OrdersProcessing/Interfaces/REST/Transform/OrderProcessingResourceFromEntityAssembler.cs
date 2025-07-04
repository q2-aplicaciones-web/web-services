using Q2.Web_Service.API.OrdersProcessing.Domain.Model.Aggregates;
using Q2.Web_Service.API.OrdersProcessing.Interfaces.REST.Resources;

namespace Q2.Web_Service.API.OrdersProcessing.Interfaces.REST.Transform;

public static class OrderProcessingResourceFromEntityAssembler
{
    public static OrderProcessingResource ToResourceFromEntity(OrderProcessing entity)
    {
        return new OrderProcessingResource
        {
            Id = entity.Id,
            UserId = entity.UserId,
            CreatedAt = entity.CreatedAt,
            Items = entity.Items.Select(ItemResourceFromEntityAssembler.ToResourceFromEntity).ToList()
        };
    }

    public static IEnumerable<OrderProcessingResource> ToResourceFromEntity(IEnumerable<OrderProcessing> entities)
    {
        return entities.Select(ToResourceFromEntity);
    }
}

public static class ItemResourceFromEntityAssembler
{
    public static ItemResource ToResourceFromEntity(Domain.Model.Entities.Item entity)
    {
        return new ItemResource
        {
            Id = entity.Id,
            ProductId = entity.ProductId,
            Quantity = entity.Quantity
        };
    }
}
