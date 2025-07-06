using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Q2.Web_Service.API.Shared.Domain.Model.ValueObjects;
using Q2.Web_Service.API.Shared.Infrastructure.Stripe.Configuration;
using Stripe;

namespace Q2.Web_Service.API.Shared.Infrastructure.Stripe.Services;

/// <summary>
/// Implementation of Stripe payment services
/// </summary>
public class StripeService : IStripeService
{
    private readonly ILogger<StripeService> _logger;
    private readonly PaymentIntentService _paymentIntentService;
    private readonly StripeSettings _stripeSettings;

    public StripeService(IOptions<StripeSettings> stripeSettings, ILogger<StripeService> logger, IStripeInitializer stripeInitializer)
    {
        _logger = logger;
        _stripeSettings = stripeSettings.Value;
        
        // Ensure Stripe is initialized
        stripeInitializer.Initialize();
        
        _paymentIntentService = new PaymentIntentService();
    }

    /// <summary>
    /// Creates a payment intent with the specified amount
    /// </summary>
    /// <param name="amount">The monetary amount for the payment intent</param>
    /// <returns>The created PaymentIntent from Stripe</returns>
    /// <exception cref="StripeException">Thrown when Stripe API call fails</exception>
    public async Task<PaymentIntent> CreatePaymentIntentAsync(Money amount)
    {
        if (amount == null)
            throw new ArgumentNullException(nameof(amount));

        try
        {
            _logger.LogInformation("Creating payment intent for amount: {Amount} {Currency}", 
                amount.Amount, amount.Currency);

            // Convert amount to cents (Stripe requires amounts in smallest currency unit)
            var amountInCents = Math.Max(50, (long)(amount.Amount * 100));

            var options = new PaymentIntentCreateOptions
            {
                Amount = amountInCents,
                Currency = amount.Currency.ToLower(),
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
            };

            var paymentIntent = await _paymentIntentService.CreateAsync(options);
            
            _logger.LogInformation("Payment intent created successfully: {PaymentIntentId}", paymentIntent.Id);
            
            return paymentIntent;
        }
        catch (StripeException ex)
        {
            _logger.LogError(ex, "Failed to create payment intent: {ErrorMessage}", ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error creating payment intent: {ErrorMessage}", ex.Message);
            throw new InvalidOperationException("An error occurred while creating the payment intent", ex);
        }
    }
}
