using Q2.Web_Service.API.OrdersProcessing.Domain.Model.Aggregates;
using Q2.Web_Service.API.OrdersProcessing.Domain.Model.Commands;

namespace Q2.Web_Service.API.OrdersProcessing.Domain.Services;

public interface IOrderProcessingCommandService
{
    Task<Guid?> Handle(CreateOrderCommand command);
    Task Handle(ProcessOrderCommand command);
}