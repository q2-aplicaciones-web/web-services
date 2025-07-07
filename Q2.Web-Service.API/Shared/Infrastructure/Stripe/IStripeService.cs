using Q2.Web_Service.API.Shared.Domain.Model.ValueObjects;
using Stripe;

namespace Q2.Web_Service.API.Shared.Infrastructure.Stripe;

/// <summary>
/// Interface for Stripe payment services
/// </summary>
public interface IStripeService
{
    /// <summary>
    /// Creates a payment intent with the specified amount
    /// </summary>
    /// <param name="amount">The monetary amount for the payment intent</param>
    /// <returns>The created PaymentIntent from Stripe</returns>
    /// <exception cref="StripeException">Thrown when Stripe API call fails</exception>
    Task<PaymentIntent> CreatePaymentIntentAsync(Money amount);
}
