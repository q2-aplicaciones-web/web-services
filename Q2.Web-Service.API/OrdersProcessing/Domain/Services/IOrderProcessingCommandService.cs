using Q2.Web_Service.API.OrdersProcessing.Domain.Model.Aggregates;
using Q2.Web_Service.API.OrdersProcessing.Domain.Model.Commands;
using Stripe;

namespace Q2.Web_Service.API.OrdersProcessing.Domain.Services;

public interface IOrderProcessingCommandService
{
    Task<Guid?> Handle(CreateOrderCommand command);
    Task Handle(ProcessOrderCommand command);
    Task<PaymentIntent> Handle(CreatePaymentIntentCommand command);
}