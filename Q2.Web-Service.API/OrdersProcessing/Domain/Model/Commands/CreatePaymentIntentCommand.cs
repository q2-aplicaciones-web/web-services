using Q2.Web_Service.API.Shared.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.OrdersProcessing.Domain.Model.Commands;

/// <summary>
/// Command to create a payment intent for an order
/// </summary>
/// <param name="Amount">The monetary amount for the payment intent</param>
public record CreatePaymentIntentCommand(Money Amount);
