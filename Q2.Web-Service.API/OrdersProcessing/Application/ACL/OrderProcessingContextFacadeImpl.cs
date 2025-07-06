using System;
using System.Collections.Generic;
using System.Linq;
using Q2.Web_Service.API.OrdersProcessing.Domain.Model.Queries;
using Q2.Web_Service.API.OrdersProcessing.Domain.Services;
using Q2.Web_Service.API.OrdersProcessing.Interfaces.REST.ACL;

namespace Q2.Web_Service.API.OrdersProcessing.Application.ACL;

/// <summary>
/// Implementation of the facade for OrderProcessing context
/// </summary>
public class OrderProcessingContextFacadeImpl : IOrderProcessingContextFacade
{
    private readonly IOrderProcessingQueryService _orderQueryService;
    
    public OrderProcessingContextFacadeImpl(IOrderProcessingQueryService orderQueryService)
    {
        _orderQueryService = orderQueryService ?? throw new ArgumentNullException(nameof(orderQueryService));
    }

    public OrderDto? FetchOrderById(Guid orderId)
    {
        try
        {
            var query = new GetOrderByIdQuery(orderId);
            var order = _orderQueryService.Handle(query).GetAwaiter().GetResult();
            if (order == null) return null;
            
            var firstItemId = order.Items.FirstOrDefault()?.Id ?? Guid.Empty;
            return new OrderDto(order.Id, firstItemId);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public List<OrderDto> FetchOrdersByUserId(Guid userId)
    {
        try
        {
            var query = new GetOrdersByUserIdQuery(userId);
            var orders = _orderQueryService.Handle(query).GetAwaiter().GetResult();
            
            return orders.SelectMany(order => 
                order.Items.Select(item => new OrderDto(order.Id, item.Id))
            ).ToList();
        }
        catch (Exception)
        {
            return new List<OrderDto>();
        }
    }
}