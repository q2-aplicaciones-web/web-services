using Q2.Web_Service.API.OrdersProcessing.Domain.Model.Aggregates;
using Q2.Web_Service.API.OrdersProcessing.Domain.Model.Queries;
using Q2.Web_Service.API.OrdersProcessing.Domain.Repositories;
using Q2.Web_Service.API.OrdersProcessing.Domain.Services;

namespace Q2.Web_Service.API.OrdersProcessing.Application.Internal.QueryServices;

public class OrderProcessingQueryService(IOrderProcessingRepository repository) : IOrderProcessingQueryService
{
    public async Task<IEnumerable<OrderProcessing>> Handle(GetOrdersByUserIdQuery query)
    {
        return await repository.GetAllOrdersByUserIdAsync(query.UserId);
    }

    public async Task<OrderProcessing?> Handle(GetOrderByIdQuery query)
    {
        return await repository.GetOrderByIdAsync(query.OrderId);
    }
}

