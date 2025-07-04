using Q2.Web_Service.API.OrdersProcessing.Domain.Model.Aggregates;
using Q2.Web_Service.API.OrdersProcessing.Domain.Model.Queries;

namespace Q2.Web_Service.API.OrdersProcessing.Domain.Services;

public interface IOrderProcessingQueryService
{
    Task<IEnumerable<OrderProcessing>> Handle(GetOrdersByUserIdQuery query);
    Task<OrderProcessing?> Handle(GetOrderByIdQuery query);
}