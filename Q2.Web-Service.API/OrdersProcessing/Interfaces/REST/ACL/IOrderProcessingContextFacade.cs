using System;
using System.Collections.Generic;

namespace Q2.Web_Service.API.OrdersProcessing.Interfaces.REST.ACL;

/// <summary>
/// Facade para acceso a información de órdenes desde otros bounded contexts
/// </summary>
public interface IOrderProcessingContextFacade
{
    /// <summary>
    /// Obtiene una orden por su ID
    /// </summary>
    OrderDto? FetchOrderById(Guid orderId);
    
    /// <summary>
    /// Obtiene todas las órdenes de un usuario específico
    /// </summary>
    List<OrderDto> FetchOrdersByUserId(Guid userId);
}

/// <summary>
/// DTO simplificado para transferencia de datos de órdenes entre contextos
/// </summary>
public record OrderDto(
    Guid Id,        // Order ID
    Guid ItemId     // Specific order item ID
);