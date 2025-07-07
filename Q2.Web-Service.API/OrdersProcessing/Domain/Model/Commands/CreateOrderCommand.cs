using Q2.Web_Service.API.OrdersProcessing.Domain.Model.Entities;

namespace Q2.Web_Service.API.OrdersProcessing.Domain.Model.Commands;
public record CreateOrderCommand(Guid UserId, List<Item> Items);
