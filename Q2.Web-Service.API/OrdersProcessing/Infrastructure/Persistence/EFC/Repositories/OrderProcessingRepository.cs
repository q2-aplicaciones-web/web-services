using Q2.Web_Service.API.OrdersProcessing.Domain.Model.Aggregates;
using Q2.Web_Service.API.OrdersProcessing.Domain.Repositories;
using Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace Q2.Web_Service.API.OrdersProcessing.Infrastructure.Persistence.EFC.Repositories;

public class OrderProcessingRepository(AppDbContext context) : BaseRepository<OrderProcessing>(context), IOrderProcessingRepository
{
    public async Task<IEnumerable<OrderProcessing>> GetAllOrdersByUserIdAsync(Guid userId)
    {
        return await Context
            .Set<OrderProcessing>()
            .Include(order => order.Items)
            .Where(order => order.UserId == userId)
            .OrderByDescending(order => order.CreatedAt)
            .ToListAsync();
    }

    public async Task<OrderProcessing?> GetOrderByIdAsync(Guid orderId)
    {
        return await Context
            .Set<OrderProcessing>()
            .Include(order => order.Items)
            .FirstOrDefaultAsync(order => order.Id == orderId);
    }
}