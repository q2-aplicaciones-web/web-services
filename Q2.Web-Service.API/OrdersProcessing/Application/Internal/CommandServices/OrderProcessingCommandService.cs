using Q2.Web_Service.API.OrdersProcessing.Domain.Model.Aggregates;
using Q2.Web_Service.API.OrdersProcessing.Domain.Model.Commands;
using Q2.Web_Service.API.OrdersProcessing.Domain.Repositories;
using Q2.Web_Service.API.OrdersProcessing.Domain.Services;
using Q2.Web_Service.API.Shared.Domain.Repositories;

namespace Q2.Web_Service.API.OrdersProcessing.Application.Internal.CommandServices;

public class OrderProcessingCommandService(IOrderProcessingRepository repository, IUnitOfWork unitOfWork) : IOrderProcessingCommandService
{
    public async Task<Guid?> Handle(CreateOrderCommand command)
    {
        try
        {
            var order = new OrderProcessing(command);
            await repository.AddAsync(order);
            await unitOfWork.CompleteAsync();
            
            // Here should be executed all events
            
            return order.Id;
        }
        catch (Exception ex)
        {
            // Log the error for debugging
            throw new Exception($"Error creating order: {ex.Message}", ex);
        }
    }

    public async Task Handle(ProcessOrderCommand command)
    {
        // Aquí puedes agregar lógica para procesar la orden (cambiar estado, etc.)
        var order = await repository.GetOrderByIdAsync(command.OrderId);
        if (order == null) throw new Exception($"Order not found: {command.OrderId}");
        
        // Lógica de procesamiento de pago, actualización de estado, etc.
        // await unitOfWork.CompleteAsync();
    }
}