using System.ComponentModel.DataAnnotations;

namespace Q2.Web_Service.API.OrdersProcessing.Interfaces.REST.Resources;

public class CreateOrderProcessingResource
{
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public List<CreateOrderProcessingItemResource> Items { get; set; } = new();
}

public class CreateOrderProcessingItemResource
{
    [Required]
    public Guid ProductId { get; set; }
    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public int Quantity { get; set; }
}