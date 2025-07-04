namespace Q2.Web_Service.API.OrdersProcessing.Interfaces.REST.Resources;

public class OrderProcessingResource
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<ItemResource> Items { get; set; } = new();
}

public class ItemResource
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
