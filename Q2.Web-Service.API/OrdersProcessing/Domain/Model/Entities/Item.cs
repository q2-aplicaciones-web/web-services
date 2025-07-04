using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Q2.Web_Service.API.OrdersProcessing.Domain.Model.Entities;

public class Item
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; private set; }

    [Required]
    public Guid ProductId { get; private set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; private set; }

    // Foreign Key hacia OrderProcessing - EF Core manejará esto automáticamente
    public Guid OrderProcessingId { get; internal set; }

    protected Item() { }

    public Item(Guid productId, int quantity)
    {
        ProductId = productId != Guid.Empty ? productId : throw new ArgumentNullException(nameof(productId), "productId cannot be null");
        Quantity = quantity > 0 ? quantity : throw new ArgumentException("Quantity must be greater than 0", nameof(quantity));
    }

    // Constructor con ID explícito (para casos especiales)
    public Item(Guid? id, Guid productId, int quantity) : this(productId, quantity)
    {
        if (id.HasValue && id.Value != Guid.Empty)
        {
            Id = id.Value;
        }
    }
}