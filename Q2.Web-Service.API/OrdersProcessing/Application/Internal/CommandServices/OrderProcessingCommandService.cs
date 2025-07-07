using Q2.Web_Service.API.OrdersProcessing.Domain.Model.Aggregates;
using Q2.Web_Service.API.OrdersProcessing.Domain.Model.Commands;
using Q2.Web_Service.API.OrdersProcessing.Domain.Repositories;
using Q2.Web_Service.API.OrdersProcessing.Domain.Services;
using Q2.Web_Service.API.Shared.Domain.Repositories;
using Q2.Web_Service.API.Shared.Infrastructure.Stripe;
using Q2.Web_Service.API.Shared.Infrastructure.Stripe.Exceptions;
using Microsoft.Extensions.Logging;
using Stripe;

namespace Q2.Web_Service.API.OrdersProcessing.Application.Internal.CommandServices;

public class OrderProcessingCommandService(
    IOrderProcessingRepository repository, 
    IUnitOfWork unitOfWork, 
    IStripeService stripeService,
    ILogger<OrderProcessingCommandService> logger) : IOrderProcessingCommandService
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

    public async Task<PaymentIntent> Handle(CreatePaymentIntentCommand command)
    {
        logger.LogInformation("Creating payment intent for amount: {Amount} {Currency}", 
            command.Amount.Amount, command.Amount.Currency);
        
        try
        {
            var paymentIntent = await stripeService.CreatePaymentIntentAsync(command.Amount);
            
            logger.LogInformation("Payment intent created successfully: {PaymentIntentId}", paymentIntent.Id);
            
            return paymentIntent;
        }
        catch (StripeException ex)
        {
            logger.LogError(ex, "Failed to create payment intent: {ErrorMessage}", ex.Message);
            
            // Map Stripe exception to domain exception
            var domainException = StripeExceptionMapper.MapToDomainException(ex);
            throw new InvalidOperationException($"Error creating payment intent: {domainException.Message}", domainException);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error creating payment intent: {ErrorMessage}", ex.Message);
            throw new InvalidOperationException($"Unexpected error creating payment intent: {ex.Message}", ex);
        }
    }
}