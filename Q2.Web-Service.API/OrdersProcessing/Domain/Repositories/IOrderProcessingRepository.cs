using Q2.Web_Service.API.OrdersProcessing.Domain.Model.Aggregates;
using Q2.Web_Service.API.Shared.Domain.Repositories;

namespace Q2.Web_Service.API.OrdersProcessing.Domain.Repositories;

public interface IOrderProcessingRepository : IBaseRepository<OrderProcessing>
{
    Task<IEnumerable<OrderProcessing>> GetAllOrdersByUserIdAsync(Guid userId);
    Task<OrderProcessing?> GetOrderByIdAsync(Guid orderId);
}