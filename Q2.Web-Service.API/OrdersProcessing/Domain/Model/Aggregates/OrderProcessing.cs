using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Q2.Web_Service.API.OrdersProcessing.Domain.Model.Entities;
using Q2.Web_Service.API.OrdersProcessing.Domain.Model.Commands; 


namespace Q2.Web_Service.API.OrdersProcessing.Domain.Model.Aggregates;

public class OrderProcessing
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; private set; }

    [Required]
    [Column("user_id")]
    public Guid UserId { get; private set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; private set; }

    public List<Item> Items { get; private set; } = new List<Item>();

    protected OrderProcessing()
    {
        Items = new List<Item>();
    }

    public OrderProcessing(CreateOrderCommand command)
    {
        UserId = command.UserId;
        CreatedAt = DateTime.UtcNow;
        Items = new List<Item>();
        
        if (command.Items != null)
        {
            foreach (var item in command.Items)
            {
                Items.Add(item);
            }
        }
    }

    // Métodos de dominio
    public void AddNewItem(Guid productId, int quantity)
    {
        var item = new Item(productId, quantity);
        Items.Add(item);
    }

    public decimal GetTotalQuantity()
    {
        return Items.Sum(i => i.Quantity);
    }

    public bool HasItems()
    {
        return Items.Any();
    }

    public int GetItemCount()
    {
        return Items.Count;
    }
}