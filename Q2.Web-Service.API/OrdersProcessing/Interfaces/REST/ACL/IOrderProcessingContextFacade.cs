using System;
using System.Collections.Generic;

namespace Q2.Web_Service.API.OrdersProcessing.Interfaces.REST.ACL;

/// <summary>
/// Facade for accessing order information from other bounded contexts
/// </summary>
public interface IOrderProcessingContextFacade
{
    /// <summary>
    /// Gets an order by its ID
    /// </summary>
    OrderDto? FetchOrderById(Guid orderId);
    
    /// <summary>
    /// Gets all orders for a specific user
    /// </summary>
    List<OrderDto> FetchOrdersByUserId(Guid userId);
}

/// <summary>
/// Simplified DTO for order data transfer between contexts
/// </summary>
public record OrderDto(
    Guid Id,        // Order ID
    Guid ItemId     // Specific order item ID
);